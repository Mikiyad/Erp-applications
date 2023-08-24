using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataSync
{
	public class DataSyncForm : RibbonForm
	{
		private IContainer components;

		private BarButtonItem barButtonPrintPreview;

		private DefaultBarAndDockingController defaultBarAndDockingController1;

		private RibbonPageGroup ribbonPageGroup5;

		private SaveFileDialog saveFileDialog1;

		private OpenFileDialog openFileDialog1;

		private RibbonControl ribbon;

		private BarButtonItem barButtonItemNewGroup;

		private BarButtonItem barButtonItem1;

		private BarButtonGroup barButtonGroup1;

		private BarButtonGroup barButtonGroup2;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItem4;

		private BarButtonItem barButtonItem5;

		private BarButtonItem barButtonItem6;

		private BarButtonItem barButtonItem2;

		private BarButtonItem barButtonLoadSync;

		private BarButtonGroup barButtonGroup3;

		private BarCheckItem barItemSelectedRowsOnly;

		private BarButtonItem barButtonPrintReport;

		private BarButtonItem barButtonPrintChart;

		private BarCheckItem barItemHideFilterHeader;

		private BarSubItem barItemChartColor;

		private BarEditItem barItemTitle;

		private RepositoryItemTextEdit repositoryItemTextEdit1;

		private BarSubItem barItemChartTypes;

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

		private BarButtonItem barButtonNewSync;

		private BarButtonItem barButtonItemDelete;

		private BarButtonItem barButtonItemRefresh;

		private BarButtonItem barButtonSyncData;

		private RibbonPage ribbonPagePivotReport;

		private RibbonPageGroup ribbonPageGroupEdit;

		private BackstageViewControl backstageViewControl1;

		private BackstageViewClientControl backstageViewClientControl1;

		private BackstageViewTabItem backstageViewTabItem1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButtonSync;

		private Label label1;

		private DataGridList dataGridList;

		private ProgressBar syncProgress;

		private Label lbWait;

		private Label labelConnection;

		private Label labellastprocess;

		public DataSyncForm()
		{
			InitializeComponent();
			base.Load += DataSyncForm_Load;
			AddEvents();
			RegistryHelper registryHelper = new RegistryHelper();
			string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
			defaultBarAndDockingController1.Controller.LookAndFeel.SkinName = stringValue;
			barItemHideFilterHeader.Checked = true;
		}

		private void AddEvents()
		{
			dataGridList.ClickCell += dataGridList_ClickCell;
			dataGridList.ClickCellButton += DataGridList_ClickCellButton;
			dataGridList.InitializeRow += dataGridList_InitializeRow;
			dataGridList.AfterCellUpdate += dataGridList_AfterCellUpdate;
			dataGridList.ClickCell += dataGridList_ClickCell;
		}

		private void DataSyncForm_Load(object sender, EventArgs e)
		{
			try
			{
				labelConnection.Visible = false;
				Global.GlobalSettings.LoadFormProperties(this);
				SetupGrid();
			}
			catch (Exception e2)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridList.ApplyUIDesign();
				dataGridList.ApplyFormat();
				dataGridList.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("C", typeof(bool));
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("Last Sync Time");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("RecType");
				dataTable.Columns.Add("Code");
				dataTable.Columns.Add("View");
				dataGridList.DataSource = dataTable;
				dataGridList.LoadLayout();
				dataGridList.DisplayLayout.Bands[0].Columns["RecType"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Code"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Last Sync Time"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Status"].Hidden = true;
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("D"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns["D"].CellActivation = Activation.AllowEdit;
					dataGridList.DisplayLayout.Bands[0].Columns["D"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridList.DisplayLayout.Bands[0].Columns["D"].CellClickAction = CellClickAction.Edit;
					dataGridList.DisplayLayout.Bands[0].Columns["D"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
					dataGridList.DisplayLayout.Bands[0].Columns["D"].CellClickAction = CellClickAction.EditAndSelectText;
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.NoEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Remarks"].CellActivation = Activation.NoEdit;
				dataGridList.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				dataGridList.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
				dataGridList.DisplayLayout.Bands[0].Columns["View"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridList.Rows.Band.Columns["View"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridList.DisplayLayout.Bands[0].Columns["View"].MaxWidth = 200;
				dataGridList.DisplayLayout.Bands[0].Columns["View"].CellAppearance.TextHAlign = HAlign.Center;
				dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
				dataGridList.DisplayLayout.Override.AllowGroupBy = DefaultableBoolean.False;
				dataGridList.DisplayLayout.Override.HeaderAppearance.BorderColor = Color.Silver;
				dataGridList.DisplayLayout.Override.HeaderAppearance.TextHAlign = HAlign.Center;
				dataGridList.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
				dataGridList.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.False;
				dataGridList.DisplayLayout.Override.CellClickAction = CellClickAction.Default;
				dataGridList.DisplayLayout.Override.ColumnSizingArea = ColumnSizingArea.HeadersOnly;
				dataGridList.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
				dataGridList.DisplayLayout.Override.SummaryFooterCaptionAppearance.BackColor = Color.LightYellow;
				dataGridList.DisplayLayout.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.False;
				dataGridList.DisplayLayout.Override.BorderStyleCell = UIElementBorderStyle.Solid;
				dataGridList.DisplayLayout.GroupByBox.Style = GroupByBoxStyle.Compact;
				dataGridList.DisplayLayout.GroupByBox.Appearance.BackColor = Color.White;
				dataGridList.DisplayLayout.GroupByBox.Appearance.BackColor2 = Color.White;
				dataGridList.DisplayLayout.GroupByBox.PromptAppearance.BackColor = Color.White;
				dataGridList.DisplayLayout.GroupByBox.PromptAppearance.BackColor2 = Color.White;
				dataGridList.DisplayLayout.CaptionVisible = DefaultableBoolean.True;
				Color color3 = dataGridList.DisplayLayout.Override.SelectedRowAppearance.BackColor = (dataGridList.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.FromKnownColor(KnownColor.Highlight));
				color3 = (dataGridList.DisplayLayout.Override.SelectedRowAppearance.ForeColor = (dataGridList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.FromKnownColor(KnownColor.HighlightText)));
				dataGridList.DisplayLayout.Override.DefaultRowHeight = 15;
				dataGridList.DisplayLayout.Override.RowAppearance.FontData.SizeInPoints = 10f;
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void barButtonNewSync_ItemClick(object sender, ItemClickEventArgs e)
		{
			new DataSyncSetupDetailsForm().Show();
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			if (selectDocumentDialog == null)
			{
				return;
			}
			List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
			if (selectedRows != null)
			{
				string text = "";
				foreach (UltraGridRow item in selectedRows)
				{
					if (text == "")
					{
						text = item.Cells["Code"].Value.ToString();
					}
				}
				bool result = false;
				DataSet connected = Factory.DataSyncSystem.GetConnected(text);
				bool.TryParse(connected.Tables[0].Rows[0]["Isconnected"].ToString(), out result);
				labelConnection.Visible = true;
				if (result)
				{
					labelConnection.Text = "Connected with :" + connected.Tables[0].Rows[0]["ServerID"].ToString() + "  DB:" + connected.Tables[0].Rows[0]["DatabaseName"].ToString();
					labelConnection.ForeColor = Color.Black;
					xpButtonSync.Enabled = true;
				}
				else
				{
					labelConnection.Text = "Connection Lost  with :" + text;
					labelConnection.ForeColor = Color.Red;
					xpButtonSync.Enabled = false;
				}
			}
		}

		private void barButtonLoadSync_ItemClick(object sender, ItemClickEventArgs e)
		{
			DataSet dataSyncList = Factory.DataSyncSystem.GetDataSyncList();
			if (dataSyncList != null)
			{
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.DataSource = dataSyncList;
				selectDocumentDialog.Text = "Select Sync";
				selectDocumentDialog.ValidateSelection += form_ValidateSelection;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string code = selectDocumentDialog.SelectedRow.Cells["Code"].Value.ToString();
					DataSyncData dataSyncByID = Factory.DataSyncSystem.GetDataSyncByID(code);
					_ = dataSyncByID.Tables[0].Rows[0];
					DataTable dataTable = dataGridList.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in dataSyncByID.Tables[1].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["C"] = false;
						dataRow2["Name"] = row["Name"];
						dataRow2["Description"] = row["Description"];
						dataRow2["RecType"] = row["RecordType"];
						dataRow2["Code"] = row["Code"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
				}
			}
		}

		private void xpButtonSync_Click(object sender, EventArgs e)
		{
			if (dataGridList.Rows.Count != 0)
			{
				try
				{
					bool flag = false;
					lbWait.Visible = true;
					labellastprocess.Visible = false;
					labellastprocess.Text = "";
					syncProgress.Visible = true;
					syncProgress.Style = ProgressBarStyle.Marquee;
					syncProgress.MarqueeAnimationSpeed = 50;
					xpButtonSync.Enabled = false;
					foreach (UltraGridRow row in dataGridList.Rows)
					{
						row.Cells["View"].Value = "";
						row.Cells["View"].Tag = null;
					}
					syncProgress.Maximum = int.Parse(dataGridList.Rows.Count.ToString());
					flag = SyncData();
					if (!flag | flag)
					{
						xpButtonSync.Enabled = true;
						syncProgress.Visible = false;
						lbWait.Visible = false;
					}
					labellastprocess.Visible = true;
					labellastprocess.Text = "Last Processed at:" + DateTime.Now.ToString();
					MessageBox.Show("Process Completed", DateTime.Now.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private bool SyncData()
		{
			try
			{
				foreach (UltraGridRow row in dataGridList.Rows)
				{
					DataSet dataSet = new DataSet();
					if (bool.Parse(row.Cells["C"].Value.ToString()))
					{
						if (row.Cells["RecType"].Value.ToString() == "25")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncItems(row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("25", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "25");
						}
						if (row.Cells["RecType"].Value.ToString() == "1")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncCustomers(row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("1", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "1");
						}
						if (row.Cells["RecType"].Value.ToString() == "26")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncBySales("26", row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("26", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "26");
						}
						if (row.Cells["RecType"].Value.ToString() == "28")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncBySales("28", row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("28", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "28");
						}
						if (row.Cells["RecType"].Value.ToString() == "3")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncByCollections("3", row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("3", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "3");
						}
						if (row.Cells["RecType"].Value.ToString() == "2")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncByCollections("2", row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("2", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "2");
						}
						if (row.Cells["RecType"].Value.ToString() == "19")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncByStockTransfer("19", row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("19", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "19");
						}
						if (row.Cells["RecType"].Value.ToString() == "20")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncByStockTransfer("20", row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("20", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "20");
						}
						if (row.Cells["RecType"].Value.ToString() == "101")
						{
							dataSet = Factory.DataSyncSystem.GetDataSyncNewCustomers(row.Cells["Code"].Value.ToString());
							Factory.DataSyncSystem.UpdateLastSyncTime("101", row.Cells["Code"].Value.ToString());
							AllocateRowView(row, dataSet, "101");
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			return true;
		}

		private bool AllocateRowView(UltraGridRow dgridRow, DataSet recViews, string recType)
		{
			dgridRow.Cells["View"].Tag = recViews;
			dgridRow.Cells["View"].Value = "Log";
			return false;
		}

		private bool AllocateView(UltraGridCell cell, DataSet recViews, string recType)
		{
			dataGridList.ActiveRow.Cells["View"].Tag = recViews;
			dataGridList.ActiveRow.Cells["View"].Value = "Log";
			dataGridList.ActiveRow.Cells["View"].Activation = Activation.AllowEdit;
			return true;
		}

		private void dataGridList_ClickCell(object sender, ClickCellEventArgs e)
		{
			_ = (e.Cell.Column.Key == "View");
		}

		private void DataGridList_ClickCellButton(object sender, CellEventArgs e)
		{
			SyncLogViewer syncLogViewer = new SyncLogViewer();
			if (e.Cell.Column.Key == "View")
			{
				DataSet dataSet2 = syncLogViewer.SyncData = (e.Cell.Row.Cells["View"].Tag as DataSet);
				if (dataSet2 != null)
				{
					syncLogViewer.ShowDialog();
				}
			}
		}

		private void dataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			_ = e.ReInitialize;
		}

		private void dataGridList_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				_ = dataGridList.ActiveRow;
			}
			catch (Exception)
			{
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline != DefaultableBoolean.True || !(e.Cell.Column.Key == "View"))
			{
				return;
			}
			SyncLogViewer syncLogViewer = new SyncLogViewer();
			if (e.Cell.Column.Key == "View")
			{
				DataSet dataSet2 = syncLogViewer.SyncData = (e.Cell.Row.Cells["View"].Tag as DataSet);
				if (dataSet2 != null)
				{
					syncLogViewer.ShowDialog();
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataSync.DataSyncForm));
			ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			backstageViewControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
			backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
			backstageViewTabItem1 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
			barButtonItemNewGroup = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
			barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			barButtonLoadSync = new DevExpress.XtraBars.BarButtonItem();
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
			barButtonNewSync = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
			barButtonItemRefresh = new DevExpress.XtraBars.BarButtonItem();
			barButtonSyncData = new DevExpress.XtraBars.BarButtonItem();
			ribbonPagePivotReport = new DevExpress.XtraBars.Ribbon.RibbonPage();
			ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			barButtonPrintPreview = new DevExpress.XtraBars.BarButtonItem();
			defaultBarAndDockingController1 = new DevExpress.XtraBars.DefaultBarAndDockingController(components);
			ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			panelButtons = new System.Windows.Forms.Panel();
			labellastprocess = new System.Windows.Forms.Label();
			lbWait = new System.Windows.Forms.Label();
			syncProgress = new System.Windows.Forms.ProgressBar();
			linePanelDown = new Micromind.UISupport.Line();
			xpButtonSync = new Micromind.UISupport.XPButton();
			label1 = new System.Windows.Forms.Label();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			labelConnection = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl1).BeginInit();
			backstageViewControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)defaultBarAndDockingController1.Controller).BeginInit();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			SuspendLayout();
			ribbon.ApplicationButtonDropDownControl = backstageViewControl1;
			ribbon.ApplicationButtonImageOptions.Image = Micromind.ClientUI.Properties.Resources.axolon_48;
			ribbon.ExpandCollapseItem.Id = 0;
			ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[34]
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
				barButtonLoadSync,
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
				barButtonNewSync,
				barButtonItemDelete,
				barButtonItemRefresh,
				barButtonSyncData
			});
			ribbon.Location = new System.Drawing.Point(0, 0);
			ribbon.MaxItemId = 44;
			ribbon.Name = "ribbon";
			ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[1]
			{
				ribbonPagePivotReport
			});
			ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[1]
			{
				repositoryItemTextEdit1
			});
			ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
			ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
			ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
			ribbon.Size = new System.Drawing.Size(1290, 122);
			ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
			ribbon.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
			backstageViewControl1.Controls.Add(backstageViewClientControl1);
			backstageViewControl1.Items.Add(backstageViewTabItem1);
			backstageViewControl1.Location = new System.Drawing.Point(71, 214);
			backstageViewControl1.Name = "backstageViewControl1";
			backstageViewControl1.OwnerControl = ribbon;
			backstageViewControl1.Size = new System.Drawing.Size(480, 150);
			backstageViewControl1.TabIndex = 189;
			backstageViewClientControl1.Location = new System.Drawing.Point(188, 0);
			backstageViewClientControl1.Name = "backstageViewClientControl1";
			backstageViewClientControl1.Size = new System.Drawing.Size(292, 150);
			backstageViewClientControl1.TabIndex = 1;
			backstageViewTabItem1.Caption = "backstageViewTabItem1";
			backstageViewTabItem1.ContentControl = backstageViewClientControl1;
			backstageViewTabItem1.Name = "backstageViewTabItem1";
			barButtonItemNewGroup.Caption = "New Group";
			barButtonItemNewGroup.Id = 1;
			barButtonItemNewGroup.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.newfolder;
			barButtonItemNewGroup.Name = "barButtonItemNewGroup";
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
			barButtonItem2.Caption = "Save";
			barButtonItem2.Id = 10;
			barButtonItem2.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.saveicon;
			barButtonItem2.Name = "barButtonItem2";
			barButtonLoadSync.Caption = "Select Sync";
			barButtonLoadSync.Id = 11;
			barButtonLoadSync.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.editdoc;
			barButtonLoadSync.Name = "barButtonLoadSync";
			barButtonLoadSync.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonLoadSync_ItemClick);
			barButtonGroup3.Caption = "barButtonGroup3";
			barButtonGroup3.Id = 12;
			barButtonGroup3.Name = "barButtonGroup3";
			barItemSelectedRowsOnly.BindableChecked = true;
			barItemSelectedRowsOnly.Caption = "Selection Only";
			barItemSelectedRowsOnly.Checked = true;
			barItemSelectedRowsOnly.Id = 15;
			barItemSelectedRowsOnly.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.selectedrows;
			barItemSelectedRowsOnly.Name = "barItemSelectedRowsOnly";
			barButtonPrintReport.Caption = "Print Report";
			barButtonPrintReport.Id = 17;
			barButtonPrintReport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.print;
			barButtonPrintReport.Name = "barButtonPrintReport";
			barButtonPrintChart.Caption = "Print Chart";
			barButtonPrintChart.Id = 18;
			barButtonPrintChart.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.chart_1;
			barButtonPrintChart.Name = "barButtonPrintChart";
			barItemHideFilterHeader.Caption = "Hide Fields Panel";
			barItemHideFilterHeader.Id = 21;
			barItemHideFilterHeader.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.columns;
			barItemHideFilterHeader.Name = "barItemHideFilterHeader";
			barItemChartColor.Caption = "Colors";
			barItemChartColor.Id = 22;
			barItemChartColor.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.colorpalet;
			barItemChartColor.Name = "barItemChartColor";
			barItemTitle.Caption = "Chart Title:";
			barItemTitle.Edit = repositoryItemTextEdit1;
			barItemTitle.EditWidth = 150;
			barItemTitle.Id = 25;
			barItemTitle.Name = "barItemTitle";
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
			barButtonItemImport.Caption = "Import...";
			barButtonItemImport.Id = 39;
			barButtonItemImport.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.download_icon;
			barButtonItemImport.Name = "barButtonItemImport";
			barButtonNewSync.Caption = "Setup Sync";
			barButtonNewSync.Id = 40;
			barButtonNewSync.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.newfile;
			barButtonNewSync.Name = "barButtonNewSync";
			barButtonNewSync.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonNewSync_ItemClick);
			barButtonItemDelete.Caption = "Delete";
			barButtonItemDelete.Id = 41;
			barButtonItemDelete.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.DeleteRed;
			barButtonItemDelete.Name = "barButtonItemDelete";
			barButtonItemRefresh.Caption = "Refresh";
			barButtonItemRefresh.Id = 42;
			barButtonItemRefresh.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Circulation;
			barButtonItemRefresh.Name = "barButtonItemRefresh";
			barButtonSyncData.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			barButtonSyncData.Caption = "Sync Data";
			barButtonSyncData.Id = 43;
			barButtonSyncData.Name = "barButtonSyncData";
			ribbonPagePivotReport.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[1]
			{
				ribbonPageGroupEdit
			});
			ribbonPagePivotReport.Name = "ribbonPagePivotReport";
			ribbonPagePivotReport.Text = "Sync";
			ribbonPageGroupEdit.ItemLinks.Add(barButtonItemRefresh);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonNewSync);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonLoadSync);
			ribbonPageGroupEdit.ItemLinks.Add(barButtonPrintReport);
			ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
			ribbonPageGroupEdit.Text = "Data Sync";
			barButtonPrintPreview.Caption = "Print Preview";
			barButtonPrintPreview.Id = 14;
			barButtonPrintPreview.ImageOptions.LargeImage = Micromind.ClientUI.Properties.Resources.Print_preview;
			barButtonPrintPreview.Name = "barButtonPrintPreview";
			defaultBarAndDockingController1.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
			ribbonPageGroup5.Name = "ribbonPageGroup5";
			ribbonPageGroup5.Text = "Data";
			saveFileDialog1.DefaultExt = "axp";
			saveFileDialog1.FileName = "pivot report";
			openFileDialog1.DefaultExt = "axp";
			openFileDialog1.FileName = "openFileDialog1";
			openFileDialog1.Filter = "Axolon Pivot Reports|*.axp";
			panelButtons.Controls.Add(labellastprocess);
			panelButtons.Controls.Add(lbWait);
			panelButtons.Controls.Add(syncProgress);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButtonSync);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 614);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1290, 76);
			panelButtons.TabIndex = 194;
			labellastprocess.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labellastprocess.AutoSize = true;
			labellastprocess.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labellastprocess.ForeColor = System.Drawing.Color.Blue;
			labellastprocess.Location = new System.Drawing.Point(23, 30);
			labellastprocess.Name = "labellastprocess";
			labellastprocess.Size = new System.Drawing.Size(130, 16);
			labellastprocess.TabIndex = 295;
			labellastprocess.Text = "Last Processed at:";
			labellastprocess.Visible = false;
			lbWait.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			lbWait.AutoSize = true;
			lbWait.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			lbWait.Location = new System.Drawing.Point(382, 26);
			lbWait.Name = "lbWait";
			lbWait.Size = new System.Drawing.Size(132, 19);
			lbWait.TabIndex = 294;
			lbWait.Text = "Synchronizing..";
			syncProgress.BackColor = System.Drawing.Color.Lime;
			syncProgress.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			syncProgress.Location = new System.Drawing.Point(520, 26);
			syncProgress.Name = "syncProgress";
			syncProgress.Size = new System.Drawing.Size(630, 23);
			syncProgress.TabIndex = 21;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(1290, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButtonSync.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonSync.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButtonSync.BackColor = System.Drawing.Color.DarkGray;
			xpButtonSync.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonSync.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonSync.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButtonSync.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			xpButtonSync.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonSync.Location = new System.Drawing.Point(1179, 3);
			xpButtonSync.Name = "xpButtonSync";
			xpButtonSync.Size = new System.Drawing.Size(96, 65);
			xpButtonSync.TabIndex = 20;
			xpButtonSync.Text = "Sync";
			xpButtonSync.UseVisualStyleBackColor = false;
			xpButtonSync.Click += new System.EventHandler(xpButtonSync_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 130);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(67, 13);
			label1.TabIndex = 196;
			label1.Text = "Sync Name :";
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(12, 151);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(1266, 457);
			dataGridList.TabIndex = 186;
			labelConnection.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			labelConnection.AutoSize = true;
			labelConnection.BackColor = System.Drawing.Color.FromArgb(235, 236, 239);
			labelConnection.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
			labelConnection.Location = new System.Drawing.Point(952, 52);
			labelConnection.Name = "labelConnection";
			labelConnection.Size = new System.Drawing.Size(38, 13);
			labelConnection.TabIndex = 295;
			labelConnection.Text = "Status";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1290, 690);
			base.Controls.Add(labelConnection);
			base.Controls.Add(label1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridList);
			base.Controls.Add(backstageViewControl1);
			base.Controls.Add(ribbon);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "DataSyncForm";
			Ribbon = ribbon;
			Text = "Data Sync";
			((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
			((System.ComponentModel.ISupportInitialize)backstageViewControl1).EndInit();
			backstageViewControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit1).EndInit();
			((System.ComponentModel.ISupportInitialize)defaultBarAndDockingController1.Controller).EndInit();
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
