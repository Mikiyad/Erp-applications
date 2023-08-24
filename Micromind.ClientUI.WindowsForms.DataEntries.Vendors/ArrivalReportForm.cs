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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class ArrivalReportForm : Form, IForm
	{
		private bool allowEdit = true;

		private ArrivalReportData currentData;

		private const string TABLENAME_CONST = "Arrival_Report";

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

		private string taskID = "";

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

		private DateTimePicker dateTimePickerReceiveDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private vendorsFlatComboBox comboBoxVendor;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxVendorName;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private JobComboBox comboBoxJob;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator6;

		private TextBox textBoxRef2;

		private Label label2;

		private Label label3;

		private Label label20;

		private Label label19;

		private TextBox textBoxDescription;

		private Label label17;

		private Label label18;

		private TextBox textBoxVesselName;

		private TextBox textBoxContainerNumber;

		private NumberTextBox textBoxTotalQuantity;

		private NumberTextBox textBoxTotalPallet;

		private NumberTextBox textBoxContainerTemp;

		private Label label14;

		private Label label13;

		private Label label9;

		private DateTimePicker dateTimePickerInspectionDate;

		private Label label4;

		private Panel panel1;

		private ComboBox comboBoxConclusion;

		private TextBox textBoxConclusion;

		private ComboBox comboBoxPackType;

		private ComboBox comboBoxPackingCondition;

		private Label label26;

		private Label label29;

		private Label labelWeight;

		private TextBox textBoxTotalIssue1;

		private TextBox textBoxTotalWeightLess;

		private TextBox textBoxTotalIssue2;

		private Label labelIssue4Name;

		private TextBox textBoxTotalIssue3;

		private Label labelIssue3Name;

		private TextBox textBoxTotalIssue4;

		private ArrivalReportTemplateComboBox comboBoxTemplate;

		private CountryComboBox comboBoxOrigin;

		private Label label5;

		private Label labelIssue2Name;

		private Label labelIssue1Name;

		private GenericListComboBox genericListComboBox1;

		private Label label8;

		private ProductCategoryComboBox comboBoxCategory;

		private ProductStyleComboBox comboBoxVariety;

		private ProductBrandComboBox comboBoxBrand;

		private Label label6;

		private ImageList imageList1;

		private XPButton buttonGRN;

		private Label label10;

		private TextBox textBoxGRNNumber;

		private TextBox textBoxGRNSysDocID;

		private Label label7;

		private TextBox textBoxTaskID;

		private SysDocComboBox comboBoxSysDoc;

		private ToolStripButton toolStripButtonPrintPhoto;

		private ToolStripMenuItem duplicateRowToolStripMenuItem;

		private Label labelSysDocQC;

		private LinkLabel linkLabel1;

		private XPButton buttonTempPhoto;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator3;

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
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = true);
					sysDocComboBox.Enabled = enabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["P"].CellActivation = Activation.Disabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool enabled = textBoxVoucherNumber.Enabled = false;
					sysDocComboBox2.Enabled = enabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["P"].CellActivation = Activation.ActivateOnly;
				}
				buttonGRN.Enabled = isNewRecord;
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
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				buttonSave.Enabled = !value;
				panel1.Enabled = !value;
				textBoxNote.Enabled = !value;
				labelVoided.Visible = value;
				if (value)
				{
					buttonVoid.Enabled = false;
					return;
				}
				buttonVoid.Text = UIMessages.Void;
				if (!IsNewRecord)
				{
					buttonVoid.Enabled = true;
				}
				else
				{
					buttonVoid.Enabled = false;
				}
			}
		}

		public event EventHandler LinkClicked;

		public ArrivalReportForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridItems.ContextMenuStrip.Items.AddRange(contextMenuStrip1.Items);
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
			dataGridItems.KeyDown += dataGridItems_KeyDown;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			comboBoxTemplate.SelectedIndexChanged += comboBoxTemplate_SelectedIndexChanged;
			dataGridItems.ClickCellButton += dataGridItems_ClickCellButton;
		}

		private void dataGridItems_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.B)
			{
				DuplicateRow();
			}
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
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			decimal d3 = default(decimal);
			decimal d4 = default(decimal);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal d5 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!row.Cells["Issue1"].Value.IsNullOrEmpty())
				{
					d += decimal.Parse(row.Cells["Issue1"].Value.ToString());
				}
				if (!row.Cells["Issue2"].Value.IsNullOrEmpty())
				{
					d2 += decimal.Parse(row.Cells["Issue2"].Value.ToString());
				}
				if (!row.Cells["Issue3"].Value.IsNullOrEmpty())
				{
					d3 += decimal.Parse(row.Cells["Issue3"].Value.ToString());
				}
				if (!row.Cells["Issue4"].Value.IsNullOrEmpty())
				{
					d4 += decimal.Parse(row.Cells["Issue4"].Value.ToString());
				}
				if (!row.Cells["SampleCount"].Value.IsNullOrEmpty())
				{
					num += decimal.Parse(row.Cells["SampleCount"].Value.ToString());
				}
				if (!row.Cells["StandardWeight"].Value.IsNullOrEmpty())
				{
					num2 += decimal.Parse(row.Cells["StandardWeight"].Value.ToString());
				}
				if (!row.Cells["Weight"].Value.IsNullOrEmpty())
				{
					d5 += decimal.Parse(row.Cells["Weight"].Value.ToString());
				}
			}
			if (num > 0m)
			{
				textBoxTotalIssue1.Text = Math.Round(d / num * 100m, Global.CurDecimalPoints).ToString();
				textBoxTotalIssue2.Text = Math.Round(d2 / num * 100m, Global.CurDecimalPoints).ToString();
				textBoxTotalIssue3.Text = Math.Round(d3 / num * 100m, Global.CurDecimalPoints).ToString();
				textBoxTotalIssue4.Text = Math.Round(d4 / num * 100m, Global.CurDecimalPoints).ToString();
			}
			else
			{
				TextBox textBox = textBoxTotalIssue1;
				TextBox textBox2 = textBoxTotalIssue2;
				TextBox textBox3 = textBoxTotalIssue3;
				string text2 = textBoxTotalIssue4.Text = "0";
				string text4 = textBox3.Text = text2;
				string text7 = textBox.Text = (textBox2.Text = text4);
			}
			if (num2 > 0m && num2 - d5 > 0m)
			{
				textBoxTotalWeightLess.Text = Math.Round((num2 - d5) / num2 * 100m, 2).ToString();
			}
			else
			{
				textBoxTotalWeightLess.Text = "0";
			}
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
				_ = dataGridItems.ActiveRow;
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
						currentData = new ArrivalReportData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.ArrivalReportTable.Rows[0] : currentData.ArrivalReportTable.NewRow();
					dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow["VendorID"] = comboBoxVendor.SelectedID;
					dataRow["DateInspected"] = dateTimePickerInspectionDate.Value;
					dataRow["Reference"] = textBoxRef1.Text;
					dataRow["Reference2"] = textBoxRef2.Text;
					dataRow["TemplateID"] = comboBoxTemplate.SelectedID;
					dataRow["ContainerNumber"] = textBoxContainerNumber.Text;
					dataRow["VesselName"] = textBoxVesselName.Text;
					dataRow["DateReceived"] = dateTimePickerReceiveDate.Value;
					dataRow["ContainerTemp"] = textBoxContainerTemp.Text;
					dataRow["OriginID"] = comboBoxOrigin.SelectedID;
					dataRow["TotalPallets"] = textBoxTotalPallet.Text;
					dataRow["TotalQuantity"] = textBoxTotalQuantity.Text;
					dataRow["Description"] = textBoxDescription.Text;
					dataRow["Note"] = textBoxNote.Text;
					dataRow["Conclusion"] = comboBoxConclusion.SelectedIndex + 1;
					dataRow["TaskID"] = textBoxTaskID.Text;
					dataRow["SourceSysDocID"] = textBoxGRNSysDocID.Text;
					dataRow["SourceVoucherID"] = textBoxGRNNumber.Text;
					dataRow["InspectorID"] = genericListComboBox1.Text;
					dataRow["Note"] = textBoxConclusion.Text;
					dataRow["TotalIssue1"] = ((textBoxTotalIssue1.Text == "") ? "0" : textBoxTotalIssue1.Text);
					dataRow["TotalIssue2"] = ((textBoxTotalIssue1.Text == "") ? "0" : textBoxTotalIssue2.Text);
					dataRow["TotalIssue3"] = ((textBoxTotalIssue1.Text == "") ? "0" : textBoxTotalIssue3.Text);
					dataRow["TotalIssue4"] = ((textBoxTotalIssue1.Text == "") ? "0" : textBoxTotalIssue4.Text);
					dataRow["TotalWeightLess"] = ((textBoxTotalWeightLess.Text == "") ? "0" : textBoxTotalWeightLess.Text);
					dataRow["Issue1Name"] = (labelIssue1Name.Visible ? ((IConvertible)labelIssue1Name.Text.Replace(":", " ").Trim()) : ((IConvertible)DBNull.Value));
					dataRow["Issue2Name"] = (labelIssue2Name.Visible ? ((IConvertible)labelIssue2Name.Text.Replace(":", " ").Trim()) : ((IConvertible)DBNull.Value));
					dataRow["Issue3Name"] = (labelIssue3Name.Visible ? ((IConvertible)labelIssue3Name.Text.Replace(":", " ").Trim()) : ((IConvertible)DBNull.Value));
					dataRow["Issue4Name"] = (labelIssue4Name.Visible ? ((IConvertible)labelIssue4Name.Text.Replace(":", " ").Trim()) : ((IConvertible)DBNull.Value));
					dataRow["PackingCondition"] = comboBoxPackingCondition.SelectedIndex + 1;
					dataRow.EndEdit();
					if (IsNewRecord)
					{
						currentData.ArrivalReportTable.Rows.Add(dataRow);
					}
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (!currentData.ArrivalReportDetailTable.Columns.Contains(column.Key))
						{
							currentData.ArrivalReportDetailTable.Columns.Add(column.Key, column.DataType);
						}
					}
					currentData.ArrivalReportDetailTable.Rows.Clear();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						DataRow dataRow2 = currentData.ArrivalReportDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["ComodityID"] = row.Cells["Commodity"].Value.ToString();
						dataRow2["VarietyID"] = (row.Cells["Variety"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Variety"].Value.ToString()));
						dataRow2["BrandID"] = (row.Cells["Brand"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Brand"].Value.ToString()));
						dataRow2["ItemSize"] = (row.Cells["Size"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Size"].Value.ToString()));
						dataRow2["Grade"] = (row.Cells["Grade"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Grade"].Value.ToString()));
						dataRow2["LotNumber"] = (row.Cells["PalletID"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["PalletID"].Value.ToString()));
						dataRow2["DateCode"] = (row.Cells["DateCode"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["DateCode"].Value.ToString()));
						dataRow2["Grower"] = (row.Cells["Grower"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Grower"].Value.ToString()));
						dataRow2["SampleCount"] = (row.Cells["SampleCount"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["SampleCount"].Value.ToString()));
						dataRow2["StandardWeight"] = (row.Cells["StandardWeight"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["StandardWeight"].Value.ToString()));
						dataRow2["Issue1Count"] = (row.Cells["Issue1"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Issue1"].Value.ToString()));
						dataRow2["Issue2Count"] = (row.Cells["Issue2"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Issue2"].Value.ToString()));
						dataRow2["Issue3Count"] = (row.Cells["Issue3"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Issue3"].Value.ToString()));
						dataRow2["Issue4Count"] = (row.Cells["Issue4"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Issue4"].Value.ToString()));
						dataRow2["Weight"] = (row.Cells["Weight"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Weight"].Value.ToString()));
						dataRow2["Temperature"] = (row.Cells["Temperature"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Temperature"].Value.ToString()));
						dataRow2["Brix"] = (row.Cells["Brix"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Brix"].Value.ToString()));
						dataRow2["Pressure"] = (row.Cells["Pressure"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Pressure"].Value.ToString()));
						dataRow2["NumericAtr1"] = (row.Cells["AtrNum1"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrNum1"].Value.ToString()));
						dataRow2["NumericAtr2"] = (row.Cells["AtrNum2"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrNum2"].Value.ToString()));
						dataRow2["NumericAtr3"] = (row.Cells["AtrNum3"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrNum3"].Value.ToString()));
						dataRow2["NumericAtr4"] = (row.Cells["AtrNum4"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrNum4"].Value.ToString()));
						dataRow2["TextAtr1"] = (row.Cells["AtrText1"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrText1"].Value.ToString()));
						dataRow2["TextAtr2"] = (row.Cells["AtrText2"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrText2"].Value.ToString()));
						dataRow2["TextAtr3"] = (row.Cells["AtrText3"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrText3"].Value.ToString()));
						dataRow2["TextAtr4"] = (row.Cells["AtrText4"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AtrText4"].Value.ToString()));
						dataRow2["Remarks"] = (row.Cells["Remarks"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Remarks"].Value.ToString()));
						dataRow2["RowIndex"] = row.Index;
						dataRow2.EndEdit();
						currentData.ArrivalReportDetailTable.Rows.Add(dataRow2);
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
				dataTable.Columns.Add("Commodity");
				dataTable.Columns.Add("Variety");
				dataTable.Columns.Add("Brand");
				dataTable.Columns.Add("Size");
				dataTable.Columns.Add("Grade");
				dataTable.Columns.Add("PalletID");
				dataTable.Columns.Add("DateCode");
				dataTable.Columns.Add("Grower");
				dataTable.Columns.Add("SampleCount", typeof(decimal));
				dataTable.Columns.Add("StandardWeight", typeof(decimal));
				dataTable.Columns.Add("Issue1", typeof(decimal));
				dataTable.Columns.Add("Issue2", typeof(decimal));
				dataTable.Columns.Add("Issue3", typeof(decimal));
				dataTable.Columns.Add("Issue4", typeof(decimal));
				dataTable.Columns.Add("Weight", typeof(decimal));
				dataTable.Columns.Add("Temperature", typeof(decimal));
				dataTable.Columns.Add("Brix", typeof(decimal));
				dataTable.Columns.Add("Pressure", typeof(decimal));
				dataTable.Columns.Add("AtrNum1", typeof(decimal));
				dataTable.Columns.Add("AtrNum2", typeof(decimal));
				dataTable.Columns.Add("AtrNum3", typeof(decimal));
				dataTable.Columns.Add("AtrNum4", typeof(decimal));
				dataTable.Columns.Add("AtrText1", typeof(string));
				dataTable.Columns.Add("AtrText2", typeof(string));
				dataTable.Columns.Add("AtrText3", typeof(string));
				dataTable.Columns.Add("AtrText4", typeof(string));
				dataTable.Columns.Add("Remarks", typeof(string));
				dataTable.Columns.Add("P");
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"];
				bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["SampleCount"].Header.Caption = "Count";
				dataGridItems.DisplayLayout.Bands[0].Columns["StandardWeight"].Header.Caption = "St.Weight";
				dataGridItems.DisplayLayout.Bands[0].Columns["Temperature"].Header.Caption = "Temp";
				dataGridItems.DisplayLayout.Bands[0].Columns["DateCode"].Header.Caption = "Date Code";
				dataGridItems.DisplayLayout.Bands[0].Columns["PalletID"].Header.Caption = "Lot No";
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].CellButtonAppearance.Image = imageList1.Images[0];
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].ValueList = comboBoxCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["Variety"].ValueList = comboBoxVariety;
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].ValueList = comboBoxBrand;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
				dataGridItems.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commodity"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Variety"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Size"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Grade"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["PalletID"].Width = 70;
				dataGridItems.DisplayLayout.Bands[0].Columns["DateCode"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Grower"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["SampleCount"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["StandardWeight"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue1"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue2"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue3"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue4"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Weight"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Temperature"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Brix"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Pressure"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum2"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum3"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum4"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText1"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText2"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText3"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText4"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = 300;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].Width = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].LockedWidth = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].MinLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Issue1", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Issue1"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue1"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue1"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue1"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue1"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue1"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Issue2", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Issue2"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue2"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue2"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue2"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue2"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue2"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Issue3", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Issue3"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue3"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue3"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue3"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue3"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue3"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Issue4", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Issue4"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue4"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue4"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue4"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue4"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Issue4"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Weight", SummaryType.Average, dataGridItems.DisplayLayout.Bands[0].Columns["Weight"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Weight"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Weight"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Weight"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Weight"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Weight"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("AtrNum1", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum1"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum1"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum1"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum1"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum1"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("AtrNum2", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum2"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum2"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum2"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum2"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum2"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum2"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("AtrNum3", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum3"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum3"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum3"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum3"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum3"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum3"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("AtrNum4", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum4"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum4"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum4"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum4"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum4"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["AtrNum4"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("SampleCount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["SampleCount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["SampleCount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["SampleCount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["SampleCount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["SampleCount"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["SampleCount"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].CellActivation = Activation.Disabled;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			if (comboBoxTemplate.SelectedID == "")
			{
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Issue1"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Issue2"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Issue3"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Issue4"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum2"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum3"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum4"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrText1"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrText2"];
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrText3"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["AtrText4"].Hidden = true;
				bool flag4 = ultraGridColumn11.Hidden = flag2;
				bool flag6 = ultraGridColumn10.Hidden = flag4;
				bool flag8 = ultraGridColumn9.Hidden = flag6;
				bool flag10 = ultraGridColumn8.Hidden = flag8;
				bool flag12 = ultraGridColumn7.Hidden = flag10;
				bool flag14 = ultraGridColumn6.Hidden = flag12;
				bool flag16 = ultraGridColumn5.Hidden = flag14;
				bool flag18 = ultraGridColumn4.Hidden = flag16;
				bool flag20 = ultraGridColumn3.Hidden = flag18;
				bool hidden = ultraGridColumn2.Hidden = flag20;
				ultraGridColumn.Hidden = hidden;
				return;
			}
			DataRow dataRow = Factory.ArrivalReportTemplateSystem.GetArrivalReportTemplateByID(comboBoxTemplate.SelectedID).ArrivalReportTemplateTable.Rows[0];
			if (dataRow["Issue1Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue1"].Hidden = true;
				Label label = labelIssue1Name;
				bool hidden = textBoxTotalIssue1.Visible = false;
				label.Visible = hidden;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue1"].Hidden = false;
				string text3 = labelIssue1Name.Text = (dataGridItems.DisplayLayout.Bands[0].Columns["Issue1"].Header.Caption = dataRow["Issue1Name"].ToString());
				Label label2 = labelIssue1Name;
				bool hidden = textBoxTotalIssue1.Visible = true;
				label2.Visible = hidden;
			}
			if (dataRow["Issue2Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue2"].Hidden = true;
				Label label3 = labelIssue2Name;
				bool hidden = textBoxTotalIssue2.Visible = false;
				label3.Visible = hidden;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue2"].Hidden = false;
				string text3 = labelIssue2Name.Text = (dataGridItems.DisplayLayout.Bands[0].Columns["Issue2"].Header.Caption = dataRow["Issue2Name"].ToString());
				Label label4 = labelIssue2Name;
				bool hidden = textBoxTotalIssue2.Visible = true;
				label4.Visible = hidden;
			}
			if (dataRow["Issue3Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue3"].Hidden = true;
				Label label5 = labelIssue3Name;
				bool hidden = textBoxTotalIssue3.Visible = false;
				label5.Visible = hidden;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue3"].Hidden = false;
				string text3 = labelIssue3Name.Text = (dataGridItems.DisplayLayout.Bands[0].Columns["Issue3"].Header.Caption = dataRow["Issue3Name"].ToString());
				Label label6 = labelIssue3Name;
				bool hidden = textBoxTotalIssue3.Visible = true;
				label6.Visible = hidden;
			}
			if (dataRow["Issue4Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue4"].Hidden = true;
				Label label7 = labelIssue4Name;
				bool hidden = textBoxTotalIssue4.Visible = false;
				label7.Visible = hidden;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Issue4"].Hidden = false;
				string text3 = labelIssue4Name.Text = (dataGridItems.DisplayLayout.Bands[0].Columns["Issue4"].Header.Caption = dataRow["Issue4Name"].ToString());
				Label label8 = labelIssue4Name;
				bool hidden = textBoxTotalIssue4.Visible = true;
				label8.Visible = hidden;
			}
			if (dataRow["AtrNum1Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum1"].Header.Caption = dataRow["AtrNum1Name"].ToString();
			}
			if (dataRow["AtrNum2Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum2"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum2"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum2"].Header.Caption = dataRow["AtrNum2Name"].ToString();
			}
			if (dataRow["AtrNum3Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum3"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum3"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum3"].Header.Caption = dataRow["AtrNum3Name"].ToString();
			}
			if (dataRow["AtrNum4Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum4"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum4"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrNum4"].Header.Caption = dataRow["AtrNum4Name"].ToString();
			}
			if (dataRow["AtrText1Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText1"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText1"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText1"].Header.Caption = dataRow["AtrText1Name"].ToString();
			}
			if (dataRow["AtrText2Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText2"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText2"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText2"].Header.Caption = dataRow["AtrText2Name"].ToString();
			}
			if (dataRow["AtrText3Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText3"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText3"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText3"].Header.Caption = dataRow["AtrText3Name"].ToString();
			}
			if (dataRow["AtrText4Name"].IsDBNullOrEmpty())
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText4"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText4"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["AtrText4"].Header.Caption = dataRow["AtrText4Name"].ToString();
			}
			if (dataRow["IsBrix"].IsDBNullOrEmpty() || !bool.Parse(dataRow["IsBrix"].ToString()))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Brix"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Brix"].Hidden = false;
			}
			if (dataRow["IsPressure"].IsDBNullOrEmpty() || !bool.Parse(dataRow["IsPressure"].ToString()))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Pressure"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Pressure"].Hidden = false;
			}
			if (dataRow["IsGrower"].IsDBNullOrEmpty() || !bool.Parse(dataRow["IsGrower"].ToString()))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Grower"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Grower"].Hidden = false;
			}
			if (dataRow["IsDateCode"].IsDBNullOrEmpty() || !bool.Parse(dataRow["IsDateCode"].ToString()))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["DateCode"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["DateCode"].Hidden = false;
			}
			if (dataRow["IsPalletID"].IsDBNullOrEmpty() || !bool.Parse(dataRow["IsPalletID"].ToString()))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["PalletID"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["PalletID"].Hidden = false;
			}
			if (dataRow["IsTemperature"].IsDBNullOrEmpty() || !bool.Parse(dataRow["IsTemperature"].ToString()))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Temperature"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Temperature"].Hidden = false;
			}
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
					currentData = Factory.ArrivalReportSystem.GetArrivalReportByID(SystemDocID, voucherID);
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
						DataRow dataRow = currentData.Tables["Arrival_Report"].Rows[0];
						comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
						textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
						comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
						dateTimePickerInspectionDate.Value = DateTime.Parse(dataRow["DateInspected"].ToString());
						textBoxRef1.Text = dataRow["Reference"].ToString();
						textBoxRef2.Text = dataRow["Reference2"].ToString();
						comboBoxTemplate.SelectedID = dataRow["TemplateID"].ToString();
						textBoxContainerNumber.Text = dataRow["ContainerNumber"].ToString();
						textBoxVesselName.Text = dataRow["VesselName"].ToString();
						dateTimePickerReceiveDate.Value = DateTime.Parse(dataRow["DateReceived"].ToString());
						textBoxContainerTemp.Text = dataRow["ContainerTemp"].ToString();
						comboBoxOrigin.SelectedID = dataRow["OriginID"].ToString();
						textBoxTotalPallet.Text = dataRow["TotalPallets"].ToString();
						textBoxTotalQuantity.Text = dataRow["TotalQuantity"].ToString();
						textBoxDescription.Text = dataRow["Description"].ToString();
						textBoxNote.Text = dataRow["Note"].ToString();
						string text2 = taskID = (textBoxTaskID.Text = dataRow["TaskID"].ToString());
						textBoxGRNSysDocID.Text = dataRow["SourceSysDocID"].ToString();
						textBoxGRNNumber.Text = dataRow["SourceVoucherID"].ToString();
						if (dataRow["Conclusion"] != DBNull.Value)
						{
							comboBoxConclusion.SelectedIndex = unchecked((int)byte.Parse(dataRow["Conclusion"].ToString())) - 1;
						}
						else
						{
							comboBoxConclusion.SelectedIndex = 0;
						}
						textBoxTotalIssue1.Text = dataRow["TotalIssue1"].ToString();
						textBoxTotalIssue2.Text = dataRow["TotalIssue2"].ToString();
						textBoxTotalIssue3.Text = dataRow["TotalIssue3"].ToString();
						textBoxTotalIssue4.Text = dataRow["TotalIssue4"].ToString();
						genericListComboBox1.Text = dataRow["InspectorID"].ToString();
						textBoxConclusion.Text = dataRow["Note"].ToString();
						if (!dataRow["TotalWeightLess"].IsDBNullOrEmpty())
						{
							textBoxTotalWeightLess.Text = dataRow["TotalWeightLess"].ToString();
						}
						else
						{
							textBoxTotalWeightLess.Text = "0";
						}
						if (dataRow["QualityClaim"] != DBNull.Value)
						{
							linkLabel1.Visible = true;
							linkLabel1.Text = dataRow["QualityClaim"].ToString();
							labelSysDocQC.Text = dataRow["QualityClaimSysDoc"].ToString();
						}
						else
						{
							linkLabel1.Visible = false;
						}
						comboBoxPackingCondition.SelectedIndex = int.Parse(dataRow["PackingCondition"].ToString()) - 1;
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						dataTable.Rows.Clear();
						if (currentData.Tables.Contains("Arrival_Report_Detail") && currentData.ArrivalReportDetailTable.Rows.Count != 0)
						{
							foreach (DataRow row in currentData.Tables["Arrival_Report_Detail"].Rows)
							{
								DataRow dataRow3 = dataTable.NewRow();
								dataRow3["Commodity"] = row["ComodityID"].ToString();
								dataRow3["Variety"] = row["VarietyID"];
								dataRow3["Brand"] = row["BrandID"];
								dataRow3["Size"] = row["ItemSize"];
								dataRow3["Grade"] = row["Grade"];
								dataRow3["PalletID"] = row["LotNumber"];
								dataRow3["DateCode"] = row["DateCode"];
								dataRow3["Grower"] = row["Grower"];
								dataRow3["SampleCount"] = row["SampleCount"];
								dataRow3["StandardWeight"] = row["StandardWeight"];
								dataRow3["Issue1"] = row["Issue1Count"];
								dataRow3["Issue2"] = row["Issue2Count"];
								dataRow3["Issue3"] = row["Issue3Count"];
								dataRow3["Issue4"] = row["Issue4Count"];
								dataRow3["Weight"] = row["Weight"];
								dataRow3["Brix"] = row["Brix"];
								dataRow3["Pressure"] = row["Pressure"];
								dataRow3["AtrNum1"] = row["NumericAtr1"];
								dataRow3["StandardWeight"] = row["StandardWeight"];
								dataRow3["AtrNum2"] = row["NumericAtr2"];
								dataRow3["AtrNum3"] = row["NumericAtr3"];
								dataRow3["AtrNum4"] = row["NumericAtr4"];
								dataRow3["AtrText1"] = row["TextAtr1"];
								dataRow3["AtrText2"] = row["TextAtr2"];
								dataRow3["AtrText3"] = row["TextAtr3"];
								dataRow3["AtrText4"] = row["TextAtr4"];
								dataRow3["Remarks"] = row["Remarks"];
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
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
				flag = Factory.ArrivalReportSystem.CreateArrivalReport(currentData, !isNewRecord);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Arrival_Report", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			DateTime value = dateTimePickerReceiveDate.Value;
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
				if (isNewRecord && dateTimePickerReceiveDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerReceiveDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (comboBoxTemplate.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select a template for the report.");
					comboBoxTemplate.Focus();
					return false;
				}
				if (comboBoxConclusion.SelectedIndex == -1)
				{
					ErrorHelper.InformationMessage("Please select conclusion for the report.");
					comboBoxConclusion.Focus();
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxVendor.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (!IsNewRecord && !Factory.ArrivalReportSystem.CanUpdate(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this order has been already received. You are not able to modify.");
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else if (dataGridItems.Rows[i].Cells["Commodity"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select a commodity.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Arrival_Report", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				return true;
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
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				comboBoxOrigin.Clear();
				comboBoxPackingCondition.SelectedIndex = 0;
				textBoxTotalPallet.Text = "0";
				textBoxTotalQuantity.Text = "0";
				textBoxDescription.Clear();
				comboBoxConclusion.SelectedIndex = -1;
				textBoxConclusion.Clear();
				textBoxTotalWeightLess.Text = "0";
				textBoxVesselName.Clear();
				textBoxContainerNumber.Clear();
				textBoxContainerTemp.Text = "0";
				dateTimePickerReceiveDate.Value = DateTime.Now;
				textBoxTaskID.Clear();
				textBoxGRNNumber.Clear();
				textBoxGRNSysDocID.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxVendorName.Clear();
				comboBoxVendor.Clear();
				textBoxTotalIssue1.Text = "0";
				textBoxTotalIssue2.Text = "0";
				textBoxTotalIssue3.Text = "0";
				labelIssue4Name.Text = "0";
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
				if (!Factory.ArrivalReportSystem.CanUpdate(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this order has been already received. You are not able to delete.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.ArrivalReportSystem.DeleteArrivalReport(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Arrival_Report", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Arrival_Report", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Arrival_Report", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Arrival_Report", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Arrival_Report", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.ArrivalReport);
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
				if (!Factory.ArrivalReportSystem.CanUpdate(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ArrivalReport);
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
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
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
					DataSet arrivalReportToPrint = Factory.ArrivalReportSystem.GetArrivalReportToPrint(selectedID, text);
					if (arrivalReportToPrint == null || arrivalReportToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string defaultTemplateName = arrivalReportToPrint.Tables["Arrival_Report_Template"].Rows[0]["TemplateName"].ToString();
						PrintHelper.PrintDocument(arrivalReportToPrint, selectedID, defaultTemplateName, SysDocTypes.ArrivalReport, isPrint, showPrintDialog);
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
			FormActivator.BringFormToFront(FormActivator.ArrivalReportListFormObj);
		}

		private void closeOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdatePOStatusDialog updatePOStatusDialog = new UpdatePOStatusDialog();
			updatePOStatusDialog.SetDocument(SysDocTypes.ArrivalReport, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
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

		private void buttonGRN_Click(object sender, EventArgs e)
		{
			try
			{
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.RequireDataRefresh += form_RequireDataRefresh;
				selectDocumentDialog.Text = "Select Task";
				selectDocumentDialog.AllowDateFilter = true;
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.ValidateSelection += form_ValidateSelection;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						string text2 = taskID = (textBoxTaskID.Text = selectedRow.Cells["Task Code"].Value.ToString());
						textBoxGRNSysDocID.Text = selectedRow.Cells["GRN DocID"].Value.ToString();
						textBoxGRNNumber.Text = selectedRow.Cells["GRN Number"].Value.ToString();
						comboBoxVendor.SelectedID = selectedRow.Cells["VendorID"].Value.ToString();
						textBoxContainerNumber.Text = selectedRow.Cells["Container#"].Value.ToString();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void form_RequireDataRefresh(object sender, DateRangeStruct e)
		{
			try
			{
				DataSet qualityTaskList = Factory.QualityTaskSystem.GetQualityTaskList(includeClosedTasks: false);
				SelectDocumentDialog obj = sender as SelectDocumentDialog;
				obj.DataSource = qualityTaskList;
				obj.Grid.DisplayLayout.Bands[0].Columns["VendorID"].Hidden = true;
				obj.Grid.DisplayLayout.Bands[0].Columns["VendorID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
		}

		private void duplicateRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DuplicateRow();
		}

		private void DuplicateRow()
		{
			try
			{
				if (dataGridItems.ActiveRow != null)
				{
					UltraGridRow activeRow = dataGridItems.ActiveRow;
					UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (!activeRow.Cells[column].Value.IsNullOrEmpty())
						{
							ultraGridRow.Cells[column].Value = activeRow.Cells[column].Value;
						}
					}
					ultraGridRow.ParentCollection.Move(ultraGridRow, checked(dataGridItems.Rows.Count - 1));
					dataGridItems.ActiveRowScrollRegion.ScrollRowIntoView(ultraGridRow);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string text = labelSysDocQC.Text;
			string text2 = linkLabel1.Text;
			new FormHelper().EditTransaction(TransactionListType.QualityClaim, text, text2);
		}

		private void buttonTempPhoto_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				DocManagementForm docManagementForm = new DocManagementForm();
				docManagementForm.EntityID = textBoxVoucherNumber.Text;
				docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
				docManagementForm.EntityRowIndex = 1000;
				docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
				docManagementForm.EntityType = EntityTypesEnum.Transactions;
				docManagementForm.ShowDialog(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.ArrivalReportForm));
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
			toolStripButtonPrintPhoto = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerReceiveDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			buttonTempPhoto = new Micromind.UISupport.XPButton();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			label10 = new System.Windows.Forms.Label();
			textBoxGRNNumber = new System.Windows.Forms.TextBox();
			textBoxGRNSysDocID = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			textBoxTaskID = new System.Windows.Forms.TextBox();
			buttonGRN = new Micromind.UISupport.XPButton();
			label8 = new System.Windows.Forms.Label();
			genericListComboBox1 = new Micromind.DataControls.GenericListComboBox();
			comboBoxPackType = new System.Windows.Forms.ComboBox();
			comboBoxOrigin = new Micromind.DataControls.CountryComboBox();
			label26 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label19 = new System.Windows.Forms.Label();
			textBoxDescription = new System.Windows.Forms.TextBox();
			label17 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			textBoxVesselName = new System.Windows.Forms.TextBox();
			textBoxContainerNumber = new System.Windows.Forms.TextBox();
			textBoxTotalQuantity = new Micromind.UISupport.NumberTextBox();
			textBoxTotalPallet = new Micromind.UISupport.NumberTextBox();
			textBoxContainerTemp = new Micromind.UISupport.NumberTextBox();
			label14 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			dateTimePickerInspectionDate = new System.Windows.Forms.DateTimePicker();
			label4 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			label20 = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			duplicateRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panel1 = new System.Windows.Forms.Panel();
			linkLabel1 = new System.Windows.Forms.LinkLabel();
			labelSysDocQC = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			textBoxConclusion = new System.Windows.Forms.TextBox();
			comboBoxConclusion = new System.Windows.Forms.ComboBox();
			comboBoxPackingCondition = new System.Windows.Forms.ComboBox();
			label29 = new System.Windows.Forms.Label();
			textBoxTotalWeightLess = new System.Windows.Forms.TextBox();
			labelWeight = new System.Windows.Forms.Label();
			labelIssue2Name = new System.Windows.Forms.Label();
			labelIssue1Name = new System.Windows.Forms.Label();
			textBoxTotalIssue4 = new System.Windows.Forms.TextBox();
			labelIssue4Name = new System.Windows.Forms.Label();
			textBoxTotalIssue1 = new System.Windows.Forms.TextBox();
			textBoxTotalIssue3 = new System.Windows.Forms.TextBox();
			textBoxTotalIssue2 = new System.Windows.Forms.TextBox();
			labelIssue3Name = new System.Windows.Forms.Label();
			imageList1 = new System.Windows.Forms.ImageList(components);
			formManager = new Micromind.DataControls.FormManager();
			comboBoxTemplate = new Micromind.DataControls.ArrivalReportTemplateComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxBrand = new Micromind.DataControls.ProductBrandComboBox();
			comboBoxVariety = new Micromind.DataControls.ProductStyleComboBox();
			comboBoxCategory = new Micromind.DataControls.ProductCategoryComboBox();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOrigin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			contextMenuStrip1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTemplate).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVariety).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
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
				toolStripButtonPrintPhoto,
				toolStripSeparator3,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1041, 31);
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
			toolStripButtonPrintPhoto.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPrintPhoto.Image");
			toolStripButtonPrintPhoto.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrintPhoto.Name = "toolStripButtonPrintPhoto";
			toolStripButtonPrintPhoto.Size = new System.Drawing.Size(111, 28);
			toolStripButtonPrintPhoto.Text = "Print with Photo";
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				duplicateToolStripMenuItem
			});
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(55, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 632);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1041, 40);
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
			linePanelDown.Size = new System.Drawing.Size(1041, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(931, 8);
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
			dateTimePickerReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerReceiveDate.Location = new System.Drawing.Point(488, 47);
			dateTimePickerReceiveDate.Name = "dateTimePickerReceiveDate";
			dateTimePickerReceiveDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerReceiveDate.TabIndex = 11;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(138, 20);
			textBoxVoucherNumber.TabIndex = 2;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(407, 95);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(27, 13);
			label1.TabIndex = 20;
			label1.Text = "Ref:";
			textBoxRef1.Location = new System.Drawing.Point(445, 91);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(121, 20);
			textBoxRef1.TabIndex = 19;
			textBoxNote.Location = new System.Drawing.Point(495, 116);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(366, 70);
			textBoxNote.TabIndex = 22;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(212, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel2.TabIndex = 1;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(buttonTempPhoto);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(label10);
			panelDetails.Controls.Add(textBoxGRNNumber);
			panelDetails.Controls.Add(textBoxGRNSysDocID);
			panelDetails.Controls.Add(label7);
			panelDetails.Controls.Add(textBoxTaskID);
			panelDetails.Controls.Add(buttonGRN);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(genericListComboBox1);
			panelDetails.Controls.Add(comboBoxPackType);
			panelDetails.Controls.Add(comboBoxOrigin);
			panelDetails.Controls.Add(label26);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(label19);
			panelDetails.Controls.Add(textBoxDescription);
			panelDetails.Controls.Add(label17);
			panelDetails.Controls.Add(label18);
			panelDetails.Controls.Add(textBoxVesselName);
			panelDetails.Controls.Add(textBoxContainerNumber);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxTotalQuantity);
			panelDetails.Controls.Add(textBoxTotalPallet);
			panelDetails.Controls.Add(textBoxContainerTemp);
			panelDetails.Controls.Add(label14);
			panelDetails.Controls.Add(label13);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(dateTimePickerInspectionDate);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxVendorName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerReceiveDate);
			panelDetails.Location = new System.Drawing.Point(0, 32);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(1017, 191);
			panelDetails.TabIndex = 0;
			buttonTempPhoto.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonTempPhoto.BackColor = System.Drawing.Color.DarkGray;
			buttonTempPhoto.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonTempPhoto.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonTempPhoto.Image = Micromind.ClientUI.Properties.Resources.camera_icon;
			buttonTempPhoto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonTempPhoto.Location = new System.Drawing.Point(184, 69);
			buttonTempPhoto.Name = "buttonTempPhoto";
			buttonTempPhoto.Size = new System.Drawing.Size(25, 22);
			buttonTempPhoto.TabIndex = 330;
			buttonTempPhoto.UseVisualStyleBackColor = false;
			buttonTempPhoto.Click += new System.EventHandler(buttonTempPhoto_Click);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance3;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(99, 0);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(661, 5);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(34, 13);
			label10.TabIndex = 329;
			label10.Text = "GRN:";
			textBoxGRNNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGRNNumber.Location = new System.Drawing.Point(751, 1);
			textBoxGRNNumber.MaxLength = 64;
			textBoxGRNNumber.Name = "textBoxGRNNumber";
			textBoxGRNNumber.ReadOnly = true;
			textBoxGRNNumber.Size = new System.Drawing.Size(110, 20);
			textBoxGRNNumber.TabIndex = 6;
			textBoxGRNNumber.TabStop = false;
			textBoxGRNSysDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGRNSysDocID.Location = new System.Drawing.Point(701, 1);
			textBoxGRNSysDocID.MaxLength = 64;
			textBoxGRNSysDocID.Name = "textBoxGRNSysDocID";
			textBoxGRNSysDocID.ReadOnly = true;
			textBoxGRNSysDocID.Size = new System.Drawing.Size(47, 20);
			textBoxGRNSysDocID.TabIndex = 5;
			textBoxGRNSysDocID.TabStop = false;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(469, 5);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(34, 13);
			label7.TabIndex = 327;
			label7.Text = "Task:";
			textBoxTaskID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaskID.Location = new System.Drawing.Point(511, 1);
			textBoxTaskID.MaxLength = 64;
			textBoxTaskID.Name = "textBoxTaskID";
			textBoxTaskID.ReadOnly = true;
			textBoxTaskID.Size = new System.Drawing.Size(103, 20);
			textBoxTaskID.TabIndex = 3;
			textBoxTaskID.TabStop = false;
			buttonGRN.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonGRN.BackColor = System.Drawing.Color.DarkGray;
			buttonGRN.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonGRN.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonGRN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonGRN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonGRN.Location = new System.Drawing.Point(618, -1);
			buttonGRN.Name = "buttonGRN";
			buttonGRN.Size = new System.Drawing.Size(32, 22);
			buttonGRN.TabIndex = 4;
			buttonGRN.Text = "...";
			buttonGRN.UseVisualStyleBackColor = false;
			buttonGRN.Click += new System.EventHandler(buttonGRN_Click);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(10, 94);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(54, 13);
			label8.TabIndex = 324;
			label8.Text = "Inspector:";
			genericListComboBox1.Assigned = false;
			genericListComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBox1.CustomReportFieldName = "";
			genericListComboBox1.CustomReportKey = "";
			genericListComboBox1.CustomReportValueType = 1;
			genericListComboBox1.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBox1.DisplayLayout.Appearance = appearance15;
			genericListComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			genericListComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			genericListComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			genericListComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBox1.DisplayLayout.Override.CellAppearance = appearance22;
			genericListComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			genericListComboBox1.DisplayLayout.Override.HeaderAppearance = appearance24;
			genericListComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			genericListComboBox1.DisplayLayout.Override.RowAppearance = appearance25;
			genericListComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			genericListComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBox1.Editable = true;
			genericListComboBox1.FilterString = "";
			genericListComboBox1.GenericListType = Micromind.Common.Data.GenericListTypes.QCInspector;
			genericListComboBox1.HasAllAccount = false;
			genericListComboBox1.HasCustom = false;
			genericListComboBox1.IsDataLoaded = false;
			genericListComboBox1.IsSingleColumn = false;
			genericListComboBox1.Location = new System.Drawing.Point(99, 91);
			genericListComboBox1.MaxDropDownItems = 12;
			genericListComboBox1.Name = "genericListComboBox1";
			genericListComboBox1.ShowInactiveItems = false;
			genericListComboBox1.ShowQuickAdd = true;
			genericListComboBox1.Size = new System.Drawing.Size(109, 20);
			genericListComboBox1.TabIndex = 17;
			genericListComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPackType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxPackType.FormattingEnabled = true;
			comboBoxPackType.Items.AddRange(new object[3]
			{
				"Palletized",
				"Non-Palletized",
				"Break Bulk"
			});
			comboBoxPackType.Location = new System.Drawing.Point(291, 91);
			comboBoxPackType.MaxLength = 20;
			comboBoxPackType.Name = "comboBoxPackType";
			comboBoxPackType.Size = new System.Drawing.Size(109, 21);
			comboBoxPackType.TabIndex = 18;
			comboBoxOrigin.Assigned = false;
			comboBoxOrigin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOrigin.CustomReportFieldName = "";
			comboBoxOrigin.CustomReportKey = "";
			comboBoxOrigin.CustomReportValueType = 1;
			comboBoxOrigin.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOrigin.DisplayLayout.Appearance = appearance27;
			comboBoxOrigin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOrigin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOrigin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxOrigin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOrigin.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxOrigin.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOrigin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOrigin.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOrigin.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxOrigin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOrigin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOrigin.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxOrigin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOrigin.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOrigin.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxOrigin.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxOrigin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOrigin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxOrigin.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxOrigin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOrigin.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxOrigin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOrigin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOrigin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOrigin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOrigin.Editable = true;
			comboBoxOrigin.FilterString = "";
			comboBoxOrigin.HasAllAccount = false;
			comboBoxOrigin.HasCustom = false;
			comboBoxOrigin.IsDataLoaded = false;
			comboBoxOrigin.Location = new System.Drawing.Point(291, 70);
			comboBoxOrigin.MaxDropDownItems = 12;
			comboBoxOrigin.Name = "comboBoxOrigin";
			comboBoxOrigin.ShowInactiveItems = false;
			comboBoxOrigin.ShowQuickAdd = true;
			comboBoxOrigin.Size = new System.Drawing.Size(110, 20);
			comboBoxOrigin.TabIndex = 14;
			comboBoxOrigin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label26.AutoSize = true;
			label26.Location = new System.Drawing.Point(209, 94);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(76, 13);
			label26.TabIndex = 28;
			label26.Text = "Packing Type:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(214, 72);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(37, 13);
			label5.TabIndex = 321;
			label5.Text = "Origin:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(439, 118);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 319;
			label3.Text = "Note:";
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(10, 120);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(52, 13);
			label19.TabIndex = 316;
			label19.Text = "Remarks:";
			textBoxDescription.Location = new System.Drawing.Point(99, 116);
			textBoxDescription.MaxLength = 5000;
			textBoxDescription.Multiline = true;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxDescription.Size = new System.Drawing.Size(334, 69);
			textBoxDescription.TabIndex = 21;
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(10, 50);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(72, 13);
			label17.TabIndex = 312;
			label17.Text = "Vessel Name:";
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(213, 50);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(72, 13);
			label18.TabIndex = 313;
			label18.Text = "Container No:";
			textBoxVesselName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVesselName.Location = new System.Drawing.Point(99, 47);
			textBoxVesselName.MaxLength = 15;
			textBoxVesselName.Name = "textBoxVesselName";
			textBoxVesselName.Size = new System.Drawing.Size(109, 20);
			textBoxVesselName.TabIndex = 9;
			textBoxContainerNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxContainerNumber.Location = new System.Drawing.Point(291, 47);
			textBoxContainerNumber.MaxLength = 20;
			textBoxContainerNumber.Name = "textBoxContainerNumber";
			textBoxContainerNumber.Size = new System.Drawing.Size(109, 20);
			textBoxContainerNumber.TabIndex = 10;
			textBoxTotalQuantity.AllowDecimal = true;
			textBoxTotalQuantity.CustomReportFieldName = "";
			textBoxTotalQuantity.CustomReportKey = "";
			textBoxTotalQuantity.CustomReportValueType = 1;
			textBoxTotalQuantity.IsComboTextBox = false;
			textBoxTotalQuantity.Location = new System.Drawing.Point(627, 70);
			textBoxTotalQuantity.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalQuantity.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalQuantity.Name = "textBoxTotalQuantity";
			textBoxTotalQuantity.NullText = "0";
			textBoxTotalQuantity.Size = new System.Drawing.Size(84, 20);
			textBoxTotalQuantity.TabIndex = 16;
			textBoxTotalQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalPallet.AllowDecimal = true;
			textBoxTotalPallet.CustomReportFieldName = "";
			textBoxTotalPallet.CustomReportKey = "";
			textBoxTotalPallet.CustomReportValueType = 1;
			textBoxTotalPallet.IsComboTextBox = false;
			textBoxTotalPallet.Location = new System.Drawing.Point(489, 70);
			textBoxTotalPallet.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalPallet.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalPallet.Name = "textBoxTotalPallet";
			textBoxTotalPallet.NullText = "0";
			textBoxTotalPallet.Size = new System.Drawing.Size(77, 20);
			textBoxTotalPallet.TabIndex = 15;
			textBoxTotalPallet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxContainerTemp.AllowDecimal = true;
			textBoxContainerTemp.CustomReportFieldName = "";
			textBoxContainerTemp.CustomReportKey = "";
			textBoxContainerTemp.CustomReportValueType = 1;
			textBoxContainerTemp.IsComboTextBox = false;
			textBoxContainerTemp.Location = new System.Drawing.Point(99, 70);
			textBoxContainerTemp.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxContainerTemp.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxContainerTemp.Name = "textBoxContainerTemp";
			textBoxContainerTemp.NullText = "0";
			textBoxContainerTemp.Size = new System.Drawing.Size(83, 20);
			textBoxContainerTemp.TabIndex = 13;
			textBoxContainerTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(569, 74);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(49, 13);
			label14.TabIndex = 299;
			label14.Text = "Quantity:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(407, 73);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(72, 13);
			label13.TabIndex = 297;
			label13.Text = "Pallets Count:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(10, 73);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(85, 13);
			label9.TabIndex = 298;
			label9.Text = "Container Temp:";
			dateTimePickerInspectionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerInspectionDate.Location = new System.Drawing.Point(715, 47);
			dateTimePickerInspectionDate.Name = "dateTimePickerInspectionDate";
			dateTimePickerInspectionDate.Size = new System.Drawing.Size(124, 20);
			dateTimePickerInspectionDate.TabIndex = 12;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(406, 50);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(76, 13);
			label4.TabIndex = 144;
			label4.Text = "Receive Date:";
			textBoxRef2.Location = new System.Drawing.Point(627, 91);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(121, 20);
			textBoxRef2.TabIndex = 20;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(571, 95);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(36, 13);
			label2.TabIndex = 142;
			label2.Text = "Ref 2:";
			appearance39.FontData.BoldAsString = "True";
			appearance39.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance39;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(10, 23);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(63, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			appearance40.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance40;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxVendor.AlwaysInEditMode = true;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance41;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
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
			comboBoxVendor.TabIndex = 7;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance53.FontData.BoldAsString = "True";
			appearance53.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance53;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance54;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(610, 50);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(101, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Inspection Date:";
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(211, 23);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(253, 20);
			textBoxVendorName.TabIndex = 8;
			textBoxVendorName.TabStop = false;
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(7, 229);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(54, 13);
			label20.TabIndex = 318;
			label20.Text = "Template:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(7, 441);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(1022, 64);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				duplicateRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(143, 26);
			duplicateRowToolStripMenuItem.Name = "duplicateRowToolStripMenuItem";
			duplicateRowToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			duplicateRowToolStripMenuItem.Text = "Duplicate Row";
			duplicateRowToolStripMenuItem.Click += new System.EventHandler(duplicateRowToolStripMenuItem_Click);
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panel1.Controls.Add(linkLabel1);
			panel1.Controls.Add(labelSysDocQC);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(textBoxConclusion);
			panel1.Controls.Add(comboBoxConclusion);
			panel1.Controls.Add(comboBoxPackingCondition);
			panel1.Controls.Add(label29);
			panel1.Controls.Add(textBoxTotalWeightLess);
			panel1.Controls.Add(labelWeight);
			panel1.Controls.Add(labelIssue2Name);
			panel1.Controls.Add(labelIssue1Name);
			panel1.Controls.Add(textBoxTotalIssue4);
			panel1.Controls.Add(labelIssue4Name);
			panel1.Controls.Add(textBoxTotalIssue1);
			panel1.Controls.Add(textBoxTotalIssue3);
			panel1.Controls.Add(textBoxTotalIssue2);
			panel1.Controls.Add(labelIssue3Name);
			panel1.Location = new System.Drawing.Point(6, 513);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(1025, 113);
			panel1.TabIndex = 122;
			linkLabel1.AutoSize = true;
			linkLabel1.Location = new System.Drawing.Point(660, 16);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new System.Drawing.Size(0, 13);
			linkLabel1.TabIndex = 332;
			linkLabel1.Visible = false;
			linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
			labelSysDocQC.AutoSize = true;
			labelSysDocQC.Location = new System.Drawing.Point(821, 12);
			labelSysDocQC.Name = "labelSysDocQC";
			labelSysDocQC.Size = new System.Drawing.Size(0, 13);
			labelSysDocQC.TabIndex = 331;
			labelSysDocQC.Visible = false;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(440, 14);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(62, 13);
			label6.TabIndex = 39;
			label6.Text = "Conclusion:";
			textBoxConclusion.Location = new System.Drawing.Point(525, 34);
			textBoxConclusion.MaxLength = 5000;
			textBoxConclusion.Multiline = true;
			textBoxConclusion.Name = "textBoxConclusion";
			textBoxConclusion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxConclusion.Size = new System.Drawing.Size(308, 65);
			textBoxConclusion.TabIndex = 1;
			comboBoxConclusion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxConclusion.FormattingEnabled = true;
			comboBoxConclusion.Items.AddRange(new object[2]
			{
				"No Claim",
				"Claimable"
			});
			comboBoxConclusion.Location = new System.Drawing.Point(525, 11);
			comboBoxConclusion.MaxLength = 20;
			comboBoxConclusion.Name = "comboBoxConclusion";
			comboBoxConclusion.Size = new System.Drawing.Size(112, 21);
			comboBoxConclusion.TabIndex = 0;
			comboBoxPackingCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxPackingCondition.FormattingEnabled = true;
			comboBoxPackingCondition.Items.AddRange(new object[2]
			{
				"Good",
				"Weak"
			});
			comboBoxPackingCondition.Location = new System.Drawing.Point(273, 37);
			comboBoxPackingCondition.MaxLength = 20;
			comboBoxPackingCondition.Name = "comboBoxPackingCondition";
			comboBoxPackingCondition.Size = new System.Drawing.Size(135, 21);
			comboBoxPackingCondition.TabIndex = 1;
			label29.AutoSize = true;
			label29.Location = new System.Drawing.Point(171, 40);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(96, 13);
			label29.TabIndex = 28;
			label29.Text = "Packing Condition:";
			label29.Click += new System.EventHandler(label29_Click);
			textBoxTotalWeightLess.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTotalWeightLess.Location = new System.Drawing.Point(273, 14);
			textBoxTotalWeightLess.MaxLength = 20;
			textBoxTotalWeightLess.Name = "textBoxTotalWeightLess";
			textBoxTotalWeightLess.ReadOnly = true;
			textBoxTotalWeightLess.Size = new System.Drawing.Size(85, 20);
			textBoxTotalWeightLess.TabIndex = 5;
			textBoxTotalWeightLess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelWeight.AutoSize = true;
			labelWeight.Location = new System.Drawing.Point(171, 17);
			labelWeight.Name = "labelWeight";
			labelWeight.Size = new System.Drawing.Size(80, 13);
			labelWeight.TabIndex = 36;
			labelWeight.Text = "Weight Less %:";
			labelIssue2Name.AutoSize = true;
			labelIssue2Name.Location = new System.Drawing.Point(7, 40);
			labelIssue2Name.Name = "labelIssue2Name";
			labelIssue2Name.Size = new System.Drawing.Size(44, 13);
			labelIssue2Name.TabIndex = 38;
			labelIssue2Name.Text = "Issue 2:";
			labelIssue1Name.AutoSize = true;
			labelIssue1Name.Location = new System.Drawing.Point(7, 17);
			labelIssue1Name.Name = "labelIssue1Name";
			labelIssue1Name.Size = new System.Drawing.Size(44, 13);
			labelIssue1Name.TabIndex = 37;
			labelIssue1Name.Text = "Issue 1:";
			textBoxTotalIssue4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTotalIssue4.Location = new System.Drawing.Point(93, 79);
			textBoxTotalIssue4.MaxLength = 20;
			textBoxTotalIssue4.Name = "textBoxTotalIssue4";
			textBoxTotalIssue4.ReadOnly = true;
			textBoxTotalIssue4.Size = new System.Drawing.Size(67, 20);
			textBoxTotalIssue4.TabIndex = 3;
			textBoxTotalIssue4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelIssue4Name.AutoSize = true;
			labelIssue4Name.Location = new System.Drawing.Point(7, 82);
			labelIssue4Name.Name = "labelIssue4Name";
			labelIssue4Name.Size = new System.Drawing.Size(44, 13);
			labelIssue4Name.TabIndex = 33;
			labelIssue4Name.Text = "Issue 4:";
			textBoxTotalIssue1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTotalIssue1.Location = new System.Drawing.Point(93, 14);
			textBoxTotalIssue1.MaxLength = 20;
			textBoxTotalIssue1.Name = "textBoxTotalIssue1";
			textBoxTotalIssue1.ReadOnly = true;
			textBoxTotalIssue1.Size = new System.Drawing.Size(67, 20);
			textBoxTotalIssue1.TabIndex = 0;
			textBoxTotalIssue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalIssue3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTotalIssue3.Location = new System.Drawing.Point(93, 58);
			textBoxTotalIssue3.MaxLength = 20;
			textBoxTotalIssue3.Name = "textBoxTotalIssue3";
			textBoxTotalIssue3.ReadOnly = true;
			textBoxTotalIssue3.Size = new System.Drawing.Size(67, 20);
			textBoxTotalIssue3.TabIndex = 2;
			textBoxTotalIssue3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalIssue2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTotalIssue2.Location = new System.Drawing.Point(93, 36);
			textBoxTotalIssue2.MaxLength = 20;
			textBoxTotalIssue2.Name = "textBoxTotalIssue2";
			textBoxTotalIssue2.ReadOnly = true;
			textBoxTotalIssue2.Size = new System.Drawing.Size(67, 20);
			textBoxTotalIssue2.TabIndex = 1;
			textBoxTotalIssue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelIssue3Name.AutoSize = true;
			labelIssue3Name.Location = new System.Drawing.Point(7, 61);
			labelIssue3Name.Name = "labelIssue3Name";
			labelIssue3Name.Size = new System.Drawing.Size(44, 13);
			labelIssue3Name.TabIndex = 30;
			labelIssue3Name.Text = "Issue 3:";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "camera-icon.png");
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
			comboBoxTemplate.Assigned = false;
			comboBoxTemplate.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTemplate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTemplate.CustomReportFieldName = "";
			comboBoxTemplate.CustomReportKey = "";
			comboBoxTemplate.CustomReportValueType = 1;
			comboBoxTemplate.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTemplate.DisplayLayout.Appearance = appearance55;
			comboBoxTemplate.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTemplate.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTemplate.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTemplate.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxTemplate.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTemplate.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxTemplate.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTemplate.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTemplate.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTemplate.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxTemplate.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTemplate.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTemplate.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTemplate.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxTemplate.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTemplate.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTemplate.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxTemplate.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxTemplate.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTemplate.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxTemplate.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxTemplate.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTemplate.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
			comboBoxTemplate.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTemplate.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTemplate.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTemplate.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTemplate.Editable = true;
			comboBoxTemplate.FilterString = "";
			comboBoxTemplate.HasAllAccount = false;
			comboBoxTemplate.HasCustom = false;
			comboBoxTemplate.IsDataLoaded = false;
			comboBoxTemplate.Location = new System.Drawing.Point(99, 226);
			comboBoxTemplate.MaxDropDownItems = 12;
			comboBoxTemplate.Name = "comboBoxTemplate";
			comboBoxTemplate.ShowInactiveItems = false;
			comboBoxTemplate.ShowQuickAdd = true;
			comboBoxTemplate.Size = new System.Drawing.Size(124, 20);
			comboBoxTemplate.TabIndex = 320;
			comboBoxTemplate.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance67;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance68;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance69;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance70.BackColor2 = System.Drawing.SystemColors.Control;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance70;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance71;
			appearance72.BackColor = System.Drawing.SystemColors.Highlight;
			appearance72.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance72;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance73;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			appearance74.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance74;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance75.BackColor = System.Drawing.SystemColors.Control;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance75;
			appearance76.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance76;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance77;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance78;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(9, 249);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(1020, 258);
			dataGridItems.TabIndex = 1;
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
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBrand.DisplayLayout.Appearance = appearance79;
			comboBoxBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBrand.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBrand.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBrand.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBrand.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBrand.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxBrand.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxBrand.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
			comboBoxBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBrand.Editable = true;
			comboBoxBrand.FilterString = "";
			comboBoxBrand.HasAllAccount = false;
			comboBoxBrand.HasCustom = false;
			comboBoxBrand.IsDataLoaded = false;
			comboBoxBrand.Location = new System.Drawing.Point(688, 252);
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
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVariety.DisplayLayout.Appearance = appearance91;
			comboBoxVariety.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVariety.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVariety.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVariety.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			comboBoxVariety.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVariety.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			comboBoxVariety.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVariety.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVariety.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVariety.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			comboBoxVariety.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVariety.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVariety.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVariety.DisplayLayout.Override.CellAppearance = appearance98;
			comboBoxVariety.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVariety.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVariety.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			comboBoxVariety.DisplayLayout.Override.HeaderAppearance = appearance100;
			comboBoxVariety.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVariety.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			comboBoxVariety.DisplayLayout.Override.RowAppearance = appearance101;
			comboBoxVariety.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVariety.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
			comboBoxVariety.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVariety.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVariety.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVariety.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVariety.Editable = true;
			comboBoxVariety.FilterString = "";
			comboBoxVariety.HasAllAccount = false;
			comboBoxVariety.HasCustom = false;
			comboBoxVariety.IsDataLoaded = false;
			comboBoxVariety.Location = new System.Drawing.Point(553, 252);
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
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance103;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance104.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance104.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance104;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance105;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance106.BackColor2 = System.Drawing.SystemColors.Control;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance106;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance107;
			appearance108.BackColor = System.Drawing.SystemColors.Highlight;
			appearance108.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance108;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance109;
			appearance110.BorderColor = System.Drawing.Color.Silver;
			appearance110.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance110;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance111.BackColor = System.Drawing.SystemColors.Control;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance111;
			appearance112.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance112;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance113;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance114;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(426, 249);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.Size = new System.Drawing.Size(100, 20);
			comboBoxCategory.TabIndex = 321;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.Visible = false;
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(1041, 672);
			base.Controls.Add(panel1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(labelVoided);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(comboBoxTemplate);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(label20);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(comboBoxBrand);
			base.Controls.Add(comboBoxVariety);
			base.Controls.Add(comboBoxCategory);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "ArrivalReportForm";
			Text = "Arrival Report";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)genericListComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOrigin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTemplate).EndInit();
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
