namespace BitmapPad
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBoxWithInterpolationMode();
            contextMenuStrip1 = new ContextMenuStrip(components);
            openToolStripMenuItem = new ToolStripMenuItem();
            toClipboardToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            viewToolStripMenuItem = new ToolStripMenuItem();
            interpolationModeToolStripMenuItem = new ToolStripMenuItem();
            nearestToolStripMenuItem = new ToolStripMenuItem();
            cubicToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            asMonochrome1BitToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripButton6 = new ToolStripButton();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            extractToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            toolStripButton7 = new ToolStripButton();
            toolStripDropDownButton3 = new ToolStripDropDownButton();
            blurToolStripMenuItem = new ToolStripMenuItem();
            bniarizeToolStripMenuItem = new ToolStripMenuItem();
            ditheringToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton4 = new ToolStripDropDownButton();
            connectedComponentsToolStripMenuItem = new ToolStripMenuItem();
            sliceToolStripMenuItem = new ToolStripMenuItem();
            verticalToolStripMenuItem = new ToolStripMenuItem();
            horizontalToolStripMenuItem = new ToolStripMenuItem();
            resizeToolStripMenuItem = new ToolStripMenuItem();
            cropWhiteToolStripMenuItem = new ToolStripMenuItem();
            whiteToolStripMenuItem = new ToolStripMenuItem();
            minimalRectToolStripMenuItem = new ToolStripMenuItem();
            customToolStripMenuItem = new ToolStripMenuItem();
            editorToolStripMenuItem = new ToolStripMenuItem();
            inverseToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripDropDownButton5 = new ToolStripDropDownButton();
            backColorToolStripMenuItem = new ToolStripMenuItem();
            blackToolStripMenuItem = new ToolStripMenuItem();
            whiteToolStripMenuItem1 = new ToolStripMenuItem();
            customToolStripMenuItem1 = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.ContextMenuStrip = contextMenuStrip1;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            pictureBox1.Location = new Point(0, 25);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 291);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { openToolStripMenuItem, toClipboardToolStripMenuItem, toolStripSeparator1, viewToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(139, 76);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(138, 22);
            openToolStripMenuItem.Text = "open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toClipboardToolStripMenuItem
            // 
            toClipboardToolStripMenuItem.Name = "toClipboardToolStripMenuItem";
            toClipboardToolStripMenuItem.Size = new Size(138, 22);
            toClipboardToolStripMenuItem.Text = "to clipboard";
            toClipboardToolStripMenuItem.Click += toClipboardToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(135, 6);
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { interpolationModeToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(138, 22);
            viewToolStripMenuItem.Text = "view";
            // 
            // interpolationModeToolStripMenuItem
            // 
            interpolationModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { nearestToolStripMenuItem, cubicToolStripMenuItem });
            interpolationModeToolStripMenuItem.Name = "interpolationModeToolStripMenuItem";
            interpolationModeToolStripMenuItem.Size = new Size(176, 22);
            interpolationModeToolStripMenuItem.Text = "interpolation mode";
            // 
            // nearestToolStripMenuItem
            // 
            nearestToolStripMenuItem.Name = "nearestToolStripMenuItem";
            nearestToolStripMenuItem.Size = new Size(115, 22);
            nearestToolStripMenuItem.Text = "nearest";
            nearestToolStripMenuItem.Click += nearestToolStripMenuItem_Click;
            // 
            // cubicToolStripMenuItem
            // 
            cubicToolStripMenuItem.Name = "cubicToolStripMenuItem";
            cubicToolStripMenuItem.Size = new Size(115, 22);
            cubicToolStripMenuItem.Text = "smooth";
            cubicToolStripMenuItem.Click += cubicToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5, toolStripButton6, toolStripDropDownButton2, toolStripButton7, toolStripDropDownButton3, toolStripDropDownButton4, toolStripDropDownButton5 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(776, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, exportToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(53, 22);
            toolStripDropDownButton1.Text = "Image";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(108, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { asMonochrome1BitToolStripMenuItem, toolStripMenuItem1 });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(108, 22);
            exportToolStripMenuItem.Text = "Export";
            // 
            // asMonochrome1BitToolStripMenuItem
            // 
            asMonochrome1BitToolStripMenuItem.Name = "asMonochrome1BitToolStripMenuItem";
            asMonochrome1BitToolStripMenuItem.Size = new Size(223, 22);
            asMonochrome1BitToolStripMenuItem.Text = "as monochrome BMP (1 bit)";
            asMonochrome1BitToolStripMenuItem.Click += asMonochrome1BitToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(223, 22);
            toolStripMenuItem1.Text = "PPM";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(102, 22);
            toolStripMenuItem2.Text = "ASCII";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(42, 22);
            toolStripButton1.Text = "mode";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(74, 22);
            toolStripButton2.Text = "to grayscale";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(65, 22);
            toolStripButton3.Text = "edit mode";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(43, 22);
            toolStripButton4.Text = "to hsv";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(71, 22);
            toolStripButton5.Text = "color range";
            toolStripButton5.Click += toolStripButton5_Click;
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(32, 22);
            toolStripButton6.Text = "info";
            toolStripButton6.Click += toolStripButton6_Click;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { extractToolStripMenuItem, removeToolStripMenuItem });
            toolStripDropDownButton2.Image = (Image)resources.GetObject("toolStripDropDownButton2.Image");
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(49, 22);
            toolStripDropDownButton2.Text = "alpha";
            // 
            // extractToolStripMenuItem
            // 
            extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            extractToolStripMenuItem.Size = new Size(114, 22);
            extractToolStripMenuItem.Text = "extract";
            extractToolStripMenuItem.Click += extractToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(114, 22);
            removeToolStripMenuItem.Text = "remove";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(83, 22);
            toolStripButton7.Text = "split channels";
            toolStripButton7.Click += toolStripButton7_Click_1;
            // 
            // toolStripDropDownButton3
            // 
            toolStripDropDownButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton3.DropDownItems.AddRange(new ToolStripItem[] { blurToolStripMenuItem, bniarizeToolStripMenuItem, ditheringToolStripMenuItem });
            toolStripDropDownButton3.Image = (Image)resources.GetObject("toolStripDropDownButton3.Image");
            toolStripDropDownButton3.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            toolStripDropDownButton3.Size = new Size(49, 22);
            toolStripDropDownButton3.Text = "filters";
            // 
            // blurToolStripMenuItem
            // 
            blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            blurToolStripMenuItem.Size = new Size(122, 22);
            blurToolStripMenuItem.Text = "blur";
            blurToolStripMenuItem.Click += blurToolStripMenuItem_Click;
            // 
            // bniarizeToolStripMenuItem
            // 
            bniarizeToolStripMenuItem.Name = "bniarizeToolStripMenuItem";
            bniarizeToolStripMenuItem.Size = new Size(122, 22);
            bniarizeToolStripMenuItem.Text = "bniarize";
            bniarizeToolStripMenuItem.Click += bniarizeToolStripMenuItem_Click;
            // 
            // ditheringToolStripMenuItem
            // 
            ditheringToolStripMenuItem.Name = "ditheringToolStripMenuItem";
            ditheringToolStripMenuItem.Size = new Size(122, 22);
            ditheringToolStripMenuItem.Text = "dithering";
            ditheringToolStripMenuItem.Click += ditheringToolStripMenuItem_Click;
            // 
            // toolStripDropDownButton4
            // 
            toolStripDropDownButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton4.DropDownItems.AddRange(new ToolStripItem[] { connectedComponentsToolStripMenuItem, sliceToolStripMenuItem, resizeToolStripMenuItem, cropWhiteToolStripMenuItem, inverseToolStripMenuItem, toolStripMenuItem3, toolStripMenuItem6 });
            toolStripDropDownButton4.Image = (Image)resources.GetObject("toolStripDropDownButton4.Image");
            toolStripDropDownButton4.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            toolStripDropDownButton4.Size = new Size(76, 22);
            toolStripDropDownButton4.Text = "operations";
            // 
            // connectedComponentsToolStripMenuItem
            // 
            connectedComponentsToolStripMenuItem.Name = "connectedComponentsToolStripMenuItem";
            connectedComponentsToolStripMenuItem.Size = new Size(200, 22);
            connectedComponentsToolStripMenuItem.Text = "connected components";
            connectedComponentsToolStripMenuItem.Click += connectedComponentsToolStripMenuItem_Click;
            // 
            // sliceToolStripMenuItem
            // 
            sliceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verticalToolStripMenuItem, horizontalToolStripMenuItem });
            sliceToolStripMenuItem.Name = "sliceToolStripMenuItem";
            sliceToolStripMenuItem.Size = new Size(200, 22);
            sliceToolStripMenuItem.Text = "split";
            sliceToolStripMenuItem.Click += sliceToolStripMenuItem_Click;
            // 
            // verticalToolStripMenuItem
            // 
            verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            verticalToolStripMenuItem.Size = new Size(127, 22);
            verticalToolStripMenuItem.Text = "vertical";
            verticalToolStripMenuItem.Click += verticalToolStripMenuItem_Click;
            // 
            // horizontalToolStripMenuItem
            // 
            horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            horizontalToolStripMenuItem.Size = new Size(127, 22);
            horizontalToolStripMenuItem.Text = "horizontal";
            horizontalToolStripMenuItem.Click += horizontalToolStripMenuItem_Click;
            // 
            // resizeToolStripMenuItem
            // 
            resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            resizeToolStripMenuItem.Size = new Size(200, 22);
            resizeToolStripMenuItem.Text = "resize";
            resizeToolStripMenuItem.Click += resizeToolStripMenuItem_Click;
            // 
            // cropWhiteToolStripMenuItem
            // 
            cropWhiteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { whiteToolStripMenuItem, minimalRectToolStripMenuItem, customToolStripMenuItem, editorToolStripMenuItem });
            cropWhiteToolStripMenuItem.Name = "cropWhiteToolStripMenuItem";
            cropWhiteToolStripMenuItem.Size = new Size(200, 22);
            cropWhiteToolStripMenuItem.Text = "crop";
            cropWhiteToolStripMenuItem.Click += cropWhiteToolStripMenuItem_Click;
            // 
            // whiteToolStripMenuItem
            // 
            whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            whiteToolStripMenuItem.Size = new Size(141, 22);
            whiteToolStripMenuItem.Text = "AABC";
            whiteToolStripMenuItem.Click += whiteToolStripMenuItem_Click;
            // 
            // minimalRectToolStripMenuItem
            // 
            minimalRectToolStripMenuItem.Name = "minimalRectToolStripMenuItem";
            minimalRectToolStripMenuItem.Size = new Size(141, 22);
            minimalRectToolStripMenuItem.Text = "Minimal rect";
            minimalRectToolStripMenuItem.Click += minimalRectToolStripMenuItem_Click;
            // 
            // customToolStripMenuItem
            // 
            customToolStripMenuItem.Name = "customToolStripMenuItem";
            customToolStripMenuItem.Size = new Size(141, 22);
            customToolStripMenuItem.Text = "Custom";
            customToolStripMenuItem.Click += customToolStripMenuItem_Click;
            // 
            // editorToolStripMenuItem
            // 
            editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            editorToolStripMenuItem.Size = new Size(141, 22);
            editorToolStripMenuItem.Text = "Editor";
            editorToolStripMenuItem.Click += editorToolStripMenuItem_Click;
            // 
            // inverseToolStripMenuItem
            // 
            inverseToolStripMenuItem.Name = "inverseToolStripMenuItem";
            inverseToolStripMenuItem.Size = new Size(200, 22);
            inverseToolStripMenuItem.Text = "inverse";
            inverseToolStripMenuItem.Click += inverseToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem4, toolStripMenuItem5 });
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(200, 22);
            toolStripMenuItem3.Text = "rotate";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(101, 22);
            toolStripMenuItem4.Text = "CCW";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(101, 22);
            toolStripMenuItem5.Text = "CW";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8 });
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(200, 22);
            toolStripMenuItem6.Text = "mirror";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(127, 22);
            toolStripMenuItem7.Text = "vertical";
            toolStripMenuItem7.Click += toolStripMenuItem7_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(127, 22);
            toolStripMenuItem8.Text = "horizontal";
            toolStripMenuItem8.Click += toolStripMenuItem8_Click;
            // 
            // toolStripDropDownButton5
            // 
            toolStripDropDownButton5.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton5.DropDownItems.AddRange(new ToolStripItem[] { backColorToolStripMenuItem });
            toolStripDropDownButton5.Image = (Image)resources.GetObject("toolStripDropDownButton5.Image");
            toolStripDropDownButton5.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton5.Name = "toolStripDropDownButton5";
            toolStripDropDownButton5.Size = new Size(39, 22);
            toolStripDropDownButton5.Text = "aux";
            // 
            // backColorToolStripMenuItem
            // 
            backColorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { blackToolStripMenuItem, whiteToolStripMenuItem1, customToolStripMenuItem1 });
            backColorToolStripMenuItem.Name = "backColorToolStripMenuItem";
            backColorToolStripMenuItem.Size = new Size(180, 22);
            backColorToolStripMenuItem.Text = "back color";
            backColorToolStripMenuItem.Click += backColorToolStripMenuItem_Click;
            // 
            // blackToolStripMenuItem
            // 
            blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            blackToolStripMenuItem.Size = new Size(180, 22);
            blackToolStripMenuItem.Text = "black";
            blackToolStripMenuItem.Click += blackToolStripMenuItem_Click;
            // 
            // whiteToolStripMenuItem1
            // 
            whiteToolStripMenuItem1.Name = "whiteToolStripMenuItem1";
            whiteToolStripMenuItem1.Size = new Size(180, 22);
            whiteToolStripMenuItem1.Text = "white";
            whiteToolStripMenuItem1.Click += whiteToolStripMenuItem1_Click;
            // 
            // customToolStripMenuItem1
            // 
            customToolStripMenuItem1.Name = "customToolStripMenuItem1";
            customToolStripMenuItem1.Size = new Size(180, 22);
            customToolStripMenuItem1.Text = "custom";
            customToolStripMenuItem1.Click += customToolStripMenuItem1_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 316);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(776, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(13, 17);
            toolStripStatusLabel1.Text = "..";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 25;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 338);
            Controls.Add(pictureBox1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Image";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private PictureBoxWithInterpolationMode pictureBox1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem extractToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripButton toolStripButton7;
        private ToolStripDropDownButton toolStripDropDownButton3;
        private ToolStripMenuItem blurToolStripMenuItem;
        private ToolStripMenuItem bniarizeToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton4;
        private ToolStripMenuItem connectedComponentsToolStripMenuItem;
        private ToolStripMenuItem sliceToolStripMenuItem;
        private ToolStripMenuItem toClipboardToolStripMenuItem;
        private ToolStripMenuItem resizeToolStripMenuItem;
        private ToolStripMenuItem cropWhiteToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem asMonochrome1BitToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem interpolationModeToolStripMenuItem;
        private ToolStripMenuItem nearestToolStripMenuItem;
        private ToolStripMenuItem cubicToolStripMenuItem;
        private ToolStripMenuItem whiteToolStripMenuItem;
        private ToolStripMenuItem minimalRectToolStripMenuItem;
        private ToolStripMenuItem inverseToolStripMenuItem;
        private ToolStripMenuItem verticalToolStripMenuItem;
        private ToolStripMenuItem horizontalToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem ditheringToolStripMenuItem;
        private ToolStripMenuItem customToolStripMenuItem;
        private ToolStripMenuItem editorToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton5;
        private ToolStripMenuItem backColorToolStripMenuItem;
        private ToolStripMenuItem blackToolStripMenuItem;
        private ToolStripMenuItem whiteToolStripMenuItem1;
        private ToolStripMenuItem customToolStripMenuItem1;
    }
}