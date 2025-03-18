using System;
using Avalonia.Platform;
namespace Avalonia.Media.Imaging;

internal record struct Rgba64Pixel
{
    public Rgba64Pixel(uint16 r, uint16 g, uint16 b, uint16 a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public uint16 R;
    public uint16 G;
    public uint16 B;
    public uint16 A;
}

internal record struct Rgba8888Pixel
{
    public Rgba8888Pixel(uint8 r, uint8 g, uint8 b, uint8 a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public uint8 R;
    public uint8 G;
    public uint8 B;
    public uint8 A;
}

internal interface IPixelFormatReader
{
    Rgba8888Pixel ReadNext();
    void Reset(IntPtr address);
}

internal static unsafe class PixelFormatReader
{
    private static readonly Rgba8888Pixel s_white = new Rgba8888Pixel
    {
        A = 255,
        B = 255,
        G = 255,
        R = 255
    };

    private static readonly Rgba8888Pixel s_black = new Rgba8888Pixel
    {
        A = 255,
        B = 0,
        G = 0,
        R = 0
    };

    public unsafe struct BlackWhitePixelFormatReader : IPixelFormatReader
    {
        private int32 _bit;
        private uint8* _address;

        public void Reset(IntPtr address)
        {
            _address = (uint8*)address;
            _bit = 0;
        }

        public Rgba8888Pixel ReadNext()
        {
            var shift = 7 - _bit;
            var value = (*_address >> shift) & 1;
            _bit++;
            if (_bit == 8)
            {
                _address++;
                _bit = 0;
            }
            return value == 1 ? s_white : s_black;
        }
    }

    public unsafe struct Gray2PixelFormatReader : IPixelFormatReader
    {
        private int32 _bit;
        private uint8* _address;

        public void Reset(IntPtr address)
        {
            _address = (uint8*)address;
            _bit = 0;
        }

        private static readonly Rgba8888Pixel[] Palette = new[]
        {
            s_black,
            new Rgba8888Pixel
            {
                A = 255, B = 0x55, G = 0x55, R = 0x55
            },
            new Rgba8888Pixel
            {
                A = 255, B = 0xAA, G = 0xAA, R = 0xAA
            },
            s_white
        };

        public Rgba8888Pixel ReadNext()
        {
            var shift = 6 - _bit;
            var value = (uint8)((*_address >> shift));
            value = (uint8)((value & 3));
            _bit += 2;
            if (_bit == 8)
            {
                _address++;
                _bit = 0;
            }

            return Palette[value];
        }
    }

    public unsafe struct Gray4PixelFormatReader : IPixelFormatReader
    {
        private int32 _bit;
        private uint8* _address;

        public void Reset(IntPtr address)
        {
            _address = (uint8*)address;
            _bit = 0;
        }

        public Rgba8888Pixel ReadNext()
        {
            var shift = 4 - _bit;
            var value = (uint8)((*_address >> shift));
            value = (uint8)((value & 0xF));
            value = (uint8)(value | (value << 4));
            _bit += 4;
            if (_bit == 8)
            {
                _address++;
                _bit = 0;
            }

            return new Rgba8888Pixel
            {
                A = 255,
                B = value,
                G = value,
                R = value
            };
        }
    }

    public unsafe struct Gray8PixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public void Reset(IntPtr address)
        {
            _address = (uint8*)address;
        }

        public Rgba8888Pixel ReadNext()
        {
            var value = *_address;
            _address++;

            return new Rgba8888Pixel
            {
                A = 255,
                B = value,
                G = value,
                R = value
            };
        }
    }

    public unsafe struct Gray16PixelFormatReader : IPixelFormatReader
    {
        private uint16* _address;
        public Rgba8888Pixel ReadNext()
        {
            var value16 = *_address;
            _address++;
            var value8 = (uint8)(value16 >> 8);
            return new Rgba8888Pixel
            {
                A = 255,
                B = value8,
                G = value8,
                R = value8
            };
        }

        public void Reset(IntPtr address) => _address = (uint16*)address;
    }

    public unsafe struct Gray32FloatPixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public Rgba8888Pixel ReadNext()
        {
            var f = *(float*)_address;
            var srgb = Math.Pow(f, 1 / 2.2);
            var value = (uint8)(srgb * 255);

            _address += 4;
            return new Rgba8888Pixel
            {
                A = 255,
                B = value,
                G = value,
                R = value
            };
        }

        public void Reset(IntPtr address) => _address = (uint8*)address;
    }

    public unsafe struct Rgba64PixelFormatReader : IPixelFormatReader
    {
        private Rgba64Pixel* _address;
        public Rgba8888Pixel ReadNext()
        {
            var value = *_address;

            _address++;
            return new Rgba8888Pixel
            {
                A = (uint8)(value.A >> 8),
                B = (uint8)(value.B >> 8),
                G = (uint8)(value.G >> 8),
                R = (uint8)(value.R >> 8),
            };
        }

        public void Reset(IntPtr address) => _address = (Rgba64Pixel*)address;
    }

    public unsafe struct Rgb24PixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public Rgba8888Pixel ReadNext()
        {
            var addr = _address;
            _address += 3;
            return new Rgba8888Pixel
            {
                R = addr[0],
                G = addr[1],
                B = addr[2],
                A = 255,
            };
        }

