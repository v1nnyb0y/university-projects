using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    internal class FlipVertical : PhotoTool
    {
        public FlipVertical(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            if (canvas.OriginPicture != null) canvas.OriginPicture.RotateFlip(RotateFlipType.RotateNoneFlipY);
            canvas.ShowedPicture.RotateFlip(RotateFlipType.RotateNoneFlipY);
            canvas.ExecuteChanging();
        }
    }
}