// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.
// Ported from: https://github.com/SixLabors/Fonts/blob/034a440aece357341fcc6b02db58ffbe153e54ef/src/SixLabors.Fonts

using System.Diagnostics;
using System.Text;

namespace Avalonia.Media.Fonts.Tables
{
    [DebuggerDisplay("Offset: {Offset}, Length: {Length}, Value: {Value}")]
    internal class StringLoader
    {
        public StringLoader(uint16 length, uint16 offset, Encoding encoding)
        {
            Length = length;
            Offset = offset;
            Encoding = encoding;
            Value = string.Empty;
        }

        public uint16 Length { get; }

        public uint16 Offset { get; }

        public string Value { get; private set; }

        public Encoding Encoding { get; }

        public static StringLoader Create(BigEndianBinaryReader reader)
            => Create(reader, Encoding.BigEndianUnicode);

        public static StringLoader Create(BigEndianBinaryReader reader, Encoding encoding)
            => new StringLoader(reader.ReadUInt16(), reader.ReadUInt16(), encoding);

        public void LoadValue(BigEndianBinaryReader reader)
            => Value = reader.ReadString(Length, Encoding).Replace("\0", string.Empty);
    }
}
