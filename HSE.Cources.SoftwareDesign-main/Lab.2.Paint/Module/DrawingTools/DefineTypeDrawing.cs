using Lab._2.Paint.Interfaces.UIInt;
using Lab._2.Paint.Module.DrawingTools.Tools;

namespace Lab._2.Paint.Module.DrawingTools
{
    internal static class DefineTypeDrawing
    {
        public enum Type
        {
            None,
            Pencil,
            Ellipse,
            Eraser,
            Line,
            MagicalEraser,
            NCornerStar,
            Rectangle
        }

        public static DrawingTool TypeDrawing(Type type, ICanvas canvas, params int[] corner) {
            switch (type) {
                case Type.None: return default(DrawingTool);
                case Type.Pencil: return new Pencil(DataCore.Paintbrush, false);
                case Type.Rectangle: return new Rectangle(DataCore.Paintbrush, true);
                case Type.Ellipse: return new Ellipse(DataCore.Paintbrush, true);
                case Type.Line: return new Line(DataCore.Paintbrush, true);
                case Type.MagicalEraser: {
                    if (canvas.OriginPicture == null) return new Eraser(DataCore.Paintbrush, false);
                    return new MagicalEraser(DataCore.Paintbrush, false);
                }
                case Type.NCornerStar: return new NCornerStar(DataCore.Paintbrush, true, corner[0]);
                case Type.Eraser: return new Eraser(DataCore.Paintbrush, false);
                default: return default(DrawingTool);
            }
        }
    }
}