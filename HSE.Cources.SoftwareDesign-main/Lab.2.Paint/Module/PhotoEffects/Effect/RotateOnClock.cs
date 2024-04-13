using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    public class RotateOnClock : PhotoTool
    {
        public RotateOnClock(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            if (canvas.OriginPicture != null) canvas.OriginPicture.RotateFlip(RotateFlipType.Rotate90FlipNone);

            canvas.ShowedPicture.RotateFlip(RotateFlipType.Rotate90FlipNone);
            canvas.ExecuteChanging();
        }
    }
}