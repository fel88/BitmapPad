using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapPad
{
    public partial class mdi : Form
    {
        public mdi()
        {
            InitializeComponent();
            MainForm = this;
        }

        public static mdi MainForm;

        public void OpenChild(Mat mat, string text = null)
        {
            Form1 f = new Form1();
            if (text != null)
            {
                f.Text = text;
            }
            f.MdiParent = this;
            f.Init(mat);
            f.Show();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);

        }

        private void arrangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Form1 f = new Form1();
            f.MdiParent = this;
            f.Init(ofd.FileName);
            f.Show();
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Form1 f = new Form1();
            f.MdiParent = this;
            var bmp = ((Bitmap)Clipboard.GetImage()).ToMat();
            f.Init(bmp);
            f.Show();
        }
    }
}
