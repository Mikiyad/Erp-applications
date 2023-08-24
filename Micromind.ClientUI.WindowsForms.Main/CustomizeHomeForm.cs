using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class CustomizeHomeForm : XtraForm
	{
		private IContainer components;

		private ListBox listBox1;

		public CustomizeHomeForm()
		{
			InitializeComponent();
		}

		private void CustomizeHomeForm_Load(object sender, EventArgs e)
		{
		}

		public void LoadData(NavBarControl leftPanel, NavBarControl rightPanel)
		{
			foreach (NavBarGroup group in leftPanel.Groups)
			{
				listBox1.Items.Add(group.Caption);
			}
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
			listBox1 = new System.Windows.Forms.ListBox();
			SuspendLayout();
			listBox1.FormattingEnabled = true;
			listBox1.Location = new System.Drawing.Point(12, 27);
			listBox1.Name = "listBox1";
			listBox1.Size = new System.Drawing.Size(207, 368);
			listBox1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(539, 409);
			base.Controls.Add(listBox1);
			base.Name = "CustomizeHomeForm";
			Text = "CustomizeHomeForm";
			base.Load += new System.EventHandler(CustomizeHomeForm_Load);
			ResumeLayout(false);
		}
	}
}
