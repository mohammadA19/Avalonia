using Avalonia.Media;
using Avalonia.Utilities;

namespace Avalonia.Controls
{
    /// <summary>
    /// Implements a reduced flat design or flat UI color palette.
    /// </summary>
    /// <remarks>
    /// See:
    ///  - https://htmlcolorcodes.com/color-chart/
    ///  - https://htmlcolorcodes.com/color-chart/flat-design-color-chart/
    ///  - http://designmodo.github.io/Flat-UI/
    ///
    /// The GitHub project is licensed as MIT: https://github.com/designmodo/Flat-UI.
    ///
    /// </remarks>
    public class FlatColorPalette : IColorPalette
    {
        /// <summary>
        /// Defines all colors in the <see cref="FlatColorPalette"/>.
        /// </summary>
        /// <remarks>
        /// This is done in an enum to ensure it is compiled into the assembly improving
        /// startup performance.
        /// </remarks>
        public enum FlatColor : uint32
        {
            // Pomegranate
            Pomegranate1  = 0xFFF9EBEA,
            Pomegranate2  = 0xFFF2D7D5,
            Pomegranate3  = 0xFFE6B0AA,
            Pomegranate4  = 0xFFD98880,
            Pomegranate5  = 0xFFCD6155,
            Pomegranate6  = 0xFFC0392B,
            Pomegranate7  = 0xFFA93226,
            Pomegranate8  = 0xFF922B21,
            Pomegranate9  = 0xFF7B241C,
            Pomegranate10 = 0xFF641E16,

            // Alizarin
            Alizarin1  = 0xFFFDEDEC,
            Alizarin2  = 0xFFFADBD8,
            Alizarin3  = 0xFFF5B7B1,
            Alizarin4  = 0xFFF1948A,
            Alizarin5  = 0xFFEC7063,
            Alizarin6  = 0xFFE74C3C,
            Alizarin7  = 0xFFCB4335,
            Alizarin8  = 0xFFB03A2E,
            Alizarin9  = 0xFF943126,
            Alizarin10 = 0xFF78281F,

            // Amethyst
            Amethyst1  = 0xFFF5EEF8,
            Amethyst2  = 0xFFEBDEF0,
            Amethyst3  = 0xFFD7BDE2,
            Amethyst4  = 0xFFC39BD3,
            Amethyst5  = 0xFFAF7AC5,
            Amethyst6  = 0xFF9B59B6,
            Amethyst7  = 0xFF884EA0,
            Amethyst8  = 0xFF76448A,
            Amethyst9  = 0xFF633974,
            Amethyst10 = 0xFF512E5F,

            // Wisteria
            Wisteria1  = 0xFFF4ECF7,
            Wisteria2  = 0xFFE8DAEF,
            Wisteria3  = 0xFFD2B4DE,
            Wisteria4  = 0xFFBB8FCE,
            Wisteria5  = 0xFFA569BD,
            Wisteria6  = 0xFF8E44AD,
            Wisteria7  = 0xFF7D3C98,
            Wisteria8  = 0xFF6C3483,
            Wisteria9  = 0xFF5B2C6F,
            Wisteria10 = 0xFF4A235A,

            // Belize Hole
            BelizeHole1  = 0xFFEAF2F8,
            BelizeHole2  = 0xFFD4E6F1,
            BelizeHole3  = 0xFFA9CCE3,
            BelizeHole4  = 0xFF7FB3D5,
            BelizeHole5  = 0xFF5499C7,
            BelizeHole6  = 0xFF2980B9,
            BelizeHole7  = 0xFF2471A3,
            BelizeHole8  = 0xFF1F618D,
            BelizeHole9  = 0xFF1A5276,
            BelizeHole10 = 0xFF154360,

            // Peter River
            PeterRiver1  = 0xFFEBF5FB,
            PeterRiver2  = 0xFFD6EAF8,
            PeterRiver3  = 0xFFAED6F1,
            PeterRiver4  = 0xFF85C1E9,
            PeterRiver5  = 0xFF5DADE2,
            PeterRiver6  = 0xFF3498DB,
            PeterRiver7  = 0xFF2E86C1,
            PeterRiver8  = 0xFF2874A6,
            PeterRiver9  = 0xFF21618C,
            PeterRiver10 = 0xFF1B4F72,

