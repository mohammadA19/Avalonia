// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.
// Ported from: https://github.com/SixLabors/Fonts/blob/034a440aece357341fcc6b02db58ffbe153e54ef/src/SixLabors.Fonts

using System.IO;

namespace Avalonia.Media.Fonts.Tables
{
    internal class HorizontalHeadTable
    {
        internal const string TableName = "hhea";
        internal static OpenTypeTag Tag = OpenTypeTag.Parse(TableName);

        public HorizontalHeadTable(
            int16 ascender,
            int16 descender,
            int16 lineGap,
            uint16 advanceWidthMax,
            int16 minLeftSideBearing,
            int16 minRightSideBearing,
            int16 xMaxExtent,
            int16 caretSlopeRise,
            int16 caretSlopeRun,
            int16 caretOffset,
            uint16 numberOfHMetrics)
        {
            Ascender = ascender;
            Descender = descender;
            LineGap = lineGap;
            AdvanceWidthMax = advanceWidthMax;
            MinLeftSideBearing = minLeftSideBearing;
            MinRightSideBearing = minRightSideBearing;
            XMaxExtent = xMaxExtent;
            CaretSlopeRise = caretSlopeRise;
            CaretSlopeRun = caretSlopeRun;
            CaretOffset = caretOffset;
            NumberOfHMetrics = numberOfHMetrics;
        }

        public uint16 AdvanceWidthMax { get; }

        public int16 Ascender { get; }

        public int16 CaretOffset { get; }

        public int16 CaretSlopeRise { get; }

        public int16 CaretSlopeRun { get; }

        public int16 Descender { get; }

        public int16 LineGap { get; }

        public int16 MinLeftSideBearing { get; }

        public int16 MinRightSideBearing { get; }

        public uint16 NumberOfHMetrics { get; }

        public int16 XMaxExtent { get; }

        public static HorizontalHeadTable? Load(IGlyphTypeface glyphTypeface)
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

        public static HorizontalHeadTable Load(BigEndianBinaryReader reader)
        {
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | Type   | Name                | Description                                                                     |
            // +========+=====================+=================================================================================+
            // | Fixed  | version             | 0x00010000 (1.0)                                                                |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | ascent              | Distance from baseline of highest ascender                                      |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | descent             | Distance from baseline of lowest descender                                      |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | lineGap             | typographic line gap                                                            |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | uFWord | advanceWidthMax     | must be consistent with horizontal metrics                                      |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | minLeftSideBearing  | must be consistent with horizontal metrics                                      |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | minRightSideBearing | must be consistent with horizontal metrics                                      |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | xMaxExtent          | max(lsb + (xMax-xMin))                                                          |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | caretSlopeRise      | used to calculate the slope of the caret (rise/run) set to 1 for vertical caret |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | caretSlopeRun       | 0 for vertical                                                                  |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | FWord  | caretOffset         | set value to 0 for non-slanted fonts                                            |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | reserved            | set value to 0                                                                  |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | reserved            | set value to 0                                                                  |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | reserved            | set value to 0                                                                  |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | reserved            | set value to 0                                                                  |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | int16  | metricDataFormat    | 0 for current format                                                            |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            // | uint16 | numOfLongHorMetrics | number of advance widths in metrics table                                       |
            // +--------+---------------------+---------------------------------------------------------------------------------+
            uint16 majorVersion = reader.ReadUInt16();
            uint16 minorVersion = reader.ReadUInt16();
            int16 ascender = reader.ReadFWORD();
            int16 descender = reader.ReadFWORD();
            int16 lineGap = reader.ReadFWORD();
            uint16 advanceWidthMax = reader.ReadUFWORD();
            int16 minLeftSideBearing = reader.ReadFWORD();
            int16 minRightSideBearing = reader.ReadFWORD();
            int16 xMaxExtent = reader.ReadFWORD();
            int16 caretSlopeRise = reader.ReadInt16();
            int16 caretSlopeRun = reader.ReadInt16();
            int16 caretOffset = reader.ReadInt16();
            reader.ReadInt16(); // reserved
            reader.ReadInt16(); // reserved
            reader.ReadInt16(); // reserved
            reader.ReadInt16(); // reserved
            int16 metricDataFormat = reader.ReadInt16(); // 0
            if (metricDataFormat != 0)
            {
                throw new InvalidFontTableException($"Expected metricDataFormat = 0 found {metricDataFormat}", TableName);
            }

            uint16 numberOfHMetrics = reader.ReadUInt16();

            return new HorizontalHeadTable(
                ascender,
                descender,
                lineGap,
                advanceWidthMax,
                minLeftSideBearing,
                minRightSideBearing,
                xMaxExtent,
                caretSlopeRise,
                caretSlopeRun,
                caretOffset,
                numberOfHMetrics);
        }
    }
}
