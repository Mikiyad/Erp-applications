using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
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
	public class ContainerTrackingWizardForm : Form, IForm
	{
		private enum arrayStatus
		{
			Shipped = 1,
			OriginalDocumentReceived,
			Arrived,
			DORelease,
			CustomsChecking,
			ShipmentRemoval,
			ShipmentDelivered,
			OffLoaded,
			Returning,
			Returned,
			Cancelled
		}

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

		private int CurrentLabelstatus;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private IContainer components;

		private ToolStrip toolStrip1;

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

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private Panel panelDetails;

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

		private Label label17;

		private Label labelVoided;

		private TextBox textBoxVendor;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator3;

		private TextBox textBoxNote;

		private Label label3;

		private TextBox textBoxContainerSize;

		private TextBox textBoxTransporter;

		private TextBox textBoxSourcePort;

		private TextBox textBoxDestPort;

		private TextBox textBoxShippingMethod;

		private vendorsFlatComboBox comboBoxVendor;

		private ContainerStstusComboBox comboBoxContainerStatus;

		private MMLabel mmLabel6;

		private Panel panel1;

		private XPButton buttonDelete;

		private Line line1;

		private XPButton xpButton2;

		private XPButton buttonSave;

		private XPButton buttonNew;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private TextBox textBoxVoucherNumber;

		private XPButton buttonNext;

		private UltraGroupBox ultraGroupBox1;

		private Panel panel2;

		private Label labelTickCancelled;

		private Label labelTickReturned;

		private Label labelTickReturning;

		private Label labelTickOffLoaded;

		private Label labelTickShipmentDelivered;

		private Label labelTickShipmentRemoval;

		private Label labelTickCustomsChecking;

		private Label labelTickDORelease;

		private Label labelTickArrived;

		private Label labelTickOriginalDocumentReceived;

		private Label labelTickShipped;

		private Label labelReturned;

		private Label labelCancelled;

		private Label labelReturning;

		private Label labelOffLoaded;

		private Label labelShipmentDelivered;

		private Label labelShipmentRemoval;

		private Label labelCustomsChecking;

		private Label labelDORelease;

		private Label labelArrived;

		private Label labelOriginalDocumentReceived;

		private Label labelShipped;

		private UltraGroupBox groupBoxStatusChange;

		private UltraGroupBox groupBoxDelivery;

		private RadioButton radioButtonDate;

		private NumberTextBox textBoxDays;

		private RadioButton radioButtonDays;

		private MMSDateTimePicker dateTimePickerFreeTimeDate;

		private DateTimePicker dateTimePickerFreeTime;

		private Label labelDeliveryFreeTime;

		private Panel panelStatus;

		private Label labelDriver;

		private TextBox textBoxDriver;

		private TransporterComboBox comboBoxTransporter;

		private UltraFormattedLinkLabel labelTransporter;

		private TextBox textBoxTruckNumber;

		private Label labelTrcukNumber;

		private UltraGroupBox groupBoxDocuments;

		private CheckBox checkBoxOriginCertificate;

		private CheckBox checkBoxhealthCertificate;

		private CheckBox checkBoxPL;

		private CheckBox checkBoxInvoice;

		private CheckBox checkBoxBL;

		private Panel panelStatusDate;

		private Label labelDateCancelled;

		private Label labelDateReturned;

		private Label labelDateReturning;

		private Label labelDateOffLoaded;

		private Label labelDateShipmentDelivered;

		private Label labelDateShipmentRemoval;

		private Label labelDateCustomsChecking;

		private Label labelDateDORelease;

		private Label labelDateArrived;

		private Label labelDateOriginalDocumentReceived;

		private Label labelDateShipped;

		private Label labelStatusCancelled;

		private Label labelStatusReturned;

		private Label labelStatusReturning;

		private Label labelStatusOffLoaded;

		private Label labelStatusShipmentDelivered;

		private Label labelStatusShipmentRemoval;

		private Label labelStatusCustomsChecking;

		private Label labelStatusDORelease;

		private Label labelStatusArrived;

		private Label labelStatusOriginalDocumentReceived;

		private Label labelStatusShipped;

		private XPButton xpButton1;

		public ScreenAreas ScreenArea => ScreenAreas.Purchases;

		public int ScreenID => 3007;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private bool IsDirty => formManager.GetDirtyStatus();

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
					comboBoxSysDoc.Enabled = true;
					textBoxVoucherNumber.ReadOnly = false;
					comboBoxContainerStatus.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					comboBoxSysDoc.Enabled = false;
					textBoxVoucherNumber.ReadOnly = true;
				}
				if (value)
				{
					foreach (Label control in panel2.Controls)
					{
						control.Font = new Font(control.Font, FontStyle.Regular);
					}
				}
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

		public ContainerTrackingWizardForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			foreach (Label control in panel2.Controls)
			{
				control.Click += label_Click;
				control.ForeColorChanged += label_ForeColorChanged;
			}
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
				if (textBoxDestPort.Text != "")
				{
					dataRow["DestinationPort"] = textBoxDestPort.Text;
				}
				else
				{
					dataRow["DestinationPort"] = DBNull.Value;
				}
				if (textBoxSourcePort.Text != "")
				{
					dataRow["LoadingPort"] = textBoxSourcePort.Text;
				}
				else
				{
					dataRow["LoadingPort"] = DBNull.Value;
				}
				if (textBoxShippingMethod.Text != "")
				{
					dataRow["ShippingMethodID"] = textBoxShippingMethod.Text;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				dataRow["TransporterID"] = comboBoxTransporter.SelectedID;
				dataRow["ContainerSizeID"] = textBoxContainerSize.Text;
				dataRow["Container_Status"] = CurrentLabelstatus;
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
					textBoxDestPort.Text = dataRow["DestinationPort"].ToString();
					textBoxSourcePort.Text = dataRow["LoadingPort"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxShippingMethod.Text = dataRow["ShippingMethodID"].ToString();
					comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
					textBoxContainerSize.Text = dataRow["ContainerSizeID"].ToString();
					comboBoxContainerStatus.SelectedID = int.Parse(dataRow["Container_Status"].ToString());
					lastStatus = int.Parse(dataRow["Container_Status"].ToString());
					CurrentLabelstatus = lastStatus;
					arrayStatus arrayStatus = (arrayStatus)lastStatus;
					arrayStatus.ToString().Replace(" ", "");
					string text = "";
					int num = 0;
					checked
					{
						foreach (Label control in panel2.Controls)
						{
							num = (int)Enum.Parse(value: (!control.Name.Contains("Tick")) ? control.Name.Substring(5, control.Name.Length - 5) : control.Name.Substring(9, control.Name.Length - 9), enumType: typeof(arrayStatus));
							if (num < lastStatus)
							{
								if (control.Name.Contains("Tick"))
								{
									control.Visible = true;
								}
								control.Enabled = true;
								control.Font = new Font(control.Font, FontStyle.Bold);
							}
							else if (num == lastStatus)
							{
								if (control.Name.Contains("Tick"))
								{
									control.Visible = true;
								}
								control.Enabled = true;
								control.Font = new Font(control.Font, FontStyle.Bold);
							}
							else if (num == lastStatus + 1)
							{
								if (control.Name.Contains("Tick"))
								{
									control.Visible = false;
								}
								control.Enabled = true;
								control.Font = new Font(control.Font, FontStyle.Regular);
							}
							else
							{
								if (control.Name.Contains("Tick"))
								{
									control.Visible = false;
								}
								control.Enabled = false;
								control.Font = new Font(control.Font, FontStyle.Regular);
							}
						}
						foreach (Label control2 in panelStatusDate.Controls)
						{
							_ = control2;
							string text2 = "";
							foreach (Label control3 in panelStatusDate.Controls)
							{
								num = (int)Enum.Parse(value: (!control3.Name.Contains("Date")) ? control3.Name.Substring(11, control3.Name.Length - 11) : control3.Name.Substring(9, control3.Name.Length - 9), enumType: typeof(arrayStatus));
								DataSet containerDetailsOnStatus = Factory.ContainerStrackingSystem.GetContainerDetailsOnStatus(textBoxContainerNumber.Text, num);
								if (containerDetailsOnStatus != null && containerDetailsOnStatus.Tables.Count > 0 && containerDetailsOnStatus.Tables[0].Rows.Count > 0)
								{
									DataRow dataRow2 = containerDetailsOnStatus.Tables[0].Rows[0];
									if (num <= lastStatus)
									{
										if (control3.Name.Contains("Date"))
										{
											if (!string.IsNullOrEmpty(dataRow2["TransactionDate"].ToString()))
											{
												string text3 = DateTime.Parse(dataRow2["TransactionDate"].ToString()).ToShortDateString();
												control3.Visible = true;
												control3.Text = text3;
											}
											else
											{
												control3.Text = "";
											}
										}
										control3.Visible = true;
									}
									else if (control3.Name.Contains("Date"))
									{
										control3.Visible = false;
									}
								}
							}
						}
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
						textBoxDriver.Text = dataRow["DriverID"].ToString();
						textBoxTruckNumber.Text = dataRow["TruckNumber"].ToString();
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
						comboBoxContainerStatus.Enabled = false;
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
			int currentLabelstatus = CurrentLabelstatus;
			if (IsNewRecord && lastStatus == currentLabelstatus)
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
				textBoxContainerSize.Clear();
				textBoxVendor.Clear();
				comboBoxVendor.Clear();
				lastStatus = 0;
				currentIndex = 0;
				comboBoxContainerStatus.Clear();
				textBoxBOL.Clear();
				textBoxClearingAgent.Clear();
				textBoxContainerNumber.Clear();
				textBoxShipper.Clear();
				textBoxWeight.Text = "0";
				textBoxDestPort.Clear();
				textBoxSourcePort.Clear();
				textBoxValue.Text = 0.ToString(Format.TotalAmountFormat);
				dateTimePickerETA.Value = DateTime.Now;
				dateTimePickerETA.Checked = false;
				dateTimePickerATD.Value = DateTime.Now;
				dateTimePickerATD.Checked = false;
				textBoxShippingMethod.Clear();
				textBoxDriver.Clear();
				comboBoxTransporter.Clear();
				textBoxTruckNumber.Clear();
				textBoxDays.Text = 0.ToString();
				dateTimePickerFreeTimeDate.Clear();
				dateTimePickerFreeTime.Checked = false;
				comboBoxTransporter.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				checkBoxBL.Checked = false;
				checkBoxhealthCertificate.Checked = false;
				checkBoxInvoice.Checked = false;
				checkBoxOriginCertificate.Checked = false;
				checkBoxPL.Checked = false;
				groupBoxDocuments.Visible = false;
				groupBoxStatusChange.Visible = false;
				formManager.ResetDirty();
				foreach (Label control in panel2.Controls)
				{
					if (control.Name.Contains("Tick"))
					{
						control.Visible = false;
					}
					else
					{
						control.Font = new Font(control.Font, FontStyle.Regular);
						control.Enabled = true;
						control.Visible = true;
					}
				}
				foreach (Label control2 in panelStatusDate.Controls)
				{
					if (control2.Name.Contains("Date"))
					{
						control2.Visible = false;
					}
				}
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
			checked
			{
				try
				{
					comboBoxSysDoc.FilterByType(SysDocTypes.ContainerTracking);
					comboBoxSysDoc.SelectedIndex++;
					Label label = labelTickShipped;
					Label label2 = labelTickOriginalDocumentReceived;
					Label label3 = labelTickArrived;
					Label label4 = labelTickDORelease;
					Label label5 = labelTickCustomsChecking;
					Label label6 = labelTickOffLoaded;
					Label label7 = labelTickShipmentRemoval;
					Label label8 = labelTickShipmentDelivered;
					Label label9 = labelTickCancelled;
					Label label10 = labelTickReturned;
					string text2 = labelTickReturning.Text = "✔";
					string text4 = label10.Text = text2;
					string text6 = label9.Text = text4;
					string text8 = label8.Text = text6;
					string text10 = label7.Text = text8;
					string text12 = label6.Text = text10;
					string text14 = label5.Text = text12;
					string text16 = label4.Text = text14;
					string text18 = label3.Text = text16;
					string text21 = label.Text = (label2.Text = text18);
					textBoxDays.Text = 0.ToString();
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
								textBoxShippingMethod.Text = dataRow["ShippingMethodID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["ContainerNumber"].ToString()))
							{
								textBoxContainerNumber.Text = dataRow["ContainerNumber"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["ContainerSizeID"].ToString()))
							{
								textBoxContainerSize.Text = dataRow["ContainerSizeID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["VendorID"].ToString()))
							{
								comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["DestinationPort"].ToString()))
							{
								textBoxDestPort.Text = dataRow["DestinationPort"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["LoadingPort"].ToString()))
							{
								textBoxSourcePort.Text = dataRow["LoadingPort"].ToString();
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
								textBoxSourcePort.Text = dataRow["Port"].ToString();
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
								CurrentLabelstatus = lastStatus;
								arrayStatus arrayStatus = (arrayStatus)lastStatus;
								arrayStatus.ToString().Replace(" ", "");
								string text = "";
								int num = 0;
								checked
								{
									foreach (Label control in panel2.Controls)
									{
										num = (int)Enum.Parse(value: (!control.Name.Contains("Tick")) ? control.Name.Substring(5, control.Name.Length - 5) : control.Name.Substring(9, control.Name.Length - 9), enumType: typeof(arrayStatus));
										if (num < lastStatus)
										{
											if (control.Name.Contains("Tick"))
											{
												control.Visible = true;
											}
											control.Enabled = true;
											control.Font = new Font(control.Font, FontStyle.Regular);
										}
										else if (num == lastStatus)
										{
											if (control.Name.Contains("Tick"))
											{
												control.Visible = true;
											}
											control.Enabled = true;
											control.Font = new Font(control.Font, FontStyle.Bold);
										}
										else if (num == lastStatus + 1)
										{
											if (control.Name.Contains("Tick"))
											{
												control.Visible = false;
											}
											control.Enabled = true;
											control.Font = new Font(control.Font, FontStyle.Regular);
										}
										else
										{
											if (control.Name.Contains("Tick"))
											{
												control.Visible = false;
											}
											control.Enabled = false;
											control.Font = new Font(control.Font, FontStyle.Regular);
										}
									}
									foreach (Label control2 in panelStatusDate.Controls)
									{
										_ = control2;
										string text2 = "";
										foreach (Label control3 in panelStatusDate.Controls)
										{
											num = (int)Enum.Parse(value: (!control3.Name.Contains("Date")) ? control3.Name.Substring(11, control3.Name.Length - 11) : control3.Name.Substring(9, control3.Name.Length - 9), enumType: typeof(arrayStatus));
											DataSet containerDetailsOnStatus = Factory.ContainerStrackingSystem.GetContainerDetailsOnStatus(containerNumber, num);
											if (containerDetailsOnStatus != null && containerDetailsOnStatus.Tables.Count > 0 && containerDetailsOnStatus.Tables[0].Rows.Count > 0)
											{
												DataRow dataRow2 = containerDetailsOnStatus.Tables[0].Rows[0];
												if (num <= lastStatus)
												{
													if (control3.Name.Contains("Date"))
													{
														if (!string.IsNullOrEmpty(dataRow2["TransactionDate"].ToString()))
														{
															string text3 = DateTime.Parse(dataRow2["TransactionDate"].ToString()).ToShortDateString();
															control3.Visible = true;
															control3.Text = text3;
														}
														else
														{
															control3.Text = "";
														}
													}
													control3.Visible = true;
												}
												else if (control3.Name.Contains("Date"))
												{
													control3.Visible = false;
												}
											}
										}
									}
								}
							}
							else
							{
								foreach (Label control4 in panel2.Controls)
								{
									control4.Enabled = false;
								}
								labelShipped.Font = new Font(labelShipped.Font, FontStyle.Regular);
								labelShipped.Enabled = true;
								CurrentLabelstatus = 1;
								comboBoxContainerStatus.SelectedID = 1;
								comboBoxContainerStatus.Enabled = false;
							}
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
							textBoxTruckNumber.Text = dataRow["TruckNumber"].ToString();
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
				textBoxDays.Text = 0.ToString();
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
			else
			{
				textBoxDays.Text = 0.ToString();
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void ultraGroupBox2_Click(object sender, EventArgs e)
		{
		}

		private void label26_Click(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click_1(object sender, EventArgs e)
		{
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			checked
			{
				if (currentData != null)
				{
					CurrentLabelstatus++;
					if (CurrentLabelstatus > 11)
					{
						ErrorHelper.InformationMessage("Container Already completed all the Transactions");
						return;
					}
					arrayStatus currentLabelstatus = unchecked((arrayStatus)CurrentLabelstatus);
					string value = currentLabelstatus.ToString();
					foreach (Label control in panel2.Controls)
					{
						if (control.Name.Contains(value))
						{
							control.Font = new Font(control.Font, FontStyle.Bold);
						}
						else
						{
							control.Font = new Font(control.Font, FontStyle.Regular);
						}
					}
					if (CurrentLabelstatus == 2)
					{
						groupBoxDocuments.Enabled = true;
					}
					else
					{
						groupBoxDocuments.Enabled = false;
					}
					DataRow dataRow = (!isNewRecord) ? currentData.ContainerTrackingTable.Rows[0] : currentData.ContainerTrackingTable.NewRow();
					if (dataRow["Container_Status"] != DBNull.Value)
					{
						currentIndex = int.Parse(dataRow["Container_Status"].ToString());
						if (CurrentLabelstatus > currentIndex + 1 && CurrentLabelstatus != 11)
						{
							ErrorHelper.ErrorMessage("Up To one higher entry or cancellation is possible only in  the CurrentLabelstatus field");
							CurrentLabelstatus = int.Parse(dataRow["Container_Status"].ToString());
						}
						if (CurrentLabelstatus < currentIndex && CurrentLabelstatus != 11 && textBoxContainerNumber.Text != "")
						{
							ErrorHelper.ErrorMessage("You can only enter next higher CurrentLabelstatus");
							CurrentLabelstatus = int.Parse(dataRow["Container_Status"].ToString());
						}
					}
				}
				if (lastStatus > 0 && isNewRecord)
				{
					if (CurrentLabelstatus > lastStatus + 1 && CurrentLabelstatus != 11)
					{
						ErrorHelper.ErrorMessage("Up To one higher entry or cancellation is possible only in  the CurrentLabelstatus field");
						CurrentLabelstatus = lastStatus;
					}
					if (CurrentLabelstatus < lastStatus && CurrentLabelstatus != 11)
					{
						ErrorHelper.ErrorMessage("You can only enter next higher CurrentLabelstatus");
						CurrentLabelstatus = lastStatus;
					}
				}
				else if (currentData == null && CurrentLabelstatus > 1)
				{
					CurrentLabelstatus = 1;
				}
				else if (!isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					isNewRecord = true;
				}
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void label_ForeColorChanged(object sender, EventArgs e)
		{
			Label label = sender as Label;
			if (label != null && label.Font.Bold)
			{
				label.Text = label.Text.Replace(" ", "");
				_ = (int)Enum.Parse(typeof(arrayStatus), label.Text);
				if (CurrentLabelstatus == 2 && isNewRecord)
				{
					groupBoxStatusChange.Visible = true;
					groupBoxDocuments.Visible = true;
					groupBoxDocuments.Enabled = true;
					panelStatus.Visible = false;
					groupBoxDelivery.Visible = false;
				}
				else if (CurrentLabelstatus == 1)
				{
					groupBoxStatusChange.Visible = false;
					groupBoxDocuments.Visible = false;
				}
				else
				{
					groupBoxStatusChange.Visible = true;
					groupBoxDocuments.Visible = true;
					groupBoxDocuments.Enabled = false;
					panelStatus.Visible = false;
					groupBoxDelivery.Visible = false;
				}
				if (CurrentLabelstatus >= 6)
				{
					if (CurrentLabelstatus == 6)
					{
						panelStatus.Visible = true;
						groupBoxDelivery.Visible = false;
					}
					else if (CurrentLabelstatus > 6)
					{
						panelStatus.Visible = true;
						panelStatus.Enabled = false;
					}
					else if (CurrentLabelstatus < 6)
					{
						panelStatus.Visible = false;
						groupBoxDelivery.Visible = false;
					}
				}
				if (CurrentLabelstatus < 9)
				{
					return;
				}
				groupBoxDelivery.Visible = true;
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
					num = checked(dateTimePickerDate.Value.Subtract(dateTimePickerFreeTimeDate.Value).Negate().Days + 1);
					textBoxDays.Text = num.ToString();
				}
				return;
			}
			if (CurrentLabelstatus == 2 && isNewRecord)
			{
				groupBoxStatusChange.Visible = true;
				groupBoxDocuments.Visible = true;
				groupBoxDocuments.Enabled = true;
				panelStatus.Visible = false;
				groupBoxDelivery.Visible = false;
			}
			else if (CurrentLabelstatus == 1)
			{
				groupBoxStatusChange.Visible = false;
				groupBoxDocuments.Visible = false;
			}
			else
			{
				groupBoxStatusChange.Visible = true;
				groupBoxDocuments.Visible = true;
				groupBoxDocuments.Enabled = false;
				panelStatus.Visible = false;
				groupBoxDelivery.Visible = false;
			}
			if (CurrentLabelstatus >= 6)
			{
				if (CurrentLabelstatus == 6)
				{
					panelStatus.Visible = true;
					groupBoxDelivery.Visible = false;
				}
				else if (CurrentLabelstatus > 6)
				{
					panelStatus.Visible = true;
					panelStatus.Enabled = false;
				}
				else if (CurrentLabelstatus < 6)
				{
					panelStatus.Visible = false;
					groupBoxDelivery.Visible = false;
				}
			}
		}

		private void label8_Click(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click_1(object sender, EventArgs e)
		{
			Close();
		}

		private void label_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				isNewRecord = true;
				return;
			}
			Label label = sender as Label;
			if (label == null || label.Font == new Font(label.Font, FontStyle.Bold))
			{
				return;
			}
			label.Font = new Font(label.Font, FontStyle.Bold);
			string[] names = Enum.GetNames(typeof(arrayStatus));
			checked
			{
				foreach (string text in names)
				{
					label.Text = label.Text.Replace(" ", "");
					if (!text.Contains(label.Text.Trim()))
					{
						continue;
					}
					CurrentLabelstatus = (int)Enum.Parse(typeof(arrayStatus), text);
					if (CurrentLabelstatus == 2 && isNewRecord)
					{
						groupBoxStatusChange.Visible = true;
						groupBoxDocuments.Visible = true;
						groupBoxDocuments.Enabled = true;
						panelStatus.Visible = false;
						groupBoxDelivery.Visible = false;
					}
					else if (CurrentLabelstatus == 1 || CurrentLabelstatus == 11)
					{
						groupBoxStatusChange.Visible = false;
						groupBoxDocuments.Visible = false;
					}
					else
					{
						groupBoxStatusChange.Visible = true;
						groupBoxDocuments.Visible = true;
						groupBoxDocuments.Enabled = false;
						panelStatus.Visible = false;
						groupBoxDelivery.Visible = false;
					}
					if (CurrentLabelstatus >= 6)
					{
						if (CurrentLabelstatus == 6)
						{
							panelStatus.Visible = true;
							panelStatus.Enabled = true;
							groupBoxDelivery.Visible = false;
						}
						else
						{
							panelStatus.Visible = true;
							panelStatus.Enabled = false;
						}
					}
					if (CurrentLabelstatus >= 9)
					{
						if (CurrentLabelstatus == 9)
						{
							groupBoxDelivery.Visible = true;
						}
						else
						{
							groupBoxDelivery.Visible = true;
							groupBoxDelivery.Enabled = false;
						}
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
					if (CurrentLabelstatus == lastStatus)
					{
						panelStatus.Enabled = false;
					}
					if (currentData != null)
					{
						DataRow dataRow2 = (!isNewRecord) ? currentData.ContainerTrackingTable.Rows[0] : currentData.ContainerTrackingTable.NewRow();
						if (dataRow2["Container_Status"] != DBNull.Value)
						{
							currentIndex = int.Parse(dataRow2["Container_Status"].ToString());
							if (CurrentLabelstatus > currentIndex + 1 && CurrentLabelstatus != 11)
							{
								ErrorHelper.ErrorMessage("Up To one higher entry or cancellation is possible only in  the CurrentLabelstatus field");
								CurrentLabelstatus = int.Parse(dataRow2["Container_Status"].ToString());
							}
							if (CurrentLabelstatus < currentIndex && CurrentLabelstatus != 11 && textBoxContainerNumber.Text != "")
							{
								ErrorHelper.ErrorMessage("You can only enter next higher CurrentLabelstatus");
								CurrentLabelstatus = int.Parse(dataRow2["Container_Status"].ToString());
							}
						}
					}
					if (lastStatus > 0 && isNewRecord)
					{
						if (CurrentLabelstatus > lastStatus + 1 && CurrentLabelstatus != 11)
						{
							ErrorHelper.ErrorMessage("Up To one higher entry or cancellation is possible only in  the CurrentLabelstatus field");
							CurrentLabelstatus = lastStatus;
						}
						if (CurrentLabelstatus < lastStatus && CurrentLabelstatus != 11)
						{
							ErrorHelper.ErrorMessage("You can only enter next higher CurrentLabelstatus");
							CurrentLabelstatus = lastStatus;
						}
					}
					else if (currentData == null && CurrentLabelstatus > 1)
					{
						CurrentLabelstatus = 1;
					}
				}
				label = null;
				formManager.IsForcedDirty = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ContainerTrackingWizardForm));
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
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxContainerSize = new System.Windows.Forms.TextBox();
			textBoxTransporter = new System.Windows.Forms.TextBox();
			textBoxSourcePort = new System.Windows.Forms.TextBox();
			textBoxDestPort = new System.Windows.Forms.TextBox();
			textBoxShippingMethod = new System.Windows.Forms.TextBox();
			textBoxVendor = new System.Windows.Forms.TextBox();
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
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxContainerNumber = new System.Windows.Forms.TextBox();
			textBoxShipper = new System.Windows.Forms.TextBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			purchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			labelVoided = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			xpButton2 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			panel2 = new System.Windows.Forms.Panel();
			labelTickCancelled = new System.Windows.Forms.Label();
			labelTickReturned = new System.Windows.Forms.Label();
			labelTickReturning = new System.Windows.Forms.Label();
			labelTickOffLoaded = new System.Windows.Forms.Label();
			labelTickShipmentDelivered = new System.Windows.Forms.Label();
			labelTickShipmentRemoval = new System.Windows.Forms.Label();
			labelTickCustomsChecking = new System.Windows.Forms.Label();
			labelTickDORelease = new System.Windows.Forms.Label();
			labelTickArrived = new System.Windows.Forms.Label();
			labelTickOriginalDocumentReceived = new System.Windows.Forms.Label();
			labelTickShipped = new System.Windows.Forms.Label();
			labelReturned = new System.Windows.Forms.Label();
			labelCancelled = new System.Windows.Forms.Label();
			labelReturning = new System.Windows.Forms.Label();
			labelOffLoaded = new System.Windows.Forms.Label();
			labelShipmentDelivered = new System.Windows.Forms.Label();
			labelShipmentRemoval = new System.Windows.Forms.Label();
			labelCustomsChecking = new System.Windows.Forms.Label();
			labelDORelease = new System.Windows.Forms.Label();
			labelArrived = new System.Windows.Forms.Label();
			labelOriginalDocumentReceived = new System.Windows.Forms.Label();
			labelShipped = new System.Windows.Forms.Label();
			groupBoxStatusChange = new Infragistics.Win.Misc.UltraGroupBox();
			groupBoxDelivery = new Infragistics.Win.Misc.UltraGroupBox();
			radioButtonDate = new System.Windows.Forms.RadioButton();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			radioButtonDays = new System.Windows.Forms.RadioButton();
			dateTimePickerFreeTimeDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerFreeTime = new System.Windows.Forms.DateTimePicker();
			labelDeliveryFreeTime = new System.Windows.Forms.Label();
			panelStatus = new System.Windows.Forms.Panel();
			labelDriver = new System.Windows.Forms.Label();
			textBoxDriver = new System.Windows.Forms.TextBox();
			comboBoxTransporter = new Micromind.DataControls.TransporterComboBox();
			labelTransporter = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTruckNumber = new System.Windows.Forms.TextBox();
			labelTrcukNumber = new System.Windows.Forms.Label();
			groupBoxDocuments = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxOriginCertificate = new System.Windows.Forms.CheckBox();
			checkBoxhealthCertificate = new System.Windows.Forms.CheckBox();
			checkBoxPL = new System.Windows.Forms.CheckBox();
			checkBoxInvoice = new System.Windows.Forms.CheckBox();
			checkBoxBL = new System.Windows.Forms.CheckBox();
			panelStatusDate = new System.Windows.Forms.Panel();
			labelDateCancelled = new System.Windows.Forms.Label();
			labelDateReturned = new System.Windows.Forms.Label();
			labelDateReturning = new System.Windows.Forms.Label();
			labelDateOffLoaded = new System.Windows.Forms.Label();
			labelDateShipmentDelivered = new System.Windows.Forms.Label();
			labelDateShipmentRemoval = new System.Windows.Forms.Label();
			labelDateCustomsChecking = new System.Windows.Forms.Label();
			labelDateDORelease = new System.Windows.Forms.Label();
			labelDateArrived = new System.Windows.Forms.Label();
			labelDateOriginalDocumentReceived = new System.Windows.Forms.Label();
			labelDateShipped = new System.Windows.Forms.Label();
			labelStatusCancelled = new System.Windows.Forms.Label();
			labelStatusReturned = new System.Windows.Forms.Label();
			labelStatusReturning = new System.Windows.Forms.Label();
			labelStatusOffLoaded = new System.Windows.Forms.Label();
			labelStatusShipmentDelivered = new System.Windows.Forms.Label();
			labelStatusShipmentRemoval = new System.Windows.Forms.Label();
			labelStatusCustomsChecking = new System.Windows.Forms.Label();
			labelStatusDORelease = new System.Windows.Forms.Label();
			labelStatusArrived = new System.Windows.Forms.Label();
			labelStatusOriginalDocumentReceived = new System.Windows.Forms.Label();
			labelStatusShipped = new System.Windows.Forms.Label();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxContainerStatus = new Micromind.DataControls.ContainerStstusComboBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			buttonNext = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			contextMenuStrip1.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxStatusChange).BeginInit();
			groupBoxStatusChange.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxDelivery).BeginInit();
			groupBoxDelivery.SuspendLayout();
			panelStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupBoxDocuments).BeginInit();
			groupBoxDocuments.SuspendLayout();
			panelStatusDate.SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(707, 31);
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
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(598, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(94, 20);
			dateTimePickerDate.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(501, 30);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRef1.Location = new System.Drawing.Point(567, 26);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.ReadOnly = true;
			textBoxRef1.Size = new System.Drawing.Size(125, 20);
			textBoxRef1.TabIndex = 5;
			textBoxRef1.TabStop = false;
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(textBoxContainerSize);
			panelDetails.Controls.Add(textBoxTransporter);
			panelDetails.Controls.Add(textBoxSourcePort);
			panelDetails.Controls.Add(textBoxDestPort);
			panelDetails.Controls.Add(textBoxShippingMethod);
			panelDetails.Controls.Add(textBoxVendor);
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
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxContainerNumber);
			panelDetails.Controls.Add(textBoxShipper);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Location = new System.Drawing.Point(0, 35);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(695, 128);
			panelDetails.TabIndex = 0;
			panelDetails.Paint += new System.Windows.Forms.PaintEventHandler(panelDetails_Paint);
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
			comboBoxSysDoc.Location = new System.Drawing.Point(592, 49);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 180;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.Visible = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(506, 52);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 182;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			ultraFormattedLinkLabel5.Visible = false;
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(215, 164);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel2.TabIndex = 183;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			textBoxVoucherNumber.Location = new System.Drawing.Point(329, 160);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(140, 20);
			textBoxVoucherNumber.TabIndex = 181;
			textBoxContainerSize.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxContainerSize.Location = new System.Drawing.Point(594, 78);
			textBoxContainerSize.MaxLength = 20;
			textBoxContainerSize.Name = "textBoxContainerSize";
			textBoxContainerSize.ReadOnly = true;
			textBoxContainerSize.Size = new System.Drawing.Size(98, 20);
			textBoxContainerSize.TabIndex = 178;
			textBoxContainerSize.TabStop = false;
			textBoxTransporter.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTransporter.Location = new System.Drawing.Point(573, 101);
			textBoxTransporter.MaxLength = 20;
			textBoxTransporter.Name = "textBoxTransporter";
			textBoxTransporter.ReadOnly = true;
			textBoxTransporter.Size = new System.Drawing.Size(119, 20);
			textBoxTransporter.TabIndex = 177;
			textBoxTransporter.TabStop = false;
			textBoxSourcePort.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSourcePort.Location = new System.Drawing.Point(484, 52);
			textBoxSourcePort.MaxLength = 20;
			textBoxSourcePort.Name = "textBoxSourcePort";
			textBoxSourcePort.ReadOnly = true;
			textBoxSourcePort.Size = new System.Drawing.Size(94, 20);
			textBoxSourcePort.TabIndex = 176;
			textBoxSourcePort.TabStop = false;
			textBoxDestPort.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDestPort.Location = new System.Drawing.Point(297, 52);
			textBoxDestPort.MaxLength = 20;
			textBoxDestPort.Name = "textBoxDestPort";
			textBoxDestPort.ReadOnly = true;
			textBoxDestPort.Size = new System.Drawing.Size(116, 20);
			textBoxDestPort.TabIndex = 175;
			textBoxDestPort.TabStop = false;
			textBoxShippingMethod.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxShippingMethod.Location = new System.Drawing.Point(97, 52);
			textBoxShippingMethod.MaxLength = 20;
			textBoxShippingMethod.Name = "textBoxShippingMethod";
			textBoxShippingMethod.ReadOnly = true;
			textBoxShippingMethod.Size = new System.Drawing.Size(142, 20);
			textBoxShippingMethod.TabIndex = 174;
			textBoxShippingMethod.TabStop = false;
			textBoxVendor.Location = new System.Drawing.Point(97, 29);
			textBoxVendor.MaxLength = 20;
			textBoxVendor.Name = "textBoxVendor";
			textBoxVendor.ReadOnly = true;
			textBoxVendor.Size = new System.Drawing.Size(338, 20);
			textBoxVendor.TabIndex = 173;
			textBoxVendor.TabStop = false;
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(11, 29);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(44, 13);
			label17.TabIndex = 171;
			label17.Text = "Vendor:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 49);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(48, 26);
			label9.TabIndex = 166;
			label9.Text = "Shipping\r\nMethod:";
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(325, 5);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 21;
			buttonSelectDocument.TabStop = false;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(208, 78);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(31, 13);
			label12.TabIndex = 165;
			label12.Text = "ETA:";
			dateTimePickerATD.Checked = false;
			dateTimePickerATD.CustomFormat = " ";
			dateTimePickerATD.Enabled = false;
			dateTimePickerATD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerATD.Location = new System.Drawing.Point(97, 75);
			dateTimePickerATD.Name = "dateTimePickerATD";
			dateTimePickerATD.ShowCheckBox = true;
			dateTimePickerATD.Size = new System.Drawing.Size(109, 20);
			dateTimePickerATD.TabIndex = 10;
			dateTimePickerATD.TabStop = false;
			dateTimePickerATD.Value = new System.DateTime(0L);
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(11, 82);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(32, 13);
			label11.TabIndex = 164;
			label11.Text = "ATD:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(419, 55);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(66, 13);
			label8.TabIndex = 162;
			label8.Text = "Source Port:";
			label8.Click += new System.EventHandler(label8_Click);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(514, 81);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(78, 13);
			label7.TabIndex = 16;
			label7.Text = "Container Size:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(509, 105);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(64, 13);
			label6.TabIndex = 159;
			label6.Text = "Transporter:";
			textBoxValue.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxValue.CustomReportFieldName = "";
			textBoxValue.CustomReportKey = "";
			textBoxValue.CustomReportValueType = 1;
			textBoxValue.IsComboTextBox = false;
			textBoxValue.Location = new System.Drawing.Point(422, 98);
			textBoxValue.Name = "textBoxValue";
			textBoxValue.ReadOnly = true;
			textBoxValue.Size = new System.Drawing.Size(86, 20);
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
			label16.Location = new System.Drawing.Point(370, 77);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(46, 13);
			label16.TabIndex = 150;
			label16.Text = "Shipper:";
			textBoxWeight.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxWeight.CustomReportFieldName = "";
			textBoxWeight.CustomReportKey = "";
			textBoxWeight.CustomReportValueType = 1;
			textBoxWeight.IsComboTextBox = false;
			textBoxWeight.Location = new System.Drawing.Point(97, 97);
			textBoxWeight.MaxLength = 10;
			textBoxWeight.Name = "textBoxWeight";
			textBoxWeight.ReadOnly = true;
			textBoxWeight.Size = new System.Drawing.Size(109, 20);
			textBoxWeight.TabIndex = 12;
			textBoxWeight.TabStop = false;
			textBoxWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(449, 164);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(79, 13);
			label14.TabIndex = 18;
			label14.Text = "Clearing Agent:";
			label14.Visible = false;
			textBoxClearingAgent.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxClearingAgent.Location = new System.Drawing.Point(518, 157);
			textBoxClearingAgent.MaxLength = 20;
			textBoxClearingAgent.Name = "textBoxClearingAgent";
			textBoxClearingAgent.ReadOnly = true;
			textBoxClearingAgent.Size = new System.Drawing.Size(118, 20);
			textBoxClearingAgent.TabIndex = 19;
			textBoxClearingAgent.TabStop = false;
			textBoxClearingAgent.Visible = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(371, 102);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(37, 13);
			label5.TabIndex = 146;
			label5.Text = "Value:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(208, 102);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(48, 13);
			label13.TabIndex = 146;
			label13.Text = "BOL No:";
			textBoxBOL.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBOL.Location = new System.Drawing.Point(257, 98);
			textBoxBOL.MaxLength = 20;
			textBoxBOL.Name = "textBoxBOL";
			textBoxBOL.ReadOnly = true;
			textBoxBOL.Size = new System.Drawing.Size(110, 20);
			textBoxBOL.TabIndex = 13;
			textBoxBOL.TabStop = false;
			dateTimePickerETA.Checked = false;
			dateTimePickerETA.CustomFormat = " ";
			dateTimePickerETA.Enabled = false;
			dateTimePickerETA.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerETA.Location = new System.Drawing.Point(257, 75);
			dateTimePickerETA.Name = "dateTimePickerETA";
			dateTimePickerETA.ShowCheckBox = true;
			dateTimePickerETA.Size = new System.Drawing.Size(109, 20);
			dateTimePickerETA.TabIndex = 11;
			dateTimePickerETA.TabStop = false;
			dateTimePickerETA.Value = new System.DateTime(0L);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(242, 55);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(57, 13);
			label4.TabIndex = 142;
			label4.Text = "Dest. Port:";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(11, 101);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(44, 13);
			label15.TabIndex = 142;
			label15.Text = "Weight:";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(11, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(85, 13);
			label2.TabIndex = 20;
			label2.Text = "Container No:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(538, 7);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 4;
			mmLabel1.Text = "Date:";
			textBoxContainerNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxContainerNumber.Location = new System.Drawing.Point(97, 6);
			textBoxContainerNumber.MaxLength = 64;
			textBoxContainerNumber.Name = "textBoxContainerNumber";
			textBoxContainerNumber.ReadOnly = true;
			textBoxContainerNumber.Size = new System.Drawing.Size(224, 20);
			textBoxContainerNumber.TabIndex = 4;
			textBoxContainerNumber.TabStop = false;
			textBoxShipper.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxShipper.Location = new System.Drawing.Point(422, 76);
			textBoxShipper.MaxLength = 15;
			textBoxShipper.Name = "textBoxShipper";
			textBoxShipper.ReadOnly = true;
			textBoxShipper.Size = new System.Drawing.Size(86, 20);
			textBoxShipper.TabIndex = 7;
			textBoxShipper.TabStop = false;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxVendor;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance5;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
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
			comboBoxVendor.Location = new System.Drawing.Point(253, 29);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ReadOnly = true;
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(103, 20);
			comboBoxVendor.TabIndex = 179;
			comboBoxVendor.TabStop = false;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendor.Visible = false;
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
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(569, 560);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(132, 58);
			labelVoided.TabIndex = 132;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			textBoxNote.Location = new System.Drawing.Point(51, 548);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(512, 70);
			textBoxNote.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(11, 551);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 175;
			label3.Text = "Note:";
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panel1.Controls.Add(xpButton1);
			panel1.Controls.Add(buttonNew);
			panel1.Controls.Add(buttonDelete);
			panel1.Controls.Add(line1);
			panel1.Controls.Add(xpButton2);
			panel1.Controls.Add(buttonSave);
			panel1.Location = new System.Drawing.Point(3, 648);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(705, 48);
			panel1.TabIndex = 186;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(564, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 5;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click_1);
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
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(219, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Top;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Silver;
			line1.Location = new System.Drawing.Point(0, 0);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(705, 1);
			line1.TabIndex = 0;
			line1.TabStop = false;
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(438, 8);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(96, 24);
			xpButton2.TabIndex = 4;
			xpButton2.Text = "&Next";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Visible = false;
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
			ultraGroupBox1.Controls.Add(panel2);
			ultraGroupBox1.Controls.Add(groupBoxStatusChange);
			ultraGroupBox1.Location = new System.Drawing.Point(11, 237);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(686, 304);
			ultraGroupBox1.TabIndex = 188;
			ultraGroupBox1.Text = "Container Status Change";
			panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel2.Controls.Add(labelTickCancelled);
			panel2.Controls.Add(labelTickReturned);
			panel2.Controls.Add(labelTickReturning);
			panel2.Controls.Add(labelTickOffLoaded);
			panel2.Controls.Add(labelTickShipmentDelivered);
			panel2.Controls.Add(labelTickShipmentRemoval);
			panel2.Controls.Add(labelTickCustomsChecking);
			panel2.Controls.Add(labelTickDORelease);
			panel2.Controls.Add(labelTickArrived);
			panel2.Controls.Add(labelTickOriginalDocumentReceived);
			panel2.Controls.Add(labelTickShipped);
			panel2.Controls.Add(labelReturned);
			panel2.Controls.Add(labelCancelled);
			panel2.Controls.Add(labelReturning);
			panel2.Controls.Add(labelOffLoaded);
			panel2.Controls.Add(labelShipmentDelivered);
			panel2.Controls.Add(labelShipmentRemoval);
			panel2.Controls.Add(labelCustomsChecking);
			panel2.Controls.Add(labelDORelease);
			panel2.Controls.Add(labelArrived);
			panel2.Controls.Add(labelOriginalDocumentReceived);
			panel2.Controls.Add(labelShipped);
			panel2.Cursor = System.Windows.Forms.Cursors.Hand;
			panel2.Location = new System.Drawing.Point(6, 19);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(242, 273);
			panel2.TabIndex = 4;
			labelTickCancelled.AutoSize = true;
			labelTickCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickCancelled.Location = new System.Drawing.Point(7, 237);
			labelTickCancelled.Name = "labelTickCancelled";
			labelTickCancelled.Size = new System.Drawing.Size(54, 17);
			labelTickCancelled.TabIndex = 215;
			labelTickCancelled.Text = "label27";
			labelTickReturned.AutoSize = true;
			labelTickReturned.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickReturned.Location = new System.Drawing.Point(7, 216);
			labelTickReturned.Name = "labelTickReturned";
			labelTickReturned.Size = new System.Drawing.Size(54, 17);
			labelTickReturned.TabIndex = 214;
			labelTickReturned.Text = "label26";
			labelTickReturning.AutoSize = true;
			labelTickReturning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickReturning.Location = new System.Drawing.Point(7, 193);
			labelTickReturning.Name = "labelTickReturning";
			labelTickReturning.Size = new System.Drawing.Size(54, 17);
			labelTickReturning.TabIndex = 213;
			labelTickReturning.Text = "label25";
			labelTickOffLoaded.AutoSize = true;
			labelTickOffLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickOffLoaded.Location = new System.Drawing.Point(7, 174);
			labelTickOffLoaded.Name = "labelTickOffLoaded";
			labelTickOffLoaded.Size = new System.Drawing.Size(54, 17);
			labelTickOffLoaded.TabIndex = 212;
			labelTickOffLoaded.Text = "label24";
			labelTickShipmentDelivered.AutoSize = true;
			labelTickShipmentDelivered.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickShipmentDelivered.Location = new System.Drawing.Point(7, 153);
			labelTickShipmentDelivered.Name = "labelTickShipmentDelivered";
			labelTickShipmentDelivered.Size = new System.Drawing.Size(54, 17);
			labelTickShipmentDelivered.TabIndex = 211;
			labelTickShipmentDelivered.Text = "label23";
			labelTickShipmentRemoval.AutoSize = true;
			labelTickShipmentRemoval.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickShipmentRemoval.Location = new System.Drawing.Point(7, 132);
			labelTickShipmentRemoval.Name = "labelTickShipmentRemoval";
			labelTickShipmentRemoval.Size = new System.Drawing.Size(54, 17);
			labelTickShipmentRemoval.TabIndex = 210;
			labelTickShipmentRemoval.Text = "label22";
			labelTickCustomsChecking.AutoSize = true;
			labelTickCustomsChecking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickCustomsChecking.Location = new System.Drawing.Point(7, 111);
			labelTickCustomsChecking.Name = "labelTickCustomsChecking";
			labelTickCustomsChecking.Size = new System.Drawing.Size(54, 17);
			labelTickCustomsChecking.TabIndex = 209;
			labelTickCustomsChecking.Text = "label21";
			labelTickDORelease.AutoSize = true;
			labelTickDORelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickDORelease.Location = new System.Drawing.Point(7, 90);
			labelTickDORelease.Name = "labelTickDORelease";
			labelTickDORelease.Size = new System.Drawing.Size(54, 17);
			labelTickDORelease.TabIndex = 208;
			labelTickDORelease.Text = "label20";
			labelTickArrived.AutoSize = true;
			labelTickArrived.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickArrived.Location = new System.Drawing.Point(7, 69);
			labelTickArrived.Name = "labelTickArrived";
			labelTickArrived.Size = new System.Drawing.Size(54, 17);
			labelTickArrived.TabIndex = 207;
			labelTickArrived.Text = "label19";
			labelTickOriginalDocumentReceived.AutoSize = true;
			labelTickOriginalDocumentReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickOriginalDocumentReceived.Location = new System.Drawing.Point(7, 48);
			labelTickOriginalDocumentReceived.Name = "labelTickOriginalDocumentReceived";
			labelTickOriginalDocumentReceived.Size = new System.Drawing.Size(54, 17);
			labelTickOriginalDocumentReceived.TabIndex = 206;
			labelTickOriginalDocumentReceived.Text = "label18";
			labelTickShipped.AutoSize = true;
			labelTickShipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTickShipped.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			labelTickShipped.Location = new System.Drawing.Point(7, 28);
			labelTickShipped.Name = "labelTickShipped";
			labelTickShipped.Size = new System.Drawing.Size(54, 17);
			labelTickShipped.TabIndex = 183;
			labelTickShipped.Text = "label10";
			labelReturned.AutoSize = true;
			labelReturned.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelReturned.Location = new System.Drawing.Point(35, 216);
			labelReturned.Name = "labelReturned";
			labelReturned.Size = new System.Drawing.Size(67, 17);
			labelReturned.TabIndex = 205;
			labelReturned.Text = "Returned";
			labelReturned.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelReturned.Click += new System.EventHandler(label_Click);
			labelCancelled.AutoSize = true;
			labelCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelCancelled.Location = new System.Drawing.Point(35, 237);
			labelCancelled.Name = "labelCancelled";
			labelCancelled.Size = new System.Drawing.Size(70, 17);
			labelCancelled.TabIndex = 204;
			labelCancelled.Text = "Cancelled";
			labelCancelled.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelCancelled.Click += new System.EventHandler(label_Click);
			labelReturning.AutoSize = true;
			labelReturning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelReturning.Location = new System.Drawing.Point(35, 195);
			labelReturning.Name = "labelReturning";
			labelReturning.Size = new System.Drawing.Size(70, 17);
			labelReturning.TabIndex = 203;
			labelReturning.Text = "Returning";
			labelReturning.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelReturning.Click += new System.EventHandler(label_Click);
			labelOffLoaded.AutoSize = true;
			labelOffLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelOffLoaded.Location = new System.Drawing.Point(35, 174);
			labelOffLoaded.Name = "labelOffLoaded";
			labelOffLoaded.Size = new System.Drawing.Size(79, 17);
			labelOffLoaded.TabIndex = 202;
			labelOffLoaded.Text = "Off Loaded";
			labelOffLoaded.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelOffLoaded.Click += new System.EventHandler(label_Click);
			labelShipmentDelivered.AutoSize = true;
			labelShipmentDelivered.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelShipmentDelivered.Location = new System.Drawing.Point(35, 153);
			labelShipmentDelivered.Name = "labelShipmentDelivered";
			labelShipmentDelivered.Size = new System.Drawing.Size(131, 17);
			labelShipmentDelivered.TabIndex = 201;
			labelShipmentDelivered.Text = "Shipment Delivered";
			labelShipmentDelivered.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelShipmentDelivered.Click += new System.EventHandler(label_Click);
			labelShipmentRemoval.AutoSize = true;
			labelShipmentRemoval.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelShipmentRemoval.Location = new System.Drawing.Point(35, 132);
			labelShipmentRemoval.Name = "labelShipmentRemoval";
			labelShipmentRemoval.Size = new System.Drawing.Size(126, 17);
			labelShipmentRemoval.TabIndex = 200;
			labelShipmentRemoval.Text = "Shipment Removal";
			labelShipmentRemoval.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelShipmentRemoval.Click += new System.EventHandler(label_Click);
			labelCustomsChecking.AutoSize = true;
			labelCustomsChecking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelCustomsChecking.Location = new System.Drawing.Point(35, 111);
			labelCustomsChecking.Name = "labelCustomsChecking";
			labelCustomsChecking.Size = new System.Drawing.Size(124, 17);
			labelCustomsChecking.TabIndex = 199;
			labelCustomsChecking.Text = "Customs Checking";
			labelCustomsChecking.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelCustomsChecking.Click += new System.EventHandler(label_Click);
			labelDORelease.AutoSize = true;
			labelDORelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelDORelease.Location = new System.Drawing.Point(35, 90);
			labelDORelease.Name = "labelDORelease";
			labelDORelease.Size = new System.Drawing.Size(85, 17);
			labelDORelease.TabIndex = 198;
			labelDORelease.Text = "DO Release";
			labelDORelease.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelDORelease.Click += new System.EventHandler(label_Click);
			labelArrived.AutoSize = true;
			labelArrived.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelArrived.Location = new System.Drawing.Point(35, 69);
			labelArrived.Name = "labelArrived";
			labelArrived.Size = new System.Drawing.Size(53, 17);
			labelArrived.TabIndex = 197;
			labelArrived.Text = "Arrived";
			labelArrived.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelArrived.Click += new System.EventHandler(label_Click);
			labelOriginalDocumentReceived.AutoSize = true;
			labelOriginalDocumentReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelOriginalDocumentReceived.Location = new System.Drawing.Point(35, 48);
			labelOriginalDocumentReceived.Name = "labelOriginalDocumentReceived";
			labelOriginalDocumentReceived.Size = new System.Drawing.Size(180, 17);
			labelOriginalDocumentReceived.TabIndex = 196;
			labelOriginalDocumentReceived.Text = "OriginalDocumentReceived";
			labelOriginalDocumentReceived.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelOriginalDocumentReceived.Click += new System.EventHandler(label_Click);
			labelShipped.AutoSize = true;
			labelShipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelShipped.Location = new System.Drawing.Point(33, 28);
			labelShipped.Name = "labelShipped";
			labelShipped.Size = new System.Drawing.Size(60, 17);
			labelShipped.TabIndex = 195;
			labelShipped.Text = "Shipped";
			labelShipped.FontChanged += new System.EventHandler(label_ForeColorChanged);
			labelShipped.Click += new System.EventHandler(label_Click);
			groupBoxStatusChange.Controls.Add(groupBoxDelivery);
			groupBoxStatusChange.Controls.Add(panelStatus);
			groupBoxStatusChange.Controls.Add(groupBoxDocuments);
			groupBoxStatusChange.Location = new System.Drawing.Point(254, 12);
			groupBoxStatusChange.Name = "groupBoxStatusChange";
			groupBoxStatusChange.Size = new System.Drawing.Size(404, 280);
			groupBoxStatusChange.TabIndex = 5;
			groupBoxStatusChange.Visible = false;
			groupBoxDelivery.Controls.Add(radioButtonDate);
			groupBoxDelivery.Controls.Add(textBoxDays);
			groupBoxDelivery.Controls.Add(radioButtonDays);
			groupBoxDelivery.Controls.Add(dateTimePickerFreeTimeDate);
			groupBoxDelivery.Controls.Add(dateTimePickerFreeTime);
			groupBoxDelivery.Controls.Add(labelDeliveryFreeTime);
			groupBoxDelivery.Location = new System.Drawing.Point(6, 185);
			groupBoxDelivery.Name = "groupBoxDelivery";
			groupBoxDelivery.Size = new System.Drawing.Size(318, 85);
			groupBoxDelivery.TabIndex = 177;
			groupBoxDelivery.Text = "Delivery ";
			groupBoxDelivery.Visible = false;
			radioButtonDate.AutoSize = true;
			radioButtonDate.Location = new System.Drawing.Point(24, 24);
			radioButtonDate.Name = "radioButtonDate";
			radioButtonDate.Size = new System.Drawing.Size(51, 17);
			radioButtonDate.TabIndex = 182;
			radioButtonDate.Text = "Date:";
			radioButtonDate.UseVisualStyleBackColor = true;
			radioButtonDate.CheckedChanged += new System.EventHandler(radioButtonDate_CheckedChanged);
			textBoxDays.AllowDecimal = false;
			textBoxDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxDays.IsComboTextBox = false;
			textBoxDays.Location = new System.Drawing.Point(253, 24);
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
			textBoxDays.Size = new System.Drawing.Size(38, 20);
			textBoxDays.TabIndex = 2;
			textBoxDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDays.TextChanged += new System.EventHandler(textBoxDays_TextChanged);
			radioButtonDays.AutoSize = true;
			radioButtonDays.Location = new System.Drawing.Point(197, 25);
			radioButtonDays.Name = "radioButtonDays";
			radioButtonDays.Size = new System.Drawing.Size(52, 17);
			radioButtonDays.TabIndex = 1;
			radioButtonDays.Text = "Days:";
			radioButtonDays.UseVisualStyleBackColor = true;
			dateTimePickerFreeTimeDate.Checked = false;
			dateTimePickerFreeTimeDate.CustomFormat = " ";
			dateTimePickerFreeTimeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerFreeTimeDate.Location = new System.Drawing.Point(87, 24);
			dateTimePickerFreeTimeDate.Name = "dateTimePickerFreeTimeDate";
			dateTimePickerFreeTimeDate.ShowCheckBox = true;
			dateTimePickerFreeTimeDate.Size = new System.Drawing.Size(101, 20);
			dateTimePickerFreeTimeDate.TabIndex = 0;
			dateTimePickerFreeTimeDate.TabStop = false;
			dateTimePickerFreeTimeDate.Value = new System.DateTime(0L);
			dateTimePickerFreeTimeDate.ValueChanged += new System.EventHandler(dateTimePickerFreeTimeDate_ValueChanged);
			dateTimePickerFreeTime.Checked = false;
			dateTimePickerFreeTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerFreeTime.Location = new System.Drawing.Point(87, 49);
			dateTimePickerFreeTime.Name = "dateTimePickerFreeTime";
			dateTimePickerFreeTime.ShowCheckBox = true;
			dateTimePickerFreeTime.ShowUpDown = true;
			dateTimePickerFreeTime.Size = new System.Drawing.Size(116, 20);
			dateTimePickerFreeTime.TabIndex = 3;
			labelDeliveryFreeTime.AutoSize = true;
			labelDeliveryFreeTime.Location = new System.Drawing.Point(42, 55);
			labelDeliveryFreeTime.Name = "labelDeliveryFreeTime";
			labelDeliveryFreeTime.Size = new System.Drawing.Size(33, 13);
			labelDeliveryFreeTime.TabIndex = 178;
			labelDeliveryFreeTime.Text = "Time:";
			panelStatus.Controls.Add(labelDriver);
			panelStatus.Controls.Add(textBoxDriver);
			panelStatus.Controls.Add(comboBoxTransporter);
			panelStatus.Controls.Add(labelTransporter);
			panelStatus.Controls.Add(textBoxTruckNumber);
			panelStatus.Controls.Add(labelTrcukNumber);
			panelStatus.Location = new System.Drawing.Point(6, 96);
			panelStatus.Name = "panelStatus";
			panelStatus.Size = new System.Drawing.Size(318, 83);
			panelStatus.TabIndex = 0;
			panelStatus.Visible = false;
			labelDriver.AutoSize = true;
			labelDriver.Location = new System.Drawing.Point(7, 8);
			labelDriver.Name = "labelDriver";
			labelDriver.Size = new System.Drawing.Size(38, 13);
			labelDriver.TabIndex = 178;
			labelDriver.Text = "Driver:";
			textBoxDriver.BackColor = System.Drawing.SystemColors.Window;
			textBoxDriver.Location = new System.Drawing.Point(86, 8);
			textBoxDriver.MaxLength = 20;
			textBoxDriver.Name = "textBoxDriver";
			textBoxDriver.Size = new System.Drawing.Size(196, 20);
			textBoxDriver.TabIndex = 0;
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
			comboBoxTransporter.Location = new System.Drawing.Point(86, 31);
			comboBoxTransporter.MaxDropDownItems = 12;
			comboBoxTransporter.Name = "comboBoxTransporter";
			comboBoxTransporter.ShowInactiveItems = false;
			comboBoxTransporter.ShowQuickAdd = true;
			comboBoxTransporter.Size = new System.Drawing.Size(196, 20);
			comboBoxTransporter.TabIndex = 1;
			comboBoxTransporter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance17.FontData.BoldAsString = "False";
			appearance17.FontData.Name = "Tahoma";
			labelTransporter.Appearance = appearance17;
			labelTransporter.AutoSize = true;
			labelTransporter.Location = new System.Drawing.Point(8, 31);
			labelTransporter.Name = "labelTransporter";
			labelTransporter.Size = new System.Drawing.Size(65, 15);
			labelTransporter.TabIndex = 170;
			labelTransporter.TabStop = true;
			labelTransporter.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTransporter.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTransporter.Value = "Transporter:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			labelTransporter.VisitedLinkAppearance = appearance18;
			textBoxTruckNumber.BackColor = System.Drawing.SystemColors.Window;
			textBoxTruckNumber.Location = new System.Drawing.Point(86, 55);
			textBoxTruckNumber.MaxLength = 20;
			textBoxTruckNumber.Name = "textBoxTruckNumber";
			textBoxTruckNumber.Size = new System.Drawing.Size(196, 20);
			textBoxTruckNumber.TabIndex = 2;
			labelTrcukNumber.AutoSize = true;
			labelTrcukNumber.Location = new System.Drawing.Point(7, 55);
			labelTrcukNumber.Name = "labelTrcukNumber";
			labelTrcukNumber.Size = new System.Drawing.Size(57, 13);
			labelTrcukNumber.TabIndex = 165;
			labelTrcukNumber.Text = "Truck NO:";
			groupBoxDocuments.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.None;
			groupBoxDocuments.Controls.Add(checkBoxOriginCertificate);
			groupBoxDocuments.Controls.Add(checkBoxhealthCertificate);
			groupBoxDocuments.Controls.Add(checkBoxPL);
			groupBoxDocuments.Controls.Add(checkBoxInvoice);
			groupBoxDocuments.Controls.Add(checkBoxBL);
			groupBoxDocuments.Location = new System.Drawing.Point(6, 14);
			groupBoxDocuments.Name = "groupBoxDocuments";
			groupBoxDocuments.Size = new System.Drawing.Size(318, 76);
			groupBoxDocuments.TabIndex = 2;
			groupBoxDocuments.Text = "Documents Received";
			checkBoxOriginCertificate.AutoSize = true;
			checkBoxOriginCertificate.Location = new System.Drawing.Point(17, 50);
			checkBoxOriginCertificate.Name = "checkBoxOriginCertificate";
			checkBoxOriginCertificate.Size = new System.Drawing.Size(115, 17);
			checkBoxOriginCertificate.TabIndex = 176;
			checkBoxOriginCertificate.Text = "Certificate of Origin";
			checkBoxOriginCertificate.UseVisualStyleBackColor = true;
			checkBoxhealthCertificate.AutoSize = true;
			checkBoxhealthCertificate.Location = new System.Drawing.Point(201, 27);
			checkBoxhealthCertificate.Name = "checkBoxhealthCertificate";
			checkBoxhealthCertificate.Size = new System.Drawing.Size(107, 17);
			checkBoxhealthCertificate.TabIndex = 175;
			checkBoxhealthCertificate.Text = "Health Certificate";
			checkBoxhealthCertificate.UseVisualStyleBackColor = true;
			checkBoxPL.AutoSize = true;
			checkBoxPL.Location = new System.Drawing.Point(121, 27);
			checkBoxPL.Name = "checkBoxPL";
			checkBoxPL.Size = new System.Drawing.Size(84, 17);
			checkBoxPL.TabIndex = 174;
			checkBoxPL.Text = "Packing List";
			checkBoxPL.UseVisualStyleBackColor = true;
			checkBoxInvoice.AutoSize = true;
			checkBoxInvoice.Location = new System.Drawing.Point(63, 27);
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
			panelStatusDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panelStatusDate.Controls.Add(labelDateCancelled);
			panelStatusDate.Controls.Add(labelDateReturned);
			panelStatusDate.Controls.Add(labelDateReturning);
			panelStatusDate.Controls.Add(labelDateOffLoaded);
			panelStatusDate.Controls.Add(labelDateShipmentDelivered);
			panelStatusDate.Controls.Add(labelDateShipmentRemoval);
			panelStatusDate.Controls.Add(labelDateCustomsChecking);
			panelStatusDate.Controls.Add(labelDateDORelease);
			panelStatusDate.Controls.Add(labelDateArrived);
			panelStatusDate.Controls.Add(labelDateOriginalDocumentReceived);
			panelStatusDate.Controls.Add(labelDateShipped);
			panelStatusDate.Controls.Add(labelStatusCancelled);
			panelStatusDate.Controls.Add(labelStatusReturned);
			panelStatusDate.Controls.Add(labelStatusReturning);
			panelStatusDate.Controls.Add(labelStatusOffLoaded);
			panelStatusDate.Controls.Add(labelStatusShipmentDelivered);
			panelStatusDate.Controls.Add(labelStatusShipmentRemoval);
			panelStatusDate.Controls.Add(labelStatusCustomsChecking);
			panelStatusDate.Controls.Add(labelStatusDORelease);
			panelStatusDate.Controls.Add(labelStatusArrived);
			panelStatusDate.Controls.Add(labelStatusOriginalDocumentReceived);
			panelStatusDate.Controls.Add(labelStatusShipped);
			panelStatusDate.Location = new System.Drawing.Point(14, 165);
			panelStatusDate.Name = "panelStatusDate";
			panelStatusDate.Size = new System.Drawing.Size(680, 61);
			panelStatusDate.TabIndex = 190;
			labelDateCancelled.AutoSize = true;
			labelDateCancelled.Location = new System.Drawing.Point(621, 34);
			labelDateCancelled.Name = "labelDateCancelled";
			labelDateCancelled.Size = new System.Drawing.Size(0, 13);
			labelDateCancelled.TabIndex = 218;
			labelDateCancelled.Visible = false;
			labelDateReturned.AutoSize = true;
			labelDateReturned.Location = new System.Drawing.Point(558, 34);
			labelDateReturned.Name = "labelDateReturned";
			labelDateReturned.Size = new System.Drawing.Size(0, 13);
			labelDateReturned.TabIndex = 217;
			labelDateReturned.Visible = false;
			labelDateReturning.AutoSize = true;
			labelDateReturning.Location = new System.Drawing.Point(496, 34);
			labelDateReturning.Name = "labelDateReturning";
			labelDateReturning.Size = new System.Drawing.Size(0, 13);
			labelDateReturning.TabIndex = 216;
			labelDateReturning.Visible = false;
			labelDateOffLoaded.AutoSize = true;
			labelDateOffLoaded.Location = new System.Drawing.Point(431, 34);
			labelDateOffLoaded.Name = "labelDateOffLoaded";
			labelDateOffLoaded.Size = new System.Drawing.Size(0, 13);
			labelDateOffLoaded.TabIndex = 215;
			labelDateOffLoaded.Visible = false;
			labelDateShipmentDelivered.AutoSize = true;
			labelDateShipmentDelivered.Location = new System.Drawing.Point(367, 34);
			labelDateShipmentDelivered.Name = "labelDateShipmentDelivered";
			labelDateShipmentDelivered.Size = new System.Drawing.Size(0, 13);
			labelDateShipmentDelivered.TabIndex = 214;
			labelDateShipmentDelivered.Visible = false;
			labelDateShipmentRemoval.AutoSize = true;
			labelDateShipmentRemoval.Location = new System.Drawing.Point(297, 34);
			labelDateShipmentRemoval.Name = "labelDateShipmentRemoval";
			labelDateShipmentRemoval.Size = new System.Drawing.Size(0, 13);
			labelDateShipmentRemoval.TabIndex = 213;
			labelDateShipmentRemoval.Visible = false;
			labelDateCustomsChecking.AutoSize = true;
			labelDateCustomsChecking.Location = new System.Drawing.Point(229, 34);
			labelDateCustomsChecking.Name = "labelDateCustomsChecking";
			labelDateCustomsChecking.Size = new System.Drawing.Size(0, 13);
			labelDateCustomsChecking.TabIndex = 212;
			labelDateCustomsChecking.Visible = false;
			labelDateDORelease.AutoSize = true;
			labelDateDORelease.Location = new System.Drawing.Point(174, 34);
			labelDateDORelease.Name = "labelDateDORelease";
			labelDateDORelease.Size = new System.Drawing.Size(0, 13);
			labelDateDORelease.TabIndex = 211;
			labelDateDORelease.Visible = false;
			labelDateArrived.AutoSize = true;
			labelDateArrived.Location = new System.Drawing.Point(122, 34);
			labelDateArrived.Name = "labelDateArrived";
			labelDateArrived.Size = new System.Drawing.Size(0, 13);
			labelDateArrived.TabIndex = 210;
			labelDateArrived.Visible = false;
			labelDateOriginalDocumentReceived.AutoSize = true;
			labelDateOriginalDocumentReceived.Location = new System.Drawing.Point(65, 34);
			labelDateOriginalDocumentReceived.Name = "labelDateOriginalDocumentReceived";
			labelDateOriginalDocumentReceived.Size = new System.Drawing.Size(0, 13);
			labelDateOriginalDocumentReceived.TabIndex = 209;
			labelDateOriginalDocumentReceived.Visible = false;
			labelDateShipped.AutoSize = true;
			labelDateShipped.Location = new System.Drawing.Point(6, 34);
			labelDateShipped.Name = "labelDateShipped";
			labelDateShipped.Size = new System.Drawing.Size(0, 13);
			labelDateShipped.TabIndex = 208;
			labelDateShipped.Visible = false;
			labelStatusCancelled.AutoSize = true;
			labelStatusCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusCancelled.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusCancelled.Location = new System.Drawing.Point(614, 8);
			labelStatusCancelled.Name = "labelStatusCancelled";
			labelStatusCancelled.Size = new System.Drawing.Size(54, 13);
			labelStatusCancelled.TabIndex = 207;
			labelStatusCancelled.Text = "Cancelled";
			labelStatusReturned.AutoSize = true;
			labelStatusReturned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusReturned.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusReturned.Location = new System.Drawing.Point(553, 8);
			labelStatusReturned.Name = "labelStatusReturned";
			labelStatusReturned.Size = new System.Drawing.Size(51, 13);
			labelStatusReturned.TabIndex = 206;
			labelStatusReturned.Text = "Returned";
			labelStatusReturning.AutoSize = true;
			labelStatusReturning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusReturning.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusReturning.Location = new System.Drawing.Point(490, 8);
			labelStatusReturning.Name = "labelStatusReturning";
			labelStatusReturning.Size = new System.Drawing.Size(53, 13);
			labelStatusReturning.TabIndex = 204;
			labelStatusReturning.Text = "Returning";
			labelStatusOffLoaded.AutoSize = true;
			labelStatusOffLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusOffLoaded.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusOffLoaded.Location = new System.Drawing.Point(423, 8);
			labelStatusOffLoaded.Name = "labelStatusOffLoaded";
			labelStatusOffLoaded.Size = new System.Drawing.Size(57, 13);
			labelStatusOffLoaded.TabIndex = 203;
			labelStatusOffLoaded.Text = "OffLoaded";
			labelStatusShipmentDelivered.AutoSize = true;
			labelStatusShipmentDelivered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusShipmentDelivered.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusShipmentDelivered.Location = new System.Drawing.Point(361, 8);
			labelStatusShipmentDelivered.Name = "labelStatusShipmentDelivered";
			labelStatusShipmentDelivered.Size = new System.Drawing.Size(52, 13);
			labelStatusShipmentDelivered.TabIndex = 202;
			labelStatusShipmentDelivered.Text = "Delivered";
			labelStatusShipmentRemoval.AutoSize = true;
			labelStatusShipmentRemoval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusShipmentRemoval.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusShipmentRemoval.Location = new System.Drawing.Point(283, 8);
			labelStatusShipmentRemoval.Name = "labelStatusShipmentRemoval";
			labelStatusShipmentRemoval.Size = new System.Drawing.Size(68, 13);
			labelStatusShipmentRemoval.TabIndex = 201;
			labelStatusShipmentRemoval.Text = "ShpRemoval";
			labelStatusCustomsChecking.AutoSize = true;
			labelStatusCustomsChecking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusCustomsChecking.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusCustomsChecking.Location = new System.Drawing.Point(226, 8);
			labelStatusCustomsChecking.Name = "labelStatusCustomsChecking";
			labelStatusCustomsChecking.Size = new System.Drawing.Size(47, 13);
			labelStatusCustomsChecking.TabIndex = 200;
			labelStatusCustomsChecking.Text = "Customs";
			labelStatusDORelease.AutoSize = true;
			labelStatusDORelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusDORelease.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusDORelease.Location = new System.Drawing.Point(172, 8);
			labelStatusDORelease.Name = "labelStatusDORelease";
			labelStatusDORelease.Size = new System.Drawing.Size(44, 13);
			labelStatusDORelease.TabIndex = 199;
			labelStatusDORelease.Text = "DORlse";
			labelStatusArrived.AutoSize = true;
			labelStatusArrived.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusArrived.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusArrived.Location = new System.Drawing.Point(122, 8);
			labelStatusArrived.Name = "labelStatusArrived";
			labelStatusArrived.Size = new System.Drawing.Size(40, 13);
			labelStatusArrived.TabIndex = 198;
			labelStatusArrived.Text = "Arrived";
			labelStatusOriginalDocumentReceived.AutoSize = true;
			labelStatusOriginalDocumentReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusOriginalDocumentReceived.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusOriginalDocumentReceived.Location = new System.Drawing.Point(59, 8);
			labelStatusOriginalDocumentReceived.Name = "labelStatusOriginalDocumentReceived";
			labelStatusOriginalDocumentReceived.Size = new System.Drawing.Size(53, 13);
			labelStatusOriginalDocumentReceived.TabIndex = 197;
			labelStatusOriginalDocumentReceived.Text = "DocRcvd";
			labelStatusShipped.AutoSize = true;
			labelStatusShipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelStatusShipped.ForeColor = System.Drawing.Color.Firebrick;
			labelStatusShipped.Location = new System.Drawing.Point(3, 8);
			labelStatusShipped.Name = "labelStatusShipped";
			labelStatusShipped.Size = new System.Drawing.Size(46, 13);
			labelStatusShipped.TabIndex = 196;
			labelStatusShipped.Text = "Shipped";
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
			comboBoxContainerStatus.Location = new System.Drawing.Point(483, 573);
			comboBoxContainerStatus.Name = "comboBoxContainerStatus";
			comboBoxContainerStatus.SelectedID = 0;
			comboBoxContainerStatus.Size = new System.Drawing.Size(69, 21);
			comboBoxContainerStatus.TabIndex = 182;
			comboBoxContainerStatus.Visible = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(473, 557);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(47, 13);
			mmLabel6.TabIndex = 183;
			mmLabel6.Text = "Status:";
			mmLabel6.Visible = false;
			buttonNext.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNext.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonNext.BackColor = System.Drawing.Color.DarkGray;
			buttonNext.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNext.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNext.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNext.Location = new System.Drawing.Point(598, 594);
			buttonNext.Name = "buttonNext";
			buttonNext.Size = new System.Drawing.Size(96, 24);
			buttonNext.TabIndex = 187;
			buttonNext.Text = "&Next";
			buttonNext.UseVisualStyleBackColor = false;
			buttonNext.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(707, 698);
			base.Controls.Add(panelStatusDate);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(panel1);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxContainerStatus);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(buttonNext);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ContainerTrackingWizardForm";
			Text = "Container Tracking";
			base.Load += new System.EventHandler(JournalLeavesForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)groupBoxStatusChange).EndInit();
			groupBoxStatusChange.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)groupBoxDelivery).EndInit();
			groupBoxDelivery.ResumeLayout(false);
			groupBoxDelivery.PerformLayout();
			panelStatus.ResumeLayout(false);
			panelStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).EndInit();
			((System.ComponentModel.ISupportInitialize)groupBoxDocuments).EndInit();
			groupBoxDocuments.ResumeLayout(false);
			groupBoxDocuments.PerformLayout();
			panelStatusDate.ResumeLayout(false);
			panelStatusDate.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
