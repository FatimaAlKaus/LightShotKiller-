using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightShot
{
    public partial class ToolsDialog : Form
    {
        Rectangle selectedRegion;
        Point startPoint;
        Image Image;
        Pen pen;
        Pen Rectanglepen = new Pen(Color.Aqua, 1);
        SolidBrush SolidBrush = new SolidBrush(Color.AliceBlue);
        public ToolsDialog()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();


        }
        public ToolsDialog(Image image)
        {
            TransparencyKey = Color.AliceBlue;
            Image = image;
            InitializeComponent();
            Opacity = 0.5;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            panel1.Visible = false;
            panel2.Visible = false;
        }

        private void ToolsDialog_Load(object sender, EventArgs e)
        {

        }

        private void ToolsDialog_MouseDown(object sender, MouseEventArgs e)
        {
            //TransparencyKey = Color.White;
            panel1.Location = new Point(-100, -100);
            panel2.Location = new Point(-100, -100);

            panel1.Visible = false;
            panel2.Visible = false;
            pictureBox1.Image = null;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(0, 0);
            selectedRegion = new Rectangle(e.X, e.Y, 0, 0);
            startPoint = new Point(e.X, e.Y);
        }

        private void ToolsDialog_MouseUp(object sender, MouseEventArgs e)
        {
            // TransparencyKey = Color.Empty;
            if (selectedRegion.Width < 3 || selectedRegion.Height < 3)
            {
                selectedRegion = new Rectangle();
                pictureBox1.Image = null;
                pictureBox1.Location = new Point(0, 0);
                pictureBox1.Size = new Size(0, 0);

            }
            else
            {
                if (selectedRegion.Size.Width != 0 && selectedRegion.Size.Height != 0)
                    pictureBox1.Size = selectedRegion.Size;
                pictureBox1.Location = selectedRegion.Location;
                if (selectedRegion.Size.Width != 0 && selectedRegion.Size.Height != 0)

                    pictureBox1.Image = Crop(Image, selectedRegion);
                panel1.Visible = true;
                panel2.Visible = true;
                panel1.Location = new Point(pictureBox1.Location.X - panel1.Width - 1, pictureBox1.Location.Y);
                panel2.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - panel2.Height - 1);
            }

            Refresh();
        }

        private void ToolsDialog_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {


                var size1 = e.X - startPoint.X > 0 ? e.X - startPoint.X : startPoint.X - e.X;
                var size2 = e.Y - startPoint.Y > 0 ? e.Y - startPoint.Y : startPoint.Y - e.Y;
                var x = e.X - startPoint.X > 0 ? startPoint.X : e.X;
                var y = e.Y - startPoint.Y > 0 ? startPoint.Y : e.Y;

                selectedRegion = new Rectangle(x, y, size1, size2);

                Refresh();




            }

        }
        private Image Crop(Image image, Rectangle selection)
        {
            Bitmap bmp = image as Bitmap;

            Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

            return cropBmp;
        }
        private void ToolsDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.C)
                if (pictureBox1.Image != null)
                    Clipboard.SetImage(pictureBox1.Image);
        }

        private void ToolsDialog_Paint(object sender, PaintEventArgs e)
        {

            Rectangle rectangle = new Rectangle(new Point(selectedRegion.X - 1, selectedRegion.Y - 1), new Size(selectedRegion.Width + 1, selectedRegion.Height + 1));
            //e.Graphics.DrawRectangle(pen, rectangle);
            e.Graphics.FillRectangle(SolidBrush, rectangle);
            e.Graphics.DrawRectangle(Rectanglepen, rectangle);
        }


        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Resource1.CopyIconActive;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Resource1.CopyIcon;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)

                Clipboard.SetImage(pictureBox1.Image);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Resource1.CloseActive;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Resource1.Close;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Resource1.SaveAS1;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Resource1.SaveAS;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ScreenShot";
            ;

            saveFileDialog.Filter = "PNG|*.PNG|JPG|*.JPG|BMP|*.BMP";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {


                string filename = saveFileDialog.FileName;
                pictureBox1.Image.Save(filename);

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            panel1.Visible = false;
            panel2.Visible = false;
            Refresh();

            Graphics graphics = Graphics.FromImage(BM as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, BM.Size);
            pen = new Pen(Color.Red, 15);
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = pictureBox5.Image;
            pictureBox.Location = new Point(panel1.Location.X + pictureBox5.Location.X, panel1.Location.Y + pictureBox5.Location.Y);
            pictureBox.Size = pictureBox5.Size;

            DrawForm drawForm = new DrawForm(pen, BM, pictureBox, selectedRegion);
            drawForm.ShowDialog();
            panel1.Visible = true;
            panel2.Visible = true;
        }



        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Image = Resource1.Pen1;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Resource1.Pen;
        }
    }
}
