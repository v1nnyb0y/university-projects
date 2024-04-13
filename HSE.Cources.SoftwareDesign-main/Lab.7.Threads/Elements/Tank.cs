using System;
using System.Collections.Generic;
using System.Drawing;
using Timer = System.Threading.Timer;

namespace Lab._7.Threads.Elements
{
    #region Enum

    public enum EnumDirection
    {
        Up, Down, Left, Right
    }

    #endregion

    public abstract class Tank
    {
        private void Reload
        (
            object state
        ) {
            if (!ReadyToShoot) {
                ReadyToShoot = true;
            }
        }

        #region Fields

        public int Index;
        private Timer timer;
        public Bitmap ImgTank;
        public int Score;
        public Point Position;

        private bool _died;
        public bool Died
            {
                get => _died;
                set {
                    _died = value;
                    if (!_died) return;

                    lock (ImgTank) {
                        ImgTank = new Bitmap("Resources/died.jpg");
                    }
                }
            }
        private EnumDirection _direction;
        public bool ReadyToShoot = true;
        public readonly List<Ammo> ListAmmo = new List<Ammo>();

        #endregion

        #region Initialize

        protected Tank(
            Point createdPosition,
            Bitmap imgTank,
            int id
        ) {
            Died = false;
            Score = 0;
            Position = createdPosition;
            ImgTank = imgTank;
            _direction = EnumDirection.Up;
            timer = new Timer(Reload, null, 0, 1000);
            Index = id;
        }

        #endregion

        #region Movements

        private bool isSavePlace(int x, int y, List<Point> positionTanks) {
            foreach (var point in positionTanks)
            {
                if (Math.Abs
                        (
                         point.X - x
                        ) <=
                    32 &&
                    Math.Abs
                        (
                         point.Y - y
                        ) <=
                    32) {
                    return false;
                }
            }

            return true;
        }

        private bool CanMove
        (
            int x,
            int y
        ) {
            return (x <= 985 && x >= 0 && y >= 0 && y <= 550);
        }

        /// <summary>
        /// One cell front
        /// </summary>
        protected internal void Move(List<Point> positionTanks) {
            lock (this) {
                if (Died) return;

                switch (_direction)
                {
                    case EnumDirection.Up:
                        int y = Position.Y - 10;
                        if (CanMove(Position.X, y) && isSavePlace(Position.X, y, positionTanks))
                            Position.Y = y;
                        break;
                    case EnumDirection.Down:
                        y = Position.Y + 10;
                        if (CanMove(Position.X, y + 30) && isSavePlace(Position.X, y, positionTanks))
                            Position.Y = y;
                        break;
                    case EnumDirection.Left:
                        int x = Position.X - 10;
                        if (CanMove(x, Position.Y) && isSavePlace(x, Position.Y, positionTanks))
                            Position.X = x;
                        break;
                    case EnumDirection.Right:
                        x = Position.X + 10;
                        if (CanMove(x + 30, Position.Y) && isSavePlace(x, Position.Y, positionTanks))
                            Position.X = x;
                        break;
                }
            }
        }

        /// <summary>
        /// Turn left
        /// </summary>
        protected internal void TurnLeft() {
            lock (ImgTank) {
                if (Died) return;

                switch (_direction)
                {
                    case EnumDirection.Up:
                        _direction = EnumDirection.Left;
                        break;
                    case EnumDirection.Down:
                        _direction = EnumDirection.Right;
                        break;
                    case EnumDirection.Left:
                        _direction = EnumDirection.Down;
                        break;
                    case EnumDirection.Right:
                        _direction = EnumDirection.Up;
                        break;
                }
                ImgTank.RotateFlip(RotateFlipType.Rotate90FlipXY);
            }
        }

        /// <summary>
        /// Turn right
        /// </summary>
        protected internal void TurnRight() {
            lock (ImgTank) {
                if (Died) return;

                switch (_direction)
                {
                    case EnumDirection.Up:
                        _direction = EnumDirection.Right;
                        break;
                    case EnumDirection.Down:
                        _direction = EnumDirection.Left;
                        break;
                    case EnumDirection.Left:
                        _direction = EnumDirection.Up;
                        break;
                    case EnumDirection.Right:
                        _direction = EnumDirection.Down;
                        break;
                }
                ImgTank.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        #endregion

        #region Shoot

        protected internal void Shoot() {
            lock (ListAmmo) {
                if (Died) return ;

                ReadyToShoot = false;
                var ammoStartPoint = new Point
                    (
                     Position.X,
                     Position.Y
                    );

                switch (_direction)
                {
                    case EnumDirection.Up:
                        ammoStartPoint.Y -= 25;
                        ammoStartPoint.X += 25;
                        break;
                    case EnumDirection.Down:
                        ammoStartPoint.Y += 55;
                        ammoStartPoint.X += 25;
                        break;
                    case EnumDirection.Left:
                        ammoStartPoint.X -= 25;
                        ammoStartPoint.Y += 25;
                        break;
                    case EnumDirection.Right:
                        ammoStartPoint.X += 55;
                        ammoStartPoint.Y += 25;
                        break;
                }

                ListAmmo.Add(new Ammo
                    (
                     this,
                     ammoStartPoint,
                     _direction
                    ));
            }
        }

        #endregion
    }
}
