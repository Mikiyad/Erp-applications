using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ContainerStatusChangingForm : Form, IForm
	{
		private ServiceItemData currentData;

		private const string TABLENAME_CONST = "Service_Item";

		private const string IDFIELD_CONST = "ServiceItemID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private Label label2;

		private TextBox textBoxContainerNumber;

		private Label label13;

		private TextBox textBoxBOL;

		private MMLabel mmLabel1;

		private DateTimePicker dateTimePickerDate;

		private Label label1;

		private TextBox textBoxCurrentStatus;

		private Label label3;

		private TextBox textBoxChangingTo;

		private UltraGroupBox groupBoxDocuments;

		private CheckBox checkBoxOriginCertificate;

		private CheckBox checkBoxhealthCertificate;

		private CheckBox checkBoxPL;

		private CheckBox checkBoxInvoice;

		private CheckBox checkBoxBL;

		private UltraGroupBox groupBoxDelivery;

		private Label label5;

		private Label label4;

		private NumberTextBox textBoxDays;

		private MMSDateTimePicker dateTimePickerFreeTimeDate;

		private Label labelDriver;

		private TextBox textBoxDriver;

		private TransporterComboBox comboBoxTransporter;

		private UltraFormattedLinkLabel labelTransporter;

		private TextBox textBoxTruckNumber;

		private Label labelTrcukNumber;

		private Label label22;

		private MMTextBox textBoxRemarks;

		private TextBox textBoxNote;

		private Label label6;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6011;

		public ScreenTypes ScreenType => ScreenTypes.Other;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
				}
			}
		}

		public ContainerStatusChangingForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ServiceItemForm_Load;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
		}

		private void ClearForm()
		{
		}

		private void ServiceItemForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsDisposed)
				{
					textBoxCurrentStatus.Text = "";
					textBoxChangingTo.Text = "Shipped";
					textBoxContainerNumber.Text = "SEGU9163271";
					textBoxBOL.Text = "COSU6112712250";
					checkBoxBL.Checked = false;
					checkBoxInvoice.Checked = false;
					checkBoxhealthCertificate.Checked = false;
					if (textBoxChangingTo.Text == "Shipped")
					{
						groupBoxDocuments.Enabled = false;
					}
					else if (textBoxChangingTo.Text == "Original Document Received")
					{
						groupBoxDocuments.Enabled = true;
					}
					else
					{
						groupBoxDocuments.Enabled = false;
					}
					if (textBoxChangingTo.Text == "Shipment Removal")
					{
						groupBoxDelivery.Enabled = true;
					}
					else
					{
						groupBoxDelivery.Enabled = false;
					}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ContainerStatusChangingForm));
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
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
			panelButtons = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			textBoxContainerNumber = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			textBoxBOL = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			textBoxCurrentStatus = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxChangingTo = new System.Windows.Forms.TextBox();
			groupBoxDocuments = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxOriginCertificate = new System.Windows.Forms.CheckBox();
			checkBoxhealthCertificate = new System.Windows.Forms.CheckBox();
			checkBoxPL = new System.Windows.Forms.CheckBox();
			checkBoxInvoice = new System.Windows.Forms.CheckBox();
			checkBoxBL = new System.Windows.Forms.CheckBox();
			groupBoxDelivery = new Infragistics.Win.Misc.UltraGroupBox();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			labelDriver = new System.Windows.Forms.Label();
			textBoxDriver = new System.Windows.Forms.TextBox();
			labelTransporter = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTruckNumber = new System.Windows.Forms.TextBox();
			labelTrcukNumber = new System.Windows.Forms.Label();
			label22 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			dateTimePickerFreeTimeDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxTransporter = new Micromind.DataControls.TransporterComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDocuments).BeginInit();
			groupBoxDocuments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDelivery).BeginInit();
			groupBoxDelivery.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(627, 25);
			toolStrip1.TabIndex = 14;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(23, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(23, 22);
			toolStripButtonLast.Text = "Last";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(23, 22);
			toolStripButtonOpenList.Text = "Open List";
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(50, 22);
			toolStripButtonFind.Text = "Find";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(23, 22);
			toolStripButtonInformation.Text = "Document Information";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 402);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(627, 40);
			panelButtons.TabIndex = 13;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(16, 43);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(85, 13);
			label2.TabIndex = 22;
			label2.Text = "Container No:";
			textBoxContainerNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxContainerNumber.Location = new System.Drawing.Point(106, 39);
			textBoxContainerNumber.MaxLength = 64;
			textBoxContainerNumber.Name = "textBoxContainerNumber";
			textBoxContainerNumber.ReadOnly = true;
			textBoxContainerNumber.Size = new System.Drawing.Size(215, 20);
			textBoxContainerNumber.TabIndex = 21;
			textBoxContainerNumber.TabStop = false;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(16, 66);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(48, 13);
			label13.TabIndex = 148;
			label13.Text = "BOL No:";
			textBoxBOL.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBOL.Location = new System.Drawing.Point(106, 62);
			textBoxBOL.MaxLength = 20;
			textBoxBOL.Name = "textBoxBOL";
			textBoxBOL.ReadOnly = true;
			textBoxBOL.Size = new System.Drawing.Size(215, 20);
			textBoxBOL.TabIndex = 147;
			textBoxBOL.TabStop = false;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(501, 39);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(118, 20);
			dateTimePickerDate.TabIndex = 149;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(16, 89);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(77, 13);
			label1.TabIndex = 152;
			label1.Text = "Current Status:";
			textBoxCurrentStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCurrentStatus.Location = new System.Drawing.Point(106, 85);
			textBoxCurrentStatus.MaxLength = 64;
			textBoxCurrentStatus.Name = "textBoxCurrentStatus";
			textBoxCurrentStatus.ReadOnly = true;
			textBoxCurrentStatus.Size = new System.Drawing.Size(215, 20);
			textBoxCurrentStatus.TabIndex = 151;
			textBoxCurrentStatus.TabStop = false;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(327, 89);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(71, 13);
			label3.TabIndex = 154;
			label3.Text = "Changing To:";
			textBoxChangingTo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxChangingTo.Location = new System.Drawing.Point(404, 85);
			textBoxChangingTo.MaxLength = 64;
			textBoxChangingTo.Name = "textBoxChangingTo";
			textBoxChangingTo.ReadOnly = true;
			textBoxChangingTo.Size = new System.Drawing.Size(215, 20);
			textBoxChangingTo.TabIndex = 153;
			textBoxChangingTo.TabStop = false;
			groupBoxDocuments.Controls.Add(checkBoxOriginCertificate);
			groupBoxDocuments.Controls.Add(checkBoxhealthCertificate);
			groupBoxDocuments.Controls.Add(checkBoxPL);
			groupBoxDocuments.Controls.Add(checkBoxInvoice);
			groupBoxDocuments.Controls.Add(checkBoxBL);
			groupBoxDocuments.Location = new System.Drawing.Point(390, 129);
			groupBoxDocuments.Name = "groupBoxDocuments";
			groupBoxDocuments.Size = new System.Drawing.Size(212, 152);
			groupBoxDocuments.TabIndex = 185;
			groupBoxDocuments.Text = "Documents Received";
			checkBoxOriginCertificate.AutoSize = true;
			checkBoxOriginCertificate.Location = new System.Drawing.Point(17, 115);
			checkBoxOriginCertificate.Name = "checkBoxOriginCertificate";
			checkBoxOriginCertificate.Size = new System.Drawing.Size(115, 17);
			checkBoxOriginCertificate.TabIndex = 176;
			checkBoxOriginCertificate.Text = "Certificate of Origin";
			checkBoxOriginCertificate.UseVisualStyleBackColor = true;
			checkBoxhealthCertificate.AutoSize = true;
			checkBoxhealthCertificate.Location = new System.Drawing.Point(17, 93);
			checkBoxhealthCertificate.Name = "checkBoxhealthCertificate";
			checkBoxhealthCertificate.Size = new System.Drawing.Size(107, 17);
			checkBoxhealthCertificate.TabIndex = 175;
			checkBoxhealthCertificate.Text = "Health Certificate";
			checkBoxhealthCertificate.UseVisualStyleBackColor = true;
			checkBoxPL.AutoSize = true;
			checkBoxPL.Location = new System.Drawing.Point(17, 71);
			checkBoxPL.Name = "checkBoxPL";
			checkBoxPL.Size = new System.Drawing.Size(84, 17);
			checkBoxPL.TabIndex = 174;
			checkBoxPL.Text = "Packing List";
			checkBoxPL.UseVisualStyleBackColor = true;
			checkBoxInvoice.AutoSize = true;
			checkBoxInvoice.Location = new System.Drawing.Point(17, 49);
			checkBoxInvoice.Name = "checkBoxInvoice";
			checkBoxInvoice.Size = new System.Drawing.Size(61, 17);
			checkBoxInvoice.TabIndex = 173;
			checkBoxInvoice.Text = "Invoice";
			checkBoxInvoice.UseVisualStyleBackColor = true;
			checkBoxBL.AutoSize = true;
			checkBoxBL.Location = new System.Drawing.Point(17, 27);
			checkBoxBL.Name = "checkBoxBL";
			checkBoxBL.Size = new System.Drawing.Size(39, 17);
			checkBoxBL.TabIndex = 172;
			checkBoxBL.Text = "BL";
			checkBoxBL.UseVisualStyleBackColor = true;
			groupBoxDelivery.Controls.Add(label5);
			groupBoxDelivery.Controls.Add(label4);
			groupBoxDelivery.Controls.Add(textBoxDays);
			groupBoxDelivery.Controls.Add(dateTimePickerFreeTimeDate);
			groupBoxDelivery.Controls.Add(labelDriver);
			groupBoxDelivery.Controls.Add(textBoxDriver);
			groupBoxDelivery.Controls.Add(comboBoxTransporter);
			groupBoxDelivery.Controls.Add(labelTransporter);
			groupBoxDelivery.Controls.Add(textBoxTruckNumber);
			groupBoxDelivery.Controls.Add(labelTrcukNumber);
			groupBoxDelivery.Location = new System.Drawing.Point(8, 129);
			groupBoxDelivery.Name = "groupBoxDelivery";
			groupBoxDelivery.Size = new System.Drawing.Size(352, 152);
			groupBoxDelivery.TabIndex = 186;
			groupBoxDelivery.Text = "Delivery Details";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(210, 25);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(34, 13);
			label5.TabIndex = 194;
			label5.Text = "Days:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(17, 26);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 193;
			label4.Text = "Date:";
			labelDriver.AutoSize = true;
			labelDriver.Location = new System.Drawing.Point(17, 50);
			labelDriver.Name = "labelDriver";
			labelDriver.Size = new System.Drawing.Size(38, 13);
			labelDriver.TabIndex = 190;
			labelDriver.Text = "Driver:";
			textBoxDriver.BackColor = System.Drawing.SystemColors.Window;
			textBoxDriver.Location = new System.Drawing.Point(96, 46);
			textBoxDriver.MaxLength = 20;
			textBoxDriver.Name = "textBoxDriver";
			textBoxDriver.Size = new System.Drawing.Size(196, 20);
			textBoxDriver.TabIndex = 189;
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			labelTransporter.Appearance = appearance;
			labelTransporter.AutoSize = true;
			labelTransporter.Location = new System.Drawing.Point(18, 72);
			labelTransporter.Name = "labelTransporter";
			labelTransporter.Size = new System.Drawing.Size(65, 15);
			labelTransporter.TabIndex = 188;
			labelTransporter.TabStop = true;
			labelTransporter.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTransporter.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTransporter.Value = "Transporter:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			labelTransporter.VisitedLinkAppearance = appearance2;
			textBoxTruckNumber.BackColor = System.Drawing.SystemColors.Window;
			textBoxTruckNumber.Location = new System.Drawing.Point(96, 93);
			textBoxTruckNumber.MaxLength = 20;
			textBoxTruckNumber.Name = "textBoxTruckNumber";
			textBoxTruckNumber.Size = new System.Drawing.Size(196, 20);
			textBoxTruckNumber.TabIndex = 185;
			labelTrcukNumber.AutoSize = true;
			labelTrcukNumber.Location = new System.Drawing.Point(17, 97);
			labelTrcukNumber.Name = "labelTrcukNumber";
			labelTrcukNumber.Size = new System.Drawing.Size(57, 13);
			labelTrcukNumber.TabIndex = 187;
			labelTrcukNumber.Text = "Truck NO:";
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(11, 291);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(52, 13);
			label22.TabIndex = 189;
			label22.Text = "Remarks:";
			textBoxNote.Location = new System.Drawing.Point(106, 329);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(402, 55);
			textBoxNote.TabIndex = 188;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(11, 329);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(33, 13);
			label6.TabIndex = 190;
			label6.Text = "Note:";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.Location = new System.Drawing.Point(106, 291);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(402, 36);
			textBoxRemarks.TabIndex = 187;
			textBoxDays.AllowDecimal = false;
			textBoxDays.BackColor = System.Drawing.Color.White;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxDays.IsComboTextBox = false;
			textBoxDays.Location = new System.Drawing.Point(250, 21);
			textBoxDays.MaxLength = 15;
			textBoxDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDays.Name = "textBoxDays";
			textBoxDays.NullText = "0";
			textBoxDays.Size = new System.Drawing.Size(62, 20);
			textBoxDays.TabIndex = 192;
			textBoxDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			dateTimePickerFreeTimeDate.Checked = false;
			dateTimePickerFreeTimeDate.CustomFormat = " ";
			dateTimePickerFreeTimeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerFreeTimeDate.Location = new System.Drawing.Point(96, 22);
			dateTimePickerFreeTimeDate.Name = "dateTimePickerFreeTimeDate";
			dateTimePickerFreeTimeDate.ShowCheckBox = true;
			dateTimePickerFreeTimeDate.Size = new System.Drawing.Size(106, 20);
			dateTimePickerFreeTimeDate.TabIndex = 191;
			dateTimePickerFreeTimeDate.TabStop = false;
			dateTimePickerFreeTimeDate.Value = new System.DateTime(0L);
			comboBoxTransporter.Assigned = false;
			comboBoxTransporter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTransporter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransporter.CustomReportFieldName = "";
			comboBoxTransporter.CustomReportKey = "";
			comboBoxTransporter.CustomReportValueType = 1;
			comboBoxTransporter.DescriptionTextBox = null;
			comboBoxTransporter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransporter.Editable = true;
			comboBoxTransporter.FilterString = "";
			comboBoxTransporter.HasAllAccount = false;
			comboBoxTransporter.HasCustom = false;
			comboBoxTransporter.IsDataLoaded = false;
			comboBoxTransporter.Location = new System.Drawing.Point(96, 69);
			comboBoxTransporter.MaxDropDownItems = 12;
			comboBoxTransporter.Name = "comboBoxTransporter";
			comboBoxTransporter.ShowInactiveItems = false;
			comboBoxTransporter.ShowQuickAdd = true;
			comboBoxTransporter.Size = new System.Drawing.Size(196, 20);
			comboBoxTransporter.TabIndex = 186;
			comboBoxTransporter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(457, 43);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 150;
			mmLabel1.Text = "Date:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(627, 1);
			linePanelDown.TabIndex = 0;
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
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(519, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 2;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
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
			buttonSave.TabIndex = 1;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(627, 442);
			base.Controls.Add(label22);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label6);
			base.Controls.Add(groupBoxDelivery);
			base.Controls.Add(groupBoxDocuments);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxChangingTo);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxCurrentStatus);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(label13);
			base.Controls.Add(textBoxBOL);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxContainerNumber);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ContainerStatusChangingForm";
			Text = "Container Status Change";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)groupBoxDocuments).EndInit();
			groupBoxDocuments.ResumeLayout(false);
			groupBoxDocuments.PerformLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDelivery).EndInit();
			groupBoxDelivery.ResumeLayout(false);
			groupBoxDelivery.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
