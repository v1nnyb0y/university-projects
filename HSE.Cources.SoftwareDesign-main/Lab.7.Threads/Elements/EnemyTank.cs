using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab._7.Threads.Elements
{
    [Synchronization(false)]
    class EnemyTank:Tank
    {
        

    public EnemyTank
        (
            Point createdPosition,
            int   id
        )
            : base
                (
                 createdPosition,
                 new Bitmap
                     (
                      "Resources/tankusual.png"
                     ),
                 id
                ) { }

        #region Play

        public void Play()
        {
            while (true) {
                if (Died) return;

                Random rnd = new Random();
                int task = rnd.Next
                    (
                     1,
                     10
                    );
                switch (task) {
                    case 1:
                    case 2:
                    case 3:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        if (ReadyToShoot)
                            Shoot();
                        break;
                    case 4:
                        TurnRight();
                        break;
                    case 5:
                        TurnLeft();
                        break;
                }
                Thread.Sleep(250);
            }
        }

        #endregion
    }
}
