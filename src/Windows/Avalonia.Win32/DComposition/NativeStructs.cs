using System.Runtime.InteropServices;

namespace Avalonia.Win32.DComposition;

[StructLayout(LayoutKind.Sequential)]
internal struct DXGI_RATIONAL
{
    public uint32 Numerator;
    public uint32 Denominator;
}

[StructLayout(LayoutKind.Sequential)]
internal struct DCOMPOSITION_FRAME_STATISTICS
{
    public int64 lastFrameTime;
    public DXGI_RATIONAL currentCompositionRate;
    public int64 currentTime;
    public int64 timeFrequency;
    public int64 nextEstimatedFrameTime;
}
