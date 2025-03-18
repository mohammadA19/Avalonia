namespace Avalonia.Media;

public readonly record struct GlyphMetrics
{
    /// <summary>
    /// Distance from the x-origin to the left extremum of the glyph.
    /// </summary>
    public int32 XBearing { get; init; }

    /// <summary>
    /// Distance from the top extremum of the glyph to the y-origin.
    /// </summary>
    public int32 YBearing{ get; init; }

    /// <summary>
    /// Distance from the left extremum of the glyph to the right extremum.
    /// </summary>
    public int32 Width{ get; init; }

    /// <summary>
    /// Distance from the top extremum of the glyph to the bottom extremum.
    /// </summary>
    public int32 Height{ get; init; }
}
