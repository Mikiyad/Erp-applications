using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ContactDetailsForm : Form, IForm
	{
		private ContactData currentData;

		private const string TABLENAME_CONST = "Contact";

		private const string IDFIELD_CONST = "ContactID";

		private bool isNewRecord = true;

		private MMTextBox textBoxFirstName;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton buttonClose;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private MMLabel mmLabel6;

		private Panel panel1;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel20;

		private MMTextBox textBoxPostalCode;

		private MMLabel mmLabel18;

		private MMTextBox textBoxEmail1;

		private MMLabel mmLabel17;

		private MMTextBox textBoxMobile;

		private MMLabel mmLabel16;

		private MMTextBox textBoxFax;

		private MMLabel mmLabel15;

		private MMTextBox textBoxPhone2;

		private MMLabel mmLabel14;

		private MMTextBox textBoxPhone1;

		private MMLabel mmLabel12;

		private MMTextBox textBoxCountry;

		private MMLabel mmLabel11;

		private MMTextBox textBoxState;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCity;

		private MMLabel mmLabel10;

		private MMTextBox textBoxAddress;

		private MMTextBox textBoxAddressPrintFormat;

		private MMLabel mmLabel21;

		private MMLabel mmLabel19;

		private MMTextBox textBoxWebsite;

		private MMLabel mmLabel30;

		private MMTextBox textBoxBankAccountNumber;

		private MMLabel mmLabel28;

		private MMTextBox textBoxBankName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel4;

		private MMTextBox textBoxMiddleName;

		private MMLabel mmLabel5;

		private MMTextBox textBoxLastName;

		private MMLabel mmLabel8;

		private MMTextBox textBoxSpouseName;

		private MMLabel mmLabel9;

		private MMLabel mmLabel22;

		private MMTextBox textBoxIMAddress;

		private MMTextBox textBoxJobTitle;

		private MMLabel mmLabel23;

		private MMLabel mmLabel24;

		private MMLabel mmLabel29;

		private MMTextBox textBoxEmail2;

		private MMTextBox textBoxLanguages;

		private MMTextBox textBoxHobbies;

		private MMLabel mmLabel27;

		private MMLabel mmLabel2;

		private MMTextBox textBoxChildren;

		private MMLabel mmLabel31;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private MMTextBox textBoxNickName;

		private MMLabel mmLabel32;

		private DateTimePicker datePickerBirthDate;

		private DateTimePicker datePickerAnniversary;

		private MMLabel mmLabel33;

		private MMTextBox textBoxTwitter;

		private MMLabel mmLabel35;

		private MMTextBox textBoxSkype;

		private MMLabel mmLabel34;

		private MMTextBox textBoxFacebook;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel36;

		private MMLabel mmLabel7;

		private MMLabel mmLabel37;

		private ComboBox comboBoxGender;

		private MMLabel mmLabel1;

		private MMLabel mmLabel26;

		private MMTextBox textBoxAssistant;

		private MMLabel mmLabel25;

		private MMTextBox textBoxManagerName;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private ToolStripButton toolStripButtonShowPicture;

		private OpenFileDialog openFileDialog1;

		private PictureBox pictureBoxNoImage;

		private UDFEntryControl udfEntryGrid;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel39;

		private MMLabel mmLabel38;

		private MMTextBox textBoxLinkedIn;

		private XPButton buttonCategories;

		private MMTextBox textBoxName;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6003;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
				}
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public ContactDetailsForm()
		{
			InitializeComponent();
			AddEvents();
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ContactDetailsForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxName = new Micromind.UISupport.MMTextBox();
			buttonCategories = new Micromind.UISupport.XPButton();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			textBoxNickName = new Micromind.UISupport.MMTextBox();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxFirstName = new Micromind.UISupport.MMTextBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			textBoxLinkedIn = new Micromind.UISupport.MMTextBox();
			textBoxTwitter = new Micromind.UISupport.MMTextBox();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			textBoxSkype = new Micromind.UISupport.MMTextBox();
			textBoxEmail2 = new Micromind.UISupport.MMTextBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxFacebook = new Micromind.UISupport.MMTextBox();
			textBoxWebsite = new Micromind.UISupport.MMTextBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxAddressPrintFormat = new Micromind.UISupport.MMTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxPostalCode = new Micromind.UISupport.MMTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxEmail1 = new Micromind.UISupport.MMTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxIMAddress = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxMobile = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxFax = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			textBoxPhone2 = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxPhone1 = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxCountry = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxState = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxAddress = new Micromind.UISupport.MMTextBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxMiddleName = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxLastName = new Micromind.UISupport.MMTextBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxJobTitle = new Micromind.UISupport.MMTextBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxGender = new System.Windows.Forms.ComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			textBoxAssistant = new Micromind.UISupport.MMTextBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			textBoxManagerName = new Micromind.UISupport.MMTextBox();
			datePickerAnniversary = new System.Windows.Forms.DateTimePicker();
			datePickerBirthDate = new System.Windows.Forms.DateTimePicker();
			textBoxChildren = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			textBoxBankAccountNumber = new Micromind.UISupport.MMTextBox();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			textBoxSpouseName = new Micromind.UISupport.MMTextBox();
			textBoxHobbies = new Micromind.UISupport.MMTextBox();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			textBoxLanguages = new Micromind.UISupport.MMTextBox();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			tabPageDetails.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(buttonCategories);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(textBoxNickName);
			tabPageGeneral.Controls.Add(mmLabel32);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxFirstName);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(textBoxMiddleName);
			tabPageGeneral.Controls.Add(mmLabel4);
			tabPageGeneral.Controls.Add(textBoxLastName);
			tabPageGeneral.Controls.Add(mmLabel23);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(textBoxJobTitle);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(711, 502);
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(128, 141);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(229, 20);
			textBoxName.TabIndex = 347;
			textBoxName.Visible = false;
			buttonCategories.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCategories.BackColor = System.Drawing.Color.DarkGray;
			buttonCategories.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCategories.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCategories.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCategories.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCategories.Location = new System.Drawing.Point(519, 176);
			buttonCategories.Name = "buttonCategories";
			buttonCategories.Size = new System.Drawing.Size(134, 24);
			buttonCategories.TabIndex = 346;
			buttonCategories.Text = "Categories...";
			buttonCategories.UseVisualStyleBackColor = false;
			buttonCategories.Click += new System.EventHandler(buttonCategories_Click);
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(564, 141);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 336;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(525, 141);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 335;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance2;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(554, 60);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 334;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance3;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(525, 12);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(122, 125);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 333;
			pictureBoxPhoto.TabStop = false;
			textBoxNickName.BackColor = System.Drawing.Color.White;
			textBoxNickName.CustomReportFieldName = "";
			textBoxNickName.CustomReportKey = "";
			textBoxNickName.CustomReportValueType = 1;
			textBoxNickName.IsComboTextBox = false;
			textBoxNickName.IsModified = false;
			textBoxNickName.IsRequired = true;
			textBoxNickName.Location = new System.Drawing.Point(128, 98);
			textBoxNickName.MaxLength = 64;
			textBoxNickName.Name = "textBoxNickName";
			textBoxNickName.Size = new System.Drawing.Size(379, 20);
			textBoxNickName.TabIndex = 5;
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(5, 100);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(60, 13);
			mmLabel32.TabIndex = 332;
			mmLabel32.Text = "Nick Name:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(5, 13);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(85, 13);
			mmLabel3.TabIndex = 0;
			mmLabel3.Text = "Contact Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(128, 10);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(174, 20);
			textBoxCode.TabIndex = 0;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(308, 12);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(87, 16);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			textBoxFirstName.BackColor = System.Drawing.Color.White;
			textBoxFirstName.CustomReportFieldName = "";
			textBoxFirstName.CustomReportKey = "";
			textBoxFirstName.CustomReportValueType = 1;
			textBoxFirstName.IsComboTextBox = false;
			textBoxFirstName.IsModified = false;
			textBoxFirstName.IsRequired = true;
			textBoxFirstName.Location = new System.Drawing.Point(128, 32);
			textBoxFirstName.MaxLength = 64;
			textBoxFirstName.Name = "textBoxFirstName";
			textBoxFirstName.Size = new System.Drawing.Size(379, 20);
			textBoxFirstName.TabIndex = 2;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel39);
			ultraGroupBox1.Controls.Add(mmLabel38);
			ultraGroupBox1.Controls.Add(textBoxLinkedIn);
			ultraGroupBox1.Controls.Add(textBoxTwitter);
			ultraGroupBox1.Controls.Add(mmLabel37);
			ultraGroupBox1.Controls.Add(mmLabel36);
			ultraGroupBox1.Controls.Add(mmLabel33);
			ultraGroupBox1.Controls.Add(mmLabel7);
			ultraGroupBox1.Controls.Add(mmLabel35);
			ultraGroupBox1.Controls.Add(mmLabel29);
			ultraGroupBox1.Controls.Add(textBoxSkype);
			ultraGroupBox1.Controls.Add(textBoxEmail2);
			ultraGroupBox1.Controls.Add(mmLabel34);
			ultraGroupBox1.Controls.Add(mmLabel19);
			ultraGroupBox1.Controls.Add(textBoxFacebook);
			ultraGroupBox1.Controls.Add(textBoxWebsite);
			ultraGroupBox1.Controls.Add(mmLabel21);
			ultraGroupBox1.Controls.Add(textBoxAddressPrintFormat);
			ultraGroupBox1.Controls.Add(mmLabel20);
			ultraGroupBox1.Controls.Add(textBoxPostalCode);
			ultraGroupBox1.Controls.Add(mmLabel18);
			ultraGroupBox1.Controls.Add(textBoxEmail1);
			ultraGroupBox1.Controls.Add(mmLabel22);
			ultraGroupBox1.Controls.Add(textBoxIMAddress);
			ultraGroupBox1.Controls.Add(mmLabel17);
			ultraGroupBox1.Controls.Add(textBoxMobile);
			ultraGroupBox1.Controls.Add(mmLabel16);
			ultraGroupBox1.Controls.Add(textBoxFax);
			ultraGroupBox1.Controls.Add(mmLabel15);
			ultraGroupBox1.Controls.Add(textBoxPhone2);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(textBoxPhone1);
			ultraGroupBox1.Controls.Add(mmLabel12);
			ultraGroupBox1.Controls.Add(textBoxCountry);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(textBoxState);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxCity);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(textBoxAddress);
			ultraGroupBox1.Controls.Add(pictureBoxNoImage);
			ultraGroupBox1.Location = new System.Drawing.Point(5, 196);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(652, 309);
			ultraGroupBox1.TabIndex = 7;
			ultraGroupBox1.Text = "Address";
			ultraGroupBox1.Click += new System.EventHandler(ultraGroupBox1_Click);
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.Image = (System.Drawing.Image)resources.GetObject("mmLabel39.Image");
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(333, 216);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(21, 17);
			mmLabel39.TabIndex = 344;
			mmLabel39.Text = "      ";
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(359, 219);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(51, 13);
			mmLabel38.TabIndex = 343;
			mmLabel38.Text = "LinkedIn:";
			textBoxLinkedIn.BackColor = System.Drawing.Color.White;
			textBoxLinkedIn.CustomReportFieldName = "";
			textBoxLinkedIn.CustomReportKey = "";
			textBoxLinkedIn.CustomReportValueType = 1;
			textBoxLinkedIn.IsComboTextBox = false;
			textBoxLinkedIn.IsModified = false;
			textBoxLinkedIn.Location = new System.Drawing.Point(428, 216);
			textBoxLinkedIn.MaxLength = 255;
			textBoxLinkedIn.Name = "textBoxLinkedIn";
			textBoxLinkedIn.Size = new System.Drawing.Size(214, 20);
			textBoxLinkedIn.TabIndex = 342;
			textBoxTwitter.BackColor = System.Drawing.Color.White;
			textBoxTwitter.CustomReportFieldName = "";
			textBoxTwitter.CustomReportKey = "";
			textBoxTwitter.CustomReportValueType = 1;
			textBoxTwitter.IsComboTextBox = false;
			textBoxTwitter.IsModified = false;
			textBoxTwitter.Location = new System.Drawing.Point(428, 239);
			textBoxTwitter.MaxLength = 30;
			textBoxTwitter.Name = "textBoxTwitter";
			textBoxTwitter.Size = new System.Drawing.Size(214, 20);
			textBoxTwitter.TabIndex = 14;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.Image = Micromind.ClientUI.Properties.Resources.skype;
			mmLabel37.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(333, 282);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(21, 19);
			mmLabel37.TabIndex = 341;
			mmLabel37.Text = "      ";
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.Image = Micromind.ClientUI.Properties.Resources.facebooklogo;
			mmLabel36.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(333, 260);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(21, 19);
			mmLabel36.TabIndex = 340;
			mmLabel36.Text = "      ";
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel33.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(358, 242);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(45, 13);
			mmLabel33.TabIndex = 334;
			mmLabel33.Text = "Twitter:";
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.Image = Micromind.ClientUI.Properties.Resources.twitter;
			mmLabel7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(333, 239);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(21, 19);
			mmLabel7.TabIndex = 339;
			mmLabel7.Text = "      ";
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(357, 285);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(40, 13);
			mmLabel35.TabIndex = 338;
			mmLabel35.Text = "Skype:";
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(357, 154);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(44, 13);
			mmLabel29.TabIndex = 324;
			mmLabel29.Text = "Email 2:";
			textBoxSkype.BackColor = System.Drawing.Color.White;
			textBoxSkype.CustomReportFieldName = "";
			textBoxSkype.CustomReportKey = "";
			textBoxSkype.CustomReportValueType = 1;
			textBoxSkype.IsComboTextBox = false;
			textBoxSkype.IsModified = false;
			textBoxSkype.Location = new System.Drawing.Point(428, 282);
			textBoxSkype.MaxLength = 30;
			textBoxSkype.Name = "textBoxSkype";
			textBoxSkype.Size = new System.Drawing.Size(214, 20);
			textBoxSkype.TabIndex = 16;
			textBoxEmail2.BackColor = System.Drawing.Color.White;
			textBoxEmail2.CustomReportFieldName = "";
			textBoxEmail2.CustomReportKey = "";
			textBoxEmail2.CustomReportValueType = 1;
			textBoxEmail2.IsComboTextBox = false;
			textBoxEmail2.IsModified = false;
			textBoxEmail2.Location = new System.Drawing.Point(428, 151);
			textBoxEmail2.MaxLength = 64;
			textBoxEmail2.Name = "textBoxEmail2";
			textBoxEmail2.Size = new System.Drawing.Size(214, 20);
			textBoxEmail2.TabIndex = 11;
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(357, 263);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(57, 13);
			mmLabel34.TabIndex = 336;
			mmLabel34.Text = "Facebook:";
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(357, 197);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(50, 13);
			mmLabel19.TabIndex = 30;
			mmLabel19.Text = "Website:";
			textBoxFacebook.BackColor = System.Drawing.Color.White;
			textBoxFacebook.CustomReportFieldName = "";
			textBoxFacebook.CustomReportKey = "";
			textBoxFacebook.CustomReportValueType = 1;
			textBoxFacebook.IsComboTextBox = false;
			textBoxFacebook.IsModified = false;
			textBoxFacebook.Location = new System.Drawing.Point(428, 260);
			textBoxFacebook.MaxLength = 255;
			textBoxFacebook.Name = "textBoxFacebook";
			textBoxFacebook.Size = new System.Drawing.Size(214, 20);
			textBoxFacebook.TabIndex = 15;
			textBoxWebsite.BackColor = System.Drawing.Color.White;
			textBoxWebsite.CustomReportFieldName = "";
			textBoxWebsite.CustomReportKey = "";
			textBoxWebsite.CustomReportValueType = 1;
			textBoxWebsite.IsComboTextBox = false;
			textBoxWebsite.IsModified = false;
			textBoxWebsite.Location = new System.Drawing.Point(428, 195);
			textBoxWebsite.MaxLength = 255;
			textBoxWebsite.Name = "textBoxWebsite";
			textBoxWebsite.Size = new System.Drawing.Size(214, 20);
			textBoxWebsite.TabIndex = 13;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(-1, 130);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(112, 13);
			mmLabel21.TabIndex = 16;
			mmLabel21.Text = "Address Print Format:";
			textBoxAddressPrintFormat.BackColor = System.Drawing.Color.White;
			textBoxAddressPrintFormat.CustomReportFieldName = "";
			textBoxAddressPrintFormat.CustomReportKey = "";
			textBoxAddressPrintFormat.CustomReportValueType = 1;
			textBoxAddressPrintFormat.IsComboTextBox = false;
			textBoxAddressPrintFormat.IsModified = false;
			textBoxAddressPrintFormat.Location = new System.Drawing.Point(122, 129);
			textBoxAddressPrintFormat.MaxLength = 255;
			textBoxAddressPrintFormat.Multiline = true;
			textBoxAddressPrintFormat.Name = "textBoxAddressPrintFormat";
			textBoxAddressPrintFormat.Size = new System.Drawing.Size(229, 81);
			textBoxAddressPrintFormat.TabIndex = 5;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(-1, 110);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(68, 13);
			mmLabel20.TabIndex = 14;
			mmLabel20.Text = "Postal Code:";
			textBoxPostalCode.BackColor = System.Drawing.Color.White;
			textBoxPostalCode.CustomReportFieldName = "";
			textBoxPostalCode.CustomReportKey = "";
			textBoxPostalCode.CustomReportValueType = 1;
			textBoxPostalCode.IsComboTextBox = false;
			textBoxPostalCode.IsModified = false;
			textBoxPostalCode.Location = new System.Drawing.Point(122, 107);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(229, 20);
			textBoxPostalCode.TabIndex = 4;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(357, 131);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(44, 13);
			mmLabel18.TabIndex = 28;
			mmLabel18.Text = "Email 1:";
			textBoxEmail1.BackColor = System.Drawing.Color.White;
			textBoxEmail1.CustomReportFieldName = "";
			textBoxEmail1.CustomReportKey = "";
			textBoxEmail1.CustomReportValueType = 1;
			textBoxEmail1.IsComboTextBox = false;
			textBoxEmail1.IsModified = false;
			textBoxEmail1.Location = new System.Drawing.Point(428, 129);
			textBoxEmail1.MaxLength = 64;
			textBoxEmail1.Name = "textBoxEmail1";
			textBoxEmail1.Size = new System.Drawing.Size(214, 20);
			textBoxEmail1.TabIndex = 10;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(357, 176);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(65, 13);
			mmLabel22.TabIndex = 321;
			mmLabel22.Text = "IM Address:";
			textBoxIMAddress.BackColor = System.Drawing.Color.White;
			textBoxIMAddress.CustomReportFieldName = "";
			textBoxIMAddress.CustomReportKey = "";
			textBoxIMAddress.CustomReportValueType = 1;
			textBoxIMAddress.IsComboTextBox = false;
			textBoxIMAddress.IsModified = false;
			textBoxIMAddress.IsRequired = true;
			textBoxIMAddress.Location = new System.Drawing.Point(428, 173);
			textBoxIMAddress.MaxLength = 64;
			textBoxIMAddress.Name = "textBoxIMAddress";
			textBoxIMAddress.Size = new System.Drawing.Size(214, 20);
			textBoxIMAddress.TabIndex = 12;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(357, 110);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(41, 13);
			mmLabel17.TabIndex = 26;
			mmLabel17.Text = "Mobile:";
			textBoxMobile.BackColor = System.Drawing.Color.White;
			textBoxMobile.CustomReportFieldName = "";
			textBoxMobile.CustomReportKey = "";
			textBoxMobile.CustomReportValueType = 1;
			textBoxMobile.IsComboTextBox = false;
			textBoxMobile.IsModified = false;
			textBoxMobile.Location = new System.Drawing.Point(428, 107);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(214, 20);
			textBoxMobile.TabIndex = 9;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(357, 87);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(29, 13);
			mmLabel16.TabIndex = 24;
			mmLabel16.Text = "Fax:";
			textBoxFax.BackColor = System.Drawing.Color.White;
			textBoxFax.CustomReportFieldName = "";
			textBoxFax.CustomReportKey = "";
			textBoxFax.CustomReportValueType = 1;
			textBoxFax.IsComboTextBox = false;
			textBoxFax.IsModified = false;
			textBoxFax.Location = new System.Drawing.Point(428, 85);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(214, 20);
			textBoxFax.TabIndex = 8;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(357, 66);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(50, 13);
			mmLabel15.TabIndex = 22;
			mmLabel15.Text = "Phone 2:";
			textBoxPhone2.BackColor = System.Drawing.Color.White;
			textBoxPhone2.CustomReportFieldName = "";
			textBoxPhone2.CustomReportKey = "";
			textBoxPhone2.CustomReportValueType = 1;
			textBoxPhone2.IsComboTextBox = false;
			textBoxPhone2.IsModified = false;
			textBoxPhone2.Location = new System.Drawing.Point(428, 63);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(214, 20);
			textBoxPhone2.TabIndex = 7;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(357, 44);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(50, 13);
			mmLabel14.TabIndex = 20;
			mmLabel14.Text = "Phone 1:";
			textBoxPhone1.BackColor = System.Drawing.Color.White;
			textBoxPhone1.CustomReportFieldName = "";
			textBoxPhone1.CustomReportKey = "";
			textBoxPhone1.CustomReportValueType = 1;
			textBoxPhone1.IsComboTextBox = false;
			textBoxPhone1.IsModified = false;
			textBoxPhone1.Location = new System.Drawing.Point(428, 41);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(214, 20);
			textBoxPhone1.TabIndex = 6;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(-1, 88);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(50, 13);
			mmLabel12.TabIndex = 12;
			mmLabel12.Text = "Country:";
			textBoxCountry.BackColor = System.Drawing.Color.White;
			textBoxCountry.CustomReportFieldName = "";
			textBoxCountry.CustomReportKey = "";
			textBoxCountry.CustomReportValueType = 1;
			textBoxCountry.IsComboTextBox = false;
			textBoxCountry.IsModified = false;
			textBoxCountry.Location = new System.Drawing.Point(122, 85);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(229, 20);
			textBoxCountry.TabIndex = 3;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(-1, 66);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(37, 13);
			mmLabel11.TabIndex = 10;
			mmLabel11.Text = "State:";
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.IsModified = false;
			textBoxState.Location = new System.Drawing.Point(122, 63);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(229, 20);
			textBoxState.TabIndex = 2;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(-1, 43);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(30, 13);
			mmLabel13.TabIndex = 2;
			mmLabel13.Text = "City:";
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
			textBoxCity.IsModified = false;
			textBoxCity.Location = new System.Drawing.Point(122, 41);
			textBoxCity.MaxLength = 30;
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(229, 20);
			textBoxCity.TabIndex = 1;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(-1, 21);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(50, 13);
			mmLabel10.TabIndex = 0;
			mmLabel10.Text = "Address:";
			textBoxAddress.BackColor = System.Drawing.Color.White;
			textBoxAddress.CustomReportFieldName = "";
			textBoxAddress.CustomReportKey = "";
			textBoxAddress.CustomReportValueType = 1;
			textBoxAddress.IsComboTextBox = false;
			textBoxAddress.IsModified = false;
			textBoxAddress.Location = new System.Drawing.Point(122, 19);
			textBoxAddress.MaxLength = 64;
			textBoxAddress.Name = "textBoxAddress";
			textBoxAddress.Size = new System.Drawing.Size(520, 20);
			textBoxAddress.TabIndex = 0;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(578, 145);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 337;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(5, 34);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(62, 13);
			mmLabel6.TabIndex = 3;
			mmLabel6.Text = "First Name:";
			textBoxMiddleName.BackColor = System.Drawing.Color.White;
			textBoxMiddleName.CustomReportFieldName = "";
			textBoxMiddleName.CustomReportKey = "";
			textBoxMiddleName.CustomReportValueType = 1;
			textBoxMiddleName.IsComboTextBox = false;
			textBoxMiddleName.IsModified = false;
			textBoxMiddleName.IsRequired = true;
			textBoxMiddleName.Location = new System.Drawing.Point(128, 54);
			textBoxMiddleName.MaxLength = 64;
			textBoxMiddleName.Name = "textBoxMiddleName";
			textBoxMiddleName.Size = new System.Drawing.Size(379, 20);
			textBoxMiddleName.TabIndex = 3;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(5, 56);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(71, 13);
			mmLabel4.TabIndex = 315;
			mmLabel4.Text = "Middle Name:";
			textBoxLastName.BackColor = System.Drawing.Color.White;
			textBoxLastName.CustomReportFieldName = "";
			textBoxLastName.CustomReportKey = "";
			textBoxLastName.CustomReportValueType = 1;
			textBoxLastName.IsComboTextBox = false;
			textBoxLastName.IsModified = false;
			textBoxLastName.IsRequired = true;
			textBoxLastName.Location = new System.Drawing.Point(128, 76);
			textBoxLastName.MaxLength = 64;
			textBoxLastName.Name = "textBoxLastName";
			textBoxLastName.Size = new System.Drawing.Size(379, 20);
			textBoxLastName.TabIndex = 4;
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(5, 123);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(51, 13);
			mmLabel23.TabIndex = 321;
			mmLabel23.Text = "Job Title:";
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(5, 78);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(61, 13);
			mmLabel5.TabIndex = 317;
			mmLabel5.Text = "Last Name:";
			textBoxJobTitle.BackColor = System.Drawing.Color.White;
			textBoxJobTitle.CustomReportFieldName = "";
			textBoxJobTitle.CustomReportKey = "";
			textBoxJobTitle.CustomReportValueType = 1;
			textBoxJobTitle.IsComboTextBox = false;
			textBoxJobTitle.IsModified = false;
			textBoxJobTitle.IsRequired = true;
			textBoxJobTitle.Location = new System.Drawing.Point(127, 120);
			textBoxJobTitle.MaxLength = 30;
			textBoxJobTitle.Name = "textBoxJobTitle";
			textBoxJobTitle.Size = new System.Drawing.Size(380, 20);
			textBoxJobTitle.TabIndex = 6;
			tabPageDetails.Controls.Add(comboBoxGender);
			tabPageDetails.Controls.Add(mmLabel1);
			tabPageDetails.Controls.Add(mmLabel26);
			tabPageDetails.Controls.Add(textBoxAssistant);
			tabPageDetails.Controls.Add(mmLabel25);
			tabPageDetails.Controls.Add(textBoxManagerName);
			tabPageDetails.Controls.Add(datePickerAnniversary);
			tabPageDetails.Controls.Add(datePickerBirthDate);
			tabPageDetails.Controls.Add(textBoxChildren);
			tabPageDetails.Controls.Add(mmLabel8);
			tabPageDetails.Controls.Add(mmLabel31);
			tabPageDetails.Controls.Add(textBoxBankAccountNumber);
			tabPageDetails.Controls.Add(mmLabel30);
			tabPageDetails.Controls.Add(mmLabel2);
			tabPageDetails.Controls.Add(mmLabel27);
			tabPageDetails.Controls.Add(mmLabel9);
			tabPageDetails.Controls.Add(textBoxBankName);
			tabPageDetails.Controls.Add(textBoxSpouseName);
			tabPageDetails.Controls.Add(textBoxHobbies);
			tabPageDetails.Controls.Add(mmLabel28);
			tabPageDetails.Controls.Add(textBoxLanguages);
			tabPageDetails.Controls.Add(mmLabel24);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(711, 502);
			comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGender.FormattingEnabled = true;
			comboBoxGender.Location = new System.Drawing.Point(138, 65);
			comboBoxGender.Name = "comboBoxGender";
			comboBoxGender.Size = new System.Drawing.Size(132, 21);
			comboBoxGender.TabIndex = 2;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(15, 70);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(46, 13);
			mmLabel1.TabIndex = 336;
			mmLabel1.Text = "Gender:";
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(15, 36);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(85, 13);
			mmLabel26.TabIndex = 335;
			mmLabel26.Text = "Assistant Name:";
			textBoxAssistant.BackColor = System.Drawing.Color.White;
			textBoxAssistant.CustomReportFieldName = "";
			textBoxAssistant.CustomReportKey = "";
			textBoxAssistant.CustomReportValueType = 1;
			textBoxAssistant.IsComboTextBox = false;
			textBoxAssistant.IsModified = false;
			textBoxAssistant.IsRequired = true;
			textBoxAssistant.Location = new System.Drawing.Point(138, 34);
			textBoxAssistant.MaxLength = 64;
			textBoxAssistant.Name = "textBoxAssistant";
			textBoxAssistant.Size = new System.Drawing.Size(216, 20);
			textBoxAssistant.TabIndex = 1;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(15, 15);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(83, 13);
			mmLabel25.TabIndex = 334;
			mmLabel25.Text = "Manager Name:";
			textBoxManagerName.BackColor = System.Drawing.Color.White;
			textBoxManagerName.CustomReportFieldName = "";
			textBoxManagerName.CustomReportKey = "";
			textBoxManagerName.CustomReportValueType = 1;
			textBoxManagerName.IsComboTextBox = false;
			textBoxManagerName.IsModified = false;
			textBoxManagerName.IsRequired = true;
			textBoxManagerName.Location = new System.Drawing.Point(138, 12);
			textBoxManagerName.MaxLength = 64;
			textBoxManagerName.Name = "textBoxManagerName";
			textBoxManagerName.Size = new System.Drawing.Size(216, 20);
			textBoxManagerName.TabIndex = 0;
			datePickerAnniversary.Checked = false;
			datePickerAnniversary.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			datePickerAnniversary.Location = new System.Drawing.Point(138, 133);
			datePickerAnniversary.Name = "datePickerAnniversary";
			datePickerAnniversary.ShowCheckBox = true;
			datePickerAnniversary.Size = new System.Drawing.Size(132, 20);
			datePickerAnniversary.TabIndex = 5;
			datePickerBirthDate.Checked = false;
			datePickerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			datePickerBirthDate.Location = new System.Drawing.Point(138, 89);
			datePickerBirthDate.Name = "datePickerBirthDate";
			datePickerBirthDate.ShowCheckBox = true;
			datePickerBirthDate.Size = new System.Drawing.Size(132, 20);
			datePickerBirthDate.TabIndex = 3;
			textBoxChildren.BackColor = System.Drawing.Color.White;
			textBoxChildren.CustomReportFieldName = "";
			textBoxChildren.CustomReportKey = "";
			textBoxChildren.CustomReportValueType = 1;
			textBoxChildren.IsComboTextBox = false;
			textBoxChildren.IsModified = false;
			textBoxChildren.Location = new System.Drawing.Point(138, 155);
			textBoxChildren.MaxLength = 64;
			textBoxChildren.Name = "textBoxChildren";
			textBoxChildren.Size = new System.Drawing.Size(509, 20);
			textBoxChildren.TabIndex = 6;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(15, 92);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(59, 13);
			mmLabel8.TabIndex = 320;
			mmLabel8.Text = "Birth Date:";
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(15, 158);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(50, 13);
			mmLabel31.TabIndex = 326;
			mmLabel31.Text = "Children:";
			textBoxBankAccountNumber.BackColor = System.Drawing.Color.White;
			textBoxBankAccountNumber.CustomReportFieldName = "";
			textBoxBankAccountNumber.CustomReportKey = "";
			textBoxBankAccountNumber.CustomReportValueType = 1;
			textBoxBankAccountNumber.IsComboTextBox = false;
			textBoxBankAccountNumber.IsModified = false;
			textBoxBankAccountNumber.Location = new System.Drawing.Point(138, 253);
			textBoxBankAccountNumber.MaxLength = 30;
			textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
			textBoxBankAccountNumber.Size = new System.Drawing.Size(190, 20);
			textBoxBankAccountNumber.TabIndex = 10;
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(15, 255);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(92, 13);
			mmLabel30.TabIndex = 4;
			mmLabel30.Text = "Bank Account No:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(15, 179);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(49, 13);
			mmLabel2.TabIndex = 4;
			mmLabel2.Text = "Hobbies:";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(15, 200);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(63, 13);
			mmLabel27.TabIndex = 4;
			mmLabel27.Text = "Languages:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(15, 113);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(76, 13);
			mmLabel9.TabIndex = 317;
			mmLabel9.Text = "Spouse Name:";
			textBoxBankName.BackColor = System.Drawing.Color.White;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.Location = new System.Drawing.Point(138, 231);
			textBoxBankName.MaxLength = 30;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.Size = new System.Drawing.Size(190, 20);
			textBoxBankName.TabIndex = 9;
			textBoxSpouseName.BackColor = System.Drawing.Color.White;
			textBoxSpouseName.CustomReportFieldName = "";
			textBoxSpouseName.CustomReportKey = "";
			textBoxSpouseName.CustomReportValueType = 1;
			textBoxSpouseName.IsComboTextBox = false;
			textBoxSpouseName.IsModified = false;
			textBoxSpouseName.IsRequired = true;
			textBoxSpouseName.Location = new System.Drawing.Point(138, 111);
			textBoxSpouseName.MaxLength = 64;
			textBoxSpouseName.Name = "textBoxSpouseName";
			textBoxSpouseName.Size = new System.Drawing.Size(216, 20);
			textBoxSpouseName.TabIndex = 4;
			textBoxHobbies.BackColor = System.Drawing.Color.White;
			textBoxHobbies.CustomReportFieldName = "";
			textBoxHobbies.CustomReportKey = "";
			textBoxHobbies.CustomReportValueType = 1;
			textBoxHobbies.IsComboTextBox = false;
			textBoxHobbies.IsModified = false;
			textBoxHobbies.Location = new System.Drawing.Point(138, 177);
			textBoxHobbies.MaxLength = 255;
			textBoxHobbies.Name = "textBoxHobbies";
			textBoxHobbies.Size = new System.Drawing.Size(509, 20);
			textBoxHobbies.TabIndex = 7;
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(15, 233);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(64, 13);
			mmLabel28.TabIndex = 0;
			mmLabel28.Text = "Bank Name:";
			textBoxLanguages.BackColor = System.Drawing.Color.White;
			textBoxLanguages.CustomReportFieldName = "";
			textBoxLanguages.CustomReportKey = "";
			textBoxLanguages.CustomReportValueType = 1;
			textBoxLanguages.IsComboTextBox = false;
			textBoxLanguages.IsModified = false;
			textBoxLanguages.Location = new System.Drawing.Point(138, 199);
			textBoxLanguages.MaxLength = 255;
			textBoxLanguages.Name = "textBoxLanguages";
			textBoxLanguages.Size = new System.Drawing.Size(509, 20);
			textBoxLanguages.TabIndex = 8;
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(15, 135);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(69, 13);
			mmLabel24.TabIndex = 323;
			mmLabel24.Text = "Anniversary:";
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(711, 508);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(11, 15);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(690, 479);
			textBoxNote.TabIndex = 13;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(711, 508);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(6, 4);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(701, 487);
			udfEntryGrid.TabIndex = 1;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 564);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(715, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(715, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(605, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonShowPicture,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(715, 31);
			toolStrip1.TabIndex = 306;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(104, 28);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MaximumSize = new System.Drawing.Size(0, 8);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(715, 8);
			panel1.TabIndex = 314;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 39);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(715, 525);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance4.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance4;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "Note";
			ultraTab4.TabPage = tabPageUserDefined;
			ultraTab4.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(711, 502);
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 307;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(715, 604);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ContactDetailsForm";
			Text = "Contact Detail";
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private void AddEvents()
		{
			base.Load += ContactDetailsForm_Load;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void ContactDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				LoadGender();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
					ClearForm();
					textBoxCode.Focus();
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void LoadGender()
		{
			comboBoxGender.Items.Clear();
			comboBoxGender.Items.Add("Male");
			comboBoxGender.Items.Add("Female");
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.ContactTable.Rows[0];
			textBoxCode.Text = dataRow["ContactID"].ToString();
			textBoxFirstName.Text = dataRow["FirstName"].ToString();
			textBoxMiddleName.Text = dataRow["MiddleName"].ToString();
			textBoxLastName.Text = dataRow["LastName"].ToString();
			textBoxNickName.Text = dataRow["NickName"].ToString();
			textBoxJobTitle.Text = dataRow["JobTitle"].ToString();
			textBoxManagerName.Text = dataRow["ManagerName"].ToString();
			textBoxAssistant.Text = dataRow["AssistantName"].ToString();
			checkBoxIsInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
			textBoxAddress.Text = dataRow["Address"].ToString();
			textBoxCity.Text = dataRow["City"].ToString();
			textBoxState.Text = dataRow["State"].ToString();
			textBoxCountry.Text = dataRow["Country"].ToString();
			textBoxPostalCode.Text = dataRow["PostalCode"].ToString();
			textBoxAddressPrintFormat.Text = dataRow["AddressPrintFormat"].ToString();
			textBoxPhone1.Text = dataRow["Phone1"].ToString();
			textBoxPhone2.Text = dataRow["Phone2"].ToString();
			textBoxFax.Text = dataRow["Fax"].ToString();
			textBoxMobile.Text = dataRow["Mobile"].ToString();
			textBoxEmail1.Text = dataRow["Email1"].ToString();
			textBoxEmail2.Text = dataRow["Email2"].ToString();
			textBoxWebsite.Text = dataRow["Website"].ToString();
			textBoxIMAddress.Text = dataRow["IMAddress"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			textBoxFacebook.Text = dataRow["Facebook"].ToString();
			textBoxTwitter.Text = dataRow["Twitter"].ToString();
			textBoxSkype.Text = dataRow["Skype"].ToString();
			textBoxLinkedIn.Text = dataRow["LinkedIn"].ToString();
			if (dataRow["Gender"].ToString() == "F")
			{
				comboBoxGender.SelectedIndex = 1;
			}
			else
			{
				comboBoxGender.SelectedIndex = 0;
			}
			if (dataRow["BirthDate"] != DBNull.Value)
			{
				datePickerBirthDate.Value = DateTime.Parse(dataRow["BirthDate"].ToString());
				datePickerBirthDate.Checked = true;
			}
			else
			{
				datePickerBirthDate.Checked = false;
			}
			if (dataRow["Anniversary"] != DBNull.Value)
			{
				datePickerAnniversary.Value = DateTime.Parse(dataRow["Anniversary"].ToString());
				datePickerAnniversary.Checked = true;
			}
			else
			{
				datePickerAnniversary.Checked = false;
			}
			textBoxSpouseName.Text = dataRow["SpouseName"].ToString();
			textBoxChildren.Text = dataRow["ChildrenName"].ToString();
			textBoxHobbies.Text = dataRow["Hobbies"].ToString();
			textBoxLanguages.Text = dataRow["Language"].ToString();
			textBoxBankName.Text = dataRow["BankName"].ToString();
			textBoxBankAccountNumber.Text = dataRow["BankAccountNumber"].ToString();
			if (dataRow["HasPhoto"] != DBNull.Value)
			{
				bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
				linkLoadImage.Visible = flag;
				linkRemovePicture.Enabled = flag;
				if (flag)
				{
					pictureBoxPhoto.Image = null;
				}
				else
				{
					pictureBoxPhoto.Image = pictureBoxNoImage.Image;
				}
			}
			else
			{
				linkLoadImage.Visible = false;
				pictureBoxPhoto.Image = pictureBoxNoImage.Image;
				linkRemovePicture.Enabled = false;
			}
			if (currentData.Tables["UDF"].Rows.Count > 0)
			{
				_ = currentData.Tables["UDF"].Rows[0];
				foreach (DataColumn column in currentData.Tables["UDF"].Columns)
				{
					_ = column;
				}
			}
			else
			{
				udfEntryGrid.ClearData();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ContactData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ContactTable.Rows[0] : currentData.ContactTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ContactID"] = textBoxCode.Text;
				dataRow["FirstName"] = textBoxFirstName.Text;
				dataRow["MiddleName"] = textBoxMiddleName.Text;
				dataRow["LastName"] = textBoxLastName.Text;
				dataRow["NickName"] = textBoxNickName.Text;
				dataRow["JobTitle"] = textBoxJobTitle.Text;
				dataRow["ManagerName"] = textBoxManagerName.Text;
				dataRow["AssistantName"] = textBoxAssistant.Text;
				dataRow["Inactive"] = checkBoxIsInactive.Checked;
				dataRow["Address"] = textBoxAddress.Text;
				dataRow["City"] = textBoxCity.Text;
				dataRow["State"] = textBoxState.Text;
				dataRow["Country"] = textBoxCountry.Text;
				dataRow["PostalCode"] = textBoxPostalCode.Text;
				dataRow["AddressPrintFormat"] = textBoxAddressPrintFormat.Text;
				dataRow["Phone1"] = textBoxPhone1.Text;
				dataRow["Phone2"] = textBoxPhone2.Text;
				dataRow["Fax"] = textBoxFax.Text;
				dataRow["Mobile"] = textBoxMobile.Text;
				dataRow["Email1"] = textBoxEmail1.Text;
				dataRow["Email2"] = textBoxEmail2.Text;
				dataRow["Website"] = textBoxWebsite.Text;
				dataRow["IMAddress"] = textBoxIMAddress.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Facebook"] = textBoxFacebook.Text;
				dataRow["Twitter"] = textBoxTwitter.Text;
				dataRow["Skype"] = textBoxSkype.Text;
				dataRow["LinkedIn"] = textBoxLinkedIn.Text;
				dataRow["Gender"].ToString();
				if (comboBoxGender.SelectedIndex == 1)
				{
					dataRow["Gender"] = "F";
				}
				else
				{
					dataRow["Gender"] = "M";
				}
				if (datePickerBirthDate.Checked)
				{
					dataRow["BirthDate"] = DateTime.Parse(datePickerBirthDate.Value.ToString());
				}
				else
				{
					dataRow["BirthDate"] = DBNull.Value;
				}
				if (datePickerAnniversary.Checked)
				{
					dataRow["Anniversary"] = DateTime.Parse(datePickerAnniversary.Value.ToString());
				}
				else
				{
					dataRow["Anniversary"] = DBNull.Value;
				}
				dataRow["SpouseName"] = textBoxSpouseName.Text;
				dataRow["ChildrenName"] = textBoxChildren.Text;
				dataRow["Hobbies"] = textBoxHobbies.Text;
				dataRow["Language"] = textBoxLanguages.Text;
				dataRow["BankName"] = textBoxBankName.Text;
				dataRow["BankAccountNumber"] = textBoxBankAccountNumber.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ContactTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			try
			{
				if (!screenRight.New && isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
					return false;
				}
				if (!screenRight.Edit && !isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Contact", "ContactID", textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("You dont have permission to edit.");
					return false;
				}
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (textBoxCode.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Contact", "ContactID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					textBoxCode.Focus();
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return true;
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel("Data has been changed.", "Do you want to save the changes?"))
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.ContactSystem.CreateContact(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Contact, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ContactSystem.UpdateContact(currentData);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id == "") && CanClose())
				{
					PublicFunctions.StartWaiting(this);
					currentData = Factory.ContactSystem.GetContactByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
						if (toolStripButtonShowPicture.Checked && linkLoadImage.Visible)
						{
							LoadPhoto();
						}
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetContactThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Sales;
		}

		public static int GetScreenID()
		{
			return 7003;
		}

		public void RefreshData()
		{
			Refresh();
			if (FormActivator.ProgramLoaded)
			{
				_ = Global.ConStatus;
				_ = 2;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Contact", "ContactID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Contact", "ContactID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Contact", "ContactID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Contact", "ContactID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Contact", "ContactID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.No)
				{
					return false;
				}
				return Factory.ContactSystem.DeleteContact(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Contact", "ContactID");
			textBoxFirstName.Clear();
			textBoxMiddleName.Clear();
			textBoxLastName.Clear();
			textBoxNickName.Clear();
			textBoxJobTitle.Clear();
			textBoxManagerName.Clear();
			textBoxAssistant.Clear();
			textBoxIMAddress.Clear();
			comboBoxGender.SelectedIndex = 0;
			datePickerBirthDate.Checked = false;
			datePickerAnniversary.Checked = false;
			textBoxSpouseName.Clear();
			textBoxChildren.Clear();
			textBoxHobbies.Clear();
			textBoxLanguages.Clear();
			textBoxBankName.Clear();
			textBoxBankAccountNumber.Clear();
			udfEntryGrid.ClearData();
			pictureBoxPhoto.Image = null;
			textBoxSkype.Clear();
			textBoxTwitter.Clear();
			textBoxFacebook.Clear();
			textBoxLinkedIn.Clear();
			textBoxNote.Clear();
			textBoxAddress.Clear();
			textBoxAddressPrintFormat.Clear();
			textBoxCity.Clear();
			textBoxCountry.Clear();
			textBoxEmail1.Clear();
			textBoxEmail2.Clear();
			textBoxFax.Clear();
			textBoxMobile.Clear();
			textBoxPhone1.Clear();
			textBoxPhone2.Clear();
			textBoxPostalCode.Clear();
			textBoxState.Clear();
			textBoxWebsite.Clear();
			checkBoxIsInactive.Checked = false;
			formManager.ResetDirty();
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

		public void OnActivated()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		private void ContactGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		public bool CanClose()
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

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddContactPhoto(textBoxCode.Text, image))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add image.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.ContactSystem.RemoveContactPhoto(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetContactThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Contact);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraGroupBox1_Click(object sender, EventArgs e)
		{
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
			EntityCategoryAssignDialog entityCategoryAssignDialog = new EntityCategoryAssignDialog();
			entityCategoryAssignDialog.EntityID = textBoxCode.Text;
			entityCategoryAssignDialog.EntityName = textBoxName.Text;
			entityCategoryAssignDialog.EntityType = EntityTypesEnum.Contacts;
			if (!screenRight.Edit)
			{
				entityCategoryAssignDialog.AllowEdit = false;
			}
			entityCategoryAssignDialog.ShowDialog(this);
		}
	}
}
