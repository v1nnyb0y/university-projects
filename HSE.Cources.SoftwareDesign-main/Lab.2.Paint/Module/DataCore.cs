using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lab._2.Paint.Interfaces.UIInt;
using Lab._2.Paint.Module.DrawingTools;
using Lab._2.Paint.Module.DrawingTools.Tools;
using Lab._2.Paint.Module.PhotoEffects;

namespace Lab._2.Paint.Module
{
    public static class DataCore
    {
        public static ToolStripButton Button;

        static DataCore() {
            Paintbrush = new Pen(Color.Black, 7f)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                };

            CurrentDrawingTool = new Pencil(Paintbrush, false);
        }

        public static IDescriptionable Descriptor { get; set; }

        public static ICanvasProvider CanvasProvider { get; set; }

        public static Pen Paintbrush { get; set; }

        public static bool isSmoothing { get; set; }

        public static DrawingTool CurrentDrawingTool { get; set; }

        public static PhotoTool CurrentPhotoTool { get; set; }
    }
}