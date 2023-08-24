using DevExpress.Utils;
using DevExpress.XtraCharts;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using Micromind.UISupport.DataGrid;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class InventoryPurchasesStatisticForm : Form, IDataForm, IDataList, IExternalReport
	{
		private bool isFirstTimeActivating = true;

		private bool canAccessCost;

		private bool showVendor;

		public string parameter1 = "";

		private Hashtable listViewKeys = new Hashtable();

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet listData;

		private XPButton buttonDone;

		private Panel panelButtons;

		private ContextMenu contextMenuPopup;

		private MenuItem menuItemGotoAccount;

		private MenuItem menuItem2;

		private MenuItem menuItemNew;

		private MenuItem menuItemDelete;

		private MenuItem menuItem1;

		private MenuItem menuItemMarkAs;

		private MenuItem menuItemInactive;

		private ContextMenu contextMenu2;

		private MenuItem menuItemFlat;

		private MenuItem menuItemHierarchical;

		private MenuItem menuItem3;

		private MenuItem menuItemShowInactiveItems;

		private Line linePanelDown;

		private IContainer components;

		private DataGridList dataGridDetail;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonAllowGrouping;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private DateControl dateControl1;

		private PictureBox pictureBoxVoid;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonMerge;

		private TextBox textBoxCode;

		private Label label2;

		private RadioButton radioButtonCode;

		private RadioButton radioButtonItem;

		private ProductComboBox comboBoxItem;

		private TextBox textBoxDescription;

		private UltraTabControl tabControlTab;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private Panel panel1;

		private UltraTabPageControl tabPageSalesperson;

		private SplitContainer splitContainer1;

		private DataGridList dataGridSalesperson;

		private DevXChart devXChartSalesperson;

		private Label label6;

		private TextBox textBoxMargin;

		private Label label7;

		private TextBox textBoxProfit;

		private Label label4;

		private TextBox textBoxAvgCost;

		private Label label5;

		private TextBox textBoxAvgPrice;

		private Label label3;

		private TextBox textBoxTotalAmount;

		private Label label1;

		private TextBox textBoxTotalQty;

		private Panel panelCOGS;

		private CheckBox checkBoxShowCost;

		private UltraTabPageControl ultraTabControl;

		private SplitContainer splitContainer2;

		private DataGridList dataGridItems;

		private DevXChart devXChartItems;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private SplitContainer splitContainer3;

		private DataGridList dataGridCategory;

		private DevXChart devXChartCategory;

		private SplitContainer splitContainer4;

		private DataGridList dataGridCustomer;

		private DevXChart devXChartCustomer;

		private UltraTabPageControl ultraTabPageControl3;

		private DevXChart chartControlPrice;

		private Panel panel2;

		private CheckBox checkBoxShowVolume;

		private CheckBox checkBoxShowCOGS;

		private UltraTabPageControl ultraTabPageControl4;

		private DataGridList dataGridPODetail;

		private UltraTabPageControl ultraTabPageControl5;

		private DataGridList dataGridGRNDetail;

		private ToolStrip toolStrip1;

		private vendorsFlatComboBox comboBoxVendor;

		private TextBox textBoxVendor;

		private RadioButton radioButtonVendor;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private ScreenAccessRight screenRight;

		public string SelectedID
		{
			get
			{
				if (radioButtonItem.Checked)
				{
					return comboBoxItem.SelectedID;
				}
				return textBoxCode.Text;
			}
			set
			{
				if (radioButtonItem.Checked)
				{
					comboBoxItem.SelectedID = value;
				}
				else
				{
					textBoxCode.Text = value;
				}
			}
		}

		public bool ShowVendor
		{
			get
			{
				return showVendor;
			}
			set
			{
				showVendor = value;
			}
		}

		public string SelectedCode
		{
			get
			{
				return comboBoxVendor.SelectedID;
			}
			set
			{
				comboBoxVendor.SelectedID = value;
			}
		}

		private bool ShowCost
		{
			get
			{
				if (!checkBoxShowCost.Checked || !canAccessCost)
				{
					return false;
				}
				return true;
			}
		}

		private bool ShowInactiveItems
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public InventoryPurchasesStatisticForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridDetail.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridDetail.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridDetail.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridDetail.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			dataGridDetail.ShowDeleteMenu = false;
			dataGridDetail.ShowNewMenu = false;
			base.FormClosing += InventoryPurchasesStatisticForm_FormClosing;
			dataGridDetail.AllowUnfittedView = true;
		}

		private void InventoryPurchasesStatisticForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (listData != null)
			{
				SaveUserSettings();
				Global.GlobalSettings.SaveFormProperties(this);
			}
		}

		private void SaveUserSettings()
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
				UserPreferences.SaveCurrentUserSetting(base.Name + "Period", (int)dateControl1.SelectedPeriod);
				dataGridDetail.SaveLayout();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadUserSettings()
		{
			try
			{
				toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
				dateControl1.SelectedPeriod = (DatePeriods)UserPreferences.GetCurrentUserSetting(base.Name + "Period", 2);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			OpenForEdit();
		}

		private void CompanyAccountsListForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTimeActivating)
			{
				isFirstTimeActivating = false;
				dataGridDetail.AutoFitColumns();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && listData != null)
			{
				listData.Dispose();
				listData = null;
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
			DevExpress.XtraCharts.XYDiagram xYDiagram = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
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
			DevExpress.XtraCharts.XYDiagram xYDiagram2 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel5 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel6 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			DevExpress.XtraCharts.XYDiagram xYDiagram3 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel7 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel8 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel9 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			DevExpress.XtraCharts.XYDiagram xYDiagram4 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel10 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel11 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel12 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.XYDiagram xYDiagram5 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.XYDiagramPane xYDiagramPane = new DevExpress.XtraCharts.XYDiagramPane();
			DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY = new DevExpress.XtraCharts.SecondaryAxisY();
			DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel13 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView = new DevExpress.XtraCharts.SideBySideBarSeriesView();
			DevExpress.XtraCharts.Series series10 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel = new DevExpress.XtraCharts.PointSeriesLabel();
			DevExpress.XtraCharts.SplineSeriesView splineSeriesView = new DevExpress.XtraCharts.SplineSeriesView();
			DevExpress.XtraCharts.Series series11 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
			DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
			DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
			DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.InventoryPurchasesStatisticForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panel1 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			textBoxAvgPrice = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxTotalAmount = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxTotalQty = new System.Windows.Forms.TextBox();
			panelCOGS = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			textBoxMargin = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			textBoxProfit = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxAvgCost = new System.Windows.Forms.TextBox();
			dataGridDetail = new Micromind.UISupport.DataGridList(components);
			tabPageSalesperson = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			dataGridSalesperson = new Micromind.UISupport.DataGridList(components);
			devXChartSalesperson = new Micromind.UISupport.DataGrid.DevXChart(components);
			ultraTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			splitContainer2 = new System.Windows.Forms.SplitContainer();
			dataGridItems = new Micromind.UISupport.DataGridList(components);
			devXChartItems = new Micromind.UISupport.DataGrid.DevXChart(components);
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			splitContainer3 = new System.Windows.Forms.SplitContainer();
			dataGridCategory = new Micromind.UISupport.DataGridList(components);
			devXChartCategory = new Micromind.UISupport.DataGrid.DevXChart(components);
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			splitContainer4 = new System.Windows.Forms.SplitContainer();
			dataGridCustomer = new Micromind.UISupport.DataGridList(components);
			devXChartCustomer = new Micromind.UISupport.DataGrid.DevXChart(components);
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			chartControlPrice = new Micromind.UISupport.DataGrid.DevXChart(components);
			panel2 = new System.Windows.Forms.Panel();
			checkBoxShowVolume = new System.Windows.Forms.CheckBox();
			checkBoxShowCOGS = new System.Windows.Forms.CheckBox();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridPODetail = new Micromind.UISupport.DataGridList(components);
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridGRNDetail = new Micromind.UISupport.DataGridList(components);
			panelButtons = new System.Windows.Forms.Panel();
			checkBoxShowCost = new System.Windows.Forms.CheckBox();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDone = new Micromind.UISupport.XPButton();
			contextMenuPopup = new System.Windows.Forms.ContextMenu();
			menuItemGotoAccount = new System.Windows.Forms.MenuItem();
			menuItem2 = new System.Windows.Forms.MenuItem();
			menuItemNew = new System.Windows.Forms.MenuItem();
			menuItemDelete = new System.Windows.Forms.MenuItem();
			menuItem1 = new System.Windows.Forms.MenuItem();
			menuItemMarkAs = new System.Windows.Forms.MenuItem();
			menuItemInactive = new System.Windows.Forms.MenuItem();
			contextMenu2 = new System.Windows.Forms.ContextMenu();
			menuItemFlat = new System.Windows.Forms.MenuItem();
			menuItemHierarchical = new System.Windows.Forms.MenuItem();
			menuItem3 = new System.Windows.Forms.MenuItem();
			menuItemShowInactiveItems = new System.Windows.Forms.MenuItem();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			pictureBoxVoid = new System.Windows.Forms.PictureBox();
			textBoxCode = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			radioButtonCode = new System.Windows.Forms.RadioButton();
			radioButtonItem = new System.Windows.Forms.RadioButton();
			textBoxDescription = new System.Windows.Forms.TextBox();
			tabControlTab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			comboBoxItem = new Micromind.DataControls.ProductComboBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			textBoxVendor = new System.Windows.Forms.TextBox();
			radioButtonVendor = new System.Windows.Forms.RadioButton();
			tabPageGeneral.SuspendLayout();
			panel1.SuspendLayout();
			panelCOGS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDetail).BeginInit();
			tabPageSalesperson.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)devXChartSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).BeginInit();
			((System.ComponentModel.ISupportInitialize)series).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).BeginInit();
			((System.ComponentModel.ISupportInitialize)series2).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel3).BeginInit();
			ultraTabControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
			splitContainer2.Panel1.SuspendLayout();
			splitContainer2.Panel2.SuspendLayout();
			splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)devXChartItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram2).BeginInit();
			((System.ComponentModel.ISupportInitialize)series3).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel4).BeginInit();
			((System.ComponentModel.ISupportInitialize)series4).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel5).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel6).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
			splitContainer3.Panel1.SuspendLayout();
			splitContainer3.Panel2.SuspendLayout();
			splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)devXChartCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram3).BeginInit();
			((System.ComponentModel.ISupportInitialize)series5).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel7).BeginInit();
			((System.ComponentModel.ISupportInitialize)series6).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel8).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel9).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
			splitContainer4.Panel1.SuspendLayout();
			splitContainer4.Panel2.SuspendLayout();
			splitContainer4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)devXChartCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram4).BeginInit();
			((System.ComponentModel.ISupportInitialize)series7).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel10).BeginInit();
			((System.ComponentModel.ISupportInitialize)series8).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel11).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel12).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)chartControlPrice).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram5).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagramPane).BeginInit();
			((System.ComponentModel.ISupportInitialize)secondaryAxisY).BeginInit();
			((System.ComponentModel.ISupportInitialize)series9).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel13).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesView).BeginInit();
			((System.ComponentModel.ISupportInitialize)series10).BeginInit();
			((System.ComponentModel.ISupportInitialize)pointSeriesLabel).BeginInit();
			((System.ComponentModel.ISupportInitialize)splineSeriesView).BeginInit();
			((System.ComponentModel.ISupportInitialize)series11).BeginInit();
			((System.ComponentModel.ISupportInitialize)pointSeriesLabel2).BeginInit();
			((System.ComponentModel.ISupportInitialize)splineSeriesView2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pointSeriesLabel3).BeginInit();
			((System.ComponentModel.ISupportInitialize)splineSeriesView3).BeginInit();
			panel2.SuspendLayout();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPODetail).BeginInit();
			ultraTabPageControl5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridGRNDetail).BeginInit();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).BeginInit();
			((System.ComponentModel.ISupportInitialize)tabControlTab).BeginInit();
			tabControlTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(panel1);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(805, 345);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxAvgPrice);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(textBoxTotalAmount);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(textBoxTotalQty);
			panel1.Controls.Add(panelCOGS);
			panel1.Controls.Add(dataGridDetail);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(805, 345);
			panel1.TabIndex = 7;
			label5.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			label5.Location = new System.Drawing.Point(203, 299);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(97, 19);
			label5.TabIndex = 299;
			label5.Text = "Avg Price";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textBoxAvgPrice.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxAvgPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxAvgPrice.Location = new System.Drawing.Point(203, 317);
			textBoxAvgPrice.Name = "textBoxAvgPrice";
			textBoxAvgPrice.ReadOnly = true;
			textBoxAvgPrice.Size = new System.Drawing.Size(97, 20);
			textBoxAvgPrice.TabIndex = 298;
			textBoxAvgPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			label3.Location = new System.Drawing.Point(104, 299);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(100, 19);
			label3.TabIndex = 297;
			label3.Text = "Total Amount";
			label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textBoxTotalAmount.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxTotalAmount.Location = new System.Drawing.Point(104, 317);
			textBoxTotalAmount.Name = "textBoxTotalAmount";
			textBoxTotalAmount.ReadOnly = true;
			textBoxTotalAmount.Size = new System.Drawing.Size(100, 20);
			textBoxTotalAmount.TabIndex = 296;
			textBoxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			label1.Location = new System.Drawing.Point(8, 299);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(97, 19);
			label1.TabIndex = 295;
			label1.Text = "Total Qty";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textBoxTotalQty.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxTotalQty.Location = new System.Drawing.Point(8, 317);
			textBoxTotalQty.Name = "textBoxTotalQty";
			textBoxTotalQty.ReadOnly = true;
			textBoxTotalQty.Size = new System.Drawing.Size(97, 20);
			textBoxTotalQty.TabIndex = 4;
			textBoxTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			panelCOGS.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panelCOGS.Controls.Add(label6);
			panelCOGS.Controls.Add(textBoxMargin);
			panelCOGS.Controls.Add(label7);
			panelCOGS.Controls.Add(textBoxProfit);
			panelCOGS.Controls.Add(label4);
			panelCOGS.Controls.Add(textBoxAvgCost);
			panelCOGS.Location = new System.Drawing.Point(290, 295);
			panelCOGS.Name = "panelCOGS";
			panelCOGS.Size = new System.Drawing.Size(323, 45);
			panelCOGS.TabIndex = 306;
			label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			label6.Location = new System.Drawing.Point(204, 4);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(100, 19);
			label6.TabIndex = 305;
			label6.Text = "Margin %";
			label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textBoxMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxMargin.Location = new System.Drawing.Point(204, 22);
			textBoxMargin.Name = "textBoxMargin";
			textBoxMargin.ReadOnly = true;
			textBoxMargin.Size = new System.Drawing.Size(100, 20);
			textBoxMargin.TabIndex = 304;
			textBoxMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			label7.Location = new System.Drawing.Point(108, 4);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(97, 19);
			label7.TabIndex = 303;
			label7.Text = "Profit";
			label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textBoxProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxProfit.Location = new System.Drawing.Point(108, 22);
			textBoxProfit.Name = "textBoxProfit";
			textBoxProfit.ReadOnly = true;
			textBoxProfit.Size = new System.Drawing.Size(97, 20);
			textBoxProfit.TabIndex = 302;
			textBoxProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			label4.Location = new System.Drawing.Point(9, 4);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(100, 19);
			label4.TabIndex = 301;
			label4.Text = "Avg Cost";
			label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textBoxAvgCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxAvgCost.Location = new System.Drawing.Point(9, 22);
			textBoxAvgCost.Name = "textBoxAvgCost";
			textBoxAvgCost.ReadOnly = true;
			textBoxAvgCost.Size = new System.Drawing.Size(100, 20);
			textBoxAvgCost.TabIndex = 300;
			textBoxAvgCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			dataGridDetail.AllowUnfittedView = false;
			dataGridDetail.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDetail.DisplayLayout.Appearance = appearance;
			dataGridDetail.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDetail.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDetail.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDetail.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridDetail.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDetail.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridDetail.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDetail.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDetail.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDetail.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridDetail.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDetail.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridDetail.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDetail.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridDetail.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDetail.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDetail.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridDetail.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridDetail.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDetail.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridDetail.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridDetail.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDetail.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridDetail.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDetail.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDetail.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDetail.LoadLayoutFailed = false;
			dataGridDetail.Location = new System.Drawing.Point(6, 4);
			dataGridDetail.Name = "dataGridDetail";
			dataGridDetail.ShowDeleteMenu = false;
			dataGridDetail.ShowMinusInRed = true;
			dataGridDetail.ShowNewMenu = false;
			dataGridDetail.Size = new System.Drawing.Size(796, 290);
			dataGridDetail.TabIndex = 3;
			dataGridDetail.Text = "dataGridList1";
			tabPageSalesperson.Controls.Add(splitContainer1);
			tabPageSalesperson.Location = new System.Drawing.Point(-10000, -10000);
			tabPageSalesperson.Name = "tabPageSalesperson";
			tabPageSalesperson.Size = new System.Drawing.Size(805, 345);
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainer1.Panel1.Controls.Add(dataGridSalesperson);
			splitContainer1.Panel2.Controls.Add(devXChartSalesperson);
			splitContainer1.Size = new System.Drawing.Size(805, 345);
			splitContainer1.SplitterDistance = 201;
			splitContainer1.TabIndex = 298;
			dataGridSalesperson.AllowUnfittedView = false;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSalesperson.DisplayLayout.Appearance = appearance13;
			dataGridSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSalesperson.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSalesperson.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridSalesperson.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridSalesperson.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSalesperson.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridSalesperson.LoadLayoutFailed = false;
			dataGridSalesperson.Location = new System.Drawing.Point(0, 0);
			dataGridSalesperson.Name = "dataGridSalesperson";
			dataGridSalesperson.ShowDeleteMenu = false;
			dataGridSalesperson.ShowMinusInRed = true;
			dataGridSalesperson.ShowNewMenu = false;
			dataGridSalesperson.Size = new System.Drawing.Size(805, 201);
			dataGridSalesperson.TabIndex = 4;
			dataGridSalesperson.Text = "dataGridList1";
			devXChartSalesperson.CrossHairPattern = null;
			xYDiagram.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram.AxisY.VisibleInPanesSerializable = "-1";
			devXChartSalesperson.Diagram = xYDiagram;
			devXChartSalesperson.Dock = System.Windows.Forms.DockStyle.Fill;
			devXChartSalesperson.Legend.Name = "Default Legend";
			devXChartSalesperson.Location = new System.Drawing.Point(0, 0);
			devXChartSalesperson.Name = "devXChartSalesperson";
			sideBySideBarSeriesLabel.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series.Label = sideBySideBarSeriesLabel;
			series.Name = "Series 1";
			sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series2.Label = sideBySideBarSeriesLabel2;
			series2.Name = "Series 2";
			devXChartSalesperson.SeriesSerializable = new DevExpress.XtraCharts.Series[2]
			{
				series,
				series2
			};
			sideBySideBarSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			devXChartSalesperson.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
			devXChartSalesperson.Size = new System.Drawing.Size(805, 140);
			devXChartSalesperson.TabIndex = 0;
			ultraTabControl.Controls.Add(splitContainer2);
			ultraTabControl.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabControl.Name = "ultraTabControl";
			ultraTabControl.Size = new System.Drawing.Size(805, 345);
			splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer2.Location = new System.Drawing.Point(0, 0);
			splitContainer2.Name = "splitContainer2";
			splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainer2.Panel1.Controls.Add(dataGridItems);
			splitContainer2.Panel2.Controls.Add(devXChartItems);
			splitContainer2.Size = new System.Drawing.Size(805, 345);
			splitContainer2.SplitterDistance = 201;
			splitContainer2.TabIndex = 299;
			dataGridItems.AllowUnfittedView = false;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance25;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(0, 0);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowDeleteMenu = false;
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.ShowNewMenu = false;
			dataGridItems.Size = new System.Drawing.Size(805, 201);
			dataGridItems.TabIndex = 4;
			dataGridItems.Text = "By Item";
			devXChartItems.CrossHairPattern = null;
			xYDiagram2.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram2.AxisY.VisibleInPanesSerializable = "-1";
			devXChartItems.Diagram = xYDiagram2;
			devXChartItems.Dock = System.Windows.Forms.DockStyle.Fill;
			devXChartItems.Legend.Name = "Default Legend";
			devXChartItems.Location = new System.Drawing.Point(0, 0);
			devXChartItems.Name = "devXChartItems";
			sideBySideBarSeriesLabel4.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series3.Label = sideBySideBarSeriesLabel4;
			series3.Name = "Series 1";
			sideBySideBarSeriesLabel5.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series4.Label = sideBySideBarSeriesLabel5;
			series4.Name = "Series 2";
			devXChartItems.SeriesSerializable = new DevExpress.XtraCharts.Series[2]
			{
				series3,
				series4
			};
			sideBySideBarSeriesLabel6.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			devXChartItems.SeriesTemplate.Label = sideBySideBarSeriesLabel6;
			devXChartItems.Size = new System.Drawing.Size(805, 140);
			devXChartItems.TabIndex = 0;
			ultraTabPageControl1.Controls.Add(splitContainer3);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(805, 345);
			splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer3.Location = new System.Drawing.Point(0, 0);
			splitContainer3.Name = "splitContainer3";
			splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainer3.Panel1.Controls.Add(dataGridCategory);
			splitContainer3.Panel2.Controls.Add(devXChartCategory);
			splitContainer3.Size = new System.Drawing.Size(805, 345);
			splitContainer3.SplitterDistance = 201;
			splitContainer3.TabIndex = 300;
			dataGridCategory.AllowUnfittedView = false;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridCategory.DisplayLayout.Appearance = appearance37;
			dataGridCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCategory.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridCategory.DisplayLayout.MaxColScrollRegions = 1;
			dataGridCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridCategory.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridCategory.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridCategory.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridCategory.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridCategory.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCategory.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridCategory.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridCategory.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridCategory.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridCategory.LoadLayoutFailed = false;
			dataGridCategory.Location = new System.Drawing.Point(0, 0);
			dataGridCategory.Name = "dataGridCategory";
			dataGridCategory.ShowDeleteMenu = false;
			dataGridCategory.ShowMinusInRed = true;
			dataGridCategory.ShowNewMenu = false;
			dataGridCategory.Size = new System.Drawing.Size(805, 201);
			dataGridCategory.TabIndex = 4;
			dataGridCategory.Text = "By Item";
			devXChartCategory.CrossHairPattern = null;
			xYDiagram3.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram3.AxisY.VisibleInPanesSerializable = "-1";
			devXChartCategory.Diagram = xYDiagram3;
			devXChartCategory.Dock = System.Windows.Forms.DockStyle.Fill;
			devXChartCategory.Legend.Name = "Default Legend";
			devXChartCategory.Location = new System.Drawing.Point(0, 0);
			devXChartCategory.Name = "devXChartCategory";
			sideBySideBarSeriesLabel7.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series5.Label = sideBySideBarSeriesLabel7;
			series5.Name = "Series 1";
			sideBySideBarSeriesLabel8.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series6.Label = sideBySideBarSeriesLabel8;
			series6.Name = "Series 2";
			devXChartCategory.SeriesSerializable = new DevExpress.XtraCharts.Series[2]
			{
				series5,
				series6
			};
			sideBySideBarSeriesLabel9.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			devXChartCategory.SeriesTemplate.Label = sideBySideBarSeriesLabel9;
			devXChartCategory.Size = new System.Drawing.Size(805, 140);
			devXChartCategory.TabIndex = 0;
			ultraTabPageControl2.Controls.Add(splitContainer4);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(805, 345);
			splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer4.Location = new System.Drawing.Point(0, 0);
			splitContainer4.Name = "splitContainer4";
			splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainer4.Panel1.Controls.Add(dataGridCustomer);
			splitContainer4.Panel2.Controls.Add(devXChartCustomer);
			splitContainer4.Size = new System.Drawing.Size(805, 345);
			splitContainer4.SplitterDistance = 201;
			splitContainer4.TabIndex = 300;
			dataGridCustomer.AllowUnfittedView = false;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridCustomer.DisplayLayout.Appearance = appearance49;
			dataGridCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCustomer.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridCustomer.DisplayLayout.MaxColScrollRegions = 1;
			dataGridCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridCustomer.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridCustomer.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridCustomer.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridCustomer.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridCustomer.LoadLayoutFailed = false;
			dataGridCustomer.Location = new System.Drawing.Point(0, 0);
			dataGridCustomer.Name = "dataGridCustomer";
			dataGridCustomer.ShowDeleteMenu = false;
			dataGridCustomer.ShowMinusInRed = true;
			dataGridCustomer.ShowNewMenu = false;
			dataGridCustomer.Size = new System.Drawing.Size(805, 201);
			dataGridCustomer.TabIndex = 4;
			dataGridCustomer.Text = "By Item";
			devXChartCustomer.CrossHairPattern = null;
			xYDiagram4.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram4.AxisY.VisibleInPanesSerializable = "-1";
			devXChartCustomer.Diagram = xYDiagram4;
			devXChartCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
			devXChartCustomer.Legend.Name = "Default Legend";
			devXChartCustomer.Location = new System.Drawing.Point(0, 0);
			devXChartCustomer.Name = "devXChartCustomer";
			sideBySideBarSeriesLabel10.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series7.Label = sideBySideBarSeriesLabel10;
			series7.Name = "Series 1";
			sideBySideBarSeriesLabel11.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series8.Label = sideBySideBarSeriesLabel11;
			series8.Name = "Series 2";
			devXChartCustomer.SeriesSerializable = new DevExpress.XtraCharts.Series[2]
			{
				series7,
				series8
			};
			sideBySideBarSeriesLabel12.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			devXChartCustomer.SeriesTemplate.Label = sideBySideBarSeriesLabel12;
			devXChartCustomer.Size = new System.Drawing.Size(805, 140);
			devXChartCustomer.TabIndex = 0;
			ultraTabPageControl3.Controls.Add(chartControlPrice);
			ultraTabPageControl3.Controls.Add(panel2);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(805, 345);
			chartControlPrice.CrossHairPattern = null;
			xYDiagram5.AxisX.VisibleInPanesSerializable = "0";
			xYDiagram5.AxisY.VisibleInPanesSerializable = "-1";
			xYDiagram5.EnableAxisXScrolling = true;
			xYDiagram5.EnableAxisXZooming = true;
			xYDiagramPane.Name = "Pane 1";
			xYDiagramPane.PaneID = 0;
			xYDiagram5.Panes.AddRange(new DevExpress.XtraCharts.XYDiagramPane[1]
			{
				xYDiagramPane
			});
			secondaryAxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Near;
			secondaryAxisY.AxisID = 0;
			secondaryAxisY.GridLines.Visible = true;
			secondaryAxisY.Name = "Secondary AxisY 1";
			secondaryAxisY.VisibleInPanesSerializable = "0";
			xYDiagram5.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[1]
			{
				secondaryAxisY
			});
			chartControlPrice.Diagram = xYDiagram5;
			chartControlPrice.Dock = System.Windows.Forms.DockStyle.Fill;
			chartControlPrice.Legend.Name = "Default Legend";
			chartControlPrice.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
			chartControlPrice.Location = new System.Drawing.Point(0, 0);
			chartControlPrice.Name = "chartControlPrice";
			chartControlPrice.PaletteBaseColorNumber = 1;
			chartControlPrice.PaletteName = "Concourse";
			sideBySideBarSeriesLabel13.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			sideBySideBarSeriesLabel13.TextPattern = "{V:N2}";
			series9.Label = sideBySideBarSeriesLabel13;
			series9.Name = "Volume";
			sideBySideBarSeriesView.AxisYName = "Secondary AxisY 1";
			sideBySideBarSeriesView.Color = System.Drawing.Color.DarkGray;
			sideBySideBarSeriesView.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
			sideBySideBarSeriesView.PaneName = "Pane 1";
			series9.View = sideBySideBarSeriesView;
			pointSeriesLabel.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series10.Label = pointSeriesLabel;
			series10.Name = "Price";
			splineSeriesView.Color = System.Drawing.Color.FromArgb(50, 116, 214);
			splineSeriesView.LineMarkerOptions.Size = 4;
			splineSeriesView.LineTensionPercent = 40;
			series10.View = splineSeriesView;
			pointSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series11.Label = pointSeriesLabel2;
			series11.Name = "Cost";
			splineSeriesView2.Color = System.Drawing.Color.Red;
			splineSeriesView2.LineMarkerOptions.Size = 5;
			splineSeriesView2.LineTensionPercent = 40;
			series11.View = splineSeriesView2;
			chartControlPrice.SeriesSerializable = new DevExpress.XtraCharts.Series[3]
			{
				series9,
				series10,
				series11
			};
			pointSeriesLabel3.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			chartControlPrice.SeriesTemplate.Label = pointSeriesLabel3;
			chartControlPrice.SeriesTemplate.View = splineSeriesView3;
			chartControlPrice.Size = new System.Drawing.Size(805, 310);
			chartControlPrice.TabIndex = 0;
			panel2.Controls.Add(checkBoxShowVolume);
			panel2.Controls.Add(checkBoxShowCOGS);
			panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel2.Location = new System.Drawing.Point(0, 310);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(805, 35);
			panel2.TabIndex = 1;
			checkBoxShowVolume.AutoSize = true;
			checkBoxShowVolume.Checked = true;
			checkBoxShowVolume.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxShowVolume.Location = new System.Drawing.Point(103, 6);
			checkBoxShowVolume.Name = "checkBoxShowVolume";
			checkBoxShowVolume.Size = new System.Drawing.Size(91, 17);
			checkBoxShowVolume.TabIndex = 299;
			checkBoxShowVolume.Text = "Show Volume";
			checkBoxShowVolume.UseVisualStyleBackColor = true;
			checkBoxShowVolume.CheckedChanged += new System.EventHandler(checkBoxShowVolume_CheckedChanged);
			checkBoxShowCOGS.AutoSize = true;
			checkBoxShowCOGS.Checked = true;
			checkBoxShowCOGS.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxShowCOGS.Location = new System.Drawing.Point(14, 6);
			checkBoxShowCOGS.Name = "checkBoxShowCOGS";
			checkBoxShowCOGS.Size = new System.Drawing.Size(77, 17);
			checkBoxShowCOGS.TabIndex = 299;
			checkBoxShowCOGS.Text = "Show Cost";
			checkBoxShowCOGS.UseVisualStyleBackColor = true;
			checkBoxShowCOGS.CheckedChanged += new System.EventHandler(checkBoxShowCOGS_CheckedChanged);
			ultraTabPageControl4.Controls.Add(dataGridPODetail);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(805, 345);
			dataGridPODetail.AllowUnfittedView = false;
			dataGridPODetail.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPODetail.DisplayLayout.Appearance = appearance61;
			dataGridPODetail.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPODetail.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPODetail.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPODetail.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			dataGridPODetail.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPODetail.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			dataGridPODetail.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPODetail.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPODetail.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPODetail.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			dataGridPODetail.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPODetail.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			dataGridPODetail.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPODetail.DisplayLayout.Override.CellAppearance = appearance68;
			dataGridPODetail.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPODetail.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPODetail.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			dataGridPODetail.DisplayLayout.Override.HeaderAppearance = appearance70;
			dataGridPODetail.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPODetail.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			dataGridPODetail.DisplayLayout.Override.RowAppearance = appearance71;
			dataGridPODetail.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPODetail.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			dataGridPODetail.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPODetail.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPODetail.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPODetail.LoadLayoutFailed = false;
			dataGridPODetail.Location = new System.Drawing.Point(4, 1);
			dataGridPODetail.Name = "dataGridPODetail";
			dataGridPODetail.ShowDeleteMenu = false;
			dataGridPODetail.ShowMinusInRed = true;
			dataGridPODetail.ShowNewMenu = false;
			dataGridPODetail.Size = new System.Drawing.Size(796, 290);
			dataGridPODetail.TabIndex = 4;
			dataGridPODetail.Text = "dataGridList1";
			ultraTabPageControl5.Controls.Add(dataGridGRNDetail);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(805, 345);
			dataGridGRNDetail.AllowUnfittedView = false;
			dataGridGRNDetail.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridGRNDetail.DisplayLayout.Appearance = appearance73;
			dataGridGRNDetail.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridGRNDetail.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGRNDetail.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGRNDetail.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			dataGridGRNDetail.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGRNDetail.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			dataGridGRNDetail.DisplayLayout.MaxColScrollRegions = 1;
			dataGridGRNDetail.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridGRNDetail.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridGRNDetail.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			dataGridGRNDetail.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridGRNDetail.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			dataGridGRNDetail.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridGRNDetail.DisplayLayout.Override.CellAppearance = appearance80;
			dataGridGRNDetail.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridGRNDetail.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGRNDetail.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			dataGridGRNDetail.DisplayLayout.Override.HeaderAppearance = appearance82;
			dataGridGRNDetail.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridGRNDetail.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			dataGridGRNDetail.DisplayLayout.Override.RowAppearance = appearance83;
			dataGridGRNDetail.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridGRNDetail.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			dataGridGRNDetail.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridGRNDetail.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridGRNDetail.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridGRNDetail.LoadLayoutFailed = false;
			dataGridGRNDetail.Location = new System.Drawing.Point(4, 2);
			dataGridGRNDetail.Name = "dataGridGRNDetail";
			dataGridGRNDetail.ShowDeleteMenu = false;
			dataGridGRNDetail.ShowMinusInRed = true;
			dataGridGRNDetail.ShowNewMenu = false;
			dataGridGRNDetail.Size = new System.Drawing.Size(796, 290);
			dataGridGRNDetail.TabIndex = 4;
			dataGridGRNDetail.Text = "dataGridList1";
			panelButtons.Controls.Add(checkBoxShowCost);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 506);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(828, 48);
			panelButtons.TabIndex = 4;
			checkBoxShowCost.AutoSize = true;
			checkBoxShowCost.Location = new System.Drawing.Point(14, 16);
			checkBoxShowCost.Name = "checkBoxShowCost";
			checkBoxShowCost.Size = new System.Drawing.Size(177, 17);
			checkBoxShowCost.TabIndex = 298;
			checkBoxShowCost.Text = "Show cost and profit information";
			checkBoxShowCost.UseVisualStyleBackColor = true;
			checkBoxShowCost.CheckedChanged += new System.EventHandler(checkBoxShowCost_CheckedChanged);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(828, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(720, 16);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 3;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			contextMenuPopup.MenuItems.AddRange(new System.Windows.Forms.MenuItem[6]
			{
				menuItemGotoAccount,
				menuItem2,
				menuItemNew,
				menuItemDelete,
				menuItem1,
				menuItemMarkAs
			});
			menuItemGotoAccount.Index = 0;
			menuItemGotoAccount.Text = "&Goto Account";
			menuItem2.Index = 1;
			menuItem2.Text = "-";
			menuItemNew.Index = 2;
			menuItemNew.Text = "&New Account";
			menuItemDelete.Index = 3;
			menuItemDelete.Text = "&Delete Account";
			menuItem1.Index = 4;
			menuItem1.Text = "-";
			menuItemMarkAs.Index = 5;
			menuItemMarkAs.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1]
			{
				menuItemInactive
			});
			menuItemMarkAs.Text = "Mark As";
			menuItemInactive.Index = 0;
			menuItemInactive.Text = "Inactive";
			contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[4]
			{
				menuItemFlat,
				menuItemHierarchical,
				menuItem3,
				menuItemShowInactiveItems
			});
			menuItemFlat.Index = 0;
			menuItemFlat.Text = "Flat View";
			menuItemHierarchical.Index = 1;
			menuItemHierarchical.Text = "Hierarchical View";
			menuItem3.Index = 2;
			menuItem3.Text = "-";
			menuItemShowInactiveItems.Index = 3;
			menuItemShowInactiveItems.Text = "Show Inactive Items";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButton1,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonMerge
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(828, 31);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(74, 28);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = Micromind.ClientUI.Properties.Resources.Export;
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(77, 28);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(118, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonAutoFit.Image");
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(99, 28);
			toolStripButtonAutoFit.Text = "Fit Columns";
			toolStripButtonAutoFit.ToolTipText = "Fit Columns to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(118, 28);
			toolStripButton1.Text = "Resize Columns";
			toolStripButton1.ToolTipText = "Resize columns to length of text";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(83, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(97, 28);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButtonMerge_Click);
			ultraGridPrintDocument1.Grid = dataGridDetail;
			pictureBoxVoid.Image = Micromind.ClientUI.Properties.Resources.voidicon;
			pictureBoxVoid.Location = new System.Drawing.Point(802, 28);
			pictureBoxVoid.Name = "pictureBoxVoid";
			pictureBoxVoid.Size = new System.Drawing.Size(26, 20);
			pictureBoxVoid.TabIndex = 291;
			pictureBoxVoid.TabStop = false;
			pictureBoxVoid.Visible = false;
			textBoxCode.Enabled = false;
			textBoxCode.Location = new System.Drawing.Point(73, 86);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(268, 20);
			textBoxCode.TabIndex = 292;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(73, 108);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(239, 13);
			label2.TabIndex = 294;
			label2.Text = "Use wild character '%' to select range of products";
			radioButtonCode.AutoSize = true;
			radioButtonCode.Location = new System.Drawing.Point(14, 87);
			radioButtonCode.Name = "radioButtonCode";
			radioButtonCode.Size = new System.Drawing.Size(53, 17);
			radioButtonCode.TabIndex = 295;
			radioButtonCode.Text = "Code:";
			radioButtonCode.UseVisualStyleBackColor = true;
			radioButtonCode.CheckedChanged += new System.EventHandler(radioButtonItem_CheckedChanged);
			radioButtonItem.AutoSize = true;
			radioButtonItem.Checked = true;
			radioButtonItem.Location = new System.Drawing.Point(12, 36);
			radioButtonItem.Name = "radioButtonItem";
			radioButtonItem.Size = new System.Drawing.Size(48, 17);
			radioButtonItem.TabIndex = 295;
			radioButtonItem.TabStop = true;
			radioButtonItem.Text = "Item:";
			radioButtonItem.UseVisualStyleBackColor = true;
			radioButtonItem.CheckedChanged += new System.EventHandler(radioButtonItem_CheckedChanged);
			textBoxDescription.Location = new System.Drawing.Point(73, 58);
			textBoxDescription.MaxLength = 255;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.Size = new System.Drawing.Size(268, 20);
			textBoxDescription.TabIndex = 292;
			tabControlTab.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			tabControlTab.Controls.Add(ultraTabSharedControlsPage1);
			tabControlTab.Controls.Add(tabPageGeneral);
			tabControlTab.Controls.Add(tabPageSalesperson);
			tabControlTab.Controls.Add(ultraTabControl);
			tabControlTab.Controls.Add(ultraTabPageControl1);
			tabControlTab.Controls.Add(ultraTabPageControl2);
			tabControlTab.Controls.Add(ultraTabPageControl3);
			tabControlTab.Controls.Add(ultraTabPageControl4);
			tabControlTab.Controls.Add(ultraTabPageControl5);
			tabControlTab.Location = new System.Drawing.Point(9, 132);
			tabControlTab.MinTabWidth = 80;
			tabControlTab.Name = "tabControlTab";
			tabControlTab.SharedControlsPage = ultraTabSharedControlsPage1;
			tabControlTab.Size = new System.Drawing.Size(809, 368);
			tabControlTab.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			tabControlTab.TabIndex = 297;
			appearance85.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance85;
			ultraTab.Key = "Details";
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "Purchase Details";
			ultraTab2.Key = "Buyer";
			ultraTab2.TabPage = tabPageSalesperson;
			ultraTab2.Text = "By Buyer";
			ultraTab3.Key = "Item";
			ultraTab3.TabPage = ultraTabControl;
			ultraTab3.Text = "By Item";
			ultraTab4.Key = "Category";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "By Category";
			ultraTab5.Key = "Vendor";
			ultraTab5.TabPage = ultraTabPageControl2;
			ultraTab5.Text = "By Vendor";
			ultraTab6.Key = "PriceChart";
			ultraTab6.TabPage = ultraTabPageControl3;
			ultraTab6.Text = "Price Chart";
			ultraTab7.Key = "PurchaseOrder";
			ultraTab7.TabPage = ultraTabPageControl4;
			ultraTab7.Text = "PO Details";
			ultraTab8.Key = "GRN";
			ultraTab8.TabPage = ultraTabPageControl5;
			ultraTab8.Text = "GRN Details";
			tabControlTab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[8]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(805, 345);
			comboBoxItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxItem.Assigned = false;
			comboBoxItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxItem.CustomReportFieldName = "";
			comboBoxItem.CustomReportKey = "";
			comboBoxItem.CustomReportValueType = 1;
			comboBoxItem.DescriptionTextBox = textBoxDescription;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxItem.DisplayLayout.Appearance = appearance86;
			comboBoxItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance87.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxItem.DisplayLayout.GroupByBox.Appearance = appearance87;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance88;
			comboBoxItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance89.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance89.BackColor2 = System.Drawing.SystemColors.Control;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxItem.DisplayLayout.GroupByBox.PromptAppearance = appearance89;
			comboBoxItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxItem.DisplayLayout.Override.ActiveCellAppearance = appearance90;
			appearance91.BackColor = System.Drawing.SystemColors.Highlight;
			appearance91.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxItem.DisplayLayout.Override.ActiveRowAppearance = appearance91;
			comboBoxItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			comboBoxItem.DisplayLayout.Override.CardAreaAppearance = appearance92;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			appearance93.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxItem.DisplayLayout.Override.CellAppearance = appearance93;
			comboBoxItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxItem.DisplayLayout.Override.CellPadding = 0;
			appearance94.BackColor = System.Drawing.SystemColors.Control;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxItem.DisplayLayout.Override.GroupByRowAppearance = appearance94;
			appearance95.TextHAlignAsString = "Left";
			comboBoxItem.DisplayLayout.Override.HeaderAppearance = appearance95;
			comboBoxItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance96.BackColor = System.Drawing.SystemColors.Window;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			comboBoxItem.DisplayLayout.Override.RowAppearance = appearance96;
			comboBoxItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance97;
			comboBoxItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxItem.Editable = true;
			comboBoxItem.FilterCustomerID = "";
			comboBoxItem.FilterString = "";
			comboBoxItem.FilterSysDocID = "";
			comboBoxItem.HasAllAccount = false;
			comboBoxItem.HasCustom = false;
			comboBoxItem.IsDataLoaded = false;
			comboBoxItem.Location = new System.Drawing.Point(73, 36);
			comboBoxItem.MaxDropDownItems = 12;
			comboBoxItem.Name = "comboBoxItem";
			comboBoxItem.Show3PLItems = true;
			comboBoxItem.ShowInactiveItems = false;
			comboBoxItem.ShowOnlyLotItems = false;
			comboBoxItem.ShowQuickAdd = true;
			comboBoxItem.Size = new System.Drawing.Size(268, 20);
			comboBoxItem.TabIndex = 296;
			comboBoxItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(374, 33);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(318, 55);
			dateControl1.TabIndex = 0;
			dateControl1.ToDate = new System.DateTime(2017, 12, 25, 23, 59, 59, 59);
			comboBoxVendor.Assigned = false;
			comboBoxVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxVendor;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance98;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance99;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance100;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance101.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance101.BackColor2 = System.Drawing.SystemColors.Control;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance101;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance102;
			appearance103.BackColor = System.Drawing.SystemColors.Highlight;
			appearance103.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance103;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance104;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			appearance105.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance105;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance106.BackColor = System.Drawing.SystemColors.Control;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance106;
			appearance107.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance107;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance108;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance109;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(73, 36);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(268, 20);
			comboBoxVendor.TabIndex = 298;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVendor.Location = new System.Drawing.Point(73, 58);
			textBoxVendor.MaxLength = 255;
			textBoxVendor.Name = "textBoxVendor";
			textBoxVendor.ReadOnly = true;
			textBoxVendor.Size = new System.Drawing.Size(268, 20);
			textBoxVendor.TabIndex = 299;
			radioButtonVendor.AutoSize = true;
			radioButtonVendor.Checked = true;
			radioButtonVendor.Location = new System.Drawing.Point(11, 36);
			radioButtonVendor.Name = "radioButtonVendor";
			radioButtonVendor.Size = new System.Drawing.Size(62, 17);
			radioButtonVendor.TabIndex = 300;
			radioButtonVendor.TabStop = true;
			radioButtonVendor.Text = "Vendor:";
			radioButtonVendor.UseVisualStyleBackColor = true;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(828, 554);
			base.Controls.Add(radioButtonVendor);
			base.Controls.Add(textBoxVendor);
			base.Controls.Add(comboBoxVendor);
			base.Controls.Add(tabControlTab);
			base.Controls.Add(comboBoxItem);
			base.Controls.Add(radioButtonItem);
			base.Controls.Add(radioButtonCode);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(pictureBoxVoid);
			base.Controls.Add(dateControl1);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "InventoryPurchasesStatisticForm";
			Text = "Inventory Purchase Statistics";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			tabPageGeneral.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelCOGS.ResumeLayout(false);
			panelCOGS.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDetail).EndInit();
			tabPageSalesperson.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)series).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).EndInit();
			((System.ComponentModel.ISupportInitialize)series2).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel3).EndInit();
			((System.ComponentModel.ISupportInitialize)devXChartSalesperson).EndInit();
			ultraTabControl.ResumeLayout(false);
			splitContainer2.Panel1.ResumeLayout(false);
			splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
			splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram2).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel4).EndInit();
			((System.ComponentModel.ISupportInitialize)series3).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel5).EndInit();
			((System.ComponentModel.ISupportInitialize)series4).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel6).EndInit();
			((System.ComponentModel.ISupportInitialize)devXChartItems).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			splitContainer3.Panel1.ResumeLayout(false);
			splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
			splitContainer3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram3).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel7).EndInit();
			((System.ComponentModel.ISupportInitialize)series5).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel8).EndInit();
			((System.ComponentModel.ISupportInitialize)series6).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel9).EndInit();
			((System.ComponentModel.ISupportInitialize)devXChartCategory).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			splitContainer4.Panel1.ResumeLayout(false);
			splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
			splitContainer4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram4).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel10).EndInit();
			((System.ComponentModel.ISupportInitialize)series7).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel11).EndInit();
			((System.ComponentModel.ISupportInitialize)series8).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel12).EndInit();
			((System.ComponentModel.ISupportInitialize)devXChartCustomer).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)xYDiagramPane).EndInit();
			((System.ComponentModel.ISupportInitialize)secondaryAxisY).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram5).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel13).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesView).EndInit();
			((System.ComponentModel.ISupportInitialize)series9).EndInit();
			((System.ComponentModel.ISupportInitialize)pointSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)splineSeriesView).EndInit();
			((System.ComponentModel.ISupportInitialize)series10).EndInit();
			((System.ComponentModel.ISupportInitialize)pointSeriesLabel2).EndInit();
			((System.ComponentModel.ISupportInitialize)splineSeriesView2).EndInit();
			((System.ComponentModel.ISupportInitialize)series11).EndInit();
			((System.ComponentModel.ISupportInitialize)pointSeriesLabel3).EndInit();
			((System.ComponentModel.ISupportInitialize)splineSeriesView3).EndInit();
			((System.ComponentModel.ISupportInitialize)chartControlPrice).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ultraTabPageControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPODetail).EndInit();
			ultraTabPageControl5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridGRNDetail).EndInit();
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).EndInit();
			((System.ComponentModel.ISupportInitialize)tabControlTab).EndInit();
			tabControlTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			LoadUserSettings();
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void SetupList()
		{
			try
			{
				dataGridDetail.ApplyFormat();
				HideShowColumns();
				dataGridDetail.LoadLayout();
				dataGridDetail.ApplyUIDesign();
				dataGridDetail.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridDetail.ApplyFormat();
				UltraGridColumn ultraGridColumn = dataGridDetail.DisplayLayout.Bands[0].Columns["Category"];
				bool hidden = dataGridDetail.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridDetail.DisplayLayout.Bands[0].Columns["Category"].ExcludeFromColumnChooser = (dataGridDetail.DisplayLayout.Bands[0].Columns["CategoryID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				dataGridDetail.ApplyQuantityColumnFormat(dataGridDetail.DisplayLayout.Bands[0].Columns["Quantity"], addSummary: true);
				dataGridDetail.ApplyQuantityColumnFormat(dataGridDetail.DisplayLayout.Bands[0].Columns["UnitPrice"], addSummary: false);
				dataGridDetail.ApplyNumericColumnFormat(dataGridDetail.DisplayLayout.Bands[0].Columns["Amount"], addSummary: true);
				dataGridDetail.ApplyNumericColumnFormat(dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"], addSummary: true);
				dataGridDetail.ApplyNumericColumnFormat(dataGridDetail.DisplayLayout.Bands[0].Columns["Profit"], addSummary: true);
				if (dataGridDetail.DisplayLayout.Bands.Count > 0 && dataGridDetail.DisplayLayout.Bands[0].Summaries.Count > 0 && dataGridDetail.DisplayLayout.Bands[0].Summaries.Exists("count"))
				{
					dataGridDetail.DisplayLayout.Bands[0].Summaries["count"].SummaryPosition = SummaryPosition.UseSummaryPositionColumn;
					dataGridDetail.DisplayLayout.Bands[0].Summaries["count"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridDetail.DisplayLayout.Bands[0].Summaries["count"].SummaryPositionColumn = dataGridDetail.DisplayLayout.Bands[0].Columns[1];
				}
				dataGridDetail.DisplayLayout.Bands[0].Columns["CurrencyID"].Header.Caption = "Cur";
				dataGridDetail.DisplayLayout.Bands[0].Columns["CurrencyRate"].Header.Caption = "Cur Rate";
				dataGridDetail.DisplayLayout.Bands[0].Columns["VendorID"].Header.Caption = "Vendor Code";
				dataGridDetail.DisplayLayout.Bands[0].Columns["VendorName"].Header.Caption = "Vendor";
				dataGridDetail.DisplayLayout.Bands[0].Columns["ProductID"].Header.Caption = "Item Code";
				dataGridDetail.DisplayLayout.Bands[0].Columns["Reference"].Header.Caption = "Ref";
				dataGridDetail.DisplayLayout.Bands[0].Columns["Buyer Name"].Header.Caption = "Buyer";
				dataGridDetail.DisplayLayout.Bands[0].Columns["UnitPrice"].Header.Caption = "Price";
				dataGridDetail.DisplayLayout.Override.AllowColSizing = AllowColSizing.Free;
				dataGridDetail.DisplayLayout.Bands[0].Columns["Doc ID"].Hidden = true;
				dataGridDetail.DisplayLayout.Bands[0].Columns["Doc ID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				if (dataGridDetail.DisplayLayout != null && dataGridDetail.DisplayLayout.Bands.Count > 0)
				{
					ValueList sysDocTypesValueList = UILib.GetSysDocTypesValueList();
					dataGridDetail.DisplayLayout.Bands[0].Columns["Type"].ValueList = sysDocTypesValueList;
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
					{
						dataGridDetail.DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = true;
						dataGridDetail.DisplayLayout.Bands[0].Columns["UnitPrice"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
						dataGridDetail.DisplayLayout.Bands[0].Columns["AverageCost"].Hidden = true;
						dataGridDetail.DisplayLayout.Bands[0].Columns["AverageCost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
						dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"].Hidden = true;
						dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					}
					else
					{
						UltraGridColumn ultraGridColumn2 = dataGridDetail.DisplayLayout.Bands[0].Columns["UnitPrice"];
						UltraGridColumn ultraGridColumn3 = dataGridDetail.DisplayLayout.Bands[0].Columns["AverageCost"];
						ExcludeFromColumnChooser excludeFromColumnChooser5 = dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
						excludeFromColumnChooser3 = (ultraGridColumn2.ExcludeFromColumnChooser = (ultraGridColumn3.ExcludeFromColumnChooser = excludeFromColumnChooser5));
					}
				}
				dataGridSalesperson.ApplyUIDesign();
				dataGridSalesperson.ApplyQuantityColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["NetQuantity"], addSummary: true);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["QuantitySold"], addSummary: true);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["QuantityReturn"], addSummary: true);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["Purchase"], addSummary: true);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["COGS"], addSummary: true);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["Profit"], addSummary: true);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["AvgCost"], addSummary: false);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["AvgPrice"], addSummary: false);
				dataGridSalesperson.ApplyNumericColumnFormat(dataGridSalesperson.DisplayLayout.Bands[0].Columns["Margin"], addSummary: false);
				dataGridItems.ApplyUIDesign();
				dataGridItems.ApplyQuantityColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["NetQuantity"], addSummary: true);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["QuantitySold"], addSummary: true);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["QuantityReturn"], addSummary: true);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["Purchase"], addSummary: true);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["COGS"], addSummary: true);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["Profit"], addSummary: true);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["AvgCost"], addSummary: false);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["AvgPrice"], addSummary: false);
				dataGridItems.ApplyNumericColumnFormat(dataGridItems.DisplayLayout.Bands[0].Columns["Margin"], addSummary: false);
				dataGridCategory.ApplyUIDesign();
				dataGridCategory.ApplyQuantityColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["NetQuantity"], addSummary: true);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["QuantitySold"], addSummary: true);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["QuantityReturn"], addSummary: true);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["Purchase"], addSummary: true);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["COGS"], addSummary: true);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["Profit"], addSummary: true);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["AvgCost"], addSummary: false);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["AvgPrice"], addSummary: false);
				dataGridCategory.ApplyNumericColumnFormat(dataGridCategory.DisplayLayout.Bands[0].Columns["Margin"], addSummary: false);
				dataGridCustomer.ApplyUIDesign();
				dataGridCustomer.ApplyQuantityColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["NetQuantity"], addSummary: true);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["QuantitySold"], addSummary: true);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["QuantityReturn"], addSummary: true);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["Purchase"], addSummary: true);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["COGS"], addSummary: true);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["Profit"], addSummary: true);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["AvgCost"], addSummary: false);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["AvgPrice"], addSummary: false);
				dataGridCustomer.ApplyNumericColumnFormat(dataGridCustomer.DisplayLayout.Bands[0].Columns["Margin"], addSummary: false);
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].Header.Caption = "Item Code";
				dataGridDetail.FormatAllNumericColumns(new string[1]
				{
					"Type"
				});
				ShowHideCostInfo();
				SetupOtherGrid();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupOtherGrid()
		{
			dataGridPODetail.ApplyUIDesign();
			dataGridPODetail.ApplyQuantityColumnFormat(dataGridPODetail.DisplayLayout.Bands[0].Columns["Quantity"], addSummary: true);
			dataGridPODetail.ApplyNumericColumnFormat(dataGridPODetail.DisplayLayout.Bands[0].Columns["Price"], addSummary: true);
			dataGridPODetail.ApplyNumericColumnFormat(dataGridPODetail.DisplayLayout.Bands[0].Columns["Amount"], addSummary: true);
			dataGridGRNDetail.ApplyUIDesign();
			dataGridGRNDetail.ApplyQuantityColumnFormat(dataGridGRNDetail.DisplayLayout.Bands[0].Columns["Quantity"], addSummary: true);
			UltraGridColumn ultraGridColumn = dataGridPODetail.DisplayLayout.Bands[0].Columns["Job"];
			bool hidden = dataGridGRNDetail.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
			ultraGridColumn.Hidden = hidden;
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					if (radioButtonVendor.Checked)
					{
						listData = Factory.VendorSystem.GetInventoryPurchaseDetailByVendor(dateControl1.FromDate, dateControl1.ToDate, comboBoxVendor.SelectedID);
					}
					else
					{
						listData = Factory.VendorSystem.GetInventoryPurchaseDetail(dateControl1.FromDate, dateControl1.ToDate, SelectedID);
					}
					dataGridDetail.DataSource = listData;
					UltraGridColumn ultraGridColumn = dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"];
					UltraGridColumn ultraGridColumn2 = dataGridDetail.DisplayLayout.Bands[0].Columns["Profit"];
					UltraGridColumn ultraGridColumn3 = dataGridDetail.DisplayLayout.Bands[0].Columns["Category"];
					UltraGridColumn ultraGridColumn4 = dataGridDetail.DisplayLayout.Bands[0].Columns["CategoryID"];
					UltraGridColumn ultraGridColumn5 = dataGridDetail.DisplayLayout.Bands[0].Columns["ProductDescription"];
					bool flag2 = dataGridDetail.DisplayLayout.Bands[0].Columns["AmountFC"].Hidden = true;
					bool flag4 = ultraGridColumn5.Hidden = flag2;
					bool flag6 = ultraGridColumn4.Hidden = flag4;
					bool flag8 = ultraGridColumn3.Hidden = flag6;
					bool hidden = ultraGridColumn2.Hidden = flag8;
					ultraGridColumn.Hidden = hidden;
					UltraGridColumn ultraGridColumn6 = dataGridDetail.DisplayLayout.Bands[0].Columns["Category"];
					UltraGridColumn ultraGridColumn7 = dataGridDetail.DisplayLayout.Bands[0].Columns["AmountFC"];
					UltraGridColumn ultraGridColumn8 = dataGridDetail.DisplayLayout.Bands[0].Columns["ProductDescription"];
					ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridDetail.DisplayLayout.Bands[0].Columns["CategoryID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					ExcludeFromColumnChooser excludeFromColumnChooser4 = ultraGridColumn8.ExcludeFromColumnChooser = excludeFromColumnChooser2;
					ExcludeFromColumnChooser excludeFromColumnChooser7 = ultraGridColumn6.ExcludeFromColumnChooser = (ultraGridColumn7.ExcludeFromColumnChooser = excludeFromColumnChooser4);
					dataGridDetail.DisplayLayout.Bands[0].Columns["AverageCost"].Header.Caption = "Avg Cost";
					DataTable dataTable = new DataSet().Tables.Add("Table");
					dataTable.Columns.Add("Buyer");
					dataTable.Columns.Add("QuantitySold", typeof(decimal));
					dataTable.Columns.Add("QuantityReturn", typeof(decimal));
					dataTable.Columns.Add("NetQuantity", typeof(decimal));
					dataTable.Columns.Add("AvgPrice", typeof(decimal));
					dataTable.Columns.Add("AvgCost", typeof(decimal));
					dataTable.Columns.Add("Purchase", typeof(decimal));
					dataTable.Columns.Add("COGS", typeof(decimal));
					dataTable.Columns.Add("Profit", typeof(decimal));
					dataTable.Columns.Add("Margin", typeof(decimal));
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					decimal num3 = default(decimal);
					if (listData != null && listData.Tables.Count > 0)
					{
						DataTable dataTable2 = listData.Tables[0];
						DataView dataView = new DataView(dataTable2);
						new DataTable();
						foreach (DataRow row in dataView.ToTable(true, "BuyerID", "Buyer Name").Rows)
						{
							object obj = null;
							decimal d = default(decimal);
							decimal d2 = default(decimal);
							decimal num4 = default(decimal);
							decimal num5 = default(decimal);
							decimal num6 = default(decimal);
							string text = row["BuyerID"].ToString();
							obj = ((!(text == "")) ? dataTable2.Compute("SUM(Amount)", "BuyerID = '" + text + "' AND Amount>0") : dataTable2.Compute("SUM(Amount)", "(BuyerID IS NULL OR BuyerID = '" + text + "') AND Amount>0"));
							if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
							{
								d = Math.Round(decimal.Parse(obj.ToString()), Global.CurDecimalPoints);
							}
							obj = ((!(text == "")) ? dataTable2.Compute("SUM(Amount)", "BuyerID = '" + text + "' AND Amount < 0") : dataTable2.Compute("SUM(Amount)", "(BuyerID IS NULL OR BuyerID = '" + text + "') AND Amount < 0"));
							if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
							{
								d2 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), Global.CurDecimalPoints));
							}
							obj = ((!(text == "")) ? dataTable2.Compute("SUM(COGS)", "BuyerID = '" + text + "'") : dataTable2.Compute("SUM(COGS)", "BuyerID IS NULL OR BuyerID = '" + text + "'"));
							if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
							{
								num4 = Math.Round(decimal.Parse(obj.ToString()), Global.CurDecimalPoints);
							}
							obj = ((!(text == "")) ? dataTable2.Compute("SUM(Quantity)", "BuyerID = '" + text + "' AND Quantity > 0") : dataTable2.Compute("SUM(Quantity)", "(BuyerID IS NULL OR BuyerID = '" + text + "') AND Quantity < 0"));
							if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
							{
								num5 = Math.Round(decimal.Parse(obj.ToString()), Global.CurDecimalPoints);
							}
							obj = ((!(text == "")) ? dataTable2.Compute("SUM(Quantity)", "BuyerID = '" + text + "' AND Quantity < 0") : dataTable2.Compute("SUM(Quantity)", "(BuyerID IS NULL OR BuyerID = '" + text + "') AND Quantity < 0"));
							if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
							{
								num6 = Math.Abs(Math.Round(decimal.Parse(obj.ToString()), Global.CurDecimalPoints));
							}
							DataRow dataRow2 = dataTable.NewRow();
							if (text != "")
							{
								dataRow2["Buyer"] = row["Buyer Name"];
							}
							else
							{
								dataRow2["Buyer"] = "None";
							}
							dataRow2["QuantitySold"] = num5;
							dataRow2["QuantityReturn"] = num6;
							dataRow2["NetQuantity"] = num5 - num6;
							dataRow2["Purchase"] = d - d2;
							if (num5 - num6 != 0m)
							{
								dataRow2["AvgPrice"] = (d - d2) / (num5 - num6);
							}
							else
							{
								dataRow2["AvgPrice"] = 0;
							}
							if (num5 - num6 != 0m)
							{
								dataRow2["AvgCost"] = num4 / (num5 - num6);
							}
							else
							{
								dataRow2["AvgCost"] = 0;
							}
							dataRow2["COGS"] = num4;
							decimal num7 = d - d2 - num4;
							dataRow2["Profit"] = num7;
							if (d - d2 != 0m)
							{
								dataRow2["Margin"] = num7 / (d - d2) * 100m;
							}
							else
							{
								dataRow2["Margin"] = 0;
							}
							dataRow2.EndEdit();
							dataTable.Rows.Add(dataRow2);
							num += d - d2;
							num3 += num5 - num6;
							num2 += num4;
						}
					}
					dataGridSalesperson.DataSource = dataTable;
					dataGridSalesperson.DisplayLayout.Bands[0].Columns["QuantitySold"].Header.Caption = "Qty Purchased";
					dataGridSalesperson.DisplayLayout.Bands[0].Columns["QuantityReturn"].Header.Caption = "Qty Ret";
					dataGridSalesperson.DisplayLayout.Bands[0].Columns["NetQuantity"].Header.Caption = "Net Qty Purchased";
					dataGridSalesperson.DisplayLayout.Bands[0].Columns["AvgPrice"].Header.Caption = "Avg Price";
					dataGridSalesperson.DisplayLayout.Bands[0].Columns["AvgCost"].Header.Caption = "Avg Cost";
					dataGridSalesperson.DisplayLayout.Bands[0].Columns["Margin"].Header.Caption = "Margin %";
					devXChartSalesperson.SetData(dataTable, "Purchase", "Buyer");
					textBoxTotalQty.Text = num3.ToString(Format.QuantityFormat);
					textBoxTotalAmount.Text = num.ToString(Format.TotalAmountFormat);
					textBoxProfit.Text = (num - num2).ToString(Format.TotalAmountFormat);
					if (num3 != 0m)
					{
						textBoxAvgPrice.Text = Math.Round(num / num3, Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						textBoxAvgCost.Text = Math.Round(num2 / num3, Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxAvgPrice.Text = 0.ToString(Format.TotalAmountFormat);
						textBoxAvgCost.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (num != 0m)
					{
						textBoxMargin.Text = Math.Round((num - num2) / num * 100m, Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxMargin.Text = 0.ToString(Format.TotalAmountFormat);
					}
					dataTable = new DataSet().Tables.Add("Table");
					dataTable.Columns.Add("ProductID");
					dataTable.Columns.Add("Description");
					dataTable.Columns.Add("QuantitySold", typeof(decimal));
					dataTable.Columns.Add("QuantityReturn", typeof(decimal));
					dataTable.Columns.Add("NetQuantity", typeof(decimal));
					dataTable.Columns.Add("AvgPrice", typeof(decimal));
					dataTable.Columns.Add("AvgCost", typeof(decimal));
					dataTable.Columns.Add("Purchase", typeof(decimal));
					dataTable.Columns.Add("COGS", typeof(decimal));
					dataTable.Columns.Add("Profit", typeof(decimal));
					dataTable.Columns.Add("Margin", typeof(decimal));
					if (listData != null && listData.Tables.Count > 0)
					{
						DataTable dataTable3 = listData.Tables[0];
						DataView dataView2 = new DataView(dataTable3);
						new DataTable();
						foreach (DataRow row2 in dataView2.ToTable(true, "ProductID", "ProductDescription").Rows)
						{
							object obj2 = null;
							decimal d3 = default(decimal);
							decimal d4 = default(decimal);
							decimal num8 = default(decimal);
							decimal num9 = default(decimal);
							decimal num10 = default(decimal);
							string text2 = row2["ProductID"].ToString();
							obj2 = dataTable3.Compute("SUM(Amount)", "ProductID = '" + text2 + "' AND Amount>0");
							if (obj2 != null && !string.IsNullOrEmpty(obj2.ToString()))
							{
								d3 = Math.Round(decimal.Parse(obj2.ToString()), 2);
							}
							obj2 = dataTable3.Compute("SUM(Amount)", "ProductID = '" + text2 + "' AND Amount < 0");
							if (obj2 != null && !string.IsNullOrEmpty(obj2.ToString()))
							{
								d4 = Math.Abs(Math.Round(decimal.Parse(obj2.ToString()), Global.CurDecimalPoints));
							}
							obj2 = dataTable3.Compute("SUM(COGS)", "ProductID = '" + text2 + "'");
							if (obj2 != null && !string.IsNullOrEmpty(obj2.ToString()))
							{
								num8 = Math.Round(decimal.Parse(obj2.ToString()), Global.CurDecimalPoints);
							}
							obj2 = dataTable3.Compute("SUM(Quantity)", "ProductID = '" + text2 + "' AND Quantity > 0");
							if (obj2 != null && !string.IsNullOrEmpty(obj2.ToString()))
							{
								num9 = Math.Round(decimal.Parse(obj2.ToString()), Global.CurDecimalPoints);
							}
							obj2 = dataTable3.Compute("SUM(Quantity)", "ProductID = '" + text2 + "' AND Quantity < 0");
							if (obj2 != null && !string.IsNullOrEmpty(obj2.ToString()))
							{
								num10 = Math.Abs(Math.Round(decimal.Parse(obj2.ToString()), Global.CurDecimalPoints));
							}
							DataRow dataRow4 = dataTable.NewRow();
							if (text2 != "")
							{
								dataRow4["ProductID"] = row2["ProductID"];
							}
							else
							{
								dataRow4["ProductID"] = "None";
							}
							dataRow4["Description"] = row2["ProductDescription"];
							dataRow4["QuantitySold"] = num9;
							dataRow4["QuantityReturn"] = num10;
							dataRow4["NetQuantity"] = num9 - num10;
							dataRow4["Purchase"] = d3 - d4;
							if (num9 - num10 != 0m)
							{
								dataRow4["AvgPrice"] = (d3 - d4) / (num9 - num10);
							}
							else
							{
								dataRow4["AvgPrice"] = 0;
							}
							if (num9 - num10 != 0m)
							{
								dataRow4["AvgCost"] = num8 / (num9 - num10);
							}
							else
							{
								dataRow4["AvgCost"] = 0;
							}
							dataRow4["COGS"] = num8;
							decimal num11 = d3 - d4 - num8;
							dataRow4["Profit"] = num11;
							if (d3 - d4 != 0m)
							{
								dataRow4["Margin"] = num11 / (d3 - d4) * 100m;
							}
							else
							{
								dataRow4["Margin"] = 0;
							}
							dataRow4.EndEdit();
							dataTable.Rows.Add(dataRow4);
							num += d3 - d4;
							num3 += num9 - num10;
							num2 += num8;
						}
					}
					dataGridItems.DataSource = dataTable;
					dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].Header.Caption = "Item Code";
					dataGridItems.DisplayLayout.Bands[0].Columns["QuantitySold"].Header.Caption = "Qty Purchased";
					dataGridItems.DisplayLayout.Bands[0].Columns["QuantityReturn"].Header.Caption = "Qty Ret";
					dataGridItems.DisplayLayout.Bands[0].Columns["NetQuantity"].Header.Caption = "Net Qty Purchased";
					dataGridItems.DisplayLayout.Bands[0].Columns["AvgPrice"].Header.Caption = "Avg Price";
					dataGridItems.DisplayLayout.Bands[0].Columns["AvgCost"].Header.Caption = "Avg Cost";
					dataGridItems.DisplayLayout.Bands[0].Columns["Margin"].Header.Caption = "Margin %";
					devXChartItems.SetData(dataTable, "Purchase", "ProductID");
					dataTable = new DataSet().Tables.Add("Table");
					dataTable.Columns.Add("CategoryID");
					dataTable.Columns.Add("Category");
					dataTable.Columns.Add("QuantitySold", typeof(decimal));
					dataTable.Columns.Add("QuantityReturn", typeof(decimal));
					dataTable.Columns.Add("NetQuantity", typeof(decimal));
					dataTable.Columns.Add("AvgPrice", typeof(decimal));
					dataTable.Columns.Add("AvgCost", typeof(decimal));
					dataTable.Columns.Add("Purchase", typeof(decimal));
					dataTable.Columns.Add("COGS", typeof(decimal));
					dataTable.Columns.Add("Profit", typeof(decimal));
					dataTable.Columns.Add("Margin", typeof(decimal));
					if (listData != null && listData.Tables.Count > 0)
					{
						DataTable dataTable4 = listData.Tables[0];
						DataView dataView3 = new DataView(dataTable4);
						new DataTable();
						foreach (DataRow row3 in dataView3.ToTable(true, "CategoryID", "Category").Rows)
						{
							object obj3 = null;
							decimal d5 = default(decimal);
							decimal d6 = default(decimal);
							decimal num12 = default(decimal);
							decimal num13 = default(decimal);
							decimal num14 = default(decimal);
							string text3 = row3["CategoryID"].ToString();
							if (dataTable.Select("CategoryID = '" + text3.Trim() + "'").Length == 0)
							{
								obj3 = ((!(text3 == "")) ? dataTable4.Compute("SUM(Amount)", "CategoryID = '" + text3 + "' AND Amount>0") : dataTable4.Compute("SUM(Amount)", "(CategoryID IS NULL OR CategoryID = '" + text3 + "') AND Amount>0"));
								if (obj3 != null && !string.IsNullOrEmpty(obj3.ToString()))
								{
									d5 = Math.Round(decimal.Parse(obj3.ToString()), Global.CurDecimalPoints);
								}
								obj3 = ((!(text3 == "")) ? dataTable4.Compute("SUM(Amount)", "CategoryID = '" + text3 + "' AND Amount < 0") : dataTable4.Compute("SUM(Amount)", "(CategoryID IS NULL OR CategoryID = '" + text3 + "') AND Amount<0"));
								if (obj3 != null && !string.IsNullOrEmpty(obj3.ToString()))
								{
									d6 = Math.Abs(Math.Round(decimal.Parse(obj3.ToString()), Global.CurDecimalPoints));
								}
								obj3 = dataTable4.Compute("SUM(COGS)", "CategoryID = '" + text3 + "'");
								if (obj3 != null && !string.IsNullOrEmpty(obj3.ToString()))
								{
									num12 = Math.Round(decimal.Parse(obj3.ToString()), Global.CurDecimalPoints);
								}
								obj3 = ((!(text3 == "")) ? dataTable4.Compute("SUM(Quantity)", "CategoryID = '" + text3 + "' AND Quantity > 0") : dataTable4.Compute("SUM(Quantity)", "(CategoryID IS NULL OR CategoryID = '" + text3 + "') AND Quantity>0"));
								if (obj3 != null && !string.IsNullOrEmpty(obj3.ToString()))
								{
									num13 = Math.Round(decimal.Parse(obj3.ToString()), Global.CurDecimalPoints);
								}
								obj3 = ((!(text3 == "")) ? dataTable4.Compute("SUM(Quantity)", "CategoryID = '" + text3 + "' AND Quantity < 0") : dataTable4.Compute("SUM(Quantity)", "(CategoryID IS NULL OR CategoryID = '" + text3 + "') AND Quantity<0"));
								if (obj3 != null && !string.IsNullOrEmpty(obj3.ToString()))
								{
									num14 = Math.Abs(Math.Round(decimal.Parse(obj3.ToString()), Global.CurDecimalPoints));
								}
								DataRow dataRow6 = dataTable.NewRow();
								if (text3 != "")
								{
									dataRow6["CategoryID"] = row3["CategoryID"].ToString().Trim();
								}
								else
								{
									dataRow6["CategoryID"] = "None";
								}
								dataRow6["Category"] = row3["Category"];
								dataRow6["QuantitySold"] = num13;
								dataRow6["QuantityReturn"] = num14;
								dataRow6["NetQuantity"] = num13 - num14;
								dataRow6["Purchase"] = d5 - d6;
								if (num13 - num14 != 0m)
								{
									dataRow6["AvgPrice"] = (d5 - d6) / (num13 - num14);
								}
								else
								{
									dataRow6["AvgPrice"] = 0;
								}
								if (num13 - num14 != 0m)
								{
									dataRow6["AvgCost"] = num12 / (num13 - num14);
								}
								else
								{
									dataRow6["AvgCost"] = 0;
								}
								dataRow6["COGS"] = num12;
								decimal num15 = d5 - d6 - num12;
								dataRow6["Profit"] = num15;
								if (d5 - d6 != 0m)
								{
									dataRow6["Margin"] = num15 / (d5 - d6) * 100m;
								}
								else
								{
									dataRow6["Margin"] = 0;
								}
								dataRow6.EndEdit();
								dataTable.Rows.Add(dataRow6);
								num += d5 - d6;
								num3 += num13 - num14;
								num2 += num12;
							}
						}
					}
					dataGridCategory.DataSource = dataTable;
					dataGridCategory.DisplayLayout.Bands[0].Columns["QuantitySold"].Header.Caption = "Qty Purchased";
					dataGridCategory.DisplayLayout.Bands[0].Columns["QuantityReturn"].Header.Caption = "Qty Ret";
					dataGridCategory.DisplayLayout.Bands[0].Columns["NetQuantity"].Header.Caption = "Net Qty Purchased";
					dataGridCategory.DisplayLayout.Bands[0].Columns["AvgPrice"].Header.Caption = "Avg Price";
					dataGridCategory.DisplayLayout.Bands[0].Columns["AvgCost"].Header.Caption = "Avg Cost";
					dataGridCategory.DisplayLayout.Bands[0].Columns["Margin"].Header.Caption = "Margin %";
					dataGridCategory.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
					dataGridCategory.DisplayLayout.Bands[0].Columns["CategoryID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					devXChartCategory.SetData(dataTable, "Purchase", "Category");
					dataTable = new DataSet().Tables.Add("Table");
					dataTable.Columns.Add("VendorID");
					dataTable.Columns.Add("Vendor");
					dataTable.Columns.Add("QuantitySold", typeof(decimal));
					dataTable.Columns.Add("QuantityReturn", typeof(decimal));
					dataTable.Columns.Add("NetQuantity", typeof(decimal));
					dataTable.Columns.Add("AvgPrice", typeof(decimal));
					dataTable.Columns.Add("AvgCost", typeof(decimal));
					dataTable.Columns.Add("Purchase", typeof(decimal));
					dataTable.Columns.Add("COGS", typeof(decimal));
					dataTable.Columns.Add("Profit", typeof(decimal));
					dataTable.Columns.Add("Margin", typeof(decimal));
					if (listData != null && listData.Tables.Count > 0)
					{
						DataTable dataTable5 = listData.Tables[0];
						DataView dataView4 = new DataView(dataTable5);
						new DataTable();
						foreach (DataRow row4 in dataView4.ToTable(true, "VendorID", "VendorName").Rows)
						{
							object obj4 = null;
							decimal d7 = default(decimal);
							decimal d8 = default(decimal);
							decimal num16 = default(decimal);
							decimal num17 = default(decimal);
							decimal num18 = default(decimal);
							string text4 = row4["VendorID"].ToString();
							obj4 = dataTable5.Compute("SUM(Amount)", "VendorID = '" + text4 + "' AND Amount>0");
							if (obj4 != null && !string.IsNullOrEmpty(obj4.ToString()))
							{
								d7 = Math.Round(decimal.Parse(obj4.ToString()), Global.CurDecimalPoints);
							}
							obj4 = dataTable5.Compute("SUM(Amount)", "VendorID = '" + text4 + "' AND Amount < 0");
							if (obj4 != null && !string.IsNullOrEmpty(obj4.ToString()))
							{
								d8 = Math.Abs(Math.Round(decimal.Parse(obj4.ToString()), Global.CurDecimalPoints));
							}
							obj4 = dataTable5.Compute("SUM(COGS)", "VendorID = '" + text4 + "'");
							if (obj4 != null && !string.IsNullOrEmpty(obj4.ToString()))
							{
								num16 = Math.Round(decimal.Parse(obj4.ToString()), Global.CurDecimalPoints);
							}
							obj4 = dataTable5.Compute("SUM(Quantity)", "VendorID = '" + text4 + "' AND Quantity > 0");
							if (obj4 != null && !string.IsNullOrEmpty(obj4.ToString()))
							{
								num17 = Math.Round(decimal.Parse(obj4.ToString()), Global.CurDecimalPoints);
							}
							obj4 = dataTable5.Compute("SUM(Quantity)", "VendorID = '" + text4 + "' AND Quantity < 0");
							if (obj4 != null && !string.IsNullOrEmpty(obj4.ToString()))
							{
								num18 = Math.Abs(Math.Round(decimal.Parse(obj4.ToString()), Global.CurDecimalPoints));
							}
							DataRow dataRow8 = dataTable.NewRow();
							if (text4 != "")
							{
								dataRow8["VendorID"] = row4["VendorID"];
							}
							else
							{
								dataRow8["VendorID"] = "None";
							}
							dataRow8["Vendor"] = row4["VendorName"];
							dataRow8["QuantitySold"] = num17;
							dataRow8["QuantityReturn"] = num18;
							dataRow8["NetQuantity"] = num17 - num18;
							dataRow8["Purchase"] = d7 - d8;
							if (num17 - num18 != 0m)
							{
								dataRow8["AvgPrice"] = (d7 - d8) / (num17 - num18);
							}
							else
							{
								dataRow8["AvgPrice"] = 0;
							}
							if (num17 - num18 != 0m)
							{
								dataRow8["AvgCost"] = num16 / (num17 - num18);
							}
							else
							{
								dataRow8["AvgCost"] = 0;
							}
							dataRow8["COGS"] = num16;
							decimal num19 = d7 - d8 - num16;
							dataRow8["Profit"] = num19;
							if (d7 - d8 != 0m)
							{
								dataRow8["Margin"] = num19 / (d7 - d8) * 100m;
							}
							else
							{
								dataRow8["Margin"] = 0;
							}
							dataRow8.EndEdit();
							dataTable.Rows.Add(dataRow8);
							num += d7 - d8;
							num3 += num17 - num18;
							num2 += num16;
						}
					}
					dataGridCustomer.DataSource = dataTable;
					dataGridCustomer.DisplayLayout.Bands[0].Columns["QuantitySold"].Header.Caption = "Qty Purchased";
					dataGridCustomer.DisplayLayout.Bands[0].Columns["QuantityReturn"].Header.Caption = "Qty Ret";
					dataGridCustomer.DisplayLayout.Bands[0].Columns["NetQuantity"].Header.Caption = "Net Qty Purchased";
					dataGridCustomer.DisplayLayout.Bands[0].Columns["AvgPrice"].Header.Caption = "Avg Price";
					dataGridCustomer.DisplayLayout.Bands[0].Columns["AvgCost"].Header.Caption = "Avg Cost";
					dataGridCustomer.DisplayLayout.Bands[0].Columns["Margin"].Header.Caption = "Margin %";
					dataGridCustomer.DisplayLayout.Bands[0].Columns["VendorID"].Hidden = true;
					dataGridCustomer.DisplayLayout.Bands[0].Columns["VendorID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					devXChartCustomer.SetData(dataTable, "Purchase", "Vendor");
					chartControlPrice.DataSource = listData.Tables["Price"];
					chartControlPrice.SetData(listData.Tables["Price"], "AvgPrice", "PriceDate");
					chartControlPrice.Series["Price"].ArgumentDataMember = "PriceDate";
					chartControlPrice.Series["Price"].ValueDataMembers.AddRange("AvgPrice");
					chartControlPrice.Series["Price"].ArgumentScaleType = ScaleType.DateTime;
					chartControlPrice.Series["Cost"].ArgumentDataMember = "PriceDate";
					chartControlPrice.Series["Cost"].ValueDataMembers.AddRange("AvgCost");
					chartControlPrice.Series["Cost"].ArgumentScaleType = ScaleType.DateTime;
					chartControlPrice.Series["Volume"].ArgumentDataMember = "PriceDate";
					chartControlPrice.Series["Volume"].ValueDataMembers.AddRange("Volume");
					chartControlPrice.Series["Volume"].ArgumentScaleType = ScaleType.DateTime;
					XYDiagram xYDiagram = chartControlPrice.Diagram as XYDiagram;
					xYDiagram.DefaultPane.EnableAxisXScrolling = DefaultBoolean.True;
					xYDiagram.DefaultPane.EnableAxisXZooming = DefaultBoolean.True;
					xYDiagram.AxisY.Visible = true;
					xYDiagram.AxisX.DateTimeScaleMode = DateTimeScaleMode.Manual;
					xYDiagram.DefaultPane.Weight = 5.0;
					xYDiagram.AxisX.SetVisibilityInPane(visible: false, xYDiagram.DefaultPane);
					xYDiagram.AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Day;
					xYDiagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Day;
					xYDiagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
					xYDiagram.AxisX.DateTimeOptions.FormatString = "dd-MM-yyyy";
					xYDiagram.AxisX.Label.MaxLineCount = 1;
					DataTable dataTable6 = new DataSet().Tables.Add("Table");
					dataTable6.Columns.Add("Doc Number");
					dataTable6.Columns.Add("Date", typeof(DateTime));
					dataTable6.Columns.Add("Supplier");
					dataTable6.Columns.Add("Item Code");
					dataTable6.Columns.Add("Job");
					dataTable6.Columns.Add("Unit");
					dataTable6.Columns.Add("Quantity", typeof(decimal));
					if (listData != null && listData.Tables.Count > 0)
					{
						DataView dataView5 = new DataView(listData.Tables[3]);
						new DataTable();
						foreach (DataRow row5 in dataView5.ToTable().Rows)
						{
							string a = row5["ProductID"].ToString();
							DataRow dataRow10 = dataTable6.NewRow();
							if (a != "")
							{
								dataRow10["Supplier"] = row5["SupplierID"];
							}
							else
							{
								dataRow10["Supplier"] = "None";
							}
							dataRow10["Doc Number"] = row5["VoucherID"];
							dataRow10["Date"] = row5["Date"];
							dataRow10["Item Code"] = row5["ProductID"];
							dataRow10["Quantity"] = row5["Quantity"];
							dataRow10["Unit"] = row5["UnitID"];
							dataRow10["Job"] = row5["JobID"];
							dataRow10.EndEdit();
							dataTable6.Rows.Add(dataRow10);
						}
					}
					dataGridGRNDetail.DataSource = dataTable6;
					DataTable dataTable7 = new DataSet().Tables.Add("Table");
					dataTable7.Columns.Add("Doc Number");
					dataTable7.Columns.Add("Date", typeof(DateTime));
					dataTable7.Columns.Add("Supplier");
					dataTable7.Columns.Add("Item Code");
					dataTable7.Columns.Add("Job");
					dataTable7.Columns.Add("Unit");
					dataTable7.Columns.Add("Quantity", typeof(decimal));
					dataTable7.Columns.Add("Price", typeof(decimal));
					dataTable7.Columns.Add("Amount", typeof(decimal));
					if (listData != null && listData.Tables.Count > 0)
					{
						DataView dataView6 = new DataView(listData.Tables[2]);
						new DataTable();
						foreach (DataRow row6 in dataView6.ToTable().Rows)
						{
							string a2 = row6["ProductID"].ToString();
							DataRow dataRow12 = dataTable7.NewRow();
							if (a2 != "")
							{
								dataRow12["Supplier"] = row6["SupplierID"];
							}
							else
							{
								dataRow12["Supplier"] = "None";
							}
							dataRow12["Doc Number"] = row6["VoucherID"];
							dataRow12["Date"] = row6["Date"];
							dataRow12["Item Code"] = row6["ProductID"];
							dataRow12["Quantity"] = row6["Quantity"];
							dataRow12["Price"] = row6["UnitPrice"];
							dataRow12["Amount"] = row6["Amount"];
							dataRow12["Job"] = row6["JobID"];
							dataRow12["Unit"] = row6["UnitID"];
							dataRow12.EndEdit();
							dataTable7.Rows.Add(dataRow12);
						}
					}
					dataGridPODetail.DataSource = dataTable7;
				}
				catch (SqlException ex)
				{
					listData = null;
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					listData = null;
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		private void ShowHideCostInfo()
		{
			panelCOGS.Visible = ShowCost;
			UltraGridColumn ultraGridColumn = dataGridDetail.DisplayLayout.Bands[0].Columns["AverageCost"];
			UltraGridColumn ultraGridColumn2 = dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn3 = dataGridDetail.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["Margin"];
			UltraGridColumn ultraGridColumn8 = dataGridCustomer.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn9 = dataGridCustomer.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn10 = dataGridCustomer.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn11 = dataGridCustomer.DisplayLayout.Bands[0].Columns["Margin"];
			UltraGridColumn ultraGridColumn12 = dataGridCategory.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn13 = dataGridCategory.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn14 = dataGridCategory.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn15 = dataGridCategory.DisplayLayout.Bands[0].Columns["Margin"];
			UltraGridColumn ultraGridColumn16 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn17 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn18 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["Profit"];
			bool flag2 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["Margin"].Hidden = !ShowCost;
			bool flag4 = ultraGridColumn18.Hidden = flag2;
			bool flag6 = ultraGridColumn17.Hidden = flag4;
			bool flag8 = ultraGridColumn16.Hidden = flag6;
			bool flag10 = ultraGridColumn15.Hidden = flag8;
			bool flag12 = ultraGridColumn14.Hidden = flag10;
			bool flag14 = ultraGridColumn13.Hidden = flag12;
			bool flag16 = ultraGridColumn12.Hidden = flag14;
			bool flag18 = ultraGridColumn11.Hidden = flag16;
			bool flag20 = ultraGridColumn10.Hidden = flag18;
			bool flag22 = ultraGridColumn9.Hidden = flag20;
			bool flag24 = ultraGridColumn8.Hidden = flag22;
			bool flag26 = ultraGridColumn7.Hidden = flag24;
			bool flag28 = ultraGridColumn6.Hidden = flag26;
			bool flag30 = ultraGridColumn5.Hidden = flag28;
			bool flag32 = ultraGridColumn4.Hidden = flag30;
			bool flag34 = ultraGridColumn3.Hidden = flag32;
			bool hidden = ultraGridColumn2.Hidden = flag34;
			ultraGridColumn.Hidden = hidden;
			ExcludeFromColumnChooser excludeFromColumnChooser = ExcludeFromColumnChooser.False;
			if (!ShowCost)
			{
				excludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			UltraGridColumn ultraGridColumn19 = dataGridDetail.DisplayLayout.Bands[0].Columns["AverageCost"];
			UltraGridColumn ultraGridColumn20 = dataGridDetail.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn21 = dataGridDetail.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn22 = dataGridItems.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn23 = dataGridItems.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn24 = dataGridItems.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn25 = dataGridItems.DisplayLayout.Bands[0].Columns["Margin"];
			UltraGridColumn ultraGridColumn26 = dataGridCustomer.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn27 = dataGridCustomer.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn28 = dataGridCustomer.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn29 = dataGridCustomer.DisplayLayout.Bands[0].Columns["Margin"];
			UltraGridColumn ultraGridColumn30 = dataGridCategory.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn31 = dataGridCategory.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn32 = dataGridCategory.DisplayLayout.Bands[0].Columns["Profit"];
			UltraGridColumn ultraGridColumn33 = dataGridCategory.DisplayLayout.Bands[0].Columns["Margin"];
			UltraGridColumn ultraGridColumn34 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["AvgCost"];
			UltraGridColumn ultraGridColumn35 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["COGS"];
			UltraGridColumn ultraGridColumn36 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["Profit"];
			ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridSalesperson.DisplayLayout.Bands[0].Columns["Margin"].ExcludeFromColumnChooser = excludeFromColumnChooser;
			ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn36.ExcludeFromColumnChooser = excludeFromColumnChooser3;
			ExcludeFromColumnChooser excludeFromColumnChooser7 = ultraGridColumn35.ExcludeFromColumnChooser = excludeFromColumnChooser5;
			ExcludeFromColumnChooser excludeFromColumnChooser9 = ultraGridColumn34.ExcludeFromColumnChooser = excludeFromColumnChooser7;
			ExcludeFromColumnChooser excludeFromColumnChooser11 = ultraGridColumn33.ExcludeFromColumnChooser = excludeFromColumnChooser9;
			ExcludeFromColumnChooser excludeFromColumnChooser13 = ultraGridColumn32.ExcludeFromColumnChooser = excludeFromColumnChooser11;
			ExcludeFromColumnChooser excludeFromColumnChooser15 = ultraGridColumn31.ExcludeFromColumnChooser = excludeFromColumnChooser13;
			ExcludeFromColumnChooser excludeFromColumnChooser17 = ultraGridColumn30.ExcludeFromColumnChooser = excludeFromColumnChooser15;
			ExcludeFromColumnChooser excludeFromColumnChooser19 = ultraGridColumn29.ExcludeFromColumnChooser = excludeFromColumnChooser17;
			ExcludeFromColumnChooser excludeFromColumnChooser21 = ultraGridColumn28.ExcludeFromColumnChooser = excludeFromColumnChooser19;
			ExcludeFromColumnChooser excludeFromColumnChooser23 = ultraGridColumn27.ExcludeFromColumnChooser = excludeFromColumnChooser21;
			ExcludeFromColumnChooser excludeFromColumnChooser25 = ultraGridColumn26.ExcludeFromColumnChooser = excludeFromColumnChooser23;
			ExcludeFromColumnChooser excludeFromColumnChooser27 = ultraGridColumn25.ExcludeFromColumnChooser = excludeFromColumnChooser25;
			ExcludeFromColumnChooser excludeFromColumnChooser29 = ultraGridColumn24.ExcludeFromColumnChooser = excludeFromColumnChooser27;
			ExcludeFromColumnChooser excludeFromColumnChooser31 = ultraGridColumn23.ExcludeFromColumnChooser = excludeFromColumnChooser29;
			ExcludeFromColumnChooser excludeFromColumnChooser33 = ultraGridColumn22.ExcludeFromColumnChooser = excludeFromColumnChooser31;
			ExcludeFromColumnChooser excludeFromColumnChooser35 = ultraGridColumn21.ExcludeFromColumnChooser = excludeFromColumnChooser33;
			ExcludeFromColumnChooser excludeFromColumnChooser38 = ultraGridColumn19.ExcludeFromColumnChooser = (ultraGridColumn20.ExcludeFromColumnChooser = excludeFromColumnChooser35);
		}

		private void HideShowColumns()
		{
			if (dataGridDetail.DataSource != null && dataGridDetail.DisplayLayout.Bands.Count != 0)
			{
				UltraGridColumn ultraGridColumn = dataGridDetail.DisplayLayout.Bands[0].Columns["VendorID"];
				UltraGridColumn ultraGridColumn2 = dataGridDetail.DisplayLayout.Bands[0].Columns["BuyerID"];
				UltraGridColumn ultraGridColumn3 = dataGridDetail.DisplayLayout.Bands[0].Columns["CurrencyID"];
				UltraGridColumn ultraGridColumn4 = dataGridDetail.DisplayLayout.Bands[0].Columns["CurrencyRate"];
				bool flag2 = dataGridDetail.DisplayLayout.Bands[0].Columns["Reference"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
			}
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OpenForEdit()
		{
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void CompanyAccountsListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					RadioButton radioButton = radioButtonVendor;
					vendorsFlatComboBox vendorsFlatComboBox = comboBoxVendor;
					TextBox textBox = textBoxVendor;
					bool flag2 = radioButtonVendor.Checked = ShowVendor;
					bool flag4 = textBox.Visible = flag2;
					bool visible = vendorsFlatComboBox.Visible = flag4;
					radioButton.Visible = visible;
					Init();
					LoadData();
					if (listData != null)
					{
						SetupList();
					}
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
				if (base.IsHandleCreated)
				{
					Close();
				}
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			if (canAccessCost)
			{
				checkBoxShowCost.Visible = true;
				checkBoxShowCOGS.Enabled = true;
				return;
			}
			checkBoxShowCost.Checked = false;
			checkBoxShowCost.Visible = false;
			checkBoxShowCOGS.Enabled = false;
			checkBoxShowCOGS.Checked = false;
		}

		public void OnActivated()
		{
		}

		private void CloseForm()
		{
			Close();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Accounts;
		}

		public static int GetScreenID()
		{
			return 7004;
		}

		private string GetSelectedID()
		{
			try
			{
				string result = "";
				if (dataGridDetail.ActiveRow != null && dataGridDetail.ActiveRow.Band.Index == 0)
				{
					if (dataGridDetail.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					if (dataGridDetail.ActiveRow.Cells.Exists("Doc Number"))
					{
						result = dataGridDetail.ActiveRow.Cells["Doc Number"].Text.ToString();
					}
					else if (dataGridDetail.ActiveRow.Cells.Exists("VoucherID"))
					{
						result = dataGridDetail.ActiveRow.Cells["VoucherID"].Text.ToString();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private string GetSelectedSysDocID()
		{
			try
			{
				string result = "";
				if (dataGridDetail.ActiveRow.Band.Index == 1)
				{
					return "";
				}
				if (dataGridDetail.ActiveRow != null)
				{
					if (dataGridDetail.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					if (dataGridDetail.ActiveRow.Cells.Exists("Doc ID"))
					{
						result = dataGridDetail.ActiveRow.Cells["Doc ID"].Text.ToString();
					}
					else if (dataGridDetail.ActiveRow.Cells.Exists("SysDocID"))
					{
						result = dataGridDetail.ActiveRow.Cells["SysDocID"].Text.ToString();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private UltraGridRow GetSelectedItem()
		{
			if (dataGridDetail.ActiveRow != null)
			{
				return dataGridDetail.ActiveRow;
			}
			return null;
		}

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Account List";
		}

		private void Print()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public bool SaveReport(ExternalReportTypes reportType)
		{
			return true;
		}

		public void ClearView()
		{
		}

		private void toolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void microsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataExportHelper dataExportHelper = new DataExportHelper();
			if (tabControlTab.SelectedTab == tabControlTab.Tabs["Details"])
			{
				dataExportHelper.ExportToExcel(dataGridDetail);
			}
			else if (tabControlTab.SelectedTab == tabControlTab.Tabs["Buyer"])
			{
				dataExportHelper.ExportToExcel(dataGridSalesperson);
			}
			if (tabControlTab.SelectedTab == tabControlTab.Tabs["Item"])
			{
				dataExportHelper.ExportToExcel(dataGridItems);
			}
			if (tabControlTab.SelectedTab == tabControlTab.Tabs["Vendor"])
			{
				dataExportHelper.ExportToExcel(dataGridCustomer);
			}
			if (tabControlTab.SelectedTab == tabControlTab.Tabs["Category"])
			{
				dataExportHelper.ExportToExcel(dataGridCategory);
			}
			if (tabControlTab.SelectedTab == tabControlTab.Tabs["PurchaseOrder"])
			{
				dataExportHelper.ExportToExcel(dataGridPODetail);
			}
			if (tabControlTab.SelectedTab == tabControlTab.Tabs["GRN"])
			{
				dataExportHelper.ExportToExcel(dataGridGRNDetail);
			}
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
			dataGridDetail.DisplayLayout.GroupByBox.Hidden = !toolStripButtonAllowGrouping.Checked;
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGridDetail.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridDetail.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			dataGridDetail.AutoSizeColumns();
		}

		private void toolStripButtonMerge_Click(object sender, EventArgs e)
		{
			if (toolStripButtonMerge.Checked)
			{
				dataGridDetail.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.OnlyWhenSorted;
			}
			else
			{
				dataGridDetail.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Never;
			}
		}

		private void radioButtonItem_CheckedChanged(object sender, EventArgs e)
		{
			textBoxCode.Enabled = radioButtonCode.Checked;
			ProductComboBox productComboBox = comboBoxItem;
			bool enabled = textBoxDescription.Enabled = radioButtonItem.Checked;
			productComboBox.Enabled = enabled;
		}

		private void checkBoxShowCost_CheckedChanged(object sender, EventArgs e)
		{
			ShowHideCostInfo();
		}

		private void checkBoxShowCOGS_CheckedChanged(object sender, EventArgs e)
		{
			chartControlPrice.Series["Cost"].Visible = checkBoxShowCOGS.Checked;
		}

		private void checkBoxShowVolume_CheckedChanged(object sender, EventArgs e)
		{
			chartControlPrice.Series["Volume"].Visible = checkBoxShowVolume.Checked;
			((XYDiagram2D)chartControlPrice.Diagram).Panes[0].Visible = checkBoxShowVolume.Checked;
			((XYDiagram)chartControlPrice.Diagram).AxisX.SetVisibilityInPane(!checkBoxShowVolume.Checked, ((XYDiagram)chartControlPrice.Diagram).DefaultPane);
			((XYDiagram)chartControlPrice.Diagram).SecondaryAxesY[0].Visible = checkBoxShowVolume.Checked;
		}
	}
}
