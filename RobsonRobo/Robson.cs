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
                SetTurnRight(10000);
                // Limit our speed to 5
                MaxVelocity = 5;
                // Start moving (and turning)
                Ahead(10000);
            }
        }
        //Inimigo scaneado
        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            //Se distancia for menor que 50 e energia maior que 50, usar fire 3
            if (e.Distance < 50 && Energy > 50)
            {
                Fire(3);
                Back(90);
            }
            //Se não usa fire 1
            else
            {
                count = count + 1;
                if (count % 2 == 0)
                {
                    Fire(1);
                    Fire(1);
                }
                else
                {
                    Fire(2);
                }
            }
            //depois de atirar scannear novamente
            Scan();
        }
        //Se for atingido por uma bala
        public override void OnHitByBullet(HitByBulletEvent e)
        {
            TurnRight(Utils.NormalRelativeAngleDegrees(90 - (Heading - e.Heading)));

            Ahead(distancia);
            distancia *= -1;
            Scan();
            
        }
        //Quando acertar robo
        public override void OnHitRobot(HitRobotEvent e)
        {
            double turnGunAmt = Utils.NormalRelativeAngleDegrees(e.Bearing + Heading - GunHeading);

            TurnGunRight(turnGunAmt);
            Fire(2);
        }
        public override void OnBulletMissed(BulletMissedEvent e)
        {

            if (Perdido > 10)
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
    }
}
