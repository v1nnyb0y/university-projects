using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    class ZoomIn:PhotoTool
    {
        public ZoomIn(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            int k = canvas.CZoom;
            if (k * 2 > 800) return;

            k *= 2;
            canvas.CZoom = k;
            Descriptor.Zoom = k.ToString();


            canvas.ShowedPicture = new Bitmap(Zooming(canvas.ShowedPicture, canvas));
            canvas.ExecuteChanging();
        }

        private Image Zooming(Image image, ICanvas canvas) {
            image = new Bitmap(canvas.ShowedPicture, canvas.ShowedPicture.Width * 2,
                canvas.ShowedPicture.Height * 2);
            var w = (canvas.GetSize.Width - 20) * 2 + 20;

            var h = (canvas.GetSize.Height - 40) * 2 + 40;
            canvas.GetSize = new Size(w, h);
            var g = Graphics.FromImage(image);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return image;
        }
    }
}
