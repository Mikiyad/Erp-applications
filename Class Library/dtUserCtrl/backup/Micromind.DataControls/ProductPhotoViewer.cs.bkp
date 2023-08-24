using Infragistics.Win;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.DataControls.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductPhotoViewer : UserControl
	{
		private IContainer components;

		private PictureBox pictureBoxImage;

		private UltraButton buttonClose;

		private UltraButton buttonEnlarge;

		private PictureBox pictureBoxNoImage;

		public Image Image => pictureBoxImage.Image;

		public event EventHandler EnlargeRequested;

		public ProductPhotoViewer()
		{
			InitializeComponent();
			base.Visible = false;
			base.Leave += ProductPhotoViewer_Leave;
			base.LostFocus += ProductPhotoViewer_LostFocus;
		}

		private void ProductPhotoViewer_LostFocus(object sender, EventArgs e)
		{
			base.Visible = false;
		}

		private void ProductPhotoViewer_Leave(object sender, EventArgs e)
		{
			base.Visible = false;
		}

		private void pictureBoxImage_Click(object sender, EventArgs e)
		{
			base.Visible = false;
		}

		public void ShowImage(string productID, int x, int y)
		{
			try
			{
				Image productThumbnailImage = PublicFunctions.GetProductThumbnailImage(productID, isProductParentID: false);
				if (productThumbnailImage != null)
				{
					pictureBoxImage.Image = productThumbnailImage;
					buttonEnlarge.Enabled = true;
				}
				else
				{
					pictureBoxImage.Image = pictureBoxNoImage.Image;
					buttonEnlarge.Enabled = false;
				}
				base.Left = x;
				base.Top = y;
				base.Visible = true;
				BringToFront();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonEnlarge_Click(object sender, EventArgs e)
		{
			if (this.EnlargeRequested != null)
			{
				this.EnlargeRequested(pictureBoxImage.Image, null);
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.Visible = false;
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			buttonClose = new Infragistics.Win.Misc.UltraButton();
			buttonEnlarge = new Infragistics.Win.Misc.UltraButton();
			pictureBoxImage = new System.Windows.Forms.PictureBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			SuspendLayout();
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance.ImageBackground = Micromind.DataControls.Properties.Resources.close;
			appearance.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Centered;
			buttonClose.Appearance = appearance;
			buttonClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupSoftBorderless;
			buttonClose.Location = new System.Drawing.Point(165, -1);
			buttonClose.Name = "buttonClose";
			buttonClose.ShowFocusRect = false;
			buttonClose.ShowOutline = false;
			buttonClose.Size = new System.Drawing.Size(20, 19);
			buttonClose.TabIndex = 1;
			buttonClose.TabStop = false;
			buttonClose.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			appearance2.ImageBackground = Micromind.DataControls.Properties.Resources.enlarge;
			appearance2.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Centered;
			buttonEnlarge.Appearance = appearance2;
			buttonEnlarge.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupSoftBorderless;
			buttonEnlarge.Location = new System.Drawing.Point(0, -1);
			buttonEnlarge.Name = "buttonEnlarge";
			buttonEnlarge.ShowFocusRect = false;
			buttonEnlarge.ShowOutline = false;
			buttonEnlarge.Size = new System.Drawing.Size(26, 20);
			buttonEnlarge.TabIndex = 2;
			buttonEnlarge.TabStop = false;
			buttonEnlarge.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			buttonEnlarge.Click += new System.EventHandler(buttonEnlarge_Click);
			pictureBoxImage.BackColor = System.Drawing.Color.White;
			pictureBoxImage.Image = Micromind.DataControls.Properties.Resources.noimage;
			pictureBoxImage.Location = new System.Drawing.Point(0, 18);
			pictureBoxImage.Name = "pictureBoxImage";
			pictureBoxImage.Size = new System.Drawing.Size(186, 144);
			pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxImage.TabIndex = 0;
			pictureBoxImage.TabStop = false;
			pictureBoxImage.Click += new System.EventHandler(pictureBoxImage_Click);
			pictureBoxNoImage.BackColor = System.Drawing.Color.White;
			pictureBoxNoImage.Image = Micromind.DataControls.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(16, 24);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(36, 28);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 3;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(buttonEnlarge);
			base.Controls.Add(buttonClose);
			base.Controls.Add(pictureBoxImage);
			base.Controls.Add(pictureBoxNoImage);
			MaximumSize = new System.Drawing.Size(186, 162);
			MinimumSize = new System.Drawing.Size(186, 162);
			base.Name = "ProductPhotoViewer";
			base.Size = new System.Drawing.Size(184, 160);
			((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			ResumeLayout(false);
		}
	}
}
