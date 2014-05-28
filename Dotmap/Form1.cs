using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dotmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Graphics g = Graphics.FromImage(bitmap);
            ////缩写图片
            //Bitmap smallbitmap = new Bitmap((int)(bitmap.Width)  ,(int)(bitmap.Height));
            //Graphics g = Graphics.FromImage(smallbitmap);
            //g.DrawImage(bitmap,
            //new Rectangle(0, 0, smallbitmap.Width, smallbitmap.Height),
            //new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            //GraphicsUnit.Pixel);
            //    g.Dispose();
            //    bitmap = smallbitmap;
            string printChar = "#";
            if (textBox3.Text.Length > 0)
            {
                printChar = textBox3.Text;
            }

            int num = 1;
            try
            {
                num = Int16.Parse(textBox4.Text);
            }
            catch { }
            string space = "";
            for (int i = 0; i < num; i++)
                space += " ";
            if (space.Length == 0) space = " ";

            Bitmap bitmap = canvas1.GetBitmap2();
            //x scale 6.25, y scale 
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {   
                    Color c = bitmap.GetPixel(i, j);
                    if (c.R == 255 && c.G == 0 && c.B == 0)
                    {
                        sb.Append(printChar);
                    }
                    else
                    {

                        sb.Append(space);
                    }
                }
                sb.AppendLine();
            }
            textBox1.Text = sb.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string s = textBox2.Text;
            if (!string.IsNullOrEmpty(s))
            {
                canvas1.SetString(s);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
                Clipboard.SetText(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK )
            {
                canvas1.Font = fontDialog1.Font;
                canvas1.Invalidate();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Properties.Settings.Default.Save();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
