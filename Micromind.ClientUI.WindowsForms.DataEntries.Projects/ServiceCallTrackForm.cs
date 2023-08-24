using DevExpress.XtraRichEdit;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.ClientUI.WindowsForms.DataEntries.Vendors;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class ServiceCallTrackForm : Form, IForm
	{
		private bool supressInventoryMessage;

		private ServiceCallTrackData currentData;

		private const string TABLENAME_CONST = "Service_CallTrack";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

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

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem saveADraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private SysDocComboBox comboBoxSysDoc;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem changeRequestStatusToolStripMenuItem;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl2;

		private DataEntryGrid dataGridItems;

		private UltraTabPageControl ultraTabPageControl1;

		private RichEditControl textBoxRepairDetails;

		private UltraTabPageControl ultraTabPageControl3;

		private DataEntryGrid datapartsGrid;

		private DateTimePicker dateTimePickerReqReceivedDate;

		private Label label4;

		private Label label2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private customersFlatComboBox comboBoxCustomer;

		private TextBox textBoxCustomerName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxBilltoAddress;

		private TextBox textBoxContactPerson;

		private Label label6;

		private DateTimePicker dateTimePickerServiceAssignedDate;

		private Label label3;

		private MMLabel mmLabel17;

		private MMTextBox textBoxContactNo;

		private Label label18;

		private Label label17;

		private Label label16;

		private Label label15;

		private Label label14;

		private AmountTextBox textBoxTotalCharges;

		private AmountTextBox textBoxTravelTotal;

		private AmountTextBox textBoxLabourTotal;

		private AmountTextBox textBoxPartsTotal;

		private Panel panel1;

		private Label label13;

		private NumberTextBox textBoxServiceHours;

		private Label label12;

		private AmountTextBox textBoxServiceTotal;

		private NumericUpDown numericUpDownLabourMins;

		private NumericUpDown numericUpDownLabourHours;

		private NumericUpDown numericUpDownTravelMins;

		private NumericUpDown numericUpDownTravelHours;

		private Label label11;

		private Label label10;

		private DateTimePicker dateTimePickerServiceFinishDate;

		private DateTimePicker dateTimePickerServiceStartDate;

		private DateTimePicker dateTimePickerServiceEndTime;

		private DateTimePicker dateTimePickerServiceStartTime;

		private Label label9;

		private Label label8;

		private RadioButton radioButtonChargeable;

		private RadioButton radioButtonWarranty;

		private RadioButton radioButtonAMC;

		private Label label7;

		private DateTimePicker dateTimePickerserviceAssignedTime;

		private Label label5;

		private DateTimePicker dateTimePickerReqReceivedTime;

		private ClientAssetComboBox ComboBoxclientAsset;

		private ProductComboBox ComboBoxproduct;

		private EmployeeComboBox comboBoxServiceEmployee;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private CampaignStatusComboBox comboBoxStatus;

		private MMLabel mmLabel6;

		private GroupBox groupBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private JobComboBox comboBoxJob;

		private TextBox textBoxJobName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMLabel mmLabel2;

		private MMTextBox textBoxLocation;

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
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				changeRequestStatusToolStripMenuItem.Enabled = !isNewRecord;
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

		public ServiceCallTrackForm()
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

		private void textBoxRepairDetails_ContentChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
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
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			textBoxRepairDetails.ContentChanged += textBoxRepairDetails_ContentChanged;
			comboBoxJob.SelectedIndexChanged += comboBoxJob_SelectedIndexChanged;
			datapartsGrid.AfterCellUpdate += datapartsGrid_AfterCellUpdate;
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
			LoadCustomerBillingAddress();
		}

		private void LoadCustomerBillingAddress()
		{
			try
			{
				DataSet customerDocumentAddress = Factory.CustomerSystem.GetCustomerDocumentAddress(comboBoxCustomer.SelectedID, "BillToAddressID");
				DataRow dataRow = null;
				if (customerDocumentAddress != null && customerDocumentAddress.Tables.Count > 0 && customerDocumentAddress.Tables[0].Rows.Count > 0)
				{
					dataRow = customerDocumentAddress.Tables[0].Rows[0];
				}
				if (dataRow != null)
				{
					textBoxBilltoAddress.Text = dataRow["AddressPrintFormat"].ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void comboBoxJob_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxJob.SelectedID != "")
			{
				object selectedCellValue = comboBoxJob.GetSelectedCellValue("CustomerID");
				if (selectedCellValue != null)
				{
					comboBoxCustomer.SelectedID = selectedCellValue.ToString();
				}
				ComboBoxclientAsset.FilterByProject(comboBoxJob.SelectedID);
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
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

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null && e.Cell.Column.Key == "EQUIPMENT")
				{
					dataGridItems.ActiveRow.Cells["SERIALNO"].Value = ComboBoxclientAsset.SerialNo;
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
			_ = dataGridItems.ActiveRow;
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
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			_ = (e.Cell.Column.Key == "Item Code");
			checked
			{
				if (activeRow != null && activeRow.DataChanged && activeRow.IsAddRow && e.Cell.Column.Key == "Quantity")
				{
					if (activeRow.Cells["ItemType"].Value.ToString() != "")
					{
						_ = (byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString());
					}
					if (CompanyPreferences.NegativeQuantityAction != 1 && !supressInventoryMessage && !IsNewRecord)
					{
						_ = comboBoxSysDoc.SelectedID;
						_ = textBoxVoucherNumber.Text;
					}
				}
			}
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
					currentData = new ServiceCallTrackData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ServiceCallTrackTable.Rows[0] : currentData.ServiceCallTrackTable.NewRow();
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
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["JobID"] = comboBoxJob.SelectedID;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["ShippingAddressID"] = "";
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["ContactName"] = textBoxContactPerson.Text;
				dataRow["ContactNo"] = textBoxContactNo.Text;
				dataRow["Location"] = textBoxLocation.Text;
				if (dateTimePickerReqReceivedDate.Checked)
				{
					dataRow["ReqReceivedDate"] = dateTimePickerReqReceivedDate.Value;
				}
				else
				{
					dataRow["ReqReceivedDate"] = DBNull.Value;
				}
				dataRow["ReqReceivedTime"] = dateTimePickerReqReceivedTime.Value.TimeOfDay;
				dataRow["ServiceEmployeeID"] = comboBoxServiceEmployee.SelectedID;
				if (dateTimePickerReqReceivedDate.Checked)
				{
					dataRow["ServiceAssignDate"] = dateTimePickerServiceAssignedDate.Value;
				}
				else
				{
					dataRow["ServiceAssignDate"] = DBNull.Value;
				}
				dataRow["ServiceAssignTime"] = dateTimePickerserviceAssignedTime.Value.TimeOfDay;
				string value2 = "";
				if (radioButtonAMC.Checked)
				{
					value2 = "A";
				}
				else if (radioButtonWarranty.Checked)
				{
					value2 = "W";
				}
				else if (radioButtonChargeable.Checked)
				{
					value2 = "C";
				}
				dataRow["ServiceUnder"] = value2;
				dataRow["ServiceStartDate"] = dateTimePickerServiceStartDate.Value;
				dataRow["ServiceStartTime"] = dateTimePickerServiceStartTime.Value.TimeOfDay;
				dataRow["ServiceFinishedDate"] = dateTimePickerServiceFinishDate.Value;
				dataRow["ServiceFinishedTime"] = dateTimePickerServiceFinishDate.Value.TimeOfDay;
				dataRow["TravelHours"] = numericUpDownTravelHours.Value;
				dataRow["TravelMins"] = numericUpDownTravelMins.Value;
				dataRow["LabourHours"] = numericUpDownLabourHours.Value;
				dataRow["LabourMins"] = numericUpDownLabourMins.Value;
				if (textBoxServiceHours.Text != "")
				{
					dataRow["ServiceHours"] = textBoxServiceHours.Text;
				}
				else
				{
					dataRow["ServiceHours"] = 0;
				}
				dataRow["ServiceTotal"] = textBoxServiceTotal.Text;
				dataRow["PartsTotal"] = textBoxPartsTotal.Text;
				dataRow["LabourTotal"] = textBoxLabourTotal.Text;
				dataRow["TravelTotal"] = textBoxTravelTotal.Text;
				dataRow["TotalCharges"] = textBoxTotalCharges.Text;
				dataRow["RepairDetails"] = textBoxRepairDetails.WordMLText;
				dataRow["Status"] = comboBoxStatus.SelectedID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ServiceCallTrackTable.Rows.Add(dataRow);
				}
				currentData.ServiceClientAssetDetail.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.ServiceClientAssetDetail.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["ClientAssetID"] = row.Cells["EQUIPMENT"].Value.ToString();
					dataRow2["SerialNo"] = row.Cells["SERIALNO"].Value.ToString();
					dataRow2["ProblemDescription"] = row.Cells["PROBLEM"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.ServiceClientAssetDetail.Rows.Add(dataRow2);
				}
				currentData.ServicePartsReplacedDetail.Rows.Clear();
				foreach (UltraGridRow row2 in datapartsGrid.Rows)
				{
					DataRow dataRow3 = currentData.ServicePartsReplacedDetail.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow3["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow3["ProductID"] = row2.Cells["PartNo"].Value.ToString();
					dataRow3["Quantity"] = row2.Cells["Qty"].Value.ToString();
					dataRow3["Description"] = row2.Cells["Description"].Value.ToString();
					dataRow3["ChargeableStatus"] = row2.Cells["Chargeable"].Value.ToString();
					dataRow3["RowIndex"] = row2.Index;
					dataRow3.EndEdit();
					currentData.ServicePartsReplacedDetail.Rows.Add(dataRow3);
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
				dataTable.Columns.Add("EQUIPMENT");
				dataTable.Columns.Add("SERIALNO");
				dataTable.Columns.Add("PROBLEM");
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["EQUIPMENT"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["EQUIPMENT"].MaxLength = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["EQUIPMENT"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["EQUIPMENT"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["EQUIPMENT"].ValueList = ComboBoxclientAsset;
				dataGridItems.DisplayLayout.Bands[0].Columns["EQUIPMENT"].Header.Caption = "EQUIPMENT MAKE /MODEL";
				dataGridItems.DisplayLayout.Bands[0].Columns["SERIALNO"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["SERIALNO"].MaxLength = 30;
				dataGridItems.DisplayLayout.Bands[0].Columns["SERIALNO"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["SERIALNO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["SERIALNO"].Header.Caption = "SERIAL NO:";
				dataGridItems.DisplayLayout.Bands[0].Columns["PROBLEM"].Header.Caption = "PROBLEM DESCRIPTION:";
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupPartsGrid()
		{
			datapartsGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("PartNo");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("Qty");
			dataTable.Columns.Add("Chargeable");
			datapartsGrid.DataSource = dataTable;
			datapartsGrid.DisplayLayout.Bands[0].Columns["PartNo"].CharacterCasing = CharacterCasing.Upper;
			datapartsGrid.DisplayLayout.Bands[0].Columns["PartNo"].MaxLength = 50;
			datapartsGrid.DisplayLayout.Bands[0].Columns["PartNo"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			datapartsGrid.DisplayLayout.Bands[0].Columns["PartNo"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			datapartsGrid.DisplayLayout.Bands[0].Columns["PartNo"].ValueList = ComboBoxproduct;
			datapartsGrid.DisplayLayout.Bands[0].Columns["PartNo"].Header.Caption = "PartNo.";
			datapartsGrid.DisplayLayout.Bands[0].Columns["Description"].CharacterCasing = CharacterCasing.Upper;
			datapartsGrid.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 100;
			datapartsGrid.DisplayLayout.Bands[0].Columns["Description"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			datapartsGrid.DisplayLayout.Bands[0].Columns["Description"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			datapartsGrid.DisplayLayout.Bands[0].Columns["Description"].Header.Caption = "DESCRIPTION";
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add(0, "Yes");
			valueList.ValueListItems.Add(1, "No");
			datapartsGrid.DisplayLayout.Bands[0].Columns["Chargeable"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			datapartsGrid.DisplayLayout.Bands[0].Columns["Chargeable"].ValueList = valueList;
			datapartsGrid.DisplayLayout.Bands[0].Columns["Chargeable"].Header.Caption = "Chargeable (Y/N)";
			datapartsGrid.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
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
					currentData = Factory.ServiceCallTrackSystem.GetJobMaterialRequisitionByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["Service_CallTrack"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					textBoxContactPerson.Text = dataRow["ContactName"].ToString();
					textBoxContactNo.Text = dataRow["ContactNo"].ToString();
					textBoxLocation.Text = dataRow["Location"].ToString();
					if (dataRow["ReqReceivedDate"] != DBNull.Value)
					{
						dateTimePickerReqReceivedDate.Value = DateTime.Parse(dataRow["ReqReceivedDate"].ToString());
						dateTimePickerReqReceivedDate.Checked = true;
					}
					else
					{
						dateTimePickerReqReceivedDate.Checked = false;
					}
					dateTimePickerReqReceivedTime.Value = DateTime.Parse(dataRow["ReqReceivedTime"].ToString());
					comboBoxServiceEmployee.SelectedID = dataRow["ServiceEmployeeID"].ToString();
					if (dataRow["ServiceAssignDate"] != DBNull.Value)
					{
						dateTimePickerServiceAssignedDate.Value = DateTime.Parse(dataRow["ServiceAssignDate"].ToString());
						dateTimePickerServiceAssignedDate.Checked = true;
					}
					else
					{
						dateTimePickerServiceAssignedDate.Checked = false;
					}
					dateTimePickerserviceAssignedTime.Value = DateTime.Parse(dataRow["ServiceAssignTime"].ToString());
					string a = dataRow["ServiceUnder"].ToString().Trim();
					if (a == "A")
					{
						radioButtonAMC.Checked = true;
					}
					else if (a == "W")
					{
						radioButtonWarranty.Checked = true;
					}
					else if (a == "C")
					{
						radioButtonChargeable.Checked = true;
					}
					if (dataRow["ServiceStartDate"] != DBNull.Value)
					{
						dateTimePickerServiceStartDate.Value = DateTime.Parse(dataRow["ServiceStartDate"].ToString());
						dateTimePickerServiceStartDate.Checked = true;
					}
					else
					{
						dateTimePickerServiceStartDate.Checked = false;
					}
					dateTimePickerServiceStartTime.Value = DateTime.Parse(dataRow["ServiceStartTime"].ToString());
					if (dataRow["ServiceFinishedDate"] != DBNull.Value)
					{
						dateTimePickerServiceFinishDate.Value = DateTime.Parse(dataRow["ServiceFinishedDate"].ToString());
						dateTimePickerServiceFinishDate.Checked = true;
					}
					else
					{
						dateTimePickerServiceFinishDate.Checked = false;
					}
					numericUpDownTravelHours.Value = int.Parse(dataRow["TravelHours"].ToString());
					numericUpDownTravelMins.Value = int.Parse(dataRow["TravelMins"].ToString());
					numericUpDownLabourHours.Value = int.Parse(dataRow["LabourHours"].ToString());
					numericUpDownLabourMins.Value = int.Parse(dataRow["LabourMins"].ToString());
					textBoxServiceHours.Text = dataRow["ServiceHours"].ToString();
					textBoxServiceTotal.Text = dataRow["ServiceTotal"].ToString();
					textBoxPartsTotal.Text = dataRow["PartsTotal"].ToString();
					textBoxLabourTotal.Text = dataRow["LabourTotal"].ToString();
					textBoxTravelTotal.Text = dataRow["TravelTotal"].ToString();
					textBoxTotalCharges.Text = dataRow["TotalCharges"].ToString();
					if (dataRow["Status"] != DBNull.Value)
					{
						comboBoxStatus.SelectedIndex = int.Parse(dataRow["Status"].ToString());
					}
					else
					{
						comboBoxStatus.SelectedIndex = 0;
					}
					textBoxRepairDetails.WordMLText = dataRow["RepairDetails"].ToString();
					textBoxRepairDetails.EndUpdate();
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					DataTable dataTable2 = datapartsGrid.DataSource as DataTable;
					dataTable2.Rows.Clear();
					if ((currentData.Tables.Contains("Service_ClientAsset_Detail") && currentData.ServiceClientAssetDetail.Rows.Count != 0) || (currentData.Tables.Contains("Service_PartsReplaced_Detail") && currentData.ServicePartsReplacedDetail.Rows.Count != 0))
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Service_ClientAsset_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["EQUIPMENT"] = row["ClientAssetID"];
							dataRow3["SERIALNO"] = row["SerialNo"];
							dataRow3["PROBLEM"] = row["ProblemDescription"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.EndUpdate();
						datapartsGrid.BeginUpdate();
						foreach (DataRow row2 in currentData.Tables["Service_PartsReplaced_Detail"].Rows)
						{
							DataRow dataRow5 = dataTable2.NewRow();
							dataRow5["PartNo"] = row2["ProductID"];
							dataRow5["Description"] = row2["Description"];
							dataRow5["Qty"] = row2["Quantity"];
							dataRow5["Chargeable"] = row2["ChargeableStatus"];
							dataRow5.EndEdit();
							dataTable2.Rows.Add(dataRow5);
						}
						dataTable.AcceptChanges();
						datapartsGrid.EndUpdate();
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
				bool flag = Factory.ServiceCallTrackSystem.CreateJobMaterialRequisition(currentData, !isNewRecord);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Service_CallTrack", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one item row.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Service_CallTrack", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxRepairDetails.ResetText();
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerReqReceivedDate.Value = DateTime.Now;
				dateTimePickerReqReceivedDate.Checked = false;
				comboBoxCustomer.Clear();
				comboBoxJob.Clear();
				textBoxBilltoAddress.Clear();
				textBoxContactPerson.Clear();
				textBoxContactNo.Clear();
				dateTimePickerReqReceivedDate.Checked = false;
				dateTimePickerReqReceivedTime.Checked = false;
				comboBoxServiceEmployee.Clear();
				dateTimePickerServiceAssignedDate.Checked = false;
				dateTimePickerserviceAssignedTime.Value = DateTime.Now;
				textBoxLocation.Clear();
				RadioButton radioButton = radioButtonAMC;
				RadioButton radioButton2 = radioButtonWarranty;
				bool flag2 = radioButtonChargeable.Checked = false;
				bool @checked = radioButton2.Checked = flag2;
				radioButton.Checked = @checked;
				dateTimePickerServiceStartDate.Checked = false;
				dateTimePickerServiceStartDate.Value = DateTime.Now;
				dateTimePickerServiceStartTime.Value = DateTime.Now;
				dateTimePickerServiceFinishDate.Checked = false;
				dateTimePickerServiceFinishDate.Value = DateTime.Now;
				numericUpDownTravelHours.Value = 0m;
				numericUpDownTravelMins.Value = 0m;
				numericUpDownLabourHours.Value = 0m;
				numericUpDownLabourMins.Value = 0m;
				textBoxServiceHours.Clear();
				textBoxServiceTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPartsTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxLabourTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTravelTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotalCharges.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxStatus.LoadData();
				comboBoxStatus.SelectedIndex = 0;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				(datapartsGrid.DataSource as DataTable).Rows.Clear();
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
				if (!IsNewRecord)
				{
					bool flag = true;
					string text = "";
					string str = "";
					DataSet dataSet = Factory.ServiceCallTrackSystem.AllowDelete(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
					if (dataSet.Tables["PODetails"].Rows.Count > 0 || dataSet.Tables["IssueDetails"].Rows.Count > 0)
					{
						flag = false;
						if (dataSet.Tables["PODetails"].Rows.Count > 0)
						{
							text = dataSet.Tables["PODetails"].Rows[0]["VoucherID"].ToString();
						}
						if (text == "")
						{
							str = dataSet.Tables["IssueDetails"].Rows[0]["VoucherID"].ToString();
						}
					}
					if (!flag)
					{
						if (text != "")
						{
							ErrorHelper.WarningMessage("Some items in this transaction has been already allocated in purchase order " + text + ". You are not able to modify.");
						}
						else
						{
							ErrorHelper.WarningMessage("Some items in this transaction has been already allocated in Issued " + str + ". You are not able to modify.");
						}
						return false;
					}
				}
				return Factory.ServiceCallTrackSystem.DeleteJobMaterialRequisition(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Service_CallTrack", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Service_CallTrack", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Service_CallTrack", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Service_CallTrack", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Service_CallTrack", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				datapartsGrid.SetupUI();
				SetupPartsGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.ServiceCallTrack);
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerReqReceivedTime.Format = DateTimePickerFormat.Time;
				dateTimePickerReqReceivedTime.ShowUpDown = true;
				dateTimePickerserviceAssignedTime.Format = DateTimePickerFormat.Time;
				dateTimePickerserviceAssignedTime.ShowUpDown = true;
				dateTimePickerServiceStartTime.Format = DateTimePickerFormat.Time;
				dateTimePickerServiceStartTime.ShowUpDown = true;
				dateTimePickerServiceEndTime.Format = DateTimePickerFormat.Time;
				dateTimePickerServiceEndTime.ShowUpDown = true;
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
				datapartsGrid.LoadLayoutFailed = true;
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.ServiceCallTrack);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 120.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.ServiceCallTrack);
					currentData = (dataSet as ServiceCallTrackData);
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
			Print(isPrint: false, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					if (selectedID == "" || text == "")
					{
						ErrorHelper.InformationMessage("Please select a document to print!");
					}
					else
					{
						DataSet jobMaterialRequisitionToPrint = Factory.ServiceCallTrackSystem.GetJobMaterialRequisitionToPrint(selectedID, text);
						if (jobMaterialRequisitionToPrint == null || jobMaterialRequisitionToPrint.Tables.Count == 0)
						{
							ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
						}
						else
						{
							string text3 = (string)(jobMaterialRequisitionToPrint.Tables["Service_CallTrack"].Rows[0]["StrRepairDetails"] = textBoxRepairDetails.Text);
							PrintHelper.PrintDocument(jobMaterialRequisitionToPrint, selectedID, "Service Call Track", SysDocTypes.ServiceCallTrack, isPrint, showPrintDialog);
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

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Service_CallTrack", "VoucherID");
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

		private void LinkLabelVoucherNumber_Clicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void LinkLabelDocID_Clicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ServiceCallTrack);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void changeRequestStatusToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdatePOStatusDialog updatePOStatusDialog = new UpdatePOStatusDialog();
			updatePOStatusDialog.SetDocument(SysDocTypes.ServiceCallTrack, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
			updatePOStatusDialog.ShowDialog(this);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ServiceCallTrackListFormObj);
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

		private void toolStripSeparator7_Click(object sender, EventArgs e)
		{
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

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
		}

		private void datapartsGrid_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (datapartsGrid.ActiveRow != null && e.Cell.Column.Key == "PartNo")
				{
					datapartsGrid.ActiveRow.Cells["Description"].Value = ComboBoxproduct.SelectedName;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void CalculateTotal()
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal num = default(decimal);
			decimal.TryParse(textBoxPartsTotal.Text, out result);
			decimal.TryParse(textBoxLabourTotal.Text, out result2);
			decimal.TryParse(textBoxTravelTotal.Text, out result3);
			num = result + result2 + result3;
			textBoxTotalCharges.Text = Math.Round(num, Global.CurDecimalPoints).ToString();
		}

		private void textBoxPartsTotal_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxLabourTotal_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxTravelTotal_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxServiceEmployee.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.ServiceCallTrackForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ComboBoxclientAsset = new Micromind.DataControls.ClientAssetComboBox();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ComboBoxproduct = new Micromind.DataControls.ProductComboBox();
			datapartsGrid = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxRepairDetails = new DevExpress.XtraRichEdit.RichEditControl();
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
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			saveADraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			changeRequestStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxLocation = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			textBoxJobName = new System.Windows.Forms.TextBox();
			comboBoxStatus = new Micromind.DataControls.CampaignStatusComboBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			comboBoxServiceEmployee = new Micromind.DataControls.EmployeeComboBox();
			dateTimePickerserviceAssignedTime = new System.Windows.Forms.DateTimePicker();
			label5 = new System.Windows.Forms.Label();
			dateTimePickerReqReceivedTime = new System.Windows.Forms.DateTimePicker();
			dateTimePickerServiceAssignedDate = new System.Windows.Forms.DateTimePicker();
			label3 = new System.Windows.Forms.Label();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxContactNo = new Micromind.UISupport.MMTextBox();
			textBoxContactPerson = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
			dateTimePickerReqReceivedDate = new System.Windows.Forms.DateTimePicker();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			label13 = new System.Windows.Forms.Label();
			textBoxServiceHours = new Micromind.UISupport.NumberTextBox();
			label12 = new System.Windows.Forms.Label();
			textBoxServiceTotal = new Micromind.UISupport.AmountTextBox();
			numericUpDownLabourMins = new System.Windows.Forms.NumericUpDown();
			numericUpDownLabourHours = new System.Windows.Forms.NumericUpDown();
			numericUpDownTravelMins = new System.Windows.Forms.NumericUpDown();
			numericUpDownTravelHours = new System.Windows.Forms.NumericUpDown();
			label11 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			dateTimePickerServiceFinishDate = new System.Windows.Forms.DateTimePicker();
			dateTimePickerServiceStartDate = new System.Windows.Forms.DateTimePicker();
			dateTimePickerServiceEndTime = new System.Windows.Forms.DateTimePicker();
			dateTimePickerServiceStartTime = new System.Windows.Forms.DateTimePicker();
			label9 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			radioButtonChargeable = new System.Windows.Forms.RadioButton();
			radioButtonWarranty = new System.Windows.Forms.RadioButton();
			radioButtonAMC = new System.Windows.Forms.RadioButton();
			label7 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			label18 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			textBoxTotalCharges = new Micromind.UISupport.AmountTextBox();
			textBoxTravelTotal = new Micromind.UISupport.AmountTextBox();
			textBoxLabourTotal = new Micromind.UISupport.AmountTextBox();
			textBoxPartsTotal = new Micromind.UISupport.AmountTextBox();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxclientAsset).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxproduct).BeginInit();
			((System.ComponentModel.ISupportInitialize)datapartsGrid).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownLabourMins).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownLabourHours).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTravelMins).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTravelHours).BeginInit();
			SuspendLayout();
			ultraTabPageControl2.Controls.Add(ComboBoxclientAsset);
			ultraTabPageControl2.Controls.Add(dataGridItems);
			ultraTabPageControl2.Location = new System.Drawing.Point(1, 23);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(805, 255);
			ComboBoxclientAsset.Assigned = false;
			ComboBoxclientAsset.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxclientAsset.CalcManager = ultraCalcManager1;
			ComboBoxclientAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxclientAsset.CustomReportFieldName = "";
			ComboBoxclientAsset.CustomReportKey = "";
			ComboBoxclientAsset.CustomReportValueType = 1;
			ComboBoxclientAsset.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxclientAsset.DisplayLayout.Appearance = appearance;
			ComboBoxclientAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxclientAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxclientAsset.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxclientAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ComboBoxclientAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxclientAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ComboBoxclientAsset.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxclientAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxclientAsset.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxclientAsset.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ComboBoxclientAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxclientAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxclientAsset.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxclientAsset.DisplayLayout.Override.CellAppearance = appearance8;
			ComboBoxclientAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxclientAsset.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxclientAsset.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ComboBoxclientAsset.DisplayLayout.Override.HeaderAppearance = appearance10;
			ComboBoxclientAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxclientAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ComboBoxclientAsset.DisplayLayout.Override.RowAppearance = appearance11;
			ComboBoxclientAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxclientAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ComboBoxclientAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxclientAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxclientAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxclientAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxclientAsset.Editable = true;
			ComboBoxclientAsset.FilterString = "";
			ComboBoxclientAsset.HasAllAccount = false;
			ComboBoxclientAsset.HasCustom = false;
			ComboBoxclientAsset.IsDataLoaded = false;
			ComboBoxclientAsset.Location = new System.Drawing.Point(533, 55);
			ComboBoxclientAsset.MaxDropDownItems = 12;
			ComboBoxclientAsset.Name = "ComboBoxclientAsset";
			ComboBoxclientAsset.ShowInactiveItems = false;
			ComboBoxclientAsset.ShowQuickAdd = true;
			ComboBoxclientAsset.Size = new System.Drawing.Size(100, 20);
			ComboBoxclientAsset.TabIndex = 6;
			ComboBoxclientAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxclientAsset.Visible = false;
			ultraCalcManager1.ContainingControl = this;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance13;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(2, 3);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(795, 249);
			dataGridItems.TabIndex = 0;
			dataGridItems.Text = "dataEntryGrid1";
			ultraTabPageControl3.Controls.Add(ComboBoxproduct);
			ultraTabPageControl3.Controls.Add(datapartsGrid);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(805, 255);
			ComboBoxproduct.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			ComboBoxproduct.Assigned = false;
			ComboBoxproduct.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxproduct.CalcManager = ultraCalcManager1;
			ComboBoxproduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxproduct.CustomReportFieldName = "";
			ComboBoxproduct.CustomReportKey = "";
			ComboBoxproduct.CustomReportValueType = 1;
			ComboBoxproduct.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxproduct.DisplayLayout.Appearance = appearance25;
			ComboBoxproduct.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxproduct.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxproduct.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxproduct.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			ComboBoxproduct.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxproduct.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			ComboBoxproduct.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxproduct.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxproduct.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxproduct.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			ComboBoxproduct.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxproduct.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxproduct.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxproduct.DisplayLayout.Override.CellAppearance = appearance32;
			ComboBoxproduct.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxproduct.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxproduct.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			ComboBoxproduct.DisplayLayout.Override.HeaderAppearance = appearance34;
			ComboBoxproduct.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxproduct.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			ComboBoxproduct.DisplayLayout.Override.RowAppearance = appearance35;
			ComboBoxproduct.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxproduct.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			ComboBoxproduct.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxproduct.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxproduct.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxproduct.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxproduct.Editable = true;
			ComboBoxproduct.FilterCustomerID = "";
			ComboBoxproduct.FilterString = "";
			ComboBoxproduct.FilterSysDocID = "";
			ComboBoxproduct.HasAllAccount = false;
			ComboBoxproduct.HasCustom = false;
			ComboBoxproduct.IsDataLoaded = false;
			ComboBoxproduct.Location = new System.Drawing.Point(352, 120);
			ComboBoxproduct.MaxDropDownItems = 12;
			ComboBoxproduct.Name = "ComboBoxproduct";
			ComboBoxproduct.Show3PLItems = true;
			ComboBoxproduct.ShowInactiveItems = false;
			ComboBoxproduct.ShowQuickAdd = true;
			ComboBoxproduct.Size = new System.Drawing.Size(100, 20);
			ComboBoxproduct.TabIndex = 8;
			ComboBoxproduct.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxproduct.Visible = false;
			datapartsGrid.AllowAddNew = false;
			datapartsGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			datapartsGrid.CalcManager = ultraCalcManager1;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			datapartsGrid.DisplayLayout.Appearance = appearance37;
			datapartsGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			datapartsGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			datapartsGrid.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			datapartsGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			datapartsGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			datapartsGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			datapartsGrid.DisplayLayout.MaxColScrollRegions = 1;
			datapartsGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			datapartsGrid.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			datapartsGrid.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			datapartsGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			datapartsGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			datapartsGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			datapartsGrid.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			datapartsGrid.DisplayLayout.Override.CellAppearance = appearance44;
			datapartsGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			datapartsGrid.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			datapartsGrid.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			datapartsGrid.DisplayLayout.Override.HeaderAppearance = appearance46;
			datapartsGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			datapartsGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			datapartsGrid.DisplayLayout.Override.RowAppearance = appearance47;
			datapartsGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			datapartsGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			datapartsGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			datapartsGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			datapartsGrid.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			datapartsGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			datapartsGrid.ExitEditModeOnLeave = false;
			datapartsGrid.IncludeLotItems = false;
			datapartsGrid.LoadLayoutFailed = false;
			datapartsGrid.Location = new System.Drawing.Point(5, 3);
			datapartsGrid.MinimumSize = new System.Drawing.Size(622, 154);
			datapartsGrid.Name = "datapartsGrid";
			datapartsGrid.ShowClearMenu = true;
			datapartsGrid.ShowDeleteMenu = true;
			datapartsGrid.ShowInsertMenu = true;
			datapartsGrid.ShowMoveRowsMenu = true;
			datapartsGrid.Size = new System.Drawing.Size(795, 249);
			datapartsGrid.TabIndex = 0;
			datapartsGrid.Text = "dataEntryGrid1";
			ultraTabPageControl1.Controls.Add(textBoxRepairDetails);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(805, 255);
			textBoxRepairDetails.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxRepairDetails.Location = new System.Drawing.Point(2, 3);
			textBoxRepairDetails.Name = "textBoxRepairDetails";
			textBoxRepairDetails.Size = new System.Drawing.Size(800, 247);
			textBoxRepairDetails.TabIndex = 0;
			textBoxRepairDetails.Text = "richEditControl1";
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
				toolStripButtonDistribution,
				toolStripButtonInformation,
				toolStripButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(820, 31);
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
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
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator7.Click += new System.EventHandler(toolStripSeparator7_Click);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
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
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				saveADraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				toolStripSeparator4,
				changeRequestStatusToolStripMenuItem
			});
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(60, 28);
			toolStripButton1.Text = "Actions";
			saveADraftToolStripMenuItem.Name = "saveADraftToolStripMenuItem";
			saveADraftToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			saveADraftToolStripMenuItem.Text = "Save as Draft";
			saveADraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(201, 6);
			changeRequestStatusToolStripMenuItem.Name = "changeRequestStatusToolStripMenuItem";
			changeRequestStatusToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			changeRequestStatusToolStripMenuItem.Text = "Change Request Status...";
			changeRequestStatusToolStripMenuItem.Click += new System.EventHandler(changeRequestStatusToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 674);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(820, 40);
			panelButtons.TabIndex = 9;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 12;
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
			buttonDelete.TabIndex = 11;
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
			buttonNew.TabIndex = 10;
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
			linePanelDown.Size = new System.Drawing.Size(820, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(710, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 13;
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
			buttonSave.TabIndex = 9;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(510, 2);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 20);
			dateTimePickerDate.TabIndex = 3;
			textBoxVoucherNumber.Location = new System.Drawing.Point(302, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 2;
			appearance49.FontData.BoldAsString = "True";
			appearance49.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance49;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(195, 5);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(101, 15);
			linkLabelVoucherNumber.TabIndex = 2;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Voucher Number:";
			appearance50.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance50;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(LinkLabelVoucherNumber_Clicked);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(textBoxLocation);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel7);
			panelDetails.Controls.Add(comboBoxJob);
			panelDetails.Controls.Add(textBoxJobName);
			panelDetails.Controls.Add(comboBoxStatus);
			panelDetails.Controls.Add(mmLabel6);
			panelDetails.Controls.Add(comboBoxServiceEmployee);
			panelDetails.Controls.Add(dateTimePickerserviceAssignedTime);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(dateTimePickerReqReceivedTime);
			panelDetails.Controls.Add(dateTimePickerServiceAssignedDate);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(mmLabel17);
			panelDetails.Controls.Add(textBoxContactNo);
			panelDetails.Controls.Add(textBoxContactPerson);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(textBoxBilltoAddress);
			panelDetails.Controls.Add(dateTimePickerReqReceivedDate);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 32);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(813, 208);
			panelDetails.TabIndex = 0;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(330, 122);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(51, 13);
			mmLabel2.TabIndex = 163;
			mmLabel2.Text = "Location:";
			textBoxLocation.BackColor = System.Drawing.Color.White;
			textBoxLocation.CustomReportFieldName = "";
			textBoxLocation.CustomReportKey = "";
			textBoxLocation.CustomReportValueType = 1;
			textBoxLocation.IsComboTextBox = false;
			textBoxLocation.IsModified = false;
			textBoxLocation.Location = new System.Drawing.Point(396, 119);
			textBoxLocation.MaxLength = 30;
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.Size = new System.Drawing.Size(229, 20);
			textBoxLocation.TabIndex = 162;
			appearance51.FontData.BoldAsString = "False";
			appearance51.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance51;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(3, 170);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(94, 15);
			ultraFormattedLinkLabel3.TabIndex = 161;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Service Assign To:";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance52;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance53.FontData.BoldAsString = "True";
			appearance53.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance53;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(10, 29);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel7.TabIndex = 160;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Project:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance54;
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CalcManager = ultraCalcManager1;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = textBoxJobName;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(84, 25);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(109, 20);
			comboBoxJob.TabIndex = 4;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxJobName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJobName.Location = new System.Drawing.Point(195, 26);
			textBoxJobName.MaxLength = 64;
			textBoxJobName.Name = "textBoxJobName";
			textBoxJobName.ReadOnly = true;
			textBoxJobName.Size = new System.Drawing.Size(376, 20);
			textBoxJobName.TabIndex = 159;
			textBoxJobName.TabStop = false;
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(641, 46);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(164, 21);
			comboBoxStatus.TabIndex = 40;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(589, 51);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(47, 13);
			mmLabel6.TabIndex = 41;
			mmLabel6.Text = "Status:";
			comboBoxServiceEmployee.Assigned = false;
			comboBoxServiceEmployee.CalcManager = ultraCalcManager1;
			comboBoxServiceEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxServiceEmployee.CustomReportFieldName = "";
			comboBoxServiceEmployee.CustomReportKey = "";
			comboBoxServiceEmployee.CustomReportValueType = 1;
			comboBoxServiceEmployee.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxServiceEmployee.DisplayLayout.Appearance = appearance55;
			comboBoxServiceEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxServiceEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceEmployee.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxServiceEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxServiceEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxServiceEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxServiceEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxServiceEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxServiceEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxServiceEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxServiceEmployee.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxServiceEmployee.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxServiceEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxServiceEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxServiceEmployee.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxServiceEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxServiceEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxServiceEmployee.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxServiceEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxServiceEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
			comboBoxServiceEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxServiceEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxServiceEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxServiceEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxServiceEmployee.Editable = true;
			comboBoxServiceEmployee.FilterString = "";
			comboBoxServiceEmployee.HasAllAccount = false;
			comboBoxServiceEmployee.HasCustom = false;
			comboBoxServiceEmployee.IsDataLoaded = false;
			comboBoxServiceEmployee.Location = new System.Drawing.Point(103, 169);
			comboBoxServiceEmployee.MaxDropDownItems = 12;
			comboBoxServiceEmployee.Name = "comboBoxServiceEmployee";
			comboBoxServiceEmployee.ShowInactiveItems = false;
			comboBoxServiceEmployee.ShowQuickAdd = true;
			comboBoxServiceEmployee.ShowTerminatedEmployees = false;
			comboBoxServiceEmployee.Size = new System.Drawing.Size(153, 20);
			comboBoxServiceEmployee.TabIndex = 8;
			comboBoxServiceEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerserviceAssignedTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerserviceAssignedTime.Location = new System.Drawing.Point(494, 169);
			dateTimePickerserviceAssignedTime.Name = "dateTimePickerserviceAssignedTime";
			dateTimePickerserviceAssignedTime.Size = new System.Drawing.Size(109, 20);
			dateTimePickerserviceAssignedTime.TabIndex = 13;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(455, 172);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 157;
			label5.Text = "Time:";
			dateTimePickerReqReceivedTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerReqReceivedTime.Location = new System.Drawing.Point(252, 138);
			dateTimePickerReqReceivedTime.Name = "dateTimePickerReqReceivedTime";
			dateTimePickerReqReceivedTime.Size = new System.Drawing.Size(109, 20);
			dateTimePickerReqReceivedTime.TabIndex = 11;
			dateTimePickerServiceAssignedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerServiceAssignedDate.Location = new System.Drawing.Point(318, 169);
			dateTimePickerServiceAssignedDate.Name = "dateTimePickerServiceAssignedDate";
			dateTimePickerServiceAssignedDate.ShowCheckBox = true;
			dateTimePickerServiceAssignedDate.Size = new System.Drawing.Size(109, 20);
			dateTimePickerServiceAssignedDate.TabIndex = 12;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(212, 141);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 154;
			label3.Text = "Time:";
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(330, 99);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(65, 13);
			mmLabel17.TabIndex = 152;
			mmLabel17.Text = "Contact No:";
			textBoxContactNo.BackColor = System.Drawing.Color.White;
			textBoxContactNo.CustomReportFieldName = "";
			textBoxContactNo.CustomReportKey = "";
			textBoxContactNo.CustomReportValueType = 1;
			textBoxContactNo.IsComboTextBox = false;
			textBoxContactNo.IsModified = false;
			textBoxContactNo.Location = new System.Drawing.Point(396, 96);
			textBoxContactNo.MaxLength = 30;
			textBoxContactNo.Name = "textBoxContactNo";
			textBoxContactNo.Size = new System.Drawing.Size(229, 20);
			textBoxContactNo.TabIndex = 10;
			textBoxContactPerson.Location = new System.Drawing.Point(396, 73);
			textBoxContactPerson.MaxLength = 30;
			textBoxContactPerson.Name = "textBoxContactPerson";
			textBoxContactPerson.Size = new System.Drawing.Size(229, 20);
			textBoxContactPerson.TabIndex = 9;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(311, 76);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(83, 13);
			label6.TabIndex = 151;
			label6.Text = "Contact Person:";
			appearance67.FontData.BoldAsString = "False";
			appearance67.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance67;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(14, 73);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(38, 15);
			ultraFormattedLinkLabel1.TabIndex = 149;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill To:";
			appearance68.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance68;
			textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
			textBoxBilltoAddress.CustomReportFieldName = "";
			textBoxBilltoAddress.CustomReportKey = "";
			textBoxBilltoAddress.CustomReportValueType = 1;
			textBoxBilltoAddress.IsComboTextBox = false;
			textBoxBilltoAddress.IsModified = false;
			textBoxBilltoAddress.Location = new System.Drawing.Point(84, 73);
			textBoxBilltoAddress.MaxLength = 255;
			textBoxBilltoAddress.Multiline = true;
			textBoxBilltoAddress.Name = "textBoxBilltoAddress";
			textBoxBilltoAddress.Size = new System.Drawing.Size(215, 59);
			textBoxBilltoAddress.TabIndex = 6;
			dateTimePickerReqReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerReqReceivedDate.Location = new System.Drawing.Point(84, 136);
			dateTimePickerReqReceivedDate.Name = "dateTimePickerReqReceivedDate";
			dateTimePickerReqReceivedDate.ShowCheckBox = true;
			dateTimePickerReqReceivedDate.Size = new System.Drawing.Size(109, 20);
			dateTimePickerReqReceivedDate.TabIndex = 7;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(278, 172);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 144;
			label4.Text = "Date:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 124);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(62, 39);
			label2.TabIndex = 142;
			label2.Text = "Req\r\nReceived : \r\nDate :";
			appearance69.FontData.BoldAsString = "True";
			appearance69.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance69;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(3, 51);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel4.TabIndex = 132;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Customer ID:";
			appearance70.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance70;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCustomer.AlwaysInEditMode = true;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CalcManager = ultraCalcManager1;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxCustomerName;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance71;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance72;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance73;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance74.BackColor2 = System.Drawing.SystemColors.Control;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance74;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance75;
			appearance76.BackColor = System.Drawing.SystemColors.Highlight;
			appearance76.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance76;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance77;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			appearance78.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance78;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance79.BackColor = System.Drawing.SystemColors.Control;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance79;
			appearance80.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance80;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance81;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance82;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.FilterSysDocID = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.IsDataLoaded = false;
			comboBoxCustomer.Location = new System.Drawing.Point(84, 48);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(109, 20);
			comboBoxCustomer.TabIndex = 5;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(195, 48);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(376, 20);
			textBoxCustomerName.TabIndex = 134;
			textBoxCustomerName.TabStop = false;
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance83;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance84;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance85;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance86.BackColor2 = System.Drawing.SystemColors.Control;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance86;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance87;
			appearance88.BackColor = System.Drawing.SystemColors.Highlight;
			appearance88.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance88;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance89;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			appearance90.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance90;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance91.BackColor = System.Drawing.SystemColors.Control;
			appearance91.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance91.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance91;
			appearance92.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance92;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance93;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance94;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(84, 2);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance95.FontData.BoldAsString = "True";
			appearance95.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance95;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 5);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance96.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance96;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(LinkLabelDocID_Clicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(466, 5);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				printListToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(130, 26);
			printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			printListToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			printListToolStripMenuItem.Text = "Print List...";
			printListToolStripMenuItem.Click += new System.EventHandler(printListToolStripMenuItem_Click);
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Location = new System.Drawing.Point(7, 246);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(809, 281);
			ultraTabControl1.TabIndex = 23;
			ultraTab.TabPage = ultraTabPageControl2;
			ultraTab.Text = "Asset Details";
			ultraTab2.TabPage = ultraTabPageControl3;
			ultraTab2.Text = "Parts Replaced";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "Repair Details/Remarks";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(805, 255);
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panel1.Controls.Add(label13);
			panel1.Controls.Add(textBoxServiceHours);
			panel1.Controls.Add(label12);
			panel1.Controls.Add(textBoxServiceTotal);
			panel1.Controls.Add(numericUpDownLabourMins);
			panel1.Controls.Add(numericUpDownLabourHours);
			panel1.Controls.Add(numericUpDownTravelMins);
			panel1.Controls.Add(numericUpDownTravelHours);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label10);
			panel1.Controls.Add(dateTimePickerServiceFinishDate);
			panel1.Controls.Add(dateTimePickerServiceStartDate);
			panel1.Controls.Add(dateTimePickerServiceEndTime);
			panel1.Controls.Add(dateTimePickerServiceStartTime);
			panel1.Controls.Add(label9);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(radioButtonChargeable);
			panel1.Controls.Add(radioButtonWarranty);
			panel1.Controls.Add(radioButtonAMC);
			panel1.Controls.Add(label7);
			panel1.Location = new System.Drawing.Point(12, 537);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(476, 131);
			panel1.TabIndex = 24;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(269, 80);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(38, 13);
			label13.TabIndex = 172;
			label13.Text = "Hours:";
			textBoxServiceHours.AllowDecimal = true;
			textBoxServiceHours.CustomReportFieldName = "";
			textBoxServiceHours.CustomReportKey = "";
			textBoxServiceHours.CustomReportValueType = 1;
			textBoxServiceHours.IsComboTextBox = false;
			textBoxServiceHours.IsModified = false;
			textBoxServiceHours.Location = new System.Drawing.Point(315, 75);
			textBoxServiceHours.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxServiceHours.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxServiceHours.Name = "textBoxServiceHours";
			textBoxServiceHours.NullText = "0";
			textBoxServiceHours.Size = new System.Drawing.Size(147, 20);
			textBoxServiceHours.TabIndex = 12;
			textBoxServiceHours.Text = "0";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(268, 104);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(40, 13);
			label12.TabIndex = 170;
			label12.Text = "Dhrms:";
			textBoxServiceTotal.AllowDecimal = true;
			textBoxServiceTotal.CustomReportFieldName = "";
			textBoxServiceTotal.CustomReportKey = "";
			textBoxServiceTotal.CustomReportValueType = 1;
			textBoxServiceTotal.IsComboTextBox = false;
			textBoxServiceTotal.IsModified = false;
			textBoxServiceTotal.Location = new System.Drawing.Point(315, 101);
			textBoxServiceTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxServiceTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxServiceTotal.Name = "textBoxServiceTotal";
			textBoxServiceTotal.NullText = "0";
			textBoxServiceTotal.Size = new System.Drawing.Size(147, 20);
			textBoxServiceTotal.TabIndex = 13;
			textBoxServiceTotal.Text = "0.00";
			textBoxServiceTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxServiceTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			numericUpDownLabourMins.Location = new System.Drawing.Point(150, 102);
			numericUpDownLabourMins.Maximum = new decimal(new int[4]
			{
				59,
				0,
				0,
				0
			});
			numericUpDownLabourMins.Name = "numericUpDownLabourMins";
			numericUpDownLabourMins.Size = new System.Drawing.Size(53, 20);
			numericUpDownLabourMins.TabIndex = 11;
			numericUpDownLabourHours.Location = new System.Drawing.Point(91, 102);
			numericUpDownLabourHours.Maximum = new decimal(new int[4]
			{
				24,
				0,
				0,
				0
			});
			numericUpDownLabourHours.Name = "numericUpDownLabourHours";
			numericUpDownLabourHours.Size = new System.Drawing.Size(53, 20);
			numericUpDownLabourHours.TabIndex = 9;
			numericUpDownTravelMins.Location = new System.Drawing.Point(150, 80);
			numericUpDownTravelMins.Maximum = new decimal(new int[4]
			{
				59,
				0,
				0,
				0
			});
			numericUpDownTravelMins.Name = "numericUpDownTravelMins";
			numericUpDownTravelMins.Size = new System.Drawing.Size(53, 20);
			numericUpDownTravelMins.TabIndex = 10;
			numericUpDownTravelHours.Location = new System.Drawing.Point(91, 79);
			numericUpDownTravelHours.Maximum = new decimal(new int[4]
			{
				24,
				0,
				0,
				0
			});
			numericUpDownTravelHours.Name = "numericUpDownTravelHours";
			numericUpDownTravelHours.Size = new System.Drawing.Size(53, 20);
			numericUpDownTravelHours.TabIndex = 8;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(7, 104);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(74, 13);
			label11.TabIndex = 164;
			label11.Text = "Labour Hours:";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(6, 81);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(71, 13);
			label10.TabIndex = 163;
			label10.Text = "Travel Hours:";
			dateTimePickerServiceFinishDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerServiceFinishDate.Location = new System.Drawing.Point(193, 54);
			dateTimePickerServiceFinishDate.Name = "dateTimePickerServiceFinishDate";
			dateTimePickerServiceFinishDate.ShowCheckBox = true;
			dateTimePickerServiceFinishDate.Size = new System.Drawing.Size(91, 20);
			dateTimePickerServiceFinishDate.TabIndex = 7;
			dateTimePickerServiceStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerServiceStartDate.Location = new System.Drawing.Point(191, 28);
			dateTimePickerServiceStartDate.Name = "dateTimePickerServiceStartDate";
			dateTimePickerServiceStartDate.ShowCheckBox = true;
			dateTimePickerServiceStartDate.Size = new System.Drawing.Size(93, 20);
			dateTimePickerServiceStartDate.TabIndex = 6;
			dateTimePickerServiceEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerServiceEndTime.Location = new System.Drawing.Point(91, 54);
			dateTimePickerServiceEndTime.Name = "dateTimePickerServiceEndTime";
			dateTimePickerServiceEndTime.Size = new System.Drawing.Size(90, 20);
			dateTimePickerServiceEndTime.TabIndex = 5;
			dateTimePickerServiceStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerServiceStartTime.Location = new System.Drawing.Point(91, 28);
			dateTimePickerServiceStartTime.Name = "dateTimePickerServiceStartTime";
			dateTimePickerServiceStartTime.Size = new System.Drawing.Size(90, 20);
			dateTimePickerServiceStartTime.TabIndex = 4;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(6, 56);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(49, 13);
			label9.TabIndex = 160;
			label9.Text = "Finished:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(6, 32);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(44, 13);
			label8.TabIndex = 159;
			label8.Text = "Started:";
			radioButtonChargeable.AutoSize = true;
			radioButtonChargeable.Location = new System.Drawing.Point(298, 4);
			radioButtonChargeable.Name = "radioButtonChargeable";
			radioButtonChargeable.Size = new System.Drawing.Size(79, 17);
			radioButtonChargeable.TabIndex = 3;
			radioButtonChargeable.TabStop = true;
			radioButtonChargeable.Text = "Chargeable";
			radioButtonChargeable.UseVisualStyleBackColor = true;
			radioButtonWarranty.AutoSize = true;
			radioButtonWarranty.Location = new System.Drawing.Point(193, 4);
			radioButtonWarranty.Name = "radioButtonWarranty";
			radioButtonWarranty.Size = new System.Drawing.Size(68, 17);
			radioButtonWarranty.TabIndex = 2;
			radioButtonWarranty.TabStop = true;
			radioButtonWarranty.Text = "Warranty";
			radioButtonWarranty.UseVisualStyleBackColor = true;
			radioButtonAMC.AutoSize = true;
			radioButtonAMC.Location = new System.Drawing.Point(96, 4);
			radioButtonAMC.Name = "radioButtonAMC";
			radioButtonAMC.Size = new System.Drawing.Size(48, 17);
			radioButtonAMC.TabIndex = 1;
			radioButtonAMC.TabStop = true;
			radioButtonAMC.Text = "AMC";
			radioButtonAMC.UseVisualStyleBackColor = true;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(6, 6);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(78, 13);
			label7.TabIndex = 0;
			label7.Text = "Service Under:";
			label14.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(602, 540);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(64, 13);
			label14.TabIndex = 173;
			label14.Text = "Parts Total :";
			label15.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(601, 564);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(73, 13);
			label15.TabIndex = 174;
			label15.Text = "Labour Total :";
			label16.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(601, 588);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(67, 13);
			label16.TabIndex = 175;
			label16.Text = "Travel Total:";
			label17.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(601, 613);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(76, 13);
			label17.TabIndex = 176;
			label17.Text = "Total Charges:";
			label18.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(615, 642);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(62, 13);
			label18.TabIndex = 177;
			label18.Text = "Invoice No:";
			ultraFormattedLinkLabel2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			appearance97.FontData.BoldAsString = "False";
			appearance97.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance97;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(682, 642);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(22, 15);
			ultraFormattedLinkLabel2.TabIndex = 178;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "No:";
			appearance98.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance98;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			groupBox1.Location = new System.Drawing.Point(592, 523);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(221, 114);
			groupBox1.TabIndex = 179;
			groupBox1.TabStop = false;
			textBoxTotalCharges.AllowDecimal = true;
			textBoxTotalCharges.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalCharges.CustomReportFieldName = "";
			textBoxTotalCharges.CustomReportKey = "";
			textBoxTotalCharges.CustomReportValueType = 1;
			textBoxTotalCharges.IsComboTextBox = false;
			textBoxTotalCharges.IsModified = false;
			textBoxTotalCharges.Location = new System.Drawing.Point(678, 609);
			textBoxTotalCharges.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalCharges.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalCharges.Name = "textBoxTotalCharges";
			textBoxTotalCharges.NullText = "0";
			textBoxTotalCharges.Size = new System.Drawing.Size(127, 20);
			textBoxTotalCharges.TabIndex = 173;
			textBoxTotalCharges.Text = "0.00";
			textBoxTotalCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalCharges.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTravelTotal.AllowDecimal = true;
			textBoxTravelTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTravelTotal.CustomReportFieldName = "";
			textBoxTravelTotal.CustomReportKey = "";
			textBoxTravelTotal.CustomReportValueType = 1;
			textBoxTravelTotal.IsComboTextBox = false;
			textBoxTravelTotal.IsModified = false;
			textBoxTravelTotal.Location = new System.Drawing.Point(678, 584);
			textBoxTravelTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTravelTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTravelTotal.Name = "textBoxTravelTotal";
			textBoxTravelTotal.NullText = "0";
			textBoxTravelTotal.Size = new System.Drawing.Size(127, 20);
			textBoxTravelTotal.TabIndex = 2;
			textBoxTravelTotal.Text = "0.00";
			textBoxTravelTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTravelTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTravelTotal.TextChanged += new System.EventHandler(textBoxTravelTotal_TextChanged);
			textBoxLabourTotal.AllowDecimal = true;
			textBoxLabourTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxLabourTotal.CustomReportFieldName = "";
			textBoxLabourTotal.CustomReportKey = "";
			textBoxLabourTotal.CustomReportValueType = 1;
			textBoxLabourTotal.IsComboTextBox = false;
			textBoxLabourTotal.IsModified = false;
			textBoxLabourTotal.Location = new System.Drawing.Point(678, 560);
			textBoxLabourTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLabourTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLabourTotal.Name = "textBoxLabourTotal";
			textBoxLabourTotal.NullText = "0";
			textBoxLabourTotal.Size = new System.Drawing.Size(127, 20);
			textBoxLabourTotal.TabIndex = 1;
			textBoxLabourTotal.Text = "0.00";
			textBoxLabourTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLabourTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxLabourTotal.TextChanged += new System.EventHandler(textBoxLabourTotal_TextChanged);
			textBoxPartsTotal.AllowDecimal = true;
			textBoxPartsTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxPartsTotal.CustomReportFieldName = "";
			textBoxPartsTotal.CustomReportKey = "";
			textBoxPartsTotal.CustomReportValueType = 1;
			textBoxPartsTotal.IsComboTextBox = false;
			textBoxPartsTotal.IsModified = false;
			textBoxPartsTotal.Location = new System.Drawing.Point(678, 536);
			textBoxPartsTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPartsTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPartsTotal.Name = "textBoxPartsTotal";
			textBoxPartsTotal.NullText = "0";
			textBoxPartsTotal.Size = new System.Drawing.Size(127, 20);
			textBoxPartsTotal.TabIndex = 0;
			textBoxPartsTotal.Text = "0.00";
			textBoxPartsTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPartsTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxPartsTotal.TextChanged += new System.EventHandler(textBoxPartsTotal_TextChanged);
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(820, 714);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(label18);
			base.Controls.Add(label17);
			base.Controls.Add(label16);
			base.Controls.Add(label15);
			base.Controls.Add(label14);
			base.Controls.Add(textBoxTotalCharges);
			base.Controls.Add(textBoxTravelTotal);
			base.Controls.Add(textBoxLabourTotal);
			base.Controls.Add(textBoxPartsTotal);
			base.Controls.Add(panel1);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(groupBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "ServiceCallTrackForm";
			Text = "Service Call Track";
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxclientAsset).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxproduct).EndInit();
			((System.ComponentModel.ISupportInitialize)datapartsGrid).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownLabourMins).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownLabourHours).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTravelMins).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTravelHours).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
