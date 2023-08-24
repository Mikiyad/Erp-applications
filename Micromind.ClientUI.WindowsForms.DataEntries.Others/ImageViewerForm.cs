using Infragistics.Win.UltraWinCalcManager;
using Micromind.ClientUI.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ImageViewerForm : Form
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButton2;

		private UltraCalcManager ultraCalcManager1;

		private PictureBox pictureBoxImage;

		private PictureBox pictureBoxNoImage;

		public Image Image
		{
			get
			{
				return pictureBoxImage.Image;
			}
			set
			{
				pictureBoxImage.Image = value;
			}
		}

		public ImageViewerForm()
		{
			InitializeComponent();
			base.Load += ImageViewerForm_Load;
		}

		private void ImageViewerForm_Load(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ImageViewerForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			pictureBoxImage = new System.Windows.Forms.PictureBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			SuspendLayout();
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				toolStripButton1,
				toolStripButton2
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(763, 25);
			toolStrip1.TabIndex = 2;
			toolStrip1.Text = "toolStrip1";
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(23, 22);
			toolStripButton1.Text = "toolStripButton1";
			toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
			toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(23, 22);
			toolStripButton2.Text = "toolStripButton2";
			ultraCalcManager1.ContainingControl = this;
			pictureBoxImage.BackColor = System.Drawing.Color.White;
			pictureBoxImage.Dock = System.Windows.Forms.DockStyle.Fill;
			pictureBoxImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxImage.Location = new System.Drawing.Point(0, 25);
			pictureBoxImage.Name = "pictureBoxImage";
			pictureBoxImage.Size = new System.Drawing.Size(763, 507);
			pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			pictureBoxImage.TabIndex = 3;
			pictureBoxImage.TabStop = false;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(44, 271);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(140, 60);
			pictureBoxNoImage.TabIndex = 4;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(763, 532);
			base.Controls.Add(pictureBoxImage);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(pictureBoxNoImage);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ImageViewerForm";
			Text = "Image Viewer";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
