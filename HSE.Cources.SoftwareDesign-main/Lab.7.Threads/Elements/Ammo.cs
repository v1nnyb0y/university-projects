using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab._7.Threads.Elements
{
    public class Ammo : IDisposable
    {
        #region Fields

        private bool _disposed = false;
        private readonly Tank _sourceTank;
        public Bitmap _imgAmmo;
        public Point Position;
        private readonly EnumDirection _direction;
        public bool Died;

        #endregion

        #region Initialize

        ~Ammo() {
            Dispose
                (
                 false
                );
        }

        public Ammo
        (
            Tank source,
            Point position,
            EnumDirection direction
        ) {
            _direction = direction;
            _sourceTank = source;
            Position = position;
            _imgAmmo = new Bitmap("Resources/ammo.bmp");
        }

        #endregion

        #region Actions

        public int Move(List<Tank> positionTanks) {
            if (Died) return -1;

            switch (_direction)
                {
                    case EnumDirection.Up:
                        int y = Position.Y - 20;
                        if (y - 30 >= 650 ||
                            y - 30 <= -50)
                            Died = true;

                        int id = IsSavePlace
                            (
                             Position.X,
                             y,
                             positionTanks
                            );
                        if (id != -1) {
                            _sourceTank.Score = Interlocked.Increment(ref _sourceTank.Score);
                            Died = true;
                            _imgAmmo = new Bitmap(Properties.Resources.BG);
                        return id;
                        }
                        Position.Y = y;
                        break;
                    case EnumDirection.Down:
                        y = Position.Y + 20;
                        if (y + 30 >= 650 || y + 30 <= -50)
                            Died = true;
                        id = IsSavePlace
                            (
                             Position.X,
                             y,
                             positionTanks
                            );
                        if (id != -1) {
                        _sourceTank.Score = Interlocked.Increment(ref _sourceTank.Score);
                          Died = true;
                          _imgAmmo = new Bitmap(Properties.Resources.BG);
                        return id;
                        }
                        Position.Y = y;
                        break;
                    case EnumDirection.Left:
                        int x = Position.X - 20;
                        if (x - 30 >= 1050 || x - 30 <= -50)
                            Died = true;
                        id = IsSavePlace
                            (
                             x,
                             Position.Y,
                             positionTanks
                            );
                        if (id != -1) {
                        _sourceTank.Score = Interlocked.Increment(ref _sourceTank.Score);
                        Died = true;
                        _imgAmmo = new Bitmap(Properties.Resources.BG);
                        return id;
                        }
                        Position.X = x;
                        break;
                    case EnumDirection.Right:
                        x = Position.X + 20;
                        if (x + 30 >= 1050 || x + 30 <= -50)
                            Died = true;
                        id = IsSavePlace
                            (
                             x,
                             Position.Y,
                             positionTanks
                            );
                        if (id != -1) {
                        _sourceTank.Score = Interlocked.Increment(ref _sourceTank.Score);
                        Died = true;
                        _imgAmmo = new Bitmap(Properties.Resources.BG);
                        return id;
                        }
                        Position.X = x;
                        break;
                }

            return -1;
        }

        private int IsSavePlace(int x, int y, List<Tank> positionTanks)
        {
            for (int index = 0;
                 index < positionTanks.Count;
                 ++index) {
                if (Math.Abs
                        (
                         x -
                         positionTanks[index].Position.X
                        ) <=
                    25 &&
                    Math.Abs
                        (
                         y -
                         positionTanks[index].Position.Y
                        ) <=
                    25) {
                    return index;
                }
            }

            return -1;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }

        #endregion
    }
}
