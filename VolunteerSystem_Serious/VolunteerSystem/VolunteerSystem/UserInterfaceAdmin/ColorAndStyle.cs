using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin
{
    public static class ColorAndStyle
    {
        public static Color AlternatingColorsONE()
        {
            return Color.LightGray;
        }
        public static Color AlternatingColorsTWO()
        {
            return Color.Gray;
        }

        public static Color SmallAlternatingColorsONE()
        {
            return Color.LightGray;
        }
        public static Color SmallAlternatingColorsTWO()
        {
            return Color.White;
        }

        public static Color PrimaryColor()
        {
            return Color.MediumAquamarine;
        }

        public static Color SecondaryColor()
        {
            return Color.Teal;
        }

        public static Color ShiftColorCompletelyFree()
        {
            return Color.Red;
        }

        public static Color ShiftColorEnoughRequests()
        {
            return Color.Yellow;
        }

        public static Color ShiftColorWhenFull()
        {
            return Color.LawnGreen;
        }

        public static Color SmallButtonColor()
        {
            return Color.White;
        }

        public static Color PrimaryColor(bool transparrent)
        {
            if (transparrent)
                return Color.FromArgb(100, PrimaryColor());
            else
                return PrimaryColor();
        }

        public static Color SecondaryColor(bool transparrent)
        {
            if (transparrent)
                return Color.FromArgb(100, SecondaryColor());
            else
                return SecondaryColor();
        }
    }
}
