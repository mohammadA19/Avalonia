using System;
using static Avalonia.Win32.Interop.UnmanagedMethods;
// ReSharper disable InconsistentNaming
#pragma warning disable CS0649

namespace Avalonia.Win32.DirectX
{
    internal unsafe struct HANDLE
    {
        public readonly void* Value;

        public HANDLE(void* value)
        {
            Value = value;
        }

        public static HANDLE INVALID_VALUE => new HANDLE((void*)(-1));

        public static HANDLE NULL => new HANDLE(null);

        public static bool operator ==(HANDLE left, HANDLE right) => left.Value == right.Value;

        public static bool operator !=(HANDLE left, HANDLE right) => left.Value != right.Value;

        public override bool Equals(object? obj) => (obj is HANDLE other) && Equals(other);

        public bool Equals(HANDLE other) => ((nuint)(Value)).Equals((nuint)(other.Value));

        public override int32 GetHashCode() => ((nuint)(Value)).GetHashCode();

        public override string ToString() => ((IntPtr)Value).ToString();
    }

    internal unsafe struct DXGI_ADAPTER_DESC
    {
        public fixed ushort Description[128];

        public uint32 VendorId;

        public uint32 DeviceId;

        public uint32 SubSysId;

        public uint32 Revision;

        public nuint DedicatedVideoMemory;

        public nuint DedicatedSystemMemory;

        public nuint SharedSystemMemory;

        public ulong AdapterLuid;
    }

    internal unsafe struct DXGI_ADAPTER_DESC1
    {
        public fixed ushort Description[128];

        public uint32 VendorId;

        public uint32 DeviceId;

        public uint32 SubSysId;

        public uint32 Revision;

        public nuint DedicatedVideoMemory;

        public nuint DedicatedSystemMemory;

        public nuint SharedSystemMemory;

        public ulong AdapterLuid;

        public uint32 Flags;
    }

    internal struct DXGI_FRAME_STATISTICS
    {
        public uint32 PresentCount;

        public uint32 PresentRefreshCount;

        public uint32 SyncRefreshCount;

        public ulong SyncQPCTime;

        public ulong SyncGPUTime;
    }

    internal unsafe struct DXGI_GAMMA_CONTROL_CAPABILITIES
    {
        public int32 ScaleAndOffsetSupported;

        public float MaxConvertedValue;

        public float MinConvertedValue;

        public uint32 NumGammaControlPoints;

        public fixed float ControlPointPositions[1025];
    }

    internal unsafe struct DXGI_MAPPED_RECT
    {
        public int32 Pitch;
        public byte* pBits;
    }

    internal struct DXGI_MODE_DESC
    {
        public ushort Width;
        public ushort Height;
        public DXGI_RATIONAL RefreshRate;
        public DXGI_FORMAT Format;
        public DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;
        public DXGI_MODE_SCALING Scaling;
    }

    internal unsafe struct DXGI_OUTPUT_DESC
    {
        internal fixed ushort DeviceName[32];

        internal RECT DesktopCoordinates;

        internal int32 AttachedToDesktop; // BOOL maps to int32. If we use the CLR 'bool' type here, the struct becomes non-blittable. See #9599

        internal DXGI_MODE_ROTATION Rotation;

        internal HANDLE Monitor;
    }

    internal unsafe struct DXGI_PRESENT_PARAMETERS
    {
        public uint32 DirtyRectsCount;

        public RECT* pDirtyRects;

        public RECT* pScrollRect;

        public POINT* pScrollOffset;
    }

    internal struct DXGI_RATIONAL
    {
        public ushort Numerator;
        public ushort Denominator;
    }

    internal struct DXGI_RGB
    {
        public float Red;

        public float Green;

        public float Blue;
    }

    internal struct DXGI_RGBA
    {
        public float r;

        public float g;

        public float b;

        public float a;
    }

    internal struct DXGI_SAMPLE_DESC
    {
        public uint32 Count;
        public uint32 Quality;
    }

    internal struct DXGI_SURFACE_DESC
    {
        public uint32 Width;

        public uint32 Height;

        public DXGI_FORMAT Format;

        public DXGI_SAMPLE_DESC SampleDesc;
    }

    internal struct DXGI_SWAP_CHAIN_DESC
    {
        public DXGI_MODE_DESC BufferDesc;
        public DXGI_SAMPLE_DESC SampleDesc;
        public uint32 BufferUsage;
        public ushort BufferCount;
        public IntPtr OutputWindow;
        public int32 Windowed;
        public DXGI_SWAP_EFFECT SwapEffect;
        public ushort Flags;
    }

    internal struct DXGI_SWAP_CHAIN_DESC1
    {
        public uint32 Width;
        public uint32 Height;
        public DXGI_FORMAT Format;
        public int32 Stereo; // BOOL maps to int32. If we use the CLR 'bool' type here, the struct becomes non-blittable. See #9599
        public DXGI_SAMPLE_DESC SampleDesc;
        public uint32 BufferUsage;
        public uint32 BufferCount;
        public DXGI_SCALING Scaling;
        public DXGI_SWAP_EFFECT SwapEffect;
        public DXGI_ALPHA_MODE AlphaMode;
        public uint32 Flags;
    }

    internal struct DXGI_SWAP_CHAIN_FULLSCREEN_DESC
    {
        public DXGI_RATIONAL RefreshRate;

        public DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;

        public DXGI_MODE_SCALING Scaling;

        public int32 Windowed;
    }

    internal struct D3D11_TEXTURE2D_DESC
    {
        public uint32 Width;

        public uint32 Height;

        public uint32 MipLevels;

        public uint32 ArraySize;

        public DXGI_FORMAT Format;

        public DXGI_SAMPLE_DESC SampleDesc;

        public D3D11_USAGE Usage;

        public D3D11_BIND_FLAG BindFlags;

        public uint32 CPUAccessFlags;

        public D3D11_RESOURCE_MISC_FLAG MiscFlags;
    }
}
