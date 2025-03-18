using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Avalonia.Platform.Storage.FileIO;

/// <summary>
/// Stream wrapper currently used by Apple platforms,
/// where in sandboxed scenario it's advised to call [NSUri startAccessingSecurityScopedResource].
/// </summary>
internal sealed class SecurityScopedStream(FileStream _stream, IDisposable _securityScope) : Stream
{
    public override bool CanRead => _stream.CanRead;

    public override bool CanSeek => _stream.CanSeek;

    public override bool CanWrite => _stream.CanWrite;

    public override long Length => _stream.Length;

    public override long Position
    {
        get => _stream.Position;
        set => _stream.Position = value;
    }

    public override void Flush() =>
        _stream.Flush();

    public override Task FlushAsync(CancellationToken cancellationToken) =>
        _stream.FlushAsync(cancellationToken);

    public override int32 ReadByte() =>
        _stream.ReadByte();

    public override int32 Read(uint8[] buffer, int32 offset, int32 count) =>
        _stream.Read(buffer, offset, count);

    public override Task<int32> ReadAsync(uint8[] buffer, int32 offset, int32 count, CancellationToken cancellationToken) =>
        _stream.ReadAsync(buffer, offset, count, cancellationToken);

#if NET6_0_OR_GREATER
    public override int32 Read(Span<uint8> buffer) => _stream.Read(buffer);

    public override ValueTask<int32> ReadAsync(Memory<uint8> buffer, CancellationToken cancellationToken = default) =>
        _stream.ReadAsync(buffer, cancellationToken);
#endif

    public override void Write(uint8[] buffer, int32 offset, int32 count) =>
        _stream.Write(buffer, offset, count);

    public override Task WriteAsync(uint8[] buffer, int32 offset, int32 count, CancellationToken cancellationToken) =>
        _stream.WriteAsync(buffer, offset, count, cancellationToken);

#if NET6_0_OR_GREATER
    public override void Write(ReadOnlySpan<uint8> buffer) => _stream.Write(buffer);

    public override ValueTask WriteAsync(ReadOnlyMemory<uint8> buffer, CancellationToken cancellationToken = default) =>
        _stream.WriteAsync(buffer, cancellationToken);
#endif

    public override void WriteByte(uint8 value) => _stream.WriteByte(value);

    public override long Seek(long offset, SeekOrigin origin) =>
        _stream.Seek(offset, origin);

    public override void SetLength(long value) =>
        _stream.SetLength(value);

#if NET6_0_OR_GREATER
    public override void CopyTo(Stream destination, int32 bufferSize) => _stream.CopyTo(destination, bufferSize);
#endif

    public override Task CopyToAsync(Stream destination, int32 bufferSize, CancellationToken cancellationToken) =>
        _stream.CopyToAsync(destination, bufferSize, cancellationToken);

    public override IAsyncResult BeginRead(uint8[] buffer, int32 offset, int32 count, AsyncCallback? callback, object? state) =>
        _stream.BeginRead(buffer, offset, count, callback, state);

    public override int32 EndRead(IAsyncResult asyncResult) => _stream.EndRead(asyncResult);

    public override IAsyncResult BeginWrite(uint8[] buffer, int32 offset, int32 count, AsyncCallback? callback, object? state) =>
        _stream.BeginWrite(buffer, offset, count, callback, state);

    public override void EndWrite(IAsyncResult asyncResult) => _stream.EndWrite(asyncResult);

    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
                _stream.Dispose();
            }
        }
        finally
        {
            _securityScope.Dispose();
        }
    }

#if NET6_0_OR_GREATER
    public override async ValueTask DisposeAsync()
    {
        try
        {
            await _stream.DisposeAsync();
        }
        finally
        {
            _securityScope.Dispose();
        }
    }
#endif
}
