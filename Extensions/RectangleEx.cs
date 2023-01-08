using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyNewYearScreenSaver.Extensions;

internal static class RectangleEx
{
    public static void Deconstruct(this System.Drawing.Rectangle rect, out int Left, out int Top, out int Width, out int Height) =>
        (Left, Top, Width, Height) = (rect.Left, rect.Top, rect.Width, rect.Height);
}
