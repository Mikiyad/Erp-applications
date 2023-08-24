using Micromind.DataControls.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.FlatDashboard
{
	public class GadgetIcon : UserControl
	{
		private GroupLayout layout;

		private IContainer components;

		private Label label1;

		private PictureBox pictureBox1;

		public string Title
		{
			get
			{
				return label1.Text;
			}
			set
			{
				label1.Text = value;
			}
		}

		public string Description
		{
			get
			{
				if (label1.Tag == null)
				{
					return "";
				}
				return label1.Tag.ToString();
			}
			set
			{
				label1.Tag = value;
			}
		}

		public GroupLayout GroupLayout
		{
			get
			{
				return layout;
			}
			set
			{
				layout = value;
			}
		}

		public GadgetIcon()
		{
			InitializeComponent();
			pictureBox1.DoubleClick += PictureBox1_DoubleClick;
			label1.DoubleClick += Label1_DoubleClick;
		}

		private void Label1_DoubleClick(object sender, EventArgs e)
		{
			OnDoubleClick(e);
		}

		private void PictureBox1_DoubleClick(object sender, EventArgs e)
		{
			OnDoubleClick(e);
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
			label1 = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			label1.Location = new System.Drawing.Point(3, 3);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(191, 15);
			label1.TabIndex = 0;
			label1.Text = "Title";
			pictureBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			pictureBox1.Image = Micromind.DataControls.Properties.Resources.ExportXML;
			pictureBox1.Location = new System.Drawing.Point(0, 23);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(218, 130);
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.Controls.Add(pictureBox1);
			base.Controls.Add(label1);
			base.Name = "GadgetIcon";
			base.Size = new System.Drawing.Size(218, 154);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
