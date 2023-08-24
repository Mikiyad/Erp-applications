using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class QualityClaimForm : Form, IForm
	{
		private bool allowEdit = true;

		private QualityClaimData currentData;

		private const string TABLENAME_CONST = "Quality_Claim";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private ItemSourceTypes sourceDocType;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isTaxPercent;

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

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private Panel panelClaim;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxVendorName;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem purchaseStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private JobComboBox comboBoxJob;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator6;

		private Label label19;

		private TextBox textBoxDescription;

		private DateTimePicker dateTimePickerInspectionDate;

		private Panel panel1;

		private TextBox textBoxTotalAmount;

		private Label labelIssue1Name;

		private ProductCategoryComboBox comboBoxCategory;

		private ProductStyleComboBox comboBoxVariety;

		private ProductBrandComboBox comboBoxBrand;

		private ComboBox comboBoxStatus;

		private Label label2;

		private Label label8;

		private DateTimePicker dateTimePickerSurveyDate;

		private Label label7;

		private Label label5;

		private Label label4;

		private ComboBox comboBoxSurveyType;

		private TextBox textBoxCreditNote;

		private Label label6;

		private Label label3;

		private Panel panelArrivalReport;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private XPButton buttonSelect;

		private TextBox textBoxArrivalVoucherID;

		private Label label11;

		private Label label16;

		private Label label21;

		private TextBox textBoxVesselName;

		private TextBox textBoxContainerNo;

		private Label label22;

		private Label label23;

		private Label label24;

		private Label label25;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private vendorsFlatComboBox vendorsFlatComboBox1;

		private TextBox textBoxvendorsFlat;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxReceivedAmount;

		private SurveyorComboBox comboBoxSurveyor1;

		private SurveyorComboBox comboBoxSurveyor2;

		private TextBox textBoxArrivalReportSysDocID;

		private Panel panelSurveyor1;

		private Panel panelSurveyor2;

		private TextBox textBoxReceiveDate;

		private TextBox textBoxOrigin;

		private TextBox textBoxContainerTemp;

		private TextBox textBoxPalletsCount;

		private TextBox textBoxQuantity;

		private vendorsFlatComboBox comboBoxVendor;

		private CurrencySelector comboBoxCurrency;

		private Label labelCurrency;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator3;

		private Label label9;

		private TextBox textBoxBatchNo;

		public ScreenAreas ScreenArea => ScreenAreas.Purchases;

		public int ScreenID => 3010;

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
					buttonDelete.Enabled = false;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool enabled = textBoxVoucherNumber.Enabled = true;
					sysDocComboBox.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool enabled = textBoxVoucherNumber.Enabled = false;
					sysDocComboBox2.Enabled = enabled;
				}
				buttonSelect.Enabled = value;
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
					panelClaim.Enabled = !value;
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
					panel1.Enabled = !value;
				}
			}
		}

		public QualityClaimForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			comboBoxSurveyType.SelectedIndexChanged += comboBoxSurveyType_SelectedIndexChanged;
		}

		private void comboBoxSurveyType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxSurveyType.SelectedIndex == 0)
			{
				Panel panel = panelSurveyor1;
				bool visible = panelSurveyor2.Visible = false;
				panel.Visible = visible;
			}
			else if (comboBoxSurveyType.SelectedIndex == 1)
			{
				panelSurveyor1.Visible = true;
				panelSurveyor2.Visible = false;
			}
			else if (comboBoxSurveyType.SelectedIndex == 0)
			{
				Panel panel2 = panelSurveyor1;
				bool visible = panelSurveyor2.Visible = true;
				panel2.Visible = visible;
			}
		}

		private void AddEvents()
		{
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
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridItems.AfterRowUpdate += dataGridItems_AfterRowUpdate;
			comboBoxVendor.SelectedIndexChanged += comboBoxVendor_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dataGridItems.ClickCellButton += dataGridItems_ClickCellButton;
		}

		private void dataGridItems_ClickCellButton(object sender, CellEventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text;
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityRowIndex = e.Cell.Row.Index;
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

		private void dataGridItems_AfterRowUpdate(object sender, RowEventArgs e)
		{
			CalculateTotals();
		}

		private void CalculateTotals()
		{
		}

		private void comboBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			AdjustGridColumnSettings();
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
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
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVendorName.Text = comboBoxVendor.SelectedName;
			LoadVendorBillingAddress();
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				if (isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
				comboBoxVendor.FilterSysDocID = comboBoxSysDoc.SelectedID;
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					if (!e.Cell.Row.Cells["ReceivedQuantity"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["ReceivedQuantity"].Value.ToString(), out result);
					}
					if (!e.Cell.Row.Cells["IssuePercent"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["IssuePercent"].Value.ToString(), out result2);
					}
					if (!e.Cell.Row.Cells["Quantity"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result3);
					}
					if (!e.Cell.Row.Cells["lossRatio"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["lossRatio"].Value.ToString(), out result4);
					}
					if (!e.Cell.Row.Cells["claimRate"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["claimRate"].Value.ToString(), out result5);
					}
					if (!e.Cell.Row.Cells["unitCost"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["unitCost"].Value.ToString(), out result6);
					}
					if (!e.Cell.Row.Cells["ClaimAmount"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(e.Cell.Row.Cells["ClaimAmount"].Value.ToString(), out result7);
					}
					if (e.Cell.Column.Key == "IssuePercent" && e.Cell.IsActiveCell)
					{
						result2 = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result2 = decimal.Parse(e.Cell.Value.ToString());
						}
						e.Cell.Row.Cells["Quantity"].Value = Math.Round(result2 * result / 100m, 0);
					}
					else if (e.Cell.Column.Key == "ReceivedQuantity" && e.Cell.IsActiveCell)
					{
						result = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result = decimal.Parse(e.Cell.Value.ToString());
						}
						e.Cell.Row.Cells["Quantity"].Value = Math.Round(result2 * result / 100m, 0);
					}
					else if (e.Cell.Column.Key == "Quantity" && e.Cell.IsActiveCell)
					{
						result3 = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result3 = decimal.Parse(e.Cell.Value.ToString());
						}
						if (result == 0m)
						{
							result = result3;
							e.Cell.Row.Cells["ReceivedQuantity"].Value = result;
						}
						if (result > 0m)
						{
							e.Cell.Row.Cells["IssuePercent"].Value = Math.Round(result3 / result * 100m, 0);
						}
						else
						{
							e.Cell.Row.Cells["IssuePercent"].Value = 0;
						}
						e.Cell.Row.Cells["ClaimAmount"].Value = Math.Round(result3 * result5, Global.CurDecimalPoints);
					}
					if (e.Cell.Column.Key == "LossRatio" && e.Cell.IsActiveCell)
					{
						result4 = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result4 = decimal.Parse(e.Cell.Value.ToString());
						}
						result5 = Math.Round(result4 * result6 / 100m, 2);
						e.Cell.Row.Cells["ClaimRate"].Value = result5;
						e.Cell.Row.Cells["ClaimAmount"].Value = Math.Round(result3 * result5, Global.CurDecimalPoints);
					}
					else if (e.Cell.Column.Key == "ClaimRate" && e.Cell.IsActiveCell)
					{
						result5 = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result5 = decimal.Parse(e.Cell.Value.ToString());
						}
						result4 = ((!(result6 > 0m)) ? default(decimal) : Math.Round(result5 / result6 * 100m, 2));
						e.Cell.Row.Cells["LossRatio"].Value = result4;
						e.Cell.Row.Cells["ClaimAmount"].Value = Math.Round(result3 * result5, Global.CurDecimalPoints);
					}
					else if (e.Cell.Column.Key == "ClaimAmount" && e.Cell.IsActiveCell)
					{
						result7 = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result7 = decimal.Parse(e.Cell.Value.ToString());
						}
						result5 = ((!(result3 != 0m)) ? default(decimal) : Math.Round(result7 / result3, 2));
						e.Cell.Row.Cells["ClaimRate"].Value = result5;
						if (result6 != 0m)
						{
							e.Cell.Row.Cells["LossRatio"].Value = Math.Round(result5 / result6 * 100m, 2);
						}
						else
						{
							e.Cell.Row.Cells["LossRatio"].Value = 0;
						}
					}
					else if (e.Cell.Column.Key == "UnitCost" && e.Cell.IsActiveCell)
					{
						result6 = default(decimal);
						if (!e.Cell.Value.IsNullOrEmpty())
						{
							result6 = decimal.Parse(e.Cell.Value.ToString());
						}
						result5 = Math.Round(result6 * result4 / 100m, 2);
						e.Cell.Row.Cells["ClaimRate"].Value = result5;
						e.Cell.Row.Cells["ClaimAmount"].Value = Math.Round(result3 * result5, Global.CurDecimalPoints);
					}
					decimal num = default(decimal);
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (!row.Cells["ClaimAmount"].Value.IsNullOrEmpty())
						{
							num += decimal.Parse(row.Cells["ClaimAmount"].Value.ToString());
						}
					}
					textBoxTotalAmount.Text = num.ToString(Format.TotalAmountFormat);
				}
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
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && dataGridItems.HasRowAnyValue(activeRow) && activeRow.Cells["Commodity"].Value.IsNullOrEmpty())
			{
				ErrorHelper.InformationMessage("Please select a commodity.");
				e.Cancel = true;
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new QualityClaimData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.QualityClaimTable.Rows[0] : currentData.QualityClaimTable.NewRow();
					dataRow["SourceSysDocID"] = textBoxArrivalReportSysDocID.Text;
					dataRow["SourceVoucherID"] = textBoxArrivalVoucherID.Text;
					dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow["VendorID"] = comboBoxVendor.SelectedID;
					dataRow["DateInspected"] = dateTimePickerInspectionDate.Value;
					dataRow["Reference"] = textBoxRef1.Text;
					dataRow["Description"] = textBoxDescription.Text;
					dataRow["ClaimAmount"] = textBoxTotalAmount.Text;
					dataRow["ReceivedAmount"] = textBoxReceivedAmount.Text;
					dataRow["Reference"] = textBoxRef1.Text;
					dataRow["TotalPallets"] = textBoxPalletsCount.Text;
					dataRow["TotalQuantity"] = textBoxQuantity.Text;
					dataRow["Note"] = textBoxDescription.Text;
					dataRow["DateReceived"] = DateTime.Now;
					dataRow["SurveyType"] = comboBoxSurveyType.SelectedIndex + 1;
					dataRow["SurveyDate"] = dateTimePickerSurveyDate.Value;
					dataRow["SurveyerID"] = comboBoxSurveyor1.SelectedID;
					dataRow["Surveyer2ID"] = comboBoxSurveyor2.SelectedID;
					dataRow["ClaimDate"] = dateTimePickerInspectionDate.Value;
					dataRow["ClaimStatus"] = comboBoxStatus.SelectedIndex + 1;
					dataRow["VesselName"] = textBoxVesselName.Text;
					dataRow["ContainerNumber"] = textBoxContainerNo.Text;
					dataRow["OriginID"] = textBoxOrigin.Text;
					dataRow["Status"] = 0;
					dataRow["BatchNumber"] = textBoxBatchNo.Text;
					dataRow["CRSysDocID"] = "";
					dataRow["CRVoucherID"] = "";
					if (comboBoxCurrency.SelectedID != "")
					{
						dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
					}
					else
					{
						dataRow["CurrencyID"] = DBNull.Value;
					}
					dataRow.EndEdit();
					if (IsNewRecord)
					{
						currentData.QualityClaimTable.Rows.Add(dataRow);
					}
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (!currentData.QualityClaimDetailTable.Columns.Contains(column.Key))
						{
							currentData.QualityClaimDetailTable.Columns.Add(column.Key, column.DataType);
						}
					}
					currentData.QualityClaimDetailTable.Rows.Clear();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						DataRow dataRow2 = currentData.QualityClaimDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow2["RowType"] = row.Cells["RowType"].Value.ToString();
						dataRow2["CommodityID"] = row.Cells["Commodity"].Value.ToString();
						dataRow2["VarietyID"] = (row.Cells["Variety"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Variety"].Value.ToString()));
						dataRow2["BrandID"] = (row.Cells["Brand"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Brand"].Value.ToString()));
						dataRow2["IssueName"] = (row.Cells["IssueName"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["IssueName"].Value.ToString()));
						dataRow2["IssuePercent"] = (row.Cells["IssuePercent"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["IssuePercent"].Value.ToString()));
						dataRow2["UnitCost"] = (row.Cells["UnitCost"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["UnitCost"].Value.ToString()));
						dataRow2["ReceivedQuantity"] = (row.Cells["ReceivedQuantity"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["ReceivedQuantity"].Value.ToString()));
						dataRow2["Quantity"] = (row.Cells["Quantity"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Quantity"].Value.ToString()));
						dataRow2["LossRatio"] = (row.Cells["LossRatio"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["LossRatio"].Value.ToString()));
						dataRow2["Grade"] = (row.Cells["Grade"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Grade"].Value.ToString()));
						dataRow2["ClaimRate"] = (row.Cells["ClaimRate"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["ClaimRate"].Value.ToString()));
						dataRow2["ClaimAmount"] = (row.Cells["ClaimAmount"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["ClaimAmount"].Value.ToString()));
						dataRow2["Description"] = (row.Cells["Description"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Description"].Value.ToString()));
						dataRow2.EndEdit();
						currentData.QualityClaimDetailTable.Rows.Add(dataRow2);
					}
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("RowType", typeof(byte));
				dataTable.Columns.Add("Commodity");
				dataTable.Columns.Add("Variety");
				dataTable.Columns.Add("Brand");
				dataTable.Columns.Add("Grade");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("IssueName");
				dataTable.Columns.Add("ReceivedQuantity", typeof(decimal));
				dataTable.Columns.Add("IssuePercent", typeof(decimal));
				dataTable.Columns.Add("UnitCost", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("LossRatio", typeof(decimal));
				dataTable.Columns.Add("ClaimRate", typeof(decimal));
				dataTable.Columns.Add("ClaimAmount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].ValueList = comboBoxCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["Variety"].ValueList = comboBoxVariety;
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].ValueList = comboBoxBrand;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["ReceivedQuantity"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["IssuePercent"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["UnitCost"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["LossRatio"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["ClaimRate"].CellAppearance;
				HAlign hAlign2 = dataGridItems.DisplayLayout.Bands[0].Columns["ClaimAmount"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign4 = cellAppearance6.TextHAlign = hAlign2;
				HAlign hAlign6 = cellAppearance5.TextHAlign = hAlign4;
				HAlign hAlign8 = cellAppearance4.TextHAlign = hAlign6;
				HAlign hAlign10 = cellAppearance3.TextHAlign = hAlign8;
				HAlign hAlign13 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign10);
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["ReceivedQuantity"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["LossRatio"];
				string text2 = dataGridItems.DisplayLayout.Bands[0].Columns["IssuePercent"].Format = "n";
				string text4 = ultraGridColumn3.Format = text2;
				string text7 = ultraGridColumn.Format = (ultraGridColumn2.Format = text4);
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["UnitCost"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["ClaimRate"];
				text4 = (dataGridItems.DisplayLayout.Bands[0].Columns["ClaimAmount"].Format = Format.GridAmountFormat);
				text7 = (ultraGridColumn4.Format = (ultraGridColumn5.Format = text4));
				dataGridItems.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Quality");
				valueList.ValueListItems.Add(2, "Weight");
				valueList.ValueListItems.Add(3, "Packing");
				valueList.ValueListItems.Add(4, "Expense");
				dataGridItems.DisplayLayout.Bands[0].Columns["RowType"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowType"].Header.Caption = "Type";
				dataGridItems.DisplayLayout.Bands[0].Columns["RowType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["IssueName"].Header.Caption = "Defect Name";
				dataGridItems.DisplayLayout.Bands[0].Columns["IssuePercent"].Header.Caption = "Issue %";
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceivedQuantity"].Header.Caption = "Total Qty";
				dataGridItems.DisplayLayout.Bands[0].Columns["UnitCost"].Header.Caption = "Unit Cost";
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Header.Caption = "Affected Qty";
				dataGridItems.DisplayLayout.Bands[0].Columns["LossRatio"].Header.Caption = "Dep %";
				dataGridItems.DisplayLayout.Bands[0].Columns["ClaimRate"].Header.Caption = "Claim Rate";
				dataGridItems.DisplayLayout.Bands[0].Columns["ClaimAmount"].Header.Caption = "Claim Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Variety"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["IssueName"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["IssuePercent"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["UnitCost"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["ReceivedQuantity"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["LossRatio"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["ClaimRate"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["ClaimAmount"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("ClaimAmount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["ClaimAmount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["ClaimAmount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["ClaimAmount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["ClaimAmount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["ClaimAmount"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["ClaimAmount"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
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
			comboBoxVendor.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.QualityClaimSystem.GetQualityClaimByID(SystemDocID, voucherID);
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
			checked
			{
				try
				{
					isDataLoading = true;
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
					{
						DataRow dataRow = currentData.Tables["Quality_Claim"].Rows[0];
						comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
						textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
						comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
						textBoxRef1.Text = dataRow["Reference"].ToString();
						textBoxDescription.Text = dataRow["Description"].ToString();
						textBoxTotalAmount.Text = dataRow["ClaimAmount"].ToString();
						textBoxReceivedAmount.Text = dataRow["ReceivedAmount"].ToString();
						textBoxQuantity.Text = dataRow["TotalQuantity"].ToString();
						textBoxDescription.Text = dataRow["Note"].ToString();
						if (dataRow["SurveyType"] != DBNull.Value)
						{
							comboBoxSurveyType.SelectedIndex = int.Parse(dataRow["SurveyType"].ToString()) - 1;
						}
						else
						{
							comboBoxSurveyType.SelectedIndex = 0;
						}
						dateTimePickerSurveyDate.Value = DateTime.Parse(dataRow["SurveyDate"].ToString());
						comboBoxSurveyor1.SelectedID = dataRow["SurveyerID"].ToString();
						comboBoxSurveyor2.SelectedID = dataRow["Surveyer2ID"].ToString();
						textBoxArrivalReportSysDocID.Text = dataRow["SourceSysDocID"].ToString();
						textBoxArrivalVoucherID.Text = dataRow["SourceVoucherID"].ToString();
						dateTimePickerInspectionDate.Value = DateTime.Parse(dataRow["ClaimDate"].ToString());
						textBoxBatchNo.Text = dataRow["BatchNumber"].ToString();
						comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
						if (dataRow["ClaimStatus"] != DBNull.Value)
						{
							comboBoxStatus.SelectedIndex = int.Parse(dataRow["ClaimStatus"].ToString()) - 1;
						}
						else
						{
							comboBoxStatus.SelectedIndex = 0;
						}
						ArrivalReportData arrivalReportByID = Factory.ArrivalReportSystem.GetArrivalReportByID(textBoxArrivalReportSysDocID.Text, textBoxArrivalVoucherID.Text);
						if (arrivalReportByID != null && arrivalReportByID.Tables[0].Rows.Count > 0)
						{
							DataRow dataRow2 = arrivalReportByID.ArrivalReportTable.Rows[0];
							vendorsFlatComboBox1.SelectedID = dataRow2["VendorID"].ToString();
							textBoxvendorsFlat.Text = dataRow2["VendorName"].ToString();
							textBoxVesselName.Text = dataRow2["VesselName"].ToString();
							textBoxContainerNo.Text = dataRow2["ContainerNumber"].ToString();
							if (dataRow2["DateReceived"] != DBNull.Value)
							{
								textBoxReceiveDate.Text = DateTime.Parse(dataRow2["DateReceived"].ToString()).ToShortDateString();
							}
							else
							{
								textBoxReceiveDate.Clear();
							}
							textBoxOrigin.Text = dataRow2["OriginID"].ToString();
							textBoxContainerTemp.Text = dataRow2["ContainerTemp"].ToString();
							textBoxPalletsCount.Text = dataRow2["TotalPallets"].ToString();
							textBoxQuantity.Text = dataRow2["TotalQuantity"].ToString();
						}
						(dataGridItems.DataSource as DataTable)?.Rows.Clear();
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						if (currentData.Tables.Contains("Quality_Claim_Detail") && currentData.QualityClaimDetailTable.Rows.Count != 0)
						{
							foreach (DataRow row in currentData.Tables["Quality_Claim_Detail"].Rows)
							{
								DataRow dataRow4 = dataTable.NewRow();
								dataRow4["Commodity"] = row["CommodityID"].ToString();
								dataRow4["Variety"] = row["VarietyID"].ToString();
								dataRow4["Brand"] = row["BrandID"].ToString();
								dataRow4["Grade"] = row["Grade"].ToString();
								dataRow4["RowType"] = row["RowType"].ToString();
								dataRow4["IssueName"] = row["IssueName"].ToString();
								if (row["IssuePercent"] != DBNull.Value)
								{
									dataRow4["IssuePercent"] = row["IssuePercent"].ToString();
								}
								else
								{
									dataRow4["IssuePercent"] = 0;
								}
								if (row["UnitCost"] != DBNull.Value)
								{
									dataRow4["UnitCost"] = row["UnitCost"].ToString();
								}
								else
								{
									dataRow4["UnitCost"] = 0;
								}
								if (row["ReceivedQuantity"] != DBNull.Value)
								{
									dataRow4["ReceivedQuantity"] = row["ReceivedQuantity"].ToString();
								}
								else
								{
									dataRow4["ReceivedQuantity"] = 0;
								}
								if (row["Quantity"] != DBNull.Value)
								{
									dataRow4["Quantity"] = row["Quantity"].ToString();
								}
								else
								{
									dataRow4["Quantity"] = 0;
								}
								if (row["LossRatio"] != DBNull.Value)
								{
									dataRow4["LossRatio"] = row["LossRatio"].ToString();
								}
								else
								{
									dataRow4["LossRatio"] = 0;
								}
								if (row["ClaimRate"] != DBNull.Value)
								{
									dataRow4["ClaimRate"] = row["ClaimRate"].ToString();
								}
								else
								{
									dataRow4["ClaimRate"] = 0;
								}
								if (row["ClaimAmount"] != DBNull.Value)
								{
									dataRow4["ClaimAmount"] = row["ClaimAmount"].ToString();
								}
								else
								{
									dataRow4["ClaimAmount"] = 0;
								}
								dataRow4["Description"] = row["Description"].ToString();
								dataRow4.EndEdit();
								dataTable.Rows.Add(dataRow4);
							}
							dataTable.AcceptChanges();
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
				bool flag = true;
				flag = Factory.QualityClaimSystem.CreateQualityClaim(currentData, !isNewRecord);
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
				if (ex.Number == 1037)
				{
					ErrorHelper.ErrorMessage(ex.Message);
				}
				else
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
				}
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
			bool flag = false;
			flag = (Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction) ? true : false);
			if (!IsNewRecord && !Global.IsUserAdmin && !flag && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Quality_Claim", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			if (!GetValidArrivalReport() && isNewRecord)
			{
				ErrorHelper.WarningMessage("This Arrival Report is already Claimed.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxVendor.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (!IsNewRecord && !Factory.QualityClaimSystem.CanUpdate(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.ErrorMessage("Some items in this order has been already received. You are not able to modify.");
				return false;
			}
			for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
			{
				if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
				{
					dataGridItems.Rows[i].Delete(displayPrompt: false);
				}
				else if (dataGridItems.Rows[i].Cells["Commodity"].Value.IsNullOrEmpty() || dataGridItems.Rows[i].Cells["Variety"].Value.IsNullOrEmpty() || dataGridItems.Rows[i].Cells["Brand"].Value.IsNullOrEmpty() || dataGridItems.Rows[i].Cells["Grade"].Value.IsNullOrEmpty() || dataGridItems.Rows[i].Cells["RowType"].Value.IsNullOrEmpty() || dataGridItems.Rows[i].Cells["ClaimAmount"].Value.IsNullOrEmpty())
				{
					ErrorHelper.InformationMessage("Please select all required fields for the rows.");
					dataGridItems.Rows[i].Activate();
					return false;
				}
			}
			if (textBoxArrivalReportSysDocID.Text == "")
			{
				ErrorHelper.InformationMessage("Please select an arrival report.");
				return false;
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one row of item.");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Quality_Claim", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
		}

		private bool GetValidArrivalReport()
		{
			try
			{
				new DataSet();
				if (Factory.QualityClaimSystem.GetQualityClaimAll().Tables["QualityClaim"].Select("SourceSysDocID='" + textBoxArrivalReportSysDocID.Text.Trim() + "' AND SourceVoucherID='" + textBoxArrivalVoucherID.Text.Trim() + "'").Length == 0)
				{
					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
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
				allowEdit = true;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxVendorName.Clear();
				comboBoxVendor.Clear();
				textBoxTotalAmount.Text = "0";
				textBoxVesselName.Clear();
				textBoxContainerNo.Clear();
				textBoxOrigin.Clear();
				textBoxBatchNo.Clear();
				comboBoxVendor.Clear();
				dateTimePickerInspectionDate.Value = DateTime.Now;
				textBoxRef1.Clear();
				textBoxDescription.Clear();
				textBoxReceivedAmount.Clear();
				textBoxRef1.Clear();
				textBoxPalletsCount.Clear();
				textBoxQuantity.Clear();
				textBoxContainerTemp.Clear();
				textBoxDescription.Clear();
				dateTimePickerSurveyDate.Value = DateTime.Now;
				comboBoxSurveyor1.Clear();
				comboBoxSurveyor2.Clear();
				dateTimePickerInspectionDate.Value = DateTime.Now;
				textBoxVesselName.Clear();
				textBoxContainerNo.Clear();
				textBoxOrigin.Clear();
				textBoxArrivalVoucherID.Clear();
				textBoxvendorsFlat.Clear();
				vendorsFlatComboBox1.Clear();
				comboBoxVendor.Clear();
				comboBoxStatus.SelectedIndex = 0;
				textBoxVendorName.Clear();
				textBoxArrivalReportSysDocID.Clear();
				IsNewRecord = true;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable)?.Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxVendor.Focus();
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (!Factory.QualityClaimSystem.CanUpdate(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this order has been already received. You are not able to delete.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.QualityClaimSystem.DeleteQualityClaim(SystemDocID, textBoxVoucherNumber.Text);
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1037)
				{
					ErrorHelper.ErrorMessage(ex.Message);
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
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Quality_Claim", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Quality_Claim", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Quality_Claim", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Quality_Claim", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Quality_Claim", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.QualityClaim);
				SetSecurity();
				Label label = labelCurrency;
				bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
				label.Visible = visible;
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
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
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
			if (Void(isVoid: true))
			{
				IsVoid = true;
			}
			else
			{
				ErrorHelper.ErrorMessage("Unable to void the transaction.");
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				if (!Factory.QualityClaimSystem.CanUpdate(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this order has been already received. You are not able to void.");
					return false;
				}
				if (isVoid)
				{
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				}
				else
				{
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid);
				}
				_ = 7;
				return false;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1037)
				{
					ErrorHelper.ErrorMessage(ex.Message);
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
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.QualityClaim);
		}

		private void LoadVendorBillingAddress()
		{
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!(currentVendorAddressID == ""))
			{
				new FormHelper().EditVendorAddress(comboBoxVendor.SelectedID, currentVendorAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
		}

		private void numberTextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxTaxPercent_TextChanged(object sender, EventArgs e)
		{
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
					DataSet qualityClaimToPrint = Factory.QualityClaimSystem.GetQualityClaimToPrint(selectedID, text);
					if (qualityClaimToPrint == null || qualityClaimToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(qualityClaimToPrint, selectedID, "Quality Claim", SysDocTypes.QualityClaim, isPrint, showPrintDialog);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.QualityClaimListFormObj);
		}

		private void closeOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdatePOStatusDialog updatePOStatusDialog = new UpdatePOStatusDialog();
			updatePOStatusDialog.SetDocument(SysDocTypes.QualityClaim, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
			updatePOStatusDialog.ShowDialog(this);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text;
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

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void label29_Click(object sender, EventArgs e)
		{
		}

		private void buttonSelect_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet claimableArrivalReports = Factory.ArrivalReportSystem.GetClaimableArrivalReports();
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = claimableArrivalReports;
				selectDocumentDialog.Text = "Select Arrival Report";
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.ValidateSelection += form_ValidateSelection;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					UltraGridRow ultraGridRow = selectDocumentDialog.SelectedRows[0];
					textBoxArrivalVoucherID.Text = ultraGridRow.Cells["Number"].Value.ToString();
					textBoxArrivalReportSysDocID.Text = ultraGridRow.Cells["Doc ID"].Value.ToString();
					ArrivalReportData arrivalReportByID = Factory.ArrivalReportSystem.GetArrivalReportByID(textBoxArrivalReportSysDocID.Text, textBoxArrivalVoucherID.Text);
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					decimal num3 = default(decimal);
					decimal num4 = default(decimal);
					decimal num5 = default(decimal);
					string value = "";
					string value2 = "";
					string value3 = "";
					string value4 = "";
					decimal num6 = default(decimal);
					string text = "";
					if (arrivalReportByID != null && arrivalReportByID.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = arrivalReportByID.ArrivalReportTable.Rows[0];
						vendorsFlatComboBox1.SelectedID = dataRow["VendorID"].ToString();
						textBoxvendorsFlat.Text = dataRow["VendorName"].ToString();
						textBoxVesselName.Text = dataRow["VesselName"].ToString();
						textBoxContainerNo.Text = dataRow["ContainerNumber"].ToString();
						textBoxOrigin.Text = dataRow["OriginID"].ToString();
						textBoxContainerTemp.Text = dataRow["ContainerTemp"].ToString();
						comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
						if (dataRow["DateReceived"] != DBNull.Value)
						{
							textBoxReceiveDate.Text = DateTime.Parse(dataRow["DateReceived"].ToString()).ToShortDateString();
						}
						else
						{
							textBoxReceiveDate.Clear();
						}
						textBoxPalletsCount.Text = dataRow["TotalPallets"].ToString();
						textBoxQuantity.Text = dataRow["TotalQuantity"].ToString();
						text = dataRow["TemplateID"].ToString();
						num = decimal.Parse(dataRow["TotalIssue1"].ToString());
						num2 = decimal.Parse(dataRow["TotalIssue2"].ToString());
						num3 = decimal.Parse(dataRow["TotalIssue3"].ToString());
						num4 = decimal.Parse(dataRow["TotalIssue4"].ToString());
						if (!dataRow["TotalWeightLess"].IsDBNullOrEmpty())
						{
							num5 = decimal.Parse(dataRow["TotalWeightLess"].ToString());
						}
						if (!dataRow["TotalQuantity"].IsDBNullOrEmpty())
						{
							num6 = decimal.Parse(dataRow["TotalQuantity"].ToString());
						}
						value = dataRow["Issue1Name"].ToString();
						value2 = dataRow["Issue2Name"].ToString();
						value3 = dataRow["Issue3Name"].ToString();
						value4 = dataRow["Issue4Name"].ToString();
					}
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					if (text != "")
					{
						ArrivalReportTemplateData arrivalReportTemplateByID = Factory.ArrivalReportTemplateSystem.GetArrivalReportTemplateByID(text);
						if (arrivalReportTemplateByID != null)
						{
							DataRow dataRow2 = arrivalReportTemplateByID.ArrivalReportTemplateTable.Rows[0];
							decimal.TryParse(dataRow2["Issue1LossPercent"].ToString(), out result);
							decimal.TryParse(dataRow2["Issue2LossPercent"].ToString(), out result2);
							decimal.TryParse(dataRow2["Issue3LossPercent"].ToString(), out result3);
							decimal.TryParse(dataRow2["Issue4LossPercent"].ToString(), out result4);
						}
					}
					List<string> list = new List<string>();
					foreach (DataRow row in arrivalReportByID.ArrivalReportDetailTable.Rows)
					{
						string item = row["ComodityID"].ToString() + "$" + row["VarietyID"].ToString() + "$" + row["BrandID"].ToString() + "$" + row["Grade"].ToString();
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (string item2 in list)
					{
						string[] array = item2.Split('$');
						DataRow dataRow4 = null;
						if (num > 0m)
						{
							dataRow4 = dataTable.NewRow();
							dataRow4["RowType"] = 1;
							dataRow4["Commodity"] = array[0];
							dataRow4["Variety"] = array[1];
							dataRow4["Brand"] = array[2];
							dataRow4["Grade"] = array[3];
							dataRow4["ReceivedQuantity"] = num6;
							dataRow4["IssueName"] = value;
							dataRow4["Quantity"] = Math.Round(num6 * num / 100m, 2);
							dataRow4["LossRatio"] = result;
							dataRow4["UnitCost"] = 0;
							dataRow4["ClaimAmount"] = 0;
							dataRow4["ClaimRate"] = 0;
							dataRow4["IssuePercent"] = num;
							dataTable.Rows.Add(dataRow4);
						}
						if (num2 > 0m)
						{
							dataRow4 = dataTable.NewRow();
							dataRow4["RowType"] = 1;
							dataRow4["Commodity"] = array[0];
							dataRow4["Variety"] = array[1];
							dataRow4["Brand"] = array[2];
							dataRow4["Grade"] = array[3];
							dataRow4["IssuePercent"] = num2;
							dataRow4["ReceivedQuantity"] = num6;
							dataRow4["Quantity"] = Math.Round(num6 * num2 / 100m, 2);
							dataRow4["IssueName"] = value2;
							dataRow4["LossRatio"] = result2;
							dataRow4["UnitCost"] = 0;
							dataRow4["ClaimAmount"] = 0;
							dataRow4["ClaimRate"] = 0;
							dataTable.Rows.Add(dataRow4);
						}
						if (num3 > 0m)
						{
							dataRow4 = dataTable.NewRow();
							dataRow4["RowType"] = 1;
							dataRow4["Commodity"] = array[0];
							dataRow4["Variety"] = array[1];
							dataRow4["Brand"] = array[2];
							dataRow4["Grade"] = array[3];
							dataRow4["IssuePercent"] = num3;
							dataRow4["ReceivedQuantity"] = num6;
							dataRow4["Quantity"] = Math.Round(num6 * num3 / 100m, 2);
							dataRow4["IssueName"] = value3;
							dataRow4["LossRatio"] = result3;
							dataRow4["UnitCost"] = 0;
							dataRow4["ClaimAmount"] = 0;
							dataRow4["ClaimRate"] = 0;
							dataTable.Rows.Add(dataRow4);
						}
						if (num4 > 0m)
						{
							dataRow4 = dataTable.NewRow();
							dataRow4["RowType"] = 1;
							dataRow4["Commodity"] = array[0];
							dataRow4["Variety"] = array[1];
							dataRow4["Brand"] = array[2];
							dataRow4["Grade"] = array[3];
							dataRow4["IssuePercent"] = num4;
							dataRow4["Quantity"] = Math.Round(num6 * num4 / 100m, 2);
							dataRow4["ReceivedQuantity"] = num6;
							dataRow4["IssueName"] = value4;
							dataRow4["LossRatio"] = result4;
							dataRow4["UnitCost"] = 0;
							dataRow4["ClaimAmount"] = 0;
							dataRow4["ClaimRate"] = 0;
							dataTable.Rows.Add(dataRow4);
						}
					}
					foreach (string item3 in list)
					{
						string[] array2 = item3.Split('$');
						DataRow dataRow5 = null;
						if (num5 > 0m)
						{
							dataRow5 = dataTable.NewRow();
							dataRow5["RowType"] = 2;
							dataRow5["Commodity"] = array2[0];
							dataRow5["Variety"] = array2[1];
							dataRow5["Brand"] = array2[2];
							dataRow5["Grade"] = array2[3];
							dataRow5["IssuePercent"] = num5;
							dataRow5["ReceivedQuantity"] = num6;
							dataTable.Rows.Add(dataRow5);
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransaction(TransactionListType.ArrivalReport, textBoxArrivalReportSysDocID.Text, textBoxArrivalVoucherID.Text);
		}

		private void comboBoxVendor_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			try
			{
				if (comboBoxVendor.SelectedID != "")
				{
					string defaultCurrencyID = comboBoxVendor.DefaultCurrencyID;
					comboBoxCurrency.SelectedID = defaultCurrencyID;
					comboBoxCurrency.GetLastRate();
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
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.QualityClaimForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelClaim = new System.Windows.Forms.Panel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			panelSurveyor1 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			comboBoxSurveyor1 = new Micromind.DataControls.SurveyorComboBox();
			dateTimePickerSurveyDate = new System.Windows.Forms.DateTimePicker();
			labelCurrency = new System.Windows.Forms.Label();
			panelSurveyor2 = new System.Windows.Forms.Panel();
			comboBoxSurveyor2 = new Micromind.DataControls.SurveyorComboBox();
			label7 = new System.Windows.Forms.Label();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			comboBoxSurveyType = new System.Windows.Forms.ComboBox();
			label19 = new System.Windows.Forms.Label();
			textBoxDescription = new System.Windows.Forms.TextBox();
			dateTimePickerInspectionDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			purchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panel1 = new System.Windows.Forms.Panel();
			textBoxReceivedAmount = new Micromind.UISupport.AmountTextBox();
			textBoxCreditNote = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			textBoxTotalAmount = new System.Windows.Forms.TextBox();
			labelIssue1Name = new System.Windows.Forms.Label();
			panelArrivalReport = new System.Windows.Forms.Panel();
			textBoxReceiveDate = new System.Windows.Forms.TextBox();
			textBoxOrigin = new System.Windows.Forms.TextBox();
			textBoxContainerTemp = new System.Windows.Forms.TextBox();
			textBoxPalletsCount = new System.Windows.Forms.TextBox();
			textBoxQuantity = new System.Windows.Forms.TextBox();
			textBoxArrivalReportSysDocID = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonSelect = new Micromind.UISupport.XPButton();
			textBoxArrivalVoucherID = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			label21 = new System.Windows.Forms.Label();
			textBoxVesselName = new System.Windows.Forms.TextBox();
			textBoxContainerNo = new System.Windows.Forms.TextBox();
			label22 = new System.Windows.Forms.Label();
			label23 = new System.Windows.Forms.Label();
			label24 = new System.Windows.Forms.Label();
			label25 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			vendorsFlatComboBox1 = new Micromind.DataControls.vendorsFlatComboBox();
			textBoxvendorsFlat = new System.Windows.Forms.TextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxBrand = new Micromind.DataControls.ProductBrandComboBox();
			comboBoxVariety = new Micromind.DataControls.ProductStyleComboBox();
			comboBoxCategory = new Micromind.DataControls.ProductCategoryComboBox();
			label9 = new System.Windows.Forms.Label();
			textBoxBatchNo = new System.Windows.Forms.TextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelClaim.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			panelSurveyor1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSurveyor1).BeginInit();
			panelSurveyor2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSurveyor2).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			panel1.SuspendLayout();
			panelArrivalReport.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)vendorsFlatComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVariety).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
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
				toolStripSeparator5,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator6,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(944, 31);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 632);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(944, 40);
			panelButtons.TabIndex = 4;
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
			linePanelDown.Size = new System.Drawing.Size(944, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(834, 8);
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
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(138, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(469, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(544, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(125, 20);
			textBoxRef1.TabIndex = 6;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(212, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 0;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelClaim.Controls.Add(label9);
			panelClaim.Controls.Add(textBoxBatchNo);
			panelClaim.Controls.Add(comboBoxVendor);
			panelClaim.Controls.Add(comboBoxCurrency);
			panelClaim.Controls.Add(panelSurveyor1);
			panelClaim.Controls.Add(labelCurrency);
			panelClaim.Controls.Add(panelSurveyor2);
			panelClaim.Controls.Add(comboBoxStatus);
			panelClaim.Controls.Add(label2);
			panelClaim.Controls.Add(label4);
			panelClaim.Controls.Add(comboBoxSurveyType);
			panelClaim.Controls.Add(label19);
			panelClaim.Controls.Add(textBoxDescription);
			panelClaim.Controls.Add(dateTimePickerInspectionDate);
			panelClaim.Controls.Add(ultraFormattedLinkLabel4);
			panelClaim.Controls.Add(ultraFormattedLinkLabel5);
			panelClaim.Controls.Add(comboBoxSysDoc);
			panelClaim.Controls.Add(ultraFormattedLinkLabel2);
			panelClaim.Controls.Add(mmLabel1);
			panelClaim.Controls.Add(label1);
			panelClaim.Controls.Add(textBoxVendorName);
			panelClaim.Controls.Add(textBoxRef1);
			panelClaim.Controls.Add(textBoxVoucherNumber);
			panelClaim.Location = new System.Drawing.Point(6, 164);
			panelClaim.Name = "panelClaim";
			panelClaim.Size = new System.Drawing.Size(938, 142);
			panelClaim.TabIndex = 1;
			comboBoxVendor.AlwaysInEditMode = true;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance3;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(99, 23);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(109, 20);
			comboBoxVendor.TabIndex = 328;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(730, 24);
			comboBoxCurrency.MaximumSize = new System.Drawing.Size(99999, 20);
			comboBoxCurrency.MinimumSize = new System.Drawing.Size(5, 20);
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			comboBoxCurrency.SelectedID = "";
			comboBoxCurrency.Size = new System.Drawing.Size(138, 20);
			comboBoxCurrency.TabIndex = 329;
			panelSurveyor1.Controls.Add(label8);
			panelSurveyor1.Controls.Add(label5);
			panelSurveyor1.Controls.Add(comboBoxSurveyor1);
			panelSurveyor1.Controls.Add(dateTimePickerSurveyDate);
			panelSurveyor1.Location = new System.Drawing.Point(210, 44);
			panelSurveyor1.Name = "panelSurveyor1";
			panelSurveyor1.Size = new System.Drawing.Size(369, 24);
			panelSurveyor1.TabIndex = 8;
			panelSurveyor1.Visible = false;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(5, 5);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(75, 13);
			label8.TabIndex = 328;
			label8.Text = "Surevey Date:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(182, 6);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(61, 13);
			label5.TabIndex = 322;
			label5.Text = "Surveyer 1:";
			comboBoxSurveyor1.Assigned = false;
			comboBoxSurveyor1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSurveyor1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSurveyor1.CustomReportFieldName = "";
			comboBoxSurveyor1.CustomReportKey = "";
			comboBoxSurveyor1.CustomReportValueType = 1;
			comboBoxSurveyor1.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSurveyor1.DisplayLayout.Appearance = appearance15;
			comboBoxSurveyor1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSurveyor1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSurveyor1.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSurveyor1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxSurveyor1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSurveyor1.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxSurveyor1.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSurveyor1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSurveyor1.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSurveyor1.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxSurveyor1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSurveyor1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSurveyor1.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSurveyor1.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxSurveyor1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSurveyor1.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSurveyor1.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxSurveyor1.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxSurveyor1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSurveyor1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxSurveyor1.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxSurveyor1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSurveyor1.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxSurveyor1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSurveyor1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSurveyor1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSurveyor1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSurveyor1.Editable = true;
			comboBoxSurveyor1.FilterString = "";
			comboBoxSurveyor1.HasAllAccount = false;
			comboBoxSurveyor1.HasCustom = false;
			comboBoxSurveyor1.IsDataLoaded = false;
			comboBoxSurveyor1.Location = new System.Drawing.Point(246, 2);
			comboBoxSurveyor1.MaxDropDownItems = 12;
			comboBoxSurveyor1.Name = "comboBoxSurveyor1";
			comboBoxSurveyor1.ShowInactiveItems = false;
			comboBoxSurveyor1.Size = new System.Drawing.Size(120, 20);
			comboBoxSurveyor1.TabIndex = 1;
			comboBoxSurveyor1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerSurveyDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerSurveyDate.Location = new System.Drawing.Point(82, 2);
			dateTimePickerSurveyDate.Name = "dateTimePickerSurveyDate";
			dateTimePickerSurveyDate.Size = new System.Drawing.Size(95, 20);
			dateTimePickerSurveyDate.TabIndex = 0;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(673, 26);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 13);
			labelCurrency.TabIndex = 330;
			labelCurrency.Text = "Currency:";
			panelSurveyor2.Controls.Add(comboBoxSurveyor2);
			panelSurveyor2.Controls.Add(label7);
			panelSurveyor2.Location = new System.Drawing.Point(580, 43);
			panelSurveyor2.Name = "panelSurveyor2";
			panelSurveyor2.Size = new System.Drawing.Size(195, 24);
			panelSurveyor2.TabIndex = 9;
			panelSurveyor2.Visible = false;
			comboBoxSurveyor2.Assigned = false;
			comboBoxSurveyor2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSurveyor2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSurveyor2.CustomReportFieldName = "";
			comboBoxSurveyor2.CustomReportKey = "";
			comboBoxSurveyor2.CustomReportValueType = 1;
			comboBoxSurveyor2.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSurveyor2.DisplayLayout.Appearance = appearance27;
			comboBoxSurveyor2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSurveyor2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSurveyor2.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSurveyor2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxSurveyor2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSurveyor2.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxSurveyor2.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSurveyor2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSurveyor2.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSurveyor2.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxSurveyor2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSurveyor2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSurveyor2.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSurveyor2.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxSurveyor2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSurveyor2.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSurveyor2.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxSurveyor2.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxSurveyor2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSurveyor2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxSurveyor2.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxSurveyor2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSurveyor2.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxSurveyor2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSurveyor2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSurveyor2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSurveyor2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSurveyor2.Editable = true;
			comboBoxSurveyor2.FilterString = "";
			comboBoxSurveyor2.HasAllAccount = false;
			comboBoxSurveyor2.HasCustom = false;
			comboBoxSurveyor2.IsDataLoaded = false;
			comboBoxSurveyor2.Location = new System.Drawing.Point(67, 2);
			comboBoxSurveyor2.MaxDropDownItems = 12;
			comboBoxSurveyor2.Name = "comboBoxSurveyor2";
			comboBoxSurveyor2.ShowInactiveItems = false;
			comboBoxSurveyor2.Size = new System.Drawing.Size(120, 20);
			comboBoxSurveyor2.TabIndex = 0;
			comboBoxSurveyor2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(5, 6);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(61, 13);
			label7.TabIndex = 324;
			label7.Text = "Surveyer 2:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[3]
			{
				"Open",
				"Closed",
				"Draft"
			});
			comboBoxStatus.Location = new System.Drawing.Point(720, 1);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(109, 21);
			comboBoxStatus.TabIndex = 3;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(674, 6);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 329;
			label2.Text = "Status:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(10, 47);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(76, 13);
			label4.TabIndex = 321;
			label4.Text = "Surevey Type:";
			comboBoxSurveyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSurveyType.FormattingEnabled = true;
			comboBoxSurveyType.Items.AddRange(new object[3]
			{
				"No Survey",
				"Single Survey",
				"Join Survey"
			});
			comboBoxSurveyType.Location = new System.Drawing.Point(99, 45);
			comboBoxSurveyType.Name = "comboBoxSurveyType";
			comboBoxSurveyType.Size = new System.Drawing.Size(109, 21);
			comboBoxSurveyType.TabIndex = 7;
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(10, 94);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(52, 13);
			label19.TabIndex = 316;
			label19.Text = "Remarks:";
			textBoxDescription.Location = new System.Drawing.Point(99, 92);
			textBoxDescription.MaxLength = 5000;
			textBoxDescription.Multiline = true;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxDescription.Size = new System.Drawing.Size(823, 45);
			textBoxDescription.TabIndex = 11;
			dateTimePickerInspectionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerInspectionDate.Location = new System.Drawing.Point(545, 0);
			dateTimePickerInspectionDate.Name = "dateTimePickerInspectionDate";
			dateTimePickerInspectionDate.Size = new System.Drawing.Size(124, 20);
			dateTimePickerInspectionDate.TabIndex = 2;
			appearance39.FontData.BoldAsString = "True";
			appearance39.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance39;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(10, 23);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(64, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			appearance40.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance40;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance41.FontData.BoldAsString = "True";
			appearance41.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance41;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance42;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance43;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(99, 1);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(467, 3);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(72, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Claim Date:";
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(211, 23);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(253, 20);
			textBoxVendorName.TabIndex = 5;
			textBoxVendorName.TabStop = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				availableQuantityToolStripMenuItem,
				purchaseStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			purchaseStatisticsToolStripMenuItem.Name = "purchaseStatisticsToolStripMenuItem";
			purchaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			purchaseStatisticsToolStripMenuItem.Text = "Purchase Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panel1.Controls.Add(textBoxReceivedAmount);
			panel1.Controls.Add(textBoxCreditNote);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(textBoxTotalAmount);
			panel1.Controls.Add(labelIssue1Name);
			panel1.Location = new System.Drawing.Point(6, 554);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(704, 63);
			panel1.TabIndex = 3;
			textBoxReceivedAmount.AllowDecimal = true;
			textBoxReceivedAmount.CustomReportFieldName = "";
			textBoxReceivedAmount.CustomReportKey = "";
			textBoxReceivedAmount.CustomReportValueType = 1;
			textBoxReceivedAmount.IsComboTextBox = false;
			textBoxReceivedAmount.IsModified = false;
			textBoxReceivedAmount.Location = new System.Drawing.Point(342, 8);
			textBoxReceivedAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxReceivedAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxReceivedAmount.Name = "textBoxReceivedAmount";
			textBoxReceivedAmount.NullText = "0";
			textBoxReceivedAmount.Size = new System.Drawing.Size(132, 20);
			textBoxReceivedAmount.TabIndex = 1;
			textBoxReceivedAmount.Text = "0.00";
			textBoxReceivedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxReceivedAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxCreditNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCreditNote.Location = new System.Drawing.Point(121, 31);
			textBoxCreditNote.MaxLength = 20;
			textBoxCreditNote.Name = "textBoxCreditNote";
			textBoxCreditNote.Size = new System.Drawing.Size(112, 20);
			textBoxCreditNote.TabIndex = 2;
			label6.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(14, 34);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(80, 13);
			label6.TabIndex = 328;
			label6.Text = "Credit Note No:";
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(241, 11);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(95, 13);
			label3.TabIndex = 327;
			label3.Text = "Received Amount:";
			textBoxTotalAmount.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTotalAmount.Location = new System.Drawing.Point(121, 6);
			textBoxTotalAmount.MaxLength = 20;
			textBoxTotalAmount.Name = "textBoxTotalAmount";
			textBoxTotalAmount.ReadOnly = true;
			textBoxTotalAmount.Size = new System.Drawing.Size(112, 20);
			textBoxTotalAmount.TabIndex = 0;
			textBoxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelIssue1Name.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelIssue1Name.AutoSize = true;
			labelIssue1Name.Location = new System.Drawing.Point(14, 9);
			labelIssue1Name.Name = "labelIssue1Name";
			labelIssue1Name.Size = new System.Drawing.Size(101, 13);
			labelIssue1Name.TabIndex = 37;
			labelIssue1Name.Text = "Total Claim Amount:";
			panelArrivalReport.Controls.Add(textBoxReceiveDate);
			panelArrivalReport.Controls.Add(textBoxOrigin);
			panelArrivalReport.Controls.Add(textBoxContainerTemp);
			panelArrivalReport.Controls.Add(textBoxPalletsCount);
			panelArrivalReport.Controls.Add(textBoxQuantity);
			panelArrivalReport.Controls.Add(textBoxArrivalReportSysDocID);
			panelArrivalReport.Controls.Add(ultraFormattedLinkLabel3);
			panelArrivalReport.Controls.Add(buttonSelect);
			panelArrivalReport.Controls.Add(textBoxArrivalVoucherID);
			panelArrivalReport.Controls.Add(label11);
			panelArrivalReport.Controls.Add(label16);
			panelArrivalReport.Controls.Add(label21);
			panelArrivalReport.Controls.Add(textBoxVesselName);
			panelArrivalReport.Controls.Add(textBoxContainerNo);
			panelArrivalReport.Controls.Add(label22);
			panelArrivalReport.Controls.Add(label23);
			panelArrivalReport.Controls.Add(label24);
			panelArrivalReport.Controls.Add(label25);
			panelArrivalReport.Controls.Add(ultraFormattedLinkLabel1);
			panelArrivalReport.Controls.Add(vendorsFlatComboBox1);
			panelArrivalReport.Controls.Add(textBoxvendorsFlat);
			panelArrivalReport.Location = new System.Drawing.Point(6, 32);
			panelArrivalReport.Name = "panelArrivalReport";
			panelArrivalReport.Size = new System.Drawing.Size(843, 102);
			panelArrivalReport.TabIndex = 0;
			textBoxReceiveDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxReceiveDate.Location = new System.Drawing.Point(663, 56);
			textBoxReceiveDate.MaxLength = 15;
			textBoxReceiveDate.Name = "textBoxReceiveDate";
			textBoxReceiveDate.ReadOnly = true;
			textBoxReceiveDate.Size = new System.Drawing.Size(116, 20);
			textBoxReceiveDate.TabIndex = 7;
			textBoxReceiveDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxOrigin.Location = new System.Drawing.Point(462, 56);
			textBoxOrigin.MaxLength = 15;
			textBoxOrigin.Name = "textBoxOrigin";
			textBoxOrigin.ReadOnly = true;
			textBoxOrigin.Size = new System.Drawing.Size(116, 20);
			textBoxOrigin.TabIndex = 6;
			textBoxContainerTemp.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxContainerTemp.Location = new System.Drawing.Point(99, 77);
			textBoxContainerTemp.MaxLength = 15;
			textBoxContainerTemp.Name = "textBoxContainerTemp";
			textBoxContainerTemp.ReadOnly = true;
			textBoxContainerTemp.Size = new System.Drawing.Size(109, 20);
			textBoxContainerTemp.TabIndex = 8;
			textBoxContainerTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPalletsCount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPalletsCount.Location = new System.Drawing.Point(291, 77);
			textBoxPalletsCount.MaxLength = 15;
			textBoxPalletsCount.Name = "textBoxPalletsCount";
			textBoxPalletsCount.ReadOnly = true;
			textBoxPalletsCount.Size = new System.Drawing.Size(109, 20);
			textBoxPalletsCount.TabIndex = 9;
			textBoxPalletsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxQuantity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxQuantity.Location = new System.Drawing.Point(462, 77);
			textBoxQuantity.MaxLength = 15;
			textBoxQuantity.Name = "textBoxQuantity";
			textBoxQuantity.ReadOnly = true;
			textBoxQuantity.Size = new System.Drawing.Size(116, 20);
			textBoxQuantity.TabIndex = 10;
			textBoxQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxArrivalReportSysDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxArrivalReportSysDocID.Location = new System.Drawing.Point(99, 5);
			textBoxArrivalReportSysDocID.MaxLength = 64;
			textBoxArrivalReportSysDocID.Name = "textBoxArrivalReportSysDocID";
			textBoxArrivalReportSysDocID.ReadOnly = true;
			textBoxArrivalReportSysDocID.Size = new System.Drawing.Size(59, 20);
			textBoxArrivalReportSysDocID.TabIndex = 0;
			textBoxArrivalReportSysDocID.TabStop = false;
			appearance55.FontData.BoldAsString = "True";
			appearance55.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance55;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 7);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(86, 15);
			ultraFormattedLinkLabel3.TabIndex = 327;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Arrival Report:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			buttonSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelect.BackColor = System.Drawing.Color.DarkGray;
			buttonSelect.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelect.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelect.Location = new System.Drawing.Point(303, 3);
			buttonSelect.Name = "buttonSelect";
			buttonSelect.Size = new System.Drawing.Size(28, 24);
			buttonSelect.TabIndex = 2;
			buttonSelect.Text = "...";
			buttonSelect.UseVisualStyleBackColor = false;
			buttonSelect.Click += new System.EventHandler(buttonSelect_Click);
			textBoxArrivalVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxArrivalVoucherID.Location = new System.Drawing.Point(160, 5);
			textBoxArrivalVoucherID.MaxLength = 64;
			textBoxArrivalVoucherID.Name = "textBoxArrivalVoucherID";
			textBoxArrivalVoucherID.ReadOnly = true;
			textBoxArrivalVoucherID.Size = new System.Drawing.Size(138, 20);
			textBoxArrivalVoucherID.TabIndex = 1;
			textBoxArrivalVoucherID.TabStop = false;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(403, 57);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(37, 13);
			label11.TabIndex = 321;
			label11.Text = "Origin:";
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(10, 58);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(72, 13);
			label16.TabIndex = 312;
			label16.Text = "Vessel Name:";
			label21.AutoSize = true;
			label21.Location = new System.Drawing.Point(212, 59);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(72, 13);
			label21.TabIndex = 313;
			label21.Text = "Container No:";
			textBoxVesselName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVesselName.Location = new System.Drawing.Point(99, 56);
			textBoxVesselName.MaxLength = 15;
			textBoxVesselName.Name = "textBoxVesselName";
			textBoxVesselName.ReadOnly = true;
			textBoxVesselName.Size = new System.Drawing.Size(109, 20);
			textBoxVesselName.TabIndex = 4;
			textBoxContainerNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxContainerNo.Location = new System.Drawing.Point(291, 56);
			textBoxContainerNo.MaxLength = 20;
			textBoxContainerNo.Name = "textBoxContainerNo";
			textBoxContainerNo.ReadOnly = true;
			textBoxContainerNo.Size = new System.Drawing.Size(109, 20);
			textBoxContainerNo.TabIndex = 5;
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(404, 80);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(49, 13);
			label22.TabIndex = 299;
			label22.Text = "Quantity:";
			label23.AutoSize = true;
			label23.Location = new System.Drawing.Point(212, 80);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(72, 13);
			label23.TabIndex = 297;
			label23.Text = "Pallets Count:";
			label24.AutoSize = true;
			label24.Location = new System.Drawing.Point(10, 80);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(85, 13);
			label24.TabIndex = 298;
			label24.Text = "Container Temp:";
			label25.AutoSize = true;
			label25.Location = new System.Drawing.Point(585, 58);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(76, 13);
			label25.TabIndex = 144;
			label25.Text = "Receive Date:";
			appearance57.FontData.BoldAsString = "True";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance57;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(10, 33);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel1.TabIndex = 129;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Vendor:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance58;
			vendorsFlatComboBox1.AlwaysInEditMode = true;
			vendorsFlatComboBox1.Assigned = false;
			vendorsFlatComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			vendorsFlatComboBox1.CustomReportFieldName = "";
			vendorsFlatComboBox1.CustomReportKey = "";
			vendorsFlatComboBox1.CustomReportValueType = 1;
			vendorsFlatComboBox1.DescriptionTextBox = null;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			vendorsFlatComboBox1.DisplayLayout.Appearance = appearance59;
			vendorsFlatComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			vendorsFlatComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			vendorsFlatComboBox1.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			vendorsFlatComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			vendorsFlatComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			vendorsFlatComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			vendorsFlatComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			vendorsFlatComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			vendorsFlatComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			vendorsFlatComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			vendorsFlatComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			vendorsFlatComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			vendorsFlatComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			vendorsFlatComboBox1.DisplayLayout.Override.CellAppearance = appearance66;
			vendorsFlatComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			vendorsFlatComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			vendorsFlatComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			vendorsFlatComboBox1.DisplayLayout.Override.HeaderAppearance = appearance68;
			vendorsFlatComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			vendorsFlatComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			vendorsFlatComboBox1.DisplayLayout.Override.RowAppearance = appearance69;
			vendorsFlatComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			vendorsFlatComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			vendorsFlatComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			vendorsFlatComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			vendorsFlatComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			vendorsFlatComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			vendorsFlatComboBox1.Editable = true;
			vendorsFlatComboBox1.Enabled = false;
			vendorsFlatComboBox1.FilterString = "";
			vendorsFlatComboBox1.FilterSysDocID = "";
			vendorsFlatComboBox1.HasAll = false;
			vendorsFlatComboBox1.HasCustom = false;
			vendorsFlatComboBox1.IsDataLoaded = false;
			vendorsFlatComboBox1.Location = new System.Drawing.Point(99, 31);
			vendorsFlatComboBox1.MaxDropDownItems = 12;
			vendorsFlatComboBox1.Name = "vendorsFlatComboBox1";
			vendorsFlatComboBox1.ShowConsignmentOnly = false;
			vendorsFlatComboBox1.ShowQuickAdd = true;
			vendorsFlatComboBox1.Size = new System.Drawing.Size(109, 20);
			vendorsFlatComboBox1.TabIndex = 2;
			vendorsFlatComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxvendorsFlat.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxvendorsFlat.Location = new System.Drawing.Point(211, 31);
			textBoxvendorsFlat.MaxLength = 64;
			textBoxvendorsFlat.Name = "textBoxvendorsFlat";
			textBoxvendorsFlat.ReadOnly = true;
			textBoxvendorsFlat.Size = new System.Drawing.Size(367, 20);
			textBoxvendorsFlat.TabIndex = 3;
			textBoxvendorsFlat.TabStop = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(8, 148);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(84, 13);
			mmLabel2.TabIndex = 325;
			mmLabel2.Text = "Claim Details:";
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
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance71;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance72;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance73;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance74.BackColor2 = System.Drawing.SystemColors.Control;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance74;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance75;
			appearance76.BackColor = System.Drawing.SystemColors.Highlight;
			appearance76.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance76;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance77;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			appearance78.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance78;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance79.BackColor = System.Drawing.SystemColors.Control;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance79;
			appearance80.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance80;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance81;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance82;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(9, 312);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(923, 242);
			dataGridItems.TabIndex = 2;
			dataGridItems.Text = "dataEntryGrid1";
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
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
			comboBoxJob.Location = new System.Drawing.Point(669, 193);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 121;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			comboBoxBrand.Assigned = false;
			comboBoxBrand.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBrand.CustomReportFieldName = "";
			comboBoxBrand.CustomReportKey = "";
			comboBoxBrand.CustomReportValueType = 1;
			comboBoxBrand.DescriptionTextBox = null;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBrand.DisplayLayout.Appearance = appearance83;
			comboBoxBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.GroupByBox.Appearance = appearance84;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance85;
			comboBoxBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance86.BackColor2 = System.Drawing.SystemColors.Control;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance86;
			comboBoxBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBrand.DisplayLayout.Override.ActiveCellAppearance = appearance87;
			appearance88.BackColor = System.Drawing.SystemColors.Highlight;
			appearance88.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBrand.DisplayLayout.Override.ActiveRowAppearance = appearance88;
			comboBoxBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.CardAreaAppearance = appearance89;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			appearance90.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBrand.DisplayLayout.Override.CellAppearance = appearance90;
			comboBoxBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBrand.DisplayLayout.Override.CellPadding = 0;
			appearance91.BackColor = System.Drawing.SystemColors.Control;
			appearance91.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance91.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.GroupByRowAppearance = appearance91;
			appearance92.TextHAlignAsString = "Left";
			comboBoxBrand.DisplayLayout.Override.HeaderAppearance = appearance92;
			comboBoxBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			comboBoxBrand.DisplayLayout.Override.RowAppearance = appearance93;
			comboBoxBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance94;
			comboBoxBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBrand.Editable = true;
			comboBoxBrand.FilterString = "";
			comboBoxBrand.HasAllAccount = false;
			comboBoxBrand.HasCustom = false;
			comboBoxBrand.IsDataLoaded = false;
			comboBoxBrand.Location = new System.Drawing.Point(690, 348);
			comboBoxBrand.MaxDropDownItems = 12;
			comboBoxBrand.Name = "comboBoxBrand";
			comboBoxBrand.ShowInactiveItems = false;
			comboBoxBrand.Size = new System.Drawing.Size(100, 20);
			comboBoxBrand.TabIndex = 323;
			comboBoxBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBrand.Visible = false;
			comboBoxVariety.Assigned = false;
			comboBoxVariety.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVariety.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVariety.CustomReportFieldName = "";
			comboBoxVariety.CustomReportKey = "";
			comboBoxVariety.CustomReportValueType = 1;
			comboBoxVariety.DescriptionTextBox = null;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVariety.DisplayLayout.Appearance = appearance95;
			comboBoxVariety.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVariety.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVariety.DisplayLayout.GroupByBox.Appearance = appearance96;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVariety.DisplayLayout.GroupByBox.BandLabelAppearance = appearance97;
			comboBoxVariety.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance98.BackColor2 = System.Drawing.SystemColors.Control;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance98.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVariety.DisplayLayout.GroupByBox.PromptAppearance = appearance98;
			comboBoxVariety.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVariety.DisplayLayout.MaxRowScrollRegions = 1;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVariety.DisplayLayout.Override.ActiveCellAppearance = appearance99;
			appearance100.BackColor = System.Drawing.SystemColors.Highlight;
			appearance100.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVariety.DisplayLayout.Override.ActiveRowAppearance = appearance100;
			comboBoxVariety.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVariety.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVariety.DisplayLayout.Override.CardAreaAppearance = appearance101;
			appearance102.BorderColor = System.Drawing.Color.Silver;
			appearance102.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVariety.DisplayLayout.Override.CellAppearance = appearance102;
			comboBoxVariety.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVariety.DisplayLayout.Override.CellPadding = 0;
			appearance103.BackColor = System.Drawing.SystemColors.Control;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVariety.DisplayLayout.Override.GroupByRowAppearance = appearance103;
			appearance104.TextHAlignAsString = "Left";
			comboBoxVariety.DisplayLayout.Override.HeaderAppearance = appearance104;
			comboBoxVariety.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVariety.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			comboBoxVariety.DisplayLayout.Override.RowAppearance = appearance105;
			comboBoxVariety.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVariety.DisplayLayout.Override.TemplateAddRowAppearance = appearance106;
			comboBoxVariety.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVariety.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVariety.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVariety.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVariety.Editable = true;
			comboBoxVariety.FilterString = "";
			comboBoxVariety.HasAllAccount = false;
			comboBoxVariety.HasCustom = false;
			comboBoxVariety.IsDataLoaded = false;
			comboBoxVariety.Location = new System.Drawing.Point(555, 348);
			comboBoxVariety.MaxDropDownItems = 12;
			comboBoxVariety.Name = "comboBoxVariety";
			comboBoxVariety.ShowInactiveItems = false;
			comboBoxVariety.Size = new System.Drawing.Size(100, 20);
			comboBoxVariety.TabIndex = 322;
			comboBoxVariety.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVariety.Visible = false;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance107;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(428, 345);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.Size = new System.Drawing.Size(100, 20);
			comboBoxCategory.TabIndex = 321;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.Visible = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(10, 69);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(55, 13);
			label9.TabIndex = 332;
			label9.Text = "Batch No:";
			textBoxBatchNo.Location = new System.Drawing.Point(99, 69);
			textBoxBatchNo.MaxLength = 20;
			textBoxBatchNo.Name = "textBoxBatchNo";
			textBoxBatchNo.Size = new System.Drawing.Size(237, 20);
			textBoxBatchNo.TabIndex = 10;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(944, 672);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(panelArrivalReport);
			base.Controls.Add(panel1);
			base.Controls.Add(panelClaim);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(comboBoxBrand);
			base.Controls.Add(comboBoxVariety);
			base.Controls.Add(comboBoxCategory);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "QualityClaimForm";
			Text = "Quality Claim";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelClaim.ResumeLayout(false);
			panelClaim.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			panelSurveyor1.ResumeLayout(false);
			panelSurveyor1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSurveyor1).EndInit();
			panelSurveyor2.ResumeLayout(false);
			panelSurveyor2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSurveyor2).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelArrivalReport.ResumeLayout(false);
			panelArrivalReport.PerformLayout();
			((System.ComponentModel.ISupportInitialize)vendorsFlatComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVariety).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
