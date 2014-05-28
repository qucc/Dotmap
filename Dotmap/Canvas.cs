using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dotmap
{
    public class Canvas : Control
    {
        GraphicsPath p;
        string s;

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (s != null)
                {
                    SetString(s);
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = Properties.Settings.Default.Font;
           
        }

        public void SetString(string s)
        {
            this.s = s;
            p = new GraphicsPath();
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            p.AddString(s, Font.FontFamily, (int)Font.Style, Font.Size, ClientRectangle, sf);
            Matrix matrix = new Matrix();
            matrix.Scale(1f, 0.65f);
            p.Transform(matrix);
            Invalidate();
        }

        public Bitmap GetBitmap()
        {
            
            Rectangle targetR = ClientRectangle;
            Bitmap bitmap = new Bitmap(targetR.Width, targetR.Height);

            DrawToBitmap(bitmap, targetR);
            return bitmap;
 
        }

        public Bitmap GetBitmap2()
        {
            Rectangle bounds = Rectangle.Round(p.GetBounds());
            bounds.Height += 10;
            bounds.Width += 10;

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            Graphics g = Graphics.FromImage(bitmap);
            Size size = bounds.Size;
            Console.Write(size);
            g.CopyFromScreen(PointToScreen(bounds.Location), new Point(1, 1), size);
            g.Dispose();
            Console.WriteLine("bitmap size " + bitmap.Size);
            return bitmap;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (p != null)
            {
                e.Graphics.FillPath(Brushes.Red, p);
                //e.Graphics.FillRectangle(Brushes.Blue, GetBounds());
            }
        }
    }
}