            // Turquoise
            Turquoise1  = 0xFFE8F8F5,
            Turquoise2  = 0xFFD1F2EB,
            Turquoise3  = 0xFFA3E4D7,
            Turquoise4  = 0xFF76D7C4,
            Turquoise5  = 0xFF48C9B0,
            Turquoise6  = 0xFF1ABC9C,
            Turquoise7  = 0xFF17A589,
            Turquoise8  = 0xFF148F77,
            Turquoise9  = 0xFF117864,
            Turquoise10 = 0xFF0E6251,

            // Green Sea
            GreenSea1  = 0xFFE8F6F3,
            GreenSea2  = 0xFFD0ECE7,
            GreenSea3  = 0xFFA2D9CE,
            GreenSea4  = 0xFF73C6B6,
            GreenSea5  = 0xFF45B39D,
            GreenSea6  = 0xFF16A085,
            GreenSea7  = 0xFF138D75,
            GreenSea8  = 0xFF117A65,
            GreenSea9  = 0xFF0E6655,
            GreenSea10 = 0xFF0B5345,

            // Nephritis
            Nephritis1  = 0xFFE9F7EF,
            Nephritis2  = 0xFFD4EFDF,
            Nephritis3  = 0xFFA9DFBF,
            Nephritis4  = 0xFF7DCEA0,
            Nephritis5  = 0xFF52BE80,
            Nephritis6  = 0xFF27AE60,
            Nephritis7  = 0xFF229954,
            Nephritis8  = 0xFF1E8449,
            Nephritis9  = 0xFF196F3D,
            Nephritis10 = 0xFF145A32,

            // Emerald
            Emerald1  = 0xFFEAFAF1,
            Emerald2  = 0xFFD5F5E3,
            Emerald3  = 0xFFABEBC6,
            Emerald4  = 0xFF82E0AA,
            Emerald5  = 0xFF58D68D,
            Emerald6  = 0xFF2ECC71,
            Emerald7  = 0xFF28B463,
            Emerald8  = 0xFF239B56,
            Emerald9  = 0xFF1D8348,
            Emerald10 = 0xFF186A3B,

            // Sunflower
            Sunflower1  = 0xFFFEF9E7,
            Sunflower2  = 0xFFFCF3CF,
            Sunflower3  = 0xFFF9E79F,
            Sunflower4  = 0xFFF7DC6F,
            Sunflower5  = 0xFFF4D03F,
            Sunflower6  = 0xFFF1C40F,
            Sunflower7  = 0xFFD4AC0D,
            Sunflower8  = 0xFFB7950B,
            Sunflower9  = 0xFF9A7D0A,
            Sunflower10 = 0xFF7D6608,

            // Orange
            Orange1  = 0xFFFEF5E7,
            Orange2  = 0xFFFDEBD0,
            Orange3  = 0xFFFAD7A0,
            Orange4  = 0xFFF8C471,
            Orange5  = 0xFFF5B041,
            Orange6  = 0xFFF39C12,
            Orange7  = 0xFFD68910,
            Orange8  = 0xFFB9770E,
            Orange9  = 0xFF9C640C,
            Orange10 = 0xFF7E5109,

            // Carrot
            Carrot1  = 0xFFFDF2E9,
            Carrot2  = 0xFFFAE5D3,
            Carrot3  = 0xFFF5CBA7,
            Carrot4  = 0xFFF0B27A,
            Carrot5  = 0xFFEB984E,
            Carrot6  = 0xFFE67E22,
            Carrot7  = 0xFFCA6F1E,
            Carrot8  = 0xFFAF601A,
            Carrot9  = 0xFF935116,
            Carrot10 = 0xFF784212,

            // Pumpkin
            Pumpkin1  = 0xFFFBEEE6,
            Pumpkin2  = 0xFFF6DDCC,
            Pumpkin3  = 0xFFEDBB99,
            Pumpkin4  = 0xFFE59866,
            Pumpkin5  = 0xFFDC7633,
            Pumpkin6  = 0xFFD35400,
            Pumpkin7  = 0xFFBA4A00,
            Pumpkin8  = 0xFFA04000,
            Pumpkin9  = 0xFF873600,
            Pumpkin10 = 0xFF6E2C00,

