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
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class BankDetailsForm : Form, IForm
	{
		private BankData currentData;

		private const string TABLENAME_CONST = "Bank";

		private const string IDFIELD_CONST = "BankID";

		private bool isNewRecord = true;

		private CheckBox editIsDiscontinued;

		private MMTextBox editPostalCode;

		private MMTextBox editCountry;

		private MMTextBox editContactName;

		private MMTextBox editBankName;

		private Label label3;

		private MMTextBox editAddress3;

		private MMTextBox editAddress2;

		private Label lblAddress;

		private Label lblCity;

		private MMTextBox editAddress;

		private MMTextBox editContactTitle;

		private MMTextBox editCity;

		private Label lblPostalCode;

		private Label lblCountry;

		private MMTextBox editPhone;

		private MMTextBox editFax;

		private Label label2;

		private Label lblNotes;

		private MMTextBox editNotes;

		private Label lblPhone;

		private Label label21;

		private MMTextBox editState;

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

		private MMTextBox textBoxCode;

		private MMLabel labelCode;

		private MMLabel mmLabel1;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMTextBox textBoxRoutingCode;

		private Label label1;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private MMTextBox textBoxTaxIDNumber;

		private MMLabel mmLabel58;

		private Container components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1032;

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
				textBoxCode.Focus();
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

		public BankDetailsForm()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.BankDetailsForm));
			label21 = new System.Windows.Forms.Label();
			editState = new Micromind.UISupport.MMTextBox();
			editIsDiscontinued = new System.Windows.Forms.CheckBox();
			editPostalCode = new Micromind.UISupport.MMTextBox();
			editCountry = new Micromind.UISupport.MMTextBox();
			editContactName = new Micromind.UISupport.MMTextBox();
			editBankName = new Micromind.UISupport.MMTextBox();
			label3 = new System.Windows.Forms.Label();
			editAddress3 = new Micromind.UISupport.MMTextBox();
			editAddress2 = new Micromind.UISupport.MMTextBox();
			lblAddress = new System.Windows.Forms.Label();
			lblCity = new System.Windows.Forms.Label();
			editAddress = new Micromind.UISupport.MMTextBox();
			editContactTitle = new Micromind.UISupport.MMTextBox();
			editCity = new Micromind.UISupport.MMTextBox();
			lblPostalCode = new System.Windows.Forms.Label();
			lblCountry = new System.Windows.Forms.Label();
			editPhone = new Micromind.UISupport.MMTextBox();
			editFax = new Micromind.UISupport.MMTextBox();
			label2 = new System.Windows.Forms.Label();
			lblNotes = new System.Windows.Forms.Label();
			editNotes = new Micromind.UISupport.MMTextBox();
			lblPhone = new System.Windows.Forms.Label();
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
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelCode = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxRoutingCode = new Micromind.UISupport.MMTextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxTaxIDNumber = new Micromind.UISupport.MMTextBox();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			label21.AutoSize = true;
			label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label21.Location = new System.Drawing.Point(9, 235);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(82, 13);
			label21.TabIndex = 9;
			label21.Text = "State/Province:";
			editState.BackColor = System.Drawing.Color.White;
			editState.CustomReportFieldName = "";
			editState.CustomReportKey = "";
			editState.CustomReportValueType = 1;
			editState.IsComboTextBox = false;
			editState.IsModified = false;
			editState.Location = new System.Drawing.Point(124, 233);
			editState.MaxLength = 30;
			editState.Name = "editState";
			editState.Size = new System.Drawing.Size(152, 20);
			editState.TabIndex = 14;
			editIsDiscontinued.FlatStyle = System.Windows.Forms.FlatStyle.System;
			editIsDiscontinued.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			editIsDiscontinued.Location = new System.Drawing.Point(394, 39);
			editIsDiscontinued.Name = "editIsDiscontinued";
			editIsDiscontinued.Size = new System.Drawing.Size(104, 16);
			editIsDiscontinued.TabIndex = 2;
			editIsDiscontinued.Text = "Inactive";
			editPostalCode.BackColor = System.Drawing.Color.White;
			editPostalCode.CustomReportFieldName = "";
			editPostalCode.CustomReportKey = "";
			editPostalCode.CustomReportValueType = 1;
			editPostalCode.IsComboTextBox = false;
			editPostalCode.IsModified = false;
			editPostalCode.Location = new System.Drawing.Point(372, 233);
			editPostalCode.MaxLength = 10;
			editPostalCode.Name = "editPostalCode";
			editPostalCode.Size = new System.Drawing.Size(120, 20);
			editPostalCode.TabIndex = 16;
			editCountry.BackColor = System.Drawing.Color.White;
			editCountry.CustomReportFieldName = "";
			editCountry.CustomReportKey = "";
			editCountry.CustomReportValueType = 1;
			editCountry.IsComboTextBox = false;
			editCountry.IsModified = false;
			editCountry.Location = new System.Drawing.Point(124, 255);
			editCountry.MaxLength = 15;
			editCountry.Name = "editCountry";
			editCountry.Size = new System.Drawing.Size(368, 20);
			editCountry.TabIndex = 17;
			editContactName.BackColor = System.Drawing.Color.White;
			editContactName.CustomReportFieldName = "";
			editContactName.CustomReportKey = "";
			editContactName.CustomReportValueType = 1;
			editContactName.IsComboTextBox = false;
			editContactName.IsModified = false;
			editContactName.Location = new System.Drawing.Point(236, 81);
			editContactName.MaxLength = 64;
			editContactName.Name = "editContactName";
			editContactName.Size = new System.Drawing.Size(256, 20);
			editContactName.TabIndex = 7;
			editBankName.BackColor = System.Drawing.Color.White;
			editBankName.CustomReportFieldName = "";
			editBankName.CustomReportKey = "";
			editBankName.CustomReportValueType = 1;
			editBankName.IsComboTextBox = false;
			editBankName.IsModified = false;
			editBankName.Location = new System.Drawing.Point(124, 59);
			editBankName.MaxLength = 64;
			editBankName.Name = "editBankName";
			editBankName.Size = new System.Drawing.Size(368, 20);
			editBankName.TabIndex = 4;
			label3.AutoSize = true;
			label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label3.Location = new System.Drawing.Point(9, 83);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(103, 13);
			label3.TabIndex = 5;
			label3.Text = "Contact Title/Name:";
			editAddress3.BackColor = System.Drawing.Color.White;
			editAddress3.CustomReportFieldName = "";
			editAddress3.CustomReportKey = "";
			editAddress3.CustomReportValueType = 1;
			editAddress3.IsComboTextBox = false;
			editAddress3.IsModified = false;
			editAddress3.Location = new System.Drawing.Point(124, 189);
			editAddress3.MaxLength = 64;
			editAddress3.Name = "editAddress3";
			editAddress3.Size = new System.Drawing.Size(368, 20);
			editAddress3.TabIndex = 12;
			editAddress2.BackColor = System.Drawing.Color.White;
			editAddress2.CustomReportFieldName = "";
			editAddress2.CustomReportKey = "";
			editAddress2.CustomReportValueType = 1;
			editAddress2.IsComboTextBox = false;
			editAddress2.IsModified = false;
			editAddress2.Location = new System.Drawing.Point(124, 167);
			editAddress2.MaxLength = 64;
			editAddress2.Name = "editAddress2";
			editAddress2.Size = new System.Drawing.Size(368, 20);
			editAddress2.TabIndex = 11;
			lblAddress.AutoSize = true;
			lblAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblAddress.Location = new System.Drawing.Point(9, 147);
			lblAddress.Name = "lblAddress";
			lblAddress.Size = new System.Drawing.Size(48, 13);
			lblAddress.TabIndex = 8;
			lblAddress.Text = "Address:";
			lblCity.AutoSize = true;
			lblCity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblCity.Location = new System.Drawing.Point(9, 211);
			lblCity.Name = "lblCity";
			lblCity.Size = new System.Drawing.Size(27, 13);
			lblCity.TabIndex = 6;
			lblCity.Text = "City:";
			editAddress.BackColor = System.Drawing.Color.White;
			editAddress.CustomReportFieldName = "";
			editAddress.CustomReportKey = "";
			editAddress.CustomReportValueType = 1;
			editAddress.IsComboTextBox = false;
			editAddress.IsModified = false;
			editAddress.Location = new System.Drawing.Point(124, 145);
			editAddress.MaxLength = 64;
			editAddress.Name = "editAddress";
			editAddress.Size = new System.Drawing.Size(368, 20);
			editAddress.TabIndex = 10;
			editContactTitle.BackColor = System.Drawing.Color.White;
			editContactTitle.CustomReportFieldName = "";
			editContactTitle.CustomReportKey = "";
			editContactTitle.CustomReportValueType = 1;
			editContactTitle.IsComboTextBox = false;
			editContactTitle.IsModified = false;
			editContactTitle.Location = new System.Drawing.Point(124, 81);
			editContactTitle.MaxLength = 30;
			editContactTitle.Name = "editContactTitle";
			editContactTitle.Size = new System.Drawing.Size(104, 20);
			editContactTitle.TabIndex = 6;
			editCity.BackColor = System.Drawing.Color.White;
			editCity.CustomReportFieldName = "";
			editCity.CustomReportKey = "";
			editCity.CustomReportValueType = 1;
			editCity.IsComboTextBox = false;
			editCity.IsModified = false;
			editCity.Location = new System.Drawing.Point(124, 211);
			editCity.MaxLength = 30;
			editCity.Name = "editCity";
			editCity.Size = new System.Drawing.Size(368, 20);
			editCity.TabIndex = 13;
			lblPostalCode.AutoSize = true;
			lblPostalCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPostalCode.Location = new System.Drawing.Point(284, 237);
			lblPostalCode.Name = "lblPostalCode";
			lblPostalCode.Size = new System.Drawing.Size(87, 13);
			lblPostalCode.TabIndex = 15;
			lblPostalCode.Text = "Zip/Postal Code:";
			lblCountry.AutoSize = true;
			lblCountry.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblCountry.Location = new System.Drawing.Point(9, 257);
			lblCountry.Name = "lblCountry";
			lblCountry.Size = new System.Drawing.Size(46, 13);
			lblCountry.TabIndex = 12;
			lblCountry.Text = "Country:";
			editPhone.BackColor = System.Drawing.Color.White;
			editPhone.CustomReportFieldName = "";
			editPhone.CustomReportKey = "";
			editPhone.CustomReportValueType = 1;
			editPhone.IsComboTextBox = false;
			editPhone.IsModified = false;
			editPhone.Location = new System.Drawing.Point(124, 277);
			editPhone.MaxLength = 24;
			editPhone.Name = "editPhone";
			editPhone.Size = new System.Drawing.Size(152, 20);
			editPhone.TabIndex = 18;
			editFax.BackColor = System.Drawing.Color.White;
			editFax.CustomReportFieldName = "";
			editFax.CustomReportKey = "";
			editFax.CustomReportValueType = 1;
			editFax.IsComboTextBox = false;
			editFax.IsModified = false;
			editFax.Location = new System.Drawing.Point(331, 277);
			editFax.MaxLength = 24;
			editFax.Name = "editFax";
			editFax.Size = new System.Drawing.Size(161, 20);
			editFax.TabIndex = 20;
			label2.AutoSize = true;
			label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label2.Location = new System.Drawing.Point(284, 281);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(27, 13);
			label2.TabIndex = 19;
			label2.Text = "Fax:";
			lblNotes.AutoSize = true;
			lblNotes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblNotes.Location = new System.Drawing.Point(9, 300);
			lblNotes.Name = "lblNotes";
			lblNotes.Size = new System.Drawing.Size(33, 13);
			lblNotes.TabIndex = 3;
			lblNotes.Text = "Note:";
			editNotes.AcceptsReturn = true;
			editNotes.BackColor = System.Drawing.Color.White;
			editNotes.CustomReportFieldName = "";
			editNotes.CustomReportKey = "";
			editNotes.CustomReportValueType = 1;
			editNotes.IsComboTextBox = false;
			editNotes.IsModified = false;
			editNotes.Location = new System.Drawing.Point(124, 299);
			editNotes.MaxLength = 32000;
			editNotes.Multiline = true;
			editNotes.Name = "editNotes";
			editNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			editNotes.Size = new System.Drawing.Size(368, 103);
			editNotes.TabIndex = 21;
			lblPhone.AutoSize = true;
			lblPhone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPhone.Location = new System.Drawing.Point(9, 279);
			lblPhone.Name = "lblPhone";
			lblPhone.Size = new System.Drawing.Size(41, 13);
			lblPhone.TabIndex = 0;
			lblPhone.Text = "Phone:";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 406);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(510, 40);
			panelButtons.TabIndex = 23;
			panelButtons.TabStop = true;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(510, 1);
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
			xpButton1.Location = new System.Drawing.Point(400, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(buttonClose_Click);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(510, 31);
			toolStrip1.TabIndex = 25;
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(124, 37);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(264, 20);
			textBoxCode.TabIndex = 1;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 37);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(73, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Bank Code:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 61);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(76, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Bank Name:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 29;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxRoutingCode.BackColor = System.Drawing.Color.White;
			textBoxRoutingCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRoutingCode.CustomReportFieldName = "";
			textBoxRoutingCode.CustomReportKey = "";
			textBoxRoutingCode.CustomReportValueType = 1;
			textBoxRoutingCode.IsComboTextBox = false;
			textBoxRoutingCode.IsModified = false;
			textBoxRoutingCode.Location = new System.Drawing.Point(124, 103);
			textBoxRoutingCode.MaxLength = 30;
			textBoxRoutingCode.Name = "textBoxRoutingCode";
			textBoxRoutingCode.Size = new System.Drawing.Size(264, 20);
			textBoxRoutingCode.TabIndex = 8;
			label1.AutoSize = true;
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.Location = new System.Drawing.Point(9, 106);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(75, 13);
			label1.TabIndex = 31;
			label1.Text = "Routing Code:";
			textBoxTaxIDNumber.BackColor = System.Drawing.Color.White;
			textBoxTaxIDNumber.CustomReportFieldName = "";
			textBoxTaxIDNumber.CustomReportKey = "";
			textBoxTaxIDNumber.CustomReportValueType = 1;
			textBoxTaxIDNumber.IsComboTextBox = false;
			textBoxTaxIDNumber.IsModified = false;
			textBoxTaxIDNumber.Location = new System.Drawing.Point(124, 124);
			textBoxTaxIDNumber.MaxLength = 30;
			textBoxTaxIDNumber.Name = "textBoxTaxIDNumber";
			textBoxTaxIDNumber.Size = new System.Drawing.Size(264, 20);
			textBoxTaxIDNumber.TabIndex = 9;
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(9, 128);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(83, 13);
			mmLabel58.TabIndex = 69;
			mmLabel58.Text = "Tax ID Number:";
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(510, 446);
			base.Controls.Add(textBoxTaxIDNumber);
			base.Controls.Add(mmLabel58);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxRoutingCode);
			base.Controls.Add(formManager);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(labelCode);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(label21);
			base.Controls.Add(editState);
			base.Controls.Add(editIsDiscontinued);
			base.Controls.Add(editPostalCode);
			base.Controls.Add(lblPhone);
			base.Controls.Add(editCountry);
			base.Controls.Add(editNotes);
			base.Controls.Add(editContactName);
			base.Controls.Add(lblNotes);
			base.Controls.Add(editBankName);
			base.Controls.Add(label2);
			base.Controls.Add(label3);
			base.Controls.Add(editFax);
			base.Controls.Add(editAddress3);
			base.Controls.Add(editPhone);
			base.Controls.Add(editAddress2);
			base.Controls.Add(lblCountry);
			base.Controls.Add(lblPostalCode);
			base.Controls.Add(lblAddress);
			base.Controls.Add(editCity);
			base.Controls.Add(lblCity);
			base.Controls.Add(editContactTitle);
			base.Controls.Add(editAddress);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "BankDetailsForm";
			Text = "Bank";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(BankDetailsForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new BankData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BankTable.Rows[0] : currentData.BankTable.NewRow();
				dataRow.BeginEdit();
				dataRow["BankID"] = textBoxCode.Text.Trim();
				dataRow["BankName"] = editBankName.Text.Trim();
				dataRow["RoutingCode"] = textBoxRoutingCode.Text.Trim();
				dataRow["TaxIDNumber"] = textBoxTaxIDNumber.Text.Trim();
				dataRow["Address"] = editAddress.Text;
				dataRow["Address2"] = editAddress2.Text;
				dataRow["Address3"] = editAddress3.Text;
				dataRow["City"] = editCity.Text;
				dataRow["ContactName"] = editContactName.Text;
				dataRow["ContactTitle"] = editContactTitle.Text;
				dataRow["Country"] = editCountry.Text;
				dataRow["Fax"] = editFax.Text;
				dataRow["Note"] = editNotes.Text;
				dataRow["Phone"] = editPhone.Text;
				dataRow["PostalCode"] = editPostalCode.Text;
				dataRow["State"] = editState.Text;
				dataRow["IsInactive"] = editIsDiscontinued.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.BankTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["BankID"].ToString();
					editBankName.Text = dataRow["BankName"].ToString();
					textBoxRoutingCode.Text = dataRow["RoutingCode"].ToString();
					textBoxTaxIDNumber.Text = dataRow["TaxIDNumber"].ToString();
					editAddress.Text = dataRow["Address"].ToString();
					editAddress2.Text = dataRow["Address2"].ToString();
					editAddress3.Text = dataRow["Address3"].ToString();
					editCity.Text = dataRow["City"].ToString();
					editContactName.Text = dataRow["ContactName"].ToString();
					editContactTitle.Text = dataRow["ContactTitle"].ToString();
					editCountry.Text = dataRow["Country"].ToString();
					editFax.Text = dataRow["Fax"].ToString();
					editNotes.Text = dataRow["Note"].ToString();
					editPhone.Text = dataRow["Phone"].ToString();
					editPostalCode.Text = dataRow["PostalCode"].ToString();
					editState.Text = dataRow["State"].ToString();
					editIsDiscontinued.Checked = bool.Parse(dataRow["IsInactive"].ToString());
				}
			}
			catch
			{
				throw;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.BankSystem.GetBankByID(id);
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
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
		}

		private void BankDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				Init();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
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

		private bool ValidateData()
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Bank", "BankID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (textBoxCode.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				textBoxCode.Focus();
				return false;
			}
			if (editBankName.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				editBankName.Focus();
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Bank", "BankID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
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
				bool flag = (!isNewRecord) ? Factory.BankSystem.UpdateBank(currentData) : Factory.BankSystem.CreateBank(currentData);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Bank, needRefresh: true);
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

		public void OnActivated()
		{
			textBoxCode.Focus();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Accounts;
		}

		public static int GetScreenID()
		{
			return 7050;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
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
			foreach (Control control in base.Controls)
			{
				if (control.GetType() == typeof(TextBox) || control.GetType().BaseType == typeof(TextBox))
				{
					control.Text = "";
				}
			}
			editIsDiscontinued.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
			textBoxTaxIDNumber.Clear();
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.BankSystem.DeleteBank(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Bank", "BankID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Bank", "BankID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Bank", "BankID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Bank", "BankID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Bank", "BankID", toolStripTextBoxFind.Text.Trim()))
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Bank);
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
					docManagementForm.EntityName = editBankName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Acccounts;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
