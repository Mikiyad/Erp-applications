using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class RentIncomePostingDetails : Form, IForm
	{
		private RentalPostingData currentData;

		private const string TABLENAME_CONST = "Rental_Posting";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool isUpdatingGrid;

		private string _agreestatus = "";

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private BOMComboBox bomComboBox2;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageExpense;

		private CurrencyComboBox comboBoxGridCurrency;

		private DataEntryGrid dataGridProperty;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonDistribution;

		private DataSet dataSet1;

		private XPButton buttonPayCash;

		private XPButton buttonPayCheque;

		private PropertyIncomeCodeComboBox comboBoxGridIncomeCode;

		private Label label1;

		private SysDocComboBox comboBoxSysDoc;

		private GroupBox groupBox1;

		private ToolStripButton toolStripButtonOpenList;

		private XPButton buttonVerify;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel ultraLabel1;

		private Label labelTotalCost;

		private ToolStripButton toolStripButtonInformation;

		private MMSDateTimePicker dateTimeTransactionDate;

		private MMSDateTimePicker dateTimeStartDate;

		public ScreenAreas ScreenArea => ScreenAreas.PropertyRental;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private string AgreementStatus
		{
			get
			{
				return _agreestatus;
			}
			set
			{
			}
		}

		private bool IsDirty
		{
			get
			{
				if (IsVoid)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
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
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
					XPButton xPButton2 = buttonPayCash;
					enabled = (buttonPayCheque.Enabled = false);
					xPButton2.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton3 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton3.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
					XPButton xPButton4 = buttonPayCash;
					enabled = (buttonPayCheque.Enabled = true);
					xPButton4.Enabled = enabled;
				}
				toolStripButtonDistribution.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
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
					panelDetails.Enabled = !value;
					buttonSave.Enabled = !value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public RentIncomePostingDetails()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += WorkOrderForm_Load;
			dataGridProperty.AfterCellUpdate += dataGridIncome_AfterCellUpdate;
			dataGridProperty.BeforeRowDeactivate += dataGridIncome_BeforeRowDeactivate;
			dataGridProperty.BeforeCellDeactivate += dataGridIncome_BeforeCellDeactivate;
			dataGridProperty.AfterRowsDeleted += dataGridIncome_AfterRowsDeleted;
			dataGridProperty.CellDataError += dataGridIncome_CellDataError;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridProperty.AfterCellUpdate += dataGridProperty_AfterCellUpdate;
			dataGridProperty.CellChange += dataGridProperty_CellChange;
		}

		private void dataGridIncome_CellDataError(object sender, CellDataErrorEventArgs e)
		{
		}

		private void textBoxQuantity_TextChanged(object sender, EventArgs e)
		{
			CalculateUnitCost();
		}

		private void dataGridProperty_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (!isUpdatingGrid)
			{
				try
				{
					isUpdatingGrid = true;
					if (e.Cell.Column.Key == "Total")
					{
						if (e.Cell.Value == null || e.Cell.Value.ToString() == "" || decimal.Parse(e.Cell.Value.ToString()) == 0m)
						{
							e.Cell.Row.Cells["C"].Value = false;
						}
						else
						{
							e.Cell.Row.Cells["C"].Value = true;
						}
						CalculateTotalCost();
					}
					else if (e.Cell.Column.Key == "C")
					{
						if (!bool.Parse(e.Cell.Text.ToString()))
						{
							e.Cell.Row.Cells["Total"].Value = 0.ToString(Format.TotalAmountFormat);
						}
						else if (e.Cell.Row.Cells["Total"].Value == null || e.Cell.Row.Cells["Total"].Value.ToString() == "" || decimal.Parse(e.Cell.Row.Cells["Total"].Value.ToString()) == 0m)
						{
							e.Cell.Row.Cells["Total"].Value = e.Cell.Row.Cells["Rent Amount"].Value;
						}
						CalculateTotalCost();
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				finally
				{
					isUpdatingGrid = false;
				}
			}
		}

		private void CalculateTotalCost()
		{
			decimal d = default(decimal);
			try
			{
				decimal d2 = default(decimal);
				decimal num = default(decimal);
				foreach (UltraGridRow row in dataGridProperty.Rows)
				{
					decimal result = default(decimal);
					if (row.Cells["Total"].Value != null && !(row.Cells["Total"].Value.ToString() == ""))
					{
						decimal.TryParse(row.Cells["Total"].Value.ToString(), out result);
						d2 += result;
					}
				}
				num = d + d2;
				labelTotalCost.Text = num.ToString(Format.TotalAmountFormat);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateUnitCost()
		{
			decimal result = default(decimal);
			try
			{
				decimal.TryParse(labelTotalCost.Text, out result);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			_ = (e.Cell.Column.Key == "Qty");
		}

		private void dataGridIncome_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridProperty.ActiveRow != null && e.Cell.Column.Key == "Amount")
				{
					CalculateTotalCost();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridIncome_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridProperty.ActiveRow;
			if (activeRow != null && activeRow.Cells["Amount"].Value.ToString() == "")
			{
				activeRow.Cells["Total"].Value = 0;
			}
		}

		private void dataGridProperty_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "C")
			{
				if (!bool.Parse(e.Cell.Text.ToString()))
				{
					e.Cell.Row.Cells["Total"].Value = 0.ToString(Format.TotalAmountFormat);
				}
				else if (e.Cell.Row.Cells["Total"].Value == null || e.Cell.Row.Cells["Total"].Value.ToString() == "" || decimal.Parse(e.Cell.Row.Cells["Total"].Value.ToString()) == 0m)
				{
					e.Cell.Row.Cells["Total"].Value = e.Cell.Row.Cells["Rent Amount"].Value;
				}
			}
		}

		private void dataGridIncome_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridProperty.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridProperty.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridProperty.ActiveCell.Value = result;
			}
		}

		private void dataGridIncome_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new RentalPostingData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.RentalPostingTable.Rows[0] : currentData.RentalPostingTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimeTransactionDate.Value;
				dataRow["AsofDate"] = dateTimeStartDate.Value;
				dataRow["Month"] = 0;
				dataRow["Year"] = 0;
				dataRow["SheetName"] = "";
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Reference"] = "";
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.RentalPostingTable.Rows.Add(dataRow);
				}
				currentData.RentalPostingDetailTable.Rows.Clear();
				currentData.RentalPostingDetailItemsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridProperty.Rows)
				{
					if (bool.Parse(row.Cells["C"].Value.ToString()))
					{
						DataRow dataRow2 = currentData.RentalPostingDetailTable.NewRow();
						dataRow2.BeginEdit();
						string value = (string)(dataRow2["TenantID"] = row.Cells["TenantID"].Value.ToString());
						dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow2["RentedDays"] = 0;
						dataRow2["RowIndex"] = row.Index;
						dataRow2["Property"] = row.Cells["Property"].Value.ToString();
						dataRow2["PropertyUnitID"] = row.Cells["UnitID"].Value.ToString();
						dataRow2["StartDate"] = row.Cells["Start Date"].Value.ToString();
						dataRow2["EndDate"] = row.Cells["End Date"].Value.ToString();
						dataRow2["TenantID"] = row.Cells["TenantID"].Value.ToString();
						dataRow2["NetAmount"] = row.Cells["Total"].Value.ToString();
						dataRow2["SourceSysDocID"] = row.Cells["DocID"].Value.ToString();
						dataRow2["SourceVoucherID"] = row.Cells["Voucher No"].Value.ToString();
						dataRow2["RentedDays"] = row.Cells["Rented Days"].Value.ToString();
						dataRow2.EndEdit();
						currentData.RentalPostingDetailTable.Rows.Add(dataRow2);
						foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
						{
							DataRow dataRow3 = currentData.RentalPostingDetailItemsTable.NewRow();
							dataRow3.BeginEdit();
							dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
							dataRow3["VoucherID"] = textBoxVoucherNumber.Text;
							dataRow3["TenantID"] = value;
							dataRow3["RowIndex"] = row2.Index;
							dataRow3["PayType"] = row2.Cells["PayCodeType"].Value.ToString();
							dataRow3["PayrollItemID"] = row2.Cells["PayrollItemID"].Value.ToString();
							dataRow3["Property"] = row2.Cells["Property"].Value.ToString();
							dataRow3["Description"] = row2.Cells["Description"].Value.ToString();
							dataRow3["Amount"] = row2.Cells["Amount"].Value.ToString();
							dataRow3["PayableAmount"] = row2.Cells["PayableAmount"].Value.ToString();
							dataRow3["SourceSysDocID"] = row2.Cells["DocID"].Value.ToString();
							dataRow3["SourceVoucherID"] = row2.Cells["Voucher No"].Value.ToString();
							dataRow3.EndEdit();
							currentData.RentalPostingDetailItemsTable.Rows.Add(dataRow3);
						}
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridProperty.Rows)
			{
				if (row.Cells["Total"].Value != null && row.Cells["Total"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Total"].Value.ToString());
				}
			}
			return result;
		}

		private void SetupPropertyGrid()
		{
			try
			{
				DataSet dataSet = new DataSet();
				dataGridProperty.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable("RentalPosting");
				dataTable.Columns.Add("C", typeof(bool));
				dataTable.Columns.Add("TenantID");
				dataTable.Columns.Add("Tenant");
				dataTable.Columns.Add("DocID");
				dataTable.Columns.Add("Voucher No");
				dataTable.Columns.Add("Property");
				dataTable.Columns.Add("Property Name");
				dataTable.Columns.Add("UnitID");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("Start Date");
				dataTable.Columns.Add("End Date");
				dataTable.Columns.Add("Total Days");
				dataTable.Columns.Add("Rented Days");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("Rent Amount", typeof(decimal));
				dataTable.Columns.Add("Total", typeof(decimal));
				dataSet.Tables.Add(dataTable);
				DataTable dataTable2 = new DataTable("RentalPostingItems");
				dataTable2.Columns.Add("TenantID");
				dataTable2.Columns.Add("DocID");
				dataTable2.Columns.Add("Voucher No");
				dataTable2.Columns.Add("PayrollItemID");
				dataTable2.Columns.Add("Description");
				dataTable2.Columns.Add("Property");
				dataTable2.Columns.Add("Property Name");
				dataTable2.Columns.Add("UnitID");
				dataTable2.Columns.Add("Unit");
				dataTable2.Columns.Add("PayCodeType", typeof(byte));
				dataTable2.Columns.Add("Amount", typeof(decimal));
				dataTable2.Columns.Add("PayableAmount", typeof(decimal));
				dataSet.Tables.Add(dataTable2);
				dataSet.Relations.Add("REL", new DataColumn[2]
				{
					dataSet.Tables["RentalPosting"].Columns["Voucher No"],
					dataSet.Tables["RentalPosting"].Columns["DocID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["RentalPostingItems"].Columns["Voucher No"],
					dataSet.Tables["RentalPostingItems"].Columns["DocID"]
				}, createConstraints: false);
				dataGridProperty.DataSource = dataSet;
				dataGridProperty.DisplayLayout.Bands[0].Columns["TenantID"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["PayCodeType"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["DocID"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Voucher No"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["TenantID"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Property"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Property Name"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["UnitID"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Unit"].Hidden = true;
				dataGridProperty.DisplayLayout.Bands[1].Columns["PayrollItemID"].Header.Caption = "Income Code";
				CellClickAction cellClickAction3 = dataGridProperty.DisplayLayout.Bands[0].Columns["C"].CellClickAction = (dataGridProperty.DisplayLayout.Bands[0].Columns["Amount"].CellClickAction = CellClickAction.EditAndSelectText);
				Activation activation3 = dataGridProperty.DisplayLayout.Bands[0].Columns["C"].CellActivation = (dataGridProperty.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit);
				dataGridProperty.DisplayLayout.Bands[0].Columns["C"].Width = 25;
				dataGridProperty.DisplayLayout.Bands[0].Columns["C"].LockedWidth = true;
				dataGridProperty.DisplayLayout.Bands[0].Columns["C"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
				dataGridProperty.DisplayLayout.Override.TemplateAddRowPrompt = "";
				dataGridProperty.AllowAddNew = false;
				dataGridProperty.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
				dataGridProperty.DisplayLayout.Bands[0].Columns["TenantID"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Tenant"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["DocID"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Voucher No"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Property"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Property Name"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["UnitID"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Start Date"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["End Date"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Total Days"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Rented Days"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Rent Amount"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["TenantID"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Voucher No"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["PayrollItemID"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Description"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Property"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Property Name"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["UnitID"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Unit"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["PayCodeType"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["Amount"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[1].Columns["PayableAmount"].CellActivation = Activation.NoEdit;
				dataGridProperty.DisplayLayout.Bands[0].Summaries.Add("Total", SummaryType.Sum, dataGridProperty.DisplayLayout.Bands[0].Columns["Total"], SummaryPosition.UseSummaryPositionColumn);
				dataGridProperty.DisplayLayout.Bands[0].Summaries.Add("RentAmount", SummaryType.Sum, dataGridProperty.DisplayLayout.Bands[0].Columns["Rent Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridProperty.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridProperty.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
			}
			catch
			{
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridProperty.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.RentalPostingSystem.GetRentalPostingByID(SystemDocID, voucherID);
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Rental_Posting"].Rows[0];
					dateTimeTransactionDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					dateTimeStartDate.Value = DateTime.Parse(dataRow["AsofDate"].ToString());
					DataSet obj = dataGridProperty.DataSource as DataSet;
					DataTable dataTable = obj.Tables["RentalPosting"];
					DataTable dataTable2 = obj.Tables["RentalPostingItems"];
					obj.Tables[1].Rows.Clear();
					obj.Tables[0].Rows.Clear();
					foreach (DataRow row in currentData.Tables["Rental_Posting_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["C"] = true;
						dataRow3["TenantID"] = row["CustomerID"];
						dataRow3["Tenant"] = row["CustomerName"];
						dataRow3["DocID"] = row["SourceSysDocID"];
						dataRow3["Voucher No"] = row["SourceVoucherID"];
						dataRow3["Property"] = row["PropertyID"];
						dataRow3["Property Name"] = row["PropertyName"];
						dataRow3["UnitID"] = row["PropertyUnitID"];
						dataRow3["Unit"] = row["PropertyUnitName"];
						dataRow3["Start Date"] = DateTime.Parse(row["ContractStartDate"].ToString()).Date.ToString("d");
						dataRow3["End Date"] = DateTime.Parse(row["ContractEndDate"].ToString()).Date.ToString("d");
						dataRow3["Total Days"] = row["TotalDays"];
						dataRow3["Rented Days"] = row["RentalDays"];
						dataRow3["Amount"] = decimal.Parse(row["Total"].ToString()).ToString(Format.TotalAmountFormat);
						double num = 0.0;
						double num2 = double.Parse(row["Total"].ToString());
						num = double.Parse(row["TotalDays"].ToString());
						double num3 = double.Parse(row["RentalDays"].ToString());
						_ = num2 / num;
						dataRow3["Rent Amount"] = decimal.Parse(row["NetAmount"].ToString()).ToString(Format.TotalAmountFormat);
						dataRow3["Total"] = decimal.Parse(row["NetAmount"].ToString()).ToString(Format.TotalAmountFormat);
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					foreach (DataRow row2 in currentData.Tables["Rental_Posting_Detail_Item"].Rows)
					{
						double num4 = 0.0;
						DataRow dataRow5 = dataTable2.NewRow();
						double num5 = double.Parse(row2["RentalDays"].ToString());
						double num6 = double.Parse(row2["TotalDays"].ToString());
						double num7 = double.Parse(row2["Amount"].ToString());
						dataRow5["DocID"] = row2["SourceSysDocID"];
						dataRow5["Voucher No"] = row2["SourceVoucherID"];
						dataRow5["TenantID"] = row2["CustomerID"];
						dataRow5["PayCodeType"] = row2["PayType"];
						dataRow5["PayrollItemID"] = row2["PayrollItemID"];
						dataRow5["Description"] = row2["IncomeName"];
						dataRow5["Amount"] = row2["Amount"];
						dataRow5["PayableAmount"] = (num7 / num6 * num5).ToString(Format.TotalAmountFormat);
						dataRow5["Property"] = row2["PropertyID"];
						dataRow5.EndEdit();
						dataTable2.Rows.Add(dataRow5);
					}
					dataTable2.AcceptChanges();
					CalculateTotalCost();
				}
			}
			catch
			{
				throw;
			}
		}

		private bool SaveData()
		{
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
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
				bool flag = (!isNewRecord) ? Factory.RentalPostingSystem.CreateRentalPosting(currentData, isUpdate: true) : Factory.RentalPostingSystem.CreateRentalPosting(currentData, isUpdate: false);
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
			catch (CompanyException ex)
			{
				if (ex.Number == 1046)
				{
					string nextVoucherNumber = GetNextVoucherNumber();
					if (nextVoucherNumber == textBoxVoucherNumber.Text)
					{
						ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
						return false;
					}
					if (nextVoucherNumber != "")
					{
						textBoxVoucherNumber.Text = nextVoucherNumber;
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Rental_Posting", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			int num = 0;
			num = Security.AllowedDays(GeneralSecurityRoles.EnterBackDatedTransaction);
			DateTime value = dateTimeTransactionDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			if (timeSpan.Days <= checked(num + 1))
			{
				flag = true;
			}
			else if (Global.isUserAdmin)
			{
				flag = true;
			}
			else if (num == 0)
			{
				flag = true;
			}
			if (isNewRecord && dateTimeTransactionDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (!flag)
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
				return false;
			}
			if (isNewRecord && dateTimeTransactionDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			new ArrayList();
			if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (dataGridProperty.Rows.Count <= 0)
			{
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Rental_Posting", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
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
			try
			{
				textBoxNote.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				labelTotalCost.Text = 0.ToString(Format.TotalAmountFormat);
				dateTimeTransactionDate.Value = DateTime.Now;
				dateTimeStartDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				DataSet obj = dataGridProperty.DataSource as DataSet;
				obj.Tables[1].Rows.Clear();
				obj.Tables[0].Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void JournalLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JournalLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
			else
			{
				ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.RentalPostingSystem.DeleteRentalPosting(SystemDocID, textBoxVoucherNumber.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Rental_Posting", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Rental_Posting", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Rental_Posting", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Rental_Posting", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Rental_Posting", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void OnActivated()
		{
		}

		private void ItemGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void WorkOrderForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridProperty.SetupUI();
				SetupPropertyGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.PropertyRentPost);
				dateTimeTransactionDate.Value = DateTime.Now;
				SetSecurity();
				if (!base.IsDisposed)
				{
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (Void(!IsVoid))
			{
				IsVoid = !IsVoid;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				return Factory.JournalSystem.VoidJournalVoucher(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PropertyRentPost);
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private bool SaveDraft()
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PropertyRentPost);
					}
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 104.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.PropertyRentPost);
					currentData = (dataSet as RentalPostingData);
					FillData();
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet propertyRentToPrint = Factory.PropertyRentSystem.GetPropertyRentToPrint(selectedID, text);
					if (propertyRentToPrint == null || propertyRentToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(propertyRentToPrint, selectedID, "Rental Regestration", SysDocTypes.PropertyRentPost, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void printListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintList();
		}

		private string GetDocumentTitle()
		{
			return "Inventory Adjustment - Products List";
		}

		private void PrintList()
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void buttonPayCash_Click(object sender, EventArgs e)
		{
			try
			{
				CashReceiptForm cashReceiptForm = new CashReceiptForm();
				cashReceiptForm.EntityType = "C";
				cashReceiptForm.SourceSysDocID = comboBoxSysDoc.SelectedID;
				cashReceiptForm.SourceVoucherID = textBoxVoucherNumber.Text;
				cashReceiptForm.Reference = textBoxVoucherNumber.Text;
				cashReceiptForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonPayCheque_Click(object sender, EventArgs e)
		{
			try
			{
				ChequeReceiptForm chequeReceiptForm = new ChequeReceiptForm();
				chequeReceiptForm.EntityType = "C";
				chequeReceiptForm.SourceSysDocID = comboBoxSysDoc.SelectedID;
				chequeReceiptForm.SourceVoucherID = textBoxVoucherNumber.Text;
				chequeReceiptForm.Reference = textBoxVoucherNumber.Text;
				chequeReceiptForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxTotalDays_TabIndexChanged(object sender, EventArgs e)
		{
		}

		private void textBoxTotalDays_Leave(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyIncomePostingListFormObj);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = SystemDocID;
			LoadData(voucherID);
		}

		private void dateTimeEndDate_TabIndexChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void textBoxVoucherNumber_TextChanged(object sender, EventArgs e)
		{
		}

		private void buttonVerify_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet dataSet = new DataSet();
				dataSet = Factory.PropertyRentSystem.GetPropertyRentActiveDetail(dateTimeStartDate.ValueTo, dateTimeStartDate.ValueTo);
				if (dataSet != null)
				{
					DataSet obj = dataGridProperty.DataSource as DataSet;
					DataTable dataTable = obj.Tables["RentalPosting"];
					DataTable dataTable2 = obj.Tables["RentalPostingItems"];
					obj.Tables[1].Rows.Clear();
					obj.Tables[0].Rows.Clear();
					dataTable?.Rows.Clear();
					foreach (DataRow row in dataSet.Tables["Property_Rent"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						if (int.Parse(row["RentalDays"].ToString()) >= 0)
						{
							dataRow2["C"] = false;
							dataRow2["TenantID"] = row["CustomerID"];
							dataRow2["Tenant"] = row["CustomerName"];
							dataRow2["DocID"] = row["SysDocID"];
							dataRow2["Voucher No"] = row["VoucherID"];
							dataRow2["Property"] = row["PropertyID"];
							dataRow2["Property Name"] = row["Property"];
							dataRow2["UnitID"] = row["UnitID"];
							dataRow2["Unit"] = row["Unit"];
							dataRow2["Start Date"] = DateTime.Parse(row["ContractStartDate"].ToString()).Date.ToString("d");
							dataRow2["End Date"] = DateTime.Parse(row["ContractEndDate"].ToString()).Date.ToString("d");
							dataRow2["Total Days"] = row["Calc_TotalDays"];
							dataRow2["Rented Days"] = row["RentalDays"];
							dataRow2["Amount"] = decimal.Parse(row["OriginalAmount"].ToString()).ToString(Format.TotalAmountFormat);
							double num = 0.0;
							double num2 = double.Parse(row["OriginalAmount"].ToString());
							num = double.Parse(row["Calc_TotalDays"].ToString());
							double num3 = double.Parse(row["RentalDays"].ToString());
							dataRow2["Rent Amount"] = (num2 / num * num3).ToString(Format.TotalAmountFormat);
							dataRow2.EndEdit();
							dataTable.Rows.Add(dataRow2);
						}
					}
					foreach (DataRow row2 in dataSet.Tables["Property_Rent_Detail"].Rows)
					{
						double num4 = 0.0;
						DataRow dataRow4 = dataTable2.NewRow();
						double num5 = double.Parse(row2["RentalDays"].ToString());
						if (!(num5 < 0.0))
						{
							double num6 = double.Parse(row2["Calc_TotalDays"].ToString());
							double num7 = double.Parse(row2["Amount"].ToString());
							dataRow4["DocID"] = row2["SysDocID"];
							dataRow4["Voucher No"] = row2["VoucherID"];
							dataRow4["TenantID"] = row2["CustomerID"];
							dataRow4["PayCodeType"] = row2["IncomeType"];
							dataRow4["PayrollItemID"] = row2["IncomeID"];
							dataRow4["Description"] = row2["IncomeName"];
							dataRow4["Amount"] = row2["Amount"];
							dataRow4["PayableAmount"] = (num7 / num6 * num5).ToString(Format.TotalAmountFormat);
							dataRow4["Property"] = row2["PropertyID"];
							dataRow4.EndEdit();
							dataTable2.Rows.Add(dataRow4);
						}
					}
					dataTable2.AcceptChanges();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.RentIncomePostingDetails));
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridProperty = new Micromind.DataControls.DataEntryGrid();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			comboBoxGridIncomeCode = new Micromind.DataControls.PropertyIncomeCodeComboBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			dateTimeTransactionDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			buttonVerify = new Micromind.UISupport.XPButton();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			label1 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			dataSet1 = new System.Data.DataSet();
			groupBox1 = new System.Windows.Forms.GroupBox();
			buttonPayCash = new Micromind.UISupport.XPButton();
			buttonPayCheque = new Micromind.UISupport.XPButton();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			labelTotalCost = new System.Windows.Forms.Label();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			bomComboBox2 = new Micromind.DataControls.BOMComboBox();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataSet1).BeginInit();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).BeginInit();
			SuspendLayout();
			tabPageExpense.Controls.Add(dataGridProperty);
			tabPageExpense.Controls.Add(comboBoxGridIncomeCode);
			tabPageExpense.Location = new System.Drawing.Point(1, 23);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(893, 281);
			dataGridProperty.AllowAddNew = true;
			dataGridProperty.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridProperty.CalcManager = ultraCalcManager1;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridProperty.DisplayLayout.Appearance = appearance;
			dataGridProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridProperty.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridProperty.DisplayLayout.MaxColScrollRegions = 1;
			dataGridProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridProperty.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridProperty.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridProperty.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridProperty.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridProperty.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridProperty.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridProperty.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridProperty.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridProperty.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridProperty.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridProperty.ExitEditModeOnLeave = false;
			dataGridProperty.IncludeLotItems = false;
			dataGridProperty.LoadLayoutFailed = false;
			dataGridProperty.Location = new System.Drawing.Point(1, 0);
			dataGridProperty.MinimumSize = new System.Drawing.Size(622, 95);
			dataGridProperty.Name = "dataGridProperty";
			dataGridProperty.ShowClearMenu = true;
			dataGridProperty.ShowDeleteMenu = true;
			dataGridProperty.ShowInsertMenu = true;
			dataGridProperty.ShowMoveRowsMenu = true;
			dataGridProperty.Size = new System.Drawing.Size(891, 278);
			dataGridProperty.TabIndex = 0;
			dataGridProperty.Text = "dataEntryGrid1";
			ultraCalcManager1.ContainingControl = this;
			comboBoxGridIncomeCode.AlwaysInEditMode = true;
			comboBoxGridIncomeCode.Assigned = false;
			comboBoxGridIncomeCode.CalcManager = ultraCalcManager1;
			comboBoxGridIncomeCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridIncomeCode.CustomReportFieldName = "";
			comboBoxGridIncomeCode.CustomReportKey = "";
			comboBoxGridIncomeCode.CustomReportValueType = 1;
			comboBoxGridIncomeCode.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridIncomeCode.DisplayLayout.Appearance = appearance13;
			comboBoxGridIncomeCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridIncomeCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridIncomeCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridIncomeCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridIncomeCode.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridIncomeCode.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridIncomeCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridIncomeCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridIncomeCode.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridIncomeCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridIncomeCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridIncomeCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridIncomeCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridIncomeCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridIncomeCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridIncomeCode.Editable = true;
			comboBoxGridIncomeCode.FilterString = "";
			comboBoxGridIncomeCode.HasAllAccount = false;
			comboBoxGridIncomeCode.HasCustom = false;
			comboBoxGridIncomeCode.IsDataLoaded = false;
			comboBoxGridIncomeCode.Location = new System.Drawing.Point(539, 66);
			comboBoxGridIncomeCode.MaxDropDownItems = 12;
			comboBoxGridIncomeCode.Name = "comboBoxGridIncomeCode";
			comboBoxGridIncomeCode.ShowInactiveItems = false;
			comboBoxGridIncomeCode.ShowQuickAdd = true;
			comboBoxGridIncomeCode.Size = new System.Drawing.Size(100, 20);
			comboBoxGridIncomeCode.TabIndex = 136;
			comboBoxGridIncomeCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridIncomeCode.Visible = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(922, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 521);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(922, 40);
			panelButtons.TabIndex = 0;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 19;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 18;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 17;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(922, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(811, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 20;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			buttonSave.TabIndex = 16;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			textBoxVoucherNumber.Location = new System.Drawing.Point(308, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(113, 20);
			textBoxVoucherNumber.TabIndex = 3;
			textBoxVoucherNumber.TextChanged += new System.EventHandler(textBoxVoucherNumber_TextChanged);
			textBoxNote.Location = new System.Drawing.Point(69, 446);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(447, 69);
			textBoxNote.TabIndex = 0;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 451);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance25;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(201, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(dateTimeTransactionDate);
			panelDetails.Controls.Add(dateTimeStartDate);
			panelDetails.Controls.Add(buttonVerify);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Location = new System.Drawing.Point(10, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(897, 78);
			panelDetails.TabIndex = 0;
			dateTimeTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeTransactionDate.Location = new System.Drawing.Point(745, 5);
			dateTimeTransactionDate.Name = "dateTimeTransactionDate";
			dateTimeTransactionDate.Size = new System.Drawing.Size(104, 20);
			dateTimeTransactionDate.TabIndex = 174;
			dateTimeTransactionDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 341);
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeStartDate.Location = new System.Drawing.Point(68, 35);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.Size = new System.Drawing.Size(104, 20);
			dateTimeStartDate.TabIndex = 173;
			dateTimeStartDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 341);
			buttonVerify.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVerify.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonVerify.BackColor = System.Drawing.Color.DarkGray;
			buttonVerify.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVerify.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVerify.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVerify.Location = new System.Drawing.Point(215, 32);
			buttonVerify.Name = "buttonVerify";
			buttonVerify.Size = new System.Drawing.Size(122, 24);
			buttonVerify.TabIndex = 172;
			buttonVerify.Text = "Load..";
			buttonVerify.UseVisualStyleBackColor = false;
			buttonVerify.Click += new System.EventHandler(buttonVerify_Click);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(68, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 2;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 38);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 26);
			label1.TabIndex = 150;
			label1.Text = "Date:\r\n\r\n";
			appearance27.FontData.BoldAsString = "True";
			appearance27.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance27;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(5, 5);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(701, 6);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				printListToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(130, 26);
			printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			printListToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			printListToolStripMenuItem.Text = "Print List...";
			printListToolStripMenuItem.Click += new System.EventHandler(printListToolStripMenuItem_Click);
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.SettingsKey = "BuildAssemblyForm.ultraGridPrintDocument1";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.CausesValidation = false;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Location = new System.Drawing.Point(10, 112);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(897, 307);
			ultraTabControl1.TabIndex = 1;
			ultraTab.TabPage = tabPageExpense;
			ultraTab.Text = "Agreement Details";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[1]
			{
				ultraTab
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(893, 281);
			dataSet1.DataSetName = "NewDataSet";
			groupBox1.Controls.Add(buttonPayCash);
			groupBox1.Controls.Add(buttonPayCheque);
			groupBox1.Location = new System.Drawing.Point(550, 464);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(256, 51);
			groupBox1.TabIndex = 23;
			groupBox1.TabStop = false;
			groupBox1.Text = "Receipt";
			groupBox1.Visible = false;
			buttonPayCash.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCash.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCash.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCash.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCash.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCash.Location = new System.Drawing.Point(20, 21);
			buttonPayCash.Name = "buttonPayCash";
			buttonPayCash.Size = new System.Drawing.Size(96, 24);
			buttonPayCash.TabIndex = 21;
			buttonPayCash.Text = "Cash";
			buttonPayCash.UseVisualStyleBackColor = false;
			buttonPayCash.Visible = false;
			buttonPayCash.Click += new System.EventHandler(buttonPayCash_Click);
			buttonPayCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCheque.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCheque.Location = new System.Drawing.Point(138, 21);
			buttonPayCheque.Name = "buttonPayCheque";
			buttonPayCheque.Size = new System.Drawing.Size(96, 24);
			buttonPayCheque.TabIndex = 22;
			buttonPayCheque.Text = "Cheque";
			buttonPayCheque.UseVisualStyleBackColor = false;
			buttonPayCheque.Visible = false;
			buttonPayCheque.Click += new System.EventHandler(buttonPayCheque_Click);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance29.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance29.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance29;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.Controls.Add(labelTotalCost);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(10, 420);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(896, 21);
			ultraGroupBox1.TabIndex = 24;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance30.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance30.FontData.BoldAsString = "True";
			appearance30.FontData.Name = "Tahoma";
			appearance30.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance30.TextHAlignAsString = "Right";
			appearance30.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance30;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(-10, 2);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(759, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			labelTotalCost.Dock = System.Windows.Forms.DockStyle.Right;
			labelTotalCost.Location = new System.Drawing.Point(750, 2);
			labelTotalCost.Name = "labelTotalCost";
			labelTotalCost.Size = new System.Drawing.Size(144, 17);
			labelTotalCost.TabIndex = 139;
			labelTotalCost.Text = "0.00";
			labelTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			comboBoxGridCurrency.AlwaysInEditMode = true;
			comboBoxGridCurrency.Assigned = false;
			comboBoxGridCurrency.CalcManager = ultraCalcManager1;
			comboBoxGridCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCurrency.CustomReportFieldName = "";
			comboBoxGridCurrency.CustomReportKey = "";
			comboBoxGridCurrency.CustomReportValueType = 1;
			comboBoxGridCurrency.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance31;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxGridCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCurrency.Editable = true;
			comboBoxGridCurrency.FilterString = "";
			comboBoxGridCurrency.HasAllAccount = false;
			comboBoxGridCurrency.HasCustom = false;
			comboBoxGridCurrency.IsDataLoaded = false;
			comboBoxGridCurrency.Location = new System.Drawing.Point(350, 104);
			comboBoxGridCurrency.MaxDropDownItems = 12;
			comboBoxGridCurrency.Name = "comboBoxGridCurrency";
			comboBoxGridCurrency.ShowInactiveItems = false;
			comboBoxGridCurrency.ShowQuickAdd = true;
			comboBoxGridCurrency.Size = new System.Drawing.Size(95, 20);
			comboBoxGridCurrency.TabIndex = 122;
			comboBoxGridCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCurrency.Visible = false;
			bomComboBox2.Assigned = false;
			bomComboBox2.CalcManager = ultraCalcManager1;
			bomComboBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			bomComboBox2.CustomReportFieldName = "";
			bomComboBox2.CustomReportKey = "";
			bomComboBox2.CustomReportValueType = 1;
			bomComboBox2.DescriptionTextBox = null;
			bomComboBox2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			bomComboBox2.Editable = true;
			bomComboBox2.FilterString = "";
			bomComboBox2.HasAllAccount = false;
			bomComboBox2.HasCustom = false;
			bomComboBox2.IsDataLoaded = false;
			bomComboBox2.Location = new System.Drawing.Point(0, 0);
			bomComboBox2.MaxDropDownItems = 12;
			bomComboBox2.Name = "bomComboBox2";
			bomComboBox2.ShowInactiveItems = false;
			bomComboBox2.ShowQuickAdd = true;
			bomComboBox2.Size = new System.Drawing.Size(100, 20);
			bomComboBox2.TabIndex = 0;
			bomComboBox2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(922, 561);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(groupBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "RentIncomePostingDetails";
			Text = "Rent Income Posting";
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataSet1).EndInit();
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
