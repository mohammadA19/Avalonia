using System;

namespace Avalonia.Platform
{
    public class LockedFramebuffer : ILockedFramebuffer
    {
        private readonly Action? _onDispose;

        public LockedFramebuffer(IntPtr address, PixelSize size, int32 rowBytes, Vector dpi, PixelFormat format,
            Action? onDispose)
        {
            _onDispose = onDispose;
            Address = address;
            Size = size;
            RowBytes = rowBytes;
            Dpi = dpi;
            Format = format;
        }

        public IntPtr Address { get; }
        public PixelSize Size { get; }
        public int32 RowBytes { get; }
        public Vector Dpi { get; }
        public PixelFormat Format { get; }

        public void Dispose()
        {
            _onDispose?.Invoke();
        }
    }
}
