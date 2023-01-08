using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HappyNewYearScreenSaver.API.pInvoke;

internal struct Rect
{
    public int Left;

    public int Top;

    public int Right;

    public int Bottom;

    public int X => Left;

    public int Y => Top;

    public int Width => Right - Left;

    public int Height => Bottom - Top;

    public Size Size => new(Width, Height);

    public Rect(int left, int top, int right, int bottom)
    {
        Left   = left;
        Top    = top;
        Right  = right;
        Bottom = bottom;
    }

    public Rect(Rectangle r)
    {
        Left   = r.Left;
        Top    = r.Top;
        Right  = r.Right;
        Bottom = r.Bottom;
    }

    public override string ToString()
    {
        var result = new DefaultInterpolatedStringHandler(3, 4);
        result.AppendFormatted(Left);
        result.AppendLiteral(",");
        result.AppendFormatted(Top);
        result.AppendLiteral(",");
        result.AppendFormatted(Right);
        result.AppendLiteral(",");
        result.AppendFormatted(Bottom);
        return result.ToStringAndClear();
    }

    public static implicit operator Rectangle(Rect r) => Rectangle.FromLTRB(r.Left, r.Top, r.Right, r.Bottom);

    public static implicit operator Rect(Rectangle r) => new(r);
}
