using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class test : Form
	{
		private LinkLabel linkLabelSendToMSOutlook;

		private Container components;

		public test()
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
			linkLabelSendToMSOutlook = new System.Windows.Forms.LinkLabel();
			SuspendLayout();
			linkLabelSendToMSOutlook.ActiveLinkColor = System.Drawing.Color.DarkSlateGray;
			linkLabelSendToMSOutlook.AutoSize = true;
			linkLabelSendToMSOutlook.BackColor = System.Drawing.Color.Transparent;
			linkLabelSendToMSOutlook.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			linkLabelSendToMSOutlook.LinkArea = new System.Windows.Forms.LinkArea(0, 19);
			linkLabelSendToMSOutlook.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			linkLabelSendToMSOutlook.LinkColor = System.Drawing.Color.Black;
			linkLabelSendToMSOutlook.Location = new System.Drawing.Point(245, 270);
			linkLabelSendToMSOutlook.Name = "linkLabelSendToMSOutlook";
			linkLabelSendToMSOutlook.Size = new System.Drawing.Size(111, 16);
			linkLabelSendToMSOutlook.TabIndex = 318;
			linkLabelSendToMSOutlook.TabStop = true;
			linkLabelSendToMSOutlook.Text = "Export to MS Outlook";
			linkLabelSendToMSOutlook.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelSendToMSOutlook_LinkClicked);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(600, 557);
			base.Controls.Add(linkLabelSendToMSOutlook);
			base.Name = "test";
			Text = "test";
			base.Load += new System.EventHandler(test_Load);
			ResumeLayout(false);
		}

		private void linkLabelSendToMSOutlook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void test_Load(object sender, EventArgs e)
		{
		}
	}
}
