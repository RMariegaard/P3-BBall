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
            return Color.Green;
        }

        public static Color ShiftColorEnoughRequests()
        {
            return Color.Yellow;
        }

        public static Color ShiftColorWhenFull()
        {
            return Color.Red;
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

        public static void OnPaintDrawRect(object sender, PaintEventArgs e)
        {
            Graphics v = e.Graphics;
            _drawRoundRect(v, new Pen(Color.Black, 5), e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 10);
            
        }
        
        public static GraphicsPath GetRoundRectGP(float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath GetRoundRectGP(Point point, Size size, float radius)
        {
            return GetRoundRectGP(point.X, point.Y, size.Width, size.Height, radius);
        }

        private static void _drawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            g.DrawPath(p, GetRoundRectGP(X, Y, width, height, radius));
        }
    }
}
