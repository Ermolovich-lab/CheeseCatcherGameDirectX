using GameUILibrary.Drawing;
using SharpDX.Mathematics.Interop;
using System;

namespace GameUILibrary.Backgrounds
{
    public interface IBackground
    {
        void Draw(DrawingContext context, RawRectangleF bounds);
    }
}
