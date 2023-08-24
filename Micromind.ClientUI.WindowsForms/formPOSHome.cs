using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries;
using Micromind.ClientUI.WindowsForms.DataEntries.POS;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.DataControls.MMSDataGrid;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	public class formPOSHome : Form
	{
		public class MMSBarManager : BarManager
		{
			private int scrollInterval = 100;

			public int ScrollInterval
			{
				get
				{
					return scrollInterval;
				}
				set
				{
					if (scrollInterval > 0)
					{
						scrollInterval = value;
					}
				}
			}

			public event KeyEventHandler KeyDown;

			public MMSBarManager()
			{
			}

			public MMSBarManager(IContainer container)
				: base(container)
			{
			}

			protected override BarSelectionInfo CreateSelectionInfo()
			{
				return new MMSBarSelectionInfo(this);
			}

			public void OnKeyDown(object sender, KeyEventArgs e)
			{
				if (this.KeyDown != null)
				{
					this.KeyDown(sender, e);
				}
			}
		}

		public class MMSBarSelectionInfo : BarSelectionInfo
		{
			public new MMSBarManager Manager => base.Manager as MMSBarManager;

			public MMSBarSelectionInfo(BarManager manager)
				: base(manager)
			{
			}

			protected override void InitHighligthTimer(BarItemLink link)
			{
				base.InitHighligthTimer(link);
				base.HighlightTimer.Interval = Manager.ScrollInterval;
			}

			public override bool ProcessKeyDown(KeyEventArgs e)
			{
				bool result = base.ProcessKeyDown(e);
				Manager.OnKeyDown(this, e);
				return result;
			}
		}

		private delegate void updateLabelTextDelegate(string newText);

		private bool allowEdit = true;

		private int batchID = -1;

		private int shiftID = -1;

		private string recalledID = "";

		private SalesPOSData currentData;

		private const string TABLENAME_CONST = "Sales_POS";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string salesPersonID = "";

		private string searchValue = "";

		private DataSet productListData = new DataSet();

		private bool canAccessCost = true;

		private bool LoadItemFeatures = CompanyPreferences.POSDisplayItemFeatures;

		private bool ChangeSalespersonwhileSave = CompanyPreferences.ChangeSalespersonWhileSave;

		private DataTable refPaymentTable;

		private bool isFirstActivated;

		private UltraDataSource ultraDataSource1;

		private GridView gridView1;

		private ToolStripMenuItem resetLayoutToolStripMenuItem;

		private ContextMenuStrip contextMenuStrip1;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private GridView gridView2;

		private XtraTabPage tabHome;

		private Label label4;

		private ScanTextBox textBoxScan;

		private Panel panel1;

		private POSGrid posGrid1;

		private XtraTabControl tabControlMain;

		private Panel panelMain;

		private PanelControl panelControl2;

		private SimpleButton buttonTotal;

		private SimpleButton buttonItems;

		private SimpleButton buttonCustomers;

		private SimpleButton buttonTransactions;

		private SimpleButton buttonTasks;

		private PictureBox pictureBox1;

		private SimpleButton buttonQty;

		private Label labelCustomerCode;

		private MMSBarManager barManager1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barButtonItemHold;

		private BarButtonItem barButtonItemReturnMode;

		private BarLargeButtonItem barButtonItemFindReceipt;

		private PopupMenu popupMenuTransactions;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItemReprintReceipt;

		private BarButtonItem barButtonItem5;

		private BarButtonItem barButtonItem6;

		private BarButtonItem barButtonItem7;

		private PopupMenu popupMenuItems;

		private BarButtonItem barButtonCheckItemPrice;

		private BarButtonItem barButtonCheckItemQuantity;

		private BarButtonItem barButtonItem10;

		private BarButtonItem barButtonItem11;

		private Label labelQuantity;

		private SimpleButton buttonPayment;

		private SimpleButton buttonDiscount;

		private TextEdit textBoxSubtotal;

		private TextEdit textBoxDiscount;

		private TextEdit textBoxTotal;

		private FormManager formManager;

		private Label labelCustomerName;

		private PictureBox pictureBox3;

		private SimpleButton buttonCustomer;

		private PanelControl panelControl4;

		private Label label3;

		private TextEdit textBoxNote;

		private BarButtonItem barButtonItemRecall;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelTotalQty;

		private UltraLabel ultraLabel1;

		private UltraLabel labelRowCount;

		private UltraLabel ultraLabel3;

		private BarSubItem barSubItem1;

		private SimpleButton buttonClear;

		private BarButtonItem barButtonItem1;

		private BarStaticItem barStaticItem1;

		private BarSubItem barSubItem2;

		private BarStaticItem barStaticItem2;

		private PopupMenu popupMenuTasks;

		private PopupMenu popupMenuCustomers;

		private BarButtonItem barButtonItemXReport;

		private BarButtonItem barButtonItem12;

		private BarButtonItem barButtonItemYReport;

		private BarButtonItem barButtonItemZReport;

		private UltraCheckEditor checkBoxReturnMode;

		private BarButtonItem barButtonItemClose;

		private BarLargeButtonItem barLargeButtonItem2;

		private BarButtonItem menuItemStartNewShift;

		private BarButtonItem barButtonItemChangeSalesperson;

		private LabelControl labelSalespersonBack;

		private LabelControl labelVoucherDate;

		private LabelControl labelControl5;

		private LabelControl labelVoucherNumber;

		private LabelControl labelControl4;

		private LabelControl labelShift;

		private LabelControl labelVoucherf;

		private LabelControl labelBatchID;

		private LabelControl labelControl3;

		private LabelControl labelCashRegisterID;

		private LabelControl labelControl2;

		private LabelControl labelControl1;

		private LinkLabel labelSalesperson;

		private Timer timerDate;

		private LabelControl labelSysDocID;

		private LabelControl labelControl9;

		private Label label2;

		private TextEdit textBoxTaxAmount;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private Label labelPayeeTaxOption;

		private CheckBox checkBoxPriceIncludeTax;

		private BarButtonItem barButtonItemVoidReceipt;

		private Label labelVoid;

		private SplitContainer splitContainer1;

		private Panel panel2;

		private Panel panel3;

		private Line line1;

		private Line line2;

		private BarButtonItem barButtonCashPayments;

		private BarLargeButtonItem barLargeButtonItem1;

		private BarButtonItem barButtonItem4;

		private BarLinkContainerItem barLinkContainerItem1;

		private BarSubItem barSubItem3;

		private BarButtonItem barButtonItem13;

		private BarButtonItem barButtonItemPettyCash;

		private Panel panelItemDetails;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label9;

		private Label label10;

		private Label labelStock;

		private Label labelCountry;

		private Label labelManufacturer;

		private Label labelBrand;

		private Label labelCategory;

		private Label labelUnit;

		private ProductPhotoViewer productPhotoViewer;

		private Label labelLSTCost;

		private Label labelStdrdCost;

		private Label labelAVGCost;

		private Label labelLastCost;

		private Label labelStdCost;

		private Label labelAverageCost;

		private LinkLabel linkLabelShowHideCost;

		private IContainer components;

		private bool validateQtyOnhand;

		private bool supressInventoryMessage;

		private bool isEditable = true;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private string sysDocID = "";

		private string expenseDocID = "";

		private bool isVoid;

		public DataTable RefPaymentTable
		{
			get
			{
				return refPaymentTable;
			}
			set
			{
				refPaymentTable = value;
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
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		private string SystemDocID
		{
			get
			{
				return sysDocID;
			}
			set
			{
				sysDocID = value;
				labelSysDocID.Text = value;
			}
		}

		private string ExpenseDocID
		{
			get
			{
				return expenseDocID;
			}
			set
			{
				expenseDocID = value;
			}
		}

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid != value)
				{
					isVoid = value;
					labelVoid.Visible = value;
					buttonPayment.Enabled = !value;
				}
			}
		}

		public formPOSHome(Form parent)
		{
			InitializeComponent();
			base.MdiParent = parent;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Load += formPOSHome_Load;
			base.FormClosing += formPOSHome_FormClosing;
			EventHelper.HomeTabChanged += EventHelper_HomeTabChanged;
			base.KeyDown += FormPOSHome_KeyDown;
			popupMenuTasks.BeforePopup += popupMenuTasks_BeforePopup;
			Init();
			labelAverageCost.Visible = (labelAVGCost.Visible = false);
			labelStdCost.Visible = (labelStdrdCost.Visible = (labelLastCost.Visible = (labelLSTCost.Visible = false)));
		}

		public formPOSHome()
		{
			InitializeComponent();
			base.FormBorderStyle = FormBorderStyle.Sizable;
			base.Load += formPOSHome_Load;
			base.FormClosing += formPOSHome_FormClosing;
			EventHelper.HomeTabChanged += EventHelper_HomeTabChanged;
			popupMenuTasks.BeforePopup += popupMenuTasks_BeforePopup;
			posGrid1.DataGrid.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			posGrid1.DataGrid.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			textBoxDiscount.TextChanged += textBoxDiscountAmount_TextChanged;
			base.KeyDown += FormPOSHome_KeyDown;
			base.KeyPreview = true;
			base.MinimizeBox = true;
			base.MaximizeBox = true;
			base.ShowInTaskbar = true;
			base.ControlBox = true;
			base.WindowState = FormWindowState.Normal;
			posGrid1.RowButtonClicked += PosGrid1_RowButtonClicked;
			posGrid1.ShowRowButtons = true;
			textBoxScan.EnterPressed += TextBoxScan_EnterPressed;
			base.Activated += FormPOSHome_Activated;
			Init();
			barManager1.KeyDown += BarManager1_KeyDown;
			posGrid1.DataGrid.ClickCell += dataGridItems_ClickCell;
		}

		private void BarManager1_KeyDown(object sender, KeyEventArgs e)
		{
			if (popupMenuTransactions.Visible)
			{
				popupMenuTransactions.HidePopup();
				if (e.KeyCode == Keys.D1)
				{
					barButtonItemHold_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D2)
				{
					barButtonItemRecall_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D3)
				{
					barButtonItemReturnMode_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D4)
				{
					barItemFindReceipt_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D5)
				{
					barButtonItemReprintReceipt_ItemClick(sender, null);
				}
				else if (e.KeyCode != Keys.D6)
				{
					if (e.KeyCode == Keys.D7)
					{
						barButtonItemVoidReceipt_ItemClick(sender, null);
					}
					else if (e.KeyCode == Keys.D8)
					{
						barButtonItemPettyCash_ItemClick(sender, null);
					}
				}
			}
			else if (popupMenuCustomers.Visible)
			{
				popupMenuCustomers.HidePopup();
				if (e.KeyCode == Keys.D1)
				{
					barButtonItemChangeSalesperson_ItemClick(sender, null);
				}
			}
			else if (popupMenuItems.Visible)
			{
				popupMenuItems.HidePopup();
				if (e.KeyCode == Keys.D1)
				{
					buttonItemLookup_Click(sender, null);
				}
				else if (e.KeyCode == Keys.D2)
				{
					barButtonCheckItemPrice_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D3)
				{
					barButtonCheckItemQuantity_ItemClick(sender, null);
				}
			}
			else if (popupMenuTasks.Visible)
			{
				popupMenuTasks.HidePopup();
				if (e.KeyCode == Keys.D1)
				{
					menuItemStartNewShift_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D2)
				{
					barButtonItemXReport_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D3)
				{
					barButtonItemYReport_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D4)
				{
					barButtonItemZReport_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D5)
				{
					barButtonCashPayments_ItemClick(sender, null);
				}
				else if (e.KeyCode == Keys.D6)
				{
					barButtonClose_ItemClick(sender, null);
				}
			}
		}

		private void FormPOSHome_Activated(object sender, EventArgs e)
		{
			textBoxScan.Focus();
		}

		private void TextBoxScan_EnterPressed(object sender, EventArgs e)
		{
			if (textBoxScan.Text.Trim() == "")
			{
				textBoxScan.Focus();
				return;
			}
			string text = textBoxScan.Text;
			ScanItem(text);
		}

		private void PosGrid1_RowButtonClicked(object sender, EventArgs e)
		{
			try
			{
				string name = (sender as Button).Name;
				if (name == "Qty")
				{
					EnterAmountForm enterAmountForm = new EnterAmountForm
					{
						IsQuantity = true
					};
					if (posGrid1.DataGrid.ActiveCell.Value != null && posGrid1.DataGrid.ActiveCell.Value.ToString() != "")
					{
						enterAmountForm.Total = decimal.Parse(posGrid1.DataGrid.ActiveCell.Value.ToString());
					}
					else
					{
						enterAmountForm.Total = 0m;
					}
					enterAmountForm.MaxValue = 99999999m;
					if (enterAmountForm.ShowDialog() == DialogResult.OK)
					{
						posGrid1.DataGrid.ActiveCell.Value = enterAmountForm.Total;
						if (textBoxDiscount.Text.ToDecimal() > 0m)
						{
							CalculateAllRowsTaxes();
							CalculateTotal();
						}
					}
				}
				if (name == "Price")
				{
					EnterAmountForm enterAmountForm2 = new EnterAmountForm
					{
						IsItemPrice = true
					};
					if (posGrid1.DataGrid.ActiveCell.Value != null && posGrid1.DataGrid.ActiveCell.Value.ToString() != "")
					{
						enterAmountForm2.Total = decimal.Parse(posGrid1.DataGrid.ActiveCell.Value.ToString());
					}
					else
					{
						enterAmountForm2.Total = 0m;
					}
					enterAmountForm2.MaxValue = 99999999m;
					if (enterAmountForm2.ShowDialog() == DialogResult.OK)
					{
						posGrid1.DataGrid.ActiveCell.Value = enterAmountForm2.Total;
						if (textBoxDiscount.Text.ToDecimal() > 0m)
						{
							CalculateAllRowsTaxes();
							CalculateTotal();
						}
					}
				}
				if (name == "Discount")
				{
					EnterAmountForm enterAmountForm3 = new EnterAmountForm
					{
						IsDiscount = true
					};
					if (posGrid1.DataGrid.ActiveCell.Value != null && posGrid1.DataGrid.ActiveCell.Value.ToString() != "")
					{
						enterAmountForm3.Total = decimal.Parse(posGrid1.DataGrid.ActiveCell.Value.ToString());
					}
					else
					{
						enterAmountForm3.Total = 0m;
					}
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(posGrid1.DataGrid.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
					decimal.TryParse(posGrid1.DataGrid.ActiveRow.Cells["Price"].Value.ToString(), out result2);
					enterAmountForm3.MaxValue = result2;
					if (enterAmountForm3.ShowDialog() == DialogResult.OK)
					{
						posGrid1.DataGrid.ActiveCell.Value = enterAmountForm3.Total;
						if (textBoxDiscount.Text.ToDecimal() > 0m)
						{
							CalculateAllRowsTaxes();
							CalculateTotal();
						}
					}
				}
				if (name == "Amount")
				{
					EnterAmountForm enterAmountForm4 = new EnterAmountForm
					{
						IsPrice = true
					};
					if (posGrid1.DataGrid.ActiveCell.Value != null && posGrid1.DataGrid.ActiveCell.Value.ToString() != "")
					{
						enterAmountForm4.Total = decimal.Parse(posGrid1.DataGrid.ActiveCell.Value.ToString());
					}
					else
					{
						enterAmountForm4.Total = 0m;
					}
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(posGrid1.DataGrid.ActiveRow.Cells["Quantity"].Value.ToString(), out result3);
					decimal.TryParse(posGrid1.DataGrid.ActiveRow.Cells["Price"].Value.ToString(), out result4);
					enterAmountForm4.MaxValue = result3 * result4;
					if (enterAmountForm4.ShowDialog() == DialogResult.OK)
					{
						posGrid1.DataGrid.ActiveCell.Value = enterAmountForm4.Total;
						if (textBoxDiscount.Text.ToDecimal() > 0m)
						{
							CalculateAllRowsTaxes();
							CalculateTotal();
						}
					}
				}
				if (name == "Delete")
				{
					DeleteRow();
				}
				if (name == "View" && posGrid1.DataGrid.ActiveRow != null)
				{
					CheckPriceForm checkPriceForm = new CheckPriceForm();
					checkPriceForm.SelectedItem = posGrid1.DataGrid.ActiveRow.Cells["ProductID"].Value.ToString();
					checkPriceForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FormPOSHome_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2)
			{
				buttonItemLookup_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F3)
			{
				buttonCustomer_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F4)
			{
				buttonTasks_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F5)
			{
				buttonTransactions_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F6)
			{
				buttonCustomers_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F7)
			{
				buttonItems_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F8)
			{
				buttonClear_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F9)
			{
				barButtonItemHold_ItemClick(sender, null);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F10)
			{
				buttonDiscount_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F11)
			{
				buttonTotal_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.F12)
			{
				buttonPayment_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				buttonItemDown_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Up)
			{
				buttonItemUp_Click(sender, e);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Delete)
			{
				DeleteRow();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.R && e.Control)
			{
				checkBoxReturnMode.Checked = !checkBoxReturnMode.Checked;
				e.Handled = true;
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
			CalculateAllRowsTaxes();
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (e.Cell.Column.Key == "Price")
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal num = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result);
					decimal.TryParse(e.Cell.Row.Cells["Price"].Value.ToString(), out result2);
					decimal.TryParse(e.Cell.Row.Cells["Discount"].Value.ToString(), out result3);
					num = Math.Round(result * (result2 - result3), Global.CurDecimalPoints);
					posGrid1.DataGrid.ActiveRow.Cells["Amount"].Value = num;
				}
				if (e.Cell.Column.Key == "Quantity")
				{
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal num2 = default(decimal);
					decimal result6 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result4);
					decimal.TryParse(e.Cell.Row.Cells["Price"].Value.ToString(), out result5);
					decimal.TryParse(e.Cell.Row.Cells["Discount"].Value.ToString(), out result6);
					num2 = Math.Round(result4 * (result5 - result6), Global.CurDecimalPoints);
					posGrid1.DataGrid.ActiveRow.Cells["Amount"].Value = num2;
				}
				if (e.Cell.Column.Key == "Discount" && e.Cell.IsActiveCell)
				{
					decimal result7 = default(decimal);
					decimal result8 = default(decimal);
					decimal num3 = default(decimal);
					decimal result9 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result7);
					decimal.TryParse(e.Cell.Row.Cells["Price"].Value.ToString(), out result8);
					decimal.TryParse(e.Cell.Row.Cells["Discount"].Value.ToString(), out result9);
					num3 = Math.Round(result7 * (result8 - result9), Global.CurDecimalPoints);
					posGrid1.DataGrid.ActiveRow.Cells["Amount"].Value = num3;
				}
				if (e.Cell.Column.Key == "Amount")
				{
					if (e.Cell.Row.Cells["TaxGroupID"].Value.ToString() != "")
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.BasedOnCustomer;
						if (e.Cell.Row.Cells["TaxOption"].Value.ToString() != "")
						{
							itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData((PayeeTaxOptions)checked((byte)int.Parse(labelPayeeTaxOption.Text)), comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, e.Cell.Row.Cells["TaxGroupID"].Value.ToString());
						e.Cell.Row.Cells["Tax"].Tag = tag;
					}
					decimal result10 = default(decimal);
					decimal result11 = default(decimal);
					decimal num4 = default(decimal);
					decimal num5 = decimal.Parse(e.Cell.Row.Cells["Amount"].Value.ToString());
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
					decimal tradeDiscount = decimal.Parse(textBoxDiscount.Text);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result10);
					decimal.TryParse(e.Cell.Row.Cells["Price"].Value.ToString(), out result11);
					if (e.Cell.IsActiveCell)
					{
						if (result10 != 0m)
						{
							num4 = -1m * (num5 / result10 - result11);
						}
						e.Cell.Row.Cells["Discount"].Value = num4;
					}
					UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", num5, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
					CalculateTotal();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			Panel panel = panelItemDetails;
			bool visible = productPhotoViewer.Visible = LoadItemFeatures;
			panel.Visible = visible;
			Label label = labelAverageCost;
			visible = (labelAVGCost.Visible = false);
			label.Visible = visible;
			Label label2 = labelStdCost;
			Label label3 = labelStdrdCost;
			Label label4 = labelLastCost;
			bool flag3 = labelLSTCost.Visible = false;
			bool flag5 = label4.Visible = flag3;
			visible = (label3.Visible = flag5);
			label2.Visible = visible;
			linkLabelShowHideCost.Text = "Show";
			linkLabelShowHideCost.Visible = canAccessCost;
			UltraGridRow activeRow = posGrid1.DataGrid.ActiveRow;
			string text = "";
			if (posGrid1.DataGrid.Rows.Count > 0 && posGrid1.DataGrid.ActiveRow != null && posGrid1.DataGrid.ActiveRow.Cells["ProductID"].Value != null)
			{
				text = activeRow.Cells["ProductID"].Value.ToString();
			}
			if (!(text != "") || !LoadItemFeatures)
			{
				return;
			}
			productPhotoViewer.ShowImage(text, posGrid1.Left, checked(posGrid1.Top + 400));
			DataSet dataSet = new DataSet();
			dataSet = CommonLib.DecompressDataSet(Factory.ProductSystem.GetItemFeatures(text));
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				labelUnit.Text = dataRow["UnitName"].ToString();
				labelCategory.Text = dataRow["CategoryName"].ToString();
				labelBrand.Text = dataRow["BrandName"].ToString();
				labelManufacturer.Text = dataRow["ManufacturerName"].ToString();
				labelCountry.Text = dataRow["CountryName"].ToString();
				labelStock.Text = decimal.Parse(dataRow["Quantity"].ToString()).ToString(Format.TotalAmountFormat);
				if (!string.IsNullOrEmpty(dataRow["StandardCost"].ToString()))
				{
					labelStdrdCost.Text = decimal.Parse(dataRow["StandardCost"].ToString()).ToString(Format.TotalAmountFormat);
				}
				if (!string.IsNullOrEmpty(dataRow["AverageCost"].ToString()))
				{
					labelAVGCost.Text = decimal.Parse(dataRow["AverageCost"].ToString()).ToString(Format.TotalAmountFormat);
				}
				if (!string.IsNullOrEmpty(dataRow["LAST COST"].ToString()))
				{
					labelLSTCost.Text = decimal.Parse(dataRow["LAST COST"].ToString()).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void popupMenuTasks_BeforePopup(object sender, CancelEventArgs e)
		{
			if (UIGlobal.GetActiveShiftNumber() <= 0)
			{
				menuItemStartNewShift.Enabled = true;
			}
			else
			{
				menuItemStartNewShift.Enabled = false;
			}
		}

		private void EventHelper_HomeTabChanged(object sender, EventArgs e)
		{
		}

		private void formPOSHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private void SaveLayout()
		{
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPOSHome));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.resetLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabHome = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelItemDetails = new System.Windows.Forms.Panel();
            this.labelLSTCost = new System.Windows.Forms.Label();
            this.labelStdrdCost = new System.Windows.Forms.Label();
            this.labelAVGCost = new System.Windows.Forms.Label();
            this.labelLastCost = new System.Windows.Forms.Label();
            this.labelStdCost = new System.Windows.Forms.Label();
            this.labelAverageCost = new System.Windows.Forms.Label();
            this.linkLabelShowHideCost = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelStock = new System.Windows.Forms.Label();
            this.labelCountry = new System.Windows.Forms.Label();
            this.labelManufacturer = new System.Windows.Forms.Label();
            this.labelBrand = new System.Windows.Forms.Label();
            this.labelCategory = new System.Windows.Forms.Label();
            this.labelUnit = new System.Windows.Forms.Label();
            this.productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.line1 = new Micromind.UISupport.Line();
            this.checkBoxReturnMode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.textBoxScan = new Micromind.DataControls.ScanTextBox(this.components);
            this.labelQuantity = new System.Windows.Forms.Label();
            this.buttonQty = new DevExpress.XtraEditors.SimpleButton();
            this.line2 = new Micromind.UISupport.Line();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.labelRowCount = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.labelTotalQty = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.posGrid1 = new Micromind.DataControls.MMSDataGrid.POSGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNote = new DevExpress.XtraEditors.TextEdit();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelVoid = new System.Windows.Forms.Label();
            this.buttonCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
            this.labelPayeeTaxOption = new System.Windows.Forms.Label();
            this.labelCustomerCode = new System.Windows.Forms.Label();
            this.labelCustomerName = new System.Windows.Forms.Label();
            this.comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTaxAmount = new DevExpress.XtraEditors.TextEdit();
            this.textBoxSubtotal = new DevExpress.XtraEditors.TextEdit();
            this.textBoxDiscount = new DevExpress.XtraEditors.TextEdit();
            this.textBoxTotal = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new Micromind.ClientUI.WindowsForms.formPOSHome.MMSBarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItemHold = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemReturnMode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFindReceipt = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemReprintReceipt = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCheckItemPrice = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCheckItemQuantity = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRecall = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemXReport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemYReport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemZReport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barLargeButtonItem2 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.menuItemStartNewShift = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemChangeSalesperson = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemVoidReceipt = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCashPayments = new DevExpress.XtraBars.BarButtonItem();
            this.barLargeButtonItem1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPettyCash = new DevExpress.XtraBars.BarButtonItem();
            this.buttonTransactions = new DevExpress.XtraEditors.SimpleButton();
            this.popupMenuTransactions = new DevExpress.XtraBars.PopupMenu(this.components);
            this.buttonDiscount = new DevExpress.XtraEditors.SimpleButton();
            this.buttonTotal = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClear = new DevExpress.XtraEditors.SimpleButton();
            this.buttonItems = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCustomers = new DevExpress.XtraEditors.SimpleButton();
            this.buttonPayment = new DevExpress.XtraEditors.SimpleButton();
            this.buttonTasks = new DevExpress.XtraEditors.SimpleButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelSysDocID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelSalesperson = new System.Windows.Forms.LinkLabel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelCashRegisterID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelVoucherDate = new DevExpress.XtraEditors.LabelControl();
            this.labelBatchID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelVoucherf = new DevExpress.XtraEditors.LabelControl();
            this.labelVoucherNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelShift = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelSalespersonBack = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.popupMenuItems = new DevExpress.XtraBars.PopupMenu(this.components);
            this.tabControlMain = new DevExpress.XtraTab.XtraTabControl();
            this.formManager = new Micromind.DataControls.FormManager();
            this.popupMenuTasks = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuCustomers = new DevExpress.XtraBars.PopupMenu(this.components);
            this.timerDate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tabHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelItemDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxReturnMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxScan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPayeeTaxGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTaxAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSubtotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTransactions)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).BeginInit();
            this.tabControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // resetLayoutToolStripMenuItem
            // 
            this.resetLayoutToolStripMenuItem.Name = "resetLayoutToolStripMenuItem";
            this.resetLayoutToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.resetLayoutToolStripMenuItem.Text = "Reset Layout";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetLayoutToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 28);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // tabHome
            // 
            this.tabHome.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.tabHome.Appearance.Header.Options.UseFont = true;
            this.tabHome.Appearance.Header.Options.UseTextOptions = true;
            this.tabHome.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tabHome.Controls.Add(this.splitContainer1);
            this.tabHome.Controls.Add(this.panel1);
            this.tabHome.Controls.Add(this.panelMain);
            this.tabHome.Name = "tabHome";
            this.tabHome.Size = new System.Drawing.Size(1356, 735);
            this.tabHome.TabPageWidth = 50;
            this.tabHome.Text = "POS";
            this.tabHome.Paint += new System.Windows.Forms.PaintEventHandler(this.tabHome_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelItemDetails);
            this.splitContainer1.Panel1.Controls.Add(this.productPhotoViewer);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.line2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.ultraGroupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.posGrid1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxNote);
            this.splitContainer1.Panel2.Controls.Add(this.panelControl4);
            this.splitContainer1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainer1.Size = new System.Drawing.Size(1356, 603);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 35;
            // 
            // panelItemDetails
            // 
            this.panelItemDetails.Controls.Add(this.labelLSTCost);
            this.panelItemDetails.Controls.Add(this.labelStdrdCost);
            this.panelItemDetails.Controls.Add(this.labelAVGCost);
            this.panelItemDetails.Controls.Add(this.labelLastCost);
            this.panelItemDetails.Controls.Add(this.labelStdCost);
            this.panelItemDetails.Controls.Add(this.labelAverageCost);
            this.panelItemDetails.Controls.Add(this.linkLabelShowHideCost);
            this.panelItemDetails.Controls.Add(this.label5);
            this.panelItemDetails.Controls.Add(this.label6);
            this.panelItemDetails.Controls.Add(this.label7);
            this.panelItemDetails.Controls.Add(this.label8);
            this.panelItemDetails.Controls.Add(this.label9);
            this.panelItemDetails.Controls.Add(this.label10);
            this.panelItemDetails.Controls.Add(this.labelStock);
            this.panelItemDetails.Controls.Add(this.labelCountry);
            this.panelItemDetails.Controls.Add(this.labelManufacturer);
            this.panelItemDetails.Controls.Add(this.labelBrand);
            this.panelItemDetails.Controls.Add(this.labelCategory);
            this.panelItemDetails.Controls.Add(this.labelUnit);
            this.panelItemDetails.Location = new System.Drawing.Point(6, 127);
            this.panelItemDetails.Name = "panelItemDetails";
            this.panelItemDetails.Size = new System.Drawing.Size(278, 227);
            this.panelItemDetails.TabIndex = 124;
            this.panelItemDetails.Visible = false;
            // 
            // labelLSTCost
            // 
            this.labelLSTCost.AutoSize = true;
            this.labelLSTCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLSTCost.Location = new System.Drawing.Point(67, 207);
            this.labelLSTCost.Name = "labelLSTCost";
            this.labelLSTCost.Size = new System.Drawing.Size(0, 17);
            this.labelLSTCost.TabIndex = 21;
            // 
            // labelStdrdCost
            // 
            this.labelStdrdCost.AutoSize = true;
            this.labelStdrdCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStdrdCost.Location = new System.Drawing.Point(99, 184);
            this.labelStdrdCost.Name = "labelStdrdCost";
            this.labelStdrdCost.Size = new System.Drawing.Size(0, 17);
            this.labelStdrdCost.TabIndex = 20;
            // 
            // labelAVGCost
            // 
            this.labelAVGCost.AutoSize = true;
            this.labelAVGCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAVGCost.Location = new System.Drawing.Point(97, 161);
            this.labelAVGCost.Name = "labelAVGCost";
            this.labelAVGCost.Size = new System.Drawing.Size(0, 17);
            this.labelAVGCost.TabIndex = 19;
            // 
            // labelLastCost
            // 
            this.labelLastCost.AutoSize = true;
            this.labelLastCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastCost.Location = new System.Drawing.Point(3, 207);
            this.labelLastCost.Name = "labelLastCost";
            this.labelLastCost.Size = new System.Drawing.Size(81, 17);
            this.labelLastCost.TabIndex = 18;
            this.labelLastCost.Text = "Last Cost:";
            // 
            // labelStdCost
            // 
            this.labelStdCost.AutoSize = true;
            this.labelStdCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStdCost.Location = new System.Drawing.Point(5, 184);
            this.labelStdCost.Name = "labelStdCost";
            this.labelStdCost.Size = new System.Drawing.Size(116, 17);
            this.labelStdCost.TabIndex = 17;
            this.labelStdCost.Text = "Standard Cost:";
            // 
            // labelAverageCost
            // 
            this.labelAverageCost.AutoSize = true;
            this.labelAverageCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAverageCost.Location = new System.Drawing.Point(5, 161);
            this.labelAverageCost.Name = "labelAverageCost";
            this.labelAverageCost.Size = new System.Drawing.Size(110, 17);
            this.labelAverageCost.TabIndex = 16;
            this.labelAverageCost.Text = "Average Cost:";
            // 
            // linkLabelShowHideCost
            // 
            this.linkLabelShowHideCost.AutoSize = true;
            this.linkLabelShowHideCost.Location = new System.Drawing.Point(217, 212);
            this.linkLabelShowHideCost.Name = "linkLabelShowHideCost";
            this.linkLabelShowHideCost.Size = new System.Drawing.Size(39, 16);
            this.linkLabelShowHideCost.TabIndex = 15;
            this.linkLabelShowHideCost.TabStop = true;
            this.linkLabelShowHideCost.Text = "Show";
            this.linkLabelShowHideCost.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelShowHideCost_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Stock In Hand:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Country :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Manufacturer :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "Brand :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Category :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Main Unit :";
            // 
            // labelStock
            // 
            this.labelStock.AutoSize = true;
            this.labelStock.Location = new System.Drawing.Point(99, 136);
            this.labelStock.Name = "labelStock";
            this.labelStock.Size = new System.Drawing.Size(0, 16);
            this.labelStock.TabIndex = 6;
            // 
            // labelCountry
            // 
            this.labelCountry.AutoSize = true;
            this.labelCountry.Location = new System.Drawing.Point(72, 111);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new System.Drawing.Size(0, 16);
            this.labelCountry.TabIndex = 5;
            // 
            // labelManufacturer
            // 
            this.labelManufacturer.AutoSize = true;
            this.labelManufacturer.Location = new System.Drawing.Point(97, 86);
            this.labelManufacturer.Name = "labelManufacturer";
            this.labelManufacturer.Size = new System.Drawing.Size(0, 16);
            this.labelManufacturer.TabIndex = 4;
            // 
            // labelBrand
            // 
            this.labelBrand.AutoSize = true;
            this.labelBrand.Location = new System.Drawing.Point(72, 59);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(0, 16);
            this.labelBrand.TabIndex = 3;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(72, 35);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(0, 16);
            this.labelCategory.TabIndex = 2;
            // 
            // labelUnit
            // 
            this.labelUnit.AutoSize = true;
            this.labelUnit.Location = new System.Drawing.Point(72, 8);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(0, 16);
            this.labelUnit.TabIndex = 1;
            // 
            // productPhotoViewer
            // 
            this.productPhotoViewer.BackColor = System.Drawing.Color.White;
            this.productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productPhotoViewer.Location = new System.Drawing.Point(6, 369);
            this.productPhotoViewer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 250);
            this.productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.Name = "productPhotoViewer";
            this.productPhotoViewer.Size = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.TabIndex = 123;
            this.productPhotoViewer.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.line1);
            this.panel3.Controls.Add(this.checkBoxReturnMode);
            this.panel3.Controls.Add(this.textBoxScan);
            this.panel3.Controls.Add(this.labelQuantity);
            this.panel3.Controls.Add(this.buttonQty);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 122);
            this.panel3.TabIndex = 0;
            // 
            // line1
            // 
            this.line1.BackColor = System.Drawing.Color.White;
            this.line1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.line1.DrawWidth = 1;
            this.line1.IsVertical = false;
            this.line1.LineBackColor = System.Drawing.Color.Black;
            this.line1.Location = new System.Drawing.Point(0, 121);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(288, 1);
            this.line1.TabIndex = 35;
            this.line1.TabStop = false;
            // 
            // checkBoxReturnMode
            // 
            this.checkBoxReturnMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxReturnMode.BackColor = System.Drawing.Color.DimGray;
            this.checkBoxReturnMode.BackColorInternal = System.Drawing.Color.DimGray;
            this.checkBoxReturnMode.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
            this.checkBoxReturnMode.Checked = true;
            this.checkBoxReturnMode.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxReturnMode.Location = new System.Drawing.Point(192, 55);
            this.checkBoxReturnMode.Name = "checkBoxReturnMode";
            this.checkBoxReturnMode.Size = new System.Drawing.Size(92, 40);
            this.checkBoxReturnMode.Style = Infragistics.Win.EditCheckStyle.Button;
            this.checkBoxReturnMode.TabIndex = 34;
            this.checkBoxReturnMode.Text = "Return";
            this.checkBoxReturnMode.CheckedChanged += new System.EventHandler(this.checkBoxReturnMode_CheckedChanged);
            // 
            // textBoxScan
            // 
            this.textBoxScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScan.EditValue = "";
            this.textBoxScan.Location = new System.Drawing.Point(5, 8);
            this.textBoxScan.Name = "textBoxScan";
            this.textBoxScan.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxScan.Properties.Appearance.Options.UseFont = true;
            this.textBoxScan.Properties.AutoHeight = false;
            this.textBoxScan.Properties.NullText = "Null Test";
            this.textBoxScan.Properties.NullValuePrompt = "Scan items here";
            this.textBoxScan.Properties.NullValuePromptShowForEmptyValue = true;
            this.textBoxScan.Size = new System.Drawing.Size(279, 38);
            this.textBoxScan.TabIndex = 19;
            // 
            // labelQuantity
            // 
            this.labelQuantity.BackColor = System.Drawing.Color.Transparent;
            this.labelQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuantity.ForeColor = System.Drawing.Color.Black;
            this.labelQuantity.Location = new System.Drawing.Point(76, 55);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(64, 40);
            this.labelQuantity.TabIndex = 20;
            this.labelQuantity.Text = "1";
            this.labelQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonQty
            // 
            this.buttonQty.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonQty.Appearance.Options.UseFont = true;
            this.buttonQty.Location = new System.Drawing.Point(9, 55);
            this.buttonQty.Name = "buttonQty";
            this.buttonQty.Size = new System.Drawing.Size(69, 40);
            this.buttonQty.TabIndex = 26;
            this.buttonQty.Text = "QTY";
            this.buttonQty.Click += new System.EventHandler(this.buttonQty_Click);
            // 
            // line2
            // 
            this.line2.BackColor = System.Drawing.Color.White;
            this.line2.Dock = System.Windows.Forms.DockStyle.Right;
            this.line2.DrawWidth = 1;
            this.line2.IsVertical = true;
            this.line2.LineBackColor = System.Drawing.Color.Black;
            this.line2.Location = new System.Drawing.Point(288, 0);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(1, 603);
            this.line2.TabIndex = 2;
            this.line2.TabStop = false;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
            this.ultraGroupBox1.Controls.Add(this.labelRowCount);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel3);
            this.ultraGroupBox1.Controls.Add(this.labelTotalQty);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraGroupBox1.Location = new System.Drawing.Point(17, 883);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(1712, 29);
            this.ultraGroupBox1.TabIndex = 33;
            // 
            // labelRowCount
            // 
            this.labelRowCount.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.labelRowCount.Location = new System.Drawing.Point(241, 6);
            this.labelRowCount.Name = "labelRowCount";
            this.labelRowCount.Size = new System.Drawing.Size(84, 16);
            this.labelRowCount.TabIndex = 115;
            this.labelRowCount.Text = "0";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraLabel3.Location = new System.Drawing.Point(158, 3);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(77, 22);
            this.ultraLabel3.TabIndex = 114;
            this.ultraLabel3.Text = "Rows Count:";
            // 
            // labelTotalQty
            // 
            this.labelTotalQty.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.labelTotalQty.Location = new System.Drawing.Point(84, 6);
            this.labelTotalQty.Name = "labelTotalQty";
            this.labelTotalQty.Size = new System.Drawing.Size(66, 16);
            this.labelTotalQty.TabIndex = 113;
            this.labelTotalQty.Text = "0";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraLabel1.Location = new System.Drawing.Point(6, 3);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(72, 22);
            this.ultraLabel1.TabIndex = 112;
            this.ultraLabel1.Text = "Total QTY:";
            // 
            // posGrid1
            // 
            this.posGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.posGrid1.Location = new System.Drawing.Point(2, 0);
            this.posGrid1.Margin = new System.Windows.Forms.Padding(6);
            this.posGrid1.Name = "posGrid1";
            this.posGrid1.ShowRowButtons = false;
            this.posGrid1.Size = new System.Drawing.Size(1055, 424);
            this.posGrid1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.label3.Location = new System.Drawing.Point(13, 568);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "Note:";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.EditValue = "";
            this.textBoxNote.Location = new System.Drawing.Point(73, 563);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.textBoxNote.Properties.Appearance.Options.UseFont = true;
            this.textBoxNote.Properties.AutoHeight = false;
            this.textBoxNote.Properties.MaxLength = 255;
            this.textBoxNote.Size = new System.Drawing.Size(651, 29);
            this.textBoxNote.TabIndex = 30;
            // 
            // panelControl4
            // 
            this.panelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl4.Controls.Add(this.labelVoid);
            this.panelControl4.Controls.Add(this.buttonCustomer);
            this.panelControl4.Controls.Add(this.checkBoxPriceIncludeTax);
            this.panelControl4.Controls.Add(this.labelPayeeTaxOption);
            this.panelControl4.Controls.Add(this.labelCustomerCode);
            this.panelControl4.Controls.Add(this.labelCustomerName);
            this.panelControl4.Controls.Add(this.comboBoxPayeeTaxGroup);
            this.panelControl4.Location = new System.Drawing.Point(4, 425);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(725, 131);
            this.panelControl4.TabIndex = 29;
            // 
            // labelVoid
            // 
            this.labelVoid.BackColor = System.Drawing.Color.Transparent;
            this.labelVoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoid.ForeColor = System.Drawing.Color.Red;
            this.labelVoid.Location = new System.Drawing.Point(130, 100);
            this.labelVoid.Name = "labelVoid";
            this.labelVoid.Size = new System.Drawing.Size(215, 25);
            this.labelVoid.TabIndex = 150;
            this.labelVoid.Text = "Voided";
            this.labelVoid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelVoid.Visible = false;
            // 
            // buttonCustomer
            // 
            this.buttonCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCustomer.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonCustomer.Appearance.Options.UseFont = true;
            this.buttonCustomer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonCustomer.ImageOptions.Image")));
            this.buttonCustomer.Location = new System.Drawing.Point(6, 72);
            this.buttonCustomer.Name = "buttonCustomer";
            this.buttonCustomer.Size = new System.Drawing.Size(134, 40);
            this.buttonCustomer.TabIndex = 28;
            this.buttonCustomer.Text = "Customer:";
            this.buttonCustomer.ToolTip = "F3";
            this.buttonCustomer.Click += new System.EventHandler(this.buttonCustomer_Click);
            // 
            // checkBoxPriceIncludeTax
            // 
            this.checkBoxPriceIncludeTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPriceIncludeTax.AutoSize = true;
            this.checkBoxPriceIncludeTax.Checked = true;
            this.checkBoxPriceIncludeTax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPriceIncludeTax.Location = new System.Drawing.Point(1632, 171);
            this.checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
            this.checkBoxPriceIncludeTax.Size = new System.Drawing.Size(145, 20);
            this.checkBoxPriceIncludeTax.TabIndex = 35;
            this.checkBoxPriceIncludeTax.Text = "Price inclusive of tax";
            this.checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
            this.checkBoxPriceIncludeTax.CheckedChanged += new System.EventHandler(this.checkBoxPriceIncludeTax_CheckedChanged);
            // 
            // labelPayeeTaxOption
            // 
            this.labelPayeeTaxOption.AutoSize = true;
            this.labelPayeeTaxOption.Location = new System.Drawing.Point(12, 81);
            this.labelPayeeTaxOption.Name = "labelPayeeTaxOption";
            this.labelPayeeTaxOption.Size = new System.Drawing.Size(14, 16);
            this.labelPayeeTaxOption.TabIndex = 149;
            this.labelPayeeTaxOption.Text = "0";
            this.labelPayeeTaxOption.Visible = false;
            // 
            // labelCustomerCode
            // 
            this.labelCustomerCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCustomerCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomerCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.labelCustomerCode.Location = new System.Drawing.Point(146, 78);
            this.labelCustomerCode.Name = "labelCustomerCode";
            this.labelCustomerCode.Size = new System.Drawing.Size(1629, 30);
            this.labelCustomerCode.TabIndex = 20;
            // 
            // labelCustomerName
            // 
            this.labelCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(37)))), ((int)(((byte)(127)))));
            this.labelCustomerName.Location = new System.Drawing.Point(118, 117);
            this.labelCustomerName.Name = "labelCustomerName";
            this.labelCustomerName.Size = new System.Drawing.Size(1657, 51);
            this.labelCustomerName.TabIndex = 20;
            // 
            // comboBoxPayeeTaxGroup
            // 
            this.comboBoxPayeeTaxGroup.Assigned = false;
            this.comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPayeeTaxGroup.CustomReportFieldName = "";
            this.comboBoxPayeeTaxGroup.CustomReportKey = "";
            this.comboBoxPayeeTaxGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxPayeeTaxGroup.DescriptionTextBox = null;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance1;
            this.comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance8;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance11;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPayeeTaxGroup.Editable = true;
            this.comboBoxPayeeTaxGroup.FilterString = "";
            this.comboBoxPayeeTaxGroup.HasAllAccount = false;
            this.comboBoxPayeeTaxGroup.HasCustom = false;
            this.comboBoxPayeeTaxGroup.IsDataLoaded = false;
            this.comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(12, 100);
            this.comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
            this.comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
            this.comboBoxPayeeTaxGroup.ReadOnly = true;
            this.comboBoxPayeeTaxGroup.ShowInactiveItems = false;
            this.comboBoxPayeeTaxGroup.ShowQuickAdd = true;
            this.comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(92, 24);
            this.comboBoxPayeeTaxGroup.TabIndex = 148;
            this.comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxPayeeTaxGroup.Visible = false;
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.label2);
            this.panelControl2.Controls.Add(this.textBoxTaxAmount);
            this.panelControl2.Controls.Add(this.textBoxSubtotal);
            this.panelControl2.Controls.Add(this.textBoxDiscount);
            this.panelControl2.Controls.Add(this.textBoxTotal);
            this.panelControl2.Controls.Add(this.buttonDiscount);
            this.panelControl2.Controls.Add(this.buttonTotal);
            this.panelControl2.Controls.Add(this.label4);
            this.panelControl2.Location = new System.Drawing.Point(730, 425);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(327, 175);
            this.panelControl2.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 29;
            this.label2.Text = "Tax:";
            // 
            // textBoxTaxAmount
            // 
            this.textBoxTaxAmount.EditValue = "0.00";
            this.textBoxTaxAmount.Location = new System.Drawing.Point(134, 75);
            this.textBoxTaxAmount.Name = "textBoxTaxAmount";
            this.textBoxTaxAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.textBoxTaxAmount.Properties.Appearance.Options.UseFont = true;
            this.textBoxTaxAmount.Properties.Appearance.Options.UseTextOptions = true;
            this.textBoxTaxAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textBoxTaxAmount.Properties.AutoHeight = false;
            this.textBoxTaxAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxTaxAmount.Properties.ReadOnly = true;
            this.textBoxTaxAmount.Size = new System.Drawing.Size(191, 33);
            this.textBoxTaxAmount.TabIndex = 28;
            // 
            // textBoxSubtotal
            // 
            this.textBoxSubtotal.EditValue = "0.00";
            this.textBoxSubtotal.Location = new System.Drawing.Point(134, 1);
            this.textBoxSubtotal.Name = "textBoxSubtotal";
            this.textBoxSubtotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.textBoxSubtotal.Properties.Appearance.Options.UseFont = true;
            this.textBoxSubtotal.Properties.Appearance.Options.UseTextOptions = true;
            this.textBoxSubtotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textBoxSubtotal.Properties.AutoHeight = false;
            this.textBoxSubtotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxSubtotal.Properties.ReadOnly = true;
            this.textBoxSubtotal.Size = new System.Drawing.Size(191, 37);
            this.textBoxSubtotal.TabIndex = 27;
            // 
            // textBoxDiscount
            // 
            this.textBoxDiscount.EditValue = "0.00";
            this.textBoxDiscount.Location = new System.Drawing.Point(134, 38);
            this.textBoxDiscount.Name = "textBoxDiscount";
            this.textBoxDiscount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.textBoxDiscount.Properties.Appearance.Options.UseFont = true;
            this.textBoxDiscount.Properties.Appearance.Options.UseTextOptions = true;
            this.textBoxDiscount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textBoxDiscount.Properties.AutoHeight = false;
            this.textBoxDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxDiscount.Properties.ReadOnly = true;
            this.textBoxDiscount.Size = new System.Drawing.Size(191, 37);
            this.textBoxDiscount.TabIndex = 27;
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.EditValue = "0.00";
            this.textBoxTotal.Location = new System.Drawing.Point(134, 108);
            this.textBoxTotal.MenuManager = this.barManager1;
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.textBoxTotal.Properties.Appearance.Options.UseFont = true;
            this.textBoxTotal.Properties.Appearance.Options.UseTextOptions = true;
            this.textBoxTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textBoxTotal.Properties.AutoHeight = false;
            this.textBoxTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.textBoxTotal.Properties.ReadOnly = true;
            this.textBoxTotal.Size = new System.Drawing.Size(191, 63);
            this.textBoxTotal.TabIndex = 27;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemHold,
            this.barButtonItemReturnMode,
            this.barButtonItemFindReceipt,
            this.barButtonItem3,
            this.barButtonItemReprintReceipt,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonCheckItemPrice,
            this.barButtonCheckItemQuantity,
            this.barButtonItem10,
            this.barButtonItem11,
            this.barButtonItemRecall,
            this.barSubItem1,
            this.barButtonItem1,
            this.barStaticItem1,
            this.barSubItem2,
            this.barStaticItem2,
            this.barButtonItemXReport,
            this.barButtonItem12,
            this.barButtonItemYReport,
            this.barButtonItemZReport,
            this.barButtonItemClose,
            this.barLargeButtonItem2,
            this.menuItemStartNewShift,
            this.barButtonItemChangeSalesperson,
            this.barButtonItemVoidReceipt,
            this.barButtonCashPayments,
            this.barLargeButtonItem1,
            this.barButtonItem4,
            this.barLinkContainerItem1,
            this.barSubItem3,
            this.barButtonItem13,
            this.barButtonItemPettyCash});
            this.barManager1.MaxItemId = 36;
            this.barManager1.ScrollInterval = 100;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1362, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 741);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1362, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 741);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1362, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 741);
            // 
            // barButtonItemHold
            // 
            this.barButtonItemHold.Caption = "Hold Receipt";
            this.barButtonItemHold.Id = 0;
            this.barButtonItemHold.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.Pause;
            this.barButtonItemHold.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(18)));
            this.barButtonItemHold.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemHold.Name = "barButtonItemHold";
            this.barButtonItemHold.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHold_ItemClick);
            // 
            // barButtonItemReturnMode
            // 
            this.barButtonItemReturnMode.Caption = "Return Mode";
            this.barButtonItemReturnMode.Id = 1;
            this.barButtonItemReturnMode.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.drawer;
            this.barButtonItemReturnMode.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemReturnMode.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemReturnMode.Name = "barButtonItemReturnMode";
            this.barButtonItemReturnMode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemReturnMode_ItemClick);
            // 
            // barButtonItemFindReceipt
            // 
            this.barButtonItemFindReceipt.Caption = "Find Receipt";
            this.barButtonItemFindReceipt.Id = 2;
            this.barButtonItemFindReceipt.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.document_search;
            this.barButtonItemFindReceipt.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemFindReceipt.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemFindReceipt.Name = "barButtonItemFindReceipt";
            this.barButtonItemFindReceipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemFindReceipt_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 3;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItemReprintReceipt
            // 
            this.barButtonItemReprintReceipt.Caption = "Reprint Receipt";
            this.barButtonItemReprintReceipt.Id = 4;
            this.barButtonItemReprintReceipt.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.print_printer;
            this.barButtonItemReprintReceipt.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemReprintReceipt.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemReprintReceipt.Name = "barButtonItemReprintReceipt";
            this.barButtonItemReprintReceipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemReprintReceipt_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Hold/Recall";
            this.barButtonItem5.Id = 5;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Open Drawer";
            this.barButtonItem6.Id = 6;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Find Item";
            this.barButtonItem7.Id = 7;
            this.barButtonItem7.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.find_item;
            this.barButtonItem7.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItem7.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonCheckItemPrice
            // 
            this.barButtonCheckItemPrice.Caption = "Check Price";
            this.barButtonCheckItemPrice.Id = 8;
            this.barButtonCheckItemPrice.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.pricecheck;
            this.barButtonCheckItemPrice.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonCheckItemPrice.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonCheckItemPrice.Name = "barButtonCheckItemPrice";
            this.barButtonCheckItemPrice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCheckItemPrice_ItemClick);
            // 
            // barButtonCheckItemQuantity
            // 
            this.barButtonCheckItemQuantity.Caption = "Check Quantity";
            this.barButtonCheckItemQuantity.Id = 9;
            this.barButtonCheckItemQuantity.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.box_download;
            this.barButtonCheckItemQuantity.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonCheckItemQuantity.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonCheckItemQuantity.Name = "barButtonCheckItemQuantity";
            this.barButtonCheckItemQuantity.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCheckItemQuantity_ItemClick);
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "barButtonItem10";
            this.barButtonItem10.Id = 10;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "Open Drawer";
            this.barButtonItem11.Id = 11;
            this.barButtonItem11.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.drawer1;
            this.barButtonItem11.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItem11.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem11.Name = "barButtonItem11";
            this.barButtonItem11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem11_ItemClick);
            // 
            // barButtonItemRecall
            // 
            this.barButtonItemRecall.Caption = "Recall Receipt";
            this.barButtonItemRecall.Id = 12;
            this.barButtonItemRecall.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.Play;
            this.barButtonItemRecall.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemRecall.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemRecall.Name = "barButtonItemRecall";
            this.barButtonItemRecall.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemRecall_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Clear";
            this.barSubItem1.Id = 13;
            this.barSubItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barSubItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Clear";
            this.barButtonItem1.Id = 14;
            this.barButtonItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "X-Report";
            this.barStaticItem1.Id = 15;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Y-Report";
            this.barSubItem2.Id = 16;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "Y-Report";
            this.barStaticItem2.Id = 17;
            this.barStaticItem2.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barStaticItem2.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem2.Name = "barStaticItem2";
            // 
            // barButtonItemXReport
            // 
            this.barButtonItemXReport.Caption = "X-Report";
            this.barButtonItemXReport.Id = 18;
            this.barButtonItemXReport.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.XReport1;
            this.barButtonItemXReport.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemXReport.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemXReport.Name = "barButtonItemXReport";
            this.barButtonItemXReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemXReport_ItemClick);
            // 
            // barButtonItem12
            // 
            this.barButtonItem12.Caption = "Y-Report";
            this.barButtonItem12.Id = 19;
            this.barButtonItem12.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItem12.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem12.Name = "barButtonItem12";
            // 
            // barButtonItemYReport
            // 
            this.barButtonItemYReport.Caption = "Y-Report";
            this.barButtonItemYReport.Id = 20;
            this.barButtonItemYReport.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.YReport1;
            this.barButtonItemYReport.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemYReport.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemYReport.Name = "barButtonItemYReport";
            this.barButtonItemYReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemYReport_ItemClick);
            // 
            // barButtonItemZReport
            // 
            this.barButtonItemZReport.Caption = "Z-Report";
            this.barButtonItemZReport.Id = 21;
            this.barButtonItemZReport.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.ZReport;
            this.barButtonItemZReport.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemZReport.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemZReport.Name = "barButtonItemZReport";
            this.barButtonItemZReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemZReport_ItemClick);
            // 
            // barButtonItemClose
            // 
            this.barButtonItemClose.Caption = "Close";
            this.barButtonItemClose.Id = 22;
            this.barButtonItemClose.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.exit2;
            this.barButtonItemClose.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemClose.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemClose.Name = "barButtonItemClose";
            this.barButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClose_ItemClick);
            // 
            // barLargeButtonItem2
            // 
            this.barLargeButtonItem2.Caption = "fsf";
            this.barLargeButtonItem2.Id = 23;
            this.barLargeButtonItem2.Name = "barLargeButtonItem2";
            // 
            // menuItemStartNewShift
            // 
            this.menuItemStartNewShift.Caption = "Start New Shift";
            this.menuItemStartNewShift.Id = 24;
            this.menuItemStartNewShift.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.shift;
            this.menuItemStartNewShift.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.menuItemStartNewShift.ItemAppearance.Normal.Options.UseFont = true;
            this.menuItemStartNewShift.Name = "menuItemStartNewShift";
            this.menuItemStartNewShift.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuItemStartNewShift_ItemClick);
            // 
            // barButtonItemChangeSalesperson
            // 
            this.barButtonItemChangeSalesperson.Caption = "Change Salesperson...";
            this.barButtonItemChangeSalesperson.Id = 25;
            this.barButtonItemChangeSalesperson.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.salespersonicon;
            this.barButtonItemChangeSalesperson.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemChangeSalesperson.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemChangeSalesperson.Name = "barButtonItemChangeSalesperson";
            this.barButtonItemChangeSalesperson.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemChangeSalesperson_ItemClick);
            // 
            // barButtonItemVoidReceipt
            // 
            this.barButtonItemVoidReceipt.Caption = "Void Receipt";
            this.barButtonItemVoidReceipt.Id = 28;
            this.barButtonItemVoidReceipt.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.voidpos;
            this.barButtonItemVoidReceipt.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(18)));
            this.barButtonItemVoidReceipt.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemVoidReceipt.Name = "barButtonItemVoidReceipt";
            this.barButtonItemVoidReceipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemVoidReceipt_ItemClick);
            // 
            // barButtonCashPayments
            // 
            this.barButtonCashPayments.Caption = "Petty Cash Payments";
            this.barButtonCashPayments.Id = 29;
            this.barButtonCashPayments.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.pettycash;
            this.barButtonCashPayments.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonCashPayments.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonCashPayments.Name = "barButtonCashPayments";
            this.barButtonCashPayments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonCashPayments_ItemClick);
            // 
            // barLargeButtonItem1
            // 
            this.barLargeButtonItem1.Caption = "Check";
            this.barLargeButtonItem1.Id = 30;
            this.barLargeButtonItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barLargeButtonItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barLargeButtonItem1.Name = "barLargeButtonItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "barButtonItem4";
            this.barButtonItem4.Id = 31;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barLinkContainerItem1
            // 
            this.barLinkContainerItem1.Caption = "barLinkContainerItem1";
            this.barLinkContainerItem1.Id = 32;
            this.barLinkContainerItem1.Name = "barLinkContainerItem1";
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "barSubItem3";
            this.barSubItem3.Id = 33;
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barButtonItem13
            // 
            this.barButtonItem13.Caption = "chck";
            this.barButtonItem13.Id = 34;
            this.barButtonItem13.Name = "barButtonItem13";
            // 
            // barButtonItemPettyCash
            // 
            this.barButtonItemPettyCash.Caption = "Petty Cash";
            this.barButtonItemPettyCash.Id = 35;
            this.barButtonItemPettyCash.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.cashinvoice;
            this.barButtonItemPettyCash.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 16F);
            this.barButtonItemPettyCash.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemPettyCash.Name = "barButtonItemPettyCash";
            this.barButtonItemPettyCash.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPettyCash_ItemClick);
            // 
            // buttonTransactions
            // 
            this.buttonTransactions.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonTransactions.Appearance.Options.UseFont = true;
            this.buttonTransactions.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonTransactions.ImageOptions.Image")));
            this.buttonTransactions.Location = new System.Drawing.Point(156, 14);
            this.buttonTransactions.Name = "buttonTransactions";
            this.barManager1.SetPopupContextMenu(this.buttonTransactions, this.popupMenuTransactions);
            this.buttonTransactions.Size = new System.Drawing.Size(183, 46);
            this.buttonTransactions.TabIndex = 27;
            this.buttonTransactions.Text = "T&ransactions";
            this.buttonTransactions.ToolTip = "F5";
            this.buttonTransactions.Click += new System.EventHandler(this.buttonTransactions_Click);
            // 
            // popupMenuTransactions
            // 
            this.popupMenuTransactions.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemHold),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemRecall),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemReturnMode),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemFindReceipt),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemReprintReceipt),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemVoidReceipt, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPettyCash)});
            this.popupMenuTransactions.Manager = this.barManager1;
            this.popupMenuTransactions.Name = "popupMenuTransactions";
            // 
            // buttonDiscount
            // 
            this.buttonDiscount.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDiscount.Appearance.Options.UseFont = true;
            this.buttonDiscount.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonDiscount.ImageOptions.Image")));
            this.buttonDiscount.Location = new System.Drawing.Point(5, 38);
            this.buttonDiscount.Name = "buttonDiscount";
            this.buttonDiscount.Size = new System.Drawing.Size(123, 37);
            this.buttonDiscount.TabIndex = 26;
            this.buttonDiscount.Text = "Discount:";
            this.buttonDiscount.ToolTip = "F10";
            this.buttonDiscount.Click += new System.EventHandler(this.buttonDiscount_Click);
            // 
            // buttonTotal
            // 
            this.buttonTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonTotal.Appearance.Options.UseFont = true;
            this.buttonTotal.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonTotal.ImageOptions.Image")));
            this.buttonTotal.Location = new System.Drawing.Point(5, 109);
            this.buttonTotal.Name = "buttonTotal";
            this.buttonTotal.Size = new System.Drawing.Size(123, 58);
            this.buttonTotal.TabIndex = 26;
            this.buttonTotal.Text = "Total:";
            this.buttonTotal.ToolTip = "F11";
            this.buttonTotal.Click += new System.EventHandler(this.buttonTotal_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "Subtotal:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonItems);
            this.panel1.Controls.Add(this.buttonCustomers);
            this.panel1.Controls.Add(this.buttonPayment);
            this.panel1.Controls.Add(this.buttonTransactions);
            this.panel1.Controls.Add(this.buttonTasks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 665);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1356, 70);
            this.panel1.TabIndex = 18;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonClear
            // 
            this.buttonClear.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonClear.Appearance.Options.UseFont = true;
            this.buttonClear.Location = new System.Drawing.Point(709, 14);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(137, 46);
            this.buttonClear.TabIndex = 28;
            this.buttonClear.Text = "C&lear";
            this.buttonClear.ToolTip = "F8";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonItems
            // 
            this.buttonItems.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonItems.Appearance.Options.UseFont = true;
            this.buttonItems.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonItems.ImageOptions.Image")));
            this.buttonItems.Location = new System.Drawing.Point(519, 14);
            this.buttonItems.Name = "buttonItems";
            this.buttonItems.Size = new System.Drawing.Size(167, 46);
            this.buttonItems.TabIndex = 27;
            this.buttonItems.Text = "&Items";
            this.buttonItems.ToolTip = "F7";
            this.buttonItems.Click += new System.EventHandler(this.buttonItems_Click);
            // 
            // buttonCustomers
            // 
            this.buttonCustomers.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonCustomers.Appearance.Options.UseFont = true;
            this.buttonCustomers.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonCustomers.ImageOptions.Image")));
            this.buttonCustomers.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.buttonCustomers.Location = new System.Drawing.Point(344, 14);
            this.buttonCustomers.Name = "buttonCustomers";
            this.buttonCustomers.Size = new System.Drawing.Size(169, 46);
            this.buttonCustomers.TabIndex = 27;
            this.buttonCustomers.Text = "&Customers";
            this.buttonCustomers.ToolTip = "F6";
            this.buttonCustomers.Click += new System.EventHandler(this.buttonCustomers_Click);
            // 
            // buttonPayment
            // 
            this.buttonPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonPayment.Appearance.Options.UseFont = true;
            this.buttonPayment.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonPayment.ImageOptions.Image")));
            this.buttonPayment.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.buttonPayment.Location = new System.Drawing.Point(1188, 14);
            this.buttonPayment.Name = "buttonPayment";
            this.buttonPayment.Size = new System.Drawing.Size(154, 46);
            this.buttonPayment.TabIndex = 27;
            this.buttonPayment.Text = "&Payment";
            this.buttonPayment.ToolTip = "F12";
            this.buttonPayment.Click += new System.EventHandler(this.buttonPayment_Click);
            // 
            // buttonTasks
            // 
            this.buttonTasks.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.buttonTasks.Appearance.Options.UseFont = true;
            this.buttonTasks.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonTasks.ImageOptions.Image")));
            this.buttonTasks.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.buttonTasks.Location = new System.Drawing.Point(16, 14);
            this.buttonTasks.Name = "buttonTasks";
            this.buttonTasks.Size = new System.Drawing.Size(134, 46);
            this.buttonTasks.TabIndex = 27;
            this.buttonTasks.Text = "&Tasks";
            this.buttonTasks.ToolTip = "F4";
            this.buttonTasks.Click += new System.EventHandler(this.buttonTasks_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.panel2);
            this.panelMain.Controls.Add(this.pictureBox3);
            this.panelMain.Controls.Add(this.pictureBox1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.MinimumSize = new System.Drawing.Size(1061, 50);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1356, 62);
            this.panelMain.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Micromind.ClientUI.Properties.Resources.headerbg2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.labelSysDocID);
            this.panel2.Controls.Add(this.labelControl9);
            this.panel2.Controls.Add(this.labelSalesperson);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.labelCashRegisterID);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Controls.Add(this.labelControl3);
            this.panel2.Controls.Add(this.labelVoucherDate);
            this.panel2.Controls.Add(this.labelBatchID);
            this.panel2.Controls.Add(this.labelControl5);
            this.panel2.Controls.Add(this.labelVoucherf);
            this.panel2.Controls.Add(this.labelVoucherNumber);
            this.panel2.Controls.Add(this.labelShift);
            this.panel2.Controls.Add(this.labelControl4);
            this.panel2.Controls.Add(this.labelSalespersonBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(518, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(838, 62);
            this.panel2.TabIndex = 40;
            // 
            // labelSysDocID
            // 
            this.labelSysDocID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSysDocID.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelSysDocID.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelSysDocID.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelSysDocID.Appearance.Options.UseBackColor = true;
            this.labelSysDocID.Appearance.Options.UseBorderColor = true;
            this.labelSysDocID.Appearance.Options.UseForeColor = true;
            this.labelSysDocID.Appearance.Options.UseTextOptions = true;
            this.labelSysDocID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelSysDocID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelSysDocID.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelSysDocID.Location = new System.Drawing.Point(383, 28);
            this.labelSysDocID.Name = "labelSysDocID";
            this.labelSysDocID.Size = new System.Drawing.Size(94, 19);
            this.labelSysDocID.TabIndex = 38;
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl9.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl9.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl9.Appearance.Options.UseBackColor = true;
            this.labelControl9.Appearance.Options.UseBorderColor = true;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Appearance.Options.UseTextOptions = true;
            this.labelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl9.Location = new System.Drawing.Point(383, 11);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(94, 18);
            this.labelControl9.TabIndex = 39;
            this.labelControl9.Text = "Doc ID";
            // 
            // labelSalesperson
            // 
            this.labelSalesperson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSalesperson.BackColor = System.Drawing.Color.Transparent;
            this.labelSalesperson.ForeColor = System.Drawing.Color.White;
            this.labelSalesperson.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.labelSalesperson.Location = new System.Drawing.Point(693, 30);
            this.labelSalesperson.Name = "labelSalesperson";
            this.labelSalesperson.Size = new System.Drawing.Size(126, 14);
            this.labelSalesperson.TabIndex = 35;
            this.labelSalesperson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSalesperson.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelSalesperson_LinkClicked);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseBorderColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl1.Location = new System.Drawing.Point(140, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 18);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Counter ID";
            // 
            // labelCashRegisterID
            // 
            this.labelCashRegisterID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCashRegisterID.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelCashRegisterID.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelCashRegisterID.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelCashRegisterID.Appearance.Options.UseBackColor = true;
            this.labelCashRegisterID.Appearance.Options.UseBorderColor = true;
            this.labelCashRegisterID.Appearance.Options.UseForeColor = true;
            this.labelCashRegisterID.Appearance.Options.UseTextOptions = true;
            this.labelCashRegisterID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelCashRegisterID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCashRegisterID.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelCashRegisterID.Location = new System.Drawing.Point(140, 28);
            this.labelCashRegisterID.Name = "labelCashRegisterID";
            this.labelCashRegisterID.Size = new System.Drawing.Size(82, 19);
            this.labelCashRegisterID.TabIndex = 35;
            this.labelCashRegisterID.Text = "1";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseBorderColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl2.Location = new System.Drawing.Point(221, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 18);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "Batch No";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseBorderColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl3.Location = new System.Drawing.Point(302, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(82, 18);
            this.labelControl3.TabIndex = 35;
            this.labelControl3.Text = "Shift No";
            // 
            // labelVoucherDate
            // 
            this.labelVoucherDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVoucherDate.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelVoucherDate.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelVoucherDate.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelVoucherDate.Appearance.Options.UseBackColor = true;
            this.labelVoucherDate.Appearance.Options.UseBorderColor = true;
            this.labelVoucherDate.Appearance.Options.UseForeColor = true;
            this.labelVoucherDate.Appearance.Options.UseTextOptions = true;
            this.labelVoucherDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelVoucherDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelVoucherDate.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelVoucherDate.Location = new System.Drawing.Point(587, 28);
            this.labelVoucherDate.Name = "labelVoucherDate";
            this.labelVoucherDate.Size = new System.Drawing.Size(105, 19);
            this.labelVoucherDate.TabIndex = 35;
            this.labelVoucherDate.Text = "04-1-2014";
            // 
            // labelBatchID
            // 
            this.labelBatchID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBatchID.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelBatchID.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelBatchID.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelBatchID.Appearance.Options.UseBackColor = true;
            this.labelBatchID.Appearance.Options.UseBorderColor = true;
            this.labelBatchID.Appearance.Options.UseForeColor = true;
            this.labelBatchID.Appearance.Options.UseTextOptions = true;
            this.labelBatchID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelBatchID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelBatchID.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelBatchID.Location = new System.Drawing.Point(221, 28);
            this.labelBatchID.Name = "labelBatchID";
            this.labelBatchID.Size = new System.Drawing.Size(82, 19);
            this.labelBatchID.TabIndex = 35;
            this.labelBatchID.Text = "1";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl5.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.Options.UseBackColor = true;
            this.labelControl5.Appearance.Options.UseBorderColor = true;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl5.Location = new System.Drawing.Point(691, 11);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(133, 18);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "Salesperson";
            // 
            // labelVoucherf
            // 
            this.labelVoucherf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVoucherf.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelVoucherf.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelVoucherf.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelVoucherf.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelVoucherf.Appearance.Options.UseBackColor = true;
            this.labelVoucherf.Appearance.Options.UseBorderColor = true;
            this.labelVoucherf.Appearance.Options.UseFont = true;
            this.labelVoucherf.Appearance.Options.UseForeColor = true;
            this.labelVoucherf.Appearance.Options.UseTextOptions = true;
            this.labelVoucherf.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelVoucherf.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelVoucherf.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelVoucherf.Location = new System.Drawing.Point(474, 11);
            this.labelVoucherf.Name = "labelVoucherf";
            this.labelVoucherf.Size = new System.Drawing.Size(114, 18);
            this.labelVoucherf.TabIndex = 35;
            this.labelVoucherf.Text = "Voucher No";
            // 
            // labelVoucherNumber
            // 
            this.labelVoucherNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVoucherNumber.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelVoucherNumber.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelVoucherNumber.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelVoucherNumber.Appearance.Options.UseBackColor = true;
            this.labelVoucherNumber.Appearance.Options.UseBorderColor = true;
            this.labelVoucherNumber.Appearance.Options.UseForeColor = true;
            this.labelVoucherNumber.Appearance.Options.UseTextOptions = true;
            this.labelVoucherNumber.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelVoucherNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelVoucherNumber.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelVoucherNumber.Location = new System.Drawing.Point(474, 28);
            this.labelVoucherNumber.Name = "labelVoucherNumber";
            this.labelVoucherNumber.Size = new System.Drawing.Size(119, 19);
            this.labelVoucherNumber.TabIndex = 35;
            this.labelVoucherNumber.Text = "00000001";
            // 
            // labelShift
            // 
            this.labelShift.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShift.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelShift.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelShift.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelShift.Appearance.Options.UseBackColor = true;
            this.labelShift.Appearance.Options.UseBorderColor = true;
            this.labelShift.Appearance.Options.UseForeColor = true;
            this.labelShift.Appearance.Options.UseTextOptions = true;
            this.labelShift.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelShift.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelShift.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelShift.Location = new System.Drawing.Point(302, 28);
            this.labelShift.Name = "labelShift";
            this.labelShift.Size = new System.Drawing.Size(82, 19);
            this.labelShift.TabIndex = 35;
            this.labelShift.Text = "1";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseBorderColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Location = new System.Drawing.Point(586, 11);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(107, 18);
            this.labelControl4.TabIndex = 35;
            this.labelControl4.Text = "Date";
            // 
            // labelSalespersonBack
            // 
            this.labelSalespersonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSalespersonBack.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(200)))), ((int)(((byte)(243)))));
            this.labelSalespersonBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelSalespersonBack.Appearance.Options.UseBorderColor = true;
            this.labelSalespersonBack.Appearance.Options.UseForeColor = true;
            this.labelSalespersonBack.Appearance.Options.UseTextOptions = true;
            this.labelSalespersonBack.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelSalespersonBack.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelSalespersonBack.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelSalespersonBack.Location = new System.Drawing.Point(691, 28);
            this.labelSalespersonBack.Name = "labelSalespersonBack";
            this.labelSalespersonBack.Size = new System.Drawing.Size(133, 19);
            this.labelSalespersonBack.TabIndex = 35;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(322, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1034, 62);
            this.pictureBox3.TabIndex = 28;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Micromind.ClientUI.Properties.Resources.headerbg2;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(322, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // popupMenuItems
            // 
            this.popupMenuItems.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCheckItemPrice),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCheckItemQuantity)});
            this.popupMenuItems.Manager = this.barManager1;
            this.popupMenuItems.Name = "popupMenuItems";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.False;
            this.tabControlMain.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.tabControlMain.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.tabControlMain.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Vertical;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.MultiLine = DevExpress.Utils.DefaultBoolean.False;
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedTabPage = this.tabHome;
            this.tabControlMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.tabControlMain.Size = new System.Drawing.Size(1362, 741);
            this.tabControlMain.TabIndex = 14;
            this.tabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabHome});
            this.tabControlMain.TabPageWidth = 100;
            // 
            // formManager
            // 
            this.formManager.BackColor = System.Drawing.Color.RosyBrown;
            this.formManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.formManager.IsForcedDirty = false;
            this.formManager.Location = new System.Drawing.Point(0, 0);
            this.formManager.MaximumSize = new System.Drawing.Size(20, 20);
            this.formManager.MinimumSize = new System.Drawing.Size(20, 20);
            this.formManager.Name = "formManager";
            this.formManager.Size = new System.Drawing.Size(20, 20);
            this.formManager.TabIndex = 17;
            this.formManager.Text = "formManager1";
            this.formManager.Visible = false;
            this.formManager.Click += new System.EventHandler(this.formManager_Click);
            // 
            // popupMenuTasks
            // 
            this.popupMenuTasks.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuItemStartNewShift),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemXReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemYReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemZReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCashPayments, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemClose, true)});
            this.popupMenuTasks.Manager = this.barManager1;
            this.popupMenuTasks.Name = "popupMenuTasks";
            // 
            // popupMenuCustomers
            // 
            this.popupMenuCustomers.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemChangeSalesperson)});
            this.popupMenuCustomers.Manager = this.barManager1;
            this.popupMenuCustomers.Name = "popupMenuCustomers";
            // 
            // timerDate
            // 
            this.timerDate.Enabled = true;
            this.timerDate.Interval = 10000;
            // 
            // formPOSHome
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1362, 741);
            this.ControlBox = false;
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(858, 544);
            this.Name = "formPOSHome";
            this.ShowInTaskbar = false;
            this.Text = "Sales Receipt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tabHome.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelItemDetails.ResumeLayout(false);
            this.panelItemDetails.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxReturnMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxScan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPayeeTaxGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTaxAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSubtotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTransactions)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void LoadCashRegister()
		{
			try
			{
				DataSet cashRegisterByComputerName = Factory.POSCashRegisterSystem.GetCashRegisterByComputerName(Environment.MachineName);
				if (cashRegisterByComputerName == null || cashRegisterByComputerName.Tables[0].Rows.Count == 0)
				{
					if (new AssignCashRegisterForm().ShowDialog(this) == DialogResult.OK)
					{
						LoadCashRegister();
					}
					else
					{
						ErrorHelper.InformationMessage("You must assign a Cash Register to this system to continue. Application now exit.");
						Close();
					}
				}
				else
				{
					Global.DefaultCustomerID = cashRegisterByComputerName.Tables[0].Rows[0]["DefaultCustomerID"].ToString();
					Global.DefaultCustomerName = cashRegisterByComputerName.Tables[0].Rows[0]["DefaultCustomerName"].ToString();
					labelCashRegisterID.Text = (Global.CurrentCashRegisterID = (UIGlobal.CashRegisterID = cashRegisterByComputerName.Tables[0].Rows[0]["CashRegisterID"].ToString()));
					SystemDocID = cashRegisterByComputerName.Tables[0].Rows[0]["ReceiptDocID"].ToString();
					labelVoucherNumber.Text = GetNextVoucherNumber();
					ExpenseDocID = cashRegisterByComputerName.Tables[0].Rows[0]["ExpenseDocID"].ToString();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupGrid()
		{
			try
			{
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["ProductID"].Header.Caption = "Code";
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Quantity"].Format = "n";
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Price"].Format = Format.GridAmountFormat;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Tax"].Format = Format.GridAmountFormat;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].Format = Format.GridAmountFormat;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Quantity"].Width = 60;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Price"].Width = 60;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Amount"].Width = 60;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["UPC"].Width = 60;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Description"].Width = 200;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Quantity"].MaxWidth = 150;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Price"].MaxWidth = 150;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Amount"].MaxWidth = 150;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Tax"].Hidden = false;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Tax"].Header.Caption = "Tax";
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				posGrid1.DataGrid.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
				UltraGridColumn ultraGridColumn = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["ProductID"];
				UltraGridColumn ultraGridColumn2 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Description"];
				UltraGridColumn ultraGridColumn3 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["UPC"];
				UltraGridColumn ultraGridColumn4 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Tax"];
				UltraGridColumn ultraGridColumn5 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Amount"];
				UltraGridColumn ultraGridColumn6 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn7 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Price"];
				Activation activation2 = posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].CellActivation = Activation.NoEdit;
				Activation activation4 = ultraGridColumn7.CellActivation = activation2;
				Activation activation6 = ultraGridColumn6.CellActivation = activation4;
				Activation activation8 = ultraGridColumn5.CellActivation = activation6;
				Activation activation10 = ultraGridColumn4.CellActivation = activation8;
				Activation activation12 = ultraGridColumn3.CellActivation = activation10;
				Activation activation15 = ultraGridColumn.CellActivation = (ultraGridColumn2.CellActivation = activation12);
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Barcode"].Hidden = true;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries.Add("Rows", SummaryType.Count, posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["ProductID"], SummaryPosition.UseSummaryPositionColumn);
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["Rows"].Appearance.BackColor = Color.White;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["Rows"].Appearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["Rows"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				posGrid1.DataGrid.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				if (checkBoxPriceIncludeTax.Checked)
				{
					posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = true;
				}
				else
				{
					posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void formPOSHome_Load(object sender, EventArgs e)
		{
			try
			{
				LoadItems();
				SetSecurity();
				string text = salesPersonID = (labelSalesperson.Text = Security.DefaultSalespersonID);
				posGrid1.SetupUI();
				SetupGrid();
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
				{
					canAccessCost = false;
				}
				else
				{
					canAccessCost = true;
				}
				textBoxScan.Focus();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void Init()
		{
			defaultLookAndFeel1 = new DefaultLookAndFeel();
			popupMenuTransactions.Popup += PopupMenuTransactions_Popup;
			RefreshData();
		}

		private void PopupMenuTransactions_Popup(object sender, EventArgs e)
		{
			buttonTransactions.Focus();
		}

		private void LoadHomePageInfo()
		{
		}

		public void OnActivated()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					if (!isFirstActivated)
					{
						isFirstActivated = true;
						LoadReminders();
						LoadCashRegister();
						UIGlobal.LoadCurrentPOSLocation();
						batchID = UIGlobal.GetActiveBatchNumber();
						if (batchID < 0)
						{
							labelBatchID.Text = "Closed";
						}
						else
						{
							labelBatchID.Text = batchID.ToString();
						}
						shiftID = UIGlobal.GetActiveShiftNumber();
						if (shiftID < 0)
						{
							labelShift.Text = "Closed";
						}
						else
						{
							labelShift.Text = shiftID.ToString();
						}
						validateQtyOnhand = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.POSCheckSufficientQty, defaultValue: true);
						ClearForm();
						textBoxScan.Focus();
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					Global.ChangeApplicationStatusMessage(Text);
				}
			}
		}

		private void LoadReminders()
		{
			DataSet reminders = Factory.ReminderSystem.GetReminders(Global.CurrentUser);
			SetReminders(reminders);
		}

		private void SetReminders(DataSet data)
		{
			_ = data.Tables[0].Rows.Count;
			_ = data?.Tables.Count;
		}

		private void CompanyMessage()
		{
		}

		public void Reset()
		{
			CompanyMessage();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.General;
		}

		public static int GetScreenID()
		{
			return 0;
		}

		public void RefreshData()
		{
			OnActivated();
		}

		private void navBarControl2_Click(object sender, EventArgs e)
		{
		}

		private void navBarLeft_Click(object sender, EventArgs e)
		{
		}

		private void linkSalesReceipt_LinkClicked(object sender, NavBarLinkEventArgs e)
		{
		}

		private void linkSalesReturnCredit_LinkClicked(object sender, NavBarLinkEventArgs e)
		{
		}

		private void linkItemList_LinkClicked(object sender, NavBarLinkEventArgs e)
		{
		}

		private void tabHome_Paint(object sender, PaintEventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void simpleButton6_Click(object sender, EventArgs e)
		{
			if (textBoxScan.Text.Trim() == "")
			{
				textBoxScan.Focus();
				return;
			}
			string text = textBoxScan.Text;
			ScanItem(text);
		}

		private void ScanItem(string itemCode)
		{
			try
			{
				if (shiftID > 0)
				{
					goto IL_007f;
				}
				if (ErrorHelper.QuestionMessageYesNo("There is no Active Shift. Do you want to start a new shift?") == DialogResult.Yes)
				{
					if (StartNewShift())
					{
						goto IL_007f;
					}
					labelQuantity.Text = "1";
					textBoxScan.Clear();
					textBoxScan.Focus();
				}
				else
				{
					labelQuantity.Text = "1";
					textBoxScan.Clear();
					textBoxScan.Focus();
				}
				goto end_IL_0000;
				IL_0505:
				DataTable dataTable;
				DataRow dataRow;
				dataTable.Rows.Add(dataRow);
				ItemTaxOptions itemTaxOptions;
				DataRow dataRow2;
				if (itemTaxOptions != ItemTaxOptions.NonTaxable)
				{
					if (dataRow["TaxGroupID"] != DBNull.Value)
					{
						itemTaxOptions = (ItemTaxOptions)byte.Parse(dataRow2["TaxOption"].ToString());
					}
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData((PayeeTaxOptions)checked((byte)int.Parse(labelPayeeTaxOption.Text)), comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, dataRow["TaxGroupID"].ToString());
					posGrid1.DataGrid.Rows[checked(posGrid1.DataGrid.Rows.Count - 1)].Cells["Tax"].Tag = tag;
				}
				CalculateTotal();
				bool flag;
				decimal num;
				checked
				{
					if (itemTaxOptions != ItemTaxOptions.NonTaxable)
					{
						decimal amount = num;
						decimal subtotal = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
						decimal tradeDiscount = decimal.Parse(textBoxDiscount.Text);
						UIGlobal.CalculateRowTax(posGrid1.DataGrid.Rows[posGrid1.DataGrid.Rows.Count - 1], "Tax", amount, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
						CalculateTotal();
					}
					labelQuantity.Text = "1";
					posGrid1.DataGrid.ActiveRow = posGrid1.DataGrid.Rows[posGrid1.DataGrid.Rows.Count - 1];
					posGrid1.DataGrid.ActiveRowScrollRegion.ScrollRowIntoView(posGrid1.DataGrid.ActiveRow);
					flag = true;
					textBoxScan.Clear();
					textBoxScan.Focus();
					goto end_IL_0000;
				}
				IL_007f:
				DataRow[] array = null;
				flag = true;
				DataSet productList = CombosData.GetProductList(refresh: false);
				array = productList.Tables[0].Select("Code = '" + itemCode + "'");
				if (array == null || array.Length == 0)
				{
					array = productList.Tables[0].Select("UPC = '" + itemCode + "'");
					if ((array == null || array.Length == 0) && itemCode.Length >= 12)
					{
						flag = false;
						itemCode = itemCode.Substring(0, 6);
						array = productList.Tables[0].Select("UPC = '" + itemCode + "'");
					}
				}
				if (array == null || array.Length == 0)
				{
					ItemLookupDialog itemLookupDialog = new ItemLookupDialog();
					itemLookupDialog.LookupType = ItemLookupDialog.LookupItemTypes.Product;
					itemLookupDialog.SelectedItem = textBoxScan.Text;
					itemLookupDialog.SysLocation = Global.CurrentPOSLocationID;
					if (itemLookupDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(itemLookupDialog.SelectedRow.Cells["Code"].Value.ToString()))
					{
						ScanItem(itemLookupDialog.SelectedRow.Cells["Code"].Value.ToString());
					}
				}
				else
				{
					dataRow2 = array[0];
					dataTable = (posGrid1.DataGrid.DataSource as DataTable);
					ItemTypes itemTypes = ItemTypes.Inventory;
					if (dataRow2["ItemType"].ToString() != "")
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(dataRow2["ItemType"].ToString()));
					}
					dataRow = dataTable.NewRow();
					bool flag2 = false;
					dataRow["ProductID"] = dataRow2["Code"];
					if (dataRow2["UPC"] == DBNull.Value || dataRow2["UPC"].ToString() == "")
					{
						dataRow["UPC"] = dataRow2["Code"];
					}
					else
					{
						dataRow["UPC"] = dataRow2["UPC"];
					}
					dataRow["Barcode"] = textBoxScan.Text;
					dataRow["Description"] = dataRow2["Name"];
					dataRow["Price"] = dataRow2["Price"];
					dataRow["Discount"] = 0;
					itemTaxOptions = ItemTaxOptions.Taxable;
					if (dataRow2["TaxOption"].ToString() != "")
					{
						itemTaxOptions = (ItemTaxOptions)checked((byte)int.Parse(dataRow2["TaxOption"].ToString()));
					}
					dataRow["TaxOption"] = itemTaxOptions;
					switch (itemTaxOptions)
					{
					case ItemTaxOptions.BasedOnCustomer:
						dataRow["TaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
						break;
					case ItemTaxOptions.Taxable:
						dataRow["TaxGroupID"] = dataRow2["TaxGroupID"];
						break;
					case ItemTaxOptions.NonTaxable:
						dataRow["TaxGroupID"] = DBNull.Value;
						break;
					}
					if (dataRow2["IsPriceEmbedded"].ToString() != "")
					{
						flag2 = bool.Parse(dataRow2["IsPriceEmbedded"].ToString());
					}
					num = default(decimal);
					if (!flag2 || flag)
					{
						dataRow["Quantity"] = labelQuantity.Text;
						decimal num2 = decimal.Parse(dataRow["Quantity"].ToString());
						decimal d = default(decimal);
						if (dataRow["Price"] != DBNull.Value)
						{
							d = decimal.Parse(dataRow["Price"].ToString());
						}
						else
						{
							dataRow["Price"] = 0;
						}
						if (checkBoxReturnMode.Checked)
						{
							num2 *= -1m;
						}
						num = num2 * d;
						dataRow["Quantity"] = num2;
						dataRow["Amount"] = num;
						goto IL_0505;
					}
					num = GetItemDetails(textBoxScan.Text);
					if (!(num == 0m))
					{
						decimal d2 = decimal.Parse(dataRow["Price"].ToString());
						decimal num3 = num / d2;
						if (checkBoxReturnMode.Checked)
						{
							num3 *= -1m;
						}
						dataRow["Quantity"] = num3;
						dataRow["Amount"] = num;
						goto IL_0505;
					}
				}
				end_IL_0000:;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonTransactions_Click(object sender, EventArgs e)
		{
			popupMenuTransactions.ShowPopup(buttonTransactions.PointToScreen(new Point(0, 0)));
		}

		private void barItemFindReceipt_ItemClick(object sender, ItemClickEventArgs e)
		{
			TransactionLookupDialog transactionLookupDialog = new TransactionLookupDialog();
			transactionLookupDialog.SysDocID = sysDocID;
			if (transactionLookupDialog.ShowDialog() == DialogResult.OK)
			{
				string voucherID = transactionLookupDialog.SelectedRow.Cells["VoucherID"].Value.ToString();
				transactionLookupDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
				LoadData(sysDocID, voucherID);
			}
		}

		public void LoadData(string sysDocID, string voucherID)
		{
			try
			{
				if (!(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.SalesPOSSystem.GetSalesPOSByID(sysDocID, voucherID);
					if (currentData != null)
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

		private void FillData()
		{
			try
			{
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Sales_POS"].Rows[0];
					string a = dataRow["BatchID"].ToString();
					string a2 = dataRow["ShiftID"].ToString();
					if (a != UIGlobal.GetActiveBatchNumber().ToString() || a2 != UIGlobal.GetActiveShiftNumber().ToString())
					{
						isEditable = false;
					}
					else
					{
						isEditable = true;
					}
					labelVoucherDate.Text = DateTime.Parse(dataRow["TransactionDate"].ToString()).ToShortDateString();
					labelVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					labelCustomerCode.Text = dataRow["CustomerID"].ToString();
					labelCustomerName.Text = dataRow["CustomerName"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					labelSalesperson.Text = dataRow["SalespersonID"].ToString();
					if (dataRow["TotalFC"] != DBNull.Value)
					{
						textBoxTotal.Text = decimal.Parse(dataRow["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["DiscountFC"] != DBNull.Value)
					{
						textBoxDiscount.Text = decimal.Parse(dataRow["DiscountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxDiscount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["PriceIncludeTax"] != DBNull.Value && CompanyPreferences.IsTax)
					{
						checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
					}
					else
					{
						checkBoxPriceIncludeTax.Checked = false;
					}
					DataTable dataTable = posGrid1.DataGrid.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Sales_POS_Detail") && currentData.SalesPOSDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Sales_POS_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["ProductID"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							if (row["TaxOption"] != DBNull.Value)
							{
								dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
							}
							else
							{
								dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
							}
							if (row["TaxAmount"] != DBNull.Value)
							{
								dataRow3["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.GridAmountFormat);
							}
							if (row["TaxGroupID"] != DBNull.Value)
							{
								dataRow3["TaxGroupID"] = row["TaxGroupID"];
							}
							dataRow3["Description"] = row["Description"];
							dataRow3["ItemType"] = row["ItemType"];
							if (row["UnitPriceFC"] != DBNull.Value)
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPriceFC"].ToString()).ToString(Format.GridAmountFormat);
							}
							else
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.GridAmountFormat);
							}
							if (row["Discount"] != DBNull.Value)
							{
								dataRow3["Discount"] = decimal.Parse(row["Discount"].ToString()).ToString(Format.GridAmountFormat);
							}
							else
							{
								dataRow3["Discount"] = decimal.Parse(row["Discount"].ToString()).ToString(Format.GridAmountFormat);
							}
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
							decimal.TryParse(dataRow3["Price"].ToString(), out result2);
							decimal.TryParse(dataRow3["Discount"].ToString(), out result3);
							dataRow3["Amount"] = Math.Round(result * (result2 - result3), Global.CurDecimalPoints);
							if (row["Barcode"] != DBNull.Value)
							{
								dataRow3["Barcode"] = row["Barcode"];
							}
							else
							{
								dataRow3["Barcode"] = row["Barcode"];
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
						}
						foreach (UltraGridRow row2 in posGrid1.DataGrid.Rows)
						{
							if (checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString())) == 4)
							{
								row2.Cells["Quantity"].Activation = Activation.Disabled;
							}
							DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = " + row2.Index + " AND TaxLevel = " + (byte)2);
							if (array.Length != 0)
							{
								TaxTransactionData taxTransactionData = new TaxTransactionData();
								taxTransactionData.Merge(array);
								row2.Cells["Tax"].Tag = taxTransactionData;
							}
						}
						DataRow[] array2 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
						if (array2.Length != 0)
						{
							TaxTransactionData taxTransactionData2 = new TaxTransactionData();
							taxTransactionData2.Merge(array2);
							textBoxTaxAmount.Tag = taxTransactionData2;
						}
						CalculateTotal();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isDataLoading = false;
			}
		}

		private void buttonItems_Click(object sender, EventArgs e)
		{
			Point point = default(Point);
			point = buttonItems.PointToScreen(new Point(0, 0));
			popupMenuItems.ShowPopup(point);
		}

		private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void LoadItems()
		{
			productListData = Factory.ProductSystem.GetProductListPOS();
		}

		private void buttonItemDown_Click(object sender, EventArgs e)
		{
			posGrid1.DataGrid.PerformAction(UltraGridAction.NextRow);
			textBoxScan.Focus();
		}

		private void buttonItemUp_Click(object sender, EventArgs e)
		{
			posGrid1.DataGrid.PerformAction(UltraGridAction.PrevRow);
			textBoxScan.Focus();
		}

		private void buttonDeleteRow_Click(object sender, EventArgs e)
		{
		}

		private void DeleteRow()
		{
			if (posGrid1.DataGrid.ActiveRow != null && ErrorHelper.QuestionMessageYesNo("Delete selected row?") == DialogResult.Yes)
			{
				int num = posGrid1.DataGrid.ActiveRow.Index;
				posGrid1.DataGrid.ActiveRow.Delete(displayPrompt: false);
				if (posGrid1.DataGrid.Rows.Count > 0)
				{
					if (posGrid1.DataGrid.Rows.Count <= num)
					{
						num = checked(num - 1);
					}
					posGrid1.DataGrid.ActiveRow = posGrid1.DataGrid.Rows[num];
				}
			}
			CalculateTotal();
			if (textBoxDiscount.Text.ToDecimal() > 0m)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
			}
			textBoxScan.Focus();
		}

		private void buttonQty_Click(object sender, EventArgs e)
		{
			if (textBoxScan.Text == "")
			{
				labelQuantity.Text = "1";
			}
			int result = 1;
			int.TryParse(textBoxScan.Text, out result);
			if (result == 0)
			{
				result = 1;
			}
			labelQuantity.Text = result.ToString();
			textBoxScan.Clear();
			textBoxScan.Focus();
		}

		private void buttonCal1_Click(object sender, EventArgs e)
		{
		}

		private void CalculateTotal()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal result2 = default(decimal);
			foreach (UltraGridRow row in posGrid1.DataGrid.Rows)
			{
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				num3 += decimal.Parse(row.Cells["Quantity"].Value.ToString());
				decimal.Parse(row.Cells["Quantity"].Value.ToString());
				decimal.Parse(row.Cells["Price"].Value.ToString());
				decimal.TryParse(row.Cells["Discount"].Value.ToString(), out result4);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result3);
					decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result2);
					num += result3;
					num4 += result2;
					row.Cells["TaxTotal"].Value = result3 + result2;
				}
			}
			textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
			decimal.TryParse(textBoxDiscount.Text, out result);
			if (!isDataLoading && result > num)
			{
				result = default(decimal);
				textBoxDiscount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num2 = num - result;
			if (!checkBoxPriceIncludeTax.Checked)
			{
				num2 += num4;
			}
			textBoxTotal.Text = num2.ToString(Format.TotalAmountFormat);
			labelTotalQty.Text = num3.ToString(Format.QuantityFormat);
			labelRowCount.Text = posGrid1.DataGrid.Rows.Count.ToString(Format.QuantityFormat);
			CalculateTotalTaxes();
			textBoxTaxAmount.Text = num4.ToString(Format.TotalAmountFormat);
		}

		private void CalculateAllRowsTaxes()
		{
			try
			{
				foreach (UltraGridRow row in posGrid1.DataGrid.Rows)
				{
					ItemTaxOptions itemTaxOptions = ItemTaxOptions.BasedOnCustomer;
					if (row.Cells["TaxOption"].Value.ToString() != "")
					{
						itemTaxOptions = (ItemTaxOptions)byte.Parse(row.Cells["TaxOption"].Value.ToString());
					}
					if (itemTaxOptions == ItemTaxOptions.BasedOnCustomer)
					{
						row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
					}
					decimal amount = decimal.Parse(row.Cells["Amount"].Value.ToString());
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount = decimal.Parse(textBoxDiscount.Text);
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData((PayeeTaxOptions)checked((byte)int.Parse(labelPayeeTaxOption.Text)), comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = tag;
					UIGlobal.CalculateRowTax(row, "Tax", amount, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateTotalTaxes()
		{
			TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData((PayeeTaxOptions)checked((byte)int.Parse(labelPayeeTaxOption.Text)), comboBoxPayeeTaxGroup.SelectedID);
			DataTable taxDetailTable = taxTransactionData.TaxDetailTable;
			foreach (UltraGridRow row in posGrid1.DataGrid.Rows)
			{
				if (row.Cells["Tax"].Tag != null)
				{
					foreach (DataRow row2 in (row.Cells["Tax"].Tag as TaxTransactionData).TaxDetailTable.Rows)
					{
						string text = row2["TaxItemID"].ToString();
						decimal result = default(decimal);
						decimal.TryParse(row2["TaxAmount"].ToString(), out result);
						DataRow[] array = taxDetailTable.Select("TaxItemID  = '" + text + "'");
						if (array.Count() > 0)
						{
							decimal result2 = default(decimal);
							decimal.TryParse(array[0]["TaxAmount"].ToString(), out result2);
							result2 += result;
							array[0]["TaxAmount"] = result2;
						}
						else
						{
							DataRow dataRow = taxDetailTable.NewRow();
							dataRow["TaxItemID"] = text;
							dataRow["TaxAmount"] = result;
							taxDetailTable.Rows.Add(dataRow);
						}
					}
				}
			}
			textBoxTaxAmount.Tag = taxTransactionData;
		}

		private void buttonPayment_Click(object sender, EventArgs e)
		{
			if (posGrid1.DataGrid.Rows.Count == 0)
			{
				return;
			}
			if (ChangeSalespersonwhileSave)
			{
				ChangeSalespersonForm changeSalespersonForm = new ChangeSalespersonForm();
				changeSalespersonForm.SalespersonID = salesPersonID;
				if (changeSalespersonForm.ShowDialog() == DialogResult.OK)
				{
					labelSalesperson.Text = changeSalespersonForm.SalespersonID + "-" + changeSalespersonForm.SalespersonName;
					salesPersonID = changeSalespersonForm.SalespersonID;
				}
			}
			int activeBatchNumber = UIGlobal.GetActiveBatchNumber();
			int activeShiftNumber = UIGlobal.GetActiveShiftNumber();
			if (activeBatchNumber > 0)
			{
				labelBatchID.Text = activeBatchNumber.ToString();
			}
			else
			{
				labelBatchID.Text = "-";
			}
			if (activeShiftNumber > 0)
			{
				labelShift.Text = activeShiftNumber.ToString();
			}
			else
			{
				labelShift.Text = "-";
			}
			if (activeBatchNumber < 0)
			{
				ErrorHelper.ErrorMessage("There is no open batch to process this transaction.");
			}
			else if (activeShiftNumber < 0)
			{
				ErrorHelper.ErrorMessage("There is no open shift to process this transaction.");
			}
			else if (ValidateData() && GetData())
			{
				formPayment formPayment = new formPayment();
				formPayment.TotalDue = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
				formPayment.PaymentTable = currentData.PaymentTable;
				formPayment.IsEditable = isEditable;
				formPayment.IsNewRecord = isNewRecord;
				if (formPayment.ShowDialog(this) == DialogResult.OK)
				{
					currentData.SalesPOSTable.Rows[0]["Change"] = formPayment.Change;
					RefPaymentTable = formPayment.PaymentTable;
					SaveData();
				}
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
			else
			{
				labelSalespersonBack.Text = Security.DefaultSalespersonID;
				Security.IsAllowedSecurityRole(GeneralSecurityRoles.GiveDiscount);
			}
		}

		private void ClearForm()
		{
			try
			{
				allowEdit = true;
				textBoxScan.Clear();
				recalledID = "";
				IsNewRecord = true;
				IsVoid = false;
				isEditable = true;
				textBoxDiscount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxNote.Text = "";
				labelTotalQty.Text = 0.ToString(Format.QuantityFormat);
				labelRowCount.Text = 0.ToString(Format.QuantityFormat);
				labelCustomerCode.Text = Global.DefaultCustomerID;
				labelCustomerName.Text = Global.DefaultCustomerName;
				comboBoxPayeeTaxGroup.SelectedID = Factory.DatabaseSystem.GetFieldValue("Customer", "TaxGroupID", "CustomerID", Global.DefaultCustomerID).ToString();
				checkBoxReturnMode.Checked = false;
				checkBoxPriceIncludeTax.Checked = true;
				labelVoucherDate.Text = DateTime.Today.ToShortDateString();
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				(posGrid1.DataGrid.DataSource as DataTable)?.Clear();
				if (RefPaymentTable != null)
				{
					RefPaymentTable.Clear();
				}
				formManager.ResetDirty();
				labelVoucherNumber.Text = GetNextVoucherNumber();
				panelItemDetails.Visible = false;
				productPhotoViewer.Visible = false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private bool SaveData()
		{
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
			return SaveData(clearAfter: true);
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
			if (labelCustomerCode.Text == "")
			{
				ErrorHelper.InformationMessage("Please select a customer.");
				buttonCustomer_Click(null, null);
				return false;
			}
			if (posGrid1.DataGrid.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one row of item.");
				return false;
			}
			return true;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new SalesPOSData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalesPOSTable.Rows[0] : currentData.SalesPOSTable.NewRow();
				dataRow["TransactionDate"] = DateTime.Now;
				dataRow["SysDocID"] = sysDocID;
				dataRow["VoucherID"] = labelVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "DivisionID", "SysDocID", sysDocID);
				if (fieldValue != null)
				{
					dataRow["DivisionID"] = fieldValue.ToString();
				}
				else
				{
					dataRow["DivisionID"] = DBNull.Value;
				}
				dataRow["ShiftID"] = shiftID;
				dataRow["BatchID"] = batchID;
				dataRow["RegisterID"] = Global.CurrentCashRegisterID;
				dataRow["CustomerID"] = labelCustomerCode.Text;
				dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				dataRow["IsCash"] = true;
				dataRow["SalespersonID"] = salesPersonID;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Discount"] = textBoxDiscount.Text;
				dataRow["Total"] = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
				dataRow["TaxAmount"] = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
				if (comboBoxPayeeTaxGroup.SelectedID != "")
				{
					dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				}
				else
				{
					dataRow["PayeeTaxGroupID"] = DBNull.Value;
				}
				if (CompanyPreferences.IsTax)
				{
					dataRow["TaxOption"] = byte.Parse(labelPayeeTaxOption.Text);
					dataRow["PriceIncludeTax"] = checkBoxPriceIncludeTax.Checked;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
					dataRow["PriceIncludeTax"] = false;
				}
				if (!string.IsNullOrEmpty(searchValue))
				{
					dataRow["SearchValue"] = searchValue;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.SalesPOSTable.Rows.Add(dataRow);
				}
				currentData.SalesPOSDetailTable.Rows.Clear();
				foreach (UltraGridRow row in posGrid1.DataGrid.Rows)
				{
					DataRow dataRow2 = currentData.SalesPOSDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["ProductID"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					dataRow2["LocationID"] = Global.CurrentPOSLocationID;
					dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
					dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["TaxAmount"] = row.Cells["Tax"].Value.ToString();
					dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
					dataRow2["Barcode"] = row.Cells["Barcode"].Value.ToString();
					_ = Global.CurrentUserID;
					if (row.Cells["TaxOption"].Value != null && row.Cells["TaxOption"].Value.ToString() != string.Empty)
					{
						dataRow2["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow2["TaxOption"] = (byte)2;
					}
					if (row.Cells["Tax"].Value != null && row.Cells["Tax"].Value.ToString() != "")
					{
						dataRow2["TaxAmount"] = row.Cells["Tax"].Value.ToString();
					}
					else
					{
						dataRow2["TaxAmount"] = DBNull.Value;
					}
					if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow2["TaxGroupID"] = DBNull.Value;
					}
					if (row.Cells["Discount"].Value != null && row.Cells["Discount"].Value.ToString() != "")
					{
						dataRow2["Discount"] = row.Cells["Discount"].Value.ToString();
					}
					else
					{
						dataRow2["Discount"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.SalesPOSDetailTable.Rows.Add(dataRow2);
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string text = labelSysDocID.Text;
				string text2 = labelVoucherNumber.Text;
				int num = 0;
				foreach (UltraGridRow row2 in posGrid1.DataGrid.Rows)
				{
					if (row2.Cells["Tax"].Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, row2.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, text, text2, num, Global.DefaultCurrencySign, 1m);
					}
					num = checked(num + 1);
				}
				if (textBoxTaxAmount.Tag != null)
				{
					TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, text, text2, -1, Global.DefaultCurrencySign, 1m);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool DeleteHold(string id)
		{
			try
			{
				return Factory.POSHoldSystem.DeletePOSHold(recalledID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool SaveData(bool clearAfter)
		{
			try
			{
				bool flag = Factory.SalesPOSSystem.CreateSalesPOS(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					if (recalledID != "")
					{
						DeleteHold(recalledID);
						recalledID = "";
					}
					bool result = false;
					bool result2 = false;
					string text = labelSysDocID.Text;
					bool.TryParse(Factory.DatabaseSystem.GetFieldValue("System_Document", "DoPrint", "SysDocID", text).ToString(), out result);
					if (result)
					{
						bool.TryParse(Factory.DatabaseSystem.GetFieldValue("System_Document", "PrintAfterSave", "SysDocID", text).ToString(), out result2);
						if (result2)
						{
							Print(isPrint: true, showPrintDialog: true, saveChanges: false);
						}
						else
						{
							Print(isPrint: false, showPrintDialog: true, saveChanges: false);
						}
					}
					if (clearAfter)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						formManager.ResetDirty();
					}
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1032)
				{
					ErrorHelper.ErrorMessage("You have entered discount for this transaction but discount account is not set for the Register. Please assign a discount account for the Register or Location.");
				}
				else if (ex.Number == 1033)
				{
					ErrorHelper.ErrorMessage("Sales account is not set for product or location. Please assign a Sales Account for products or Location.");
				}
				else
				{
					if (ex.Number == 1046)
					{
						string nextVoucherNumber = GetNextVoucherNumber();
						if (nextVoucherNumber == labelVoucherNumber.Text)
						{
							ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
							return false;
						}
						if (nextVoucherNumber != "")
						{
							labelVoucherNumber.Text = nextVoucherNumber;
						}
						formManager.SetControlDirtyStatus(labelVoucherNumber, labelVoucherNumber.Text);
						GetData();
						DataRow dataRow = currentData.PaymentTable.NewRow();
						for (int i = 0; i < RefPaymentTable.Columns.Count; i = checked(i + 1))
						{
							dataRow[RefPaymentTable.Columns[i].ColumnName] = RefPaymentTable.Rows[0][i];
						}
						currentData.PaymentTable.Rows.Add(dataRow);
						return SaveData();
					}
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonCustomer_Click(object sender, EventArgs e)
		{
			ItemLookupDialog itemLookupDialog = new ItemLookupDialog();
			itemLookupDialog.LookupType = ItemLookupDialog.LookupItemTypes.Customer;
			if (itemLookupDialog.ShowDialog() == DialogResult.OK)
			{
				labelCustomerCode.Text = itemLookupDialog.SelectedRow.Cells["Customer Code"].Value.ToString();
				labelCustomerName.Text = itemLookupDialog.SelectedRow.Cells["Customer Name"].Value.ToString();
				if (CompanyPreferences.IsTax)
				{
					comboBoxPayeeTaxGroup.Clear();
					if (checked((byte)int.Parse(itemLookupDialog.SelectedRow.Cells["TaxOption"].Value.ToString())) == 1)
					{
						comboBoxPayeeTaxGroup.SelectedID = itemLookupDialog.SelectedRow.Cells["TaxGroupID"].Value.ToString();
					}
					labelPayeeTaxOption.Text = itemLookupDialog.SelectedRow.Cells["TaxOption"].Value.ToString();
				}
				else
				{
					comboBoxPayeeTaxGroup.Clear();
				}
			}
			textBoxScan.Focus();
		}

		private void buttonItemLookup_Click(object sender, EventArgs e)
		{
			ItemLookupDialog itemLookupDialog = new ItemLookupDialog();
			itemLookupDialog.LookupType = ItemLookupDialog.LookupItemTypes.Product;
			if (itemLookupDialog.ShowDialog() == DialogResult.OK)
			{
				ScanItem(itemLookupDialog.SelectedRow.Cells["Code"].Value.ToString());
			}
		}

		private void buttonDiscount_Click(object sender, EventArgs e)
		{
			DiscountForm discountForm = new DiscountForm();
			discountForm.Subtotal = textBoxSubtotal.Text;
			discountForm.Total = textBoxTotal.Text;
			discountForm.DiscountAmount = textBoxDiscount.Text;
			if (discountForm.ShowDialog(this) == DialogResult.OK)
			{
				textBoxDiscount.Text = decimal.Parse(discountForm.DiscountAmount).ToString(Format.TotalAmountFormat);
				CalculateTotal();
			}
			textBoxScan.Focus();
		}

		private void buttonTotal_Click(object sender, EventArgs e)
		{
			SetTotalForm setTotalForm = new SetTotalForm();
			setTotalForm.Subtotal = decimal.Parse(textBoxSubtotal.Text);
			setTotalForm.Total = decimal.Parse(textBoxTotal.Text);
			if (setTotalForm.ShowDialog() == DialogResult.OK)
			{
				decimal total = setTotalForm.Total;
				decimal num = decimal.Parse(textBoxSubtotal.Text) - total;
				textBoxDiscount.Text = num.ToString(Format.TotalAmountFormat);
				CalculateTotal();
			}
			textBoxScan.Focus();
		}

		private bool HoldCurrentTransaction()
		{
			if (!IsNewRecord)
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				if (recalledID != "")
				{
					Factory.POSHoldSystem.DeletePOSHold(recalledID);
					recalledID = "";
				}
				bool flag = Factory.POSHoldSystem.HoldSalesReceipt(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					ClearForm();
					IsNewRecord = true;
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1046)
				{
					string nextVoucherNumber = GetNextVoucherNumber();
					if (nextVoucherNumber == labelVoucherNumber.Text)
					{
						ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
						return false;
					}
					if (nextVoucherNumber != "")
					{
						labelVoucherNumber.Text = nextVoucherNumber;
					}
					formManager.SetControlDirtyStatus(labelVoucherNumber, labelVoucherNumber.Text);
					return SaveData();
				}
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void barButtonItemHold_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (posGrid1.DataGrid.DisplayLayout.Rows.Count == 0)
			{
				return;
			}
			POSNameDialog pOSNameDialog = new POSNameDialog();
			pOSNameDialog.Text = "Hold Receipt";
			pOSNameDialog.SysDocID = labelSysDocID.Text;
			pOSNameDialog.VoucherID = labelVoucherNumber.Text;
			if (pOSNameDialog.ShowDialog() == DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(pOSNameDialog.EnteredName))
				{
					searchValue = pOSNameDialog.EnteredName;
				}
				HoldCurrentTransaction();
			}
		}

		private void Recall()
		{
			ItemLookupDialog itemLookupDialog = new ItemLookupDialog();
			itemLookupDialog.LookupType = ItemLookupDialog.LookupItemTypes.HoldReceipt;
			if (itemLookupDialog.ShowDialog() == DialogResult.OK)
			{
				string selectedCode = itemLookupDialog.GetSelectedCode("Num");
				LoadRecalledReceipt(selectedCode);
			}
		}

		private void LoadRecalledReceipt(string id)
		{
			try
			{
				POSHoldData pOSHoldByID = Factory.POSHoldSystem.GetPOSHoldByID("", id);
				ClearForm();
				bool result = false;
				DataRow dataRow = pOSHoldByID.POSHoldTable.Rows[0];
				textBoxNote.Text = dataRow["Note"].ToString();
				labelCustomerCode.Text = dataRow["CustomerID"].ToString();
				comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
				labelPayeeTaxOption.Text = dataRow["TaxOption"].ToString();
				bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
				checkBoxPriceIncludeTax.Checked = result;
				DataTable dataTable = posGrid1.DataGrid.DataSource as DataTable;
				foreach (DataRow row in pOSHoldByID.POSHoldDetailTable.Rows)
				{
					DataRow dataRow3 = dataTable.Rows.Add();
					dataRow3["ProductID"] = row["ProductID"];
					if (row["UPC"] == DBNull.Value || row["UPC"].ToString() == "")
					{
						dataRow3["UPC"] = row["ProductID"];
					}
					else
					{
						dataRow3["UPC"] = row["UPC"];
					}
					dataRow3["Description"] = row["Description"];
					dataRow3["Price"] = row["UnitPrice"];
					dataRow3["Quantity"] = row["Quantity"];
					dataRow3["Discount"] = row["Discount"];
					dataRow3["Tax"] = row["TaxAmount"];
					dataRow3["TaxOption"] = row["TaxOption"];
					dataRow3["TaxGroupID"] = row["TaxGroupID"];
					decimal d = decimal.Parse(dataRow3["Quantity"].ToString());
					decimal d2 = decimal.Parse(dataRow3["Price"].ToString());
					decimal result2 = default(decimal);
					decimal.TryParse(dataRow3["Discount"].ToString(), out result2);
					decimal num = d * (d2 - result2);
					dataRow3["Amount"] = num;
				}
				CalculateAllRowsTaxes();
				CalculateTotal();
				recalledID = id;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void barButtonItemRecall_ItemClick(object sender, ItemClickEventArgs e)
		{
			Recall();
		}

		private void barButtonCheckItemPrice_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (posGrid1.DataGrid.ActiveRow != null)
			{
				CheckPriceForm checkPriceForm = new CheckPriceForm();
				checkPriceForm.SelectedItem = posGrid1.DataGrid.ActiveRow.Cells["ProductID"].Value.ToString();
				checkPriceForm.ShowDialog(this);
				textBoxScan.Focus();
			}
		}

		private void barButtonCheckItemQuantity_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (posGrid1.DataGrid.ActiveRow != null)
			{
				CheckQuantityForm checkQuantityForm = new CheckQuantityForm();
				checkQuantityForm.SelectedItem = posGrid1.DataGrid.ActiveRow.Cells["ProductID"].Value.ToString();
				checkQuantityForm.ShowDialog(this);
				textBoxScan.Focus();
			}
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			if (!(recalledID != "") || ErrorHelper.QuestionMessageYesNo("Receipt was loaded from hold.", "Do you want to remove the receipt from Hold also?") != DialogResult.Yes || DeleteHold(recalledID))
			{
				ClearForm();
			}
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			ClearForm();
		}

		private void buttonTasks_Click(object sender, EventArgs e)
		{
			popupMenuTasks.ShowPopup(buttonTasks.PointToScreen(new Point(0, 0)));
		}

		private void barButtonItemXReport_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				EndOfDayReportForm endOfDayReportForm = new EndOfDayReportForm();
				endOfDayReportForm.DefaultReportType = 1;
				endOfDayReportForm.RegisterID = labelCashRegisterID.Text;
				endOfDayReportForm.BatchID = UIGlobal.GetActiveBatchNumber();
				endOfDayReportForm.ShiftID = UIGlobal.GetActiveShiftNumber();
				ReportHelper reportHelper = new ReportHelper();
				if (endOfDayReportForm.ShowDialog() == DialogResult.OK)
				{
					reportHelper.ShowReport(endOfDayReportForm.Report);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barButtonItemYReport_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				EndOfDayReportForm endOfDayReportForm = new EndOfDayReportForm();
				endOfDayReportForm.DefaultReportType = 2;
				endOfDayReportForm.RegisterID = labelCashRegisterID.Text;
				endOfDayReportForm.BatchID = UIGlobal.GetActiveBatchNumber();
				endOfDayReportForm.ShiftID = UIGlobal.GetActiveShiftNumber();
				if (endOfDayReportForm.ShowDialog() == DialogResult.OK)
				{
					EnterAvailableCashForm enterAvailableCashForm = new EnterAvailableCashForm();
					enterAvailableCashForm.IsEndingBalance = true;
					if (enterAvailableCashForm.ShowDialog() == DialogResult.OK)
					{
						decimal availableCashAmount = enterAvailableCashForm.AvailableCashAmount;
						ReportHelper reportHelper = new ReportHelper();
						if (Factory.POSShiftSystem.ClosePOSShift(batchID, shiftID, labelCashRegisterID.Text, availableCashAmount))
						{
							labelShift.Text = "Closed";
							reportHelper.ShowReport(endOfDayReportForm.Report);
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barButtonItemZReport_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				EndOfDayReportForm endOfDayReportForm = new EndOfDayReportForm();
				endOfDayReportForm.DefaultReportType = 3;
				endOfDayReportForm.RegisterID = labelCashRegisterID.Text;
				endOfDayReportForm.BatchID = UIGlobal.GetActiveBatchNumber();
				endOfDayReportForm.ShiftID = UIGlobal.GetActiveShiftNumber();
				if (endOfDayReportForm.ShowDialog() == DialogResult.OK && ErrorHelper.QuestionMessageYesNo("Z-Report will close the active batch and close the day. All shifts must be closed before proceeding.\nAre you sure to proceed with Z-Report and close the day?") == DialogResult.Yes)
				{
					ReportHelper reportHelper = new ReportHelper();
					if (Factory.POSBatchSystem.ClosePOSBatch(batchID, Global.CurrentPOSLocationID))
					{
						labelBatchID.Text = "Closed";
						reportHelper.ShowReport(endOfDayReportForm.Report);
					}
				}
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 2003)
				{
					ErrorHelper.ErrorMessage("All open shifts must be closed before closing the batch.", "One or more Cash Registers require to run Y-Report to close the shift.", "Close all shifts and try again.");
				}
				else if (ex.Number == 2004)
				{
					ErrorHelper.ErrorMessage("This batch number is already closed.");
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxReturnMode_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxReturnMode.Checked)
			{
				labelQuantity.BackColor = Color.FromArgb(255, 192, 192);
			}
			else
			{
				labelQuantity.BackColor = Color.Transparent;
			}
			textBoxScan.Focus();
		}

		private void barButtonClose_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (ErrorHelper.WarningMessageOkCancel("Are you sure you want to close the POS screen?") == DialogResult.OK && CanClose())
			{
				Close();
			}
		}

		private void menuItemStartNewShift_ItemClick(object sender, ItemClickEventArgs e)
		{
			StartNewShift();
		}

		private bool StartNewShift()
		{
			try
			{
				if (UIGlobal.GetActiveShiftNumber() > 0)
				{
					ErrorHelper.InformationMessage("There is already an open shift for this register.");
					labelShift.Text = UIGlobal.GetActiveShiftNumber().ToString();
					return false;
				}
				EnterAvailableCashForm enterAvailableCashForm = new EnterAvailableCashForm();
				if (enterAvailableCashForm.ShowDialog() == DialogResult.OK)
				{
					shiftID = UIGlobal.CreateNewShiftNumber(enterAvailableCashForm.AvailableCashAmount);
					batchID = UIGlobal.GetActiveBatchNumber();
					labelShift.Text = shiftID.ToString();
					labelBatchID.Text = batchID.ToString();
					isEditable = true;
					return true;
				}
				return false;
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

		private void barButtonItemChangeSalesperson_ItemClick(object sender, ItemClickEventArgs e)
		{
			labelSalesperson_LinkClicked(sender, null);
		}

		private void buttonCustomers_Click(object sender, EventArgs e)
		{
			popupMenuCustomers.ShowPopup(buttonCustomers.PointToScreen(new Point(0, 0)));
		}

		private void labelSalesperson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ChangeSalespersonForm changeSalespersonForm = new ChangeSalespersonForm();
			changeSalespersonForm.SalespersonID = salesPersonID;
			if (changeSalespersonForm.ShowDialog() == DialogResult.OK)
			{
				labelSalesperson.Text = changeSalespersonForm.SalespersonID + "-" + changeSalespersonForm.SalespersonName;
				salesPersonID = changeSalespersonForm.SalespersonID;
			}
		}

		private void barButtonItemReprintReceipt_ItemClick(object sender, ItemClickEventArgs e)
		{
			switch (new PrintDialogForm().ShowDialog())
			{
			case DialogResult.OK:
				Print(isPrint: false, showPrintDialog: true, saveChanges: true);
				break;
			case DialogResult.Yes:
				Print(isPrint: true, showPrintDialog: false, saveChanges: true);
				break;
			}
		}

		private void Print()
		{
			Print(isPrint: false, showPrintDialog: true, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					DataSet salesPOSToPrint = Factory.SalesPOSSystem.GetSalesPOSToPrint(sysDocID, labelVoucherNumber.Text);
					if (salesPOSToPrint == null || salesPOSToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(salesPOSToPrint, sysDocID, "Sales POS", SysDocTypes.SalesPOS, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void checkBoxPriceIncludeTax_CheckedChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
			}
			if (checkBoxPriceIncludeTax.Checked)
			{
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = true;
			}
			else
			{
				posGrid1.DataGrid.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = false;
			}
		}

		private void barButtonItemVoidReceipt_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!isNewRecord && ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No)
			{
				if (Void(isVoid: true))
				{
					IsVoid = true;
				}
				else
				{
					ErrorHelper.ErrorMessage("Unable to void the transaction.");
				}
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				return Factory.SalesPOSSystem.VoidSalesPOS(SystemDocID, labelVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void formManager_Click(object sender, EventArgs e)
		{
		}

		private void barButtonCashPayments_ItemClick(object sender, ItemClickEventArgs e)
		{
			FormActivator.SelectedSysDocID = ExpenseDocID;
			FormActivator.BringFormToFront(FormActivator.ExpenseListFormObj);
		}

		private void barButtonItemReturnMode_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void linkLabelShowHideCost_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (canAccessCost && linkLabelShowHideCost.Text == "Show")
			{
				Label label = labelAverageCost;
				bool visible = labelAVGCost.Visible = true;
				label.Visible = visible;
				Label label2 = labelStdCost;
				Label label3 = labelStdrdCost;
				Label label4 = labelLastCost;
				bool flag3 = labelLSTCost.Visible = true;
				bool flag5 = label4.Visible = flag3;
				visible = (label3.Visible = flag5);
				label2.Visible = visible;
			}
			else if (linkLabelShowHideCost.Text == "Hide")
			{
				Label label5 = labelAverageCost;
				bool visible = labelAVGCost.Visible = false;
				label5.Visible = visible;
				Label label6 = labelStdCost;
				Label label7 = labelStdrdCost;
				Label label8 = labelLastCost;
				bool flag3 = labelLSTCost.Visible = false;
				bool flag5 = label8.Visible = flag3;
				visible = (label7.Visible = flag5);
				label6.Visible = visible;
			}
			if (linkLabelShowHideCost.Text == "Show")
			{
				linkLabelShowHideCost.Text = "Hide";
			}
			else if (linkLabelShowHideCost.Text == "Hide")
			{
				linkLabelShowHideCost.Text = "Show";
			}
		}

		private void barButtonItemPettyCash_ItemClick(object sender, ItemClickEventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CashExpenseEntryFormObj);
		}

		private decimal GetItemDetails(string UPC)
		{
			decimal num = default(decimal);
			try
			{
				string text = UPC.Substring(6, 6);
				return decimal.Parse(text.Substring(0, 4) + "." + text.Substring(4, 2));
			}
			catch
			{
				return default(decimal);
			}
		}
	}
}
