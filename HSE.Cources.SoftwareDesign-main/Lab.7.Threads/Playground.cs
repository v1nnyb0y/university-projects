using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab._7.Threads.Elements;

namespace Lab._7.Threads
{
    public enum Priority
    {
        Lowest, BelowNormal, Normal, AboveNormal, Highest
    }

    public partial class Playground : Form
    {
        #region Fields

        private readonly Dictionary<string, Thread> _dictionaryThreads = new Dictionary<string, Thread>();
        private readonly List<EnemyTank> _enemyTanks;
        private readonly PlayedTank _myTank;
        private readonly Priority _priority;

        #endregion

        /// <summary>
        /// Initialize form
        /// </summary>
        public Playground(Priority priority)
        {
            InitializeComponent();

            _myTank = new PlayedTank
                (
                 new Point
                     (
                      50,
                      50
                     )
                );
            _enemyTanks = new List<EnemyTank>()
                             {
                                 new EnemyTank(new Point(500, 500), 0),
                                 new EnemyTank(new Point(350, 400), 1 ),
                                 new EnemyTank(new Point(700, 50), 2)
                             };
            _priority = priority;
        }

        private void RefreshWindow(object sender, PaintEventArgs e) {
            e.Graphics.DrawImage
                (
                _myTank.ImgTank,
                 _myTank.Position.X,
                 _myTank.Position.Y,
                 50,
                 50
                );
            foreach (var ammo in _myTank.ListAmmo.Where(ammo => ammo.Died == false))
            {
                e.Graphics.DrawImage
                    (
                     ammo._imgAmmo,
                     ammo.Position.X,
                     ammo.Position.Y,
                     10,
                     10
                    );
            }

            foreach (var enemyTank in _enemyTanks) {
                e.Graphics.DrawImage
                    (
                     enemyTank.ImgTank,
                     enemyTank.Position.X,
                     enemyTank.Position.Y,
                     50,
                     50
                    );
                for (var id = 0;
                     id < enemyTank.ListAmmo.Count;
                     id++) {
                    e.Graphics.DrawImage
                        (
                         enemyTank.ListAmmo[id]._imgAmmo,
                         enemyTank.ListAmmo[id].Position.X,
                         enemyTank.ListAmmo[id].Position.Y,
                         10,
                         10
                        );
                }
            }
        }

        private void ChangeWay(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    _myTank.TurnLeft();
                    break;
                case Keys.Right:
                    _myTank.TurnRight();
                    break;
                case Keys.Space:
                    if (_myTank.ReadyToShoot) {
                        _myTank.Shoot();
                    }
                    break;
            }
        }

        private void TickMovement(object sender, EventArgs e) {
            _myTank.Move(_enemyTanks.Select(tank=>tank.Position).ToList());
            foreach (var ammo in _myTank.ListAmmo) {
                var tanks = _enemyTanks.Where
                                        (
                                         tank => tank.Died == false
                                        )
                                       .ToList<Tank>();
                var id = ammo.Move(tanks);
                if (id == -1) continue;

                _enemyTanks[tanks[id].Index]
                   .Died = true;
                try {
                    _dictionaryThreads[$"Tank AI #{tanks[id].Index + 1}"].Abort();
                }
                catch {
                    // ignored
                }
                if (_enemyTanks.All
                    (
                     tank => tank.Died
                    ))
                {
                    Close();
                    return;
                }
            }

            foreach (var enemyTank in _enemyTanks) {
                var list = _enemyTanks.Where
                                       (
                                        tank => enemyTank.Index != tank.Index
                                       ).
                                      ToList<Tank>();
                list.Add
                    (
                     _myTank
                    );
                                      
                enemyTank.Move
                    (
                     list.Select(tank => tank.Position).ToList()
                    );
                var tmpList = enemyTank.ListAmmo;
                foreach (var ammo in tmpList) {
                    var id = ammo.Move
                        (
                         list
                        );
                    if (id == -1) continue;
                    if (list[id].Index == -2) {
                        Close();
                        return;
                    }

                    _enemyTanks[list[id]
                                   .Index]
                       .Died = true;
                    _dictionaryThreads[$"Tank AI #{list[id].Index + 1}"].Abort();
                    if (_enemyTanks.All
                        (
                         tank => tank.Died
                        ))
                    {
                        Close();
                        return;
                    }
                }
            }

            Invalidate();
        }

        private void StartGame(object sender, EventArgs e)
        {
            foreach (var enemy in _enemyTanks) {
                var thread = new Thread
                         (
                          enemy.Play
                         )
                             {
                                 IsBackground = true,
                                 Name         = $"Tank AI #{enemy.Index + 1}"
                             };

                switch (_priority) {
                    case Priority.Lowest: thread.Priority = ThreadPriority.Lowest;
                        break;
                    case Priority.BelowNormal: thread.Priority = ThreadPriority.BelowNormal;
                        break;
                    case Priority.Normal: thread.Priority = ThreadPriority.Normal;
                        break;
                    case Priority.AboveNormal: thread.Priority = ThreadPriority.AboveNormal;
                        break;
                    case Priority.Highest: thread.Priority = ThreadPriority.Highest;
                        break;
                }
                
                _dictionaryThreads.Add(thread.Name, thread);
            }
            foreach (var index in _enemyTanks.Select(tank => tank.Index))
                _dictionaryThreads[$"Tank AI #{index + 1}"].Start();
        }

        private void EndGame(object sender, FormClosingEventArgs e)
        {
            foreach(var thread in _enemyTanks.Where(tank => tank.Died == false))
                _dictionaryThreads[$"Tank AI #{thread.Index + 1}"].Abort();
            ((StartWindow) Owner).AddInfoStat
                (
                 $"Game ended with the score = {_myTank.Score}"
                );
            Dispose();
        }
    }
}
