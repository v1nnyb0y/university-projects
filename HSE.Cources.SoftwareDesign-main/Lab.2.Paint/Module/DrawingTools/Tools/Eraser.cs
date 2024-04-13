using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.DrawingTools.Tools
{
    internal class Eraser : DrawingTool
    {
        public Eraser(Pen paintbrush, bool isDash) : base(paintbrush, isDash) { }

        public override void PreShowing(ICanvas canvas, PointF point) { }

        public override void PrepareToDrawing(ICanvas canvas, PointF point) {
            Paintbrush.Color = Color.White;
            OldPoint = point;
            IsDrawing = true;
        }

        public override void Draw(ICanvas canvas, PointF point) {
            if (!IsDrawing) return;

            var graphics = GetGraphics(canvas.ShowedPicture);
            graphics.DrawLine(Paintbrush, OldPoint, point);
            OldPoint = point;

            graphics.Dispose();
            canvas.ExecuteChanging();
        }

        public override void EndDrawing(ICanvas canvas, PointF point) {
            IsDrawing = false;
        }

        public override void PreShowDrawing(ICanvas canvas, Graphics preShowGraphics) { }
    }
}