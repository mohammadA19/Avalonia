using System;

namespace Avalonia.Benchmarks
{
    internal record struct Struct1
    {
        public Struct1(int32 value)
        {
            Int1 = value;
        }

        public int32 Int1;
    }

    internal record struct Struct2
    {
        public Struct2(int32 value)
        {
            Int1 = Int2 = value;
        }

        public int32 Int1;
        public int32 Int2;
    }

    internal record struct Struct3
    {
        public Struct3(int32 value)
        {
            Int1 = Int2 = Int3 = value;
        }

        public int32 Int1;
        public int32 Int2;
        public int32 Int3;
    }

    internal record struct Struct4
    {
        public Struct4(int32 value)
        {
            Int1 = Int2 = Int3 = Int4 = value;
        }

        public int32 Int1;
        public int32 Int2;
        public int32 Int3;
        public int32 Int4;
    }

    internal record struct Struct5
    {
        public Struct5(int32 value)
        {
            Int1 = Int2 = Int3 = Int4 = Int5 = value;
        }

        public int32 Int1;
        public int32 Int2;
        public int32 Int3;
        public int32 Int4;
        public int32 Int5;
    }

    internal record struct Struct6
    {
        public Struct6(int32 value)
        {
            Int1 = Int2 = Int3 = Int4 = Int5 = Int6 = value;
        }

        public int32 Int1;
        public int32 Int2;
        public int32 Int3;
        public int32 Int4;
        public int32 Int5;
        public int32 Int6;
    }

    internal record struct Struct7
    {
        public Struct7(int32 value)
        {
            Int1 = Int2 = Int3 = Int4 = Int5 = Int6 = Int7 = value;
        }

        public int32 Int1;
        public int32 Int2;
        public int32 Int3;
        public int32 Int4;
        public int32 Int5;
        public int32 Int6;
        public int32 Int7;
    }

    internal record struct Struct8
    {
        public Struct8(int32 value)
        {
            Int1 = Int2 = Int3 = Int4 = Int5 = Int6 = Int7 = Int8 = value;
        }

        public int32 Int1;
        public int32 Int2;
        public int32 Int3;
        public int32 Int4;
        public int32 Int5;
        public int32 Int6;
        public int32 Int7;
        public int32 Int8;
    }
}
