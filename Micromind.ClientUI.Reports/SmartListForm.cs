using DevExpress.Utils;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class SmartListForm : Form, IForm
	{
		private bool isFirstTimeActivating = true;

		private bool canAccessCost = true;

		private DataSet reportData;

		private DataSet smartListData;

		private string parm1;

		private string parm2;

		private string parm3;

		private string parm4;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet productData;

		private bool showInactiveItems;

		private DatePeriods currentDatePeriod = DatePeriods.ThisMonthToDate;

		private XPButton buttonDone;

		private Panel panelButtons;

		private Line linePanelDown;

		private IContainer components;

		private DataGridList dataGridList;

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

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem toolStripMenuItemLocationWiseOnhand;

		private TreeView treeViewList;

		private SplitContainer splitContainer1;

		private ContextMenuStrip contextMenuStripTreeView;

		private ToolStripMenuItem newGroupToolStripMenuItem;

		private ImageList imageList1;

		private ToolStripMenuItem newReportToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private System.Windows.Forms.ToolTip toolTip1;

		private ToolStripButton toolStripButtonMerge;

		private ToolStripButton toolStripButtonReset;

		private DateControl dateControlFilter;

		private UltraExpandableGroupBox ultraExpandableGroupBox1;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;

		private Button buttonRefresh;

		private ToolStripButton toolStripButtonFitText;

		private ToolStripButton toolStripButtonClearFilter;

		private Label labelViewListName;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonExpand;

		private ToolStripButton toolStripButtonCollapse;

		private ProgressBar progressBarLoad;

		private Label lbWait;

		private ToolStripMenuItem favoriteToolStripMenuItem;

		private Label label1;

		private TextBox textBoxSearch;

		private Panel panelEdit1;

		private Button buttonAddGroup1;

		private Button button21;

		private Button button31;

		private XPButton xpButton2;

		private XPButton xpButton1;

		private ToolStripMenuItem moveUpToolStripMenuItem;

		private ToolStripMenuItem moveDownToolStripMenuItem;

		private Panel panelEdit;

		private Button buttonAddGroup;

		private Button button2;

		private Button button3;

		private ToolTipController toolTipController1;

		private UltraToolTipManager ultraToolTipManager1;

		private UltraPictureBox ultraPictureBoxInformation;

		private ToolStrip toolStrip1;

		private bool isSubReport;

		private ScreenAccessRight screenRight;

		public DatePeriods CurrentDatePeriod
		{
			get
			{
				return currentDatePeriod;
			}
			set
			{
				currentDatePeriod = value;
			}
		}

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4011;

		public ScreenTypes ScreenType => ScreenTypes.List;

		public bool IsSubReport
		{
			get
			{
				return isSubReport;
			}
			set
			{
				isSubReport = value;
				if (value)
				{
					treeViewList.Visible = false;
					splitContainer1.SplitterDistance = 0;
					splitContainer1.Panel1MinSize = 0;
					splitContainer1.Left = 0;
					splitContainer1.Panel1.Hide();
					splitContainer1.IsSplitterFixed = true;
				}
			}
		}

		private bool ShowInactiveItems
		{
			get
			{
				return showInactiveItems;
			}
			set
			{
				showInactiveItems = value;
			}
		}

		public SmartListForm()
		{
			InitializeComponent();
			base.Activated += ProductListForm_Activated;
			treeViewList.ContextMenuStrip = contextMenuStripTreeView;
			treeViewList.NodeMouseClick += treeView1_NodeMouseClick;
			treeViewList.Click += treeView1_Click;
			treeViewList.AfterSelect += treeView1_AfterSelect;
			treeViewList.AfterLabelEdit += treeView1_AfterLabelEdit;
			treeViewList.MouseDown += TreeViewList_MouseUp;
			treeViewList.BeforeSelect += treeView1_BeforeSelect;
			base.FormClosing += VendorListForm_FormClosing;
			dataGridList.AllowUnfittedView = true;
			treeViewList.LabelEdit = false;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			contextMenuStripTreeView.Opening += ContextMenuStripTreeView_Opening;
			treeViewList.ItemHeight = 18;
			dateControlFilter.SelectedIndexChanged += dateControlFilter_SelectedIndexChanged;
		}

		private void dateControlFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			CurrentDatePeriod = dateControlFilter.SelectedPeriod;
		}

		private void TreeViewList_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Point pt = new Point(e.X, e.Y);
				TreeNode nodeAt = treeViewList.GetNodeAt(pt);
				if (nodeAt != null)
				{
					treeViewList.SelectedNode = nodeAt;
				}
			}
		}

		private void ContextMenuStripTreeView_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 2)
				{
					string name = treeViewList.SelectedNode.Name;
					if (name == null || name == "")
					{
						favoriteToolStripMenuItem.Enabled = false;
					}
					else
					{
						favoriteToolStripMenuItem.Enabled = true;
						if (Factory.UserFavoriteSystem.IsUserFavorite(Global.CurrentUser, 1, name))
						{
							favoriteToolStripMenuItem.Checked = true;
						}
						else
						{
							favoriteToolStripMenuItem.Checked = false;
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 2)
			{
				dataGridList.SaveLayout(treeViewList.SelectedNode.Name, saveFullSetting: true);
			}
		}

		private void VendorListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			try
			{
				string s = "-1";
				if (treeViewList.SelectedNode.Parent != null)
				{
					s = treeViewList.SelectedNode.Parent.Name;
				}
				if (!string.IsNullOrEmpty(e.Label))
				{
					if (treeViewList.SelectedNode.ImageIndex == 0)
					{
						if (!Factory.SmartListSystem.UpdateCategory(e.Label, int.Parse(s), int.Parse(treeViewList.SelectedNode.Name)))
						{
							e.CancelEdit = true;
						}
					}
					else if (!Factory.SmartListSystem.RenameReport(e.Label, int.Parse(treeViewList.SelectedNode.Name)))
					{
						e.CancelEdit = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				e.CancelEdit = true;
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				SetDefaultDate();
				LoadReport();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetDefaultDate()
		{
			TreeNode selectedNode = treeViewList.SelectedNode;
			if (selectedNode != null && selectedNode.ImageIndex == 2)
			{
				smartListData = Factory.SmartListSystem.GetSmartListByID(selectedNode.Name);
				DataRow dataRow = smartListData.Tables["SmartList"].Rows[0];
				bool result = false;
				bool.TryParse(dataRow["IsSetDateEqualTo"].ToString(), out result);
				if (result)
				{
					DatePeriods datePeriods = CurrentDatePeriod;
					dateControlFilter.SelectedPeriod = DatePeriods.DateEqualTo;
					CurrentDatePeriod = datePeriods;
				}
				else
				{
					dateControlFilter.SelectedPeriod = currentDatePeriod;
				}
			}
		}

		private void LoadReport()
		{
			try
			{
				TreeNode selectedNode = treeViewList.SelectedNode;
				if (selectedNode != null && selectedNode.ImageIndex == 2)
				{
					labelViewListName.Text = "";
					if (treeViewList.SelectedNode != null && selectedNode.ImageIndex == 2)
					{
						labelViewListName.Text = treeViewList.SelectedNode.Text;
						if (true)
						{
							dateControlFilter.Enabled = true;
						}
						else
						{
							dateControlFilter.Enabled = false;
						}
						LoadReportData(selectedNode.Name);
						try
						{
							dataGridList.LoadLayout(selectedNode.Name);
							dataGridList.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
						}
						catch
						{
						}
						dataGridList.ApplyFormat();
						dataGridList.FormatAllNumericColumns(null);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadReportData(string reportName)
		{
			try
			{
				smartListData = Factory.SmartListSystem.GetSmartListByID(reportName);
				DataRow dataRow = smartListData.Tables["SmartList"].Rows[0];
				bool result = false;
				bool.TryParse(dataRow["IsHideDateFilter"].ToString(), out result);
				dateControlFilter.Enabled = !result;
				SetToolTip(dataRow["DisplayNote"].ToString());
				string[] parameters = null;
				string[] parameterValues = null;
				if (IsSubReport)
				{
					parameters = new string[4]
					{
						"@Parm1",
						"@Parm2",
						"@Parm3",
						"@Parm4"
					};
					parameterValues = new string[4]
					{
						parm1,
						parm2,
						parm3,
						parm4
					};
				}
				buttonRefresh.Enabled = false;
				Application.EnableVisualStyles();
				lbWait.Visible = true;
				progressBarLoad.Visible = true;
				progressBarLoad.Style = ProgressBarStyle.Marquee;
				progressBarLoad.MarqueeAnimationSpeed = 50;
				Refresh();
				byte[] bytDs = Factory.SmartListSystem.GetSmartListData(reportName, dateControlFilter.FromDate, dateControlFilter.ToDate, parameters, parameterValues);
				reportData = CommonLib.DecompressDataSet(bytDs);
				dataGridList.DataSource = reportData;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				buttonRefresh.Enabled = true;
				lbWait.Visible = false;
				progressBarLoad.Visible = false;
			}
		}

		private void treeView1_Click(object sender, EventArgs e)
		{
		}

		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
		}

		private void toolStripMenuItemLocationWiseOnhand_Click(object sender, EventArgs e)
		{
		}

		private void dataGridList_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
		{
			_ = Cursor.Position;
			if (e.Cell.Column.Key == "P" && e.Cell.Value.ToString() == "1")
			{
				PublicFunctions.GetProductThumbnailImage(e.Cell.Row.Cells["Item Code"].Value.ToString(), isProductParentID: false);
			}
			else
			{
				OpenForEdit();
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
			Delete();
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
			New();
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (!e.Row.IsDataRow || smartListData == null)
			{
				return;
			}
			FormHelper formHelper = new FormHelper();
			SmartListDrillDownActions smartListDrillDownActions = SmartListDrillDownActions.None;
			DataRow dataRow = smartListData.Tables[0].Rows[0];
			if (dataRow["DrillAction"] != DBNull.Value)
			{
				smartListDrillDownActions = (SmartListDrillDownActions)int.Parse(dataRow["DrillAction"].ToString());
			}
			switch (smartListDrillDownActions)
			{
			case SmartListDrillDownActions.OpenCard:
			{
				DataComboType cardType = (DataComboType)int.Parse(dataRow["DrillCardTypeID"].ToString());
				string text11 = dataRow["DrillCardIDField"].ToString();
				if (!e.Row.Band.Columns.Exists(text11))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text11 + "'does not exist.");
					break;
				}
				string id = e.Row.Cells[text11].Value.ToString();
				formHelper.OpenCardForEdit(cardType, id);
				break;
			}
			case SmartListDrillDownActions.OpenTransaction:
			{
				string text9 = dataRow["DrillTransactionVoucherIDField"].ToString();
				string text10 = dataRow["DrillTransactionSysDocIDField"].ToString();
				bool flag = false;
				if (!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["IsPreview"]))
				{
					flag = bool.Parse(dataRow["IsPreview"].ToString());
				}
				if (!e.Row.Band.Columns.Exists(text9))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text9 + "'", "does not exist.");
					break;
				}
				if (!e.Row.Band.Columns.Exists(text10))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text10 + "'", "does not exist.");
					break;
				}
				string voucherID = e.Row.Cells[text9].Value.ToString();
				string sysDocID = e.Row.Cells[text10].Value.ToString();
				if (!flag)
				{
					formHelper.EditTransaction(sysDocID, voucherID);
					break;
				}
				DocumentViewForm documentViewForm = new DocumentViewForm();
				documentViewForm.Document = formHelper.GetTransactionPreviewDoc(sysDocID, voucherID);
				documentViewForm.ShowDialog();
				break;
			}
			case SmartListDrillDownActions.OpenSmartList:
			{
				string reportID = dataRow["DrillSubReportID"].ToString();
				string text = dataRow["DrillParm1"].ToString();
				string text2 = dataRow["DrillParm2"].ToString();
				string text3 = dataRow["DrillParm3"].ToString();
				string text4 = dataRow["DrillParm4"].ToString();
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text) && !e.Row.Band.Columns.Exists(text))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text + "'", "does not exist.");
					break;
				}
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text2) && !e.Row.Band.Columns.Exists(text2))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text2 + "'", "does not exist.");
					break;
				}
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text3) && !e.Row.Band.Columns.Exists(text3))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text3 + "'", "does not exist.");
					break;
				}
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text4) && !e.Row.Band.Columns.Exists(text4))
				{
					ErrorHelper.WarningMessage("Column name:", "'" + text4 + "'", "does not exist.");
					break;
				}
				string text5 = null;
				string text6 = null;
				string text7 = null;
				string text8 = null;
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text))
				{
					text5 = dataGridList.ActiveRow.Cells[text].Value.ToString();
				}
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text2))
				{
					text6 = dataGridList.ActiveRow.Cells[text2].Value.ToString();
				}
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text3))
				{
					text7 = dataGridList.ActiveRow.Cells[text3].Value.ToString();
				}
				if (!Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text4))
				{
					text8 = dataGridList.ActiveRow.Cells[text4].Value.ToString();
				}
				SmartListForm smartListForm = new SmartListForm();
				smartListForm.Show();
				smartListForm.ShowDrillDownReport(reportID, dateControlFilter.FromDate, dateControlFilter.ToDate, text5, text6, text7, text8);
				break;
			}
			}
		}

		public void ShowDrillDownReport(string reportID, DateTime fromDate, DateTime toDate, string parm1, string parm2, string parm3, string parm4)
		{
			try
			{
				this.parm1 = parm1;
				this.parm2 = parm2;
				this.parm3 = parm3;
				this.parm4 = parm4;
				dateControlFilter.SelectedPeriod = DatePeriods.Custom;
				dateControlFilter.FromDate = fromDate;
				dateControlFilter.ToDate = toDate;
				IsSubReport = true;
				LoadReportData(reportID.ToString());
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void ProductListForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTimeActivating)
			{
				isFirstTimeActivating = false;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && productData != null)
			{
				productData.Dispose();
				productData = null;
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.SmartListForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton2 = new Micromind.UISupport.XPButton();
			panelEdit1 = new System.Windows.Forms.Panel();
			buttonAddGroup1 = new System.Windows.Forms.Button();
			button21 = new System.Windows.Forms.Button();
			button31 = new System.Windows.Forms.Button();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonDone = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFitText = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExpand = new System.Windows.Forms.ToolStripButton();
			toolStripButtonCollapse = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonClearFilter = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			toolStripButtonReset = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemLocationWiseOnhand = new System.Windows.Forms.ToolStripMenuItem();
			treeViewList = new System.Windows.Forms.TreeView();
			contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip();
			newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			favoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			imageList1 = new System.Windows.Forms.ImageList();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			panelEdit = new System.Windows.Forms.Panel();
			buttonAddGroup = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			textBoxSearch = new System.Windows.Forms.TextBox();
			ultraExpandableGroupBox1 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			ultraPictureBoxInformation = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
			dateControlFilter = new Micromind.DataControls.DateControl();
			progressBarLoad = new System.Windows.Forms.ProgressBar();
			lbWait = new System.Windows.Forms.Label();
			buttonRefresh = new System.Windows.Forms.Button();
			toolTip1 = new System.Windows.Forms.ToolTip();
			labelViewListName = new System.Windows.Forms.Label();
			toolTipController1 = new DevExpress.Utils.ToolTipController();
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager();
			panelButtons.SuspendLayout();
			panelEdit1.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			contextMenuStripTreeView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			panelEdit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBox1).BeginInit();
			ultraExpandableGroupBox1.SuspendLayout();
			ultraExpandableGroupBoxPanel1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton2);
			panelButtons.Controls.Add(panelEdit1);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 546);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1052, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(1052, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(620, 13);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(67, 24);
			xpButton2.TabIndex = 297;
			xpButton2.Text = "Move Down";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Visible = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			panelEdit1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			panelEdit1.Controls.Add(buttonAddGroup1);
			panelEdit1.Controls.Add(button21);
			panelEdit1.Controls.Add(button31);
			panelEdit1.Location = new System.Drawing.Point(704, 3);
			panelEdit1.Name = "panelEdit1";
			panelEdit1.Size = new System.Drawing.Size(108, 31);
			panelEdit1.TabIndex = 294;
			panelEdit1.Visible = false;
			buttonAddGroup1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonAddGroup1.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			buttonAddGroup1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			buttonAddGroup1.Image = Micromind.ClientUI.Properties.Resources.newfolder;
			buttonAddGroup1.Location = new System.Drawing.Point(5, 1);
			buttonAddGroup1.Name = "buttonAddGroup1";
			buttonAddGroup1.Size = new System.Drawing.Size(26, 26);
			buttonAddGroup1.TabIndex = 294;
			toolTip1.SetToolTip(buttonAddGroup1, "Create New Group");
			buttonAddGroup1.UseVisualStyleBackColor = true;
			buttonAddGroup1.Click += new System.EventHandler(buttonAddGroup_Click);
			button21.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button21.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button21.Image = Micromind.ClientUI.Properties.Resources.newfile;
			button21.Location = new System.Drawing.Point(42, 1);
			button21.Name = "button21";
			button21.Size = new System.Drawing.Size(26, 26);
			button21.TabIndex = 295;
			toolTip1.SetToolTip(button21, "Create New Report");
			button21.UseVisualStyleBackColor = true;
			button21.Click += new System.EventHandler(button2_Click);
			button31.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button31.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			button31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button31.Image = Micromind.ClientUI.Properties.Resources.Delete;
			button31.Location = new System.Drawing.Point(75, 1);
			button31.Name = "button31";
			button31.Size = new System.Drawing.Size(26, 26);
			button31.TabIndex = 296;
			toolTip1.SetToolTip(button31, "Delete Item");
			button31.UseVisualStyleBackColor = true;
			button31.Click += new System.EventHandler(button3_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(549, 7);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(50, 30);
			xpButton1.TabIndex = 296;
			xpButton1.Text = "Move Up";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Visible = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(944, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButtonFitText,
				toolStripSeparator7,
				toolStripButtonExpand,
				toolStripButtonCollapse,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonClearFilter,
				toolStripButtonMerge,
				toolStripButtonReset
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1052, 31);
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
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.autofit;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(100, 28);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(86, 28);
			toolStripButtonFitText.Text = "Fit to Text";
			toolStripButtonFitText.Click += new System.EventHandler(toolStripButtonFitText_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripButtonExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExpand.Image = Micromind.ClientUI.Properties.Resources._001_03;
			toolStripButtonExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExpand.Name = "toolStripButtonExpand";
			toolStripButtonExpand.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExpand.Text = "toolStripButton2";
			toolStripButtonExpand.Click += new System.EventHandler(toolStripButtonExpand_Click);
			toolStripButtonCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonCollapse.Image = Micromind.ClientUI.Properties.Resources._001_04;
			toolStripButtonCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonCollapse.Name = "toolStripButtonCollapse";
			toolStripButtonCollapse.Size = new System.Drawing.Size(28, 28);
			toolStripButtonCollapse.Text = "toolStripButton1";
			toolStripButtonCollapse.Click += new System.EventHandler(toolStripButtonCollapse_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(83, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonClearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonClearFilter.Image = Micromind.ClientUI.Properties.Resources.clearfilter;
			toolStripButtonClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonClearFilter.Name = "toolStripButtonClearFilter";
			toolStripButtonClearFilter.Size = new System.Drawing.Size(28, 28);
			toolStripButtonClearFilter.Text = "Clear All Filters";
			toolStripButtonClearFilter.Click += new System.EventHandler(toolStripButtonClearFilter_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(97, 28);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonReset.Image = Micromind.ClientUI.Properties.Resources.reset;
			toolStripButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonReset.Name = "toolStripButtonReset";
			toolStripButtonReset.Size = new System.Drawing.Size(102, 28);
			toolStripButtonReset.Text = "Reset Layout";
			toolStripButtonReset.ToolTipText = "Reset the layout to default layout";
			toolStripButtonReset.Click += new System.EventHandler(toolStripButtonReset_Click);
			dataGridList.AllowUnfittedView = false;
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
			dataGridList.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(0, 73);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(777, 402);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemLocationWiseOnhand.Name = "toolStripMenuItemLocationWiseOnhand";
			toolStripMenuItemLocationWiseOnhand.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemLocationWiseOnhand.Text = "Location Wise Onhand...";
			treeViewList.AllowDrop = true;
			treeViewList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			treeViewList.ContextMenuStrip = contextMenuStripTreeView;
			treeViewList.FullRowSelect = true;
			treeViewList.HideSelection = false;
			treeViewList.ImageIndex = 0;
			treeViewList.ImageList = imageList1;
			treeViewList.Location = new System.Drawing.Point(0, 73);
			treeViewList.Name = "treeViewList";
			treeViewList.SelectedImageIndex = 1;
			treeViewList.Size = new System.Drawing.Size(244, 402);
			treeViewList.StateImageList = imageList1;
			treeViewList.TabIndex = 291;
			contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				newGroupToolStripMenuItem,
				newReportToolStripMenuItem,
				toolStripSeparator5,
				editToolStripMenuItem,
				favoriteToolStripMenuItem,
				toolStripSeparator6,
				deleteToolStripMenuItem,
				moveUpToolStripMenuItem,
				moveDownToolStripMenuItem
			});
			contextMenuStripTreeView.Name = "contextMenuStripTreeView";
			contextMenuStripTreeView.Size = new System.Drawing.Size(146, 170);
			newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
			newGroupToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			newGroupToolStripMenuItem.Text = "New Group...";
			newGroupToolStripMenuItem.Click += new System.EventHandler(newGroupToolStripMenuItem_Click);
			newReportToolStripMenuItem.Name = "newReportToolStripMenuItem";
			newReportToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			newReportToolStripMenuItem.Text = "New Report...";
			newReportToolStripMenuItem.Click += new System.EventHandler(newReportToolStripMenuItem_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			editToolStripMenuItem.Text = "Edit...";
			editToolStripMenuItem.Click += new System.EventHandler(editToolStripMenuItem_Click);
			favoriteToolStripMenuItem.CheckOnClick = true;
			favoriteToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.star_16x16;
			favoriteToolStripMenuItem.Name = "favoriteToolStripMenuItem";
			favoriteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			favoriteToolStripMenuItem.Text = "Favorite";
			favoriteToolStripMenuItem.Click += new System.EventHandler(favoriteToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(142, 6);
			deleteToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			deleteToolStripMenuItem.Text = "Delete";
			deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
			moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
			moveUpToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			moveUpToolStripMenuItem.Text = "Move Up";
			moveUpToolStripMenuItem.Click += new System.EventHandler(moveUpToolStripMenuItem_Click);
			moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
			moveDownToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			moveDownToolStripMenuItem.Text = "Move Down";
			moveDownToolStripMenuItem.Click += new System.EventHandler(moveDownToolStripMenuItem_Click);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "folder_closed.png");
			imageList1.Images.SetKeyName(1, "folder_open.png");
			imageList1.Images.SetKeyName(2, "repport16x16.png");
			splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainer1.Location = new System.Drawing.Point(12, 38);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(panelEdit);
			splitContainer1.Panel1.Controls.Add(label1);
			splitContainer1.Panel1.Controls.Add(textBoxSearch);
			splitContainer1.Panel1.Controls.Add(treeViewList);
			splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(splitContainer1_Panel1_Paint);
			splitContainer1.Panel2.Controls.Add(dataGridList);
			splitContainer1.Panel2.Controls.Add(ultraExpandableGroupBox1);
			splitContainer1.Size = new System.Drawing.Size(1028, 475);
			splitContainer1.SplitterDistance = 247;
			splitContainer1.TabIndex = 292;
			panelEdit.Controls.Add(buttonAddGroup);
			panelEdit.Controls.Add(button2);
			panelEdit.Controls.Add(button3);
			panelEdit.Location = new System.Drawing.Point(7, 7);
			panelEdit.Name = "panelEdit";
			panelEdit.Size = new System.Drawing.Size(113, 31);
			panelEdit.TabIndex = 295;
			buttonAddGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonAddGroup.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			buttonAddGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			buttonAddGroup.Image = Micromind.ClientUI.Properties.Resources.newfolder;
			buttonAddGroup.Location = new System.Drawing.Point(5, 1);
			buttonAddGroup.Name = "buttonAddGroup";
			buttonAddGroup.Size = new System.Drawing.Size(26, 26);
			buttonAddGroup.TabIndex = 294;
			toolTip1.SetToolTip(buttonAddGroup, "Create New Group");
			buttonAddGroup.UseVisualStyleBackColor = true;
			buttonAddGroup.Click += new System.EventHandler(buttonAddGroup_Click);
			button2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button2.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button2.Image = Micromind.ClientUI.Properties.Resources.newfile;
			button2.Location = new System.Drawing.Point(42, 1);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(26, 26);
			button2.TabIndex = 295;
			toolTip1.SetToolTip(button2, "Create New Report");
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			button3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button3.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button3.Image = Micromind.ClientUI.Properties.Resources.Delete;
			button3.Location = new System.Drawing.Point(75, 1);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(26, 26);
			button3.TabIndex = 296;
			toolTip1.SetToolTip(button3, "Delete Item");
			button3.UseVisualStyleBackColor = true;
			button3.Click += new System.EventHandler(button3_Click);
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 293;
			label1.Text = "Find:";
			textBoxSearch.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxSearch.Location = new System.Drawing.Point(42, 48);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(202, 20);
			textBoxSearch.TabIndex = 292;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			textBoxSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(textBoxSearch_KeyUp);
			ultraExpandableGroupBox1.Controls.Add(ultraExpandableGroupBoxPanel1);
			ultraExpandableGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			ultraExpandableGroupBox1.ExpandedSize = new System.Drawing.Size(777, 73);
			ultraExpandableGroupBox1.Location = new System.Drawing.Point(0, 0);
			ultraExpandableGroupBox1.Name = "ultraExpandableGroupBox1";
			ultraExpandableGroupBox1.Size = new System.Drawing.Size(777, 73);
			ultraExpandableGroupBox1.TabIndex = 292;
			ultraExpandableGroupBox1.Text = "Filter";
			ultraExpandableGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000;
			ultraExpandableGroupBox1.ExpandedStateChanging += new System.ComponentModel.CancelEventHandler(ultraExpandableGroupBox1_ExpandedStateChanging);
			ultraExpandableGroupBoxPanel1.Controls.Add(ultraPictureBoxInformation);
			ultraExpandableGroupBoxPanel1.Controls.Add(dateControlFilter);
			ultraExpandableGroupBoxPanel1.Controls.Add(progressBarLoad);
			ultraExpandableGroupBoxPanel1.Controls.Add(lbWait);
			ultraExpandableGroupBoxPanel1.Controls.Add(buttonRefresh);
			ultraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(3, 19);
			ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(771, 51);
			ultraExpandableGroupBoxPanel1.TabIndex = 0;
			ultraPictureBoxInformation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ultraPictureBoxInformation.BorderShadowColor = System.Drawing.Color.Empty;
			ultraPictureBoxInformation.Image = resources.GetObject("ultraPictureBoxInformation.Image");
			ultraPictureBoxInformation.Location = new System.Drawing.Point(752, 31);
			ultraPictureBoxInformation.Name = "ultraPictureBoxInformation";
			ultraPictureBoxInformation.Size = new System.Drawing.Size(18, 19);
			ultraPictureBoxInformation.TabIndex = 295;
			ultraPictureBoxInformation.Visible = false;
			ultraPictureBoxInformation.Click += new System.EventHandler(ultraPictureBox1_Click);
			dateControlFilter.BackColor = System.Drawing.Color.Transparent;
			dateControlFilter.CustomReportFieldName = "";
			dateControlFilter.CustomReportKey = "";
			dateControlFilter.CustomReportValueType = 1;
			dateControlFilter.FromDate = new System.DateTime(2019, 5, 1, 0, 0, 0, 0);
			dateControlFilter.Location = new System.Drawing.Point(0, 1);
			dateControlFilter.Name = "dateControlFilter";
			dateControlFilter.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlFilter.Size = new System.Drawing.Size(274, 49);
			dateControlFilter.TabIndex = 291;
			dateControlFilter.ToDate = new System.DateTime(2017, 12, 26, 23, 59, 59, 59);
			progressBarLoad.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			progressBarLoad.Location = new System.Drawing.Point(439, 30);
			progressBarLoad.Name = "progressBarLoad";
			progressBarLoad.Size = new System.Drawing.Size(100, 13);
			progressBarLoad.TabIndex = 294;
			progressBarLoad.Visible = false;
			lbWait.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			lbWait.AutoSize = true;
			lbWait.Location = new System.Drawing.Point(365, 30);
			lbWait.Name = "lbWait";
			lbWait.Size = new System.Drawing.Size(70, 13);
			lbWait.TabIndex = 293;
			lbWait.Text = "Please wait...";
			lbWait.Visible = false;
			buttonRefresh.Location = new System.Drawing.Point(287, 25);
			buttonRefresh.Name = "buttonRefresh";
			buttonRefresh.Size = new System.Drawing.Size(70, 21);
			buttonRefresh.TabIndex = 292;
			buttonRefresh.Text = "Refresh";
			buttonRefresh.UseVisualStyleBackColor = true;
			buttonRefresh.Click += new System.EventHandler(buttonRefresh_Click);
			labelViewListName.AutoSize = true;
			labelViewListName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelViewListName.Location = new System.Drawing.Point(526, 30);
			labelViewListName.Name = "labelViewListName";
			labelViewListName.Size = new System.Drawing.Size(0, 13);
			labelViewListName.TabIndex = 295;
			ultraToolTipManager1.ContainingControl = this;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(1052, 586);
			base.Controls.Add(labelViewListName);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "SmartListForm";
			Text = "Smart List";
			base.Load += new System.EventHandler(ProductListForm_Load);
			panelButtons.ResumeLayout(false);
			panelEdit1.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			contextMenuStripTreeView.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			panelEdit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBox1).EndInit();
			ultraExpandableGroupBox1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				dataGridList.ApplyUIDesign();
				dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridList.DisplayLayout.UseFixedHeaders = true;
				dataGridList.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.SingleSummary;
				try
				{
					PublicFunctions.StartWaiting(this);
					LoadCategories();
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		private void HideShowColumns()
		{
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OpenForEdit()
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				FormActivator.ProductDetailsFormObj.LoadData(selectedID);
				FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			New();
		}

		private void New()
		{
			FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void ProductListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
					LoadData();
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditSmartList))
			{
				panelEdit1.Enabled = false;
				treeViewList.LabelEdit = false;
				ToolStripMenuItem toolStripMenuItem = newGroupToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem2 = newReportToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem3 = editToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem4 = moveUpToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem5 = moveDownToolStripMenuItem;
				bool flag2 = deleteToolStripMenuItem.Enabled = false;
				bool flag4 = toolStripMenuItem5.Enabled = flag2;
				bool flag6 = toolStripMenuItem4.Enabled = flag4;
				bool flag8 = toolStripMenuItem3.Enabled = flag6;
				bool enabled = toolStripMenuItem2.Enabled = flag8;
				toolStripMenuItem.Enabled = enabled;
				Button button = buttonAddGroup;
				Button button2 = this.button2;
				flag8 = (button3.Enabled = false);
				enabled = (button2.Enabled = flag8);
				button.Enabled = enabled;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
		}

		public void OnActivated()
		{
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Products;
		}

		public static int GetScreenID()
		{
			return 7004;
		}

		private string GetSelectedID()
		{
			string result = "";
			if (dataGridList.ActiveRow == null)
			{
				return "";
			}
			dataGridList.ActiveRow.GetType();
			if (dataGridList.ActiveRow != null)
			{
				if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
				{
					return "";
				}
				result = dataGridList.ActiveRow.Cells["Item Code"].Text.ToString();
			}
			return result;
		}

		private UltraGridRow GetSelectedItem()
		{
			if (dataGridList.ActiveRow != null)
			{
				return dataGridList.ActiveRow;
			}
			return null;
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void Delete()
		{
			if (!isReadOnly && GetSelectedID() != "")
			{
				try
				{
					ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?");
					_ = 6;
				}
				catch (SqlException ex)
				{
					if (ex.Number == 547)
					{
						ErrorHelper.ErrorMessage("Cannot delete this item because it is in use or referenced by other records.");
					}
					else
					{
						ErrorHelper.ProcessError(ex);
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Item List";
		}

		private void Print()
		{
			try
			{
				string text = Text;
				if (labelViewListName.Text != "")
				{
					text = labelViewListName.Text;
				}
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, text);
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
			textBoxSearch.Clear();
			LoadData();
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void microsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new DataExportHelper().ExportToExcel(dataGridList, labelViewListName.Text);
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
			dataGridList.DisplayLayout.GroupByBox.Hidden = !toolStripButtonAllowGrouping.Checked;
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGridList.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridList.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
		}

		private void LoadCategories()
		{
			string key = "";
			int num = -1;
			if (treeViewList.SelectedNode != null)
			{
				key = treeViewList.SelectedNode.Name;
				num = treeViewList.SelectedNode.ImageIndex;
			}
			treeViewList.Nodes.Clear();
			DataSet categoryList = Factory.SmartListSystem.GetCategoryList();
			DataSet smartList = Factory.SmartListSystem.GetSmartList();
			if (categoryList != null && categoryList.Tables.Count > 0)
			{
				DataRow[] array = categoryList.Tables[0].Select("ParentID IS NULL");
				foreach (DataRow dataRow in array)
				{
					AddTreeNode(null, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), categoryList, smartList);
				}
			}
			TreeNode[] array2 = treeViewList.Nodes.Find(key, searchAllChildren: true);
			for (int j = 0; j < array2.Length; j = checked(j + 1))
			{
				if (array2[j].ImageIndex == num)
				{
					array2[j].EnsureVisible();
					treeViewList.SelectedNode = array2[j];
					break;
				}
			}
			if (!panelEdit1.Enabled)
			{
				int num2 = 0;
				do
				{
					num2 = RemoveEmptyGroups(null);
				}
				while (num2 > 0);
			}
		}

		private int RemoveEmptyGroups(TreeNode parentNode)
		{
			int num = 0;
			TreeNodeCollection nodes = treeViewList.Nodes;
			if (parentNode != null)
			{
				nodes = parentNode.Nodes;
			}
			checked
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					TreeNode treeNode = nodes[i];
					if (treeNode.Tag.ToString() == "1")
					{
						if (treeNode.GetNodeCount(includeSubTrees: false) == 0)
						{
							treeNode.Remove();
							num++;
						}
						else
						{
							num += RemoveEmptyGroups(treeNode);
						}
					}
				}
				return num;
			}
		}

		private void AddTreeNode(TreeNode parent, string categoryID, string categoryName, DataSet data, DataSet reportsData)
		{
			if (parent == null)
			{
				parent = treeViewList.Nodes.Add(categoryID, categoryName, 0, 0);
				parent.Tag = 1;
			}
			else
			{
				parent = parent.Nodes.Add(categoryID, categoryName, 0, 0);
				parent.Tag = 1;
			}
			DataRow[] array = data.Tables[0].Select("ParentID='" + categoryID + "'");
			foreach (DataRow dataRow in array)
			{
				AddTreeNode(parent, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), data, reportsData);
			}
			DataRow[] array2 = reportsData.Tables[0].Select("CategoryID='" + categoryID + "'");
			for (int j = 0; j < array2.Length; j = checked(j + 1))
			{
				if (Security.GetCustomReportAccessRight(CustomReportTypes.SmartList, array2[j]["SmartListID"].ToString()).Visible)
				{
					parent.Nodes.Add(array2[j]["SmartListID"].ToString(), array2[j]["SmartListName"].ToString(), 2, 2).Tag = 2;
				}
			}
		}

		private void newGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewFolder();
		}

		private void NewFolder()
		{
			NewSmartlistGroup newSmartlistGroup = new NewSmartlistGroup();
			string parentID = "-1";
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 0)
			{
				parentID = treeViewList.SelectedNode.Name;
			}
			newSmartlistGroup.ParentID = parentID;
			if (newSmartlistGroup.ShowDialog() == DialogResult.OK)
			{
				parentID = newSmartlistGroup.ParentID;
				if (parentID == "")
				{
					parentID = "-1";
				}
				Factory.SmartListSystem.CreateCategory(newSmartlistGroup.CategoryName, int.Parse(parentID));
				LoadCategories();
			}
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeViewList.SelectedNode == null)
			{
				return;
			}
			if (treeViewList.SelectedNode.ImageIndex == 0)
			{
				string parentID = "-1";
				if (treeViewList.SelectedNode.Parent != null)
				{
					parentID = treeViewList.SelectedNode.Parent.Name;
				}
				NewSmartlistGroup newSmartlistGroup = new NewSmartlistGroup();
				newSmartlistGroup.CategoryName = treeViewList.SelectedNode.Text;
				newSmartlistGroup.CategoryID = treeViewList.SelectedNode.Name;
				newSmartlistGroup.RowIndex = treeViewList.SelectedNode.Index;
				newSmartlistGroup.ParentID = parentID;
				if (newSmartlistGroup.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				parentID = newSmartlistGroup.ParentID;
				if (parentID == "")
				{
					parentID = "-1";
				}
				if (parentID == "-1")
				{
					if (Factory.SmartListSystem.UpdateCategory(newSmartlistGroup.CategoryName, int.Parse(parentID), int.Parse(treeViewList.SelectedNode.Name), treeViewList.SelectedNode.Index))
					{
						treeViewList.SelectedNode.Text = newSmartlistGroup.CategoryName;
					}
				}
				else if (Factory.SmartListSystem.UpdateCategory(newSmartlistGroup.CategoryName, int.Parse(parentID), int.Parse(treeViewList.SelectedNode.Name)))
				{
					treeViewList.SelectedNode.Text = newSmartlistGroup.CategoryName;
				}
			}
			else
			{
				EditReport();
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteItem();
		}

		private void DeleteItem()
		{
			if (treeViewList.SelectedNode == null || ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this item?") != DialogResult.Yes)
			{
				return;
			}
			if (treeViewList.SelectedNode.ImageIndex == 2)
			{
				if (Factory.SmartListSystem.DeleteSmartList(treeViewList.SelectedNode.Name))
				{
					treeViewList.SelectedNode.Remove();
				}
			}
			else if (treeViewList.SelectedNode.Nodes.Count > 0)
			{
				ErrorHelper.WarningMessage("You cannot delete groups which have subgroups or reports.");
			}
			else if (Factory.SmartListSystem.DeleteCategory(treeViewList.SelectedNode.Name))
			{
				treeViewList.SelectedNode.Remove();
			}
		}

		private void NewReport()
		{
			SmartListDetailsForm smartListDetailsForm = new SmartListDetailsForm();
			smartListDetailsForm.SetGroup = true;
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 0)
			{
				smartListDetailsForm.CategoryID = treeViewList.SelectedNode.Name;
			}
			else if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.Parent != null && treeViewList.SelectedNode.ImageIndex == 2)
			{
				smartListDetailsForm.CategoryID = treeViewList.SelectedNode.Parent.Name;
			}
			smartListDetailsForm.Show();
		}

		private void EditReport()
		{
			SmartListDetailsForm smartListDetailsForm = new SmartListDetailsForm();
			smartListDetailsForm.SetGroup = true;
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.Parent != null)
			{
				smartListDetailsForm.Show();
				smartListDetailsForm.LoadData(treeViewList.SelectedNode.Name);
			}
		}

		private void newReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewReport();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			NewReport();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			NewFolder();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			DeleteItem();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (toolStripButtonMerge.Checked)
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Always;
			}
			else
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Never;
			}
		}

		private void toolStripButtonReset_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to reset the report layout to it's default layout?") == DialogResult.Yes)
			{
				dataGridList.DisplayLayout.ResetBands();
				dataGridList.ResetDisplayLayout();
				dataGridList.DataSource = null;
				dataGridList.DataSource = reportData;
				dataGridList.ApplyUIDesign();
				dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridList.DisplayLayout.UseFixedHeaders = true;
				dataGridList.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.SingleSummary;
			}
		}

		private void ultraExpandableGroupBox1_ExpandedStateChanging(object sender, CancelEventArgs e)
		{
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadReport();
		}

		private void toolStripButtonFitText_Click(object sender, EventArgs e)
		{
			dataGridList.AutoSizeColumns();
		}

		private void toolStripButtonClearFilter_Click(object sender, EventArgs e)
		{
			if (dataGridList.DisplayLayout != null)
			{
				_ = dataGridList.DisplayLayout.Bands.Count;
				_ = 0;
			}
		}

		private void toolStripButtonExpand_Click(object sender, EventArgs e)
		{
			if (dataGridList.Rows.Count > 0)
			{
				dataGridList.Rows.ExpandAll(recursive: true);
			}
		}

		private void toolStripButtonCollapse_Click(object sender, EventArgs e)
		{
			if (dataGridList.Rows.Count > 0)
			{
				dataGridList.Rows.CollapseAll(recursive: true);
			}
		}

		public void ShowReport(string reportID)
		{
			foreach (TreeNode node in treeViewList.Nodes)
			{
				if (node.Name == reportID)
				{
					treeViewList.SelectedNode = node;
					break;
				}
				if (node.Nodes.Count > 0 && ShowReport(reportID, node))
				{
					break;
				}
			}
		}

		private bool ShowReport(string reportID, TreeNode node)
		{
			foreach (TreeNode node2 in node.Nodes)
			{
				if (node2.Name == reportID)
				{
					treeViewList.SelectedNode = node2;
					return true;
				}
				if (node2.Nodes.Count > 0)
				{
					ShowReport(reportID, node2);
				}
			}
			return false;
		}

		private void favoriteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 2)
				{
					string name = treeViewList.SelectedNode.Name;
					if (favoriteToolStripMenuItem.Checked)
					{
						Factory.UserFavoriteSystem.CreateUserFavorite(1, name, treeViewList.SelectedNode.Text);
					}
					else
					{
						Factory.UserFavoriteSystem.DeleteUserFavorite(Global.CurrentUser, 1, name);
					}
					GlobalEvents.OnUserFavoriteChanged(FavoriteTypes.SmartList, null);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			LoadCategories(textBoxSearch.Text);
		}

		private void textBoxSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (!(textBoxSearch.Text != ""))
			{
				return;
			}
			treeViewList.BeginUpdate();
			checked
			{
				if (textBoxSearch.Text.Length > 0)
				{
					for (int num = treeViewList.Nodes.Count; num > 0; num--)
					{
						if (NodeFiltering(treeViewList.Nodes[num - 1], textBoxSearch.Text))
						{
							treeViewList.Nodes[num - 1].Expand();
						}
					}
					treeViewList.EndUpdate();
				}
				else
				{
					for (int i = 0; i < treeViewList.Nodes.Count; i++)
					{
						treeViewList.Nodes[i - 1].Collapse();
					}
				}
			}
		}

		private bool NodeFiltering(TreeNode Nodo, string Texto)
		{
			bool result = false;
			checked
			{
				if (Nodo.Nodes.Count == 0)
				{
					if (Nodo.Text.ToUpper().Contains(Texto.ToUpper()))
					{
						result = true;
					}
				}
				else
				{
					for (int num = Nodo.Nodes.Count; num > 0; num--)
					{
						if (NodeFiltering(Nodo.Nodes[num - 1], Texto))
						{
							result = true;
						}
					}
				}
				return result;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			if (treeViewList.SelectedNode.Parent != null)
			{
				return;
			}
			int num = -1;
			string name = treeViewList.SelectedNode.Name;
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			object obj = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "RowIndex", "CategoryID", name) ?? null;
			checked
			{
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					num = int.Parse(obj.ToString());
					object obj2 = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "CategoryID", "RowIndex", num - 1) ?? null;
					string categoryID = "";
					if (obj2 != null)
					{
						categoryID = obj2.ToString();
					}
					bool flag = Factory.SmartListSystem.UpdateSmartListIndex(categoryID, num);
					if (flag)
					{
						flag &= Factory.SmartListSystem.UpdateSmartListIndex(treeViewList.SelectedNode.Name, num - 1);
					}
					if (flag)
					{
						LoadData();
					}
				}
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			if (treeViewList.SelectedNode.Parent != null)
			{
				return;
			}
			int num = -1;
			string name = treeViewList.SelectedNode.Name;
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			object obj = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "RowIndex", "CategoryID", name) ?? null;
			checked
			{
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					num = int.Parse(obj.ToString());
					object obj2 = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "CategoryID", "RowIndex", num + 1) ?? null;
					string categoryID = "";
					if (obj2 != null)
					{
						categoryID = obj2.ToString();
					}
					bool flag = Factory.SmartListSystem.UpdateSmartListIndex(categoryID, num);
					if (flag)
					{
						flag &= Factory.SmartListSystem.UpdateSmartListIndex(treeViewList.SelectedNode.Name, num + 1);
					}
					if (flag)
					{
						LoadData();
					}
				}
			}
		}

		private void buttonAddGroup_Click(object sender, EventArgs e)
		{
			NewFolder();
		}

		private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeViewList.SelectedNode.Parent != null)
			{
				return;
			}
			int num = -1;
			string name = treeViewList.SelectedNode.Name;
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			object obj = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "RowIndex", "CategoryID", name) ?? null;
			checked
			{
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					num = int.Parse(obj.ToString());
					object obj2 = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "CategoryID", "RowIndex", num - 1) ?? null;
					string categoryID = "";
					if (obj2 != null)
					{
						categoryID = obj2.ToString();
					}
					bool flag = Factory.SmartListSystem.UpdateSmartListIndex(categoryID, num);
					if (flag)
					{
						flag &= Factory.SmartListSystem.UpdateSmartListIndex(treeViewList.SelectedNode.Name, num - 1);
					}
					if (flag)
					{
						LoadData();
					}
				}
			}
		}

		private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeViewList.SelectedNode.Parent != null)
			{
				return;
			}
			int num = -1;
			string name = treeViewList.SelectedNode.Name;
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			object obj = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "RowIndex", "CategoryID", name) ?? null;
			checked
			{
				if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
				{
					num = int.Parse(obj.ToString());
					object obj2 = Factory.DatabaseSystem.GetFieldValue("Smartlist_Category", "CategoryID", "RowIndex", num + 1) ?? null;
					string categoryID = "";
					if (obj2 != null)
					{
						categoryID = obj2.ToString();
					}
					bool flag = Factory.SmartListSystem.UpdateSmartListIndex(categoryID, num);
					if (flag)
					{
						flag &= Factory.SmartListSystem.UpdateSmartListIndex(treeViewList.SelectedNode.Name, num + 1);
					}
					if (flag)
					{
						LoadData();
					}
				}
			}
		}

		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void SetToolTip(string text)
		{
			if (text != "")
			{
				ultraPictureBoxInformation.Visible = true;
				UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo(text, ToolTipImage.Info, "Display Note", DefaultableBoolean.True);
				ultraToolTipInfo.Appearance.BackColor = Color.White;
				ultraToolTipInfo.Appearance.BackColor2 = Color.LightBlue;
				ultraToolTipInfo.Appearance.BackGradientStyle = GradientStyle.Circular;
				ultraToolTipInfo.Appearance.ForeColor = Color.Black;
				ultraToolTipManager1.SetUltraToolTip(ultraPictureBoxInformation, ultraToolTipInfo);
				ultraToolTipManager1.DisplayStyle = ToolTipDisplayStyle.BalloonTip;
			}
			else
			{
				ultraPictureBoxInformation.Visible = false;
			}
		}

		private void ultraPictureBox1_Click(object sender, EventArgs e)
		{
		}

		private void LoadCategories(string filterText)
		{
			if (treeViewList.SelectedNode != null)
			{
				_ = treeViewList.SelectedNode.Name;
				_ = treeViewList.SelectedNode.ImageIndex;
			}
			treeViewList.Nodes.Clear();
			DataSet categoryList = Factory.SmartListSystem.GetCategoryList();
			DataSet smartList = Factory.SmartListSystem.GetSmartList();
			DataTable dataTable = smartList.Tables["SmartList"].Clone();
			DataRow[] array = smartList.Tables["SmartList"].Select("SmartListName Like '%" + filterText + "%'");
			foreach (DataRow row in array)
			{
				dataTable.ImportRow(row);
			}
			smartList.Tables.Clear();
			smartList.Tables.Add(dataTable);
			if (categoryList != null && categoryList.Tables.Count > 0)
			{
				DataRow[] array2 = categoryList.Tables[0].Select("ParentID IS NULL");
				foreach (DataRow dataRow in array2)
				{
					AddTreeNode(null, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), categoryList, smartList);
				}
			}
			if (!panelEdit1.Enabled)
			{
				int num = 0;
				do
				{
					num = RemoveEmptyGroups(null);
				}
				while (num > 0);
			}
		}
	}
}
