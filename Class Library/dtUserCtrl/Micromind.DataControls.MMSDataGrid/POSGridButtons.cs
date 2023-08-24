using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.MMSDataGrid
{
	public class POSGridButtons : UserControl
	{
		private IContainer components;

		private Button button1;

		private Button button2;

		private Button button3;

		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		public POSGridButtons()
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
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			SuspendLayout();
			button1.Location = new System.Drawing.Point(12, 1);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(73, 43);
			button1.TabIndex = 0;
			button1.Text = "Remove";
			button1.UseVisualStyleBackColor = true;
			button2.Location = new System.Drawing.Point(111, 1);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(73, 43);
			button2.TabIndex = 0;
			button2.Text = "Details";
			button2.UseVisualStyleBackColor = true;
			button3.Location = new System.Drawing.Point(212, 1);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(73, 43);
			button3.TabIndex = 0;
			button3.Text = "Edit QTY...";
			button3.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(139, 211, 216);
			base.Controls.Add(button3);
			base.Controls.Add(button2);
			base.Controls.Add(button1);
			base.Name = "POSGridButtons";
			base.Size = new System.Drawing.Size(549, 47);
			ResumeLayout(false);
		}
	}
}
