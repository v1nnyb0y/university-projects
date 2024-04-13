using System;
using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    internal class Smoothing : PhotoTool
    {
        public Smoothing(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            Descriptor.IsProcces = true;

            var bitmap = canvas.ShowedPicture;
            int DX = 1, DY = 1, red, green, blue;

            for (var i = DX; i < bitmap.Height - DX - 1; i++) {
                for (var j = DY; j < bitmap.Width - DY - 1; j++) {
                    red = (bitmap.GetPixel(j - 1, i - 1).R +
                           bitmap.GetPixel(j - 1, i).R +
                           bitmap.GetPixel(j - 1, i + 1).R +
                           bitmap.GetPixel(j, i - 1).R +
                           bitmap.GetPixel(j, i).R +
                           bitmap.GetPixel(j, i + 1).R +
                           bitmap.GetPixel(j + 1, i - 1).R +
                           bitmap.GetPixel(j + 1, i).R +
                           bitmap.GetPixel(j + 1, i + 1).R) / 9;

                    green = (bitmap.GetPixel(j - 1, i - 1).G +
                             bitmap.GetPixel(j - 1, i).G +
                             bitmap.GetPixel(j - 1, i + 1).G +
                             bitmap.GetPixel(j, i - 1).G +
                             bitmap.GetPixel(j, i).G +
                             bitmap.GetPixel(j, i + 1).G +
                             bitmap.GetPixel(j + 1, i - 1).G +
                             bitmap.GetPixel(j + 1, i).G +
                             bitmap.GetPixel(j + 1, i + 1).G) / 9;

                    blue = (bitmap.GetPixel(j - 1, i - 1).B +
                            bitmap.GetPixel(j - 1, i).B +
                            bitmap.GetPixel(j - 1, i + 1).B +
                            bitmap.GetPixel(j, i - 1).B +
                            bitmap.GetPixel(j, i).B +
                            bitmap.GetPixel(j, i + 1).B +
                            bitmap.GetPixel(j + 1, i - 1).B +
                            bitmap.GetPixel(j + 1, i).B +
                            bitmap.GetPixel(j + 1, i + 1).B) / 9;

                    red = Math.Min(Math.Max(red, 0), 255);
                    green = Math.Min(Math.Max(green, 0), 255);
                    blue = Math.Min(Math.Max(blue, 0), 255);
                    bitmap.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                if (i % 10 == 0) Descriptor.Description = 100 * i / (canvas.ShowedPicture.Height - DX - 1) + "%";
            }

            Descriptor.Description = "Эффект размытия применен";
            Descriptor.IsProcces = false;
            canvas.ExecuteChanging();
            canvas._Resize();
        }
    }
}