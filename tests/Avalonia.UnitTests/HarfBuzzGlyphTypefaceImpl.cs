using System;
using System.IO;
using Avalonia.Media;
using HarfBuzzSharp;

namespace Avalonia.UnitTests
{
    public class HarfBuzzGlyphTypefaceImpl : IGlyphTypeface
    {
        private bool _isDisposed;
        private Blob _blob;

        public HarfBuzzGlyphTypefaceImpl(Stream data)
        {
            _blob = Blob.FromStream(data);
            
            Face = new Face(_blob, 0);

            Font = new Font(Face);

            Font.SetFunctionsOpenType();

            Font.GetScale(out var scale, out _);

            const double defaultFontRenderingEmSize = 12.0;

            var metrics = Font.OpenTypeMetrics;

            Metrics = new FontMetrics
            {
                DesignEmHeight = (int16)scale,
                Ascent = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.HorizontalAscender) / defaultFontRenderingEmSize * scale),
                Descent = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.HorizontalDescender) / defaultFontRenderingEmSize * scale),
                LineGap = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.HorizontalLineGap) / defaultFontRenderingEmSize * scale),

                UnderlinePosition = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.UnderlineOffset) / defaultFontRenderingEmSize * scale),

                UnderlineThickness = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.UnderlineSize) / defaultFontRenderingEmSize * scale),

                StrikethroughPosition = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.StrikeoutOffset) / defaultFontRenderingEmSize * scale),

                StrikethroughThickness = (int32)(metrics.GetXVariation(OpenTypeMetricsTag.StrikeoutSize) / defaultFontRenderingEmSize * scale),

                IsFixedPitch = GetGlyphAdvance(GetGlyph('a')) == GetGlyphAdvance(GetGlyph('b'))
            };           

            GlyphCount = Face.GlyphCount;
        }

        public FontMetrics Metrics { get; }

        public Face Face { get; }

        public Font Font { get; }

        public int32 GlyphCount { get; set; }

        public FontSimulations FontSimulations { get; }

        public string FamilyName => "$Default";

        public FontWeight Weight { get; }

        public FontStyle Style { get; }

        public FontStretch Stretch { get; }


        /// <inheritdoc cref="IGlyphTypeface"/>
        public uint16 GetGlyph(uint32 codepoint)
        {
            if (Font.TryGetGlyph(codepoint, out var glyph))
            {
                return (uint16)glyph;
            }

            return 0;
        }

        public bool TryGetGlyph(uint32 codepoint,out uint16 glyph)
        {
            glyph = 0;

            if (Font.TryGetGlyph(codepoint, out var glyphId))
            {
                glyph = (uint16)glyphId;

                return true;
            }

            return false;
        }

        /// <inheritdoc cref="IGlyphTypeface"/>
        public uint16[] GetGlyphs(ReadOnlySpan<uint32> codepoints)
        {
            var glyphs = new uint16[codepoints.Length];

            for (var i = 0; i < codepoints.Length; i++)
            {
                if (Font.TryGetGlyph(codepoints[i], out var glyph))
                {
                    glyphs[i] = (uint16)glyph;
                }
            }

            return glyphs;
        }

        /// <inheritdoc cref="IGlyphTypeface"/>
        public int32 GetGlyphAdvance(uint16 glyph)
        {
            return Font.GetHorizontalGlyphAdvance(glyph);
        }

        /// <inheritdoc cref="IGlyphTypeface"/>
        public int32[] GetGlyphAdvances(ReadOnlySpan<uint16> glyphs)
        {
            var glyphIndices = new uint32[glyphs.Length];

            for (var i = 0; i < glyphs.Length; i++)
            {
                glyphIndices[i] = glyphs[i];
            }

            return Font.GetHorizontalGlyphAdvances(glyphIndices);
        }

        public bool TryGetTable(uint32 tag, out uint8[] table)
        {
            table = null;
            var blob = Face.ReferenceTable(tag);

            if (blob.Length > 0)
            {
                table = blob.AsSpan().ToArray();

                return true;
            }

            return false;
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (!disposing)
            {
                return;
            }

            Font?.Dispose();
            Face?.Dispose();
            _blob?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool TryGetGlyphMetrics(uint16 glyph, out GlyphMetrics metrics)
        {
            metrics = default;

            if (!Font.TryGetGlyphExtents(glyph, out var extents))
            {
                return false;
            }

            metrics = new GlyphMetrics
            {
                XBearing = extents.XBearing,
                YBearing = extents.YBearing,
                Width = extents.Width,
                Height = extents.Height
            };

            return true;
        }
    }
}
