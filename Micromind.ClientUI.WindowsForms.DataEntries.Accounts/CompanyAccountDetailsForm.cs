using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class CompanyAccountDetailsForm : Form, IDataForm, IDataEntry
	{
		private CompanyAccountData currentData;

		private const string TABLENAME_CONST = "Account";

		private const string IDFIELD_CONST = "AccountID";

		private bool isNewRecord = true;

		private MMTextBox textBoxUserDefined1;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxAlias;

		private MMTextBox editName;

		private MMLabel label1;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private MMLabel labelAccountNumber;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

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

		private AccountGroupComboBox comboBoxAccountGroup;

		private MMLabel labelUD1;

		private MMTextBox textBoxUserDefined2;

		private MMLabel labelUD2;

		private MMTextBox textBoxUserDefined3;

		private MMLabel labelUD3;

		private MMTextBox textBoxUserDefined4;

		private MMLabel labelUD4;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel5;

		private FormManager formManager;

		private ComboBox comboBoxSubType;

		private MMLabel mmLabel6;

		private Panel panelBankAccountDetails;

		private MMLabel mmLabel8;

		private MMTextBox textBoxBankAccountNumber;

		private MMLabel mmLabel7;

		private ComboBox comboBoxBankAccountType;

		private MMTextBox textBoxGroupName;

		private MMTextBox textBoxType;

		private BankComboBox comboBoxBank;

		private MMTextBox textBoxBankName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private CurrencyComboBox comboBoxCurrency;

		private MMLabel mmLabel1;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonHistory;

		private Container components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1015;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string GroupID
		{
			set
			{
				comboBoxAccountGroup.SelectedID = value;
			}
		}

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
					toolStripButtonHistory.Visible = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					toolStripButtonHistory.Visible = true;
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

		public CompanyAccountDetailsForm()
		{
			InitializeComponent();
			comboBoxSubType.Items.Clear();
			comboBoxSubType.Items.Add("General");
			comboBoxSubType.Items.Add("Cash");
			comboBoxSubType.Items.Add("Bank");
			comboBoxSubType.Items.Add("Account Receivable");
			comboBoxSubType.Items.Add("Account Payable");
			comboBoxSubType.Items.Add("Inventory");
			comboBoxSubType.Items.Add("Sales");
			comboBoxSubType.Items.Add("Cost of Goods Sold");
			comboBoxSubType.Items.Add("Work In Progress");
			comboBoxSubType.Items.Add("Other Income");
			comboBoxSubType.Items.Add("Other Optional Expense");
			comboBoxSubType.Items.Add("Other Non Operational Expense");
			comboBoxSubType.SelectedIndex = 0;
			comboBoxBankAccountType.Items.Clear();
			comboBoxBankAccountType.Items.Add("Current Account");
			comboBoxBankAccountType.Items.Add("Saving Account");
			comboBoxBankAccountType.Items.Add("Other");
			comboBoxBankAccountType.SelectedIndex = 0;
			LoadCustomFieldsName();
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
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.CompanyAccountDetailsForm));
			textBoxUserDefined1 = new Micromind.UISupport.MMTextBox();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxAlias = new Micromind.UISupport.MMTextBox();
			editName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelAccountNumber = new Micromind.UISupport.MMLabel();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonHistory = new System.Windows.Forms.ToolStripButton();
			comboBoxAccountGroup = new Micromind.DataControls.AccountGroupComboBox();
			labelUD1 = new Micromind.UISupport.MMLabel();
			textBoxUserDefined2 = new Micromind.UISupport.MMTextBox();
			labelUD2 = new Micromind.UISupport.MMLabel();
			textBoxUserDefined3 = new Micromind.UISupport.MMTextBox();
			labelUD3 = new Micromind.UISupport.MMLabel();
			textBoxUserDefined4 = new Micromind.UISupport.MMTextBox();
			labelUD4 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxSubType = new System.Windows.Forms.ComboBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			panelBankAccountDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxBankAccountNumber = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			comboBoxBankAccountType = new System.Windows.Forms.ComboBox();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			textBoxGroupName = new Micromind.UISupport.MMTextBox();
			textBoxType = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).BeginInit();
			panelBankAccountDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			SuspendLayout();
			textBoxUserDefined1.BackColor = System.Drawing.Color.White;
			textBoxUserDefined1.CustomReportFieldName = "";
			textBoxUserDefined1.CustomReportKey = "";
			textBoxUserDefined1.CustomReportValueType = 1;
			textBoxUserDefined1.IsComboTextBox = false;
			textBoxUserDefined1.IsModified = false;
			textBoxUserDefined1.Location = new System.Drawing.Point(137, 223);
			textBoxUserDefined1.MaxLength = 50;
			textBoxUserDefined1.Name = "textBoxUserDefined1";
			textBoxUserDefined1.Size = new System.Drawing.Size(196, 20);
			textBoxUserDefined1.TabIndex = 9;
			lblDescriptions.AutoSize = true;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(12, 102);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(33, 13);
			lblDescriptions.TabIndex = 7;
			lblDescriptions.Text = "Alias:";
			textBoxAlias.BackColor = System.Drawing.Color.White;
			textBoxAlias.CustomReportFieldName = "";
			textBoxAlias.CustomReportKey = "";
			textBoxAlias.CustomReportValueType = 1;
			textBoxAlias.IsComboTextBox = false;
			textBoxAlias.IsModified = false;
			textBoxAlias.Location = new System.Drawing.Point(137, 99);
			textBoxAlias.MaxLength = 64;
			textBoxAlias.Name = "textBoxAlias";
			textBoxAlias.Size = new System.Drawing.Size(442, 20);
			textBoxAlias.TabIndex = 4;
			editName.BackColor = System.Drawing.Color.White;
			editName.CustomReportFieldName = "";
			editName.CustomReportKey = "";
			editName.CustomReportValueType = 1;
			editName.IsComboTextBox = false;
			editName.IsModified = false;
			editName.IsRequired = true;
			editName.Location = new System.Drawing.Point(137, 77);
			editName.MaxLength = 64;
			editName.Name = "editName";
			editName.Size = new System.Drawing.Size(442, 20);
			editName.TabIndex = 3;
			label1.AutoSize = true;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = true;
			label1.Location = new System.Drawing.Point(12, 80);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(91, 13);
			label1.TabIndex = 3;
			label1.Text = "Account Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxIsInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(412, 57);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(128, 16);
			checkBoxIsInactive.TabIndex = 2;
			checkBoxIsInactive.Text = "Account is inac&tive";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(137, 55);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(269, 20);
			textBoxCode.TabIndex = 1;
			labelAccountNumber.AutoSize = true;
			labelAccountNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAccountNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelAccountNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelAccountNumber.IsFieldHeader = false;
			labelAccountNumber.IsRequired = true;
			labelAccountNumber.Location = new System.Drawing.Point(12, 57);
			labelAccountNumber.Name = "labelAccountNumber";
			labelAccountNumber.PenWidth = 1f;
			labelAccountNumber.ShowBorder = false;
			labelAccountNumber.Size = new System.Drawing.Size(87, 13);
			labelAccountNumber.TabIndex = 0;
			labelAccountNumber.Text = "Account Code:";
			labelAccountNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 321);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(593, 40);
			panelButtons.TabIndex = 13;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(593, 1);
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
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(483, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator2,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator3,
				toolStripButtonInformation,
				toolStripButtonHistory
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(593, 31);
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
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripButtonHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonHistory.Image = Micromind.ClientUI.Properties.Resources.historyIcon24x24;
			toolStripButtonHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonHistory.Name = "toolStripButtonHistory";
			toolStripButtonHistory.Size = new System.Drawing.Size(28, 28);
			toolStripButtonHistory.Text = "toolStripButton1";
			toolStripButtonHistory.ToolTipText = "Version";
			toolStripButtonHistory.Click += new System.EventHandler(toolStripButtonHistory_Click);
			comboBoxAccountGroup.Assigned = false;
			comboBoxAccountGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccountGroup.CustomReportFieldName = "";
			comboBoxAccountGroup.CustomReportKey = "";
			comboBoxAccountGroup.CustomReportValueType = 1;
			comboBoxAccountGroup.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccountGroup.DisplayLayout.Appearance = appearance;
			comboBoxAccountGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccountGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAccountGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccountGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccountGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccountGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAccountGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccountGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccountGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAccountGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccountGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAccountGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAccountGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccountGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccountGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAccountGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccountGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxAccountGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccountGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccountGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccountGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccountGroup.Editable = true;
			comboBoxAccountGroup.FilterString = "";
			comboBoxAccountGroup.HasAllAccount = false;
			comboBoxAccountGroup.HasCustom = false;
			comboBoxAccountGroup.IsDataLoaded = false;
			comboBoxAccountGroup.Location = new System.Drawing.Point(137, 33);
			comboBoxAccountGroup.MaxDropDownItems = 12;
			comboBoxAccountGroup.Name = "comboBoxAccountGroup";
			comboBoxAccountGroup.ShowInactiveItems = false;
			comboBoxAccountGroup.ShowQuickAdd = true;
			comboBoxAccountGroup.Size = new System.Drawing.Size(135, 20);
			comboBoxAccountGroup.TabIndex = 0;
			comboBoxAccountGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAccountGroup.SelectedIndexChanged += new System.EventHandler(comboBoxAccountGroup_SelectedIndexChanged);
			labelUD1.AutoSize = true;
			labelUD1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelUD1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelUD1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelUD1.IsFieldHeader = false;
			labelUD1.IsRequired = false;
			labelUD1.Location = new System.Drawing.Point(12, 226);
			labelUD1.Name = "labelUD1";
			labelUD1.PenWidth = 1f;
			labelUD1.ShowBorder = false;
			labelUD1.Size = new System.Drawing.Size(81, 13);
			labelUD1.TabIndex = 11;
			labelUD1.Text = "User-Defined 1:";
			textBoxUserDefined2.BackColor = System.Drawing.Color.White;
			textBoxUserDefined2.CustomReportFieldName = "";
			textBoxUserDefined2.CustomReportKey = "";
			textBoxUserDefined2.CustomReportValueType = 1;
			textBoxUserDefined2.IsComboTextBox = false;
			textBoxUserDefined2.IsModified = false;
			textBoxUserDefined2.Location = new System.Drawing.Point(137, 245);
			textBoxUserDefined2.MaxLength = 50;
			textBoxUserDefined2.Name = "textBoxUserDefined2";
			textBoxUserDefined2.Size = new System.Drawing.Size(196, 20);
			textBoxUserDefined2.TabIndex = 10;
			labelUD2.AutoSize = true;
			labelUD2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelUD2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelUD2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelUD2.IsFieldHeader = false;
			labelUD2.IsRequired = false;
			labelUD2.Location = new System.Drawing.Point(12, 248);
			labelUD2.Name = "labelUD2";
			labelUD2.PenWidth = 1f;
			labelUD2.ShowBorder = false;
			labelUD2.Size = new System.Drawing.Size(81, 13);
			labelUD2.TabIndex = 13;
			labelUD2.Text = "User-Defined 2:";
			textBoxUserDefined3.BackColor = System.Drawing.Color.White;
			textBoxUserDefined3.CustomReportFieldName = "";
			textBoxUserDefined3.CustomReportKey = "";
			textBoxUserDefined3.CustomReportValueType = 1;
			textBoxUserDefined3.IsComboTextBox = false;
			textBoxUserDefined3.IsModified = false;
			textBoxUserDefined3.Location = new System.Drawing.Point(137, 267);
			textBoxUserDefined3.MaxLength = 50;
			textBoxUserDefined3.Name = "textBoxUserDefined3";
			textBoxUserDefined3.Size = new System.Drawing.Size(196, 20);
			textBoxUserDefined3.TabIndex = 11;
			labelUD3.AutoSize = true;
			labelUD3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelUD3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelUD3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelUD3.IsFieldHeader = false;
			labelUD3.IsRequired = false;
			labelUD3.Location = new System.Drawing.Point(12, 270);
			labelUD3.Name = "labelUD3";
			labelUD3.PenWidth = 1f;
			labelUD3.ShowBorder = false;
			labelUD3.Size = new System.Drawing.Size(81, 13);
			labelUD3.TabIndex = 15;
			labelUD3.Text = "User-Defined 3:";
			textBoxUserDefined4.BackColor = System.Drawing.Color.White;
			textBoxUserDefined4.CustomReportFieldName = "";
			textBoxUserDefined4.CustomReportKey = "";
			textBoxUserDefined4.CustomReportValueType = 1;
			textBoxUserDefined4.IsComboTextBox = false;
			textBoxUserDefined4.IsModified = false;
			textBoxUserDefined4.Location = new System.Drawing.Point(137, 289);
			textBoxUserDefined4.MaxLength = 50;
			textBoxUserDefined4.Name = "textBoxUserDefined4";
			textBoxUserDefined4.Size = new System.Drawing.Size(196, 20);
			textBoxUserDefined4.TabIndex = 12;
			labelUD4.AutoSize = true;
			labelUD4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelUD4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelUD4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelUD4.IsFieldHeader = false;
			labelUD4.IsRequired = false;
			labelUD4.Location = new System.Drawing.Point(12, 292);
			labelUD4.Name = "labelUD4";
			labelUD4.PenWidth = 1f;
			labelUD4.ShowBorder = false;
			labelUD4.Size = new System.Drawing.Size(81, 13);
			labelUD4.TabIndex = 17;
			labelUD4.Text = "User-Defined 4:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(137, 121);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(442, 20);
			textBoxNote.TabIndex = 5;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(12, 123);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(34, 13);
			mmLabel5.TabIndex = 9;
			mmLabel5.Text = "Note:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 307;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			comboBoxSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSubType.FormattingEnabled = true;
			comboBoxSubType.Location = new System.Drawing.Point(137, 143);
			comboBoxSubType.Name = "comboBoxSubType";
			comboBoxSubType.Size = new System.Drawing.Size(137, 21);
			comboBoxSubType.TabIndex = 6;
			comboBoxSubType.SelectedIndexChanged += new System.EventHandler(comboBoxSubType_SelectedIndexChanged);
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = true;
			mmLabel6.Location = new System.Drawing.Point(12, 146);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(62, 13);
			mmLabel6.TabIndex = 309;
			mmLabel6.Text = "Category:";
			panelBankAccountDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelBankAccountDetails.Controls.Add(comboBoxBank);
			panelBankAccountDetails.Controls.Add(mmLabel8);
			panelBankAccountDetails.Controls.Add(textBoxBankAccountNumber);
			panelBankAccountDetails.Controls.Add(mmLabel7);
			panelBankAccountDetails.Controls.Add(comboBoxBankAccountType);
			panelBankAccountDetails.Controls.Add(textBoxBankName);
			panelBankAccountDetails.Location = new System.Drawing.Point(12, 166);
			panelBankAccountDetails.Name = "panelBankAccountDetails";
			panelBankAccountDetails.Size = new System.Drawing.Size(519, 51);
			panelBankAccountDetails.TabIndex = 8;
			panelBankAccountDetails.Visible = false;
			appearance13.FontData.BoldAsString = "False";
			ultraFormattedLinkLabel1.Appearance = appearance13;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(0, 4);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(32, 14);
			ultraFormattedLinkLabel1.TabIndex = 12;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bank:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxBank.Assigned = false;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance15;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(125, 1);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(137, 20);
			comboBoxBank.TabIndex = 0;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBank.SelectedIndexChanged += new System.EventHandler(comboBoxBank_SelectedIndexChanged);
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(268, 26);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(58, 13);
			mmLabel8.TabIndex = 11;
			mmLabel8.Text = "Account#:";
			textBoxBankAccountNumber.BackColor = System.Drawing.Color.White;
			textBoxBankAccountNumber.CustomReportFieldName = "";
			textBoxBankAccountNumber.CustomReportKey = "";
			textBoxBankAccountNumber.CustomReportValueType = 1;
			textBoxBankAccountNumber.IsComboTextBox = false;
			textBoxBankAccountNumber.IsModified = false;
			textBoxBankAccountNumber.Location = new System.Drawing.Point(327, 23);
			textBoxBankAccountNumber.MaxLength = 20;
			textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
			textBoxBankAccountNumber.Size = new System.Drawing.Size(182, 20);
			textBoxBankAccountNumber.TabIndex = 2;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(0, 26);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(103, 13);
			mmLabel7.TabIndex = 10;
			mmLabel7.Text = "Bank Account Type:";
			comboBoxBankAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxBankAccountType.FormattingEnabled = true;
			comboBoxBankAccountType.Location = new System.Drawing.Point(125, 23);
			comboBoxBankAccountType.Name = "comboBoxBankAccountType";
			comboBoxBankAccountType.Size = new System.Drawing.Size(137, 21);
			comboBoxBankAccountType.TabIndex = 1;
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.IsRequired = true;
			textBoxBankName.Location = new System.Drawing.Point(264, 1);
			textBoxBankName.MaxLength = 64;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.ReadOnly = true;
			textBoxBankName.Size = new System.Drawing.Size(245, 20);
			textBoxBankName.TabIndex = 2;
			textBoxBankName.TabStop = false;
			textBoxGroupName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGroupName.CustomReportFieldName = "";
			textBoxGroupName.CustomReportKey = "";
			textBoxGroupName.CustomReportValueType = 1;
			textBoxGroupName.IsComboTextBox = false;
			textBoxGroupName.IsModified = false;
			textBoxGroupName.IsRequired = true;
			textBoxGroupName.Location = new System.Drawing.Point(274, 33);
			textBoxGroupName.MaxLength = 64;
			textBoxGroupName.Name = "textBoxGroupName";
			textBoxGroupName.ReadOnly = true;
			textBoxGroupName.Size = new System.Drawing.Size(217, 20);
			textBoxGroupName.TabIndex = 2;
			textBoxGroupName.TabStop = false;
			textBoxType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxType.CustomReportFieldName = "";
			textBoxType.CustomReportKey = "";
			textBoxType.CustomReportValueType = 1;
			textBoxType.IsComboTextBox = false;
			textBoxType.IsModified = false;
			textBoxType.IsRequired = true;
			textBoxType.Location = new System.Drawing.Point(493, 33);
			textBoxType.MaxLength = 64;
			textBoxType.Name = "textBoxType";
			textBoxType.ReadOnly = true;
			textBoxType.Size = new System.Drawing.Size(86, 20);
			textBoxType.TabIndex = 2;
			textBoxType.TabStop = false;
			appearance27.FontData.BoldAsString = "True";
			appearance27.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance27;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 35);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkLabel4.TabIndex = 310;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Account Group:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance29;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(383, 143);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(138, 20);
			comboBoxCurrency.TabIndex = 7;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(280, 146);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(97, 13);
			mmLabel1.TabIndex = 312;
			mmLabel1.Text = "Account Currency:";
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(593, 361);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(comboBoxCurrency);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(panelBankAccountDetails);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(comboBoxSubType);
			base.Controls.Add(formManager);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(lblDescriptions);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(checkBoxIsInactive);
			base.Controls.Add(textBoxAlias);
			base.Controls.Add(labelUD4);
			base.Controls.Add(labelUD3);
			base.Controls.Add(labelUD2);
			base.Controls.Add(labelUD1);
			base.Controls.Add(textBoxUserDefined4);
			base.Controls.Add(textBoxUserDefined3);
			base.Controls.Add(textBoxUserDefined2);
			base.Controls.Add(textBoxUserDefined1);
			base.Controls.Add(comboBoxAccountGroup);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxType);
			base.Controls.Add(textBoxGroupName);
			base.Controls.Add(editName);
			base.Controls.Add(labelAccountNumber);
			base.Controls.Add(textBoxCode);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "CompanyAccountDetailsForm";
			Text = "Account Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(CompanyAccountDetailsForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).EndInit();
			panelBankAccountDetails.ResumeLayout(false);
			panelBankAccountDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
		}

		private void AddEvents()
		{
		}

		private void CompanyAccountDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
					IsNewRecord = true;
					ClearForm();
					comboBoxAccountGroup.Focus();
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

		private void LoadCustomFieldsName()
		{
			try
			{
				DataSet dataByFields = Factory.DatabaseSystem.GetDataByFields("Company", "CompanyID", "1", "AccountUD1", "AccountUD2", "AccountUD3", "AccountUD4");
				if (dataByFields != null && dataByFields.Tables.Count != 0 && dataByFields.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = dataByFields.Tables[0].Rows[0];
					if (dataRow["AccountUD1"] != DBNull.Value && dataRow["AccountUD1"].ToString() != "")
					{
						labelUD1.Text = dataRow["AccountUD1"].ToString() + ":";
					}
					if (dataRow["AccountUD2"] != DBNull.Value && dataRow["AccountUD2"].ToString() != "")
					{
						labelUD2.Text = dataRow["AccountUD2"].ToString() + ":";
					}
					if (dataRow["AccountUD3"] != DBNull.Value && dataRow["AccountUD3"].ToString() != "")
					{
						labelUD3.Text = dataRow["AccountUD3"].ToString() + ":";
					}
					if (dataRow["AccountUD4"] != DBNull.Value && dataRow["AccountUD4"].ToString() != "")
					{
						labelUD4.Text = dataRow["AccountUD4"].ToString() + ":";
					}
				}
			}
			catch
			{
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.CompanyAccountTable.Rows[0];
					comboBoxAccountGroup.SelectedID = dataRow["GroupID"].ToString();
					editName.Text = dataRow["AccountName"].ToString();
					textBoxCode.Text = dataRow["AccountID"].ToString();
					textBoxAlias.Text = dataRow["Alias"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					if (comboBoxCurrency.SelectedID == "")
					{
						comboBoxCurrency.SelectedID = Global.BaseCurrencyID;
					}
					if (dataRow["SubType"] != DBNull.Value)
					{
						comboBoxSubType.SelectedIndex = checked(int.Parse(dataRow["SubType"].ToString()) - 1);
					}
					else
					{
						comboBoxSubType.SelectedIndex = 0;
					}
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxIsInactive.Checked = false;
					}
					string a = dataRow["BankAccountType"].ToString();
					if (a == "C")
					{
						comboBoxBankAccountType.SelectedIndex = 0;
					}
					else if (a == "S")
					{
						comboBoxBankAccountType.SelectedIndex = 1;
					}
					else
					{
						comboBoxBankAccountType.SelectedIndex = 2;
					}
					textBoxBankAccountNumber.Text = dataRow["BankAccountNumber"].ToString();
					string a2 = dataRow["CurrencyID"].ToString();
					if (a2 == "")
					{
						a2 = Global.BaseCurrencyID;
					}
					bool flag = false;
					int result = 0;
					int.TryParse(dataRow["HasTransaction"].ToString(), out result);
					if (result > 0)
					{
						flag = true;
					}
					if (a2 != Global.BaseCurrencyID && flag)
					{
						comboBoxCurrency.Enabled = false;
					}
					else
					{
						comboBoxCurrency.Enabled = true;
					}
					textBoxUserDefined1.Text = dataRow["UserDefined1"].ToString();
					textBoxUserDefined2.Text = dataRow["UserDefined2"].ToString();
					textBoxUserDefined3.Text = dataRow["UserDefined3"].ToString();
					textBoxUserDefined4.Text = dataRow["UserDefined4"].ToString();
				}
			}
			catch
			{
				throw;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CompanyAccountData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CompanyAccountTable.Rows[0] : currentData.CompanyAccountTable.NewRow();
				dataRow.BeginEdit();
				dataRow["AccountID"] = textBoxCode.Text;
				dataRow["Alias"] = textBoxAlias.Text;
				dataRow["AccountName"] = editName.Text.Trim();
				dataRow["SubType"] = checked(comboBoxSubType.SelectedIndex + 1);
				dataRow["GroupID"] = comboBoxAccountGroup.SelectedID;
				dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				if (panelBankAccountDetails.Visible)
				{
					if (comboBoxBankAccountType.SelectedIndex == 0)
					{
						dataRow["BankAccountType"] = "C";
					}
					else if (comboBoxBankAccountType.SelectedIndex == 0)
					{
						dataRow["BankAccountType"] = "S";
					}
					else if (comboBoxBankAccountType.SelectedIndex == 0)
					{
						dataRow["BankAccountType"] = "O";
					}
					dataRow["BankAccountNumber"] = textBoxBankAccountNumber.Text;
				}
				else
				{
					dataRow["BankAccountType"] = DBNull.Value;
					dataRow["BankAccountNumber"] = DBNull.Value;
				}
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["UserDefined1"] = textBoxUserDefined1.Text;
				dataRow["UserDefined2"] = textBoxUserDefined2.Text;
				dataRow["UserDefined3"] = textBoxUserDefined3.Text;
				dataRow["UserDefined4"] = textBoxUserDefined4.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CompanyAccountTable.Rows.Add(dataRow);
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
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Account", "AccountID", textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("You dont have permission to edit.");
					return false;
				}
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (editName.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					editName.Focus();
					editName.SelectAll();
					return false;
				}
				if (isNewRecord && (Factory.DatabaseSystem.ExistFieldValue("Account", "AccountID", textBoxCode.Text.Trim()) || Factory.DatabaseSystem.ExistFieldValue("Account_Group", "GroupID", textBoxCode.Text.Trim())))
				{
					ErrorHelper.InformationMessage("There is already an account or account group with the same code.\nPlease enter another code.");
					textBoxCode.Focus();
					return false;
				}
				if (textBoxCode.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (comboBoxAccountGroup.SelectedID == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					comboBoxAccountGroup.Focus();
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
					flag = Factory.CompanyAccountSystem.CreateCompanyAccount(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Accounts, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CompanyAccountSystem.UpdateCompanyAccount(currentData);
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
					currentData = Factory.CompanyAccountSystem.GetAccountByID(id);
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
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				ClearForm();
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Accounts;
		}

		public static int GetScreenID()
		{
			return 7003;
		}

		public void RefreshData()
		{
			Application.DoEvents();
			Refresh();
			if (FormActivator.ProgramLoaded && Global.ConStatus != ConnectionStatus.DisConnected)
			{
				SetSecurity();
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Account", "AccountID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Account", "AccountID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Account", "AccountID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Account", "AccountID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Account", "AccountID", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.CompanyAccountSystem.DeleteAccount(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Account", "AccountID");
			editName.Clear();
			textBoxNote.Clear();
			comboBoxBank.Clear();
			textBoxBankAccountNumber.Clear();
			comboBoxBankAccountType.SelectedIndex = 0;
			textBoxBankName.Clear();
			textBoxAlias.Clear();
			checkBoxIsInactive.Checked = false;
			textBoxUserDefined1.Clear();
			textBoxUserDefined2.Clear();
			textBoxUserDefined3.Clear();
			textBoxUserDefined4.Clear();
			comboBoxCurrency.Enabled = true;
			comboBoxCurrency.SelectedID = Global.BaseCurrencyID;
			if (IsNewRecord && comboBoxAccountGroup.SelectedID != "")
			{
				textBoxCode.Text = Factory.CompanyAccountSystem.GetNextAccountNumber(comboBoxAccountGroup.SelectedID);
			}
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
			comboBoxAccountGroup.Focus();
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void comboBoxSubType_SelectedIndexChanged(object sender, EventArgs e)
		{
			panelBankAccountDetails.Visible = (comboBoxSubType.SelectedIndex == 2);
		}

		private void comboBoxAccountGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxType.Text = comboBoxAccountGroup.SelectedTypeName;
			textBoxGroupName.Text = comboBoxAccountGroup.SelectedName;
			if (IsNewRecord && comboBoxAccountGroup.SelectedID != "")
			{
				textBoxCode.Text = Factory.CompanyAccountSystem.GetNextAccountNumber(comboBoxAccountGroup.SelectedID);
			}
		}

		private void comboBoxBank_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxBankName.Text = comboBoxBank.SelectedName;
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccountGroup(comboBoxAccountGroup.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBank(comboBoxBank.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CompanyAccountsListFormObj);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = editName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Acccounts;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonHistory_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				DocumentVersionList documentVersionList = new DocumentVersionList();
				documentVersionList.LoadData(currentData, ScreenTypes.Card, 4, "", textBoxCode.Text);
				documentVersionList.ShowDialog();
			}
		}
	}
}
