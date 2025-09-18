using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace WSCADViewer.Domain.Util
{
    public static class Parsers
    {
        public static Point ParsePoint(string coordString)
        {
            var parts = coordString.Split(';', StringSplitOptions.TrimEntries);
            return new Point
            {
                X = double.Parse(parts[0].Replace(',', '.'), CultureInfo.InvariantCulture),
                Y = double.Parse(parts[1].Replace(',', '.'), CultureInfo.InvariantCulture),
            };
        }

        public static Color ParseColor(string colorString)
        {
            var parts = colorString.Split(';', StringSplitOptions.TrimEntries);
            return Color.FromArgb(
                byte.Parse(parts[0]),
                byte.Parse(parts[1]),
                byte.Parse(parts[2]),
                byte.Parse(parts[3])
            );
        }
    }
}
