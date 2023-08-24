using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Centers
{
	public class AccountCenterForm : Form, IForm
	{
		private IContainer components;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1033;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public AccountCenterForm(Form parent)
		{
			base.MdiParent = parent;
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
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(67, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(152, 24);
			label1.TabIndex = 0;
			label1.Text = "Account Center";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoScroll = true;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(440, 238);
			base.ControlBox = false;
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AccountCenterForm";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
