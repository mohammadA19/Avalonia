using Avalonia.Media;
using Avalonia.Utilities;
using MaterialColor = Avalonia.Controls.MaterialColorPalette.MaterialColor;

namespace Avalonia.Controls
{
    /// <summary>
    /// Implements half of the <see cref="MaterialColorPalette"/> for improved usability.
    /// </summary>
    /// <inheritdoc cref="MaterialColorPalette"/>
    public class MaterialHalfColorPalette : IColorPalette
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
                    // Red
                    {
                        Color.FromUInt32((uint32)MaterialColor.Red50),
                        Color.FromUInt32((uint32)MaterialColor.Red200),
                        Color.FromUInt32((uint32)MaterialColor.Red400),
                        Color.FromUInt32((uint32)MaterialColor.Red600),
                        Color.FromUInt32((uint32)MaterialColor.Red800),
                    },

                    // Purple
                    {
                        Color.FromUInt32((uint32)MaterialColor.Purple50),
                        Color.FromUInt32((uint32)MaterialColor.Purple200),
                        Color.FromUInt32((uint32)MaterialColor.Purple400),
                        Color.FromUInt32((uint32)MaterialColor.Purple600),
                        Color.FromUInt32((uint32)MaterialColor.Purple800),
                    },

                    // Indigo
                    {
                        Color.FromUInt32((uint32)MaterialColor.Indigo50),
                        Color.FromUInt32((uint32)MaterialColor.Indigo200),
                        Color.FromUInt32((uint32)MaterialColor.Indigo400),
                        Color.FromUInt32((uint32)MaterialColor.Indigo600),
                        Color.FromUInt32((uint32)MaterialColor.Indigo800),
                    },

                    // Light Blue
                    {
                        Color.FromUInt32((uint32)MaterialColor.LightBlue50),
                        Color.FromUInt32((uint32)MaterialColor.LightBlue200),
                        Color.FromUInt32((uint32)MaterialColor.LightBlue400),
                        Color.FromUInt32((uint32)MaterialColor.LightBlue600),
                        Color.FromUInt32((uint32)MaterialColor.LightBlue800),
                    },

                    // Teal
                    {
                        Color.FromUInt32((uint32)MaterialColor.Teal50),
                        Color.FromUInt32((uint32)MaterialColor.Teal200),
                        Color.FromUInt32((uint32)MaterialColor.Teal400),
                        Color.FromUInt32((uint32)MaterialColor.Teal600),
                        Color.FromUInt32((uint32)MaterialColor.Teal800),
                    },

                    // Light Green
                    {
                        Color.FromUInt32((uint32)MaterialColor.LightGreen50),
                        Color.FromUInt32((uint32)MaterialColor.LightGreen200),
                        Color.FromUInt32((uint32)MaterialColor.LightGreen400),
                        Color.FromUInt32((uint32)MaterialColor.LightGreen600),
                        Color.FromUInt32((uint32)MaterialColor.LightGreen800),
                    },

                    // Yellow
                    {
                        Color.FromUInt32((uint32)MaterialColor.Yellow50),
                        Color.FromUInt32((uint32)MaterialColor.Yellow200),
                        Color.FromUInt32((uint32)MaterialColor.Yellow400),
                        Color.FromUInt32((uint32)MaterialColor.Yellow600),
                        Color.FromUInt32((uint32)MaterialColor.Yellow800),
                    },

                    // Orange
                    {
                        Color.FromUInt32((uint32)MaterialColor.Orange50),
                        Color.FromUInt32((uint32)MaterialColor.Orange200),
                        Color.FromUInt32((uint32)MaterialColor.Orange400),
                        Color.FromUInt32((uint32)MaterialColor.Orange600),
                        Color.FromUInt32((uint32)MaterialColor.Orange800),
                    },

                    // Brown
                    {
                        Color.FromUInt32((uint32)MaterialColor.Brown50),
                        Color.FromUInt32((uint32)MaterialColor.Brown200),
                        Color.FromUInt32((uint32)MaterialColor.Brown400),
                        Color.FromUInt32((uint32)MaterialColor.Brown600),
                        Color.FromUInt32((uint32)MaterialColor.Brown800),
                    },

                    // Blue Gray
                    {
                        Color.FromUInt32((uint32)MaterialColor.BlueGray50),
                        Color.FromUInt32((uint32)MaterialColor.BlueGray200),
                        Color.FromUInt32((uint32)MaterialColor.BlueGray400),
                        Color.FromUInt32((uint32)MaterialColor.BlueGray600),
                        Color.FromUInt32((uint32)MaterialColor.BlueGray800),
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
