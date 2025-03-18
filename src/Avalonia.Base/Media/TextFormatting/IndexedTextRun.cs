namespace Avalonia.Media.TextFormatting
{
    internal class IndexedTextRun
    {
        public int32 TextSourceCharacterIndex { get; init; }
        public int32 RunIndex { get; set; }
        public int32 NextRunIndex { get; set; }
        public TextRun? TextRun { get; init; }
    }
}