            // Clouds
            Clouds1  = 0xFFFDFEFE,
            Clouds2  = 0xFFFBFCFC,
            Clouds3  = 0xFFF7F9F9,
            Clouds4  = 0xFFF4F6F7,
            Clouds5  = 0xFFF0F3F4,
            Clouds6  = 0xFFECF0F1,
            Clouds7  = 0xFFD0D3D4,
            Clouds8  = 0xFFB3B6B7,
            Clouds9  = 0xFF979A9A,
            Clouds10 = 0xFF7B7D7D,

            // Silver
            Silver1  = 0xFFF8F9F9,
            Silver2  = 0xFFF2F3F4,
            Silver3  = 0xFFE5E7E9,
            Silver4  = 0xFFD7DBDD,
            Silver5  = 0xFFCACFD2,
            Silver6  = 0xFFBDC3C7,
            Silver7  = 0xFFA6ACAF,
            Silver8  = 0xFF909497,
            Silver9  = 0xFF797D7F,
            Silver10 = 0xFF626567,

            // Concrete
            Concrete1  = 0xFFF4F6F6,
            Concrete2  = 0xFFEAEDED,
            Concrete3  = 0xFFD5DBDB,
            Concrete4  = 0xFFBFC9CA,
            Concrete5  = 0xFFAAB7B8,
            Concrete6  = 0xFF95A5A6,
            Concrete7  = 0xFF839192,
            Concrete8  = 0xFF717D7E,
            Concrete9  = 0xFF5F6A6A,
            Concrete10 = 0xFF4D5656,

            // Asbestos
            Asbestos1  = 0xFFF2F4F4,
            Asbestos2  = 0xFFE5E8E8,
            Asbestos3  = 0xFFCCD1D1,
            Asbestos4  = 0xFFB2BABB,
            Asbestos5  = 0xFF99A3A4,
            Asbestos6  = 0xFF7F8C8D,
            Asbestos7  = 0xFF707B7C,
            Asbestos8  = 0xFF616A6B,
            Asbestos9  = 0xFF515A5A,
            Asbestos10 = 0xFF424949,

            // Wet Asphalt
            WetAsphalt1  = 0xFFEBEDEF,
            WetAsphalt2  = 0xFFD6DBDF,
            WetAsphalt3  = 0xFFAEB6BF,
            WetAsphalt4  = 0xFF85929E,
            WetAsphalt5  = 0xFF5D6D7E,
            WetAsphalt6  = 0xFF34495E,
            WetAsphalt7  = 0xFF2E4053,
            WetAsphalt8  = 0xFF283747,
            WetAsphalt9  = 0xFF212F3C,
            WetAsphalt10 = 0xFF1B2631,

            // Midnight Blue
            MidnightBlue1  = 0xFFEAECEE,
            MidnightBlue2  = 0xFFD5D8DC,
            MidnightBlue3  = 0xFFABB2B9,
            MidnightBlue4  = 0xFF808B96,
            MidnightBlue5  = 0xFF566573,
            MidnightBlue6  = 0xFF2C3E50,
            MidnightBlue7  = 0xFF273746,
            MidnightBlue8  = 0xFF212F3D,
            MidnightBlue9  = 0xFF1C2833,
            MidnightBlue10 = 0xFF17202A,

            Pomegranate  = Pomegranate6,
            Alizarin     = Alizarin6,
            Amethyst     = Amethyst6,
            Wisteria     = Wisteria6,
            BelizeHole   = BelizeHole6,
            PeterRiver   = PeterRiver6,
            Turquoise    = Turquoise6,
            GreenSea     = GreenSea6,
            Nephritis    = Nephritis6,
            Emerald      = Emerald6,
            Sunflower    = Sunflower6,
            Orange       = Orange6,
            Carrot       = Carrot6,
            Pumpkin      = Pumpkin6,
            Clouds       = Clouds6,
            Silver       = Silver6,
            Concrete     = Concrete6,
            Asbestos     = Asbestos6,
            WetAsphalt   = WetAsphalt6,
            MidnightBlue = MidnightBlue6,
        };

