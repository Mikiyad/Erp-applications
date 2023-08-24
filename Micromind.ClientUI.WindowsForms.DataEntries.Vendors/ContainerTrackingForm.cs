using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
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
	public class ContainerTrackingForm : Form, IForm
	{
		private bool allowEdit = true;

		private string sourceSysDocID = string.Empty;

		private string sourceVoucherID = string.Empty;

		private int lastStatus;

		private int currentIndex;

		private ContainerTrackingData currentData;

		private const string TABLENAME_CONST = "Container_Tracking";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private bool allowReceiveMoreQuantity = CompanyPreferences.AllowIPurchaseQtyMoreThanPO;

		private bool allowImportGRNPackingListAddNew = CompanyPreferences.AllowImportGRNPackingListAddNew;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

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

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private TextBox textBoxContainerNumber;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem purchaseStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private Label label2;

		private Label label4;

		private Label label14;

		private TextBox textBoxClearingAgent;

		private Label label13;

		private TextBox textBoxBOL;

		private MMSDateTimePicker dateTimePickerETA;

		private QuantityTextBox textBoxWeight;

		private Label label15;

		private Label label16;

		private TextBox textBoxShipper;

		private AmountTextBox textBoxValue;

		private Label label5;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label12;

		private MMSDateTimePicker dateTimePickerATD;

		private Label label11;

		private XPButton buttonSelectDocument;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private Label label9;

		private UltraGroupBox ultraGroupBox1;

		private Panel panel1;

		private Label label17;

		private TextBox textBoxTruckNumber;

		private Label labelTrcukNumber;

		private MMLabel mmLabel6;

		private UltraFormattedLinkLabel labelTransporter;

		private ContainerStstusComboBox comboBoxContainerStatus;

		private SysDocComboBox comboBoxSysDoc;

		private TransporterComboBox comboBoxTransporter;

		private Label labelVoided;

		private Label label22;

		private MMTextBox textBoxRemarks;

		private TextBox textBoxNote;

		private Label label3;

		private vendorsFlatComboBox comboBoxVendor;

		private TextBox textBoxVendor;

		private TransporterComboBox transporterComboBox1;

		private ContainerSizeComboBox comboBoxContainerSize;

		private PortComboBox comboBoxSourcePort;

		private PortComboBox comboBoxDestPort;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator3;

		private UltraGroupBox groupBoxDocuments;

		private CheckBox checkBoxBL;

		private CheckBox checkBoxOriginCertificate;

		private CheckBox checkBoxhealthCertificate;

		private CheckBox checkBoxPL;

		private CheckBox checkBoxInvoice;

		private TextBox textBoxDriver;

		private UltraGroupBox groupBoxDelivery;

		private MMSDateTimePicker dateTimePickerFreeTimeDate;

		private DateTimePicker dateTimePickerFreeTime;

		private Label labelDeliveryFreeTime;

		private NumberTextBox textBoxDays;

		private RadioButton radioButtonDays;

		private RadioButton radioButtonDate;

		private Label labelDriver;

		private XPButton xpButton2;

		public ScreenAreas ScreenArea => ScreenAreas.Purchases;

		public int ScreenID => 3007;

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
					comboBoxSysDoc.Enabled = true;
					textBoxVoucherNumber.ReadOnly = false;
					comboBoxContainerStatus.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					comboBoxSysDoc.Enabled = false;
					textBoxVoucherNumber.ReadOnly = true;
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

		public ContainerTrackingForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void docStatusLabel_LinkClicked(object sender, EventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			Control control = sender as Control;
			if (control != null)
			{
				formHelper.EditTransaction(TransactionListType.ImportGRN, control.Tag.ToString(), control.Text);
			}
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
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ContainerTrackingData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ContainerTrackingTable.Rows[0] : currentData.ContainerTrackingTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["VendorID"] = comboBoxVendor.SelectedID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["ContainerNumber"] = textBoxContainerNumber.Text;
				dataRow["BOLNumber"] = textBoxBOL.Text;
				if (dateTimePickerETA.Checked)
				{
					dataRow["ETA"] = dateTimePickerETA.Value;
				}
				else
				{
					dataRow["ETA"] = DBNull.Value;
				}
				if (dateTimePickerATD.Checked)
				{
					dataRow["ATD"] = dateTimePickerATD.Value;
				}
				else
				{
					dataRow["ATD"] = DBNull.Value;
				}
				dataRow["Weight"] = textBoxWeight.Text;
				dataRow["Shipper"] = textBoxShipper.Text;
				dataRow["Note"] = textBoxNote.Text;
				if (comboBoxDestPort.SelectedID != "")
				{
					dataRow["DestinationPort"] = comboBoxDestPort.SelectedID;
				}
				else
				{
					dataRow["DestinationPort"] = DBNull.Value;
				}
				if (comboBoxSourcePort.SelectedID != "")
				{
					dataRow["LoadingPort"] = comboBoxSourcePort.SelectedID;
				}
				else
				{
					dataRow["LoadingPort"] = DBNull.Value;
				}
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				dataRow["TransporterID"] = comboBoxTransporter.SelectedID;
				dataRow["ContainerSizeID"] = comboBoxContainerSize.SelectedID;
				dataRow["Container_Status"] = int.Parse(comboBoxContainerStatus.SelectedID.ToString());
				dataRow["Remarks"] = textBoxRemarks.Text;
				dataRow["DriverID"] = textBoxDriver.Text;
				dataRow["TransportCompany"] = comboBoxTransporter.SelectedID;
				dataRow["TruckNumber"] = textBoxTruckNumber.Text;
				dataRow["Note"] = textBoxNote.Text;
				if (dateTimePickerFreeTime.Value.ToString() != "" && dateTimePickerFreeTimeDate.Checked)
				{
					dataRow["FreeTimeTODeliver"] = dateTimePickerFreeTimeDate.Value.Date.Add(dateTimePickerFreeTime.Value.TimeOfDay);
				}
				else
				{
					dataRow["FreeTimeTODeliver"] = DBNull.Value;
				}
				if (dateTimePickerFreeTimeDate.Value.ToString() != "" && dateTimePickerFreeTimeDate.Checked)
				{
					dataRow["DeliveryDate"] = dateTimePickerFreeTimeDate.Value;
				}
				else
				{
					dataRow["DeliveryDate"] = DBNull.Value;
				}
				if (radioButtonDays.Checked)
				{
					dataRow["DeliveryDate"] = dateTimePickerDate.Value.AddDays(double.Parse(textBoxDays.Text));
					dataRow["FreeTimeTODeliver"] = DBNull.Value;
				}
				if (checkBoxBL.Checked)
				{
					dataRow["IsBL"] = true;
				}
				else
				{
					dataRow["IsBL"] = false;
				}
				if (checkBoxInvoice.Checked)
				{
					dataRow["IsInvoice"] = true;
				}
				else
				{
					dataRow["IsInvoice"] = false;
				}
				if (checkBoxPL.Checked)
				{
					dataRow["IsPL"] = true;
				}
				else
				{
					dataRow["IsPL"] = false;
				}
				if (checkBoxhealthCertificate.Checked)
				{
					dataRow["IsHealthCertficate"] = true;
				}
				else
				{
					dataRow["IsHealthCertficate"] = false;
				}
				if (checkBoxOriginCertificate.Checked)
				{
					dataRow["IsCertificateOfOrigin"] = true;
				}
				else
				{
					dataRow["IsCertificateOfOrigin"] = false;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ContainerTrackingTable.Rows.Add(dataRow);
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
					currentData = Factory.ContainerStrackingSystem.GetContainerTrackingByID(voucherID);
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
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Container_Tracking"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxContainerNumber.Text = dataRow["ContainerNumber"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
					textBoxShipper.Text = dataRow["Shipper"].ToString();
					textBoxBOL.Text = dataRow["BOLNumber"].ToString();
					if (dataRow["Weight"] != DBNull.Value)
					{
						textBoxWeight.Text = decimal.Parse(dataRow["Weight"].ToString()).ToString(Format.QuantityFormat);
					}
					else
					{
						textBoxWeight.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["ETA"] != DBNull.Value)
					{
						dateTimePickerETA.Value = DateTime.Parse(dataRow["ETA"].ToString());
						dateTimePickerETA.Checked = true;
					}
					else
					{
						dateTimePickerETA.Checked = false;
					}
					if (dataRow["ATD"] != DBNull.Value)
					{
						dateTimePickerATD.Value = DateTime.Parse(dataRow["ATD"].ToString());
						dateTimePickerATD.Checked = true;
					}
					else
					{
						dateTimePickerATD.Checked = false;
					}
					comboBoxDestPort.SelectedID = dataRow["DestinationPort"].ToString();
					comboBoxSourcePort.SelectedID = dataRow["LoadingPort"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
					comboBoxContainerSize.SelectedID = dataRow["ContainerSizeID"].ToString();
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					comboBoxContainerStatus.SelectedID = int.Parse(dataRow["Container_Status"].ToString());
					lastStatus = int.Parse(dataRow["Container_Status"].ToString());
					if (dataRow["DriverID"] != DBNull.Value)
					{
						textBoxDriver.Text = dataRow["DriverID"].ToString();
					}
					if (dataRow["FreeTimeTODeliver"] != DBNull.Value)
					{
						dateTimePickerFreeTime.Value = DateTime.Parse(dataRow["FreeTimeTODeliver"].ToString());
					}
					else
					{
						dateTimePickerFreeTime.Checked = false;
					}
					if (dataRow["DeliveryDate"] != DBNull.Value)
					{
						dateTimePickerFreeTimeDate.Value = DateTime.Parse(dataRow["DeliveryDate"].ToString());
						dateTimePickerFreeTimeDate.Checked = true;
					}
					else
					{
						dateTimePickerFreeTimeDate.Checked = false;
					}
					comboBoxTransporter.SelectedID = dataRow["TransportCompany"].ToString();
					textBoxRemarks.Text = dataRow["Remarks"].ToString();
					textBoxTruckNumber.Text = dataRow["TruckNumber"].ToString();
					if (comboBoxContainerStatus.SelectedID == 2)
					{
						if (dataRow["IsBL"] != DBNull.Value)
						{
							checkBoxBL.Checked = bool.Parse(dataRow["IsBL"].ToString());
						}
						else
						{
							checkBoxBL.Checked = false;
						}
						if (dataRow["IsInvoice"] != DBNull.Value)
						{
							checkBoxInvoice.Checked = bool.Parse(dataRow["IsInvoice"].ToString());
						}
						else
						{
							checkBoxInvoice.Checked = false;
						}
						if (dataRow["IsPL"] != DBNull.Value)
						{
							checkBoxPL.Checked = bool.Parse(dataRow["IsPL"].ToString());
						}
						else
						{
							checkBoxPL.Checked = false;
						}
						if (dataRow["IsHealthCertficate"] != DBNull.Value)
						{
							checkBoxhealthCertificate.Checked = bool.Parse(dataRow["IsHealthCertficate"].ToString());
						}
						else
						{
							checkBoxhealthCertificate.Checked = false;
						}
						if (dataRow["IsCertificateOfOrigin"] != DBNull.Value)
						{
							checkBoxOriginCertificate.Checked = bool.Parse(dataRow["IsCertificateOfOrigin"].ToString());
						}
						else
						{
							checkBoxOriginCertificate.Checked = false;
						}
					}
					comboBoxContainerStatus.Enabled = false;
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
				bool flag = Factory.ContainerStrackingSystem.CreateContainerTracking(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					lastStatus = 0;
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
					ClearForm();
					IsNewRecord = true;
					comboBoxContainerStatus.Clear();
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Container_Tracking", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:116).");
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
			if (timeSpan.Days <= checked(num + 1))
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
			int num2 = int.Parse(comboBoxContainerStatus.SelectedID.ToString());
			if (IsNewRecord && lastStatus == num2)
			{
				ErrorHelper.WarningMessage("This Status is Already Entered");
				textBoxVoucherNumber.Focus();
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Container_Tracking", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			try
			{
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxContainerNumber.Clear();
				comboBoxContainerSize.Clear();
				comboBoxVendor.Clear();
				textBoxVendor.Clear();
				lastStatus = 0;
				currentIndex = 0;
				comboBoxContainerStatus.Clear();
				textBoxBOL.Clear();
				textBoxClearingAgent.Clear();
				textBoxContainerNumber.Clear();
				textBoxShipper.Clear();
				textBoxWeight.Text = "0";
				comboBoxDestPort.Clear();
				comboBoxSourcePort.Clear();
				textBoxValue.Text = 0.ToString(Format.TotalAmountFormat);
				dateTimePickerETA.Value = DateTime.Now;
				dateTimePickerETA.Checked = false;
				dateTimePickerATD.Value = DateTime.Now;
				dateTimePickerATD.Checked = false;
				comboBoxShippingMethod.Clear();
				textBoxDriver.Clear();
				comboBoxTransporter.Clear();
				textBoxTruckNumber.Clear();
				textBoxRemarks.Clear();
				textBoxDays.Text = 0.ToString();
				dateTimePickerFreeTimeDate.Clear();
				dateTimePickerFreeTime.Checked = false;
				comboBoxTransporter.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.ContainerStrackingSystem.DeleteContainerTracking(textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Container_Tracking", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Container_Tracking", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Container_Tracking", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Container_Tracking", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Container_Tracking", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.ContainerTracking);
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
				return Factory.ContainerStrackingSystem.VoidTracking(SystemDocID, textBoxVoucherNumber.Text, isVoid);
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ContainerTracking);
		}

		private void LoadVendorBillingAddress()
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!(currentVendorAddressID == ""))
			{
				new FormHelper().EditVendorAddress(textBoxVendor.Text, currentVendorAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(textBoxVendor.Text);
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

		private void transferToPOShipmentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsNewRecord)
				{
					ErrorHelper.InformationMessage("Please start a new transaction first.");
				}
				else
				{
					DataSet containerList = Factory.POShipmentSystem.GetContainerList();
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.ValidateSelection += form_ValidateSelection;
					selectDocumentDialog.HiddenColumns.Add("ParentVendorID");
					selectDocumentDialog.IsMultiSelect = false;
					selectDocumentDialog.DataSource = containerList;
					selectDocumentDialog.Text = "Select Container Number";
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						ClearForm();
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							sourceSysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
							sourceVoucherID = selectedRow.Cells["Number"].Value.ToString();
							string containerNumber = selectedRow.Cells["ContainerNumber"].Value.ToString();
							DataRow dataRow = Factory.ContainerStrackingSystem.GetContainerDetailsNew(containerNumber).Tables[0].Rows[0];
							if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
							{
								comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["ContainerNumber"].ToString()))
							{
								textBoxContainerNumber.Text = dataRow["ContainerNumber"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["ContainerSizeID"].ToString()))
							{
								comboBoxContainerSize.SelectedID = dataRow["ContainerSizeID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["VendorID"].ToString()))
							{
								comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["DestinationPort"].ToString()))
							{
								comboBoxDestPort.SelectedID = dataRow["DestinationPort"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["LoadingPort"].ToString()))
							{
								comboBoxSourcePort.SelectedID = dataRow["LoadingPort"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["Shipper"].ToString()))
							{
								textBoxShipper.Text = dataRow["Shipper"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["Weight"].ToString()))
							{
								textBoxWeight.Text = dataRow["Weight"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["BOLNumber"].ToString()))
							{
								textBoxBOL.Text = dataRow["BOLNumber"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["TransporterID"].ToString()))
							{
								comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["Port"].ToString()))
							{
								comboBoxSourcePort.SelectedID = dataRow["Port"].ToString();
							}
							if (dataRow["ETA"] != DBNull.Value)
							{
								dateTimePickerETA.Checked = true;
								dateTimePickerETA.Value = DateTime.Parse(dataRow["ETA"].ToString());
							}
							if (dataRow["ATD"] != DBNull.Value)
							{
								dateTimePickerATD.Checked = true;
								dateTimePickerATD.Value = DateTime.Parse(dataRow["ATD"].ToString());
							}
							if (dataRow["Container_Status"] != DBNull.Value)
							{
								comboBoxContainerStatus.Enabled = true;
								lastStatus = int.Parse(dataRow["Container_Status"].ToString());
								comboBoxContainerStatus.SelectedID = int.Parse(dataRow["Container_Status"].ToString());
							}
							else
							{
								comboBoxContainerStatus.SelectedID = 1;
								comboBoxContainerStatus.Enabled = false;
							}
							if (dataRow["IsBL"] != DBNull.Value)
							{
								checkBoxBL.Checked = bool.Parse(dataRow["IsBL"].ToString());
							}
							else
							{
								checkBoxBL.Checked = false;
							}
							if (dataRow["IsInvoice"] != DBNull.Value)
							{
								checkBoxInvoice.Checked = bool.Parse(dataRow["IsInvoice"].ToString());
							}
							else
							{
								checkBoxInvoice.Checked = false;
							}
							if (dataRow["IsPL"] != DBNull.Value)
							{
								checkBoxPL.Checked = bool.Parse(dataRow["IsPL"].ToString());
							}
							else
							{
								checkBoxPL.Checked = false;
							}
							if (dataRow["IsHealthCertficate"] != DBNull.Value)
							{
								checkBoxhealthCertificate.Checked = bool.Parse(dataRow["IsHealthCertficate"].ToString());
							}
							else
							{
								checkBoxhealthCertificate.Checked = false;
							}
							if (dataRow["IsCertificateOfOrigin"] != DBNull.Value)
							{
								checkBoxOriginCertificate.Checked = bool.Parse(dataRow["IsCertificateOfOrigin"].ToString());
							}
							else
							{
								checkBoxOriginCertificate.Checked = false;
							}
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
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			if (selectDocumentDialog != null && selectDocumentDialog.CanClose)
			{
				_ = selectDocumentDialog.SelectedRows;
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
					DataSet containerTrackingToPrint = Factory.ContainerStrackingSystem.GetContainerTrackingToPrint(selectedID, text);
					if (containerTrackingToPrint == null || containerTrackingToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(containerTrackingToPrint, selectedID, "Container Tracking", SysDocTypes.ContainerTracking, isPrint, showPrintDialog);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PackingList);
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

		private void toolStripMenuItemCloseOrder_Click(object sender, EventArgs e)
		{
			try
			{
				UpdatePOStatusDialog updatePOStatusDialog = new UpdatePOStatusDialog();
				updatePOStatusDialog.SysDocumentType = SysDocTypes.PackingList;
				updatePOStatusDialog.SetDocument(SysDocTypes.PackingList, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
				if (updatePOStatusDialog.ShowDialog(this) == DialogResult.OK)
				{
					Factory.DatabaseSystem.GetFieldValue("PO_Shipment", "Status", "SysDocID", comboBoxSysDoc.SelectedID, "VoucherID", textBoxVoucherNumber.Text);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			transferToPOShipmentToolStripMenuItem_Click(sender, e);
		}

		private void labelDriver_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void labelTransporter_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransporter(comboBoxTransporter.SelectedID);
		}

		private void panelDetails_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ContainerTrackingForm_Load(object sender, EventArgs e)
		{
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void ultraGroupBox1_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxContainerStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxContainerStatus.SelectedID == 2)
			{
				groupBoxDocuments.Visible = true;
				groupBoxDocuments.Enabled = true;
			}
			else if (comboBoxContainerStatus.SelectedID == 1)
			{
				groupBoxDocuments.Visible = false;
			}
			else
			{
				groupBoxDocuments.Visible = true;
				groupBoxDocuments.Enabled = false;
			}
			if (comboBoxContainerStatus.SelectedID == 6)
			{
				textBoxTruckNumber.Visible = true;
				textBoxDriver.Visible = true;
				comboBoxTransporter.Visible = true;
				textBoxDriver.Enabled = true;
				comboBoxTransporter.Enabled = true;
				textBoxTruckNumber.Enabled = true;
				dateTimePickerFreeTimeDate.Visible = true;
				dateTimePickerFreeTime.Visible = true;
				labelDriver.Visible = true;
				labelTransporter.Visible = true;
				radioButtonDate.Visible = true;
				labelDeliveryFreeTime.Visible = true;
				labelTrcukNumber.Visible = true;
				textBoxDays.Visible = true;
				radioButtonDays.Visible = true;
				groupBoxDelivery.Visible = true;
			}
			checked
			{
				if (comboBoxContainerStatus.SelectedID == 9)
				{
					textBoxTruckNumber.Visible = true;
					textBoxDriver.Visible = true;
					textBoxDriver.Enabled = false;
					comboBoxTransporter.Visible = true;
					comboBoxTransporter.Enabled = false;
					textBoxTruckNumber.Enabled = false;
					dateTimePickerFreeTimeDate.Enabled = false;
					dateTimePickerFreeTime.Enabled = false;
					dateTimePickerFreeTimeDate.Visible = true;
					dateTimePickerFreeTime.Visible = true;
					textBoxDays.ReadOnly = true;
					labelDriver.Visible = true;
					labelTransporter.Visible = true;
					radioButtonDate.Visible = true;
					labelDeliveryFreeTime.Visible = true;
					labelTrcukNumber.Visible = true;
					groupBoxDelivery.Visible = true;
					radioButtonDays.Visible = true;
					textBoxDays.Visible = true;
					ContainerTrackingData containerDetailsOnStatusChange = Factory.ContainerStrackingSystem.GetContainerDetailsOnStatusChange(textBoxContainerNumber.Text);
					if (containerDetailsOnStatusChange != null)
					{
						DataRow dataRow = containerDetailsOnStatusChange.Tables[1].Rows[0];
						textBoxDriver.Text = dataRow["DriverID"].ToString();
						textBoxTruckNumber.Text = dataRow["TruckNumber"].ToString();
						comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
						if (dataRow["FreeTimeTODeliver"] != DBNull.Value)
						{
							dateTimePickerFreeTime.Value = DateTime.Parse(dataRow["FreeTimeTODeliver"].ToString());
						}
						else
						{
							dateTimePickerFreeTime.Checked = false;
						}
						if (dataRow["DeliveryDate"] != DBNull.Value)
						{
							dateTimePickerFreeTimeDate.Value = DateTime.Parse(dataRow["DeliveryDate"].ToString());
							dateTimePickerFreeTimeDate.Checked = true;
						}
						else
						{
							dateTimePickerFreeTimeDate.Checked = false;
						}
						int num = 0;
						num = dateTimePickerDate.Value.Subtract(dateTimePickerFreeTimeDate.Value).Negate().Days + 1;
						textBoxDays.Text = num.ToString();
					}
				}
				else if (comboBoxContainerStatus.SelectedID != 6 && comboBoxContainerStatus.SelectedID != 9)
				{
					textBoxTruckNumber.Visible = false;
					textBoxDriver.Visible = false;
					comboBoxTransporter.Visible = false;
					dateTimePickerFreeTimeDate.Visible = false;
					dateTimePickerFreeTime.Visible = false;
					labelDriver.Visible = false;
					labelTransporter.Visible = false;
					radioButtonDate.Visible = false;
					labelDeliveryFreeTime.Visible = false;
					labelTrcukNumber.Visible = false;
					radioButtonDays.Visible = false;
					groupBoxDelivery.Visible = false;
					textBoxDays.Visible = false;
				}
				if (comboBoxContainerStatus.SelectedID == lastStatus)
				{
					textBoxDriver.Enabled = false;
					comboBoxTransporter.Enabled = false;
					textBoxTruckNumber.Enabled = false;
				}
				if (currentData != null)
				{
					DataRow dataRow2 = (!isNewRecord) ? currentData.ContainerTrackingTable.Rows[0] : currentData.ContainerTrackingTable.NewRow();
					if (dataRow2["Container_Status"] != DBNull.Value)
					{
						currentIndex = int.Parse(dataRow2["Container_Status"].ToString());
						if (comboBoxContainerStatus.SelectedID > currentIndex + 1 && comboBoxContainerStatus.SelectedID != 11)
						{
							ErrorHelper.ErrorMessage("Up To one higher entry or cancellation is possible only in  the status field");
							comboBoxContainerStatus.SelectedID = int.Parse(dataRow2["Container_Status"].ToString());
						}
						if (comboBoxContainerStatus.SelectedID < currentIndex && comboBoxContainerStatus.SelectedID != 11 && textBoxContainerNumber.Text != "")
						{
							ErrorHelper.ErrorMessage("You can only enter next higher status");
							comboBoxContainerStatus.SelectedID = int.Parse(dataRow2["Container_Status"].ToString());
						}
					}
				}
				if (lastStatus > 0 && isNewRecord)
				{
					if (comboBoxContainerStatus.SelectedID > lastStatus + 1 && comboBoxContainerStatus.SelectedID != 11)
					{
						ErrorHelper.ErrorMessage("Up To one higher entry or cancellation is possible only in  the status field");
						comboBoxContainerStatus.SelectedID = lastStatus;
					}
					if (comboBoxContainerStatus.SelectedID < lastStatus && comboBoxContainerStatus.SelectedID != 11)
					{
						ErrorHelper.ErrorMessage("You can only enter next higher status");
						comboBoxContainerStatus.SelectedID = lastStatus;
					}
				}
				else if (currentData == null && comboBoxContainerStatus.SelectedID > 1)
				{
					comboBoxContainerStatus.SelectedID = 1;
				}
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ContainerTrackingListFormObj);
		}

		private void comboBoxSysDoc_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
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

		private void radioButtonDays_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonDays.Checked && comboBoxContainerStatus.SelectedID != 9)
			{
				textBoxDays.ReadOnly = false;
				dateTimePickerFreeTime.Enabled = false;
				dateTimePickerFreeTimeDate.Enabled = false;
			}
			else if (comboBoxContainerStatus.SelectedID == 9)
			{
				textBoxDays.ReadOnly = true;
				dateTimePickerFreeTime.Enabled = false;
				dateTimePickerFreeTimeDate.Enabled = false;
			}
			else
			{
				textBoxDays.ReadOnly = true;
				dateTimePickerFreeTime.Enabled = true;
				dateTimePickerFreeTimeDate.Enabled = true;
			}
		}

		private void textBoxDays_TextChanged(object sender, EventArgs e)
		{
			int num = int.Parse(textBoxDays.Text);
			if (num > 0)
			{
				dateTimePickerFreeTimeDate.Value = dateTimePickerDate.Value.AddDays(num);
				dateTimePickerFreeTimeDate.Checked = true;
			}
		}

		private void radioButtonDate_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonDate.Checked && comboBoxContainerStatus.SelectedID != 9)
			{
				dateTimePickerFreeTimeDate.Enabled = true;
				dateTimePickerFreeTime.Enabled = true;
			}
			else
			{
				dateTimePickerFreeTimeDate.Enabled = false;
				dateTimePickerFreeTime.Enabled = false;
			}
		}

		private void dateTimePickerFreeTimeDate_ValueChanged(object sender, EventArgs e)
		{
			int num = 0;
			if (dateTimePickerFreeTimeDate.Value.ToString() != "" && dateTimePickerFreeTimeDate.Checked)
			{
				num = dateTimePickerDate.Value.Subtract(dateTimePickerFreeTimeDate.Value).Negate().Days;
				textBoxDays.Text = num.ToString();
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			new ContainerStatusChangingForm().ShowDialog();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.ContainerTrackingForm));
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
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
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
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxDestPort = new Micromind.DataControls.PortComboBox();
			comboBoxSourcePort = new Micromind.DataControls.PortComboBox();
			comboBoxContainerSize = new Micromind.DataControls.ContainerSizeComboBox();
			transporterComboBox1 = new Micromind.DataControls.TransporterComboBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			textBoxVendor = new System.Windows.Forms.TextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			label17 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			label12 = new System.Windows.Forms.Label();
			dateTimePickerATD = new Micromind.UISupport.MMSDateTimePicker(components);
			label11 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			textBoxValue = new Micromind.UISupport.AmountTextBox();
			label16 = new System.Windows.Forms.Label();
			textBoxWeight = new Micromind.UISupport.QuantityTextBox();
			label14 = new System.Windows.Forms.Label();
			textBoxClearingAgent = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			textBoxBOL = new System.Windows.Forms.TextBox();
			dateTimePickerETA = new Micromind.UISupport.MMSDateTimePicker(components);
			label4 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxContainerNumber = new System.Windows.Forms.TextBox();
			textBoxShipper = new System.Windows.Forms.TextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			purchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			panel1 = new System.Windows.Forms.Panel();
			labelDriver = new System.Windows.Forms.Label();
			textBoxDriver = new System.Windows.Forms.TextBox();
			groupBoxDelivery = new Infragistics.Win.Misc.UltraGroupBox();
			radioButtonDate = new System.Windows.Forms.RadioButton();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			radioButtonDays = new System.Windows.Forms.RadioButton();
			dateTimePickerFreeTimeDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerFreeTime = new System.Windows.Forms.DateTimePicker();
			labelDeliveryFreeTime = new System.Windows.Forms.Label();
			groupBoxDocuments = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxOriginCertificate = new System.Windows.Forms.CheckBox();
			checkBoxhealthCertificate = new System.Windows.Forms.CheckBox();
			checkBoxPL = new System.Windows.Forms.CheckBox();
			checkBoxInvoice = new System.Windows.Forms.CheckBox();
			checkBoxBL = new System.Windows.Forms.CheckBox();
			comboBoxTransporter = new Micromind.DataControls.TransporterComboBox();
			comboBoxContainerStatus = new Micromind.DataControls.ContainerStstusComboBox();
			labelTransporter = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTruckNumber = new System.Windows.Forms.TextBox();
			labelTrcukNumber = new System.Windows.Forms.Label();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			label22 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			xpButton2 = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSourcePort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxContainerSize).BeginInit();
			((System.ComponentModel.ISupportInitialize)transporterComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDelivery).BeginInit();
			groupBoxDelivery.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDocuments).BeginInit();
			groupBoxDocuments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
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
				toolStripSeparator3,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(646, 31);
			toolStrip1.TabIndex = 5;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 532);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(646, 45);
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
			linePanelDown.Size = new System.Drawing.Size(646, 1);
			linePanelDown.TabIndex = 1;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(536, 13);
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
			dateTimePickerDate.Location = new System.Drawing.Point(532, 2);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(94, 20);
			dateTimePickerDate.TabIndex = 3;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 2);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(140, 20);
			textBoxVoucherNumber.TabIndex = 2;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(472, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRef1.Location = new System.Drawing.Point(532, 24);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.ReadOnly = true;
			textBoxRef1.Size = new System.Drawing.Size(94, 20);
			textBoxRef1.TabIndex = 5;
			textBoxRef1.TabStop = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(212, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel2.TabIndex = 21;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(comboBoxDestPort);
			panelDetails.Controls.Add(comboBoxSourcePort);
			panelDetails.Controls.Add(comboBoxContainerSize);
			panelDetails.Controls.Add(transporterComboBox1);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(textBoxVendor);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(label17);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(buttonSelectDocument);
			panelDetails.Controls.Add(label12);
			panelDetails.Controls.Add(dateTimePickerATD);
			panelDetails.Controls.Add(label11);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(label7);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxValue);
			panelDetails.Controls.Add(label16);
			panelDetails.Controls.Add(textBoxWeight);
			panelDetails.Controls.Add(label14);
			panelDetails.Controls.Add(textBoxClearingAgent);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label13);
			panelDetails.Controls.Add(textBoxBOL);
			panelDetails.Controls.Add(dateTimePickerETA);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label15);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxContainerNumber);
			panelDetails.Controls.Add(textBoxShipper);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 32);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(639, 167);
			panelDetails.TabIndex = 0;
			panelDetails.Paint += new System.Windows.Forms.PaintEventHandler(panelDetails_Paint);
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance3;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(94, 70);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ReadOnly = true;
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(158, 20);
			comboBoxShippingMethod.TabIndex = 7;
			comboBoxShippingMethod.TabStop = false;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDestPort.Assigned = false;
			comboBoxDestPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDestPort.CustomReportFieldName = "";
			comboBoxDestPort.CustomReportKey = "";
			comboBoxDestPort.CustomReportValueType = 1;
			comboBoxDestPort.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDestPort.DisplayLayout.Appearance = appearance15;
			comboBoxDestPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDestPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestPort.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxDestPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestPort.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxDestPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDestPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDestPort.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDestPort.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxDestPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDestPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDestPort.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDestPort.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxDestPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDestPort.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestPort.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxDestPort.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxDestPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDestPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxDestPort.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxDestPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDestPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxDestPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDestPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDestPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDestPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDestPort.Editable = true;
			comboBoxDestPort.FilterString = "";
			comboBoxDestPort.HasAllAccount = false;
			comboBoxDestPort.HasCustom = false;
			comboBoxDestPort.IsDataLoaded = false;
			comboBoxDestPort.Location = new System.Drawing.Point(321, 68);
			comboBoxDestPort.MaxDropDownItems = 12;
			comboBoxDestPort.Name = "comboBoxDestPort";
			comboBoxDestPort.ReadOnly = true;
			comboBoxDestPort.ShowInactiveItems = false;
			comboBoxDestPort.ShowQuickAdd = true;
			comboBoxDestPort.Size = new System.Drawing.Size(92, 20);
			comboBoxDestPort.TabIndex = 8;
			comboBoxDestPort.TabStop = false;
			comboBoxDestPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSourcePort.Assigned = false;
			comboBoxSourcePort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSourcePort.CustomReportFieldName = "";
			comboBoxSourcePort.CustomReportKey = "";
			comboBoxSourcePort.CustomReportValueType = 1;
			comboBoxSourcePort.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSourcePort.DisplayLayout.Appearance = appearance27;
			comboBoxSourcePort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSourcePort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSourcePort.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSourcePort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxSourcePort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSourcePort.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxSourcePort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSourcePort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSourcePort.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSourcePort.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxSourcePort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSourcePort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSourcePort.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSourcePort.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxSourcePort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSourcePort.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSourcePort.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxSourcePort.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxSourcePort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSourcePort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxSourcePort.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxSourcePort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSourcePort.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxSourcePort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSourcePort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSourcePort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSourcePort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSourcePort.Editable = true;
			comboBoxSourcePort.FilterString = "";
			comboBoxSourcePort.HasAllAccount = false;
			comboBoxSourcePort.HasCustom = false;
			comboBoxSourcePort.IsDataLoaded = false;
			comboBoxSourcePort.Location = new System.Drawing.Point(484, 69);
			comboBoxSourcePort.MaxDropDownItems = 12;
			comboBoxSourcePort.Name = "comboBoxSourcePort";
			comboBoxSourcePort.ReadOnly = true;
			comboBoxSourcePort.ShowInactiveItems = false;
			comboBoxSourcePort.ShowQuickAdd = true;
			comboBoxSourcePort.Size = new System.Drawing.Size(95, 20);
			comboBoxSourcePort.TabIndex = 9;
			comboBoxSourcePort.TabStop = false;
			comboBoxSourcePort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxContainerSize.Assigned = false;
			comboBoxContainerSize.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxContainerSize.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxContainerSize.CustomReportFieldName = "";
			comboBoxContainerSize.CustomReportKey = "";
			comboBoxContainerSize.CustomReportValueType = 1;
			comboBoxContainerSize.DescriptionTextBox = null;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxContainerSize.DisplayLayout.Appearance = appearance39;
			comboBoxContainerSize.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxContainerSize.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContainerSize.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContainerSize.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxContainerSize.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContainerSize.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxContainerSize.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxContainerSize.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxContainerSize.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxContainerSize.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxContainerSize.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxContainerSize.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxContainerSize.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxContainerSize.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxContainerSize.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxContainerSize.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContainerSize.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxContainerSize.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxContainerSize.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxContainerSize.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxContainerSize.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxContainerSize.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxContainerSize.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxContainerSize.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxContainerSize.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxContainerSize.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxContainerSize.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxContainerSize.Editable = true;
			comboBoxContainerSize.FilterString = "";
			comboBoxContainerSize.HasAllAccount = false;
			comboBoxContainerSize.HasCustom = false;
			comboBoxContainerSize.IsDataLoaded = false;
			comboBoxContainerSize.Location = new System.Drawing.Point(293, 140);
			comboBoxContainerSize.MaxDropDownItems = 12;
			comboBoxContainerSize.Name = "comboBoxContainerSize";
			comboBoxContainerSize.ReadOnly = true;
			comboBoxContainerSize.ShowInactiveItems = false;
			comboBoxContainerSize.ShowQuickAdd = true;
			comboBoxContainerSize.Size = new System.Drawing.Size(100, 20);
			comboBoxContainerSize.TabIndex = 17;
			comboBoxContainerSize.TabStop = false;
			comboBoxContainerSize.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			transporterComboBox1.Assigned = false;
			transporterComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			transporterComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			transporterComboBox1.CustomReportFieldName = "";
			transporterComboBox1.CustomReportKey = "";
			transporterComboBox1.CustomReportValueType = 1;
			transporterComboBox1.DescriptionTextBox = null;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			transporterComboBox1.DisplayLayout.Appearance = appearance51;
			transporterComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			transporterComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			transporterComboBox1.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			transporterComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			transporterComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			transporterComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			transporterComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			transporterComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			transporterComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			transporterComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			transporterComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			transporterComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			transporterComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			transporterComboBox1.DisplayLayout.Override.CellAppearance = appearance58;
			transporterComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			transporterComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			transporterComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			transporterComboBox1.DisplayLayout.Override.HeaderAppearance = appearance60;
			transporterComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			transporterComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			transporterComboBox1.DisplayLayout.Override.RowAppearance = appearance61;
			transporterComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			transporterComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			transporterComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			transporterComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			transporterComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			transporterComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			transporterComboBox1.Editable = true;
			transporterComboBox1.FilterString = "";
			transporterComboBox1.HasAllAccount = false;
			transporterComboBox1.HasCustom = false;
			transporterComboBox1.IsDataLoaded = false;
			transporterComboBox1.Location = new System.Drawing.Point(94, 136);
			transporterComboBox1.MaxDropDownItems = 12;
			transporterComboBox1.Name = "transporterComboBox1";
			transporterComboBox1.ReadOnly = true;
			transporterComboBox1.ShowInactiveItems = false;
			transporterComboBox1.ShowQuickAdd = true;
			transporterComboBox1.Size = new System.Drawing.Size(109, 20);
			transporterComboBox1.TabIndex = 15;
			transporterComboBox1.TabStop = false;
			transporterComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxVendor;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance63;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
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
			comboBoxVendor.Location = new System.Drawing.Point(94, 46);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ReadOnly = true;
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(109, 20);
			comboBoxVendor.TabIndex = 6;
			comboBoxVendor.TabStop = false;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVendor.Location = new System.Drawing.Point(211, 46);
			textBoxVendor.MaxLength = 20;
			textBoxVendor.Name = "textBoxVendor";
			textBoxVendor.ReadOnly = true;
			textBoxVendor.Size = new System.Drawing.Size(259, 20);
			textBoxVendor.TabIndex = 173;
			textBoxVendor.TabStop = false;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(94, 2);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxSysDoc_InitializeLayout);
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(11, 46);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(44, 13);
			label17.TabIndex = 171;
			label17.Text = "Vendor:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 66);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(48, 26);
			label9.TabIndex = 166;
			label9.Text = "Shipping\r\nMethod:";
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(325, 23);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 21;
			buttonSelectDocument.TabStop = false;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(213, 96);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(31, 13);
			label12.TabIndex = 165;
			label12.Text = "ETA:";
			dateTimePickerATD.Checked = false;
			dateTimePickerATD.CustomFormat = " ";
			dateTimePickerATD.Enabled = false;
			dateTimePickerATD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerATD.Location = new System.Drawing.Point(94, 93);
			dateTimePickerATD.Name = "dateTimePickerATD";
			dateTimePickerATD.ShowCheckBox = true;
			dateTimePickerATD.Size = new System.Drawing.Size(109, 20);
			dateTimePickerATD.TabIndex = 10;
			dateTimePickerATD.TabStop = false;
			dateTimePickerATD.Value = new System.DateTime(0L);
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(11, 98);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(32, 13);
			label11.TabIndex = 164;
			label11.Text = "ATD:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(419, 70);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(66, 13);
			label8.TabIndex = 162;
			label8.Text = "Source Port:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(212, 141);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(78, 13);
			label7.TabIndex = 16;
			label7.Text = "Container Size:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(11, 141);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(64, 13);
			label6.TabIndex = 159;
			label6.Text = "Transporter:";
			textBoxValue.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxValue.CustomReportFieldName = "";
			textBoxValue.CustomReportKey = "";
			textBoxValue.CustomReportValueType = 1;
			textBoxValue.IsComboTextBox = false;
			textBoxValue.Location = new System.Drawing.Point(483, 117);
			textBoxValue.Name = "textBoxValue";
			textBoxValue.ReadOnly = true;
			textBoxValue.Size = new System.Drawing.Size(104, 20);
			textBoxValue.TabIndex = 14;
			textBoxValue.TabStop = false;
			textBoxValue.Text = "0.00";
			textBoxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxValue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(472, 48);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(46, 13);
			label16.TabIndex = 150;
			label16.Text = "Shipper:";
			textBoxWeight.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxWeight.CustomReportFieldName = "";
			textBoxWeight.CustomReportKey = "";
			textBoxWeight.CustomReportValueType = 1;
			textBoxWeight.IsComboTextBox = false;
			textBoxWeight.Location = new System.Drawing.Point(94, 115);
			textBoxWeight.MaxLength = 10;
			textBoxWeight.Name = "textBoxWeight";
			textBoxWeight.ReadOnly = true;
			textBoxWeight.Size = new System.Drawing.Size(109, 20);
			textBoxWeight.TabIndex = 12;
			textBoxWeight.TabStop = false;
			textBoxWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(399, 142);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(79, 13);
			label14.TabIndex = 18;
			label14.Text = "Clearing Agent:";
			label14.Visible = false;
			textBoxClearingAgent.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxClearingAgent.Location = new System.Drawing.Point(484, 140);
			textBoxClearingAgent.MaxLength = 20;
			textBoxClearingAgent.Name = "textBoxClearingAgent";
			textBoxClearingAgent.ReadOnly = true;
			textBoxClearingAgent.Size = new System.Drawing.Size(142, 20);
			textBoxClearingAgent.TabIndex = 19;
			textBoxClearingAgent.TabStop = false;
			textBoxClearingAgent.Visible = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(438, 119);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(37, 13);
			label5.TabIndex = 146;
			label5.Text = "Value:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(212, 119);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(48, 13);
			label13.TabIndex = 146;
			label13.Text = "BOL No:";
			textBoxBOL.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBOL.Location = new System.Drawing.Point(268, 117);
			textBoxBOL.MaxLength = 20;
			textBoxBOL.Name = "textBoxBOL";
			textBoxBOL.ReadOnly = true;
			textBoxBOL.Size = new System.Drawing.Size(167, 20);
			textBoxBOL.TabIndex = 13;
			textBoxBOL.TabStop = false;
			dateTimePickerETA.Checked = false;
			dateTimePickerETA.CustomFormat = " ";
			dateTimePickerETA.Enabled = false;
			dateTimePickerETA.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerETA.Location = new System.Drawing.Point(257, 93);
			dateTimePickerETA.Name = "dateTimePickerETA";
			dateTimePickerETA.ShowCheckBox = true;
			dateTimePickerETA.Size = new System.Drawing.Size(109, 20);
			dateTimePickerETA.TabIndex = 11;
			dateTimePickerETA.TabStop = false;
			dateTimePickerETA.Value = new System.DateTime(0L);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(265, 70);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(57, 13);
			label4.TabIndex = 142;
			label4.Text = "Dest. Port:";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(11, 119);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(44, 13);
			label15.TabIndex = 142;
			label15.Text = "Weight:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(11, 25);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(72, 13);
			label2.TabIndex = 20;
			label2.Text = "Container No:";
			appearance75.FontData.BoldAsString = "True";
			appearance75.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance75;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 5);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 19;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance76.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance76;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(472, 5);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 4;
			mmLabel1.Text = "Date:";
			textBoxContainerNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxContainerNumber.Location = new System.Drawing.Point(94, 25);
			textBoxContainerNumber.MaxLength = 64;
			textBoxContainerNumber.Name = "textBoxContainerNumber";
			textBoxContainerNumber.ReadOnly = true;
			textBoxContainerNumber.Size = new System.Drawing.Size(224, 20);
			textBoxContainerNumber.TabIndex = 4;
			textBoxContainerNumber.TabStop = false;
			textBoxShipper.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxShipper.Location = new System.Drawing.Point(532, 46);
			textBoxShipper.MaxLength = 15;
			textBoxShipper.Name = "textBoxShipper";
			textBoxShipper.ReadOnly = true;
			textBoxShipper.Size = new System.Drawing.Size(94, 20);
			textBoxShipper.TabIndex = 7;
			textBoxShipper.TabStop = false;
			contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				availableQuantityToolStripMenuItem,
				purchaseStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(177, 92);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			purchaseStatisticsToolStripMenuItem.Name = "purchaseStatisticsToolStripMenuItem";
			purchaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			purchaseStatisticsToolStripMenuItem.Text = "Purchase Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			ultraGroupBox1.Controls.Add(panel1);
			ultraGroupBox1.Location = new System.Drawing.Point(0, 199);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(639, 228);
			ultraGroupBox1.TabIndex = 1;
			ultraGroupBox1.Text = "Status Change";
			ultraGroupBox1.Click += new System.EventHandler(ultraGroupBox1_Click);
			panel1.Controls.Add(labelDriver);
			panel1.Controls.Add(textBoxDriver);
			panel1.Controls.Add(groupBoxDelivery);
			panel1.Controls.Add(groupBoxDocuments);
			panel1.Controls.Add(comboBoxTransporter);
			panel1.Controls.Add(comboBoxContainerStatus);
			panel1.Controls.Add(labelTransporter);
			panel1.Controls.Add(textBoxTruckNumber);
			panel1.Controls.Add(labelTrcukNumber);
			panel1.Controls.Add(mmLabel6);
			panel1.Location = new System.Drawing.Point(4, 22);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(625, 200);
			panel1.TabIndex = 0;
			labelDriver.AutoSize = true;
			labelDriver.Location = new System.Drawing.Point(11, 31);
			labelDriver.Name = "labelDriver";
			labelDriver.Size = new System.Drawing.Size(38, 13);
			labelDriver.TabIndex = 178;
			labelDriver.Text = "Driver:";
			textBoxDriver.BackColor = System.Drawing.SystemColors.Window;
			textBoxDriver.Location = new System.Drawing.Point(90, 31);
			textBoxDriver.MaxLength = 20;
			textBoxDriver.Name = "textBoxDriver";
			textBoxDriver.Size = new System.Drawing.Size(196, 20);
			textBoxDriver.TabIndex = 177;
			textBoxDriver.Visible = false;
			groupBoxDelivery.Controls.Add(radioButtonDate);
			groupBoxDelivery.Controls.Add(textBoxDays);
			groupBoxDelivery.Controls.Add(radioButtonDays);
			groupBoxDelivery.Controls.Add(dateTimePickerFreeTimeDate);
			groupBoxDelivery.Controls.Add(dateTimePickerFreeTime);
			groupBoxDelivery.Controls.Add(labelDeliveryFreeTime);
			groupBoxDelivery.Location = new System.Drawing.Point(7, 104);
			groupBoxDelivery.Name = "groupBoxDelivery";
			groupBoxDelivery.Size = new System.Drawing.Size(350, 93);
			groupBoxDelivery.TabIndex = 176;
			groupBoxDelivery.Text = "Delivery ";
			groupBoxDelivery.Visible = false;
			radioButtonDate.AutoSize = true;
			radioButtonDate.Location = new System.Drawing.Point(24, 24);
			radioButtonDate.Name = "radioButtonDate";
			radioButtonDate.Size = new System.Drawing.Size(51, 17);
			radioButtonDate.TabIndex = 182;
			radioButtonDate.Text = "Date:";
			radioButtonDate.UseVisualStyleBackColor = true;
			radioButtonDate.Visible = false;
			radioButtonDate.CheckedChanged += new System.EventHandler(radioButtonDate_CheckedChanged);
			textBoxDays.AllowDecimal = false;
			textBoxDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxDays.IsComboTextBox = false;
			textBoxDays.Location = new System.Drawing.Point(279, 24);
			textBoxDays.MaxLength = 15;
			textBoxDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDays.Name = "textBoxDays";
			textBoxDays.NullText = "0";
			textBoxDays.ReadOnly = true;
			textBoxDays.Size = new System.Drawing.Size(62, 20);
			textBoxDays.TabIndex = 181;
			textBoxDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDays.Visible = false;
			textBoxDays.TextChanged += new System.EventHandler(textBoxDays_TextChanged);
			radioButtonDays.AutoSize = true;
			radioButtonDays.Location = new System.Drawing.Point(223, 25);
			radioButtonDays.Name = "radioButtonDays";
			radioButtonDays.Size = new System.Drawing.Size(52, 17);
			radioButtonDays.TabIndex = 180;
			radioButtonDays.Text = "Days:";
			radioButtonDays.UseVisualStyleBackColor = true;
			radioButtonDays.Visible = false;
			radioButtonDays.CheckedChanged += new System.EventHandler(radioButtonDays_CheckedChanged);
			dateTimePickerFreeTimeDate.Checked = false;
			dateTimePickerFreeTimeDate.CustomFormat = " ";
			dateTimePickerFreeTimeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerFreeTimeDate.Location = new System.Drawing.Point(100, 24);
			dateTimePickerFreeTimeDate.Name = "dateTimePickerFreeTimeDate";
			dateTimePickerFreeTimeDate.ShowCheckBox = true;
			dateTimePickerFreeTimeDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerFreeTimeDate.TabIndex = 179;
			dateTimePickerFreeTimeDate.TabStop = false;
			dateTimePickerFreeTimeDate.Value = new System.DateTime(0L);
			dateTimePickerFreeTimeDate.ValueChanged += new System.EventHandler(dateTimePickerFreeTimeDate_ValueChanged);
			dateTimePickerFreeTime.Checked = false;
			dateTimePickerFreeTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerFreeTime.Location = new System.Drawing.Point(100, 49);
			dateTimePickerFreeTime.Name = "dateTimePickerFreeTime";
			dateTimePickerFreeTime.ShowCheckBox = true;
			dateTimePickerFreeTime.ShowUpDown = true;
			dateTimePickerFreeTime.Size = new System.Drawing.Size(116, 20);
			dateTimePickerFreeTime.TabIndex = 176;
			dateTimePickerFreeTime.Visible = false;
			labelDeliveryFreeTime.AutoSize = true;
			labelDeliveryFreeTime.Location = new System.Drawing.Point(42, 55);
			labelDeliveryFreeTime.Name = "labelDeliveryFreeTime";
			labelDeliveryFreeTime.Size = new System.Drawing.Size(33, 13);
			labelDeliveryFreeTime.TabIndex = 178;
			labelDeliveryFreeTime.Text = "Time:";
			labelDeliveryFreeTime.Visible = false;
			groupBoxDocuments.Controls.Add(checkBoxOriginCertificate);
			groupBoxDocuments.Controls.Add(checkBoxhealthCertificate);
			groupBoxDocuments.Controls.Add(checkBoxPL);
			groupBoxDocuments.Controls.Add(checkBoxInvoice);
			groupBoxDocuments.Controls.Add(checkBoxBL);
			groupBoxDocuments.Location = new System.Drawing.Point(372, 9);
			groupBoxDocuments.Name = "groupBoxDocuments";
			groupBoxDocuments.Size = new System.Drawing.Size(212, 177);
			groupBoxDocuments.TabIndex = 173;
			groupBoxDocuments.Text = "Documents Received";
			groupBoxDocuments.Visible = false;
			checkBoxOriginCertificate.AutoSize = true;
			checkBoxOriginCertificate.Location = new System.Drawing.Point(17, 115);
			checkBoxOriginCertificate.Name = "checkBoxOriginCertificate";
			checkBoxOriginCertificate.Size = new System.Drawing.Size(115, 17);
			checkBoxOriginCertificate.TabIndex = 176;
			checkBoxOriginCertificate.Text = "Certificate of Origin";
			checkBoxOriginCertificate.UseVisualStyleBackColor = true;
			checkBoxhealthCertificate.AutoSize = true;
			checkBoxhealthCertificate.Location = new System.Drawing.Point(17, 93);
			checkBoxhealthCertificate.Name = "checkBoxhealthCertificate";
			checkBoxhealthCertificate.Size = new System.Drawing.Size(107, 17);
			checkBoxhealthCertificate.TabIndex = 175;
			checkBoxhealthCertificate.Text = "Health Certificate";
			checkBoxhealthCertificate.UseVisualStyleBackColor = true;
			checkBoxPL.AutoSize = true;
			checkBoxPL.Location = new System.Drawing.Point(17, 71);
			checkBoxPL.Name = "checkBoxPL";
			checkBoxPL.Size = new System.Drawing.Size(84, 17);
			checkBoxPL.TabIndex = 174;
			checkBoxPL.Text = "Packing List";
			checkBoxPL.UseVisualStyleBackColor = true;
			checkBoxInvoice.AutoSize = true;
			checkBoxInvoice.Location = new System.Drawing.Point(17, 49);
			checkBoxInvoice.Name = "checkBoxInvoice";
			checkBoxInvoice.Size = new System.Drawing.Size(61, 17);
			checkBoxInvoice.TabIndex = 173;
			checkBoxInvoice.Text = "Invoice";
			checkBoxInvoice.UseVisualStyleBackColor = true;
			checkBoxBL.AutoSize = true;
			checkBoxBL.Location = new System.Drawing.Point(17, 27);
			checkBoxBL.Name = "checkBoxBL";
			checkBoxBL.Size = new System.Drawing.Size(39, 17);
			checkBoxBL.TabIndex = 172;
			checkBoxBL.Text = "BL";
			checkBoxBL.UseVisualStyleBackColor = true;
			comboBoxTransporter.Assigned = false;
			comboBoxTransporter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTransporter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransporter.CustomReportFieldName = "";
			comboBoxTransporter.CustomReportKey = "";
			comboBoxTransporter.CustomReportValueType = 1;
			comboBoxTransporter.DescriptionTextBox = null;
			comboBoxTransporter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransporter.Editable = true;
			comboBoxTransporter.FilterString = "";
			comboBoxTransporter.HasAllAccount = false;
			comboBoxTransporter.HasCustom = false;
			comboBoxTransporter.IsDataLoaded = false;
			comboBoxTransporter.Location = new System.Drawing.Point(90, 54);
			comboBoxTransporter.MaxDropDownItems = 12;
			comboBoxTransporter.Name = "comboBoxTransporter";
			comboBoxTransporter.ShowInactiveItems = false;
			comboBoxTransporter.ShowQuickAdd = true;
			comboBoxTransporter.Size = new System.Drawing.Size(196, 20);
			comboBoxTransporter.TabIndex = 3;
			comboBoxTransporter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTransporter.Visible = false;
			comboBoxContainerStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxContainerStatus.FormattingEnabled = true;
			comboBoxContainerStatus.Items.AddRange(new object[11]
			{
				"Shipped",
				"OriginalDocumentReceived",
				"Arrived",
				"DO Release",
				"Customs Checking",
				"Shipment Removal",
				"Shipment Delivered",
				"Off Loaded",
				"Returning",
				"Returned",
				"Cancelled"
			});
			comboBoxContainerStatus.Location = new System.Drawing.Point(90, 7);
			comboBoxContainerStatus.Name = "comboBoxContainerStatus";
			comboBoxContainerStatus.SelectedID = 0;
			comboBoxContainerStatus.Size = new System.Drawing.Size(196, 21);
			comboBoxContainerStatus.TabIndex = 0;
			comboBoxContainerStatus.SelectedIndexChanged += new System.EventHandler(comboBoxContainerStatus_SelectedIndexChanged);
			appearance77.FontData.BoldAsString = "False";
			appearance77.FontData.Name = "Tahoma";
			labelTransporter.Appearance = appearance77;
			labelTransporter.AutoSize = true;
			labelTransporter.Location = new System.Drawing.Point(12, 54);
			labelTransporter.Name = "labelTransporter";
			labelTransporter.Size = new System.Drawing.Size(65, 15);
			labelTransporter.TabIndex = 170;
			labelTransporter.TabStop = true;
			labelTransporter.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTransporter.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTransporter.Value = "Transporter:";
			labelTransporter.Visible = false;
			appearance78.ForeColor = System.Drawing.Color.Blue;
			labelTransporter.VisitedLinkAppearance = appearance78;
			labelTransporter.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelTransporter_LinkClicked);
			textBoxTruckNumber.BackColor = System.Drawing.SystemColors.Window;
			textBoxTruckNumber.Location = new System.Drawing.Point(90, 78);
			textBoxTruckNumber.MaxLength = 20;
			textBoxTruckNumber.Name = "textBoxTruckNumber";
			textBoxTruckNumber.Size = new System.Drawing.Size(196, 20);
			textBoxTruckNumber.TabIndex = 2;
			textBoxTruckNumber.Visible = false;
			labelTrcukNumber.AutoSize = true;
			labelTrcukNumber.Location = new System.Drawing.Point(11, 78);
			labelTrcukNumber.Name = "labelTrcukNumber";
			labelTrcukNumber.Size = new System.Drawing.Size(57, 13);
			labelTrcukNumber.TabIndex = 165;
			labelTrcukNumber.Text = "Truck NO:";
			labelTrcukNumber.Visible = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(11, 7);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(47, 13);
			mmLabel6.TabIndex = 41;
			mmLabel6.Text = "Status:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(508, 460);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(128, 58);
			labelVoided.TabIndex = 132;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(7, 433);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(52, 13);
			label22.TabIndex = 8;
			label22.Text = "Remarks:";
			textBoxNote.Location = new System.Drawing.Point(94, 471);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(402, 55);
			textBoxNote.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(7, 471);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 9;
			label3.Text = "Note:";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.Location = new System.Drawing.Point(94, 433);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(402, 36);
			textBoxRemarks.TabIndex = 2;
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
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(512, 430);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(96, 24);
			xpButton2.TabIndex = 133;
			xpButton2.Text = "Ne&w...";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(646, 577);
			base.Controls.Add(xpButton2);
			base.Controls.Add(label22);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(labelVoided);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ContainerTrackingForm";
			Text = "Container Tracking";
			base.Load += new System.EventHandler(JournalLeavesForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSourcePort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxContainerSize).EndInit();
			((System.ComponentModel.ISupportInitialize)transporterComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDelivery).EndInit();
			groupBoxDelivery.ResumeLayout(false);
			groupBoxDelivery.PerformLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDocuments).EndInit();
			groupBoxDocuments.ResumeLayout(false);
			groupBoxDocuments.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
