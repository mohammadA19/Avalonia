using Avalonia.Media;
using Avalonia.Utilities;
using FlatColor = Avalonia.Controls.FlatColorPalette.FlatColor;

namespace Avalonia.Controls
{
    /// <summary>
    /// Implements half of the <see cref="FlatColorPalette"/> for improved usability.
    /// </summary>
    /// <inheritdoc cref="FlatColorPalette"/>
    public class FlatHalfColorPalette : IColorPalette
    {
        private static Color[,]? _colorChart = null;
        private static readonly object _colorChartMutex = new();

        /// <summary>
        /// Initializes all color chart colors.
        /// </summary>
        protected void InitColorChart()
        {
            lock (_colorChartMutex)
            {
                if (_colorChart != null)
                {
                    return;
                }

                _colorChart = new Color[,]
                {
                    // Pomegranate
                    {
                        Color.FromUInt32((uint32)FlatColor.Pomegranate1),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate3),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate5),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate7),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate9),
                    },

                    // Amethyst
                    {
                        Color.FromUInt32((uint32)FlatColor.Amethyst1),
                        Color.FromUInt32((uint32)FlatColor.Amethyst3),
                        Color.FromUInt32((uint32)FlatColor.Amethyst5),
                        Color.FromUInt32((uint32)FlatColor.Amethyst7),
                        Color.FromUInt32((uint32)FlatColor.Amethyst9),
                    },

                    // Belize Hole
                    {
                        Color.FromUInt32((uint32)FlatColor.BelizeHole1),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole3),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole5),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole7),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole9),
                    },

                    // Turquoise
                    {
                        Color.FromUInt32((uint32)FlatColor.Turquoise1),
                        Color.FromUInt32((uint32)FlatColor.Turquoise3),
                        Color.FromUInt32((uint32)FlatColor.Turquoise5),
                        Color.FromUInt32((uint32)FlatColor.Turquoise7),
                        Color.FromUInt32((uint32)FlatColor.Turquoise9),
                    },

                    // Nephritis
                    {
                        Color.FromUInt32((uint32)FlatColor.Nephritis1),
                        Color.FromUInt32((uint32)FlatColor.Nephritis3),
                        Color.FromUInt32((uint32)FlatColor.Nephritis5),
                        Color.FromUInt32((uint32)FlatColor.Nephritis7),
                        Color.FromUInt32((uint32)FlatColor.Nephritis9),
                    },

                    // Sunflower
                    {
                        Color.FromUInt32((uint32)FlatColor.Sunflower1),
                        Color.FromUInt32((uint32)FlatColor.Sunflower3),
                        Color.FromUInt32((uint32)FlatColor.Sunflower5),
                        Color.FromUInt32((uint32)FlatColor.Sunflower7),
                        Color.FromUInt32((uint32)FlatColor.Sunflower9),
                    },

                    // Carrot
                    {
                        Color.FromUInt32((uint32)FlatColor.Carrot1),
                        Color.FromUInt32((uint32)FlatColor.Carrot3),
                        Color.FromUInt32((uint32)FlatColor.Carrot5),
                        Color.FromUInt32((uint32)FlatColor.Carrot7),
                        Color.FromUInt32((uint32)FlatColor.Carrot9),
                    },

                    // Clouds
                    {
                        Color.FromUInt32((uint32)FlatColor.Clouds1),
                        Color.FromUInt32((uint32)FlatColor.Clouds3),
                        Color.FromUInt32((uint32)FlatColor.Clouds5),
                        Color.FromUInt32((uint32)FlatColor.Clouds7),
                        Color.FromUInt32((uint32)FlatColor.Clouds9),
                    },

                    // Concrete
                    {
                        Color.FromUInt32((uint32)FlatColor.Concrete1),
                        Color.FromUInt32((uint32)FlatColor.Concrete3),
                        Color.FromUInt32((uint32)FlatColor.Concrete5),
                        Color.FromUInt32((uint32)FlatColor.Concrete7),
                        Color.FromUInt32((uint32)FlatColor.Concrete9),
                    },

                    // Wet Asphalt
                    {
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt1),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt3),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt5),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt7),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt9),
                    },
                };
            }

            return;
        }

        /// <inheritdoc/>
        public int32 ColorCount
        {
            get => 10;
        }

        /// <inheritdoc/>
        public int32 ShadeCount
        {
            get => 5;
        }

        /// <inheritdoc/>
        public Color GetColor(int32 colorIndex, int32 shadeIndex)
        {
            if (_colorChart == null)
            {
                InitColorChart();
            }

            return _colorChart![
                MathUtilities.Clamp(colorIndex, 0, ColorCount - 1),
                MathUtilities.Clamp(shadeIndex, 0, ShadeCount - 1)];
        }
    }
}
