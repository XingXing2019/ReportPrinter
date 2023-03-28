using System;
using System.Reflection;
using NUnit.Framework;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.PDF.Helper
{
    public class ColorHelperTest : TestBase
    {
        [Test]
        [TestCase("AliceBlue", true, XKnownColor.AliceBlue)]
        [TestCase("AntiqueWhite", true, XKnownColor.AntiqueWhite)]
        [TestCase("Aqua", true, XKnownColor.Aqua)]
        [TestCase("Aquamarine", true, XKnownColor.Aquamarine)]
        [TestCase("Azure", true, XKnownColor.Azure)]
        [TestCase("Beige", true, XKnownColor.Beige)]
        [TestCase("Bisque", true, XKnownColor.Bisque)]
        [TestCase("Black", true, XKnownColor.Black)]
        [TestCase("BlanchedAlmond", true, XKnownColor.BlanchedAlmond)]
        [TestCase("Blue", true, XKnownColor.Blue)]
        [TestCase("BlueViolet", true, XKnownColor.BlueViolet)]
        [TestCase("Brown", true, XKnownColor.Brown)]
        [TestCase("BurlyWood", true, XKnownColor.BurlyWood )]
        [TestCase("CadetBlue", true, XKnownColor.CadetBlue)]
        [TestCase("Chartreuse", true, XKnownColor.Chartreuse)]
        [TestCase("Chocolate", true, XKnownColor.Chocolate)]
        [TestCase("Coral", true, XKnownColor.Coral)]
        [TestCase("CornflowerBlue", true, XKnownColor.CornflowerBlue)]
        [TestCase("Cornsilk", true, XKnownColor.Cornsilk)]
        [TestCase("Crimson", true, XKnownColor.Crimson)]
        [TestCase("Cyan", true, XKnownColor.Cyan)]
        [TestCase("DarkBlue", true, XKnownColor.DarkBlue)]
        [TestCase("DarkCyan", true, XKnownColor.DarkCyan)]
        [TestCase("DarkGoldenrod", true, XKnownColor.DarkGoldenrod)]
        [TestCase("DarkGray", true, XKnownColor.DarkGray)]
        [TestCase("DarkGreen", true, XKnownColor.DarkGreen)]
        [TestCase("DarkKhaki", true, XKnownColor.DarkKhaki)]
        [TestCase("DarkMagenta", true, XKnownColor.DarkMagenta)]
        [TestCase("DarkOliveGreen", true, XKnownColor.DarkOliveGreen)]
        [TestCase("DarkOrange", true, XKnownColor.DarkOrange)]
        [TestCase("DarkOrchid", true, XKnownColor.DarkOrchid)]
        [TestCase("DarkRed", true, XKnownColor.DarkRed)]
        [TestCase("DarkSalmon", true, XKnownColor.DarkSalmon)]
        [TestCase("DarkSeaGreen", true, XKnownColor.DarkSeaGreen)]
        [TestCase("DarkSlateBlue", true, XKnownColor.DarkSlateBlue)]
        [TestCase("DarkSlateGray", true, XKnownColor.DarkSlateGray)]
        [TestCase("DarkTurquoise", true, XKnownColor.DarkTurquoise)]
        [TestCase("DarkViolet", true, XKnownColor.DarkViolet)]
        [TestCase("DeepPink", true, XKnownColor.DeepPink)]
        [TestCase("DeepSkyBlue", true, XKnownColor.DeepSkyBlue)]
        [TestCase("DimGray", true, XKnownColor.DimGray)]
        [TestCase("DodgerBlue", true, XKnownColor.DodgerBlue)]
        [TestCase("Firebrick", true, XKnownColor.Firebrick)]
        [TestCase("FloralWhite", true, XKnownColor.FloralWhite)]
        [TestCase("ForestGreen", true, XKnownColor.ForestGreen)]
        [TestCase("Fuchsia", true, XKnownColor.Fuchsia)]
        [TestCase("Gainsboro", true, XKnownColor.Gainsboro)]
        [TestCase("GhostWhite", true, XKnownColor.GhostWhite)]
        [TestCase("Gold", true, XKnownColor.Gold)]
        [TestCase("Goldenrod", true, XKnownColor.Goldenrod)]
        [TestCase("Gray", true, XKnownColor.Gray)]
        [TestCase("Green", true, XKnownColor.Green)]
        [TestCase("GreenYellow", true, XKnownColor.GreenYellow)]
        [TestCase("Honeydew", true, XKnownColor.Honeydew)]
        [TestCase("HotPink", true, XKnownColor.HotPink)]
        [TestCase("IndianRed", true, XKnownColor.IndianRed)]
        [TestCase("Indigo", true, XKnownColor.Indigo)]
        [TestCase("Ivory", true, XKnownColor.Ivory)]
        [TestCase("Khaki", true, XKnownColor.Khaki)]
        [TestCase("Lavender", true, XKnownColor.Lavender)]
        [TestCase("LavenderBlush", true, XKnownColor.LavenderBlush)]
        [TestCase("LawnGreen", true, XKnownColor.LawnGreen)]
        [TestCase("LemonChiffon", true, XKnownColor.LemonChiffon)]
        [TestCase("LightBlue", true, XKnownColor.LightBlue)]
        [TestCase("LightCoral", true, XKnownColor.LightCoral)]
        [TestCase("LightCyan", true, XKnownColor.LightCyan)]
        [TestCase("LightGoldenrodYellow", true, XKnownColor.LightGoldenrodYellow)]
        [TestCase("LightGray", true, XKnownColor.LightGray)]
        [TestCase("LightGreen", true, XKnownColor.LightGreen)]
        [TestCase("LightPink", true, XKnownColor.LightPink)]
        [TestCase("LightSalmon", true, XKnownColor.LightSalmon)]
        [TestCase("LightSeaGreen", true, XKnownColor.LightSeaGreen)]
        [TestCase("LightSkyBlue", true, XKnownColor.LightSkyBlue)]
        [TestCase("LightSlateGray", true, XKnownColor.LightSlateGray)]
        [TestCase("LightSteelBlue", true, XKnownColor.LightSteelBlue)]
        [TestCase("LightYellow", true, XKnownColor.LightYellow)]
        [TestCase("Lime", true, XKnownColor.Lime)]
        [TestCase("LimeGreen", true, XKnownColor.LimeGreen)]
        [TestCase("Linen", true, XKnownColor.Linen)]
        [TestCase("Magenta", true, XKnownColor.Magenta)]
        [TestCase("Maroon", true, XKnownColor.Maroon)]
        [TestCase("MediumAquamarine", true, XKnownColor.MediumAquamarine)]
        [TestCase("MediumBlue", true, XKnownColor.MediumBlue)]
        [TestCase("MediumOrchid", true, XKnownColor.MediumOrchid)]
        [TestCase("MediumPurple", true, XKnownColor.MediumPurple)]
        [TestCase("MediumSeaGreen", true, XKnownColor.MediumSeaGreen)]
        [TestCase("MediumSlateBlue", true, XKnownColor.MediumSlateBlue)]
        [TestCase("MediumSpringGreen", true, XKnownColor.MediumSpringGreen)]
        [TestCase("MediumTurquoise", true, XKnownColor.MediumTurquoise)]
        [TestCase("MediumVioletRed", true, XKnownColor.MediumVioletRed)]
        [TestCase("MidnightBlue", true, XKnownColor.MidnightBlue)]
        [TestCase("MintCream", true, XKnownColor.MintCream)]
        [TestCase("MistyRose", true, XKnownColor.MistyRose)]
        [TestCase("Moccasin", true, XKnownColor.Moccasin)]
        [TestCase("NavajoWhite", true, XKnownColor.NavajoWhite)]
        [TestCase("Navy", true, XKnownColor.Navy)]
        [TestCase("OldLace", true, XKnownColor.OldLace)]
        [TestCase("Olive", true, XKnownColor.Olive)]
        [TestCase("OliveDrab", true, XKnownColor.OliveDrab)]
        [TestCase("Orange", true, XKnownColor.Orange)]
        [TestCase("OrangeRed", true, XKnownColor.OrangeRed)]
        [TestCase("Orchid", true, XKnownColor.Orchid)]
        [TestCase("PaleGoldenrod", true, XKnownColor.PaleGoldenrod)]
        [TestCase("PaleGreen", true, XKnownColor.PaleGreen)]
        [TestCase("PaleTurquoise", true, XKnownColor.PaleTurquoise)]
        [TestCase("PaleVioletRed", true, XKnownColor.PaleVioletRed)]
        [TestCase("PapayaWhip", true, XKnownColor.PapayaWhip)]
        [TestCase("PeachPuff", true, XKnownColor.PeachPuff)]
        [TestCase("Peru", true, XKnownColor.Peru)]
        [TestCase("Pink", true, XKnownColor.Pink)]
        [TestCase("Plum", true, XKnownColor.Plum)]
        [TestCase("PowderBlue", true, XKnownColor.PowderBlue)]
        [TestCase("Purple", true, XKnownColor.Purple)]
        [TestCase("Red", true, XKnownColor.Red)]
        [TestCase("RosyBrown", true, XKnownColor.RosyBrown)]
        [TestCase("RoyalBlue", true, XKnownColor.RoyalBlue)]
        [TestCase("SaddleBrown", true, XKnownColor.SaddleBrown)]
        [TestCase("Salmon", true, XKnownColor.Salmon)]
        [TestCase("SandyBrown", true, XKnownColor.SandyBrown)]
        [TestCase("SeaGreen", true, XKnownColor.SeaGreen)]
        [TestCase("SeaShell", true, XKnownColor.SeaShell)]
        [TestCase("Sienna", true, XKnownColor.Sienna)]
        [TestCase("Silver", true, XKnownColor.Silver)]
        [TestCase("SkyBlue", true, XKnownColor.SkyBlue)]
        [TestCase("SlateBlue", true, XKnownColor.SlateBlue)]
        [TestCase("SlateGray", true, XKnownColor.SlateGray)]
        [TestCase("Snow", true, XKnownColor.Snow)]
        [TestCase("SpringGreen", true, XKnownColor.SpringGreen)]
        [TestCase("SteelBlue", true, XKnownColor.SteelBlue)]
        [TestCase("Tan", true, XKnownColor.Tan)]
        [TestCase("Teal", true, XKnownColor.Teal)]
        [TestCase("Thistle", true, XKnownColor.Thistle)]
        [TestCase("Tomato", true, XKnownColor.Tomato)]
        [TestCase("Transparent", true, XKnownColor.Transparent)]
        [TestCase("Turquoise", true, XKnownColor.Turquoise)]
        [TestCase("Violet", true, XKnownColor.Violet)]
        [TestCase("Wheat", true, XKnownColor.Wheat)]
        [TestCase("White", true, XKnownColor.White)]
        [TestCase("WhiteSmoke", true, XKnownColor.WhiteSmoke)]
        [TestCase("Yellow", true, XKnownColor.Yellow)]
        [TestCase("YellowGreen", true, XKnownColor.YellowGreen)]
        [TestCase("InvalidColor", false, -1)]
        public void TestTryGenerateColor(string color, bool expectedRes, XKnownColor expectedColorEnum)
        {
            try
            {
                var actualRes = ColorHelper.TryGenerateColor(color, out var actualColor);
                Assert.AreEqual(expectedRes, actualRes);

                object expectedColor;

                if (expectedRes)
                {
                    var type = typeof(XColor);
                    var constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(XKnownColor) }, null);
                    expectedColor = constructor?.Invoke(new object?[] { expectedColorEnum });
                }
                else
                {
                    expectedColor = XColor.Empty;
                }
                
                Assert.AreEqual(actualColor, expectedColor);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}