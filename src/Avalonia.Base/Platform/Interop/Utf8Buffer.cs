#nullable enable

using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace Avalonia.Platform.Interop
{
    internal class Utf8Buffer : SafeHandle
    {
        private GCHandle _gcHandle;
        private uint8[]? _data;
            
        public Utf8Buffer(string? s) : base(IntPtr.Zero, true)
        {
            if (s == null)
                return;
            _data = Encoding.UTF8.GetBytes(s);
            _gcHandle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            handle = _gcHandle.AddrOfPinnedObject();
        }

        public int32 ByteLen => _data?.Length ?? 0;

        protected override bool ReleaseHandle()
        {
            if (handle != IntPtr.Zero)
            {
                handle = IntPtr.Zero;
                _data = null;
                _gcHandle.Free();
            }
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        public static unsafe string? StringFromPtr(IntPtr s)
        {
            var pstr = (uint8*)s;
            if (pstr == null)
                return null;
            int32 len;
            for (len = 0; pstr[len] != 0; len++) ;

            var bytes = ArrayPool<uint8>.Shared.Rent(len);

            try
            {
                Marshal.Copy(s, bytes, 0, len);
                return Encoding.UTF8.GetString(bytes, 0, len);
            }
            finally
            {
                ArrayPool<uint8>.Shared.Return(bytes);
            }
        }

        public static implicit operator IntPtr(Utf8Buffer b) => b.handle;
        public static unsafe implicit operator uint8*(Utf8Buffer b) => (uint8*)b.handle;
    }
}
