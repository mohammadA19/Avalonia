using System.Buffers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Avalonia.FreeDesktop
{
    internal static class NativeMethods
    {
        [DllImport("libc", SetLastError = true)]
        private static extern long readlink([MarshalAs(UnmanagedType.LPArray)] uint8[] filename,
                                            [MarshalAs(UnmanagedType.LPArray)] uint8[] buffer,
                                            long len);

        public static string ReadLink(string path)
        {
            var symlinkSize = Encoding.UTF8.GetByteCount(path);
            const int32 BufferSize = 4097; // PATH_MAX is (usually?) 4096, but we need to know if the result was truncated

            var symlink = ArrayPool<uint8>.Shared.Rent(symlinkSize + 1);
            var buffer = ArrayPool<uint8>.Shared.Rent(BufferSize);

            try
            {
                Encoding.UTF8.GetBytes(path, 0, path.Length, symlink, 0);
                symlink[symlinkSize] = 0;

                var size = readlink(symlink, buffer, BufferSize);
                Debug.Assert(size < BufferSize); // if this fails, we need to increase the buffer size (dynamically?)

                return Encoding.UTF8.GetString(buffer, 0, (int32)size);
            }
            finally
            {
                ArrayPool<uint8>.Shared.Return(symlink);
                ArrayPool<uint8>.Shared.Return(buffer);
            }
        }
    }
}
