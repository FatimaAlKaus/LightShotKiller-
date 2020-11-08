using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightShot
{
    public partial class Form1 : Form
    {
        Color standartColor = Color.FromArgb(60, 60, 60);
        Color enterColor = Color.FromArgb(80, 80, 80);
        Bitmap BM;
        public Form1()
        {
            InitializeComponent();
            

            this.BackColor = standartColor;
            this.Text = "Porn";


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = standartColor;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.BackColor = enterColor;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.Visible = false;
            Graphics graphics = Graphics.FromImage(BM as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, BM.Size);

            DialogScreen screen = new DialogScreen();
            screen.BackgroundImage = BM;

            screen.ShowDialog();
            this.Visible = true;
        }
    }
}
