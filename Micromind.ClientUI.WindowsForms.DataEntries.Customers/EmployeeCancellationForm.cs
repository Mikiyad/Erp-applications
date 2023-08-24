using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
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
	public class EmployeeCancellationForm : Form, IDataForm, IDataEntry
	{
		private EmployeeActivityData currentData;

		private const string TABLENAME_CONST = "Employee_Cancellation";

		private const string IDFIELD_CONST = "ActivityID";

		private bool isNewRecord = true;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonClose;

		private XPButton buttonSave;

		private ToolStrip toolStrip1;

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

		private UltraGroupBox panelCancel;

		private PortComboBox comboBoxExitPort;

		private MMLabel mmLabel29;

		private MMLabel mmLabel11;

		private MMLabel mmLabel18;

		private MMLabel mmLabel17;

		private MMLabel mmLabel16;

		private MMTextBox textBoxMBRefNumber;

		private MMLabel mmLabel15;

		private MMLabel mmLabel14;

		private MMLabel mmLabel13;

		private ComboBox comboBoxCancellationType;

		private MMTextBox textBoxRemarks;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel5;

		private MMLabel mmLabel2;

		private MMLabel mmLabel1;

		private DateTimePicker dateTimeTransactionDate;

		private MMSDateTimePicker dateTimeVCAppRecdDate;

		private MMSDateTimePicker dateTimeVCAppSubDate;

		private MMSDateTimePicker dateTimeVCAppTypedDate;

		private MMSDateTimePicker dateTimeDepartureDate;

		private MMSDateTimePicker dateTimeRPCancelledDate;

		private XPButton buttonDelete;

		private MMLabel labelLastworking;

		private UltraFormattedLinkLabel ultraLinkVoucherNumber;

		private MMLabel mmLabel4;

		private MMSDateTimePicker dateTimePickerLastWorkingDate;

		private MMSDateTimePicker dateTimeSignedCNDocRecvd;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool isCancelled;

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

		public EmployeeCancellationForm()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.EmployeeCancellationForm));
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
			panel1 = new System.Windows.Forms.Panel();
			panelCancel = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			labelLastworking = new Micromind.UISupport.MMLabel();
			dateTimeDepartureDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeRPCancelledDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeVCAppSubDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeVCAppTypedDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeVCAppRecdDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimeTransactionDate = new System.Windows.Forms.DateTimePicker();
			comboBoxExitPort = new Micromind.DataControls.PortComboBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxMBRefNumber = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			comboBoxCancellationType = new System.Windows.Forms.ComboBox();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
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
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			textBoxGender = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			ultraLinkVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimeSignedCNDocRecvd = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerLastWorkingDate = new Micromind.UISupport.MMSDateTimePicker(components);
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelCancel).BeginInit();
			panelCancel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxExitPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 492);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(591, 40);
			panelButtons.TabIndex = 2;
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
			linePanelDown.Size = new System.Drawing.Size(591, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(481, 8);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(591, 31);
			toolStrip1.TabIndex = 306;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MaximumSize = new System.Drawing.Size(0, 8);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(591, 8);
			panel1.TabIndex = 314;
			panelCancel.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelCancel.Controls.Add(dateTimePickerLastWorkingDate);
			panelCancel.Controls.Add(dateTimeSignedCNDocRecvd);
			panelCancel.Controls.Add(mmLabel4);
			panelCancel.Controls.Add(labelLastworking);
			panelCancel.Controls.Add(dateTimeDepartureDate);
			panelCancel.Controls.Add(dateTimeRPCancelledDate);
			panelCancel.Controls.Add(dateTimeVCAppSubDate);
			panelCancel.Controls.Add(dateTimeVCAppTypedDate);
			panelCancel.Controls.Add(dateTimeVCAppRecdDate);
			panelCancel.Controls.Add(mmLabel1);
			panelCancel.Controls.Add(dateTimeTransactionDate);
			panelCancel.Controls.Add(comboBoxExitPort);
			panelCancel.Controls.Add(mmLabel29);
			panelCancel.Controls.Add(mmLabel11);
			panelCancel.Controls.Add(mmLabel18);
			panelCancel.Controls.Add(mmLabel17);
			panelCancel.Controls.Add(mmLabel16);
			panelCancel.Controls.Add(textBoxMBRefNumber);
			panelCancel.Controls.Add(mmLabel15);
			panelCancel.Controls.Add(mmLabel14);
			panelCancel.Controls.Add(mmLabel13);
			panelCancel.Controls.Add(comboBoxCancellationType);
			panelCancel.Controls.Add(textBoxRemarks);
			panelCancel.Controls.Add(textBoxReason);
			panelCancel.Controls.Add(mmLabel5);
			panelCancel.Controls.Add(mmLabel2);
			panelCancel.Location = new System.Drawing.Point(8, 215);
			panelCancel.Name = "panelCancel";
			panelCancel.Size = new System.Drawing.Size(571, 275);
			panelCancel.TabIndex = 1;
			panelCancel.Click += new System.EventHandler(panelCancel_Click);
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(261, 41);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(128, 13);
			mmLabel4.TabIndex = 404;
			mmLabel4.Text = "Signed CNDoc Recieved :";
			labelLastworking.AutoSize = true;
			labelLastworking.BackColor = System.Drawing.Color.Transparent;
			labelLastworking.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelLastworking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelLastworking.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelLastworking.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelLastworking.IsFieldHeader = false;
			labelLastworking.IsRequired = false;
			labelLastworking.Location = new System.Drawing.Point(282, 70);
			labelLastworking.Name = "labelLastworking";
			labelLastworking.PenWidth = 1f;
			labelLastworking.ShowBorder = false;
			labelLastworking.Size = new System.Drawing.Size(102, 13);
			labelLastworking.TabIndex = 402;
			labelLastworking.Text = "Last Working Date :";
			dateTimeDepartureDate.Checked = false;
			dateTimeDepartureDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeDepartureDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeDepartureDate.Location = new System.Drawing.Point(144, 145);
			dateTimeDepartureDate.Name = "dateTimeDepartureDate";
			dateTimeDepartureDate.ShowCheckBox = true;
			dateTimeDepartureDate.Size = new System.Drawing.Size(114, 20);
			dateTimeDepartureDate.TabIndex = 7;
			dateTimeDepartureDate.Value = new System.DateTime(0L);
			dateTimeRPCancelledDate.Checked = false;
			dateTimeRPCancelledDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeRPCancelledDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRPCancelledDate.Location = new System.Drawing.Point(144, 122);
			dateTimeRPCancelledDate.Name = "dateTimeRPCancelledDate";
			dateTimeRPCancelledDate.ShowCheckBox = true;
			dateTimeRPCancelledDate.Size = new System.Drawing.Size(114, 20);
			dateTimeRPCancelledDate.TabIndex = 6;
			dateTimeRPCancelledDate.Value = new System.DateTime(0L);
			dateTimeVCAppSubDate.Checked = false;
			dateTimeVCAppSubDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVCAppSubDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVCAppSubDate.Location = new System.Drawing.Point(144, 78);
			dateTimeVCAppSubDate.Name = "dateTimeVCAppSubDate";
			dateTimeVCAppSubDate.ShowCheckBox = true;
			dateTimeVCAppSubDate.Size = new System.Drawing.Size(114, 20);
			dateTimeVCAppSubDate.TabIndex = 4;
			dateTimeVCAppSubDate.Value = new System.DateTime(0L);
			dateTimeVCAppTypedDate.Checked = false;
			dateTimeVCAppTypedDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVCAppTypedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVCAppTypedDate.Location = new System.Drawing.Point(143, 57);
			dateTimeVCAppTypedDate.Name = "dateTimeVCAppTypedDate";
			dateTimeVCAppTypedDate.ShowCheckBox = true;
			dateTimeVCAppTypedDate.Size = new System.Drawing.Size(114, 20);
			dateTimeVCAppTypedDate.TabIndex = 3;
			dateTimeVCAppTypedDate.Value = new System.DateTime(0L);
			dateTimeVCAppRecdDate.Checked = false;
			dateTimeVCAppRecdDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVCAppRecdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVCAppRecdDate.Location = new System.Drawing.Point(143, 35);
			dateTimeVCAppRecdDate.Name = "dateTimeVCAppRecdDate";
			dateTimeVCAppRecdDate.ShowCheckBox = true;
			dateTimeVCAppRecdDate.Size = new System.Drawing.Size(114, 20);
			dateTimeVCAppRecdDate.TabIndex = 2;
			dateTimeVCAppRecdDate.Value = new System.DateTime(0L);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(274, 16);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(110, 13);
			mmLabel1.TabIndex = 400;
			mmLabel1.Text = "Transaction Date :";
			dateTimeTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeTransactionDate.Location = new System.Drawing.Point(387, 14);
			dateTimeTransactionDate.Name = "dateTimeTransactionDate";
			dateTimeTransactionDate.Size = new System.Drawing.Size(113, 20);
			dateTimeTransactionDate.TabIndex = 1;
			dateTimeTransactionDate.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			comboBoxExitPort.Assigned = false;
			comboBoxExitPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxExitPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExitPort.CustomReportFieldName = "";
			comboBoxExitPort.CustomReportKey = "";
			comboBoxExitPort.CustomReportValueType = 1;
			comboBoxExitPort.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExitPort.DisplayLayout.Appearance = appearance;
			comboBoxExitPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExitPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExitPort.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExitPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxExitPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExitPort.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxExitPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExitPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExitPort.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExitPort.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxExitPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExitPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExitPort.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExitPort.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxExitPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExitPort.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExitPort.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxExitPort.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxExitPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExitPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxExitPort.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxExitPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExitPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxExitPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExitPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExitPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExitPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExitPort.Editable = true;
			comboBoxExitPort.FilterString = "";
			comboBoxExitPort.HasAllAccount = false;
			comboBoxExitPort.HasCustom = false;
			comboBoxExitPort.IsDataLoaded = false;
			comboBoxExitPort.Location = new System.Drawing.Point(144, 165);
			comboBoxExitPort.MaxDropDownItems = 12;
			comboBoxExitPort.Name = "comboBoxExitPort";
			comboBoxExitPort.ShowInactiveItems = false;
			comboBoxExitPort.ShowQuickAdd = true;
			comboBoxExitPort.Size = new System.Drawing.Size(129, 20);
			comboBoxExitPort.TabIndex = 8;
			comboBoxExitPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(12, 167);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(52, 13);
			mmLabel29.TabIndex = 398;
			mmLabel29.Text = "Exit Port:";
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(12, 211);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(55, 13);
			mmLabel11.TabIndex = 396;
			mmLabel11.Text = "Remarks :";
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(12, 145);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(89, 13);
			mmLabel18.TabIndex = 393;
			mmLabel18.Text = "Departure Date :";
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(12, 124);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(121, 13);
			mmLabel17.TabIndex = 392;
			mmLabel17.Text = "RP Cancelled on (IMG) :";
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(12, 78);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(117, 13);
			mmLabel16.TabIndex = 390;
			mmLabel16.Text = "VC App Sub on (MOL) :";
			textBoxMBRefNumber.BackColor = System.Drawing.Color.White;
			textBoxMBRefNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMBRefNumber.CustomReportFieldName = "";
			textBoxMBRefNumber.CustomReportKey = "";
			textBoxMBRefNumber.CustomReportValueType = 1;
			textBoxMBRefNumber.IsComboTextBox = false;
			textBoxMBRefNumber.IsModified = false;
			textBoxMBRefNumber.Location = new System.Drawing.Point(144, 100);
			textBoxMBRefNumber.MaxLength = 30;
			textBoxMBRefNumber.Name = "textBoxMBRefNumber";
			textBoxMBRefNumber.Size = new System.Drawing.Size(205, 20);
			textBoxMBRefNumber.TabIndex = 5;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(12, 101);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(107, 13);
			mmLabel15.TabIndex = 389;
			mmLabel15.Text = "MB Ref No (Cancel) :";
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(12, 58);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(129, 13);
			mmLabel14.TabIndex = 386;
			mmLabel14.Text = "VC App Typed on (MOL) :";
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(12, 37);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(116, 13);
			mmLabel13.TabIndex = 385;
			mmLabel13.Text = "VC App. Recvd. Date :";
			comboBoxCancellationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCancellationType.FormattingEnabled = true;
			comboBoxCancellationType.Items.AddRange(new object[4]
			{
				"Termination",
				"Resignation",
				"Absconded",
				"OutofCountry"
			});
			comboBoxCancellationType.Location = new System.Drawing.Point(144, 13);
			comboBoxCancellationType.Name = "comboBoxCancellationType";
			comboBoxCancellationType.Size = new System.Drawing.Size(113, 21);
			comboBoxCancellationType.TabIndex = 0;
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(144, 210);
			textBoxRemarks.MaxLength = 1000;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(358, 59);
			textBoxRemarks.TabIndex = 10;
			textBoxRemarks.TextChanged += new System.EventHandler(textBoxRemarks_TextChanged);
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.IsModified = false;
			textBoxReason.Location = new System.Drawing.Point(144, 186);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(358, 20);
			textBoxReason.TabIndex = 9;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(12, 16);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(113, 13);
			mmLabel5.TabIndex = 382;
			mmLabel5.Text = "Cancellation Type :";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 189);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(50, 13);
			mmLabel2.TabIndex = 381;
			mmLabel2.Text = "Reason :";
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
			textBoxConfirmation.IsModified = false;
			textBoxConfirmation.IsRequired = true;
			textBoxConfirmation.Location = new System.Drawing.Point(452, 134);
			textBoxConfirmation.MaxLength = 64;
			textBoxConfirmation.Name = "textBoxConfirmation";
			textBoxConfirmation.ReadOnly = true;
			textBoxConfirmation.Size = new System.Drawing.Size(117, 20);
			textBoxConfirmation.TabIndex = 360;
			textBoxConfirmation.TabStop = false;
			textBoxJoining.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJoining.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxJoining.CustomReportFieldName = "";
			textBoxJoining.CustomReportKey = "";
			textBoxJoining.CustomReportValueType = 1;
			textBoxJoining.IsComboTextBox = false;
			textBoxJoining.IsModified = false;
			textBoxJoining.IsRequired = true;
			textBoxJoining.Location = new System.Drawing.Point(452, 112);
			textBoxJoining.MaxLength = 64;
			textBoxJoining.Name = "textBoxJoining";
			textBoxJoining.ReadOnly = true;
			textBoxJoining.Size = new System.Drawing.Size(117, 20);
			textBoxJoining.TabIndex = 359;
			textBoxJoining.TabStop = false;
			textBoxLabourID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLabourID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLabourID.CustomReportFieldName = "";
			textBoxLabourID.CustomReportKey = "";
			textBoxLabourID.CustomReportValueType = 1;
			textBoxLabourID.IsComboTextBox = false;
			textBoxLabourID.IsModified = false;
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
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(452, 45);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(117, 20);
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
			mmLabel38.Location = new System.Drawing.Point(373, 136);
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
			mmLabel10.Location = new System.Drawing.Point(21, 181);
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
			mmLabel9.Location = new System.Drawing.Point(373, 114);
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
			textBoxDepartment.IsModified = false;
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
			mmLabel8.Location = new System.Drawing.Point(21, 159);
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
			textBoxDivision.IsModified = false;
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
			mmLabel7.Location = new System.Drawing.Point(21, 135);
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
			textBoxLocation.IsModified = false;
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
			mmLabel6.Location = new System.Drawing.Point(21, 114);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(54, 13);
			mmLabel6.TabIndex = 340;
			mmLabel6.Text = "Location :";
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance13;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			textBoxEmployeeName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxEmployeeName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
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
			textBoxGender.IsModified = false;
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
			mmLabel3.Location = new System.Drawing.Point(397, 92);
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
			mmLabel53.Location = new System.Drawing.Point(21, 69);
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
			lblDescriptions.Location = new System.Drawing.Point(21, 92);
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
			textBoxDesignation.IsModified = false;
			textBoxDesignation.IsRequired = true;
			textBoxDesignation.Location = new System.Drawing.Point(121, 89);
			textBoxDesignation.MaxLength = 64;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(164, 20);
			textBoxDesignation.TabIndex = 2;
			textBoxDesignation.TabStop = false;
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
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraLinkVoucherNumber.Appearance = appearance25;
			ultraLinkVoucherNumber.AutoSize = true;
			ultraLinkVoucherNumber.Location = new System.Drawing.Point(20, 47);
			ultraLinkVoucherNumber.Name = "ultraLinkVoucherNumber";
			ultraLinkVoucherNumber.Size = new System.Drawing.Size(93, 15);
			ultraLinkVoucherNumber.TabIndex = 362;
			ultraLinkVoucherNumber.TabStop = true;
			ultraLinkVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkVoucherNumber.Value = "Employee Code:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraLinkVoucherNumber.VisitedLinkAppearance = appearance26;
			ultraLinkVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkVoucherNumber_LinkClicked);
			dateTimeSignedCNDocRecvd.Checked = false;
			dateTimeSignedCNDocRecvd.CustomFormat = " dd-MMM-yyyy";
			dateTimeSignedCNDocRecvd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeSignedCNDocRecvd.Location = new System.Drawing.Point(387, 39);
			dateTimeSignedCNDocRecvd.Name = "dateTimeSignedCNDocRecvd";
			dateTimeSignedCNDocRecvd.ShowCheckBox = true;
			dateTimeSignedCNDocRecvd.Size = new System.Drawing.Size(114, 20);
			dateTimeSignedCNDocRecvd.TabIndex = 405;
			dateTimeSignedCNDocRecvd.Value = new System.DateTime(0L);
			dateTimePickerLastWorkingDate.Checked = false;
			dateTimePickerLastWorkingDate.CustomFormat = " dd-MMM-yyyy";
			dateTimePickerLastWorkingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerLastWorkingDate.Location = new System.Drawing.Point(386, 66);
			dateTimePickerLastWorkingDate.Name = "dateTimePickerLastWorkingDate";
			dateTimePickerLastWorkingDate.ShowCheckBox = true;
			dateTimePickerLastWorkingDate.Size = new System.Drawing.Size(114, 20);
			dateTimePickerLastWorkingDate.TabIndex = 406;
			dateTimePickerLastWorkingDate.Value = new System.DateTime(0L);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(591, 532);
			base.Controls.Add(ultraLinkVoucherNumber);
			base.Controls.Add(panelCancel);
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
			base.Name = "EmployeeCancellationForm";
			Text = "Employee Cancellation";
			base.Load += new System.EventHandler(EmployeeCancellationForm_Load_1);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelCancel).EndInit();
			panelCancel.ResumeLayout(false);
			panelCancel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxExitPort).EndInit();
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
				comboBoxCancellationType.SelectedIndex = 0;
				dateTimeTransactionDate.Value = DateTime.Now;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
					ClearForm(newRecord: true);
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
				ClearForm(newRecord: false);
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				DataSet employeeBriefInfo = Factory.EmployeeSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
				if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["ActivityID"].ToString();
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
				if (textBoxCode.Text != string.Empty)
				{
					LoadData(int.Parse(textBoxCode.Text));
				}
				else
				{
					isNewRecord = true;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillData()
		{
			checked
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
					textBoxCode.Text = dataRow["ActivityID"].ToString();
					comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
					textBoxRemarks.Text = dataRow["Note"].ToString();
					textBoxReason.Text = dataRow["Reason"].ToString();
					if (dataRow["TransactionDate"] != DBNull.Value)
					{
						dateTimeTransactionDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					}
					dataRow = currentData.Tables["Employee_Cancellation"].Rows[0];
					if (dataRow["CancellationType"] != DBNull.Value)
					{
						comboBoxCancellationType.SelectedIndex = unchecked((int)byte.Parse(dataRow["CancellationType"].ToString())) - 2;
					}
					if (dataRow["VCAppReceivedDate"] != DBNull.Value)
					{
						dateTimeVCAppRecdDate.Value = DateTime.Parse(dataRow["VCAppReceivedDate"].ToString());
						dateTimeVCAppRecdDate.Checked = true;
					}
					else
					{
						dateTimeVCAppRecdDate.Checked = false;
					}
					if (dataRow["VCAppTypedDate"] != DBNull.Value)
					{
						dateTimeVCAppTypedDate.Value = DateTime.Parse(dataRow["VCAppTypedDate"].ToString());
						dateTimeVCAppTypedDate.Checked = true;
					}
					else
					{
						dateTimeVCAppTypedDate.Checked = false;
					}
					if (dataRow["SignedCNDOCRecvdDate"] != DBNull.Value)
					{
						dateTimeSignedCNDocRecvd.Value = DateTime.Parse(dataRow["SignedCNDOCRecvdDate"].ToString());
						dateTimeSignedCNDocRecvd.Checked = true;
					}
					else
					{
						dateTimeSignedCNDocRecvd.Checked = false;
					}
					if (dataRow["VCAppSubmittedDate"] != DBNull.Value)
					{
						dateTimeVCAppSubDate.Value = DateTime.Parse(dataRow["VCAppSubmittedDate"].ToString());
						dateTimeVCAppSubDate.Checked = true;
					}
					else
					{
						dateTimeVCAppSubDate.Checked = false;
					}
					textBoxMBRefNumber.Text = dataRow["MBReferenceNoCancel"].ToString();
					if (dataRow["RPCancelDateIMG"] != DBNull.Value)
					{
						dateTimeRPCancelledDate.Value = DateTime.Parse(dataRow["RPCancelDateIMG"].ToString());
						dateTimeRPCancelledDate.Checked = true;
					}
					else
					{
						dateTimeRPCancelledDate.Checked = false;
					}
					if (dataRow["DepartureDate"] != DBNull.Value)
					{
						dateTimeDepartureDate.Value = DateTime.Parse(dataRow["DepartureDate"].ToString());
						dateTimeDepartureDate.Checked = true;
					}
					else
					{
						dateTimeDepartureDate.Checked = false;
					}
					comboBoxExitPort.SelectedID = dataRow["ExitPort"].ToString();
					if (dataRow["LastWorkingDate"] != DBNull.Value)
					{
						dateTimePickerLastWorkingDate.Value = DateTime.Parse(dataRow["LastWorkingDate"].ToString());
						dateTimePickerLastWorkingDate.Checked = true;
					}
					else
					{
						dateTimePickerLastWorkingDate.Checked = false;
					}
				}
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.Cancellation);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["ActivityType"] = (byte)15;
				dataRow["TransactionDate"] = dateTimeTransactionDate.Value;
				dataRow["Reason"] = textBoxReason.Text;
				dataRow["Note"] = textBoxRemarks.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeCancellationTable.Rows[0] : currentData.EmployeeCancellationTable.NewRow());
				dataRow["CancellationType"] = checked(comboBoxCancellationType.SelectedIndex + 2);
				if (dateTimeVCAppRecdDate.Checked)
				{
					dataRow["VCAppReceivedDate"] = dateTimeVCAppRecdDate.Value;
				}
				else
				{
					dataRow["VCAppReceivedDate"] = DBNull.Value;
				}
				if (dateTimeVCAppTypedDate.Checked)
				{
					dataRow["VCAppTypedDate"] = dateTimeVCAppTypedDate.Value;
				}
				else
				{
					dataRow["VCAppTypedDate"] = DBNull.Value;
				}
				if (dateTimeVCAppSubDate.Checked)
				{
					dataRow["VCAppSubmittedDate"] = dateTimeVCAppSubDate.Value;
				}
				else
				{
					dataRow["VCAppSubmittedDate"] = DBNull.Value;
				}
				if (dateTimeSignedCNDocRecvd.Checked)
				{
					dataRow["SignedCNDOCRecvdDate"] = dateTimeSignedCNDocRecvd.Value;
				}
				else
				{
					dataRow["SignedCNDOCRecvdDate"] = DBNull.Value;
				}
				dataRow["MBReferenceNoCancel"] = textBoxMBRefNumber.Text;
				if (dateTimeRPCancelledDate.Checked)
				{
					dataRow["RPCancelDateIMG"] = dateTimeRPCancelledDate.Value;
				}
				else
				{
					dataRow["RPCancelDateIMG"] = DBNull.Value;
				}
				if (dateTimeDepartureDate.Checked)
				{
					dataRow["DepartureDate"] = dateTimeDepartureDate.Value;
				}
				else
				{
					dataRow["DepartureDate"] = DBNull.Value;
				}
				if (dateTimePickerLastWorkingDate.Checked)
				{
					dataRow["LastWorkingDate"] = dateTimePickerLastWorkingDate.Value;
				}
				else
				{
					dataRow["LastWorkingDate"] = DBNull.Value;
				}
				dataRow["ExitPort"] = comboBoxExitPort.SelectedID;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeCancellationTable.Rows.Add(dataRow);
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
				if (comboBoxEmployee.SelectedID == "" || comboBoxCancellationType.SelectedIndex == -1)
				{
					ErrorHelper.InformationMessage("Please enter required fields.");
					return false;
				}
				if (dateTimePickerLastWorkingDate.Value >= dateTimeTransactionDate.Value)
				{
					ErrorHelper.InformationMessage("Last working Date should be Less than Transaction Date");
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
				switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to cancel this employee ?"))
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
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Cancellation, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Cancellation, isUpdate: false);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm(newRecord: true);
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
				currentData = Factory.EmployeeActivitySystem.GetEmployeeActivityByID(id);
				if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
				{
					ClearForm(newRecord: true);
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
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_Cancellation", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_Cancellation", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_Cancellation", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_Cancellation", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ClearForm(bool newRecord)
		{
			if (newRecord)
			{
				comboBoxEmployee.SelectedID = "";
			}
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
			dateTimeTransactionDate.Value = DateTime.Now;
			dateTimePickerLastWorkingDate.Clear();
			MMSDateTimePicker mMSDateTimePicker = dateTimeVCAppRecdDate;
			MMSDateTimePicker mMSDateTimePicker2 = dateTimeVCAppTypedDate;
			MMSDateTimePicker mMSDateTimePicker3 = dateTimeVCAppSubDate;
			MMSDateTimePicker mMSDateTimePicker4 = dateTimeRPCancelledDate;
			MMSDateTimePicker mMSDateTimePicker5 = dateTimeDepartureDate;
			MMSDateTimePicker mMSDateTimePicker6 = dateTimeSignedCNDocRecvd;
			bool flag2 = dateTimePickerLastWorkingDate.Checked = false;
			bool flag4 = mMSDateTimePicker6.Checked = flag2;
			bool flag6 = mMSDateTimePicker5.Checked = flag4;
			bool flag8 = mMSDateTimePicker4.Checked = flag6;
			bool flag10 = mMSDateTimePicker3.Checked = flag8;
			bool @checked = mMSDateTimePicker2.Checked = flag10;
			mMSDateTimePicker.Checked = @checked;
			comboBoxCancellationType.SelectedIndex = -1;
			comboBoxExitPort.Clear();
			textBoxMBRefNumber.Clear();
			textBoxReason.Clear();
			textBoxRemarks.Clear();
			formManager.ResetDirty();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			ClearForm(newRecord: true);
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
				return Factory.EmployeeActivitySystem.DeleteActivity(textBoxCode.Text, EmployeeActivityTypes.Cancellation);
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
				ClearForm(newRecord: true);
				IsNewRecord = true;
			}
		}

		private void toolStripButtonOpenList_Click_1(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Employee);
		}

		private void panelCancel_Click(object sender, EventArgs e)
		{
		}

		private void EmployeeCancellationForm_Load_1(object sender, EventArgs e)
		{
		}

		private void dateTimePickerLastWorkingDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void ultraLinkVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void textBoxRemarks_TextChanged(object sender, EventArgs e)
		{
		}
	}
}
