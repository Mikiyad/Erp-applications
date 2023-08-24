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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class MaintenanceSchedulerForm : Form, IForm
	{
		private MaintenanceSchedulerData currentData;

		private const string TABLENAME_CONST = "Vehicle_Maintenance_Scheduler";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool allowMultiTemplate;

		private string entrysysdocID;

		private string entryvoucherID;

		private string vehiclenumber;

		private string serviceitem;

		private string serviceprovider;

		private decimal odometer;

		private bool flag;

		private DateTime nextServiceDate;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isDataLoading;

		private bool isVoid;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private TextBox textBoxVoucherNumber;

		private Label labelOdodmeter;

		private ServiceProviderComboBox comboBoxServiceProvider;

		private Label label1;

		private Label label2;

		private MMTextBox textBoxTimeRequired;

		private Label labelDate;

		private MMSDateTimePicker dateTimePickerMaintenanceDate;

		private VehicleComboBox comboBoxVehicle;

		private MMTextBox textBoxVehicleNumber;

		private MMTextBox textBoxServiceProvider;

		private AmountTextBox textBoxAmount;

		private XPButton buttonVoid;

		private ToolStrip toolStrip2;

		private ToolStripButton toolStripButton2;

		private ToolStripButton toolStripButton3;

		private ToolStripButton toolStripButton4;

		private ToolStripButton toolStripButton5;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButton6;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripButton toolStripButton9;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ServiceItemComboBox comboBoxServiceType;

		private BALinkLabel baLinkLabelMS;

		private Label labelStatus;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private NumberTextBox textBoxOdometer;

		private MMTextBox textBoxServiceItem;

		private ToolStripButton toolStripButtonVerify;

		private TextBox textBoxNote;

		private Label label3;

		public ScreenAreas ScreenArea => ScreenAreas.Vehicle;

		public int ScreenID => 4006;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
					baLinkLabelMS.Visible = false;
					labelStatus.Visible = false;
					dateTimePickerMaintenanceDate.Value = DateTime.Now;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					buttonVoid.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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

		public string VehicleNumber
		{
			get
			{
				return vehiclenumber;
			}
			set
			{
				isNewRecord = true;
				string text2 = vehiclenumber = (comboBoxVehicle.SelectedID = value);
				comboBoxVehicle.ReadOnly = true;
			}
		}

		public decimal Odometer
		{
			get
			{
				return odometer;
			}
			set
			{
				odometer = value;
				textBoxOdometer.Text = value.ToString();
			}
		}

		public DateTime NextServiceDate
		{
			get
			{
				return nextServiceDate;
			}
			set
			{
				nextServiceDate = value;
				dateTimePickerMaintenanceDate.Value = value;
			}
		}

		public string ServiceItem
		{
			get
			{
				return serviceitem;
			}
			set
			{
				string text2 = serviceitem = (comboBoxServiceType.SelectedID = value);
			}
		}

		public string ServiceProvider
		{
			get
			{
				return serviceprovider;
			}
			set
			{
				string text2 = serviceprovider = (comboBoxServiceProvider.SelectedID = value);
			}
		}

		public bool Flag
		{
			get
			{
				return flag;
			}
			set
			{
				flag = value;
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
				buttonSave.Enabled = !value;
				labelVoided.Visible = value;
				if (value)
				{
					buttonVoid.Text = UIMessages.Unvoid;
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

		public MaintenanceSchedulerForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += MaintenanceScheduleForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new MaintenanceSchedulerData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.MaintenanceSchedulerTable.Rows[0] : currentData.MaintenanceSchedulerTable.NewRow();
				dataRow.BeginEdit();
				dataRow["VoucherID"] = textBoxVoucherNumber.Text.Trim(' ');
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VehicleNumber"] = comboBoxVehicle.SelectedID;
				dataRow["TimeRequired"] = textBoxTimeRequired.Text.Trim();
				dataRow["ServiceType"] = comboBoxServiceType.SelectedID;
				dataRow["TrackMaintenance"] = "";
				dataRow["Amount"] = textBoxAmount.Text.Trim();
				dataRow["Status"] = false;
				dataRow["Note"] = textBoxNote.Text.Trim();
				_ = dateTimePickerMaintenanceDate.Value;
				dataRow["MaintenanceDate"] = dateTimePickerMaintenanceDate.Value;
				dataRow["Odometer"] = textBoxOdometer.Text.Trim();
				if (comboBoxServiceProvider.SelectedID != "")
				{
					dataRow["ServiceProvider"] = comboBoxServiceProvider.SelectedID;
				}
				else
				{
					dataRow["ServiceProvider"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.MaintenanceSchedulerTable.Rows.Add(dataRow);
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

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.MaintenanceSchedulerSystem.GetMaintenanceSchedulerByID(SystemDocID, id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxVoucherNumber.Text = id;
						textBoxVoucherNumber.Focus();
					}
					else
					{
						VehicleMaintenanceEntryData vehicleMaintenanceEntryData = new VehicleMaintenanceEntryData();
						vehicleMaintenanceEntryData = Factory.MaintenanceEntrySystem.GetMaintenanceScheduleBySourceID(SystemDocID, id);
						if (vehicleMaintenanceEntryData != null && vehicleMaintenanceEntryData.Tables.Count != 0 && vehicleMaintenanceEntryData.Tables[0].Rows.Count != 0)
						{
							DataRow dataRow = vehicleMaintenanceEntryData.MaintenanceEntryTable.Rows[0];
							entrysysdocID = dataRow["SysDocID"].ToString();
							entryvoucherID = dataRow["VoucherID"].ToString();
							if (entryvoucherID != null || entryvoucherID != "")
							{
								labelStatus.Visible = true;
								baLinkLabelMS.Visible = true;
								baLinkLabelMS.Text = dataRow["VoucherID"].ToString();
							}
						}
						else
						{
							baLinkLabelMS.Visible = false;
							labelStatus.Visible = false;
						}
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

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxOdometer.Text = dataRow["Odometer"].ToString();
				textBoxAmount.Text = dataRow["Amount"].ToString();
				comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				comboBoxServiceType.SelectedID = dataRow["ServiceType"].ToString();
				comboBoxServiceProvider.SelectedID = dataRow["ServiceProvider"].ToString();
				textBoxTimeRequired.Text = dataRow["TimeRequired"].ToString();
				comboBoxVehicle.SelectedID = dataRow["VehicleNumber"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				dateTimePickerMaintenanceDate.Value = DateTime.Parse(dataRow["MaintenanceDate"].ToString());
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
				bool flag = (!isNewRecord) ? Factory.MaintenanceSchedulerSystem.UpdateMaintenanceScheduler(currentData, !isNewRecord) : Factory.MaintenanceSchedulerSystem.CreateMaintenanceScheduler(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
					if (Flag)
					{
						Close();
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Vehicle_Maintenance_Scheduler", "VoucherID", textBoxVehicleNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxVehicleNumber.Text.Trim().Length == 0 || textBoxVoucherNumber.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxServiceProvider.SelectedID == "" || comboBoxServiceProvider.SelectedID == null)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Vehicle_Maintenance_Scheduler", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
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
			textBoxOdometer.Clear();
			textBoxAmount.Clear();
			textBoxTimeRequired.Clear();
			textBoxNote.Clear();
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			comboBoxServiceProvider.Clear();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			formManager.ResetDirty();
			textBoxServiceProvider.Clear();
			textBoxVehicleNumber.Clear();
			comboBoxServiceType.Clear();
			comboBoxVehicle.Clear();
			formManager.ResetDirty();
			IsVoid = false;
		}

		private void AdjustmentTypeGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void AdjustmentTypeGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.MaintenanceSchedulerSystem.DeleteMaintenanceScheduler(textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Vehicle_Maintenance_Scheduler", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Vehicle_Maintenance_Scheduler", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Vehicle_Maintenance_Scheduler", "VoucherID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Vehicle_Maintenance_Scheduler", "VoucherID"));
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Vehicle_Maintenance_Scheduler", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void MaintenanceScheduleForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				labelStatus.Visible = false;
				baLinkLabelMS.Visible = false;
				comboBoxSysDoc.FilterByType(SysDocTypes.MaintenanceScheduler);
				if (!base.IsDisposed)
				{
					if (nextServiceDate.ToString() == "")
					{
						dateTimePickerMaintenanceDate.Value = DateTime.Now;
					}
					if (!flag)
					{
						IsNewRecord = true;
						ClearForm();
					}
					else
					{
						formManager.IsForcedDirty = true;
					}
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.MaintenanceSchedulerListObj);
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditServiceProvider(comboBoxServiceProvider.SelectedID);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void labelOdodmeter_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.MaintenanceScheduler);
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
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

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void comboBoxSysDoc_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
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
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet maintenanceSchedulerToPrint = Factory.MaintenanceSchedulerSystem.GetMaintenanceSchedulerToPrint(selectedID, text);
					if (maintenanceSchedulerToPrint == null || maintenanceSchedulerToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(maintenanceSchedulerToPrint, selectedID, "Maintenance Scheduler", SysDocTypes.MaintenanceScheduler, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
				return Factory.MaintenanceSchedulerSystem.VoidMaintenance(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void toolStripButton5_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Vehicle_Maintenance_Scheduler", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Vehicle_Maintenance_Scheduler", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Vehicle_Maintenance_Scheduler", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Vehicle_Maintenance_Scheduler", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		private void toolStripButton8_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButton7_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVehicle(comboBoxVehicle.SelectedID);
		}

		private void labelVoided_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditServiceProvider(comboBoxServiceProvider.SelectedID);
		}

		private void toolStripButton9_Click(object sender, EventArgs e)
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
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
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

		private void toolStripButtonOpenList_Click_1(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.MaintenanceSchedulerListObj);
		}

		private void toolStripButton6_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Vehicle_Maintenance_Scheduler", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditServiceItem(comboBoxServiceType.SelectedID);
		}

		private void toolStripButtonVerify_Click(object sender, EventArgs e)
		{
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButton6.PerformClick();
			}
		}

		private void baLinkLabelMS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.MaintenanceScheduleFormObj);
			if (entryvoucherID != "")
			{
				FormActivator.VehicleMaintenanceEntryFormObj.EditDocument(entrysysdocID, entryvoucherID);
			}
		}

		private void vehicleComboBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.MaintenanceSchedulerForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			labelOdodmeter = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			labelDate = new System.Windows.Forms.Label();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButton6 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonVerify = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton9 = new System.Windows.Forms.ToolStripButton();
			labelVoided = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelStatus = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxServiceItem = new Micromind.UISupport.MMTextBox();
			textBoxOdometer = new Micromind.UISupport.NumberTextBox();
			baLinkLabelMS = new Micromind.UISupport.BALinkLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			textBoxServiceProvider = new Micromind.UISupport.MMTextBox();
			textBoxVehicleNumber = new Micromind.UISupport.MMTextBox();
			dateTimePickerMaintenanceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxTimeRequired = new Micromind.UISupport.MMTextBox();
			comboBoxServiceType = new Micromind.DataControls.ServiceItemComboBox();
			comboBoxVehicle = new Micromind.DataControls.VehicleComboBox();
			comboBoxServiceProvider = new Micromind.DataControls.ServiceProviderComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			toolStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 273);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(607, 40);
			panelButtons.TabIndex = 11;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(99, 24);
			buttonVoid.TabIndex = 3;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(607, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(321, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 4;
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
			xpButton1.Location = new System.Drawing.Point(499, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 5;
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
			buttonNew.TabIndex = 2;
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
			buttonSave.TabIndex = 1;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(375, 85);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			checkBoxInactive.Visible = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(6, 39);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel1.TabIndex = 13;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Doc ID:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance3;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(219, 39);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(77, 15);
			linkLabelVoucherNumber.TabIndex = 139;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance4;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(300, 36);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(95, 20);
			textBoxVoucherNumber.TabIndex = 1;
			labelOdodmeter.AutoSize = true;
			labelOdodmeter.Location = new System.Drawing.Point(6, 84);
			labelOdodmeter.Name = "labelOdodmeter";
			labelOdodmeter.Size = new System.Drawing.Size(56, 13);
			labelOdodmeter.TabIndex = 15;
			labelOdodmeter.Text = "Odometer:";
			labelOdodmeter.Click += new System.EventHandler(labelOdodmeter_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 153);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(94, 13);
			label1.TabIndex = 18;
			label1.Text = "Expected Amount:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(194, 152);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(112, 13);
			label2.TabIndex = 19;
			label2.Text = "Expected Down Time:";
			labelDate.AutoSize = true;
			labelDate.Location = new System.Drawing.Point(403, 41);
			labelDate.Name = "labelDate";
			labelDate.Size = new System.Drawing.Size(81, 13);
			labelDate.TabIndex = 20;
			labelDate.Text = "Schedule Date:";
			toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButton2,
				toolStripButton3,
				toolStripButton4,
				toolStripButton5,
				toolStripSeparator6,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButton6,
				toolStripSeparator7,
				toolStripButtonAttach,
				toolStripSeparator1,
				toolStripButtonPreview,
				toolStripButtonPrint,
				toolStripButtonVerify,
				toolStripSeparator8,
				toolStripButton9
			});
			toolStrip2.Location = new System.Drawing.Point(20, 0);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(587, 31);
			toolStrip2.TabIndex = 12;
			toolStrip2.Text = "toolStrip2";
			toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton2.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(28, 28);
			toolStripButton2.Text = "First Record";
			toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
			toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton3.Image = (System.Drawing.Image)resources.GetObject("toolStripButton3.Image");
			toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton3.Name = "toolStripButton3";
			toolStripButton3.Size = new System.Drawing.Size(28, 28);
			toolStripButton3.Text = "Previous Record";
			toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
			toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton4.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton4.Name = "toolStripButton4";
			toolStripButton4.Size = new System.Drawing.Size(28, 28);
			toolStripButton4.Text = "Next Record";
			toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
			toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton5.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton5.Name = "toolStripButton5";
			toolStripButton5.Size = new System.Drawing.Size(28, 28);
			toolStripButton5.Text = "Last Record";
			toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click_1);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButton6.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton6.Name = "toolStripButton6";
			toolStripButton6.Size = new System.Drawing.Size(58, 28);
			toolStripButton6.Text = "Find";
			toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButton8_Click);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButton7_Click);
			toolStripButtonVerify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonVerify.Image = Micromind.ClientUI.Properties.Resources.Circulation;
			toolStripButtonVerify.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonVerify.Name = "toolStripButtonVerify";
			toolStripButtonVerify.Size = new System.Drawing.Size(28, 28);
			toolStripButtonVerify.Text = "Update Service";
			toolStripButtonVerify.Visible = false;
			toolStripButtonVerify.Click += new System.EventHandler(toolStripButtonVerify_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
			toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton9.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton9.Name = "toolStripButton9";
			toolStripButton9.Size = new System.Drawing.Size(28, 28);
			toolStripButton9.Text = "Document Information";
			toolStripButton9.Click += new System.EventHandler(toolStripButton9_Click);
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(312, 243);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(295, 25);
			labelVoided.TabIndex = 142;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			labelVoided.Click += new System.EventHandler(labelVoided_Click);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance5;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(6, 61);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel3.TabIndex = 145;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Vehicle:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			labelStatus.AutoSize = true;
			labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatus.ForeColor = System.Drawing.Color.Maroon;
			labelStatus.Location = new System.Drawing.Point(12, 244);
			labelStatus.Name = "labelStatus";
			labelStatus.Size = new System.Drawing.Size(191, 17);
			labelStatus.TabIndex = 150;
			labelStatus.Text = "Entry Transaction Completed";
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(6, 107);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(69, 14);
			ultraFormattedLinkLabel4.TabIndex = 151;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Service Item:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance7;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance8.FontData.BoldAsString = "True";
			appearance8.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance8;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(6, 128);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(99, 15);
			ultraFormattedLinkLabel5.TabIndex = 152;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Service Provider:";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance9;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxServiceItem.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServiceItem.CustomReportFieldName = "";
			textBoxServiceItem.CustomReportKey = "";
			textBoxServiceItem.CustomReportValueType = 1;
			textBoxServiceItem.IsComboTextBox = false;
			textBoxServiceItem.IsModified = false;
			textBoxServiceItem.Location = new System.Drawing.Point(284, 104);
			textBoxServiceItem.MaxLength = 64;
			textBoxServiceItem.Name = "textBoxServiceItem";
			textBoxServiceItem.ReadOnly = true;
			textBoxServiceItem.Size = new System.Drawing.Size(253, 20);
			textBoxServiceItem.TabIndex = 153;
			textBoxServiceItem.TabStop = false;
			textBoxOdometer.AllowDecimal = false;
			textBoxOdometer.BackColor = System.Drawing.Color.White;
			textBoxOdometer.CustomReportFieldName = "";
			textBoxOdometer.CustomReportKey = "";
			textBoxOdometer.CustomReportValueType = 1;
			textBoxOdometer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxOdometer.IsComboTextBox = false;
			textBoxOdometer.IsModified = false;
			textBoxOdometer.Location = new System.Drawing.Point(114, 82);
			textBoxOdometer.MaxLength = 15;
			textBoxOdometer.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxOdometer.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxOdometer.Name = "textBoxOdometer";
			textBoxOdometer.NullText = "0";
			textBoxOdometer.Size = new System.Drawing.Size(167, 20);
			textBoxOdometer.TabIndex = 5;
			textBoxOdometer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			baLinkLabelMS.AutoSize = true;
			baLinkLabelMS.AvailableInEdition = true;
			baLinkLabelMS.Description = "";
			baLinkLabelMS.LinkArea = new System.Windows.Forms.LinkArea(0, 53);
			baLinkLabelMS.Location = new System.Drawing.Point(209, 248);
			baLinkLabelMS.Name = "baLinkLabelMS";
			baLinkLabelMS.OriginalText = "";
			baLinkLabelMS.Size = new System.Drawing.Size(24, 17);
			baLinkLabelMS.TabIndex = 149;
			baLinkLabelMS.TabStop = true;
			baLinkLabelMS.Text = "BL1";
			baLinkLabelMS.ToBeAligned = true;
			baLinkLabelMS.UseCompatibleTextRendering = true;
			baLinkLabelMS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(baLinkLabelMS_LinkClicked);
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(114, 149);
			textBoxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.NullText = "0";
			textBoxAmount.Size = new System.Drawing.Size(74, 20);
			textBoxAmount.TabIndex = 9;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxServiceProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServiceProvider.CustomReportFieldName = "";
			textBoxServiceProvider.CustomReportKey = "";
			textBoxServiceProvider.CustomReportValueType = 1;
			textBoxServiceProvider.IsComboTextBox = false;
			textBoxServiceProvider.IsModified = false;
			textBoxServiceProvider.Location = new System.Drawing.Point(284, 127);
			textBoxServiceProvider.MaxLength = 64;
			textBoxServiceProvider.Name = "textBoxServiceProvider";
			textBoxServiceProvider.ReadOnly = true;
			textBoxServiceProvider.Size = new System.Drawing.Size(253, 20);
			textBoxServiceProvider.TabIndex = 8;
			textBoxServiceProvider.TabStop = false;
			textBoxVehicleNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVehicleNumber.CustomReportFieldName = "";
			textBoxVehicleNumber.CustomReportKey = "";
			textBoxVehicleNumber.CustomReportValueType = 1;
			textBoxVehicleNumber.IsComboTextBox = false;
			textBoxVehicleNumber.IsModified = false;
			textBoxVehicleNumber.Location = new System.Drawing.Point(284, 59);
			textBoxVehicleNumber.MaxLength = 64;
			textBoxVehicleNumber.Name = "textBoxVehicleNumber";
			textBoxVehicleNumber.ReadOnly = true;
			textBoxVehicleNumber.Size = new System.Drawing.Size(252, 20);
			textBoxVehicleNumber.TabIndex = 4;
			textBoxVehicleNumber.TabStop = false;
			dateTimePickerMaintenanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerMaintenanceDate.Location = new System.Drawing.Point(489, 37);
			dateTimePickerMaintenanceDate.Name = "dateTimePickerMaintenanceDate";
			dateTimePickerMaintenanceDate.Size = new System.Drawing.Size(109, 20);
			dateTimePickerMaintenanceDate.TabIndex = 2;
			dateTimePickerMaintenanceDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 341);
			textBoxTimeRequired.BackColor = System.Drawing.Color.White;
			textBoxTimeRequired.CustomReportFieldName = "";
			textBoxTimeRequired.CustomReportKey = "";
			textBoxTimeRequired.CustomReportValueType = 1;
			textBoxTimeRequired.IsComboTextBox = false;
			textBoxTimeRequired.IsModified = false;
			textBoxTimeRequired.Location = new System.Drawing.Point(312, 149);
			textBoxTimeRequired.MaxLength = 64;
			textBoxTimeRequired.Name = "textBoxTimeRequired";
			textBoxTimeRequired.Size = new System.Drawing.Size(225, 20);
			textBoxTimeRequired.TabIndex = 10;
			comboBoxServiceType.Assigned = false;
			comboBoxServiceType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxServiceType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxServiceType.CustomReportFieldName = "";
			comboBoxServiceType.CustomReportKey = "";
			comboBoxServiceType.CustomReportValueType = 1;
			comboBoxServiceType.DescriptionTextBox = textBoxServiceItem;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			appearance10.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxServiceType.DisplayLayout.Appearance = appearance10;
			comboBoxServiceType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxServiceType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance11.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceType.DisplayLayout.GroupByBox.Appearance = appearance11;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance12;
			comboBoxServiceType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance13.BackColor2 = System.Drawing.SystemColors.Control;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceType.DisplayLayout.GroupByBox.PromptAppearance = appearance13;
			comboBoxServiceType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxServiceType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxServiceType.DisplayLayout.Override.ActiveCellAppearance = appearance14;
			appearance15.BackColor = System.Drawing.SystemColors.Highlight;
			appearance15.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxServiceType.DisplayLayout.Override.ActiveRowAppearance = appearance15;
			comboBoxServiceType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxServiceType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			comboBoxServiceType.DisplayLayout.Override.CardAreaAppearance = appearance16;
			appearance17.BorderColor = System.Drawing.Color.Silver;
			appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxServiceType.DisplayLayout.Override.CellAppearance = appearance17;
			comboBoxServiceType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxServiceType.DisplayLayout.Override.CellPadding = 0;
			appearance18.BackColor = System.Drawing.SystemColors.Control;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceType.DisplayLayout.Override.GroupByRowAppearance = appearance18;
			appearance19.TextHAlignAsString = "Left";
			comboBoxServiceType.DisplayLayout.Override.HeaderAppearance = appearance19;
			comboBoxServiceType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxServiceType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			comboBoxServiceType.DisplayLayout.Override.RowAppearance = appearance20;
			comboBoxServiceType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxServiceType.DisplayLayout.Override.TemplateAddRowAppearance = appearance21;
			comboBoxServiceType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxServiceType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxServiceType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxServiceType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxServiceType.Editable = true;
			comboBoxServiceType.FilterString = "";
			comboBoxServiceType.HasAllAccount = false;
			comboBoxServiceType.HasCustom = false;
			comboBoxServiceType.IsDataLoaded = false;
			comboBoxServiceType.Location = new System.Drawing.Point(114, 104);
			comboBoxServiceType.MaxDropDownItems = 12;
			comboBoxServiceType.Name = "comboBoxServiceType";
			comboBoxServiceType.ShowInactiveItems = false;
			comboBoxServiceType.ShowQuickAdd = true;
			comboBoxServiceType.Size = new System.Drawing.Size(167, 20);
			comboBoxServiceType.TabIndex = 6;
			comboBoxServiceType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicle.Assigned = false;
			comboBoxVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicle.CustomReportFieldName = "";
			comboBoxVehicle.CustomReportKey = "";
			comboBoxVehicle.CustomReportValueType = 1;
			comboBoxVehicle.DescriptionTextBox = textBoxVehicleNumber;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicle.DisplayLayout.Appearance = appearance22;
			comboBoxVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance23.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.GroupByBox.Appearance = appearance23;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance24;
			comboBoxVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance25.BackColor2 = System.Drawing.SystemColors.Control;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance25;
			comboBoxVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance26;
			appearance27.BackColor = System.Drawing.SystemColors.Highlight;
			appearance27.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance27;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.CardAreaAppearance = appearance28;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			appearance29.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicle.DisplayLayout.Override.CellAppearance = appearance29;
			comboBoxVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance30.BackColor = System.Drawing.SystemColors.Control;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance30;
			appearance31.TextHAlignAsString = "Left";
			comboBoxVehicle.DisplayLayout.Override.HeaderAppearance = appearance31;
			comboBoxVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicle.DisplayLayout.Override.RowAppearance = appearance32;
			comboBoxVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance33;
			comboBoxVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicle.Editable = true;
			comboBoxVehicle.FilterString = "";
			comboBoxVehicle.HasAllAccount = false;
			comboBoxVehicle.HasCustom = false;
			comboBoxVehicle.IsDataLoaded = false;
			comboBoxVehicle.Location = new System.Drawing.Point(114, 59);
			comboBoxVehicle.MaxDropDownItems = 12;
			comboBoxVehicle.Name = "comboBoxVehicle";
			comboBoxVehicle.ShowInactiveItems = false;
			comboBoxVehicle.ShowQuickAdd = true;
			comboBoxVehicle.Size = new System.Drawing.Size(167, 20);
			comboBoxVehicle.TabIndex = 3;
			comboBoxVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicle.TextChanged += new System.EventHandler(vehicleComboBox1_TextChanged);
			comboBoxServiceProvider.Assigned = false;
			comboBoxServiceProvider.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxServiceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxServiceProvider.CustomReportFieldName = "";
			comboBoxServiceProvider.CustomReportKey = "";
			comboBoxServiceProvider.CustomReportValueType = 1;
			comboBoxServiceProvider.DescriptionTextBox = textBoxServiceProvider;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxServiceProvider.DisplayLayout.Appearance = appearance34;
			comboBoxServiceProvider.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxServiceProvider.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance35.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.Appearance = appearance35;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.BandLabelAppearance = appearance36;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance37.BackColor2 = System.Drawing.SystemColors.Control;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.PromptAppearance = appearance37;
			comboBoxServiceProvider.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxServiceProvider.DisplayLayout.MaxRowScrollRegions = 1;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxServiceProvider.DisplayLayout.Override.ActiveCellAppearance = appearance38;
			appearance39.BackColor = System.Drawing.SystemColors.Highlight;
			appearance39.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxServiceProvider.DisplayLayout.Override.ActiveRowAppearance = appearance39;
			comboBoxServiceProvider.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxServiceProvider.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			comboBoxServiceProvider.DisplayLayout.Override.CardAreaAppearance = appearance40;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			appearance41.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxServiceProvider.DisplayLayout.Override.CellAppearance = appearance41;
			comboBoxServiceProvider.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxServiceProvider.DisplayLayout.Override.CellPadding = 0;
			appearance42.BackColor = System.Drawing.SystemColors.Control;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceProvider.DisplayLayout.Override.GroupByRowAppearance = appearance42;
			appearance43.TextHAlignAsString = "Left";
			comboBoxServiceProvider.DisplayLayout.Override.HeaderAppearance = appearance43;
			comboBoxServiceProvider.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxServiceProvider.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			comboBoxServiceProvider.DisplayLayout.Override.RowAppearance = appearance44;
			comboBoxServiceProvider.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance45.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxServiceProvider.DisplayLayout.Override.TemplateAddRowAppearance = appearance45;
			comboBoxServiceProvider.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxServiceProvider.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxServiceProvider.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxServiceProvider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxServiceProvider.Editable = true;
			comboBoxServiceProvider.FilterString = "";
			comboBoxServiceProvider.FilterSysDocID = "";
			comboBoxServiceProvider.HasAll = false;
			comboBoxServiceProvider.HasCustom = false;
			comboBoxServiceProvider.IsDataLoaded = false;
			comboBoxServiceProvider.Location = new System.Drawing.Point(114, 127);
			comboBoxServiceProvider.MaxDropDownItems = 12;
			comboBoxServiceProvider.Name = "comboBoxServiceProvider";
			comboBoxServiceProvider.ShowConsignmentOnly = false;
			comboBoxServiceProvider.ShowQuickAdd = true;
			comboBoxServiceProvider.Size = new System.Drawing.Size(167, 20);
			comboBoxServiceProvider.TabIndex = 7;
			comboBoxServiceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance46;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance47.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance47;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance48;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance49.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance49.BackColor2 = System.Drawing.SystemColors.Control;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance49;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance50;
			appearance51.BackColor = System.Drawing.SystemColors.Highlight;
			appearance51.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance51;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance52;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			appearance53.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance53;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance54.BackColor = System.Drawing.SystemColors.Control;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance54;
			appearance55.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance55;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance56;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance57;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(114, 36);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(102, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.SelectedIndexChanged += new System.EventHandler(comboBoxSysDoc_SelectedIndexChanged);
			comboBoxSysDoc.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxSysDoc_InitializeLayout);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 12;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxNote.Location = new System.Drawing.Point(114, 172);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(422, 68);
			textBoxNote.TabIndex = 11;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(6, 176);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 155;
			label3.Text = "Note:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(607, 313);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxServiceItem);
			base.Controls.Add(textBoxOdometer);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(labelStatus);
			base.Controls.Add(baLinkLabelMS);
			base.Controls.Add(comboBoxServiceType);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(labelVoided);
			base.Controls.Add(toolStrip2);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(textBoxServiceProvider);
			base.Controls.Add(textBoxVehicleNumber);
			base.Controls.Add(comboBoxVehicle);
			base.Controls.Add(dateTimePickerMaintenanceDate);
			base.Controls.Add(labelDate);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxTimeRequired);
			base.Controls.Add(label1);
			base.Controls.Add(comboBoxServiceProvider);
			base.Controls.Add(labelOdodmeter);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(linkLabelVoucherNumber);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "MaintenanceSchedulerForm";
			Text = "Vehicle Maintenance Scheduler";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(MaintenanceScheduleForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip2.ResumeLayout(false);
			toolStrip2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
