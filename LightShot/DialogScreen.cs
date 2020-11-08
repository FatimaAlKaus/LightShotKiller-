using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightShot
{
    public partial class DialogScreen : Form
    {
        
        public DialogScreen()
        {
            InitializeComponent();
            TransparencyKey = Color.Black;
           
        }
        private void DialogScreen_Load(object sender, EventArgs e)
        {
            ToolsDialog toolsDialog = new ToolsDialog(this.BackgroundImage); 
            //toolsDialog.FormClosed += ToolsDialog_FormClosed;
            toolsDialog.ShowDialog();
            Close();
           
        }

        private void ToolsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}
