using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class DeleteMatrixDialog : Form
	{
		private IContainer components;

		private PictureBox pictureBoxIcon;

		private Panel panel1;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private Label label1;

		public DeleteMatrixDialog()
		{
			InitializeComponent();
			pictureBoxIcon.Image = SystemIcons.Question.ToBitmap();
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
			pictureBoxIcon = new System.Windows.Forms.PictureBox();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			radioButton1 = new System.Windows.Forms.RadioButton();
			radioButton2 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			pictureBoxIcon.Location = new System.Drawing.Point(12, 12);
			pictureBoxIcon.Name = "pictureBoxIcon";
			pictureBoxIcon.Size = new System.Drawing.Size(44, 39);
			pictureBoxIcon.TabIndex = 0;
			pictureBoxIcon.TabStop = false;
			panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			panel1.Controls.Add(radioButton2);
			panel1.Controls.Add(radioButton1);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(pictureBoxIcon);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(374, 130);
			panel1.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(62, 25);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(85, 13);
			label1.TabIndex = 1;
			label1.Text = "Do you want to :";
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Location = new System.Drawing.Point(83, 49);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(139, 17);
			radioButton1.TabIndex = 2;
			radioButton1.TabStop = true;
			radioButton1.Text = "Delete the Components ";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton2.AutoSize = true;
			radioButton2.Location = new System.Drawing.Point(83, 74);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(130, 17);
			radioButton2.TabIndex = 2;
			radioButton2.Text = "Keep the Components";
			radioButton2.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(374, 165);
			base.Controls.Add(panel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DeleteMatrixDialog";
			Text = "Delete Matrix Components";
			((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
