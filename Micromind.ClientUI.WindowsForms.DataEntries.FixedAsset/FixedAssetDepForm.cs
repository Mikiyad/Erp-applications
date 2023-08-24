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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset
{
	public class FixedAssetDepForm : Form, IForm
	{
		private FixedAssetDepData currentData;

		private const string TABLENAME_CONST = "FixedAsset_Dep";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private FixedAssetSelector fixedAssetSelectorObj;

		private Form fixedAssetFormObj;

		private DataSet fixedAssetData;

		private DialogResult result;

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

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelBalance;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraLabel ultraLabel1;

		private FixedAssetsComboBox comboBoxGridFixedAsset;

		private XPButton xpButton2;

		private YearComboBox comboBoxYear;

		private Label label2;

		private MonthComboBox comboBoxMonth;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		public ScreenAreas ScreenArea => ScreenAreas.FixedAsset;

		public int ScreenID => 4002;

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
					textBoxVoucherNumber.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.Enabled = false;
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

		public FixedAssetDepForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxYear.LoadData();
			comboBoxMonth.LoadData();
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
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridFixedAsset.SelectedIndexChanged += comboBoxGridFixedAsset_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["AssetID"].Value != null && !(row.Cells["AssetID"].Value.ToString() == ""))
				{
					try
					{
						row.Cells["AssetID"].Value.ToString();
						DataSet dataSet = new DataSet();
						DataRow dataRow = null;
						if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
						{
							dataRow = dataSet.Tables[0].Rows[0];
						}
						if (dataRow != null)
						{
							row.Cells["Onhand"].Value = dataRow["Quantity"].ToString();
							row.Cells["Cost"].Value = dataRow["AverageCost"].ToString();
						}
						else
						{
							row.Cells["Onhand"].Value = 0;
							row.Cells["Cost"].Value = 0;
						}
					}
					catch (Exception e2)
					{
						ErrorHelper.ProcessError(e2);
					}
				}
			}
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridFixedAsset_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "AssetID")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridFixedAsset.SelectedName;
				try
				{
					DataSet dataSet = new DataSet();
					DataRow dataRow = null;
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
					{
						dataRow = dataSet.Tables[0].Rows[0];
					}
					if (dataRow != null)
					{
						dataGridItems.ActiveRow.Cells["Onhand"].Value = dataRow["Quantity"].ToString();
						dataGridItems.ActiveRow.Cells["Cost"].Value = dataRow["AverageCost"].ToString();
					}
					else
					{
						dataGridItems.ActiveRow.Cells["Onhand"].Value = 0;
						dataGridItems.ActiveRow.Cells["Cost"].Value = 0;
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
			if (isNewRecord && e.Cell.Column.Key == "Adj Qty" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Adj Qty")
			{
				float num = 0f;
				float num2 = 0f;
				float.TryParse(e.Cell.Row.Cells["Adj Qty"].Text.ToString(), out num2);
				float.TryParse(e.Cell.Row.Cells["Onhand"].Value.ToString(), out num);
				e.Cell.Row.Cells["New Qty"].Value = (num + num2).ToString();
			}
			else if (isNewRecord && e.Cell.Column.Key == "New Qty" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "New Qty")
			{
				float num3 = 0f;
				float num4 = 0f;
				float.TryParse(e.Cell.Row.Cells["New Qty"].Text.ToString(), out num4);
				float.TryParse(e.Cell.Row.Cells["Onhand"].Value.ToString(), out num3);
				e.Cell.Row.Cells["Adj Qty"].Value = (num4 - num3).ToString();
			}
			else if (isNewRecord && e.Cell.Column.Key == "Onhand" && e.Cell.Row.Cells["Adj Qty"].Value != null && e.Cell.Row.Cells["Adj Qty"].Value.ToString() != "")
			{
				float num5 = 0f;
				float num6 = 0f;
				float.TryParse(e.Cell.Row.Cells["Adj Qty"].Text.ToString(), out num6);
				float.TryParse(e.Cell.Row.Cells["Onhand"].Value.ToString(), out num5);
				e.Cell.Row.Cells["New Qty"].Value = (num5 + num6).ToString();
			}
			if (e.Cell.Column.Key == "Adj Qty" && dataGridItems.ActiveCell != null && (dataGridItems.ActiveCell.Column.Key == "Adj Qty" || dataGridItems.ActiveCell.Column.Key == "New Qty"))
			{
				if (decimal.Parse(e.Cell.Row.Cells["Adj Qty"].Value.ToString()) < 0m)
				{
					e.Cell.Row.Cells["Cost"].Activation = Activation.Disabled;
				}
				else
				{
					e.Cell.Row.Cells["Cost"].Activation = Activation.AllowEdit;
				}
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridFixedAsset.Clear();
		}

		private void comboBoxGridFixedAsset_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell.Column.Key == "AssetID" && dataGridItems.ActiveRow.Cells["AssetID"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!row.IsActiveRow && row.Cells["AssetID"].Value == dataGridItems.ActiveRow.Cells["AssetID"].Value)
					{
						ErrorHelper.InformationMessage("This item is already in the list.");
						e.Cancel = true;
						return;
					}
				}
				if (activeRow.Cells["AssetID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["AssetID"].Activate();
				}
				else
				{
					labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "AssetID")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridFixedAsset.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridFixedAsset.QuickAddItem();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new FixedAssetDepData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.FixedAssetDepTable.Rows[0] : currentData.FixedAssetDepTable.NewRow();
				DateTime dateTime = new DateTime(int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, 1).AddMonths(1).AddSeconds(-1.0);
				dataRow["TransactionDate"] = dateTime;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Year"] = comboBoxYear.Text;
				dataRow["Month"] = comboBoxMonth.SelectedID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Description"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.FixedAssetDepTable.Rows.Add(dataRow);
				}
				currentData.FixedAssetDepDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.FixedAssetDepDetailsTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["FixedAssetID"] = row.Cells["AssetID"].Value.ToString();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["YearValue"] = ((!row.Cells["YearValue"].Value.IsNullOrEmpty()) ? ((IConvertible)row.Cells["YearValue"].Value.ToString()) : ((IConvertible)DBNull.Value));
					dataRow2["CurrentValue"] = ((!row.Cells["CurrentValue"].Value.IsNullOrEmpty()) ? ((IConvertible)row.Cells["CurrentValue"].Value.ToString()) : ((IConvertible)DBNull.Value));
					dataRow2["RowIndex"] = row.Index;
					dataRow2["Year"] = comboBoxYear.Text;
					dataRow2["Month"] = comboBoxMonth.SelectedID;
					dataRow2["DepAmount"] = row.Cells["DepAmount"].Value.ToString();
					dataRow2.EndEdit();
					currentData.FixedAssetDepDetailsTable.Rows.Add(dataRow2);
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
				dataTable.Columns.Add("AssetID");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("YearValue", typeof(decimal));
				dataTable.Columns.Add("CurrentValue", typeof(decimal));
				dataTable.Columns.Add("DepAmount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["AssetID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["AssetID"].Header.Caption = "Asset Code";
				dataGridItems.DisplayLayout.Bands[0].Columns["AssetID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["AssetID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["AssetID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].Header.Caption = "Year Value";
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["CurrentValue"].Header.Caption = "Current Value";
				dataGridItems.DisplayLayout.Bands[0].Columns["CurrentValue"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["CurrentValue"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["YearValue"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["DepAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["DepAmount"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["DepAmount"].Header.Caption = "Dep Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["AssetID"].Width = checked(18 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("DepAmount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["DepAmount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["DepAmount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["DepAmount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["DepAmount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["DepAmount"].DisplayFormat = "{0:n}";
				dataGridItems.AllowAddNew = false;
				dataGridItems.ContextMenu = null;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.ShowInsertMenu = false;
				dataGridItems.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
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
					currentData = Factory.FixedAssetDepSystem.GetFixedAssetDepByID(SystemDocID, voucherID);
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
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["FixedAsset_Dep"].Rows[0];
				comboBoxMonth.SelectedID = int.Parse(dataRow["Month"].ToString());
				comboBoxYear.Text = dataRow["Year"].ToString();
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				textBoxRef1.Text = dataRow["Reference"].ToString();
				textBoxNote.Text = dataRow["Description"].ToString();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData.Tables.Contains("FixedAsset_Dep_Detail") && currentData.FixedAssetDepDetailsTable.Rows.Count != 0)
				{
					foreach (DataRow row in currentData.Tables["FixedAsset_Dep_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Description"] = row["Description"];
						dataRow3["AssetID"] = row["FixedAssetID"];
						dataRow3["Description"] = row["Description"];
						dataRow3["YearValue"] = row["YearValue"];
						dataRow3["CurrentValue"] = row["CurrentValue"];
						dataRow3["DepAmount"] = row["DepAmount"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
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
				bool flag = Factory.FixedAssetDepSystem.CreateFixedAssetDep(currentData, !isNewRecord);
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
			DateTime t = new DateTime(int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, 1);
			if (isNewRecord && t < DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && t > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxYear.Text == "" || comboBoxMonth.SelectedID < 1 || comboBoxSysDoc.SelectedID == "")
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
				else if (dataGridItems.Rows[i].Cells["AssetID"].Value.ToString() == "")
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
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("FixedAsset_Dep", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				_ = row;
			}
			return num;
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
				comboBoxGridFixedAsset.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.FixedAssetDepSystem.DeleteFixedAssetDep(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("FixedAsset_Dep", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("FixedAsset_Dep", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("FixedAsset_Dep", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("FixedAsset_Dep", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("FixedAsset_Dep", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.FixedAssetDep);
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.FixedAssetDep);
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

		private void xpButton2_Click(object sender, EventArgs e)
		{
			try
			{
				if (comboBoxYear.Text == "" || comboBoxMonth.Text == "")
				{
					ErrorHelper.InformationMessage("Please select the period.");
				}
				else
				{
					ShowAssetSelector();
					if (result == DialogResult.OK)
					{
						new DataSet();
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						if (IsNewRecord)
						{
							dataTable.Rows.Clear();
						}
						foreach (DataRow row in fixedAssetData.Tables["FixedAsset"].Rows)
						{
							string assetID = row["Asset Code"].ToString();
							foreach (DataRow row2 in Factory.FixedAssetSystem.CalculateDepreciation(int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, assetID).Tables["FixedAsset_Dep_Detail"].Rows)
							{
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2["AssetID"] = row2["FixedAssetID"];
								dataRow2["Description"] = row2["Description"];
								dataRow2["YearValue"] = row2["YearValue"];
								dataRow2["CurrentValue"] = row2["CurrentValue"];
								dataRow2["DepAmount"] = row2["DepAmount"];
								dataTable.Rows.Add(dataRow2);
							}
						}
						if (dataTable != null && dataTable.Rows.Count == 0)
						{
							ErrorHelper.InformationMessage("No asset found that match for depreciation for the selected period.");
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ShowAssetSelector()
		{
			fixedAssetSelectorObj = new FixedAssetSelector();
			fixedAssetSelectorObj.BackColor = Color.Transparent;
			fixedAssetSelectorObj.CustomReportFieldName = "";
			fixedAssetSelectorObj.CustomReportKey = "";
			fixedAssetSelectorObj.CustomReportValueType = 1;
			fixedAssetSelectorObj.Location = new Point(6, 15);
			fixedAssetSelectorObj.Name = "FixedAssetSelector";
			fixedAssetSelectorObj.Size = new Size(430, 350);
			fixedAssetSelectorObj.TabIndex = 0;
			GroupBox groupBox = new GroupBox();
			groupBox.Controls.Add(fixedAssetSelectorObj);
			groupBox.Location = new Point(2, 12);
			groupBox.Name = "groupBoxCC";
			groupBox.Size = new Size(453, 150);
			groupBox.TabIndex = 10;
			groupBox.TabStop = false;
			groupBox.Text = "Fixed Assets";
			Button button = new Button();
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button.Location = new Point(230, 170);
			button.Name = "buttonOK";
			button.DialogResult = DialogResult.OK;
			button.Size = new Size(102, 24);
			button.TabIndex = 1;
			button.Text = "&Display";
			button.UseVisualStyleBackColor = true;
			button.Click += buttonOK_Click;
			Button button2 = new Button();
			button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button2.DialogResult = DialogResult.Cancel;
			button2.Location = new Point(350, 170);
			button2.Name = "buttonClose";
			button2.Size = new Size(102, 24);
			button2.TabIndex = 2;
			button2.Text = "&Close";
			button2.UseVisualStyleBackColor = true;
			button2.Click += buttonClose_Click;
			fixedAssetFormObj = new Form();
			fixedAssetFormObj.Size = new Size(481, 240);
			fixedAssetFormObj.Controls.Add(button);
			fixedAssetFormObj.Controls.Add(button2);
			fixedAssetFormObj.Controls.Add(groupBox);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FixedAssetDepForm));
			fixedAssetFormObj.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			fixedAssetFormObj.Text = "Select Asset";
			result = fixedAssetFormObj.ShowDialog(this);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				fixedAssetData = Factory.FixedAssetSystem.GetAssetList(fixedAssetSelectorObj.FromFixedAsset, fixedAssetSelectorObj.ToFixedAsset, fixedAssetSelectorObj.FromGroup, fixedAssetSelectorObj.ToGroup, fixedAssetSelectorObj.FromClass, fixedAssetSelectorObj.ToClass, fixedAssetSelectorObj.FromLocation, fixedAssetSelectorObj.ToLocation);
				fixedAssetFormObj.Close();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				ClearForm();
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			fixedAssetFormObj.Close();
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			DocumentInformationDialog documentInformationDialog = new DocumentInformationDialog();
			documentInformationDialog.VoucherID = textBoxVoucherNumber.Text;
			documentInformationDialog.SysDocID = comboBoxSysDoc.SelectedID;
			documentInformationDialog.ShowDialog(this);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FixedAssetDepListFormObj);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string systemDocID = SystemDocID;
				string text = textBoxVoucherNumber.Text;
				DataSet fixedAssetDepToPrint = Factory.FixedAssetDepSystem.GetFixedAssetDepToPrint(systemDocID, text);
				if (fixedAssetDepToPrint == null || fixedAssetDepToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(fixedAssetDepToPrint, systemDocID, "Fixed Asset Dep", SysDocTypes.FixedAssetDep, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset.FixedAssetDepForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxYear = new Micromind.DataControls.YearComboBox();
			label2 = new System.Windows.Forms.Label();
			comboBoxMonth = new Micromind.DataControls.MonthComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			labelVoided = new System.Windows.Forms.Label();
			comboBoxGridFixedAsset = new Micromind.DataControls.FixedAssetsComboBox();
			xpButton2 = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridFixedAsset).BeginInit();
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
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator3,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator2,
				toolStripButtonDistribution,
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 450);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(740, 40);
			panelButtons.TabIndex = 2;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 2;
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
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 139);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(714, 281);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			textBoxVoucherNumber.Location = new System.Drawing.Point(263, 3);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(482, 30);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(545, 27);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(92, 20);
			textBoxRef1.TabIndex = 7;
			textBoxNote.Location = new System.Drawing.Point(70, 49);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(567, 20);
			textBoxNote.TabIndex = 8;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 51);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance13;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(206, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel2.TabIndex = 0;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Number:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance15.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance15.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance15;
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
			appearance16.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance16.FontData.BoldAsString = "True";
			appearance16.FontData.Name = "Tahoma";
			appearance16.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance16.TextHAlignAsString = "Right";
			appearance16.TextVAlignAsString = "Middle";
			labelBalance.Appearance = appearance16;
			labelBalance.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelBalance.Location = new System.Drawing.Point(573, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance17.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance17.FontData.BoldAsString = "True";
			appearance17.FontData.Name = "Tahoma";
			appearance17.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance17.TextHAlignAsString = "Right";
			appearance17.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance17;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(571, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			panelDetails.Controls.Add(comboBoxYear);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(comboBoxMonth);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Location = new System.Drawing.Point(0, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 76);
			panelDetails.TabIndex = 0;
			comboBoxYear.FormattingEnabled = true;
			comboBoxYear.Location = new System.Drawing.Point(162, 26);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(95, 21);
			comboBoxYear.TabIndex = 23;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(16, 29);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 22;
			label2.Text = "Period:";
			comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMonth.FormattingEnabled = true;
			comboBoxMonth.IsMonthNumbers = false;
			comboBoxMonth.Location = new System.Drawing.Point(70, 26);
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Size = new System.Drawing.Size(87, 21);
			comboBoxMonth.TabIndex = 21;
			appearance18.FontData.BoldAsString = "True";
			appearance18.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance18;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(19, 6);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance19.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance19;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance20;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance21;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance22;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance23.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance23.BackColor2 = System.Drawing.SystemColors.Control;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance23;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance24;
			appearance25.BackColor = System.Drawing.SystemColors.Highlight;
			appearance25.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance25;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance26;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			appearance27.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance27;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance28.BackColor = System.Drawing.SystemColors.Control;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance28;
			appearance29.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance29;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance30;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance31;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(70, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(121, 20);
			comboBoxSysDoc.TabIndex = 3;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			comboBoxGridFixedAsset.Assigned = false;
			comboBoxGridFixedAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridFixedAsset.CustomReportFieldName = "";
			comboBoxGridFixedAsset.CustomReportKey = "";
			comboBoxGridFixedAsset.CustomReportValueType = 1;
			comboBoxGridFixedAsset.DescriptionTextBox = null;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridFixedAsset.DisplayLayout.Appearance = appearance32;
			comboBoxGridFixedAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridFixedAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.Appearance = appearance33;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance34;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance35.BackColor2 = System.Drawing.SystemColors.Control;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridFixedAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance35;
			comboBoxGridFixedAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridFixedAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridFixedAsset.DisplayLayout.Override.ActiveCellAppearance = appearance36;
			appearance37.BackColor = System.Drawing.SystemColors.Highlight;
			appearance37.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridFixedAsset.DisplayLayout.Override.ActiveRowAppearance = appearance37;
			comboBoxGridFixedAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridFixedAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridFixedAsset.DisplayLayout.Override.CardAreaAppearance = appearance38;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			appearance39.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridFixedAsset.DisplayLayout.Override.CellAppearance = appearance39;
			comboBoxGridFixedAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridFixedAsset.DisplayLayout.Override.CellPadding = 0;
			appearance40.BackColor = System.Drawing.SystemColors.Control;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridFixedAsset.DisplayLayout.Override.GroupByRowAppearance = appearance40;
			appearance41.TextHAlignAsString = "Left";
			comboBoxGridFixedAsset.DisplayLayout.Override.HeaderAppearance = appearance41;
			comboBoxGridFixedAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridFixedAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridFixedAsset.DisplayLayout.Override.RowAppearance = appearance42;
			comboBoxGridFixedAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridFixedAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance43;
			comboBoxGridFixedAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridFixedAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridFixedAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridFixedAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridFixedAsset.Editable = true;
			comboBoxGridFixedAsset.FilterString = "";
			comboBoxGridFixedAsset.HasAllAccount = false;
			comboBoxGridFixedAsset.HasCustom = false;
			comboBoxGridFixedAsset.IsDataLoaded = false;
			comboBoxGridFixedAsset.Location = new System.Drawing.Point(558, 161);
			comboBoxGridFixedAsset.MaxDropDownItems = 12;
			comboBoxGridFixedAsset.Name = "comboBoxGridFixedAsset";
			comboBoxGridFixedAsset.ShowInactiveItems = true;
			comboBoxGridFixedAsset.ShowQuickAdd = true;
			comboBoxGridFixedAsset.Size = new System.Drawing.Size(140, 20);
			comboBoxGridFixedAsset.TabIndex = 121;
			comboBoxGridFixedAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridFixedAsset.Visible = false;
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.BackColor = System.Drawing.Color.Silver;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(13, 113);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(96, 24);
			xpButton2.TabIndex = 122;
			xpButton2.Text = "Load";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(740, 490);
			base.Controls.Add(xpButton2);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxGridFixedAsset);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "FixedAssetDepForm";
			Text = "Fixed Asset Depreciation";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridFixedAsset).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
