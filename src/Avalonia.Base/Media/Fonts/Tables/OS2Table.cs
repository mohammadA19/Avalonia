// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.
// Ported from: https://github.com/SixLabors/Fonts/blob/034a440aece357341fcc6b02db58ffbe153e54ef/src/SixLabors.Fonts

using System;
using System.IO;

namespace Avalonia.Media.Fonts.Tables
{
    internal sealed class OS2Table
    {
        internal const string TableName = "OS/2";
        internal static OpenTypeTag Tag = OpenTypeTag.Parse(TableName);
  
        private readonly uint8[] panose;
        private readonly int16 capHeight;
        private readonly int16 familyClass;
        private readonly int16 heightX;
        private readonly string tag;
        private readonly uint16 codePageRange1;
        private readonly uint16 codePageRange2;
        private readonly uint32 unicodeRange1;
        private readonly uint32 unicodeRange2;
        private readonly uint32 unicodeRange3;
        private readonly uint32 unicodeRange4;
        private readonly uint16 breakChar;
        private readonly uint16 defaultChar;
        private readonly uint16 firstCharIndex;
        private readonly uint16 lastCharIndex;
        private readonly uint16 lowerOpticalPointSize;
        private readonly uint16 maxContext;
        private readonly uint16 upperOpticalPointSize;
        private readonly int16 averageCharWidth;

        public OS2Table(
            int16 averageCharWidth,
            uint16 weightClass,
            uint16 widthClass,
            uint16 styleType,
            int16 subscriptXSize,
            int16 subscriptYSize,
            int16 subscriptXOffset,
            int16 subscriptYOffset,
            int16 superscriptXSize,
            int16 superscriptYSize,
            int16 superscriptXOffset,
            int16 superscriptYOffset,
            int16 strikeoutSize,
            int16 strikeoutPosition,
            int16 familyClass,
            uint8[] panose,
            uint32 unicodeRange1,
            uint32 unicodeRange2,
            uint32 unicodeRange3,
            uint32 unicodeRange4,
            string tag,
            FontStyleSelection fontStyle,
            uint16 firstCharIndex,
            uint16 lastCharIndex,
            int16 typoAscender,
            int16 typoDescender,
            int16 typoLineGap,
            uint16 winAscent,
            uint16 winDescent)
        {
            this.averageCharWidth = averageCharWidth;
            WeightClass = weightClass;
            WidthClass = widthClass;
            StyleType = styleType;
            SubscriptXSize = subscriptXSize;
            SubscriptYSize = subscriptYSize;
            SubscriptXOffset = subscriptXOffset;
            SubscriptYOffset = subscriptYOffset;
            SuperscriptXSize = superscriptXSize;
            SuperscriptYSize = superscriptYSize;
            SuperscriptXOffset = superscriptXOffset;
            SuperscriptYOffset = superscriptYOffset;
            StrikeoutSize = strikeoutSize;
            StrikeoutPosition = strikeoutPosition;
            this.familyClass = familyClass;
            this.panose = panose;
            this.unicodeRange1 = unicodeRange1;
            this.unicodeRange2 = unicodeRange2;
            this.unicodeRange3 = unicodeRange3;
            this.unicodeRange4 = unicodeRange4;
            this.tag = tag;
            FontStyle = fontStyle;
            this.firstCharIndex = firstCharIndex;
            this.lastCharIndex = lastCharIndex;
            TypoAscender = typoAscender;
            TypoDescender = typoDescender;
            TypoLineGap = typoLineGap;
            WinAscent = winAscent;
            WinDescent = winDescent;
        }

        public OS2Table(
            OS2Table version0Table,
            uint16 codePageRange1,
            uint16 codePageRange2,
            int16 heightX,
            int16 capHeight,
            uint16 defaultChar,
            uint16 breakChar,
            uint16 maxContext)
            : this(
                version0Table.averageCharWidth,
                version0Table.WeightClass,
                version0Table.WidthClass,
                version0Table.StyleType,
                version0Table.SubscriptXSize,
                version0Table.SubscriptYSize,
                version0Table.SubscriptXOffset,
                version0Table.SubscriptYOffset,
                version0Table.SuperscriptXSize,
                version0Table.SuperscriptYSize,
                version0Table.SuperscriptXOffset,
                version0Table.SuperscriptYOffset,
                version0Table.StrikeoutSize,
                version0Table.StrikeoutPosition,
                version0Table.familyClass,
                version0Table.panose,
                version0Table.unicodeRange1,
                version0Table.unicodeRange2,
                version0Table.unicodeRange3,
                version0Table.unicodeRange4,
                version0Table.tag,
                version0Table.FontStyle,
                version0Table.firstCharIndex,
                version0Table.lastCharIndex,
                version0Table.TypoAscender,
                version0Table.TypoDescender,
                version0Table.TypoLineGap,
                version0Table.WinAscent,
                version0Table.WinDescent)
        {
            this.codePageRange1 = codePageRange1;
            this.codePageRange2 = codePageRange2;
            this.heightX = heightX;
            this.capHeight = capHeight;
            this.defaultChar = defaultChar;
            this.breakChar = breakChar;
            this.maxContext = maxContext;
        }

