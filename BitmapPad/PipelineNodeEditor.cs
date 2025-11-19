using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void button8_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            var r = TemplateMatch(mat1, mat2);
            var ms = sw.ElapsedMilliseconds;
            toolStripStatusLabel1.Text = $"Elapsed: {ms} ms";
            mdi.MainForm.Open(r);
        }

        static Mat TemplateMatch(Mat reference, Mat tplMat)
        {
            Mat refMat = reference.Clone();
            using Mat res = new Mat(refMat.Rows - tplMat.Rows + 1, refMat.Cols - tplMat.Cols + 1, MatType.CV_32FC1);

            //Convert input images to gray
            Mat gref = refMat.CvtColor(ColorConversionCodes.BGR2GRAY);
            Mat gtpl = tplMat.CvtColor(ColorConversionCodes.BGR2GRAY);

            Cv2.MatchTemplate(gref, gtpl, res, TemplateMatchModes.CCoeffNormed);
            Cv2.Threshold(res, res, 0.8, 1.0, ThresholdTypes.Tozero);

            while (true)
            {
                double minval, maxval, threshold = 0.8;
                OpenCvSharp.Point minloc, maxloc;
                Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

                if (maxval >= threshold)
                {
                    //Setup the rectangle to draw
                    Rect r = new Rect(new OpenCvSharp.Point(maxloc.X, maxloc.Y), new OpenCvSharp.Size(tplMat.Width, tplMat.Height));
                    //WriteLine($"MinVal={minval.ToString()} MaxVal={maxval.ToString()} MinLoc={minloc.ToString()} MaxLoc={maxloc.ToString()} Rect={r.ToString()}");

                    //Draw a rectangle of the matching area
                    Cv2.Rectangle(refMat, r, Scalar.LimeGreen, 2);

                    //Fill in the res Mat so you don't find the same area again in the MinMaxLoc
                    Rect outRect;
                    Cv2.FloodFill(res, maxloc, new Scalar(0), out outRect, new Scalar(0.1), new Scalar(1.0));
                }
                else
                    break;
            }

            return refMat;
        }
    }
}
