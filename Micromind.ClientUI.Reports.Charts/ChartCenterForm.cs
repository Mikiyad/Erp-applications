using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraNavBar;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraTab;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Charts
{
	public class ChartCenterForm : RibbonForm
	{
		private PivotData currentPivotData;

		private bool hideTotal;

		private IContainer components;

		private RibbonControl ribbon;

		private RibbonPage ribbonPagePivotReport;

		private RibbonPageGroup ribbonPageGroupEdit;

		private RibbonStatusBar ribbonStatusBar;

		private NavBarControl navBarGroups;

		private NavBarItem navBarItem11;

		private NavBarItem navBarItem12;

		private NavBarItem navBarItem13;

		private NavBarItem navBarItem14;

		private NavBarItem navBarItem15;

		private NavBarItem navBarItem16;

		private NavBarItem navBarItem17;

		private NavBarItem navBarItem18;

		private NavBarItem navBarItem19;

		private NavBarItem navBarItem20;

		private NavBarItem navBarItem21;

		private NavBarItem navBarItem22;

		private NavBarItem navBarItem23;

		private NavBarItem navBarItem24;

		private NavBarItem navBarItem25;

		private NavBarItem navBarItem26;

		private NavBarItem navBarItem1;

		private NavBarItem navBarItem2;

		private NavBarItem navBarItem3;

		private NavBarItem navBarItem4;

		private NavBarItem navBarItem5;

		private NavBarItem navBarItem6;

		private NavBarItem navBarItem7;

		private NavBarItem navBarItem8;

		private NavBarItem navBarItem9;

		private NavBarItem navBarItem10;

		private PanelControl panelControl1;

		private BarButtonItem barButtonItemNewGroup;

		private XtraTabControl tabControlMain;

		private XtraTabPage tabPageData;

		private BarButtonItem barButtonItem1;

		private BarButtonGroup barButtonGroup1;

		private BarButtonGroup barButtonGroup2;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItem4;

		private BarButtonItem barButtonItem5;

		private BarButtonItem barButtonItem6;

		private RibbonPageGroup ribbonPageGroupSave;

		private BarButtonItem barButtonItem2;

		private BarButtonItem barButtonEditReport;

		private RibbonPage ribbonPageChart;

		private BarButtonGroup barButtonGroup3;

		private RibbonPageGroup ribbonPageGroupDesign;

		private SplitContainer splitContainer1;

		private PivotGridControl pivotGridControlData;

		private ChartControl chartControlGraph;

		private BarCheckItem barItemSelectedRowsOnly;

		private RibbonPageGroup ribbonPageGroup3;

		private RibbonPageGroup ribbonPageGroup4;

		private BarButtonItem barButtonPrintReport;

		private BarButtonItem barButtonPrintChart;

		private BarButtonItem barButtonPrintPreview;

		private RibbonPageGroup ribbonPageGroup6;

		private BarCheckItem barItemHideFilterHeader;

		private BarSubItem barItemChartColor;

		private BarEditItem barItemTitle;

		private RepositoryItemTextEdit repositoryItemTextEdit1;

		private BarSubItem barItemChartTypes;

		private DefaultBarAndDockingController defaultBarAndDockingController1;

		private BarButtonItem barButtonItemFilter;

		private BarButtonGroup barButtonGroup5;

		private BarButtonItem barButtonItem8;

		private BarButtonItem barButtonItem9;

		private BarButtonItem barButtonItem10;

		private BarButtonGroup barButtonGroup4;

		private BarButtonItem barButtonItem7;

		private BarButtonGroup barButtonGroup6;

		private BarButtonItem barButtonItem11;

		private BarButtonItem barButtonItemExport;

		private BarButtonItem barButtonItemImport;

		private RibbonPage ribbonPageData;

		private RibbonPageGroup ribbonPageGroupImport;

		private RibbonPageGroup ribbonPageGroup5;

		private SaveFileDialog saveFileDialog1;

		private OpenFileDialog openFileDialog1;

		private BarButtonItem barButtonItemNewReport;

		private UltraExpandableGroupBox ultraExpandableGroupBox1;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;

		private Button buttonRefresh;

		private Micromind.DataControls.DateControl dateControlFilter;

		private BarButtonItem barButtonItemDelete;

		private BarButtonItem barButtonItemRefresh;

		private System.Windows.Forms.ProgressBar progressBarLoad;

		private Label lbWait;

		private Label labelViewListName;

		public ChartCenterForm()
		{
			InitializeComponent();
			base.Load += ChartCenterForm_Load;
			string[] paletteNames = chartControlGraph.GetPaletteNames();
			foreach (string caption in paletteNames)
			{
				BarButtonItem barButtonItem = new BarButtonItem();
				barButtonItem.Caption = caption;
				barButtonItem.ItemClick += barItemColor_ItemClick;
				barItemChartColor.AddItem(barButtonItem);
			}
			ViewType[] array = (ViewType[])Enum.GetValues(typeof(ViewType));
			for (int i = 0; i < array.Length; i++)
			{
				ViewType viewType = array[i];
				BarButtonItem barButtonItem2 = new BarButtonItem();
				barButtonItem2.Caption = viewType.ToString();
				barButtonItem2.Tag = (int)viewType;
				barButtonItem2.ItemClick += barItemViewType_ItemClick;
				barItemChartTypes.AddItem(barButtonItem2);
			}
			RegistryHelper registryHelper = new RegistryHelper();
			string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
			defaultBarAndDockingController1.Controller.LookAndFeel.SkinName = stringValue;
			pivotGridControlData.FieldAreaChanged += pivotGridControlData_FieldAreaChanged;
			pivotGridControlData.PopupMenuShowing += pivotGridControlData_PopupMenuShowing;
			pivotGridControlData.CustomFieldValueCells += PivotGridControlData_CustomFieldValueCells;
			barItemHideFilterHeader.Checked = true;
		}

		private void PivotGridControlData_CustomFieldValueCells(object sender, PivotCustomFieldValueCellsEventArgs e)
		{
		}

		private void pivotGridControlData_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (e.Field != null)
			{
				DXMenuItem dXMenuItem = new DXMenuItem("Intervals...");
				dXMenuItem.Tag = e.Field;
				dXMenuItem.Click += MenuItemInterval_Click;
				e.Menu.Items.Add(dXMenuItem);
			}
		}

		private void MenuItemInterval_Click(object sender, EventArgs e)
		{
			PivotGridField pivotGridField = (PivotGridField)((DXMenuItem)sender).Tag;
			if (pivotGridField != null)
			{
				SetFieldInterval setFieldInterval = new SetFieldInterval();
				setFieldInterval.SelectedItem = pivotGridField.GroupInterval;
				if (setFieldInterval.ShowDialog(this) == DialogResult.OK)
				{
					pivotGridField.GroupInterval = (PivotGroupInterval)setFieldInterval.SelectedItem;
				}
			}
		}

		private void pivotGridControlData_FieldAreaChanged(object sender, PivotFieldEventArgs e)
		{
			if (e.Field != null && e.Field.Area == PivotArea.DataArea)
			{
				e.Field.CellFormat.FormatType = FormatType.Numeric;
				e.Field.CellFormat.FormatString = "#,##0.00";
			}
		}

		private void barItemViewType_ItemClick(object sender, ItemClickEventArgs e)
		{
			ViewType viewType = (ViewType)int.Parse(e.Item.Tag.ToString());
			chartControlGraph.SeriesTemplate.ChangeView(viewType);
		}

		private void barItemColor_ItemClick(object sender, ItemClickEventArgs e)
		{
			chartControlGraph.PaletteName = e.Item.Caption;
		}

		private void ChartCenterForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupForm();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barButtonItemNewGroup_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (new PivotGroupDetailsForm().ShowDialog(this) == DialogResult.OK)
			{
				LoadReportsList();
			}
		}

		private void SetupForm()
		{
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditPivotReport))
			{
				ribbonPageGroupEdit.Visible = false;
				ribbonPageGroupSave.Visible = false;
				ribbonPageData.Visible = false;
			}
			LoadReportsList();
		}

		private void LoadReportsList()
		{
			try
			{
				DataSet pivotGroupList = Factory.PivotGroupSystem.GetPivotGroupList();
				navBarGroups.Groups.Clear();
				foreach (DataRow row in pivotGroupList.Tables["Pivot_Group"].Rows)
				{
					NavBarGroup navBarGroup = navBarGroups.Groups.Add();
					navBarGroup.Name = row["GroupID"].ToString();
					navBarGroup.Caption = row["GroupName"].ToString();
					navBarGroup.Tag = row["GroupID"].ToString();
				}
				pivotGroupList = Factory.PivotSystem.GetPivotList();
				if (pivotGroupList != null)
				{
					foreach (DataRow row2 in pivotGroupList.Tables[0].Rows)
					{
						string reportID = row2["PivotID"].ToString();
						if (Security.GetCustomReportAccessRight(CustomReportTypes.PivotReport, reportID).Visible)
						{
							NavBarItem navBarItem = new NavBarItem(row2["PivotName"].ToString());
							navBarItem.LinkClicked += item_LinkClicked;
							navBarItem.Tag = row2;
							if (navBarGroups.Groups[row2["GroupID"].ToString()] != null)
							{
								navBarGroups.Groups[row2["GroupID"].ToString()].ItemLinks.Add(navBarItem);
							}
						}
					}
				}
				if (!ribbonPageGroupEdit.Visible)
				{
					foreach (NavBarGroup group in navBarGroups.Groups)
					{
						if (group.VisibleItemLinks.Count == 0)
						{
							group.Visible = false;
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void item_LinkClicked(object sender, NavBarLinkEventArgs e)
		{
			LoadReport(retriveFields: true);
		}

		private void LoadReport(bool retriveFields)
		{
			try
			{
				NavBarItem item = navBarGroups.SelectedLink.Item;
				labelViewListName.Text = item.Caption;
				DataRow dataRow = item.Tag as DataRow;
				string id = dataRow["PivotID"].ToString();
				currentPivotData = Factory.PivotSystem.GetPivotByID(id);
				currentPivotData.PivotTable.Rows[0]["DataQuery"].ToString();
				bool.TryParse(currentPivotData.PivotTable.Rows[0]["HideTotal"].ToString(), out hideTotal);
				buttonRefresh.Enabled = false;
				Application.EnableVisualStyles();
				lbWait.Visible = true;
				progressBarLoad.Visible = true;
				progressBarLoad.Style = ProgressBarStyle.Marquee;
				progressBarLoad.MarqueeAnimationSpeed = 50;
				Refresh();
				DataSet reportByQuery = Factory.SmartListSystem.GetReportByQuery(dataRow["DataQuery"].ToString(), dateControlFilter.FromDate, dateControlFilter.ToDate);
				pivotGridControlData.DataSource = reportByQuery.Tables[0];
				pivotGridControlData.RefreshData();
				buttonRefresh.Enabled = true;
				lbWait.Visible = false;
				progressBarLoad.Visible = false;
				if (retriveFields)
				{
					pivotGridControlData.RetrieveFields();
					foreach (DataRow row in currentPivotData.PivotFieldTable.Rows)
					{
						PivotGridField fieldByName = pivotGridControlData.Fields.GetFieldByName(row["FieldName"].ToString());
						if (fieldByName != null)
						{
							fieldByName.Area = (PivotArea)int.Parse(row["Area"].ToString());
							if (row["AreaIndex"] != DBNull.Value)
							{
								fieldByName.AreaIndex = Convert.ToInt16(row["AreaIndex"].ToString());
							}
							if (row["GroupInterval"] != DBNull.Value)
							{
								fieldByName.GroupInterval = (PivotGroupInterval)int.Parse(row["GroupInterval"].ToString());
							}
							if (row["DisplayName"] != DBNull.Value && row["DisplayName"].ToString() != "")
							{
								fieldByName.Caption = row["DisplayName"].ToString();
							}
							if (fieldByName.Area == PivotArea.DataArea)
							{
								fieldByName.CellFormat.FormatString = Format.GridAmountFormat;
							}
						}
					}
					chartControlGraph.DataSource = pivotGridControlData;
					chartControlGraph.PivotGridDataSourceOptions.SelectionOnly = barItemSelectedRowsOnly.Checked;
					chartControlGraph.PivotGridDataSourceOptions.MaxAllowedSeriesCount = 100;
				}
				chartControlGraph.RefreshData();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void barButtonNewPivot_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (new PivotDetailsForm().ShowDialog() == DialogResult.OK)
			{
				LoadReportsList();
			}
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (GetData())
				{
					Factory.PivotSystem.InsertUpdatePivot(currentPivotData, isUpdate: true);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						currentPivotData.AcceptChanges();
						currentPivotData.PivotTable.Rows[0].SetAdded();
						currentPivotData.PivotTable.Rows[0]["PivotID"] = enterNameDialog.EnteredCode;
						currentPivotData.PivotTable.Rows[0]["PivotName"] = enterNameDialog.EnteredName;
						foreach (DataRow row in currentPivotData.PivotFieldTable.Rows)
						{
							row.SetAdded();
							row["PivotID"] = enterNameDialog.EnteredCode;
						}
						if (Factory.PivotSystem.InsertUpdatePivot(currentPivotData, isUpdate: false))
						{
							LoadReportsList();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool GetData()
		{
			if (currentPivotData == null)
			{
				return false;
			}
			DataRow dataRow = currentPivotData.PivotTable.Rows[0];
			currentPivotData.PivotFieldTable.Rows.Clear();
			foreach (PivotGridField field in pivotGridControlData.Fields)
			{
				DataRow dataRow2 = currentPivotData.PivotFieldTable.NewRow();
				_ = field.Index;
				dataRow2["PivotID"] = dataRow["PivotID"].ToString();
				dataRow2["FieldName"] = field.Name;
				dataRow2["Area"] = (int)field.Area;
				dataRow2["AreaIndex"] = field.AreaIndex;
				dataRow2["GroupInterval"] = (int)field.GroupInterval;
				currentPivotData.PivotFieldTable.Rows.Add(dataRow2);
			}
			return true;
		}

		private void barButtonEditReport_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (currentPivotData != null && currentPivotData.Tables.Count != 0)
			{
				PivotDetailsForm pivotDetailsForm = new PivotDetailsForm();
				pivotDetailsForm.LoadData(currentPivotData.PivotTable.Rows[0]["PivotID"].ToString());
				if (pivotDetailsForm.ShowDialog() == DialogResult.OK)
				{
					LoadReportsList();
				}
			}
		}

		private void tabControlMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
		}

		private void barItemSelectedRowsOnly_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			chartControlGraph.PivotGridDataSourceOptions.SelectionOnly = barItemSelectedRowsOnly.Checked;
		}

		private void barButtonPrintReport_ItemClick(object sender, ItemClickEventArgs e)
		{
			pivotGridControlData.ShowRibbonPrintPreview();
		}

		private void barButtonPrintChart_ItemClick(object sender, ItemClickEventArgs e)
		{
			chartControlGraph.ShowRibbonPrintPreview();
		}

		private void barButtonHideFilter_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void barItemHideFilterHeader_CheckedChanged(object sender, ItemClickEventArgs e)
		{
			pivotGridControlData.OptionsView.ShowFilterHeaders = !barItemHideFilterHeader.Checked;
		}

		private void barItemTitle_EditValueChanged(object sender, EventArgs e)
		{
			if (chartControlGraph.Titles.Count == 0)
			{
				chartControlGraph.Titles.Add(new ChartTitle());
			}
			chartControlGraph.Titles[0].Text = barItemTitle.EditValue.ToString();
		}

		private void barButtonItemFilter_ItemClick(object sender, ItemClickEventArgs e)
		{
			pivotGridControlData.Prefilter.ChangePrefilterVisible();
		}

		private void barButtonItemExport_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (currentPivotData != null && currentPivotData.Tables.Count != 0)
				{
					saveFileDialog1.AddExtension = true;
					saveFileDialog1.Filter = "Axolon Pivot Reports|*.axp";
					saveFileDialog1.FileName = currentPivotData.PivotTable.Rows[0]["PivotID"].ToString();
					if (saveFileDialog1.ShowDialog() == DialogResult.OK && currentPivotData != null)
					{
						currentPivotData.WriteXml(saveFileDialog1.FileName, XmlWriteMode.WriteSchema);
						ErrorHelper.InformationMessage("Pivot report exported successfully.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barButtonItemImport_ItemClick(object sender, ItemClickEventArgs e)
		{
			try
			{
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					currentPivotData = new PivotData();
					currentPivotData.ReadXml(openFileDialog1.FileName, XmlReadMode.ReadSchema);
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					enterNameDialog.EnteredCode = currentPivotData.PivotTable.Rows[0]["PivotID"].ToString();
					enterNameDialog.EnteredName = currentPivotData.PivotTable.Rows[0]["PivotName"].ToString();
					enterNameDialog.SelectedGroup = currentPivotData.PivotTable.Rows[0]["GroupID"].ToString();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						currentPivotData.PivotTable.Rows[0]["PivotID"] = enterNameDialog.EnteredCode;
						currentPivotData.PivotTable.Rows[0]["PivotName"] = enterNameDialog.EnteredName;
						currentPivotData.PivotTable.Rows[0]["GroupID"] = enterNameDialog.SelectedGroup;
						currentPivotData.AcceptChanges();
						currentPivotData.PivotTable.Rows[0].SetAdded();
						foreach (DataRow row in currentPivotData.PivotFieldTable.Rows)
						{
							row.SetAdded();
							row["PivotID"] = enterNameDialog.EnteredCode;
						}
						if (Factory.PivotSystem.InsertUpdatePivot(currentPivotData, isUpdate: false))
						{
							ErrorHelper.InformationMessage("Pivot report imported successfully.");
							LoadReportsList();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadReport(retriveFields: false);
		}

		private void barButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (currentPivotData != null && currentPivotData.Tables.Count != 0 && ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this report?") == DialogResult.Yes)
			{
				string iD = currentPivotData.PivotTable.Rows[0]["PivotID"].ToString();
				if (Factory.PivotSystem.DeletePivot(iD))
				{
					LoadReportsList();
					LoadReport(retriveFields: true);
				}
			}
		}

		private void barButtonItemRefresh_ItemClick(object sender, ItemClickEventArgs e)
		{
			LoadReportsList();
		}

		private void chartControlGraph_CustomDrawSeries(object sender, CustomDrawSeriesEventArgs e)
		{
		}

		private void chartControlGraph_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
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
			components = new System.ComponentModel.Container();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Charts.ChartCenterForm));
			ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			barButtonItemNewGroup = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			barButtonEditReport = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup3 = new DevExpress.XtraBars.BarButtonGroup();
			barItemSelectedRowsOnly = new DevExpress.XtraBars.BarCheckItem();
			barButtonPrintReport = new DevExpress.XtraBars.BarButtonItem();
			barButtonPrintChart = new DevExpress.XtraBars.BarButtonItem();
			barItemHideFilterHeader = new DevExpress.XtraBars.BarCheckItem();
			barItemChartColor = new DevExpress.XtraBars.BarSubItem();
			barItemTitle = new DevExpress.XtraBars.BarEditItem();
			repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			barItemChartTypes = new DevExpress.XtraBars.BarSubItem();
			barButtonItemFilter = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup5 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup4 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup6 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemExport = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemImport = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemNewReport = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
			ribbonPagePivotReport = new DevExpress.XtraBars.Ribbon.RibbonPage();
			ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageGroupSave = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageGroupDesign = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageChart = new DevExpress.XtraBars.Ribbon.RibbonPage();
			ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonPageData = new DevExpress.XtraBars.Ribbon.RibbonPage();
			ribbonPageGroupImport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
			navBarGroups = new DevExpress.XtraNavBar.NavBarControl();
			navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem5 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem6 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem7 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem8 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem9 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem10 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem11 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem12 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem13 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem14 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem15 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem16 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem17 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem18 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem19 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem20 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem21 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem22 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem23 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem24 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem25 = new DevExpress.XtraNavBar.NavBarItem();
			navBarItem26 = new DevExpress.XtraNavBar.NavBarItem();
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			tabControlMain = new DevExpress.XtraTab.XtraTabControl();
			tabPageData = new DevExpress.XtraTab.XtraTabPage();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			pivotGridControlData = new DevExpress.XtraPivotGrid.PivotGridControl();
			chartControlGraph = new DevExpress.XtraCharts.ChartControl();
			ultraExpandableGroupBox1 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			progressBarLoad = new System.Windows.Forms.ProgressBar();
			buttonRefresh = new System.Windows.Forms.Button();
			lbWait = new System.Windows.Forms.Label();
			dateControlFilter = new Micromind.DataControls.DateControl();
			barButtonPrintPreview = new DevExpress.XtraBars.BarButtonItem();
			defaultBarAndDockingController1 = new DevExpress.XtraBars.DefaultBarAndDockingController(components);
			ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			labelViewListName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)navBarGroups).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tabControlMain).BeginInit();
			tabControlMain.SuspendLayout();
			tabPageData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pivotGridControlData).BeginInit();
			((System.ComponentModel.ISupportInitialize)chartControlGraph).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBox1).BeginInit();
			ultraExpandableGroupBox1.SuspendLayout();
			ultraExpandableGroupBoxPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)defaultBarAndDockingController1.Controller).BeginInit();
			SuspendLayout();
			ribbon.ApplicationButtonImageOptions.Image = Micromind.ClientUI.Properties.Resources.axolon_48;
			ribbon.ExpandCollapseItem.Id = 0;
			ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[33]
			{
				ribbon.ExpandCollapseItem,
				barButtonItemNewGroup,
				barButtonItem1,
				barButtonGroup1,
				barButtonGroup2,
				barButtonItem3,
				barButtonItem4,
				barButtonItem5,
				barButtonItem6,
				barButtonItem2,
				barButtonEditReport,
				barButtonGroup3,
				barItemSelectedRowsOnly,
				barButtonPrintReport,
				barButtonPrintChart,
				barItemHideFilterHeader,
				barItemChartColor,
				barItemTitle,
				barItemChartTypes,
				barButtonItemFilter,
				barButtonGroup5,
				barButtonItem8,
				barButtonItem9,
				barButtonItem10,
				barButtonGroup4,
				barButtonItem7,
				barButtonGroup6,
				barButtonItem11,
				barButtonItemExport,
				barButtonItemImport,
				barButtonItemNewReport,
				barButtonItemDelete,
				barButtonItemRefresh
			});
			ribbon.Location = new System.Drawing.Point(0, 0);
			ribbon.MaxItemId = 43;
			ribbon.Name = "ribbon";
			ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[3]
			{
				ribbonPagePivotReport,
				ribbonPageChart,
				ribbonPageData
			});
			ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1]
			{
				repositoryItemTextEdit1
			});
			ribbon.Size = new System.Drawing.Size(1081, 143);
			ribbon.StatusBar = ribbonStatusBar;
			barButtonItemNewGroup.Caption = "New Group";
			barButtonItemNewGroup.Id = 1;
			barButtonItemNewGroup.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.newfolder;
			barButtonItemNewGroup.Name = "barButtonItemNewGroup";
			barButtonItemNewGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemNewGroup_ItemClick);
			barButtonItem1.Caption = "barButtonItem1";
			barButtonItem1.Id = 3;
			barButtonItem1.Name = "barButtonItem1";
			barButtonGroup1.Caption = "barButtonGroup1";
			barButtonGroup1.Id = 4;
			barButtonGroup1.Name = "barButtonGroup1";
			barButtonGroup2.Caption = "barButtonGroup2";
			barButtonGroup2.Id = 5;
			barButtonGroup2.Name = "barButtonGroup2";
			barButtonItem3.Caption = "barButtonItem3";
			barButtonItem3.Id = 6;
			barButtonItem3.Name = "barButtonItem3";
			barButtonItem4.Caption = "barButtonItem4";
			barButtonItem4.Id = 7;
			barButtonItem4.Name = "barButtonItem4";
			barButtonItem5.Caption = "barButtonItem5";
			barButtonItem5.Id = 8;
			barButtonItem5.Name = "barButtonItem5";
			barButtonItem6.Caption = "Save As...";
			barButtonItem6.Id = 9;
			barButtonItem6.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Save_as;
			barButtonItem6.Name = "barButtonItem6";
			barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem6_ItemClick);
			barButtonItem2.Caption = "Save";
			barButtonItem2.Id = 10;
			barButtonItem2.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.saveicon;
			barButtonItem2.Name = "barButtonItem2";
			barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem2_ItemClick);
			barButtonEditReport.Caption = "Edit Report";
			barButtonEditReport.Id = 11;
			barButtonEditReport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.editdoc;
			barButtonEditReport.Name = "barButtonEditReport";
			barButtonEditReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonEditReport_ItemClick);
			barButtonGroup3.Caption = "barButtonGroup3";
			barButtonGroup3.Id = 12;
			barButtonGroup3.Name = "barButtonGroup3";
			barItemSelectedRowsOnly.BindableChecked = true;
			barItemSelectedRowsOnly.Caption = "Selection Only";
			barItemSelectedRowsOnly.Checked = true;
			barItemSelectedRowsOnly.Id = 15;
			barItemSelectedRowsOnly.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.selectedrows;
			barItemSelectedRowsOnly.Name = "barItemSelectedRowsOnly";
			barItemSelectedRowsOnly.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(barItemSelectedRowsOnly_CheckedChanged);
			barButtonPrintReport.Caption = "Print Report";
			barButtonPrintReport.Id = 17;
			barButtonPrintReport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.print;
			barButtonPrintReport.Name = "barButtonPrintReport";
			barButtonPrintReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonPrintReport_ItemClick);
			barButtonPrintChart.Caption = "Print Chart";
			barButtonPrintChart.Id = 18;
			barButtonPrintChart.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.chart_1;
			barButtonPrintChart.Name = "barButtonPrintChart";
			barButtonPrintChart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonPrintChart_ItemClick);
			barItemHideFilterHeader.Caption = "Hide Fields Panel";
			barItemHideFilterHeader.Id = 21;
			barItemHideFilterHeader.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.columns;
			barItemHideFilterHeader.Name = "barItemHideFilterHeader";
			barItemHideFilterHeader.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(barItemHideFilterHeader_CheckedChanged);
			barItemChartColor.Caption = "Colors";
			barItemChartColor.Id = 22;
			barItemChartColor.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.colorpalet;
			barItemChartColor.Name = "barItemChartColor";
			barItemTitle.Caption = "Chart Title:";
			barItemTitle.Edit = repositoryItemTextEdit1;
			barItemTitle.EditWidth = 150;
			barItemTitle.Id = 25;
			barItemTitle.Name = "barItemTitle";
			barItemTitle.EditValueChanged += new System.EventHandler(barItemTitle_EditValueChanged);
			repositoryItemTextEdit1.AutoHeight = false;
			repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			barItemChartTypes.Caption = "Chart Type";
			barItemChartTypes.Id = 26;
			barItemChartTypes.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Chart_48;
			barItemChartTypes.Name = "barItemChartTypes";
			barButtonItemFilter.Caption = "Filter";
			barButtonItemFilter.Id = 27;
			barButtonItemFilter.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.filter;
			barButtonItemFilter.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.filter;
			barButtonItemFilter.Name = "barButtonItemFilter";
			barButtonItemFilter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemFilter_ItemClick);
			barButtonGroup5.Caption = "barButtonGroup5";
			barButtonGroup5.Id = 30;
			barButtonGroup5.Name = "barButtonGroup5";
			barButtonItem8.Caption = "barButtonItem8";
			barButtonItem8.Id = 31;
			barButtonItem8.Name = "barButtonItem8";
			barButtonItem9.Caption = "barButtonItem9";
			barButtonItem9.Id = 32;
			barButtonItem9.Name = "barButtonItem9";
			barButtonItem10.Caption = "barButtonItem10";
			barButtonItem10.Id = 33;
			barButtonItem10.Name = "barButtonItem10";
			barButtonGroup4.Caption = "barButtonGroup4";
			barButtonGroup4.Id = 34;
			barButtonGroup4.Name = "barButtonGroup4";
			barButtonItem7.Caption = "barButtonItem7";
			barButtonItem7.Id = 35;
			barButtonItem7.Name = "barButtonItem7";
			barButtonGroup6.Caption = "barButtonGroup6";
			barButtonGroup6.Id = 36;
			barButtonGroup6.Name = "barButtonGroup6";
			barButtonItem11.Caption = "barButtonItem11";
			barButtonItem11.Id = 37;
			barButtonItem11.Name = "barButtonItem11";
			barButtonItemExport.Caption = "Export...";
			barButtonItemExport.Id = 38;
			barButtonItemExport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.ExportToXMLFile48;
			barButtonItemExport.Name = "barButtonItemExport";
			barButtonItemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemExport_ItemClick);
			barButtonItemImport.Caption = "Import...";
			barButtonItemImport.Id = 39;
			barButtonItemImport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.download_icon;
			barButtonItemImport.Name = "barButtonItemImport";
			barButtonItemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemImport_ItemClick);
			barButtonItemNewReport.Caption = "New Report";
			barButtonItemNewReport.Id = 40;
			barButtonItemNewReport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.newfile;
			barButtonItemNewReport.Name = "barButtonItemNewReport";
			barButtonItemNewReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonNewPivot_ItemClick);
			barButtonItemDelete.Caption = "Delete";
			barButtonItemDelete.Id = 41;
			barButtonItemDelete.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.DeleteRed;
			barButtonItemDelete.Name = "barButtonItemDelete";
			barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemDelete_ItemClick);
			barButtonItemRefresh.Caption = "Refresh";
			barButtonItemRefresh.Id = 42;
			barButtonItemRefresh.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Circulation;
			barButtonItemRefresh.Name = "barButtonItemRefresh";
			barButtonItemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItemRefresh_ItemClick);
			ribbonPagePivotReport.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[4]
			{
				ribbonPageGroupEdit,
				ribbonPageGroupSave,
				ribbonPageGroupDesign,
				ribbonPageGroup6
			});
			ribbonPagePivotReport.Name = "ribbonPagePivotReport";
			ribbonPagePivotReport.Text = "Report";
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemRefresh);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemNewGroup);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemNewReport);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonEditReport);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemDelete);
			ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
			ribbonPageGroupEdit.Text = "Edit";
			ribbonPageGroupSave.ItemLinks.Add(barButtonItem6);
			ribbonPageGroupSave.ItemLinks.Add(barButtonItem2);
			ribbonPageGroupSave.Name = "ribbonPageGroupSave";
			ribbonPageGroupSave.Text = "Save";
			ribbonPageGroupDesign.ItemLinks.Add(barButtonPrintReport);
			ribbonPageGroupDesign.ItemLinks.Add(barButtonPrintChart);
			ribbonPageGroupDesign.Name = "ribbonPageGroupDesign";
			ribbonPageGroupDesign.Text = "Print";
			ribbonPageGroup6.ItemLinks.Add(barItemHideFilterHeader);
			ribbonPageGroup6.ItemLinks.Add(barButtonItemFilter);
			ribbonPageGroup6.Name = "ribbonPageGroup6";
			ribbonPageGroup6.Text = "View";
			ribbonPageChart.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[2]
			{
				ribbonPageGroup3,
				ribbonPageGroup4
			});
			ribbonPageChart.Name = "ribbonPageChart";
			ribbonPageChart.Text = "Chart";
			ribbonPageGroup3.ItemLinks.Add(barItemChartColor);
			ribbonPageGroup3.ItemLinks.Add(barItemChartTypes);
			ribbonPageGroup3.ItemLinks.Add(barItemTitle);
			ribbonPageGroup3.Name = "ribbonPageGroup3";
			ribbonPageGroup3.Text = "View";
			ribbonPageGroup4.ItemLinks.Add(barItemSelectedRowsOnly);
			ribbonPageGroup4.Name = "ribbonPageGroup4";
			ribbonPageGroup4.Text = "Data";
			ribbonPageData.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1]
			{
				ribbonPageGroupImport
			});
			ribbonPageData.Name = "ribbonPageData";
			ribbonPageData.Text = "Data";
			ribbonPageGroupImport.ItemLinks.Add(barButtonItemExport);
			ribbonPageGroupImport.ItemLinks.Add(barButtonItemImport);
			ribbonPageGroupImport.Name = "ribbonPageGroupImport";
			ribbonPageGroupImport.Text = "Import && Export";
			ribbonStatusBar.Location = new System.Drawing.Point(0, 643);
			ribbonStatusBar.Name = "ribbonStatusBar";
			ribbonStatusBar.Ribbon = ribbon;
			ribbonStatusBar.Size = new System.Drawing.Size(1081, 31);
			navBarGroups.Dock = System.Windows.Forms.DockStyle.Left;
			navBarGroups.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[26]
			{
				navBarItem1,
				navBarItem2,
				navBarItem3,
				navBarItem4,
				navBarItem5,
				navBarItem6,
				navBarItem7,
				navBarItem8,
				navBarItem9,
				navBarItem10,
				navBarItem11,
				navBarItem12,
				navBarItem13,
				navBarItem14,
				navBarItem15,
				navBarItem16,
				navBarItem17,
				navBarItem18,
				navBarItem19,
				navBarItem20,
				navBarItem21,
				navBarItem22,
				navBarItem23,
				navBarItem24,
				navBarItem25,
				navBarItem26
			});
			navBarGroups.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInControl;
			navBarGroups.Location = new System.Drawing.Point(0, 143);
			navBarGroups.Name = "navBarGroups";
			navBarGroups.OptionsNavPane.ExpandedWidth = 243;
			navBarGroups.Size = new System.Drawing.Size(243, 500);
			navBarGroups.TabIndex = 2;
			navBarGroups.Text = "navBarControl1";
			navBarItem1.Caption = "navBarItem1";
			navBarItem1.Name = "navBarItem1";
			navBarItem2.Caption = "navBarItem2";
			navBarItem2.Name = "navBarItem2";
			navBarItem3.Caption = "navBarItem3";
			navBarItem3.Name = "navBarItem3";
			navBarItem4.Caption = "navBarItem4";
			navBarItem4.Name = "navBarItem4";
			navBarItem5.Caption = "navBarItem5";
			navBarItem5.Name = "navBarItem5";
			navBarItem6.Caption = "navBarItem6";
			navBarItem6.Name = "navBarItem6";
			navBarItem7.Caption = "navBarItem7";
			navBarItem7.Name = "navBarItem7";
			navBarItem8.Caption = "navBarItem8";
			navBarItem8.Name = "navBarItem8";
			navBarItem9.Caption = "navBarItem9";
			navBarItem9.Name = "navBarItem9";
			navBarItem10.Caption = "navBarItem10";
			navBarItem10.Name = "navBarItem10";
			navBarItem11.Caption = "navBarItem11";
			navBarItem11.Name = "navBarItem11";
			navBarItem12.Caption = "navBarItem12";
			navBarItem12.Name = "navBarItem12";
			navBarItem13.Caption = "navBarItem13";
			navBarItem13.Name = "navBarItem13";
			navBarItem14.Caption = "navBarItem14";
			navBarItem14.Name = "navBarItem14";
			navBarItem15.Caption = "navBarItem15";
			navBarItem15.Name = "navBarItem15";
			navBarItem16.Caption = "navBarItem16";
			navBarItem16.Name = "navBarItem16";
			navBarItem17.Caption = "navBarItem17";
			navBarItem17.Name = "navBarItem17";
			navBarItem18.Caption = "navBarItem18";
			navBarItem18.Name = "navBarItem18";
			navBarItem19.Caption = "navBarItem19";
			navBarItem19.Name = "navBarItem19";
			navBarItem20.Caption = "navBarItem20";
			navBarItem20.Name = "navBarItem20";
			navBarItem21.Caption = "navBarItem21";
			navBarItem21.Name = "navBarItem21";
			navBarItem22.Caption = "navBarItem22";
			navBarItem22.Name = "navBarItem22";
			navBarItem23.Caption = "navBarItem23";
			navBarItem23.Name = "navBarItem23";
			navBarItem24.Caption = "navBarItem24";
			navBarItem24.Name = "navBarItem24";
			navBarItem25.Caption = "navBarItem25";
			navBarItem25.Name = "navBarItem25";
			navBarItem26.Caption = "navBarItem26";
			navBarItem26.Name = "navBarItem26";
			panelControl1.Controls.Add(tabControlMain);
			panelControl1.Controls.Add(ultraExpandableGroupBox1);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			panelControl1.Location = new System.Drawing.Point(243, 143);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(838, 500);
			panelControl1.TabIndex = 3;
			tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControlMain.Location = new System.Drawing.Point(2, 78);
			tabControlMain.Name = "tabControlMain";
			tabControlMain.SelectedTabPage = tabPageData;
			tabControlMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			tabControlMain.Size = new System.Drawing.Size(834, 420);
			tabControlMain.TabIndex = 0;
			tabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[1]
			{
				tabPageData
			});
			tabControlMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(tabControlMain_SelectedPageChanged);
			tabPageData.Controls.Add(splitContainer1);
			tabPageData.Name = "tabPageData";
			tabPageData.Size = new System.Drawing.Size(828, 414);
			tabPageData.Text = "Pivot";
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			splitContainer1.Panel1.Controls.Add(pivotGridControlData);
			splitContainer1.Panel2.Controls.Add(chartControlGraph);
			splitContainer1.Size = new System.Drawing.Size(828, 414);
			splitContainer1.SplitterDistance = 239;
			splitContainer1.TabIndex = 1;
			pivotGridControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			pivotGridControlData.Location = new System.Drawing.Point(0, 0);
			pivotGridControlData.Name = "pivotGridControlData";
			pivotGridControlData.Size = new System.Drawing.Size(828, 239);
			pivotGridControlData.TabIndex = 1;
			chartControlGraph.Dock = System.Windows.Forms.DockStyle.Fill;
			chartControlGraph.Legend.Name = "Default Legend";
			chartControlGraph.Location = new System.Drawing.Point(0, 0);
			chartControlGraph.Name = "chartControlGraph";
			chartControlGraph.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
			sideBySideBarSeriesLabel.LineLength = 12;
			sideBySideBarSeriesLabel.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			chartControlGraph.SeriesTemplate.Label = sideBySideBarSeriesLabel;
			chartControlGraph.Size = new System.Drawing.Size(828, 171);
			chartControlGraph.TabIndex = 1;
			chartControlGraph.CustomDrawSeries += new DevExpress.XtraCharts.CustomDrawSeriesEventHandler(chartControlGraph_CustomDrawSeries);
			chartControlGraph.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(chartControlGraph_CustomDrawSeriesPoint);
			ultraExpandableGroupBox1.Controls.Add(ultraExpandableGroupBoxPanel1);
			ultraExpandableGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			ultraExpandableGroupBox1.ExpandedSize = new System.Drawing.Size(834, 76);
			ultraExpandableGroupBox1.Location = new System.Drawing.Point(2, 2);
			ultraExpandableGroupBox1.Name = "ultraExpandableGroupBox1";
			ultraExpandableGroupBox1.Size = new System.Drawing.Size(834, 76);
			ultraExpandableGroupBox1.TabIndex = 293;
			ultraExpandableGroupBox1.Text = "Filter";
			ultraExpandableGroupBoxPanel1.AutoSize = true;
			ultraExpandableGroupBoxPanel1.Controls.Add(labelViewListName);
			ultraExpandableGroupBoxPanel1.Controls.Add(progressBarLoad);
			ultraExpandableGroupBoxPanel1.Controls.Add(buttonRefresh);
			ultraExpandableGroupBoxPanel1.Controls.Add(lbWait);
			ultraExpandableGroupBoxPanel1.Controls.Add(dateControlFilter);
			ultraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(3, 19);
			ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(828, 54);
			ultraExpandableGroupBoxPanel1.TabIndex = 0;
			progressBarLoad.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			progressBarLoad.Location = new System.Drawing.Point(433, 30);
			progressBarLoad.Name = "progressBarLoad";
			progressBarLoad.Size = new System.Drawing.Size(100, 13);
			progressBarLoad.TabIndex = 296;
			progressBarLoad.Visible = false;
			buttonRefresh.Location = new System.Drawing.Point(283, 26);
			buttonRefresh.Name = "buttonRefresh";
			buttonRefresh.Size = new System.Drawing.Size(70, 21);
			buttonRefresh.TabIndex = 292;
			buttonRefresh.Text = "Refresh";
			buttonRefresh.UseVisualStyleBackColor = true;
			buttonRefresh.Click += new System.EventHandler(buttonRefresh_Click);
			lbWait.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			lbWait.AutoSize = true;
			lbWait.Location = new System.Drawing.Point(359, 30);
			lbWait.Name = "lbWait";
			lbWait.Size = new System.Drawing.Size(73, 13);
			lbWait.TabIndex = 295;
			lbWait.Text = "Please wait...";
			lbWait.Visible = false;
			dateControlFilter.BackColor = System.Drawing.Color.Transparent;
			dateControlFilter.CustomReportFieldName = "";
			dateControlFilter.CustomReportKey = "";
			dateControlFilter.CustomReportValueType = 1;
			dateControlFilter.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControlFilter.Location = new System.Drawing.Point(3, 2);
			dateControlFilter.Name = "dateControlFilter";
			dateControlFilter.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlFilter.Size = new System.Drawing.Size(274, 49);
			dateControlFilter.TabIndex = 291;
			dateControlFilter.ToDate = new System.DateTime(2017, 10, 9, 23, 59, 59, 59);
			barButtonPrintPreview.Caption = "Print Preview";
			barButtonPrintPreview.Id = 14;
			barButtonPrintPreview.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Print_preview;
			barButtonPrintPreview.Name = "barButtonPrintPreview";
			defaultBarAndDockingController1.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
			ribbonPageGroup5.ItemLinks.Add(barItemSelectedRowsOnly);
			ribbonPageGroup5.Name = "ribbonPageGroup5";
			ribbonPageGroup5.Text = "Data";
			saveFileDialog1.DefaultExt = "axp";
			saveFileDialog1.FileName = "pivot report";
			openFileDialog1.DefaultExt = "axp";
			openFileDialog1.FileName = "openFileDialog1";
			openFileDialog1.Filter = "Axolon Pivot Reports|*.axp";
			labelViewListName.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelViewListName.BackColor = System.Drawing.Color.FromArgb(235, 236, 239);
			labelViewListName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelViewListName.ForeColor = System.Drawing.Color.FromArgb(32, 31, 53);
			labelViewListName.Location = new System.Drawing.Point(373, -4);
			labelViewListName.Name = "labelViewListName";
			labelViewListName.Size = new System.Drawing.Size(200, 18);
			labelViewListName.TabIndex = 298;
			labelViewListName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1081, 674);
			base.Controls.Add(panelControl1);
			base.Controls.Add(navBarGroups);
			base.Controls.Add(ribbonStatusBar);
			base.Controls.Add(ribbon);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ChartCenterForm";
			Ribbon = ribbon;
			StatusBar = ribbonStatusBar;
			Text = "Pivot Report Center";
			((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)navBarGroups).EndInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)tabControlMain).EndInit();
			tabControlMain.ResumeLayout(false);
			tabPageData.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pivotGridControlData).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)chartControlGraph).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBox1).EndInit();
			ultraExpandableGroupBox1.ResumeLayout(false);
			ultraExpandableGroupBox1.PerformLayout();
			ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)defaultBarAndDockingController1.Controller).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
