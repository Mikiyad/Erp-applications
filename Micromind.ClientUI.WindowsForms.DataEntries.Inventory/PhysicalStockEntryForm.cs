using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class PhysicalStockEntryForm : Form, IForm
	{
		private PhysicalStockEntryData currentData;

		private const string TABLENAME_CONST = "Physical_Stock_Entry";

		private const string IDFIELD_CONST = "DocNumber";

		private bool isNewRecord = true;

		private int docNumberTryCount;

		private ScreenAccessRight screenRight;

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

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelBalance;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private LocationComboBox comboBoxLocation;

		private Label label4;

		private UltraLabel ultraLabel1;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private customersFlatComboBox comboBoxInventory;

		private CurrencyComboBox comboBoxCurrency;

		private ToolStripButton toolStripButtonGroup;

		private ProductComboBox comboBoxGridItem;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
				}
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
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
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

		public PhysicalStockEntryForm()
		{
			InitializeComponent();
			AddEvents();
			checked
			{
				int num;
				for (num = 0; num < contextMenuStrip1.Items.Count; num++)
				{
					dataGridItems.DropDownMenu.Items.Add(contextMenuStrip1.Items[num]);
					num--;
				}
			}
		}

		private void AddEvents()
		{
			base.Load += PhysicalStockEntryForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.BeforeCellUpdate += dataGridItems_BeforeCellUpdate;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			comboBoxInventory.SelectedIndexChanged += comboBoxInventory_SelectedIndexChanged;
			comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeCellDeactivate += dataGridItems_BeforeCellDeactivate;
			toolStripButtonGroup.CheckedChanged += toolStripButton2_Click;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			base.FormClosing += Form_FormClosing;
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			try
			{
				if (comboBoxLocation.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select a location.");
					return false;
				}
				if (false)
				{
					ReceiveLotSelectionForm receiveLotSelectionForm = new ReceiveLotSelectionForm();
					receiveLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["InventoryID"].Value.ToString();
					receiveLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
					receiveLotSelectionForm.LocationID = comboBoxLocation.SelectedID;
					receiveLotSelectionForm.RowQuantity = float.Parse(cell.Text);
					if (cell.Tag != null)
					{
						receiveLotSelectionForm.ProductLotTable = (DataTable)cell.Tag;
					}
					if (receiveLotSelectionForm.ShowDialog() != DialogResult.OK)
					{
						return false;
					}
					cell.Tag = receiveLotSelectionForm.ProductLotTable;
					cell.Appearance.FontData.Underline = DefaultableBoolean.True;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
			else
			{
				dataGridItems.SaveLayout();
			}
		}

		private void dataGridItems_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGridItems_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridCell activeCell = dataGridItems.ActiveCell;
			if (activeCell == null)
			{
				return;
			}
			if (activeCell.Column.Key == "InvoiceAmount")
			{
				if (activeCell.Value == null || activeCell.Value.ToString() == "")
				{
					activeCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
			}
			else if (activeCell.Column.Key == "BalanceAmount" && (activeCell.Value == null || activeCell.Value.ToString() == ""))
			{
				activeCell.Value = 0.ToString(Format.TotalAmountFormat);
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void FillProductQuantityAndCost()
		{
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxInventory_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			_ = dataGridItems.ActiveRow.Index;
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (e.Cell.Column.Key == "InventoryID")
			{
				activeRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
				if (activeRow.Cells["PurchaseDate"].Value == DBNull.Value)
				{
					activeRow.Cells["PurchaseDate"].Value = dateTimePickerDate.Value;
				}
				dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
				dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
				dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
				try
				{
					DateTime date = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 11, 59, 59);
					DataSet productQuantityAndCostAsOfDate = Factory.ProductSystem.GetProductQuantityAndCostAsOfDate(comboBoxGridItem.SelectedID, comboBoxLocation.SelectedID, date);
					DataRow dataRow = null;
					if (productQuantityAndCostAsOfDate != null && productQuantityAndCostAsOfDate.Tables.Count > 0 && productQuantityAndCostAsOfDate.Tables[0].Rows.Count > 0)
					{
						dataRow = productQuantityAndCostAsOfDate.Tables[0].Rows[0];
					}
					dataGridItems.ActiveRow.Cells["Quantity"].Value = 0.ToString();
					float result = 0f;
					float.TryParse(e.Cell.Row.Cells["Quantity"].Text.ToString(), out result);
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
			else if (isNewRecord && e.Cell.Column.Key == "Quantity")
			{
				float result2 = 0f;
				float result3 = 0f;
				float.TryParse(e.Cell.Row.Cells["Quantity"].Text.ToString(), out result2);
				float.TryParse(e.Cell.Row.Cells["Unit"].Value.ToString(), out result3);
			}
			if (e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell != null && (dataGridItems.ActiveCell.Column.Key == "Quantity" || dataGridItems.ActiveCell.Column.Key == "Quantity"))
			{
				float result4 = 0f;
				float result5 = 0f;
				float.TryParse(e.Cell.Row.Cells["Quantity"].Text.ToString(), out result4);
				float.TryParse(e.Cell.Row.Cells["Unit"].Value.ToString(), out result5);
			}
			if (e.Cell.Column.Key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
			{
				float result6 = 0f;
				float result7 = 0f;
				float.TryParse(e.Cell.Row.Cells["Quantity"].Text.ToString(), out result6);
				float.TryParse(e.Cell.Row.Cells["Unit"].Text.ToString(), out result7);
			}
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
			comboBoxInventory.Clear();
		}

		private void comboBoxInventory_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && !activeRow.IsGroupByRow)
			{
				if (activeRow.Cells["InventoryID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["InventoryID"].Activate();
				}
				else if (activeRow.Cells["PurchaseDate"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter a correct Purchase Date!");
					e.Cancel = true;
					activeRow.Cells["PurchaseDate"].Activate();
				}
				else
				{
					labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "InventoryID")
			{
				e.RaiseErrorEvent = false;
				comboBoxInventory.Text = dataGridItems.ActiveCell.Text;
				comboBoxInventory.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "New Qty")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PhysicalStockEntryData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PhysicalEStockEntryTable.Rows[0] : currentData.PhysicalEStockEntryTable.NewRow();
				if (dateTimePickerDate.Value > DateTime.Today)
				{
					dataRow["PurchaseDate"] = dateTimePickerDate.Value;
				}
				else
				{
					DateTime value = dateTimePickerDate.Value;
					dataRow["PurchaseDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				dataRow["DocNumber"] = textBoxVoucherNumber.Text;
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PhysicalEStockEntryTable.Rows.Add(dataRow);
				}
				currentData.PhysicalEStockEntryDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.PhysicalEStockEntryDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["DocNumber"] = textBoxVoucherNumber.Text;
					dataRow2["ItemID"] = row.Cells["InventoryID"].Value.ToString();
					dataRow2["PurchaseDate"] = row.Cells["PurchaseDate"].Value.ToString();
					dataRow2["ItemDescription"] = row.Cells["Description"].Value.ToString();
					dataRow2["Reference"] = row.Cells["Ref"].Value.ToString();
					dataRow2["Qty"] = row.Cells["Quantity"].Value.ToString();
					dataRow2["Unit"] = row.Cells["Unit"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.PhysicalEStockEntryDetailTable.Rows.Add(dataRow2);
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
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("InventoryID");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("PurchaseDate", typeof(DateTime));
				dataTable.Columns.Add("Ref");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Unit", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].Header.Caption = "Item Code";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Header.Caption = "Description";
				dataGridItems.DisplayLayout.Bands[0].Columns["PurchaseDate"].Header.Caption = "Purchase Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["PurchaseDate"].Width = 120;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["InventoryID"].Width = checked(18 * dataGridItems.Width) / 100;
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
					currentData = Factory.PhysicalStockEntrySystem.GetPhysicalStockEntryByID(voucherID);
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
					toolStripButtonGroup.Checked = false;
					DataRow dataRow = currentData.Tables["Physical_Stock_Entry"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["PurchaseDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["DocNumber"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
					textBoxNote.Text = dataRow["ItemDescription"].ToString();
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Physical_Stock_Entry_Detail") && currentData.PhysicalEStockEntryDetailTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Physical_Stock_Entry_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["InventoryID"] = row["ItemID"];
							dataRow3["Description"] = row["ItemDescription"];
							dataRow3["PurchaseDate"] = row["PurchaseDate"];
							dataRow3["Quantity"] = row["Qty"];
							dataRow3["Unit"] = row["Unit"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.EndUpdate();
					}
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
			toolStripButtonGroup.Checked = false;
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
				bool flag = Factory.PhysicalStockEntrySystem.CreatePhysicalStockEntry(currentData, !isNewRecord);
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
				if (ex.Number == 1055)
				{
					ErrorHelper.WarningMessage(ex.Message);
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				docNumberTryCount = 0;
			}
		}

		private bool ValidateData()
		{
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxLocation.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
			{
				if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
				{
					dataGridItems.Rows[i].Delete(displayPrompt: false);
				}
				else if (dataGridItems.Rows[i].Cells["InventoryID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					dataGridItems.Rows[i].Activate();
					return false;
				}
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one adjustment row.");
				return false;
			}
			formManager.IsFieldDirty(textBoxVoucherNumber);
			return true;
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				_ = row;
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
				comboBoxInventory.Clear();
				comboBoxLocation.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				toolStripButtonGroup.Checked = false;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
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
		}

		private bool Delete()
		{
			try
			{
				ErrorHelper.InformationMessage("Purchase Stock Entry Fields cannot be deleted. Use General Journal Entry screen to delete the transactions.");
				return false;
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
			string nextID = DatabaseHelper.GetNextID("Physical_Stock_Entry", "DocNumber", textBoxVoucherNumber.Text);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Physical_Stock_Entry", "DocNumber", textBoxVoucherNumber.Text);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Physical_Stock_Entry", "DocNumber");
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Physical_Stock_Entry", "DocNumber");
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Physical_Stock_Entry", "DocNumber", "", toolStripTextBoxFind.Text);
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				dateTimePickerDate.Value = DateTime.Now;
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
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				_ = dataGridItems.ActiveRow.IsGroupByRow;
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
				return Factory.PhysicalStockEntrySystem.GetNextDocNumber();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
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
					string sysDocID = "SYS_013";
					string text = textBoxVoucherNumber.Text;
					DataSet openingBalanceBatchToPrint = Factory.OpeningBalanceBatchSystem.GetOpeningBalanceBatchToPrint(sysDocID, text);
					if (openingBalanceBatchToPrint == null || openingBalanceBatchToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(openingBalanceBatchToPrint, sysDocID, "Opening Balance Batch - Inventory", SysDocTypes.None, isPrint, showPrintDialog);
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			if (toolStripButtonGroup.Checked)
			{
				dataGridItems.DisplayLayout.Bands[0].SortedColumns.Add("InventoryID", descending: true, groupBy: true);
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].SortedColumns.Remove("InventoryID");
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			dataGridItems.ImportFromExcel(autoFill: true);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = "SYS_013";
			journalDistibutionDialog.ShowDialog(this);
		}

		private void textBoxNote_TextChanged(object sender, EventArgs e)
		{
		}

		private void label3_Click(object sender, EventArgs e)
		{
		}

		private void panelDetails_Paint(object sender, PaintEventArgs e)
		{
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void PhysicalStockEntryForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				dateTimePickerDate.Value = DateTime.Now;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, "", this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.PhysicalStockEntryForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonGroup = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label4 = new System.Windows.Forms.Label();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			comboBoxInventory = new Micromind.DataControls.customersFlatComboBox();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonDistribution,
				toolStripSeparator3,
				toolStripButton1,
				toolStripButtonGroup,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(740, 31);
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
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "Import From Excel";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonGroup.CheckOnClick = true;
			toolStripButtonGroup.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonGroup.Name = "toolStripButtonGroup";
			toolStripButtonGroup.Size = new System.Drawing.Size(104, 28);
			toolStripButtonGroup.Text = "Group By Item";
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
			panelButtons.Location = new System.Drawing.Point(0, 450);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(740, 40);
			panelButtons.TabIndex = 10;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 14;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 13;
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
			buttonNew.TabIndex = 12;
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
			linePanelDown.Size = new System.Drawing.Size(740, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(630, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 15;
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
			buttonSave.TabIndex = 11;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(347, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 21);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(123, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 21);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(494, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(61, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(557, 5);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(92, 21);
			textBoxRef1.TabIndex = 3;
			textBoxNote.Location = new System.Drawing.Point(123, 56);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(526, 49);
			textBoxNote.TabIndex = 8;
			textBoxNote.TextChanged += new System.EventHandler(textBoxNote_TextChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 56);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(34, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			label3.Click += new System.EventHandler(label3_Click);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 418);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(716, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelBalance.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance2.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance2.FontData.BoldAsString = "True";
			appearance2.FontData.Name = "Tahoma";
			appearance2.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance2.TextHAlignAsString = "Right";
			appearance2.TextVAlignAsString = "Middle";
			labelBalance.Appearance = appearance2;
			labelBalance.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelBalance.Location = new System.Drawing.Point(573, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance3.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			appearance3.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance3.TextHAlignAsString = "Right";
			appearance3.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance3;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(571, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(comboBoxLocation);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 33);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 108);
			panelDetails.TabIndex = 0;
			panelDetails.Paint += new System.Windows.Forms.PaintEventHandler(panelDetails_Paint);
			appearance4.FontData.BoldAsString = "True";
			appearance4.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance4;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(17, 7);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(75, 15);
			linkLabelVoucherNumber.TabIndex = 140;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			appearance5.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance5;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(15, 28);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(60, 13);
			label4.TabIndex = 122;
			label4.Text = "Location:";
			comboBoxLocation.AlwaysInEditMode = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance6;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance7.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance7.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance7.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance7;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance8;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance9.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance9.BackColor2 = System.Drawing.SystemColors.Control;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance9;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance10;
			appearance11.BackColor = System.Drawing.SystemColors.Highlight;
			appearance11.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance11;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance12;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			appearance13.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance13;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance14.BackColor = System.Drawing.SystemColors.Control;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance14;
			appearance15.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance15;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			appearance16.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance16;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance17;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(123, 25);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(139, 21);
			comboBoxLocation.TabIndex = 4;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(266, 6);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(95, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Purchase Date:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 339);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(712, 81);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				printListToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(128, 26);
			printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			printListToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			printListToolStripMenuItem.Text = "Print List...";
			printListToolStripMenuItem.Click += new System.EventHandler(printListToolStripMenuItem_Click);
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance18;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance19.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance19;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance20;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance21.BackColor2 = System.Drawing.SystemColors.Control;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance21;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance22;
			appearance23.BackColor = System.Drawing.SystemColors.Highlight;
			appearance23.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance23;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance24;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			appearance25.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance25;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance26.BackColor = System.Drawing.SystemColors.Control;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance26;
			appearance27.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance27;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance28;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance29;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(12, 145);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(714, 275);
			dataGridItems.TabIndex = 9;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCurrency.CalcManager = ultraCalcManager1;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance30;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance31;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance32;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance33.BackColor2 = System.Drawing.SystemColors.Control;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance33;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance34;
			appearance35.BackColor = System.Drawing.SystemColors.Highlight;
			appearance35.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance35;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance36;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			appearance37.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance37;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance38.BackColor = System.Drawing.SystemColors.Control;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance38;
			appearance39.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance39;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance40;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance41;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(663, 244);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(77, 21);
			comboBoxCurrency.TabIndex = 122;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.Visible = false;
			comboBoxInventory.Assigned = false;
			comboBoxInventory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxInventory.CalcManager = ultraCalcManager1;
			comboBoxInventory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInventory.CustomReportFieldName = "";
			comboBoxInventory.CustomReportKey = "";
			comboBoxInventory.CustomReportValueType = 1;
			comboBoxInventory.DescriptionTextBox = null;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInventory.DisplayLayout.Appearance = appearance42;
			comboBoxInventory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInventory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventory.DisplayLayout.GroupByBox.Appearance = appearance43;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance44;
			comboBoxInventory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance45.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance45.BackColor2 = System.Drawing.SystemColors.Control;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventory.DisplayLayout.GroupByBox.PromptAppearance = appearance45;
			comboBoxInventory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInventory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInventory.DisplayLayout.Override.ActiveCellAppearance = appearance46;
			appearance47.BackColor = System.Drawing.SystemColors.Highlight;
			appearance47.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInventory.DisplayLayout.Override.ActiveRowAppearance = appearance47;
			comboBoxInventory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInventory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInventory.DisplayLayout.Override.CardAreaAppearance = appearance48;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			appearance49.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInventory.DisplayLayout.Override.CellAppearance = appearance49;
			comboBoxInventory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInventory.DisplayLayout.Override.CellPadding = 0;
			appearance50.BackColor = System.Drawing.SystemColors.Control;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventory.DisplayLayout.Override.GroupByRowAppearance = appearance50;
			appearance51.TextHAlignAsString = "Left";
			comboBoxInventory.DisplayLayout.Override.HeaderAppearance = appearance51;
			comboBoxInventory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInventory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			comboBoxInventory.DisplayLayout.Override.RowAppearance = appearance52;
			comboBoxInventory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance53.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInventory.DisplayLayout.Override.TemplateAddRowAppearance = appearance53;
			comboBoxInventory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInventory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInventory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInventory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInventory.Editable = true;
			comboBoxInventory.FilterString = "";
			comboBoxInventory.FilterSysDocID = "";
			comboBoxInventory.HasAll = false;
			comboBoxInventory.HasCustom = false;
			comboBoxInventory.IsDataLoaded = false;
			comboBoxInventory.Location = new System.Drawing.Point(668, 217);
			comboBoxInventory.MaxDropDownItems = 12;
			comboBoxInventory.Name = "comboBoxInventory";
			comboBoxInventory.ShowConsignmentOnly = false;
			comboBoxInventory.ShowInactive = false;
			comboBoxInventory.ShowLPOCustomersOnly = false;
			comboBoxInventory.ShowPROCustomersOnly = false;
			comboBoxInventory.ShowQuickAdd = true;
			comboBoxInventory.Size = new System.Drawing.Size(72, 21);
			comboBoxInventory.TabIndex = 121;
			comboBoxInventory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxInventory.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.AlwaysInEditMode = true;
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CalcManager = ultraCalcManager1;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance54;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance55;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance56;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance57.BackColor2 = System.Drawing.SystemColors.Control;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance57;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance58;
			appearance59.BackColor = System.Drawing.SystemColors.Highlight;
			appearance59.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance59;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance60;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			appearance61.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance61;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance62.BackColor = System.Drawing.SystemColors.Control;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance62;
			appearance63.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance63;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance64;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance65.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance65;
			comboBoxGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridItem.Editable = true;
			comboBoxGridItem.FilterString = "";
			comboBoxGridItem.FilterSysDocID = "";
			comboBoxGridItem.HasAllAccount = false;
			comboBoxGridItem.HasCustom = false;
			comboBoxGridItem.IsDataLoaded = false;
			comboBoxGridItem.Location = new System.Drawing.Point(666, 190);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 21);
			comboBoxGridItem.TabIndex = 123;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(740, 490);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxCurrency);
			base.Controls.Add(comboBoxInventory);
			base.Controls.Add(comboBoxGridItem);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "PhysicalStockEntryForm";
			Text = "Physical Stock Entry";
			base.Load += new System.EventHandler(PhysicalStockEntryForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
