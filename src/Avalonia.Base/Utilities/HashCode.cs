// Taken from:
// https://github.com/mono/SkiaSharp/blob/main/binding/Binding.Shared/HashCode.cs
// Partial code copied from:
// https://github.com/dotnet/runtime/blob/6072e4d3a7a2a1493f514cdf4be75a3d56580e84/src/libraries/System.Private.CoreLib/src/System/HashCode.cs

#if NETSTANDARD2_0
#nullable disable

using System.Runtime.CompilerServices;

namespace System;

internal unsafe struct HashCode
{
    private static readonly uint32 s_seed = GenerateGlobalSeed();

    private const uint32 Prime1 = 2654435761U;
    private const uint32 Prime2 = 2246822519U;
    private const uint32 Prime3 = 3266489917U;
    private const uint32 Prime4 = 668265263U;
    private const uint32 Prime5 = 374761393U;

    private uint32 _v1, _v2, _v3, _v4;
    private uint32 _queue1, _queue2, _queue3;
    private uint32 _length;

    private static unsafe uint32 GenerateGlobalSeed()
    {
        var rnd = new Random();
        var result = rnd.Next();
        return unchecked((uint32)result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Initialize(out uint32 v1, out uint32 v2, out uint32 v3, out uint32 v4)
    {
        v1 = s_seed + Prime1 + Prime2;
        v2 = s_seed + Prime2;
        v3 = s_seed;
        v4 = s_seed - Prime1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint32 Round(uint32 hash, uint32 input) =>
        RotateLeft(hash + input * Prime2, 13) * Prime1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint32 QueueRound(uint32 hash, uint32 queuedValue) =>
        RotateLeft(hash + queuedValue * Prime3, 17) * Prime4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint32 MixState(uint32 v1, uint32 v2, uint32 v3, uint32 v4) =>
        RotateLeft(v1, 1) + RotateLeft(v2, 7) + RotateLeft(v3, 12) + RotateLeft(v4, 18);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint32 RotateLeft(uint32 value, int32 offset) =>
        (value << offset) | (value >> (32 - offset));

    private static uint32 MixEmptyState() =>
        s_seed + Prime5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint32 MixFinal(uint32 hash)
    {
        hash ^= hash >> 15;
        hash *= Prime2;
        hash ^= hash >> 13;
        hash *= Prime3;
        hash ^= hash >> 16;
        return hash;
    }

    public void Add(void* value) =>
        Add(value == null ? 0 : ((IntPtr)value).GetHashCode());

    public void Add<T>(T value) =>
        Add(value?.GetHashCode() ?? 0);

    private void Add(int32 value)
    {
        uint32 val = (uint32)value;

        // Storing the value of _length locally shaves of quite a few bytes
        // in the resulting machine code.
        uint32 previousLength = _length++;
        uint32 position = previousLength % 4;

        // Switch can't be inlined.

        if (position == 0)
            _queue1 = val;
        else if (position == 1)
            _queue2 = val;
        else if (position == 2)
            _queue3 = val;
        else // position == 3
        {
            if (previousLength == 3)
                Initialize(out _v1, out _v2, out _v3, out _v4);

            _v1 = Round(_v1, _queue1);
            _v2 = Round(_v2, _queue2);
            _v3 = Round(_v3, _queue3);
            _v4 = Round(_v4, val);
        }
    }

    public int32 ToHashCode()
    {
        // Storing the value of _length locally shaves of quite a few bytes
        // in the resulting machine code.
        uint32 length = _length;

        // position refers to the *next* queue position in this method, so
        // position == 1 means that _queue1 is populated; _queue2 would have
        // been populated on the next call to Add.
        uint32 position = length % 4;

        // If the length is less than 4, _v1 to _v4 don't contain anything
        // yet. xxHash32 treats this differently.

        uint32 hash = length < 4 ? MixEmptyState() : MixState(_v1, _v2, _v3, _v4);

        // _length is incremented once per Add(Int32) and is therefore 4
        // times too small (xxHash length is in bytes, not ints).

        hash += length * 4;

        // Mix what remains in the queue

        // Switch can't be inlined right now, so use as few branches as
        // possible by manually excluding impossible scenarios (position > 1
        // is always false if position is not > 0).
        if (position > 0)
        {
            hash = QueueRound(hash, _queue1);
            if (position > 1)
            {
                hash = QueueRound(hash, _queue2);
                if (position > 2)
                    hash = QueueRound(hash, _queue3);
            }
        }

        hash = MixFinal(hash);
        return (int32)hash;
    }
}
#endif
