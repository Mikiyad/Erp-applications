using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class HelpSupportForm : Form, IDataForm
	{
		private Panel panel1;

		private Line line;

		private Container components;

		public HelpSupportForm(Form parent)
		{
			InitializeComponent();
			base.MdiParent = parent;
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
			panel1 = new System.Windows.Forms.Panel();
			line = new Micromind.UISupport.Line();
			panel1.SuspendLayout();
			SuspendLayout();
			panel1.AutoScroll = true;
			panel1.Controls.Add(line);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(568, 504);
			panel1.TabIndex = 0;
			panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
			line.BackColor = System.Drawing.Color.LightGray;
			line.DrawWidth = 2;
			line.IsVertical = false;
			line.LineBackColor = System.Drawing.Color.Black;
			line.Location = new System.Drawing.Point(8, 408);
			line.Name = "line";
			line.Size = new System.Drawing.Size(552, 2);
			line.TabIndex = 36;
			line.TabStop = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(568, 504);
			base.Controls.Add(panel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "HelpSupportForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Help && Support Center";
			base.Load += new System.EventHandler(HelpSupportForm_Load);
			base.Resize += new System.EventHandler(HelpSupportForm_Resize);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		private void linkLabelSupportEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void linkLabelInfoEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void linkLabelSuggestionsEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void HelpSupportForm_Load(object sender, EventArgs e)
		{
		}

		public void OnActivated()
		{
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.General;
		}

		public static int GetScreenID()
		{
			return 1;
		}

		private void HelpSupportForm_Resize(object sender, EventArgs e)
		{
		}

		public void RefreshData()
		{
		}

		private void linkLabelInstantHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Navigator.GoHelp();
		}
	}
}
