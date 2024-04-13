using System.Drawing;

namespace Lab._2.Paint.Interfaces.UIInt
{
    public interface ICanvas
    {
        Bitmap OriginPicture { get; set; }

        Bitmap ShowedPicture { get; set; }

        string CanvasName { get; set; }

        Size GetSize { get; set; }

        string Path { get; set; }

        int CZoom { get; set; }

        void ExecuteChanging();

        void _Resize();
    }
}