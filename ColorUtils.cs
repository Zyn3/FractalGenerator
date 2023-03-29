using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalGenerator
{
    public static class ColorUtils
    {
        public static Color ColorFromHue(double hue)
        {
            int r, g, b;
            if (hue < 1.0 / 6)
            {
                r = (int)(255 * (1 - 6 * hue));
                g = 255;
                b = 0;
            }
            else if (hue < 1.0 / 3)
            {
                r = 0;
                g = 255;
                b = (int)(255 * (6 * hue - 1));
            }
            else if (hue < 1.0 / 2)
            {
                r = 0;
                g = (int)(255 * (2 - 6 * hue));
                b = 255;
            }
            else if (hue < 2.0 / 3)
            {
                r = (int)(255 * (6 * hue - 3));
                g = 0;
                b = 255;
            }
            else if (hue < 5.0 / 6)
            {
                r = 255;
                g = 0;
                b = (int)(255 * (4 - 6 * hue));
            }
            else
            {
                r = 255;
                g = (int)(255 * (6 * hue - 5));
                b = 0;
            }

            return Color.FromArgb(r, g, b);
        }

    }
}
