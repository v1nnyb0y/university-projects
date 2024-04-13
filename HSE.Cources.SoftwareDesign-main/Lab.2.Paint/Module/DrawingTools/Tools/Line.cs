using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.DrawingTools.Tools
{
    internal class Line : DrawingTool

    {
        private PointF point;
        public Line(Pen paintbrush, bool isDash) : base(paintbrush, isDash) { }

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

            graphics.DrawLine(Paintbrush, OldPoint, point);

            graphics.Dispose();
            canvas.ExecuteChanging();
        }

        public override void EndDrawing(ICanvas canvas, PointF point) {
            IsDrawing = false;
        }

        public override void PreShowDrawing(ICanvas canvas, Graphics preShowGraphics) {
            if (!IsDrawing) return;

            preShowGraphics.DrawLine(Paintbrush, OldPoint, point);
        }
    }
}