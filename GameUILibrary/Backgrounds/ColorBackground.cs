using GameUILibrary.Drawing;
using SharpDX.Mathematics.Interop;

namespace GameUILibrary.Backgrounds
{
     public class ColorBackground : IBackground
    {
        private string _brush;

        public ColorBackground(string brush)
        {
            _brush = brush;
        }

        public void Draw(DrawingContext context, RawRectangleF bounds)
        {
            context.DrawRectangle(bounds, _brush);
        }
    }
}