        public OS2Table(OS2Table versionLessThan5Table, uint16 lowerOpticalPointSize, uint16 upperOpticalPointSize)
            : this(
                versionLessThan5Table,
                versionLessThan5Table.codePageRange1,
                versionLessThan5Table.codePageRange2,
                versionLessThan5Table.heightX,
                versionLessThan5Table.capHeight,
                versionLessThan5Table.defaultChar,
                versionLessThan5Table.breakChar,
                versionLessThan5Table.maxContext)
        {
            this.lowerOpticalPointSize = lowerOpticalPointSize;
            this.upperOpticalPointSize = upperOpticalPointSize;
        }

        [Flags]
        internal enum FontStyleSelection : uint16
        {
            /// <summary>
            /// Font contains italic or oblique characters, otherwise they are upright.
            /// </summary>
            ITALIC = 1,

            /// <summary>
            /// Characters are underscored.
            /// </summary>
            UNDERSCORE = 1 << 1,

            /// <summary>
            /// Characters have their foreground and background reversed.
            /// </summary>
            NEGATIVE = 1 << 2,

            /// <summary>
            /// characters, otherwise they are solid.
            /// </summary>
            OUTLINED = 1 << 3,

            /// <summary>
            /// Characters are overstruck.
            /// </summary>
            STRIKEOUT = 1 << 4,

            /// <summary>
            /// Characters are emboldened.
            /// </summary>
            BOLD = 1 << 5,

            /// <summary>
            /// Characters are in the standard weight/style for the font.
            /// </summary>
            REGULAR = 1 << 6,

            /// <summary>
            /// If set, it is strongly recommended to use OS/2.typoAscender - OS/2.typoDescender+ OS/2.typoLineGap as a value for default line spacing for this font.
            /// </summary>
            USE_TYPO_METRICS = 1 << 7,

            /// <summary>
            /// The font has ‘name’ table strings consistent with a weight/width/slope family without requiring use of ‘name’ IDs 21 and 22. (Please see more detailed description below.)
            /// </summary>
            WWS = 1 << 8,

            /// <summary>
            /// Font contains oblique characters.
            /// </summary>
            OBLIQUE = 1 << 9,

            // 10–15        <reserved>  Reserved; set to 0.
        }

        public FontStyleSelection FontStyle { get; }

        public int16 TypoAscender { get; }

        public int16 TypoDescender { get; }

        public int16 TypoLineGap { get; }

        public uint16 WinAscent { get; }

        public uint16 WinDescent { get; }

        public int16 StrikeoutPosition { get; }

        public int16 StrikeoutSize { get; }

        public int16 SubscriptXOffset { get; }

        public int16 SubscriptXSize { get; }

        public int16 SubscriptYOffset { get; }

        public int16 SubscriptYSize { get; }

        public int16 SuperscriptXOffset { get; }

        public int16 SuperscriptXSize { get; }

        public int16 SuperscriptYOffset { get; }

        public int16 SuperscriptYSize { get; }

        public uint16 StyleType { get; }

        public uint16 WeightClass { get; }

        public uint16 WidthClass { get; }

        public static OS2Table? Load(IGlyphTypeface glyphTypeface)
        {
            if (!glyphTypeface.TryGetTable(Tag, out var table))
            {
                return null;
            }

            using var stream = new MemoryStream(table);
            using var binaryReader = new BigEndianBinaryReader(stream, false);

            // Move to start of table.
            return Load(binaryReader);
        }

