using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Avalonia.Native.Interop
{
    partial interface IAvnString
    {
        public string String { get; }
        public uint8[] Bytes { get; }
    }

    partial interface IAvnStringArray
    {
        string[] ToStringArray();
    }

    internal sealed class AvnString : NativeCallbackBase, IAvnString
    {
        private IntPtr _native;
        private int32 _nativeLen;

        public AvnString(string s) => String = s;

        public string String { get; }
        public uint8[] Bytes => Encoding.UTF8.GetBytes(String);
        
        public unsafe void* Pointer()
        {
            EnsureNative();
            return _native.ToPointer();
        }

        public int32 Length()
        {
            EnsureNative();
            return _nativeLen;
        }

        protected override void Destroyed()
        {
            if (_native != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_native);
                _native = IntPtr.Zero;
            }
        }
        
        private unsafe void EnsureNative()
        {
            if (string.IsNullOrEmpty(String))
                return;
            if (_native == IntPtr.Zero)
            {
                _nativeLen = Encoding.UTF8.GetByteCount(String);
                _native = Marshal.AllocHGlobal(_nativeLen + 1);
                var ptr = (uint8*)_native.ToPointer();
                fixed (char* chars = String)
                    Encoding.UTF8.GetBytes(chars, String.Length, ptr, _nativeLen);
                ptr[_nativeLen] = 0;
            }
        }
    }

    internal sealed class AvnStringArray : NativeCallbackBase, IAvnStringArray
    {
        private readonly IAvnString[] _items;

        public AvnStringArray(IEnumerable<string> items)
        {
            _items = items.Select(s => s.ToAvnString()).ToArray();
        }

        public string[] ToStringArray() => _items.Select(n => n.String).ToArray();

        public uint32 Count => (uint32)_items.Length;
        public IAvnString Get(uint32 index) => _items[(int32)index];

        protected override void Destroyed()
        {
            foreach (var item in _items)
            {
                item.Dispose();
            }
        }
    }
}
namespace Avalonia.Native.Interop.Impl
{
    unsafe partial class __MicroComIAvnStringProxy
    {
        private string _managed;
        private uint8[] _bytes;

        public string String
        {
            get
            {
                if (_managed == null)
                {
                    var ptr = Pointer();
                    if (ptr == null)
                        return null;
                    _managed = System.Text.Encoding.UTF8.GetString((uint8*)ptr, Length());
                }

                return _managed;
            }
        }

        public uint8[] Bytes
        {
            get
            {
                if (_bytes == null)
                {
                    _bytes = new uint8[Length()];
                    Marshal.Copy(new IntPtr(Pointer()), _bytes, 0, _bytes.Length);
                }

                return _bytes;
            }
        }

        public override string ToString() => String;
    }
    
    partial class __MicroComIAvnStringArrayProxy
    {
        public string[] ToStringArray()
        {
            var arr = new string[Count];
            for(uint32 c = 0; c<arr.Length;c++)
                using (var s = Get(c))
                    arr[c] = s.String;
            return arr;
        }
    }
}
