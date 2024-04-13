using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.DrawingTools.Tools
{
    public class MagicalEraser : DrawingTool
    {
        public int Width;

        public MagicalEraser(Pen paintbrush, bool isDash) : base(paintbrush, isDash) { }

        public override void PreShowing(ICanvas canvas, PointF point) { }

        public override void PrepareToDrawing(ICanvas canvas, PointF point) {
            IsDrawing = true;
            Width = (int) Paintbrush.Width + 4;
        }

        public override void Draw(ICanvas canvas, PointF point) {
            if (!IsDrawing) return;
            for (var i = Width / -2;
                i < Width &&
                OldPoint.X + i < canvas.OriginPicture.Width &&
                OldPoint.X + i < canvas.ShowedPicture.Width &&
                OldPoint.X + i >= 0;
                i++)
            for (var j = Width / -2;
                j < Width &&
                OldPoint.Y + j < canvas.OriginPicture.Height &&
                OldPoint.Y + j < canvas.ShowedPicture.Height &&
                OldPoint.Y + j >= 0;
                j++) {
                var pxl = canvas.OriginPicture.GetPixel((int) OldPoint.X + i,
                    (int) OldPoint.Y + j);

                canvas.ShowedPicture.SetPixel((int) OldPoint.X + i,
                    (int) OldPoint.Y + j, pxl);
            }

            OldPoint = point;
            canvas.ExecuteChanging();
        }

        public override void EndDrawing(ICanvas canvas, PointF point) {
            IsDrawing = false;
        }

        public override void PreShowDrawing(ICanvas canvas, Graphics preShowGraphics) { }
    }
}