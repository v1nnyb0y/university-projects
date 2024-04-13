using System;
using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    internal class Sharpness : PhotoTool
    {
        public Sharpness(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            Descriptor.IsProcces = true;

            var bitmap = canvas.ShowedPicture;
            var random = new Random();
            int DX = 1, DY = 1, red, blue, green;

            for (var i = 3; i < bitmap.Height - 3; i++) {
                for (var j = 3; j < bitmap.Width - 3; j++) {
                    DX = (int) (random.NextDouble() * 4 - 2);
                    DY = (int) (random.NextDouble() * 4 - 2);
                    red = bitmap.GetPixel(j + DX, i + DY).R;
                    green = bitmap.GetPixel(j + DX, i + DY).G;
                    blue = bitmap.GetPixel(j + DX, i + DY).B;
                    bitmap.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                if (i % 10 == 0) Descriptor.Description = 100 * i / (canvas.ShowedPicture.Height - 3) + "%";
            }


            Descriptor.Description = "Эффект резкости применен";
            Descriptor.IsProcces = false;
            canvas.ExecuteChanging();
            canvas._Resize();
        }
    }
}