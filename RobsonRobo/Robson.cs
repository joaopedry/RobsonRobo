using Robocode;
using Robocode.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RobsonRobo
{
    public class Robson : AdvancedRobot
    {
        //Variaveis
        private int distancia = 50;
        private int count = 0;
        int Perdido = 0;
        //Executando enquanto viver
        public override void Run()
        {
            //Cor da bala
            BulletColor = (Color.Red);
            //Cor do Corpo
            BodyColor = (Color.Black);
            //Cor da Arma
            GunColor = (Color.Orange);
            //Cor do Radar
            RadarColor = (Color.White);
            //Cor do Scan
            ScanColor = (Color.Blue);

            while (true)
            {
                SetTurnGunRight(20);
                Execute();
            }
        }
        //Inimigo scaneado
        public override void OnScannedRobot(ScannedRobotEvent e)
        {

            double absoluteBearing = Heading + e.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);

            if (Math.Abs(bearingFromGun) <= 3)
            {
                TurnGunRight(bearingFromGun);

                if (GunHeat == 0)
                {
                    Fire(Math.Min(3 - Math.Abs(bearingFromGun), Energy - .1));
                }
            }
            else
            {

                TurnGunRight(bearingFromGun);
            }

            if (bearingFromGun == 0)
            {
                Scan();
            }

        }
        //Se for atingido por uma bala
        public override void OnHitByBullet(HitByBulletEvent e)
        {
            SetAhead(100);
            SetTurnRight(90);
            Execute();
        }
        //Quando acertar robo
        public override void OnHitRobot(HitRobotEvent e)
        {
            SetBack(15);
            Fire(3);
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            SetBack(100);
            SetTurnRight(90);
            Execute();
        }


        public override void OnBulletMissed(BulletMissedEvent e)
        {

            if (Perdido > 2)
            {
                Perdido = 0;
                MudarPosicao();
            }

            Perdido++;
        }

        public override void OnBulletHit(BulletHitEvent e)
        {
            Perdido = 0;
            Fire(1);
        }

        public void MudarPosicao()
        {
            TurnLeft(90);
            Ahead(150);
        }
        public override void OnBulletHitBullet(BulletHitBulletEvent evnt)
        {
            SetTurnLeft(45);
            SetAhead(150);
            Execute();
        }
    }
}
