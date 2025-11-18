using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapPad
{
    public partial class PipelineNodeEditor : Form
    {
        public PipelineNodeEditor()
        {
            InitializeComponent();
        }
        Mat mat1;
        Mat mat2;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            if (ofd1.ShowDialog() != DialogResult.OK)
                return;

            mat1 = new Mat(ofd1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            if (ofd1.ShowDialog() != DialogResult.OK)
                return;

            mat2 = new Mat(ofd1.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mat dest = new Mat();
            OpenCvSharp.Cv2.BitwiseXor(mat1, mat2, dest);
            mdi.MainForm.Open(dest);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var d = AutoDialog.DialogHelpers.StartDialog();
            var forms = mdi.MainForm.MdiChildren.OfType<Form1>().ToArray();
            var array = forms.Select(z => z.Text).ToArray();
            d.AddOptionsField("image", "Source", array, 0);
            if (!d.ShowDialog())
                return;

            mat1 = forms[d.GetOptionsFieldIdx("image")].Image;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var d = AutoDialog.DialogHelpers.StartDialog();
            var forms = mdi.MainForm.MdiChildren.OfType<Form1>().ToArray();
            var array = forms.Select(z => z.Text).ToArray();
            d.AddOptionsField("image", "Source", array, 0);
            if (!d.ShowDialog())
                return;

            mat2 = forms[d.GetOptionsFieldIdx("image")].Image;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Mat dest = new Mat();
            OpenCvSharp.Cv2.Absdiff(mat1, mat2, dest);
            mdi.MainForm.Open(dest);
        }
    }
}
