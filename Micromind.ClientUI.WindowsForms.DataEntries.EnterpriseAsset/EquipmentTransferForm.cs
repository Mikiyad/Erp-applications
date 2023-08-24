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
	public class EquipmentTransferForm : Form, IForm
	{
		private bool allowEdit = true;

		private EquipmentTransferData currentData;

		private const string TABLENAME_CONST = "EA_Equipment_Transfer";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string SourceSysDocID = "";

		private string SourceVoucherID = "";

		private string reqsnSysDocID = "";

		private string reqsnVoucherID = "";

		private int sourceRowIndex;

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

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private GroupBox groupBox2;

		private GroupBox groupBox1;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private TextBox textBoxToEmployeeName;

		private TextBox textBoxTolocationName;

		private TextBox textBoxToJobName;

		private EmployeeComboBox comboBoxToEmployee;

		private JobComboBox comboBoxToJob;

		private TextBox textBoxEmployeeName;

		private TextBox textBoxLocationName;

		private TextBox textBoxJobName;

		private EmployeeComboBox comboBoxFromEmployee;

		private JobComboBox comboBoxFromJob;

		private MMLabel mmLabel5;

		private MMLabel mmLabel4;

		private MMLabel mmLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private Label labelVoided;

		private WorkLocationComboBox comboBoxToLocation;

		private WorkLocationComboBox comboBoxFromLocation;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private Label label1;

		private TextBox textBoxRef1;

		private MMTextBox textBoxCurrentMeterReading;

		private MMLabel mmLabel2;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxOwnedBy;

		private MMLabel mmLabel6;

		private MMLabel mmLabel7;

		private MMLabel mmLabel12;

		private MMLabel mmLabel27;

		private MMTextBox textBoxPower;

		private TextBox textBoxProviderName;

		private MMLabel mmLabel25;

		private TextBox textBoxTypeName;

		private MMLabel mmLabel31;

		private MMTextBox textBoxPlateH;

		private ComboBox comboBoxFuel;

		private MMLabel mmLabel22;

		private MMTextBox textBoxSerailH;

		private MMLabel mmLabel10;

		private ComboBox comboBoxCapacity;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCapacity;

		private MMLabel mmLabel8;

		private MMTextBox textBoxModel;

		private vendorsFlatComboBox comboBoxVendor;

		private EquipmentTypeComboBox comboBoxEquipmentType;

		private MMTextBox textBoxRegistrationNo;

		private MMLabel mmLabel9;

		private TextBox textBoxEquipmentName;

		private EAEquipmentComboBox comboBoxEquipment;

		private MMTextBox textBoxFuel;

		private MMTextBox textBoxYear;

		private MMTextBox textBoxCapacityType;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.EnterpriseAsset;

		public int ScreenID => 4004;

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
					textBoxVoucherNumber.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.Enabled = false;
				}
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
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				panelDetails.Enabled = !value;
				buttonSave.Enabled = !value;
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

		public EquipmentTransferForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void comboBoxFixedAsset_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridAsset_VisibleChanged(object sender, EventArgs e)
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

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void comboBoxGridAsset_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EquipmentTransferData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EquipmentTransferTable.Rows[0] : currentData.EquipmentTransferTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["JobFromID"] = comboBoxFromJob.SelectedID;
				dataRow["JobToID"] = comboBoxToJob.SelectedID;
				dataRow["LocationFromID"] = comboBoxFromLocation.SelectedID;
				dataRow["LocationToID"] = comboBoxToLocation.SelectedID;
				dataRow["EmployeeFromID"] = comboBoxFromEmployee.SelectedID;
				dataRow["EmployeeToID"] = comboBoxToEmployee.SelectedID;
				dataRow["CurrentMeterReading"] = textBoxCurrentMeterReading.Text;
				dataRow["EquipmentID"] = comboBoxEquipment.SelectedID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				if (SourceVoucherID != "" && SourceVoucherID != null)
				{
					dataRow["SourceSysDocID"] = SourceSysDocID;
					dataRow["SourceVoucherID"] = SourceVoucherID;
					dataRow["SourceRowIndex"] = sourceRowIndex;
					dataRow["ReqSysDocID"] = reqsnSysDocID;
					dataRow["ReqVoucherID"] = reqsnVoucherID;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.EquipmentTransferTable.Rows.Add(dataRow);
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
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.EquipmentTransferSystem.GetEquipmentTransferByID(SystemDocID, voucherID);
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
				DataRow dataRow = currentData.Tables["EA_Equipment_Transfer"].Rows[0];
				comboBoxEquipment.SelectedID = dataRow["EquipmentID"].ToString();
				dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				textBoxRef1.Text = dataRow["Reference"].ToString();
				comboBoxFromLocation.SelectedID = dataRow["LocationFromID"].ToString();
				comboBoxToLocation.SelectedID = dataRow["LocationToID"].ToString();
				comboBoxFromJob.SelectedID = dataRow["JobFromID"].ToString();
				comboBoxToJob.SelectedID = dataRow["JobToID"].ToString();
				comboBoxFromEmployee.SelectedID = dataRow["EmployeeFromID"].ToString();
				comboBoxToEmployee.SelectedID = dataRow["EmployeeToID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxCurrentMeterReading.Text = dataRow["CurrentMeterReading"].ToString();
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
				bool flag = Factory.EquipmentTransferSystem.CreateEquipmentTransfer(currentData, !isNewRecord);
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
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxEquipment.SelectedID == "" || comboBoxToEmployee.SelectedID == "" || comboBoxToJob.SelectedID == "" || comboBoxToLocation.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("EA_Equipment_Transfer", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			return 0m;
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
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxEquipment.Clear();
				comboBoxToLocation.Clear();
				comboBoxFromLocation.Clear();
				comboBoxFromJob.Clear();
				comboBoxFromEmployee.Clear();
				comboBoxToLocation.Clear();
				comboBoxToJob.Clear();
				comboBoxToEmployee.Clear();
				comboBoxEquipmentType.Clear();
				comboBoxVendor.Clear();
				textBoxRegistrationNo.Clear();
				textBoxModel.Clear();
				textBoxCapacity.Clear();
				comboBoxFuel.SelectedIndex = -1;
				textBoxFuel.Clear();
				textBoxCapacityType.Clear();
				textBoxYear.Clear();
				textBoxPower.Clear();
				textBoxRegistrationNo.Clear();
				textBoxSerailH.Clear();
				textBoxPlateH.Clear();
				textBoxOwnedBy.Clear();
				comboBoxCapacity.SelectedIndex = -1;
				textBoxCurrentMeterReading.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.EquipmentTransferSystem.DeleteEquipmentTransfer(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("EA_Equipment_Transfer", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("EA_Equipment_Transfer", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("EA_Equipment_Transfer", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("EA_Equipment_Transfer", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("EA_Equipment_Transfer", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.EquipmentTransfer);
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
			if (isVoid)
			{
				ErrorHelper.InformationMessage("This transaction is already voided.");
			}
			else if (Void(isVoid: true))
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot void this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				return Factory.EquipmentTransferSystem.VoidEquipmentTransfer(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EquipmentTransfer);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxToJob.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel3_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxToEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditWorkLocation(comboBoxToLocation.SelectedID);
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

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "EA_Equipment_Transfer", "VoucherID");
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EquipmentTransferListFormObj);
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
						comboBoxFromEmployee.SelectedID = dataRow["OwnedBy"].ToString();
					}
					if (dataRow["JobID"] != DBNull.Value && dataRow["JobID"].ToString() != "")
					{
						comboBoxFromJob.SelectedID = dataRow["JobID"].ToString();
					}
					if (dataRow["LocationID"] != DBNull.Value && dataRow["LocationID"].ToString() != "")
					{
						comboBoxFromLocation.SelectedID = dataRow["LocationID"].ToString();
					}
				}
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

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				DataSet equipmentTransferToPrint = Factory.EquipmentTransferSystem.GetEquipmentTransferToPrint(selectedID, text);
				if (equipmentTransferToPrint == null || equipmentTransferToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(equipmentTransferToPrint, selectedID, "Equipment Transfer", SysDocTypes.Requisition, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void ultraFormattedLinkLabel7_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEAEquipment(comboBoxEquipment.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked_2(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEAEquipment(comboBoxEquipment.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet mobilizationList = Factory.MobilizationSystem.GetMobilizationList();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.HiddenColumns.Add("SourceSysDocID");
			selectDocumentDialog.HiddenColumns.Add("SourceVoucherID");
			selectDocumentDialog.HiddenColumns.Add("RowIndex");
			selectDocumentDialog.DataSource = mobilizationList;
			selectDocumentDialog.Text = "Select Mobilization";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				string sourceSysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string sourceVoucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				string text = selectDocumentDialog.SelectedRow.Cells["EquipmentID"].Value.ToString();
				SourceSysDocID = sourceSysDocID;
				SourceVoucherID = sourceVoucherID;
				if (selectDocumentDialog.SelectedRow.Cells["RowIndex"].Value.ToString() != "" && selectDocumentDialog.SelectedRow.Cells["RowIndex"].Value != DBNull.Value)
				{
					sourceRowIndex = int.Parse(selectDocumentDialog.SelectedRow.Cells["RowIndex"].Value.ToString());
				}
				else
				{
					sourceRowIndex = 0;
				}
				reqsnSysDocID = selectDocumentDialog.SelectedRow.Cells["SourceSysDocID"].Value.ToString();
				reqsnVoucherID = selectDocumentDialog.SelectedRow.Cells["SourceVoucherID"].Value.ToString();
				if (text != "" && text != null)
				{
					comboBoxEquipment.SelectedID = text;
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.EnterpriseAsset.EquipmentTransferForm));
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
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
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
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			textBoxCapacityType = new Micromind.UISupport.MMTextBox();
			textBoxYear = new Micromind.UISupport.MMTextBox();
			textBoxFuel = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxCurrentMeterReading = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxOwnedBy = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			textBoxPower = new Micromind.UISupport.MMTextBox();
			textBoxProviderName = new System.Windows.Forms.TextBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			textBoxTypeName = new System.Windows.Forms.TextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			textBoxPlateH = new Micromind.UISupport.MMTextBox();
			comboBoxFuel = new System.Windows.Forms.ComboBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxSerailH = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			comboBoxCapacity = new System.Windows.Forms.ComboBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCapacity = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxModel = new Micromind.UISupport.MMTextBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxEquipmentType = new Micromind.DataControls.EquipmentTypeComboBox();
			textBoxRegistrationNo = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxEquipmentName = new System.Windows.Forms.TextBox();
			comboBoxEquipment = new Micromind.DataControls.EAEquipmentComboBox();
			labelVoided = new System.Windows.Forms.Label();
			groupBox2 = new System.Windows.Forms.GroupBox();
			comboBoxToLocation = new Micromind.DataControls.WorkLocationComboBox();
			textBoxTolocationName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxToEmployeeName = new System.Windows.Forms.TextBox();
			textBoxToJobName = new System.Windows.Forms.TextBox();
			comboBoxToEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxToJob = new Micromind.DataControls.JobComboBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			comboBoxFromLocation = new Micromind.DataControls.WorkLocationComboBox();
			textBoxLocationName = new System.Windows.Forms.TextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeName = new System.Windows.Forms.TextBox();
			textBoxJobName = new System.Windows.Forms.TextBox();
			comboBoxFromEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxFromJob = new Micromind.DataControls.JobComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipment).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToJob).BeginInit();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator3,
				toolStripButtonPreview,
				toolStripButton1,
				toolStripSeparator4,
				toolStripDropDownButton1,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(719, 31);
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
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(86, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				duplicateToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(55, 28);
			toolStripDropDownButton1.Text = "Actions";
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			duplicateToolStripMenuItem.Text = "Create From Mobilization";
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
			panelButtons.Location = new System.Drawing.Point(0, 475);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(719, 40);
			panelButtons.TabIndex = 1;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(214, 8);
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
			buttonDelete.Location = new System.Drawing.Point(315, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(113, 8);
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
			linePanelDown.Size = new System.Drawing.Size(719, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(609, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
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
			dateTimePickerDate.Location = new System.Drawing.Point(590, 6);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(113, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(315, 4);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(201, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxNote.Location = new System.Drawing.Point(71, 348);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(455, 76);
			textBoxNote.TabIndex = 8;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(17, 351);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 13;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(256, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(51, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(textBoxCapacityType);
			panelDetails.Controls.Add(textBoxYear);
			panelDetails.Controls.Add(textBoxFuel);
			panelDetails.Controls.Add(ultraFormattedLinkLabel7);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxCurrentMeterReading);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(comboBoxEmployee);
			panelDetails.Controls.Add(mmLabel6);
			panelDetails.Controls.Add(mmLabel7);
			panelDetails.Controls.Add(textBoxOwnedBy);
			panelDetails.Controls.Add(mmLabel12);
			panelDetails.Controls.Add(mmLabel27);
			panelDetails.Controls.Add(textBoxPower);
			panelDetails.Controls.Add(textBoxProviderName);
			panelDetails.Controls.Add(mmLabel25);
			panelDetails.Controls.Add(textBoxTypeName);
			panelDetails.Controls.Add(mmLabel31);
			panelDetails.Controls.Add(textBoxPlateH);
			panelDetails.Controls.Add(comboBoxFuel);
			panelDetails.Controls.Add(mmLabel22);
			panelDetails.Controls.Add(textBoxSerailH);
			panelDetails.Controls.Add(mmLabel10);
			panelDetails.Controls.Add(comboBoxCapacity);
			panelDetails.Controls.Add(mmLabel13);
			panelDetails.Controls.Add(textBoxCapacity);
			panelDetails.Controls.Add(mmLabel8);
			panelDetails.Controls.Add(textBoxModel);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(comboBoxEquipmentType);
			panelDetails.Controls.Add(textBoxRegistrationNo);
			panelDetails.Controls.Add(mmLabel9);
			panelDetails.Controls.Add(textBoxEquipmentName);
			panelDetails.Controls.Add(comboBoxEquipment);
			panelDetails.Controls.Add(labelVoided);
			panelDetails.Controls.Add(groupBox2);
			panelDetails.Controls.Add(groupBox1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(715, 427);
			panelDetails.TabIndex = 0;
			textBoxCapacityType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCapacityType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCapacityType.CustomReportFieldName = "";
			textBoxCapacityType.CustomReportKey = "";
			textBoxCapacityType.CustomReportValueType = 1;
			textBoxCapacityType.ForeColor = System.Drawing.Color.Black;
			textBoxCapacityType.IsComboTextBox = false;
			textBoxCapacityType.Location = new System.Drawing.Point(185, 150);
			textBoxCapacityType.MaxLength = 15;
			textBoxCapacityType.Name = "textBoxCapacityType";
			textBoxCapacityType.ReadOnly = true;
			textBoxCapacityType.Size = new System.Drawing.Size(53, 20);
			textBoxCapacityType.TabIndex = 321;
			textBoxCapacityType.TabStop = false;
			textBoxYear.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxYear.CustomReportFieldName = "";
			textBoxYear.CustomReportKey = "";
			textBoxYear.CustomReportValueType = 1;
			textBoxYear.ForeColor = System.Drawing.Color.Black;
			textBoxYear.IsComboTextBox = false;
			textBoxYear.Location = new System.Drawing.Point(336, 127);
			textBoxYear.MaxLength = 15;
			textBoxYear.Name = "textBoxYear";
			textBoxYear.ReadOnly = true;
			textBoxYear.Size = new System.Drawing.Size(83, 20);
			textBoxYear.TabIndex = 320;
			textBoxYear.TabStop = false;
			textBoxFuel.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFuel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFuel.CustomReportFieldName = "";
			textBoxFuel.CustomReportKey = "";
			textBoxFuel.CustomReportValueType = 1;
			textBoxFuel.ForeColor = System.Drawing.Color.Black;
			textBoxFuel.IsComboTextBox = false;
			textBoxFuel.Location = new System.Drawing.Point(227, 125);
			textBoxFuel.MaxLength = 15;
			textBoxFuel.Name = "textBoxFuel";
			textBoxFuel.ReadOnly = true;
			textBoxFuel.Size = new System.Drawing.Size(72, 20);
			textBoxFuel.TabIndex = 319;
			textBoxFuel.TabStop = false;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance3;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(17, 33);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(67, 15);
			ultraFormattedLinkLabel7.TabIndex = 318;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Equipment:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked_2);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(527, 36);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 317;
			label1.Text = "Reference:";
			label1.Visible = false;
			textBoxRef1.Location = new System.Drawing.Point(592, 32);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(113, 20);
			textBoxRef1.TabIndex = 4;
			textBoxRef1.Visible = false;
			textBoxCurrentMeterReading.BackColor = System.Drawing.Color.White;
			textBoxCurrentMeterReading.CustomReportFieldName = "";
			textBoxCurrentMeterReading.CustomReportKey = "";
			textBoxCurrentMeterReading.CustomReportValueType = 1;
			textBoxCurrentMeterReading.IsComboTextBox = false;
			textBoxCurrentMeterReading.IsRequired = true;
			textBoxCurrentMeterReading.Location = new System.Drawing.Point(114, 204);
			textBoxCurrentMeterReading.MaxLength = 64;
			textBoxCurrentMeterReading.Name = "textBoxCurrentMeterReading";
			textBoxCurrentMeterReading.Size = new System.Drawing.Size(138, 20);
			textBoxCurrentMeterReading.TabIndex = 5;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(17, 197);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(81, 26);
			mmLabel2.TabIndex = 316;
			mmLabel2.Text = "Current \r\nMeter Reading:";
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxOwnedBy;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance5;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(342, 174);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(139, 20);
			comboBoxEmployee.TabIndex = 311;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.Visible = false;
			textBoxOwnedBy.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxOwnedBy.CustomReportFieldName = "";
			textBoxOwnedBy.CustomReportKey = "";
			textBoxOwnedBy.CustomReportValueType = 1;
			textBoxOwnedBy.IsComboTextBox = false;
			textBoxOwnedBy.Location = new System.Drawing.Point(114, 174);
			textBoxOwnedBy.MaxLength = 30;
			textBoxOwnedBy.Name = "textBoxOwnedBy";
			textBoxOwnedBy.ReadOnly = true;
			textBoxOwnedBy.Size = new System.Drawing.Size(221, 20);
			textBoxOwnedBy.TabIndex = 298;
			textBoxOwnedBy.TabStop = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(17, 82);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(51, 13);
			mmLabel6.TabIndex = 310;
			mmLabel6.Text = "Provider:";
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(17, 58);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(35, 13);
			mmLabel7.TabIndex = 309;
			mmLabel7.Text = "Type:";
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(17, 178);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(60, 13);
			mmLabel12.TabIndex = 308;
			mmLabel12.Text = "Owned By:";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(428, 128);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(41, 13);
			mmLabel27.TabIndex = 307;
			mmLabel27.Text = "Power:";
			textBoxPower.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPower.CustomReportFieldName = "";
			textBoxPower.CustomReportKey = "";
			textBoxPower.CustomReportValueType = 1;
			textBoxPower.IsComboTextBox = false;
			textBoxPower.Location = new System.Drawing.Point(487, 124);
			textBoxPower.MaxLength = 64;
			textBoxPower.Name = "textBoxPower";
			textBoxPower.ReadOnly = true;
			textBoxPower.Size = new System.Drawing.Size(110, 20);
			textBoxPower.TabIndex = 291;
			textBoxPower.TabStop = false;
			textBoxProviderName.Location = new System.Drawing.Point(114, 78);
			textBoxProviderName.MaxLength = 30;
			textBoxProviderName.Name = "textBoxProviderName";
			textBoxProviderName.ReadOnly = true;
			textBoxProviderName.Size = new System.Drawing.Size(328, 20);
			textBoxProviderName.TabIndex = 305;
			textBoxProviderName.TabStop = false;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(305, 128);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(33, 13);
			mmLabel25.TabIndex = 306;
			mmLabel25.Text = "Year:";
			textBoxTypeName.Location = new System.Drawing.Point(114, 54);
			textBoxTypeName.MaxLength = 30;
			textBoxTypeName.Name = "textBoxTypeName";
			textBoxTypeName.ReadOnly = true;
			textBoxTypeName.Size = new System.Drawing.Size(329, 20);
			textBoxTypeName.TabIndex = 304;
			textBoxTypeName.TabStop = false;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(428, 153);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(53, 13);
			mmLabel31.TabIndex = 296;
			mmLabel31.Text = "Plate NO:";
			textBoxPlateH.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPlateH.CustomReportFieldName = "";
			textBoxPlateH.CustomReportKey = "";
			textBoxPlateH.CustomReportValueType = 1;
			textBoxPlateH.IsComboTextBox = false;
			textBoxPlateH.Location = new System.Drawing.Point(487, 149);
			textBoxPlateH.MaxLength = 30;
			textBoxPlateH.Name = "textBoxPlateH";
			textBoxPlateH.ReadOnly = true;
			textBoxPlateH.Size = new System.Drawing.Size(110, 20);
			textBoxPlateH.TabIndex = 297;
			textBoxPlateH.TabStop = false;
			comboBoxFuel.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			comboBoxFuel.Enabled = false;
			comboBoxFuel.FormattingEnabled = true;
			comboBoxFuel.Items.AddRange(new object[2]
			{
				"Petrol",
				"Diesel"
			});
			comboBoxFuel.Location = new System.Drawing.Point(227, 125);
			comboBoxFuel.Name = "comboBoxFuel";
			comboBoxFuel.Size = new System.Drawing.Size(72, 23);
			comboBoxFuel.TabIndex = 289;
			comboBoxFuel.TabStop = false;
			comboBoxFuel.Visible = false;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(242, 155);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(55, 13);
			mmLabel22.TabIndex = 294;
			mmLabel22.Text = "Serial NO:";
			textBoxSerailH.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSerailH.CustomReportFieldName = "";
			textBoxSerailH.CustomReportKey = "";
			textBoxSerailH.CustomReportValueType = 1;
			textBoxSerailH.IsComboTextBox = false;
			textBoxSerailH.Location = new System.Drawing.Point(303, 151);
			textBoxSerailH.MaxLength = 30;
			textBoxSerailH.Name = "textBoxSerailH";
			textBoxSerailH.ReadOnly = true;
			textBoxSerailH.Size = new System.Drawing.Size(116, 20);
			textBoxSerailH.TabIndex = 295;
			textBoxSerailH.TabStop = false;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(190, 129);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(31, 13);
			mmLabel10.TabIndex = 303;
			mmLabel10.Text = "Fuel:";
			comboBoxCapacity.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCapacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			comboBoxCapacity.Enabled = false;
			comboBoxCapacity.FormattingEnabled = true;
			comboBoxCapacity.Items.AddRange(new object[2]
			{
				"Seat",
				"Ton"
			});
			comboBoxCapacity.Location = new System.Drawing.Point(185, 150);
			comboBoxCapacity.Name = "comboBoxCapacity";
			comboBoxCapacity.Size = new System.Drawing.Size(53, 21);
			comboBoxCapacity.TabIndex = 293;
			comboBoxCapacity.TabStop = false;
			comboBoxCapacity.Visible = false;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(17, 154);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(53, 13);
			mmLabel13.TabIndex = 302;
			mmLabel13.Text = "Capacity:";
			textBoxCapacity.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCapacity.CustomReportFieldName = "";
			textBoxCapacity.CustomReportKey = "";
			textBoxCapacity.CustomReportValueType = 1;
			textBoxCapacity.IsComboTextBox = false;
			textBoxCapacity.Location = new System.Drawing.Point(114, 150);
			textBoxCapacity.MaxLength = 64;
			textBoxCapacity.Name = "textBoxCapacity";
			textBoxCapacity.ReadOnly = true;
			textBoxCapacity.Size = new System.Drawing.Size(68, 20);
			textBoxCapacity.TabIndex = 292;
			textBoxCapacity.TabStop = false;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(17, 130);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(39, 13);
			mmLabel8.TabIndex = 301;
			mmLabel8.Text = "Model:";
			textBoxModel.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxModel.CustomReportFieldName = "";
			textBoxModel.CustomReportKey = "";
			textBoxModel.CustomReportValueType = 1;
			textBoxModel.ForeColor = System.Drawing.Color.Black;
			textBoxModel.IsComboTextBox = false;
			textBoxModel.Location = new System.Drawing.Point(114, 126);
			textBoxModel.MaxLength = 15;
			textBoxModel.Name = "textBoxModel";
			textBoxModel.ReadOnly = true;
			textBoxModel.Size = new System.Drawing.Size(66, 20);
			textBoxModel.TabIndex = 288;
			textBoxModel.TabStop = false;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxProviderName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance17;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
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
			comboBoxVendor.Location = new System.Drawing.Point(446, 77);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.MaxLength = 64;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ReadOnly = true;
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(115, 20);
			comboBoxVendor.TabIndex = 286;
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
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipmentType.DisplayLayout.Appearance = appearance29;
			comboBoxEquipmentType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipmentType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipmentType.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxEquipmentType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipmentType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipmentType.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipmentType.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxEquipmentType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipmentType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipmentType.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxEquipmentType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipmentType.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipmentType.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxEquipmentType.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxEquipmentType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipmentType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipmentType.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxEquipmentType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipmentType.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxEquipmentType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipmentType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipmentType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipmentType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipmentType.Editable = true;
			comboBoxEquipmentType.FilterString = "";
			comboBoxEquipmentType.HasAllAccount = false;
			comboBoxEquipmentType.HasCustom = false;
			comboBoxEquipmentType.IsDataLoaded = false;
			comboBoxEquipmentType.Location = new System.Drawing.Point(446, 54);
			comboBoxEquipmentType.MaxDropDownItems = 12;
			comboBoxEquipmentType.Name = "comboBoxEquipmentType";
			comboBoxEquipmentType.ReadOnly = true;
			comboBoxEquipmentType.ShowInactiveItems = false;
			comboBoxEquipmentType.ShowQuickAdd = true;
			comboBoxEquipmentType.Size = new System.Drawing.Size(115, 20);
			comboBoxEquipmentType.TabIndex = 285;
			comboBoxEquipmentType.TabStop = false;
			comboBoxEquipmentType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipmentType.Visible = false;
			textBoxRegistrationNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRegistrationNo.CustomReportFieldName = "";
			textBoxRegistrationNo.CustomReportKey = "";
			textBoxRegistrationNo.CustomReportValueType = 1;
			textBoxRegistrationNo.IsComboTextBox = false;
			textBoxRegistrationNo.IsRequired = true;
			textBoxRegistrationNo.Location = new System.Drawing.Point(114, 102);
			textBoxRegistrationNo.MaxLength = 64;
			textBoxRegistrationNo.Name = "textBoxRegistrationNo";
			textBoxRegistrationNo.ReadOnly = true;
			textBoxRegistrationNo.Size = new System.Drawing.Size(226, 20);
			textBoxRegistrationNo.TabIndex = 287;
			textBoxRegistrationNo.TabStop = false;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(17, 106);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(87, 13);
			mmLabel9.TabIndex = 300;
			mmLabel9.Text = "Registration NO:";
			textBoxEquipmentName.Location = new System.Drawing.Point(245, 30);
			textBoxEquipmentName.MaxLength = 30;
			textBoxEquipmentName.Name = "textBoxEquipmentName";
			textBoxEquipmentName.ReadOnly = true;
			textBoxEquipmentName.Size = new System.Drawing.Size(271, 20);
			textBoxEquipmentName.TabIndex = 299;
			textBoxEquipmentName.TabStop = false;
			comboBoxEquipment.Assigned = false;
			comboBoxEquipment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEquipment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEquipment.CustomReportFieldName = "";
			comboBoxEquipment.CustomReportKey = "";
			comboBoxEquipment.CustomReportValueType = 1;
			comboBoxEquipment.DescriptionTextBox = textBoxEquipmentName;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEquipment.DisplayLayout.Appearance = appearance41;
			comboBoxEquipment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEquipment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxEquipment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEquipment.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxEquipment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEquipment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEquipment.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEquipment.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxEquipment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEquipment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEquipment.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxEquipment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEquipment.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEquipment.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxEquipment.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxEquipment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEquipment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxEquipment.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxEquipment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEquipment.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxEquipment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEquipment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEquipment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEquipment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEquipment.Editable = true;
			comboBoxEquipment.FilterString = "";
			comboBoxEquipment.HasAllAccount = false;
			comboBoxEquipment.HasCustom = false;
			comboBoxEquipment.IsDataLoaded = false;
			comboBoxEquipment.Location = new System.Drawing.Point(114, 30);
			comboBoxEquipment.MaxDropDownItems = 12;
			comboBoxEquipment.Name = "comboBoxEquipment";
			comboBoxEquipment.ShowInactiveItems = false;
			comboBoxEquipment.ShowQuickAdd = true;
			comboBoxEquipment.Size = new System.Drawing.Size(124, 20);
			comboBoxEquipment.TabIndex = 3;
			comboBoxEquipment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEquipment.SelectedIndexChanged += new System.EventHandler(comboBoxEquipment_SelectedIndexChanged);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(529, 377);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(178, 30);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			groupBox2.Controls.Add(comboBoxToLocation);
			groupBox2.Controls.Add(ultraFormattedLinkLabel6);
			groupBox2.Controls.Add(ultraFormattedLinkLabel4);
			groupBox2.Controls.Add(ultraFormattedLinkLabel3);
			groupBox2.Controls.Add(textBoxToEmployeeName);
			groupBox2.Controls.Add(textBoxTolocationName);
			groupBox2.Controls.Add(textBoxToJobName);
			groupBox2.Controls.Add(comboBoxToEmployee);
			groupBox2.Controls.Add(comboBoxToJob);
			groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			groupBox2.Location = new System.Drawing.Point(364, 230);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(337, 109);
			groupBox2.TabIndex = 7;
			groupBox2.TabStop = false;
			groupBox2.Text = "To";
			comboBoxToLocation.Assigned = false;
			comboBoxToLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLocation.CustomReportFieldName = "";
			comboBoxToLocation.CustomReportKey = "";
			comboBoxToLocation.CustomReportValueType = 1;
			comboBoxToLocation.DescriptionTextBox = textBoxTolocationName;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLocation.DisplayLayout.Appearance = appearance53;
			comboBoxToLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxToLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxToLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLocation.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLocation.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLocation.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxToLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLocation.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxToLocation.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxToLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLocation.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxToLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxToLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLocation.Editable = true;
			comboBoxToLocation.FilterString = "";
			comboBoxToLocation.HasAllAccount = false;
			comboBoxToLocation.HasCustom = false;
			comboBoxToLocation.IsDataLoaded = false;
			comboBoxToLocation.Location = new System.Drawing.Point(62, 44);
			comboBoxToLocation.MaxDropDownItems = 12;
			comboBoxToLocation.Name = "comboBoxToLocation";
			comboBoxToLocation.ShowAll = false;
			comboBoxToLocation.ShowConsignIn = false;
			comboBoxToLocation.ShowConsignOut = false;
			comboBoxToLocation.ShowInactiveItems = false;
			comboBoxToLocation.ShowNormalLocations = true;
			comboBoxToLocation.ShowPOSOnly = false;
			comboBoxToLocation.ShowQuickAdd = true;
			comboBoxToLocation.ShowWarehouseOnly = false;
			comboBoxToLocation.Size = new System.Drawing.Size(100, 20);
			comboBoxToLocation.TabIndex = 1;
			comboBoxToLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxTolocationName.Location = new System.Drawing.Point(163, 44);
			textBoxTolocationName.MaxLength = 30;
			textBoxTolocationName.Name = "textBoxTolocationName";
			textBoxTolocationName.ReadOnly = true;
			textBoxTolocationName.Size = new System.Drawing.Size(156, 20);
			textBoxTolocationName.TabIndex = 138;
			textBoxTolocationName.TabStop = false;
			appearance65.FontData.BoldAsString = "True";
			appearance65.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance65;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(3, 47);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel6.TabIndex = 173;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Location:";
			appearance66.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance66;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance67.FontData.BoldAsString = "True";
			appearance67.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance67;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(3, 70);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(58, 15);
			ultraFormattedLinkLabel4.TabIndex = 172;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Incharge:";
			appearance68.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance68;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance69.FontData.BoldAsString = "True";
			appearance69.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance69;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(3, 24);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(27, 15);
			ultraFormattedLinkLabel3.TabIndex = 171;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Job:";
			appearance70.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance70;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			textBoxToEmployeeName.Location = new System.Drawing.Point(163, 67);
			textBoxToEmployeeName.MaxLength = 30;
			textBoxToEmployeeName.Name = "textBoxToEmployeeName";
			textBoxToEmployeeName.ReadOnly = true;
			textBoxToEmployeeName.Size = new System.Drawing.Size(156, 20);
			textBoxToEmployeeName.TabIndex = 139;
			textBoxToEmployeeName.TabStop = false;
			textBoxToJobName.Location = new System.Drawing.Point(163, 22);
			textBoxToJobName.MaxLength = 30;
			textBoxToJobName.Name = "textBoxToJobName";
			textBoxToJobName.ReadOnly = true;
			textBoxToJobName.Size = new System.Drawing.Size(156, 20);
			textBoxToJobName.TabIndex = 137;
			textBoxToJobName.TabStop = false;
			comboBoxToEmployee.Assigned = false;
			comboBoxToEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToEmployee.CustomReportFieldName = "";
			comboBoxToEmployee.CustomReportKey = "";
			comboBoxToEmployee.CustomReportValueType = 1;
			comboBoxToEmployee.DescriptionTextBox = textBoxToEmployeeName;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToEmployee.DisplayLayout.Appearance = appearance71;
			comboBoxToEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToEmployee.DisplayLayout.GroupByBox.Appearance = appearance72;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance73;
			comboBoxToEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance74.BackColor2 = System.Drawing.SystemColors.Control;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance74;
			comboBoxToEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance75;
			appearance76.BackColor = System.Drawing.SystemColors.Highlight;
			appearance76.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance76;
			comboBoxToEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToEmployee.DisplayLayout.Override.CardAreaAppearance = appearance77;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			appearance78.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToEmployee.DisplayLayout.Override.CellAppearance = appearance78;
			comboBoxToEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance79.BackColor = System.Drawing.SystemColors.Control;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance79;
			appearance80.TextHAlignAsString = "Left";
			comboBoxToEmployee.DisplayLayout.Override.HeaderAppearance = appearance80;
			comboBoxToEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			comboBoxToEmployee.DisplayLayout.Override.RowAppearance = appearance81;
			comboBoxToEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance82;
			comboBoxToEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToEmployee.Editable = true;
			comboBoxToEmployee.FilterString = "";
			comboBoxToEmployee.HasAllAccount = false;
			comboBoxToEmployee.HasCustom = false;
			comboBoxToEmployee.IsDataLoaded = false;
			comboBoxToEmployee.Location = new System.Drawing.Point(62, 67);
			comboBoxToEmployee.MaxDropDownItems = 12;
			comboBoxToEmployee.Name = "comboBoxToEmployee";
			comboBoxToEmployee.ShowInactiveItems = false;
			comboBoxToEmployee.ShowQuickAdd = true;
			comboBoxToEmployee.ShowTerminatedEmployees = true;
			comboBoxToEmployee.Size = new System.Drawing.Size(100, 20);
			comboBoxToEmployee.TabIndex = 2;
			comboBoxToEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToJob.Assigned = false;
			comboBoxToJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToJob.CustomReportFieldName = "";
			comboBoxToJob.CustomReportKey = "";
			comboBoxToJob.CustomReportValueType = 1;
			comboBoxToJob.DescriptionTextBox = textBoxToJobName;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToJob.DisplayLayout.Appearance = appearance83;
			comboBoxToJob.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToJob.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToJob.DisplayLayout.GroupByBox.Appearance = appearance84;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToJob.DisplayLayout.GroupByBox.BandLabelAppearance = appearance85;
			comboBoxToJob.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance86.BackColor2 = System.Drawing.SystemColors.Control;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToJob.DisplayLayout.GroupByBox.PromptAppearance = appearance86;
			comboBoxToJob.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToJob.DisplayLayout.MaxRowScrollRegions = 1;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToJob.DisplayLayout.Override.ActiveCellAppearance = appearance87;
			appearance88.BackColor = System.Drawing.SystemColors.Highlight;
			appearance88.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToJob.DisplayLayout.Override.ActiveRowAppearance = appearance88;
			comboBoxToJob.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToJob.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToJob.DisplayLayout.Override.CardAreaAppearance = appearance89;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			appearance90.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToJob.DisplayLayout.Override.CellAppearance = appearance90;
			comboBoxToJob.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToJob.DisplayLayout.Override.CellPadding = 0;
			appearance91.BackColor = System.Drawing.SystemColors.Control;
			appearance91.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance91.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToJob.DisplayLayout.Override.GroupByRowAppearance = appearance91;
			appearance92.TextHAlignAsString = "Left";
			comboBoxToJob.DisplayLayout.Override.HeaderAppearance = appearance92;
			comboBoxToJob.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToJob.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			comboBoxToJob.DisplayLayout.Override.RowAppearance = appearance93;
			comboBoxToJob.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToJob.DisplayLayout.Override.TemplateAddRowAppearance = appearance94;
			comboBoxToJob.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToJob.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToJob.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToJob.Editable = true;
			comboBoxToJob.FilterString = "";
			comboBoxToJob.HasAllAccount = false;
			comboBoxToJob.HasCustom = false;
			comboBoxToJob.IsDataLoaded = false;
			comboBoxToJob.Location = new System.Drawing.Point(62, 21);
			comboBoxToJob.MaxDropDownItems = 12;
			comboBoxToJob.Name = "comboBoxToJob";
			comboBoxToJob.ShowInactiveItems = false;
			comboBoxToJob.ShowQuickAdd = true;
			comboBoxToJob.Size = new System.Drawing.Size(100, 20);
			comboBoxToJob.TabIndex = 0;
			comboBoxToJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			groupBox1.Controls.Add(comboBoxFromLocation);
			groupBox1.Controls.Add(mmLabel5);
			groupBox1.Controls.Add(mmLabel4);
			groupBox1.Controls.Add(mmLabel3);
			groupBox1.Controls.Add(textBoxEmployeeName);
			groupBox1.Controls.Add(textBoxLocationName);
			groupBox1.Controls.Add(textBoxJobName);
			groupBox1.Controls.Add(comboBoxFromEmployee);
			groupBox1.Controls.Add(comboBoxFromJob);
			groupBox1.Location = new System.Drawing.Point(17, 230);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(338, 109);
			groupBox1.TabIndex = 6;
			groupBox1.TabStop = false;
			groupBox1.Text = " From";
			comboBoxFromLocation.Assigned = false;
			comboBoxFromLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLocation.CustomReportFieldName = "";
			comboBoxFromLocation.CustomReportKey = "";
			comboBoxFromLocation.CustomReportValueType = 1;
			comboBoxFromLocation.DescriptionTextBox = textBoxLocationName;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLocation.DisplayLayout.Appearance = appearance95;
			comboBoxFromLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.GroupByBox.Appearance = appearance96;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance97;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance98.BackColor2 = System.Drawing.SystemColors.Control;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance98.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance98;
			comboBoxFromLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveCellAppearance = appearance99;
			appearance100.BackColor = System.Drawing.SystemColors.Highlight;
			appearance100.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveRowAppearance = appearance100;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.CardAreaAppearance = appearance101;
			appearance102.BorderColor = System.Drawing.Color.Silver;
			appearance102.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLocation.DisplayLayout.Override.CellAppearance = appearance102;
			comboBoxFromLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLocation.DisplayLayout.Override.CellPadding = 0;
			appearance103.BackColor = System.Drawing.SystemColors.Control;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.GroupByRowAppearance = appearance103;
			appearance104.TextHAlignAsString = "Left";
			comboBoxFromLocation.DisplayLayout.Override.HeaderAppearance = appearance104;
			comboBoxFromLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLocation.DisplayLayout.Override.RowAppearance = appearance105;
			comboBoxFromLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance106;
			comboBoxFromLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLocation.Editable = true;
			comboBoxFromLocation.FilterString = "";
			comboBoxFromLocation.HasAllAccount = false;
			comboBoxFromLocation.HasCustom = false;
			comboBoxFromLocation.IsDataLoaded = false;
			comboBoxFromLocation.Location = new System.Drawing.Point(63, 42);
			comboBoxFromLocation.MaxDropDownItems = 12;
			comboBoxFromLocation.Name = "comboBoxFromLocation";
			comboBoxFromLocation.ReadOnly = true;
			comboBoxFromLocation.ShowAll = false;
			comboBoxFromLocation.ShowConsignIn = false;
			comboBoxFromLocation.ShowConsignOut = false;
			comboBoxFromLocation.ShowInactiveItems = false;
			comboBoxFromLocation.ShowNormalLocations = true;
			comboBoxFromLocation.ShowPOSOnly = false;
			comboBoxFromLocation.ShowQuickAdd = true;
			comboBoxFromLocation.ShowWarehouseOnly = false;
			comboBoxFromLocation.Size = new System.Drawing.Size(100, 20);
			comboBoxFromLocation.TabIndex = 250;
			comboBoxFromLocation.TabStop = false;
			comboBoxFromLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxLocationName.Location = new System.Drawing.Point(165, 42);
			textBoxLocationName.MaxLength = 30;
			textBoxLocationName.Name = "textBoxLocationName";
			textBoxLocationName.ReadOnly = true;
			textBoxLocationName.Size = new System.Drawing.Size(156, 20);
			textBoxLocationName.TabIndex = 132;
			textBoxLocationName.TabStop = false;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(4, 45);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(51, 13);
			mmLabel5.TabIndex = 137;
			mmLabel5.Text = "Location:";
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(4, 68);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(54, 13);
			mmLabel4.TabIndex = 136;
			mmLabel4.Text = "Incharge:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(4, 23);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(28, 13);
			mmLabel3.TabIndex = 135;
			mmLabel3.Text = "Job:";
			textBoxEmployeeName.Location = new System.Drawing.Point(165, 65);
			textBoxEmployeeName.MaxLength = 30;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(156, 20);
			textBoxEmployeeName.TabIndex = 133;
			textBoxEmployeeName.TabStop = false;
			textBoxJobName.Location = new System.Drawing.Point(165, 20);
			textBoxJobName.MaxLength = 30;
			textBoxJobName.Name = "textBoxJobName";
			textBoxJobName.ReadOnly = true;
			textBoxJobName.Size = new System.Drawing.Size(156, 20);
			textBoxJobName.TabIndex = 131;
			textBoxJobName.TabStop = false;
			comboBoxFromEmployee.Assigned = false;
			comboBoxFromEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromEmployee.CustomReportFieldName = "";
			comboBoxFromEmployee.CustomReportKey = "";
			comboBoxFromEmployee.CustomReportValueType = 1;
			comboBoxFromEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromEmployee.DisplayLayout.Appearance = appearance107;
			comboBoxFromEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxFromEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxFromEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromEmployee.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromEmployee.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxFromEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxFromEmployee.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxFromEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromEmployee.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxFromEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
			comboBoxFromEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromEmployee.Editable = true;
			comboBoxFromEmployee.FilterString = "";
			comboBoxFromEmployee.HasAllAccount = false;
			comboBoxFromEmployee.HasCustom = false;
			comboBoxFromEmployee.IsDataLoaded = false;
			comboBoxFromEmployee.Location = new System.Drawing.Point(63, 65);
			comboBoxFromEmployee.MaxDropDownItems = 12;
			comboBoxFromEmployee.Name = "comboBoxFromEmployee";
			comboBoxFromEmployee.ReadOnly = true;
			comboBoxFromEmployee.ShowInactiveItems = false;
			comboBoxFromEmployee.ShowQuickAdd = true;
			comboBoxFromEmployee.ShowTerminatedEmployees = true;
			comboBoxFromEmployee.Size = new System.Drawing.Size(100, 20);
			comboBoxFromEmployee.TabIndex = 2;
			comboBoxFromEmployee.TabStop = false;
			comboBoxFromEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromJob.Assigned = false;
			comboBoxFromJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromJob.CustomReportFieldName = "";
			comboBoxFromJob.CustomReportKey = "";
			comboBoxFromJob.CustomReportValueType = 1;
			comboBoxFromJob.DescriptionTextBox = textBoxJobName;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromJob.DisplayLayout.Appearance = appearance119;
			comboBoxFromJob.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromJob.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance120.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromJob.DisplayLayout.GroupByBox.Appearance = appearance120;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromJob.DisplayLayout.GroupByBox.BandLabelAppearance = appearance121;
			comboBoxFromJob.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance122.BackColor2 = System.Drawing.SystemColors.Control;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance122.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromJob.DisplayLayout.GroupByBox.PromptAppearance = appearance122;
			comboBoxFromJob.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromJob.DisplayLayout.MaxRowScrollRegions = 1;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromJob.DisplayLayout.Override.ActiveCellAppearance = appearance123;
			appearance124.BackColor = System.Drawing.SystemColors.Highlight;
			appearance124.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromJob.DisplayLayout.Override.ActiveRowAppearance = appearance124;
			comboBoxFromJob.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromJob.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromJob.DisplayLayout.Override.CardAreaAppearance = appearance125;
			appearance126.BorderColor = System.Drawing.Color.Silver;
			appearance126.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromJob.DisplayLayout.Override.CellAppearance = appearance126;
			comboBoxFromJob.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromJob.DisplayLayout.Override.CellPadding = 0;
			appearance127.BackColor = System.Drawing.SystemColors.Control;
			appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance127.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance127.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromJob.DisplayLayout.Override.GroupByRowAppearance = appearance127;
			appearance128.TextHAlignAsString = "Left";
			comboBoxFromJob.DisplayLayout.Override.HeaderAppearance = appearance128;
			comboBoxFromJob.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromJob.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromJob.DisplayLayout.Override.RowAppearance = appearance129;
			comboBoxFromJob.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromJob.DisplayLayout.Override.TemplateAddRowAppearance = appearance130;
			comboBoxFromJob.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromJob.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromJob.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromJob.Editable = true;
			comboBoxFromJob.FilterString = "";
			comboBoxFromJob.HasAllAccount = false;
			comboBoxFromJob.HasCustom = false;
			comboBoxFromJob.IsDataLoaded = false;
			comboBoxFromJob.Location = new System.Drawing.Point(63, 19);
			comboBoxFromJob.MaxDropDownItems = 12;
			comboBoxFromJob.Name = "comboBoxFromJob";
			comboBoxFromJob.ReadOnly = true;
			comboBoxFromJob.ShowInactiveItems = false;
			comboBoxFromJob.ShowQuickAdd = true;
			comboBoxFromJob.Size = new System.Drawing.Size(100, 20);
			comboBoxFromJob.TabIndex = 0;
			comboBoxFromJob.TabStop = false;
			comboBoxFromJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance131.FontData.BoldAsString = "True";
			appearance131.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance131;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(17, 8);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance132.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance132;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance133;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance134;
			appearance135.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance135;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance136.BackColor2 = System.Drawing.SystemColors.Control;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance136;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance137;
			appearance138.BackColor = System.Drawing.SystemColors.Highlight;
			appearance138.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance138;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance139;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			appearance140.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance140;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance141.BackColor = System.Drawing.SystemColors.Control;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance141;
			appearance142.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance142;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance143;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance144;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(114, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(124, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(527, 8);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
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
			base.ClientSize = new System.Drawing.Size(719, 515);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 492);
			base.Name = "EquipmentTransferForm";
			Text = "Equipment Transfer";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipmentType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEquipment).EndInit();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToJob).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
