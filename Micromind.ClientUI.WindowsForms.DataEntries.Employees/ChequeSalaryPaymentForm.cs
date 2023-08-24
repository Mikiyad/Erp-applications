using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
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
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class ChequeSalaryPaymentForm : Form, IForm
	{
		private PayrollTransactionData currentData;

		private const string TABLENAME_CONST = "Payroll_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool isUpdatingGrid;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraLabel ultraLabel1;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelBalance;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private RegisterComboBox comboBoxRegister;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxAmount;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStrip toolStrip1;

		private MMLabel mmLabel4;

		private XPButton buttonSelectSalarySheet;

		private TextBox textBoxYear;

		private TextBox textBoxMonth;

		private MMLabel mmLabel3;

		private TextBox textBoxSheetName;

		private MMLabel mmLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMLabel mmLabel5;

		private MMLabel mmLabel7;

		private DateTimePicker dateTimePickerChequeDate;

		private ChequebookComboBox comboBoxChequebook;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxChequeNumber;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonInformation;

		private AmountTextBox textBoxAmountOther;

		private MMLabel mmLabel9;

		private AmountTextBox textBoxAmountTotal;

		private AllAccountsComboBox comboBoxAccount;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonDistribution;

		private MMLabel mmLabel8;

		private AmountTextBox textBoxTaxAmount;

		private TaxGroupComboBox comboBoxTaxGroup;

		private Label labelTaxGroup;

		private CheckBox checkBoxManualUpdate;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5029;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

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
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = true);
					sysDocComboBox.Enabled = enabled;
					buttonSelectSalarySheet.Enabled = true;
					textBoxAmountOther.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					if (IsVoid)
					{
						buttonVoid.Enabled = false;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
					buttonSelectSalarySheet.Enabled = false;
				}
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
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
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
					buttonSelectSalarySheet.Enabled = !value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
						buttonVoid.Enabled = false;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public ChequeSalaryPaymentForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.ContextMenuStrip = null;
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.HeaderClicked += dataGridItem_HeaderClicked;
			comboBoxChequebook.SelectedIndexChanged += comboBoxChequebook_SelectedIndexChanged;
			comboBoxTaxGroup.SelectedIndexChanged += comboBoxTaxGroup_SelectedIndexChanged;
		}

		private void comboBoxChequebook_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxChequeNumber.Text = Factory.ChequebookSystem.GetNextChequeNumber(comboBoxChequebook.SelectedID).ToString();
			}
			catch
			{
			}
		}

		private void comboBoxTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			ItemTaxOptions itemTaxOptions = ItemTaxOptions.NonTaxable;
			if (comboBoxTaxGroup.SelectedID != "")
			{
				itemTaxOptions = ItemTaxOptions.Taxable;
			}
			if (itemTaxOptions != ItemTaxOptions.NonTaxable)
			{
				TaxTransactionData tag = TaxHelper.CreateTaxDetailData(PayeeTaxOptions.Taxable, comboBoxTaxGroup.SelectedID);
				textBoxTaxAmount.Tag = tag;
			}
			else
			{
				textBoxTaxAmount.Tag = null;
			}
			CalculateTax();
			CalculateTotal();
		}

		private void dataGridItem_HeaderClicked(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn == null)
			{
				return;
			}
			if (ultraGridColumn.Key == "Payroll Item")
			{
				string text = "";
				byte b = byte.Parse(dataGridItems.ActiveRow.Cells["Pay Type"].Value.ToString());
				if (dataGridItems.ActiveRow != null)
				{
					text = dataGridItems.ActiveRow.Cells["Payroll Item"].Text;
				}
				if (!(text == ""))
				{
					FormHelper formHelper = new FormHelper();
					switch (b)
					{
					case 2:
						formHelper.EditDeduction(text);
						break;
					case 1:
						formHelper.EditPayrollItem(text);
						break;
					}
				}
			}
			else if (ultraGridColumn.Key == "Emp ID")
			{
				string text2 = "";
				if (dataGridItems.ActiveRow != null)
				{
					text2 = dataGridItems.ActiveRow.Cells["Emp ID"].Text;
				}
				if (!(text2 == ""))
				{
					new FormHelper().EditEmployee(text2);
				}
			}
		}

		private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (!isUpdatingGrid)
			{
				try
				{
					isUpdatingGrid = true;
					if (e.Cell.Column.Key == "Amount")
					{
						if (e.Cell.Value == null || e.Cell.Value.ToString() == "" || decimal.Parse(e.Cell.Value.ToString()) == 0m)
						{
							e.Cell.Row.Cells["C"].Value = false;
						}
						else
						{
							e.Cell.Row.Cells["C"].Value = true;
						}
						CalculateTotal();
					}
					else if (e.Cell.Column.Key == "C")
					{
						if (!bool.Parse(e.Cell.Text.ToString()))
						{
							e.Cell.Row.Cells["Amount"].Value = 0.ToString(Format.TotalAmountFormat);
						}
						else if (e.Cell.Row.Cells["Amount"].Value == null || e.Cell.Row.Cells["Amount"].Value.ToString() == "" || decimal.Parse(e.Cell.Row.Cells["Amount"].Value.ToString()) == 0m)
						{
							e.Cell.Row.Cells["Amount"].Value = e.Cell.Row.Cells["Balance"].Value;
						}
						CalculateTotal();
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

		private void CalculateTotal()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal num3 = default(decimal);
				if (row.Cells["Amount"].Value != null && row.Cells["Amount"].Value.ToString() != "")
				{
					num3 = decimal.Parse(row.Cells["Amount"].Value.ToString());
					d += num3;
				}
			}
			num = textBoxAmountOther.Value;
			num2 = textBoxTaxAmount.Value;
			labelBalance.Text = d.ToString(Format.TotalAmountFormat);
			string text3 = textBoxAmountTotal.Text = (textBoxAmount.Text = (d + num + num2).ToString(Format.TotalAmountFormat));
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "C")
			{
				if (!bool.Parse(e.Cell.Text.ToString()))
				{
					e.Cell.Row.Cells["Amount"].Value = 0.ToString(Format.TotalAmountFormat);
				}
				else if (e.Cell.Row.Cells["Amount"].Value == null || e.Cell.Row.Cells["Amount"].Value.ToString() == "" || decimal.Parse(e.Cell.Row.Cells["Amount"].Value.ToString()) == 0m)
				{
					e.Cell.Row.Cells["Amount"].Value = e.Cell.Row.Cells["Balance"].Value;
				}
			}
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Account")
			{
				if (dataGridItems.ActiveRow.Cells["Emp ID"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Emp Name"].Value = "";
				}
			}
			else if (dataGridItems.ActiveCell.Column.Key == "Amount" && dataGridItems.ActiveCell.Text != "")
			{
				dataGridItems.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridItems.ActiveCell.Text, NumberStyles.Any), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["Balance"].Value.ToString(), out result);
				decimal result3 = default(decimal);
				decimal.TryParse(e.NewValue.ToString(), out result3);
				decimal.TryParse(e.Cell.Row.Cells["Amount"].Value.ToString(), out result2);
				if (result2 < 0m && (Math.Abs(result3 - result) < 1m || result3 < 0m))
				{
					ErrorHelper.InformationMessage("Please enter an positive amount not greater than balance.");
					e.Cancel = true;
				}
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (!(dataGridItems.ActiveCell.Column.Key.ToString() == "Account"))
			{
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
				{
					e.RaiseErrorEvent = false;
					ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "WorkDays")
				{
					e.RaiseErrorEvent = false;
					ErrorHelper.InformationMessage("Please enter a numeric value.");
				}
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PayrollTransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PayrollTransactionTable.Rows[0] : currentData.PayrollTransactionTable.NewRow();
				dataRow["Description"] = textBoxNote.Text;
				if (checkBoxManualUpdate.Checked && string.IsNullOrEmpty(textBoxNote.Text))
				{
					dataRow["Description"] = "Updated Manually";
				}
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["RegisterID"] = comboBoxRegister.SelectedID;
				dataRow["PaymentMethodType"] = (byte)2;
				dataRow["ChequebookID"] = comboBoxChequebook.SelectedID;
				dataRow["CheckNumber"] = textBoxChequeNumber.Text;
				dataRow["CheckDate"] = dateTimePickerChequeDate.Value;
				dataRow["CurrencyID"] = DBNull.Value;
				dataRow["CurrencyRate"] = 1;
				dataRow["Amount"] = decimal.Parse(textBoxAmountTotal.Text);
				dataRow["ChequeTotal"] = decimal.Parse(textBoxAmountTotal.Text);
				dataRow["OtherCharges"] = decimal.Parse(textBoxAmountOther.Text);
				dataRow["OtherAccountID"] = comboBoxAccount.SelectedID;
				if (comboBoxTaxGroup.SelectedID != "")
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow["TaxAmount"] = decimal.Parse(textBoxTaxAmount.Text);
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PayrollTransactionTable.Rows.Add(dataRow);
				}
				currentData.PayrollTransactionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (bool.Parse(row.Cells["C"].Value.ToString()))
					{
						DataRow dataRow2 = currentData.PayrollTransactionDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["Description"] = textBoxNote.Text;
						dataRow2["EmployeeID"] = row.Cells["EmployeeID"].Value.ToString();
						dataRow2["SheetSysDocID"] = row.Cells["SheetSysDocID"].Value.ToString();
						dataRow2["SheetVoucherID"] = row.Cells["SheetVoucherID"].Value.ToString();
						if (row.Cells["WorkDays"].Value != null && row.Cells["WorkDays"].Value.ToString() != "")
						{
							dataRow2["Days"] = row.Cells["WorkDays"].Value.ToString();
						}
						dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
						dataRow2["RowIndex"] = row.Index;
						dataRow2["SheetRowIndex"] = row.Cells["SheetRowIndex"].Value.ToString();
						dataRow2.EndEdit();
						currentData.PayrollTransactionDetailTable.Rows.Add(dataRow2);
					}
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				if (textBoxTaxAmount.Tag != null)
				{
					TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text, -1, Global.BaseCurrencyID, 1m);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataTable("Salary");
				dataTable.Columns.Add("C", typeof(bool));
				dataTable.Columns.Add("SheetSysDocID");
				dataTable.Columns.Add("SheetVoucherID");
				dataTable.Columns.Add("SheetRowIndex", typeof(int));
				dataTable.Columns.Add("EmployeeID");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("WorkDays", typeof(decimal));
				dataTable.Columns.Add("NetSalary", typeof(decimal));
				dataTable.Columns.Add("Balance", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["SheetSysDocID"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["SheetVoucherID"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["SheetRowIndex"].Hidden = true;
				bool hidden = ultraGridColumn2.Hidden = flag2;
				ultraGridColumn.Hidden = hidden;
				CellClickAction cellClickAction3 = dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = (dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellClickAction = CellClickAction.EditAndSelectText);
				Activation activation3 = dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = (dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit);
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].Width = 25;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].LockedWidth = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["C"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance;
				HAlign hAlign2 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign4 = cellAppearance3.TextHAlign = hAlign2;
				HAlign hAlign7 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign4);
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance7 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = cellAppearance7.BackColor = color;
				Color color5 = cellAppearance6.BackColor = color3;
				Color color8 = cellAppearance4.BackColor = (cellAppearance5.BackColor = color5);
				AppearanceBase cellAppearance8 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance9 = dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance;
				AppearanceBase cellAppearance10 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance11 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].CellAppearance;
				color = (dataGridItems.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				color3 = (cellAppearance11.BackColorDisabled = color);
				color5 = (cellAppearance10.BackColorDisabled = color3);
				color8 = (cellAppearance8.BackColorDisabled = (cellAppearance9.BackColorDisabled = color5));
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Amount"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["Balance"];
				string text = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].Format = Format.GridAmountFormat;
				string text3 = ultraGridColumn5.Format = text;
				string text6 = ultraGridColumn3.Format = (ultraGridColumn4.Format = text3);
				dataGridItems.DisplayLayout.Override.TemplateAddRowPrompt = "";
				dataGridItems.AllowAddNew = false;
				dataGridItems.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItems.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.PayrollTransactionSystem.GetPayrollTransactionByID(SystemDocID, voucherID);
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

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables["Payroll_Transaction"].Rows[0];
			dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
			textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
			textBoxRef1.Text = dataRow["Reference"].ToString();
			comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
			comboBoxRegister.SelectedID = dataRow["RegisterID"].ToString();
			comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
			comboBoxChequebook.SelectedID = dataRow["ChequebookID"].ToString();
			textBoxChequeNumber.Text = dataRow["CheckNumber"].ToString();
			dateTimePickerChequeDate.Value = DateTime.Parse(dataRow["CheckDate"].ToString());
			if (dataRow["IsVoid"] != DBNull.Value)
			{
				IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
			}
			else
			{
				IsVoid = false;
			}
			textBoxNote.Text = dataRow["Description"].ToString();
			textBoxAmountTotal.Text = dataRow["ChequeTotal"].ToString();
			textBoxAmountOther.Text = dataRow["OtherCharges"].ToString();
			comboBoxAccount.SelectedID = dataRow["OtherAccountID"].ToString();
			if (dataRow["TaxGroupID"] != DBNull.Value)
			{
				comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
			}
			else
			{
				comboBoxTaxGroup.SelectedID = "";
			}
			textBoxTaxAmount.Text = dataRow["TaxAmount"].ToString();
			if (string.IsNullOrEmpty(comboBoxRegister.SelectedID) && string.IsNullOrEmpty(comboBoxChequebook.SelectedID))
			{
				checkBoxManualUpdate.Checked = true;
			}
			else
			{
				checkBoxManualUpdate.Checked = false;
			}
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (currentData.Tables.Contains("Payroll_Transaction_Detail") && currentData.PayrollTransactionDetailTable.Rows.Count != 0)
			{
				foreach (DataRow row in currentData.Tables["Payroll_Transaction_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["C"] = true;
					dataRow3["SheetSysDocID"] = row["SheetSysDocID"];
					dataRow3["SheetVoucherID"] = row["SheetVoucherID"];
					dataRow3["SheetRowIndex"] = row["SheetRowIndex"];
					dataRow3["EmployeeID"] = row["EmployeeID"];
					dataRow3["Name"] = row["EmployeeName"];
					dataRow3["WorkDays"] = row["Days"];
					dataRow3["NetSalary"] = row["NetSalary"];
					dataRow3["Amount"] = row["Amount"];
					dataRow3["Balance"] = row["Balance"];
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
				dataTable.AcceptChanges();
				DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
				if (array.Length != 0)
				{
					TaxTransactionData taxTransactionData = new TaxTransactionData();
					taxTransactionData.Merge(array);
					textBoxTaxAmount.Tag = taxTransactionData;
				}
				CalculateTax();
				textBoxSheetName.Text = currentData.Tables["Payroll_Transaction_Detail"].Rows[0]["SheetName"].ToString();
				textBoxMonth.Text = currentData.Tables["Payroll_Transaction_Detail"].Rows[0]["Month"].ToString();
				textBoxYear.Text = currentData.Tables["Payroll_Transaction_Detail"].Rows[0]["Year"].ToString();
				CalculateTotal();
			}
		}

		private bool SaveData()
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
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
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
				bool flag = (!isNewRecord) ? Factory.PayrollTransactionSystem.CreatePayrollTransaction(currentData, isUpdate: true, checkBoxManualUpdate.Checked) : Factory.PayrollTransactionSystem.CreatePayrollTransaction(currentData, isUpdate: false, checkBoxManualUpdate.Checked);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool result = false;
					bool result2 = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("DoPrint").ToString(), out result);
					if (result)
					{
						bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result2);
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
			if (isNewRecord && dateTimePickerDate.Value < DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			new ArrayList();
			if (checkBoxManualUpdate.Checked)
			{
				if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
			}
			else if (comboBoxRegister.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "" || comboBoxChequebook.SelectedID == "" || textBoxChequeNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (dataGridItems.Rows.Count <= 0)
			{
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Payroll_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (bool.Parse(row.Cells["C"].Value.ToString()) && row.Cells["NetSalary"].Value != null && row.Cells["NetSalary"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["NetSalary"].Value.ToString());
				}
			}
			return result;
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
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxSheetName.Clear();
				textBoxMonth.Clear();
				textBoxYear.Clear();
				textBoxAmountTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmountOther.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxRegister.SelectedID = "";
				comboBoxChequebook.SelectedID = "";
				comboBoxAccount.SelectedID = "";
				textBoxChequeNumber.Clear();
				dateTimePickerChequeDate.Value = DateTime.Now;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				labelBalance.Text = 0.ToString(Format.TotalAmountFormat);
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxTaxGroup.SelectedID = "";
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerDate.Focus();
				checkBoxManualUpdate.Checked = false;
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
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.PayrollTransactionSystem.DeletePayrollTransaction(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Payroll_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID, "PaymentMethodType", 2);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Payroll_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID, "PaymentMethodType", 2);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Payroll_Transaction", "VoucherID", "SysDocID", SystemDocID, "PaymentMethodType", 2);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Payroll_Transaction", "VoucherID", "SysDocID", SystemDocID, "PaymentMethodType", 2);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Payroll_Transaction", "VoucherID", SystemDocID, "PaymentMethodType", 1, toolStripTextBoxFind.Text);
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				comboBoxTaxGroup.ReadOnly = !CompanyPreferences.IsTax;
				SetSecurity();
				IsNewRecord = true;
				if (!base.IsDisposed)
				{
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.SalaryPaymentCheque);
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
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
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				if (!IsNewRecord)
				{
					return textBoxVoucherNumber.Text;
				}
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
				return Factory.PayrollTransactionSystem.VoidPayrollTransaction(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Payroll_Transaction", "VoucherID");
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.SalaryPaymentCheque);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRegister(comboBoxRegister.SelectedID);
		}

		private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
		{
			checked
			{
				_ = default(TimeSpan).Days + 1;
			}
		}

		private void buttonSelectSalarySheet_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet dataSet = Factory.SalarySheetSystem.GetOpenSalarySheets();
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = dataSet;
				selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["SysDocID"].Hidden = true;
				selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["StartDate"].Hidden = true;
				selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["EndDate"].Hidden = true;
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				selectDocumentDialog.Text = "Select Salary Sheet";
				selectDocumentDialog.IsMultiSelect = true;
				DialogResult dialogResult = selectDocumentDialog.ShowDialog(this);
				if (dialogResult == DialogResult.OK && dialogResult == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						string text = "";
						string text2 = "";
						text = selectedRow.Cells["SysDocID"].Value.ToString();
						text2 = selectedRow.Cells["VoucherID"].Value.ToString();
						textBoxMonth.Text = selectedRow.Cells["Month"].Value.ToString();
						textBoxYear.Text = selectedRow.Cells["Year"].Value.ToString();
						textBoxSheetName.Text = selectedRow.Cells["SheetName"].Value.ToString();
						arrayList.Add(text);
						arrayList2.Add(text2);
						if (text == "" || text2 == "")
						{
							return;
						}
						dataSet = Factory.SalarySheetSystem.GetSalarySheetEmployees(text, text2, 1);
					}
					try
					{
						CalculateSalaryForm calculateSalaryForm = new CalculateSalaryForm();
						if (calculateSalaryForm.ShowDialog() != DialogResult.OK)
						{
							return;
						}
						dataSet = Factory.SalarySheetSystem.GetSelectedSalaryBankTransfer(calculateSalaryForm.FromEmployee, calculateSalaryForm.ToEmployee, calculateSalaryForm.FromDepartment, calculateSalaryForm.ToDepartment, calculateSalaryForm.FromLocation, calculateSalaryForm.ToLocation, calculateSalaryForm.FromType, calculateSalaryForm.ToType, calculateSalaryForm.FromDivision, calculateSalaryForm.ToDivision, calculateSalaryForm.FromSponsor, calculateSalaryForm.ToSponsor, calculateSalaryForm.FromGroup, calculateSalaryForm.ToGroup, calculateSalaryForm.FromGrade, calculateSalaryForm.ToGrade, calculateSalaryForm.FromPosition, calculateSalaryForm.ToPosition, calculateSalaryForm.FromBank, calculateSalaryForm.ToBank, calculateSalaryForm.FromAccount, calculateSalaryForm.ToAccount, (string[])arrayList.ToArray(typeof(string)), (string[])arrayList2.ToArray(typeof(string)));
						if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
						{
							return;
						}
					}
					catch (Exception e2)
					{
						ErrorHelper.ProcessError(e2);
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in dataSet.Tables["SalarySheet_Detail"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["C"] = false;
						dataRow2["SheetSysDocID"] = row["SysDocID"];
						dataRow2["SheetVoucherID"] = row["VoucherID"];
						dataRow2["SheetRowIndex"] = row["RowIndex"];
						dataRow2["EmployeeID"] = row["EmployeeID"];
						dataRow2["Name"] = row["Name"];
						dataRow2["WorkDays"] = row["WorkDays"];
						dataRow2["NetSalary"] = row["NetSalary"];
						dataRow2["Balance"] = row["Balance"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					CalculateTotal();
				}
			}
			catch (Exception e3)
			{
				ErrorHelper.ProcessError(e3);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ChequeSalaryPaymentListFormObj);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				DataSet employeeSalaryToPrint = Factory.PayrollTransactionSystem.GetEmployeeSalaryToPrint(selectedID, text);
				if (employeeSalaryToPrint == null || employeeSalaryToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(employeeSalaryToPrint, selectedID, "Cheque Salary", SysDocTypes.PayrollTransaction, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void textBoxAmountOther_TextChanged(object sender, EventArgs e)
		{
			if (textBoxAmountOther.Focused)
			{
				CalculateTax();
				CalculateTotal();
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxAccount.SelectedID != "")
			{
				textBoxAmountOther.Enabled = true;
				return;
			}
			textBoxAmountOther.Value = 0m;
			textBoxAmountOther.Enabled = false;
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void CalculateTax()
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxAmountOther.Text, out result);
			UIGlobal.CalculateFieldTax(textBoxTaxAmount, result, priceIncludeTax: false);
			decimal result2 = default(decimal);
			decimal.TryParse(textBoxTaxAmount.Text, out result2);
		}

		private void checkBoxManualUpdate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxManualUpdate.Checked)
			{
				comboBoxRegister.Clear();
				comboBoxChequebook.Clear();
				textBoxChequeNumber.Clear();
				comboBoxRegister.ReadOnly = true;
				comboBoxChequebook.ReadOnly = true;
				textBoxChequeNumber.ReadOnly = true;
			}
			else
			{
				comboBoxRegister.ReadOnly = false;
				comboBoxChequebook.ReadOnly = false;
				textBoxChequeNumber.ReadOnly = false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.ChequeSalaryPaymentForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			dateTimePickerChequeDate = new System.Windows.Forms.DateTimePicker();
			textBoxChequeNumber = new System.Windows.Forms.TextBox();
			comboBoxChequebook = new Micromind.DataControls.ChequebookComboBox();
			textBoxSheetName = new System.Windows.Forms.TextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxYear = new System.Windows.Forms.TextBox();
			textBoxMonth = new System.Windows.Forms.TextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxRegister = new Micromind.DataControls.RegisterComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			formManager = new Micromind.DataControls.FormManager();
			buttonSelectSalarySheet = new Micromind.UISupport.XPButton();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			textBoxAmountOther = new Micromind.UISupport.AmountTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxAmountTotal = new Micromind.UISupport.AmountTextBox();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			labelTaxGroup = new System.Windows.Forms.Label();
			checkBoxManualUpdate = new System.Windows.Forms.CheckBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).BeginInit();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 600);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(726, 40);
			panelButtons.TabIndex = 4;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(318, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
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
			buttonNew.TabIndex = 1;
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
			linePanelDown.Size = new System.Drawing.Size(726, 1);
			linePanelDown.TabIndex = 5;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(616, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
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
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(91, 27);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 3;
			textBoxVoucherNumber.Location = new System.Drawing.Point(294, 5);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(138, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(217, 29);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(294, 27);
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(138, 20);
			textBoxRef1.TabIndex = 4;
			textBoxNote.Location = new System.Drawing.Point(91, 122);
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(536, 20);
			textBoxNote.TabIndex = 8;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 125);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(220, 7);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance3.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			appearance3.TextHAlignAsString = "Right";
			appearance3.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance3;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 2);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(557, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Salary Total:";
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance4.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance4.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance4;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 516);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(702, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelBalance.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance5.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			appearance5.TextHAlignAsString = "Right";
			appearance5.TextVAlignAsString = "Middle";
			labelBalance.Appearance = appearance5;
			labelBalance.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelBalance.Location = new System.Drawing.Point(559, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(mmLabel5);
			panelDetails.Controls.Add(mmLabel7);
			panelDetails.Controls.Add(dateTimePickerChequeDate);
			panelDetails.Controls.Add(textBoxChequeNumber);
			panelDetails.Controls.Add(comboBoxChequebook);
			panelDetails.Controls.Add(textBoxSheetName);
			panelDetails.Controls.Add(mmLabel6);
			panelDetails.Controls.Add(textBoxYear);
			panelDetails.Controls.Add(textBoxMonth);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(comboBoxRegister);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(715, 148);
			panelDetails.TabIndex = 0;
			comboBoxSysDoc.Assigned = false;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(91, 5);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(107, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance6.FontData.BoldAsString = "True";
			appearance6.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance6;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(11, 77);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel1.TabIndex = 152;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Chequebook:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance7;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = true;
			mmLabel5.Location = new System.Drawing.Point(217, 77);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(53, 13);
			mmLabel5.TabIndex = 151;
			mmLabel5.Text = "Chq No:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = true;
			mmLabel7.Location = new System.Drawing.Point(438, 78);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(64, 13);
			mmLabel7.TabIndex = 150;
			mmLabel7.Text = "Chq Date:";
			dateTimePickerChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerChequeDate.Location = new System.Drawing.Point(519, 74);
			dateTimePickerChequeDate.Name = "dateTimePickerChequeDate";
			dateTimePickerChequeDate.Size = new System.Drawing.Size(148, 20);
			dateTimePickerChequeDate.TabIndex = 7;
			textBoxChequeNumber.Location = new System.Drawing.Point(294, 75);
			textBoxChequeNumber.Name = "textBoxChequeNumber";
			textBoxChequeNumber.Size = new System.Drawing.Size(138, 20);
			textBoxChequeNumber.TabIndex = 999;
			textBoxChequeNumber.TabStop = false;
			comboBoxChequebook.Assigned = false;
			comboBoxChequebook.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxChequebook.CustomReportFieldName = "";
			comboBoxChequebook.CustomReportKey = "";
			comboBoxChequebook.CustomReportValueType = 1;
			comboBoxChequebook.DescriptionTextBox = null;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxChequebook.DisplayLayout.Appearance = appearance8;
			comboBoxChequebook.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxChequebook.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance9.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.GroupByBox.Appearance = appearance9;
			appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.BandLabelAppearance = appearance10;
			comboBoxChequebook.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance11.BackColor2 = System.Drawing.SystemColors.Control;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.PromptAppearance = appearance11;
			comboBoxChequebook.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxChequebook.DisplayLayout.MaxRowScrollRegions = 1;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxChequebook.DisplayLayout.Override.ActiveCellAppearance = appearance12;
			appearance13.BackColor = System.Drawing.SystemColors.Highlight;
			appearance13.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxChequebook.DisplayLayout.Override.ActiveRowAppearance = appearance13;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.CardAreaAppearance = appearance14;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			appearance15.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxChequebook.DisplayLayout.Override.CellAppearance = appearance15;
			comboBoxChequebook.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxChequebook.DisplayLayout.Override.CellPadding = 0;
			appearance16.BackColor = System.Drawing.SystemColors.Control;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.GroupByRowAppearance = appearance16;
			appearance17.TextHAlignAsString = "Left";
			comboBoxChequebook.DisplayLayout.Override.HeaderAppearance = appearance17;
			comboBoxChequebook.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxChequebook.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			comboBoxChequebook.DisplayLayout.Override.RowAppearance = appearance18;
			comboBoxChequebook.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxChequebook.DisplayLayout.Override.TemplateAddRowAppearance = appearance19;
			comboBoxChequebook.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxChequebook.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxChequebook.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxChequebook.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxChequebook.Editable = true;
			comboBoxChequebook.FilterString = "";
			comboBoxChequebook.HasAllAccount = false;
			comboBoxChequebook.HasCustom = false;
			comboBoxChequebook.IsDataLoaded = false;
			comboBoxChequebook.Location = new System.Drawing.Point(92, 74);
			comboBoxChequebook.MaxDropDownItems = 12;
			comboBoxChequebook.Name = "comboBoxChequebook";
			comboBoxChequebook.ShowInactiveItems = false;
			comboBoxChequebook.ShowQuickAdd = true;
			comboBoxChequebook.Size = new System.Drawing.Size(107, 20);
			comboBoxChequebook.TabIndex = 5;
			comboBoxChequebook.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxSheetName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSheetName.ForeColor = System.Drawing.Color.Black;
			textBoxSheetName.Location = new System.Drawing.Point(91, 98);
			textBoxSheetName.Name = "textBoxSheetName";
			textBoxSheetName.ReadOnly = true;
			textBoxSheetName.Size = new System.Drawing.Size(341, 20);
			textBoxSheetName.TabIndex = 111;
			textBoxSheetName.TabStop = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 101);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(69, 13);
			mmLabel6.TabIndex = 137;
			mmLabel6.Text = "Sheet Name:";
			textBoxYear.Location = new System.Drawing.Point(294, 50);
			textBoxYear.Name = "textBoxYear";
			textBoxYear.ReadOnly = true;
			textBoxYear.Size = new System.Drawing.Size(138, 20);
			textBoxYear.TabIndex = 111;
			textBoxYear.TabStop = false;
			textBoxMonth.Location = new System.Drawing.Point(91, 50);
			textBoxMonth.Name = "textBoxMonth";
			textBoxMonth.ReadOnly = true;
			textBoxMonth.Size = new System.Drawing.Size(107, 20);
			textBoxMonth.TabIndex = 111;
			textBoxMonth.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(217, 53);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(32, 13);
			mmLabel3.TabIndex = 133;
			mmLabel3.Text = "Year:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(9, 53);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(40, 13);
			mmLabel4.TabIndex = 133;
			mmLabel4.Text = "Month:";
			appearance20.FontData.BoldAsString = "True";
			appearance20.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance20;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(441, 8);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel6.TabIndex = 128;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Register:";
			appearance21.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance21;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			comboBoxRegister.Assigned = false;
			comboBoxRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRegister.CustomReportFieldName = "";
			comboBoxRegister.CustomReportKey = "";
			comboBoxRegister.CustomReportValueType = 1;
			comboBoxRegister.DescriptionTextBox = null;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRegister.DisplayLayout.Appearance = appearance22;
			comboBoxRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance23.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.GroupByBox.Appearance = appearance23;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance24;
			comboBoxRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance25.BackColor2 = System.Drawing.SystemColors.Control;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance25;
			comboBoxRegister.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRegister.DisplayLayout.MaxRowScrollRegions = 1;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRegister.DisplayLayout.Override.ActiveCellAppearance = appearance26;
			appearance27.BackColor = System.Drawing.SystemColors.Highlight;
			appearance27.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRegister.DisplayLayout.Override.ActiveRowAppearance = appearance27;
			comboBoxRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.CardAreaAppearance = appearance28;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			appearance29.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRegister.DisplayLayout.Override.CellAppearance = appearance29;
			comboBoxRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRegister.DisplayLayout.Override.CellPadding = 0;
			appearance30.BackColor = System.Drawing.SystemColors.Control;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.GroupByRowAppearance = appearance30;
			appearance31.TextHAlignAsString = "Left";
			comboBoxRegister.DisplayLayout.Override.HeaderAppearance = appearance31;
			comboBoxRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			comboBoxRegister.DisplayLayout.Override.RowAppearance = appearance32;
			comboBoxRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance33;
			comboBoxRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRegister.Editable = true;
			comboBoxRegister.FilterString = "";
			comboBoxRegister.HasAllAccount = false;
			comboBoxRegister.HasCustom = false;
			comboBoxRegister.IsDataLoaded = false;
			comboBoxRegister.Location = new System.Drawing.Point(515, 5);
			comboBoxRegister.MaxDropDownItems = 12;
			comboBoxRegister.Name = "comboBoxRegister";
			comboBoxRegister.ShowDefaultRegisterOnly = false;
			comboBoxRegister.ShowInactiveItems = false;
			comboBoxRegister.ShowQuickAdd = true;
			comboBoxRegister.Size = new System.Drawing.Size(152, 20);
			comboBoxRegister.TabIndex = 2;
			comboBoxRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(438, 31);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(46, 13);
			mmLabel2.TabIndex = 130;
			mmLabel2.Text = "Amount:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(515, 28);
			textBoxAmount.MaxLength = 15;
			textBoxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.NullText = "0";
			textBoxAmount.ReadOnly = true;
			textBoxAmount.Size = new System.Drawing.Size(152, 20);
			textBoxAmount.TabIndex = 111;
			textBoxAmount.TabStop = false;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			appearance34.FontData.BoldAsString = "True";
			appearance34.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance34;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 8);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 118;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance35.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance35;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(9, 28);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(33, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 473);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(698, 43);
			labelVoided.TabIndex = 1;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.ToolTipText = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.ToolTipText = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.ToolTipText = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.ToolTipText = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(726, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
			buttonSelectSalarySheet.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectSalarySheet.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonSelectSalarySheet.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectSalarySheet.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectSalarySheet.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectSalarySheet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectSalarySheet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectSalarySheet.Location = new System.Drawing.Point(591, 189);
			buttonSelectSalarySheet.Name = "buttonSelectSalarySheet";
			buttonSelectSalarySheet.Size = new System.Drawing.Size(122, 24);
			buttonSelectSalarySheet.TabIndex = 1;
			buttonSelectSalarySheet.Text = "Select Salary Sheet...";
			buttonSelectSalarySheet.UseVisualStyleBackColor = false;
			buttonSelectSalarySheet.Click += new System.EventHandler(buttonSelectSalarySheet_Click);
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance36;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance37.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance37;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance38;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance39.BackColor2 = System.Drawing.SystemColors.Control;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance39;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance40;
			appearance41.BackColor = System.Drawing.SystemColors.Highlight;
			appearance41.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance41;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance42;
			appearance43.BorderColor = System.Drawing.Color.Silver;
			appearance43.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance43;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance44.BackColor = System.Drawing.SystemColors.Control;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance44;
			appearance45.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance45;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance46;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance47.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance47;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 215);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(700, 302);
			dataGridItems.TabIndex = 3;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			textBoxAmountOther.AllowDecimal = true;
			textBoxAmountOther.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxAmountOther.BackColor = System.Drawing.Color.White;
			textBoxAmountOther.CustomReportFieldName = "";
			textBoxAmountOther.CustomReportKey = "";
			textBoxAmountOther.CustomReportValueType = 1;
			textBoxAmountOther.IsComboTextBox = false;
			textBoxAmountOther.IsModified = false;
			textBoxAmountOther.Location = new System.Drawing.Point(616, 537);
			textBoxAmountOther.MaxLength = 15;
			textBoxAmountOther.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmountOther.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmountOther.Name = "textBoxAmountOther";
			textBoxAmountOther.NullText = "0";
			textBoxAmountOther.Size = new System.Drawing.Size(96, 20);
			textBoxAmountOther.TabIndex = 3;
			textBoxAmountOther.TabStop = false;
			textBoxAmountOther.Text = "0.00";
			textBoxAmountOther.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmountOther.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxAmountOther.TextChanged += new System.EventHandler(textBoxAmountOther_TextChanged);
			mmLabel9.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(534, 583);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(74, 13);
			mmLabel9.TabIndex = 1002;
			mmLabel9.Text = "Cheque Total:";
			textBoxAmountTotal.AllowDecimal = true;
			textBoxAmountTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxAmountTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmountTotal.CustomReportFieldName = "";
			textBoxAmountTotal.CustomReportKey = "";
			textBoxAmountTotal.CustomReportValueType = 1;
			textBoxAmountTotal.IsComboTextBox = false;
			textBoxAmountTotal.IsModified = false;
			textBoxAmountTotal.Location = new System.Drawing.Point(616, 579);
			textBoxAmountTotal.MaxLength = 15;
			textBoxAmountTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmountTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmountTotal.Name = "textBoxAmountTotal";
			textBoxAmountTotal.NullText = "0";
			textBoxAmountTotal.ReadOnly = true;
			textBoxAmountTotal.Size = new System.Drawing.Size(96, 20);
			textBoxAmountTotal.TabIndex = 13;
			textBoxAmountTotal.Text = "0.00";
			textBoxAmountTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmountTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxAccount.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			comboBoxAccount.Assigned = false;
			comboBoxAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = null;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			appearance48.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance48;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance49.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance49;
			appearance50.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance50;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance51.BackColor2 = System.Drawing.SystemColors.Control;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance51;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance52;
			appearance53.BackColor = System.Drawing.SystemColors.Highlight;
			appearance53.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance53;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance54;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			appearance55.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance55;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance56.BackColor = System.Drawing.SystemColors.Control;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance56;
			appearance57.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance57;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance58;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance59.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance59;
			comboBoxAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccount.Editable = true;
			comboBoxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccount.FilterString = "";
			comboBoxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccount.FilterSysDocID = "";
			comboBoxAccount.HasAllAccount = false;
			comboBoxAccount.HasCustom = false;
			comboBoxAccount.IsDataLoaded = false;
			comboBoxAccount.Location = new System.Drawing.Point(501, 537);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(100, 20);
			comboBoxAccount.TabIndex = 2;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAccount.SelectedIndexChanged += new System.EventHandler(comboBoxAccount_SelectedIndexChanged);
			mmLabel8.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(417, 542);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(78, 13);
			mmLabel8.TabIndex = 1006;
			mmLabel8.Text = "Other Charges:";
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(616, 558);
			textBoxTaxAmount.MaxLength = 15;
			textBoxTaxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTaxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTaxAmount.Name = "textBoxTaxAmount";
			textBoxTaxAmount.NullText = "0";
			textBoxTaxAmount.ReadOnly = true;
			textBoxTaxAmount.Size = new System.Drawing.Size(96, 20);
			textBoxTaxAmount.TabIndex = 8;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxTaxGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			appearance60.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance60;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance61.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance61;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance62;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance63.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance63.BackColor2 = System.Drawing.SystemColors.Control;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance63;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance64;
			appearance65.BackColor = System.Drawing.SystemColors.Highlight;
			appearance65.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance65;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance66;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			appearance67.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance67;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance68.BackColor = System.Drawing.SystemColors.Control;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance68;
			appearance69.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance69;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance70;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance71.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance71;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(501, 558);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(100, 20);
			comboBoxTaxGroup.TabIndex = 4;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelTaxGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(417, 563);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(60, 13);
			labelTaxGroup.TabIndex = 1009;
			labelTaxGroup.Text = "Tax Group:";
			checkBoxManualUpdate.AutoSize = true;
			checkBoxManualUpdate.Location = new System.Drawing.Point(476, 191);
			checkBoxManualUpdate.Name = "checkBoxManualUpdate";
			checkBoxManualUpdate.Size = new System.Drawing.Size(106, 17);
			checkBoxManualUpdate.TabIndex = 1010;
			checkBoxManualUpdate.Text = "Update Manually";
			checkBoxManualUpdate.UseVisualStyleBackColor = true;
			checkBoxManualUpdate.CheckedChanged += new System.EventHandler(checkBoxManualUpdate_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(726, 640);
			base.Controls.Add(checkBoxManualUpdate);
			base.Controls.Add(textBoxTaxAmount);
			base.Controls.Add(comboBoxTaxGroup);
			base.Controls.Add(labelTaxGroup);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(comboBoxAccount);
			base.Controls.Add(textBoxAmountTotal);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxAmountOther);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(buttonSelectSalarySheet);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dataGridItems);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "ChequeSalaryPaymentForm";
			Text = "Cheque Salary Payment";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
