using System;
using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.DrawingTools.Tools
{
    internal class NCornerStar : DrawingTool
    {
        private readonly int corners;
        private PointF point;

        public NCornerStar(Pen paintbrush, bool isDash, int _corners) : base(paintbrush, isDash) {
            corners = _corners;
        }

        public override void PreShowing(ICanvas canvas, PointF _point) {
            point = _point;
            canvas.ExecuteChanging();
        }

        public override void PrepareToDrawing(ICanvas canvas, PointF point) {
            OldPoint = point;
            IsDrawing = true;
        }

        public override void Draw(ICanvas canvas, PointF point) {
            if (!IsDrawing) return;

            var graphics = GetGraphics(canvas.ShowedPicture);

            graphics.DrawLines(Paintbrush, FindCoordinates());

            graphics.Dispose();
            canvas.ExecuteChanging();
        }

        public override void EndDrawing(ICanvas canvas, PointF point) {
            IsDrawing = false;
        }

        public override void PreShowDrawing(ICanvas canvas, Graphics preShowGraphics) {
            if (!IsDrawing) return;

            preShowGraphics.DrawLines(Paintbrush, FindCoordinates());
        }

        private PointF[] FindCoordinates() {
            var n = corners; // number figure
            double R = Math.Abs((point.X - OldPoint.X) / 2);
            double r = Math.Abs(point.X - OldPoint.X); // R
            double alpha = 55; // corner
            double x0 = OldPoint.X;
            double y0 = OldPoint.Y; // middle

            var points = new PointF[2 * n + 1];
            var a = alpha;
            var da = Math.PI / n;
            double l;
            for (var k = 0; k < 2 * n + 1; k++) {
                l = k % 2 == 0 ? r : R;
                points[k] = new PointF((float) (x0 + l * Math.Cos(a)),
                    (float) (y0 + l * Math.Sin(a)));
                a += da;
            }

            return points;
        }
    }
}