        // See: https://htmlcolorcodes.com/assets/downloads/flat-design-colors/flat-design-color-chart.png
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
                        Color.FromUInt32((uint32)FlatColor.Pomegranate2),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate3),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate4),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate5),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate6),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate7),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate8),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate9),
                        Color.FromUInt32((uint32)FlatColor.Pomegranate10),
                    },

                    // Alizarin
                    {
                        Color.FromUInt32((uint32)FlatColor.Alizarin1),
                        Color.FromUInt32((uint32)FlatColor.Alizarin2),
                        Color.FromUInt32((uint32)FlatColor.Alizarin3),
                        Color.FromUInt32((uint32)FlatColor.Alizarin4),
                        Color.FromUInt32((uint32)FlatColor.Alizarin5),
                        Color.FromUInt32((uint32)FlatColor.Alizarin6),
                        Color.FromUInt32((uint32)FlatColor.Alizarin7),
                        Color.FromUInt32((uint32)FlatColor.Alizarin8),
                        Color.FromUInt32((uint32)FlatColor.Alizarin9),
                        Color.FromUInt32((uint32)FlatColor.Alizarin10),
                    },

                    // Amethyst
                    {
                        Color.FromUInt32((uint32)FlatColor.Amethyst1),
                        Color.FromUInt32((uint32)FlatColor.Amethyst2),
                        Color.FromUInt32((uint32)FlatColor.Amethyst3),
                        Color.FromUInt32((uint32)FlatColor.Amethyst4),
                        Color.FromUInt32((uint32)FlatColor.Amethyst5),
                        Color.FromUInt32((uint32)FlatColor.Amethyst6),
                        Color.FromUInt32((uint32)FlatColor.Amethyst7),
                        Color.FromUInt32((uint32)FlatColor.Amethyst8),
                        Color.FromUInt32((uint32)FlatColor.Amethyst9),
                        Color.FromUInt32((uint32)FlatColor.Amethyst10),
                    },

                    // Wisteria
                    {
                        Color.FromUInt32((uint32)FlatColor.Wisteria1),
                        Color.FromUInt32((uint32)FlatColor.Wisteria2),
                        Color.FromUInt32((uint32)FlatColor.Wisteria3),
                        Color.FromUInt32((uint32)FlatColor.Wisteria4),
                        Color.FromUInt32((uint32)FlatColor.Wisteria5),
                        Color.FromUInt32((uint32)FlatColor.Wisteria6),
                        Color.FromUInt32((uint32)FlatColor.Wisteria7),
                        Color.FromUInt32((uint32)FlatColor.Wisteria8),
                        Color.FromUInt32((uint32)FlatColor.Wisteria9),
                        Color.FromUInt32((uint32)FlatColor.Wisteria10),
                    },

                    // Belize Hole
                    {
                        Color.FromUInt32((uint32)FlatColor.BelizeHole1),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole2),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole3),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole4),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole5),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole6),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole7),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole8),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole9),
                        Color.FromUInt32((uint32)FlatColor.BelizeHole10),
                    },

                    // Peter River
                    {
                        Color.FromUInt32((uint32)FlatColor.PeterRiver1),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver2),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver3),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver4),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver5),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver6),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver7),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver8),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver9),
                        Color.FromUInt32((uint32)FlatColor.PeterRiver10),
                    },

                    // Turquoise
                    {
                        Color.FromUInt32((uint32)FlatColor.Turquoise1),
                        Color.FromUInt32((uint32)FlatColor.Turquoise2),
                        Color.FromUInt32((uint32)FlatColor.Turquoise3),
                        Color.FromUInt32((uint32)FlatColor.Turquoise4),
                        Color.FromUInt32((uint32)FlatColor.Turquoise5),
                        Color.FromUInt32((uint32)FlatColor.Turquoise6),
                        Color.FromUInt32((uint32)FlatColor.Turquoise7),
                        Color.FromUInt32((uint32)FlatColor.Turquoise8),
                        Color.FromUInt32((uint32)FlatColor.Turquoise9),
                        Color.FromUInt32((uint32)FlatColor.Turquoise10),
                    },

                    // Green Sea
                    {
                        Color.FromUInt32((uint32)FlatColor.GreenSea1),
                        Color.FromUInt32((uint32)FlatColor.GreenSea2),
                        Color.FromUInt32((uint32)FlatColor.GreenSea3),
                        Color.FromUInt32((uint32)FlatColor.GreenSea4),
                        Color.FromUInt32((uint32)FlatColor.GreenSea5),
                        Color.FromUInt32((uint32)FlatColor.GreenSea6),
                        Color.FromUInt32((uint32)FlatColor.GreenSea7),
                        Color.FromUInt32((uint32)FlatColor.GreenSea8),
                        Color.FromUInt32((uint32)FlatColor.GreenSea9),
                        Color.FromUInt32((uint32)FlatColor.GreenSea10),
                    },

                    // Nephritis
                    {
                        Color.FromUInt32((uint32)FlatColor.Nephritis1),
                        Color.FromUInt32((uint32)FlatColor.Nephritis2),
                        Color.FromUInt32((uint32)FlatColor.Nephritis3),
                        Color.FromUInt32((uint32)FlatColor.Nephritis4),
                        Color.FromUInt32((uint32)FlatColor.Nephritis5),
                        Color.FromUInt32((uint32)FlatColor.Nephritis6),
                        Color.FromUInt32((uint32)FlatColor.Nephritis7),
                        Color.FromUInt32((uint32)FlatColor.Nephritis8),
                        Color.FromUInt32((uint32)FlatColor.Nephritis9),
                        Color.FromUInt32((uint32)FlatColor.Nephritis10),
                    },

                    // Emerald
                    {
                        Color.FromUInt32((uint32)FlatColor.Emerald1),
                        Color.FromUInt32((uint32)FlatColor.Emerald2),
                        Color.FromUInt32((uint32)FlatColor.Emerald3),
                        Color.FromUInt32((uint32)FlatColor.Emerald4),
                        Color.FromUInt32((uint32)FlatColor.Emerald5),
                        Color.FromUInt32((uint32)FlatColor.Emerald6),
                        Color.FromUInt32((uint32)FlatColor.Emerald7),
                        Color.FromUInt32((uint32)FlatColor.Emerald8),
                        Color.FromUInt32((uint32)FlatColor.Emerald9),
                        Color.FromUInt32((uint32)FlatColor.Emerald10),
                    },

                    // Sunflower
                    {
                        Color.FromUInt32((uint32)FlatColor.Sunflower1),
                        Color.FromUInt32((uint32)FlatColor.Sunflower2),
                        Color.FromUInt32((uint32)FlatColor.Sunflower3),
                        Color.FromUInt32((uint32)FlatColor.Sunflower4),
                        Color.FromUInt32((uint32)FlatColor.Sunflower5),
                        Color.FromUInt32((uint32)FlatColor.Sunflower6),
                        Color.FromUInt32((uint32)FlatColor.Sunflower7),
                        Color.FromUInt32((uint32)FlatColor.Sunflower8),
                        Color.FromUInt32((uint32)FlatColor.Sunflower9),
                        Color.FromUInt32((uint32)FlatColor.Sunflower10),
                    },

                    // Orange
                    {
                        Color.FromUInt32((uint32)FlatColor.Orange1),
                        Color.FromUInt32((uint32)FlatColor.Orange2),
                        Color.FromUInt32((uint32)FlatColor.Orange3),
                        Color.FromUInt32((uint32)FlatColor.Orange4),
                        Color.FromUInt32((uint32)FlatColor.Orange5),
                        Color.FromUInt32((uint32)FlatColor.Orange6),
                        Color.FromUInt32((uint32)FlatColor.Orange7),
                        Color.FromUInt32((uint32)FlatColor.Orange8),
                        Color.FromUInt32((uint32)FlatColor.Orange9),
                        Color.FromUInt32((uint32)FlatColor.Orange10),
                    },

                    // Carrot
                    {
                        Color.FromUInt32((uint32)FlatColor.Carrot1),
                        Color.FromUInt32((uint32)FlatColor.Carrot2),
                        Color.FromUInt32((uint32)FlatColor.Carrot3),
                        Color.FromUInt32((uint32)FlatColor.Carrot4),
                        Color.FromUInt32((uint32)FlatColor.Carrot5),
                        Color.FromUInt32((uint32)FlatColor.Carrot6),
                        Color.FromUInt32((uint32)FlatColor.Carrot7),
                        Color.FromUInt32((uint32)FlatColor.Carrot8),
                        Color.FromUInt32((uint32)FlatColor.Carrot9),
                        Color.FromUInt32((uint32)FlatColor.Carrot10),
                    },

                    // Pumpkin
                    {
                        Color.FromUInt32((uint32)FlatColor.Pumpkin1),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin2),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin3),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin4),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin5),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin6),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin7),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin8),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin9),
                        Color.FromUInt32((uint32)FlatColor.Pumpkin10),
                    },

                    // Clouds
                    {
                        Color.FromUInt32((uint32)FlatColor.Clouds1),
                        Color.FromUInt32((uint32)FlatColor.Clouds2),
                        Color.FromUInt32((uint32)FlatColor.Clouds3),
                        Color.FromUInt32((uint32)FlatColor.Clouds4),
                        Color.FromUInt32((uint32)FlatColor.Clouds5),
                        Color.FromUInt32((uint32)FlatColor.Clouds6),
                        Color.FromUInt32((uint32)FlatColor.Clouds7),
                        Color.FromUInt32((uint32)FlatColor.Clouds8),
                        Color.FromUInt32((uint32)FlatColor.Clouds9),
                        Color.FromUInt32((uint32)FlatColor.Clouds10),
                    },

                    // Silver
                    {
                        Color.FromUInt32((uint32)FlatColor.Silver1),
                        Color.FromUInt32((uint32)FlatColor.Silver2),
                        Color.FromUInt32((uint32)FlatColor.Silver3),
                        Color.FromUInt32((uint32)FlatColor.Silver4),
                        Color.FromUInt32((uint32)FlatColor.Silver5),
                        Color.FromUInt32((uint32)FlatColor.Silver6),
                        Color.FromUInt32((uint32)FlatColor.Silver7),
                        Color.FromUInt32((uint32)FlatColor.Silver8),
                        Color.FromUInt32((uint32)FlatColor.Silver9),
                        Color.FromUInt32((uint32)FlatColor.Silver10),
                    },

                    // Concrete
                    {
                        Color.FromUInt32((uint32)FlatColor.Concrete1),
                        Color.FromUInt32((uint32)FlatColor.Concrete2),
                        Color.FromUInt32((uint32)FlatColor.Concrete3),
                        Color.FromUInt32((uint32)FlatColor.Concrete4),
                        Color.FromUInt32((uint32)FlatColor.Concrete5),
                        Color.FromUInt32((uint32)FlatColor.Concrete6),
                        Color.FromUInt32((uint32)FlatColor.Concrete7),
                        Color.FromUInt32((uint32)FlatColor.Concrete8),
                        Color.FromUInt32((uint32)FlatColor.Concrete9),
                        Color.FromUInt32((uint32)FlatColor.Concrete10),
                    },

                    // Asbestos
                    {
                        Color.FromUInt32((uint32)FlatColor.Asbestos1),
                        Color.FromUInt32((uint32)FlatColor.Asbestos2),
                        Color.FromUInt32((uint32)FlatColor.Asbestos3),
                        Color.FromUInt32((uint32)FlatColor.Asbestos4),
                        Color.FromUInt32((uint32)FlatColor.Asbestos5),
                        Color.FromUInt32((uint32)FlatColor.Asbestos6),
                        Color.FromUInt32((uint32)FlatColor.Asbestos7),
                        Color.FromUInt32((uint32)FlatColor.Asbestos8),
                        Color.FromUInt32((uint32)FlatColor.Asbestos9),
                        Color.FromUInt32((uint32)FlatColor.Asbestos10),
                    },

                    // Wet Asphalt
                    {
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt1),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt2),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt3),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt4),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt5),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt6),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt7),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt8),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt9),
                        Color.FromUInt32((uint32)FlatColor.WetAsphalt10),
                    },

                    // Midnight Blue
                    {
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue1),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue2),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue3),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue4),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue5),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue6),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue7),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue8),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue9),
                        Color.FromUInt32((uint32)FlatColor.MidnightBlue10),
                    },
                };
            }

            return;
        }

        /// <inheritdoc/>
        public int32 ColorCount
        {
            get => 20;
        }

        /// <inheritdoc/>
        public int32 ShadeCount
        {
            get => 10;
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
