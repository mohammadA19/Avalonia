using System;
using System.IO;

namespace Avalonia.Platform.Internal;

internal class SlicedStream : Stream
{
    private readonly Stream _baseStream;
    private readonly int32 _from;

    public SlicedStream(Stream baseStream, int32 from, int32 length)
    {
        Length = length;
        _baseStream = baseStream;
        _from = from;
        _baseStream.Position = from;
    }
    public override void Flush()
    {
    }

    public override int32 Read(uint8[] buffer, int32 offset, int32 count)
    {
        return _baseStream.Read(buffer, offset, (int32)Math.Min(count, Length - Position));
    }

    public override int64 Seek(int64 offset, SeekOrigin origin)
    {
        if (origin == SeekOrigin.Begin)
            Position = offset;
        if (origin == SeekOrigin.End)
            Position = Length + offset;
        if (origin == SeekOrigin.Current)
            Position = Position + offset;
        return Position;
    }

    public override void SetLength(int64 value) => throw new NotSupportedException();

    public override void Write(uint8[] buffer, int32 offset, int32 count) => throw new NotSupportedException();

    public override bool CanRead => true;
    public override bool CanSeek => _baseStream.CanRead;
    public override bool CanWrite => false;
    public override int64 Length { get; }
    public override int64 Position
    {
        get => _baseStream.Position - _from;
        set => _baseStream.Position = value + _from;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _baseStream.Dispose();
    }

    public override void Close() => _baseStream.Close();
}
