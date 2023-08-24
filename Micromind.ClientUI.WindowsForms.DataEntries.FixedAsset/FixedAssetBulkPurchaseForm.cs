using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset
{
	public class FixedAssetBulkPurchaseForm : Form, IForm
	{
		private FixedAssetBulkPurchaseData currentData;

		private FixedAssetData currentFixedAssetData;

		private const string TABLENAME_CONST = "FixedAsset_BulkPurchase";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private IContainer components;

		private ToolStrip toolStrip1;

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

		private DataEntryGrid dataGridItems;

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private CurrencySelector currencySelector;

		private UltraFormattedLinkLabel labelCurrency;

		private AnalysisComboBox comboBoxGridanalysis;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraLabel ultraLabel1;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelBalance;

		private CostCenterComboBox comboBoxGridCostCenter;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private PayeeSelector payeeSelector1;

		private TextBox textBoxPayeeName;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxAmount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private FixedAssetsComboBox comboBoxGridFixedAsset;

		private ToolStripButton toolStripButtonInformation;

		private NumericUpDown numericUpQuantity;

		private Label label4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxAssetClassName;

		private FixedAssetClassComboBox comboBoxClass;

		private Button buttonFillDetails;

		private Label label2;

		private AmountTextBox textBoxitemAmount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private MMTextBox textBoxAssetLocationName;

		private FixedAssetLocationComboBox comboBoxLocation;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator3;

		public ScreenAreas ScreenArea => ScreenAreas.FixedAsset;

		public int ScreenID => 1018;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					buttonVoid.Enabled = !isVoid;
				}
				comboBoxSysDoc.Enabled = value;
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
					if (value)
					{
						buttonVoid.Enabled = false;
						return;
					}
					buttonVoid.Text = UIMessages.Void;
					buttonVoid.Enabled = true;
				}
			}
		}

		public FixedAssetBulkPurchaseForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxGridFixedAsset.SelectedIndexChanged += comboBoxGridAccount_SelectedIndexChanged;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			base.KeyDown += Form_KeyDown;
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				textBoxAmount.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
			}
			_ = (e.Cell.Column.Key == "Asset");
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
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && sender.GetType() == typeof(AllAccountsComboBox) && dataGridItems.ActiveRow.Cells["Analysis"].Text != "" && !comboBoxGridanalysis.IsValidAccountAnalysis(comboBoxGridFixedAsset.SelectedID, dataGridItems.ActiveRow.Cells["Analysis"].Text))
			{
				dataGridItems.ActiveRow.Cells["Analysis"].Value = "";
			}
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Asset")
			{
				if (dataGridItems.ActiveRow.Cells["Asset"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Asset Name"].Value = "";
				}
			}
			else if (dataGridItems.ActiveCell.Column.Key == "Amount" && dataGridItems.ActiveCell.Text != "")
			{
				dataGridItems.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridItems.ActiveCell.Text, NumberStyles.Any), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			if (activeRow.Cells["Asset"].Value.ToString() == "" && activeRow.Cells["Description"].Value.ToString() != "")
			{
				ErrorHelper.InformationMessage(UIMessages.SelectAccount);
				e.Cancel = true;
				activeRow.Cells["Asset"].Activate();
				return;
			}
			if (activeRow.Cells["Amount"].Value.ToString() == "")
			{
				activeRow.Cells["Amount"].Value = 0;
			}
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Asset")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridFixedAsset.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridFixedAsset.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "C.C.")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridCostCenter.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridCostCenter.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Analysis")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.AnalysisNotAdded);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new FixedAssetBulkPurchaseData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.FixedAssetBulkPurchaseTable.Rows[0] : currentData.FixedAssetBulkPurchaseTable.NewRow();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Quantity"] = numericUpQuantity.Value;
				dataRow["ItemAmount"] = textBoxitemAmount.Value;
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["AssetClassID"] = comboBoxClass.SelectedID;
				string selectedType = payeeSelector1.SelectedType;
				if (selectedType != "")
				{
					dataRow["PayeeType"] = selectedType;
				}
				else
				{
					dataRow["PayeeType"] = DBNull.Value;
				}
				if (payeeSelector1.SelectedID != "")
				{
					dataRow["PayeeID"] = payeeSelector1.SelectedID;
				}
				else
				{
					dataRow["PayeeID"] = DBNull.Value;
				}
				if (currencySelector.SelectedID != "" && currencySelector.SelectedID != Global.BaseCurrencyID)
				{
					dataRow["CurrencyID"] = currencySelector.SelectedID;
					dataRow["CurrencyRate"] = currencySelector.Rate;
					dataRow["AmountFC"] = GetTransactionBalance();
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = 1;
					dataRow["Amount"] = GetTransactionBalance();
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.FixedAssetBulkPurchaseTable.Rows.Add(dataRow);
				}
				currentData.FixedAssetBulkPurchaseDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.FixedAssetBulkPurchaseDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["AssetID"] = row.Cells["Asset"].Value.ToString();
					if (currencySelector.SelectedID == "" || currencySelector.IsBaseCurrency)
					{
						if (row.Cells["Amount"].Value.ToString() != "")
						{
							dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
						}
						else
						{
							dataRow2["Amount"] = DBNull.Value;
						}
						dataRow2["AmountFC"] = DBNull.Value;
					}
					else if (row.Cells["Amount"].Value.ToString() != "")
					{
						dataRow2["AmountFC"] = row.Cells["Amount"].Value.ToString();
					}
					else
					{
						dataRow2["AmountFC"] = DBNull.Value;
					}
					dataRow2["AssetName"] = row.Cells["Asset Name"].Value.ToString();
					dataRow2["SerialNumber"] = row.Cells["Serial Number:"].Value.ToString();
					dataRow2["BarcodeNumber"] = row.Cells["Bar Code:"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.FixedAssetBulkPurchaseDetailTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetFixedAssetData()
		{
			try
			{
				if (currentFixedAssetData == null || isNewRecord)
				{
					currentFixedAssetData = new FixedAssetData();
				}
				DataSet dataSet = null;
				dataSet = Factory.FixedAssetSystem.GetAssetByClassID(comboBoxClass.SelectedID);
				if (dataSet.Tables[0].Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("Please create an Asset entry with selected Class");
					return false;
				}
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentFixedAssetData.AssetTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["AssetID"] = row.Cells["Asset"].Value.ToString();
					dataRow2["AssetName"] = row.Cells["Asset Name"].Value.ToString();
					dataRow2["BarcodeNumber"] = row.Cells["Bar Code:"].Value.ToString();
					dataRow2["SerialNumber"] = row.Cells["Serial Number:"].Value.ToString();
					dataRow2["AssetClassID"] = comboBoxClass.SelectedID;
					dataRow2["AssetGroupID"] = dataRow["AssetGroupID"].ToString();
					dataRow2["AssetLocationID"] = comboBoxLocation.SelectedID;
					dataRow2["DepartmentID"] = dataRow["DepartmentID"].ToString();
					dataRow2["DivisionID"] = dataRow["DivisionID"].ToString();
					dataRow2["DepStartDate"] = dateTimePickerDate.Value;
					dataRow2["InvoiceNumber"] = textBoxVoucherNumber.Text;
					dataRow2["PurchaseDate"] = dateTimePickerDate.Value;
					dataRow2.EndEdit();
					currentFixedAssetData.AssetTable.Rows.Add(dataRow2);
				}
				textBoxAmount.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
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
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Asset");
				dataTable.Columns.Add("Asset Name");
				dataTable.Columns.Add("Serial Number:");
				dataTable.Columns.Add("Bar Code:");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Asset Name"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(7 * dataGridItems.Width) / 100;
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
					currentData = Factory.FixedAssetBulkPurchaseSystem.GetFixedAssetPurchaseByID(SystemDocID, voucherID);
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
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables["FixedAsset_BulkPurchase"].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["FixedAsset_BulkPurchase"].Rows[0];
				dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				textBoxRef1.Text = dataRow["Reference"].ToString();
				comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
				payeeSelector1.SelectedType = dataRow["PayeeType"].ToString();
				payeeSelector1.SelectedID = dataRow["PayeeID"].ToString();
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
				comboBoxClass.SelectedID = dataRow["AssetClassID"].ToString();
				numericUpQuantity.Value = decimal.Parse(dataRow["Quantity"].ToString());
				textBoxitemAmount.Value = decimal.Parse(dataRow["ItemAmount"].ToString());
				if (dataRow["CurrencyID"] != DBNull.Value)
				{
					currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
					currencySelector.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
				}
				else
				{
					currencySelector.SelectedID = "";
					currencySelector.Rate = 1m;
				}
				if (dataRow["IsVoid"] != DBNull.Value)
				{
					IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
				}
				else
				{
					IsVoid = false;
				}
				textBoxNote.Text = dataRow["Note"].ToString();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData.Tables.Contains("FixedAsset_BulkPurchase_Detail") && currentData.FixedAssetBulkPurchaseDetailTable.Rows.Count != 0)
				{
					foreach (DataRow row in currentData.Tables["FixedAsset_BulkPurchase_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Asset"] = row["AssetID"];
						dataRow3["Asset Name"] = row["AssetName"];
						dataRow3["Description"] = row["Description"];
						dataRow3["Serial Number:"] = row["SerialNumber"];
						dataRow3["Asset Name"] = row["AssetName"];
						dataRow3["Bar Code:"] = row["BarcodeNumber"];
						if (row["AmountFC"] != DBNull.Value)
						{
							dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
						}
						else if (row["Amount"] != DBNull.Value)
						{
							dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
						}
						else
						{
							dataRow3["Amount"] = DBNull.Value;
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
					textBoxAmount.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
					labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				}
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
			if (!GetFixedAssetData())
			{
				return false;
			}
			try
			{
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.FixedAssetSystem.CreateAsset(currentFixedAssetData);
					flag &= Factory.FixedAssetBulkPurchaseSystem.CreateFixedAssetPurchase(currentData, isUpdate: false);
				}
				else
				{
					flag = Factory.FixedAssetBulkPurchaseSystem.CreateFixedAssetPurchase(currentData, isUpdate: true);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else if (clearAfter)
				{
					ClearForm();
					IsNewRecord = true;
				}
				else
				{
					formManager.ResetDirty();
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
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (dataGridItems.HasRowAnyValue(row) && row.Cells["Asset"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage(UIMessages.SelectAccount);
					row.Activate();
					return false;
				}
			}
			if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (payeeSelector1.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				payeeSelector1.Focus();
				return false;
			}
			if (GetTransactionBalance() == 0m && ErrorHelper.QuestionMessageYesNo(UIMessages.TransactionWithZeroAmount) == DialogResult.No)
			{
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("FixedAsset_BulkPurchase", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (row.Cells["Amount"].Value != null && row.Cells["Amount"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Amount"].Value.ToString());
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
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				payeeSelector1.Clear();
				textBoxPayeeName.Clear();
				comboBoxLocation.Clear();
				comboBoxClass.Clear();
				numericUpQuantity.Value = 0m;
				textBoxitemAmount.Clear();
				string text3 = labelBalance.Text = (textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat));
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
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
				DataSet fixedAssetPurchaseByID = Factory.FixedAssetBulkPurchaseSystem.GetFixedAssetPurchaseByID(SystemDocID, textBoxVoucherNumber.Text);
				return Factory.FixedAssetBulkPurchaseSystem.DeleteFixedAssetPurchase(SystemDocID, textBoxVoucherNumber.Text, fixedAssetPurchaseByID.Tables[1]);
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
			string nextID = DatabaseHelper.GetNextID("FixedAsset_BulkPurchase", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("FixedAsset_BulkPurchase", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("FixedAsset_BulkPurchase", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("FixedAsset_BulkPurchase", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("FixedAsset_BulkPurchase", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
				bool visible = currencySelector.Visible = CompanyPreferences.UseMultiCurrency;
				ultraFormattedLinkLabel.Visible = visible;
				SetSecurity();
				if (!base.IsDisposed)
				{
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.FixedAssetBulkPurchase);
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(currencySelector.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
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
				return Factory.FixedAssetBulkPurchaseSystem.VoidFixedAssetPurchase(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.FixedAssetBulkPurchase);
		}

		private void payeeSelector1_SelectedItemChanged(object sender, EventArgs e)
		{
			textBoxPayeeName.Text = payeeSelector1.SelectedName;
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			string selectedType = payeeSelector1.SelectedType;
			if (!(selectedType == ""))
			{
				FormHelper formHelper = new FormHelper();
				if (selectedType == "A")
				{
					formHelper.EditAccount(payeeSelector1.SelectedID);
				}
				else if (selectedType == "C")
				{
					formHelper.EditCustomer(payeeSelector1.SelectedID);
				}
				else if (selectedType == "V")
				{
					formHelper.EditVendor(payeeSelector1.SelectedID);
				}
				else if (selectedType == "E")
				{
					formHelper.EditEmployee(payeeSelector1.SelectedID);
				}
			}
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
				if (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string systemDocID = SystemDocID;
					string text = textBoxVoucherNumber.Text;
					DataSet fixedAssetPurchaseToPrint = Factory.FixedAssetBulkPurchaseSystem.GetFixedAssetPurchaseToPrint(systemDocID, text);
					if (fixedAssetPurchaseToPrint == null || fixedAssetPurchaseToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(fixedAssetPurchaseToPrint, systemDocID, "Credit Note", SysDocTypes.FixedAssetPurchase, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			if (payeeSelector1.SelectedType == "C")
			{
				formHelper.EditCustomer(payeeSelector1.SelectedID);
			}
			else if (payeeSelector1.SelectedType == "V")
			{
				formHelper.EditVendor(payeeSelector1.SelectedID);
			}
			else if (payeeSelector1.SelectedType == "A")
			{
				formHelper.EditAccount(payeeSelector1.SelectedID);
			}
			else if (payeeSelector1.SelectedType == "E")
			{
				formHelper.EditEmployee(payeeSelector1.SelectedID);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAssetClass(comboBoxClass.SelectedID);
		}

		private void buttonFillDetails_Click(object sender, EventArgs e)
		{
			FillBulkEntry();
		}

		private void FillBulkEntry()
		{
			dataGridItems.DisplayLayout.Bands[0].AddNew().Cells[0].Value = dateTimePickerDate.Value;
			_ = dataGridItems.Rows[0];
			if (comboBoxClass.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select Item Class.");
				return;
			}
			if (comboBoxLocation.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select Location.");
				return;
			}
			if (numericUpQuantity.Value == 0m)
			{
				ErrorHelper.InformationMessage("Please select No of Quantity.");
				return;
			}
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			string nextCardNumber = Factory.SystemDocumentSystem.GetNextCardNumber("FixedAsset", "AssetID", "", "");
			checked
			{
				for (int i = 1; (decimal)i <= numericUpQuantity.Value; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["Asset"] = nextCardNumber + "-" + i;
					dataRow["Asset Name"] = nextCardNumber + "-" + comboBoxClass.SelectedID + "-" + i;
					int num = 7845 + i;
					dataRow["Serial Number:"] = ("4234-3423-5343-" + num).ToString();
					dataRow["Bar Code:"] = ("576-4718-5571-" + num).ToString();
					dataRow["Description"] = textBoxAssetClassName.Text;
					dataRow["Amount"] = textBoxitemAmount.Value;
					dataRow.EndEdit();
					dataTable.Rows.Add(dataRow);
				}
				dataTable.AcceptChanges();
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAssetLocation(comboBoxLocation.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FixedAssetBulkPurchaseListFormObj);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset.FixedAssetBulkPurchaseForm));
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
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
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAssetLocationName = new Micromind.UISupport.MMTextBox();
			comboBoxLocation = new Micromind.DataControls.FixedAssetLocationComboBox();
			label2 = new System.Windows.Forms.Label();
			textBoxitemAmount = new Micromind.UISupport.AmountTextBox();
			buttonFillDetails = new System.Windows.Forms.Button();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAssetClassName = new Micromind.UISupport.MMTextBox();
			comboBoxClass = new Micromind.DataControls.FixedAssetClassComboBox();
			numericUpQuantity = new System.Windows.Forms.NumericUpDown();
			label4 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			textBoxPayeeName = new System.Windows.Forms.TextBox();
			payeeSelector1 = new Micromind.DataControls.PayeeSelector();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			labelVoided = new System.Windows.Forms.Label();
			comboBoxGridFixedAsset = new Micromind.DataControls.FixedAssetsComboBox();
			comboBoxGridCostCenter = new Micromind.DataControls.CostCenterComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridanalysis = new Micromind.DataControls.AnalysisComboBox();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpQuantity).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridFixedAsset).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridanalysis).BeginInit();
			SuspendLayout();
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
				toolStripButtonAttach,
				toolStripSeparator3,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(882, 31);
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 507);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(882, 40);
			panelButtons.TabIndex = 2;
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
			linePanelDown.Size = new System.Drawing.Size(882, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(772, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(98, 25);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(129, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(316, 3);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(129, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(234, 28);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(17, 28);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			textBoxRef1.Location = new System.Drawing.Point(316, 25);
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(162, 20);
			textBoxRef1.TabIndex = 3;
			textBoxNote.Location = new System.Drawing.Point(98, 146);
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(542, 20);
			textBoxNote.TabIndex = 12;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(23, 151);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(234, 72);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(49, 14);
			labelCurrency.TabIndex = 4;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance2;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(235, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance5.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			appearance5.TextHAlignAsString = "Right";
			appearance5.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance5;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(713, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Total:";
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance6.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance6.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance6;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 475);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(858, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelBalance.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance7.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			appearance7.TextHAlignAsString = "Right";
			appearance7.TextVAlignAsString = "Middle";
			labelBalance.Appearance = appearance7;
			labelBalance.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelBalance.Location = new System.Drawing.Point(715, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(textBoxAssetLocationName);
			panelDetails.Controls.Add(comboBoxLocation);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxitemAmount);
			panelDetails.Controls.Add(buttonFillDetails);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(textBoxAssetClassName);
			panelDetails.Controls.Add(comboBoxClass);
			panelDetails.Controls.Add(numericUpQuantity);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(textBoxPayeeName);
			panelDetails.Controls.Add(payeeSelector1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(currencySelector);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(867, 175);
			panelDetails.TabIndex = 0;
			appearance8.FontData.BoldAsString = "True";
			appearance8.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance8;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(6, 123);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(89, 15);
			ultraFormattedLinkLabel4.TabIndex = 387;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Asset Location:";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance9;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked_1);
			textBoxAssetLocationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetLocationName.CustomReportFieldName = "";
			textBoxAssetLocationName.CustomReportKey = "";
			textBoxAssetLocationName.CustomReportValueType = 1;
			textBoxAssetLocationName.IsComboTextBox = true;
			textBoxAssetLocationName.IsModified = false;
			textBoxAssetLocationName.Location = new System.Drawing.Point(215, 119);
			textBoxAssetLocationName.MaxLength = 64;
			textBoxAssetLocationName.Name = "textBoxAssetLocationName";
			textBoxAssetLocationName.ReadOnly = true;
			textBoxAssetLocationName.Size = new System.Drawing.Size(125, 20);
			textBoxAssetLocationName.TabIndex = 386;
			textBoxAssetLocationName.TabStop = false;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = textBoxAssetLocationName;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			appearance10.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance10;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance11.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance11;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance12;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance13.BackColor2 = System.Drawing.SystemColors.Control;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance13;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance14;
			appearance15.BackColor = System.Drawing.SystemColors.Highlight;
			appearance15.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance15;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance16;
			appearance17.BorderColor = System.Drawing.Color.Silver;
			appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance17;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance18.BackColor = System.Drawing.SystemColors.Control;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance18;
			appearance19.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance19;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance20;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance21;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(98, 120);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowInactiveItems = true;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.Size = new System.Drawing.Size(110, 20);
			comboBoxLocation.TabIndex = 9;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(366, 125);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(69, 13);
			label2.TabIndex = 384;
			label2.Text = "Item Amount:";
			textBoxitemAmount.AllowDecimal = true;
			textBoxitemAmount.CustomReportFieldName = "";
			textBoxitemAmount.CustomReportKey = "";
			textBoxitemAmount.CustomReportValueType = 1;
			textBoxitemAmount.IsComboTextBox = false;
			textBoxitemAmount.IsModified = false;
			textBoxitemAmount.Location = new System.Drawing.Point(441, 121);
			textBoxitemAmount.MaxLength = 15;
			textBoxitemAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxitemAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxitemAmount.Name = "textBoxitemAmount";
			textBoxitemAmount.NullText = "0";
			textBoxitemAmount.Size = new System.Drawing.Size(114, 20);
			textBoxitemAmount.TabIndex = 10;
			textBoxitemAmount.Text = "0.00";
			textBoxitemAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxitemAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			buttonFillDetails.Location = new System.Drawing.Point(668, 144);
			buttonFillDetails.Name = "buttonFillDetails";
			buttonFillDetails.Size = new System.Drawing.Size(99, 23);
			buttonFillDetails.TabIndex = 13;
			buttonFillDetails.Text = "Fill Details";
			buttonFillDetails.UseVisualStyleBackColor = true;
			buttonFillDetails.Click += new System.EventHandler(buttonFillDetails_Click);
			appearance22.FontData.BoldAsString = "True";
			appearance22.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance22;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 96);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(70, 15);
			ultraFormattedLinkLabel1.TabIndex = 138;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Asset Class:";
			appearance23.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance23;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			textBoxAssetClassName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetClassName.CustomReportFieldName = "";
			textBoxAssetClassName.CustomReportKey = "";
			textBoxAssetClassName.CustomReportValueType = 1;
			textBoxAssetClassName.IsComboTextBox = true;
			textBoxAssetClassName.IsModified = false;
			textBoxAssetClassName.Location = new System.Drawing.Point(214, 95);
			textBoxAssetClassName.MaxLength = 64;
			textBoxAssetClassName.Name = "textBoxAssetClassName";
			textBoxAssetClassName.ReadOnly = true;
			textBoxAssetClassName.Size = new System.Drawing.Size(267, 20);
			textBoxAssetClassName.TabIndex = 137;
			textBoxAssetClassName.TabStop = false;
			comboBoxClass.Assigned = false;
			comboBoxClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxClass.CustomReportFieldName = "";
			comboBoxClass.CustomReportKey = "";
			comboBoxClass.CustomReportValueType = 1;
			comboBoxClass.DescriptionTextBox = textBoxAssetClassName;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxClass.DisplayLayout.Appearance = appearance24;
			comboBoxClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.GroupByBox.Appearance = appearance25;
			appearance26.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance26;
			comboBoxClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance27.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance27.BackColor2 = System.Drawing.SystemColors.Control;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxClass.DisplayLayout.GroupByBox.PromptAppearance = appearance27;
			comboBoxClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxClass.DisplayLayout.Override.ActiveCellAppearance = appearance28;
			appearance29.BackColor = System.Drawing.SystemColors.Highlight;
			appearance29.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxClass.DisplayLayout.Override.ActiveRowAppearance = appearance29;
			comboBoxClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.Override.CardAreaAppearance = appearance30;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			appearance31.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxClass.DisplayLayout.Override.CellAppearance = appearance31;
			comboBoxClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxClass.DisplayLayout.Override.CellPadding = 0;
			appearance32.BackColor = System.Drawing.SystemColors.Control;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.Override.GroupByRowAppearance = appearance32;
			appearance33.TextHAlignAsString = "Left";
			comboBoxClass.DisplayLayout.Override.HeaderAppearance = appearance33;
			comboBoxClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			comboBoxClass.DisplayLayout.Override.RowAppearance = appearance34;
			comboBoxClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance35;
			comboBoxClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxClass.Editable = true;
			comboBoxClass.FilterString = "";
			comboBoxClass.HasAllAccount = false;
			comboBoxClass.HasCustom = false;
			comboBoxClass.IsDataLoaded = false;
			comboBoxClass.Location = new System.Drawing.Point(98, 94);
			comboBoxClass.MaxDropDownItems = 12;
			comboBoxClass.Name = "comboBoxClass";
			comboBoxClass.ShowInactiveItems = true;
			comboBoxClass.ShowQuickAdd = true;
			comboBoxClass.Size = new System.Drawing.Size(110, 20);
			comboBoxClass.TabIndex = 8;
			comboBoxClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			numericUpQuantity.Location = new System.Drawing.Point(625, 120);
			numericUpQuantity.Maximum = new decimal(new int[4]
			{
				1000,
				0,
				0,
				0
			});
			numericUpQuantity.Name = "numericUpQuantity";
			numericUpQuantity.Size = new System.Drawing.Size(56, 20);
			numericUpQuantity.TabIndex = 11;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(570, 125);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(49, 13);
			label4.TabIndex = 134;
			label4.Text = "Quantity:";
			appearance36.FontData.BoldAsString = "True";
			appearance36.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance36;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(18, 49);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(58, 15);
			ultraFormattedLinkLabel3.TabIndex = 131;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Credit To:";
			appearance37.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance37;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(17, 71);
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
			textBoxAmount.Location = new System.Drawing.Point(98, 69);
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
			textBoxAmount.Size = new System.Drawing.Size(129, 20);
			textBoxAmount.TabIndex = 6;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxPayeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeName.Location = new System.Drawing.Point(316, 47);
			textBoxPayeeName.Name = "textBoxPayeeName";
			textBoxPayeeName.ReadOnly = true;
			textBoxPayeeName.Size = new System.Drawing.Size(328, 20);
			textBoxPayeeName.TabIndex = 5;
			textBoxPayeeName.TabStop = false;
			payeeSelector1.BackColor = System.Drawing.Color.Transparent;
			payeeSelector1.Location = new System.Drawing.Point(98, 47);
			payeeSelector1.MaximumSize = new System.Drawing.Size(2000, 20);
			payeeSelector1.MinimumSize = new System.Drawing.Size(0, 20);
			payeeSelector1.Name = "payeeSelector1";
			payeeSelector1.SelectedID = "";
			payeeSelector1.SelectedType = "";
			payeeSelector1.Size = new System.Drawing.Size(202, 20);
			payeeSelector1.TabIndex = 4;
			payeeSelector1.SelectedItemChanged += new System.EventHandler(payeeSelector1_SelectedItemChanged);
			appearance38.FontData.BoldAsString = "True";
			appearance38.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance38;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(18, 6);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 118;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance40;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance41;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance43.BackColor2 = System.Drawing.SystemColors.Control;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance44;
			appearance45.BackColor = System.Drawing.SystemColors.Highlight;
			appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance45;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance46;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance47;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance48.BackColor = System.Drawing.SystemColors.Control;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance48;
			appearance49.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance49;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance50;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(98, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(129, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(316, 69);
			currencySelector.MaximumSize = new System.Drawing.Size(99999, 20);
			currencySelector.MinimumSize = new System.Drawing.Size(5, 20);
			currencySelector.Name = "currencySelector";
			currencySelector.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			currencySelector.SelectedID = "";
			currencySelector.Size = new System.Drawing.Size(162, 20);
			currencySelector.TabIndex = 7;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 438);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(854, 39);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			comboBoxGridFixedAsset.Assigned = false;
			comboBoxGridFixedAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridFixedAsset.CustomReportFieldName = "";
			comboBoxGridFixedAsset.CustomReportKey = "";
			comboBoxGridFixedAsset.CustomReportValueType = 1;
			comboBoxGridFixedAsset.DescriptionTextBox = null;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridFixedAsset.DisplayLayout.Appearance = appearance52;
			comboBoxGridFixedAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridFixedAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance53.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.Appearance = appearance53;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance54;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance55.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance55.BackColor2 = System.Drawing.SystemColors.Control;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance55;
			comboBoxGridFixedAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridFixedAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridFixedAsset.DisplayLayout.Override.ActiveCellAppearance = appearance56;
			appearance57.BackColor = System.Drawing.SystemColors.Highlight;
			appearance57.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridFixedAsset.DisplayLayout.Override.ActiveRowAppearance = appearance57;
			comboBoxGridFixedAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridFixedAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridFixedAsset.DisplayLayout.Override.CardAreaAppearance = appearance58;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			appearance59.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridFixedAsset.DisplayLayout.Override.CellAppearance = appearance59;
			comboBoxGridFixedAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridFixedAsset.DisplayLayout.Override.CellPadding = 0;
			appearance60.BackColor = System.Drawing.SystemColors.Control;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridFixedAsset.DisplayLayout.Override.GroupByRowAppearance = appearance60;
			appearance61.TextHAlignAsString = "Left";
			comboBoxGridFixedAsset.DisplayLayout.Override.HeaderAppearance = appearance61;
			comboBoxGridFixedAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridFixedAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridFixedAsset.DisplayLayout.Override.RowAppearance = appearance62;
			comboBoxGridFixedAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance63.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridFixedAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance63;
			comboBoxGridFixedAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridFixedAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridFixedAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridFixedAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridFixedAsset.Editable = true;
			comboBoxGridFixedAsset.FilterString = "";
			comboBoxGridFixedAsset.HasAllAccount = false;
			comboBoxGridFixedAsset.HasCustom = false;
			comboBoxGridFixedAsset.IsDataLoaded = false;
			comboBoxGridFixedAsset.Location = new System.Drawing.Point(504, 236);
			comboBoxGridFixedAsset.MaxDropDownItems = 12;
			comboBoxGridFixedAsset.Name = "comboBoxGridFixedAsset";
			comboBoxGridFixedAsset.ShowInactiveItems = true;
			comboBoxGridFixedAsset.ShowQuickAdd = true;
			comboBoxGridFixedAsset.Size = new System.Drawing.Size(140, 20);
			comboBoxGridFixedAsset.TabIndex = 118;
			comboBoxGridFixedAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridFixedAsset.Visible = false;
			comboBoxGridCostCenter.Assigned = false;
			comboBoxGridCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCostCenter.CustomReportFieldName = "";
			comboBoxGridCostCenter.CustomReportKey = "";
			comboBoxGridCostCenter.CustomReportValueType = 1;
			comboBoxGridCostCenter.DescriptionTextBox = null;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCostCenter.DisplayLayout.Appearance = appearance64;
			comboBoxGridCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance65.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.Appearance = appearance65;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance66;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance67.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance67.BackColor2 = System.Drawing.SystemColors.Control;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance67;
			comboBoxGridCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance68;
			appearance69.BackColor = System.Drawing.SystemColors.Highlight;
			appearance69.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance69;
			comboBoxGridCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance70;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			appearance71.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCostCenter.DisplayLayout.Override.CellAppearance = appearance71;
			comboBoxGridCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance72.BackColor = System.Drawing.SystemColors.Control;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance72;
			appearance73.TextHAlignAsString = "Left";
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderAppearance = appearance73;
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCostCenter.DisplayLayout.Override.RowAppearance = appearance74;
			comboBoxGridCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance75.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance75;
			comboBoxGridCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCostCenter.Editable = true;
			comboBoxGridCostCenter.FilterString = "";
			comboBoxGridCostCenter.HasAllAccount = false;
			comboBoxGridCostCenter.HasCustom = false;
			comboBoxGridCostCenter.IsDataLoaded = false;
			comboBoxGridCostCenter.Location = new System.Drawing.Point(661, 31);
			comboBoxGridCostCenter.MaxDropDownItems = 12;
			comboBoxGridCostCenter.Name = "comboBoxGridCostCenter";
			comboBoxGridCostCenter.ShowInactiveItems = false;
			comboBoxGridCostCenter.ShowQuickAdd = true;
			comboBoxGridCostCenter.Size = new System.Drawing.Size(43, 20);
			comboBoxGridCostCenter.TabIndex = 115;
			comboBoxGridCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCostCenter.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance76;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance77.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance77;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance78;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance79.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance79.BackColor2 = System.Drawing.SystemColors.Control;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance79;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance80;
			appearance81.BackColor = System.Drawing.SystemColors.Highlight;
			appearance81.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance81;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance82;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			appearance83.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance83;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance84.BackColor = System.Drawing.SystemColors.Control;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance84;
			appearance85.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance85;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance86;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance87.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance87;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 212);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(856, 267);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
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
			comboBoxGridanalysis.Assigned = false;
			comboBoxGridanalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridanalysis.CustomReportFieldName = "";
			comboBoxGridanalysis.CustomReportKey = "";
			comboBoxGridanalysis.CustomReportValueType = 1;
			comboBoxGridanalysis.DescriptionTextBox = null;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			appearance88.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridanalysis.DisplayLayout.Appearance = appearance88;
			comboBoxGridanalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridanalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance89.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.Appearance = appearance89;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance90;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance91.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance91.BackColor2 = System.Drawing.SystemColors.Control;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance91;
			comboBoxGridanalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridanalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridanalysis.DisplayLayout.Override.ActiveCellAppearance = appearance92;
			appearance93.BackColor = System.Drawing.SystemColors.Highlight;
			appearance93.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridanalysis.DisplayLayout.Override.ActiveRowAppearance = appearance93;
			comboBoxGridanalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridanalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.Override.CardAreaAppearance = appearance94;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			appearance95.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridanalysis.DisplayLayout.Override.CellAppearance = appearance95;
			comboBoxGridanalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridanalysis.DisplayLayout.Override.CellPadding = 0;
			appearance96.BackColor = System.Drawing.SystemColors.Control;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.Override.GroupByRowAppearance = appearance96;
			appearance97.TextHAlignAsString = "Left";
			comboBoxGridanalysis.DisplayLayout.Override.HeaderAppearance = appearance97;
			comboBoxGridanalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridanalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridanalysis.DisplayLayout.Override.RowAppearance = appearance98;
			comboBoxGridanalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridanalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance99;
			comboBoxGridanalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridanalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridanalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridanalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridanalysis.Editable = true;
			comboBoxGridanalysis.FilterString = "";
			comboBoxGridanalysis.HasAllAccount = false;
			comboBoxGridanalysis.HasCustom = false;
			comboBoxGridanalysis.IsDataLoaded = false;
			comboBoxGridanalysis.Location = new System.Drawing.Point(661, 31);
			comboBoxGridanalysis.MaxDropDownItems = 12;
			comboBoxGridanalysis.Name = "comboBoxGridanalysis";
			comboBoxGridanalysis.ShowInactiveItems = false;
			comboBoxGridanalysis.ShowQuickAdd = true;
			comboBoxGridanalysis.Size = new System.Drawing.Size(81, 20);
			comboBoxGridanalysis.TabIndex = 109;
			comboBoxGridanalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridanalysis.Visible = false;
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(882, 547);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(comboBoxGridCostCenter);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxGridanalysis);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(comboBoxGridFixedAsset);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "FixedAssetBulkPurchaseForm";
			Text = "Fixed Asset Bulk Acquisition";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxClass).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpQuantity).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridFixedAsset).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridanalysis).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
