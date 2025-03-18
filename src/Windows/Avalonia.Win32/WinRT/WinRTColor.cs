using System.Runtime.InteropServices;
using Avalonia.Media;

namespace Avalonia.Win32.WinRT
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal record struct WinRTColor
    {
        public uint8 A;
        public uint8 R;
        public uint8 G;
        public uint8 B;

        public static WinRTColor FromArgb(uint8 a, uint8 r, uint8 g, uint8 b) => new WinRTColor()
        {
            A = a, R = r, G = g, B = b
        };

        public Color ToAvalonia() => new(A, R, G, B);
    }
}
