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
	public class TransferSalaryPaymentForm : Form, IForm
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

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxAmount;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStrip toolStrip1;

		private XPButton buttonSelectSalarySheet;

		private BankAccountsComboBox comboBoxBankAccount;

		private TextBox textBoxAccountName;

		private DataEntryGrid dataGridItems;

		private MMLabel mmLabel4;

		private TextBox textBoxMonth;

		private MMLabel mmLabel3;

		private TextBox textBoxYear;

		private MMLabel mmLabel6;

		private TextBox textBoxSheetName;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private Label label4;

		private Label label5;

		private Label label6;

		private DataEntryGrid dataGriditem;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonCreateWPF;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonDistribution;

		private MMLabel mmLabel8;

		private AllAccountsComboBox comboBoxAccount;

		private AmountTextBox textBoxAmountOther;

		private AmountTextBox textBoxTaxAmount;

		private TaxGroupComboBox comboBoxTaxGroup;

		private Label labelTaxGroup;

		private AmountTextBox textBoxAmountTotal;

		private MMLabel mmLabel9;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5043;

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
				toolStripButtonCreateWPF.Enabled = !value;
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
					dataGriditem.Enabled = !value;
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

		public TransferSalaryPaymentForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGriditem.CellDataError += dataGrid_CellDataError;
			dataGriditem.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGriditem.AfterRowActivate += dataGriditem_AfterRowActivate;
			dataGriditem.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGriditem.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGriditem.BeforeCellActivate += dataGriditem_BeforeCellActivate;
			dataGriditem.CellChange += dataGriditem_CellChange;
			dataGriditem.AfterRowsDeleted += dataGriditem_AfterRowsDeleted;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGriditem.AfterCellUpdate += dataGriditem_AfterCellUpdate;
			dataGriditem.HeaderClicked += dataGridItem_HeaderClicked;
			comboBoxBankAccount.SelectedIndexChanged += comboBoxBankAccount_SelectedIndexChanged;
			comboBoxTaxGroup.SelectedIndexChanged += comboBoxTaxGroup_SelectedIndexChanged;
		}

		private void comboBoxBankAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxAccountName.Text = comboBoxBankAccount.SelectedName;
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
			if (dataGriditem.ActiveRow == null)
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
				byte b = byte.Parse(dataGriditem.ActiveRow.Cells["Pay Type"].Value.ToString());
				if (dataGriditem.ActiveRow != null)
				{
					text = dataGriditem.ActiveRow.Cells["Payroll Item"].Text;
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
				if (dataGriditem.ActiveRow != null)
				{
					text2 = dataGriditem.ActiveRow.Cells["Emp ID"].Text;
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

		private void SetBeginEndDate()
		{
		}

		private void dataGriditem_AfterCellUpdate(object sender, CellEventArgs e)
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
			foreach (UltraGridRow row in dataGriditem.Rows)
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

		private void dataGriditem_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGriditem_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGriditem_CellChange(object sender, CellEventArgs e)
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

		private void dataGriditem_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGriditem.ActiveRow == null)
			{
				return;
			}
			if (dataGriditem.ActiveCell.Column.Key == "Account")
			{
				if (dataGriditem.ActiveRow.Cells["Emp ID"].Value.ToString() == "")
				{
					dataGriditem.ActiveRow.Cells["Emp Name"].Value = "";
				}
			}
			else if (dataGriditem.ActiveCell.Column.Key == "Amount" && dataGriditem.ActiveCell.Text != "")
			{
				dataGriditem.ActiveCell.Value = decimal.Round(decimal.Parse(dataGriditem.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
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
				decimal.TryParse(e.Cell.Row.Cells["Balance"].Value.ToString(), out result);
				decimal result2 = default(decimal);
				decimal.TryParse(e.NewValue.ToString(), out result2);
				if (result2 > result || result2 < 0m)
				{
					ErrorHelper.InformationMessage("Please enter an positive amount not greater than balance.");
					e.Cancel = true;
				}
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (!(dataGriditem.ActiveCell.Column.Key.ToString() == "Account"))
			{
				if (dataGriditem.ActiveCell.Column.Key.ToString() == "Amount")
				{
					e.RaiseErrorEvent = false;
					ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
				}
				else if (dataGriditem.ActiveCell.Column.Key.ToString() == "WorkDays")
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
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["BankAccountID"] = comboBoxBankAccount.SelectedID;
				dataRow["PaymentMethodType"] = (byte)4;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
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
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PayrollTransactionTable.Rows.Add(dataRow);
				}
				currentData.PayrollTransactionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGriditem.Rows)
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
				dataGriditem.DataSource = dataTable;
				dataGriditem.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
				UltraGridColumn ultraGridColumn = dataGriditem.DisplayLayout.Bands[0].Columns["SheetSysDocID"];
				UltraGridColumn ultraGridColumn2 = dataGriditem.DisplayLayout.Bands[0].Columns["SheetVoucherID"];
				bool flag2 = dataGriditem.DisplayLayout.Bands[0].Columns["SheetRowIndex"].Hidden = true;
				bool hidden = ultraGridColumn2.Hidden = flag2;
				ultraGridColumn.Hidden = hidden;
				CellClickAction cellClickAction3 = dataGriditem.DisplayLayout.Bands[0].Columns["C"].CellClickAction = (dataGriditem.DisplayLayout.Bands[0].Columns["Amount"].CellClickAction = CellClickAction.EditAndSelectText);
				Activation activation3 = dataGriditem.DisplayLayout.Bands[0].Columns["C"].CellActivation = (dataGriditem.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit);
				dataGriditem.DisplayLayout.Bands[0].Columns["C"].Width = 25;
				dataGriditem.DisplayLayout.Bands[0].Columns["C"].LockedWidth = true;
				dataGriditem.DisplayLayout.Bands[0].Columns["C"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
				AppearanceBase cellAppearance = dataGriditem.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGriditem.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGriditem.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance;
				HAlign hAlign2 = dataGriditem.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign4 = cellAppearance3.TextHAlign = hAlign2;
				HAlign hAlign7 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign4);
				AppearanceBase cellAppearance4 = dataGriditem.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGriditem.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGriditem.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance7 = dataGriditem.DisplayLayout.Bands[0].Columns["EmployeeID"].CellAppearance;
				Color color = dataGriditem.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = cellAppearance7.BackColor = color;
				Color color5 = cellAppearance6.BackColor = color3;
				Color color8 = cellAppearance4.BackColor = (cellAppearance5.BackColor = color5);
				AppearanceBase cellAppearance8 = dataGriditem.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance9 = dataGriditem.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance;
				AppearanceBase cellAppearance10 = dataGriditem.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance11 = dataGriditem.DisplayLayout.Bands[0].Columns["EmployeeID"].CellAppearance;
				color = (dataGriditem.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				color3 = (cellAppearance11.BackColorDisabled = color);
				color5 = (cellAppearance10.BackColorDisabled = color3);
				color8 = (cellAppearance8.BackColorDisabled = (cellAppearance9.BackColorDisabled = color5));
				UltraGridColumn ultraGridColumn3 = dataGriditem.DisplayLayout.Bands[0].Columns["WorkDays"];
				UltraGridColumn ultraGridColumn4 = dataGriditem.DisplayLayout.Bands[0].Columns["Amount"];
				UltraGridColumn ultraGridColumn5 = dataGriditem.DisplayLayout.Bands[0].Columns["Balance"];
				string text = dataGriditem.DisplayLayout.Bands[0].Columns["NetSalary"].Format = Format.GridAmountFormat;
				string text3 = ultraGridColumn5.Format = text;
				string text6 = ultraGridColumn3.Format = (ultraGridColumn4.Format = text3);
				dataGriditem.DisplayLayout.Override.TemplateAddRowPrompt = "";
				dataGriditem.AllowAddNew = false;
				dataGriditem.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
				dataGriditem.SetupUI();
			}
			catch (Exception e)
			{
				dataGriditem.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGriditem.Focus();
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
			comboBoxBankAccount.SelectedID = dataRow["BankAccountID"].ToString();
			comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
			if (dataRow["IsVoid"] != DBNull.Value)
			{
				IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
			}
			else
			{
				IsVoid = false;
			}
			textBoxNote.Text = dataRow["Description"].ToString();
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
			textBoxAmountTotal.Text = dataRow["Amount"].ToString();
			DataTable dataTable = dataGriditem.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (currentData.Tables.Contains("Payroll_Transaction_Detail") && currentData.PayrollTransactionDetailTable.Rows.Count != 0)
			{
				foreach (DataRow row in currentData.Tables["Payroll_Transaction_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["C"] = false;
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
				bool flag = (!isNewRecord) ? Factory.PayrollTransactionSystem.CreatePayrollTransaction(currentData, isUpdate: true, isManual: false) : Factory.PayrollTransactionSystem.CreatePayrollTransaction(currentData, isUpdate: false, isManual: false);
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
			ArrayList arrayList = new ArrayList();
			if (dataGriditem.Rows.Count <= 0)
			{
				return false;
			}
			if (comboBoxBankAccount.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			foreach (object item in arrayList)
			{
				decimal d = default(decimal);
				DataTable dataTable = dataGriditem.DataSource as DataTable;
				if (dataTable == null)
				{
					ErrorHelper.ErrorMessage("There is no item in the list.");
					return false;
				}
				DataRow[] array = dataTable.Select("[Emp ID]='" + item.ToString() + "'");
				foreach (DataRow dataRow in array)
				{
					if (byte.Parse(dataRow["Pay Type"].ToString()) == 1)
					{
						d += decimal.Parse(dataRow["Amount"].ToString());
					}
					else
					{
						d -= decimal.Parse(dataRow["Amount"].ToString());
					}
				}
				if (d < 0m)
				{
					ErrorHelper.InformationMessage("Total payments for each employee cannot be negative. Employee:" + item.ToString());
					return false;
				}
			}
			if (GetTransactionBalance() == 0m && ErrorHelper.QuestionMessageYesNo(UIMessages.TransactionWithZeroAmount) == DialogResult.No)
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
			foreach (UltraGridRow row in dataGriditem.Rows)
			{
				if (row.Cells["NetSalary"].Value != null && row.Cells["NetSalary"].Value.ToString() != "")
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
				textBoxSheetName.Clear();
				textBoxMonth.Clear();
				textBoxYear.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxBankAccount.SelectedID = "";
				comboBoxAccount.SelectedID = "";
				comboBoxTaxGroup.SelectedID = "";
				(dataGriditem.DataSource as DataTable).Rows.Clear();
				labelBalance.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmountOther.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmountTotal.Text = 0.ToString(Format.TotalAmountFormat);
				IsVoid = false;
				formManager.ResetDirty();
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerDate.Focus();
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
			string nextID = DatabaseHelper.GetNextID("Payroll_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID, "PaymentMethodType", 4);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Payroll_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID, "PaymentMethodType", 4);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Payroll_Transaction", "VoucherID", "SysDocID", SystemDocID, "PaymentMethodType", 4);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Payroll_Transaction", "VoucherID", "SysDocID", SystemDocID, "PaymentMethodType", 4);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Payroll_Transaction", "VoucherID", SystemDocID, "PaymentMethodType", 4, toolStripTextBoxFind.Text);
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
				comboBoxTaxGroup.ReadOnly = !CompanyPreferences.IsTax;
				dataGriditem.SetupUI();
				SetupGrid();
				dataGriditem.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.PayrollTransaction);
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

		private void dataGriditem_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGriditem.ActiveRow != null)
			{
				_ = dataGriditem.ActiveCell;
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PayrollTransaction);
		}

		private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
		{
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
					DataTable dataTable = dataGriditem.DataSource as DataTable;
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

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxBankAccount.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.TransferSalaryPaymentListFormObj);
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
					PrintHelper.PrintDocument(employeeSalaryToPrint, selectedID, "Bank Transfer Salary", SysDocTypes.PayrollTransaction, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonCreateWPF_Click(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				DataSet dataSet = Factory.PayrollTransactionSystem.GeWPFToPrint(selectedID, text);
				if (dataSet == null || dataSet.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(dataSet, selectedID, "WPF Document", SysDocTypes.PayrollTransaction, isPrint: false, showPrintDialog: false);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
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

		private void comboBoxTaxGroup_SelectedIndexChanged_1(object sender, EventArgs e)
		{
		}

		private void CalculateTax()
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxAmountOther.Text, out result);
			UIGlobal.CalculateFieldTax(textBoxTaxAmount, result, priceIncludeTax: false);
			decimal result2 = default(decimal);
			decimal.TryParse(textBoxTaxAmount.Text, out result2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.TransferSalaryPaymentForm));
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
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			textBoxSheetName = new System.Windows.Forms.TextBox();
			textBoxYear = new System.Windows.Forms.TextBox();
			textBoxMonth = new System.Windows.Forms.TextBox();
			comboBoxBankAccount = new Micromind.DataControls.BankAccountsComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			textBoxAccountName = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonCreateWPF = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			buttonSelectSalarySheet = new Micromind.UISupport.XPButton();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxAmountOther = new Micromind.UISupport.AmountTextBox();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			labelTaxGroup = new System.Windows.Forms.Label();
			textBoxAmountTotal = new Micromind.UISupport.AmountTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			formManager = new Micromind.DataControls.FormManager();
			dataGriditem = new Micromind.DataControls.DataEntryGrid();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBankAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGriditem).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 552);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(792, 40);
			panelButtons.TabIndex = 6;
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
			linePanelDown.Size = new System.Drawing.Size(792, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(682, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(472, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 4;
			textBoxVoucherNumber.Location = new System.Drawing.Point(264, 3);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(138, 20);
			textBoxVoucherNumber.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(408, 27);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(408, 7);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			textBoxRef1.Location = new System.Drawing.Point(472, 25);
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(107, 20);
			textBoxRef1.TabIndex = 8;
			textBoxNote.Location = new System.Drawing.Point(91, 96);
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(536, 20);
			textBoxNote.TabIndex = 14;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 99);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(203, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
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
			ultraLabel1.Size = new System.Drawing.Size(623, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Total:";
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance4.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance4.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance4;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 461);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(768, 21);
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
			labelBalance.Location = new System.Drawing.Point(625, 2);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxSheetName);
			panelDetails.Controls.Add(textBoxYear);
			panelDetails.Controls.Add(textBoxMonth);
			panelDetails.Controls.Add(comboBoxBankAccount);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxAccountName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(780, 118);
			panelDetails.TabIndex = 0;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(8, 77);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(69, 13);
			label6.TabIndex = 142;
			label6.Text = "Sheet Name:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(213, 54);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(35, 13);
			label5.TabIndex = 141;
			label5.Text = "Year :";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(10, 54);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(40, 13);
			label4.TabIndex = 140;
			label4.Text = "Month:";
			textBoxSheetName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSheetName.ForeColor = System.Drawing.Color.Black;
			textBoxSheetName.Location = new System.Drawing.Point(91, 74);
			textBoxSheetName.Name = "textBoxSheetName";
			textBoxSheetName.ReadOnly = true;
			textBoxSheetName.Size = new System.Drawing.Size(341, 20);
			textBoxSheetName.TabIndex = 139;
			textBoxSheetName.TabStop = false;
			textBoxYear.Location = new System.Drawing.Point(263, 51);
			textBoxYear.Name = "textBoxYear";
			textBoxYear.ReadOnly = true;
			textBoxYear.Size = new System.Drawing.Size(138, 20);
			textBoxYear.TabIndex = 137;
			textBoxYear.TabStop = false;
			textBoxMonth.Location = new System.Drawing.Point(91, 51);
			textBoxMonth.Name = "textBoxMonth";
			textBoxMonth.ReadOnly = true;
			textBoxMonth.Size = new System.Drawing.Size(107, 20);
			textBoxMonth.TabIndex = 135;
			textBoxMonth.TabStop = false;
			comboBoxBankAccount.Assigned = false;
			comboBoxBankAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBankAccount.CustomReportFieldName = "";
			comboBoxBankAccount.CustomReportKey = "";
			comboBoxBankAccount.CustomReportValueType = 1;
			comboBoxBankAccount.DescriptionTextBox = null;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBankAccount.DisplayLayout.Appearance = appearance6;
			comboBoxBankAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBankAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance7.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance7.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance7.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankAccount.DisplayLayout.GroupByBox.Appearance = appearance7;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance8;
			comboBoxBankAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance9.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance9.BackColor2 = System.Drawing.SystemColors.Control;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance9;
			comboBoxBankAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBankAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBankAccount.DisplayLayout.Override.ActiveCellAppearance = appearance10;
			appearance11.BackColor = System.Drawing.SystemColors.Highlight;
			appearance11.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBankAccount.DisplayLayout.Override.ActiveRowAppearance = appearance11;
			comboBoxBankAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBankAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBankAccount.DisplayLayout.Override.CardAreaAppearance = appearance12;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			appearance13.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBankAccount.DisplayLayout.Override.CellAppearance = appearance13;
			comboBoxBankAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBankAccount.DisplayLayout.Override.CellPadding = 0;
			appearance14.BackColor = System.Drawing.SystemColors.Control;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankAccount.DisplayLayout.Override.GroupByRowAppearance = appearance14;
			appearance15.TextHAlignAsString = "Left";
			comboBoxBankAccount.DisplayLayout.Override.HeaderAppearance = appearance15;
			comboBoxBankAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBankAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			appearance16.BorderColor = System.Drawing.Color.Silver;
			comboBoxBankAccount.DisplayLayout.Override.RowAppearance = appearance16;
			comboBoxBankAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBankAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance17;
			comboBoxBankAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBankAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBankAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBankAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBankAccount.Editable = true;
			comboBoxBankAccount.FilterString = "";
			comboBoxBankAccount.HasAllAccount = false;
			comboBoxBankAccount.HasCustom = false;
			comboBoxBankAccount.IsDataLoaded = false;
			comboBoxBankAccount.Location = new System.Drawing.Point(91, 25);
			comboBoxBankAccount.MaxDropDownItems = 12;
			comboBoxBankAccount.Name = "comboBoxBankAccount";
			comboBoxBankAccount.ShowInactiveItems = false;
			comboBoxBankAccount.ShowQuickAdd = true;
			comboBoxBankAccount.Size = new System.Drawing.Size(107, 20);
			comboBoxBankAccount.TabIndex = 6;
			comboBoxBankAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance18.FontData.BoldAsString = "True";
			appearance18.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance18;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(9, 26);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel6.TabIndex = 5;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Account:";
			appearance19.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance19;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(413, 54);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(53, 13);
			mmLabel2.TabIndex = 130;
			mmLabel2.Text = "Amount:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(472, 52);
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
			textBoxAmount.Size = new System.Drawing.Size(107, 20);
			textBoxAmount.TabIndex = 10;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			appearance20.FontData.BoldAsString = "True";
			appearance20.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance20;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(9, 6);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance21.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance21;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance22;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance23.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance23;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance25.BackColor2 = System.Drawing.SystemColors.Control;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance25;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance26;
			appearance27.BackColor = System.Drawing.SystemColors.Highlight;
			appearance27.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance27;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance28;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			appearance29.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance29;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance30.BackColor = System.Drawing.SystemColors.Control;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance30;
			appearance31.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance31;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance32;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance33;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(91, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(107, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxAccountName.Location = new System.Drawing.Point(201, 25);
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(201, 20);
			textBoxAccountName.TabIndex = 7;
			textBoxAccountName.TabStop = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(12, 415);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(766, 44);
			labelVoided.TabIndex = 1;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
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
				toolStripButtonDistribution,
				toolStripButtonCreateWPF,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(792, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonCreateWPF.Enabled = false;
			toolStripButtonCreateWPF.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonCreateWPF.Image");
			toolStripButtonCreateWPF.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonCreateWPF.Name = "toolStripButtonCreateWPF";
			toolStripButtonCreateWPF.Size = new System.Drawing.Size(96, 28);
			toolStripButtonCreateWPF.Text = "Create WPS";
			toolStripButtonCreateWPF.Click += new System.EventHandler(toolStripButtonCreateWPF_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			buttonSelectSalarySheet.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectSalarySheet.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonSelectSalarySheet.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectSalarySheet.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectSalarySheet.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectSalarySheet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectSalarySheet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectSalarySheet.Location = new System.Drawing.Point(657, 153);
			buttonSelectSalarySheet.Name = "buttonSelectSalarySheet";
			buttonSelectSalarySheet.Size = new System.Drawing.Size(122, 24);
			buttonSelectSalarySheet.TabIndex = 1;
			buttonSelectSalarySheet.Text = "Select Salary Sheet...";
			buttonSelectSalarySheet.UseVisualStyleBackColor = false;
			buttonSelectSalarySheet.Click += new System.EventHandler(buttonSelectSalarySheet_Click);
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(483, 490);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(78, 13);
			mmLabel8.TabIndex = 1009;
			mmLabel8.Text = "Other Charges:";
			textBoxAmountOther.AllowDecimal = true;
			textBoxAmountOther.BackColor = System.Drawing.Color.White;
			textBoxAmountOther.CustomReportFieldName = "";
			textBoxAmountOther.CustomReportKey = "";
			textBoxAmountOther.CustomReportValueType = 1;
			textBoxAmountOther.IsComboTextBox = false;
			textBoxAmountOther.IsModified = false;
			textBoxAmountOther.Location = new System.Drawing.Point(682, 485);
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
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(682, 508);
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
			textBoxTaxAmount.TabIndex = 1013;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(483, 513);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(60, 13);
			labelTaxGroup.TabIndex = 1014;
			labelTaxGroup.Text = "Tax Group:";
			textBoxAmountTotal.AllowDecimal = true;
			textBoxAmountTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmountTotal.CustomReportFieldName = "";
			textBoxAmountTotal.CustomReportKey = "";
			textBoxAmountTotal.CustomReportValueType = 1;
			textBoxAmountTotal.IsComboTextBox = false;
			textBoxAmountTotal.IsModified = false;
			textBoxAmountTotal.Location = new System.Drawing.Point(682, 531);
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
			textBoxAmountTotal.TabIndex = 1010;
			textBoxAmountTotal.Text = "0.00";
			textBoxAmountTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmountTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(635, 535);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(34, 13);
			mmLabel9.TabIndex = 1011;
			mmLabel9.Text = "Total:";
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance34;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance35.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance35;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance36;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance37.BackColor2 = System.Drawing.SystemColors.Control;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance37;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance38;
			appearance39.BackColor = System.Drawing.SystemColors.Highlight;
			appearance39.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance39;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance40;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			appearance41.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance41;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance42.BackColor = System.Drawing.SystemColors.Control;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance42;
			appearance43.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance43;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance44;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance45.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance45;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(567, 508);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(100, 20);
			comboBoxTaxGroup.TabIndex = 5;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAccount.Assigned = false;
			comboBoxAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = null;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance46;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance47.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance47;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance48;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance49.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance49.BackColor2 = System.Drawing.SystemColors.Control;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance49;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance50;
			appearance51.BackColor = System.Drawing.SystemColors.Highlight;
			appearance51.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance51;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance52;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			appearance53.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance53;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance54.BackColor = System.Drawing.SystemColors.Control;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance54;
			appearance55.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance55;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance56;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance57;
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
			comboBoxAccount.Location = new System.Drawing.Point(567, 485);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(100, 20);
			comboBoxAccount.TabIndex = 2;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			dataGriditem.AllowAddNew = false;
			dataGriditem.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGriditem.DisplayLayout.Appearance = appearance58;
			dataGriditem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGriditem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance59.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			dataGriditem.DisplayLayout.GroupByBox.Appearance = appearance59;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGriditem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance60;
			dataGriditem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance61.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance61.BackColor2 = System.Drawing.SystemColors.Control;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGriditem.DisplayLayout.GroupByBox.PromptAppearance = appearance61;
			dataGriditem.DisplayLayout.MaxColScrollRegions = 1;
			dataGriditem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGriditem.DisplayLayout.Override.ActiveCellAppearance = appearance62;
			appearance63.BackColor = System.Drawing.SystemColors.Highlight;
			appearance63.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGriditem.DisplayLayout.Override.ActiveRowAppearance = appearance63;
			dataGriditem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGriditem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGriditem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			dataGriditem.DisplayLayout.Override.CardAreaAppearance = appearance64;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGriditem.DisplayLayout.Override.CellAppearance = appearance65;
			dataGriditem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGriditem.DisplayLayout.Override.CellPadding = 0;
			appearance66.BackColor = System.Drawing.SystemColors.Control;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			dataGriditem.DisplayLayout.Override.GroupByRowAppearance = appearance66;
			appearance67.TextHAlignAsString = "Left";
			dataGriditem.DisplayLayout.Override.HeaderAppearance = appearance67;
			dataGriditem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGriditem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			dataGriditem.DisplayLayout.Override.RowAppearance = appearance68;
			dataGriditem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGriditem.DisplayLayout.Override.TemplateAddRowAppearance = appearance69;
			dataGriditem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGriditem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGriditem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGriditem.ExitEditModeOnLeave = false;
			dataGriditem.IncludeLotItems = false;
			dataGriditem.LoadLayoutFailed = false;
			dataGriditem.Location = new System.Drawing.Point(11, 182);
			dataGriditem.Name = "dataGriditem";
			dataGriditem.ShowClearMenu = true;
			dataGriditem.ShowDeleteMenu = true;
			dataGriditem.ShowInsertMenu = true;
			dataGriditem.ShowMoveRowsMenu = true;
			dataGriditem.Size = new System.Drawing.Size(768, 277);
			dataGriditem.TabIndex = 19;
			dataGriditem.Text = "dataEntryGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(792, 592);
			base.Controls.Add(textBoxTaxAmount);
			base.Controls.Add(comboBoxTaxGroup);
			base.Controls.Add(labelTaxGroup);
			base.Controls.Add(textBoxAmountTotal);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(comboBoxAccount);
			base.Controls.Add(textBoxAmountOther);
			base.Controls.Add(buttonSelectSalarySheet);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dataGriditem);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "TransferSalaryPaymentForm";
			Text = "Bank Transfer Salary Payment";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBankAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGriditem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