        public static OS2Table Load(BigEndianBinaryReader reader)
        {
            // Version 1.0
            // Type   | Name                   | Comments
            // -------|------------------------|-----------------------
            // uint16 |version                 | 0x0005
            // int16  |xAvgCharWidth           |
            // uint16 |usWeightClass           |
            // uint16 |usWidthClass            |
            // uint16 |fsType                  |
            // int16  |ySubscriptXSize         |
            // int16  |ySubscriptYSize         |
            // int16  |ySubscriptXOffset       |
            // int16  |ySubscriptYOffset       |
            // int16  |ySuperscriptXSize       |
            // int16  |ySuperscriptYSize       |
            // int16  |ySuperscriptXOffset     |
            // int16  |ySuperscriptYOffset     |
            // int16  |yStrikeoutSize          |
            // int16  |yStrikeoutPosition      |
            // int16  |sFamilyClass            |
            // uint8  |panose[10]              |
            // uint32 |ulUnicodeRange1         | Bits 0–31
            // uint32 |ulUnicodeRange2         | Bits 32–63
            // uint32 |ulUnicodeRange3         | Bits 64–95
            // uint32 |ulUnicodeRange4         | Bits 96–127
            // Tag    |achVendID               |
            // uint16 |fsSelection             |
            // uint16 |usFirstCharIndex        |
            // uint16 |usLastCharIndex         |
            // int16  |sTypoAscender           |
            // int16  |sTypoDescender          |
            // int16  |sTypoLineGap            |
            // uint16 |usWinAscent             |
            // uint16 |usWinDescent            |
            // uint32 |ulCodePageRange1        | Bits 0–31
            // uint32 |ulCodePageRange2        | Bits 32–63
            // int16  |sxHeight                |
            // int16  |sCapHeight              |
            // uint16 |usDefaultChar           |
            // uint16 |usBreakChar             |
            // uint16 |usMaxContext            |
            // uint16 |usLowerOpticalPointSize |
            // uint16 |usUpperOpticalPointSize |
            uint16 version = reader.ReadUInt16(); // assert 0x0005
            int16 averageCharWidth = reader.ReadInt16();
            uint16 weightClass = reader.ReadUInt16();
            uint16 widthClass = reader.ReadUInt16();
            uint16 styleType = reader.ReadUInt16();
            int16 subscriptXSize = reader.ReadInt16();
            int16 subscriptYSize = reader.ReadInt16();
            int16 subscriptXOffset = reader.ReadInt16();
            int16 subscriptYOffset = reader.ReadInt16();

            int16 superscriptXSize = reader.ReadInt16();
            int16 superscriptYSize = reader.ReadInt16();
            int16 superscriptXOffset = reader.ReadInt16();
            int16 superscriptYOffset = reader.ReadInt16();

            int16 strikeoutSize = reader.ReadInt16();
            int16 strikeoutPosition = reader.ReadInt16();
            int16 familyClass = reader.ReadInt16();
            uint8[] panose = reader.ReadUInt8Array(10);
            uint32 unicodeRange1 = reader.ReadUInt32(); // Bits 0–31
            uint32 unicodeRange2 = reader.ReadUInt32(); // Bits 32–63
            uint32 unicodeRange3 = reader.ReadUInt32(); // Bits 64–95
            uint32 unicodeRange4 = reader.ReadUInt32(); // Bits 96–127
            string tag = reader.ReadTag();
            FontStyleSelection fontStyle = reader.ReadUInt16<FontStyleSelection>();
            uint16 firstCharIndex = reader.ReadUInt16();
            uint16 lastCharIndex = reader.ReadUInt16();
            int16 typoAscender = reader.ReadInt16();
            int16 typoDescender = reader.ReadInt16();
            int16 typoLineGap = reader.ReadInt16();
            uint16 winAscent = reader.ReadUInt16();
            uint16 winDescent = reader.ReadUInt16();

            var version0Table = new OS2Table(
                    averageCharWidth,
                    weightClass,
                    widthClass,
                    styleType,
                    subscriptXSize,
                    subscriptYSize,
                    subscriptXOffset,
                    subscriptYOffset,
                    superscriptXSize,
                    superscriptYSize,
                    superscriptXOffset,
                    superscriptYOffset,
                    strikeoutSize,
                    strikeoutPosition,
                    familyClass,
                    panose,
                    unicodeRange1,
                    unicodeRange2,
                    unicodeRange3,
                    unicodeRange4,
                    tag,
                    fontStyle,
                    firstCharIndex,
                    lastCharIndex,
                    typoAscender,
                    typoDescender,
                    typoLineGap,
                    winAscent,
                    winDescent);

            if (version == 0)
            {
                return version0Table;
            }

            int16 heightX = 0;
            int16 capHeight = 0;

            uint16 defaultChar = 0;
            uint16 breakChar = 0;
            uint16 maxContext = 0;

            uint16 codePageRange1 = reader.ReadUInt16(); // Bits 0–31
            uint16 codePageRange2 = reader.ReadUInt16(); // Bits 32–63

            // fields exist only in > v1 https://docs.microsoft.com/en-us/typography/opentype/spec/os2
            if (version > 1)
            {
                heightX = reader.ReadInt16();
                capHeight = reader.ReadInt16();
                defaultChar = reader.ReadUInt16();
                breakChar = reader.ReadUInt16();
                maxContext = reader.ReadUInt16();
            }

            var versionLessThan5Table = new OS2Table(
                    version0Table,
                    codePageRange1,
                    codePageRange2,
                    heightX,
                    capHeight,
                    defaultChar,
                    breakChar,
                    maxContext);

            if (version < 5)
            {
                return versionLessThan5Table;
            }

            uint16 lowerOpticalPointSize = reader.ReadUInt16();
            uint16 upperOpticalPointSize = reader.ReadUInt16();

            return new OS2Table(
                versionLessThan5Table,
                lowerOpticalPointSize,
                upperOpticalPointSize);
        }
    }
}
