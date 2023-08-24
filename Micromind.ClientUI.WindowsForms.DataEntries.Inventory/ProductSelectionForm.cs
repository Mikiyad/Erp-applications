using Infragistics.Win.Misc;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductSelectionForm : Form, IForm
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMTextBox textBoxBarcode;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private GroupBox groupBox1;

		private MMLabel mmLabel4;

		private MMLabel mmLabel5;

		public XPButton xpOkButton;

		public MMTextBox textBoxProduct;

		public MMTextBox textBoxUPC;

		public MMTextBox textBoxLotNo;

		public MMTextBox textBoxQty;

		public XPButton xpCloseButton;

		public XPButton xpButton1;

		public MMSDateTimePicker ExpDate;

		private UltraExpandableGroupBox ExpGroupBox;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;

		private MMLabel mmLabel7;

		private MMLabel mmLabel8;

		private MMLabel mmLabel9;

		private MMLabel mmLabel10;

		public MMSDateTimePicker PrdDate;

		public TextBox textBin;

		public TextBox textRack;

		public TextBox textLocation;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5021;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public ProductSelectionForm()
		{
			InitializeComponent();
		}

		public DataTable CreateDt(DataTable dt)
		{
			dt.Columns.Add("ProductID", typeof(string));
			dt.Columns.Add("LocationId", typeof(string));
			dt.Columns.Add("RowIndex", typeof(string));
			dt.Columns.Add("VoucherId", typeof(string));
			dt.Columns.Add("SysDocId", typeof(string));
			dt.Columns.Add("RackId", typeof(string));
			dt.Columns.Add("BinId", typeof(string));
			dt.Columns.Add("Reference", typeof(string));
			dt.Columns.Add("Reference2", typeof(string));
			dt.Columns.Add("SourceLotNumber", typeof(string));
			dt.Columns.Add("UPC", typeof(string));
			dt.Columns.Add("LotNumber", typeof(string));
			dt.Columns.Add("ExpiryDate", typeof(DateTime));
			dt.Columns.Add("ReceiptDate", typeof(DateTime));
			dt.Columns.Add("LotQty", typeof(decimal));
			dt.Columns.Add("SoldQty", typeof(string));
			dt.Columns.Add("ProductionDate", typeof(DateTime));
			return dt;
		}

		private void GetSplitStraing(string Barcode)
		{
			string[] array = Barcode.Split(new string[3]
			{
				"(01)",
				"(11)",
				"(10)"
			}, StringSplitOptions.None);
			if (array.Length == 4)
			{
				textBoxProduct.Text = array[1];
				textBoxLotNo.Text = array[3];
				ExpDate.Value = DateTime.Parse(setDateFormate(array[2]).ToString());
				ExpDate.Checked = true;
			}
		}

		private void xpCloseButton_Click(object sender, EventArgs e)
		{
			Hide();
		}

		public void ClearControls()
		{
			textBoxProduct.Clear();
			textBoxBarcode.Clear();
			textBoxLotNo.Clear();
			textBoxQty.Text = "1";
			textBoxUPC.Clear();
			ExpDate.Checked = false;
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			GetSplitStraing(textBoxBarcode.Text);
		}

		private void textBoxQty_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
			{
				e.Handled = true;
			}
		}

		private DateTime setDateFormate(string value)
		{
			value = value.Insert(2, "/");
			value = value.Insert(5, "/");
			CultureInfo provider = new CultureInfo("de-DE");
			return DateTime.Parse(value, provider);
		}

		private void ultraExpandableGroupBox1_ExpandedStateChanging(object sender, CancelEventArgs e)
		{
		}

		private void ExpGroupBox_ExpandedStateChanged(object sender, EventArgs e)
		{
			if (ExpGroupBox.Expanded)
			{
				base.Height = 402;
			}
			else
			{
				base.Height = 283;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductSelectionForm));
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
			xpCloseButton = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpOkButton = new Micromind.UISupport.XPButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			xpButton1 = new Micromind.UISupport.XPButton();
			textBoxBarcode = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxQty = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxLotNo = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxUPC = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxProduct = new Micromind.UISupport.MMTextBox();
			ExpDate = new Micromind.UISupport.MMSDateTimePicker(components);
			ExpGroupBox = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			PrdDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBin = new System.Windows.Forms.TextBox();
			textRack = new System.Windows.Forms.TextBox();
			textLocation = new System.Windows.Forms.TextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ExpGroupBox).BeginInit();
			ExpGroupBox.SuspendLayout();
			ultraExpandableGroupBoxPanel1.SuspendLayout();
			SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(487, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
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
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			panelButtons.Controls.Add(xpCloseButton);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpOkButton);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 319);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(503, 40);
			panelButtons.TabIndex = 8;
			xpCloseButton.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpCloseButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpCloseButton.BackColor = System.Drawing.Color.DarkGray;
			xpCloseButton.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpCloseButton.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpCloseButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpCloseButton.Location = new System.Drawing.Point(404, 8);
			xpCloseButton.Name = "xpCloseButton";
			xpCloseButton.Size = new System.Drawing.Size(96, 24);
			xpCloseButton.TabIndex = 15;
			xpCloseButton.Text = "&Close";
			xpCloseButton.UseVisualStyleBackColor = false;
			xpCloseButton.Click += new System.EventHandler(xpCloseButton_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(503, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpOkButton.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpOkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpOkButton.BackColor = System.Drawing.Color.DarkGray;
			xpOkButton.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpOkButton.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpOkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpOkButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpOkButton.Location = new System.Drawing.Point(304, 8);
			xpOkButton.Name = "xpOkButton";
			xpOkButton.Size = new System.Drawing.Size(96, 24);
			xpOkButton.TabIndex = 11;
			xpOkButton.Text = "&Ok";
			xpOkButton.UseVisualStyleBackColor = false;
			groupBox1.Controls.Add(xpButton1);
			groupBox1.Controls.Add(textBoxBarcode);
			groupBox1.Location = new System.Drawing.Point(26, 8);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(342, 46);
			groupBox1.TabIndex = 24;
			groupBox1.TabStop = false;
			groupBox1.Text = "Search Barcode";
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(298, 16);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(31, 24);
			xpButton1.TabIndex = 16;
			xpButton1.Text = "->";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			textBoxBarcode.BackColor = System.Drawing.Color.White;
			textBoxBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxBarcode.CustomReportFieldName = "";
			textBoxBarcode.CustomReportKey = "";
			textBoxBarcode.CustomReportValueType = 1;
			textBoxBarcode.IsComboTextBox = false;
			textBoxBarcode.IsModified = false;
			textBoxBarcode.Location = new System.Drawing.Point(11, 18);
			textBoxBarcode.Name = "textBoxBarcode";
			textBoxBarcode.Size = new System.Drawing.Size(287, 20);
			textBoxBarcode.TabIndex = 0;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(358, 66);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(26, 13);
			mmLabel5.TabIndex = 28;
			mmLabel5.Text = "Qty:";
			textBoxQty.BackColor = System.Drawing.Color.White;
			textBoxQty.CustomReportFieldName = "";
			textBoxQty.CustomReportKey = "";
			textBoxQty.CustomReportValueType = 1;
			textBoxQty.IsComboTextBox = false;
			textBoxQty.IsModified = false;
			textBoxQty.Location = new System.Drawing.Point(390, 63);
			textBoxQty.Name = "textBoxQty";
			textBoxQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			textBoxQty.Size = new System.Drawing.Size(90, 20);
			textBoxQty.TabIndex = 27;
			textBoxQty.Text = "1";
			textBoxQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxQty_KeyPress);
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(26, 143);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(54, 13);
			mmLabel4.TabIndex = 26;
			mmLabel4.Text = "Exp Date:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(26, 117);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(42, 13);
			mmLabel3.TabIndex = 23;
			mmLabel3.Text = "Lot No:";
			textBoxLotNo.BackColor = System.Drawing.Color.White;
			textBoxLotNo.CustomReportFieldName = "";
			textBoxLotNo.CustomReportKey = "";
			textBoxLotNo.CustomReportValueType = 1;
			textBoxLotNo.IsComboTextBox = false;
			textBoxLotNo.IsModified = false;
			textBoxLotNo.Location = new System.Drawing.Point(123, 114);
			textBoxLotNo.Name = "textBoxLotNo";
			textBoxLotNo.Size = new System.Drawing.Size(201, 20);
			textBoxLotNo.TabIndex = 22;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(26, 92);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(49, 13);
			mmLabel2.TabIndex = 21;
			mmLabel2.Text = "UPC No:";
			textBoxUPC.BackColor = System.Drawing.Color.White;
			textBoxUPC.CustomReportFieldName = "";
			textBoxUPC.CustomReportKey = "";
			textBoxUPC.CustomReportValueType = 1;
			textBoxUPC.IsComboTextBox = false;
			textBoxUPC.IsModified = false;
			textBoxUPC.Location = new System.Drawing.Point(123, 88);
			textBoxUPC.Name = "textBoxUPC";
			textBoxUPC.Size = new System.Drawing.Size(201, 20);
			textBoxUPC.TabIndex = 20;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(26, 66);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(58, 13);
			mmLabel1.TabIndex = 19;
			mmLabel1.Text = "Product ID";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxProduct.BackColor = System.Drawing.Color.White;
			textBoxProduct.CustomReportFieldName = "";
			textBoxProduct.CustomReportKey = "";
			textBoxProduct.CustomReportValueType = 1;
			textBoxProduct.IsComboTextBox = false;
			textBoxProduct.IsModified = false;
			textBoxProduct.Location = new System.Drawing.Point(123, 63);
			textBoxProduct.Name = "textBoxProduct";
			textBoxProduct.Size = new System.Drawing.Size(201, 20);
			textBoxProduct.TabIndex = 2;
			ExpDate.Checked = false;
			ExpDate.CustomFormat = " ";
			ExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			ExpDate.Location = new System.Drawing.Point(123, 140);
			ExpDate.Name = "ExpDate";
			ExpDate.ShowCheckBox = true;
			ExpDate.Size = new System.Drawing.Size(201, 20);
			ExpDate.TabIndex = 29;
			ExpDate.Value = new System.DateTime(0L);
			ExpGroupBox.Controls.Add(ultraExpandableGroupBoxPanel1);
			ExpGroupBox.ExpandedSize = new System.Drawing.Size(342, 144);
			ExpGroupBox.Location = new System.Drawing.Point(25, 170);
			ExpGroupBox.Name = "ExpGroupBox";
			ExpGroupBox.Size = new System.Drawing.Size(342, 144);
			ExpGroupBox.TabIndex = 30;
			ExpGroupBox.Text = "More Details";
			ExpGroupBox.ExpandedStateChanged += new System.EventHandler(ExpGroupBox_ExpandedStateChanged);
			ExpGroupBox.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(ultraExpandableGroupBox1_ExpandedStateChanging);
			ultraExpandableGroupBoxPanel1.Controls.Add(PrdDate);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel10);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel7);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel8);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel9);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBin);
			ultraExpandableGroupBoxPanel1.Controls.Add(textRack);
			ultraExpandableGroupBoxPanel1.Controls.Add(textLocation);
			ultraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(3, 19);
			ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(336, 122);
			ultraExpandableGroupBoxPanel1.TabIndex = 0;
			PrdDate.Checked = false;
			PrdDate.CustomFormat = " ";
			PrdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			PrdDate.Location = new System.Drawing.Point(98, 85);
			PrdDate.Name = "PrdDate";
			PrdDate.ShowCheckBox = true;
			PrdDate.Size = new System.Drawing.Size(201, 20);
			PrdDate.TabIndex = 32;
			PrdDate.Value = new System.DateTime(0L);
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(9, 90);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(52, 13);
			mmLabel10.TabIndex = 31;
			mmLabel10.Text = "Prd Date:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(9, 65);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(39, 13);
			mmLabel7.TabIndex = 29;
			mmLabel7.Text = "Bin ID:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 38);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(50, 13);
			mmLabel8.TabIndex = 28;
			mmLabel8.Text = "Rack ID:";
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(9, 12);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(65, 13);
			mmLabel9.TabIndex = 27;
			mmLabel9.Text = "Location ID:";
			textBin.Location = new System.Drawing.Point(98, 59);
			textBin.Name = "textBin";
			textBin.Size = new System.Drawing.Size(201, 20);
			textBin.TabIndex = 2;
			textRack.Location = new System.Drawing.Point(98, 33);
			textRack.Name = "textRack";
			textRack.Size = new System.Drawing.Size(201, 20);
			textRack.TabIndex = 1;
			textLocation.Location = new System.Drawing.Point(98, 7);
			textLocation.Name = "textLocation";
			textLocation.Size = new System.Drawing.Size(201, 20);
			textLocation.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(503, 359);
			base.Controls.Add(ExpGroupBox);
			base.Controls.Add(ExpDate);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(textBoxQty);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(groupBox1);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(textBoxLotNo);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxUPC);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxProduct);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ProductSelectionForm";
			Text = "ProductSelection";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ExpGroupBox).EndInit();
			ExpGroupBox.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
