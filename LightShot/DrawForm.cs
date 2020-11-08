using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LightShot
{
    public partial class DrawForm : Form
    {
        Graphics graphics;


        Pen pen;
        int x;
        int y;
        PictureBox PictureBox;
        public DrawForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

        }

        public DrawForm(Pen pen, Image image, PictureBox pictureBox, Rectangle rectangle)
        {

            PictureBox = pictureBox;
            PictureBox.Click += PictureBox_Click;
            InitializeComponent();


            this.pen = pen;

            BackgroundImage = image;

            pictureBox.BackgroundImage = Crop(image, new Rectangle(pictureBox.Location, pictureBox.Size));

            Controls.Add(pictureBox);




            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();

            graphics = CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private Image Crop(Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;

            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            return cropBmp;
        }

        private void DrawForm_Load(object sender, EventArgs e)
        {

        }

        private void DrawForm_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox.Visible = false;

        }
        private int func(int x, Point a, Point b)
        {
            if (b.X - a.X == 0) return 0;
            else
            {
                int y = ((b.X * a.Y - a.X * b.Y) + (b.Y - a.Y) * x) / (b.X - a.X);
                return y;
            }
        }
        private void DrawForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //for (int i = Math.Min(e.X, x); i <= Math.Max(e.X, x); i++)



                //graphics.FillEllipse(new SolidBrush(Color.Red), new Rectangle(i, func(i, new Point(x, y), new Point(e.X, e.Y)), 10, 10));


                //if (x == 0 && y == 0)
                //    graphics.DrawLine(pen, e.X, e.Y, e.X, e.Y);
                //else
                graphics.DrawLine(pen, x, y, e.X, e.Y);

            }
            x = e.X;
            y = e.Y;
        }

        private void DrawForm_MouseUp(object sender, MouseEventArgs e)
        {
            var BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(BM as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, BM.Size);
            BackgroundImage = BM;
            PictureBox.BackgroundImage = Crop(BM, new Rectangle(PictureBox.Location, PictureBox.Size));
            PictureBox.Visible = true;

        }
    }
}
