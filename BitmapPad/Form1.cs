using AutoDialog;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;

namespace BitmapPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            image.Dispose();
        }

        private void PictureBox1_MouseUp(object? sender, MouseEventArgs e)
        {
            drag = false;
        }

        bool drag = false;
        private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
        {
            var pos = pictureBox1.PointToClient(Cursor.Position);
            startDragPos = pos;
            startDragShift = new PointF(shiftX, shiftY);
            drag = true;
        }

        private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                scale *= 1.2f;
            else
                scale /= 1.2f;
        }

        float shiftX;
        float shiftY;
        PointF startDragPos;
        PointF startDragShift;

        float scale = 1.0f;
        private void PictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            var pos = pictureBox1.PointToClient(Cursor.Position);

            if (drag)
            {
                shiftX = startDragShift.X + pos.X - startDragPos.X;
                shiftY = startDragShift.Y + pos.Y - startDragPos.Y;
            }
            var bmp = image.ToBitmap();
            var gr = e.Graphics;
            gr.Clear(Color.White);
            gr.ResetTransform();
            gr.TranslateTransform(shiftX, shiftY);
            gr.ScaleTransform(scale, scale);

            gr.DrawImage(bmp, 0, 0);

            bmp.Dispose();
        }

        Mat image;
        public void Init(string path)
        {
            //image = OpenCvSharp.Cv2.ImRead(path);
            using (var stream = new FileStream(path, FileMode.Open)) {
                image = Mat.FromStream(stream, ImreadModes.Unchanged);                
            }
            var bmp = image.ToBitmap();
            pictureBox1.Image = bmp;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.SizeMode == PictureBoxSizeMode.Zoom)
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Mat dest = new Mat();
            Cv2.CvtColor(image, dest, ColorConversionCodes.RGB2GRAY);
            mdi.MainForm.OpenChild(dest);

        }

        internal void Init(Mat mat)
        {
            image = mat;
            var bmp = image.ToBitmap();
            pictureBox1.Image = bmp;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            image.SaveImage(sfd.FileName);
            toolStripStatusLabel1.Text = $"Saved to {sfd.FileName}";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            image.SaveImage("temp1.png");
            ProcessStartInfo startInfo = new ProcessStartInfo("temp1.png");
            //startInfo.Verb = "edit";
            startInfo.UseShellExecute = true;

            Process.Start(startInfo);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (editMode)
                pictureBox1.Invalidate();
        }

        bool editMode = false;
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            editMode = true;
            pictureBox1.Paint += PictureBox1_Paint;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Mat dest = new Mat();
            Cv2.CvtColor(image, dest, ColorConversionCodes.RGB2HSV);
            mdi.MainForm.OpenChild(dest);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var d = AutoDialog.DialogHelpers.StartDialog();
            //InRangeS(imgHSV, cvScalar(20, 100, 100), cvScalar(30, 255, 255), imgThreshed)
            d.AddNumericField("r1", "red min", 20);
            d.AddNumericField("g1", "green min", 100);
            d.AddNumericField("b1", "blue min", 100);
            d.AddNumericField("r2", "red max", 30);
            d.AddNumericField("g2", "green max", 255);
            d.AddNumericField("b2", "blue max", 255);
            if (!d.ShowDialog())
                return;

            var temp = image.CvtColor(ColorConversionCodes.BGR2HSV);

            var res = temp.InRange(new Scalar(d.GetNumericField("r1"),
                d.GetNumericField("g1"),
                d.GetNumericField("b1")),
                new Scalar(d.GetNumericField("r2"),
                d.GetNumericField("g2"), d.GetNumericField("b2")));

            mdi.MainForm.OpenChild(res);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Resolution: " + image.Size() + Environment.NewLine + "Channels: " + image.Channels());
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image.Channels() != 4)
            {
                MessageBox.Show("Image should contains 4 channels");
                return;
            }
            var mats = image.Split();
            mdi.MainForm.OpenChild(mats.Last());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image.Channels() != 4)
            {
                MessageBox.Show("Image should contains 4 channels");
                return;
            }
            var mats = image.Split();
            Mat res = new Mat();
            Cv2.Merge(mats.Take(3).ToArray(), res);
            mdi.MainForm.OpenChild(res);
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            var mats = image.Split();
            string[] names = new string[] { "red", "green", "blue", "alpha" };
            for (int i = 0; i < mats.Length; i++)
            {
                Mat? item = mats[i];
                mdi.MainForm.OpenChild(item, names[i]);
            }
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddNumericField("s", "Size", 5);
            if (!d.ShowDialog()) 
                return;

            mdi.MainForm.OpenChild(image.Blur(new OpenCvSharp.Size(d.GetNumericField("s"), d.GetNumericField("s"))));
        }

        private void bniarizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddNumericField("min", "Minimum", 128);
            d.AddNumericField("max", "Maximum", 255);
            if (!d.ShowDialog())
                return;

            Mat res = null;
            if (image.Channels() == 1)
                res = image.Threshold(d.GetNumericField("min"), d.GetNumericField("max"), ThresholdTypes.Binary);
            else
                res = image.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(d.GetNumericField("min"), d.GetNumericField("max"), ThresholdTypes.Binary);
            mdi.MainForm.OpenChild(res);

        }
    }
}