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
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.EnterpriseAsset
{
	public class RequisitionDetailsForm : Form, IForm
	{
		private RequisitionData currentData;

		private const string TABLENAME_CONST = "EA_Requisition";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

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

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel8;

		private DateTimePicker dateTimePickerDate;

		private UltraFormattedLinkLabel linkLabelCountry;

		private UltraFormattedLinkLabel linkLabelVendorClass;

		private MMSDateTimePicker datetimePickerRequiredOn;

		private MMLabel mmLabel5;

		private MMSDateTimePicker dateTimePickerRequiredTill;

		private MMLabel mmLabel3;

		private MMLabel mmLabel39;

		private MMTextBox textBoxRemarks;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private RequisitionTypeComboBox comboBoxRequisitionType;

		private EAEquipmentComboBox comboBoxEquipment;

		private EquipmentCategoryComboBox comboBoxEquipmentCategory;

		private JobComboBox comboBoxJob;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButton1;

		private WorkLocationComboBox comboBoxWorkLocation;

		private TextBox textBoxJobName;

		private TextBox textBoxLocationName;

		private TextBox textBoxEquipmentName;

		private GroupBox groupBox1;

		private TextBox textBoxCategoryName;

		private MMTextBox textBoxRegistrationNo;

		private MMLabel mmLabel6;

		private EquipmentTypeComboBox comboBoxEquipmentType;

		private vendorsFlatComboBox comboBoxVendor;

		private MMTextBox textBoxModel;

		private MMLabel mmLabel1;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCapacity;

		private TextBox textBoxProviderName;

		private TextBox textBoxTypeName;

		private MMLabel mmLabel31;

		private MMTextBox textBoxPlateH;

		private MMLabel mmLabel22;

		private MMTextBox textBoxSerailH;

		private MMLabel mmLabel10;

		private MMLabel mmLabel25;

		private MMLabel mmLabel27;

		private MMTextBox textBoxPower;

		private MMTextBox textBoxOwnedBy;

		private MMLabel mmLabel12;

		private MMLabel mmLabel4;

		private MMLabel mmLabel2;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonPreview;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxYear;

		private MMTextBox textBoxCapacityType;

		private MMTextBox textBoxFuel;

		private ComboBox comboBoxFuel;

		private ComboBox comboBoxCapacity;

		public ScreenAreas ScreenArea => ScreenAreas.EnterpriseAsset;

		public int ScreenID => 4008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		private string SystemDocID => comboBoxSysDoc.SelectedID;

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
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxVoucherNumber.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				bool enabled = toolStripButtonPreview.Enabled = !isNewRecord;
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

		public RequisitionDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ProductCategoryDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new RequisitionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.RequisitionTable.Rows[0] : currentData.RequisitionTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				if (comboBoxWorkLocation.SelectedID != "")
				{
					dataRow["LocationID"] = comboBoxWorkLocation.SelectedID;
				}
				else
				{
					dataRow["LocationID"] = DBNull.Value;
				}
				if (comboBoxJob.SelectedID != "")
				{
					dataRow["JobID"] = comboBoxJob.SelectedID;
				}
				else
				{
					dataRow["JobID"] = DBNull.Value;
				}
				if (comboBoxRequisitionType.SelectedID != "")
				{
					dataRow["RequisitionTypeID"] = comboBoxRequisitionType.SelectedID;
				}
				else
				{
					dataRow["RequisitionTypeID"] = DBNull.Value;
				}
				if (comboBoxEquipmentCategory.SelectedID != "")
				{
					dataRow["EquipmentCategoryID"] = comboBoxEquipmentCategory.SelectedID;
				}
				else
				{
					dataRow["EquipmentCategoryID"] = DBNull.Value;
				}
				if (comboBoxEquipment.SelectedID != "")
				{
					dataRow["EquipmentID"] = comboBoxEquipment.SelectedID;
				}
				else
				{
					dataRow["EquipmentID"] = DBNull.Value;
				}
				if (datetimePickerRequiredOn.Checked)
				{
					dataRow["RequiredOn"] = datetimePickerRequiredOn.Value;
				}
				else
				{
					dataRow["RequiredOn"] = DBNull.Value;
				}
				if (dateTimePickerRequiredTill.Checked)
				{
					dataRow["RequiredTill"] = dateTimePickerRequiredTill.Value;
				}
				else
				{
					dataRow["RequiredTill"] = DBNull.Value;
				}
				dataRow["Remarks"] = textBoxRemarks.Text;
				dataRow["Status"] = 1;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.RequisitionTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxVoucherNumber.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.RequisitionSystem.GetRequisitionByID(SystemDocID, id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxVoucherNumber.Text = id;
						textBoxVoucherNumber.Focus();
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
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					if (dataRow["RequisitionTypeID"] != DBNull.Value)
					{
						comboBoxRequisitionType.SelectedID = dataRow["RequisitionTypeID"].ToString();
					}
					if (dataRow["JobID"] != DBNull.Value)
					{
						comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					}
					if (dataRow["LocationID"] != DBNull.Value)
					{
						comboBoxWorkLocation.SelectedID = dataRow["LocationID"].ToString();
					}
					if (dataRow["EquipmentCategoryID"] != DBNull.Value)
					{
						comboBoxEquipmentCategory.SelectedID = dataRow["EquipmentCategoryID"].ToString();
					}
					if (dataRow["EquipmentID"] != DBNull.Value)
					{
						comboBoxEquipment.SelectedID = dataRow["EquipmentID"].ToString();
					}
					if (dataRow["RequiredOn"] != DBNull.Value)
					{
						datetimePickerRequiredOn.Value = DateTime.Parse(dataRow["RequiredOn"].ToString());
						datetimePickerRequiredOn.Checked = true;
					}
					else
					{
						datetimePickerRequiredOn.Checked = false;
					}
					if (dataRow["RequiredTill"] != DBNull.Value)
					{
						dateTimePickerRequiredTill.Value = DateTime.Parse(dataRow["RequiredTill"].ToString());
						dateTimePickerRequiredTill.Checked = true;
					}
					else
					{
						dateTimePickerRequiredTill.Checked = false;
					}
					textBoxRemarks.Text = dataRow["Remarks"].ToString();
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
				bool flag = Factory.RequisitionSystem.CreateRequisition(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("EA_Requisition", "VoucherID", textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("EA_Requisition", "VoucherID", textBoxVoucherNumber.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
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
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxJob.Clear();
				comboBoxWorkLocation.Clear();
				comboBoxEquipmentCategory.Clear();
				comboBoxEquipment.Clear();
				comboBoxRequisitionType.Clear();
				datetimePickerRequiredOn.Checked = false;
				dateTimePickerRequiredTill.Checked = false;
				textBoxRemarks.Clear();
				comboBoxEquipmentType.Clear();
				comboBoxVendor.Clear();
				textBoxRegistrationNo.Clear();
				textBoxModel.Clear();
				textBoxCapacity.Clear();
				textBoxFuel.Clear();
				textBoxYear.Clear();
				textBoxPower.Clear();
				textBoxRegistrationNo.Clear();
				textBoxSerailH.Clear();
				textBoxPlateH.Clear();
				textBoxOwnedBy.Clear();
				textBoxCapacityType.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				formManager.ResetDirty();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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

		private void ProductCategoryGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductCategoryGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.RequisitionSystem.DeleteRequisition(SystemDocID, textBoxVoucherNumber.Text);
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
			LoadData(DatabaseHelper.GetNextID("EA_Requisition", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("EA_Requisition", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("EA_Requisition", "VoucherID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("EA_Requisition", "VoucherID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("EA_Requisition", "VoucherID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
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

		private void ProductCategoryDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				comboBoxSysDoc.FilterByType(SysDocTypes.Requisition);
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

		public void EditRequisition(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
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

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.RequisitionListFormObj);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void textBoxVoucherNumber_TextChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.Requisition);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRequisitionType(comboBoxRequisitionType.SelectedID);
		}

		private void linkLabelVendorClass_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditWorkLocation(comboBoxWorkLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEAEquipment(comboBoxEquipment.SelectedID);
		}

		private void comboBoxEquipment_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEquipmentCategory(comboBoxEquipmentCategory.SelectedID);
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
					DataSet requisitionToPrint = Factory.RequisitionSystem.GetRequisitionToPrint(selectedID, text);
					if (requisitionToPrint == null || requisitionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(requisitionToPrint, selectedID, "Requisition", SysDocTypes.Requisition, isPrint, showPrintDialog);
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

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void comboBoxEquipment_SelectedIndexChanged(object sender, EventArgs e)
		{
			checked
			{
				if (comboBoxEquipment.SelectedID != "")
				{
					DataRow dataRow = Factory.EAEquipmentSystem.GetEquipmentByID(comboBoxEquipment.SelectedID).Tables[0].Rows[0];
					textBoxRegistrationNo.Text = dataRow["RegistrationNumber"].ToString();
					textBoxModel.Text = dataRow["Model"].ToString();
					if (dataRow["EquipmentTypeID"].ToString() != "" && dataRow["EquipmentTypeID"] != DBNull.Value)
					{
						comboBoxEquipmentType.SelectedID = dataRow["EquipmentTypeID"].ToString();
					}
					if (dataRow["VendorID"].ToString() != "" && dataRow["VendorID"] != DBNull.Value)
					{
						comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
					}
					if (dataRow["Fuel"].ToString() != "" && dataRow["Fuel"] != DBNull.Value)
					{
						comboBoxFuel.SelectedIndex = int.Parse(dataRow["Fuel"].ToString()) - 1;
						textBoxFuel.Text = comboBoxFuel.Text;
					}
					if (dataRow["Year"].ToString() != "" && dataRow["Year"] != DBNull.Value)
					{
						textBoxYear.Text = dataRow["Year"].ToString();
					}
					if (dataRow["CapacityType"].ToString() != "" && dataRow["CapacityType"] != DBNull.Value)
					{
						comboBoxCapacity.SelectedIndex = int.Parse(dataRow["CapacityType"].ToString()) - 1;
						textBoxCapacityType.Text = comboBoxCapacity.Text;
					}
					textBoxPower.Text = dataRow["Power"].ToString();
					textBoxCapacity.Text = dataRow["Capacity"].ToString();
					textBoxSerailH.Text = dataRow["SerialNo"].ToString();
					textBoxPlateH.Text = dataRow["PlateNo"].ToString();
					if (dataRow["OwnedBy"] != DBNull.Value && dataRow["OwnedBy"].ToString() != "")
					{
						comboBoxEmployee.SelectedID = dataRow["OwnedBy"].ToString();
					}
				}
			}
		}

		private void comboBoxEquipmentCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxEquipmentCategory.SelectedID != "")
			{
				comboBoxEquipment.Clear();
				comboBoxEquipment.Filter(comboBoxEquipmentCategory.SelectedID);
			}
			else
			{
				comboBoxEquipment.Filter("");
			}
		}

		private void mmLabel8_Click(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.EnterpriseAsset.RequisitionDetailsForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelVendorClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxCategoryName = new System.Windows.Forms.TextBox();
			textBoxEquipmentName = new System.Windows.Forms.TextBox();
			textBoxJobName = new System.Windows.Forms.TextBox();
			textBoxLocationName = new System.Windows.Forms.TextBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			textBoxYear = new Micromind.UISupport.MMTextBox();
			textBoxCapacityType = new Micromind.UISupport.MMTextBox();
			textBoxFuel = new Micromind.UISupport.MMTextBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxOwnedBy = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			textBoxPower = new Micromind.UISupport.MMTextBox();
			textBoxProviderName = new System.Windows.Forms.TextBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			textBoxTypeName = new System.Windows.Forms.TextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			textBoxPlateH = new Micromind.UISupport.MMTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxSerailH = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCapacity = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxModel = new Micromind.UISupport.MMTextBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxEquipmentType = new Micromind.DataControls.EquipmentTypeComboBox();
			textBoxRegistrationNo = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			comboBoxEquipmentCategory = new Micromind.DataControls.EquipmentCategoryComboBox();
			comboBoxEquipment = new Micromind.DataControls.EAEquipmentComboBox();
			comboBoxFuel = new System.Windows.Forms.ComboBox();
			comboBoxCapacity = new System.Windows.Forms.ComboBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			dateTimePickerRequiredTill = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			datetimePickerRequiredOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel5 = new Micromind.UISupport.MMLabel();
			comboBoxWorkLocation = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxRequisitionType = new Micromind.DataControls.RequisitionTypeComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxWorkLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRequisitionType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripButtonPreview,
				toolStripButton1,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(654, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(86, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "&Print";
			toolStripButton1.ToolTipText = "Print (Ctrl+P)";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 458);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(654, 40);
			panelButtons.TabIndex = 12;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(654, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(543, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(15, 33);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 28;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(193, 35);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel2.TabIndex = 26;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(300, 35);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(129, 20);
			textBoxVoucherNumber.TabIndex = 2;
			textBoxVoucherNumber.TextChanged += new System.EventHandler(textBoxVoucherNumber_TextChanged);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(520, 35);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(117, 20);
			dateTimePickerDate.TabIndex = 3;
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(15, 103);
			linkLabelCountry.Margin = new System.Windows.Forms.Padding(2);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(48, 14);
			linkLabelCountry.TabIndex = 35;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Location:";
			appearance5.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance5;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			linkLabelVendorClass.AutoSize = true;
			appearance6.ForeColor = System.Drawing.Color.Blue;
			linkLabelVendorClass.LinkAppearance = appearance6;
			linkLabelVendorClass.Location = new System.Drawing.Point(15, 56);
			linkLabelVendorClass.Margin = new System.Windows.Forms.Padding(2);
			linkLabelVendorClass.Name = "linkLabelVendorClass";
			linkLabelVendorClass.Size = new System.Drawing.Size(40, 14);
			linkLabelVendorClass.TabIndex = 33;
			linkLabelVendorClass.TabStop = true;
			linkLabelVendorClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVendorClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVendorClass.Value = "Project:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			linkLabelVendorClass.VisitedLinkAppearance = appearance7;
			linkLabelVendorClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVendorClass_LinkClicked);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(448, 59);
			ultraFormattedLinkLabel1.Margin = new System.Windows.Forms.Padding(2);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(83, 14);
			ultraFormattedLinkLabel1.TabIndex = 208;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "RequisitionType:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(5, 26);
			ultraFormattedLinkLabel3.Margin = new System.Windows.Forms.Padding(2);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(49, 14);
			ultraFormattedLinkLabel3.TabIndex = 213;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Category:";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance9;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(5, 49);
			ultraFormattedLinkLabel4.Margin = new System.Windows.Forms.Padding(2);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(57, 14);
			ultraFormattedLinkLabel4.TabIndex = 214;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Equipment:";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			textBoxCategoryName.Location = new System.Drawing.Point(215, 23);
			textBoxCategoryName.MaxLength = 30;
			textBoxCategoryName.Name = "textBoxCategoryName";
			textBoxCategoryName.ReadOnly = true;
			textBoxCategoryName.Size = new System.Drawing.Size(329, 20);
			textBoxCategoryName.TabIndex = 218;
			textBoxCategoryName.TabStop = false;
			textBoxEquipmentName.Location = new System.Drawing.Point(215, 47);
			textBoxEquipmentName.MaxLength = 30;
			textBoxEquipmentName.Name = "textBoxEquipmentName";
			textBoxEquipmentName.ReadOnly = true;
			textBoxEquipmentName.Size = new System.Drawing.Size(329, 20);
			textBoxEquipmentName.TabIndex = 217;
			textBoxEquipmentName.TabStop = false;
			textBoxJobName.Location = new System.Drawing.Point(75, 77);
			textBoxJobName.MaxLength = 30;
			textBoxJobName.Name = "textBoxJobName";
			textBoxJobName.ReadOnly = true;
			textBoxJobName.Size = new System.Drawing.Size(325, 20);
			textBoxJobName.TabIndex = 215;
			textBoxJobName.TabStop = false;
			textBoxLocationName.Location = new System.Drawing.Point(193, 100);
			textBoxLocationName.MaxLength = 30;
			textBoxLocationName.Name = "textBoxLocationName";
			textBoxLocationName.ReadOnly = true;
			textBoxLocationName.Size = new System.Drawing.Size(325, 20);
			textBoxLocationName.TabIndex = 216;
			textBoxLocationName.TabStop = false;
			groupBox1.Controls.Add(textBoxYear);
			groupBox1.Controls.Add(textBoxCapacityType);
			groupBox1.Controls.Add(textBoxFuel);
			groupBox1.Controls.Add(comboBoxEmployee);
			groupBox1.Controls.Add(mmLabel4);
			groupBox1.Controls.Add(mmLabel2);
			groupBox1.Controls.Add(textBoxOwnedBy);
			groupBox1.Controls.Add(mmLabel12);
			groupBox1.Controls.Add(mmLabel27);
			groupBox1.Controls.Add(textBoxPower);
			groupBox1.Controls.Add(textBoxProviderName);
			groupBox1.Controls.Add(mmLabel25);
			groupBox1.Controls.Add(textBoxTypeName);
			groupBox1.Controls.Add(mmLabel31);
			groupBox1.Controls.Add(textBoxPlateH);
			groupBox1.Controls.Add(mmLabel22);
			groupBox1.Controls.Add(textBoxSerailH);
			groupBox1.Controls.Add(mmLabel10);
			groupBox1.Controls.Add(mmLabel13);
			groupBox1.Controls.Add(textBoxCapacity);
			groupBox1.Controls.Add(mmLabel1);
			groupBox1.Controls.Add(textBoxModel);
			groupBox1.Controls.Add(comboBoxVendor);
			groupBox1.Controls.Add(comboBoxEquipmentType);
			groupBox1.Controls.Add(textBoxRegistrationNo);
			groupBox1.Controls.Add(mmLabel6);
			groupBox1.Controls.Add(textBoxCategoryName);
			groupBox1.Controls.Add(comboBoxEquipmentCategory);
			groupBox1.Controls.Add(textBoxEquipmentName);
			groupBox1.Controls.Add(ultraFormattedLinkLabel3);
			groupBox1.Controls.Add(comboBoxEquipment);
			groupBox1.Controls.Add(ultraFormattedLinkLabel4);
			groupBox1.Controls.Add(comboBoxFuel);
			groupBox1.Controls.Add(comboBoxCapacity);
			groupBox1.Location = new System.Drawing.Point(15, 131);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(621, 227);
			groupBox1.TabIndex = 7;
			groupBox1.TabStop = false;
			groupBox1.Text = "Equipment Detail";
			textBoxYear.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxYear.CustomReportFieldName = "";
			textBoxYear.CustomReportKey = "";
			textBoxYear.CustomReportValueType = 1;
			textBoxYear.ForeColor = System.Drawing.Color.Black;
			textBoxYear.IsComboTextBox = false;
			textBoxYear.Location = new System.Drawing.Point(319, 143);
			textBoxYear.MaxLength = 15;
			textBoxYear.Name = "textBoxYear";
			textBoxYear.ReadOnly = true;
			textBoxYear.Size = new System.Drawing.Size(83, 20);
			textBoxYear.TabIndex = 248;
			textBoxYear.TabStop = false;
			textBoxCapacityType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCapacityType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCapacityType.CustomReportFieldName = "";
			textBoxCapacityType.CustomReportKey = "";
			textBoxCapacityType.CustomReportValueType = 1;
			textBoxCapacityType.ForeColor = System.Drawing.Color.Black;
			textBoxCapacityType.IsComboTextBox = false;
			textBoxCapacityType.Location = new System.Drawing.Point(168, 167);
			textBoxCapacityType.MaxLength = 15;
			textBoxCapacityType.Name = "textBoxCapacityType";
			textBoxCapacityType.ReadOnly = true;
			textBoxCapacityType.Size = new System.Drawing.Size(53, 20);
			textBoxCapacityType.TabIndex = 247;
			textBoxCapacityType.TabStop = false;
			textBoxFuel.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFuel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFuel.CustomReportFieldName = "";
			textBoxFuel.CustomReportKey = "";
			textBoxFuel.CustomReportValueType = 1;
			textBoxFuel.ForeColor = System.Drawing.Color.Black;
			textBoxFuel.IsComboTextBox = false;
			textBoxFuel.Location = new System.Drawing.Point(201, 143);
			textBoxFuel.MaxLength = 15;
			textBoxFuel.Name = "textBoxFuel";
			textBoxFuel.ReadOnly = true;
			textBoxFuel.Size = new System.Drawing.Size(72, 20);
			textBoxFuel.TabIndex = 246;
			textBoxFuel.TabStop = false;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxOwnedBy;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance11;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance12;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance13;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance14.BackColor2 = System.Drawing.SystemColors.Control;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance14.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance14;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance15;
			appearance16.BackColor = System.Drawing.SystemColors.Highlight;
			appearance16.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance16;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance17;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			appearance18.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance18;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance19.BackColor = System.Drawing.SystemColors.Control;
			appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance19.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance19;
			appearance20.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance20;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance21;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance22;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(325, 191);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(139, 20);
			comboBoxEmployee.TabIndex = 245;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.Visible = false;
			textBoxOwnedBy.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxOwnedBy.CustomReportFieldName = "";
			textBoxOwnedBy.CustomReportKey = "";
			textBoxOwnedBy.CustomReportValueType = 1;
			textBoxOwnedBy.IsComboTextBox = false;
			textBoxOwnedBy.Location = new System.Drawing.Point(98, 191);
			textBoxOwnedBy.MaxLength = 30;
			textBoxOwnedBy.Name = "textBoxOwnedBy";
			textBoxOwnedBy.ReadOnly = true;
			textBoxOwnedBy.Size = new System.Drawing.Size(221, 20);
			textBoxOwnedBy.TabIndex = 15;
			textBoxOwnedBy.TabStop = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(5, 95);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(51, 13);
			mmLabel4.TabIndex = 244;
			mmLabel4.Text = "Provider:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(5, 71);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(35, 13);
			mmLabel2.TabIndex = 243;
			mmLabel2.Text = "Type:";
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(5, 195);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(60, 13);
			mmLabel12.TabIndex = 242;
			mmLabel12.Text = "Owned By:";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(411, 145);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(41, 13);
			mmLabel27.TabIndex = 241;
			mmLabel27.Text = "Power:";
			textBoxPower.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPower.CustomReportFieldName = "";
			textBoxPower.CustomReportKey = "";
			textBoxPower.CustomReportValueType = 1;
			textBoxPower.IsComboTextBox = false;
			textBoxPower.Location = new System.Drawing.Point(470, 143);
			textBoxPower.MaxLength = 64;
			textBoxPower.Name = "textBoxPower";
			textBoxPower.ReadOnly = true;
			textBoxPower.Size = new System.Drawing.Size(110, 20);
			textBoxPower.TabIndex = 8;
			textBoxPower.TabStop = false;
			textBoxProviderName.Location = new System.Drawing.Point(98, 95);
			textBoxProviderName.MaxLength = 30;
			textBoxProviderName.Name = "textBoxProviderName";
			textBoxProviderName.ReadOnly = true;
			textBoxProviderName.Size = new System.Drawing.Size(328, 20);
			textBoxProviderName.TabIndex = 237;
			textBoxProviderName.TabStop = false;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(288, 145);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(33, 13);
			mmLabel25.TabIndex = 238;
			mmLabel25.Text = "Year:";
			textBoxTypeName.Location = new System.Drawing.Point(98, 71);
			textBoxTypeName.MaxLength = 30;
			textBoxTypeName.Name = "textBoxTypeName";
			textBoxTypeName.ReadOnly = true;
			textBoxTypeName.Size = new System.Drawing.Size(329, 20);
			textBoxTypeName.TabIndex = 236;
			textBoxTypeName.TabStop = false;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(411, 170);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(53, 13);
			mmLabel31.TabIndex = 13;
			mmLabel31.Text = "Plate NO:";
			textBoxPlateH.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPlateH.CustomReportFieldName = "";
			textBoxPlateH.CustomReportKey = "";
			textBoxPlateH.CustomReportValueType = 1;
			textBoxPlateH.IsComboTextBox = false;
			textBoxPlateH.Location = new System.Drawing.Point(470, 167);
			textBoxPlateH.MaxLength = 30;
			textBoxPlateH.Name = "textBoxPlateH";
			textBoxPlateH.ReadOnly = true;
			textBoxPlateH.Size = new System.Drawing.Size(110, 20);
			textBoxPlateH.TabIndex = 14;
			textBoxPlateH.TabStop = false;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(225, 172);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(55, 13);
			mmLabel22.TabIndex = 11;
			mmLabel22.Text = "Serial NO:";
			textBoxSerailH.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSerailH.CustomReportFieldName = "";
			textBoxSerailH.CustomReportKey = "";
			textBoxSerailH.CustomReportValueType = 1;
			textBoxSerailH.IsComboTextBox = false;
			textBoxSerailH.Location = new System.Drawing.Point(286, 167);
			textBoxSerailH.MaxLength = 30;
			textBoxSerailH.Name = "textBoxSerailH";
			textBoxSerailH.ReadOnly = true;
			textBoxSerailH.Size = new System.Drawing.Size(116, 20);
			textBoxSerailH.TabIndex = 12;
			textBoxSerailH.TabStop = false;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(173, 146);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(31, 13);
			mmLabel10.TabIndex = 230;
			mmLabel10.Text = "Fuel:";
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(5, 165);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(53, 13);
			mmLabel13.TabIndex = 229;
			mmLabel13.Text = "Capacity:";
			textBoxCapacity.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCapacity.CustomReportFieldName = "";
			textBoxCapacity.CustomReportKey = "";
			textBoxCapacity.CustomReportValueType = 1;
			textBoxCapacity.IsComboTextBox = false;
			textBoxCapacity.Location = new System.Drawing.Point(98, 167);
			textBoxCapacity.MaxLength = 64;
			textBoxCapacity.Name = "textBoxCapacity";
			textBoxCapacity.ReadOnly = true;
			textBoxCapacity.Size = new System.Drawing.Size(68, 20);
			textBoxCapacity.TabIndex = 9;
			textBoxCapacity.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(5, 142);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(39, 13);
			mmLabel1.TabIndex = 226;
			mmLabel1.Text = "Model:";
			textBoxModel.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxModel.CustomReportFieldName = "";
			textBoxModel.CustomReportKey = "";
			textBoxModel.CustomReportValueType = 1;
			textBoxModel.ForeColor = System.Drawing.Color.Black;
			textBoxModel.IsComboTextBox = false;
			textBoxModel.Location = new System.Drawing.Point(98, 143);
			textBoxModel.MaxLength = 15;
			textBoxModel.Name = "textBoxModel";
			textBoxModel.ReadOnly = true;
			textBoxModel.Size = new System.Drawing.Size(66, 20);
			textBoxModel.TabIndex = 5;
			textBoxModel.TabStop = false;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxProviderName;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance23;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance24;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance25;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance26.BackColor2 = System.Drawing.SystemColors.Control;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance26.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance26;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance27;
			appearance28.BackColor = System.Drawing.SystemColors.Highlight;
			appearance28.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance28;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance29;
			appearance30.BorderColor = System.Drawing.Color.Silver;
			appearance30.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance30;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance31.BackColor = System.Drawing.SystemColors.Control;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance31;
			appearance32.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance32;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance33;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance34;
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
			comboBoxVendor.Location = new System.Drawing.Point(429, 94);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.MaxLength = 64;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ReadOnly = true;
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(115, 20);
			comboBoxVendor.TabIndex = 3;
			comboBoxVendor.TabStop = false;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendor.Visible = false;
			comboBoxEquipmentType.Assigned = false;
			comboBoxEquipmentType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipmentType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipmentType.CustomReportFieldName = "";
			comboBoxEquipmentType.CustomReportKey = "";
			comboBoxEquipmentType.CustomReportValueType = 1;
			comboBoxEquipmentType.DescriptionTextBox = textBoxTypeName;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipmentType.DisplayLayout.Appearance = appearance35;
			comboBoxEquipmentType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipmentType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.Appearance = appearance36;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance37;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance38.BackColor2 = System.Drawing.SystemColors.Control;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.PromptAppearance = appearance38;
			comboBoxEquipmentType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipmentType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipmentType.DisplayLayout.Override.ActiveCellAppearance = appearance39;
			appearance40.BackColor = System.Drawing.SystemColors.Highlight;
			appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipmentType.DisplayLayout.Override.ActiveRowAppearance = appearance40;
			comboBoxEquipmentType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipmentType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.Override.CardAreaAppearance = appearance41;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			appearance42.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipmentType.DisplayLayout.Override.CellAppearance = appearance42;
			comboBoxEquipmentType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipmentType.DisplayLayout.Override.CellPadding = 0;
			appearance43.BackColor = System.Drawing.SystemColors.Control;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.Override.GroupByRowAppearance = appearance43;
			appearance44.TextHAlignAsString = "Left";
			comboBoxEquipmentType.DisplayLayout.Override.HeaderAppearance = appearance44;
			comboBoxEquipmentType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipmentType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipmentType.DisplayLayout.Override.RowAppearance = appearance45;
			comboBoxEquipmentType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipmentType.DisplayLayout.Override.TemplateAddRowAppearance = appearance46;
			comboBoxEquipmentType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipmentType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipmentType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipmentType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipmentType.Editable = true;
			comboBoxEquipmentType.FilterString = "";
			comboBoxEquipmentType.HasAllAccount = false;
			comboBoxEquipmentType.HasCustom = false;
			comboBoxEquipmentType.IsDataLoaded = false;
			comboBoxEquipmentType.Location = new System.Drawing.Point(429, 71);
			comboBoxEquipmentType.MaxDropDownItems = 12;
			comboBoxEquipmentType.Name = "comboBoxEquipmentType";
			comboBoxEquipmentType.ReadOnly = true;
			comboBoxEquipmentType.ShowInactiveItems = false;
			comboBoxEquipmentType.ShowQuickAdd = true;
			comboBoxEquipmentType.Size = new System.Drawing.Size(115, 20);
			comboBoxEquipmentType.TabIndex = 2;
			comboBoxEquipmentType.TabStop = false;
			comboBoxEquipmentType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipmentType.Visible = false;
			textBoxRegistrationNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRegistrationNo.CustomReportFieldName = "";
			textBoxRegistrationNo.CustomReportKey = "";
			textBoxRegistrationNo.CustomReportValueType = 1;
			textBoxRegistrationNo.IsComboTextBox = false;
			textBoxRegistrationNo.IsRequired = true;
			textBoxRegistrationNo.Location = new System.Drawing.Point(98, 119);
			textBoxRegistrationNo.MaxLength = 64;
			textBoxRegistrationNo.Name = "textBoxRegistrationNo";
			textBoxRegistrationNo.ReadOnly = true;
			textBoxRegistrationNo.Size = new System.Drawing.Size(226, 20);
			textBoxRegistrationNo.TabIndex = 4;
			textBoxRegistrationNo.TabStop = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(5, 119);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(87, 13);
			mmLabel6.TabIndex = 219;
			mmLabel6.Text = "Registration NO:";
			comboBoxEquipmentCategory.Assigned = false;
			comboBoxEquipmentCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipmentCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipmentCategory.CustomReportFieldName = "";
			comboBoxEquipmentCategory.CustomReportKey = "";
			comboBoxEquipmentCategory.CustomReportValueType = 1;
			comboBoxEquipmentCategory.DescriptionTextBox = textBoxCategoryName;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipmentCategory.DisplayLayout.Appearance = appearance47;
			comboBoxEquipmentCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipmentCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.Appearance = appearance48;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance49;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance50.BackColor2 = System.Drawing.SystemColors.Control;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance50;
			comboBoxEquipmentCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipmentCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipmentCategory.DisplayLayout.Override.ActiveCellAppearance = appearance51;
			appearance52.BackColor = System.Drawing.SystemColors.Highlight;
			appearance52.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipmentCategory.DisplayLayout.Override.ActiveRowAppearance = appearance52;
			comboBoxEquipmentCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipmentCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentCategory.DisplayLayout.Override.CardAreaAppearance = appearance53;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			appearance54.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipmentCategory.DisplayLayout.Override.CellAppearance = appearance54;
			comboBoxEquipmentCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipmentCategory.DisplayLayout.Override.CellPadding = 0;
			appearance55.BackColor = System.Drawing.SystemColors.Control;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentCategory.DisplayLayout.Override.GroupByRowAppearance = appearance55;
			appearance56.TextHAlignAsString = "Left";
			comboBoxEquipmentCategory.DisplayLayout.Override.HeaderAppearance = appearance56;
			comboBoxEquipmentCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipmentCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipmentCategory.DisplayLayout.Override.RowAppearance = appearance57;
			comboBoxEquipmentCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipmentCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance58;
			comboBoxEquipmentCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipmentCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipmentCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipmentCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipmentCategory.Editable = true;
			comboBoxEquipmentCategory.FilterString = "";
			comboBoxEquipmentCategory.HasAllAccount = false;
			comboBoxEquipmentCategory.HasCustom = false;
			comboBoxEquipmentCategory.IsDataLoaded = false;
			comboBoxEquipmentCategory.Location = new System.Drawing.Point(98, 23);
			comboBoxEquipmentCategory.MaxDropDownItems = 12;
			comboBoxEquipmentCategory.Name = "comboBoxEquipmentCategory";
			comboBoxEquipmentCategory.ShowAll = false;
			comboBoxEquipmentCategory.ShowConsignIn = false;
			comboBoxEquipmentCategory.ShowConsignOut = false;
			comboBoxEquipmentCategory.ShowInactiveItems = false;
			comboBoxEquipmentCategory.ShowNormalLocations = true;
			comboBoxEquipmentCategory.ShowPOSOnly = false;
			comboBoxEquipmentCategory.ShowQuickAdd = true;
			comboBoxEquipmentCategory.ShowWarehouseOnly = false;
			comboBoxEquipmentCategory.Size = new System.Drawing.Size(115, 20);
			comboBoxEquipmentCategory.TabIndex = 0;
			comboBoxEquipmentCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipmentCategory.SelectedIndexChanged += new System.EventHandler(comboBoxEquipmentCategory_SelectedIndexChanged);
			comboBoxEquipment.Assigned = false;
			comboBoxEquipment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipment.CustomReportFieldName = "";
			comboBoxEquipment.CustomReportKey = "";
			comboBoxEquipment.CustomReportValueType = 1;
			comboBoxEquipment.DescriptionTextBox = textBoxEquipmentName;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipment.DisplayLayout.Appearance = appearance59;
			comboBoxEquipment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			comboBoxEquipment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipment.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			comboBoxEquipment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipment.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipment.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			comboBoxEquipment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipment.DisplayLayout.Override.CellAppearance = appearance66;
			comboBoxEquipment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipment.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			comboBoxEquipment.DisplayLayout.Override.HeaderAppearance = appearance68;
			comboBoxEquipment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipment.DisplayLayout.Override.RowAppearance = appearance69;
			comboBoxEquipment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipment.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			comboBoxEquipment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipment.Editable = true;
			comboBoxEquipment.FilterString = "";
			comboBoxEquipment.HasAllAccount = false;
			comboBoxEquipment.HasCustom = false;
			comboBoxEquipment.IsDataLoaded = false;
			comboBoxEquipment.Location = new System.Drawing.Point(98, 47);
			comboBoxEquipment.MaxDropDownItems = 12;
			comboBoxEquipment.Name = "comboBoxEquipment";
			comboBoxEquipment.ShowInactiveItems = false;
			comboBoxEquipment.ShowQuickAdd = true;
			comboBoxEquipment.Size = new System.Drawing.Size(115, 20);
			comboBoxEquipment.TabIndex = 1;
			comboBoxEquipment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipment.SelectedIndexChanged += new System.EventHandler(comboBoxEquipment_SelectedIndexChanged);
			comboBoxEquipment.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxEquipment_InitializeLayout);
			comboBoxFuel.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			comboBoxFuel.Enabled = false;
			comboBoxFuel.FormattingEnabled = true;
			comboBoxFuel.Items.AddRange(new object[2]
			{
				"Petrol",
				"Diesel"
			});
			comboBoxFuel.Location = new System.Drawing.Point(201, 145);
			comboBoxFuel.Name = "comboBoxFuel";
			comboBoxFuel.Size = new System.Drawing.Size(72, 21);
			comboBoxFuel.TabIndex = 294;
			comboBoxFuel.TabStop = false;
			comboBoxFuel.Visible = false;
			comboBoxCapacity.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCapacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			comboBoxCapacity.Enabled = false;
			comboBoxCapacity.FormattingEnabled = true;
			comboBoxCapacity.Items.AddRange(new object[2]
			{
				"Seat",
				"Ton"
			});
			comboBoxCapacity.Location = new System.Drawing.Point(168, 166);
			comboBoxCapacity.Name = "comboBoxCapacity";
			comboBoxCapacity.Size = new System.Drawing.Size(53, 21);
			comboBoxCapacity.TabIndex = 296;
			comboBoxCapacity.TabStop = false;
			comboBoxCapacity.Visible = false;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(15, 390);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(52, 13);
			mmLabel39.TabIndex = 207;
			mmLabel39.Text = "Remarks:";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.Location = new System.Drawing.Point(116, 390);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(424, 58);
			textBoxRemarks.TabIndex = 10;
			dateTimePickerRequiredTill.Checked = false;
			dateTimePickerRequiredTill.CustomFormat = " ";
			dateTimePickerRequiredTill.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerRequiredTill.Location = new System.Drawing.Point(321, 366);
			dateTimePickerRequiredTill.Margin = new System.Windows.Forms.Padding(2);
			dateTimePickerRequiredTill.Name = "dateTimePickerRequiredTill";
			dateTimePickerRequiredTill.ShowCheckBox = true;
			dateTimePickerRequiredTill.Size = new System.Drawing.Size(115, 20);
			dateTimePickerRequiredTill.TabIndex = 9;
			dateTimePickerRequiredTill.Value = new System.DateTime(0L);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(250, 367);
			mmLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(67, 13);
			mmLabel3.TabIndex = 155;
			mmLabel3.Text = "Required till:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = true;
			mmLabel8.Location = new System.Drawing.Point(448, 38);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(38, 13);
			mmLabel8.TabIndex = 31;
			mmLabel8.Text = "Date:";
			mmLabel8.Click += new System.EventHandler(mmLabel8_Click);
			datetimePickerRequiredOn.Checked = false;
			datetimePickerRequiredOn.CustomFormat = " ";
			datetimePickerRequiredOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerRequiredOn.Location = new System.Drawing.Point(116, 366);
			datetimePickerRequiredOn.Margin = new System.Windows.Forms.Padding(2);
			datetimePickerRequiredOn.Name = "datetimePickerRequiredOn";
			datetimePickerRequiredOn.ShowCheckBox = true;
			datetimePickerRequiredOn.Size = new System.Drawing.Size(118, 20);
			datetimePickerRequiredOn.TabIndex = 8;
			datetimePickerRequiredOn.Value = new System.DateTime(0L);
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(15, 367);
			mmLabel5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(71, 13);
			mmLabel5.TabIndex = 153;
			mmLabel5.Text = "Required On:";
			comboBoxWorkLocation.Assigned = false;
			comboBoxWorkLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxWorkLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxWorkLocation.CustomReportFieldName = "";
			comboBoxWorkLocation.CustomReportKey = "";
			comboBoxWorkLocation.CustomReportValueType = 1;
			comboBoxWorkLocation.DescriptionTextBox = textBoxLocationName;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxWorkLocation.DisplayLayout.Appearance = appearance71;
			comboBoxWorkLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxWorkLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.Appearance = appearance72;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance73;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance74.BackColor2 = System.Drawing.SystemColors.Control;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxWorkLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance74;
			comboBoxWorkLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxWorkLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxWorkLocation.DisplayLayout.Override.ActiveCellAppearance = appearance75;
			appearance76.BackColor = System.Drawing.SystemColors.Highlight;
			appearance76.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxWorkLocation.DisplayLayout.Override.ActiveRowAppearance = appearance76;
			comboBoxWorkLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxWorkLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			comboBoxWorkLocation.DisplayLayout.Override.CardAreaAppearance = appearance77;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			appearance78.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxWorkLocation.DisplayLayout.Override.CellAppearance = appearance78;
			comboBoxWorkLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxWorkLocation.DisplayLayout.Override.CellPadding = 0;
			appearance79.BackColor = System.Drawing.SystemColors.Control;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxWorkLocation.DisplayLayout.Override.GroupByRowAppearance = appearance79;
			appearance80.TextHAlignAsString = "Left";
			comboBoxWorkLocation.DisplayLayout.Override.HeaderAppearance = appearance80;
			comboBoxWorkLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxWorkLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			comboBoxWorkLocation.DisplayLayout.Override.RowAppearance = appearance81;
			comboBoxWorkLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxWorkLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance82;
			comboBoxWorkLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxWorkLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxWorkLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxWorkLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxWorkLocation.Editable = true;
			comboBoxWorkLocation.FilterString = "";
			comboBoxWorkLocation.HasAllAccount = false;
			comboBoxWorkLocation.HasCustom = false;
			comboBoxWorkLocation.IsDataLoaded = false;
			comboBoxWorkLocation.Location = new System.Drawing.Point(75, 100);
			comboBoxWorkLocation.MaxDropDownItems = 12;
			comboBoxWorkLocation.Name = "comboBoxWorkLocation";
			comboBoxWorkLocation.ShowAll = false;
			comboBoxWorkLocation.ShowConsignIn = false;
			comboBoxWorkLocation.ShowConsignOut = false;
			comboBoxWorkLocation.ShowInactiveItems = false;
			comboBoxWorkLocation.ShowNormalLocations = true;
			comboBoxWorkLocation.ShowPOSOnly = false;
			comboBoxWorkLocation.ShowQuickAdd = true;
			comboBoxWorkLocation.ShowWarehouseOnly = false;
			comboBoxWorkLocation.Size = new System.Drawing.Size(116, 20);
			comboBoxWorkLocation.TabIndex = 6;
			comboBoxWorkLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = textBoxJobName;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxJob.DisplayLayout.Appearance = appearance83;
			comboBoxJob.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxJob.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.GroupByBox.Appearance = appearance84;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJob.DisplayLayout.GroupByBox.BandLabelAppearance = appearance85;
			comboBoxJob.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance86.BackColor2 = System.Drawing.SystemColors.Control;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJob.DisplayLayout.GroupByBox.PromptAppearance = appearance86;
			comboBoxJob.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxJob.DisplayLayout.MaxRowScrollRegions = 1;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxJob.DisplayLayout.Override.ActiveCellAppearance = appearance87;
			appearance88.BackColor = System.Drawing.SystemColors.Highlight;
			appearance88.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxJob.DisplayLayout.Override.ActiveRowAppearance = appearance88;
			comboBoxJob.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxJob.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.Override.CardAreaAppearance = appearance89;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			appearance90.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxJob.DisplayLayout.Override.CellAppearance = appearance90;
			comboBoxJob.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxJob.DisplayLayout.Override.CellPadding = 0;
			appearance91.BackColor = System.Drawing.SystemColors.Control;
			appearance91.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance91.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.Override.GroupByRowAppearance = appearance91;
			appearance92.TextHAlignAsString = "Left";
			comboBoxJob.DisplayLayout.Override.HeaderAppearance = appearance92;
			comboBoxJob.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxJob.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			comboBoxJob.DisplayLayout.Override.RowAppearance = appearance93;
			comboBoxJob.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxJob.DisplayLayout.Override.TemplateAddRowAppearance = appearance94;
			comboBoxJob.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxJob.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxJob.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(75, 55);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(118, 20);
			comboBoxJob.TabIndex = 4;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxRequisitionType.Assigned = false;
			comboBoxRequisitionType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRequisitionType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRequisitionType.CustomReportFieldName = "";
			comboBoxRequisitionType.CustomReportKey = "";
			comboBoxRequisitionType.CustomReportValueType = 1;
			comboBoxRequisitionType.DescriptionTextBox = null;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRequisitionType.DisplayLayout.Appearance = appearance95;
			comboBoxRequisitionType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRequisitionType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRequisitionType.DisplayLayout.GroupByBox.Appearance = appearance96;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRequisitionType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance97;
			comboBoxRequisitionType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance98.BackColor2 = System.Drawing.SystemColors.Control;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance98.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRequisitionType.DisplayLayout.GroupByBox.PromptAppearance = appearance98;
			comboBoxRequisitionType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRequisitionType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRequisitionType.DisplayLayout.Override.ActiveCellAppearance = appearance99;
			appearance100.BackColor = System.Drawing.SystemColors.Highlight;
			appearance100.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRequisitionType.DisplayLayout.Override.ActiveRowAppearance = appearance100;
			comboBoxRequisitionType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRequisitionType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRequisitionType.DisplayLayout.Override.CardAreaAppearance = appearance101;
			appearance102.BorderColor = System.Drawing.Color.Silver;
			appearance102.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRequisitionType.DisplayLayout.Override.CellAppearance = appearance102;
			comboBoxRequisitionType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRequisitionType.DisplayLayout.Override.CellPadding = 0;
			appearance103.BackColor = System.Drawing.SystemColors.Control;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRequisitionType.DisplayLayout.Override.GroupByRowAppearance = appearance103;
			appearance104.TextHAlignAsString = "Left";
			comboBoxRequisitionType.DisplayLayout.Override.HeaderAppearance = appearance104;
			comboBoxRequisitionType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRequisitionType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			comboBoxRequisitionType.DisplayLayout.Override.RowAppearance = appearance105;
			comboBoxRequisitionType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRequisitionType.DisplayLayout.Override.TemplateAddRowAppearance = appearance106;
			comboBoxRequisitionType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRequisitionType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRequisitionType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRequisitionType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRequisitionType.Editable = true;
			comboBoxRequisitionType.FilterString = "";
			comboBoxRequisitionType.HasAllAccount = false;
			comboBoxRequisitionType.HasCustom = false;
			comboBoxRequisitionType.IsDataLoaded = false;
			comboBoxRequisitionType.Location = new System.Drawing.Point(543, 57);
			comboBoxRequisitionType.MaxDropDownItems = 12;
			comboBoxRequisitionType.Name = "comboBoxRequisitionType";
			comboBoxRequisitionType.ShowInactiveItems = false;
			comboBoxRequisitionType.ShowQuickAdd = true;
			comboBoxRequisitionType.Size = new System.Drawing.Size(94, 20);
			comboBoxRequisitionType.TabIndex = 5;
			comboBoxRequisitionType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance107;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(75, 32);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(118, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 0;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(654, 498);
			base.Controls.Add(groupBox1);
			base.Controls.Add(textBoxLocationName);
			base.Controls.Add(textBoxJobName);
			base.Controls.Add(comboBoxWorkLocation);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(comboBoxRequisitionType);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(mmLabel39);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(linkLabelCountry);
			base.Controls.Add(linkLabelVendorClass);
			base.Controls.Add(dateTimePickerRequiredTill);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(datetimePickerRequiredOn);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(591, 304);
			base.Name = "RequisitionDetailsForm";
			Text = "Requisition Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxWorkLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRequisitionType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
