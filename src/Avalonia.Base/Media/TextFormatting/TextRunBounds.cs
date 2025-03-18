namespace Avalonia.Media.TextFormatting
{
    /// <summary>
    /// The bounding rectangle of text run
    /// </summary>
    public readonly record struct TextRunBounds
    {
        /// <summary>
        /// Constructing TextRunBounds
        /// </summary>
        internal TextRunBounds(Rect bounds, int32 firstCharacterIndex, int32 length, TextRun textRun)
        {
            Rectangle = bounds;
            TextSourceCharacterIndex = firstCharacterIndex;
            Length = length;
            TextRun = textRun;
        }

        /// <summary>
        /// First text source character index of text run
        /// </summary>
        public int32 TextSourceCharacterIndex { get; }

        /// <summary>
        /// character length of bounded text run
        /// </summary>
        public int32 Length { get; }

        /// <summary>
        /// Text run bounding rectangle
        /// </summary>
        public Rect Rectangle { get; }

        /// <summary>
        /// text run
        /// </summary>
        public TextRun TextRun { get; }
    }
}
