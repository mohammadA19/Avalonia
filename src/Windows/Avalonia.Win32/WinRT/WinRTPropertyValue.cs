using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Avalonia.Win32.WinRT
{
    class WinRTPropertyValue : WinRTInspectable, IPropertyValue 
    {
        public WinRTPropertyValue(float f)
        {
            Type = PropertyType.Single;
            Single = f;
        }

        public WinRTPropertyValue(uint32 u)
        {
            UInt32 = u;
            Type = PropertyType.UInt32;
        }

        public WinRTPropertyValue(float[] uiColor)
        {
            Type = PropertyType.SingleArray;
            _singleArray = uiColor;
        }

        private readonly float[]? _singleArray;

        public PropertyType Type { get; }
        public int32 IsNumericScalar { get; }
        public uint8 UInt8 { get; }
        public int16 Int16 { get; }
        public uint16 UInt16 { get; }
        public int32 Int32 { get; }
        public uint32 UInt32 { get; }
        public int64 Int64 { get; }
        public uint64 UInt64 { get; }
        public float Single { get; }
        public double Double { get; }
        public char Char16 { get; }
        public int32 Boolean { get; }
        public IntPtr String { get; }
        public Guid Guid { get; }

        private static COMException NotImplemented => new COMException("Not supported", unchecked((int32)0x80004001));
        
        public unsafe void GetDateTime(void* value) => throw NotImplemented;

        public unsafe void GetTimeSpan(void* value) => throw NotImplemented;

        public unsafe void GetPoint(void* value) => throw NotImplemented;

        public unsafe void GetSize(void* value) => throw NotImplemented;

        public unsafe void GetRect(void* value) => throw NotImplemented;

        public unsafe uint8* GetUInt8Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe int16* GetInt16Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe uint16* GetUInt16Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe int32* GetInt32Array(uint32* __valueSize)
        {
            throw NotImplemented;
        }

        public unsafe uint32* GetUInt32Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe int64* GetInt64Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe uint64* GetUInt64Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe float* GetSingleArray(uint32* __valueSize)
        {
            if (_singleArray == null)
                throw NotImplemented;
            *__valueSize = (uint32)_singleArray.Length;
            var allocCoTaskMem = Marshal.AllocCoTaskMem(_singleArray.Length * Unsafe.SizeOf<float>());
            Marshal.Copy(_singleArray, 0, allocCoTaskMem, _singleArray.Length);
            float* s = (float*)allocCoTaskMem;

            return s;
        }

        public unsafe double* GetDoubleArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe char* GetChar16Array(uint32* __valueSize) => throw NotImplemented;

        public unsafe int32* GetBooleanArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe IntPtr* GetStringArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe void** GetInspectableArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe Guid* GetGuidArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe void* GetDateTimeArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe void* GetTimeSpanArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe void* GetPointArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe void* GetSizeArray(uint32* __valueSize) => throw NotImplemented;

        public unsafe void* GetRectArray(uint32* __valueSize) => throw NotImplemented;
    }
}
