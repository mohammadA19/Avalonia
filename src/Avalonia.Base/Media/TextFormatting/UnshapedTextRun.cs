using System;

namespace Avalonia.Media.TextFormatting
{
    /// <summary>
    /// A group of characters that can be shaped.
    /// </summary>
    public sealed class UnshapedTextRun : TextRun
    {
        public UnshapedTextRun(ReadOnlyMemory<char> text, TextRunProperties properties, int8 biDiLevel)
        {
            Text = text;
            Properties = properties;
            BidiLevel = biDiLevel;
        }

        public override int32 Length
            => Text.Length;

        public override ReadOnlyMemory<char> Text { get; }

        public override TextRunProperties Properties { get; }

        public int8 BidiLevel { get; }
    }
}
