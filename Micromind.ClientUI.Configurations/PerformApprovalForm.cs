using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
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
	public class PerformApprovalForm : Form, IForm
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

		private TreeView treeViewApproval;

		private SplitContainer splitContainer1;

		private ImageList imageList1;

		private System.Windows.Forms.ToolTip toolTip1;

		private ToolStripButton toolStripButtonFitText;

		private NavBarControl navBarControl;

		private NavBarGroup navBarGroupApproval;

		private NavBarGroupControlContainer navBarGroupControlContainer13;

		private NavBarGroupControlContainer navBarGroupControlContainer11;

		private NavBarGroup navBarGroupVerification;

		private NavBarGroup navBarGroupCheckList;

		private NavBarItem navBarItemAccount;

		private NavBarItem navBarItemAccountList;

		private NavBarItem navBarItemJV;

		private NavBarItem navBarItemDebitNote;

		private NavBarItem navBarItemCreditNote;

		private NavBarItem navBarItemCashReceipt;

		private NavBarItem navBarItemChequeReceipt;

		private NavBarItem navBarItemChequeDeposit;

		private NavBarItem navBarItemCashPayment;

		private NavBarItem navBarItemChequePayment;

		private NavBarItem navBarItemTransfer;

		private NavBarItem navBarItemCustomer;

		private NavBarItem navBarItemCustomerList;

		private NavBarGroupControlContainer navBarGroupControlContainer1;

		private TreeView treeViewCheckList;

		private TreeView treeViewVerification;

		private ToolStripButton toolStripButtonPreviewPane;

		private SplitContainer splitContainerMain;

		private PrintControl printControl1;

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
					treeViewApproval.Visible = false;
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

		public PerformApprovalForm()
		{
			InitializeComponent();
			base.Activated += ProductListForm_Activated;
			treeViewApproval.AfterSelect += treeView1_AfterSelect;
			treeViewVerification.AfterSelect += treeView1_AfterSelect;
			treeViewCheckList.AfterSelect += treeView1_AfterSelect;
			navBarControl.GroupExpanded += navBarControl_GroupExpanded;
			base.FormClosing += VendorListForm_FormClosing;
			toolStripButtonPreviewPane.CheckedChanged += toolStripButtonPreviewPane_Click;
			dataGridList.AllowUnfittedView = true;
			treeViewApproval.LabelEdit = false;
			dataGridList.AfterRowActivate += dataGridList_AfterRowActivate;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.ClickCellButton += dataGridList_ClickCellButton;
			GlobalEvents.ApprovalStatusChanged += GlobalEvents_ApprovalStatusChanged;
			treeViewApproval.ItemHeight = 18;
			Global.GlobalSettings.LoadFormProperties(this);
			toolStripButtonPreviewPane.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "PreviewPane", defaultValue: false);
		}

		private void dataGridList_AfterRowActivate(object sender, EventArgs e)
		{
			try
			{
				if (toolStripButtonPreviewPane.Checked)
				{
					UltraGridRow activeRow = dataGridList.ActiveRow;
					if (activeRow != null && activeRow.IsDataRow)
					{
						int num = int.Parse(activeRow.Cells["ObjectType"].Value.ToString());
						int num2 = int.Parse(activeRow.Cells["ObjectID"].Value.ToString());
						int.Parse(activeRow.Cells["TaskID"].Value.ToString());
						if (num == 1)
						{
							string sysDocID = "";
							if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("SysDocID"))
							{
								sysDocID = activeRow.Cells["SysDocID"].Value.ToString();
							}
							string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
							FormHelper formHelper = new FormHelper();
							new DocumentViewForm();
							XtraReport transactionPreviewDoc = formHelper.GetTransactionPreviewDoc(sysDocID, voucherID);
							if (transactionPreviewDoc != null)
							{
								printControl1.PrintingSystem = transactionPreviewDoc.PrintingSystem;
								transactionPreviewDoc.CreateDocument();
							}
						}
						else
						{
							printControl1.PrintingSystem.ClearContent();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void navBarControl_GroupExpanded(object sender, NavBarGroupEventArgs e)
		{
			dataGridList.DataSource = null;
		}

		private void dataGridList_ClickCellButton(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					UltraGridRow row = e.Cell.Row;
					if (row != null && row.IsDataRow)
					{
						if (e.Cell.Column.Key == "View")
						{
							if (navBarGroupApproval.Expanded)
							{
								OpenForApproval();
							}
							else if (navBarGroupVerification.Expanded)
							{
								OpenForView();
							}
						}
						else if (e.Cell.Column.Key == "Verify")
						{
							int num = 1;
							num = int.Parse(row.Cells["ObjectType"].Value.ToString());
							int objectID = int.Parse(row.Cells["ObjectID"].Value.ToString());
							int taskID = int.Parse(row.Cells["TaskID"].Value.ToString());
							DoubleString tableName = Factory.ApprovalSystem.GetTableName(num, objectID);
							if (Factory.ApprovalSystem.ApproveTaskVerification(taskID, tableName.FirstString, tableName.SecondString))
							{
								int index = e.Cell.Row.Index;
								e.Cell.Row.Delete(displayPrompt: false);
								if (toolStripButtonPreviewPane.Checked && printControl1.PrintingSystem != null)
								{
									printControl1.PrintingSystem.ClearContent();
								}
								if (index < dataGridList.Rows.Count - 1)
								{
									dataGridList.Rows[index].Activate();
								}
								else if (index > 0)
								{
									dataGridList.Rows[index - 1].Activate();
								}
							}
						}
						else if (e.Cell.Column.Key == "Approve")
						{
							int num2 = 1;
							num2 = int.Parse(row.Cells["ObjectType"].Value.ToString());
							int objectID2 = int.Parse(row.Cells["ObjectID"].Value.ToString());
							int taskID2 = int.Parse(row.Cells["TaskID"].Value.ToString());
							DoubleString tableName2 = Factory.ApprovalSystem.GetTableName(num2, objectID2);
							if (Factory.ApprovalSystem.ApproveTask(taskID2, tableName2.FirstString, tableName2.SecondString))
							{
								e.Cell.Row.Delete(displayPrompt: false);
							}
						}
						else if (e.Cell.Column.Key == "Reject")
						{
							int num3 = 1;
							num3 = int.Parse(row.Cells["ObjectType"].Value.ToString());
							int objectID3 = int.Parse(row.Cells["ObjectID"].Value.ToString());
							int taskID3 = int.Parse(row.Cells["TaskID"].Value.ToString());
							DoubleString tableName3 = Factory.ApprovalSystem.GetTableName(num3, objectID3);
							if (Factory.ApprovalSystem.RejectTask(taskID3, tableName3.FirstString, tableName3.SecondString))
							{
								e.Cell.Row.Delete(displayPrompt: false);
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
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

		private void VendorListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "PreviewPane", toolStripButtonPreviewPane.Checked);
				Global.GlobalSettings.SaveFormProperties(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
				TreeNode selectedNode = treeViewApproval.SelectedNode;
				if (navBarGroupVerification.Expanded)
				{
					selectedNode = treeViewVerification.SelectedNode;
				}
				else if (navBarGroupCheckList.Expanded)
				{
					selectedNode = treeViewCheckList.SelectedNode;
				}
				if (selectedNode != null && selectedNode != null)
				{
					LoadReportData(selectedNode.Name);
					dataGridList.ApplyFormat();
					dataGridList.FormatAllNumericColumns(new string[5]
					{
						"ObjectType",
						"ObjectID",
						"Month",
						"Year",
						"Age"
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
				if (navBarGroupVerification.Expanded)
				{
					approvalTaskData = Factory.ApprovalSystem.GetUserPendingApprovalTasks(ApprovalTypes.Verification, approvalID);
				}
				else if (navBarGroupCheckList.Expanded)
				{
					approvalTaskData = Factory.CheckListSystem.GetUserPendingCheckListTasks(CheckListTypes.CheckList, approvalID);
				}
				else
				{
					approvalTaskData = Factory.ApprovalSystem.GetUserPendingApprovalTasks(ApprovalTypes.Approval, approvalID);
				}
				dataGridList.DataSource = approvalTaskData;
				if (navBarGroupApproval.Expanded)
				{
					if (!dataGridList.DisplayLayout.Bands[0].Columns.Exists("View"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns.Add("View", "View");
						dataGridList.DisplayLayout.Bands[0].Columns.Add("Reject", "Reject");
						dataGridList.DisplayLayout.Bands[0].Columns.Add("Approve", "Approve");
						UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["View"];
						UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["Reject"];
						Infragistics.Win.UltraWinGrid.ButtonDisplayStyle buttonDisplayStyle2 = dataGridList.DisplayLayout.Bands[0].Columns["Approve"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
						Infragistics.Win.UltraWinGrid.ButtonDisplayStyle buttonDisplayStyle5 = ultraGridColumn.ButtonDisplayStyle = (ultraGridColumn2.ButtonDisplayStyle = buttonDisplayStyle2);
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].CellButtonAppearance.ForeColor = Color.White;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].CellButtonAppearance.BorderAlpha = Alpha.Transparent;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].AllowGroupBy = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].AllowRowFiltering = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].AllowRowSummaries = AllowRowSummaries.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].CellButtonAppearance.ImageHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].CellButtonAppearance.Image = imageList1.Images[2];
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].CellButtonAppearance.BackColor = SystemColors.ButtonFace;
						dataGridList.DisplayLayout.Bands[0].Columns["Approve"].CellButtonAppearance.TextHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].CellButtonAppearance.ForeColor = Color.White;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].CellButtonAppearance.BorderAlpha = Alpha.Transparent;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].AllowGroupBy = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].AllowRowFiltering = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].AllowRowSummaries = AllowRowSummaries.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].CellButtonAppearance.ImageHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].CellButtonAppearance.Image = imageList1.Images[3];
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].CellButtonAppearance.BackColor = SystemColors.ButtonFace;
						dataGridList.DisplayLayout.Bands[0].Columns["Reject"].CellButtonAppearance.TextHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.BackColor = SystemColors.ButtonFace;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.TextHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].AllowGroupBy = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].AllowRowFiltering = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].AllowRowSummaries = AllowRowSummaries.False;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.ImageHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.Image = imageList1.Images[4];
					}
					dataGridList.DisplayLayout.Bands[0].Columns["View"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
					dataGridList.DisplayLayout.Bands[0].Columns["Approve"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
					dataGridList.DisplayLayout.Bands[0].Columns["Reject"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
					UltraGridColumn ultraGridColumn3 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectType"];
					UltraGridColumn ultraGridColumn4 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectID"];
					bool flag2 = dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].Hidden = true;
					bool hidden = ultraGridColumn4.Hidden = flag2;
					ultraGridColumn3.Hidden = hidden;
					UltraGridColumn ultraGridColumn5 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectType"];
					UltraGridColumn ultraGridColumn6 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectID"];
					ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn5.ExcludeFromColumnChooser = (ultraGridColumn6.ExcludeFromColumnChooser = excludeFromColumnChooser2);
				}
				else if (navBarGroupVerification.Expanded)
				{
					if (!dataGridList.DisplayLayout.Bands[0].Columns.Exists("View"))
					{
						dataGridList.DisplayLayout.Bands[0].Columns.Add("View", "View");
						dataGridList.DisplayLayout.Bands[0].Columns.Add("Verify", "Verify");
						Infragistics.Win.UltraWinGrid.ButtonDisplayStyle buttonDisplayStyle5 = dataGridList.DisplayLayout.Bands[0].Columns["View"].ButtonDisplayStyle = (dataGridList.DisplayLayout.Bands[0].Columns["Verify"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always);
						dataGridList.DisplayLayout.Bands[0].Columns["View"].CellButtonAppearance.BackColor = SystemColors.ButtonFace;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.ForeColor = Color.White;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.BorderAlpha = Alpha.Transparent;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].AllowGroupBy = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].AllowRowFiltering = DefaultableBoolean.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].AllowRowSummaries = AllowRowSummaries.False;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.ImageHAlign = HAlign.Center;
						dataGridList.DisplayLayout.Bands[0].Columns["Verify"].CellButtonAppearance.Image = imageList1.Images[2];
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
					UltraGridColumn ultraGridColumn7 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectType"];
					UltraGridColumn ultraGridColumn8 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectID"];
					bool flag2 = dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].Hidden = true;
					bool hidden = ultraGridColumn8.Hidden = flag2;
					ultraGridColumn7.Hidden = hidden;
					UltraGridColumn ultraGridColumn9 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectType"];
					UltraGridColumn ultraGridColumn10 = dataGridList.DisplayLayout.Bands[0].Columns["ObjectID"];
					ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn9.ExcludeFromColumnChooser = (ultraGridColumn10.ExcludeFromColumnChooser = excludeFromColumnChooser2);
				}
				else if (navBarGroupCheckList.Expanded)
				{
					dataGridList.DisplayLayout.Bands[0].Columns["Description"].Width = 400;
					dataGridList.DisplayLayout.Bands[0].Columns["Status"].Hidden = true;
					dataGridList.DisplayLayout.Bands[0].Columns["Status"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridList.DisplayLayout.Bands[0].Columns["TaskStatus"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
					dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].Hidden = true;
					dataGridList.DisplayLayout.Bands[0].Columns["TaskID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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
			try
			{
				if (e.Row.IsDataRow)
				{
					if (navBarGroupApproval.Expanded)
					{
						OpenForApproval();
					}
					else if (navBarGroupVerification.Expanded)
					{
						OpenForView();
					}
					else
					{
						CloseTaskDialog closeTaskDialog = new CloseTaskDialog();
						if (closeTaskDialog.ShowDialog(this) == DialogResult.OK)
						{
							int num = int.Parse(dataGridList.ActiveRow.Cells["TaskID"].Value.ToString());
							Factory.CheckListSystem.CloseTask(num.ToString(), closeTaskDialog.EnteredName);
						}
					}
				}
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1057)
				{
					ErrorHelper.WarningMessage("This task is not open. It could be closed by another user. Please refresh and try again.");
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

		private void OpenForApproval()
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
					sysDocID = activeRow.Cells["SysDocID"].Value.ToString();
				}
				string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
				switch (sysDocTypes)
				{
				case SysDocTypes.PurchaseOrder:
					FormActivator.BringFormToFront(FormActivator.PurchaseOrderFormObj);
					FormActivator.PurchaseOrderFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.PurchaseOrderNI:
					FormActivator.BringFormToFront(FormActivator.PurchaseOrderNonInvFormObj);
					FormActivator.PurchaseOrderNonInvFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ImportPurchaseOrder:
					FormActivator.BringFormToFront(FormActivator.PurchaseOrderImportFormObj);
					FormActivator.PurchaseOrderImportFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.PurchaseInvoice:
					FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceFormObj);
					FormActivator.PurchaseInvoiceFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ImportPurchaseInvoice:
					FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceImportFormObj);
					FormActivator.PurchaseInvoiceImportFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ProformaInvoice:
					FormActivator.BringFormToFront(FormActivator.ProformaInvoiceFormObj);
					FormActivator.ProformaInvoiceFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.CashPurchase:
					FormActivator.BringFormToFront(FormActivator.CashPurchaseFormObj);
					FormActivator.CashPurchaseFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.PurchaseInvoiceNI:
					FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceNonInvFormObj);
					FormActivator.PurchaseInvoiceNonInvFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.GoodsReceivedNote:
					FormActivator.BringFormToFront(FormActivator.PurchaseReceiptFormObj);
					FormActivator.PurchaseReceiptFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ImportGoodsReceivedNote:
					FormActivator.BringFormToFront(FormActivator.ImportPurchaseGRNFormObj);
					FormActivator.ImportPurchaseGRNFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.PurchaseQuote:
					FormActivator.BringFormToFront(FormActivator.PurchaseQuoteFormObj);
					FormActivator.PurchaseQuoteFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.CashPayment:
					FormActivator.BringFormToFront(FormActivator.CashPaymentFormObj);
					FormActivator.CashPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.CashExpense:
					FormActivator.BringFormToFront(FormActivator.CashExpenseEntryFormObj);
					FormActivator.CashExpenseEntryFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.TTPayment:
					FormActivator.BringFormToFront(FormActivator.TTPaymentFormObj);
					FormActivator.TTPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ChequePayment:
					FormActivator.BringFormToFront(FormActivator.ChequePaymentFormObj);
					FormActivator.ChequePaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalesEnquiry:
					FormActivator.BringFormToFront(FormActivator.SalesEnquiryFormObj);
					FormActivator.SalesEnquiryFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalesOrder:
					FormActivator.BringFormToFront(FormActivator.SalesOrderFormObj);
					FormActivator.SalesOrderFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ExportSalesOrder:
					FormActivator.BringFormToFront(FormActivator.ExportSalesOrderFormObj);
					FormActivator.ExportSalesOrderFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalesQuote:
					FormActivator.BringFormToFront(FormActivator.SalesQuoteFormObj);
					FormActivator.SalesQuoteFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalesInvoice:
					FormActivator.BringFormToFront(FormActivator.SalesInvoiceFormObj);
					FormActivator.SalesInvoiceFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.DeliveryNote:
					FormActivator.BringFormToFront(FormActivator.DeliveryNoteFormObj);
					FormActivator.DeliveryNoteFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalarySheet:
					FormActivator.BringFormToFront(FormActivator.SalarySheetFormObj);
					FormActivator.SalarySheetFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.OverTimeEntry:
					FormActivator.BringFormToFront(FormActivator.OverTimeEntryFormObj);
					FormActivator.OverTimeEntryFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.GJournal:
					FormActivator.BringFormToFront(FormActivator.JournalEntryFormObj);
					FormActivator.JournalEntryFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.FixedAssetTransfer:
					FormActivator.BringFormToFront(FormActivator.FixedAssetTransferFormObj);
					FormActivator.FixedAssetTransferFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobMaterialRequisition:
					FormActivator.BringFormToFront(FormActivator.JobMaterialRequesitionFormObj);
					FormActivator.JobMaterialRequesitionFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.EmployeeAppraisal:
					FormActivator.BringFormToFront(FormActivator.EmployeeAppraisalFormObj);
					FormActivator.EmployeeAppraisalFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.EmployeeGeneralActivity:
					FormActivator.BringFormToFront(FormActivator.EmployeeGeneralActivityFormObj);
					FormActivator.EmployeeGeneralActivityFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.CLVoucher:
					FormActivator.BringFormToFront(FormActivator.CLVoucherFormObj);
					FormActivator.CLVoucherFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.CreditLimitReview:
					FormActivator.BringFormToFront(FormActivator.CreditLimitReviewFormObj);
					break;
				case SysDocTypes.PurchasePrepaymentInvoice:
					FormActivator.BringFormToFront(FormActivator.PurchasePrepaymentInvoiceFormObj);
					FormActivator.PurchasePrepaymentInvoiceFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.InventoryAdjustment:
					FormActivator.BringFormToFront(FormActivator.InventoryAdjustmentsFormObj);
					FormActivator.InventoryAdjustmentsFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.InventoryNoneSale:
					FormActivator.BringFormToFront(FormActivator.InventoryDamageFormObj);
					FormActivator.InventoryDamageFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.InventoryRepacking:
					FormActivator.BringFormToFront(FormActivator.InventoryRepackingFormObj);
					FormActivator.InventoryRepackingFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ProjectSubContractPO:
					FormActivator.BringFormToFront(FormActivator.ProjectSubContractPOFormObj);
					FormActivator.ProjectSubContractPOFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ProjectSubContractPI:
					FormActivator.BringFormToFront(FormActivator.ProjectSubContractPIFormObj);
					FormActivator.ProjectSubContractPIFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ServiceCallTrack:
					FormActivator.BringFormToFront(FormActivator.ServiceCallTrackFormObj);
					FormActivator.ServiceCallTrackFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobEstimation:
					FormActivator.BringFormToFront(FormActivator.JobEstimationFormObj);
					FormActivator.JobEstimationFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobTimesheet:
					FormActivator.BringFormToFront(FormActivator.JobTimesheetFormObj);
					FormActivator.JobTimesheetFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobExpenseIssue:
					FormActivator.BringFormToFront(FormActivator.JobExpenseIssueFormObj);
					FormActivator.JobExpenseIssueFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobInventoryIssue:
					FormActivator.BringFormToFront(FormActivator.JobInventoryIssueFormObj);
					FormActivator.JobInventoryIssueFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobInventoryReturn:
					FormActivator.BringFormToFront(FormActivator.JobInventoryReturnFormObj);
					FormActivator.JobInventoryReturnFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobInvoice:
					FormActivator.BringFormToFront(FormActivator.JobInvoiceFormObj);
					FormActivator.JobInvoiceFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobClosing:
					FormActivator.BringFormToFront(FormActivator.ProjectClosingFormObj);
					FormActivator.ProjectClosingFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.JobMaintenanceServiceEntry:
					FormActivator.BringFormToFront(FormActivator.JobMaintenanceServiceEntryFormObj);
					FormActivator.JobMaintenanceServiceEntryFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalaryPaymentCash:
					FormActivator.BringFormToFront(FormActivator.CashSalaryPaymentFormObj);
					FormActivator.CashSalaryPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalaryPaymentCheque:
					FormActivator.BringFormToFront(FormActivator.ChequeSalaryPaymentFormObj);
					FormActivator.ChequeSalaryPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SalaryPaymentBank:
					FormActivator.BringFormToFront(FormActivator.TransferSalaryPaymentFormObj);
					FormActivator.TransferSalaryPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ProjectExpenseAllocation:
					FormActivator.BringFormToFront(FormActivator.ProjectExpenseAllocationFormObj);
					FormActivator.ProjectExpenseAllocationFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.CashReceiptMultiple:
					FormActivator.BringFormToFront(FormActivator.CashReceiptMultiPayeeFormObj);
					FormActivator.CashReceiptMultiPayeeFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ChequeReceipt:
					FormActivator.BringFormToFront(FormActivator.ChequeReceiptFormObj);
					FormActivator.ChequeReceiptFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.FundTransfer:
					FormActivator.BringFormToFront(FormActivator.FundTransferFormObj);
					FormActivator.FundTransferFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.TTReceipt:
					FormActivator.BringFormToFront(FormActivator.TTReceiptFormObj);
					FormActivator.TTReceiptFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.LPOReceipt:
					FormActivator.BringFormToFront(FormActivator.LPOReceiptFormObj);
					FormActivator.LPOReceiptFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.TR:
					FormActivator.BringFormToFront(FormActivator.TREntryFormObj);
					FormActivator.TREntryFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.TRPayment:
					FormActivator.BringFormToFront(FormActivator.TRPaymentFormObj);
					FormActivator.TRPaymentFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.TRApplication:
					FormActivator.BringFormToFront(FormActivator.TRApplicationFormObj);
					FormActivator.TRApplicationFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.SendChequesToBank:
					FormActivator.BringFormToFront(FormActivator.SendChequesToBankFormObj);
					FormActivator.SendChequesToBankFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ChequeDeposit:
					FormActivator.BringFormToFront(FormActivator.ChequeDepositFormObj);
					FormActivator.ChequeDepositFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ChequeDiscount:
					FormActivator.BringFormToFront(FormActivator.ChequeDiscountFormObj);
					FormActivator.ChequeDiscountFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ReturnedCheque:
					FormActivator.BringFormToFront(FormActivator.ChequeReturnFormObj);
					FormActivator.ChequeReturnFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.ReceivedChequeCancellation:
					FormActivator.BringFormToFront(FormActivator.ReceivedChequeCancellationFormObj);
					FormActivator.ReceivedChequeCancellationFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.IssuedChequeClearance:
					FormActivator.BringFormToFront(FormActivator.IssuedChequeClearanceFormObj);
					FormActivator.IssuedChequeClearanceFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.IssuedChequeReturn:
					FormActivator.BringFormToFront(FormActivator.IssuedChequeReturnFormObj);
					FormActivator.IssuedChequeReturnFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.IssuedChequeCancellation:
					FormActivator.BringFormToFront(FormActivator.IssuedChequeCancellationFormObj);
					FormActivator.IssuedChequeCancellationFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				case SysDocTypes.IssuedSecurityCheque:
					FormActivator.BringFormToFront(FormActivator.SecurityChequeFormObj);
					FormActivator.SecurityChequeFormObj.ShowForApproval(sysDocID, voucherID, approvalTaskID);
					break;
				default:
					ErrorHelper.ErrorMessage("Perform approval is not implemented for SysDocType: ", sysDocTypes.ToString(), "in Perform Approval Form.");
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
				IsSubReport = true;
				LoadReportData(reportID.ToString());
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			int cardType = int.Parse(activeRow.Cells["ObjectID"].Value.ToString());
			int.Parse(activeRow.Cells["TaskID"].Value.ToString());
			if (num == 1)
			{
				string sysDocID = "";
				if (dataGridList.DisplayLayout.Bands[0].Columns.Exists("SysDocID"))
				{
					sysDocID = activeRow.Cells["SysDocID"].Value.ToString();
				}
				string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
				FormHelper formHelper = new FormHelper();
				DocumentViewForm documentViewForm = new DocumentViewForm();
				XtraReport transactionPreviewDoc = formHelper.GetTransactionPreviewDoc(sysDocID, voucherID);
				if (transactionPreviewDoc != null)
				{
					documentViewForm.Document = transactionPreviewDoc;
					documentViewForm.ShowDialog();
				}
			}
			else
			{
				string id = activeRow.Cells["Code"].Value.ToString();
				new FormHelper().OpenCardForEdit((DataComboType)cardType, id);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.PerformApprovalForm));
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
			toolStripButtonPreviewPane = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemLocationWiseOnhand = new System.Windows.Forms.ToolStripMenuItem();
			treeViewApproval = new System.Windows.Forms.TreeView();
			imageList1 = new System.Windows.Forms.ImageList(components);
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			navBarControl = new DevExpress.XtraNavBar.NavBarControl();
			navBarGroupApproval = new DevExpress.XtraNavBar.NavBarGroup();
			navBarGroupControlContainer13 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
			navBarGroupControlContainer11 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
			treeViewVerification = new System.Windows.Forms.TreeView();
			navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
			treeViewCheckList = new System.Windows.Forms.TreeView();
			navBarGroupVerification = new DevExpress.XtraNavBar.NavBarGroup();
			navBarGroupCheckList = new DevExpress.XtraNavBar.NavBarGroup();
			navBarItemAccount = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemAccountList = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemJV = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemDebitNote = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemCreditNote = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemCashReceipt = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemChequeReceipt = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemChequeDeposit = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemCashPayment = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemChequePayment = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemTransfer = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemCustomer = new DevExpress.XtraNavBar.NavBarItem();
			navBarItemCustomerList = new DevExpress.XtraNavBar.NavBarItem();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			splitContainerMain = new System.Windows.Forms.SplitContainer();
			printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)navBarControl).BeginInit();
			navBarControl.SuspendLayout();
			navBarGroupControlContainer13.SuspendLayout();
			navBarGroupControlContainer11.SuspendLayout();
			navBarGroupControlContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
			splitContainerMain.Panel1.SuspendLayout();
			splitContainerMain.Panel2.SuspendLayout();
			splitContainerMain.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 593);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(990, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(990, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(882, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButtonFitText,
				toolStripButtonPreviewPane
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(990, 31);
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
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
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
			toolStripButtonPreviewPane.CheckOnClick = true;
			toolStripButtonPreviewPane.Image = Micromind.ClientUI.Properties.Resources.fields;
			toolStripButtonPreviewPane.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreviewPane.Name = "toolStripButtonPreviewPane";
			toolStripButtonPreviewPane.Size = new System.Drawing.Size(105, 28);
			toolStripButtonPreviewPane.Text = "Preview Pane";
			toolStripButtonPreviewPane.Click += new System.EventHandler(toolStripButtonPreviewPane_Click);
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
			dataGridList.Location = new System.Drawing.Point(0, 0);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(737, 554);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemLocationWiseOnhand.Name = "toolStripMenuItemLocationWiseOnhand";
			toolStripMenuItemLocationWiseOnhand.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemLocationWiseOnhand.Text = "Location Wise Onhand...";
			treeViewApproval.AllowDrop = true;
			treeViewApproval.Dock = System.Windows.Forms.DockStyle.Fill;
			treeViewApproval.FullRowSelect = true;
			treeViewApproval.HideSelection = false;
			treeViewApproval.ImageIndex = 0;
			treeViewApproval.ImageList = imageList1;
			treeViewApproval.Location = new System.Drawing.Point(0, 0);
			treeViewApproval.Name = "treeViewApproval";
			treeViewApproval.SelectedImageIndex = 0;
			treeViewApproval.ShowLines = false;
			treeViewApproval.Size = new System.Drawing.Size(233, 367);
			treeViewApproval.StateImageList = imageList1;
			treeViewApproval.TabIndex = 291;
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "folder_closed.png");
			imageList1.Images.SetKeyName(1, "folder_open.png");
			imageList1.Images.SetKeyName(2, "approve.png");
			imageList1.Images.SetKeyName(3, "reject.png");
			imageList1.Images.SetKeyName(4, "icon-view.png");
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(navBarControl);
			splitContainer1.Panel2.Controls.Add(dataGridList);
			splitContainer1.Size = new System.Drawing.Size(974, 554);
			splitContainer1.SplitterDistance = 233;
			splitContainer1.TabIndex = 292;
			navBarControl.ActiveGroup = navBarGroupApproval;
			navBarControl.Controls.Add(navBarGroupControlContainer11);
			navBarControl.Controls.Add(navBarGroupControlContainer13);
			navBarControl.Controls.Add(navBarGroupControlContainer1);
			navBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
			navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[3]
			{
				navBarGroupApproval,
				navBarGroupVerification,
				navBarGroupCheckList
			});
			navBarControl.HotTrackedItemCursor = System.Windows.Forms.Cursors.Arrow;
			navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[13]
			{
				navBarItemAccount,
				navBarItemAccountList,
				navBarItemJV,
				navBarItemDebitNote,
				navBarItemCreditNote,
				navBarItemCashReceipt,
				navBarItemChequeReceipt,
				navBarItemChequeDeposit,
				navBarItemCashPayment,
				navBarItemChequePayment,
				navBarItemTransfer,
				navBarItemCustomer,
				navBarItemCustomerList
			});
			navBarControl.Location = new System.Drawing.Point(0, 0);
			navBarControl.Name = "navBarControl";
			navBarControl.OptionsNavPane.ExpandedWidth = 233;
			navBarControl.OptionsNavPane.ShowExpandButton = false;
			navBarControl.OptionsNavPane.ShowOverflowPanel = false;
			navBarControl.Size = new System.Drawing.Size(233, 554);
			navBarControl.TabIndex = 9;
			navBarControl.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
			navBarGroupApproval.Caption = "Approvals";
			navBarGroupApproval.ControlContainer = navBarGroupControlContainer13;
			navBarGroupApproval.Expanded = true;
			navBarGroupApproval.GroupClientHeight = 80;
			navBarGroupApproval.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
			navBarGroupApproval.Name = "navBarGroupApproval";
			navBarGroupApproval.SmallImage = Micromind.ClientUI.Properties.Resources.approval;
			navBarGroupControlContainer13.Controls.Add(treeViewApproval);
			navBarGroupControlContainer13.Name = "navBarGroupControlContainer13";
			navBarGroupControlContainer13.Size = new System.Drawing.Size(233, 367);
			navBarGroupControlContainer13.TabIndex = 12;
			navBarGroupControlContainer11.Controls.Add(treeViewVerification);
			navBarGroupControlContainer11.Name = "navBarGroupControlContainer11";
			navBarGroupControlContainer11.Size = new System.Drawing.Size(231, 301);
			navBarGroupControlContainer11.TabIndex = 10;
			treeViewVerification.AllowDrop = true;
			treeViewVerification.Dock = System.Windows.Forms.DockStyle.Fill;
			treeViewVerification.FullRowSelect = true;
			treeViewVerification.HideSelection = false;
			treeViewVerification.ImageIndex = 0;
			treeViewVerification.ImageList = imageList1;
			treeViewVerification.Location = new System.Drawing.Point(0, 0);
			treeViewVerification.Name = "treeViewVerification";
			treeViewVerification.SelectedImageIndex = 0;
			treeViewVerification.ShowLines = false;
			treeViewVerification.Size = new System.Drawing.Size(231, 301);
			treeViewVerification.StateImageList = imageList1;
			treeViewVerification.TabIndex = 292;
			navBarGroupControlContainer1.Controls.Add(treeViewCheckList);
			navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
			navBarGroupControlContainer1.Size = new System.Drawing.Size(231, 301);
			navBarGroupControlContainer1.TabIndex = 13;
			treeViewCheckList.AllowDrop = true;
			treeViewCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
			treeViewCheckList.FullRowSelect = true;
			treeViewCheckList.HideSelection = false;
			treeViewCheckList.ImageIndex = 0;
			treeViewCheckList.ImageList = imageList1;
			treeViewCheckList.Location = new System.Drawing.Point(0, 0);
			treeViewCheckList.Name = "treeViewCheckList";
			treeViewCheckList.SelectedImageIndex = 0;
			treeViewCheckList.ShowLines = false;
			treeViewCheckList.Size = new System.Drawing.Size(231, 301);
			treeViewCheckList.StateImageList = imageList1;
			treeViewCheckList.TabIndex = 292;
			navBarGroupVerification.Caption = "Verifications";
			navBarGroupVerification.ControlContainer = navBarGroupControlContainer11;
			navBarGroupVerification.GroupClientHeight = 80;
			navBarGroupVerification.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
			navBarGroupVerification.Name = "navBarGroupVerification";
			navBarGroupVerification.SmallImage = Micromind.ClientUI.Properties.Resources.verify3;
			navBarGroupCheckList.Caption = "Checklist Tasks";
			navBarGroupCheckList.ControlContainer = navBarGroupControlContainer1;
			navBarGroupCheckList.GroupClientHeight = 80;
			navBarGroupCheckList.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
			navBarGroupCheckList.Name = "navBarGroupCheckList";
			navBarGroupCheckList.SmallImage = Micromind.ClientUI.Properties.Resources.view_calendar_tasks;
			navBarItemAccount.Caption = "New Account";
			navBarItemAccount.Name = "navBarItemAccount";
			navBarItemAccountList.Caption = "Accounts List";
			navBarItemAccountList.Name = "navBarItemAccountList";
			navBarItemJV.Caption = "Journal Entry";
			navBarItemJV.Name = "navBarItemJV";
			navBarItemDebitNote.Caption = "Debit Note";
			navBarItemDebitNote.Name = "navBarItemDebitNote";
			navBarItemCreditNote.Caption = "Credit Note";
			navBarItemCreditNote.Name = "navBarItemCreditNote";
			navBarItemCashReceipt.Caption = "Cash Receipt Entry";
			navBarItemCashReceipt.Name = "navBarItemCashReceipt";
			navBarItemChequeReceipt.Caption = "Cheque Receipt Entry";
			navBarItemChequeReceipt.Name = "navBarItemChequeReceipt";
			navBarItemChequeDeposit.Caption = "Cheque Deposit";
			navBarItemChequeDeposit.Name = "navBarItemChequeDeposit";
			navBarItemCashPayment.Caption = "Cash Payment Entry";
			navBarItemCashPayment.Name = "navBarItemCashPayment";
			navBarItemChequePayment.Caption = "Cheque Payment Entry";
			navBarItemChequePayment.Name = "navBarItemChequePayment";
			navBarItemTransfer.Caption = "Bank Transfer";
			navBarItemTransfer.Name = "navBarItemTransfer";
			navBarItemCustomer.Caption = "Customer Card";
			navBarItemCustomer.Name = "navBarItemCustomer";
			navBarItemCustomerList.Caption = "Customers List";
			navBarItemCustomerList.Name = "navBarItemCustomerList";
			splitContainerMain.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainerMain.Location = new System.Drawing.Point(7, 28);
			splitContainerMain.Name = "splitContainerMain";
			splitContainerMain.Panel1.Controls.Add(splitContainer1);
			splitContainerMain.Panel2.Controls.Add(printControl1);
			splitContainerMain.Panel2Collapsed = true;
			splitContainerMain.Size = new System.Drawing.Size(974, 554);
			splitContainerMain.SplitterDistance = 573;
			splitContainerMain.TabIndex = 293;
			printControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl1.HorizontalScrollBarVisibility = DevExpress.XtraEditors.ViewInfo.ScrollBarVisibility.Hidden;
			printControl1.IsMetric = false;
			printControl1.Location = new System.Drawing.Point(0, 0);
			printControl1.Name = "printControl1";
			printControl1.Size = new System.Drawing.Size(96, 100);
			printControl1.TabIndex = 0;
			printControl1.VerticalScrollBarVisibility = DevExpress.XtraEditors.ViewInfo.ScrollBarVisibility.Hidden;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(990, 633);
			base.Controls.Add(splitContainerMain);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "PerformApprovalForm";
			Text = "Task Center";
			base.Load += new System.EventHandler(ProductListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)navBarControl).EndInit();
			navBarControl.ResumeLayout(false);
			navBarGroupControlContainer13.ResumeLayout(false);
			navBarGroupControlContainer11.ResumeLayout(false);
			navBarGroupControlContainer1.ResumeLayout(false);
			splitContainerMain.Panel1.ResumeLayout(false);
			splitContainerMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
			splitContainerMain.ResumeLayout(false);
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
			treeViewApproval.LabelEdit = false;
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
			checked
			{
				try
				{
					if (treeViewApproval.SelectedNode != null)
					{
						_ = treeViewApproval.SelectedNode.Name;
						_ = treeViewApproval.SelectedNode.ImageIndex;
					}
					treeViewApproval.Nodes.Clear();
					DataSet userApprovalsWithPendingTasks = Factory.ApprovalSystem.GetUserApprovalsWithPendingTasks(ApprovalTypes.Approval);
					if (userApprovalsWithPendingTasks != null && userApprovalsWithPendingTasks.Tables.Count > 0)
					{
						for (int i = 0; i < userApprovalsWithPendingTasks.Tables[0].Rows.Count; i++)
						{
							DataRow dataRow = userApprovalsWithPendingTasks.Tables[0].Rows[i];
							AddTreeNode(treeViewApproval, null, dataRow["ApprovalID"].ToString(), dataRow["ApprovalName"].ToString(), userApprovalsWithPendingTasks);
						}
					}
					if (treeViewVerification.SelectedNode != null)
					{
						_ = treeViewVerification.SelectedNode.Name;
						_ = treeViewVerification.SelectedNode.ImageIndex;
					}
					treeViewVerification.Nodes.Clear();
					userApprovalsWithPendingTasks = Factory.ApprovalSystem.GetUserApprovalsWithPendingTasks(ApprovalTypes.Verification);
					if (userApprovalsWithPendingTasks != null && userApprovalsWithPendingTasks.Tables.Count > 0)
					{
						for (int j = 0; j < userApprovalsWithPendingTasks.Tables[0].Rows.Count; j++)
						{
							DataRow dataRow2 = userApprovalsWithPendingTasks.Tables[0].Rows[j];
							AddTreeNode(treeViewVerification, null, dataRow2["ApprovalID"].ToString(), dataRow2["ApprovalName"].ToString(), userApprovalsWithPendingTasks);
						}
					}
					if (treeViewCheckList.SelectedNode != null)
					{
						_ = treeViewCheckList.SelectedNode.Name;
						_ = treeViewCheckList.SelectedNode.ImageIndex;
					}
					treeViewCheckList.Nodes.Clear();
					userApprovalsWithPendingTasks = Factory.CheckListSystem.GetUserCheckListsWithPendingTasks(CheckListTypes.CheckList);
					if (userApprovalsWithPendingTasks != null && userApprovalsWithPendingTasks.Tables.Count > 0 && userApprovalsWithPendingTasks.Tables[0].Rows.Count > 0)
					{
						AddTreeNode(treeViewCheckList, null, "Checklist", "Checklist", userApprovalsWithPendingTasks);
					}
					navBarGroupApproval.Visible = (treeViewApproval.Nodes.Count > 0);
					navBarGroupVerification.Visible = (treeViewVerification.Nodes.Count > 0);
					navBarGroupCheckList.Visible = (treeViewCheckList.Nodes.Count > 0);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					NavBarGroup navBarGroup = navBarGroupApproval;
					NavBarGroup navBarGroup2 = navBarGroupVerification;
					bool flag2 = navBarGroupCheckList.Visible = false;
					bool visible = navBarGroup2.Visible = flag2;
					navBarGroup.Visible = visible;
				}
			}
		}

		private int RemoveEmptyGroups(TreeNode parentNode)
		{
			int num = 0;
			TreeNodeCollection nodes = treeViewApproval.Nodes;
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

		private void AddTreeNode(TreeView treeView, TreeNode parent, string categoryID, string categoryName, DataSet data)
		{
			if (parent == null)
			{
				parent = treeView.Nodes.Add(categoryID, categoryName, 0, 0);
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

		private void toolStripButtonPreviewPane_Click(object sender, EventArgs e)
		{
			if (toolStripButtonPreviewPane.Checked)
			{
				splitContainerMain.Panel2Collapsed = false;
				return;
			}
			splitContainerMain.Panel2Collapsed = true;
			if (printControl1.PrintingSystem != null)
			{
				printControl1.PrintingSystem.ClearContent();
			}
		}
	}
}
