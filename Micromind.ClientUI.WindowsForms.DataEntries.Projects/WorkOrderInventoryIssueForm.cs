using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class WorkOrderInventoryIssueForm : Form, IForm
	{
		private bool supressInventoryMessage;

		private WorkOrderInventoryIssueData currentData;

		private const string TABLENAME_CONST = "EA_WorkOrder_Inventory_Issue";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private ItemSourceTypes sourceDocType;

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

		private int sourceSysDocType;

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

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private ProductComboBox comboBoxGridItem;

		private ProductUnitComboBox comboBoxGridProductUnit;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem saveADraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private Label label2;

		private TextBox textBoxRequestedBy;

		private LocationComboBox comboBoxGridLocation;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private ToolStripButton toolStripButtonDistribution;

		private TextBox textBoxRef2;

		private Label label4;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private LocationComboBox comboBoxLocation;

		private Label label5;

		private XPButton buttonSelectDocument;

		private MMTextBox textBoxWorkOrder;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
				}
				toolStripButtonDistribution.Enabled = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
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

		public int SourceSysDocType
		{
			get
			{
				return sourceSysDocType;
			}
			set
			{
				sourceSysDocType = value;
			}
		}

		public string SourceSysDocID
		{
			get
			{
				return sourceSysDocID;
			}
			set
			{
				sourceSysDocID = value;
			}
		}

		public string SourceVoucherID
		{
			get
			{
				return sourceVoucherID;
			}
			set
			{
				sourceVoucherID = value;
			}
		}

		public WorkOrderInventoryIssueForm()
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
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxGridProductUnit.SelectedIndexChanged += comboBoxGridProductUnit_SelectedIndexChanged;
			comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result = -1m;
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			checked
			{
				if (activeRow != null && activeRow.Cells["Item Code"].Value != null && !(activeRow.Cells["Item Code"].Value.ToString() == "") && activeRow != null && activeRow.DataChanged && activeRow.Cells["ItemType"].Value.ToString() != "")
				{
					_ = (byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString());
				}
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
			FillProductQuantityAndCost();
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillProductQuantityAndCost();
		}

		private void FillProductQuantityAndCost()
		{
			try
			{
				if (dataGridItems.Rows.Count != 0)
				{
					ArrayList arrayList = new ArrayList();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["Item Code"].Value != null && !(row.Cells["Item Code"].Value.ToString() == ""))
						{
							string value = row.GetCellValue("Item Code").ToString();
							arrayList.Add(value);
						}
					}
					DateTime date = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 23, 59, 59);
					DataSet productQuantityAndCostAsOfDate = Factory.ProductSystem.GetProductQuantityAndCostAsOfDate((string[])arrayList.ToArray(typeof(string)), comboBoxLocation.SelectedID, date);
					DataTable dataTable = null;
					if (productQuantityAndCostAsOfDate != null && productQuantityAndCostAsOfDate.Tables.Count > 0)
					{
						dataTable = productQuantityAndCostAsOfDate.Tables[0];
					}
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						if (row2.Cells["Item Code"].Value != null && !(row2.Cells["Item Code"].Value.ToString() == ""))
						{
							string str = row2.Cells["Item Code"].Value.ToString();
							DataRow[] array = null;
							if (dataTable != null)
							{
								array = dataTable.Select("ProductID = '" + str + "'");
							}
							if (array != null && array.Length != 0)
							{
								_ = array[0];
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
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

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip2.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null)
				{
					if (!(e.Cell.Column.Key == "Item Code"))
					{
						goto IL_03f1;
					}
					ItemTypes itemTypes = ItemTypes.None;
					if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes != ItemTypes.Matrix)
					{
						dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
						dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
						if (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == "")
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
						}
						if (e.Cell.Row.Index > 0 && (dataGridItems.ActiveRow.Cells["LocationID"].Value == null || dataGridItems.ActiveRow.Cells["LocationID"].Value.ToString() == ""))
						{
							dataGridItems.ActiveRow.Cells["LocationID"].Value = dataGridItems.Rows[checked(dataGridItems.ActiveRow.Index - 1)].Cells["LocationID"].Value;
						}
						goto IL_03f1;
					}
					supressInventoryMessage = true;
					dataGridItems.ActiveRow.Cells["JobID"].Value.ToString();
					dataGridItems.ActiveRow.Cells["CostCategoryID"].Value.ToString();
					MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
					matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
					dataGridItems.ActiveRow.Delete(displayPrompt: false);
					if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
					{
						foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
						{
							UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
							ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
							ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
							ultraGridRow.Cells["ItemType"].Value = ItemTypes.Inventory;
							ultraGridRow.Cells["Quantity"].Value = row["Quantity"].ToString();
							ultraGridRow.Update();
							decimal result = default(decimal);
							decimal.TryParse(ultraGridRow.Cells["Quantity"].Value.ToString(), out result);
						}
					}
					supressInventoryMessage = false;
				}
				goto end_IL_0000;
				IL_03f1:
				checked
				{
					if (e.Cell.Column.Key == "LocationID")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["LocationID"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["LocationID"].Value = e.Cell.Value.ToString();
							}
						}
					}
					if (e.Cell.Column.Key == "Quantity" && (dataGridItems.DataSource as DataTable).Columns.Contains("Issued"))
					{
						int num = e.Cell.Row.Index;
						decimal num2;
						decimal num3;
						while (true)
						{
							if (num >= dataGridItems.Rows.Count)
							{
								return;
							}
							num2 = default(decimal);
							num3 = default(decimal);
							num2 = decimal.Parse(dataGridItems.Rows[num].Cells["Issued"].Value.ToString());
							num3 = decimal.Parse(dataGridItems.Rows[num].Cells["Requested"].Value.ToString());
							if (decimal.Parse(dataGridItems.Rows[num].Cells["Quantity"].Value.ToString()) > num3 - num2)
							{
								break;
							}
							num++;
						}
						ErrorHelper.InformationMessage("Quantity Should not be greater than " + (num3 - num2).ToString());
						dataGridItems.Rows[num].Cells["Quantity"].Activate();
					}
				}
				end_IL_0000:;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			checked
			{
				if (activeRow != null && IsNewRecord)
				{
					if (dataGridItems.ActiveRow.Index > 0)
					{
						activeRow.Cells["JobID"].Value = (dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["JobID"].Value ?? null);
					}
					if (dataGridItems.ActiveRow.Index > 0)
					{
						activeRow.Cells["CostCategoryID"].Value = (dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["CostCategoryID"].Value ?? null);
					}
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && activeRow.Cells["Item Code"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				activeRow.Cells["Item Code"].Activate();
			}
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.Clear();
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			ItemTypes itemTypes = ItemTypes.Inventory;
			if (e.Cell.Column.Key == "Item Code")
			{
				itemTypes = comboBoxGridItem.SelectedItemType;
				if (itemTypes != ItemTypes.Inventory && itemTypes != ItemTypes.Matrix && itemTypes != ItemTypes.Assembly)
				{
					ErrorHelper.InformationMessage("This item type is not allowed in this document.");
					e.Cancel = true;
					return;
				}
			}
			if (activeRow != null && activeRow.DataChanged && activeRow.IsAddRow && e.Cell.Column.Key == "Quantity" && activeRow.Cells["ItemType"].Value.ToString() != "")
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString()));
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Item Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridItem.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridItem.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid location for the item.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new WorkOrderInventoryIssueData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.WorkOrderInventoryIssueTable.Rows[0] : currentData.WorkOrderInventoryIssueTable.NewRow();
				if (dateTimePickerDate.Value > DateTime.Today)
				{
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
				}
				else
				{
					DateTime value = dateTimePickerDate.Value;
					dataRow["TransactionDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["RequestedBy"] = textBoxRequestedBy.Text;
				dataRow["SourceSysDocType"] = SourceSysDocType;
				dataRow["SourceSysDocID"] = SourceSysDocID;
				dataRow["SourceVoucherID"] = SourceVoucherID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.WorkOrderInventoryIssueTable.Rows.Add(dataRow);
				}
				currentData.WorkOrderInventoryIssueDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.WorkOrderInventoryIssueDetailTable.NewRow();
					dataRow2.BeginEdit();
					decimal d = default(decimal);
					if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
					{
						d = decimal.Parse(row.Cells["Quantity"].Value.ToString());
					}
					if (!(d == 0m))
					{
						dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow2["JobID"] = row.Cells["JobID"].Value.ToString();
						dataRow2["CostCategoryID"] = row.Cells["CostCategoryID"].Value.ToString();
						dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
						dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
						dataRow2["Description"] = row.Cells["Description"].Value.ToString();
						dataRow2["RowIndex"] = row.Index;
						dataRow2["LocationID"] = row.Cells["LocationID"].Value.ToString();
						if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
						{
							dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
						}
						if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SourceSysDocID"))
						{
							if (row.Cells["SourceSysDocID"].Value.ToString() != "")
							{
								dataRow2["SourceSysDocID"] = row.Cells["SourceSysDocID"].Value.ToString();
							}
							if (row.Cells["SourceVoucherID"].Value.ToString() != "")
							{
								dataRow2["SourceVoucherID"] = row.Cells["SourceVoucherID"].Value.ToString();
							}
							if (row.Cells["SourceRowIndex"].Value.ToString() != "")
							{
								dataRow2["SourceRowIndex"] = row.Cells["SourceRowIndex"].Value.ToString();
							}
						}
						dataRow2.EndEdit();
						currentData.WorkOrderInventoryIssueDetailTable.Rows.Add(dataRow2);
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

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("JOBID");
				dataTable.Columns.Add("CostCategoryID");
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("ItemType", typeof(byte));
				dataTable.Columns.Add("LocationID");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("SourceRowIndex", typeof(int));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["JobID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["JobID"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["JobID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["JobID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["JobID"].ValueList = comboBoxJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["JobID"].Header.Caption = "Project";
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].MaxLength = 30;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].ValueList = comboBoxCostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Header.Caption = "Cost Category";
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["LocationID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["LocationID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["LocationID"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["LocationID"].ValueList = comboBoxLocation;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["JOBID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "#,0.#####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(18 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			dataGridItems.DisplayLayout.Bands[0].Columns["Stock inHand"].CellActivation = Activation.NoEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Issued"].CellActivation = Activation.NoEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Requested"].CellActivation = Activation.NoEdit;
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
					currentData = Factory.WorkOrderInventoryIssueSystem.GetWorkOrderInventoryIssueByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						FillData();
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
					DataRow dataRow = currentData.Tables["EA_WorkOrder_Inventory_Issue"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxWorkOrder.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxRequestedBy.Text = dataRow["RequestedBy"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("EA_WorkOrder_Inventory_Issue_Detail") && currentData.WorkOrderInventoryIssueDetailTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["EA_WorkOrder_Inventory_Issue_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["JobID"] = row["JobID"];
							dataRow3["CostCategoryID"] = row["CostCategoryID"];
							dataRow3["Item Code"] = row["ProductID"];
							dataRow3["Quantity"] = row["Quantity"];
							dataRow3["ItemType"] = row["ItemType"];
							dataRow3["Description"] = row["Description"];
							dataRow3["LocationID"] = row["LocationID"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["SourceSysDocID"] = row["SourceSysDocID"];
							dataRow3["SourceVoucherID"] = row["SourceVoucherID"];
							dataRow3["SourceRowIndex"] = row["SourceRowIndex"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.EndUpdate();
						FillProductQuantityAndCost();
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
				bool flag = Factory.WorkOrderInventoryIssueSystem.CreateWorkOrderInventoryIssue(currentData, !isNewRecord);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("EA_WorkOrder_Inventory_Issue", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			DateTime value = dateTimePickerDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
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
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else
					{
						if (dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please select an item.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (dataGridItems.Rows[i].Cells["LocationID"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please select a location for all the rows.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
					}
					if ((dataGridItems.DataSource as DataTable).Columns.Contains("Issued"))
					{
						decimal num2 = default(decimal);
						decimal num3 = default(decimal);
						num2 = decimal.Parse(dataGridItems.Rows[i].Cells["Issued"].Value.ToString());
						num3 = decimal.Parse(dataGridItems.Rows[i].Cells["Requested"].Value.ToString());
						if (decimal.Parse(dataGridItems.Rows[i].Cells["Quantity"].Value.ToString()) > num3 - num2)
						{
							ErrorHelper.InformationMessage("Quantity Should not be greater than " + (num3 - num2).ToString());
							dataGridItems.Rows[i].Cells["Quantity"].Activate();
							return false;
						}
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one item row.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("EA_WorkOrder_Inventory_Issue", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				return true;
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.GetCellValue("Quantity") != null && row.GetCellValue("Quantity").ToString() != "")
				{
					result += decimal.Parse(row.Cells["Quantity"].Value.ToString());
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
				textBoxRef2.Clear();
				textBoxRequestedBy.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxWorkOrder.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (dataTable.Columns.Contains("Stock inHand"))
				{
					dataTable.Columns.Remove("Stock inHand");
					dataTable.Columns.Remove("Issued");
					dataTable.Columns.Remove("Requested");
				}
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
				return Factory.WorkOrderInventoryIssueSystem.DeleteWorkOrderInventoryIssue(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("EA_WorkOrder_Inventory_Issue", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("EA_WorkOrder_Inventory_Issue", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("EA_WorkOrder_Inventory_Issue", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("EA_WorkOrder_Inventory_Issue", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("EA_WorkOrder_Inventory_Issue", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.WorkOrderInventoryIssue);
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.WorkOrderInventoryIssue);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.JobInventoryIssue);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 71.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.JobInventoryIssue);
					currentData = (dataSet as WorkOrderInventoryIssueData);
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
					if (selectedID == "" || text == "")
					{
						ErrorHelper.InformationMessage("Please select a document to print!");
					}
					else
					{
						DataSet workOrderInventoryIssueToPrint = Factory.WorkOrderInventoryIssueSystem.GetWorkOrderInventoryIssueToPrint(selectedID, text);
						if (workOrderInventoryIssueToPrint == null || workOrderInventoryIssueToPrint.Tables.Count == 0)
						{
							ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
						}
						else
						{
							PrintHelper.PrintDocument(workOrderInventoryIssueToPrint, selectedID, "Work Order Inventory Issue", SysDocTypes.WorkOrderInventoryIssue, isPrint, showPrintDialog);
						}
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
			return "Job Inventory Issue";
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

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void loadFromMRToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet workOrderAll = Factory.EquipmentWorkOrderSystem.GetWorkOrderAll();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = workOrderAll;
			selectDocumentDialog.Text = "Select Work Order";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			EquipmentWorkOrderData equipmentWorkOrderByID = Factory.EquipmentWorkOrderSystem.GetEquipmentWorkOrderByID(text, text2);
			DataRow dataRow = equipmentWorkOrderByID.WorkOrderTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			sourceDocType = ItemSourceTypes.WorkOrder;
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (equipmentWorkOrderByID.Tables.Contains("EA_Work_Order") && equipmentWorkOrderByID.WorkOrderTable.Rows.Count != 0)
			{
				if (!dataTable.Columns.Contains("Stock inHand"))
				{
					dataTable.Columns.Remove("Quantity");
					dataTable.Columns.Add("Stock inHand", typeof(decimal));
					dataTable.Columns.Add("Requested", typeof(decimal));
					dataTable.Columns.Add("Issued", typeof(decimal));
					dataTable.Columns.Add("Quantity", typeof(decimal));
					AdjustGridColumnSettings();
				}
				foreach (DataRow row in equipmentWorkOrderByID.Tables["EA_WorkOrder_Resources_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["ProductID"];
					dataRow3["Description"] = row["Description"];
					if (row["Quantity"] != DBNull.Value)
					{
						dataRow3["Requested"] = row["Quantity"];
					}
					else
					{
						dataRow3["Requested"] = row["Quantity"];
					}
					dataRow3["JobID"] = row["JobID"];
					dataRow3["LocationID"] = row["LocationID"];
					dataRow3["Unit"] = row["UnitID"];
					dataRow3["SourceSysDocID"] = text;
					dataRow3["SourceVoucherID"] = text2;
					dataRow3["SourceRowIndex"] = row["RowIndex"];
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["IssuedQty"].ToString(), out result);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					if (!(result2 - result <= 0m))
					{
						dataRow3["Quantity"] = result2 - result;
						if (dataRow3["Quantity"].ToString() != "")
						{
							decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
						}
						dataRow3["Stock inHand"] = row["Stock"];
						if (dataRow3["Stock inHand"].ToString() != "")
						{
							decimal.Parse(dataRow3["Stock inHand"].ToString(), NumberStyles.Any);
						}
						dataRow3["Issued"] = result;
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
				}
			}
		}

		private void loadFromGRNToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet purchaseReceiptAll = Factory.PurchaseReceiptSystem.GetPurchaseReceiptAll();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = purchaseReceiptAll;
			selectDocumentDialog.Text = "Select GRN";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				_ = string.Empty;
				string empty = string.Empty;
				if (selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString() != "")
				{
					selectDocumentDialog.SelectedRow.Cells["JobID"].Value.ToString();
				}
				ClearForm();
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				SourceSysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				SourceVoucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				SourceSysDocType = Convert.ToByte(SysDocTypes.JobInventoryIssue);
				PurchaseReceiptData purchaseReceiptByID = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(sysDocID, voucherID);
				DataRow dataRow = purchaseReceiptByID.PurchaseReceiptTable.Rows[0];
				textBoxRef1.Text = dataRow["VoucherID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				sourceDocType = ItemSourceTypes.GRN;
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (purchaseReceiptByID.Tables.Contains("Purchase_Receipt") && purchaseReceiptByID.PurchaseReceiptDetailTable.Rows.Count != 0)
				{
					foreach (DataRow row in purchaseReceiptByID.Tables["Purchase_Receipt_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Item Code"] = row["ProductID"];
						dataRow3["Description"] = row["Description"];
						if (row["Quantity"] != DBNull.Value)
						{
							dataRow3["Quantity"] = row["Quantity"];
						}
						else
						{
							dataRow3["Quantity"] = row["Quantity"];
						}
						dataRow3["JobID"] = row["JobID"];
						dataRow3["CostCategoryID"] = empty;
						dataRow3["LocationID"] = row["LocationID"];
						dataRow3["Unit"] = row["UnitID"];
						if (dataRow3["Quantity"].ToString() != "")
						{
							decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
				}
			}
		}

		private void loadFromPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet purchaseOrderAll = Factory.PurchaseOrderSystem.GetPurchaseOrderAll();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = purchaseOrderAll;
			selectDocumentDialog.Text = "Select Purchase Order";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			_ = string.Empty;
			_ = string.Empty;
			if (dataGridItems.Rows.Count > 0)
			{
				dataGridItems.Rows[0].Cells["JobID"].Value.ToString();
				dataGridItems.Rows[0].Cells["CostCategoryID"].Value.ToString();
			}
			ClearForm();
			string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			SourceSysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			SourceVoucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			SourceSysDocType = Convert.ToByte(SysDocTypes.PurchaseOrder);
			PurchaseOrderData purchaseOrderByID = Factory.PurchaseOrderSystem.GetPurchaseOrderByID(sysDocID, voucherID);
			DataRow dataRow = purchaseOrderByID.PurchaseOrderTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			sourceDocType = ItemSourceTypes.PurchaseOrder;
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (!purchaseOrderByID.Tables.Contains("Purchase_Order_Detail") || purchaseOrderByID.PurchaseOrderDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in purchaseOrderByID.Tables["Purchase_Order_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["JobID"] = row["JobID"];
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["Description"] = row["Description"];
				dataRow3["Unit"] = row["UnitID"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenGRNThenInvoice || CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenInvoice)
			{
				if (!CompanyPreferences.AllowLocalGRNAddNewRow)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
		}

		public void issueToProject(string sysDocID, string voucherID)
		{
			_ = string.Empty;
			string value = string.Empty;
			if (dataGridItems.Rows.Count > 0)
			{
				dataGridItems.Rows[0].Cells["JobID"].Value.ToString();
				value = dataGridItems.Rows[0].Cells["CostCategoryID"].Value.ToString();
			}
			ClearForm();
			PurchaseReceiptData purchaseReceiptByID = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(sysDocID, voucherID);
			DataRow dataRow = purchaseReceiptByID.PurchaseReceiptTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			sourceDocType = ItemSourceTypes.GRN;
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (purchaseReceiptByID.Tables.Contains("Purchase_Receipt") && purchaseReceiptByID.PurchaseReceiptDetailTable.Rows.Count != 0)
			{
				foreach (DataRow row in purchaseReceiptByID.Tables["Purchase_Receipt_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["ProductID"];
					dataRow3["Description"] = row["Description"];
					if (row["Quantity"] != DBNull.Value)
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					else
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					dataRow3["JobID"] = row["JobID"];
					dataRow3["CostCategoryID"] = value;
					dataRow3["LocationID"] = row["LocationID"];
					dataRow3["Unit"] = row["UnitID"];
					if (dataRow3["Quantity"].ToString() != "")
					{
						decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
					}
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.WorkOrderInventoryIssueListFormObj);
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
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

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataGridItems.Left + 20, dataGridItems.Top + 20);
				}
			}
		}

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet workOrderAll = Factory.EquipmentWorkOrderSystem.GetWorkOrderAll();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = workOrderAll;
			selectDocumentDialog.Text = "Select Work Order";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			EquipmentWorkOrderData equipmentWorkOrderByID = Factory.EquipmentWorkOrderSystem.GetEquipmentWorkOrderByID(text, text2);
			if (equipmentWorkOrderByID.Tables.Contains("EA_Work_Order") && equipmentWorkOrderByID.WorkOrderTable.Rows.Count != 0)
			{
				DataRow dataRow = equipmentWorkOrderByID.WorkOrderTable.Rows[0];
				textBoxWorkOrder.Text = dataRow["VoucherID"].ToString();
				textBoxRef1.Text = dataRow["VoucherID"].ToString();
				sourceDocType = ItemSourceTypes.WorkOrder;
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (!dataTable.Columns.Contains("Stock inHand"))
				{
					dataTable.Columns.Remove("Quantity");
					dataTable.Columns.Add("Stock inHand", typeof(decimal));
					dataTable.Columns.Add("Requested", typeof(decimal));
					dataTable.Columns.Add("Issued", typeof(decimal));
					dataTable.Columns.Add("Quantity", typeof(decimal));
					AdjustGridColumnSettings();
				}
				foreach (DataRow row in equipmentWorkOrderByID.Tables["EA_WorkOrder_Resources_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["ProductID"];
					dataRow3["Description"] = row["Description"];
					if (row["Quantity"] != DBNull.Value)
					{
						dataRow3["Requested"] = row["Quantity"];
					}
					else
					{
						dataRow3["Requested"] = row["Quantity"];
					}
					dataRow3["JobID"] = row["JobID"];
					dataRow3["LocationID"] = row["LocationID"];
					dataRow3["Unit"] = row["UnitID"];
					dataRow3["SourceSysDocID"] = text;
					dataRow3["SourceVoucherID"] = text2;
					dataRow3["SourceRowIndex"] = row["RowIndex"];
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["IssuedQty"].ToString(), out result);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					if (!(result2 - result <= 0m))
					{
						dataRow3["Quantity"] = result2 - result;
						if (dataRow3["Quantity"].ToString() != "")
						{
							decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
						}
						dataRow3["Stock inHand"] = row["Stock"];
						if (dataRow3["Stock inHand"].ToString() != "")
						{
							decimal.Parse(dataRow3["Stock inHand"].ToString(), NumberStyles.Any);
						}
						dataRow3["Issued"] = result;
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
				}
			}
		}

		private void WorkOrderInventoryIssueForm_Load(object sender, EventArgs e)
		{
		}

		private void toolStripSeparator7_Click(object sender, EventArgs e)
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.WorkOrderInventoryIssueForm));
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			saveADraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
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
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			textBoxWorkOrder = new Micromind.UISupport.MMTextBox();
			label5 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxRequestedBy = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			contextMenuStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
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
				toolStripSeparator7,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButton1,
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(86, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator7.Click += new System.EventHandler(toolStripSeparator7_Click);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				saveADraftToolStripMenuItem,
				loadDraftToolStripMenuItem
			});
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(55, 28);
			toolStripButton1.Text = "Actions";
			saveADraftToolStripMenuItem.Name = "saveADraftToolStripMenuItem";
			saveADraftToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			saveADraftToolStripMenuItem.Text = "Save as Draft";
			saveADraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Visible = false;
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
			buttonVoid.Location = new System.Drawing.Point(318, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
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
			ultraCalcManager1.ContainingControl = this;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(501, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 20);
			dateTimePickerDate.TabIndex = 4;
			textBoxVoucherNumber.Location = new System.Drawing.Point(312, 3);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(17, 52);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference 1:";
			textBoxRef1.Location = new System.Drawing.Point(93, 49);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(110, 20);
			textBoxRef1.TabIndex = 7;
			textBoxNote.Location = new System.Drawing.Point(93, 72);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(550, 20);
			textBoxNote.TabIndex = 10;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(17, 76);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(203, 5);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel2.TabIndex = 11;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(buttonSelectDocument);
			panelDetails.Controls.Add(textBoxWorkOrder);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxRequestedBy);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(13, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 106);
			panelDetails.TabIndex = 0;
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(280, 25);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 6;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			textBoxWorkOrder.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxWorkOrder.CustomReportFieldName = "";
			textBoxWorkOrder.CustomReportKey = "";
			textBoxWorkOrder.CustomReportValueType = 1;
			textBoxWorkOrder.IsComboTextBox = false;
			textBoxWorkOrder.Location = new System.Drawing.Point(93, 26);
			textBoxWorkOrder.MaxLength = 255;
			textBoxWorkOrder.Name = "textBoxWorkOrder";
			textBoxWorkOrder.ReadOnly = true;
			textBoxWorkOrder.Size = new System.Drawing.Size(184, 20);
			textBoxWorkOrder.TabIndex = 5;
			textBoxWorkOrder.TabStop = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(17, 26);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(65, 13);
			label5.TabIndex = 129;
			label5.Text = "Work Order:";
			textBoxRef2.Location = new System.Drawing.Point(271, 50);
			textBoxRef2.MaxLength = 30;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(125, 20);
			textBoxRef2.TabIndex = 8;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(204, 53);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 128;
			label4.Text = "Reference 2:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(417, 54);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(77, 13);
			label2.TabIndex = 127;
			label2.Text = "Requested By:";
			textBoxRequestedBy.Location = new System.Drawing.Point(501, 50);
			textBoxRequestedBy.MaxLength = 30;
			textBoxRequestedBy.Name = "textBoxRequestedBy";
			textBoxRequestedBy.Size = new System.Drawing.Size(142, 20);
			textBoxRequestedBy.TabIndex = 9;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance3;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(17, 5);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance5;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(93, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(110, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(452, 5);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 10;
			mmLabel1.Text = "Date:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(16, 286);
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
			contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator5,
				removeRowToolStripMenuItem
			});
			contextMenuStrip2.Name = "contextMenuStrip1";
			contextMenuStrip2.Size = new System.Drawing.Size(175, 120);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(171, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(152, 199);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 139;
			productPhotoViewer.Visible = false;
			comboBoxGridProductUnit.AlwaysInEditMode = true;
			comboBoxGridProductUnit.Assigned = false;
			comboBoxGridProductUnit.CalcManager = ultraCalcManager1;
			comboBoxGridProductUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit.CustomReportFieldName = "";
			comboBoxGridProductUnit.CustomReportKey = "";
			comboBoxGridProductUnit.CustomReportValueType = 1;
			comboBoxGridProductUnit.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance17;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxGridProductUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit.Editable = true;
			comboBoxGridProductUnit.FilterString = "";
			comboBoxGridProductUnit.IsDataLoaded = false;
			comboBoxGridProductUnit.Location = new System.Drawing.Point(666, 47);
			comboBoxGridProductUnit.MaxDropDownItems = 12;
			comboBoxGridProductUnit.Name = "comboBoxGridProductUnit";
			comboBoxGridProductUnit.Size = new System.Drawing.Size(74, 20);
			comboBoxGridProductUnit.TabIndex = 120;
			comboBoxGridProductUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.AlwaysInEditMode = true;
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CalcManager = ultraCalcManager1;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance29;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
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
			comboBoxGridItem.Location = new System.Drawing.Point(666, 73);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
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
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance41;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance48;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance50;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance51;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(11, 143);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(715, 301);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxGridLocation.AlwaysInEditMode = true;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CalcManager = ultraCalcManager1;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance53;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(626, 251);
			comboBoxGridLocation.MaxDropDownItems = 12;
			comboBoxGridLocation.Name = "comboBoxGridLocation";
			comboBoxGridLocation.ShowAll = false;
			comboBoxGridLocation.ShowConsignIn = false;
			comboBoxGridLocation.ShowConsignOut = false;
			comboBoxGridLocation.ShowDefaultLocationOnly = false;
			comboBoxGridLocation.ShowInactiveItems = false;
			comboBoxGridLocation.ShowNormalLocations = true;
			comboBoxGridLocation.ShowPOSOnly = false;
			comboBoxGridLocation.ShowQuickAdd = true;
			comboBoxGridLocation.ShowWarehouseOnly = false;
			comboBoxGridLocation.Size = new System.Drawing.Size(114, 20);
			comboBoxGridLocation.TabIndex = 123;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CalcManager = ultraCalcManager1;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(640, 199);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(100, 20);
			comboBoxCostCategory.TabIndex = 125;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCostCategory.Visible = false;
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CalcManager = ultraCalcManager1;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = null;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(640, 173);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 124;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance65;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance72;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance74;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance75;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(641, 266);
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
			comboBoxLocation.Size = new System.Drawing.Size(100, 20);
			comboBoxLocation.TabIndex = 140;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.Visible = false;
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(740, 490);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(comboBoxGridProductUnit);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(comboBoxCostCategory);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(comboBoxLocation);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "WorkOrderInventoryIssueForm";
			Text = "WorkOrder Inventory Issue";
			base.Load += new System.EventHandler(WorkOrderInventoryIssueForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			contextMenuStrip2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
