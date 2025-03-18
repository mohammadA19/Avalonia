namespace Avalonia.Media
{
    /// <summary>
    /// The font metrics is holding information about a font's ascent, descent, etc. in design em units.
    /// </summary>
    public readonly record struct FontMetrics
    {
        /// <summary>
        ///     Gets the font design units per em.
        /// </summary>
        public int16 DesignEmHeight { get; init; }

        /// <summary>
        ///     A <see cref="bool"/> value indicating whether all glyphs in the font have the same advancement. 
        /// </summary>
        public bool IsFixedPitch { get; init; }

        /// <summary>
        ///     Gets the recommended distance above the baseline in design em size. 
        /// </summary>
        public int32 Ascent { get; init; }

        /// <summary>
        ///     Gets the recommended distance under the baseline in design em size. 
        /// </summary>
        public int32 Descent { get; init; }

        /// <summary>
        ///      Gets the recommended additional space between two lines of text in design em size. 
        /// </summary>
        public int32 LineGap { get; init; }

        /// <summary>
        ///     Gets the recommended line spacing of a formed text line.
        /// </summary>
        public int32 LineSpacing => Descent - Ascent + LineGap;

        /// <summary>
        ///     Gets a value that indicates the distance of the underline from the baseline in design em size.
        /// </summary>
        public int32 UnderlinePosition { get; init; }

        /// <summary>
        ///     Gets a value that indicates the thickness of the underline in design em size.
        /// </summary>
        public int32 UnderlineThickness { get; init; }

        /// <summary>
        ///     Gets a value that indicates the distance of the strikethrough from the baseline in design em size.
        /// </summary>
        public int32 StrikethroughPosition { get; init; }

        /// <summary>
        ///     Gets a value that indicates the thickness of the underline in design em size.
        /// </summary>
        public int32 StrikethroughThickness { get; init; }
    }
}
