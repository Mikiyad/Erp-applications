using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.OCR
{
	public class OCROptionsForm : Form
	{
		public DocumentOCRForm fmMain;

		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private Button bkOK;

		private Button bkCancel;

		private CheckBox cbFindBarcodes;

		private CheckBox cbImgInversion;

		private CheckBox cbZonesInversion;

		private CheckBox cbDeskew;

		private CheckBox cbRotation;

		private CheckBox cbImgNoiseFilter;

		private CheckBox cbRemoveLines;

		private CheckBox cbGrayMode;

		private CheckBox cbFastMode;

		private CheckBox cbBinTwice;

		private Label label4;

		private TextBox edEnabledChars;

		private TextBox edDisabledChars;

		private Label label5;

		private TextBox edTextQual;

		private Label label6;

		private TextBox edBinThreshold;

		private Label label7;

		private TextBox edPDFDPI;

		private Label label8;

		private Button bkHelp;

		private CheckBox cbCorrectMixed;

		private CheckBox cbDictionary;

		private CheckBox cbOneColumn;

		public OCROptionsForm()
		{
			InitializeComponent();
		}

		private void bkCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OCROptionsForm_Load(object sender, EventArgs e)
		{
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Zoning/FindBarcodes", out string OptionValue);
			cbFindBarcodes.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "ImgAlizer/Inversion", out OptionValue);
			cbImgInversion.Checked = (OptionValue == "2");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Zoning/DetectInversion", out OptionValue);
			cbZonesInversion.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "ImgAlizer/SkewAngle", out OptionValue);
			cbDeskew.Checked = (OptionValue == "360");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "ImgAlizer/AutoRotate", out OptionValue);
			cbRotation.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "ImgAlizer/NoiseFilter", out OptionValue);
			cbImgNoiseFilter.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "PixLines/RemoveLines", out OptionValue);
			cbRemoveLines.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Main/GrayMode", out OptionValue);
			cbGrayMode.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Main/FastMode", out OptionValue);
			cbFastMode.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Binarizer/BinTwice", out OptionValue);
			cbBinTwice.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "WordAlizer/CorrectMixed", out OptionValue);
			cbCorrectMixed.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Dictionaries/UseDictionary", out OptionValue);
			cbDictionary.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Zoning/OneColumn", out OptionValue);
			cbOneColumn.Checked = (OptionValue == "1");
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Main/EnabledChars", out OptionValue);
			edEnabledChars.Text = OptionValue;
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Main/DisabledChars", out OptionValue);
			edDisabledChars.Text = OptionValue;
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Binarizer/SimpleThr", out OptionValue);
			edBinThreshold.Text = OptionValue;
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "WordAlizer/TextQual", out OptionValue);
			edTextQual.Text = OptionValue;
			fmMain.NsOCR.Cfg_GetOption(fmMain.CfgObj, 0, "Main/PdfDPI", out OptionValue);
			edPDFDPI.Text = OptionValue;
		}

		private void bkOK_Click(object sender, EventArgs e)
		{
			string optionValue = cbFindBarcodes.Checked ? "1" : "0";
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Zoning/FindBarcodes", optionValue);
			optionValue = (cbImgInversion.Checked ? "2" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "ImgAlizer/Inversion", optionValue);
			optionValue = (cbZonesInversion.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Zoning/DetectInversion", optionValue);
			optionValue = (cbDeskew.Checked ? "360" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "ImgAlizer/SkewAngle", optionValue);
			optionValue = (cbRotation.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "ImgAlizer/AutoRotate", optionValue);
			optionValue = (cbImgNoiseFilter.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "ImgAlizer/NoiseFilter", optionValue);
			optionValue = (cbRemoveLines.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "PixLines/RemoveLines", optionValue);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "PixLines/FindHorLines", optionValue);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "PixLines/FindVerLines", optionValue);
			optionValue = (cbGrayMode.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Main/GrayMode", optionValue);
			optionValue = (cbFastMode.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Main/FastMode", optionValue);
			optionValue = (cbBinTwice.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Binarizer/BinTwice", optionValue);
			optionValue = (cbCorrectMixed.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "WordAlizer/CorrectMixed", optionValue);
			optionValue = (cbDictionary.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Dictionaries/UseDictionary", optionValue);
			optionValue = (cbOneColumn.Checked ? "1" : "0");
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Zoning/OneColumn", optionValue);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Main/EnabledChars", edEnabledChars.Text);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Main/DisabledChars", edDisabledChars.Text);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Binarizer/SimpleThr", edBinThreshold.Text);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "WordAlizer/TextQual", edTextQual.Text);
			fmMain.NsOCR.Cfg_SetOption(fmMain.CfgObj, 0, "Main/PdfDPI", edPDFDPI.Text);
			Close();
		}

		private void bkHelp_Click(object sender, EventArgs e)
		{
			fmMain.ShowHelp("config.htm");
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
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			bkOK = new System.Windows.Forms.Button();
			bkCancel = new System.Windows.Forms.Button();
			cbFindBarcodes = new System.Windows.Forms.CheckBox();
			cbImgInversion = new System.Windows.Forms.CheckBox();
			cbZonesInversion = new System.Windows.Forms.CheckBox();
			cbDeskew = new System.Windows.Forms.CheckBox();
			cbRotation = new System.Windows.Forms.CheckBox();
			cbImgNoiseFilter = new System.Windows.Forms.CheckBox();
			cbRemoveLines = new System.Windows.Forms.CheckBox();
			cbGrayMode = new System.Windows.Forms.CheckBox();
			cbFastMode = new System.Windows.Forms.CheckBox();
			cbBinTwice = new System.Windows.Forms.CheckBox();
			label4 = new System.Windows.Forms.Label();
			edEnabledChars = new System.Windows.Forms.TextBox();
			edDisabledChars = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			edTextQual = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			edBinThreshold = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			edPDFDPI = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			bkHelp = new System.Windows.Forms.Button();
			cbCorrectMixed = new System.Windows.Forms.CheckBox();
			cbDictionary = new System.Windows.Forms.CheckBox();
			cbOneColumn = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			label1.AutoSize = true;
			label1.ForeColor = System.Drawing.Color.Red;
			label1.Location = new System.Drawing.Point(12, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(406, 13);
			label1.TabIndex = 0;
			label1.Text = "You can improve OCR performance and accuracy by changing default configuration.";
			label2.AutoSize = true;
			label2.ForeColor = System.Drawing.Color.Red;
			label2.Location = new System.Drawing.Point(12, 31);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(438, 13);
			label2.TabIndex = 1;
			label2.Text = "Check \"Configuration\" and \"Performance Tips\" sections of documentation for more options.";
			label3.AutoSize = true;
			label3.ForeColor = System.Drawing.Color.Red;
			label3.Location = new System.Drawing.Point(14, 519);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(420, 13);
			label3.TabIndex = 2;
			label3.Text = "NOTE: Some options will not be applied  immediately, reload image to apply all changes.";
			bkOK.Location = new System.Drawing.Point(149, 545);
			bkOK.Name = "bkOK";
			bkOK.Size = new System.Drawing.Size(75, 27);
			bkOK.TabIndex = 3;
			bkOK.Text = "OK";
			bkOK.UseVisualStyleBackColor = true;
			bkOK.Click += new System.EventHandler(bkOK_Click);
			bkCancel.Location = new System.Drawing.Point(230, 545);
			bkCancel.Name = "bkCancel";
			bkCancel.Size = new System.Drawing.Size(75, 27);
			bkCancel.TabIndex = 4;
			bkCancel.Text = "Cancel";
			bkCancel.UseVisualStyleBackColor = true;
			bkCancel.Click += new System.EventHandler(bkCancel_Click);
			cbFindBarcodes.AutoSize = true;
			cbFindBarcodes.Location = new System.Drawing.Point(15, 69);
			cbFindBarcodes.Name = "cbFindBarcodes";
			cbFindBarcodes.Size = new System.Drawing.Size(93, 17);
			cbFindBarcodes.TabIndex = 5;
			cbFindBarcodes.Text = "Find barcodes";
			cbFindBarcodes.UseVisualStyleBackColor = true;
			cbImgInversion.AutoSize = true;
			cbImgInversion.Location = new System.Drawing.Point(15, 92);
			cbImgInversion.Name = "cbImgInversion";
			cbImgInversion.Size = new System.Drawing.Size(134, 17);
			cbImgInversion.TabIndex = 6;
			cbImgInversion.Text = "Detect image inversion";
			cbImgInversion.UseVisualStyleBackColor = true;
			cbZonesInversion.AutoSize = true;
			cbZonesInversion.Location = new System.Drawing.Point(15, 115);
			cbZonesInversion.Name = "cbZonesInversion";
			cbZonesInversion.Size = new System.Drawing.Size(134, 17);
			cbZonesInversion.TabIndex = 7;
			cbZonesInversion.Text = "Detect zones inversion";
			cbZonesInversion.UseVisualStyleBackColor = true;
			cbDeskew.AutoSize = true;
			cbDeskew.Location = new System.Drawing.Point(15, 138);
			cbDeskew.Name = "cbDeskew";
			cbDeskew.Size = new System.Drawing.Size(151, 17);
			cbDeskew.TabIndex = 8;
			cbDeskew.Text = "Detect and fix image skew";
			cbDeskew.UseVisualStyleBackColor = true;
			cbRotation.AutoSize = true;
			cbRotation.Location = new System.Drawing.Point(15, 161);
			cbRotation.Name = "cbRotation";
			cbRotation.Size = new System.Drawing.Size(263, 17);
			cbRotation.TabIndex = 9;
			cbRotation.Text = "Detect and fix image rotation 90/180/270 degrees";
			cbRotation.UseVisualStyleBackColor = true;
			cbImgNoiseFilter.AutoSize = true;
			cbImgNoiseFilter.Location = new System.Drawing.Point(15, 184);
			cbImgNoiseFilter.Name = "cbImgNoiseFilter";
			cbImgNoiseFilter.Size = new System.Drawing.Size(148, 17);
			cbImgNoiseFilter.TabIndex = 10;
			cbImgNoiseFilter.Text = "Apply noise filter for image";
			cbImgNoiseFilter.UseVisualStyleBackColor = true;
			cbRemoveLines.AutoSize = true;
			cbRemoveLines.Location = new System.Drawing.Point(15, 207);
			cbRemoveLines.Name = "cbRemoveLines";
			cbRemoveLines.Size = new System.Drawing.Size(141, 17);
			cbRemoveLines.TabIndex = 11;
			cbRemoveLines.Text = "Detect and remove lines";
			cbRemoveLines.UseVisualStyleBackColor = true;
			cbGrayMode.AutoSize = true;
			cbGrayMode.Location = new System.Drawing.Point(15, 230);
			cbGrayMode.Name = "cbGrayMode";
			cbGrayMode.Size = new System.Drawing.Size(77, 17);
			cbGrayMode.TabIndex = 12;
			cbGrayMode.Text = "Gray mode";
			cbGrayMode.UseVisualStyleBackColor = true;
			cbFastMode.AutoSize = true;
			cbFastMode.Location = new System.Drawing.Point(15, 253);
			cbFastMode.Name = "cbFastMode";
			cbFastMode.Size = new System.Drawing.Size(147, 17);
			cbFastMode.TabIndex = 13;
			cbFastMode.Text = "Fast mode (less accurate)";
			cbFastMode.UseVisualStyleBackColor = true;
			cbBinTwice.AutoSize = true;
			cbBinTwice.Location = new System.Drawing.Point(15, 276);
			cbBinTwice.Name = "cbBinTwice";
			cbBinTwice.Size = new System.Drawing.Size(91, 17);
			cbBinTwice.TabIndex = 14;
			cbBinTwice.Text = "Binarize twice";
			cbBinTwice.UseVisualStyleBackColor = true;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(14, 377);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(78, 13);
			label4.TabIndex = 15;
			label4.Text = "Enabled chars:";
			edEnabledChars.Location = new System.Drawing.Point(98, 370);
			edEnabledChars.Name = "edEnabledChars";
			edEnabledChars.Size = new System.Drawing.Size(320, 20);
			edEnabledChars.TabIndex = 16;
			edDisabledChars.Location = new System.Drawing.Point(98, 396);
			edDisabledChars.Name = "edDisabledChars";
			edDisabledChars.Size = new System.Drawing.Size(320, 20);
			edDisabledChars.TabIndex = 18;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(14, 403);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(80, 13);
			label5.TabIndex = 17;
			label5.Text = "Disabled chars:";
			edTextQual.Location = new System.Drawing.Point(223, 452);
			edTextQual.Name = "edTextQual";
			edTextQual.Size = new System.Drawing.Size(53, 20);
			edTextQual.TabIndex = 22;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(14, 455);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(148, 13);
			label6.TabIndex = 21;
			label6.Text = "Text quality (0..100; -1 - auto):";
			edBinThreshold.Location = new System.Drawing.Point(223, 426);
			edBinThreshold.Name = "edBinThreshold";
			edBinThreshold.Size = new System.Drawing.Size(53, 20);
			edBinThreshold.TabIndex = 20;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(14, 429);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(203, 13);
			label7.TabIndex = 19;
			label7.Text = "Binarization threshold (0..254; 255 - auto):";
			edPDFDPI.Location = new System.Drawing.Point(223, 478);
			edPDFDPI.Name = "edPDFDPI";
			edPDFDPI.Size = new System.Drawing.Size(53, 20);
			edPDFDPI.TabIndex = 24;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(14, 481);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(99, 13);
			label8.TabIndex = 23;
			label8.Text = "PDF rendering DPI:";
			bkHelp.Location = new System.Drawing.Point(394, 545);
			bkHelp.Name = "bkHelp";
			bkHelp.Size = new System.Drawing.Size(75, 27);
			bkHelp.TabIndex = 25;
			bkHelp.Text = "Help";
			bkHelp.UseVisualStyleBackColor = true;
			bkHelp.Click += new System.EventHandler(bkHelp_Click);
			cbCorrectMixed.AutoSize = true;
			cbCorrectMixed.Location = new System.Drawing.Point(15, 299);
			cbCorrectMixed.Name = "cbCorrectMixed";
			cbCorrectMixed.Size = new System.Drawing.Size(269, 17);
			cbCorrectMixed.TabIndex = 26;
			cbCorrectMixed.Text = "Correct mixed chars (letters and digits in same word)";
			cbCorrectMixed.UseVisualStyleBackColor = true;
			cbDictionary.AutoSize = true;
			cbDictionary.Location = new System.Drawing.Point(15, 322);
			cbDictionary.Name = "cbDictionary";
			cbDictionary.Size = new System.Drawing.Size(93, 17);
			cbDictionary.TabIndex = 27;
			cbDictionary.Text = "Use dictionary";
			cbDictionary.UseVisualStyleBackColor = true;
			cbOneColumn.AutoSize = true;
			cbOneColumn.Location = new System.Drawing.Point(15, 345);
			cbOneColumn.Name = "cbOneColumn";
			cbOneColumn.Size = new System.Drawing.Size(153, 17);
			cbOneColumn.TabIndex = 28;
			cbOneColumn.Text = "Combine zones horizontally";
			cbOneColumn.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(479, 584);
			base.Controls.Add(cbOneColumn);
			base.Controls.Add(cbDictionary);
			base.Controls.Add(cbCorrectMixed);
			base.Controls.Add(bkHelp);
			base.Controls.Add(edPDFDPI);
			base.Controls.Add(label8);
			base.Controls.Add(edTextQual);
			base.Controls.Add(label6);
			base.Controls.Add(edBinThreshold);
			base.Controls.Add(label7);
			base.Controls.Add(edDisabledChars);
			base.Controls.Add(label5);
			base.Controls.Add(edEnabledChars);
			base.Controls.Add(label4);
			base.Controls.Add(cbBinTwice);
			base.Controls.Add(cbFastMode);
			base.Controls.Add(cbGrayMode);
			base.Controls.Add(cbRemoveLines);
			base.Controls.Add(cbImgNoiseFilter);
			base.Controls.Add(cbRotation);
			base.Controls.Add(cbDeskew);
			base.Controls.Add(cbZonesInversion);
			base.Controls.Add(cbImgInversion);
			base.Controls.Add(cbFindBarcodes);
			base.Controls.Add(bkCancel);
			base.Controls.Add(bkOK);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OCROptionsForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "OCR Options";
			base.Load += new System.EventHandler(OCROptionsForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
