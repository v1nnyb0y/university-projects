using System.Drawing;
using System.Drawing.Drawing2D;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.DrawingTools
{
    public abstract class DrawingTool
    {
        public DrawingTool(Pen paintbrush, bool isDash) {
            if (isDash)
                Paintbrush = paintbrush;
            else
                Paintbrush = new Pen(paintbrush.Color, paintbrush.Width)
                    {
                        EndCap = paintbrush.EndCap,
                        StartCap = paintbrush.StartCap,
                        DashStyle = DashStyle.Solid
                    };
        }

        public Pen Paintbrush { get; protected set; }

        protected PointF OldPoint { get; set; }

        protected bool IsDrawing { get; set; }

        protected Graphics GetGraphics(Bitmap bitmap) {
            var graphics = Graphics.FromImage(bitmap);

            if (DataCore.isSmoothing) graphics.SmoothingMode = SmoothingMode.AntiAlias;

            return graphics;
        }

        public abstract void PreShowing(ICanvas canvas, PointF point);

        public abstract void PrepareToDrawing(ICanvas canvas, PointF point);

        public abstract void Draw(ICanvas canvas, PointF point);

        public abstract void EndDrawing(ICanvas canvas, PointF point);

        public abstract void PreShowDrawing(ICanvas canvas, Graphics preShowGraphics);
    }
}