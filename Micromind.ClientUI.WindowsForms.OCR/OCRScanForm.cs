using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.OCR
{
	public class OCRScanForm : Form
	{
		public DocumentOCRForm fmMain;

		private IContainer components;

		private Button bkScan;

		private Button button2;

		private CheckBox cbNoUI;

		private ComboBox cbScanners;

		private Label label1;

		private Label label2;

		private ComboBox cbSrc;

		private Button bkSetDefault;

		public OCRScanForm()
		{
			InitializeComponent();
		}

		private void OCRScanForm_Load(object sender, EventArgs e)
		{
			cbSrc.SelectedIndex = 0;
			cbScanners.Items.Clear();
			int num = fmMain.NsOCR.Scan_Enumerate(fmMain.ScanObj, out string ScannerNames, 0);
			if (num > 1879048192)
			{
				fmMain.ShowError("Scan_Enumerate", num);
				return;
			}
			string text = "";
			for (int i = 0; i < ScannerNames.Length; i = checked(i + 1))
			{
				if (ScannerNames[i] != ',')
				{
					text += ScannerNames[i].ToString();
					continue;
				}
				cbScanners.Items.Add(text);
				text = "";
			}
			if (text != "")
			{
				cbScanners.Items.Add(text);
			}
			num = fmMain.NsOCR.Scan_Enumerate(fmMain.ScanObj, out ScannerNames, 1);
			if (num > 1879048192)
			{
				if (num != 1879048216)
				{
					fmMain.ShowError("Scan_Enumerate (1)", num);
				}
			}
			else if (num < cbScanners.Items.Count)
			{
				cbScanners.SelectedIndex = num;
			}
		}

		private void bkCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void bkSetDefault_Click(object sender, EventArgs e)
		{
			int selectedIndex = cbScanners.SelectedIndex;
			if (selectedIndex >= 0)
			{
				string ScannerNames;
				int num = fmMain.NsOCR.Scan_Enumerate(fmMain.ScanObj, out ScannerNames, 0x100 | selectedIndex);
				if (num > 1879048192)
				{
					fmMain.ShowError("Scan_Enumerate (2)", num);
				}
			}
		}

		private void bkScan_Click(object sender, EventArgs e)
		{
			int selectedIndex = cbScanners.SelectedIndex;
			if (selectedIndex < 0)
			{
				MessageBox.Show("No scanner selected!");
				return;
			}
			int num = 0;
			switch (cbSrc.SelectedIndex)
			{
			case 1:
				num |= 2;
				break;
			case 2:
				num |= 4;
				break;
			}
			if (cbNoUI.Checked)
			{
				num |= 1;
			}
			int num2 = fmMain.NsOCR.Scan_ScanToImg(fmMain.ScanObj, fmMain.ImgObj, selectedIndex, num);
			if (num2 > 1879048192)
			{
				fmMain.ShowError("Scan_ScanToImg", num2);
				return;
			}
			Close();
			fmMain.DoImageLoaded();
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
			bkScan = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			cbNoUI = new System.Windows.Forms.CheckBox();
			cbScanners = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			cbSrc = new System.Windows.Forms.ComboBox();
			bkSetDefault = new System.Windows.Forms.Button();
			SuspendLayout();
			bkScan.Location = new System.Drawing.Point(174, 236);
			bkScan.Name = "bkScan";
			bkScan.Size = new System.Drawing.Size(75, 23);
			bkScan.TabIndex = 0;
			bkScan.Text = "Scan";
			bkScan.UseVisualStyleBackColor = true;
			bkScan.Click += new System.EventHandler(bkScan_Click);
			button2.Location = new System.Drawing.Point(255, 236);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 1;
			button2.Text = "Cancel";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(bkCancel_Click);
			cbNoUI.AutoSize = true;
			cbNoUI.Location = new System.Drawing.Point(15, 138);
			cbNoUI.Name = "cbNoUI";
			cbNoUI.Size = new System.Drawing.Size(97, 17);
			cbNoUI.TabIndex = 2;
			cbNoUI.Text = "No Scanner UI";
			cbNoUI.UseVisualStyleBackColor = true;
			cbScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cbScanners.FormattingEnabled = true;
			cbScanners.Location = new System.Drawing.Point(114, 18);
			cbScanners.Name = "cbScanners";
			cbScanners.Size = new System.Drawing.Size(378, 21);
			cbScanners.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 21);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(77, 13);
			label1.TabIndex = 4;
			label1.Text = "Select Device:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 77);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(96, 13);
			label2.TabIndex = 6;
			label2.Text = "Document Source:";
			cbSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cbSrc.FormattingEnabled = true;
			cbSrc.Items.AddRange(new object[3]
			{
				"Flatbed",
				"ADF(Automatic Document Feeder)",
				"Auto"
			});
			cbSrc.Location = new System.Drawing.Point(114, 74);
			cbSrc.Name = "cbSrc";
			cbSrc.Size = new System.Drawing.Size(203, 21);
			cbSrc.TabIndex = 5;
			bkSetDefault.Location = new System.Drawing.Point(396, 49);
			bkSetDefault.Name = "bkSetDefault";
			bkSetDefault.Size = new System.Drawing.Size(96, 23);
			bkSetDefault.TabIndex = 7;
			bkSetDefault.Text = "Set as Default";
			bkSetDefault.UseVisualStyleBackColor = true;
			bkSetDefault.Click += new System.EventHandler(bkSetDefault_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(504, 268);
			base.Controls.Add(bkSetDefault);
			base.Controls.Add(label2);
			base.Controls.Add(cbSrc);
			base.Controls.Add(label1);
			base.Controls.Add(cbScanners);
			base.Controls.Add(cbNoUI);
			base.Controls.Add(button2);
			base.Controls.Add(bkScan);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OCRScanForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Scanning";
			base.Load += new System.EventHandler(OCRScanForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
