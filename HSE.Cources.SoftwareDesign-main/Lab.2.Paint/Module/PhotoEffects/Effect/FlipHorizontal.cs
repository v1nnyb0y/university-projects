using System.Drawing;
using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects.Effect
{
    internal class FlipHorizontal : PhotoTool
    {
        public FlipHorizontal(IDescriptionable descriptor) : base(descriptor) { }

        public override void Render(ICanvas canvas) {
            if (canvas.OriginPicture != null) canvas.OriginPicture.RotateFlip(RotateFlipType.RotateNoneFlipX);
            canvas.ShowedPicture.RotateFlip(RotateFlipType.RotateNoneFlipX);
            canvas.ExecuteChanging();
        }
    }
}