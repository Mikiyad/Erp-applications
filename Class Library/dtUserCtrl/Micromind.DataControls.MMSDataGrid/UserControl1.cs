using Micromind.DataControls.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.MMSDataGrid
{
	public class UserControl1 : UserControl
	{
		private IContainer components;

		private PictureBox pictureBoxVoid;

		private PictureBox pictureBoxTotalLine;

		public UserControl1()
		{
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.MMSDataGrid.UserControl1));
			pictureBoxVoid = new System.Windows.Forms.PictureBox();
			pictureBoxTotalLine = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxTotalLine).BeginInit();
			SuspendLayout();
			pictureBoxVoid.Image = (System.Drawing.Image)resources.GetObject("pictureBoxVoid.Image");
			pictureBoxVoid.Location = new System.Drawing.Point(25, 50);
			pictureBoxVoid.Name = "pictureBoxVoid";
			pictureBoxVoid.Size = new System.Drawing.Size(100, 50);
			pictureBoxVoid.TabIndex = 2;
			pictureBoxVoid.TabStop = false;
			pictureBoxTotalLine.Image = Micromind.DataControls.Properties.Resources.GridCellDoubleLine;
			pictureBoxTotalLine.Location = new System.Drawing.Point(25, 50);
			pictureBoxTotalLine.Name = "pictureBoxTotalLine";
			pictureBoxTotalLine.Size = new System.Drawing.Size(100, 50);
			pictureBoxTotalLine.TabIndex = 1;
			pictureBoxTotalLine.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(pictureBoxVoid);
			base.Controls.Add(pictureBoxTotalLine);
			base.Name = "UserControl1";
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxTotalLine).EndInit();
			ResumeLayout(false);
		}
	}
}
