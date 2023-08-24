using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
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
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CandidateCancellationForm : Form, IDataForm, IDataEntry
	{
		private CandidateData currentData;

		private const string TABLENAME_CONST = "Candidate";

		private const string IDFIELD_CONST = "CandidateID";

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private Panel panel1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel mmLabel69;

		private MMLabel mmLabel53;

		private MMTextBox textBoxPassportNo;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxSurName;

		private MMTextBox textBoxGivenName;

		private MMLabel labelGivenName;

		private UltraGroupBox panelCancel;

		private MMLabel mmLabel1;

		private MMTextBox textBoxRemarks;

		private MMLabel mmLabel2;

		private ComboBox comboBoxCancelType;

		private MMTextBox textBoxNationality;

		private MMLabel mmLabel3;

		private MMTextBox textBoxGender;

		private MMLabel mmLabel4;

		private CheckBox checkBoxIsCancelled;

		private DateTimePicker dateTimePickerCancellation;

		private MMLabel mmLabel92;

		private MMLabel mmLabel5;

		private MMTextBox textBoxReason;

		private Panel panelB;

		private DateTimePicker dateTimeMOLCancellationDate;

		private MMLabel mmLabel7;

		private MMLabel mmLabel6;

		private DateTimePicker dateTimeIMGCancellationDate;

		private Panel panelA;

		private MMLabel mmLabel8;

		private DateTimePicker dateTimeVCAppRecdDate;

		private MMLabel mmLabel9;

		private MMTextBox textBoxMBRefNumber;

		private ToolStripButton toolStripButtonInformation;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool isExist;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private string EmployeeNo
		{
			get;
			set;
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		public bool IsExist
		{
			get
			{
				return isExist;
			}
			set
			{
				XPButton xPButton = buttonSave;
				bool enabled = panelCancel.Enabled = !value;
				xPButton.Enabled = enabled;
				isExist = value;
			}
		}

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
			}
		}

		public CandidateCancellationForm()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CandidateCancellationForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonClose = new Micromind.UISupport.XPButton();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			panelCancel = new Infragistics.Win.Misc.UltraGroupBox();
			dateTimeVCAppRecdDate = new System.Windows.Forms.DateTimePicker();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxMBRefNumber = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			panelB = new System.Windows.Forms.Panel();
			dateTimeMOLCancellationDate = new System.Windows.Forms.DateTimePicker();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			dateTimeIMGCancellationDate = new System.Windows.Forms.DateTimePicker();
			panelA = new System.Windows.Forms.Panel();
			mmLabel92 = new Micromind.UISupport.MMLabel();
			dateTimePickerCancellation = new System.Windows.Forms.DateTimePicker();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			checkBoxIsCancelled = new System.Windows.Forms.CheckBox();
			comboBoxCancelType = new System.Windows.Forms.ComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxGender = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxNationality = new Micromind.UISupport.MMTextBox();
			mmLabel69 = new Micromind.UISupport.MMLabel();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			textBoxPassportNo = new Micromind.UISupport.MMTextBox();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxSurName = new Micromind.UISupport.MMTextBox();
			textBoxGivenName = new Micromind.UISupport.MMTextBox();
			labelGivenName = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelCancel).BeginInit();
			panelCancel.SuspendLayout();
			panelB.SuspendLayout();
			panelA.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 423);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(568, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(568, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(458, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 1;
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
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
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(568, 31);
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
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MaximumSize = new System.Drawing.Size(0, 8);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(568, 8);
			panel1.TabIndex = 314;
			panelCancel.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelCancel.Controls.Add(dateTimeVCAppRecdDate);
			panelCancel.Controls.Add(mmLabel8);
			panelCancel.Controls.Add(textBoxMBRefNumber);
			panelCancel.Controls.Add(mmLabel9);
			panelCancel.Controls.Add(panelB);
			panelCancel.Controls.Add(panelA);
			panelCancel.Controls.Add(mmLabel5);
			panelCancel.Controls.Add(textBoxReason);
			panelCancel.Controls.Add(checkBoxIsCancelled);
			panelCancel.Controls.Add(comboBoxCancelType);
			panelCancel.Controls.Add(mmLabel2);
			panelCancel.Controls.Add(mmLabel1);
			panelCancel.Controls.Add(textBoxRemarks);
			panelCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelCancel.Location = new System.Drawing.Point(15, 191);
			panelCancel.Name = "panelCancel";
			panelCancel.Size = new System.Drawing.Size(531, 228);
			panelCancel.TabIndex = 1;
			dateTimeVCAppRecdDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeVCAppRecdDate.Location = new System.Drawing.Point(120, 36);
			dateTimeVCAppRecdDate.Name = "dateTimeVCAppRecdDate";
			dateTimeVCAppRecdDate.Size = new System.Drawing.Size(113, 20);
			dateTimeVCAppRecdDate.TabIndex = 2;
			dateTimeVCAppRecdDate.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(1, 38);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(116, 13);
			mmLabel8.TabIndex = 332;
			mmLabel8.Text = "VC App. Recvd. Date :";
			textBoxMBRefNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxMBRefNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMBRefNumber.CustomReportFieldName = "";
			textBoxMBRefNumber.CustomReportKey = "";
			textBoxMBRefNumber.CustomReportValueType = 1;
			textBoxMBRefNumber.IsComboTextBox = false;
			textBoxMBRefNumber.IsModified = false;
			textBoxMBRefNumber.Location = new System.Drawing.Point(120, 86);
			textBoxMBRefNumber.MaxLength = 30;
			textBoxMBRefNumber.Name = "textBoxMBRefNumber";
			textBoxMBRefNumber.ReadOnly = true;
			textBoxMBRefNumber.Size = new System.Drawing.Size(205, 20);
			textBoxMBRefNumber.TabIndex = 4;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(1, 89);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(107, 13);
			mmLabel9.TabIndex = 333;
			mmLabel9.Text = "MB Ref No (Cancel) :";
			panelB.Controls.Add(dateTimeMOLCancellationDate);
			panelB.Controls.Add(mmLabel7);
			panelB.Controls.Add(mmLabel6);
			panelB.Controls.Add(dateTimeIMGCancellationDate);
			panelB.Location = new System.Drawing.Point(0, 59);
			panelB.Name = "panelB";
			panelB.Size = new System.Drawing.Size(508, 25);
			panelB.TabIndex = 4;
			panelB.Visible = false;
			dateTimeMOLCancellationDate.Checked = false;
			dateTimeMOLCancellationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeMOLCancellationDate.Location = new System.Drawing.Point(376, 1);
			dateTimeMOLCancellationDate.Name = "dateTimeMOLCancellationDate";
			dateTimeMOLCancellationDate.ShowCheckBox = true;
			dateTimeMOLCancellationDate.Size = new System.Drawing.Size(113, 20);
			dateTimeMOLCancellationDate.TabIndex = 1;
			dateTimeMOLCancellationDate.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(248, 3);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(122, 13);
			mmLabel7.TabIndex = 328;
			mmLabel7.Text = "MOL Cancellation Date :";
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(1, 3);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(120, 13);
			mmLabel6.TabIndex = 327;
			mmLabel6.Text = "IMG Cancellation Date :";
			dateTimeIMGCancellationDate.Checked = false;
			dateTimeIMGCancellationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeIMGCancellationDate.Location = new System.Drawing.Point(121, 1);
			dateTimeIMGCancellationDate.Name = "dateTimeIMGCancellationDate";
			dateTimeIMGCancellationDate.ShowCheckBox = true;
			dateTimeIMGCancellationDate.Size = new System.Drawing.Size(113, 20);
			dateTimeIMGCancellationDate.TabIndex = 0;
			dateTimeIMGCancellationDate.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			panelA.Controls.Add(mmLabel92);
			panelA.Controls.Add(dateTimePickerCancellation);
			panelA.Location = new System.Drawing.Point(0, 59);
			panelA.Name = "panelA";
			panelA.Size = new System.Drawing.Size(508, 25);
			panelA.TabIndex = 3;
			mmLabel92.AutoSize = true;
			mmLabel92.BackColor = System.Drawing.Color.Transparent;
			mmLabel92.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel92.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel92.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel92.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel92.IsFieldHeader = false;
			mmLabel92.IsRequired = false;
			mmLabel92.Location = new System.Drawing.Point(1, 4);
			mmLabel92.Name = "mmLabel92";
			mmLabel92.PenWidth = 1f;
			mmLabel92.ShowBorder = false;
			mmLabel92.Size = new System.Drawing.Size(98, 13);
			mmLabel92.TabIndex = 327;
			mmLabel92.Text = "Cancellation Date :";
			dateTimePickerCancellation.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerCancellation.Location = new System.Drawing.Point(121, 2);
			dateTimePickerCancellation.Name = "dateTimePickerCancellation";
			dateTimePickerCancellation.Size = new System.Drawing.Size(113, 20);
			dateTimePickerCancellation.TabIndex = 3;
			dateTimePickerCancellation.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(1, 113);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(50, 13);
			mmLabel5.TabIndex = 329;
			mmLabel5.Text = "Reason :";
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.IsModified = false;
			textBoxReason.IsRequired = true;
			textBoxReason.Location = new System.Drawing.Point(121, 109);
			textBoxReason.MaxLength = 64;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(387, 20);
			textBoxReason.TabIndex = 5;
			checkBoxIsCancelled.AutoSize = true;
			checkBoxIsCancelled.Location = new System.Drawing.Point(292, 17);
			checkBoxIsCancelled.Name = "checkBoxIsCancelled";
			checkBoxIsCancelled.Size = new System.Drawing.Size(84, 17);
			checkBoxIsCancelled.TabIndex = 1;
			checkBoxIsCancelled.Text = "Is Cancelled";
			checkBoxIsCancelled.UseVisualStyleBackColor = true;
			comboBoxCancelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCancelType.FormattingEnabled = true;
			comboBoxCancelType.Items.AddRange(new object[4]
			{
				"Application",
				"Visa",
				"A/O",
				"OutofCountry"
			});
			comboBoxCancelType.Location = new System.Drawing.Point(120, 13);
			comboBoxCancelType.Name = "comboBoxCancelType";
			comboBoxCancelType.Size = new System.Drawing.Size(154, 21);
			comboBoxCancelType.TabIndex = 0;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(1, 138);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(55, 13);
			mmLabel2.TabIndex = 323;
			mmLabel2.Text = "Remarks :";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(1, 16);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(99, 13);
			mmLabel1.TabIndex = 321;
			mmLabel1.Text = "Cancellation Type :";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(121, 132);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(387, 85);
			textBoxRemarks.TabIndex = 6;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(16, 46);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(109, 13);
			mmLabel4.TabIndex = 331;
			mmLabel4.Text = "VS Application Code :";
			textBoxGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxGender.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGender.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxGender.CustomReportFieldName = "";
			textBoxGender.CustomReportKey = "";
			textBoxGender.CustomReportValueType = 1;
			textBoxGender.IsComboTextBox = false;
			textBoxGender.IsModified = false;
			textBoxGender.Location = new System.Drawing.Point(136, 154);
			textBoxGender.MaxLength = 20;
			textBoxGender.Name = "textBoxGender";
			textBoxGender.ReadOnly = true;
			textBoxGender.Size = new System.Drawing.Size(100, 20);
			textBoxGender.TabIndex = 5;
			textBoxGender.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(16, 157);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(49, 13);
			mmLabel3.TabIndex = 329;
			mmLabel3.Text = "Gender :";
			textBoxNationality.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxNationality.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxNationality.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxNationality.CustomReportFieldName = "";
			textBoxNationality.CustomReportKey = "";
			textBoxNationality.CustomReportValueType = 1;
			textBoxNationality.IsComboTextBox = false;
			textBoxNationality.IsModified = false;
			textBoxNationality.Location = new System.Drawing.Point(136, 132);
			textBoxNationality.MaxLength = 20;
			textBoxNationality.Name = "textBoxNationality";
			textBoxNationality.ReadOnly = true;
			textBoxNationality.Size = new System.Drawing.Size(218, 20);
			textBoxNationality.TabIndex = 4;
			textBoxNationality.TabStop = false;
			mmLabel69.AutoSize = true;
			mmLabel69.BackColor = System.Drawing.Color.Transparent;
			mmLabel69.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel69.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel69.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel69.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel69.IsFieldHeader = false;
			mmLabel69.IsRequired = false;
			mmLabel69.Location = new System.Drawing.Point(16, 114);
			mmLabel69.Name = "mmLabel69";
			mmLabel69.PenWidth = 1f;
			mmLabel69.ShowBorder = false;
			mmLabel69.Size = new System.Drawing.Size(60, 13);
			mmLabel69.TabIndex = 323;
			mmLabel69.Text = "Sur Name :";
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(16, 70);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(72, 13);
			mmLabel53.TabIndex = 322;
			mmLabel53.Text = "Passport No :";
			textBoxPassportNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPassportNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPassportNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPassportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPassportNo.CustomReportFieldName = "";
			textBoxPassportNo.CustomReportKey = "";
			textBoxPassportNo.CustomReportValueType = 1;
			textBoxPassportNo.IsComboTextBox = false;
			textBoxPassportNo.IsModified = false;
			textBoxPassportNo.Location = new System.Drawing.Point(136, 66);
			textBoxPassportNo.MaxLength = 20;
			textBoxPassportNo.Name = "textBoxPassportNo";
			textBoxPassportNo.ReadOnly = true;
			textBoxPassportNo.Size = new System.Drawing.Size(164, 20);
			textBoxPassportNo.TabIndex = 1;
			textBoxPassportNo.TabStop = false;
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(16, 135);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(65, 13);
			lblDescriptions.TabIndex = 320;
			lblDescriptions.Text = "Nationality :";
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(136, 41);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(164, 20);
			textBoxCode.TabIndex = 0;
			textBoxSurName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSurName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSurName.CustomReportFieldName = "";
			textBoxSurName.CustomReportKey = "";
			textBoxSurName.CustomReportValueType = 1;
			textBoxSurName.IsComboTextBox = false;
			textBoxSurName.IsModified = false;
			textBoxSurName.IsRequired = true;
			textBoxSurName.Location = new System.Drawing.Point(136, 110);
			textBoxSurName.MaxLength = 64;
			textBoxSurName.Name = "textBoxSurName";
			textBoxSurName.ReadOnly = true;
			textBoxSurName.Size = new System.Drawing.Size(314, 20);
			textBoxSurName.TabIndex = 3;
			textBoxSurName.TabStop = false;
			textBoxGivenName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGivenName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxGivenName.CustomReportFieldName = "";
			textBoxGivenName.CustomReportKey = "";
			textBoxGivenName.CustomReportValueType = 1;
			textBoxGivenName.IsComboTextBox = false;
			textBoxGivenName.IsModified = false;
			textBoxGivenName.IsRequired = true;
			textBoxGivenName.Location = new System.Drawing.Point(136, 88);
			textBoxGivenName.MaxLength = 64;
			textBoxGivenName.Name = "textBoxGivenName";
			textBoxGivenName.ReadOnly = true;
			textBoxGivenName.Size = new System.Drawing.Size(314, 20);
			textBoxGivenName.TabIndex = 2;
			textBoxGivenName.TabStop = false;
			labelGivenName.AutoSize = true;
			labelGivenName.BackColor = System.Drawing.Color.Transparent;
			labelGivenName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelGivenName.Font = new System.Drawing.Font("Tahoma", 8.25f);
			labelGivenName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelGivenName.IsFieldHeader = false;
			labelGivenName.IsRequired = false;
			labelGivenName.Location = new System.Drawing.Point(16, 92);
			labelGivenName.Name = "labelGivenName";
			labelGivenName.PenWidth = 1f;
			labelGivenName.ShowBorder = false;
			labelGivenName.Size = new System.Drawing.Size(71, 13);
			labelGivenName.TabIndex = 317;
			labelGivenName.Text = "Given Name :";
			labelGivenName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			base.ClientSize = new System.Drawing.Size(568, 463);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(textBoxGender);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(textBoxNationality);
			base.Controls.Add(panelCancel);
			base.Controls.Add(mmLabel69);
			base.Controls.Add(mmLabel53);
			base.Controls.Add(textBoxPassportNo);
			base.Controls.Add(lblDescriptions);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(textBoxSurName);
			base.Controls.Add(textBoxGivenName);
			base.Controls.Add(labelGivenName);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "CandidateCancellationForm";
			Text = "Candidate Cancellation";
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelCancel).EndInit();
			panelCancel.ResumeLayout(false);
			panelCancel.PerformLayout();
			panelB.ResumeLayout(false);
			panelB.PerformLayout();
			panelA.ResumeLayout(false);
			panelA.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private void AddEvents()
		{
			base.Load += CandidateCancellationForm_Load;
			checkBoxIsCancelled.CheckStateChanged += checkBoxIsCancelled_CheckStateChanged;
			comboBoxCancelType.SelectedIndexChanged += comboBoxCancelType_SelectedIndexChanged;
		}

		private void comboBoxCancelType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxCancelType.SelectedIndex == 0)
			{
				panelA.Visible = true;
				panelB.Visible = false;
				textBoxMBRefNumber.Clear();
				textBoxMBRefNumber.ReadOnly = true;
			}
			else
			{
				panelA.Visible = false;
				panelB.Visible = true;
				textBoxMBRefNumber.Clear();
				textBoxMBRefNumber.ReadOnly = false;
			}
		}

		private void checkBoxIsCancelled_CheckStateChanged(object sender, EventArgs e)
		{
		}

		private void CandidateCancellationForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
					ClearForm();
					comboBoxCancelType.SelectedIndex = 0;
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

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.CandidateTable.Rows[0];
			textBoxCode.Text = "VS" + dataRow["CandidateID"].ToString();
			textBoxPassportNo.Text = dataRow["PassportNo"].ToString();
			textBoxGivenName.Text = dataRow["GivenName"].ToString();
			textBoxSurName.Text = dataRow["SurName"].ToString();
			textBoxNationality.Text = dataRow["NationalityID"].ToString();
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
			if (dataRow["IsCancelled"] != DBNull.Value)
			{
				checkBoxIsCancelled.Checked = bool.Parse(dataRow["IsCancelled"].ToString());
			}
			else
			{
				checkBoxIsCancelled.Checked = false;
			}
			comboBoxCancelType.SelectedItem = dataRow["CancellationStage"].ToString();
			if (dataRow["VCAppReceivedDate"] != DBNull.Value)
			{
				dateTimeVCAppRecdDate.Value = DateTime.Parse(dataRow["VCAppReceivedDate"].ToString());
			}
			else
			{
				dateTimeVCAppRecdDate.Value = DateTime.Now;
			}
			if (dataRow["AppCancellationDate"] != DBNull.Value)
			{
				dateTimePickerCancellation.Value = DateTime.Parse(dataRow["AppCancellationDate"].ToString());
			}
			else
			{
				dateTimePickerCancellation.Value = DateTime.Now;
			}
			if (dataRow["IMGCancellationDate"] != DBNull.Value)
			{
				dateTimeIMGCancellationDate.Value = DateTime.Parse(dataRow["IMGCancellationDate"].ToString());
				dateTimeIMGCancellationDate.Checked = true;
			}
			else
			{
				dateTimeIMGCancellationDate.Value = DateTime.Now;
				dateTimeIMGCancellationDate.Checked = false;
			}
			if (dataRow["MOLCancellationDate"] != DBNull.Value)
			{
				dateTimeMOLCancellationDate.Value = DateTime.Parse(dataRow["MOLCancellationDate"].ToString());
				dateTimeMOLCancellationDate.Checked = true;
			}
			else
			{
				dateTimeMOLCancellationDate.Value = DateTime.Now;
				dateTimeMOLCancellationDate.Checked = false;
			}
			textBoxMBRefNumber.Text = dataRow["MBNumberCancel"].ToString();
			textBoxReason.Text = dataRow["CancellationReason"].ToString();
			textBoxRemarks.Text = dataRow["CancellationRemarks"].ToString();
			EmployeeNo = dataRow["EmployeeNo"].ToString();
			IsExist = bool.Parse(dataRow["IsExist"].ToString());
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CandidateData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CandidateTable.Rows[0] : currentData.CandidateTable.NewRow();
				dataRow.BeginEdit();
				dataRow["IsCancelled"] = checkBoxIsCancelled.Checked;
				if (checkBoxIsCancelled.Checked)
				{
					dataRow["VCAppReceivedDate"] = dateTimeVCAppRecdDate.Value;
					dataRow["CancellationStage"] = comboBoxCancelType.SelectedItem;
					if (comboBoxCancelType.SelectedIndex == 0)
					{
						dataRow["AppCancellationDate"] = dateTimePickerCancellation.Value;
						dataRow["IMGCancellationDate"] = DBNull.Value;
						dataRow["MOLCancellationDate"] = DBNull.Value;
						dataRow["MBNumberCancel"] = DBNull.Value;
					}
					else
					{
						dataRow["AppCancellationDate"] = DBNull.Value;
						if (dateTimeIMGCancellationDate.Checked)
						{
							dataRow["IMGCancellationDate"] = dateTimeIMGCancellationDate.Value;
						}
						else
						{
							dataRow["IMGCancellationDate"] = DBNull.Value;
						}
						if (dateTimeMOLCancellationDate.Checked)
						{
							dataRow["MOLCancellationDate"] = dateTimeMOLCancellationDate.Value;
						}
						else
						{
							dataRow["MOLCancellationDate"] = DBNull.Value;
						}
						dataRow["MBNumberCancel"] = textBoxMBRefNumber.Text;
					}
					dataRow["CancellationReason"] = textBoxReason.Text.Trim();
					dataRow["CancellationRemarks"] = textBoxRemarks.Text.Trim();
				}
				else
				{
					dataRow["VCAppReceivedDate"] = DBNull.Value;
					dataRow["CancellationStage"] = DBNull.Value;
					dataRow["AppCancellationDate"] = DBNull.Value;
					dataRow["IMGCancellationDate"] = DBNull.Value;
					dataRow["MOLCancellationDate"] = DBNull.Value;
					dataRow["MBNumberCancel"] = DBNull.Value;
					dataRow["CancellationReason"] = DBNull.Value;
					dataRow["Remarks"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CandidateTable.Rows.Add(dataRow);
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
				if (Factory.CandidateSystem.IsEmployee(EmployeeNo))
				{
					ErrorHelper.ErrorMessage("You can not cancel candidate which exist as an employoee! Please follow the employee cancellation process");
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
				if (checkBoxIsCancelled.Checked)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to cancel this candidate ?"))
					{
					case DialogResult.No:
						return true;
					case DialogResult.Cancel:
						return false;
					}
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
				bool flag = Factory.CandidateSystem.CancelCandidate(currentData);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Candidate, needRefresh: true);
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
					currentData = Factory.CandidateSystem.GetCandidateByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
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
			finally
			{
				PublicFunctions.EndWaiting(this);
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
			string currentID = (textBoxCode.Text != string.Empty) ? textBoxCode.Text.Substring(2) : string.Empty;
			LoadData(DatabaseHelper.GetPreviousID("Candidate", "CandidateID", currentID));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string currentID = (textBoxCode.Text != string.Empty) ? textBoxCode.Text.Substring(2) : string.Empty;
			LoadData(DatabaseHelper.GetNextID("Candidate", "CandidateID", currentID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Candidate", "CandidateID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Candidate", "CandidateID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Candidate", "CandidateID", toolStripTextBoxFind.Text.Trim()))
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
				ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?");
				_ = 7;
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Clear();
			textBoxPassportNo.Clear();
			textBoxGivenName.Clear();
			textBoxSurName.Clear();
			textBoxNationality.Clear();
			textBoxGender.Clear();
			comboBoxCancelType.SelectedIndex = 0;
			checkBoxIsCancelled.Checked = false;
			dateTimeVCAppRecdDate.Value = DateTime.Now;
			dateTimePickerCancellation.Value = DateTime.Now;
			dateTimeIMGCancellationDate.Value = DateTime.Now;
			dateTimeMOLCancellationDate.Value = DateTime.Now;
			dateTimeIMGCancellationDate.Checked = false;
			dateTimeMOLCancellationDate.Checked = false;
			textBoxMBRefNumber.Clear();
			textBoxReason.Clear();
			textBoxRemarks.Clear();
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
			new FormHelper().ShowList(DataComboType.Candidate);
		}
	}
}
