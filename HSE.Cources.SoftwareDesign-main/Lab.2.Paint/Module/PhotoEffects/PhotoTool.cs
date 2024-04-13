using Lab._2.Paint.Interfaces.UIInt;

namespace Lab._2.Paint.Module.PhotoEffects
{
    public abstract class PhotoTool
    {
        public PhotoTool(IDescriptionable descriptor) {
            Descriptor = descriptor;
        }

        protected IDescriptionable Descriptor { get; set; }

        public abstract void Render(ICanvas canvas);
    }
}