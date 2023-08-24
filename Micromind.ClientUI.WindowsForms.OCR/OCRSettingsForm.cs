using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.OCR
{
	public class OCRSettingsForm : Form, IForm
	{
		private CompanyOptionData companyOptionData;

		private CurrencyData currencyData;

		private string TABLENAME_CONST = "";

		private string IDFIELD_CONST = "";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private CheckBox checkBoxFindBarcode;

		private NumberTextBox FromtextBoxToMonth4;

		private NumberTextBox textBoxBinaryThr;

		private CheckBox checkBoxImageInversion;

		private CheckBox checkBoxFixSkew;

		private CheckBox checkBoxFixRotation;

		private CheckBox checkBoxCombineZones;

		private CheckBox checkBoxDictionary;

		private CheckBox checkBoxCorrectMixed;

		private CheckBox checkBoxBinTwice;

		private CheckBox checkBoxFastMode;

		private CheckBox checkBoxGrayMode;

		private CheckBox checkBoxRemoveLines;

		private CheckBox checkBoxApplyNoise;

		private Label label7;

		private TextBox textBoxTextQuality;

		private Label label6;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private TextBox textBoxDocIDSeparator;

		private TextBox textBoxNumberName;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxAssignUnknown;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public OCRSettingsForm()
		{
			InitializeComponent();
		}

		private bool GetData()
		{
			try
			{
				companyOptionData = new CompanyOptionData();
				DataTable companyOptionTable = companyOptionData.CompanyOptionTable;
				companyOptionTable.Rows.Add(305, checkBoxFixRotation.Checked, 2);
				companyOptionTable.Rows.Add(313, textBoxBinaryThr.Text, 2);
				companyOptionTable.Rows.Add(310, checkBoxBinTwice.Checked, 2);
				companyOptionTable.Rows.Add(315, checkBoxCombineZones.Checked, 2);
				companyOptionTable.Rows.Add(311, checkBoxCorrectMixed.Checked, 2);
				companyOptionTable.Rows.Add(301, textBoxDocIDSeparator.Text, 2);
				companyOptionTable.Rows.Add(309, checkBoxFastMode.Checked, 2);
				companyOptionTable.Rows.Add(302, checkBoxFindBarcode.Checked, 2);
				companyOptionTable.Rows.Add(304, checkBoxFixSkew.Checked, 2);
				companyOptionTable.Rows.Add(308, checkBoxGrayMode.Checked, 2);
				companyOptionTable.Rows.Add(303, checkBoxImageInversion.Checked, 2);
				companyOptionTable.Rows.Add(306, checkBoxApplyNoise.Checked, 2);
				companyOptionTable.Rows.Add(300, textBoxNumberName.Text, 2);
				companyOptionTable.Rows.Add(307, checkBoxRemoveLines.Checked, 2);
				companyOptionTable.Rows.Add(314, textBoxTextQuality.Text, 2);
				companyOptionTable.Rows.Add(312, checkBoxDictionary.Checked, 2);
				companyOptionTable.Rows.Add(316, checkBoxAssignUnknown.Checked, 2);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					CompanyOptions.LoadCompanyOptions();
					FillData();
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			checkBoxFixRotation.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_AutoRotate, defaultValue: true);
			textBoxBinaryThr.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_BinaryThr, "0");
			checkBoxBinTwice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_BinTwice, defaultValue: false);
			checkBoxCombineZones.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_CombineZonesHorz, defaultValue: false);
			checkBoxCorrectMixed.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_CorrectMixed, defaultValue: true);
			textBoxDocIDSeparator.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_DocIDSeparator, "-");
			checkBoxFastMode.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_FastMode, defaultValue: false);
			checkBoxFindBarcode.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_FindBarcodes, defaultValue: false);
			checkBoxFixSkew.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_FixSkew, defaultValue: true);
			checkBoxGrayMode.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_GrayMode, defaultValue: false);
			checkBoxImageInversion.Checked = !CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_ImageInversion, defaultValue: true);
			checkBoxApplyNoise.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_NoiseFilter, defaultValue: true);
			textBoxNumberName.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_NumberTitle, "Document Num:");
			checkBoxRemoveLines.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_RemoveLines, defaultValue: true);
			textBoxTextQuality.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_TextQuality, "0");
			checkBoxDictionary.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_UseDictionary, defaultValue: true);
			checkBoxAssignUnknown.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OCR_AssignUnknown, defaultValue: false);
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				Close();
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = true;
				flag &= Factory.CompanyOptionSystem.CreateCompanyOption(companyOptionData, 2);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyOptions.LoadCompanyOptions();
					ClearForm();
					Close();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.Edit)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					if (!GlobalRules.IsFeatureAllowed(EditionFeatures.MultiCurrency))
					{
						checkBoxFindBarcode.Enabled = false;
					}
					IsNewRecord = true;
					ClearForm();
					LoadData("1");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.OCR.OCRSettingsForm));
			checkBoxFindBarcode = new System.Windows.Forms.CheckBox();
			textBoxBinaryThr = new Micromind.UISupport.NumberTextBox();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxImageInversion = new System.Windows.Forms.CheckBox();
			checkBoxFixSkew = new System.Windows.Forms.CheckBox();
			checkBoxFixRotation = new System.Windows.Forms.CheckBox();
			checkBoxCombineZones = new System.Windows.Forms.CheckBox();
			checkBoxDictionary = new System.Windows.Forms.CheckBox();
			checkBoxCorrectMixed = new System.Windows.Forms.CheckBox();
			checkBoxBinTwice = new System.Windows.Forms.CheckBox();
			checkBoxFastMode = new System.Windows.Forms.CheckBox();
			checkBoxGrayMode = new System.Windows.Forms.CheckBox();
			checkBoxRemoveLines = new System.Windows.Forms.CheckBox();
			checkBoxApplyNoise = new System.Windows.Forms.CheckBox();
			label7 = new System.Windows.Forms.Label();
			textBoxTextQuality = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxDocIDSeparator = new System.Windows.Forms.TextBox();
			textBoxNumberName = new System.Windows.Forms.TextBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxAssignUnknown = new System.Windows.Forms.CheckBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			checkBoxFindBarcode.AutoSize = true;
			checkBoxFindBarcode.Location = new System.Drawing.Point(6, 19);
			checkBoxFindBarcode.Name = "checkBoxFindBarcode";
			checkBoxFindBarcode.Size = new System.Drawing.Size(93, 17);
			checkBoxFindBarcode.TabIndex = 2;
			checkBoxFindBarcode.Text = "Find barcodes";
			checkBoxFindBarcode.UseVisualStyleBackColor = true;
			textBoxBinaryThr.AllowDecimal = true;
			textBoxBinaryThr.CustomReportFieldName = "";
			textBoxBinaryThr.CustomReportKey = "";
			textBoxBinaryThr.CustomReportValueType = 1;
			textBoxBinaryThr.IsComboTextBox = false;
			textBoxBinaryThr.Location = new System.Drawing.Point(545, 109);
			textBoxBinaryThr.MaxLength = 3;
			textBoxBinaryThr.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxBinaryThr.MinValue = new decimal(new int[4]);
			textBoxBinaryThr.Name = "textBoxBinaryThr";
			textBoxBinaryThr.NullText = "0";
			textBoxBinaryThr.Size = new System.Drawing.Size(66, 20);
			textBoxBinaryThr.TabIndex = 15;
			textBoxBinaryThr.Text = "1";
			textBoxBinaryThr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 440);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(718, 40);
			panelButtons.TabIndex = 18;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(718, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(608, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(buttonClose_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(508, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 15;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			checkBoxImageInversion.AutoSize = true;
			checkBoxImageInversion.Location = new System.Drawing.Point(6, 40);
			checkBoxImageInversion.Name = "checkBoxImageInversion";
			checkBoxImageInversion.Size = new System.Drawing.Size(134, 17);
			checkBoxImageInversion.TabIndex = 3;
			checkBoxImageInversion.Text = "Detect image inversion";
			checkBoxImageInversion.UseVisualStyleBackColor = true;
			checkBoxFixSkew.AutoSize = true;
			checkBoxFixSkew.Location = new System.Drawing.Point(6, 61);
			checkBoxFixSkew.Name = "checkBoxFixSkew";
			checkBoxFixSkew.Size = new System.Drawing.Size(151, 17);
			checkBoxFixSkew.TabIndex = 4;
			checkBoxFixSkew.Text = "Detect and fix image skew";
			checkBoxFixSkew.UseVisualStyleBackColor = true;
			checkBoxFixRotation.AutoSize = true;
			checkBoxFixRotation.Location = new System.Drawing.Point(6, 82);
			checkBoxFixRotation.Name = "checkBoxFixRotation";
			checkBoxFixRotation.Size = new System.Drawing.Size(263, 17);
			checkBoxFixRotation.TabIndex = 5;
			checkBoxFixRotation.Text = "Detect and fix image rotation 90/180/270 degrees";
			checkBoxFixRotation.UseVisualStyleBackColor = true;
			checkBoxCombineZones.AutoSize = true;
			checkBoxCombineZones.Location = new System.Drawing.Point(339, 85);
			checkBoxCombineZones.Name = "checkBoxCombineZones";
			checkBoxCombineZones.Size = new System.Drawing.Size(153, 17);
			checkBoxCombineZones.TabIndex = 13;
			checkBoxCombineZones.Text = "Combine zones horizontally";
			checkBoxCombineZones.UseVisualStyleBackColor = true;
			checkBoxDictionary.AutoSize = true;
			checkBoxDictionary.Location = new System.Drawing.Point(339, 63);
			checkBoxDictionary.Name = "checkBoxDictionary";
			checkBoxDictionary.Size = new System.Drawing.Size(93, 17);
			checkBoxDictionary.TabIndex = 12;
			checkBoxDictionary.Text = "Use dictionary";
			checkBoxDictionary.UseVisualStyleBackColor = true;
			checkBoxCorrectMixed.AutoSize = true;
			checkBoxCorrectMixed.Location = new System.Drawing.Point(339, 41);
			checkBoxCorrectMixed.Name = "checkBoxCorrectMixed";
			checkBoxCorrectMixed.Size = new System.Drawing.Size(269, 17);
			checkBoxCorrectMixed.TabIndex = 11;
			checkBoxCorrectMixed.Text = "Correct mixed chars (letters and digits in same word)";
			checkBoxCorrectMixed.UseVisualStyleBackColor = true;
			checkBoxBinTwice.AutoSize = true;
			checkBoxBinTwice.Location = new System.Drawing.Point(339, 19);
			checkBoxBinTwice.Name = "checkBoxBinTwice";
			checkBoxBinTwice.Size = new System.Drawing.Size(91, 17);
			checkBoxBinTwice.TabIndex = 10;
			checkBoxBinTwice.Text = "Binarize twice";
			checkBoxBinTwice.UseVisualStyleBackColor = true;
			checkBoxFastMode.AutoSize = true;
			checkBoxFastMode.Location = new System.Drawing.Point(6, 166);
			checkBoxFastMode.Name = "checkBoxFastMode";
			checkBoxFastMode.Size = new System.Drawing.Size(147, 17);
			checkBoxFastMode.TabIndex = 9;
			checkBoxFastMode.Text = "Fast mode (less accurate)";
			checkBoxFastMode.UseVisualStyleBackColor = true;
			checkBoxGrayMode.AutoSize = true;
			checkBoxGrayMode.Location = new System.Drawing.Point(6, 145);
			checkBoxGrayMode.Name = "checkBoxGrayMode";
			checkBoxGrayMode.Size = new System.Drawing.Size(77, 17);
			checkBoxGrayMode.TabIndex = 8;
			checkBoxGrayMode.Text = "Gray mode";
			checkBoxGrayMode.UseVisualStyleBackColor = true;
			checkBoxRemoveLines.AutoSize = true;
			checkBoxRemoveLines.Location = new System.Drawing.Point(6, 124);
			checkBoxRemoveLines.Name = "checkBoxRemoveLines";
			checkBoxRemoveLines.Size = new System.Drawing.Size(141, 17);
			checkBoxRemoveLines.TabIndex = 7;
			checkBoxRemoveLines.Text = "Detect and remove lines";
			checkBoxRemoveLines.UseVisualStyleBackColor = true;
			checkBoxApplyNoise.AutoSize = true;
			checkBoxApplyNoise.Location = new System.Drawing.Point(6, 103);
			checkBoxApplyNoise.Name = "checkBoxApplyNoise";
			checkBoxApplyNoise.Size = new System.Drawing.Size(148, 17);
			checkBoxApplyNoise.TabIndex = 6;
			checkBoxApplyNoise.Text = "Apply noise filter for image";
			checkBoxApplyNoise.UseVisualStyleBackColor = true;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(336, 111);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(203, 13);
			label7.TabIndex = 14;
			label7.Text = "Binarization threshold (0..254; 255 - auto):";
			textBoxTextQuality.Location = new System.Drawing.Point(545, 135);
			textBoxTextQuality.Name = "textBoxTextQuality";
			textBoxTextQuality.Size = new System.Drawing.Size(63, 20);
			textBoxTextQuality.TabIndex = 17;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(336, 136);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(148, 13);
			label6.TabIndex = 16;
			label6.Text = "Text quality (0..100; -1 - auto):";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(27, 34);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(154, 13);
			mmLabel1.TabIndex = 40;
			mmLabel1.Text = "Document Number Field Name:";
			mmLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(347, 34);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(91, 13);
			mmLabel2.TabIndex = 40;
			mmLabel2.Text = "DocID Separator:";
			mmLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxDocIDSeparator.Location = new System.Drawing.Point(444, 32);
			textBoxDocIDSeparator.Name = "textBoxDocIDSeparator";
			textBoxDocIDSeparator.Size = new System.Drawing.Size(104, 20);
			textBoxDocIDSeparator.TabIndex = 1;
			textBoxDocIDSeparator.Text = "-";
			textBoxNumberName.Location = new System.Drawing.Point(187, 32);
			textBoxNumberName.Name = "textBoxNumberName";
			textBoxNumberName.Size = new System.Drawing.Size(142, 20);
			textBoxNumberName.TabIndex = 0;
			textBoxNumberName.Text = "Doc Number:";
			ultraGroupBox1.Controls.Add(checkBoxFindBarcode);
			ultraGroupBox1.Controls.Add(checkBoxImageInversion);
			ultraGroupBox1.Controls.Add(checkBoxFixSkew);
			ultraGroupBox1.Controls.Add(checkBoxFixRotation);
			ultraGroupBox1.Controls.Add(checkBoxApplyNoise);
			ultraGroupBox1.Controls.Add(textBoxTextQuality);
			ultraGroupBox1.Controls.Add(checkBoxRemoveLines);
			ultraGroupBox1.Controls.Add(label6);
			ultraGroupBox1.Controls.Add(checkBoxGrayMode);
			ultraGroupBox1.Controls.Add(label7);
			ultraGroupBox1.Controls.Add(textBoxBinaryThr);
			ultraGroupBox1.Controls.Add(checkBoxFastMode);
			ultraGroupBox1.Controls.Add(checkBoxCombineZones);
			ultraGroupBox1.Controls.Add(checkBoxBinTwice);
			ultraGroupBox1.Controls.Add(checkBoxDictionary);
			ultraGroupBox1.Controls.Add(checkBoxCorrectMixed);
			ultraGroupBox1.Location = new System.Drawing.Point(30, 131);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(648, 205);
			ultraGroupBox1.TabIndex = 41;
			ultraGroupBox1.Text = "OCR Settings";
			checkBoxAssignUnknown.AutoSize = true;
			checkBoxAssignUnknown.Location = new System.Drawing.Point(36, 77);
			checkBoxAssignUnknown.Name = "checkBoxAssignUnknown";
			checkBoxAssignUnknown.Size = new System.Drawing.Size(328, 17);
			checkBoxAssignUnknown.TabIndex = 42;
			checkBoxAssignUnknown.Text = "Assign unrecognized document to the last recognized document";
			checkBoxAssignUnknown.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(718, 480);
			base.Controls.Add(checkBoxAssignUnknown);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBoxNumberName);
			base.Controls.Add(textBoxDocIDSeparator);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(formManager);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(530, 315);
			base.Name = "OCRSettingsForm";
			Text = "OCR Settings";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AccountGroupDetailsForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
