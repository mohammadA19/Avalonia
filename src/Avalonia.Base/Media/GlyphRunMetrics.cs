namespace Avalonia.Media
{
    public readonly record struct GlyphRunMetrics
    {
        public double Baseline { get; init; }

        public double Width { get; init; }

        public double WidthIncludingTrailingWhitespace { get; init; }

        public double Height { get; init; }

        public int32 TrailingWhitespaceLength { get; init; }

        public int32 NewLineLength { get; init; }

        public int32 FirstCluster { get; init; }

        public int32 LastCluster { get; init; }
    }
}
