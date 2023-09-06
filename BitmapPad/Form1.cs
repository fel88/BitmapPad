using AutoDialog;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XPhoto;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Packaging;
using System.Text;
using static System.Windows.Forms.AxHost;

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

            var p = pictureBox1.PointToClient(Cursor.Position);
            var pos = pictureBox1.PointToClient(Cursor.Position);
            var posx = (pos.X / zoom - sx);
            var posy = (-pos.Y / zoom - sy);
        }

        bool drag = false;

        private void PictureBox1_MouseDown(object? sender, MouseEventArgs e)
        {
            var pos = pictureBox1.PointToClient(Cursor.Position);
            var p = Transform(pos);

            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                startx = pos.X;
                starty = pos.Y;
                origsx = sx;
                origsy = sy;
            }
        }

        float startx, starty;
        float origsx, origsy;
        private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {

            float zold = zoom;
            if (e.Delta > 0) { zoom *= 1.5f; ; }
            else { zoom *= 0.5f; }
            if (zoom < 0.08) { zoom = 0.08f; }
            if (zoom > 1000) { zoom = 1000f; }

            var pos = pictureBox1.PointToClient(Cursor.Position);

            sx = -(pos.X / zold - sx - pos.X / zoom);
            sy = (pos.Y / zold + sy - pos.Y / zoom);
        }

        float shiftX;
        float shiftY;



        private void PictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            var pos = pictureBox1.PointToClient(Cursor.Position);


            if (drag)
            {
                var p = pictureBox1.PointToClient(Cursor.Position);

                sx = origsx + ((p.X - startx) / zoom);
                sy = origsy + (-(p.Y - starty) / zoom);
            }
            var bmp = image.ToBitmap();
            var gr = e.Graphics;
            gr.Clear(Color.White);
            var p0 = Transform(new PointF(0, 0));
            var p1 = Transform(new PointF(0, 100));
            var p2 = Transform(new PointF(100, 0));

            var temp1 = gr.PixelOffsetMode;
            gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gr.DrawImage(bmp, new RectangleF(p0.X, p0.Y, bmp.Width * zoom, bmp.Height * zoom), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            gr.PixelOffsetMode = temp1;

            var bt = BackTransform(pos);
            var pp = Transform(new PointF((int)bt.X, (int)bt.Y));
            gr.DrawRectangle(Pens.Blue, pp.X, pp.Y, zoom, zoom);

            gr.DrawLine(Pens.Red, p0, p1);
            gr.DrawLine(Pens.Blue, p0, p2);
            bt = GetPos();

            var pickerX = (int)bt.X;
            var pickerY = -(int)bt.Y;



            if (pickerX < 0)
                pickerX = 0;
            if (pickerX > image.Width)
                pickerX = image.Width - 1;
            if (pickerY < 0)
                pickerY = 0;
            if (pickerY > image.Height)
                pickerY = image.Height - 1;

            var px = image.At<Vec3b>(pickerY, pickerX);
            gr.FillRectangle(Brushes.White, 5, 5, 150, 150);
            gr.DrawString($"{pickerX} {pickerY}", new Font("Arial", 10), Brushes.Blue, 10, 10);
            gr.DrawString($"RGB: {px.Item2} {px.Item1} {px.Item0}", new Font("Arial", 10), Brushes.Blue, 10, 30);

            if (pickerX >= 0 && pickerY >= 0 && pickerX < image.Width && pickerY < image.Height)
            {
                var crop = new Mat(image, new Rect(pickerX, pickerY, 1, 1));
                var hsv = crop.CvtColor(ColorConversionCodes.BGR2HSV);
                var pxHsv = hsv.At<Vec3b>(0, 0);
                gr.DrawString($"HSV: {pxHsv.Item0} {pxHsv.Item1} {pxHsv.Item2}", new Font("Arial", 10), Brushes.Blue, 10, 50);
            }
            bmp.Dispose();
        }

        public float sx, sy;
        public float zoom = 1;


        public PointF Transform(PointF p1)
        {
            return new PointF((p1.X + sx) * zoom, -1 * (p1.Y + sy) * zoom);
        }
        public PointF GetPos()
        {
            var pos = pictureBox1.PointToClient(Cursor.Position);
            var posx = (pos.X / zoom - sx);
            var posy = (-pos.Y / zoom - sy);

            return new PointF(posx, posy);
        }
        public PointF BackTransform(PointF p1)
        {
            return new PointF((p1.X / zoom - sx), -(p1.Y / zoom + sy));
        }


        Mat image;

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
        public void Init(string path)
        {
            Text = path;
            //image = OpenCvSharp.Cv2.ImRead(path);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                image = Mat.FromStream(stream, ImreadModes.Unchanged);
            }
            Init(image);
        }

        internal void Init(Mat mat)
        {
            image = mat;
            var bmp = image.ToBitmap();
            pictureBox1.Image = bmp;

            toolStripStatusLabel1.Text = $"{bmp.Width}x{bmp.Height}";
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
            if (editMode)
            {
                pictureBox1.Paint -= PictureBox1_Paint;
                editMode = false;
                pictureBox1.Image = image.ToBitmap();
            }
            else
            {
                editMode = true;
                pictureBox1.Paint += PictureBox1_Paint;
            }
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
            d.AddBoolField("hsv", "HSV");
            if (!d.ShowDialog())
                return;

            Mat temp = null;
            Mat res = null;
            if (d.GetBoolField("hsv"))
            {
                temp = image.CvtColor(ColorConversionCodes.BGR2HSV);
                res = temp.InRange(new Scalar(d.GetNumericField("r1"),
             d.GetNumericField("g1"),
             d.GetNumericField("b1")),
             new Scalar(d.GetNumericField("r2"),
             d.GetNumericField("g2"), d.GetNumericField("b2")));
            }
            else
            {
                temp = image.Clone();
                res = temp.InRange(new Scalar(d.GetNumericField("b1"),
             d.GetNumericField("g1"),
             d.GetNumericField("r1")),
             new Scalar(d.GetNumericField("b2"),
             d.GetNumericField("g2"), d.GetNumericField("r2")));
            }



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

        private void sliceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var left = new Mat(image, new Rect(0, 0, image.Width / 2, image.Height));
            var right = new Mat(image, new Rect(image.Width / 2, 0, image.Width / 2, image.Height));
            mdi.MainForm.OpenChild(left);
            mdi.MainForm.OpenChild(right);
        }

        private void sliceHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var top = new Mat(image, new Rect(0, 0, image.Width, image.Height / 2));
            var bottom = new Mat(image, new Rect(0, image.Height / 2, image.Width, image.Height / 2));
            mdi.MainForm.OpenChild(top);
            mdi.MainForm.OpenChild(bottom);
        }

        private void toClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddNumericField("w", "Width", image.Width);
            d.AddNumericField("h", "Height", image.Height);
            if (!d.ShowDialog())
                return;

            mdi.MainForm.OpenChild(image.Resize(new OpenCvSharp.Size(d.GetNumericField("w"), d.GetNumericField("h"))));
        }

        private void cropWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat inv = new Mat();
            Cv2.BitwiseNot(image, inv);
            var coords = inv.FindNonZero();
            var rect = Cv2.BoundingRect(coords);
            var cropped = image.Clone(rect);

            mdi.MainForm.OpenChild(cropped);
        }

        private void asMonochrome1BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            using (var bmp = image.ToBitmap())
            {
                using (var clone = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format1bppIndexed))
                {
                    clone.Save(sfd.FileName);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            using (var bmp = image.ToBitmap())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("P3");
                sb.AppendLine($"{bmp.Width} {bmp.Height}");
                sb.AppendLine("255");
                for (int j = 0; j < bmp.Height; j++)
                {
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        var px = bmp.GetPixel(i, j);
                        sb.AppendLine($"{px.R} {px.G} {px.B}");
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }

        private void nearestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            nearestToolStripMenuItem.Checked = true;
            cubicToolStripMenuItem.Checked = false;
            pictureBox1.Invalidate();
        }

        private void cubicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            nearestToolStripMenuItem.Checked = false;
            cubicToolStripMenuItem.Checked = true;
            pictureBox1.Invalidate();
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
            d.AddBoolField("otsu", "Otsu");

            if (!d.ShowDialog())
                return;

            Mat res = null;
            var method = ThresholdTypes.Binary;
            if (d.GetBoolField("otsu"))
            {
                method = ThresholdTypes.Otsu;
            }
            if (image.Channels() == 1)
                res = image.Threshold(d.GetNumericField("min"), d.GetNumericField("max"), method);
            else
                res = image.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(d.GetNumericField("min"), d.GetNumericField("max"), method);
            mdi.MainForm.OpenChild(res);

        }

        private void connectedComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int[,] labels = null;
            //Cv2.ConnectedComponents(image, out labels, PixelConnectivity.Connectivity8);
            OpenCvSharp.Point[][] points = null;
            HierarchyIndex[] hindex = null;
            image.FindContours(out points, out hindex, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            Mat ret = image.Clone();
            ret = ret.CvtColor(ColorConversionCodes.GRAY2BGR);
            var fr = points.OrderByDescending(z => Math.Abs(Cv2.ContourArea(z))).First();
            List<OpenCvSharp.Point[]> points2 = new List<OpenCvSharp.Point[]>();
            points2.Add(fr);
            for (int i = 0; i < points2.Count; i++)
            {
                var mm = Cv2.Moments(points2[i]);
                var cx = mm.M10 / mm.M00;
                var cy = mm.M01 / mm.M00;
                ret.DrawContours(points2.ToArray(), i, Scalar.Blue);
                ret.DrawMarker(new OpenCvSharp.Point(cx, cy), Scalar.Red);
            }
            mdi.MainForm.OpenChild(ret);
        }
    }
}