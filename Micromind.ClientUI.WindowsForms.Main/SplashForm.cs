using DevExpress.Utils;
using DevExpress.XtraEditors;
using Micromind.ClientLibraries;
using Micromind.Securities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class SplashForm : Form
	{
		private bool isTrial;

		private PictureBox pictureBox1;

		private LabelControl labelWarning;

		private Panel panel1;

		private LabelControl labelEdition;

		private LabelControl labelTrial;

		private IContainer components;

		public SplashForm(bool isTrial)
		{
			InitializeComponent();
			this.isTrial = isTrial;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Main.SplashForm));
			pictureBox1 = new System.Windows.Forms.PictureBox();
			labelWarning = new DevExpress.XtraEditors.LabelControl();
			panel1 = new System.Windows.Forms.Panel();
			labelEdition = new DevExpress.XtraEditors.LabelControl();
			labelTrial = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(-10, 2);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(11, 23);
			pictureBox1.TabIndex = 13;
			pictureBox1.TabStop = false;
			labelWarning.Appearance.Options.UseTextOptions = true;
			labelWarning.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			labelWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			labelWarning.LineVisible = true;
			labelWarning.Location = new System.Drawing.Point(43, 290);
			labelWarning.Name = "labelWarning";
			labelWarning.Size = new System.Drawing.Size(476, 54);
			labelWarning.TabIndex = 14;
			labelWarning.Text = resources.GetString("labelWarning.Text");
			panel1.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel1.BackgroundImage");
			panel1.Controls.Add(labelEdition);
			panel1.Controls.Add(labelTrial);
			panel1.Controls.Add(labelWarning);
			panel1.Location = new System.Drawing.Point(12, 12);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(671, 412);
			panel1.TabIndex = 15;
			labelEdition.Appearance.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelEdition.Appearance.ForeColor = System.Drawing.Color.FromArgb(21, 65, 113);
			labelEdition.Appearance.Options.UseFont = true;
			labelEdition.Appearance.Options.UseForeColor = true;
			labelEdition.Location = new System.Drawing.Point(45, 201);
			labelEdition.Name = "labelEdition";
			labelEdition.Size = new System.Drawing.Size(0, 15);
			labelEdition.TabIndex = 16;
			labelTrial.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelTrial.Appearance.Options.UseFont = true;
			labelTrial.Location = new System.Drawing.Point(43, 222);
			labelTrial.Name = "labelTrial";
			labelTrial.Size = new System.Drawing.Size(104, 13);
			labelTrial.TabIndex = 15;
			labelTrial.Text = "Evaluation Version";
			labelTrial.Visible = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(655, 399);
			base.Controls.Add(panel1);
			base.Controls.Add(pictureBox1);
			DoubleBuffered = true;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SplashForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Splash ";
			base.TransparencyKey = System.Drawing.Color.White;
			base.Load += new System.EventHandler(SplashForm_Load);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}

		private void OnConnecting(object o, EventArgs e)
		{
			Application.DoEvents();
			try
			{
				Application.DoEvents();
			}
			catch
			{
			}
		}

		private void DisplayWarning()
		{
			string str = "Warning: " + base.ProductName + " is registered ";
			str += "and is protected by copyright law. Therefore, unauthorized reproduction, duplication, or distribution of this program";
			str += " will result in civil and criminal penalties, and will be prosecuted under the law.";
			labelWarning.Text = str;
		}

		private void Init()
		{
			DisplayWarning();
			if (GlobalRules.Edition == Editions.Professional)
			{
				if (GlobalRules.IsMultiUser)
				{
					labelEdition.Text = "Professional Edition: Multi-User";
				}
				else
				{
					labelEdition.Text = "Professional Edition";
				}
			}
			else if (GlobalRules.Edition == Editions.Basic)
			{
				if (GlobalRules.IsMultiUser)
				{
					labelEdition.Text = "Basic Edition: Multi-User";
				}
				else
				{
					labelEdition.Text = "Basic Edition";
				}
			}
			labelEdition.BringToFront();
			Application.DoEvents();
			if (!isTrial)
			{
				labelTrial.Visible = false;
			}
			labelTrial.BringToFront();
			Application.DoEvents();
		}

		private void SplashForm_Load(object sender, EventArgs e)
		{
			Init();
		}

		private void label2_Click(object sender, EventArgs e)
		{
		}
	}
}
