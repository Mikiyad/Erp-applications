using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.OCR
{
	public class OCRProgressForm : Form
	{
		public DocumentOCRForm fmMain;

		public int res;

		public int mode;

		private IContainer components;

		internal Label Label1;

		internal ProgressBar ProgressBar1;

		internal Button Button1;

		internal System.Windows.Forms.Timer Timer1;

		public OCRProgressForm()
		{
			InitializeComponent();
		}

		private bool CheckDone()
		{
			if (mode == 0)
			{
				res = fmMain.NsOCR.Img_OCR(fmMain.ImgObj, 0, 0, 2);
			}
			else
			{
				res = fmMain.NsOCR.Ocr_ProcessPages(fmMain.ImgObj, 0, 0, 0, 0, 2);
			}
			return res != 1879048219;
		}

		private void OCRProgressForm_Load(object sender, EventArgs e)
		{
			Timer1.Enabled = true;
			ProgressBar1.Value = 0;
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			if (CheckDone())
			{
				Close();
			}
			int num = (mode != 0) ? fmMain.NsOCR.Ocr_ProcessPages(fmMain.ImgObj, 0, 0, 0, 0, 3) : fmMain.NsOCR.Img_OCR(fmMain.ImgObj, 0, 0, 3);
			if (num < 1879048192 && ProgressBar1.Value != num)
			{
				ProgressBar1.Value = num;
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			if (mode == 0)
			{
				fmMain.NsOCR.Img_OCR(fmMain.ImgObj, 0, 0, 4);
			}
			else
			{
				fmMain.NsOCR.Ocr_ProcessPages(fmMain.ImgObj, 0, 0, 0, 0, 4);
			}
			Close();
		}

		private void OCRProgressForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Timer1.Enabled = false;
			while (!CheckDone())
			{
				Thread.Sleep(10);
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.OCR.OCRProgressForm));
			Label1 = new System.Windows.Forms.Label();
			ProgressBar1 = new System.Windows.Forms.ProgressBar();
			Button1 = new System.Windows.Forms.Button();
			Timer1 = new System.Windows.Forms.Timer(components);
			SuspendLayout();
			Label1.AutoSize = true;
			Label1.Location = new System.Drawing.Point(183, 16);
			Label1.Name = "Label1";
			Label1.Size = new System.Drawing.Size(70, 13);
			Label1.TabIndex = 5;
			Label1.Text = "Please wait...";
			ProgressBar1.Location = new System.Drawing.Point(12, 53);
			ProgressBar1.Name = "ProgressBar1";
			ProgressBar1.Size = new System.Drawing.Size(402, 23);
			ProgressBar1.TabIndex = 4;
			Button1.Location = new System.Drawing.Point(178, 106);
			Button1.Name = "Button1";
			Button1.Size = new System.Drawing.Size(75, 23);
			Button1.TabIndex = 3;
			Button1.Text = "Cancel";
			Button1.UseVisualStyleBackColor = true;
			Button1.Click += new System.EventHandler(Button1_Click);
			Timer1.Tick += new System.EventHandler(Timer1_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(425, 136);
			base.Controls.Add(Label1);
			base.Controls.Add(ProgressBar1);
			base.Controls.Add(Button1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OCRProgressForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Progress";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(OCRProgressForm_FormClosed);
			base.Load += new System.EventHandler(OCRProgressForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
