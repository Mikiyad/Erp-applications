using DevExpress.XtraEditors.Controls;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerInsuranceClaimForm : Form, IForm
	{
		private CustomerInsuranceClaimData currentData;

		private const string TABLENAME_CONST = "Customer_Insurance_Claim";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool EnableLegalAnalysis = CompanyPreferences.EnableLegalAnalysisCode;

		private DataSet companyInformation;

		private string customerID;

		private ScreenAccessRight screenRight;

		private DataSet followUpData;

		private string parentSysDocID = "";

		private string parentVoucherID = "";

		private IContainer components;

		private ToolStrip toolStrip1;

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

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonComments;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem createFromExistingActivityToolStripMenuItem;

		private UltraTabPageControl ultraTabPageControl2;

		private MMLabel mmLabel25;

		private Button button1;

		private GadgetDateRangeComboBox comboBoxFollowupPeriod;

		private Button buttonAddActivity;

		private DataGridList dataGridListFollowup;

		private UltraTabPageControl ultraTabPageControl1;

		private customersFlatComboBox customersFlatComboBox1;

		private MMLabel mmLabel7;

		private GenericListComboBox comboBoxStatus;

		private MMLabel mmLabel5;

		private AmountTextBox textboxPaidAmount;

		private MMLabel mmLabel3;

		private MMTextBox mmTextboxReason;

		private MMLabel mmLabel2;

		private AmountTextBox textboxClaimAmount;

		private UltraGroupBox ultraGroupBox5;

		private InsuranceProviderComboBox comboBoxInsuranceProvider;

		private MMTextBox textBoxProvider;

		private MMLabel mmLabel53;

		private MMSDateTimePicker dateTimePickerValidTo;

		private MMLabel mmLabel55;

		private MMSDateTimePicker datetimePickerEffectiveDate;

		private MMLabel mmLabel54;

		private MMTextBox textBoxInsuranceID;

		private MMLabel mmLabel39;

		private MMTextBox textBoxInsuranceRemarks;

		private MMLabel mmLabel38;

		private MMLabel mmLabel37;

		private MMTextBox textBoxInsuranceNumber;

		private AmountTextBox textBoxInsuranceApprovedAmount;

		private MMLabel mmLabel36;

		private AmountTextBox textBoxInsurancePayableAmount;

		private MMSDateTimePicker dateTimePickerInsuranceDate;

		private MMLabel mmLabel34;

		private MMTextBox textBoxRemarks;

		private MMLabel mmLabel6;

		private UltraFormattedLinkLabel labelCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private SysDocComboBox comboBoxSysDoc;

		private DateTimePicker dateTimePickerDate;

		private MMLabel labelCloseDate;

		private FormManager formManager;

		private MMTextBox textBoxVoucherNumber;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabControl ultraTabControl1;

		private MMTextBox textboxCustomerName;

		private MMLabel mmLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelInsuranceProvider;

		private MMSDateTimePicker dateTimePickerPaidDate;

		public ScreenAreas ScreenArea => ScreenAreas.Legal;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public string CustomerID
		{
			get
			{
				return customerID;
			}
			set
			{
				customerID = value;
			}
		}

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
					buttonDelete.Enabled = false;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxVoucherNumber.ReadOnly = true;
				}
				comboBoxSysDoc.Enabled = value;
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

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		public string ParentSysDocID
		{
			get
			{
				return parentSysDocID;
			}
			set
			{
				parentSysDocID = value;
			}
		}

		public string ParentVoucherID
		{
			get
			{
				return parentVoucherID;
			}
			set
			{
				parentVoucherID = value;
			}
		}

		public CustomerInsuranceClaimForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += LegalActivityDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridListFollowup.DoubleClick += dataGridListFollowup_DoubleClick;
		}

		private void dataGridListFollowup_DoubleClick(object sender, EventArgs e)
		{
			int num = checked(dataGridListFollowup.Rows.Count - 1);
			num = dataGridListFollowup.ActiveRow.Index;
			string followupId = dataGridListFollowup.Rows[num].Cells["FollowupID"].Value.ToString();
			dataGridListFollowup.Rows[num].Cells["SourceVoucherID"].Value.ToString();
			dataGridListFollowup.Rows[num].Cells["SourceSysDocID"].Value.ToString();
			FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
			FormActivator.FollowupDetailsFormObj.SourceSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.FollowupDetailsFormObj.SourceVoucherID = textBoxVoucherNumber.Text;
			FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.InsuranceClaim;
			FormActivator.FollowupDetailsFormObj.FollowupId = followupId;
			FormActivator.FollowupDetailsFormObj.EditDocument(followupId);
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomerInsuranceClaimData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerInsuranceClaimTable.Rows[0] : currentData.CustomerInsuranceClaimTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["CustomerID"] = customersFlatComboBox1.SelectedID;
				dataRow["InsApprovedAmount"] = textBoxInsuranceApprovedAmount.Text;
				dataRow["InsPolicyNumber"] = textBoxInsuranceNumber.Text;
				dataRow["InsRemarks"] = textBoxInsuranceRemarks.Text.Trim();
				dataRow["InsPayableAmount"] = textBoxInsurancePayableAmount.Text;
				dataRow["InsuranceID"] = textBoxInsuranceID.Text;
				dataRow["InsProviderID"] = comboBoxInsuranceProvider.SelectedID;
				dataRow["InsApprovedAmount"] = textBoxInsuranceApprovedAmount.Text;
				if (datetimePickerEffectiveDate.Checked)
				{
					dataRow["InsEffectiveDate"] = datetimePickerEffectiveDate.Value;
				}
				else
				{
					dataRow["InsEffectiveDate"] = DBNull.Value;
				}
				if (dateTimePickerValidTo.Checked)
				{
					dataRow["InsExpiryDate"] = dateTimePickerValidTo.Value;
				}
				else
				{
					dataRow["InsExpiryDate"] = DBNull.Value;
				}
				if (dateTimePickerInsuranceDate.Checked)
				{
					dataRow["InsApplicationDate"] = dateTimePickerInsuranceDate.Value;
				}
				else
				{
					dataRow["InsApplicationDate"] = DBNull.Value;
				}
				if (dateTimePickerPaidDate.Checked)
				{
					dataRow["PaidDate"] = dateTimePickerPaidDate.Value;
				}
				else
				{
					dataRow["PaidDate"] = DBNull.Value;
				}
				dataRow["ClaimAmount"] = textboxClaimAmount.Text;
				dataRow["PaidAmount"] = textboxPaidAmount.Text;
				dataRow["CustomerInsStatus"] = comboBoxStatus.SelectedID;
				dataRow["Reason"] = mmTextboxReason.Text;
				dataRow["CustomerInsRemarks"] = textBoxRemarks.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerInsuranceClaimTable.Rows.Add(dataRow);
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

		public void AddNewActivity(CRMRelatedTypes type, string relatedID, string parentSysDocID, string parentVoucherID)
		{
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CustomerInsuranceClaimSystem.GetCustomerInsuranceClaimByID(SystemDocID, id);
					IsNewRecord = false;
					FillData();
					LoadFollowUp();
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
			textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
			customersFlatComboBox1.SelectedID = dataRow["CustomerID"].ToString();
			if (dataRow["TransactionDate"] != DBNull.Value)
			{
				dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
			}
			if (dataRow["InsApprovedAmount"] != DBNull.Value)
			{
				textBoxInsuranceApprovedAmount.Text = decimal.Parse(dataRow["InsApprovedAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsuranceApprovedAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			if (dataRow["InsPayableAmount"] != DBNull.Value)
			{
				textBoxInsurancePayableAmount.Text = decimal.Parse(dataRow["InsPayableAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsurancePayableAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			if (dataRow["InsApplicationDate"] != DBNull.Value)
			{
				dateTimePickerInsuranceDate.Value = DateTime.Parse(dataRow["InsApplicationDate"].ToString());
				dateTimePickerInsuranceDate.Checked = true;
			}
			else
			{
				dateTimePickerInsuranceDate.IsNull = true;
				dateTimePickerInsuranceDate.Checked = false;
			}
			textBoxInsuranceNumber.Text = dataRow["InsPolicyNumber"].ToString();
			textBoxInsuranceRemarks.Text = dataRow["InsRemarks"].ToString();
			textBoxInsuranceID.Text = dataRow["InsuranceID"].ToString();
			if (dataRow["InsProviderID"] != DBNull.Value)
			{
				comboBoxInsuranceProvider.SelectedID = dataRow["InsProviderID"].ToString().TrimStart();
			}
			DateTime result = new DateTime(1753, 1, 1);
			DateTime result2 = new DateTime(1753, 1, 1);
			if (dataRow["InsEffectiveDate"] != DBNull.Value)
			{
				DateTime.TryParse(dataRow["InsEffectiveDate"].ToString(), out result);
				if ((SqlBoolean)true && result > SqlDateTime.MinValue)
				{
					datetimePickerEffectiveDate.Value = DateTime.Parse(dataRow["InsEffectiveDate"].ToString());
					datetimePickerEffectiveDate.Checked = true;
				}
				else
				{
					datetimePickerEffectiveDate.IsNull = true;
					datetimePickerEffectiveDate.Checked = false;
				}
			}
			else
			{
				datetimePickerEffectiveDate.IsNull = true;
				datetimePickerEffectiveDate.Checked = false;
			}
			if (dataRow["InsExpiryDate"] != DBNull.Value)
			{
				DateTime.TryParse(dataRow["InsExpiryDate"].ToString(), out result2);
				if ((SqlBoolean)true && result2 > SqlDateTime.MinValue)
				{
					dateTimePickerValidTo.Value = DateTime.Parse(dataRow["InsExpiryDate"].ToString());
					dateTimePickerValidTo.Checked = true;
				}
				else
				{
					dateTimePickerValidTo.IsNull = true;
					dateTimePickerValidTo.Checked = false;
				}
			}
			else
			{
				dateTimePickerValidTo.IsNull = true;
				dateTimePickerValidTo.Checked = false;
			}
			if (dataRow["ClaimAmount"] != DBNull.Value)
			{
				textboxClaimAmount.Text = decimal.Parse(dataRow["ClaimAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textboxClaimAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			if (dataRow["PaidAmount"] != DBNull.Value)
			{
				textboxPaidAmount.Text = decimal.Parse(dataRow["PaidAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textboxPaidAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			comboBoxStatus.SelectedID = dataRow["CustomerInsStatus"].ToString();
			mmTextboxReason.Text = dataRow["Reason"].ToString();
			if (dataRow["PaidDate"] != DBNull.Value)
			{
				dateTimePickerPaidDate.Value = DateTime.Parse(dataRow["PaidDate"].ToString());
				dateTimePickerPaidDate.Checked = true;
			}
			else
			{
				dateTimePickerPaidDate.IsNull = true;
				dateTimePickerPaidDate.Checked = false;
			}
			textBoxRemarks.Text = dataRow["CustomerInsRemarks"].ToString();
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
				bool flag = (!isNewRecord) ? Factory.CustomerInsuranceClaimSystem.CreateCustomerInsuranceClaim(currentData, !isNewRecord) : Factory.CustomerInsuranceClaimSystem.CreateCustomerInsuranceClaim(currentData, !isNewRecord);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Customer_Insurance_Claim", "VoucherID", textBoxVoucherNumber.Text.Trim(), "SysDocID", SystemDocID))
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
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			dateTimePickerInsuranceDate.Value = DateTime.Now;
			textBoxInsuranceApprovedAmount.Clear();
			customersFlatComboBox1.Enabled = true;
			customersFlatComboBox1.ReadOnly = false;
			customersFlatComboBox1.Clear();
			textBoxInsuranceNumber.Clear();
			textBoxInsuranceRemarks.Clear();
			textBoxInsurancePayableAmount.Clear();
			textBoxInsuranceID.Clear();
			comboBoxInsuranceProvider.Clear();
			datetimePickerEffectiveDate.Value = DateTime.Now;
			datetimePickerEffectiveDate.Checked = false;
			textboxClaimAmount.Clear();
			dateTimePickerValidTo.Clear();
			dateTimePickerValidTo.Checked = false;
			dateTimePickerInsuranceDate.Clear();
			dateTimePickerInsuranceDate.Checked = false;
			textboxClaimAmount.Clear();
			textboxPaidAmount.Clear();
			mmTextboxReason.Clear();
			textBoxRemarks.Clear();
			comboBoxStatus.Clear();
			dateTimePickerPaidDate.Clear();
			dateTimePickerPaidDate.Checked = false;
			textboxCustomerName.Clear();
			formManager.ResetDirty();
			textBoxVoucherNumber.Focus();
			customersFlatComboBox1.Focus();
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
				if (ErrorHelper.QuestionMessageYesNo("Are you sure! you want to delete the record?") == DialogResult.No)
				{
					return false;
				}
				bool num = Factory.CustomerInsuranceClaimSystem.DeleteCustomerInsuranceClaim(textBoxVoucherNumber.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Activity, needRefresh: true);
				}
				return num;
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
			LoadData(DatabaseHelper.GetNextID("Customer_Insurance_Claim", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Customer_Insurance_Claim", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Customer_Insurance_Claim", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Customer_Insurance_Claim", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer_Insurance_Claim", "VoucherID", toolStripTextBoxFind.Text.Trim(), "SysDocID", SystemDocID))
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

		private void LegalActivityDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				dataGridListFollowup.ApplyUIDesign();
				SetupFollowGrid();
				SetupGrid();
				comboBoxFollowupPeriod.LoadData();
				comboBoxFollowupPeriod.SelectedIndex = 13;
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					comboBoxSysDoc.FilterByType(SysDocTypes.CustomerInsuranceClaim);
					ClearForm();
					comboBoxSysDoc.Focus();
				}
			}
			catch (Exception e2)
			{
				dataGridListFollowup.LoadLayoutFailed = true;
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerInsuranceClaimListFormObj);
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.LegalActivity);
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				comboBoxFollowupPeriod.SelectedIndex = 13;
				LoadFollowUp();
				DateTime dateTime = DateTime.Now;
				string text = "";
				int result = 0;
				if (dataGridListFollowup.Rows.Count > 0)
				{
					int index = checked(dataGridListFollowup.Rows.Count - 1);
					dateTime = DateTime.Parse(dataGridListFollowup.Rows[index].Cells["NextFollowupDate"].Value.ToString());
					dataGridListFollowup.Rows[index].Cells["NextFollow_upBy"].Value.ToString().TrimEnd();
					text = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
					int.TryParse(dataGridListFollowup.Rows[index].Cells["StatusID"].Value.ToString(), out result);
				}
				FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
				FormActivator.FollowupDetailsFormObj.SourceSysDocID = comboBoxSysDoc.SelectedID;
				FormActivator.FollowupDetailsFormObj.SourceVoucherID = textBoxVoucherNumber.Text;
				FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.InsuranceClaim;
				FormActivator.FollowupDetailsFormObj.ThisfollowupBy = text.TrimEnd();
				FormActivator.FollowupDetailsFormObj.ThisfollowupDate = dateTime;
				FormActivator.FollowupDetailsFormObj.ThisfollowupTime = dateTime;
				FormActivator.FollowupDetailsFormObj.Status = result;
				FormActivator.FollowupDetailsFormObj.AddFollowUp();
				FormActivator.FollowupDetailsFormObj.LoadFollowUp();
			}
		}

		private void SetupFollowGrid()
		{
			try
			{
				dataGridListFollowup.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("FollowupID");
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("FollowupDate", typeof(DateTime));
				dataTable.Columns.Add("Follow_upBy");
				dataTable.Columns.Add("NextFollowupDate", typeof(DateTime));
				dataTable.Columns.Add("NextFollow_upBy");
				dataTable.Columns.Add("NextFollowupByID");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("StatusID");
				dataGridListFollowup.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridListFollowup.DisplayLayout.Bands[0].Columns["StatusID"];
				UltraGridColumn ultraGridColumn2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollowupByID"];
				UltraGridColumn ultraGridColumn3 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["FollowupID"];
				UltraGridColumn ultraGridColumn4 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
				bool flag2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadFollowUp()
		{
			try
			{
				if (!isNewRecord)
				{
					followUpData = Factory.FollowupSystem.GetFollowupListByActivityID(CRMRelatedTypes.InsuranceClaim, textBoxVoucherNumber.Text, textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, comboBoxFollowupPeriod.FromDate, comboBoxFollowupPeriod.ToDate);
					DataTable dataTable = dataGridListFollowup.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in followUpData.Tables[0].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["FollowupID"] = row["FollowupID"];
						dataRow2["SourceSysDocID"] = row["SourceSysDocID"];
						dataRow2["SourceVoucherID"] = row["SourceVoucherID"];
						dataRow2["FollowupDate"] = row["ThisFollowupDate"];
						dataRow2["Follow_upBy"] = row["FollowupBy"];
						dataRow2["NextFollowupDate"] = row["NextFollowupDate"];
						dataRow2["NextFollow_upBy"] = row["NextFollowupBy"];
						dataRow2["Status"] = row["Status"];
						dataRow2["StatusID"] = row["ThisFollowupStatusID"];
						dataRow2["NextFollowupByID"] = row["NextFollowupByID"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxFollowupPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadFollowUp();
		}

		private void ultraFormattedLinkLabel5_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.CustomerInsuranceClaim);
		}

		private void ultraFormattedLinkLabel3_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraTabPageControl1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void formManager_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonComments_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					EntityCommentsForm entityCommentsForm = new EntityCommentsForm();
					entityCommentsForm.EntityID = comboBoxSysDoc.SelectedID;
					entityCommentsForm.EntityName = textBoxVoucherNumber.Text;
					entityCommentsForm.EntityType = EntityTypesEnum.LegalActivitys;
					entityCommentsForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel6_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(customersFlatComboBox1.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked_2(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void SetupGrid()
		{
			try
			{
				new DataTable
				{
					Columns = 
					{
						"SysDocID",
						"VoucherID",
						{
							"Date",
							typeof(DateTime)
						},
						"Description",
						"Reference",
						{
							"Amount",
							typeof(decimal)
						}
					}
				};
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				DataSet customerInsuranceClaimToPrint = Factory.CustomerInsuranceClaimSystem.GetCustomerInsuranceClaimToPrint(selectedID, text);
				if (customerInsuranceClaimToPrint == null || customerInsuranceClaimToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(customerInsuranceClaimToPrint, selectedID, "Customer Insurance Claim", SysDocTypes.CustomerInsuranceClaim, isPrint, showPrintDialog);
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

		private void createFromExistingActivityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet legalActionList = Factory.LegalActionSystem.GetLegalActionList(DateTime.MinValue, DateTime.MaxValue);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = legalActionList;
			selectDocumentDialog.Text = "Select Legal Action";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			}
		}

		private void ultraFormattedLinkLabel7_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraTabControl1_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void checkBoxChangeStatus_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void customersFlatComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			CustomerID = customersFlatComboBox1.SelectedID;
			if (CustomerID != null && CustomerID != "")
			{
				LoadCustomerData(CustomerID);
			}
		}

		public void LoadCustomerData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == ""))
				{
					CustomerData customerByID = Factory.CustomerSystem.GetCustomerByID(id);
					if (customerByID != null || customerByID.Tables.Count != 0 || customerByID.Tables[0].Rows.Count != 0)
					{
						DataRow dataRow = customerByID.Tables[0].Rows[0];
						if (dataRow["InsApplicationDate"] != DBNull.Value)
						{
							dateTimePickerInsuranceDate.Value = DateTime.Parse(dataRow["InsApplicationDate"].ToString());
							dateTimePickerInsuranceDate.Checked = true;
						}
						textboxCustomerName.Text = customersFlatComboBox1.SelectedName;
						DateTime result = new DateTime(1, 1, 1);
						DateTime result2 = new DateTime(1, 1, 1);
						DateTime.TryParse(dataRow["InsEffectiveDate"].ToString(), out result);
						DateTime.TryParse(dataRow["InsExpiryDate"].ToString(), out result2);
						if (dataRow["InsEffectiveDate"] != DBNull.Value)
						{
							DateTime.TryParse(dataRow["InsEffectiveDate"].ToString(), out result);
							if ((SqlBoolean)true && result > SqlDateTime.MinValue)
							{
								datetimePickerEffectiveDate.Value = DateTime.Parse(dataRow["InsEffectiveDate"].ToString());
								datetimePickerEffectiveDate.Checked = true;
							}
							else
							{
								datetimePickerEffectiveDate.IsNull = true;
								datetimePickerEffectiveDate.Checked = false;
							}
						}
						else
						{
							datetimePickerEffectiveDate.IsNull = true;
							datetimePickerEffectiveDate.Checked = false;
						}
						if (dataRow["InsExpiryDate"] != DBNull.Value)
						{
							DateTime.TryParse(dataRow["InsExpiryDate"].ToString(), out result2);
							if ((SqlBoolean)true && result2 > SqlDateTime.MinValue)
							{
								dateTimePickerValidTo.Value = DateTime.Parse(dataRow["InsExpiryDate"].ToString());
								dateTimePickerValidTo.Checked = true;
							}
							else
							{
								dateTimePickerValidTo.IsNull = true;
								dateTimePickerValidTo.Checked = false;
							}
						}
						else
						{
							dateTimePickerValidTo.IsNull = true;
							dateTimePickerValidTo.Checked = false;
						}
						if (dataRow["InsPolicyNumber"] != null)
						{
							textBoxInsuranceNumber.Text = dataRow["InsPolicyNumber"].ToString();
						}
						textBoxInsuranceID.Text = dataRow["InsuranceID"].ToString();
						if (dataRow["InsApprovedAmount"] != null && dataRow["InsApprovedAmount"].ToString() != "")
						{
							textBoxInsuranceApprovedAmount.Text = dataRow["InsApprovedAmount"].ToString();
						}
						if (dataRow["InsRemarks"] != null)
						{
							textBoxInsuranceRemarks.Text = dataRow["InsRemarks"].ToString();
						}
						if (dataRow["InsProviderID"] != null)
						{
							comboBoxInsuranceProvider.SelectedID = dataRow["InsProviderID"].ToString().TrimStart();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void comboBoxInsuranceRating_ValueChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void ultraFormattedLinkLabelInsuranceProvider_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditInsuranceProvider(comboBoxInsuranceProvider.SelectedID);
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet legalActionList = Factory.LegalActionSystem.GetLegalActionList(DateTime.MinValue, DateTime.MaxValue);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = legalActionList;
			selectDocumentDialog.Text = "Select Legal Action";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerInsuranceClaimForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textboxCustomerName = new Micromind.UISupport.MMTextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			customersFlatComboBox1 = new Micromind.DataControls.customersFlatComboBox();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			dateTimePickerPaidDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			textboxClaimAmount = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabelInsuranceProvider = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			dateTimePickerValidTo = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxInsuranceProvider = new Micromind.DataControls.InsuranceProviderComboBox();
			textBoxProvider = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel55 = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			mmTextboxReason = new Micromind.UISupport.MMTextBox();
			datetimePickerEffectiveDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxStatus = new Micromind.DataControls.GenericListComboBox();
			mmLabel54 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceID = new Micromind.UISupport.MMTextBox();
			textboxPaidAmount = new Micromind.UISupport.AmountTextBox();
			textBoxInsurancePayableAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceRemarks = new Micromind.UISupport.MMTextBox();
			dateTimePickerInsuranceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel38 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceApprovedAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
			textBoxVoucherNumber = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			labelCustomer = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			button1 = new System.Windows.Forms.Button();
			comboBoxFollowupPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			buttonAddActivity = new System.Windows.Forms.Button();
			dataGridListFollowup = new Micromind.UISupport.DataGridList(components);
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonComments = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			createFromExistingActivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)customersFlatComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStatus).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(textboxCustomerName);
			ultraTabPageControl1.Controls.Add(comboBoxSysDoc);
			ultraTabPageControl1.Controls.Add(customersFlatComboBox1);
			ultraTabPageControl1.Controls.Add(ultraGroupBox5);
			ultraTabPageControl1.Controls.Add(textBoxVoucherNumber);
			ultraTabPageControl1.Controls.Add(formManager);
			ultraTabPageControl1.Controls.Add(labelCloseDate);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel5);
			ultraTabPageControl1.Controls.Add(dateTimePickerDate);
			ultraTabPageControl1.Controls.Add(labelCustomer);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel2);
			ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(777, 419);
			ultraTabPageControl1.Paint += new System.Windows.Forms.PaintEventHandler(ultraTabPageControl1_Paint);
			textboxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textboxCustomerName.CustomReportFieldName = "";
			textboxCustomerName.CustomReportKey = "";
			textboxCustomerName.CustomReportValueType = 1;
			textboxCustomerName.IsComboTextBox = false;
			textboxCustomerName.IsModified = false;
			textboxCustomerName.Location = new System.Drawing.Point(284, 31);
			textboxCustomerName.MaxLength = 64;
			textboxCustomerName.Name = "textboxCustomerName";
			textboxCustomerName.ReadOnly = true;
			textboxCustomerName.Size = new System.Drawing.Size(350, 20);
			textboxCustomerName.TabIndex = 8;
			textboxCustomerName.TabStop = false;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(122, 9);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(160, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			customersFlatComboBox1.Assigned = false;
			customersFlatComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			customersFlatComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			customersFlatComboBox1.CustomReportFieldName = "";
			customersFlatComboBox1.CustomReportKey = "";
			customersFlatComboBox1.CustomReportValueType = 1;
			customersFlatComboBox1.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			customersFlatComboBox1.DisplayLayout.Appearance = appearance13;
			customersFlatComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			customersFlatComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			customersFlatComboBox1.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			customersFlatComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			customersFlatComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			customersFlatComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			customersFlatComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			customersFlatComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			customersFlatComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			customersFlatComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			customersFlatComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			customersFlatComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			customersFlatComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			customersFlatComboBox1.DisplayLayout.Override.CellAppearance = appearance20;
			customersFlatComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			customersFlatComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			customersFlatComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			customersFlatComboBox1.DisplayLayout.Override.HeaderAppearance = appearance22;
			customersFlatComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			customersFlatComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			customersFlatComboBox1.DisplayLayout.Override.RowAppearance = appearance23;
			customersFlatComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			customersFlatComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			customersFlatComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			customersFlatComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			customersFlatComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			customersFlatComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			customersFlatComboBox1.Editable = true;
			customersFlatComboBox1.FilterString = "";
			customersFlatComboBox1.FilterSysDocID = "";
			customersFlatComboBox1.HasAll = false;
			customersFlatComboBox1.HasCustom = false;
			customersFlatComboBox1.IsDataLoaded = false;
			customersFlatComboBox1.Location = new System.Drawing.Point(122, 31);
			customersFlatComboBox1.MaxDropDownItems = 12;
			customersFlatComboBox1.Name = "customersFlatComboBox1";
			customersFlatComboBox1.ShowConsignmentOnly = false;
			customersFlatComboBox1.ShowInactive = false;
			customersFlatComboBox1.ShowLPOCustomersOnly = false;
			customersFlatComboBox1.ShowPROCustomersOnly = false;
			customersFlatComboBox1.ShowQuickAdd = true;
			customersFlatComboBox1.Size = new System.Drawing.Size(160, 20);
			customersFlatComboBox1.TabIndex = 3;
			customersFlatComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			customersFlatComboBox1.SelectedIndexChanged += new System.EventHandler(customersFlatComboBox1_SelectedIndexChanged);
			ultraGroupBox5.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox5.Controls.Add(dateTimePickerPaidDate);
			ultraGroupBox5.Controls.Add(mmLabel1);
			ultraGroupBox5.Controls.Add(mmLabel53);
			ultraGroupBox5.Controls.Add(textboxClaimAmount);
			ultraGroupBox5.Controls.Add(ultraFormattedLinkLabelInsuranceProvider);
			ultraGroupBox5.Controls.Add(mmLabel6);
			ultraGroupBox5.Controls.Add(dateTimePickerValidTo);
			ultraGroupBox5.Controls.Add(comboBoxInsuranceProvider);
			ultraGroupBox5.Controls.Add(mmLabel7);
			ultraGroupBox5.Controls.Add(mmLabel55);
			ultraGroupBox5.Controls.Add(textBoxRemarks);
			ultraGroupBox5.Controls.Add(textBoxProvider);
			ultraGroupBox5.Controls.Add(mmTextboxReason);
			ultraGroupBox5.Controls.Add(datetimePickerEffectiveDate);
			ultraGroupBox5.Controls.Add(comboBoxStatus);
			ultraGroupBox5.Controls.Add(mmLabel54);
			ultraGroupBox5.Controls.Add(mmLabel2);
			ultraGroupBox5.Controls.Add(mmLabel3);
			ultraGroupBox5.Controls.Add(mmLabel5);
			ultraGroupBox5.Controls.Add(textBoxInsuranceID);
			ultraGroupBox5.Controls.Add(textboxPaidAmount);
			ultraGroupBox5.Controls.Add(textBoxInsurancePayableAmount);
			ultraGroupBox5.Controls.Add(mmLabel39);
			ultraGroupBox5.Controls.Add(mmLabel36);
			ultraGroupBox5.Controls.Add(mmLabel34);
			ultraGroupBox5.Controls.Add(textBoxInsuranceRemarks);
			ultraGroupBox5.Controls.Add(dateTimePickerInsuranceDate);
			ultraGroupBox5.Controls.Add(mmLabel38);
			ultraGroupBox5.Controls.Add(textBoxInsuranceApprovedAmount);
			ultraGroupBox5.Controls.Add(mmLabel37);
			ultraGroupBox5.Controls.Add(textBoxInsuranceNumber);
			ultraGroupBox5.Location = new System.Drawing.Point(3, 55);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(704, 361);
			ultraGroupBox5.TabIndex = 3;
			ultraGroupBox5.Text = "Credit Insurance Info";
			dateTimePickerPaidDate.Checked = false;
			dateTimePickerPaidDate.CustomFormat = " ";
			dateTimePickerPaidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerPaidDate.Location = new System.Drawing.Point(535, 237);
			dateTimePickerPaidDate.Name = "dateTimePickerPaidDate";
			dateTimePickerPaidDate.ShowCheckBox = true;
			dateTimePickerPaidDate.Size = new System.Drawing.Size(137, 20);
			dateTimePickerPaidDate.TabIndex = 14;
			dateTimePickerPaidDate.Value = new System.DateTime(0L);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(279, 241);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(70, 13);
			mmLabel1.TabIndex = 197;
			mmLabel1.Text = "Claim Status:";
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(8, 89);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(48, 13);
			mmLabel53.TabIndex = 82;
			mmLabel53.Text = "Valid To:";
			textboxClaimAmount.AllowDecimal = true;
			textboxClaimAmount.BackColor = System.Drawing.Color.White;
			textboxClaimAmount.CustomReportFieldName = "";
			textboxClaimAmount.CustomReportKey = "";
			textboxClaimAmount.CustomReportValueType = 1;
			textboxClaimAmount.IsComboTextBox = false;
			textboxClaimAmount.IsModified = false;
			textboxClaimAmount.Location = new System.Drawing.Point(118, 165);
			textboxClaimAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textboxClaimAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textboxClaimAmount.Name = "textboxClaimAmount";
			textboxClaimAmount.NullText = "0";
			textboxClaimAmount.Size = new System.Drawing.Size(140, 20);
			textboxClaimAmount.TabIndex = 9;
			textboxClaimAmount.Text = "0.00";
			textboxClaimAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textboxClaimAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelInsuranceProvider.Appearance = appearance25;
			ultraFormattedLinkLabelInsuranceProvider.AutoSize = true;
			ultraFormattedLinkLabelInsuranceProvider.Location = new System.Drawing.Point(7, 21);
			ultraFormattedLinkLabelInsuranceProvider.Name = "ultraFormattedLinkLabelInsuranceProvider";
			ultraFormattedLinkLabelInsuranceProvider.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabelInsuranceProvider.TabIndex = 166;
			ultraFormattedLinkLabelInsuranceProvider.TabStop = true;
			ultraFormattedLinkLabelInsuranceProvider.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelInsuranceProvider.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelInsuranceProvider.Value = "Provider:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelInsuranceProvider.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabelInsuranceProvider.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelInsuranceProvider_LinkClicked);
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(8, 263);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(80, 13);
			mmLabel6.TabIndex = 177;
			mmLabel6.Text = "Claim Remarks:";
			dateTimePickerValidTo.Checked = false;
			dateTimePickerValidTo.CustomFormat = " ";
			dateTimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerValidTo.Location = new System.Drawing.Point(118, 84);
			dateTimePickerValidTo.Name = "dateTimePickerValidTo";
			dateTimePickerValidTo.ShowCheckBox = true;
			dateTimePickerValidTo.Size = new System.Drawing.Size(159, 20);
			dateTimePickerValidTo.TabIndex = 4;
			dateTimePickerValidTo.Value = new System.DateTime(0L);
			comboBoxInsuranceProvider.Assigned = false;
			comboBoxInsuranceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInsuranceProvider.CustomReportFieldName = "";
			comboBoxInsuranceProvider.CustomReportKey = "";
			comboBoxInsuranceProvider.CustomReportValueType = 1;
			comboBoxInsuranceProvider.DescriptionTextBox = textBoxProvider;
			comboBoxInsuranceProvider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInsuranceProvider.Editable = true;
			comboBoxInsuranceProvider.FilterString = "";
			comboBoxInsuranceProvider.HasAllAccount = false;
			comboBoxInsuranceProvider.HasCustom = false;
			comboBoxInsuranceProvider.IsDataLoaded = false;
			comboBoxInsuranceProvider.Location = new System.Drawing.Point(118, 18);
			comboBoxInsuranceProvider.MaxDropDownItems = 12;
			comboBoxInsuranceProvider.Name = "comboBoxInsuranceProvider";
			comboBoxInsuranceProvider.ShowInactiveItems = false;
			comboBoxInsuranceProvider.ShowQuickAdd = true;
			comboBoxInsuranceProvider.Size = new System.Drawing.Size(159, 20);
			comboBoxInsuranceProvider.TabIndex = 1;
			comboBoxInsuranceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProvider.CustomReportFieldName = "";
			textBoxProvider.CustomReportKey = "";
			textBoxProvider.CustomReportValueType = 1;
			textBoxProvider.IsComboTextBox = false;
			textBoxProvider.IsModified = false;
			textBoxProvider.Location = new System.Drawing.Point(279, 18);
			textBoxProvider.MaxLength = 64;
			textBoxProvider.Name = "textBoxProvider";
			textBoxProvider.ReadOnly = true;
			textBoxProvider.Size = new System.Drawing.Size(352, 20);
			textBoxProvider.TabIndex = 2;
			textBoxProvider.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(470, 241);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(57, 13);
			mmLabel7.TabIndex = 196;
			mmLabel7.Text = "Paid Date:";
			mmLabel55.AutoSize = true;
			mmLabel55.BackColor = System.Drawing.Color.Transparent;
			mmLabel55.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel55.IsFieldHeader = false;
			mmLabel55.IsRequired = false;
			mmLabel55.Location = new System.Drawing.Point(8, 67);
			mmLabel55.Name = "mmLabel55";
			mmLabel55.PenWidth = 1f;
			mmLabel55.ShowBorder = false;
			mmLabel55.Size = new System.Drawing.Size(80, 13);
			mmLabel55.TabIndex = 81;
			mmLabel55.Text = "Effective Date:";
			textBoxRemarks.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(118, 259);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(554, 96);
			textBoxRemarks.TabIndex = 15;
			mmTextboxReason.BackColor = System.Drawing.Color.White;
			mmTextboxReason.CustomReportFieldName = "";
			mmTextboxReason.CustomReportKey = "";
			mmTextboxReason.CustomReportValueType = 1;
			mmTextboxReason.IsComboTextBox = false;
			mmTextboxReason.IsModified = false;
			mmTextboxReason.Location = new System.Drawing.Point(118, 187);
			mmTextboxReason.MaxLength = 255;
			mmTextboxReason.Multiline = true;
			mmTextboxReason.Name = "mmTextboxReason";
			mmTextboxReason.Size = new System.Drawing.Size(554, 35);
			mmTextboxReason.TabIndex = 11;
			datetimePickerEffectiveDate.Checked = false;
			datetimePickerEffectiveDate.CustomFormat = " ";
			datetimePickerEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerEffectiveDate.Location = new System.Drawing.Point(118, 62);
			datetimePickerEffectiveDate.Name = "datetimePickerEffectiveDate";
			datetimePickerEffectiveDate.ShowCheckBox = true;
			datetimePickerEffectiveDate.Size = new System.Drawing.Size(159, 20);
			datetimePickerEffectiveDate.TabIndex = 3;
			datetimePickerEffectiveDate.Value = new System.DateTime(0L);
			comboBoxStatus.Assigned = false;
			comboBoxStatus.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStatus.CustomReportFieldName = "";
			comboBoxStatus.CustomReportKey = "";
			comboBoxStatus.CustomReportValueType = 1;
			comboBoxStatus.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStatus.DisplayLayout.Appearance = appearance27;
			comboBoxStatus.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStatus.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStatus.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStatus.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxStatus.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStatus.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxStatus.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStatus.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStatus.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStatus.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxStatus.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStatus.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStatus.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStatus.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxStatus.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStatus.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStatus.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxStatus.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxStatus.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStatus.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxStatus.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxStatus.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStatus.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxStatus.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStatus.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStatus.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStatus.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxStatus.Editable = true;
			comboBoxStatus.FilterString = "";
			comboBoxStatus.GenericListType = Micromind.Common.Data.GenericListTypes.ActionStatus;
			comboBoxStatus.HasAllAccount = false;
			comboBoxStatus.HasCustom = false;
			comboBoxStatus.IsDataLoaded = false;
			comboBoxStatus.IsSingleColumn = false;
			comboBoxStatus.Location = new System.Drawing.Point(357, 237);
			comboBoxStatus.MaxDropDownItems = 12;
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.ShowInactiveItems = false;
			comboBoxStatus.ShowQuickAdd = true;
			comboBoxStatus.Size = new System.Drawing.Size(109, 20);
			comboBoxStatus.TabIndex = 13;
			comboBoxStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel54.AutoSize = true;
			mmLabel54.BackColor = System.Drawing.Color.Transparent;
			mmLabel54.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel54.IsFieldHeader = false;
			mmLabel54.IsRequired = false;
			mmLabel54.Location = new System.Drawing.Point(278, 88);
			mmLabel54.Name = "mmLabel54";
			mmLabel54.PenWidth = 1f;
			mmLabel54.ShowBorder = false;
			mmLabel54.Size = new System.Drawing.Size(73, 13);
			mmLabel54.TabIndex = 74;
			mmLabel54.Text = "Insurance ID:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(9, 170);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(76, 13);
			mmLabel2.TabIndex = 188;
			mmLabel2.Text = "Claim Amount:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(9, 192);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(52, 13);
			mmLabel3.TabIndex = 190;
			mmLabel3.Text = "Reasons:";
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 241);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(71, 13);
			mmLabel5.TabIndex = 192;
			mmLabel5.Text = "Paid Amount:";
			textBoxInsuranceID.BackColor = System.Drawing.Color.White;
			textBoxInsuranceID.CustomReportFieldName = "";
			textBoxInsuranceID.CustomReportKey = "";
			textBoxInsuranceID.CustomReportValueType = 1;
			textBoxInsuranceID.IsComboTextBox = false;
			textBoxInsuranceID.IsModified = false;
			textBoxInsuranceID.Location = new System.Drawing.Point(388, 84);
			textBoxInsuranceID.MaxLength = 30;
			textBoxInsuranceID.Name = "textBoxInsuranceID";
			textBoxInsuranceID.Size = new System.Drawing.Size(139, 20);
			textBoxInsuranceID.TabIndex = 7;
			textboxPaidAmount.AllowDecimal = true;
			textboxPaidAmount.BackColor = System.Drawing.Color.White;
			textboxPaidAmount.CustomReportFieldName = "";
			textboxPaidAmount.CustomReportKey = "";
			textboxPaidAmount.CustomReportValueType = 1;
			textboxPaidAmount.IsComboTextBox = false;
			textboxPaidAmount.IsModified = false;
			textboxPaidAmount.Location = new System.Drawing.Point(118, 237);
			textboxPaidAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textboxPaidAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textboxPaidAmount.Name = "textboxPaidAmount";
			textboxPaidAmount.NullText = "0";
			textboxPaidAmount.Size = new System.Drawing.Size(140, 20);
			textboxPaidAmount.TabIndex = 12;
			textboxPaidAmount.Text = "0.00";
			textboxPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textboxPaidAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxInsurancePayableAmount.AllowDecimal = true;
			textBoxInsurancePayableAmount.BackColor = System.Drawing.Color.White;
			textBoxInsurancePayableAmount.CustomReportFieldName = "";
			textBoxInsurancePayableAmount.CustomReportKey = "";
			textBoxInsurancePayableAmount.CustomReportValueType = 1;
			textBoxInsurancePayableAmount.IsComboTextBox = false;
			textBoxInsurancePayableAmount.IsModified = false;
			textBoxInsurancePayableAmount.Location = new System.Drawing.Point(388, 165);
			textBoxInsurancePayableAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsurancePayableAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsurancePayableAmount.Name = "textBoxInsurancePayableAmount";
			textBoxInsurancePayableAmount.NullText = "0";
			textBoxInsurancePayableAmount.Size = new System.Drawing.Size(139, 20);
			textBoxInsurancePayableAmount.TabIndex = 10;
			textBoxInsurancePayableAmount.Text = "0.00";
			textBoxInsurancePayableAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsurancePayableAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(8, 109);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(52, 13);
			mmLabel39.TabIndex = 69;
			mmLabel39.Text = "Remarks:";
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(278, 168);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(89, 13);
			mmLabel36.TabIndex = 63;
			mmLabel36.Text = "Payable Amount:";
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(8, 44);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(85, 13);
			mmLabel34.TabIndex = 61;
			mmLabel34.Text = "Insurance Date:";
			textBoxInsuranceRemarks.BackColor = System.Drawing.Color.White;
			textBoxInsuranceRemarks.CustomReportFieldName = "";
			textBoxInsuranceRemarks.CustomReportKey = "";
			textBoxInsuranceRemarks.CustomReportValueType = 1;
			textBoxInsuranceRemarks.IsComboTextBox = false;
			textBoxInsuranceRemarks.IsModified = false;
			textBoxInsuranceRemarks.Location = new System.Drawing.Point(118, 106);
			textBoxInsuranceRemarks.MaxLength = 255;
			textBoxInsuranceRemarks.Multiline = true;
			textBoxInsuranceRemarks.Name = "textBoxInsuranceRemarks";
			textBoxInsuranceRemarks.Size = new System.Drawing.Size(409, 46);
			textBoxInsuranceRemarks.TabIndex = 8;
			dateTimePickerInsuranceDate.Checked = false;
			dateTimePickerInsuranceDate.CustomFormat = " ";
			dateTimePickerInsuranceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerInsuranceDate.Location = new System.Drawing.Point(118, 40);
			dateTimePickerInsuranceDate.Name = "dateTimePickerInsuranceDate";
			dateTimePickerInsuranceDate.ShowCheckBox = true;
			dateTimePickerInsuranceDate.Size = new System.Drawing.Size(159, 20);
			dateTimePickerInsuranceDate.TabIndex = 2;
			dateTimePickerInsuranceDate.Value = new System.DateTime(0L);
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(278, 44);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(99, 13);
			mmLabel38.TabIndex = 1;
			mmLabel38.Text = "Insurance Number:";
			textBoxInsuranceApprovedAmount.AllowDecimal = true;
			textBoxInsuranceApprovedAmount.BackColor = System.Drawing.Color.White;
			textBoxInsuranceApprovedAmount.CustomReportFieldName = "";
			textBoxInsuranceApprovedAmount.CustomReportKey = "";
			textBoxInsuranceApprovedAmount.CustomReportValueType = 1;
			textBoxInsuranceApprovedAmount.IsComboTextBox = false;
			textBoxInsuranceApprovedAmount.IsModified = false;
			textBoxInsuranceApprovedAmount.Location = new System.Drawing.Point(388, 62);
			textBoxInsuranceApprovedAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceApprovedAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceApprovedAmount.Name = "textBoxInsuranceApprovedAmount";
			textBoxInsuranceApprovedAmount.NullText = "0";
			textBoxInsuranceApprovedAmount.Size = new System.Drawing.Size(139, 20);
			textBoxInsuranceApprovedAmount.TabIndex = 6;
			textBoxInsuranceApprovedAmount.Text = "0.00";
			textBoxInsuranceApprovedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceApprovedAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(278, 66);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(99, 13);
			mmLabel37.TabIndex = 4;
			mmLabel37.Text = "Insurance Amount:";
			textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
			textBoxInsuranceNumber.CustomReportFieldName = "";
			textBoxInsuranceNumber.CustomReportKey = "";
			textBoxInsuranceNumber.CustomReportValueType = 1;
			textBoxInsuranceNumber.IsComboTextBox = false;
			textBoxInsuranceNumber.IsModified = false;
			textBoxInsuranceNumber.Location = new System.Drawing.Point(388, 40);
			textBoxInsuranceNumber.MaxLength = 30;
			textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
			textBoxInsuranceNumber.Size = new System.Drawing.Size(139, 20);
			textBoxInsuranceNumber.TabIndex = 5;
			textBoxVoucherNumber.BackColor = System.Drawing.Color.White;
			textBoxVoucherNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVoucherNumber.CustomReportFieldName = "";
			textBoxVoucherNumber.CustomReportKey = "";
			textBoxVoucherNumber.CustomReportValueType = 1;
			textBoxVoucherNumber.IsComboTextBox = false;
			textBoxVoucherNumber.IsModified = false;
			textBoxVoucherNumber.Location = new System.Drawing.Point(377, 9);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(129, 20);
			textBoxVoucherNumber.TabIndex = 1;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 146;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			formManager.Click += new System.EventHandler(formManager_Click);
			labelCloseDate.AutoSize = true;
			labelCloseDate.BackColor = System.Drawing.Color.Transparent;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(510, 11);
			labelCloseDate.Name = "labelCloseDate";
			labelCloseDate.PenWidth = 1f;
			labelCloseDate.ShowBorder = false;
			labelCloseDate.Size = new System.Drawing.Size(33, 13);
			labelCloseDate.TabIndex = 148;
			labelCloseDate.Text = "Date:";
			appearance39.BackColor = System.Drawing.Color.WhiteSmoke;
			appearance39.FontData.BoldAsString = "True";
			appearance39.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance39;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 11);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance40.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance40;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked_1);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(549, 9);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(85, 20);
			dateTimePickerDate.TabIndex = 2;
			appearance41.FontData.BoldAsString = "True";
			appearance41.FontData.Name = "Tahoma";
			labelCustomer.Appearance = appearance41;
			labelCustomer.AutoSize = true;
			labelCustomer.Location = new System.Drawing.Point(11, 34);
			labelCustomer.Name = "labelCustomer";
			labelCustomer.Size = new System.Drawing.Size(62, 15);
			labelCustomer.TabIndex = 165;
			labelCustomer.TabStop = true;
			labelCustomer.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCustomer.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCustomer.Value = "Customer:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			labelCustomer.VisitedLinkAppearance = appearance42;
			labelCustomer.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked_1);
			appearance43.FontData.BoldAsString = "True";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance43;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(285, 12);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 149;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance44;
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(button1);
			ultraTabPageControl2.Controls.Add(comboBoxFollowupPeriod);
			ultraTabPageControl2.Controls.Add(buttonAddActivity);
			ultraTabPageControl2.Controls.Add(dataGridListFollowup);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(777, 419);
			mmLabel25.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(576, 6);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(41, 13);
			mmLabel25.TabIndex = 366;
			mmLabel25.Text = "Period:";
			button1.Image = Micromind.ClientUI.Properties.Resources.add;
			button1.Location = new System.Drawing.Point(2, 1);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(23, 22);
			button1.TabIndex = 365;
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			comboBoxFollowupPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxFollowupPeriod.Location = new System.Drawing.Point(623, 3);
			comboBoxFollowupPeriod.Name = "comboBoxFollowupPeriod";
			comboBoxFollowupPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxFollowupPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxFollowupPeriod.Size = new System.Drawing.Size(152, 20);
			comboBoxFollowupPeriod.TabIndex = 364;
			comboBoxFollowupPeriod.SelectedIndexChanged += new System.EventHandler(comboBoxFollowupPeriod_SelectedIndexChanged);
			buttonAddActivity.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAddActivity.Location = new System.Drawing.Point(0, -60);
			buttonAddActivity.Name = "buttonAddActivity";
			buttonAddActivity.Size = new System.Drawing.Size(23, 22);
			buttonAddActivity.TabIndex = 362;
			buttonAddActivity.UseVisualStyleBackColor = true;
			dataGridListFollowup.AllowUnfittedView = false;
			dataGridListFollowup.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListFollowup.DisplayLayout.Appearance = appearance45;
			dataGridListFollowup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListFollowup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			dataGridListFollowup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			dataGridListFollowup.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListFollowup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListFollowup.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListFollowup.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListFollowup.DisplayLayout.Override.CellAppearance = appearance52;
			dataGridListFollowup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListFollowup.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			dataGridListFollowup.DisplayLayout.Override.HeaderAppearance = appearance54;
			dataGridListFollowup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListFollowup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			dataGridListFollowup.DisplayLayout.Override.RowAppearance = appearance55;
			dataGridListFollowup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListFollowup.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			dataGridListFollowup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListFollowup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListFollowup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListFollowup.LoadLayoutFailed = false;
			dataGridListFollowup.Location = new System.Drawing.Point(2, 27);
			dataGridListFollowup.Name = "dataGridListFollowup";
			dataGridListFollowup.ShowDeleteMenu = false;
			dataGridListFollowup.ShowMinusInRed = true;
			dataGridListFollowup.ShowNewMenu = false;
			dataGridListFollowup.Size = new System.Drawing.Size(775, 386);
			dataGridListFollowup.TabIndex = 363;
			dataGridListFollowup.Text = "dataGridList1";
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
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonComments,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripDropDownButton1,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(793, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonComments.Image = Micromind.ClientUI.Properties.Resources.comment;
			toolStripButtonComments.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonComments.Name = "toolStripButtonComments";
			toolStripButtonComments.Size = new System.Drawing.Size(28, 28);
			toolStripButtonComments.Text = "Comments...";
			toolStripButtonComments.Click += new System.EventHandler(toolStripButtonComments_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				createFromExistingActivityToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.Visible = false;
			createFromExistingActivityToolStripMenuItem.Name = "createFromExistingActivityToolStripMenuItem";
			createFromExistingActivityToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			createFromExistingActivityToolStripMenuItem.Text = "Create from Legal Actions";
			createFromExistingActivityToolStripMenuItem.Click += new System.EventHandler(createFromExistingActivityToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 483);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(793, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(793, 1);
			linePanelDown.TabIndex = 0;
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
			xpButton1.Location = new System.Drawing.Point(685, 8);
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
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(777, 419);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
			ultraTabControl1.Location = new System.Drawing.Point(6, 34);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(781, 442);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance57.Cursor = System.Windows.Forms.Cursors.Hand;
			ultraTab.ActiveAppearance = appearance57;
			appearance58.BorderColor3DBase = System.Drawing.Color.Transparent;
			appearance58.Cursor = System.Windows.Forms.Cursors.Arrow;
			ultraTab.Appearance = appearance58;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Activity";
			appearance59.Cursor = System.Windows.Forms.Cursors.Hand;
			ultraTab2.ActiveAppearance = appearance59;
			appearance60.BorderColor3DBase = System.Drawing.Color.Transparent;
			appearance60.Cursor = System.Windows.Forms.Cursors.Arrow;
			ultraTab2.Appearance = appearance60;
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Follow Up";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabControl1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			ultraTabControl1.Click += new System.EventHandler(ultraTabControl1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(793, 523);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CustomerInsuranceClaimForm";
			Text = "Customer Insurance Claim";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)customersFlatComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			ultraGroupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStatus).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
