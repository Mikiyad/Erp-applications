using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class FreightChargesForm : Form, IForm
	{
		private bool allowEdit = true;

		private FreightChargeData currentData;

		private const string TABLENAME_CONST = "Freight_Charge";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private bool isDataLoading;

		private string seekID;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool totalChanged;

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

		private NumberTextBox textBoxSubtotal;

		private PercentTextBox textBoxDiscountPercent;

		private Panel panel1;

		private Label label5;

		private NumberTextBox textBoxDiscountAmount;

		private Label label6;

		private Label label8;

		private Label label7;

		private NumberTextBox textBoxTotal;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private Label label11;

		private Label label12;

		private PercentTextBox textBoxTaxPercent;

		private NumberTextBox textBoxTaxAmount;

		private Panel panelNonTax;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem existingPriceListToolStripMenuItem;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private PortComboBox comboBoxGridFromPort;

		private PortComboBox comboBoxGridToPort;

		private ContainerSizeComboBox comboBoxGridContainerSize;

		private GenericListComboBox comboBoxGridContainerType;

		private Label label4;

		private DateTimePicker dateTimePickerValidTo;

		private DateTimePicker dateTimePickerValidFrom;

		private Label label2;

		private GenericListComboBox comboBoxGridShippingCompany;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2012;

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
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = true);
					sysDocComboBox.Enabled = enabled;
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
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
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
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
			}
		}

		private string SeekID
		{
			get
			{
				return seekID;
			}
			set
			{
				seekID = value;
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

		public FreightChargesForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
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
			comboBoxGridShippingCompany.SelectedIndexChanged += comboBoxGridShippingCompany_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			textBoxDiscountAmount.Validating += textBoxDiscountAmount_Validating;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxDiscountAmount.Validated += textBoxDiscountAmount_Validated;
			textBoxDiscountPercent.Validated += textBoxDiscountPercent_Validated;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
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
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
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

		private void textBoxDiscountAmount_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			num = decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num3 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			if (!(num == 0m))
			{
				if (num > num2 + num3)
				{
					ErrorHelper.InformationMessage("Discount amount should be less or equal to the subtotal.");
					e.Cancel = true;
				}
				else if (num < 0m)
				{
					ErrorHelper.InformationMessage("Discount amount should be greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
			FormActivator.ImageViewerFormObj.Image = productPhotoViewer.Image;
			FormActivator.BringFormToFront(FormActivator.ImageViewerFormObj);
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			FormHelper formHelper = new FormHelper();
			string text = "";
			if (ultraGridColumn == null)
			{
				return;
			}
			if (ultraGridColumn.Key == "Shipping Company ID")
			{
				text = dataGridItems.ActiveRow.Cells["Shipping Company ID"].Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					formHelper.EditShippingCompany(text);
				}
			}
			else if (ultraGridColumn.Key == "From Port")
			{
				text = dataGridItems.ActiveRow.Cells["From Port"].Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					formHelper.EditPort(text);
				}
			}
			else if (ultraGridColumn.Key == "To Port")
			{
				text = dataGridItems.ActiveRow.Cells["To Port"].Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					formHelper.EditPort(text);
				}
			}
			else if (ultraGridColumn.Key == "Container Size")
			{
				text = dataGridItems.ActiveRow.Cells["Container Size"].Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					formHelper.EditContainerSize(text);
				}
			}
			else if (ultraGridColumn.Key == "Type")
			{
				text = dataGridItems.ActiveRow.Cells["Type"].Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					formHelper.EditGenericList(GenericListTypes.ContainerType, text);
				}
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			dataGridItems.ActiveRow.Cells["Shipping Company Name"].Value = comboBoxGridShippingCompany.SelectedName;
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridFromPort.IsLoadingData = true;
			comboBoxGridToPort.IsLoadingData = true;
		}

		private void comboBoxGridShippingCompany_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new FreightChargeData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.FreightChargeTable.Rows[0] : currentData.FreightChargeTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["ValidDateFrom"] = new DateTime(dateTimePickerValidFrom.Value.Year, dateTimePickerValidFrom.Value.Month, dateTimePickerValidFrom.Value.Day, 0, 0, 0, 0);
				dataRow["ValidDateTo"] = new DateTime(dateTimePickerValidTo.Value.Year, dateTimePickerValidTo.Value.Month, dateTimePickerValidTo.Value.Day, 23, 59, 59, 999);
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Discount"] = textBoxDiscountAmount.Text;
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				dataRow["Total"] = textBoxTotal.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.FreightChargeTable.Rows.Add(dataRow);
				}
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.FreightChargeDetailTable.Columns.Contains(column.Key))
					{
						currentData.FreightChargeDetailTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.FreightChargeDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.FreightChargeDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SourcePortID"] = row.Cells["From Port"].Value.ToString();
					dataRow2["DestinationPortID"] = row.Cells["To Port"].Value.ToString();
					if (!string.IsNullOrEmpty(row.Cells["Shipping Company ID"].Value.ToString()))
					{
						dataRow2["ShippingCompanyID"] = row.Cells["Shipping Company ID"].Value.ToString();
					}
					dataRow2["ContainerSizeID"] = row.Cells["Container Size"].Value.ToString();
					dataRow2["TypeID"] = row.Cells["Type"].Value.ToString();
					if (row.Cells["Free Days"].Value.ToString() != "")
					{
						dataRow2["FreeDays"] = row.Cells["Free Days"].Value.ToString();
					}
					else
					{
						dataRow2["FreeDays"] = 0;
					}
					if (row.Cells["Transit Days"].Value.ToString() != "")
					{
						dataRow2["TransitDays"] = row.Cells["Transit Days"].Value.ToString();
					}
					else
					{
						dataRow2["TransitDays"] = 0;
					}
					if (row.Cells["Amount"].Value.ToString() != "")
					{
						dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					}
					else
					{
						dataRow2["Amount"] = 0;
					}
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.FreightChargeDetailTable.Rows.Add(dataRow2);
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
				dataTable.Columns.Add("From Port");
				dataTable.Columns.Add("To Port");
				dataTable.Columns.Add("Shipping Company ID");
				dataTable.Columns.Add("Shipping Company Name");
				dataTable.Columns.Add("Container Size");
				dataTable.Columns.Add("Type");
				dataTable.Columns.Add("Free Days", typeof(decimal));
				dataTable.Columns.Add("Transit Days", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("Remarks");
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company Name"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(30 * dataGridItems.Width) / 100;
				int num3 = dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].Width = (dataGridItems.DisplayLayout.Bands[0].Columns["Free Days"].Width = checked(20 * dataGridItems.Width) / 100);
				num3 = (dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].Width = (dataGridItems.DisplayLayout.Bands[0].Columns["Transit Days"].Width = checked(20 * dataGridItems.Width) / 100));
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company Name"].Header.Appearance.BackColorDisabled = Color.Transparent;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].ValueList = comboBoxGridFromPort;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company Name"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company Name"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company Name"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].ValueList = comboBoxGridContainerSize;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].ValueList = comboBoxGridToPort;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].ValueList = comboBoxGridShippingCompany;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Type"].ValueList = comboBoxGridContainerType;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Free Days"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Free Days"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Free Days"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Free Days"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Free Days"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Transit Days"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Transit Days"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Transit Days"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Transit Days"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Transit Days"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["From Port"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["To Port"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["Container Size"].Header.Caption = "Cont.Size";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company ID"].Header.Caption = "Shp.Company Code";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipping Company Name"].Header.Caption = "Name";
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
			if (SaveData())
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxVoucherNumber.Enabled = true;
			}
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == ""))
				{
					currentData = Factory.FreightChargeSystem.GetFreightChargeByID(SystemDocID, voucherID);
					if (currentData == null)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
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
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Freight_Charge"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					if (dataRow["TransactionDate"] != DBNull.Value)
					{
						dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					}
					else
					{
						dateTimePickerDate.Value = DateTime.Now;
					}
					if (dataRow["ValidDateFrom"] != DBNull.Value)
					{
						dateTimePickerValidFrom.Value = DateTime.Parse(dataRow["ValidDateFrom"].ToString());
					}
					else
					{
						dateTimePickerValidFrom.Value = DateTime.Now;
					}
					if (dataRow["ValidDateTo"] != DBNull.Value)
					{
						dateTimePickerValidTo.Value = DateTime.Parse(dataRow["ValidDateTo"].ToString());
					}
					else
					{
						dateTimePickerValidTo.Value = DateTime.Now;
					}
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
					if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
					}
					textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					if (dataRow["Inactive"] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Freight_Charge_Detail") && currentData.FreightChargeDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Freight_Charge_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["From Port"] = row["SourcePortID"];
							dataRow3["To Port"] = row["DestinationPortID"];
							if (!string.IsNullOrEmpty(row["ShippingCompanyID"].ToString()))
							{
								dataRow3["Shipping Company ID"] = row["ShippingCompanyID"];
								comboBoxGridShippingCompany.SelectedID = row["ShippingCompanyID"].ToString();
								dataRow3["Shipping Company Name"] = comboBoxGridShippingCompany.SelectedName;
							}
							dataRow3["Remarks"] = row["Remarks"];
							dataRow3["Container Size"] = row["ContainerSizeID"];
							dataRow3["Type"] = row["TypeID"];
							dataRow3["Free Days"] = row["FreeDays"];
							dataRow3["Transit Days"] = row["TransitDays"];
							if (row["Amount"] != DBNull.Value)
							{
								dataRow3["Amount"] = decimal.Parse(row["Amount"].ToString());
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
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
				bool flag = Factory.FreightChargeSystem.CreateFreightCharge(currentData, !isNewRecord);
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
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			decimal result = -1m;
			if (!decimal.TryParse(textBoxTotal.Text, out result) || result < 0m)
			{
				ErrorHelper.InformationMessage("Cannot save an invoice with negative total.");
				return false;
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one row of item.");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Freight_Charge", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
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
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxTaxPercent.Text = "0";
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountPercent.Text = "0";
				isDiscountPercent = false;
				isDiscountPercent = false;
				DateTimePicker dateTimePicker = dateTimePickerDate;
				DateTimePicker dateTimePicker2 = dateTimePickerValidFrom;
				DateTime dateTime = dateTimePickerValidTo.Value = DateTime.Now;
				DateTime dateTime4 = dateTimePicker.Value = (dateTimePicker2.Value = dateTime);
				checkBoxInactive.Checked = false;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				IsNewRecord = true;
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.FreightChargeSystem.DeleteFreightCharge(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Freight_Charge", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Freight_Charge", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Freight_Charge", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Freight_Charge", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Freight_Charge", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
			checked
			{
				try
				{
					SetupGrid();
					if (!CompanyPreferences.IsTax)
					{
						panelNonTax.Top -= 22;
					}
					comboBoxSysDoc.FilterByType(SysDocTypes.FreightCharges);
					comboBoxSysDoc.SelectedIndex++;
					SetSecurity();
					if (!base.IsDisposed)
					{
						dateTimePickerDate.Value = DateTime.Now;
						dateTimePickerValidFrom.Value = DateTime.Now;
						dateTimePickerValidTo.Value = DateTime.Now;
						IsNewRecord = true;
						if (isNewRecord)
						{
							textBoxVoucherNumber.Text = GetNextVoucherNumber();
						}
						formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
						formManager.ResetDirty();
					}
				}
				catch (Exception e2)
				{
					dataGridItems.LoadLayoutFailed = true;
					ErrorHelper.ProcessError(e2);
				}
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
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				return Factory.FreightChargeSystem.VoidFreightCharge(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.FreightCharges);
		}

		private void LoadCustomerBillingAddress()
		{
		}

		private void CalculateTotal()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal result4 = default(decimal);
			decimal num2 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result5 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result5);
					num += result5;
				}
			}
			textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
			decimal.TryParse(textBoxDiscountPercent.Text, out result2);
			decimal.TryParse(textBoxDiscountAmount.Text, out result);
			decimal.TryParse(textBoxTaxPercent.Text, out result4);
			decimal.TryParse(textBoxTaxAmount.Text, out result3);
			if (isTaxPercent && result4 != 0m)
			{
				result3 = Math.Round(num * result4 / 100m, Global.CurDecimalPoints);
				textBoxTaxAmount.Text = result3.ToString(Format.TotalAmountFormat);
			}
			else if (num > 0m)
			{
				result4 = Math.Round(result3 / num * 100m, Global.CurDecimalPoints);
				textBoxTaxPercent.Text = result4.ToString();
			}
			else
			{
				textBoxTaxPercent.Text = "0";
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num2 = num + result3;
			if (isDiscountPercent && result2 != 0m)
			{
				result = Math.Round(num2 * result2 / 100m, Global.CurDecimalPoints);
				textBoxDiscountAmount.Text = result.ToString(Format.TotalAmountFormat);
			}
			else if (num > 0m)
			{
				result2 = Math.Round(result / num2 * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = result2.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num2 = num + result3 - result;
			textBoxTotal.Text = num2.ToString(Format.TotalAmountFormat);
		}

		private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Focused)
			{
				isDiscountPercent = true;
			}
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Focused)
			{
				isDiscountPercent = false;
			}
		}

		private void textBoxTotal_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTotal.Focused)
			{
				totalChanged = true;
			}
		}

		private void textBoxTotal_Validating(object sender, CancelEventArgs e)
		{
			if (!totalChanged)
			{
				return;
			}
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			num4 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num2 = num + num5 - num4;
			if (num2 < 0m)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Total amount cannot be greater than the subtotal.", "Please enter a numeric value less or equal to the subtotal.");
				e.Cancel = true;
				return;
			}
			_ = (num4 < 0m);
			textBoxDiscountAmount.Text = num2.ToString(Format.TotalAmountFormat);
			if (num > 0m)
			{
				num3 = Math.Round(num2 / (num + num5) * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = num3.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
			}
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

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
		}

		private void numberTextBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTaxAmount.Focused)
			{
				isTaxPercent = false;
				CalculateTotal();
			}
		}

		private void textBoxTaxPercent_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTaxPercent.Focused)
			{
				isTaxPercent = true;
				CalculateTotal();
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
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet freightChargeToPrint = Factory.FreightChargeSystem.GetFreightChargeToPrint(selectedID, text);
					if (freightChargeToPrint == null || freightChargeToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(freightChargeToPrint, selectedID, "Freight Charge", SysDocTypes.FreightCharges, isPrint, showPrintDialog);
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
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FreightChargesListFormObj);
		}

		private void existingPriceListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet freightChargeAll = Factory.FreightChargeSystem.GetFreightChargeAll();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = freightChargeAll;
			selectDocumentDialog.Text = "Select Freight Charge";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string voucherID = selectDocumentDialog.SelectedRow.Cells["Doc Number"].Value.ToString();
				FreightChargeData freightChargeByID = Factory.FreightChargeSystem.GetFreightChargeByID(sysDocID, voucherID);
				_ = freightChargeByID.FreightChargeDetailTable.Rows[0];
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (freightChargeByID.Tables.Contains("Freight_Charge_Detail") && freightChargeByID.FreightChargeDetailTable.Rows.Count != 0)
				{
					foreach (DataRow row in freightChargeByID.Tables["Freight_Charge_Detail"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["From Port"] = row["SourcePortID"];
						dataRow2["To Port"] = row["DestinationPortID"];
						if (!string.IsNullOrEmpty(row["ShippingCompanyID"].ToString()))
						{
							dataRow2["Shipping Company ID"] = row["ShippingCompanyID"];
							comboBoxGridShippingCompany.SelectedID = dataRow2["Shipping Company ID"].ToString();
							dataRow2["Shipping Company Name"] = comboBoxGridShippingCompany.SelectedName;
						}
						dataRow2["Container Size"] = row["ContainerSizeID"];
						dataRow2["Type"] = row["TypeID"];
						dataRow2["Amount"] = row["Amount"];
						dataRow2["Remarks"] = row["Remarks"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					AdjustGridColumnSettings();
					dataGridItems.DropDownMenu.Enabled = false;
				}
			}
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			dataGridItems.ImportFromExcel(autoFill: true);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind.PerformClick();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.FreightChargesForm));
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
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			existingPriceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			dateTimePickerValidTo = new System.Windows.Forms.DateTimePicker();
			dateTimePickerValidFrom = new System.Windows.Forms.DateTimePicker();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			panelNonTax = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
			textBoxDiscountAmount = new Micromind.UISupport.NumberTextBox();
			textBoxTotal = new Micromind.UISupport.NumberTextBox();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			textBoxTaxPercent = new Micromind.UISupport.PercentTextBox();
			textBoxTaxAmount = new Micromind.UISupport.NumberTextBox();
			label5 = new System.Windows.Forms.Label();
			textBoxSubtotal = new Micromind.UISupport.NumberTextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			comboBoxGridShippingCompany = new Micromind.DataControls.GenericListComboBox();
			comboBoxGridContainerSize = new Micromind.DataControls.ContainerSizeComboBox();
			comboBoxGridToPort = new Micromind.DataControls.PortComboBox();
			comboBoxGridFromPort = new Micromind.DataControls.PortComboBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridContainerType = new Micromind.DataControls.GenericListComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panel1.SuspendLayout();
			panelNonTax.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridShippingCompany).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridContainerSize).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridToPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridFromPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridContainerType).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
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
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButton1,
				toolStripDropDownButton1,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(894, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "Import From Excel";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				existingPriceListToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			existingPriceListToolStripMenuItem.Name = "existingPriceListToolStripMenuItem";
			existingPriceListToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			existingPriceListToolStripMenuItem.Text = "Existing Freight Charge";
			existingPriceListToolStripMenuItem.Click += new System.EventHandler(existingPriceListToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 568);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(894, 40);
			panelButtons.TabIndex = 12;
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
			linePanelDown.Size = new System.Drawing.Size(894, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(784, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(527, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 3;
			textBoxVoucherNumber.Location = new System.Drawing.Point(125, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(146, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(458, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(527, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(178, 20);
			textBoxRef1.TabIndex = 6;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(48, 475);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(381, 79);
			textBoxNote.TabIndex = 10;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 474);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 9;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(13, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 1;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(dateTimePickerValidTo);
			panelDetails.Controls.Add(dateTimePickerValidFrom);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(checkBoxInactive);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(739, 53);
			panelDetails.TabIndex = 0;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(234, 29);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(23, 13);
			label4.TabIndex = 148;
			label4.Text = "To:";
			dateTimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerValidTo.Location = new System.Drawing.Point(259, 26);
			dateTimePickerValidTo.Name = "dateTimePickerValidTo";
			dateTimePickerValidTo.Size = new System.Drawing.Size(109, 20);
			dateTimePickerValidTo.TabIndex = 5;
			dateTimePickerValidTo.Value = new System.DateTime(1950, 12, 31, 10, 0, 0, 0);
			dateTimePickerValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerValidFrom.Location = new System.Drawing.Point(125, 26);
			dateTimePickerValidFrom.Name = "dateTimePickerValidFrom";
			dateTimePickerValidFrom.Size = new System.Drawing.Size(109, 20);
			dateTimePickerValidFrom.TabIndex = 4;
			dateTimePickerValidFrom.Value = new System.DateTime(1950, 12, 31, 10, 0, 0, 0);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(13, 28);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(59, 13);
			label2.TabIndex = 147;
			label2.Text = "Valid From:";
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance3;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(341, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 144;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			ultraFormattedLinkLabel5.Visible = false;
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
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
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(392, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(60, 20);
			comboBoxSysDoc.TabIndex = 143;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.Visible = false;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(285, 5);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(458, 3);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 419);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(866, 49);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label12);
			panel1.Controls.Add(textBoxTaxPercent);
			panel1.Controls.Add(textBoxTaxAmount);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(662, 469);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(220, 87);
			panel1.TabIndex = 10;
			panel1.Visible = false;
			panelNonTax.Controls.Add(label6);
			panelNonTax.Controls.Add(label8);
			panelNonTax.Controls.Add(label7);
			panelNonTax.Controls.Add(textBoxDiscountPercent);
			panelNonTax.Controls.Add(textBoxDiscountAmount);
			panelNonTax.Controls.Add(textBoxTotal);
			panelNonTax.Location = new System.Drawing.Point(0, 43);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(219, 43);
			panelNonTax.TabIndex = 139;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(115, 3);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(15, 13);
			label6.TabIndex = 133;
			label6.Text = "%";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(4, 26);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(40, 13);
			label8.TabIndex = 133;
			label8.Text = "Total:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 4);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(52, 13);
			label7.TabIndex = 133;
			label7.Text = "Discount:";
			textBoxDiscountPercent.CustomReportFieldName = "";
			textBoxDiscountPercent.CustomReportKey = "";
			textBoxDiscountPercent.CustomReportValueType = 1;
			textBoxDiscountPercent.IsComboTextBox = false;
			textBoxDiscountPercent.IsModified = false;
			textBoxDiscountPercent.Location = new System.Drawing.Point(80, 0);
			textBoxDiscountPercent.MaxLength = 4;
			textBoxDiscountPercent.Name = "textBoxDiscountPercent";
			textBoxDiscountPercent.Size = new System.Drawing.Size(34, 20);
			textBoxDiscountPercent.TabIndex = 1;
			textBoxDiscountPercent.Text = "0";
			textBoxDiscountPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDiscountPercent.TextChanged += new System.EventHandler(textBoxDiscountPercent_TextChanged);
			textBoxDiscountAmount.AllowDecimal = true;
			textBoxDiscountAmount.CustomReportFieldName = "";
			textBoxDiscountAmount.CustomReportKey = "";
			textBoxDiscountAmount.CustomReportValueType = 1;
			textBoxDiscountAmount.IsComboTextBox = false;
			textBoxDiscountAmount.IsModified = false;
			textBoxDiscountAmount.Location = new System.Drawing.Point(131, 0);
			textBoxDiscountAmount.MaxLength = 15;
			textBoxDiscountAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDiscountAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDiscountAmount.Name = "textBoxDiscountAmount";
			textBoxDiscountAmount.NullText = "0";
			textBoxDiscountAmount.Size = new System.Drawing.Size(87, 20);
			textBoxDiscountAmount.TabIndex = 2;
			textBoxDiscountAmount.Text = "0.00";
			textBoxDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDiscountAmount.TextChanged += new System.EventHandler(textBoxDiscountAmount_TextChanged);
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(80, 22);
			textBoxTotal.MaxLength = 15;
			textBoxTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.NullText = "0";
			textBoxTotal.Size = new System.Drawing.Size(138, 20);
			textBoxTotal.TabIndex = 3;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.TextChanged += new System.EventHandler(textBoxTotal_TextChanged);
			textBoxTotal.Validating += new System.ComponentModel.CancelEventHandler(textBoxTotal_Validating);
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(115, 24);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(15, 13);
			label11.TabIndex = 138;
			label11.Text = "%";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(4, 25);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(28, 13);
			label12.TabIndex = 137;
			label12.Text = "Tax:";
			textBoxTaxPercent.CustomReportFieldName = "";
			textBoxTaxPercent.CustomReportKey = "";
			textBoxTaxPercent.CustomReportValueType = 1;
			textBoxTaxPercent.IsComboTextBox = false;
			textBoxTaxPercent.IsModified = false;
			textBoxTaxPercent.Location = new System.Drawing.Point(80, 21);
			textBoxTaxPercent.MaxLength = 4;
			textBoxTaxPercent.Name = "textBoxTaxPercent";
			textBoxTaxPercent.Size = new System.Drawing.Size(34, 20);
			textBoxTaxPercent.TabIndex = 134;
			textBoxTaxPercent.Text = "0";
			textBoxTaxPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxPercent.TextChanged += new System.EventHandler(textBoxTaxPercent_TextChanged);
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(131, 21);
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
			textBoxTaxAmount.Size = new System.Drawing.Size(87, 20);
			textBoxTaxAmount.TabIndex = 135;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.TextChanged += new System.EventHandler(numberTextBox1_TextChanged);
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(4, 3);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(49, 13);
			label5.TabIndex = 2;
			label5.Text = "Subtotal:";
			textBoxSubtotal.AllowDecimal = true;
			textBoxSubtotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSubtotal.CustomReportFieldName = "";
			textBoxSubtotal.CustomReportKey = "";
			textBoxSubtotal.CustomReportValueType = 1;
			textBoxSubtotal.ForeColor = System.Drawing.Color.Black;
			textBoxSubtotal.IsComboTextBox = false;
			textBoxSubtotal.IsModified = false;
			textBoxSubtotal.Location = new System.Drawing.Point(80, -1);
			textBoxSubtotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSubtotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSubtotal.Name = "textBoxSubtotal";
			textBoxSubtotal.NullText = "0";
			textBoxSubtotal.ReadOnly = true;
			textBoxSubtotal.Size = new System.Drawing.Size(138, 20);
			textBoxSubtotal.TabIndex = 0;
			textBoxSubtotal.TabStop = false;
			textBoxSubtotal.Text = "0.00";
			textBoxSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			comboBoxGridShippingCompany.Assigned = false;
			comboBoxGridShippingCompany.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridShippingCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridShippingCompany.CustomReportFieldName = "";
			comboBoxGridShippingCompany.CustomReportKey = "";
			comboBoxGridShippingCompany.CustomReportValueType = 1;
			comboBoxGridShippingCompany.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridShippingCompany.DisplayLayout.Appearance = appearance17;
			comboBoxGridShippingCompany.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridShippingCompany.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridShippingCompany.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridShippingCompany.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxGridShippingCompany.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridShippingCompany.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxGridShippingCompany.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridShippingCompany.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridShippingCompany.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridShippingCompany.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxGridShippingCompany.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridShippingCompany.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridShippingCompany.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridShippingCompany.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxGridShippingCompany.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridShippingCompany.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridShippingCompany.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxGridShippingCompany.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxGridShippingCompany.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridShippingCompany.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridShippingCompany.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxGridShippingCompany.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridShippingCompany.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxGridShippingCompany.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridShippingCompany.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridShippingCompany.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridShippingCompany.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridShippingCompany.Editable = true;
			comboBoxGridShippingCompany.FilterString = "";
			comboBoxGridShippingCompany.GenericListType = Micromind.Common.Data.GenericListTypes.ShippingCompany;
			comboBoxGridShippingCompany.HasAllAccount = false;
			comboBoxGridShippingCompany.HasCustom = false;
			comboBoxGridShippingCompany.IsDataLoaded = false;
			comboBoxGridShippingCompany.IsSingleColumn = false;
			comboBoxGridShippingCompany.Location = new System.Drawing.Point(552, 200);
			comboBoxGridShippingCompany.MaxDropDownItems = 12;
			comboBoxGridShippingCompany.Name = "comboBoxGridShippingCompany";
			comboBoxGridShippingCompany.ShowInactiveItems = false;
			comboBoxGridShippingCompany.ShowQuickAdd = true;
			comboBoxGridShippingCompany.Size = new System.Drawing.Size(140, 20);
			comboBoxGridShippingCompany.TabIndex = 118;
			comboBoxGridShippingCompany.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridShippingCompany.Visible = false;
			comboBoxGridContainerSize.Assigned = false;
			comboBoxGridContainerSize.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridContainerSize.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridContainerSize.CustomReportFieldName = "";
			comboBoxGridContainerSize.CustomReportKey = "";
			comboBoxGridContainerSize.CustomReportValueType = 1;
			comboBoxGridContainerSize.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridContainerSize.DisplayLayout.Appearance = appearance29;
			comboBoxGridContainerSize.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridContainerSize.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridContainerSize.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridContainerSize.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxGridContainerSize.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridContainerSize.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxGridContainerSize.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridContainerSize.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridContainerSize.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridContainerSize.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxGridContainerSize.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridContainerSize.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridContainerSize.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridContainerSize.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxGridContainerSize.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridContainerSize.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridContainerSize.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxGridContainerSize.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxGridContainerSize.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridContainerSize.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridContainerSize.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxGridContainerSize.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridContainerSize.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxGridContainerSize.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridContainerSize.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridContainerSize.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridContainerSize.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridContainerSize.Editable = true;
			comboBoxGridContainerSize.FilterString = "";
			comboBoxGridContainerSize.HasAllAccount = false;
			comboBoxGridContainerSize.HasCustom = false;
			comboBoxGridContainerSize.IsDataLoaded = false;
			comboBoxGridContainerSize.Location = new System.Drawing.Point(475, 251);
			comboBoxGridContainerSize.MaxDropDownItems = 12;
			comboBoxGridContainerSize.Name = "comboBoxGridContainerSize";
			comboBoxGridContainerSize.ShowInactiveItems = false;
			comboBoxGridContainerSize.ShowQuickAdd = true;
			comboBoxGridContainerSize.Size = new System.Drawing.Size(100, 20);
			comboBoxGridContainerSize.TabIndex = 7;
			comboBoxGridContainerSize.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridContainerSize.Visible = false;
			comboBoxGridToPort.Assigned = false;
			comboBoxGridToPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridToPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridToPort.CustomReportFieldName = "";
			comboBoxGridToPort.CustomReportKey = "";
			comboBoxGridToPort.CustomReportValueType = 1;
			comboBoxGridToPort.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridToPort.DisplayLayout.Appearance = appearance41;
			comboBoxGridToPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridToPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridToPort.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridToPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxGridToPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridToPort.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxGridToPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridToPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridToPort.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridToPort.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxGridToPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridToPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridToPort.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridToPort.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxGridToPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridToPort.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridToPort.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxGridToPort.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxGridToPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridToPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridToPort.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxGridToPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridToPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxGridToPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridToPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridToPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridToPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridToPort.Editable = true;
			comboBoxGridToPort.FilterString = "";
			comboBoxGridToPort.HasAllAccount = false;
			comboBoxGridToPort.HasCustom = false;
			comboBoxGridToPort.IsDataLoaded = false;
			comboBoxGridToPort.Location = new System.Drawing.Point(418, 200);
			comboBoxGridToPort.MaxDropDownItems = 12;
			comboBoxGridToPort.Name = "comboBoxGridToPort";
			comboBoxGridToPort.ShowInactiveItems = false;
			comboBoxGridToPort.ShowQuickAdd = true;
			comboBoxGridToPort.Size = new System.Drawing.Size(100, 20);
			comboBoxGridToPort.TabIndex = 4;
			comboBoxGridToPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridToPort.Visible = false;
			comboBoxGridFromPort.Assigned = false;
			comboBoxGridFromPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridFromPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridFromPort.CustomReportFieldName = "";
			comboBoxGridFromPort.CustomReportKey = "";
			comboBoxGridFromPort.CustomReportValueType = 1;
			comboBoxGridFromPort.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridFromPort.DisplayLayout.Appearance = appearance53;
			comboBoxGridFromPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridFromPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridFromPort.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridFromPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxGridFromPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridFromPort.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxGridFromPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridFromPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridFromPort.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridFromPort.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxGridFromPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridFromPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridFromPort.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridFromPort.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxGridFromPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridFromPort.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridFromPort.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxGridFromPort.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxGridFromPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridFromPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridFromPort.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxGridFromPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridFromPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxGridFromPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridFromPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridFromPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridFromPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridFromPort.Editable = true;
			comboBoxGridFromPort.FilterString = "";
			comboBoxGridFromPort.HasAllAccount = false;
			comboBoxGridFromPort.HasCustom = false;
			comboBoxGridFromPort.IsDataLoaded = false;
			comboBoxGridFromPort.Location = new System.Drawing.Point(303, 200);
			comboBoxGridFromPort.MaxDropDownItems = 12;
			comboBoxGridFromPort.Name = "comboBoxGridFromPort";
			comboBoxGridFromPort.ShowInactiveItems = false;
			comboBoxGridFromPort.ShowQuickAdd = true;
			comboBoxGridFromPort.Size = new System.Drawing.Size(100, 20);
			comboBoxGridFromPort.TabIndex = 3;
			comboBoxGridFromPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridFromPort.Visible = false;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(48, 251);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 8;
			productPhotoViewer.Visible = false;
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
			comboBoxGridContainerType.Assigned = false;
			comboBoxGridContainerType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridContainerType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridContainerType.CustomReportFieldName = "";
			comboBoxGridContainerType.CustomReportKey = "";
			comboBoxGridContainerType.CustomReportValueType = 1;
			comboBoxGridContainerType.DescriptionTextBox = null;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridContainerType.DisplayLayout.Appearance = appearance65;
			comboBoxGridContainerType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridContainerType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridContainerType.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridContainerType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			comboBoxGridContainerType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridContainerType.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			comboBoxGridContainerType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridContainerType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridContainerType.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridContainerType.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			comboBoxGridContainerType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridContainerType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridContainerType.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridContainerType.DisplayLayout.Override.CellAppearance = appearance72;
			comboBoxGridContainerType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridContainerType.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridContainerType.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			comboBoxGridContainerType.DisplayLayout.Override.HeaderAppearance = appearance74;
			comboBoxGridContainerType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridContainerType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridContainerType.DisplayLayout.Override.RowAppearance = appearance75;
			comboBoxGridContainerType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridContainerType.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			comboBoxGridContainerType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridContainerType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridContainerType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridContainerType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridContainerType.Editable = true;
			comboBoxGridContainerType.FilterString = "";
			comboBoxGridContainerType.GenericListType = Micromind.Common.Data.GenericListTypes.ContainerType;
			comboBoxGridContainerType.HasAllAccount = false;
			comboBoxGridContainerType.HasCustom = false;
			comboBoxGridContainerType.IsDataLoaded = false;
			comboBoxGridContainerType.IsSingleColumn = false;
			comboBoxGridContainerType.Location = new System.Drawing.Point(318, 251);
			comboBoxGridContainerType.MaxDropDownItems = 12;
			comboBoxGridContainerType.Name = "comboBoxGridContainerType";
			comboBoxGridContainerType.ShowInactiveItems = false;
			comboBoxGridContainerType.ShowQuickAdd = true;
			comboBoxGridContainerType.Size = new System.Drawing.Size(100, 20);
			comboBoxGridContainerType.TabIndex = 6;
			comboBoxGridContainerType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridContainerType.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance77;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance78.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance78.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance78;
			appearance79.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance79;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance80.BackColor2 = System.Drawing.SystemColors.Control;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance80.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance80;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance81;
			appearance82.BackColor = System.Drawing.SystemColors.Highlight;
			appearance82.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance82;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance83;
			appearance84.BorderColor = System.Drawing.Color.Silver;
			appearance84.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance84;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance85.BackColor = System.Drawing.SystemColors.Control;
			appearance85.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance85.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance85;
			appearance86.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance86;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance87;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance88;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 90);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(867, 379);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(894, 608);
			base.Controls.Add(comboBoxGridShippingCompany);
			base.Controls.Add(comboBoxGridContainerSize);
			base.Controls.Add(comboBoxGridToPort);
			base.Controls.Add(comboBoxGridFromPort);
			base.Controls.Add(panel1);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(panelDetails);
			base.Controls.Add(labelVoided);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxGridContainerType);
			base.Controls.Add(dataGridItems);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "FreightChargesForm";
			Text = "Freight Charges";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelNonTax.ResumeLayout(false);
			panelNonTax.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridShippingCompany).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridContainerSize).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridToPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridFromPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridContainerType).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
