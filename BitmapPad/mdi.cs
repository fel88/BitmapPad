using OpenCvSharp;
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

        public void OpenChild(Mat mat)
        {
            Form1 f = new Form1();
            f.MdiParent = this;
            f.Init(mat);
            f.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Form1 f = new Form1();
            f.MdiParent = this;
            f.Init(ofd.FileName);
            f.Show();
            
        }
    }
}
