using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class VerifyObjectForm : Form, IForm
	{
		private bool isFirstTimeActivating = true;

		private bool canAccessCost = true;

		private DataSet reportData;

		private DataSet approvalTaskData;

		private string parm1;

		private string parm2;

		private string parm3;

		private string parm4;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet productData;

		private bool showInactiveItems;

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

		private ToolStripButton toolStripButtonAutoFit;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem toolStripMenuItemLocationWiseOnhand;

		private TreeView treeViewList;

		private SplitContainer splitContainer1;

		private ImageList imageList1;

		private System.Windows.Forms.ToolTip toolTip1;

		private ToolStripButton toolStripButtonFitText;

		private ToolStrip toolStrip1;

		private bool isSubReport;

		private ScreenAccessRight screenRight;

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

		public VerifyObjectForm()
		{
			InitializeComponent();
			base.Activated += ProductListForm_Activated;
			treeViewList.NodeMouseClick += treeView1_NodeMouseClick;
			treeViewList.Click += treeView1_Click;
			treeViewList.AfterSelect += treeView1_AfterSelect;
			treeViewList.BeforeSelect += treeView1_BeforeSelect;
			base.FormClosing += VendorListForm_FormClosing;
			dataGridList.AllowUnfittedView = true;
			treeViewList.LabelEdit = false;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.ClickCellButton += dataGridList_ClickCellButton;
			GlobalEvents.ApprovalStatusChanged += GlobalEvents_ApprovalStatusChanged;
			treeViewList.ItemHeight = 18;
		}

		private void dataGridList_ClickCellButton(object sender, CellEventArgs e)
		{
			UltraGridRow row = e.Cell.Row;
			if (row == null || !row.IsDataRow)
			{
				return;
			}
			if (e.Cell.Column.Key == "View")
			{
				OpenForView();
			}
			else if (e.Cell.Column.Key == "Verify")
			{
				int num = 1;
				num = int.Parse(row.Cells["ObjectType"].Value.ToString());
				int objectID = int.Parse(row.Cells["ObjectID"].Value.ToString());
				int taskID = int.Parse(row.Cells["TaskID"].Value.ToString());
				DoubleString tableName = Factory.ApprovalSystem.GetTableName(num, objectID);
				if (Factory.ApprovalSystem.ApproveTask(taskID, tableName.FirstString, tableName.SecondString))
				{
					e.Cell.Row.Delete(displayPrompt: false);
				}
			}
		}

		private void GlobalEvents_ApprovalStatusChanged(ApprovalStatus newStatus, int taskID)
		{
			try
			{
				int approvalTaskStatusByID = Factory.ApprovalSystem.GetApprovalTaskStatusByID(taskID);
				if (approvalTaskStatusByID > 0 && dataGridList.Rows != null && approvalTaskStatusByID != 1)
				{
					int num = 0;
					UltraGridRow ultraGridRow;
					while (true)
					{
						if (num >= dataGridList.Rows.Count)
						{
							return;
						}
						ultraGridRow = dataGridList.Rows[num];
						if (ultraGridRow.Cells["TaskID"].Value.ToString() == taskID.ToString())
						{
							break;
						}
						num = checked(num + 1);
					}
					ultraGridRow.Delete(displayPrompt: false);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				LoadReport();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadReport()
		{
			try
			{
				TreeNode selectedNode = treeViewList.SelectedNode;
				if (selectedNode != null && treeViewList.SelectedNode != null)
				{
					LoadReportData(selectedNode.Name);
					try
					{
						dataGridList.LoadLayout(selectedNode.Name);
					}
					catch
					{
					}
					dataGridList.ApplyFormat();
					dataGridList.FormatAllNumericColumns(new string[2]
					{
						"ObjectType",
						"ObjectID"
					});
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadReportData(string approvalID)
		{
			try
			{
				approvalTaskData = Factory.ApprovalSystem.GetUserPendingApprovalTasks(ApprovalTypes.Verification, approvalID);
				dataGridList.DataSource = approvalTaskData;
				if (!dataGridList.DisplayLayout.Bands[0].Columns.Exists("View"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns.Add("View", "View");
					dataGridList.DisplayLayout.Bands[0].Columns.Add("Verify", "Verify");
					Infragistics.Win.UltraWinGrid.ButtonDisplayStyle buttonDisplayStyle3 = dataGridList.DisplayLayout.Bands[0].Columns["View"].ButtonDisplayStyle = (dataGridList.DisplayLayout.Bands[0].Columns["Verify"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always);
					dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.BackColor = SystemColors.ButtonFace;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.ForeColor = Color.White;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.BorderAlpha = Alpha.Transparent;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].AllowGroupBy = DefaultableBoolean.False;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].AllowRowFiltering = DefaultableBoolean.False;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].AllowRowSummaries = AllowRowSummaries.False;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.ImageHAlign = HAlign.Center;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.Image = imageList1.Images[3];
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.BackColor = SystemColors.ButtonFace;
					dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.TextHAlign = HAlign.Center;
					dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.TextHAlign = HAlign.Center;
					dataGridList.DisplayLayout.Bands[0].Columns["View"].AllowGroupBy = DefaultableBoolean.False;
					dataGridList.DisplayLayout.Bands[0].Columns["View"].AllowRowFiltering = DefaultableBoolean.False;
					dataGridList.DisplayLayout.Bands[0].Columns["View"].AllowRowSummaries = AllowRowSummaries.False;
					dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.ImageHAlign = HAlign.Center;
					dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.Image = imageList1.Images[4];
				}
				dataGridList.DisplayLayout.Bands[0].Columns["View"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridList.DisplayLayout.Bands[0].Columns["Verify"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["ObjectType"];
				UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectID"];
				bool flag2 = dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].Hidden = true;
				bool hidden = ultraGridColumn2.Hidden = flag2;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn3 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectType"];
				UltraGridColumn ultraGridColumn4 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectID"];
				ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn3.ExcludeFromColumnChooser = (ultraGridColumn4.ExcludeFromColumnChooser = excludeFromColumnChooser2);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			if (e.Row.IsDataRow)
			{
				OpenForView();
			}
		}

		private void OpenForView()
		{
			UltraGridRow activeRow = dataGridList.ActiveRow;
			if (activeRow == null || !activeRow.IsDataRow)
			{
				return;
			}
			int num = int.Parse(activeRow.Cells["ObjectType"].Value.ToString());
			int num2 = int.Parse(activeRow.Cells["ObjectID"].Value.ToString());
			int approvalTaskID = int.Parse(activeRow.Cells["TaskID"].Value.ToString());
			if (num == 1)
			{
				SysDocTypes sysDocTypes = (SysDocTypes)num2;
				string sysDocID = "";
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("SysDocID"))
				{
					activeRow.Cells["SysDocID"].Value.ToString();
				}
				string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
				switch (sysDocTypes)
				{
				case SysDocTypes.CashPayment:
					FormActivator.BringFormToFront(FormActivator.CashPaymentFormObj);
					FormActivator.CashPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.TTPayment:
					FormActivator.BringFormToFront(FormActivator.TTPaymentFormObj);
					FormActivator.TTPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ChequePayment:
					FormActivator.BringFormToFront(FormActivator.ChequePaymentFormObj);
					FormActivator.ChequePaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				default:
					new FormHelper().EditTransaction(sysDocID, voucherID);
					break;
				}
			}
			else
			{
				string id = activeRow.Cells["Code"].Value.ToString();
				new FormHelper().OpenCardForEdit((DataComboType)num2, id);
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.VerifyObjectForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
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
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemLocationWiseOnhand = new System.Windows.Forms.ToolStripMenuItem();
			treeViewList = new System.Windows.Forms.TreeView();
			imageList1 = new System.Windows.Forms.ImageList(components);
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 532);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(980, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(980, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(872, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButtonFitText
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(980, 25);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(66, 22);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(69, 22);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(110, 22);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.autofit;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(92, 22);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(79, 22);
			toolStripButtonFitText.Text = "Fit to Text";
			toolStripButtonFitText.Click += new System.EventHandler(toolStripButtonFitText_Click);
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
			dataGridList.Location = new System.Drawing.Point(0, 0);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(721, 488);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemLocationWiseOnhand.Name = "toolStripMenuItemLocationWiseOnhand";
			toolStripMenuItemLocationWiseOnhand.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemLocationWiseOnhand.Text = "Location Wise Onhand...";
			treeViewList.AllowDrop = true;
			treeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			treeViewList.FullRowSelect = true;
			treeViewList.HideSelection = false;
			treeViewList.ImageIndex = 0;
			treeViewList.ImageList = imageList1;
			treeViewList.Location = new System.Drawing.Point(0, 0);
			treeViewList.Name = "treeViewList";
			treeViewList.SelectedImageIndex = 1;
			treeViewList.ShowLines = false;
			treeViewList.Size = new System.Drawing.Size(231, 488);
			treeViewList.StateImageList = imageList1;
			treeViewList.TabIndex = 291;
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "folder_closed.png");
			imageList1.Images.SetKeyName(1, "folder_open.png");
			imageList1.Images.SetKeyName(2, "repport16x16.png");
			imageList1.Images.SetKeyName(3, "verification.png");
			imageList1.Images.SetKeyName(4, "icon-view.png");
			splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainer1.Location = new System.Drawing.Point(12, 38);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(treeViewList);
			splitContainer1.Panel2.Controls.Add(dataGridList);
			splitContainer1.Size = new System.Drawing.Size(956, 488);
			splitContainer1.SplitterDistance = 231;
			splitContainer1.TabIndex = 292;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(980, 572);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "VerifyObjectForm";
			Text = "Approval Form";
			base.Load += new System.EventHandler(ProductListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
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
			new DataExportHelper().ExportToExcel(dataGridList);
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
			if (treeViewList.SelectedNode != null)
			{
				_ = treeViewList.SelectedNode.Name;
				_ = treeViewList.SelectedNode.ImageIndex;
			}
			treeViewList.Nodes.Clear();
			DataSet userApprovalsWithPendingTasks = Factory.ApprovalSystem.GetUserApprovalsWithPendingTasks(ApprovalTypes.Verification);
			if (userApprovalsWithPendingTasks != null && userApprovalsWithPendingTasks.Tables.Count > 0)
			{
				for (int i = 0; i < userApprovalsWithPendingTasks.Tables[0].Rows.Count; i = checked(i + 1))
				{
					DataRow dataRow = userApprovalsWithPendingTasks.Tables[0].Rows[i];
					AddTreeNode(null, dataRow["ApprovalID"].ToString(), dataRow["ApprovalName"].ToString(), userApprovalsWithPendingTasks);
				}
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

		private void AddTreeNode(TreeNode parent, string categoryID, string categoryName, DataSet data)
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
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
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

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}
	}
}