        public void Reset(IntPtr address) => _address = (uint8*)address;
    }

    public unsafe struct Bgr24PixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public Rgba8888Pixel ReadNext()
        {
            var addr = _address;
            _address += 3;
            return new Rgba8888Pixel
            {
                R = addr[2],
                G = addr[1],
                B = addr[0],
                A = 255,
            };
        }

        public void Reset(IntPtr address) => _address = (uint8*)address;
    }

    public unsafe struct Bgr555PixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public Rgba8888Pixel ReadNext()
        {
            var addr = (uint16*)_address;

            _address += 2;

            return UnPack(*addr);
        }

        public void Reset(IntPtr address) => _address = (uint8*)address;

        private static Rgba8888Pixel UnPack(uint16 value)
        {
            var r = (uint8)Math.Round(((value >> 10) & 0x1F) / 31F * 255);
            var g = (uint8)Math.Round(((value >> 5) & 0x1F) / 31F * 255);
            var b = (uint8)Math.Round(((value >> 0) & 0x1F) / 31F * 255);

            return new Rgba8888Pixel(r, g, b, 255);
        }
    }

    public unsafe struct Bgr565PixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public Rgba8888Pixel ReadNext()
        {
            var addr = (uint16*)_address;

            _address += 2;

            return UnPack(*addr);
        }

        public void Reset(IntPtr address) => _address = (uint8*)address;

        private static Rgba8888Pixel UnPack(uint16 value)
        {
            var r = (uint8)Math.Round(((value >> 11) & 0x1F) / 31F * 255);
            var g = (uint8)Math.Round(((value >> 5) & 0x3F) / 63F * 255);
            var b = (uint8)Math.Round(((value >> 0) & 0x1F) / 31F * 255);

            return new Rgba8888Pixel(r, g, b, 255);
        }
    }

    public unsafe struct Rgba8888PixelFormatReader : IPixelFormatReader
    {
        private Rgba8888Pixel* _address;
        public Rgba8888Pixel ReadNext()
        {
            var value = *_address;

            _address++;

            return value;
        }

        public void Reset(IntPtr address) => _address = (Rgba8888Pixel*)address;
    }

    public unsafe struct Rgb32PixelFormatReader : IPixelFormatReader
    {
        private Rgba8888Pixel* _address;
        public Rgba8888Pixel ReadNext()
        {
            var address = (uint8*)_address;

            var value = new Rgba8888Pixel(address[0], address[1], address[2], 255);

            _address++;

            return value;
        }

        public void Reset(IntPtr address) => _address = (Rgba8888Pixel*)address;
    }

    public unsafe struct Bgra8888PixelFormatReader : IPixelFormatReader
    {
        private uint8* _address;
        public Rgba8888Pixel ReadNext()
        {
            var addr = _address;

            _address += 4;

            return new Rgba8888Pixel(addr[2], addr[1], addr[0], addr[3]);
        }

        public void Reset(IntPtr address) => _address = (uint8*)address;
    }

    public unsafe struct Bgr32PixelFormatReader : IPixelFormatReader
    {
        private Rgba8888Pixel* _address;
        public Rgba8888Pixel ReadNext()
        {
            var address = (uint8*)_address;

            var value = new Rgba8888Pixel(address[2], address[1], address[0], 255);

            _address++;

            return value;
        }

        public void Reset(IntPtr address) => _address = (Rgba8888Pixel*)address;
    }

    public static bool SupportsFormat(PixelFormat format)
    {
        switch (format.FormatEnum)
        {
            case PixelFormatEnum.Rgb565:
            case PixelFormatEnum.Rgba8888:
            case PixelFormatEnum.Bgra8888:
            case PixelFormatEnum.BlackWhite:
            case PixelFormatEnum.Gray2:
            case PixelFormatEnum.Gray4:
            case PixelFormatEnum.Gray8:
            case PixelFormatEnum.Gray16:
            case PixelFormatEnum.Gray32Float:
            case PixelFormatEnum.Rgba64:
            case PixelFormatEnum.Rgb24:
            case PixelFormatEnum.Rgb32:
            case PixelFormatEnum.Bgr24:
            case PixelFormatEnum.Bgr32:
            case PixelFormatEnum.Bgr555:
            case PixelFormatEnum.Bgr565:
                return true;
            default:
                return false;
        }
    }

    private static void Read<T>(Span<Rgba8888Pixel> pixels, IntPtr source, PixelSize size, int32 stride) where T : struct, IPixelFormatReader
    {
        var reader = new T();

        var w = size.Width;
        var h = size.Height;
        var count = 0;

        for (var y = 0; y < h; y++)
        {
            reader.Reset(source + stride * y);

            for (var x = 0; x < w; x++)
            {
                pixels[count++] = reader.ReadNext();
            }
        }
    }

    public static void Read(Span<Rgba8888Pixel> pixels, IntPtr source, PixelSize size, int32 stride, PixelFormat format)
    {
        switch (format.FormatEnum)
        {
            case PixelFormatEnum.Rgb565:
                Read<Bgr565PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Rgba8888:
                Read<Rgba8888PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Bgra8888: 
                Read<Bgra8888PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.BlackWhite:
                Read<BlackWhitePixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Gray2:
                Read<Gray2PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Gray4:
                Read<Gray4PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Gray8:
                Read<Gray8PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Gray16:
                Read<Gray16PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Gray32Float:
                Read<Gray32FloatPixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Rgba64:
                Read<Rgba64PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Rgb24:
                Read<Rgb24PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Rgb32:
                Read<Rgb32PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Bgr24:
                Read<Bgr24PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Bgr32:
                Read<Bgr32PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Bgr555:
                Read<Bgr555PixelFormatReader>(pixels, source, size, stride);
                break;
            case PixelFormatEnum.Bgr565:
                Read<Bgr565PixelFormatReader>(pixels, source, size, stride);
                break;
            default:
                throw new NotSupportedException($"Pixel format {format} is not supported");
        }
    }
}
