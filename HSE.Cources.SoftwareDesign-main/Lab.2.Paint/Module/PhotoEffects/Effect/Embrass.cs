using System;
using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    internal class Embrass : PhotoTool
    {
        public Embrass(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            Descriptor.IsProcces = true;

            var bitmap = canvas.ShowedPicture;
            int DX = 1, dispY = 1, red, blue, green;

            for (var i = 0; i < bitmap.Height - DX - 1; i++) {
                for (var j = 0; j < bitmap.Width - dispY - 1; j++) {
                    var pxlA = bitmap.GetPixel(j, i);
                    var pxlB = bitmap.GetPixel(j + DX, i + dispY);
                    red = Math.Min(Math.Abs(pxlA.R - pxlB.R) + 128, 255);
                    green = Math.Min(Math.Abs(pxlA.G - pxlB.G) + 128, 255);
                    blue = Math.Min(Math.Abs(pxlA.B - pxlB.B) + 128, 255);
                    bitmap.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                if (i % 10 == 0) Descriptor.Description = 100 * i / (canvas.ShowedPicture.Height - DX - 1) + "%";
            }

            Descriptor.Description = "Эффект рельефа применен";
            Descriptor.IsProcces = false;
            canvas.ExecuteChanging();
            canvas._Resize();
        }
    }
}