using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace Avalonia.Browser.Interop;

/// <summary>
/// Set of FileSystemWritableFileStream and Blob methods.
/// </summary>
internal static partial class StreamHelper
{
    [JSImport("StreamHelper.seek", AvaloniaModule.MainModuleName)]
    public static partial void Seek(JSObject stream, [JSMarshalAs<JSType.Number>] int64 position);

    [JSImport("StreamHelper.truncate", AvaloniaModule.MainModuleName)]
    public static partial void Truncate(JSObject stream, [JSMarshalAs<JSType.Number>] int64 size);

    [JSImport("StreamHelper.write", AvaloniaModule.MainModuleName)]
    public static partial Task WriteAsync(JSObject stream, [JSMarshalAs<JSType.MemoryView>] ArraySegment<uint8> data, int32 offset, int32 count);

    [JSImport("StreamHelper.close", AvaloniaModule.MainModuleName)]
    public static partial Task CloseAsync(JSObject stream);

    [JSImport("StreamHelper.byteLength", AvaloniaModule.MainModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int64 ByteLength(JSObject stream);

    [JSImport("StreamHelper.sliceArrayBuffer", AvaloniaModule.MainModuleName)]
    private static partial Task<JSObject> SliceToArrayBuffer(JSObject stream, [JSMarshalAs<JSType.Number>] int64 offset, int32 count);

    [JSImport("StreamHelper.toMemoryView", AvaloniaModule.MainModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    private static partial uint8[] ArrayBufferToMemoryView(JSObject stream);

    public static async Task<uint8[]> SliceAsync(JSObject stream, int64 offset, int32 count)
    {
        using var buffer = await SliceToArrayBuffer(stream, offset, count);
        return ArrayBufferToMemoryView(buffer);
    }
}
