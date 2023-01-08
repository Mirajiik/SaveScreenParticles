using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SaveScreenParticles.API.pInvoke;

internal static class Gdi32
{
    private const string __LibName = "gdi32.dll";

    public readonly struct HDC : IEquatable<HDC>
    {
        public nint Handle { get; }

        public HDC(nint handle) => Handle = handle;

        public bool IsNull => Handle == nint.Zero;

        public static implicit operator nint(HDC hdc) => hdc.Handle;

        public static explicit operator HDC(nint hdc) => new(hdc);

        public static implicit operator HGDIOBJ(HDC hdc) => new(hdc.Handle);

        public static bool operator ==(HDC value1, HDC value2) => value1.Handle == value2.Handle;

        public static bool operator !=(HDC value1, HDC value2) => value1.Handle != value2.Handle;

        public override bool Equals(object? obj) => obj is HDC hdc && hdc.Handle == Handle;

        public bool Equals(HDC other) => other.Handle == Handle;

        public override int GetHashCode() => Handle.GetHashCode();
    }

    public struct HGDIOBJ : IEquatable<HGDIOBJ>
    {
        public nint Handle { get; }

        public HGDIOBJ(nint handle) => Handle = handle;

        public bool IsNull => Handle == nint.Zero;

        public static explicit operator nint(HGDIOBJ hgdiobj) => hgdiobj.Handle;

        public static explicit operator HGDIOBJ(nint hgdiobj) => new HGDIOBJ(hgdiobj);

        public static bool operator ==(HGDIOBJ value1, HGDIOBJ value2) => value1.Handle == value2.Handle;

        public static bool operator !=(HGDIOBJ value1, HGDIOBJ value2) => value1.Handle != value2.Handle;

        public override bool Equals(object? obj) => obj is HGDIOBJ hgdiobj && hgdiobj.Handle == Handle;

        public bool Equals(HGDIOBJ other) => other.Handle == Handle;

        public override int GetHashCode() => Handle.GetHashCode();

        public OBJ ObjectType => GetObjectType(this);
    }

    public enum OBJ
    {
        PEN = 1,
        BRUSH = 2,
        DC = 3,
        METADC = 4,
        PAL = 5,
        FONT = 6,
        BITMAP = 7,
        REGION = 8,
        METAFILE = 9,
        MEMDC = 10,       // 0x0000000A
        EXTPEN = 11,      // 0x0000000B
        ENHMETADC = 12,   // 0x0000000C
        ENHMETAFILE = 13, // 0x0000000D
        COLORSPACE = 14,  // 0x0000000E
    }

    public enum DeviceCapability
    {
        BITSPIXEL = 12,  // 0x0000000C
        PLANES = 14,     // 0x0000000E
        LOGPIXELSX = 88, // 0x00000058
        LOGPIXELSY = 90, // 0x0000005A
    }

    [DllImport(__LibName)]
    public static extern OBJ GetObjectType(HGDIOBJ h);

    [DllImport(__LibName)]
    public static extern HDC CreateDC(string lpszDriver, string? lpszDevice, string? lpszOutput, nint lpInitData);

    [DllImport(__LibName)]
    public static extern int GetDeviceCaps(HDC hdc, DeviceCapability nIndex);

    [DllImport(__LibName)]
    public static extern bool DeleteDC(HDC hdc);
}
