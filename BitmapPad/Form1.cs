using AutoDialog;
using DitheringLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XPhoto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Packaging;
using System.Net.Http.Headers;
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
            BackColor = pictureBox1.BackColor;
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
            gr.Clear(BackColor);
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
            gr.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.White)), 5, 5, 150, 150);
            gr.DrawString($"{pickerX} {pickerY}", new Font("Arial", 10), Brushes.Blue, 10, 10);
            gr.DrawString($"RGB: {px.Item2} {px.Item1} {px.Item0}", new Font("Arial", 10), Brushes.Blue, 10, 30);

            if (pickerX >= 0 && pickerY >= 0 && pickerX < image.Width && pickerY < image.Height)
            {
                var crop = new Mat(image, new Rect(pickerX, pickerY, 1, 1));
                if (crop.Channels() == 3)
                {
                    var hsv = crop.CvtColor(ColorConversionCodes.BGR2HSV);
                    var pxHsv = hsv.At<Vec3b>(0, 0);
                    gr.DrawString($"HSV: {pxHsv.Item0} {pxHsv.Item1} {pxHsv.Item2}", new Font("Arial", 10), Brushes.Blue, 10, 50);
                }
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
        public Mat Image => image;

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
            SpawnChild(dest);

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

        private void ApplySettings(ViewerSettings settings)
        {
            pictureBox1.BackColor = BackColor = settings.BackColor;
            pictureBox1.InterpolationMode = settings.InterpolationMode;
        }

        internal void Init(Mat mat, ViewerSettings settings = null)
        {
            if (settings != null)
                ApplySettings(settings);

            image = mat;
            var bmp = image.ToBitmap();
            pictureBox1.Image = bmp;

            toolStripStatusLabel1.Text = $"{bmp.Width}x{bmp.Height} ({bmp.PixelFormat})";
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
            mdi.MainForm.OpenChild(dest, GetViewerSettings());
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
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
            SpawnChild(mats.Last());
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image.Channels() != 4)
            {
                MessageBox.Show("Image should contains 4 channels");
                return;
            }

            SpawnChild(RemoveAlpha(image));
        }

        Mat RemoveAlpha(Mat image)
        {
            var mats = image.Split();
            Mat res = new Mat();
            Cv2.Merge(mats.Take(3).ToArray(), res);
            return res;
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {

        }

        private void sliceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sliceHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddNumericField("w", "Width", image.Width, 10000, 1, 0);
            d.AddNumericField("h", "Height", image.Height, 10000, 1, 0);
            d.AddOptionsField("mode", "Interpolation", Enum.GetNames(typeof(InterpolationFlags)), Enum.GetName(typeof(InterpolationFlags), InterpolationFlags.Linear));
            if (!d.ShowDialog())
                return;

            var mode = d.GetOptionsField("mode");
            var ff = (InterpolationFlags)Enum.Parse(typeof(InterpolationFlags), mode);
            SpawnChild(image.Resize(new OpenCvSharp.Size(d.GetNumericField("w"), d.GetNumericField("h")), interpolation: ff));
        }

        private void cropWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                    clone.Save(sfd.FileName, ImageFormat.Bmp);
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

        private void minimalRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat inv = new Mat();
            var bmp = image.ToBitmap();
            List<Point2f> pp = new List<Point2f>();
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    if (bmp.GetPixel(i, j).R > 128)
                    {
                        pp.Add(new Point2f(i, j));
                    }
                }
            }
            bmp.Dispose();
            Cv2.BitwiseNot(image, inv);

            var rect = Cv2.MinAreaRect(pp.ToArray());

            var mtr = Cv2.GetRotationMatrix2D(rect.Center, rect.Angle, 1);
            //var pp1 = rect.Points().OrderBy(z=>z.X).ThenBy(z=>z.Y).ToArray();
            var ptr = Cv2.GetPerspectiveTransform(rect.Points(), new Point2f[] { new Point2f (0,rect.Size.Height),new Point2f (0,0),
+            new Point2f (rect.Size.Width,0),new Point2f (rect.Size.Width,rect.Size.Height)});
            var cropped = image.WarpPerspective(ptr, new OpenCvSharp.Size(rect.Size.Width, rect.Size.Height));
            //var cropped = image.WarpAffine(mtr, new OpenCvSharp.Size(rect.Size.Width, rect.Size.Height));            

            SpawnChild(cropped);
        }

        private void inverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat inversed = new Mat();
            Cv2.BitwiseNot(image, inversed);
            SpawnChild(inversed);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo make dialog to select N parts
            var top = new Mat(image, new Rect(0, 0, image.Width, image.Height / 2));
            var bottom = new Mat(image, new Rect(0, image.Height / 2, image.Width, image.Height / 2));
            SpawnChild(top);
            SpawnChild(bottom);
        }

        public void SpawnChild(Mat mat)
        {
            mdi.MainForm.OpenChild(mat, GetViewerSettings());
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var left = new Mat(image, new Rect(0, 0, image.Width / 2, image.Height));
            var right = new Mat(image, new Rect(image.Width / 2, 0, image.Width / 2, image.Height));
            mdi.MainForm.OpenChild(left, GetViewerSettings());
            mdi.MainForm.OpenChild(right, GetViewerSettings());
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make dialog to select color black/white
            Mat inv = new Mat();
            Cv2.BitwiseNot(image, inv);
            var coords = inv.FindNonZero();
            var rect = Cv2.BoundingRect(coords);
            var cropped = image.Clone(rect);

            SpawnChild(cropped);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Mat dst = new Mat();
            Cv2.Rotate(image, dst, RotateFlags.Rotate90Counterclockwise);
            SpawnChild(dst);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Mat dst = new Mat();
            Cv2.Rotate(image, dst, RotateFlags.Rotate90Clockwise);
            SpawnChild(dst);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Mat dst = new Mat();
            Cv2.Flip(image, dst, FlipMode.Y);
            SpawnChild(dst);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Mat dst = new Mat();
            Cv2.Flip(image, dst, FlipMode.X);
            SpawnChild(dst);
        }

        private void ditheringToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dithering d = new Dithering();
            using var tt = image.ToBitmap();
            using var res = d.Process(tt);

            SpawnChild(res.ToMat());
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddNumericField("top", "Top", 0, image.Height - 1, 0, 0);
            d.AddNumericField("bottom", "Bottom", 0, image.Height - 1, 0, 0);
            d.AddNumericField("left", "Left", 0, image.Width - 1, 0, 0);
            d.AddNumericField("right", "Right", 0, image.Width - 1, 0, 0);
            if (!d.ShowDialog())
                return;

            var xx = d.GetIntegerNumericField("left");
            var yy = d.GetIntegerNumericField("top");
            var ww = image.Width - d.GetIntegerNumericField("right") - xx;
            var hh = image.Height - d.GetIntegerNumericField("bottom") - yy;
            var crop = new Mat(image, new Rect(xx, yy, ww, hh));
            SpawnChild(crop);
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddNumericField("s", "Size", 5);
            if (!d.ShowDialog())
                return;

            SpawnChild(image.Blur(new OpenCvSharp.Size(d.GetNumericField("s"), d.GetNumericField("s"))));
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
            mdi.MainForm.OpenChild(res, GetViewerSettings());

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
            SpawnChild(ret);
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CropImage cropEditor = new CropImage();
            cropEditor.Init(image.ToBitmap());
            if (cropEditor.ShowDialog() != DialogResult.OK)
                return;

            var ca = cropEditor.CropArea;
            SpawnChild(image.Clone(new Rect((int)ca.X, (int)ca.Y, (int)ca.Width, (int)ca.Height)));
        }


        public ViewerSettings GetViewerSettings()
        {
            return new ViewerSettings()
            {
                BackColor = BackColor,
                InterpolationMode = pictureBox1.InterpolationMode
            };
        }

        Color BackColor;
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = BackColor;
            if (cd.ShowDialog() != DialogResult.OK)
                return;

            pictureBox1.BackColor = BackColor = cd.Color;
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = BackColor = Color.Black;
        }

        public class ColorInfo
        {
            public Vec3b Color;
            public int Frequency;
        }

        public ColorInfo[] GetColorsInfo(Mat image)
        {
            Dictionary<string, ColorInfo> dic = new Dictionary<string, ColorInfo>();
            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b pixel = image.Get<Vec3b>(y, x);
                    var key = $"{pixel.Item0};{pixel.Item1};{pixel.Item2}";
                    if (!dic.ContainsKey(key))
                    {
                        dic.Add(key, new ColorInfo() { Color = pixel });
                    }
                    var fr = dic[key];
                    fr.Frequency++;
                }
            }
            return dic.Values.ToArray();
        }

        public static Scalar[] FindDominantColors(Mat image, int k)
        {
            // 1. Convert to a suitable color space (e.g., HSV)
            Mat hsvImage = new Mat();
            Cv2.CvtColor(image, hsvImage, ColorConversionCodes.BGR2HSV);

            // 2. Reshape the image data for K-Means
            Mat samples = new Mat(hsvImage.Rows * hsvImage.Cols, 3, MatType.CV_32F);
            for (int y = 0; y < hsvImage.Rows; y++)
            {
                for (int x = 0; x < hsvImage.Cols; x++)
                {
                    Vec3b pixel = hsvImage.Get<Vec3b>(y, x);
                    samples.Set(y * hsvImage.Cols + x, 0, (float)pixel.Item0); // H
                    samples.Set(y * hsvImage.Cols + x, 1, (float)pixel.Item1); // S
                    samples.Set(y * hsvImage.Cols + x, 2, (float)pixel.Item2); // V
                }
            }

            // 3. Apply K-Means clustering
            Mat labels = new Mat();
            Mat centers = new Mat();
            Cv2.Kmeans(samples, k, labels, new TermCriteria(CriteriaTypes.Eps | CriteriaTypes.MaxIter, 10, 1.0), 3, KMeansFlags.PpCenters, centers);

            // 4. Extract dominant colors (cluster centroids)
            Scalar[] dominantColors = new Scalar[k];
            for (int i = 0; i < k; i++)
            {
                // Convert back to BGR if needed for visualization
                Mat colorMat = new Mat(1, 1, MatType.CV_8UC3);
                colorMat.Set(0, 0, new Vec3b((byte)centers.Get<float>(i, 0), (byte)centers.Get<float>(i, 1), (byte)centers.Get<float>(i, 2)));
                Mat bgrColor = new Mat();
                Cv2.CvtColor(colorMat, bgrColor, ColorConversionCodes.HSV2BGR);
                dominantColors[i] = new Scalar(bgrColor.Get<Vec3b>(0, 0).Item0, bgrColor.Get<Vec3b>(0, 0).Item1, bgrColor.Get<Vec3b>(0, 0).Item2);
            }
            return dominantColors;
        }
        private void whiteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = BackColor = Color.White;
        }
        // You can also define a custom palette directly
        public static Mat QuantizeToCustomPalette(Mat src, List<Vec3b> customPalette)
        {
            Mat dst = new Mat(src.Size(), src.Type());
            for (int r = 0; r < src.Rows; r++)
            {
                for (int c = 0; c < src.Cols; c++)
                {
                    Vec3b originalColor = src.At<Vec3b>(r, c);
                    Vec3b closestColor = FindClosestColor(originalColor, customPalette);
                    dst.Set(r, c, closestColor);
                }
            }
            return dst;
        }

        // Helper function to find the closest color in a palette
        private static Vec3b FindClosestColor(Vec3b targetColor, List<Vec3b> palette)
        {
            double minDistance = double.MaxValue;
            Vec3b closestColor = palette[0];

            foreach (var paletteColor in palette)
            {
                // Calculate Euclidean distance (or other color distance metric)
                double distance = System.Math.Pow(targetColor.Item0 - paletteColor.Item0, 2) +
                                  System.Math.Pow(targetColor.Item1 - paletteColor.Item1, 2) +
                                  System.Math.Pow(targetColor.Item2 - paletteColor.Item2, 2);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestColor = paletteColor;
                }
            }
            return closestColor;
        }

        private void kmeansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat matToDispose = null;
            Mat targetImage = image;
            if (image.Channels() == 4)
            {
                if (MessageBox.Show("Only 24 bit images allowed. Remove alpha channel?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                targetImage = matToDispose = RemoveAlpha(image);

            }
            var d = AutoDialog.DialogHelpers.StartDialog();
            d.AddIntegerNumericField("k", "K", 8, 12, 2);// Desired number of clusters (colors)
            d.AddIntegerNumericField("maxCount", "maxCount", 10, 200, 1);
            d.AddNumericField("eps", "eps", 1.0, 10, 0.01m);
            d.AddOptionsField("flags", "Flags", Enum.GetNames<KMeansFlags>(), 0);
            if (!d.ShowDialog())
                return;

            int K = d.GetIntegerNumericField("k");
            var eps = d.GetNumericField("eps");
            var maxCount = d.GetIntegerNumericField("maxCount");
            var flags = Enum.GetValues<KMeansFlags>()[d.GetOptionsFieldIdx("flags")];
            /*
             * # Define criteria = ( type, max_iter = 10 , epsilon = 1.0 )
criteria = (cv.TERM_CRITERIA_EPS + cv.TERM_CRITERIA_MAX_ITER, 10, 1.0)
# Set flags (Just to avoid line break in the code)
flags = cv.KMEANS_RANDOM_CENTERS
# Apply KMeans
compactness,labels,centers = cv.kmeans(z,2,None,criteria,10,flags)
             */  // Reshape the image to a 2D array of pixels (Mx3 for an RGB image)
                 // Each row is a pixel, and columns are R, G, B values.



            using (Mat samples = targetImage.Reshape(1, targetImage.Rows * targetImage.Cols))
            {
                // Convert to float32 for KMeans
                samples.ConvertTo(samples, MatType.CV_32F);
                TermCriteria criteria = new TermCriteria(
                    CriteriaTypes.Eps | CriteriaTypes.MaxIter, maxCount, eps);
                using (Mat bestLabels = new Mat())
                using (Mat centers = new Mat())
                {

                    // Perform K-Means clustering
                    Cv2.Kmeans(samples, K, bestLabels, criteria, 10, flags, centers);

                    // Convert centers back to uint8 for image representation
                    centers.ConvertTo(centers, MatType.CV_8U);

                    // Map labels back to the corresponding center colors
                    Mat result = new Mat(targetImage.Size(), targetImage.Type());

                    for (int i = 0; i < samples.Rows; i++)
                    {
                        int label = bestLabels.Get<int>(i);
                        Vec3b color = centers.Get<Vec3b>(label);
                        result.Set<Vec3b>(i / targetImage.Cols, i % targetImage.Cols, color);
                    }
                    SpawnChild(result);

                }
            }

            matToDispose?.Dispose();
        }

        private void toDominantColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image.Channels() == 4)
            {
                MessageBox.Show("only 24 bit images allowed");
                return;
            }
            var d = AutoDialog.DialogHelpers.StartDialog();
            d.AddIntegerNumericField("k", "K", 8, 12, 2);// Desired number of clusters (colors)

            if (!d.ShowDialog())
                return;

            int K = d.GetIntegerNumericField("k");

            var dominants = FindDominantColors(image, K);
            var result = QuantizeToCustomPalette(image, dominants.Select(z => new Vec3b((byte)z.Val0, (byte)z.Val1, (byte)z.Val2)).ToList());
            SpawnChild(result);
        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Resolution: {image.Size()}{Environment.NewLine}Channels: {image.Channels()}{Environment.NewLine}Aspect: {Math.Round((double)image.Width / image.Height, 4)}");

        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colors = GetColorsInfo(image).OrderByDescending(z => z.Frequency);
            Form f = new Form();
            ListView lv = new ListView() { FullRowSelect = true, GridLines = true, View = View.Details };
            lv.Columns.Add("R", 60);
            lv.Columns.Add("G", 60);
            lv.Columns.Add("B", 60);
            lv.Columns.Add("Freq", 255);
            lv.Dock = DockStyle.Fill;
            f.Controls.Add(lv);
            foreach (var item in colors)
            {
                lv.Items.Add(new ListViewItem([
                    item.Color.Item2.ToString(),
                    item.Color.Item1.ToString(),
                    item.Color.Item0.ToString(),
                    item.Frequency.ToString(),
                ])
                { Tag = item });
            }
            f.MdiParent = MdiParent;
            f.Show();
        }

        private void rangeToolStripMenuItem_Click(object sender, EventArgs e)
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



            SpawnChild(res);
        }

        private void replaceColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.Text = "Target color";
            d.AddIntegerNumericField("r", "R", max: 255);
            d.AddIntegerNumericField("g", "G", max: 255);
            d.AddIntegerNumericField("b", "B", max: 255);

            if (!d.ShowDialog())
                return;
            var r = (byte)d.GetIntegerNumericField("r");
            var g = (byte)d.GetIntegerNumericField("g");
            var b = (byte)d.GetIntegerNumericField("b");

            d = DialogHelpers.StartDialog();
            d.Text = "New color";
            d.AddIntegerNumericField("r", "R", max: 255);
            d.AddIntegerNumericField("g", "G", max: 255);
            d.AddIntegerNumericField("b", "B", max: 255);

            if (!d.ShowDialog())
                return;

            var r2 = (byte)d.GetIntegerNumericField("r");
            var g2 = (byte)d.GetIntegerNumericField("g");
            var b2 = (byte)d.GetIntegerNumericField("b");
            Mat clone = image.Clone();
            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b pixel = image.Get<Vec3b>(y, x);
                    if (pixel.Item2 == r && pixel.Item1 == g && pixel.Item0 == b)
                    {
                        clone.Set<Vec3b>(y, x, new Vec3b(b2, g2, r2));
                    }
                }
            }
            SpawnChild(clone);
        }

        private void binarizeWithDominantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clrs = GetColorsInfo(image);
            if (clrs.Length != 2)
            {
                ShowError("More than 2 colors detected.");
                return;
            }
            var d = DialogHelpers.StartDialog();

            d.AddBoolField("inverse", "Inverse");

            if (!d.ShowDialog())
                return;

            var inverse = d.GetBoolField("inverse");

            var colorToMatch = clrs[inverse ? 1 : 0].Color;

            var r = colorToMatch.Item2;
            var g = colorToMatch.Item1;
            var b = colorToMatch.Item0;

            Mat clone = image.Clone();
            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b pixel = image.Get<Vec3b>(y, x);
                    if (pixel.Item2 == r && pixel.Item1 == g && pixel.Item0 == b)
                    {
                        clone.Set<Vec3b>(y, x, new Vec3b(252, 255, 255));
                    }
                    else
                    {
                        clone.Set<Vec3b>(y, x, new Vec3b(0, 0, 0));

                    }
                }
            }
            SpawnChild(clone);
        }

        private void ShowError(string v)
        {
            MessageBox.Show(v, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void splitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mats = image.Split();
            string[] names = new string[] { "red", "green", "blue", "alpha" };
            for (int i = 0; i < mats.Length; i++)
            {
                Mat? item = mats[i];
                mdi.MainForm.OpenChild(item, GetViewerSettings(), names[i]);
            }
        }

        private void to32BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnChild(image.CvtColor(ColorConversionCodes.GRAY2BGR));
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //# Define brightness adjustment value (e.g., +50 for brighter, -50 for darker)
            double brightness_value = 50;

            var d = DialogHelpers.StartDialog();
            d.AddNumericField("value", "Value", brightness_value);
            if (!d.ShowDialog())
                return;

            brightness_value = d.GetNumericField("value");

            //# Adjust brightness using cv2.convertScaleAbs()
            //# alpha=1.0 for no contrast change, beta=brightness_value for brightness adjustment
            Mat brightened_image = new Mat();
            Cv2.ConvertScaleAbs(image, brightened_image, 1.0, brightness_value);
            SpawnChild(brightened_image);

        }

        private void setCaptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = DialogHelpers.StartDialog();
            d.AddStringField("caption", "Caption", Text);
            if (!d.ShowDialog())
                return;

            Text = d.GetStringField("caption");
        }

        private void erodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat dest = new Mat(); 
            OpenCvSharp.Size kernelSize = new OpenCvSharp.Size(3, 3);
            Scalar fillValue = new Scalar (1);
            Mat kernel = new Mat(kernelSize, MatType.CV_16SC1, fillValue);
            OpenCvSharp.InputArray kernelArr = OpenCvSharp.InputArray.Create(kernel);

            Cv2.Erode(image, dest, kernelArr);
            SpawnChild(dest);
        }
    }
}
