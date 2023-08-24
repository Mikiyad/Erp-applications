using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class EmployeeEOSSettlementForm : Form, IDataForm, IDataEntry
	{
		private EmployeeEOSSettlementData currentData;

		private const string TABLENAME_CONST = "Employee_EOSSettlement";

		private const string IDFIELD_CONST = "EmployeeID";

		private bool isNewRecord = true;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonClose;

		private XPButton buttonSave;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private FormManager formManager;

		private Panel panel1;

		private MMLabel mmLabel53;

		private MMTextBox textBoxEmployeeName;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxDesignation;

		private MMLabel mmLabel3;

		private MMTextBox textBoxGender;

		private EmployeeComboBox comboBoxEmployee;

		private MMLabel mmLabel6;

		private MMTextBox textBoxLocation;

		private MMLabel mmLabel7;

		private MMTextBox textBoxDivision;

		private MMLabel mmLabel8;

		private MMTextBox textBoxDepartment;

		private MMLabel mmLabel9;

		private MMLabel mmLabel10;

		private MMLabel mmLabel38;

		private XPButton buttonNew;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxLabourID;

		private MMTextBox textBoxJoining;

		private MMTextBox textBoxConfirmation;

		private MMLabel labelCancelled;

		private XPButton buttonDelete;

		private GroupBox groupBox1;

		private MMLabel mmLabel1;

		private MMSDateTimePicker dateTimePickerLastWorkDate;

		private MMLabel mmLabel14;

		private XPButton buttonApply;

		private MMLabel mmLabel12;

		private MMLabel mmLabel5;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxNetTotal;

		private AmountTextBox textBoxOtherDeductions;

		private AmountTextBox textBoxLoan;

		private AmountTextBox textBoxTotal;

		private AmountTextBox textBoxOtherBenefits;

		private AmountTextBox textBoxSalaryDue;

		private AmountTextBox textBoxLeaveDue;

		private AmountTextBox textBoxEOSBenefit;

		private XPButton buttonPayCheque;

		private XPButton buttonPayCash;

		private GroupBox groupBox2;

		private PayrollItemComboBox comboBoxPayrollItemDeduction;

		private MMLabel mmLabel15;

		private MMLabel mmLabel16;

		private MMLabel mmLabel17;

		private TextBox textBoxTotalDays;

		private Label label3;

		private GroupBox groupBox3;

		private UltraFormattedLinkLabel LabelEmployee;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel4;

		private AmountTextBox textBoxTicketAmount;

		private UltraFormattedLinkLabel linkLabelCountry;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool isCancelled;

		private bool IsOT;

		private bool IsDD;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private string EmployeeNo
		{
			get;
			set;
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
					buttonDelete.Enabled = false;
					buttonPayCash.Enabled = false;
					buttonPayCheque.Enabled = false;
				}
				else
				{
					buttonDelete.Enabled = true;
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

		public EmployeeEOSSettlementForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.EmployeeEOSSettlementForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxTicketAmount = new Micromind.UISupport.AmountTextBox();
			textBoxTotalDays = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			buttonPayCheque = new Micromind.UISupport.XPButton();
			buttonPayCash = new Micromind.UISupport.XPButton();
			textBoxNetTotal = new Micromind.UISupport.AmountTextBox();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			textBoxOtherBenefits = new Micromind.UISupport.AmountTextBox();
			textBoxSalaryDue = new Micromind.UISupport.AmountTextBox();
			textBoxLeaveDue = new Micromind.UISupport.AmountTextBox();
			textBoxEOSBenefit = new Micromind.UISupport.AmountTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimePickerLastWorkDate = new Micromind.UISupport.MMSDateTimePicker(components);
			groupBox2 = new System.Windows.Forms.GroupBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxLoan = new Micromind.UISupport.AmountTextBox();
			buttonApply = new Micromind.UISupport.XPButton();
			comboBoxPayrollItemDeduction = new Micromind.DataControls.PayrollItemComboBox();
			textBoxOtherDeductions = new Micromind.UISupport.AmountTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			LabelEmployee = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelCancelled = new Micromind.UISupport.MMLabel();
			textBoxConfirmation = new Micromind.UISupport.MMTextBox();
			textBoxJoining = new Micromind.UISupport.MMTextBox();
			textBoxLabourID = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxDepartment = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxDivision = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxLocation = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			textBoxGender = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			groupBox1.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 600);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(712, 41);
			panelButtons.TabIndex = 12;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(214, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(112, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(712, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(602, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(xpButton1_Click);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[10]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPreview,
				toolStripButtonInformation,
				toolStripButtonPrint
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(712, 27);
			toolStrip1.TabIndex = 13;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(24, 24);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Visible = false;
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Visible = false;
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(24, 24);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Visible = false;
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(24, 24);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Visible = false;
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(82, 24);
			toolStripButtonAttach.Text = "Attach File";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(24, 24);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(53, 24);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 27);
			panel1.MaximumSize = new System.Drawing.Size(0, 8);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(712, 8);
			panel1.TabIndex = 314;
			groupBox1.Controls.Add(mmLabel4);
			groupBox1.Controls.Add(textBoxTicketAmount);
			groupBox1.Controls.Add(textBoxTotalDays);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(mmLabel15);
			groupBox1.Controls.Add(mmLabel16);
			groupBox1.Controls.Add(mmLabel17);
			groupBox1.Controls.Add(buttonPayCheque);
			groupBox1.Controls.Add(buttonPayCash);
			groupBox1.Controls.Add(textBoxNetTotal);
			groupBox1.Controls.Add(textBoxTotal);
			groupBox1.Controls.Add(textBoxOtherBenefits);
			groupBox1.Controls.Add(textBoxSalaryDue);
			groupBox1.Controls.Add(textBoxLeaveDue);
			groupBox1.Controls.Add(textBoxEOSBenefit);
			groupBox1.Controls.Add(mmLabel14);
			groupBox1.Controls.Add(mmLabel5);
			groupBox1.Controls.Add(mmLabel2);
			groupBox1.Controls.Add(mmLabel1);
			groupBox1.Controls.Add(dateTimePickerLastWorkDate);
			groupBox1.Controls.Add(groupBox2);
			groupBox1.Controls.Add(groupBox3);
			groupBox1.Location = new System.Drawing.Point(24, 204);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(555, 300);
			groupBox1.TabIndex = 11;
			groupBox1.TabStop = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(6, 117);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(82, 13);
			mmLabel4.TabIndex = 390;
			mmLabel4.Text = "Ticket Amount :";
			textBoxTicketAmount.CustomReportFieldName = "";
			textBoxTicketAmount.CustomReportKey = "";
			textBoxTicketAmount.CustomReportValueType = 1;
			textBoxTicketAmount.IsComboTextBox = false;
			textBoxTicketAmount.Location = new System.Drawing.Point(98, 117);
			textBoxTicketAmount.MaxLength = 15;
			textBoxTicketAmount.Name = "textBoxTicketAmount";
			textBoxTicketAmount.Size = new System.Drawing.Size(114, 20);
			textBoxTicketAmount.TabIndex = 5;
			textBoxTicketAmount.Text = "0.00";
			textBoxTicketAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTicketAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTicketAmount.TextChanged += new System.EventHandler(textBoxTicketAmount_TextChanged);
			textBoxTotalDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalDays.ForeColor = System.Drawing.Color.Black;
			textBoxTotalDays.Location = new System.Drawing.Point(98, 69);
			textBoxTotalDays.MaxLength = 15;
			textBoxTotalDays.Name = "textBoxTotalDays";
			textBoxTotalDays.ReadOnly = true;
			textBoxTotalDays.Size = new System.Drawing.Size(62, 20);
			textBoxTotalDays.TabIndex = 2;
			textBoxTotalDays.TabStop = false;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(167, 73);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(37, 13);
			label3.TabIndex = 364;
			label3.Text = "(Days)";
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(6, 45);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(71, 13);
			mmLabel15.TabIndex = 363;
			mmLabel15.Text = "EOS Benefit :";
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(6, 69);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(65, 13);
			mmLabel16.TabIndex = 364;
			mmLabel16.Text = "Leave Due :";
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(6, 93);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(66, 13);
			mmLabel17.TabIndex = 365;
			mmLabel17.Text = "Salary Due :";
			buttonPayCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCheque.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCheque.Location = new System.Drawing.Point(434, 263);
			buttonPayCheque.Name = "buttonPayCheque";
			buttonPayCheque.Size = new System.Drawing.Size(96, 24);
			buttonPayCheque.TabIndex = 10;
			buttonPayCheque.Text = "Pay Cheque";
			buttonPayCheque.UseVisualStyleBackColor = false;
			buttonPayCheque.Visible = false;
			buttonPayCheque.Click += new System.EventHandler(buttonPayCheque_Click);
			buttonPayCash.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCash.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCash.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCash.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCash.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCash.Location = new System.Drawing.Point(333, 263);
			buttonPayCash.Name = "buttonPayCash";
			buttonPayCash.Size = new System.Drawing.Size(96, 24);
			buttonPayCash.TabIndex = 9;
			buttonPayCash.Text = "Pay Cash";
			buttonPayCash.UseVisualStyleBackColor = false;
			buttonPayCash.Visible = false;
			buttonPayCash.Click += new System.EventHandler(buttonPayCash_Click);
			textBoxNetTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNetTotal.CustomReportFieldName = "";
			textBoxNetTotal.CustomReportKey = "";
			textBoxNetTotal.CustomReportValueType = 1;
			textBoxNetTotal.IsComboTextBox = false;
			textBoxNetTotal.Location = new System.Drawing.Point(99, 263);
			textBoxNetTotal.MaxLength = 15;
			textBoxNetTotal.Name = "textBoxNetTotal";
			textBoxNetTotal.ReadOnly = true;
			textBoxNetTotal.Size = new System.Drawing.Size(114, 20);
			textBoxNetTotal.TabIndex = 8;
			textBoxNetTotal.TabStop = false;
			textBoxNetTotal.Text = "0.00";
			textBoxNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNetTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.Location = new System.Drawing.Point(98, 166);
			textBoxTotal.MaxLength = 15;
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.ReadOnly = true;
			textBoxTotal.Size = new System.Drawing.Size(114, 20);
			textBoxTotal.TabIndex = 7;
			textBoxTotal.TabStop = false;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxOtherBenefits.CustomReportFieldName = "";
			textBoxOtherBenefits.CustomReportKey = "";
			textBoxOtherBenefits.CustomReportValueType = 1;
			textBoxOtherBenefits.IsComboTextBox = false;
			textBoxOtherBenefits.Location = new System.Drawing.Point(98, 141);
			textBoxOtherBenefits.MaxLength = 15;
			textBoxOtherBenefits.Name = "textBoxOtherBenefits";
			textBoxOtherBenefits.Size = new System.Drawing.Size(114, 20);
			textBoxOtherBenefits.TabIndex = 6;
			textBoxOtherBenefits.Text = "0.00";
			textBoxOtherBenefits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOtherBenefits.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxOtherBenefits.TextChanged += new System.EventHandler(textBoxOtherBenefits_TextChanged);
			textBoxSalaryDue.CustomReportFieldName = "";
			textBoxSalaryDue.CustomReportKey = "";
			textBoxSalaryDue.CustomReportValueType = 1;
			textBoxSalaryDue.IsComboTextBox = false;
			textBoxSalaryDue.Location = new System.Drawing.Point(98, 93);
			textBoxSalaryDue.MaxLength = 15;
			textBoxSalaryDue.Name = "textBoxSalaryDue";
			textBoxSalaryDue.Size = new System.Drawing.Size(114, 20);
			textBoxSalaryDue.TabIndex = 4;
			textBoxSalaryDue.Text = "0.00";
			textBoxSalaryDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSalaryDue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxSalaryDue.TextChanged += new System.EventHandler(textBoxSalaryDue_TextChanged);
			textBoxLeaveDue.CustomReportFieldName = "";
			textBoxLeaveDue.CustomReportKey = "";
			textBoxLeaveDue.CustomReportValueType = 1;
			textBoxLeaveDue.IsComboTextBox = false;
			textBoxLeaveDue.Location = new System.Drawing.Point(221, 68);
			textBoxLeaveDue.MaxLength = 15;
			textBoxLeaveDue.Name = "textBoxLeaveDue";
			textBoxLeaveDue.Size = new System.Drawing.Size(114, 20);
			textBoxLeaveDue.TabIndex = 3;
			textBoxLeaveDue.Text = "0.00";
			textBoxLeaveDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLeaveDue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxLeaveDue.TextChanged += new System.EventHandler(textBoxLeaveDue_TextChanged);
			textBoxEOSBenefit.CustomReportFieldName = "";
			textBoxEOSBenefit.CustomReportKey = "";
			textBoxEOSBenefit.CustomReportValueType = 1;
			textBoxEOSBenefit.IsComboTextBox = false;
			textBoxEOSBenefit.Location = new System.Drawing.Point(98, 45);
			textBoxEOSBenefit.MaxLength = 15;
			textBoxEOSBenefit.Name = "textBoxEOSBenefit";
			textBoxEOSBenefit.Size = new System.Drawing.Size(114, 20);
			textBoxEOSBenefit.TabIndex = 1;
			textBoxEOSBenefit.Text = "0.00";
			textBoxEOSBenefit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxEOSBenefit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxEOSBenefit.TextChanged += new System.EventHandler(textBoxEOSBenefit_TextChanged);
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(11, 263);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(58, 13);
			mmLabel14.TabIndex = 373;
			mmLabel14.Text = "Net Total :";
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(6, 166);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(76, 13);
			mmLabel5.TabIndex = 368;
			mmLabel5.Text = "Total Payable:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(6, 141);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(84, 13);
			mmLabel2.TabIndex = 367;
			mmLabel2.Text = "Other Benefits :";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(6, 19);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 363;
			mmLabel1.Text = "Last Work Date:";
			dateTimePickerLastWorkDate.Checked = false;
			dateTimePickerLastWorkDate.CustomFormat = " ";
			dateTimePickerLastWorkDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerLastWorkDate.Location = new System.Drawing.Point(98, 19);
			dateTimePickerLastWorkDate.Name = "dateTimePickerLastWorkDate";
			dateTimePickerLastWorkDate.ShowCheckBox = true;
			dateTimePickerLastWorkDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerLastWorkDate.TabIndex = 0;
			dateTimePickerLastWorkDate.Value = new System.DateTime(0L);
			dateTimePickerLastWorkDate.ValueChanged += new System.EventHandler(dateTimePickerLastWorkDate_ValueChanged);
			groupBox2.Location = new System.Drawing.Point(324, 251);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(214, 44);
			groupBox2.TabIndex = 386;
			groupBox2.TabStop = false;
			groupBox2.Visible = false;
			groupBox3.Controls.Add(linkLabelCountry);
			groupBox3.Controls.Add(textBoxLoan);
			groupBox3.Controls.Add(buttonApply);
			groupBox3.Controls.Add(comboBoxPayrollItemDeduction);
			groupBox3.Controls.Add(textBoxOtherDeductions);
			groupBox3.Controls.Add(mmLabel12);
			groupBox3.Location = new System.Drawing.Point(6, 188);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(352, 64);
			groupBox3.TabIndex = 388;
			groupBox3.TabStop = false;
			groupBox3.Text = "Deductions";
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(2, 40);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(90, 14);
			linkLabelCountry.TabIndex = 391;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Other Deductions:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			textBoxLoan.CustomReportFieldName = "";
			textBoxLoan.CustomReportKey = "";
			textBoxLoan.CustomReportValueType = 1;
			textBoxLoan.IsComboTextBox = false;
			textBoxLoan.Location = new System.Drawing.Point(93, 15);
			textBoxLoan.MaxLength = 15;
			textBoxLoan.Name = "textBoxLoan";
			textBoxLoan.Size = new System.Drawing.Size(114, 20);
			textBoxLoan.TabIndex = 1;
			textBoxLoan.Text = "0.00";
			textBoxLoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLoan.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxLoan.TextChanged += new System.EventHandler(textBoxLoan_TextChanged);
			buttonApply.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonApply.Anchor = System.Windows.Forms.AnchorStyles.None;
			buttonApply.BackColor = System.Drawing.Color.DarkGray;
			buttonApply.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonApply.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonApply.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonApply.Location = new System.Drawing.Point(245, 9);
			buttonApply.Name = "buttonApply";
			buttonApply.Size = new System.Drawing.Size(52, 24);
			buttonApply.TabIndex = 4;
			buttonApply.Text = "&Apply";
			buttonApply.UseVisualStyleBackColor = false;
			buttonApply.Visible = false;
			buttonApply.Click += new System.EventHandler(buttonApply_Click);
			comboBoxPayrollItemDeduction.Assigned = false;
			comboBoxPayrollItemDeduction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrollItemDeduction.CustomReportFieldName = "";
			comboBoxPayrollItemDeduction.CustomReportKey = "";
			comboBoxPayrollItemDeduction.CustomReportValueType = 1;
			comboBoxPayrollItemDeduction.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrollItemDeduction.DisplayLayout.Appearance = appearance2;
			comboBoxPayrollItemDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrollItemDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxPayrollItemDeduction.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrollItemDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxPayrollItemDeduction.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxPayrollItemDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrollItemDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrollItemDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrollItemDeduction.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrollItemDeduction.Editable = true;
			comboBoxPayrollItemDeduction.FilterString = "";
			comboBoxPayrollItemDeduction.HasAllAccount = false;
			comboBoxPayrollItemDeduction.HasCustom = false;
			comboBoxPayrollItemDeduction.IsDataLoaded = false;
			comboBoxPayrollItemDeduction.IsDeduction = true;
			comboBoxPayrollItemDeduction.Location = new System.Drawing.Point(93, 37);
			comboBoxPayrollItemDeduction.MaxDropDownItems = 12;
			comboBoxPayrollItemDeduction.Name = "comboBoxPayrollItemDeduction";
			comboBoxPayrollItemDeduction.ShowInactiveItems = false;
			comboBoxPayrollItemDeduction.ShowQuickAdd = true;
			comboBoxPayrollItemDeduction.Size = new System.Drawing.Size(114, 20);
			comboBoxPayrollItemDeduction.TabIndex = 2;
			comboBoxPayrollItemDeduction.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxOtherDeductions.CustomReportFieldName = "";
			textBoxOtherDeductions.CustomReportKey = "";
			textBoxOtherDeductions.CustomReportValueType = 1;
			textBoxOtherDeductions.IsComboTextBox = false;
			textBoxOtherDeductions.Location = new System.Drawing.Point(211, 38);
			textBoxOtherDeductions.MaxLength = 15;
			textBoxOtherDeductions.Name = "textBoxOtherDeductions";
			textBoxOtherDeductions.Size = new System.Drawing.Size(114, 20);
			textBoxOtherDeductions.TabIndex = 3;
			textBoxOtherDeductions.Text = "0.00";
			textBoxOtherDeductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOtherDeductions.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxOtherDeductions.TextChanged += new System.EventHandler(textBoxOtherDeductions_TextChanged);
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(8, 17);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(37, 13);
			mmLabel12.TabIndex = 0;
			mmLabel12.Text = "Loan :";
			appearance14.FontData.BoldAsString = "True";
			appearance14.FontData.Name = "Tahoma";
			LabelEmployee.Appearance = appearance14;
			LabelEmployee.AutoSize = true;
			LabelEmployee.Location = new System.Drawing.Point(20, 45);
			LabelEmployee.Name = "LabelEmployee";
			LabelEmployee.Size = new System.Drawing.Size(93, 15);
			LabelEmployee.TabIndex = 363;
			LabelEmployee.TabStop = true;
			LabelEmployee.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			LabelEmployee.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			LabelEmployee.Value = "Employee Code:";
			appearance15.ForeColor = System.Drawing.Color.Blue;
			LabelEmployee.VisitedLinkAppearance = appearance15;
			LabelEmployee.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(LabelEmployee_LinkClicked);
			labelCancelled.AutoSize = true;
			labelCancelled.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCancelled.ForeColor = System.Drawing.Color.Red;
			labelCancelled.IsFieldHeader = false;
			labelCancelled.IsRequired = false;
			labelCancelled.Location = new System.Drawing.Point(294, 47);
			labelCancelled.Name = "labelCancelled";
			labelCancelled.PenWidth = 1f;
			labelCancelled.ShowBorder = false;
			labelCancelled.Size = new System.Drawing.Size(149, 13);
			labelCancelled.TabIndex = 361;
			labelCancelled.Text = "Employee is already cancelled";
			labelCancelled.Visible = false;
			textBoxConfirmation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConfirmation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxConfirmation.CustomReportFieldName = "";
			textBoxConfirmation.CustomReportKey = "";
			textBoxConfirmation.CustomReportValueType = 1;
			textBoxConfirmation.IsComboTextBox = false;
			textBoxConfirmation.IsRequired = true;
			textBoxConfirmation.Location = new System.Drawing.Point(452, 134);
			textBoxConfirmation.MaxLength = 64;
			textBoxConfirmation.Name = "textBoxConfirmation";
			textBoxConfirmation.ReadOnly = true;
			textBoxConfirmation.Size = new System.Drawing.Size(117, 20);
			textBoxConfirmation.TabIndex = 10;
			textBoxConfirmation.TabStop = false;
			textBoxJoining.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJoining.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxJoining.CustomReportFieldName = "";
			textBoxJoining.CustomReportKey = "";
			textBoxJoining.CustomReportValueType = 1;
			textBoxJoining.IsComboTextBox = false;
			textBoxJoining.IsRequired = true;
			textBoxJoining.Location = new System.Drawing.Point(452, 112);
			textBoxJoining.MaxLength = 64;
			textBoxJoining.Name = "textBoxJoining";
			textBoxJoining.ReadOnly = true;
			textBoxJoining.Size = new System.Drawing.Size(117, 20);
			textBoxJoining.TabIndex = 9;
			textBoxJoining.TabStop = false;
			textBoxLabourID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLabourID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLabourID.CustomReportFieldName = "";
			textBoxLabourID.CustomReportKey = "";
			textBoxLabourID.CustomReportValueType = 1;
			textBoxLabourID.IsComboTextBox = false;
			textBoxLabourID.IsRequired = true;
			textBoxLabourID.Location = new System.Drawing.Point(121, 178);
			textBoxLabourID.MaxLength = 64;
			textBoxLabourID.Name = "textBoxLabourID";
			textBoxLabourID.ReadOnly = true;
			textBoxLabourID.Size = new System.Drawing.Size(164, 20);
			textBoxLabourID.TabIndex = 6;
			textBoxLabourID.TabStop = false;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(452, 45);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(87, 20);
			textBoxCode.TabIndex = 7;
			textBoxCode.TabStop = false;
			textBoxCode.Visible = false;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(373, 134);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(72, 13);
			mmLabel38.TabIndex = 350;
			mmLabel38.Text = "Confirmation:";
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(21, 178);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(58, 13);
			mmLabel10.TabIndex = 349;
			mmLabel10.Text = "Labour ID:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(373, 112);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(73, 13);
			mmLabel9.TabIndex = 346;
			mmLabel9.Text = "Joining Date :";
			textBoxDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDepartment.CustomReportFieldName = "";
			textBoxDepartment.CustomReportKey = "";
			textBoxDepartment.CustomReportValueType = 1;
			textBoxDepartment.IsComboTextBox = false;
			textBoxDepartment.IsRequired = true;
			textBoxDepartment.Location = new System.Drawing.Point(121, 156);
			textBoxDepartment.MaxLength = 64;
			textBoxDepartment.Name = "textBoxDepartment";
			textBoxDepartment.ReadOnly = true;
			textBoxDepartment.Size = new System.Drawing.Size(164, 20);
			textBoxDepartment.TabIndex = 5;
			textBoxDepartment.TabStop = false;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(21, 156);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(71, 13);
			mmLabel8.TabIndex = 344;
			mmLabel8.Text = "Department :";
			textBoxDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDivision.CustomReportFieldName = "";
			textBoxDivision.CustomReportKey = "";
			textBoxDivision.CustomReportValueType = 1;
			textBoxDivision.IsComboTextBox = false;
			textBoxDivision.IsRequired = true;
			textBoxDivision.Location = new System.Drawing.Point(121, 134);
			textBoxDivision.MaxLength = 64;
			textBoxDivision.Name = "textBoxDivision";
			textBoxDivision.ReadOnly = true;
			textBoxDivision.Size = new System.Drawing.Size(164, 20);
			textBoxDivision.TabIndex = 4;
			textBoxDivision.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(21, 134);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(50, 13);
			mmLabel7.TabIndex = 342;
			mmLabel7.Text = "Division :";
			textBoxLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLocation.CustomReportFieldName = "";
			textBoxLocation.CustomReportKey = "";
			textBoxLocation.CustomReportValueType = 1;
			textBoxLocation.IsComboTextBox = false;
			textBoxLocation.IsRequired = true;
			textBoxLocation.Location = new System.Drawing.Point(121, 111);
			textBoxLocation.MaxLength = 64;
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.ReadOnly = true;
			textBoxLocation.Size = new System.Drawing.Size(164, 20);
			textBoxLocation.TabIndex = 3;
			textBoxLocation.TabStop = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(21, 111);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(54, 13);
			mmLabel6.TabIndex = 340;
			mmLabel6.Text = "Location :";
			textBoxEmployeeName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxEmployeeName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(121, 67);
			textBoxEmployeeName.MaxLength = 20;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(244, 20);
			textBoxEmployeeName.TabIndex = 1;
			textBoxEmployeeName.TabStop = false;
			textBoxGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxGender.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGender.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxGender.CustomReportFieldName = "";
			textBoxGender.CustomReportKey = "";
			textBoxGender.CustomReportValueType = 1;
			textBoxGender.IsComboTextBox = false;
			textBoxGender.Location = new System.Drawing.Point(452, 90);
			textBoxGender.MaxLength = 20;
			textBoxGender.Name = "textBoxGender";
			textBoxGender.ReadOnly = true;
			textBoxGender.Size = new System.Drawing.Size(117, 20);
			textBoxGender.TabIndex = 8;
			textBoxGender.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(373, 89);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(49, 13);
			mmLabel3.TabIndex = 329;
			mmLabel3.Text = "Gender :";
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(21, 67);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(90, 13);
			mmLabel53.TabIndex = 322;
			mmLabel53.Text = "Employee Name :";
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(21, 89);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(70, 13);
			lblDescriptions.TabIndex = 320;
			lblDescriptions.Text = "Designation :";
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.IsRequired = true;
			textBoxDesignation.Location = new System.Drawing.Point(121, 89);
			textBoxDesignation.MaxLength = 64;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(164, 20);
			textBoxDesignation.TabIndex = 2;
			textBoxDesignation.TabStop = false;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance16;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance17.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance17;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance18;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance19.BackColor2 = System.Drawing.SystemColors.Control;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance19;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance20;
			appearance21.BackColor = System.Drawing.SystemColors.Highlight;
			appearance21.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance21;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance22;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			appearance23.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance23;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance24.BackColor = System.Drawing.SystemColors.Control;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance24;
			appearance25.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance25;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance26;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance27;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(121, 45);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(164, 20);
			comboBoxEmployee.TabIndex = 0;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 28);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 307;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(712, 641);
			base.Controls.Add(LabelEmployee);
			base.Controls.Add(groupBox1);
			base.Controls.Add(labelCancelled);
			base.Controls.Add(textBoxConfirmation);
			base.Controls.Add(textBoxJoining);
			base.Controls.Add(textBoxLabourID);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel38);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxDepartment);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(textBoxDivision);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(textBoxLocation);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(textBoxGender);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel53);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(lblDescriptions);
			base.Controls.Add(textBoxDesignation);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(607, 589);
			base.Name = "EmployeeEOSSettlementForm";
			Text = "End of Service Settlement ";
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private void AddEvents()
		{
			base.Load += EmployeeCancellationForm_Load;
			comboBoxEmployee.SelectedIndexChanged += comboBoxEmployee_SelectedIndexChanged;
		}

		private void checkBoxIsCancelled_CheckStateChanged(object sender, EventArgs e)
		{
		}

		private void EmployeeCancellationForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
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
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ClearForm();
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				DataSet employeeBriefInfo = Factory.EmployeeEOSSettlementSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
				if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
					textBoxLocation.Text = dataRow["LocationID"].ToString();
					textBoxDivision.Text = dataRow["DivisionName"].ToString();
					textBoxDepartment.Text = dataRow["DepartmentName"].ToString();
					textBoxDesignation.Text = dataRow["PositionName"].ToString();
					textBoxLabourID.Text = dataRow["LabourID"].ToString();
					if (dataRow["Gender"] != DBNull.Value && dataRow["Gender"].ToString() != string.Empty)
					{
						if (dataRow["Gender"].ToString() == "M")
						{
							textBoxGender.Text = "MALE";
						}
						else
						{
							textBoxGender.Text = "FEMALE";
						}
					}
					if (dataRow["JoiningDate"] != DBNull.Value)
					{
						textBoxJoining.Text = DateTime.Parse(dataRow["JoiningDate"].ToString()).ToShortDateString();
					}
					else
					{
						textBoxJoining.Clear();
					}
					if (dataRow["ConfirmationDate"] != DBNull.Value)
					{
						textBoxConfirmation.Text = DateTime.Parse(dataRow["JoiningDate"].ToString()).ToShortDateString();
					}
					else
					{
						textBoxConfirmation.Clear();
					}
					bool.TryParse(dataRow["IsCancelled"].ToString(), out isCancelled);
				}
				if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[1].Rows.Count > 0)
				{
					DataRow dataRow2 = employeeBriefInfo.Tables[1].Rows[0];
					dateTimePickerLastWorkDate.Checked = true;
					dateTimePickerLastWorkDate.Enabled = false;
					dateTimePickerLastWorkDate.Value = DateTime.Parse(dataRow2["LastWorkingDate"].ToString());
					textBoxEOSBenefit.Text = dataRow2["EOSBenefit"].ToString();
					textBoxSalaryDue.Text = dataRow2["SalaryDue"].ToString();
					textBoxTotalDays.Text = dataRow2["TotalPayable"].ToString();
					textBoxOtherDeductions.Text = dataRow2["DeductionAmount"].ToString();
					textBoxNetTotal.Text = dataRow2["NetTotal"].ToString();
					textBoxLeaveDue.Text = dataRow2["DueAmount"].ToString();
					textBoxTotalDays.Text = dataRow2["LeaveDue"].ToString();
					textBoxTotal.Text = dataRow2["TotalPayable"].ToString();
					textBoxLoan.Text = dataRow2["Loan"].ToString();
					comboBoxPayrollItemDeduction.SelectedID = dataRow2["OtherDeductionID"].ToString();
					textBoxOtherBenefits.Text = dataRow2["OtherBenefits"].ToString();
					textBoxTotal.Text = dataRow2["TotalPayable"].ToString();
				}
				if (employeeBriefInfo.Tables[1].Rows.Count > 0)
				{
					IsNewRecord = false;
				}
				else
				{
					IsNewRecord = true;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["Employee_EOSSettlement"].Rows[0];
				textBoxCode.Text = dataRow["EmployeeID"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				dataRow = currentData.Tables["Employee_EOSSettlement"].Rows[0];
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeEOSSettlementData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeEOSSettlementTable.Rows[0] : currentData.EmployeeEOSSettlementTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["LastWorkingDate"] = dateTimePickerLastWorkDate.Value;
				dataRow["EOSBenefit"] = textBoxEOSBenefit.Text;
				dataRow["LeaveDue"] = textBoxTotalDays.Text;
				dataRow["DueAmount"] = textBoxLeaveDue.Text;
				dataRow["SalaryDue"] = textBoxSalaryDue.Text;
				dataRow["OtherBenefits"] = textBoxOtherBenefits.Text;
				dataRow["TotalPayable"] = textBoxTotal.Text;
				dataRow["Loan"] = textBoxLoan.Text;
				dataRow["OtherDeductionID"] = comboBoxPayrollItemDeduction.SelectedID;
				dataRow["DeductionAmount"] = textBoxOtherDeductions.Text;
				dataRow["NetTotal"] = textBoxNetTotal.Text;
				dataRow["TicketAmount"] = textBoxTicketAmount.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeEOSSettlementTable.Rows.Add(dataRow);
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
				if (Factory.EmployeeSystem.IsEmployeeSettled(comboBoxEmployee.SelectedID))
				{
					ErrorHelper.ErrorMessage("This Employee is already Settled");
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
			if (!IsNewRecord)
			{
				DialogResult dialogResult = DialogResult.Yes;
				switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to save this employee EOS ?"))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
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
				bool flag = false;
				if (isNewRecord)
				{
					flag = Factory.EmployeeEOSSettlementSystem.CreateEmployeeLoan(currentData, isUpdate: false);
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
			catch (CompanyException ex)
			{
				ErrorHelper.ErrorMessage(ex.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(int id)
		{
			try
			{
				if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
				{
					ClearForm();
					IsNewRecord = true;
					textBoxCode.Text = id.ToString();
					textBoxCode.Focus();
				}
				else
				{
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
			int result = -1;
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_EOSSettlement", "EmployeeID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_EOSSettlement", "EmployeeID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_EOSSettlement", "EmployeeID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_EOSSettlement", "EmployeeID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ClearForm()
		{
			textBoxCode.Clear();
			textBoxEmployeeName.Clear();
			textBoxDesignation.Clear();
			textBoxDivision.Clear();
			textBoxDepartment.Clear();
			textBoxGender.Clear();
			textBoxLocation.Clear();
			textBoxLabourID.Clear();
			textBoxJoining.Clear();
			textBoxConfirmation.Clear();
			dateTimePickerLastWorkDate.Checked = false;
			dateTimePickerLastWorkDate.Enabled = true;
			textBoxEOSBenefit.Clear();
			textBoxLeaveDue.Clear();
			textBoxTotalDays.Clear();
			textBoxTotal.Clear();
			comboBoxPayrollItemDeduction.Clear();
			textBoxOtherDeductions.Clear();
			textBoxOtherBenefits.Clear();
			textBoxTicketAmount.Clear();
			textBoxNetTotal.Clear();
			IsNewRecord = true;
			formManager.ResetDirty();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			ClearForm();
			IsNewRecord = true;
		}

		public void OnActivated()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		private void SalespersonGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Employee);
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.EmployeeEOSSettlementSystem.DeleteEOS(comboBoxEmployee.SelectedID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void dateTimePickerLastWorkDate_ValueChanged(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			if (dateTimePickerLastWorkDate.Text == " " || dateTimePickerLastWorkDate.Text == null || dateTimePickerLastWorkDate.Text == string.Empty)
			{
				textBoxEOSBenefit.Clear();
				textBoxLeaveDue.Clear();
				textBoxTotalDays.Clear();
				textBoxTotal.Clear();
				comboBoxPayrollItemDeduction.Clear();
				textBoxOtherDeductions.Clear();
				textBoxOtherBenefits.Clear();
				textBoxSalaryDue.Clear();
			}
			if (dateTimePickerLastWorkDate.Checked && comboBoxEmployee.SelectedID != "")
			{
				DateTime value = dateTimePickerLastWorkDate.Value;
				int month = value.Month;
				int year = value.Year;
				DateTime startDate = new DateTime(year, month, 1);
				bool flag = true;
				flag = (!CompanyPreferences.Annual || true);
				dataSet = Factory.EmployeeEOSSettlementSystem.GetEmployeeFinalSettlement(comboBoxEmployee.SelectedID, dateTimePickerLastWorkDate.Value, flag);
				if (dataSet.Tables.Count > 0)
				{
					if (dataSet.Tables["EOSRule"].Rows.Count == 0)
					{
						ErrorHelper.InformationMessage("EOS Settings in Employee Class has not Activated.");
						return;
					}
					DataTable dataTable = dataSet.Tables["GratuaityDetails"];
					object value2 = dataTable.Compute("Sum(Graduity)", "");
					DataTable dataTable2 = dataSet.Tables[1];
					if (dataTable2.Rows.Count > 0)
					{
						object obj = dataTable2.Compute("Sum(LeavesRemaining)", "");
						textBoxTotalDays.Text = obj.ToString();
						decimal.Parse(dataTable2.Rows[0]["DailySalary"].ToString());
						decimal d = decimal.Parse(dataTable2.Rows[0]["DailySalary2"].ToString());
						textBoxLeaveDue.Value = Convert.ToDecimal(obj) * d;
					}
					textBoxEOSBenefit.Value = Convert.ToDecimal(value2);
					textBoxTicketAmount.Value = decimal.Parse(dataTable.Rows[0]["TicketAmount"].ToString());
					Factory.SalarySheetSystem.CalculateSalarySheet(comboBoxEmployee.SelectedID, comboBoxEmployee.SelectedID, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", startDate, dateTimePickerLastWorkDate.Value, year, month, "");
					salaryBalance();
					buttonPayCash.Enabled = true;
					buttonPayCheque.Enabled = true;
				}
				else
				{
					buttonPayCash.Enabled = false;
					buttonPayCheque.Enabled = false;
				}
			}
			else
			{
				buttonPayCash.Enabled = false;
				buttonPayCheque.Enabled = false;
			}
			buttonApply_Click(sender, e);
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			decimal value = default(decimal);
			if (dateTimePickerLastWorkDate.Checked && comboBoxEmployee.SelectedID != "")
			{
				dataSet = Factory.EmployeeEOSSettlementSystem.GetEmployeeLoanByID(comboBoxEmployee.SelectedID);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						value += decimal.Parse(row["Balance"].ToString());
					}
				}
			}
			textBoxLoan.Value = value;
		}

		private void salaryBalance()
		{
			DateTime value = dateTimePickerLastWorkDate.Value;
			int month = value.Month;
			int year = value.Year;
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal result = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal num7 = default(decimal);
			decimal d = default(decimal);
			int totalWorkingMonthHours = CompanyPreferences.TotalWorkingMonthHours;
			DateTime dateTime = new DateTime(year, month, 1);
			int num8 = Convert.ToInt32((double)(value - dateTime).Days);
			num8 = 30;
			DataSet dataSet = Factory.SalarySheetSystem.CalculateSalarySheet(comboBoxEmployee.SelectedID, comboBoxEmployee.SelectedID, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", dateTime, dateTimePickerLastWorkDate.Value, year, month, "");
			if (dataSet.Tables.Count > 1)
			{
				DataTable dataTable = dataSet.Tables[1];
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				num = Convert.ToInt32(dataRow["WorkDays"].ToString());
				num8 = Convert.ToInt32(dataRow["WorkDays"].ToString());
				decimal.TryParse(dataRow["absent"].ToString(), out result);
				num8 = DateTime.DaysInMonth(year, month);
				num2 = num - result;
				foreach (DataRow row in dataTable.Rows)
				{
					IsOT = false;
					IsDD = false;
					int result2 = 1;
					int.TryParse(row["PayType"].ToString(), out result2);
					int result3 = 1;
					int.TryParse(row["PayCodeType"].ToString(), out result3);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Amount"].ToString(), out result4);
					bool result5 = true;
					int result6 = 1;
					bool.TryParse(row["InDeduction"].ToString(), out result5);
					int.TryParse(row["InDeduction"].ToString(), out result6);
					switch (result6)
					{
					case 1:
						result5 = true;
						break;
					case 0:
						result5 = false;
						break;
					}
					decimal result7 = default(decimal);
					decimal num9 = default(decimal);
					switch (result2)
					{
					case 1:
						switch (result3)
						{
						case 1:
							if (result5)
							{
								if ((decimal)num8 * num2 > 0m)
								{
									num9 = ((CompanyPreferences.Annual && (decimal)num8 != num2) ? (result4 * 12m / 365m * num2) : ((CompanyPreferences.ThirtyDays && (decimal)num8 != num2) ? (result4 / 30m * num2) : ((CompanyPreferences.DaysInMonth && (decimal)num8 != num2) ? (result4 / (decimal)num8 * num2) : (((!CompanyPreferences.Annual && !CompanyPreferences.DaysInMonth && !CompanyPreferences.ThirtyDays) || !((decimal)num8 == num2)) ? (result4 / (decimal)num8 * num2) : result4))));
								}
							}
							else
							{
								num9 = result4;
							}
							num3 += num9;
							row["PayableAmount"] = Math.Round(num9, Global.CurDecimalPoints);
							break;
						case 6:
							decimal.TryParse(row["OTHiddenAmount"].ToString(), out result7);
							num9 = ((!IsNewRecord) ? result7 : (result7 * (d / (decimal)totalWorkingMonthHours)));
							if (!IsOT)
							{
								num7 += num9;
								object obj3 = row["PayableAmount"] = (row["Amount"] = Math.Round(num9, Global.CurDecimalPoints));
							}
							else
							{
								row["PayableAmount"] = row["Amount"];
							}
							break;
						case 7:
							num9 = result4;
							num4 += num9;
							row["PayableAmount"] = Math.Round(num9, Global.CurDecimalPoints);
							break;
						case 2:
							if (result5)
							{
								if ((decimal)num8 * num2 > 0m)
								{
									num9 = ((CompanyPreferences.Annual && (decimal)num8 != num2) ? (result4 * 12m / 365m * num2) : ((CompanyPreferences.ThirtyDays && (decimal)num8 != num2) ? (result4 / 30m * num2) : ((CompanyPreferences.DaysInMonth && (decimal)num8 != num2) ? (result4 / (decimal)num8 * num2) : (((!CompanyPreferences.Annual && !CompanyPreferences.DaysInMonth && !CompanyPreferences.ThirtyDays) || !((decimal)num8 == num2)) ? (result4 / (decimal)num8 * num2) : result4))));
								}
							}
							else
							{
								num9 = result4;
							}
							num9 = Math.Round(num9, Global.CurDecimalPoints);
							num4 += num9;
							row["PayableAmount"] = num9;
							break;
						default:
							ErrorHelper.WarningMessage("Unreccogonized payment code type:", result3.ToString());
							break;
						case 0:
							break;
						}
						break;
					case 2:
						num5 += result4;
						if (!IsDD)
						{
							row["Amount"] = Math.Round(result4, Global.CurDecimalPoints);
							row["PayableAmount"] = Math.Round(result4, Global.CurDecimalPoints);
						}
						break;
					case 3:
						num6 += result4;
						row["PayableAmount"] = Math.Round(result4, Global.CurDecimalPoints);
						break;
					}
				}
				decimal num10 = default(decimal);
				foreach (DataRow row2 in dataTable.Rows)
				{
					num10 += decimal.Parse(row2["PayableAmount"].ToString());
				}
				object obj4 = null;
				object obj5 = null;
				obj4 = dataTable.Compute("Sum(PayableAmount)", "PayCodeType = 1");
				obj5 = dataTable.Compute("Sum(PayableAmount)", "PayCodeType <> 1");
				if (obj4 != null && obj4.ToString() != string.Empty && obj4.ToString() != "")
				{
					textBoxSalaryDue.Value = Convert.ToDecimal(obj4);
				}
				if (obj5 != null && obj5.ToString() != string.Empty && obj5.ToString() != "")
				{
					textBoxOtherBenefits.Value = Convert.ToDecimal(obj5);
				}
			}
		}

		private void CalculateTotal()
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal result4 = default(decimal);
			decimal result5 = default(decimal);
			decimal result6 = default(decimal);
			decimal result7 = default(decimal);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal.TryParse(textBoxEOSBenefit.Text, out result);
			decimal.TryParse(textBoxLeaveDue.Text, out result2);
			decimal.TryParse(textBoxSalaryDue.Text, out result3);
			decimal.TryParse(textBoxOtherBenefits.Text, out result4);
			decimal.TryParse(textBoxLoan.Text, out result5);
			decimal.TryParse(textBoxOtherDeductions.Text, out result6);
			decimal.TryParse(textBoxTicketAmount.Text, out result7);
			num = result + result2 + result3 + result4 + result7;
			num2 = num - (result5 + result6);
			textBoxTotal.Value = num;
			textBoxNetTotal.Value = num2;
		}

		private void textBoxEOSBenefit_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxLeaveDue_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxSalaryDue_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxOtherBenefits_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxLoan_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxOtherDeductions_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void buttonPayCash_Click(object sender, EventArgs e)
		{
			try
			{
				CashPaymentForm cashPaymentForm = new CashPaymentForm();
				cashPaymentForm.EntityID = comboBoxEmployee.SelectedID;
				cashPaymentForm.EntityType = "E";
				cashPaymentForm.Amount = ((textBoxNetTotal.Text != string.Empty) ? Convert.ToDecimal(textBoxNetTotal.Text) : 0m);
				cashPaymentForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonPayCheque_Click(object sender, EventArgs e)
		{
			try
			{
				ChequePaymentForm chequePaymentForm = new ChequePaymentForm();
				chequePaymentForm.EntityID = comboBoxEmployee.SelectedID;
				chequePaymentForm.EntityType = "E";
				chequePaymentForm.Amount = ((textBoxNetTotal.Text != string.Empty) ? Convert.ToDecimal(textBoxNetTotal.Text) : 0m);
				chequePaymentForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LabelEmployee_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: false);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					DataSet eOSSettlementToPrint = Factory.EmployeeEOSSettlementSystem.GetEOSSettlementToPrint(comboBoxEmployee.SelectedID);
					if (eOSSettlementToPrint == null || eOSSettlementToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(eOSSettlementToPrint, "", "EOS Settlement", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButton5_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: false);
		}

		private void textBoxTicketAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPayrollItem(comboBoxPayrollItemDeduction.SelectedID);
		}
	}
}
