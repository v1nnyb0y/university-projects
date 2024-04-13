using Lab._2.Paint.Interfaces.UIInt;
using Lab._2.Paint.Module.PhotoEffects.Effect;

namespace Lab._2.Paint.Module.PhotoEffects
{
    internal static class DefinePhotoEffect
    {
        public enum Effect
        {
            None,
            RotateOnClock,
            RotateUnderClock,
            ZoomIn,
            ZoomOut,
            Blur,
            Embrass,
            Smoothing,
            Sharpness,
            FlipVertical,
            FlipHorizontal
        }

        public static PhotoTool EffectRender(Effect type, IDescriptionable descriptionable) {
            switch (type) {
                case Effect.None: return default(PhotoTool);
                case Effect.Blur: return new Blur(descriptionable);
                case Effect.Embrass: return new Embrass(descriptionable);
                case Effect.RotateOnClock: return new RotateOnClock(descriptionable);
                case Effect.RotateUnderClock: return new RotateUnderClock(descriptionable);
                case Effect.Smoothing: return new Smoothing(descriptionable);
                case Effect.Sharpness: return new Sharpness(descriptionable);
                case Effect.FlipHorizontal: return new FlipHorizontal(descriptionable);
                case Effect.FlipVertical: return new FlipVertical(descriptionable);
                case Effect.ZoomIn: return new ZoomIn(descriptionable);
                case Effect.ZoomOut: return new ZoomOut(descriptionable);
                default: return default(PhotoTool);
            }
        }
    }
}