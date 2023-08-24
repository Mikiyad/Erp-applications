using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class JobFeeDetailForm : Form, IForm
	{
		private bool allowEdit = true;

		private JobData currentData;

		private const string TABLENAME_CONST = "Job_Fee_Detail";

		private const string IDFIELD_CONST = "JobID";

		private bool isNewRecord = true;

		private decimal _totalAmount;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isDiscountPercent;

		private EntityTypesEnum entityType;

		private string entityID;

		private string entityName;

		private string projectID;

		private string projectName;

		private string retentionDays;

		private DateTime retentionDate = DateTime.Now;

		private IContainer components;

		private ToolStrip toolStrip1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private Panel panelDetails;

		private ProductUnitComboBox comboBoxGridProductUnit;

		private TextBox textBoxEntityName;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator4;

		private TextBox textBoxProjectCode;

		private Label label2;

		private TextBox textBoxProjectName;

		private TextBox textBoxEntityCode;

		private Label label4;

		private JobFeeComboBox comboBoxGridItem;

		private TextBox textBoxRetentionName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private JobFeeComboBox comboBoxRetention;

		private UltraGroupBox ultraGroupBox1;

		private Label label3;

		private Label label1;

		private PercentTextBox textBoxRetentionPercent;

		private UltraGroupBox groupBoxFees;

		private UltraGroupBox ultraGroupBox3;

		private AmountTextBox textBoxAdvanceAmount;

		private Label label6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private TextBox textBoxAdvanceDescription;

		private JobFeeComboBox comboBoxAdvanceItem;

		private Label label5;

		private NumberTextBox textBoxRetentionDays;

		private MMSDateTimePicker dateTimePickerRetDate;

		private MMLabel mmLabel38;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2012;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public decimal TotalAmount
		{
			get;
			private set;
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		public decimal RetentionPercent
		{
			get
			{
				if (comboBoxRetention.SelectedID != "")
				{
					return decimal.Parse(textBoxRetentionPercent.Text);
				}
				return 0m;
			}
		}

		public decimal AdvanceAmount
		{
			get
			{
				if (comboBoxAdvanceItem.SelectedID != "")
				{
					return decimal.Parse(textBoxAdvanceAmount.Text);
				}
				return 0m;
			}
		}

		public EntityTypesEnum EntityType
		{
			get
			{
				return entityType;
			}
			set
			{
				entityType = value;
			}
		}

		public string EntityID
		{
			get
			{
				return entityID;
			}
			set
			{
				string text2 = entityID = (textBoxEntityCode.Text = value);
			}
		}

		public string EntityName
		{
			get
			{
				return entityName;
			}
			set
			{
				string text2 = entityName = (textBoxEntityName.Text = value);
			}
		}

		public string ProjectID
		{
			get
			{
				return projectID;
			}
			set
			{
				string text2 = projectID = (textBoxProjectCode.Text = value);
			}
		}

		public string ProjectName
		{
			get
			{
				return projectName;
			}
			set
			{
				string text2 = projectName = (textBoxProjectName.Text = value);
			}
		}

		public string RetentionDays
		{
			get
			{
				return retentionDays;
			}
			set
			{
				string text2 = retentionDays = (textBoxRetentionDays.Text = value);
			}
		}

		public DateTime RetentionDate
		{
			get
			{
				return retentionDate;
			}
			set
			{
				DateTime dateTime2 = retentionDate = (dateTimePickerRetDate.Value = value);
			}
		}

		public bool isRetDateChecked
		{
			get
			{
				return dateTimePickerRetDate.Checked;
			}
			set
			{
				dateTimePickerRetDate.Checked = value;
			}
		}

		public JobFeeDetailForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			comboBoxRetention.DescriptionTextBox = textBoxRetentionName;
		}

		private void AddEvents()
		{
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				SetFeePaymentTerm(e.Cell);
			}
		}

		private bool SetFeePaymentTerm(UltraGridCell cell)
		{
			try
			{
				if (cell == null || cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
				{
					return false;
				}
				JobFeeScheduledTermDialog jobFeeScheduledTermDialog = new JobFeeScheduledTermDialog();
				jobFeeScheduledTermDialog.FeeID = dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString();
				jobFeeScheduledTermDialog.FeeName = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
				jobFeeScheduledTermDialog.JobID = textBoxProjectCode.Text;
				jobFeeScheduledTermDialog.JobName = textBoxProjectName.Text;
				jobFeeScheduledTermDialog.FeeAmount = decimal.Parse(cell.Value.ToString());
				if (cell.Tag != null)
				{
					jobFeeScheduledTermDialog.JobFeeTermTable = (DataTable)cell.Tag;
				}
				if (jobFeeScheduledTermDialog.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				cell.Tag = jobFeeScheduledTermDialog.JobFeeTermTable;
				formManager.IsForcedDirty = true;
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			if (e.Row != null && e.Row.Cells["Fee Code"].Value != null)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!row.IsActiveRow && row.Cells[0].Value.ToString().Trim() == e.Row.Cells["Fee Code"].Value.ToString().Trim())
					{
						ErrorHelper.InformationMessage("Item is already exist in the list!");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative amount is not allowed. Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void comboBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountAmount_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null && ultraGridColumn != null && ultraGridColumn.Key == "Fee Code" && dataGridItems.ActiveCell.Column.Key == "Fee Code" && dataGridItems.ActiveCell.Value.ToString() != "")
			{
				new FormHelper().EditJobFee(dataGridItems.ActiveCell.Value.ToString());
			}
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow == null)
					{
						return;
					}
					if (e.Cell.Column.Key == "Fee Code")
					{
						if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
						{
							comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
						}
						else if (comboBoxGridItem.SelectedRow == null)
						{
							return;
						}
						dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
						if (e.Cell.Row.Cells["Amount"].Text == "")
						{
							e.Cell.Row.Cells["Amount"].Value = 0;
						}
					}
					if (e.Cell.Column.Key == "Location")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["Location"].Value = e.Cell.Value.ToString();
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				if (dataGridItems.ActiveRow != null && e.Cell.Column.Key == "Amount")
				{
					CalculateTotal();
				}
			}
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
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.IsLoadingData = true;
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Fee Code")
			{
				if (dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = "";
				}
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridItems.ActiveCell.Value = result;
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && activeRow.Cells["Fee Code"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				activeRow.Cells["Fee Code"].Activate();
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Fee Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridItem.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridItem.QuickAddItem();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new JobData();
				}
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.JobFeeTable.Columns.Contains(column.Key))
					{
						currentData.JobFeeTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.JobFeePaymentTermTable.Rows.Clear();
				currentData.JobFeeTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow = currentData.JobFeeTable.NewRow();
					dataRow.BeginEdit();
					dataRow["FeeID"] = row.Cells["Fee Code"].Value.ToString();
					dataRow["Description"] = row.Cells["Description"].Value.ToString();
					dataRow["Amount"] = row.Cells["Amount"].Value.ToString();
					if (row.Cells["Amount"].Value.ToString() != "" && row.Cells["Amount"].Value != null)
					{
						_totalAmount += decimal.Parse(row.Cells["Amount"].Value.ToString());
					}
					dataRow["RowIndex"] = row.Index;
					dataRow.EndEdit();
					currentData.JobFeeTable.Rows.Add(dataRow);
					if (row.Cells["Amount"].Tag != null)
					{
						foreach (DataRow row2 in (row.Cells["Amount"].Tag as DataTable).Rows)
						{
							DataRow dataRow3 = currentData.Tables["Job_FEE_PAYMENT_TERM"].NewRow();
							dataRow3["JobID"] = row2["JobID"];
							dataRow3["FeeID"] = row2["FeeID"];
							dataRow3["Description"] = row2["Description"];
							dataRow3["DueDate"] = row2["DueDate"];
							dataRow3["Amount"] = row2["Amount"];
							dataRow3["AmountPercent"] = row2["AmountPercent"];
							dataRow3["TermType"] = 1;
							dataRow3["RowIndex"] = row.Index;
							currentData.Tables["Job_FEE_PAYMENT_TERM"].Rows.Add(dataRow3);
						}
					}
				}
				currentData.JobTable.Rows.Clear();
				DataRow dataRow4 = currentData.JobTable.NewRow();
				dataRow4["JobID"] = textBoxProjectCode.Text;
				dataRow4["JobName"] = textBoxProjectName.Text;
				dataRow4["CustomerID"] = textBoxEntityCode.Text;
				dataRow4["ProjectAmount"] = _totalAmount;
				if (comboBoxRetention.SelectedID != "")
				{
					dataRow4["RetentionItemID"] = comboBoxRetention.SelectedID;
					dataRow4["RetentionDescription"] = textBoxRetentionName.Text;
					dataRow4["RetentionPercent"] = textBoxRetentionPercent.Text;
				}
				else
				{
					dataRow4["RetentionItemID"] = DBNull.Value;
					dataRow4["RetentionDescription"] = DBNull.Value;
					dataRow4["RetentionPercent"] = DBNull.Value;
				}
				if (textBoxRetentionDays.Text != "")
				{
					dataRow4["RetentionDays"] = textBoxRetentionDays.Text;
					RetentionDays = textBoxRetentionDays.Text;
				}
				if (dateTimePickerRetDate.Checked)
				{
					dataRow4["RetentionDate"] = dateTimePickerRetDate.Value;
				}
				else
				{
					dataRow4["RetentionDays"] = DBNull.Value;
				}
				if (comboBoxAdvanceItem.SelectedID != "")
				{
					dataRow4["AdvanceItemID"] = comboBoxAdvanceItem.SelectedID;
					dataRow4["AdvanceDescription"] = textBoxAdvanceDescription.Text;
					dataRow4["AdvanceAmount"] = textBoxAdvanceAmount.Text;
				}
				else
				{
					dataRow4["AdvanceItemID"] = DBNull.Value;
					dataRow4["AdvanceDescription"] = DBNull.Value;
					dataRow4["AdvanceAmount"] = DBNull.Value;
				}
				dataRow4.EndEdit();
				currentData.JobTable.Rows.Add(dataRow4);
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
				dataTable.Columns.Add("Fee Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].ValueList = comboBoxGridItem;
				if (CompanyPreferences.UseProjectPhase)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].Header.Caption = "Phase Code";
					Text = "Project Phases and Fees";
					groupBoxFees.Text = "Phases";
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.FontData.Underline = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Total", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["Fee Code"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData()
		{
			try
			{
				currentData = Factory.JobSystem.GetJobFeeDetailsByID(ProjectID);
				if (currentData != null)
				{
					FillData();
					isNewRecord = false;
					formManager.ResetDirty();
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
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.JobFeeTable.Rows.Count != 0)
				{
					DataRow dataRow = currentData.JobTable.Rows[0];
					comboBoxRetention.SelectedID = dataRow["RetentionItemID"].ToString();
					textBoxRetentionName.Text = dataRow["RetentionDescription"].ToString();
					if (dataRow["RetentionPercent"] != DBNull.Value)
					{
						textBoxRetentionPercent.Text = dataRow["RetentionPercent"].ToString();
					}
					else
					{
						textBoxRetentionPercent.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["RetentionDays"] != DBNull.Value)
					{
						textBoxRetentionDays.Text = Convert.ToString(Convert.ToInt64(Convert.ToDouble(dataRow["RetentionDays"].ToString())));
					}
					else
					{
						textBoxRetentionDays.Text = 0.ToString(Format.NumberFormat);
					}
					if (dataRow["RetentionDate"] != DBNull.Value)
					{
						dateTimePickerRetDate.Value = DateTime.Parse(dataRow["RetentionDate"].ToString());
						dateTimePickerRetDate.Checked = true;
					}
					else
					{
						dateTimePickerRetDate.Clear();
					}
					comboBoxAdvanceItem.SelectedID = dataRow["AdvanceItemID"].ToString();
					textBoxAdvanceDescription.Text = dataRow["AdvanceDescription"].ToString();
					if (dataRow["AdvanceAmount"] != DBNull.Value)
					{
						textBoxAdvanceAmount.Text = dataRow["AdvanceAmount"].ToString();
					}
					else
					{
						textBoxAdvanceAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Job_Fee_Detail") && currentData.JobFeeTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Job_Fee_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Fee Code"] = row["FeeID"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Amount"] = row["Amount"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							DataRow[] array = currentData.Tables["Job_FEE_PAYMENT_TERM"].Select("FeeID = '" + row2.Cells["Fee Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
							if (array.Length != 0)
							{
								DataSet dataSet = new DataSet();
								dataSet.Merge(array);
								DataTable tag = dataSet.Tables[0];
								row2.Cells["Amount"].Tag = tag;
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isDataLoading = false;
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!isNewRecord)
				{
					isNewRecord = true;
					ClearForm();
				}
				return true;
			}
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
			if (!allowEdit)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
				return false;
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
				bool flag = Factory.JobSystem.CreateJobFeeDetails(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
					base.DialogResult = DialogResult.No;
				}
				else
				{
					base.DialogResult = DialogResult.Yes;
					TotalAmount = _totalAmount;
					RetentionDays = retentionDays;
					if (clearAfter)
					{
						ClearForm();
					}
					else
					{
						formManager.ResetDirty();
					}
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
			for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
			{
				if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
				{
					dataGridItems.Rows[i].Delete(displayPrompt: false);
					continue;
				}
				if (dataGridItems.Rows[i].Cells["Fee Code"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					dataGridItems.Rows[i].Activate();
					return false;
				}
				if (dataGridItems.Rows[i].Cells["Amount"].Value == null || dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter amount for all rows.");
					dataGridItems.Rows[i].Activate();
					return false;
				}
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one row of item.");
				return false;
			}
			return true;
		}

		private void ClearForm()
		{
			try
			{
				allowEdit = true;
				comboBoxGridItem.Clear();
				isDiscountPercent = false;
				isDiscountPercent = false;
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
			}
		}

		private bool Delete()
		{
			try
			{
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord);
				_ = 7;
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

		public void OnActivated()
		{
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			dataGridItems.SaveLayout();
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				SetupGrid();
				LoadData();
				ClearForm();
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				comboBoxGridItem.IsLoadingData = false;
			}
		}

		private void CalculateTotal()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal d2 = default(decimal);
			decimal num3 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
					d += result;
				}
			}
			num3 = d + d2;
			if (isDiscountPercent && num2 != 0m)
			{
				num = Math.Round(num3 * num2 / 100m, Global.CurDecimalPoints);
			}
			else if (d > 0m)
			{
				num2 = Math.Round(num / num3 * 100m, Global.CurDecimalPoints);
			}
			num3 = d + d2 - num;
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Fee Code"].Value != null && dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Fee Code"].Value != null && dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Fee Code"].Value != null && dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString() != "")
			{
				dataGridItems.ActiveRow.Cells["Fee Code"].Value.ToString();
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
				if (IsDirty && saveChanges && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && !SaveData(clearAfter: false))
				{
					base.DialogResult = DialogResult.None;
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PurchaseQuoteListFormObj);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJobFee(comboBoxRetention.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJobFee(comboBoxAdvanceItem.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.JobFeeDetailForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelDetails = new System.Windows.Forms.Panel();
			textBoxProjectCode = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxProjectName = new System.Windows.Forms.TextBox();
			textBoxEntityCode = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxEntityName = new System.Windows.Forms.TextBox();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			comboBoxGridItem = new Micromind.DataControls.JobFeeComboBox();
			textBoxRetentionName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxRetention = new Micromind.DataControls.JobFeeComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			label5 = new System.Windows.Forms.Label();
			textBoxRetentionDays = new Micromind.UISupport.NumberTextBox();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxRetentionPercent = new Micromind.UISupport.PercentTextBox();
			groupBoxFees = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxAdvanceAmount = new Micromind.UISupport.AmountTextBox();
			label6 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAdvanceDescription = new System.Windows.Forms.TextBox();
			comboBoxAdvanceItem = new Micromind.DataControls.JobFeeComboBox();
			dateTimePickerRetDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel38 = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRetention).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxFees).BeginInit();
			groupBoxFees.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvanceItem).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(770, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 506);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(770, 40);
			panelButtons.TabIndex = 4;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(770, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(660, 6);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(558, 6);
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
			dataGridItems.Location = new System.Drawing.Point(12, 19);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(742, 200);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			panelDetails.Controls.Add(textBoxProjectCode);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxProjectName);
			panelDetails.Controls.Add(textBoxEntityCode);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxEntityName);
			panelDetails.Location = new System.Drawing.Point(0, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(749, 65);
			panelDetails.TabIndex = 0;
			textBoxProjectCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCode.Location = new System.Drawing.Point(98, 33);
			textBoxProjectCode.MaxLength = 64;
			textBoxProjectCode.Name = "textBoxProjectCode";
			textBoxProjectCode.ReadOnly = true;
			textBoxProjectCode.Size = new System.Drawing.Size(105, 20);
			textBoxProjectCode.TabIndex = 148;
			textBoxProjectCode.TabStop = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 36);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(43, 13);
			label2.TabIndex = 147;
			label2.Text = "Project:";
			textBoxProjectName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectName.Location = new System.Drawing.Point(209, 33);
			textBoxProjectName.MaxLength = 64;
			textBoxProjectName.Name = "textBoxProjectName";
			textBoxProjectName.ReadOnly = true;
			textBoxProjectName.Size = new System.Drawing.Size(293, 20);
			textBoxProjectName.TabIndex = 146;
			textBoxProjectName.TabStop = false;
			textBoxEntityCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityCode.Location = new System.Drawing.Point(99, 10);
			textBoxEntityCode.MaxLength = 64;
			textBoxEntityCode.Name = "textBoxEntityCode";
			textBoxEntityCode.ReadOnly = true;
			textBoxEntityCode.Size = new System.Drawing.Size(105, 20);
			textBoxEntityCode.TabIndex = 145;
			textBoxEntityCode.TabStop = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(14, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(54, 13);
			label4.TabIndex = 140;
			label4.Text = "Customer:";
			textBoxEntityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEntityName.Location = new System.Drawing.Point(210, 10);
			textBoxEntityName.MaxLength = 64;
			textBoxEntityName.Name = "textBoxEntityName";
			textBoxEntityName.ReadOnly = true;
			textBoxEntityName.Size = new System.Drawing.Size(293, 20);
			textBoxEntityName.TabIndex = 6;
			textBoxEntityName.TabStop = false;
			comboBoxGridProductUnit.Assigned = false;
			comboBoxGridProductUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit.CustomReportFieldName = "";
			comboBoxGridProductUnit.CustomReportKey = "";
			comboBoxGridProductUnit.CustomReportValueType = 1;
			comboBoxGridProductUnit.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance13;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridProductUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit.Editable = true;
			comboBoxGridProductUnit.FilterString = "";
			comboBoxGridProductUnit.IsDataLoaded = false;
			comboBoxGridProductUnit.Location = new System.Drawing.Point(668, 59);
			comboBoxGridProductUnit.MaxDropDownItems = 12;
			comboBoxGridProductUnit.Name = "comboBoxGridProductUnit";
			comboBoxGridProductUnit.Size = new System.Drawing.Size(101, 20);
			comboBoxGridProductUnit.TabIndex = 119;
			comboBoxGridProductUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator3,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 120);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance25;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridItem.Editable = true;
			comboBoxGridItem.FilteredJobID = null;
			comboBoxGridItem.FilterString = "";
			comboBoxGridItem.HasAllAccount = false;
			comboBoxGridItem.HasCustom = false;
			comboBoxGridItem.IsDataLoaded = false;
			comboBoxGridItem.Location = new System.Drawing.Point(668, 33);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(101, 20);
			comboBoxGridItem.TabIndex = 120;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			textBoxRetentionName.BackColor = System.Drawing.Color.White;
			textBoxRetentionName.Location = new System.Drawing.Point(239, 19);
			textBoxRetentionName.MaxLength = 64;
			textBoxRetentionName.Name = "textBoxRetentionName";
			textBoxRetentionName.Size = new System.Drawing.Size(353, 20);
			textBoxRetentionName.TabIndex = 150;
			appearance37.FontData.BoldAsString = "False";
			appearance37.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance37;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(15, 21);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(81, 15);
			ultraFormattedLinkLabel1.TabIndex = 151;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Retention Item:";
			appearance38.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance38;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxRetention.Assigned = false;
			comboBoxRetention.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRetention.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRetention.CustomReportFieldName = "";
			comboBoxRetention.CustomReportKey = "";
			comboBoxRetention.CustomReportValueType = 1;
			comboBoxRetention.DescriptionTextBox = textBoxRetentionName;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRetention.DisplayLayout.Appearance = appearance39;
			comboBoxRetention.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRetention.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRetention.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRetention.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxRetention.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRetention.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxRetention.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRetention.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRetention.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRetention.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxRetention.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRetention.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRetention.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRetention.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxRetention.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRetention.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRetention.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxRetention.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxRetention.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRetention.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxRetention.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxRetention.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRetention.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxRetention.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRetention.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRetention.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRetention.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRetention.Editable = true;
			comboBoxRetention.FilteredJobID = null;
			comboBoxRetention.FilteredType = Micromind.Common.Data.JobFeeTypes.Retention;
			comboBoxRetention.FilterString = "";
			comboBoxRetention.HasAllAccount = false;
			comboBoxRetention.HasCustom = false;
			comboBoxRetention.IsDataLoaded = false;
			comboBoxRetention.Location = new System.Drawing.Point(104, 19);
			comboBoxRetention.MaxDropDownItems = 12;
			comboBoxRetention.Name = "comboBoxRetention";
			comboBoxRetention.ShowInactiveItems = false;
			comboBoxRetention.ShowQuickAdd = true;
			comboBoxRetention.Size = new System.Drawing.Size(131, 20);
			comboBoxRetention.TabIndex = 149;
			comboBoxRetention.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(dateTimePickerRetDate);
			ultraGroupBox1.Controls.Add(mmLabel38);
			ultraGroupBox1.Controls.Add(label5);
			ultraGroupBox1.Controls.Add(textBoxRetentionDays);
			ultraGroupBox1.Controls.Add(label3);
			ultraGroupBox1.Controls.Add(label1);
			ultraGroupBox1.Controls.Add(textBoxRetentionPercent);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox1.Controls.Add(textBoxRetentionName);
			ultraGroupBox1.Controls.Add(comboBoxRetention);
			ultraGroupBox1.Location = new System.Drawing.Point(0, 98);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(764, 74);
			ultraGroupBox1.TabIndex = 152;
			ultraGroupBox1.Text = "Retentions";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(213, 45);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(85, 13);
			label5.TabIndex = 155;
			label5.Text = "Retension Days:";
			textBoxRetentionDays.AllowDecimal = false;
			textBoxRetentionDays.BackColor = System.Drawing.Color.White;
			textBoxRetentionDays.CustomReportFieldName = "";
			textBoxRetentionDays.CustomReportKey = "";
			textBoxRetentionDays.CustomReportValueType = 1;
			textBoxRetentionDays.ForeColor = System.Drawing.Color.Black;
			textBoxRetentionDays.IsComboTextBox = false;
			textBoxRetentionDays.IsModified = false;
			textBoxRetentionDays.Location = new System.Drawing.Point(304, 42);
			textBoxRetentionDays.MaxValue = new decimal(new int[4]
			{
				365,
				0,
				0,
				0
			});
			textBoxRetentionDays.MinValue = new decimal(new int[4]);
			textBoxRetentionDays.Name = "textBoxRetentionDays";
			textBoxRetentionDays.NullText = "0";
			textBoxRetentionDays.Size = new System.Drawing.Size(79, 20);
			textBoxRetentionDays.TabIndex = 156;
			textBoxRetentionDays.Text = "0";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(195, 45);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(15, 13);
			label3.TabIndex = 153;
			label3.Text = "%";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(14, 45);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(46, 13);
			label1.TabIndex = 153;
			label1.Text = "Amount:";
			textBoxRetentionPercent.CustomReportFieldName = "";
			textBoxRetentionPercent.CustomReportKey = "";
			textBoxRetentionPercent.CustomReportValueType = 1;
			textBoxRetentionPercent.IsComboTextBox = false;
			textBoxRetentionPercent.IsModified = false;
			textBoxRetentionPercent.Location = new System.Drawing.Point(104, 42);
			textBoxRetentionPercent.MaxLength = 5;
			textBoxRetentionPercent.Name = "textBoxRetentionPercent";
			textBoxRetentionPercent.Size = new System.Drawing.Size(88, 20);
			textBoxRetentionPercent.TabIndex = 152;
			textBoxRetentionPercent.Text = "0";
			textBoxRetentionPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			groupBoxFees.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			groupBoxFees.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			groupBoxFees.Controls.Add(dataGridItems);
			groupBoxFees.Controls.Add(comboBoxGridItem);
			groupBoxFees.Controls.Add(comboBoxGridProductUnit);
			groupBoxFees.Location = new System.Drawing.Point(0, 264);
			groupBoxFees.Name = "groupBoxFees";
			groupBoxFees.Size = new System.Drawing.Size(754, 236);
			groupBoxFees.TabIndex = 153;
			groupBoxFees.Text = "Project Fees";
			ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox3.Controls.Add(textBoxAdvanceAmount);
			ultraGroupBox3.Controls.Add(label6);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox3.Controls.Add(textBoxAdvanceDescription);
			ultraGroupBox3.Controls.Add(comboBoxAdvanceItem);
			ultraGroupBox3.Location = new System.Drawing.Point(0, 168);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(764, 74);
			ultraGroupBox3.TabIndex = 154;
			ultraGroupBox3.Text = "Advances";
			textBoxAdvanceAmount.AllowDecimal = true;
			textBoxAdvanceAmount.CustomReportFieldName = "";
			textBoxAdvanceAmount.CustomReportKey = "";
			textBoxAdvanceAmount.CustomReportValueType = 1;
			textBoxAdvanceAmount.IsComboTextBox = false;
			textBoxAdvanceAmount.IsModified = false;
			textBoxAdvanceAmount.Location = new System.Drawing.Point(104, 42);
			textBoxAdvanceAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAdvanceAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAdvanceAmount.Name = "textBoxAdvanceAmount";
			textBoxAdvanceAmount.NullText = "0";
			textBoxAdvanceAmount.Size = new System.Drawing.Size(100, 20);
			textBoxAdvanceAmount.TabIndex = 154;
			textBoxAdvanceAmount.Text = "0.00";
			textBoxAdvanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAdvanceAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(14, 45);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(46, 13);
			label6.TabIndex = 153;
			label6.Text = "Amount:";
			appearance51.FontData.BoldAsString = "False";
			appearance51.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance51;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(15, 21);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(76, 15);
			ultraFormattedLinkLabel2.TabIndex = 151;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Advance Item:";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance52;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxAdvanceDescription.BackColor = System.Drawing.Color.White;
			textBoxAdvanceDescription.Location = new System.Drawing.Point(239, 19);
			textBoxAdvanceDescription.MaxLength = 64;
			textBoxAdvanceDescription.Name = "textBoxAdvanceDescription";
			textBoxAdvanceDescription.Size = new System.Drawing.Size(321, 20);
			textBoxAdvanceDescription.TabIndex = 150;
			comboBoxAdvanceItem.Assigned = false;
			comboBoxAdvanceItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAdvanceItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAdvanceItem.CustomReportFieldName = "";
			comboBoxAdvanceItem.CustomReportKey = "";
			comboBoxAdvanceItem.CustomReportValueType = 1;
			comboBoxAdvanceItem.DescriptionTextBox = textBoxAdvanceDescription;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAdvanceItem.DisplayLayout.Appearance = appearance53;
			comboBoxAdvanceItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAdvanceItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAdvanceItem.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAdvanceItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxAdvanceItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAdvanceItem.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxAdvanceItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAdvanceItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAdvanceItem.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAdvanceItem.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxAdvanceItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAdvanceItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAdvanceItem.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAdvanceItem.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxAdvanceItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAdvanceItem.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAdvanceItem.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxAdvanceItem.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxAdvanceItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAdvanceItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxAdvanceItem.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxAdvanceItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAdvanceItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxAdvanceItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAdvanceItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAdvanceItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAdvanceItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAdvanceItem.Editable = true;
			comboBoxAdvanceItem.FilteredJobID = null;
			comboBoxAdvanceItem.FilteredType = Micromind.Common.Data.JobFeeTypes.Advance;
			comboBoxAdvanceItem.FilterString = "";
			comboBoxAdvanceItem.HasAllAccount = false;
			comboBoxAdvanceItem.HasCustom = false;
			comboBoxAdvanceItem.IsDataLoaded = false;
			comboBoxAdvanceItem.Location = new System.Drawing.Point(104, 19);
			comboBoxAdvanceItem.MaxDropDownItems = 12;
			comboBoxAdvanceItem.Name = "comboBoxAdvanceItem";
			comboBoxAdvanceItem.ShowInactiveItems = false;
			comboBoxAdvanceItem.ShowQuickAdd = true;
			comboBoxAdvanceItem.Size = new System.Drawing.Size(131, 20);
			comboBoxAdvanceItem.TabIndex = 149;
			comboBoxAdvanceItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerRetDate.Checked = false;
			dateTimePickerRetDate.CustomFormat = " ";
			dateTimePickerRetDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerRetDate.Location = new System.Drawing.Point(474, 42);
			dateTimePickerRetDate.Name = "dateTimePickerRetDate";
			dateTimePickerRetDate.ShowCheckBox = true;
			dateTimePickerRetDate.Size = new System.Drawing.Size(118, 20);
			dateTimePickerRetDate.TabIndex = 160;
			dateTimePickerRetDate.Value = new System.DateTime(0L);
			mmLabel38.AutoSize = true;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(389, 46);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(82, 13);
			mmLabel38.TabIndex = 161;
			mmLabel38.Text = "Retention Date:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(770, 546);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(groupBoxFees);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "JobFeeDetailForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Project Fees";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRetention).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxFees).EndInit();
			groupBoxFees.ResumeLayout(false);
			groupBoxFees.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvanceItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
