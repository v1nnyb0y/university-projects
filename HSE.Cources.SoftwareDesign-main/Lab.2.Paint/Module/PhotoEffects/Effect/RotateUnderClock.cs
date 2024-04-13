using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    internal class RotateUnderClock : PhotoTool
    {
        public RotateUnderClock(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            if (canvas.OriginPicture != null) canvas.OriginPicture.RotateFlip(RotateFlipType.Rotate270FlipNone);

            canvas.ShowedPicture.RotateFlip(RotateFlipType.Rotate270FlipNone);
            canvas.ExecuteChanging();
        }
    }
}