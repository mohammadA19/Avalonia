using System;

namespace Avalonia.Media.Fonts
{
    internal readonly record struct OpenTypeTag
    {
        public static readonly OpenTypeTag None = new OpenTypeTag(0, 0, 0, 0);
        public static readonly OpenTypeTag Max = new OpenTypeTag(uint8.MaxValue, uint8.MaxValue, uint8.MaxValue, uint8.MaxValue);
        public static readonly OpenTypeTag MaxSigned = new OpenTypeTag((uint8)int8.MaxValue, uint8.MaxValue, uint8.MaxValue, uint8.MaxValue);

        private readonly uint32 _value;

        public OpenTypeTag(uint32 value)
        {
            _value = value;
        }

        public OpenTypeTag(char c1, char c2, char c3, char c4)
        {
            _value = (uint32)(((uint8)c1 << 24) | ((uint8)c2 << 16) | ((uint8)c3 << 8) | (uint8)c4);
        }

        private OpenTypeTag(uint8 c1, uint8 c2, uint8 c3, uint8 c4)
        {
            _value = (uint32)((c1 << 24) | (c2 << 16) | (c3 << 8) | c4);
        }

        public static OpenTypeTag Parse(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return None;

            var realTag = new char[4];

            var len = Math.Min(4, tag.Length);
            var i = 0;
            for (; i < len; i++)
                realTag[i] = tag[i];
            for (; i < 4; i++)
                realTag[i] = ' ';

            return new OpenTypeTag(realTag[0], realTag[1], realTag[2], realTag[3]);
        }

        public override string ToString()
        {
            if (_value == None)
            {
                return nameof(None);
            }
            if (_value == Max)
            {
                return nameof(Max);
            }
            if (_value == MaxSigned)
            {
                return nameof(MaxSigned);
            }

            return string.Concat(
                (char)(uint8)(_value >> 24),
                (char)(uint8)(_value >> 16),
                (char)(uint8)(_value >> 8),
                (char)(uint8)_value);
        }

        public static implicit operator uint32(OpenTypeTag tag) => tag._value;

        public static implicit operator OpenTypeTag(uint32 tag) => new OpenTypeTag(tag);
    }
}
