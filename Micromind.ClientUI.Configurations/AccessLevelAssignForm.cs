using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class AccessLevelAssignForm : Form, IForm
	{
		private bool isGridDirty;

		private bool isLoading;

		private bool isUserLevel = true;

		private DataSet currentData;

		private const string TABLENAME_CONST = "Menu_Security";

		private const string IDFIELD_CONST = "MenuID";

		private bool isNewRecord = true;

		private string userID = "";

		private ProductSelector productObj;

		private Form productFormObj;

		private GroupBox groupBoxCC;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private MMLabel labelUserID;

		private MMTextBox textBoxUserName;

		private FormManager formManager;

		private UserComboBox comboBoxUser;

		private Label labelUserName;

		private Label label2;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private DataEntryGrid dataGridMenu;

		private AmountTextBox textBoxTotalSalary;

		private UltraTabPageControl tabPageDetails;

		private DataEntryGrid dataGridForms;

		private Label label3;

		private Label label5;

		private UserGroupComboBox comboBoxUserGroup;

		private UltraTabPageControl tabPageControlOther;

		private Label label1;

		private DataEntryGrid dataGridGeneral;

		private UltraTabPageControl tabPageDashboard;

		private Label label4;

		private DataEntryGrid dataGridGadgets;

		private UltraTabPageControl tabPageCustomReports;

		private Label label6;

		private DataEntryGrid dataGridCustomReport;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private Label label7;

		private DataEntryGrid dataGridSmartList;

		private Label label8;

		private DataEntryGrid dataGridPivotReport;

		private XPButton buttonCopy;

		private UltraTabPageControl ultraTabPageControl3;

		private Label label9;

		private DataEntryGrid dataGridExternalReport;

		private XPButton buttonReport;

		private CheckBox checkBoxReport;

		private CheckBox checkBoxFull;

		private UltraTabPageControl ultraTabPageControl4;

		private Label label10;

		private DataEntryGrid dataGridCards;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8004;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public string UserID
		{
			set
			{
				userID = value;
			}
		}

		private bool IsDirty
		{
			get
			{
				if (!formManager.GetDirtyStatus())
				{
					return isGridDirty;
				}
				return true;
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

		public bool IsUserLevel
		{
			get
			{
				return isUserLevel;
			}
			set
			{
				isUserLevel = value;
				if (!isUserLevel)
				{
					labelUserID.Text = "Group ID:";
					labelUserName.Text = "Group Name:";
					comboBoxUserGroup.Visible = true;
					comboBoxUser.Visible = false;
				}
			}
		}

		public AccessLevelAssignForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += AccessLevelAssignForm_Load;
			comboBoxUser.SelectedIndexChanged += comboBoxUser_SelectedIndexChanged;
			comboBoxUserGroup.SelectedIndexChanged += comboBoxUserGroup_SelectedIndexChanged;
			dataGridForms.CellChange += dataGridForms_CellChange;
			dataGridMenu.CellChange += dataGridMenu_CellChange;
			dataGridGeneral.CellChange += dataGridGeneral_CellChange;
			dataGridGeneral.AfterCellUpdate += dataGridGeneral_AfterCellUpdate;
			dataGridGeneral.AfterRowUpdate += dataGridGeneral_AfterRowUpdate;
			dataGridCards.ClickCellButton += dataGridCards_ClickCellButton;
			dataGridCards.InitializeRow += dataGridCards_InitializeRow;
		}

		private void comboBoxUserGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxUserName.Text = comboBoxUserGroup.SelectedName;
			LoadData(comboBoxUserGroup.SelectedID);
		}

		private void dataGridMenu_AfterCellUpdate(object sender, CellEventArgs e)
		{
			_ = isLoading;
		}

		private void dataGridGeneral_AfterRowUpdate(object sender, RowEventArgs e)
		{
		}

		private void dataGridGeneral_AfterCellUpdate(object sender, CellEventArgs e)
		{
		}

		private void dataGridGeneral_CellChange(object sender, CellEventArgs e)
		{
			if (isLoading || !(e.Cell.Column.Key == "IsAllowed") || !e.Cell.Row.HasChild())
			{
				return;
			}
			isGridDirty = true;
			checked
			{
				if (e.Cell.Band.Index == 0 || e.Cell.Band.Index == 1)
				{
					for (int i = 1; i < 2; i++)
					{
						foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
						{
							if (item.ParentRow == e.Cell.Row)
							{
								item.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
							}
							if (item.HasChild())
							{
								for (int j = 0; j < item.ChildBands.Count; j++)
								{
									bool flag = bool.Parse(e.Cell.Text);
									string a = item.Cells["ItemID"].Value.ToString();
									if (!flag && a == "701")
									{
										dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].CellActivation = Activation.AllowEdit;
									}
									else if (flag)
									{
										_ = (a == "701");
									}
								}
							}
						}
					}
				}
			}
		}

		private void dataGridMenu_CellChange(object sender, CellEventArgs e)
		{
			if (isLoading || !(e.Cell.Column.Key == "Visible") || !e.Cell.Row.HasChild())
			{
				return;
			}
			isGridDirty = true;
			checked
			{
				if ((e.Cell.Band.Index == 0 || e.Cell.Band.Index == 1) && ErrorHelper.QuestionMessageYesNo("Apply to child items?") == DialogResult.Yes)
				{
					for (int i = 0; i < e.Cell.Row.ChildBands.Count; i++)
					{
						foreach (UltraGridRow row in e.Cell.Row.ChildBands[i].Rows)
						{
							if (row.ParentRow == e.Cell.Row)
							{
								row.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
							}
							if (row.HasChild())
							{
								for (int j = 0; j < row.ChildBands.Count; j++)
								{
									foreach (UltraGridRow row2 in row.ChildBands[j].Rows)
									{
										if (row2.ParentRow == row)
										{
											row2.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void dataGridForms_AfterCellUpdate(object sender, CellEventArgs e)
		{
			_ = isLoading;
		}

		private void dataGridForms_CellChange(object sender, CellEventArgs e)
		{
			if (isLoading || (!(e.Cell.Column.Key == "View") && !(e.Cell.Column.Key == "New") && !(e.Cell.Column.Key == "Delete") && !(e.Cell.Column.Key == "Edit")) || !e.Cell.Row.HasChild())
			{
				return;
			}
			isGridDirty = true;
			checked
			{
				if ((e.Cell.Band.Index == 0 || e.Cell.Band.Index == 1) && ErrorHelper.QuestionMessageYesNo("Apply to child items?") == DialogResult.Yes)
				{
					for (int i = 0; i < e.Cell.Row.ChildBands.Count; i++)
					{
						foreach (UltraGridRow row in e.Cell.Row.ChildBands[i].Rows)
						{
							if (row.ParentRow == e.Cell.Row)
							{
								row.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
							}
							if (row.HasChild())
							{
								for (int j = 0; j < row.ChildBands.Count; j++)
								{
									foreach (UltraGridRow row2 in row.ChildBands[j].Rows)
									{
										if (row2.ParentRow == row)
										{
											row2.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
		{
			checkBoxReport.Checked = false;
			textBoxUserName.Text = comboBoxUser.SelectedName;
			LoadData(comboBoxUser.SelectedID);
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					SecurityData securityData = new SecurityData();
					DataRow dataRow = null;
					object obj = null;
					object obj2 = null;
					if (IsUserLevel)
					{
						obj = comboBoxUser.SelectedID;
					}
					else
					{
						obj2 = comboBoxUserGroup.SelectedID;
					}
					foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item.Cells["ItemID"].Value.ToString() == "101" || item.Cells["ItemID"].Value.ToString() == "206" || item.Cells["ItemID"].Value.ToString() == "200" || item.Cells["ItemID"].Value.ToString() == "212")
						{
							decimal.TryParse(item.ChildBands[0].Rows[0].Cells["Days/Percentage Allowed"].Value.ToString(), out decimal result);
							securityData.GeneralSecurityTable.Rows.Add(item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2, result);
						}
						else
						{
							securityData.GeneralSecurityTable.Rows.Add(item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2);
						}
					}
					foreach (UltraGridRow item2 in dataGridGeneral.DisplayLayout.Bands[3].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item2.Cells["ItemID"].Value.ToString() == "702" || item2.Cells["ItemID"].Value.ToString() == "703")
						{
							securityData.GeneralSecurityTable.Rows.Add(item2.Cells["ItemID"].Value.ToString(), item2.Cells["IsAllowed"].Value.ToString(), obj, obj2);
						}
					}
					for (int i = 0; i < 3; i++)
					{
						foreach (UltraGridRow item3 in dataGridMenu.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item3.Cells["Visible"].Value.ToString()))
							{
								dataRow = securityData.MenuSecurityTable.NewRow();
								dataRow["MenuID"] = item3.Cells["MenuID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["Enable"] = true;
								dataRow["Visible"] = item3.Cells["Visible"].Value.ToString();
								dataRow.EndEdit();
								securityData.MenuSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					foreach (UltraGridRow item4 in dataGridGadgets.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item4.Cells["IsAllowed"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item4.Cells["ItemID"].Value.ToString();
							dataRow["ReportType"] = 4;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item4.Cells["IsAllowed"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item5 in dataGridCustomReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item5.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item5.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 1;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item5.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item6 in dataGridSmartList.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item6.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item6.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 2;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item6.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item7 in dataGridPivotReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item7.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item7.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 3;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item7.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item8 in dataGridExternalReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item8.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item8.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 5;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item8.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					for (int j = 0; j < 4; j++)
					{
						foreach (UltraGridRow item9 in dataGridForms.DisplayLayout.Bands[j].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item9.Cells["View"].Value.ToString()) || bool.Parse(item9.Cells["New"].Value.ToString()) || bool.Parse(item9.Cells["Delete"].Value.ToString()) || bool.Parse(item9.Cells["Edit"].Value.ToString()))
							{
								dataRow = securityData.ScreenSecurityTable.NewRow();
								dataRow["ScreenID"] = item9.Cells["ScreenID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["ViewRight"] = item9.Cells["View"].Value.ToString();
								dataRow["NewRight"] = item9.Cells["New"].Value.ToString();
								dataRow["EditRight"] = item9.Cells["Edit"].Value.ToString();
								dataRow["DeleteRight"] = item9.Cells["Delete"].Value.ToString();
								dataRow.EndEdit();
								securityData.ScreenSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					foreach (UltraGridRow item10 in dataGridCards.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item10.Cells["ConditionalQuery"].Value.ToString() != "")
						{
							dataRow = securityData.CardSecurityTable.NewRow();
							dataRow["CardID"] = item10.Cells["CardID"].Value.ToString();
							dataRow["ConditionalQuery"] = item10.Cells["ConditionalQuery"].Value.ToString();
							dataRow["FilterControl"] = item10.Cells["FilterControl"].Value.ToString();
							dataRow["FilterFrom"] = item10.Cells["FilterFrom"].Value.ToString();
							dataRow["FilterTo"] = item10.Cells["FilterTo"].Value.ToString();
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow.EndEdit();
							securityData.CardSecurityTable.Rows.Add(dataRow);
						}
					}
					currentData = securityData;
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		public void LoadData(string id)
		{
			LoadData(id, isUserLevel);
		}

		public void LoadData(string id, bool isUserLevelRight)
		{
			try
			{
				if (!base.IsDisposed)
				{
					isLoading = true;
					if (!(id.Trim() == ""))
					{
						id = ((!isUserLevelRight) ? comboBoxUserGroup.SelectedID : comboBoxUser.SelectedID);
						if (!checkBoxReport.Checked)
						{
							currentData = Factory.SecuritySystem.GetSecurityDataByID(id, !isUserLevelRight);
						}
						else
						{
							currentData = Factory.SecuritySystem.GetReportSecurityDataByID(id, !isUserLevelRight, checkBoxFull.Checked);
						}
						FillData(currentData);
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isLoading = false;
			}
		}

		private void FillData(DataSet data)
		{
			FillGeneralAccess(data);
			FillMenuObjects(data);
			FillFormObjects(data);
			FillGadgetObjects(data);
			FillCustomReports(data);
			FillPivotReports(data);
			FillSmartLists(data);
			FillExternalReports(data);
			FillCardsSettings(data);
			DataEntryGrid dataEntryGrid = dataGridCustomReport;
			DataEntryGrid dataEntryGrid2 = dataGridForms;
			DataEntryGrid dataEntryGrid3 = dataGridGadgets;
			DataEntryGrid dataEntryGrid4 = dataGridGeneral;
			DataEntryGrid dataEntryGrid5 = dataGridMenu;
			DataEntryGrid dataEntryGrid6 = dataGridPivotReport;
			DataEntryGrid dataEntryGrid7 = dataGridExternalReport;
			ContextMenuStrip contextMenuStrip2 = dataGridSmartList.ContextMenuStrip = null;
			ContextMenuStrip contextMenuStrip4 = dataEntryGrid7.ContextMenuStrip = contextMenuStrip2;
			ContextMenuStrip contextMenuStrip6 = dataEntryGrid6.ContextMenuStrip = contextMenuStrip4;
			ContextMenuStrip contextMenuStrip8 = dataEntryGrid5.ContextMenuStrip = contextMenuStrip6;
			ContextMenuStrip contextMenuStrip10 = dataEntryGrid4.ContextMenuStrip = contextMenuStrip8;
			ContextMenuStrip contextMenuStrip12 = dataEntryGrid3.ContextMenuStrip = contextMenuStrip10;
			ContextMenuStrip contextMenuStrip15 = dataEntryGrid.ContextMenuStrip = (dataEntryGrid2.ContextMenuStrip = contextMenuStrip12);
		}

		private bool GetGeneralSecurityRoleValue(string roleID)
		{
			DataRow[] array = currentData.Tables["General_Security"].Select("SecurityRoleID= " + roleID.ToString());
			if (array.Length == 0)
			{
				return true;
			}
			return bool.Parse(array[0]["IsAllowed"].ToString());
		}

		private void FillGadgetObjects(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Category");
			dataTable.Columns.Add("CategoryID", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("IsAllowed", typeof(bool));
			DataTable dataTable2 = dataSet.Tables.Add("Item");
			dataTable2.Columns.Add("ItemID", typeof(string));
			dataTable2.Columns.Add("CategoryID", typeof(string));
			dataTable2.Columns.Add("Description", typeof(string));
			dataTable2.Columns.Add("IsAllowed", typeof(bool));
			dataTable.Rows.Add("1", "General", false);
			dataTable.Rows.Add("2", "Accounts", false);
			dataTable.Rows.Add("3", "Sales & Customers", false);
			dataTable.Rows.Add("4", "Purchase & Vendors", false);
			dataTable.Rows.Add("5", "Inventory", false);
			dataTable.Rows.Add("6", "HR & Admin", false);
			dataTable.Rows.Add("7", "Miscellaneous", false);
			dataTable2.Rows.Add(5011, "1", "Shortcut to card screens", false);
			dataTable2.Rows.Add(5010, "1", "Shortcut to transaction screens", false);
			dataTable2.Rows.Add(5012, "1", "Shortcut to reports", false);
			dataTable2.Rows.Add(5002, "3", "Monthly Sales Chart", false);
			dataTable2.Rows.Add(5006, "3", "Top Customers Chart", false);
			dataTable2.Rows.Add(5007, "3", "Top Invoices Chart", false);
			dataTable2.Rows.Add(5008, "3", "Top Products by Sale Chart", false);
			dataTable2.Rows.Add(5009, "3", "Top Salesperson Chart", false);
			dataTable2.Rows.Add(5013, "3", "Daily Sales Chart", false);
			dataTable2.Rows.Add(5001, "2", "Favorite Bank Accounts List", false);
			dataTable2.Rows.Add(5003, "2", "PDCs Issued Near to Maturity", false);
			dataTable2.Rows.Add(5004, "2", "PDCs Onhand Near to Maturity", false);
			dataTable2.Rows.Add(5005, "5", "Items near Reorder Level", false);
			foreach (DataRow row in Factory.CustomGadgetSystem.GetCustomGadgetComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable2.NewRow();
				dataRow2["ItemID"] = row["Code"].ToString();
				if (row["CategoryID"] == DBNull.Value || row["CategoryID"].ToString() == "")
				{
					dataRow2["CategoryID"] = "7";
				}
				else
				{
					dataRow2["CategoryID"] = row["CategoryID"].ToString();
				}
				dataRow2["Description"] = row["Name"].ToString();
				dataRow2["IsAllowed"] = false;
				dataTable2.Rows.Add(dataRow2);
			}
			dataSet.Relations.Add("Rel", dataSet.Tables["Category"].Columns["CategoryID"], dataSet.Tables["Item"].Columns["CategoryID"]);
			dataGridGadgets.SetupUI();
			dataGridGadgets.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.Caption = "C";
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].MaxWidth = 36;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].FilterOperandStyle = FilterOperandStyle.None;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].LockedWidth = true;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].CellActivation = Activation.Disabled;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["CategoryID"].Hidden = true;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["ItemID"].Hidden = true;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGadgets.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridGadgets.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridGadgets.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridGadgets.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridGadgets.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridGadgets.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			foreach (UltraGridRow item in dataGridGadgets.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 4 AND MenuID = '" + item.Cells["ItemID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["IsAllowed"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].CellDisplayStyle = CellDisplayStyle.PlainText;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].CellAppearance.ForeColorDisabled = Color.White;
		}

		private void FillGeneralAccess(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Category");
			dataTable.Columns.Add("CategoryID", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("IsAllowed", typeof(bool));
			DataTable dataTable2 = dataSet.Tables.Add("Item");
			dataTable2.Columns.Add("ItemID", typeof(string));
			dataTable2.Columns.Add("CategoryID", typeof(string));
			dataTable2.Columns.Add("Description", typeof(string));
			dataTable2.Columns.Add("IsAllowed", typeof(bool));
			dataTable.Rows.Add("1", "Transactions", false);
			dataTable.Rows.Add("2", "Sales", false);
			dataTable.Rows.Add("3", "Purchase", false);
			dataTable.Rows.Add("4", "Customers", false);
			dataTable.Rows.Add("5", "Vendors", false);
			dataTable.Rows.Add("6", "Inventory", false);
			dataTable.Rows.Add("7", "HR & Admin", false);
			dataTable.Rows.Add("8", "Reports", false);
			dataTable2.Rows.Add("100", "1", "Allow to change transaction numbers", true);
			dataTable2.Rows.Add("101", "1", "Allow to enter back-dated transactions", true);
			dataTable2.Rows.Add("102", "1", "Allow to change document ID", true);
			dataTable2.Rows.Add("103", "1", "Allow to change currency", true);
			dataTable2.Rows.Add("104", "1", "Allow to change currency rate", true);
			dataTable2.Rows.Add("105", "1", "Allow to enter future-dated transactions", true);
			dataTable2.Rows.Add("106", "1", "Allow to change transaction register", true);
			dataTable2.Rows.Add("107", "1", "Allow to change customer in delivery notes", false);
			dataTable2.Rows.Add("108", "1", "Allow to change vendor in GRN", false);
			dataTable2.Rows.Add("109", "1", "Allow to change vendor in Purchase Return", false);
			dataTable2.Rows.Add("110", "1", "Allow to view item details", false);
			dataTable2.Rows.Add("111", "1", "Allow to access supplier balance and due", false);
			dataTable2.Rows.Add("112", "1", "Allow to access customer balance", false);
			dataTable2.Rows.Add("113", "1", "Allow to access account balance", false);
			dataTable2.Rows.Add("114", "1", "Allow to access employee balance", false);
			dataTable2.Rows.Add("115", "1", "Allow to edit card created by other user", false);
			dataTable2.Rows.Add("116", "1", "Allow to edit transaction created by other user", false);
			dataTable2.Rows.Add("117", "1", "Allow to create/edit transaction made in another location", false);
			dataTable2.Rows.Add("118", "1", "Allow to change Tax settings", false);
			dataTable2.Rows.Add("119", "1", "Allow to edit Tax Total", false);
			dataTable2.Rows.Add("200", "2", "Allow to give discount", true);
			dataTable2.Rows.Add("201", "2", "Allow to change the price", true);
			dataTable2.Rows.Add("202", "2", "Allow to change salesperson", true);
			dataTable2.Rows.Add("204", "2", "Allow to change inventory location in transactions", true);
			dataTable2.Rows.Add("205", "2", "Allow to change payment term and due date in invoicing", true);
			dataTable2.Rows.Add("206", "2", "Allow to take sales return upto Days", true);
			dataTable2.Rows.Add("209", "2", "Allow to change Bill to Address on DN & Sale Invoice(Local & Export)", false);
			dataTable2.Rows.Add("210", "2", "Allow to create from Default Location Only", false);
			dataTable2.Rows.Add("211", "2", "Allow to change Description", true);
			dataTable2.Rows.Add("212", "2", "Allow to give price discount", true);
			dataTable2.Rows.Add("213", "2", "Block Copy Print", false);
			dataTable2.Rows.Add("214", "2", "Block Discount Modification", false);
			dataTable2.Rows.Add("300", "3", "Allow to create multiple POs on single BOL", false);
			dataTable2.Rows.Add("301", "3", "Allow to create from Default Location Only", false);
			dataTable2.Rows.Add("203", "6", "Allow to access item cost", false);
			dataTable2.Rows.Add("600", "6", "Allow to access item unit price 1", true);
			dataTable2.Rows.Add("601", "6", "Allow to access item unit price 2", true);
			dataTable2.Rows.Add("602", "6", "Allow to access item unit price 3", true);
			dataTable2.Rows.Add("603", "6", "Allow to access item min price", true);
			dataTable2.Rows.Add("604", "6", "Allow to flag items", false);
			dataTable2.Rows.Add("700", "7", "Allow to access salary information", false);
			dataTable2.Rows.Add("701", "7", "Allow only Self leave Request", false);
			dataTable2.Rows.Add("750", "8", "Allow to edit smart list reports", false);
			dataTable2.Rows.Add("751", "8", "Allow to edit pivot reports", false);
			dataTable2.Rows.Add("207", "8", "Allow to edit Print Template", false);
			dataTable2.Rows.Add("208", "8", "Allow to Export Print", false);
			dataTable2.Rows.Add("401", "4", "Allow flag customers", false);
			dataTable2.Rows.Add("402", "4", "Show only customers assigned to user for collection", false);
			dataTable2.Rows.Add("501", "5", "Allow flag vendors", false);
			DataTable dataTable3 = dataSet.Tables.Add("Itemnew");
			dataTable3.Columns.Add("ItemID", typeof(string));
			dataTable3.Columns.Add("Days/Percentage Allowed", typeof(decimal));
			dataTable3.Rows.Add("101", 0);
			dataTable3.Rows.Add("206", 0);
			dataTable3.Rows.Add("200", 0);
			dataTable3.Rows.Add("212", 0);
			dataSet.Relations.Add("Rel", dataSet.Tables["Category"].Columns["CategoryID"], dataSet.Tables["Item"].Columns["CategoryID"]);
			dataSet.Relations.Add("Rel_new", dataSet.Tables["Item"].Columns["ItemID"], dataSet.Tables["Itemnew"].Columns["ItemID"]);
			DataTable dataTable4 = dataSet.Tables.Add("HRnew");
			dataTable4.Columns.Add("ItemID", typeof(string));
			dataTable4.Columns.Add("ItemIDRel", typeof(string));
			dataTable4.Columns.Add("CategoryID", typeof(string));
			dataTable4.Columns.Add("IsAllowed", typeof(bool));
			dataTable4.Columns.Add("Description", typeof(string));
			dataTable4.Rows.Add("702", "701", "7", false, "Allow only Intermediate Suboridnates");
			dataTable4.Rows.Add("703", "701", "7", false, "Allow all Suboridnates");
			dataSet.Relations.Add("Rel_HR", dataSet.Tables["Item"].Columns["ItemID"], dataSet.Tables["HRnew"].Columns["ItemIDRel"]);
			dataGridGeneral.SetupUI();
			dataGridGeneral.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.Caption = "C";
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].MaxWidth = 36;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].LockedWidth = true;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].CellActivation = Activation.Disabled;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["CategoryID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["ItemID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].Header.Caption = "C";
			dataGridGeneral.DisplayLayout.Bands[3].Columns["CategoryID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["ItemID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["ItemIDRel"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].MaxWidth = 36;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].LockedWidth = true;
			dataGridGeneral.DisplayLayout.Bands[2].Columns["ItemID"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[2].Columns["ItemID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[2].Columns["Days/Percentage Allowed"].Width = 500;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridGeneral.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridGeneral.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridGeneral.DisplayLayout.Bands[0].Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridGeneral.DisplayLayout.Bands[1].Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridGeneral.DisplayLayout.Bands[2].Override.AllowColSizing = AllowColSizing.Default;
			dataGridGeneral.DisplayLayout.Bands[3].Override.AllowColSizing = AllowColSizing.Default;
			dataGridGeneral.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridGeneral.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["General_Security"].Select("SecurityRoleID='" + item.Cells["ItemID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["IsAllowed"].Value = bool.Parse(array[0]["IsAllowed"].ToString());
				}
				if (array.Length != 0 && (item.Cells["ItemID"].Value.ToString() == "101" || item.Cells["ItemID"].Value.ToString() == "206" || item.Cells["ItemID"].Value.ToString() == "200" || item.Cells["ItemID"].Value.ToString() == "212"))
				{
					item.ChildBands[0].Rows[0].Cells["Days/Percentage Allowed"].Value = decimal.Parse(array[0]["intVal"].ToString());
				}
				if (checkBoxReport.Checked && array.Length != 0)
				{
					if (array.Length > 1)
					{
						item.Cells["IsAllowed"].Value = bool.Parse(array[1]["IsAllowed"].ToString());
					}
					else
					{
						item.Cells["IsAllowed"].Value = bool.Parse(array[0]["IsAllowed"].ToString());
					}
				}
			}
			foreach (UltraGridRow item2 in dataGridGeneral.DisplayLayout.Bands[3].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array2 = gData.Tables["General_Security"].Select("SecurityRoleID='" + item2.Cells["ItemID"].Value.ToString() + "'");
				if (array2.Length != 0)
				{
					item2.Cells["IsAllowed"].Value = bool.Parse(array2[0]["IsAllowed"].ToString());
				}
				if (checkBoxReport.Checked && array2.Length != 0)
				{
					if (array2.Length > 1)
					{
						item2.Cells["IsAllowed"].Value = bool.Parse(array2[1]["IsAllowed"].ToString());
					}
					else
					{
						item2.Cells["IsAllowed"].Value = bool.Parse(array2[0]["IsAllowed"].ToString());
					}
				}
			}
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].CellDisplayStyle = CellDisplayStyle.PlainText;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].CellAppearance.ForeColorDisabled = Color.White;
		}

		private void FillMenuObjects(DataSet data)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Menu");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			DataTable dataTable2 = dataSet.Tables.Add("SubMenu");
			dataTable2.Columns.Add("MenuID", typeof(string));
			dataTable2.Columns.Add("ParentID", typeof(string));
			dataTable2.Columns.Add("MenuName", typeof(string));
			dataTable2.Columns.Add("Visible", typeof(bool));
			DataTable dataTable3 = dataSet.Tables.Add("SubSubMenu");
			dataTable3.Columns.Add("MenuID", typeof(string));
			dataTable3.Columns.Add("ParentID", typeof(string));
			dataTable3.Columns.Add("MenuName", typeof(string));
			dataTable3.Columns.Add("Visible", typeof(bool));
			foreach (ToolStripItem item in ((formMain)FormActivator.MainForm).MainMenu.Items)
			{
				if (item.Tag == null || !(item.Tag.ToString() == "1"))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["MenuID"] = item.Name;
					dataRow["MenuName"] = item.Text.Replace("&&", "and").Replace("&", "");
					dataRow["Visible"] = false;
					dataTable.Rows.Add(dataRow);
					foreach (ToolStripItem dropDownItem in (item as ToolStripDropDownItem).DropDownItems)
					{
						if (dropDownItem.Tag == null || !(dropDownItem.Tag.ToString() == "1"))
						{
							dataRow = dataTable2.NewRow();
							dataRow["MenuID"] = dropDownItem.Name;
							dataRow["ParentID"] = item.Name;
							if (dropDownItem.GetType() == typeof(ToolStripSeparator))
							{
								dataRow["MenuName"] = "-";
							}
							else
							{
								dataRow["MenuName"] = dropDownItem.Text.Replace("&&", "and").Replace("&", "");
							}
							dataRow["Visible"] = false;
							dataTable2.Rows.Add(dataRow);
							ToolStripDropDownItem toolStripDropDownItem = dropDownItem as ToolStripDropDownItem;
							if (toolStripDropDownItem != null && toolStripDropDownItem.GetType() == typeof(ToolStripMenuItem))
							{
								foreach (ToolStripItem dropDownItem2 in toolStripDropDownItem.DropDownItems)
								{
									if (dropDownItem2.Tag == null || !(dropDownItem2.Tag.ToString() == "1"))
									{
										dataRow = dataTable3.NewRow();
										dataRow["MenuID"] = dropDownItem2.Name;
										dataRow["ParentID"] = dropDownItem.Name;
										if (dropDownItem2.GetType() == typeof(ToolStripSeparator))
										{
											dataRow["MenuName"] = "-";
										}
										else
										{
											dataRow["MenuName"] = dropDownItem2.Text.Replace("&&", "and").Replace("&", "");
										}
										dataRow["Visible"] = false;
										dataTable3.Rows.Add(dataRow);
									}
								}
							}
						}
					}
				}
			}
			dataSet.Relations.Add("Rel", dataSet.Tables["Menu"].Columns["MenuID"], dataSet.Tables["SubMenu"].Columns["ParentID"]);
			dataSet.Relations.Add("Rel2", dataSet.Tables["SubMenu"].Columns["MenuID"], dataSet.Tables["SubSubMenu"].Columns["ParentID"]);
			dataGridMenu.SetupUI();
			dataGridMenu.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridMenu.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[1].Columns["MenuID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[2].Columns["MenuID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridMenu.DisplayLayout.Bands[1].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridMenu.DisplayLayout.Bands[2].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridMenu.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridMenu.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridMenu.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridMenu.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridMenu.DisplayLayout.Bands[1].Columns["ParentID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridMenu.DisplayLayout.Bands[2].Columns["ParentID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[2].ColHeadersVisible = false;
			for (int i = 0; i < 3; i = checked(i + 1))
			{
				foreach (UltraGridRow item2 in dataGridMenu.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
				{
					DataRow[] array = data.Tables["Menu_Security"].Select("MenuID='" + item2.Cells["MenuID"].Value.ToString() + "'");
					if (array.Length != 0)
					{
						item2.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
					}
					if (checkBoxReport.Checked && array.Length != 0)
					{
						if (array.Length > 1)
						{
							item2.Cells["Visible"].Value = bool.Parse(array[1]["Visible"].ToString());
						}
						else
						{
							item2.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
						}
					}
				}
			}
			dataGridMenu.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridMenu.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridMenu.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridMenu.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridMenu.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridMenu.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridMenu.DisplayLayout.Bands)
			{
				band.Columns["Visible"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Visible"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
			}
			dataGridForms.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private void FillSmartLists(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Category", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.SmartListSystem.GetSmartListComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Category"] = row["Category"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridSmartList.SetupUI();
			dataGridSmartList.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridSmartList.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridSmartList.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridSmartList.DisplayLayout.Bands[0].Columns["Category"].CellActivation = Activation.NoEdit;
			dataGridSmartList.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridSmartList.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridSmartList.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridSmartList.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridSmartList.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 2 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridSmartList.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortMulti;
			dataGridSmartList.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridSmartList.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridSmartList.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridSmartList.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridSmartList.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridSmartList.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridSmartList.DisplayLayout.Bands)
			{
				band.Columns["Visible"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Category"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Visible"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
				band.Columns["Category"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
			}
			dataGridSmartList.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private void FillExternalReports(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Category", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.ExternalReportSystem.GetExternalReportComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Category"] = row["Category"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridExternalReport.SetupUI();
			dataGridExternalReport.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridExternalReport.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridExternalReport.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridExternalReport.DisplayLayout.Bands[0].Columns["Category"].CellActivation = Activation.NoEdit;
			dataGridExternalReport.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridExternalReport.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridExternalReport.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridExternalReport.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridExternalReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 5 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridExternalReport.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortMulti;
			dataGridExternalReport.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridExternalReport.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridExternalReport.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridExternalReport.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridExternalReport.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridExternalReport.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridExternalReport.DisplayLayout.Bands)
			{
				band.Columns["Visible"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Category"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Visible"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
				band.Columns["Category"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
			}
			dataGridExternalReport.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private void FillPivotReports(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Category", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.PivotSystem.GetPivotComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Category"] = row["Category"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridPivotReport.SetupUI();
			dataGridPivotReport.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridPivotReport.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridPivotReport.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridPivotReport.DisplayLayout.Bands[0].Columns["Category"].CellActivation = Activation.NoEdit;
			dataGridPivotReport.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridPivotReport.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridPivotReport.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridPivotReport.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridPivotReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 3 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridPivotReport.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortMulti;
		}

		private void FillCustomReports(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.CustomReportSystem.GetCustomReportComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridCustomReport.SetupUI();
			dataGridCustomReport.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridCustomReport.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridCustomReport.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridCustomReport.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridCustomReport.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridCustomReport.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridCustomReport.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridCustomReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 1 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
				if (checkBoxReport.Checked && array.Length != 0)
				{
					if (array.Length > 1)
					{
						item.Cells["Visible"].Value = bool.Parse(array[1]["Visible"].ToString());
					}
					else
					{
						item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
					}
				}
			}
		}

		private void FillFormObjects(DataSet data)
		{
			DataSet securityFormList = new FormHelper().GetSecurityFormList();
			dataGridForms.DataSource = securityFormList;
			securityFormList.AcceptChanges();
			dataGridForms.SetupUI();
			dataGridForms.DisplayLayout.Bands[0].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[1].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenType"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenSubArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenType"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenSubArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[0].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[1].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridForms.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridForms.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridForms.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridForms.DisplayLayout.Bands[1].Columns["ParentID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenType"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].ColHeadersVisible = false;
			dataGridForms.DisplayLayout.Bands[3].ColHeadersVisible = false;
			dataGridForms.DisplayLayout.Bands[1].Columns["New"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[1].Columns["Edit"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[1].Columns["Delete"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[2].Columns["New"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[2].Columns["Edit"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[2].Columns["Delete"].CellActivation = Activation.Disabled;
			foreach (UltraGridRow row in dataGridForms.Rows)
			{
				if (row.Cells["ScreenName"].Value.ToString().ToLower() == "reports")
				{
					row.Cells["New"].Activation = Activation.Disabled;
					row.Cells["Edit"].Activation = Activation.Disabled;
					row.Cells["Delete"].Activation = Activation.Disabled;
				}
			}
			for (int i = 0; i < 4; i = checked(i + 1))
			{
				foreach (UltraGridRow item in dataGridForms.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
				{
					DataRow[] array = data.Tables["Screen_Security"].Select("ScreenID='" + item.Cells["ScreenID"].Value.ToString() + "'");
					if (array.Length != 0)
					{
						item.Cells["View"].Value = bool.Parse(array[0]["ViewRight"].ToString());
						item.Cells["New"].Value = bool.Parse(array[0]["NewRight"].ToString());
						item.Cells["Edit"].Value = bool.Parse(array[0]["EditRight"].ToString());
						item.Cells["Delete"].Value = bool.Parse(array[0]["DeleteRight"].ToString());
					}
					if (checkBoxReport.Checked && array.Length != 0)
					{
						if (array.Length > 1)
						{
							item.Cells["View"].Value = bool.Parse(array[1]["ViewRight"].ToString());
							item.Cells["New"].Value = bool.Parse(array[1]["NewRight"].ToString());
							item.Cells["Edit"].Value = bool.Parse(array[1]["EditRight"].ToString());
							item.Cells["Delete"].Value = bool.Parse(array[1]["DeleteRight"].ToString());
						}
						else
						{
							item.Cells["View"].Value = bool.Parse(array[0]["ViewRight"].ToString());
							item.Cells["New"].Value = bool.Parse(array[0]["NewRight"].ToString());
							item.Cells["Edit"].Value = bool.Parse(array[0]["EditRight"].ToString());
							item.Cells["Delete"].Value = bool.Parse(array[0]["DeleteRight"].ToString());
						}
					}
				}
			}
			dataGridForms.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridForms.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridForms.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridForms.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridForms.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridForms.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridForms.DisplayLayout.Bands)
			{
				band.Columns["Delete"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["View"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["New"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Edit"].FilterOperandStyle = FilterOperandStyle.None;
				AppearanceBase filterOperatorAppearance = band.Columns["Delete"].FilterOperatorAppearance;
				AppearanceBase filterOperatorAppearance2 = band.Columns["View"].FilterOperatorAppearance;
				AppearanceBase filterOperatorAppearance3 = band.Columns["New"].FilterOperatorAppearance;
				Color color = band.Columns["Edit"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = filterOperatorAppearance3.BackColor = color;
				Color color6 = filterOperatorAppearance.BackColor = (filterOperatorAppearance2.BackColor = color3);
			}
			dataGridForms.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private bool SaveData()
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
				bool flag = true;
				if (checkBoxReport.Checked)
				{
					ErrorHelper.WarningMessage("Please Uncheck Generate Report");
					return false;
				}
				flag = Factory.SecuritySystem.CreateSecurity((SecurityData)currentData, comboBoxUser.SelectedID, comboBoxUserGroup.SelectedID);
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

		private bool ValidateData()
		{
			if (isUserLevel && comboBoxUser.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a user.");
				return false;
			}
			if (!isUserLevel && comboBoxUserGroup.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a user group.");
				return false;
			}
			return true;
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

		private void ClearForm()
		{
		}

		private void UserGroupGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void UserGroupGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
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
			return true;
		}

		private void AccessLevelAssignForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					comboBoxUser.LoadData();
					IsNewRecord = true;
					ClearForm();
					if (IsUserLevel)
					{
						comboBoxUser.SelectedID = userID;
					}
					else
					{
						comboBoxUserGroup.SelectedID = userID;
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
				Close();
				Dispose();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.UserGroup);
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			CopyRightsDialog copyRightsDialog = new CopyRightsDialog();
			if (copyRightsDialog.ShowDialog(this) == DialogResult.OK)
			{
				bool isUser = copyRightsDialog.IsUser;
				if (copyRightsDialog.SelectedID == "")
				{
					return;
				}
				DataSet securityDataByID = Factory.SecuritySystem.GetSecurityDataByID(copyRightsDialog.SelectedID, !isUser);
				FillData(securityDataByID);
				formManager.IsForcedDirty = true;
			}
			base.DialogResult = DialogResult.None;
		}

		private void buttonReport_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					SecurityData securityData = new SecurityData();
					checkBoxReport.Checked = true;
					DataTable dataTable = new DataTable("UserDetail");
					dataTable.Columns.Add("UserID", typeof(string));
					dataTable.Rows.Add(comboBoxUser.SelectedID);
					for (int i = 0; i < securityData.Tables.Count; i++)
					{
						securityData.Tables[i].Columns.Add(new DataColumn("Description"));
						securityData.Tables[i].Columns.Add("SlNo.", typeof(string)).SetOrdinal(0);
					}
					securityData.Merge(dataTable);
					DataRow dataRow = null;
					object obj = null;
					object obj2 = null;
					if (IsUserLevel)
					{
						obj = comboBoxUser.SelectedID;
					}
					else
					{
						obj2 = comboBoxUserGroup.SelectedID;
					}
					string text = "";
					foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item.Cells["ItemID"].Value.ToString() == "101" || item.Cells["ItemID"].Value.ToString() == "206" || item.Cells["ItemID"].Value.ToString() == "200" || item.Cells["ItemID"].Value.ToString() == "212")
						{
							decimal.TryParse(item.ChildBands[0].Rows[0].Cells["Days/Percentage Allowed"].Value.ToString(), out decimal result);
							text = item.Cells["Description"].Text;
							if (bool.Parse(item.Cells["IsAllowed"].Value.ToString()))
							{
								securityData.GeneralSecurityTable.Rows.Add(item.Index + 1, item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2, result, text);
							}
						}
						else
						{
							text = item.Cells["Description"].Text;
							if (bool.Parse(item.Cells["IsAllowed"].Value.ToString()))
							{
								securityData.GeneralSecurityTable.Rows.Add(item.Index + 1, item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2, 0, text);
							}
						}
					}
					foreach (UltraGridRow item2 in dataGridGeneral.DisplayLayout.Bands[3].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item2.Cells["ItemID"].Value.ToString() == "702" || item2.Cells["ItemID"].Value.ToString() == "703")
						{
							text = item2.Cells["Description"].Text;
							if (bool.Parse(item2.Cells["IsAllowed"].Value.ToString()))
							{
								securityData.GeneralSecurityTable.Rows.Add(item2.Index + 1, item2.Cells["ItemID"].Value.ToString(), item2.Cells["IsAllowed"].Value.ToString(), obj, obj2, 0, text);
							}
						}
					}
					for (int j = 0; j < 3; j++)
					{
						foreach (UltraGridRow item3 in dataGridMenu.DisplayLayout.Bands[j].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item3.Cells["Visible"].Value.ToString()))
							{
								dataRow = securityData.MenuSecurityTable.NewRow();
								dataRow["SlNo."] = item3.Index + 1;
								dataRow["MenuID"] = item3.Cells["MenuID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["Enable"] = true;
								dataRow["Visible"] = item3.Cells["Visible"].Value.ToString();
								dataRow["Description"] = item3.Cells["MenuName"].Text;
								dataRow.EndEdit();
								securityData.MenuSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					foreach (UltraGridRow item4 in dataGridGadgets.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item4.Cells["IsAllowed"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item4.Index + 1;
							dataRow["MenuID"] = item4.Cells["ItemID"].Value.ToString();
							dataRow["ReportType"] = 4;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item4.Cells["IsAllowed"].Value.ToString();
							dataRow["Description"] = item4.Cells["Description"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item5 in dataGridCustomReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item5.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item5.Index + 1;
							dataRow["MenuID"] = item5.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 1;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item5.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item5.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item6 in dataGridSmartList.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item6.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item6.Index + 1;
							dataRow["MenuID"] = item6.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 2;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item6.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item6.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item7 in dataGridPivotReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item7.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item7.Index + 1;
							dataRow["MenuID"] = item7.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 3;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item7.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item7.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item8 in dataGridExternalReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item8.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item8.Index + 1;
							dataRow["MenuID"] = item8.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 5;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item8.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item8.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					for (int k = 0; k < 4; k++)
					{
						foreach (UltraGridRow item9 in dataGridForms.DisplayLayout.Bands[k].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item9.Cells["View"].Value.ToString()) || bool.Parse(item9.Cells["New"].Value.ToString()) || bool.Parse(item9.Cells["Delete"].Value.ToString()) || bool.Parse(item9.Cells["Edit"].Value.ToString()))
							{
								dataRow = securityData.ScreenSecurityTable.NewRow();
								dataRow["SlNo."] = item9.Index + 1;
								dataRow["ScreenID"] = item9.Cells["ScreenID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["ViewRight"] = item9.Cells["View"].Value.ToString();
								dataRow["NewRight"] = item9.Cells["New"].Value.ToString();
								dataRow["EditRight"] = item9.Cells["Edit"].Value.ToString();
								dataRow["DeleteRight"] = item9.Cells["Delete"].Value.ToString();
								dataRow["Description"] = item9.Cells["ScreenName"].Text;
								dataRow.EndEdit();
								securityData.ScreenSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					for (int l = 0; l < securityData.Tables.Count; l++)
					{
						if (!(securityData.Tables[l].ToString() == "UserDetail"))
						{
							securityData.Relations.Add(securityData.Tables[l].ToString() + "_Rel", securityData.Tables["UserDetail"].Columns["UserID"], securityData.Tables[l].Columns["UserID"], createConstraints: false);
						}
					}
					ReportHelper reportHelper = new ReportHelper();
					XtraReport xtraReport = null;
					xtraReport = reportHelper.GetReport("UserRightReport");
					DataSet data = securityData;
					reportHelper.AddGeneralReportData(ref data, "");
					if (xtraReport == null)
					{
						checkBoxReport.Checked = false;
						ErrorHelper.ErrorMessage("Cannot find the report file. Please make sure you have access to reports path and the files are not corrupted.", "'UserRightReport.repx'");
					}
					else
					{
						xtraReport.DataSource = data;
						reportHelper.ShowReport(xtraReport);
						checkBoxReport.Checked = false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private void LoadMenuSubstitutes(DataSet reportData)
		{
			List<string> list = (from x in reportData.Tables["MenuSecurity"].AsEnumerable()
				select x["MenuText"].ToString()).ToList();
			if (list.Count > 0)
			{
				foreach (ToolStripItem item in ((formMain)FormActivator.MainForm).MainMenu.Items)
				{
					foreach (ToolStripItem dropDownItem in (item as ToolStripDropDownItem).DropDownItems)
					{
						if (list.Contains(dropDownItem.Text) && dropDownItem.Text != "" && dropDownItem.Text != string.Empty)
						{
							string filterExpression = "MenuText ='" + dropDownItem.Text + "'";
							DataRow[] array = reportData.Tables[0].Select(filterExpression);
							dropDownItem.Text = array[0]["AliasName"].ToString();
						}
						ToolStripDropDownItem toolStripDropDownItem = dropDownItem as ToolStripDropDownItem;
						if (toolStripDropDownItem != null && toolStripDropDownItem.GetType() == typeof(ToolStripMenuItem))
						{
							foreach (ToolStripItem dropDownItem2 in toolStripDropDownItem.DropDownItems)
							{
								if (list.Contains(dropDownItem2.Text) && dropDownItem2.Text != "" && dropDownItem2.Text != string.Empty)
								{
									string filterExpression2 = "MenuText ='" + dropDownItem2.Text + "'";
									DataRow[] array2 = reportData.Tables[0].Select(filterExpression2);
									dropDownItem2.Text = array2[0]["AliasName"].ToString();
								}
							}
						}
					}
				}
			}
		}

		private void checkBoxReport_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxReport.Checked)
			{
				LoadData(comboBoxUser.SelectedID);
			}
			else
			{
				LoadData(comboBoxUser.SelectedID);
			}
		}

		private void FillCardsSettings(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CardSettings");
			dataTable.Columns.Add("CardID", typeof(int));
			dataTable.Columns.Add("CardName", typeof(string));
			dataTable.Columns.Add("ConditionalQuery", typeof(string));
			dataTable.Columns.Add("FilterControl", typeof(string));
			dataTable.Columns.Add("FilterFrom", typeof(string));
			dataTable.Columns.Add("FilterTo", typeof(string));
			dataTable.Columns.Add("Condition", typeof(string));
			dataTable.Columns.Add("Clear", typeof(string));
			foreach (object value in Enum.GetValues(typeof(EntityTypesEnum)))
			{
				if ((EntityTypesEnum)value == EntityTypesEnum.Items)
				{
					dataTable.Rows.Add((int)Enum.Parse(value.GetType(), value.ToString()), value, "", "", "", "", "", "");
				}
			}
			dataGridCards.SetupUI();
			dataGridCards.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridCards.DisplayLayout.Bands[0].Columns["CardID"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["ConditionalQuery"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["FilterControl"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["FilterFrom"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["FilterTo"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["ConditionalQuery"].CellActivation = Activation.NoEdit;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].CellButtonAppearance.TextHAlign = HAlign.Center;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].Width = 20;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].CellButtonAppearance.TextHAlign = HAlign.Center;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].Width = 20;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].Header.Caption = "";
			dataGridCards.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridCards.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridCards.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			foreach (UltraGridRow item in dataGridCards.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Card_Security"].Select("CardID = '" + item.Cells["CardID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["ConditionalQuery"].Value = array[0]["ConditionalQuery"].ToString();
					item.Cells["FilterControl"].Value = array[0]["FilterControl"].ToString();
					item.Cells["FilterFrom"].Value = array[0]["FilterFrom"].ToString();
					item.Cells["FilterTo"].Value = array[0]["FilterTo"].ToString();
				}
			}
		}

		private void dataGridCards_ClickCellButton(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Condition")
			{
				int index = e.Cell.Row.Index;
				if (int.Parse(dataGridCards.Rows[index].Cells["CardID"].Value.ToString()) == 25)
				{
					setupProductSelector();
				}
			}
			else if (e.Cell.Column.Key == "Clear" && ErrorHelper.QuestionMessageYesNo("Clear this condition?") == DialogResult.Yes)
			{
				int index2 = e.Cell.Row.Index;
				dataGridCards.Rows[index2].Cells["ConditionalQuery"].Value = "";
				dataGridCards.Rows[index2].Cells["FilterControl"].Value = "";
				dataGridCards.Rows[index2].Cells["FilterFrom"].Value = "";
				dataGridCards.Rows[index2].Cells["FilterTo"].Value = "";
			}
		}

		private void dataGridCards_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			if (!e.ReInitialize)
			{
				e.Row.Cells["Condition"].Value = "...";
				e.Row.Cells["Clear"].Value = "Clear";
			}
		}

		private void setupProductSelector()
		{
			productObj = new ProductSelector();
			productObj.BackColor = Color.Transparent;
			productObj.Location = new Point(6, 15);
			productObj.Name = "Product Selector";
			productObj.Size = new Size(430, 200);
			groupBoxCC = new GroupBox();
			groupBoxCC.Controls.Add(productObj);
			groupBoxCC.Location = new Point(2, 12);
			groupBoxCC.Name = "groupBoxCC";
			groupBoxCC.Size = new Size(453, 200);
			groupBoxCC.Text = "Product";
			Button button = new Button();
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button.Location = new Point(249, 230);
			button.Name = "buttonOK";
			button.Size = new Size(102, 24);
			button.Text = "&Ok";
			button.UseVisualStyleBackColor = true;
			button.Click += buttonOK_Click;
			Button button2 = new Button();
			button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button2.DialogResult = DialogResult.Cancel;
			button2.Location = new Point(353, 230);
			button2.Name = "buttonClose";
			button2.Size = new Size(102, 24);
			button2.TabIndex = 5;
			button2.Text = "&Close";
			button2.UseVisualStyleBackColor = true;
			productFormObj = new Form();
			productFormObj.Size = new Size(481, 300);
			productFormObj.Controls.Add(button);
			productFormObj.Controls.Add(button2);
			productFormObj.Controls.Add(groupBoxCC);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AccessLevelAssignForm));
			productFormObj.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			productFormObj.Text = "Select Product";
			productFormObj.ShowDialog();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			string text = "";
			string value = "";
			string value2 = "";
			RadioButton radioButton = productObj.Controls.OfType<RadioButton>().FirstOrDefault((RadioButton r) => r.Checked);
			switch (radioButton.Name)
			{
			case "radioButtonSingle":
			case "radioButtonRange":
				if (productObj.FromItem == "" || productObj.ToItem == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "ProductID Between '" + productObj.FromItem + "' AND '" + productObj.ToItem + "' ";
				value = productObj.FromItem;
				value2 = productObj.ToItem;
				break;
			case "radioButtonClass":
				if (productObj.FromClass == "" || productObj.ToClass == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "ClassID Between '" + productObj.FromClass + "' AND '" + productObj.ToClass + "' ";
				value = productObj.FromClass;
				value2 = productObj.ToClass;
				break;
			case "radioButtonCategory":
				if (productObj.FromCategory == "" || productObj.FromCategory == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "CategoryID Between '" + productObj.FromCategory + "' AND '" + productObj.ToCategory + "' ";
				value = productObj.FromCategory;
				value2 = productObj.ToCategory;
				break;
			case "radioButtonBrand":
				if (productObj.FromBrand == "" || productObj.FromBrand == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "BrandID Between '" + productObj.FromBrand + "' AND '" + productObj.ToBrand + "' ";
				value = productObj.FromBrand;
				value2 = productObj.ToBrand;
				break;
			case "radioButtonManufacturer":
				if (productObj.FromManufacturer == "" || productObj.FromManufacturer == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "ManufacturerID Between '" + productObj.FromManufacturer + "' AND '" + productObj.ToManufacturer + "' ";
				value = productObj.FromManufacturer;
				value2 = productObj.ToManufacturer;
				break;
			case "radioButtonOrigin":
				if (productObj.FromOrigin == "" || productObj.FromOrigin == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "Origin Between '" + productObj.FromOrigin + "' AND '" + productObj.ToOrigin + "' ";
				value = productObj.FromOrigin;
				value2 = productObj.ToOrigin;
				break;
			case "radioButtonStyle":
				if (productObj.FromStyle == "" || productObj.FromStyle == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "StyleID Between '" + productObj.FromStyle + "' AND '" + productObj.ToStyle + "' ";
				value = productObj.FromStyle;
				value2 = productObj.ToStyle;
				break;
			}
			if (dataGridCards.ActiveCell != null && text != "")
			{
				dataGridCards.ActiveRow.Cells["ConditionalQuery"].Value = text;
				dataGridCards.ActiveRow.Cells["FilterControl"].Value = radioButton.Name;
				dataGridCards.ActiveRow.Cells["FilterFrom"].Value = value;
				dataGridCards.ActiveRow.Cells["FilterTo"].Value = value2;
			}
			productFormObj.Close();
		}

		private void FillCardSecurity(string filterControl, string fromFilter, string toFilter)
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
            Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessLevelAssignForm));
            this.tabPageControlOther = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridGeneral = new Micromind.DataControls.DataEntryGrid();
            this.tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.dataGridMenu = new Micromind.DataControls.DataEntryGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTotalSalary = new Micromind.UISupport.AmountTextBox();
            this.tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridForms = new Micromind.DataControls.DataEntryGrid();
            this.tabPageDashboard = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridGadgets = new Micromind.DataControls.DataEntryGrid();
            this.tabPageCustomReports = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridCustomReport = new Micromind.DataControls.DataEntryGrid();
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridCards = new Micromind.DataControls.DataEntryGrid();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridSmartList = new Micromind.DataControls.DataEntryGrid();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridPivotReport = new Micromind.DataControls.DataEntryGrid();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridExternalReport = new Micromind.DataControls.DataEntryGrid();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonReport = new Micromind.UISupport.XPButton();
            this.buttonCopy = new Micromind.UISupport.XPButton();
            this.linePanelDown = new Micromind.UISupport.Line();
            this.xpButton1 = new Micromind.UISupport.XPButton();
            this.buttonSave = new Micromind.UISupport.XPButton();
            this.labelUserName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.checkBoxReport = new System.Windows.Forms.CheckBox();
            this.checkBoxFull = new System.Windows.Forms.CheckBox();
            this.comboBoxUserGroup = new Micromind.DataControls.UserGroupComboBox();
            this.comboBoxUser = new Micromind.DataControls.UserComboBox();
            this.formManager = new Micromind.DataControls.FormManager();
            this.textBoxUserName = new Micromind.UISupport.MMTextBox();
            this.labelUserID = new Micromind.UISupport.MMLabel();
            this.tabPageControlOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGeneral)).BeginInit();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMenu)).BeginInit();
            this.tabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridForms)).BeginInit();
            this.tabPageDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGadgets)).BeginInit();
            this.tabPageCustomReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomReport)).BeginInit();
            this.ultraTabPageControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCards)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSmartList)).BeginInit();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPivotReport)).BeginInit();
            this.ultraTabPageControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExternalReport)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxUserGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxUser)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageControlOther
            // 
            this.tabPageControlOther.Controls.Add(this.label1);
            this.tabPageControlOther.Controls.Add(this.dataGridGeneral);
            this.tabPageControlOther.Location = new System.Drawing.Point(1, 23);
            this.tabPageControlOther.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageControlOther.Name = "tabPageControlOther";
            this.tabPageControlOther.Size = new System.Drawing.Size(929, 422);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridGeneral
            // 
            this.dataGridGeneral.AllowAddNew = false;
            this.dataGridGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridGeneral.DisplayLayout.Appearance = appearance1;
            this.dataGridGeneral.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridGeneral.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridGeneral.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridGeneral.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.dataGridGeneral.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridGeneral.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.dataGridGeneral.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridGeneral.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridGeneral.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridGeneral.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.dataGridGeneral.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridGeneral.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridGeneral.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridGeneral.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridGeneral.DisplayLayout.Override.CellAppearance = appearance8;
            this.dataGridGeneral.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridGeneral.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridGeneral.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.dataGridGeneral.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.dataGridGeneral.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridGeneral.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.dataGridGeneral.DisplayLayout.Override.RowAppearance = appearance11;
            this.dataGridGeneral.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridGeneral.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.dataGridGeneral.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridGeneral.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridGeneral.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridGeneral.IncludeLotItems = false;
            this.dataGridGeneral.LoadLayoutFailed = false;
            this.dataGridGeneral.Location = new System.Drawing.Point(19, 41);
            this.dataGridGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridGeneral.Name = "dataGridGeneral";
            this.dataGridGeneral.ShowClearMenu = true;
            this.dataGridGeneral.ShowDeleteMenu = true;
            this.dataGridGeneral.ShowInsertMenu = true;
            this.dataGridGeneral.ShowMoveRowsMenu = true;
            this.dataGridGeneral.Size = new System.Drawing.Size(892, 374);
            this.dataGridGeneral.TabIndex = 23;
            this.dataGridGeneral.Text = "dataEntryGrid1";
            this.dataGridGeneral.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.dataGridGeneral_AfterCellUpdate);
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.dataGridMenu);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.textBoxTotalSalary);
            this.tabPageGeneral.Location = new System.Drawing.Point(-13333, -12308);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new System.Drawing.Size(929, 422);
            // 
            // dataGridMenu
            // 
            this.dataGridMenu.AllowAddNew = false;
            this.dataGridMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridMenu.DisplayLayout.Appearance = appearance13;
            this.dataGridMenu.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridMenu.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridMenu.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridMenu.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.dataGridMenu.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridMenu.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.dataGridMenu.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridMenu.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridMenu.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridMenu.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.dataGridMenu.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridMenu.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridMenu.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridMenu.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridMenu.DisplayLayout.Override.CellAppearance = appearance20;
            this.dataGridMenu.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridMenu.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridMenu.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.dataGridMenu.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.dataGridMenu.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridMenu.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.dataGridMenu.DisplayLayout.Override.RowAppearance = appearance23;
            this.dataGridMenu.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridMenu.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.dataGridMenu.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridMenu.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridMenu.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridMenu.IncludeLotItems = false;
            this.dataGridMenu.LoadLayoutFailed = false;
            this.dataGridMenu.Location = new System.Drawing.Point(19, 41);
            this.dataGridMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridMenu.Name = "dataGridMenu";
            this.dataGridMenu.ShowClearMenu = true;
            this.dataGridMenu.ShowDeleteMenu = true;
            this.dataGridMenu.ShowInsertMenu = true;
            this.dataGridMenu.ShowMoveRowsMenu = true;
            this.dataGridMenu.Size = new System.Drawing.Size(889, 374);
            this.dataGridMenu.TabIndex = 17;
            this.dataGridMenu.Text = "dataEntryGrid1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Select the restrictions you want to apply on menus:";
            // 
            // textBoxTotalSalary
            // 
            this.textBoxTotalSalary.AllowDecimal = true;
            this.textBoxTotalSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTotalSalary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTotalSalary.CustomReportFieldName = "";
            this.textBoxTotalSalary.CustomReportKey = "";
            this.textBoxTotalSalary.CustomReportValueType = ((byte)(1));
            this.textBoxTotalSalary.ForeColor = System.Drawing.Color.Black;
            this.textBoxTotalSalary.IsComboTextBox = false;
            this.textBoxTotalSalary.IsModified = false;
            this.textBoxTotalSalary.Location = new System.Drawing.Point(1009, 399);
            this.textBoxTotalSalary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTotalSalary.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxTotalSalary.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxTotalSalary.Name = "textBoxTotalSalary";
            this.textBoxTotalSalary.NullText = "0";
            this.textBoxTotalSalary.ReadOnly = true;
            this.textBoxTotalSalary.Size = new System.Drawing.Size(151, 24);
            this.textBoxTotalSalary.TabIndex = 9;
            this.textBoxTotalSalary.TabStop = false;
            this.textBoxTotalSalary.Text = "0.00";
            this.textBoxTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTotalSalary.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.label5);
            this.tabPageDetails.Controls.Add(this.dataGridForms);
            this.tabPageDetails.Location = new System.Drawing.Point(-13333, -12308);
            this.tabPageDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Size = new System.Drawing.Size(929, 422);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridForms
            // 
            this.dataGridForms.AllowAddNew = false;
            this.dataGridForms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridForms.DisplayLayout.Appearance = appearance25;
            this.dataGridForms.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridForms.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance26.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridForms.DisplayLayout.GroupByBox.Appearance = appearance26;
            appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridForms.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
            this.dataGridForms.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance28.BackColor2 = System.Drawing.SystemColors.Control;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridForms.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
            this.dataGridForms.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridForms.DisplayLayout.MaxRowScrollRegions = 1;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridForms.DisplayLayout.Override.ActiveCellAppearance = appearance29;
            appearance30.BackColor = System.Drawing.SystemColors.Highlight;
            appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridForms.DisplayLayout.Override.ActiveRowAppearance = appearance30;
            this.dataGridForms.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridForms.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridForms.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridForms.DisplayLayout.Override.CardAreaAppearance = appearance31;
            appearance32.BorderColor = System.Drawing.Color.Silver;
            appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridForms.DisplayLayout.Override.CellAppearance = appearance32;
            this.dataGridForms.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridForms.DisplayLayout.Override.CellPadding = 0;
            appearance33.BackColor = System.Drawing.SystemColors.Control;
            appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance33.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridForms.DisplayLayout.Override.GroupByRowAppearance = appearance33;
            appearance34.TextHAlignAsString = "Left";
            this.dataGridForms.DisplayLayout.Override.HeaderAppearance = appearance34;
            this.dataGridForms.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridForms.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            appearance35.BorderColor = System.Drawing.Color.Silver;
            this.dataGridForms.DisplayLayout.Override.RowAppearance = appearance35;
            this.dataGridForms.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridForms.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
            this.dataGridForms.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridForms.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridForms.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridForms.IncludeLotItems = false;
            this.dataGridForms.LoadLayoutFailed = false;
            this.dataGridForms.Location = new System.Drawing.Point(19, 37);
            this.dataGridForms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridForms.Name = "dataGridForms";
            this.dataGridForms.ShowClearMenu = true;
            this.dataGridForms.ShowDeleteMenu = true;
            this.dataGridForms.ShowInsertMenu = true;
            this.dataGridForms.ShowMoveRowsMenu = true;
            this.dataGridForms.Size = new System.Drawing.Size(892, 378);
            this.dataGridForms.TabIndex = 18;
            this.dataGridForms.Text = "dataEntryGrid1";
            // 
            // tabPageDashboard
            // 
            this.tabPageDashboard.Controls.Add(this.label4);
            this.tabPageDashboard.Controls.Add(this.dataGridGadgets);
            this.tabPageDashboard.Location = new System.Drawing.Point(-13333, -12308);
            this.tabPageDashboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageDashboard.Name = "tabPageDashboard";
            this.tabPageDashboard.Size = new System.Drawing.Size(929, 422);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 17);
            this.label4.TabIndex = 26;
            this.label4.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridGadgets
            // 
            this.dataGridGadgets.AllowAddNew = false;
            this.dataGridGadgets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridGadgets.DisplayLayout.Appearance = appearance37;
            this.dataGridGadgets.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridGadgets.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance38.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridGadgets.DisplayLayout.GroupByBox.Appearance = appearance38;
            appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridGadgets.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
            this.dataGridGadgets.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance40.BackColor2 = System.Drawing.SystemColors.Control;
            appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridGadgets.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
            this.dataGridGadgets.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridGadgets.DisplayLayout.MaxRowScrollRegions = 1;
            appearance41.BackColor = System.Drawing.SystemColors.Window;
            appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridGadgets.DisplayLayout.Override.ActiveCellAppearance = appearance41;
            appearance42.BackColor = System.Drawing.SystemColors.Highlight;
            appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridGadgets.DisplayLayout.Override.ActiveRowAppearance = appearance42;
            this.dataGridGadgets.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridGadgets.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridGadgets.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridGadgets.DisplayLayout.Override.CardAreaAppearance = appearance43;
            appearance44.BorderColor = System.Drawing.Color.Silver;
            appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridGadgets.DisplayLayout.Override.CellAppearance = appearance44;
            this.dataGridGadgets.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridGadgets.DisplayLayout.Override.CellPadding = 0;
            appearance45.BackColor = System.Drawing.SystemColors.Control;
            appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance45.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridGadgets.DisplayLayout.Override.GroupByRowAppearance = appearance45;
            appearance46.TextHAlignAsString = "Left";
            this.dataGridGadgets.DisplayLayout.Override.HeaderAppearance = appearance46;
            this.dataGridGadgets.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridGadgets.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance47.BackColor = System.Drawing.SystemColors.Window;
            appearance47.BorderColor = System.Drawing.Color.Silver;
            this.dataGridGadgets.DisplayLayout.Override.RowAppearance = appearance47;
            this.dataGridGadgets.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridGadgets.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
            this.dataGridGadgets.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridGadgets.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridGadgets.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridGadgets.IncludeLotItems = false;
            this.dataGridGadgets.LoadLayoutFailed = false;
            this.dataGridGadgets.Location = new System.Drawing.Point(19, 41);
            this.dataGridGadgets.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridGadgets.Name = "dataGridGadgets";
            this.dataGridGadgets.ShowClearMenu = true;
            this.dataGridGadgets.ShowDeleteMenu = true;
            this.dataGridGadgets.ShowInsertMenu = true;
            this.dataGridGadgets.ShowMoveRowsMenu = true;
            this.dataGridGadgets.Size = new System.Drawing.Size(892, 374);
            this.dataGridGadgets.TabIndex = 25;
            this.dataGridGadgets.Text = "dataEntryGrid1";
            // 
            // tabPageCustomReports
            // 
            this.tabPageCustomReports.Controls.Add(this.label6);
            this.tabPageCustomReports.Controls.Add(this.dataGridCustomReport);
            this.tabPageCustomReports.Location = new System.Drawing.Point(-13333, -12308);
            this.tabPageCustomReports.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageCustomReports.Name = "tabPageCustomReports";
            this.tabPageCustomReports.Size = new System.Drawing.Size(929, 422);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(329, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridCustomReport
            // 
            this.dataGridCustomReport.AllowAddNew = false;
            this.dataGridCustomReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance49.BackColor = System.Drawing.SystemColors.Window;
            appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridCustomReport.DisplayLayout.Appearance = appearance49;
            this.dataGridCustomReport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridCustomReport.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance50.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridCustomReport.DisplayLayout.GroupByBox.Appearance = appearance50;
            appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridCustomReport.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
            this.dataGridCustomReport.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance52.BackColor2 = System.Drawing.SystemColors.Control;
            appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridCustomReport.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
            this.dataGridCustomReport.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridCustomReport.DisplayLayout.MaxRowScrollRegions = 1;
            appearance53.BackColor = System.Drawing.SystemColors.Window;
            appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridCustomReport.DisplayLayout.Override.ActiveCellAppearance = appearance53;
            appearance54.BackColor = System.Drawing.SystemColors.Highlight;
            appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridCustomReport.DisplayLayout.Override.ActiveRowAppearance = appearance54;
            this.dataGridCustomReport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridCustomReport.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridCustomReport.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridCustomReport.DisplayLayout.Override.CardAreaAppearance = appearance55;
            appearance56.BorderColor = System.Drawing.Color.Silver;
            appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridCustomReport.DisplayLayout.Override.CellAppearance = appearance56;
            this.dataGridCustomReport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridCustomReport.DisplayLayout.Override.CellPadding = 0;
            appearance57.BackColor = System.Drawing.SystemColors.Control;
            appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance57.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridCustomReport.DisplayLayout.Override.GroupByRowAppearance = appearance57;
            appearance58.TextHAlignAsString = "Left";
            this.dataGridCustomReport.DisplayLayout.Override.HeaderAppearance = appearance58;
            this.dataGridCustomReport.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridCustomReport.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance59.BackColor = System.Drawing.SystemColors.Window;
            appearance59.BorderColor = System.Drawing.Color.Silver;
            this.dataGridCustomReport.DisplayLayout.Override.RowAppearance = appearance59;
            this.dataGridCustomReport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridCustomReport.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
            this.dataGridCustomReport.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridCustomReport.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridCustomReport.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridCustomReport.IncludeLotItems = false;
            this.dataGridCustomReport.LoadLayoutFailed = false;
            this.dataGridCustomReport.Location = new System.Drawing.Point(19, 41);
            this.dataGridCustomReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridCustomReport.Name = "dataGridCustomReport";
            this.dataGridCustomReport.ShowClearMenu = true;
            this.dataGridCustomReport.ShowDeleteMenu = true;
            this.dataGridCustomReport.ShowInsertMenu = true;
            this.dataGridCustomReport.ShowMoveRowsMenu = true;
            this.dataGridCustomReport.Size = new System.Drawing.Size(892, 374);
            this.dataGridCustomReport.TabIndex = 27;
            this.dataGridCustomReport.Text = "dataEntryGrid1";
            // 
            // ultraTabPageControl4
            // 
            this.ultraTabPageControl4.Controls.Add(this.label10);
            this.ultraTabPageControl4.Controls.Add(this.dataGridCards);
            this.ultraTabPageControl4.Location = new System.Drawing.Point(-13333, -12308);
            this.ultraTabPageControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            this.ultraTabPageControl4.Size = new System.Drawing.Size(929, 422);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 17);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(329, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridCards
            // 
            this.dataGridCards.AllowAddNew = false;
            this.dataGridCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridCards.DisplayLayout.Appearance = appearance61;
            this.dataGridCards.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridCards.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance62.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridCards.DisplayLayout.GroupByBox.Appearance = appearance62;
            appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridCards.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
            this.dataGridCards.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance64.BackColor2 = System.Drawing.SystemColors.Control;
            appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridCards.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
            this.dataGridCards.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridCards.DisplayLayout.MaxRowScrollRegions = 1;
            appearance65.BackColor = System.Drawing.SystemColors.Window;
            appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridCards.DisplayLayout.Override.ActiveCellAppearance = appearance65;
            appearance66.BackColor = System.Drawing.SystemColors.Highlight;
            appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridCards.DisplayLayout.Override.ActiveRowAppearance = appearance66;
            this.dataGridCards.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridCards.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridCards.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance67.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridCards.DisplayLayout.Override.CardAreaAppearance = appearance67;
            appearance68.BorderColor = System.Drawing.Color.Silver;
            appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridCards.DisplayLayout.Override.CellAppearance = appearance68;
            this.dataGridCards.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridCards.DisplayLayout.Override.CellPadding = 0;
            appearance69.BackColor = System.Drawing.SystemColors.Control;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridCards.DisplayLayout.Override.GroupByRowAppearance = appearance69;
            appearance70.TextHAlignAsString = "Left";
            this.dataGridCards.DisplayLayout.Override.HeaderAppearance = appearance70;
            this.dataGridCards.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridCards.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance71.BackColor = System.Drawing.SystemColors.Window;
            appearance71.BorderColor = System.Drawing.Color.Silver;
            this.dataGridCards.DisplayLayout.Override.RowAppearance = appearance71;
            this.dataGridCards.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridCards.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
            this.dataGridCards.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridCards.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridCards.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridCards.IncludeLotItems = false;
            this.dataGridCards.LoadLayoutFailed = false;
            this.dataGridCards.Location = new System.Drawing.Point(19, 41);
            this.dataGridCards.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridCards.Name = "dataGridCards";
            this.dataGridCards.ShowClearMenu = true;
            this.dataGridCards.ShowDeleteMenu = true;
            this.dataGridCards.ShowInsertMenu = true;
            this.dataGridCards.ShowMoveRowsMenu = true;
            this.dataGridCards.Size = new System.Drawing.Size(892, 374);
            this.dataGridCards.TabIndex = 29;
            this.dataGridCards.Text = "dataEntryGrid1";
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.label7);
            this.ultraTabPageControl1.Controls.Add(this.dataGridSmartList);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-13333, -12308);
            this.ultraTabPageControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(929, 422);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 17);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(329, 17);
            this.label7.TabIndex = 30;
            this.label7.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridSmartList
            // 
            this.dataGridSmartList.AllowAddNew = false;
            this.dataGridSmartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance73.BackColor = System.Drawing.SystemColors.Window;
            appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridSmartList.DisplayLayout.Appearance = appearance73;
            this.dataGridSmartList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridSmartList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance74.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridSmartList.DisplayLayout.GroupByBox.Appearance = appearance74;
            appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridSmartList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
            this.dataGridSmartList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance76.BackColor2 = System.Drawing.SystemColors.Control;
            appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridSmartList.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
            this.dataGridSmartList.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridSmartList.DisplayLayout.MaxRowScrollRegions = 1;
            appearance77.BackColor = System.Drawing.SystemColors.Window;
            appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridSmartList.DisplayLayout.Override.ActiveCellAppearance = appearance77;
            appearance78.BackColor = System.Drawing.SystemColors.Highlight;
            appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridSmartList.DisplayLayout.Override.ActiveRowAppearance = appearance78;
            this.dataGridSmartList.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridSmartList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridSmartList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance79.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridSmartList.DisplayLayout.Override.CardAreaAppearance = appearance79;
            appearance80.BorderColor = System.Drawing.Color.Silver;
            appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridSmartList.DisplayLayout.Override.CellAppearance = appearance80;
            this.dataGridSmartList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridSmartList.DisplayLayout.Override.CellPadding = 0;
            appearance81.BackColor = System.Drawing.SystemColors.Control;
            appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance81.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridSmartList.DisplayLayout.Override.GroupByRowAppearance = appearance81;
            appearance82.TextHAlignAsString = "Left";
            this.dataGridSmartList.DisplayLayout.Override.HeaderAppearance = appearance82;
            this.dataGridSmartList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridSmartList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance83.BackColor = System.Drawing.SystemColors.Window;
            appearance83.BorderColor = System.Drawing.Color.Silver;
            this.dataGridSmartList.DisplayLayout.Override.RowAppearance = appearance83;
            this.dataGridSmartList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridSmartList.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
            this.dataGridSmartList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridSmartList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridSmartList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridSmartList.IncludeLotItems = false;
            this.dataGridSmartList.LoadLayoutFailed = false;
            this.dataGridSmartList.Location = new System.Drawing.Point(19, 41);
            this.dataGridSmartList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridSmartList.Name = "dataGridSmartList";
            this.dataGridSmartList.ShowClearMenu = true;
            this.dataGridSmartList.ShowDeleteMenu = true;
            this.dataGridSmartList.ShowInsertMenu = true;
            this.dataGridSmartList.ShowMoveRowsMenu = true;
            this.dataGridSmartList.Size = new System.Drawing.Size(892, 374);
            this.dataGridSmartList.TabIndex = 29;
            this.dataGridSmartList.Text = "dataEntryGrid1";
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.label8);
            this.ultraTabPageControl2.Controls.Add(this.dataGridPivotReport);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(-13333, -12308);
            this.ultraTabPageControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(929, 422);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(329, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridPivotReport
            // 
            this.dataGridPivotReport.AllowAddNew = false;
            this.dataGridPivotReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance85.BackColor = System.Drawing.SystemColors.Window;
            appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridPivotReport.DisplayLayout.Appearance = appearance85;
            this.dataGridPivotReport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridPivotReport.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance86.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridPivotReport.DisplayLayout.GroupByBox.Appearance = appearance86;
            appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridPivotReport.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
            this.dataGridPivotReport.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance88.BackColor2 = System.Drawing.SystemColors.Control;
            appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridPivotReport.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
            this.dataGridPivotReport.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridPivotReport.DisplayLayout.MaxRowScrollRegions = 1;
            appearance89.BackColor = System.Drawing.SystemColors.Window;
            appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridPivotReport.DisplayLayout.Override.ActiveCellAppearance = appearance89;
            appearance90.BackColor = System.Drawing.SystemColors.Highlight;
            appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridPivotReport.DisplayLayout.Override.ActiveRowAppearance = appearance90;
            this.dataGridPivotReport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridPivotReport.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridPivotReport.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance91.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridPivotReport.DisplayLayout.Override.CardAreaAppearance = appearance91;
            appearance92.BorderColor = System.Drawing.Color.Silver;
            appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridPivotReport.DisplayLayout.Override.CellAppearance = appearance92;
            this.dataGridPivotReport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridPivotReport.DisplayLayout.Override.CellPadding = 0;
            appearance93.BackColor = System.Drawing.SystemColors.Control;
            appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance93.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridPivotReport.DisplayLayout.Override.GroupByRowAppearance = appearance93;
            appearance94.TextHAlignAsString = "Left";
            this.dataGridPivotReport.DisplayLayout.Override.HeaderAppearance = appearance94;
            this.dataGridPivotReport.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridPivotReport.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance95.BackColor = System.Drawing.SystemColors.Window;
            appearance95.BorderColor = System.Drawing.Color.Silver;
            this.dataGridPivotReport.DisplayLayout.Override.RowAppearance = appearance95;
            this.dataGridPivotReport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridPivotReport.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
            this.dataGridPivotReport.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridPivotReport.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridPivotReport.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridPivotReport.IncludeLotItems = false;
            this.dataGridPivotReport.LoadLayoutFailed = false;
            this.dataGridPivotReport.Location = new System.Drawing.Point(19, 41);
            this.dataGridPivotReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridPivotReport.Name = "dataGridPivotReport";
            this.dataGridPivotReport.ShowClearMenu = true;
            this.dataGridPivotReport.ShowDeleteMenu = true;
            this.dataGridPivotReport.ShowInsertMenu = true;
            this.dataGridPivotReport.ShowMoveRowsMenu = true;
            this.dataGridPivotReport.Size = new System.Drawing.Size(892, 374);
            this.dataGridPivotReport.TabIndex = 29;
            this.dataGridPivotReport.Text = "dataEntryGrid1";
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.label9);
            this.ultraTabPageControl3.Controls.Add(this.dataGridExternalReport);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(-13333, -12308);
            this.ultraTabPageControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(929, 422);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(329, 17);
            this.label9.TabIndex = 32;
            this.label9.Text = "Select the restrictions you want to apply on screens:";
            // 
            // dataGridExternalReport
            // 
            this.dataGridExternalReport.AllowAddNew = false;
            this.dataGridExternalReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance97.BackColor = System.Drawing.SystemColors.Window;
            appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridExternalReport.DisplayLayout.Appearance = appearance97;
            this.dataGridExternalReport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridExternalReport.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance98.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridExternalReport.DisplayLayout.GroupByBox.Appearance = appearance98;
            appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridExternalReport.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
            this.dataGridExternalReport.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance100.BackColor2 = System.Drawing.SystemColors.Control;
            appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridExternalReport.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
            this.dataGridExternalReport.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridExternalReport.DisplayLayout.MaxRowScrollRegions = 1;
            appearance101.BackColor = System.Drawing.SystemColors.Window;
            appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridExternalReport.DisplayLayout.Override.ActiveCellAppearance = appearance101;
            appearance102.BackColor = System.Drawing.SystemColors.Highlight;
            appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridExternalReport.DisplayLayout.Override.ActiveRowAppearance = appearance102;
            this.dataGridExternalReport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridExternalReport.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridExternalReport.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance103.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridExternalReport.DisplayLayout.Override.CardAreaAppearance = appearance103;
            appearance104.BorderColor = System.Drawing.Color.Silver;
            appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridExternalReport.DisplayLayout.Override.CellAppearance = appearance104;
            this.dataGridExternalReport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridExternalReport.DisplayLayout.Override.CellPadding = 0;
            appearance105.BackColor = System.Drawing.SystemColors.Control;
            appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance105.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridExternalReport.DisplayLayout.Override.GroupByRowAppearance = appearance105;
            appearance106.TextHAlignAsString = "Left";
            this.dataGridExternalReport.DisplayLayout.Override.HeaderAppearance = appearance106;
            this.dataGridExternalReport.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridExternalReport.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance107.BackColor = System.Drawing.SystemColors.Window;
            appearance107.BorderColor = System.Drawing.Color.Silver;
            this.dataGridExternalReport.DisplayLayout.Override.RowAppearance = appearance107;
            this.dataGridExternalReport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridExternalReport.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
            this.dataGridExternalReport.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridExternalReport.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridExternalReport.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridExternalReport.IncludeLotItems = false;
            this.dataGridExternalReport.LoadLayoutFailed = false;
            this.dataGridExternalReport.Location = new System.Drawing.Point(21, 37);
            this.dataGridExternalReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridExternalReport.Name = "dataGridExternalReport";
            this.dataGridExternalReport.ShowClearMenu = true;
            this.dataGridExternalReport.ShowDeleteMenu = true;
            this.dataGridExternalReport.ShowInsertMenu = true;
            this.dataGridExternalReport.ShowMoveRowsMenu = true;
            this.dataGridExternalReport.Size = new System.Drawing.Size(892, 374);
            this.dataGridExternalReport.TabIndex = 31;
            this.dataGridExternalReport.Text = "dataEntryGrid1";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonReport);
            this.panelButtons.Controls.Add(this.buttonCopy);
            this.panelButtons.Controls.Add(this.linePanelDown);
            this.panelButtons.Controls.Add(this.xpButton1);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 573);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(964, 49);
            this.panelButtons.TabIndex = 3;
            // 
            // buttonReport
            // 
            this.buttonReport.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReport.BackColor = System.Drawing.Color.DarkGray;
            this.buttonReport.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonReport.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonReport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonReport.Location = new System.Drawing.Point(628, 10);
            this.buttonReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(128, 30);
            this.buttonReport.TabIndex = 16;
            this.buttonReport.Text = "&Report";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonCopy.BackColor = System.Drawing.Color.Silver;
            this.buttonCopy.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonCopy.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCopy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCopy.Location = new System.Drawing.Point(156, 10);
            this.buttonCopy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(128, 30);
            this.buttonCopy.TabIndex = 15;
            this.buttonCopy.Text = "Copy From....";
            this.buttonCopy.UseVisualStyleBackColor = false;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // linePanelDown
            // 
            this.linePanelDown.BackColor = System.Drawing.Color.White;
            this.linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.linePanelDown.DrawWidth = 1;
            this.linePanelDown.IsVertical = false;
            this.linePanelDown.LineBackColor = System.Drawing.Color.Silver;
            this.linePanelDown.Location = new System.Drawing.Point(0, 0);
            this.linePanelDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.linePanelDown.Name = "linePanelDown";
            this.linePanelDown.Size = new System.Drawing.Size(964, 1);
            this.linePanelDown.TabIndex = 14;
            this.linePanelDown.TabStop = false;
            // 
            // xpButton1
            // 
            this.xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.xpButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xpButton1.BackColor = System.Drawing.Color.DarkGray;
            this.xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.xpButton1.Location = new System.Drawing.Point(817, 10);
            this.xpButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xpButton1.Name = "xpButton1";
            this.xpButton1.Size = new System.Drawing.Size(128, 30);
            this.xpButton1.TabIndex = 1;
            this.xpButton1.Text = "&Close";
            this.xpButton1.UseVisualStyleBackColor = false;
            this.xpButton1.Click += new System.EventHandler(this.xpButton1_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSave.BackColor = System.Drawing.Color.Silver;
            this.buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSave.Location = new System.Drawing.Point(16, 10);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(128, 30);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(11, 42);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(79, 16);
            this.labelUserName.TabIndex = 18;
            this.labelUserName.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Define the access rights and restrictions :";
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.tabPageGeneral);
            this.ultraTabControl1.Controls.Add(this.tabPageDetails);
            this.ultraTabControl1.Controls.Add(this.tabPageControlOther);
            this.ultraTabControl1.Controls.Add(this.tabPageDashboard);
            this.ultraTabControl1.Controls.Add(this.tabPageCustomReports);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl3);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl4);
            this.ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraTabControl1.Location = new System.Drawing.Point(15, 119);
            this.ultraTabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraTabControl1.MinTabWidth = 80;
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(931, 446);
            this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.ultraTabControl1.TabIndex = 2;
            ultraTab1.TabPage = this.tabPageControlOther;
            ultraTab1.Text = "General";
            appearance109.BackColor = System.Drawing.Color.WhiteSmoke;
            ultraTab2.Appearance = appearance109;
            ultraTab2.TabPage = this.tabPageGeneral;
            ultraTab2.Text = "Menus";
            ultraTab3.TabPage = this.tabPageDetails;
            ultraTab3.Text = "Screens";
            ultraTab4.TabPage = this.tabPageDashboard;
            ultraTab4.Text = "Dashboard";
            ultraTab5.TabPage = this.tabPageCustomReports;
            ultraTab5.Text = "Custom Reports";
            ultraTab6.TabPage = this.ultraTabPageControl4;
            ultraTab6.Text = "Cards";
            ultraTab7.TabPage = this.ultraTabPageControl1;
            ultraTab7.Text = "Smart List";
            ultraTab8.TabPage = this.ultraTabPageControl2;
            ultraTab8.Text = "Pivot Reports";
            ultraTab9.TabPage = this.ultraTabPageControl3;
            ultraTab9.Text = "External Reports";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3,
            ultraTab4,
            ultraTab5,
            ultraTab6,
            ultraTab7,
            ultraTab8,
            ultraTab9});
            this.ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(this.ultraTabControl1_SelectedTabChanged);
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(929, 422);
            // 
            // checkBoxReport
            // 
            this.checkBoxReport.AutoSize = true;
            this.checkBoxReport.Location = new System.Drawing.Point(604, 37);
            this.checkBoxReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxReport.Name = "checkBoxReport";
            this.checkBoxReport.Size = new System.Drawing.Size(129, 20);
            this.checkBoxReport.TabIndex = 21;
            this.checkBoxReport.Text = "Generate Report";
            this.checkBoxReport.UseVisualStyleBackColor = true;
            this.checkBoxReport.Visible = false;
            this.checkBoxReport.CheckedChanged += new System.EventHandler(this.checkBoxReport_CheckedChanged);
            // 
            // checkBoxFull
            // 
            this.checkBoxFull.AutoSize = true;
            this.checkBoxFull.Location = new System.Drawing.Point(753, 38);
            this.checkBoxFull.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxFull.Name = "checkBoxFull";
            this.checkBoxFull.Size = new System.Drawing.Size(50, 20);
            this.checkBoxFull.TabIndex = 22;
            this.checkBoxFull.Text = "Full";
            this.checkBoxFull.UseVisualStyleBackColor = true;
            this.checkBoxFull.Visible = false;
            // 
            // comboBoxUserGroup
            // 
            this.comboBoxUserGroup.AlwaysInEditMode = true;
            this.comboBoxUserGroup.Assigned = false;
            this.comboBoxUserGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxUserGroup.CustomReportFieldName = "";
            this.comboBoxUserGroup.CustomReportKey = "";
            this.comboBoxUserGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxUserGroup.DescriptionTextBox = null;
            appearance110.BackColor = System.Drawing.SystemColors.Window;
            appearance110.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxUserGroup.DisplayLayout.Appearance = appearance110;
            this.comboBoxUserGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxUserGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance111.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance111.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxUserGroup.DisplayLayout.GroupByBox.Appearance = appearance111;
            appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxUserGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance112;
            this.comboBoxUserGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance113.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance113.BackColor2 = System.Drawing.SystemColors.Control;
            appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxUserGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance113;
            this.comboBoxUserGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxUserGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance114.BackColor = System.Drawing.SystemColors.Window;
            appearance114.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxUserGroup.DisplayLayout.Override.ActiveCellAppearance = appearance114;
            appearance115.BackColor = System.Drawing.SystemColors.Highlight;
            appearance115.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxUserGroup.DisplayLayout.Override.ActiveRowAppearance = appearance115;
            this.comboBoxUserGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxUserGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance116.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxUserGroup.DisplayLayout.Override.CardAreaAppearance = appearance116;
            appearance117.BorderColor = System.Drawing.Color.Silver;
            appearance117.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxUserGroup.DisplayLayout.Override.CellAppearance = appearance117;
            this.comboBoxUserGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxUserGroup.DisplayLayout.Override.CellPadding = 0;
            appearance118.BackColor = System.Drawing.SystemColors.Control;
            appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance118.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance118.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxUserGroup.DisplayLayout.Override.GroupByRowAppearance = appearance118;
            appearance119.TextHAlignAsString = "Left";
            this.comboBoxUserGroup.DisplayLayout.Override.HeaderAppearance = appearance119;
            this.comboBoxUserGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxUserGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance120.BackColor = System.Drawing.SystemColors.Window;
            appearance120.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxUserGroup.DisplayLayout.Override.RowAppearance = appearance120;
            this.comboBoxUserGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance121.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxUserGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance121;
            this.comboBoxUserGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxUserGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxUserGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxUserGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxUserGroup.Editable = true;
            this.comboBoxUserGroup.FilterString = "";
            this.comboBoxUserGroup.HasAllAccount = false;
            this.comboBoxUserGroup.HasCustom = false;
            this.comboBoxUserGroup.IsDataLoaded = false;
            this.comboBoxUserGroup.Location = new System.Drawing.Point(120, 11);
            this.comboBoxUserGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxUserGroup.MaxDropDownItems = 12;
            this.comboBoxUserGroup.Name = "comboBoxUserGroup";
            this.comboBoxUserGroup.ShowInactiveItems = false;
            this.comboBoxUserGroup.ShowQuickAdd = true;
            this.comboBoxUserGroup.Size = new System.Drawing.Size(239, 23);
            this.comboBoxUserGroup.TabIndex = 0;
            this.comboBoxUserGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxUserGroup.Visible = false;
            // 
            // comboBoxUser
            // 
            this.comboBoxUser.AlwaysInEditMode = true;
            this.comboBoxUser.Assigned = false;
            this.comboBoxUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxUser.CustomReportFieldName = "";
            this.comboBoxUser.CustomReportKey = "";
            this.comboBoxUser.CustomReportValueType = ((byte)(1));
            this.comboBoxUser.DescriptionTextBox = null;
            appearance122.BackColor = System.Drawing.SystemColors.Window;
            appearance122.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxUser.DisplayLayout.Appearance = appearance122;
            this.comboBoxUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance123.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance123.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance123.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance123.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxUser.DisplayLayout.GroupByBox.Appearance = appearance123;
            appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance124;
            this.comboBoxUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance125.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance125.BackColor2 = System.Drawing.SystemColors.Control;
            appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxUser.DisplayLayout.GroupByBox.PromptAppearance = appearance125;
            this.comboBoxUser.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxUser.DisplayLayout.MaxRowScrollRegions = 1;
            appearance126.BackColor = System.Drawing.SystemColors.Window;
            appearance126.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxUser.DisplayLayout.Override.ActiveCellAppearance = appearance126;
            appearance127.BackColor = System.Drawing.SystemColors.Highlight;
            appearance127.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxUser.DisplayLayout.Override.ActiveRowAppearance = appearance127;
            this.comboBoxUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance128.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxUser.DisplayLayout.Override.CardAreaAppearance = appearance128;
            appearance129.BorderColor = System.Drawing.Color.Silver;
            appearance129.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxUser.DisplayLayout.Override.CellAppearance = appearance129;
            this.comboBoxUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxUser.DisplayLayout.Override.CellPadding = 0;
            appearance130.BackColor = System.Drawing.SystemColors.Control;
            appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance130.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance130.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxUser.DisplayLayout.Override.GroupByRowAppearance = appearance130;
            appearance131.TextHAlignAsString = "Left";
            this.comboBoxUser.DisplayLayout.Override.HeaderAppearance = appearance131;
            this.comboBoxUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance132.BackColor = System.Drawing.SystemColors.Window;
            appearance132.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxUser.DisplayLayout.Override.RowAppearance = appearance132;
            this.comboBoxUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance133.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance133;
            this.comboBoxUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxUser.Editable = true;
            this.comboBoxUser.FilterString = "";
            this.comboBoxUser.HasAllAccount = false;
            this.comboBoxUser.HasCustom = false;
            this.comboBoxUser.IsDataLoaded = false;
            this.comboBoxUser.Location = new System.Drawing.Point(120, 11);
            this.comboBoxUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxUser.MaxDropDownItems = 12;
            this.comboBoxUser.Name = "comboBoxUser";
            this.comboBoxUser.ShowInactiveItems = false;
            this.comboBoxUser.ShowQuickAdd = true;
            this.comboBoxUser.Size = new System.Drawing.Size(239, 23);
            this.comboBoxUser.TabIndex = 0;
            this.comboBoxUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // formManager
            // 
            this.formManager.BackColor = System.Drawing.Color.RosyBrown;
            this.formManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.formManager.IsForcedDirty = false;
            this.formManager.Location = new System.Drawing.Point(0, 0);
            this.formManager.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.formManager.MaximumSize = new System.Drawing.Size(27, 25);
            this.formManager.MinimumSize = new System.Drawing.Size(27, 25);
            this.formManager.Name = "formManager";
            this.formManager.Size = new System.Drawing.Size(27, 25);
            this.formManager.TabIndex = 16;
            this.formManager.Text = "formManager1";
            this.formManager.Visible = false;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxUserName.CustomReportFieldName = "";
            this.textBoxUserName.CustomReportKey = "";
            this.textBoxUserName.CustomReportValueType = ((byte)(1));
            this.textBoxUserName.ForeColor = System.Drawing.Color.Black;
            this.textBoxUserName.IsComboTextBox = false;
            this.textBoxUserName.IsModified = false;
            this.textBoxUserName.Location = new System.Drawing.Point(120, 38);
            this.textBoxUserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUserName.MaxLength = 15;
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            this.textBoxUserName.Size = new System.Drawing.Size(385, 22);
            this.textBoxUserName.TabIndex = 1;
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelUserID.IsFieldHeader = false;
            this.labelUserID.IsRequired = true;
            this.labelUserID.Location = new System.Drawing.Point(11, 11);
            this.labelUserID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.PenWidth = 1F;
            this.labelUserID.ShowBorder = false;
            this.labelUserID.Size = new System.Drawing.Size(67, 17);
            this.labelUserID.TabIndex = 0;
            this.labelUserID.Text = "User ID:";
            // 
            // AccessLevelAssignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(964, 622);
            this.Controls.Add(this.checkBoxFull);
            this.Controls.Add(this.checkBoxReport);
            this.Controls.Add(this.comboBoxUserGroup);
            this.Controls.Add(this.ultraTabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.comboBoxUser);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.panelButtons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccessLevelAssignForm";
            this.Text = "Access Right Setup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountGroupDetailsForm_FormClosing);
            this.Load += new System.EventHandler(this.AccessLevelAssignForm_Load);
            this.tabPageControlOther.ResumeLayout(false);
            this.tabPageControlOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGeneral)).EndInit();
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMenu)).EndInit();
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridForms)).EndInit();
            this.tabPageDashboard.ResumeLayout(false);
            this.tabPageDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGadgets)).EndInit();
            this.tabPageCustomReports.ResumeLayout(false);
            this.tabPageCustomReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomReport)).EndInit();
            this.ultraTabPageControl4.ResumeLayout(false);
            this.ultraTabPageControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCards)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            this.ultraTabPageControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSmartList)).EndInit();
            this.ultraTabPageControl2.ResumeLayout(false);
            this.ultraTabPageControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPivotReport)).EndInit();
            this.ultraTabPageControl3.ResumeLayout(false);
            this.ultraTabPageControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExternalReport)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxUserGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
