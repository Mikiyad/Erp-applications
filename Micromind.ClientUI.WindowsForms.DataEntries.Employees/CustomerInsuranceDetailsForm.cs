using DevExpress.Utils;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Customers;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class CustomerInsuranceDetailsForm : Form, IForm
	{
		private CustomerInsuranceData currentData;

		private const string TABLENAME_CONST = "Customer_Insurance";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string customerID;

		private string policyNumber;

		private string amount;

		private string remarks;

		private int status;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private FormManager formManager;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonInformation;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private ToolStrip miniToolStrip;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private MMTextBox textBoxProvider;

		private MMTextBox textBoxInsuranceNumber;

		private CreditInsuranceStatusComboBox comboBoxInsuranceStatus;

		private MMSDateTimePicker dateTimePickerInsuranceDate;

		private UltraComboEditor comboBoxInsuranceRating;

		private AmountTextBox textBoxInsuranceApprovedAmount;

		private AmountTextBox textBoxInsuranceReqAmount;

		private MMSDateTimePicker datetimePickerEffectiveDate;

		private MMSDateTimePicker dateTimePickerValidTo;

		private TextBox textBoxVoucherNumber;

		private MMTextBox textBoxInsuranceID;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private customersFlatComboBox comboBoxCustomer;

		private TextBox textBoxCustomerName;

		private InsuranceProviderComboBox comboBoxInsuranceProvider;

		private MMTextBox textBoxInsuranceRemarks;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private MMSDateTimePicker dateTimePickerReviewDate;

		private LayoutControl layoutControl1;

		private LayoutControlGroup Root;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem7;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem6;

		private LayoutControlItem layoutControlItem8;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem10;

		private LayoutControlItem layoutControlItem11;

		private LayoutControlItem layoutControlItem12;

		private LayoutControlItem layoutControlItem13;

		private SimpleSeparator simpleSeparator2;

		private LayoutControlItem layoutControlItem9;

		private LayoutControlItem layoutControlItem14;

		private LayoutControlItem layoutControlItem15;

		private LayoutControlItem layoutControlItem16;

		private LayoutControlItem layoutControlItem17;

		private LayoutControlGroup layoutControlGroupInsuranceDetails;

		private EmptySpaceItem emptySpaceItem1;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 4012;

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

		public string PolicyNumber
		{
			get
			{
				return policyNumber;
			}
			set
			{
				policyNumber = value;
			}
		}

		public string Amount
		{
			get
			{
				return amount;
			}
			set
			{
				amount = value;
			}
		}

		public string Remarks
		{
			get
			{
				return remarks;
			}
			set
			{
				remarks = value;
			}
		}

		public int Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
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
			}
		}

		public CustomerInsuranceDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxInsuranceStatus.LoadData();
		}

		private void AddEvents()
		{
			base.Load += CustomerInsuranceDetailsForm_Load;
			comboBoxInsuranceStatus.ValueChanged += ComboBoxInsuranceStatus_ValueChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
		}

		private void ComboBoxInsuranceStatus_ValueChanged(object sender, EventArgs e)
		{
			if (comboBoxInsuranceStatus.SelectedID == 1)
			{
				layoutControlGroupInsuranceDetails.Visibility = LayoutVisibility.OnlyInCustomization;
			}
			else
			{
				layoutControlGroupInsuranceDetails.Visibility = LayoutVisibility.Always;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomerInsuranceData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerInsuranceTable.Rows[0] : currentData.CustomerInsuranceTable.NewRow();
				dataRow.BeginEdit();
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["InsStatus"] = checked(comboBoxInsuranceStatus.SelectedIndex + 1);
				dataRow["InsuranceID"] = textBoxInsuranceID.Text.TrimStart();
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["ReviewDate"] = dateTimePickerReviewDate.Value;
				dataRow["SysDocID"] = "SYS_015";
				if (layoutControlGroupInsuranceDetails.Visible)
				{
					dataRow["InsPolicyNumber"] = textBoxInsuranceNumber.Text;
				}
				else
				{
					dataRow["InsPolicyNumber"] = "";
				}
				if (layoutControlGroupInsuranceDetails.Visible && dateTimePickerInsuranceDate.Checked)
				{
					dataRow["InsApplicationDate"] = dateTimePickerInsuranceDate.Value;
				}
				else
				{
					dataRow["InsApplicationDate"] = DateTime.Now;
				}
				if (layoutControlGroupInsuranceDetails.Visible && textBoxInsuranceApprovedAmount.Text != "")
				{
					dataRow["InsApprovedAmount"] = textBoxInsuranceApprovedAmount.Text;
				}
				else
				{
					dataRow["InsApprovedAmount"] = 0.0;
				}
				if (layoutControlGroupInsuranceDetails.Visible && textBoxInsuranceReqAmount.Text != "")
				{
					dataRow["InsRequestedAmount"] = textBoxInsuranceReqAmount.Text;
				}
				else
				{
					dataRow["InsRequestedAmount"] = 0.0;
				}
				if (layoutControlGroupInsuranceDetails.Visible)
				{
					dataRow["InsRemarks"] = textBoxInsuranceRemarks.Text.Trim();
				}
				else
				{
					dataRow["InsRemarks"] = "";
				}
				if (layoutControlGroupInsuranceDetails.Visible && comboBoxInsuranceRating.SelectedIndex != -1)
				{
					dataRow["InsRating"] = comboBoxInsuranceRating.SelectedIndex;
				}
				else
				{
					dataRow["InsRating"] = DBNull.Value;
				}
				if (layoutControlGroupInsuranceDetails.Visible)
				{
					dataRow["InsProvider"] = comboBoxInsuranceProvider.Text.TrimStart();
				}
				else
				{
					dataRow["InsProvider"] = DBNull.Value;
				}
				if (layoutControlGroupInsuranceDetails.Visible && datetimePickerEffectiveDate.Checked)
				{
					dataRow["InsEffectiveDate"] = datetimePickerEffectiveDate.Value;
				}
				else
				{
					dataRow["InsEffectiveDate"] = DBNull.Value;
				}
				if (layoutControlGroupInsuranceDetails.Visible && dateTimePickerValidTo.Checked)
				{
					dataRow["InsValidTo"] = dateTimePickerValidTo.Value;
				}
				else
				{
					dataRow["InsValidTo"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerInsuranceTable.Rows.Add(dataRow);
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
						if (dataRow["InsRequestedAmount"] != null && dataRow["InsRequestedAmount"].ToString() != "")
						{
							textBoxInsuranceReqAmount.Text = dataRow["InsRequestedAmount"].ToString();
						}
						if (dataRow["CustomerID"] != null)
						{
							comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
							comboBoxCustomer.ReadOnly = true;
						}
						if (dataRow["InsStatus"] != DBNull.Value)
						{
							comboBoxInsuranceStatus.SelectedIndex = checked(int.Parse(dataRow["InsStatus"].ToString()) - 1);
						}
						else
						{
							comboBoxInsuranceStatus.SelectedIndex = 0;
						}
						if (dataRow["InsProviderID"] != null)
						{
							comboBoxInsuranceProvider.SelectedID = dataRow["InsProviderID"].ToString().TrimStart();
						}
						if (dataRow["InsRating"] != DBNull.Value)
						{
							comboBoxInsuranceRating.SelectedIndex = int.Parse(dataRow["InsRating"].ToString());
						}
						else
						{
							comboBoxInsuranceRating.SelectedIndex = 0;
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

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CustomerInsuranceSystem.GetCustomerInsuranceByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						FillData();
						IsNewRecord = false;
						buttonSave.Enabled = false;
						textBoxVoucherNumber.ReadOnly = true;
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
					DataRow dataRow = currentData.Tables[0].Rows[0];
					if (dataRow["VoucherID"] != null)
					{
						textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					}
					if (!dataRow["ReviewDate"].IsDBNullOrEmpty())
					{
						dateTimePickerReviewDate.Value = DateTime.Parse(dataRow["ReviewDate"].ToString());
					}
					if (dataRow["InsApplicationDate"] != DBNull.Value)
					{
						dateTimePickerInsuranceDate.Value = DateTime.Parse(dataRow["InsApplicationDate"].ToString());
						dateTimePickerInsuranceDate.Checked = true;
					}
					if (dataRow["InsEffectiveDate"] != DBNull.Value)
					{
						datetimePickerEffectiveDate.Value = DateTime.Parse(dataRow["InsEffectiveDate"].ToString());
						datetimePickerEffectiveDate.Checked = true;
					}
					if (dataRow["InsValidTo"] != DBNull.Value)
					{
						dateTimePickerValidTo.Value = DateTime.Parse(dataRow["InsValidTo"].ToString());
						dateTimePickerValidTo.Checked = true;
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
					if (dataRow["InsRequestedAmount"] != null)
					{
						textBoxInsuranceReqAmount.Text = dataRow["InsRequestedAmount"].ToString();
					}
					if (dataRow["CustomerID"] != null)
					{
						comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					}
					if (dataRow["InsStatus"] != null)
					{
						comboBoxInsuranceStatus.SelectedIndex = checked(int.Parse(dataRow["InsStatus"].ToString()) - 1);
					}
					if (dataRow["InsProvider"] != null)
					{
						comboBoxInsuranceProvider.SelectedID = dataRow["InsProvider"].ToString();
					}
					if (dataRow["InsRating"] != null)
					{
						comboBoxInsuranceRating.SelectedIndex = int.Parse(dataRow["InsRating"].ToString());
					}
					else
					{
						comboBoxInsuranceRating.SelectedIndex = 0;
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
				bool flag = (!isNewRecord) ? Factory.CustomerInsuranceSystem.CreateCustomerInsurance(currentData, isUpdate: true) : Factory.CustomerInsuranceSystem.CreateCustomerInsurance(currentData, isUpdate: false);
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
			_ = dateTimePickerReviewDate.Value;
			if (comboBoxCustomer.SelectedID == "" || textBoxVoucherNumber.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
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
			datetimePickerEffectiveDate.Clear();
			dateTimePickerValidTo.Clear();
			textBoxInsuranceRemarks.Clear();
			textBoxInsuranceReqAmount.Clear();
			comboBoxInsuranceStatus.SelectedIndex = 0;
			comboBoxInsuranceRating.SelectedIndex = 0;
			textBoxInsuranceID.Clear();
			dateTimePickerInsuranceDate.Clear();
			dateTimePickerReviewDate.Value = DateTime.Now;
			formManager.ResetDirty();
		}

		private void ProductManufacturerGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductManufacturerGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Customer_Insurance", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Customer_Insurance", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Customer_Insurance", "VoucherID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Customer_Insurance", "VoucherID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer_Insurance", "VoucherID", toolStripTextBoxFind.Text.Trim()))
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
			CustomerInsuranceListForm customerInsuranceListForm = new CustomerInsuranceListForm();
			customerInsuranceListForm.CustomerID = comboBoxCustomer.SelectedID;
			customerInsuranceListForm.ShowDialog();
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditInsuranceProvider(comboBoxInsuranceProvider.SelectedID);
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.CustomerInsuranceSystem.GetNextDocumentNumber("SYS_015");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void CustomerInsuranceDetailsForm_Load(object sender, EventArgs e)
		{
			SetSecurity();
			if (!base.IsDisposed)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				if (CustomerID != null && CustomerID != "")
				{
					LoadCustomerData(CustomerID);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
				}
				formManager.ResetDirty();
			}
		}

		private void ultraGroupBox5_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxInsuranceProvider_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void comboBoxInsuranceProvider_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxProvider.Text = comboBoxInsuranceProvider.SelectedName;
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = "SYS_015";
					docManagementForm.EntityName = "SYS_015";
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxInsuranceRating_ValueChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!(comboBoxCustomer.SelectedID != ""))
			{
				return;
			}
			string selectedID = comboBoxCustomer.SelectedID;
			DataSet customerByID = Factory.CustomerSystem.GetCustomerByID(selectedID);
			if (customerByID == null || customerByID.Tables.Count == 0 || customerByID.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = customerByID.Tables[0].Rows[0];
			if (dataRow["InsApprovedAmount"] != DBNull.Value)
			{
				textBoxInsuranceApprovedAmount.Text = decimal.Parse(dataRow["InsApprovedAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsuranceApprovedAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			if (dataRow["InsRequestedAmount"] != DBNull.Value)
			{
				textBoxInsuranceReqAmount.Text = decimal.Parse(dataRow["InsRequestedAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsuranceReqAmount.Text = 0.ToString(Format.TotalAmountFormat);
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
			checked
			{
				if (dataRow["InsStatus"] != DBNull.Value)
				{
					comboBoxInsuranceStatus.SelectedIndex = unchecked((int)byte.Parse(dataRow["InsStatus"].ToString())) - 1;
				}
				else
				{
					comboBoxInsuranceStatus.SelectedIndex = 0;
				}
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
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void layoutControlItem12_Click(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.CustomerInsuranceDetailsForm));
			Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			miniToolStrip = new System.Windows.Forms.ToolStrip();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			comboBoxInsuranceRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			textBoxProvider = new Micromind.UISupport.MMTextBox();
			textBoxInsuranceID = new Micromind.UISupport.MMTextBox();
			dateTimePickerValidTo = new Micromind.UISupport.MMSDateTimePicker(components);
			datetimePickerEffectiveDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxInsuranceApprovedAmount = new Micromind.UISupport.AmountTextBox();
			textBoxInsuranceReqAmount = new Micromind.UISupport.AmountTextBox();
			textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
			dateTimePickerInsuranceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxInsuranceRemarks = new Micromind.UISupport.MMTextBox();
			dateTimePickerReviewDate = new Micromind.UISupport.MMSDateTimePicker(components);
			layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			comboBoxInsuranceProvider = new Micromind.DataControls.InsuranceProviderComboBox();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxInsuranceStatus = new Micromind.DataControls.CreditInsuranceStatusComboBox();
			Root = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
			layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
			formManager = new Micromind.DataControls.FormManager();
			layoutControlGroupInsuranceDetails = new DevExpress.XtraLayout.LayoutControlGroup();
			emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceRating).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
			layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceStatus).BeginInit();
			((System.ComponentModel.ISupportInitialize)Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
			((System.ComponentModel.ISupportInitialize)simpleSeparator2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroupInsuranceDetails).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
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
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(827, 31);
			toolStrip1.TabIndex = 16;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
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
			toolStripButtonNext.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonNext.Image");
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
			toolStripButtonOpenList.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonOpenList.Image");
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 491);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(827, 40);
			panelButtons.TabIndex = 6;
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
			buttonDelete.Visible = false;
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
			linePanelDown.Size = new System.Drawing.Size(827, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(714, 8);
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
			miniToolStrip.AutoSize = false;
			miniToolStrip.CanOverflow = false;
			miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			miniToolStrip.Location = new System.Drawing.Point(377, 3);
			miniToolStrip.Name = "miniToolStrip";
			miniToolStrip.Size = new System.Drawing.Size(567, 25);
			miniToolStrip.TabIndex = 189;
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(563, 211);
			comboBoxInsuranceRating.AutoSize = false;
			comboBoxInsuranceRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			valueListItem.DataValue = (byte)0;
			valueListItem.DisplayText = "N/A";
			valueListItem2.DataValue = (byte)1;
			valueListItem2.DisplayText = "1";
			valueListItem3.DataValue = "2";
			valueListItem3.DisplayText = "2";
			valueListItem4.DataValue = (byte)3;
			valueListItem4.DisplayText = "3";
			valueListItem5.DataValue = "4";
			valueListItem5.DisplayText = "4";
			valueListItem6.DataValue = (byte)5;
			valueListItem6.DisplayText = "5";
			valueListItem7.DataValue = (byte)6;
			valueListItem7.DisplayText = "6";
			valueListItem8.DataValue = "7";
			valueListItem8.DisplayText = "7";
			valueListItem9.DataValue = "8";
			valueListItem9.DisplayText = "8";
			valueListItem10.DataValue = "9";
			valueListItem10.DisplayText = "9";
			valueListItem11.DataValue = "10";
			valueListItem11.DisplayText = "10";
			comboBoxInsuranceRating.Items.AddRange(new Infragistics.Win.ValueListItem[11]
			{
				valueListItem,
				valueListItem2,
				valueListItem3,
				valueListItem4,
				valueListItem5,
				valueListItem6,
				valueListItem7,
				valueListItem8,
				valueListItem9,
				valueListItem10,
				valueListItem11
			});
			comboBoxInsuranceRating.Location = new System.Drawing.Point(140, 202);
			comboBoxInsuranceRating.Name = "comboBoxInsuranceRating";
			comboBoxInsuranceRating.Size = new System.Drawing.Size(280, 20);
			comboBoxInsuranceRating.TabIndex = 11;
			comboBoxInsuranceRating.ValueChanged += new System.EventHandler(comboBoxInsuranceRating_ValueChanged);
			textBoxVoucherNumber.Location = new System.Drawing.Point(128, 12);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(676, 20);
			textBoxVoucherNumber.TabIndex = 0;
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(329, 36);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(475, 20);
			textBoxCustomerName.TabIndex = 2;
			textBoxCustomerName.TabStop = false;
			textBoxProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProvider.CustomReportFieldName = "";
			textBoxProvider.CustomReportKey = "";
			textBoxProvider.CustomReportValueType = 1;
			textBoxProvider.Enabled = false;
			textBoxProvider.IsComboTextBox = false;
			textBoxProvider.IsModified = false;
			textBoxProvider.Location = new System.Drawing.Point(424, 130);
			textBoxProvider.MaxLength = 64;
			textBoxProvider.Name = "textBoxProvider";
			textBoxProvider.ReadOnly = true;
			textBoxProvider.Size = new System.Drawing.Size(368, 20);
			textBoxProvider.TabIndex = 6;
			textBoxProvider.TabStop = false;
			textBoxInsuranceID.BackColor = System.Drawing.Color.White;
			textBoxInsuranceID.CustomReportFieldName = "";
			textBoxInsuranceID.CustomReportKey = "";
			textBoxInsuranceID.CustomReportValueType = 1;
			textBoxInsuranceID.IsComboTextBox = false;
			textBoxInsuranceID.IsModified = false;
			textBoxInsuranceID.Location = new System.Drawing.Point(540, 202);
			textBoxInsuranceID.MaxLength = 30;
			textBoxInsuranceID.Name = "textBoxInsuranceID";
			textBoxInsuranceID.Size = new System.Drawing.Size(252, 20);
			textBoxInsuranceID.TabIndex = 12;
			dateTimePickerValidTo.Checked = false;
			dateTimePickerValidTo.CustomFormat = " ";
			dateTimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerValidTo.Location = new System.Drawing.Point(540, 178);
			dateTimePickerValidTo.Name = "dateTimePickerValidTo";
			dateTimePickerValidTo.ShowCheckBox = true;
			dateTimePickerValidTo.Size = new System.Drawing.Size(252, 20);
			dateTimePickerValidTo.TabIndex = 14;
			dateTimePickerValidTo.Value = new System.DateTime(0L);
			datetimePickerEffectiveDate.Checked = false;
			datetimePickerEffectiveDate.CustomFormat = " ";
			datetimePickerEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerEffectiveDate.Location = new System.Drawing.Point(140, 178);
			datetimePickerEffectiveDate.Name = "datetimePickerEffectiveDate";
			datetimePickerEffectiveDate.ShowCheckBox = true;
			datetimePickerEffectiveDate.Size = new System.Drawing.Size(280, 20);
			datetimePickerEffectiveDate.TabIndex = 13;
			datetimePickerEffectiveDate.Value = new System.DateTime(0L);
			textBoxInsuranceApprovedAmount.AllowDecimal = true;
			textBoxInsuranceApprovedAmount.CustomReportFieldName = "";
			textBoxInsuranceApprovedAmount.CustomReportKey = "";
			textBoxInsuranceApprovedAmount.CustomReportValueType = 1;
			textBoxInsuranceApprovedAmount.IsComboTextBox = false;
			textBoxInsuranceApprovedAmount.IsModified = false;
			textBoxInsuranceApprovedAmount.Location = new System.Drawing.Point(540, 226);
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
			textBoxInsuranceApprovedAmount.Size = new System.Drawing.Size(252, 20);
			textBoxInsuranceApprovedAmount.TabIndex = 10;
			textBoxInsuranceApprovedAmount.Text = "0.00";
			textBoxInsuranceApprovedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceApprovedAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxInsuranceReqAmount.AllowDecimal = true;
			textBoxInsuranceReqAmount.CustomReportFieldName = "";
			textBoxInsuranceReqAmount.CustomReportKey = "";
			textBoxInsuranceReqAmount.CustomReportValueType = 1;
			textBoxInsuranceReqAmount.IsComboTextBox = false;
			textBoxInsuranceReqAmount.IsModified = false;
			textBoxInsuranceReqAmount.Location = new System.Drawing.Point(140, 226);
			textBoxInsuranceReqAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceReqAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceReqAmount.Name = "textBoxInsuranceReqAmount";
			textBoxInsuranceReqAmount.NullText = "0";
			textBoxInsuranceReqAmount.Size = new System.Drawing.Size(280, 20);
			textBoxInsuranceReqAmount.TabIndex = 9;
			textBoxInsuranceReqAmount.Text = "0.00";
			textBoxInsuranceReqAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceReqAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
			textBoxInsuranceNumber.CustomReportFieldName = "";
			textBoxInsuranceNumber.CustomReportKey = "";
			textBoxInsuranceNumber.CustomReportValueType = 1;
			textBoxInsuranceNumber.IsComboTextBox = false;
			textBoxInsuranceNumber.IsModified = false;
			textBoxInsuranceNumber.Location = new System.Drawing.Point(540, 154);
			textBoxInsuranceNumber.MaxLength = 30;
			textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
			textBoxInsuranceNumber.Size = new System.Drawing.Size(252, 20);
			textBoxInsuranceNumber.TabIndex = 8;
			dateTimePickerInsuranceDate.Checked = false;
			dateTimePickerInsuranceDate.CustomFormat = " ";
			dateTimePickerInsuranceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerInsuranceDate.Location = new System.Drawing.Point(140, 154);
			dateTimePickerInsuranceDate.Name = "dateTimePickerInsuranceDate";
			dateTimePickerInsuranceDate.ShowCheckBox = true;
			dateTimePickerInsuranceDate.Size = new System.Drawing.Size(280, 20);
			dateTimePickerInsuranceDate.TabIndex = 7;
			dateTimePickerInsuranceDate.Value = new System.DateTime(0L);
			textBoxInsuranceRemarks.BackColor = System.Drawing.Color.White;
			textBoxInsuranceRemarks.CustomReportFieldName = "";
			textBoxInsuranceRemarks.CustomReportKey = "";
			textBoxInsuranceRemarks.CustomReportValueType = 1;
			textBoxInsuranceRemarks.IsComboTextBox = false;
			textBoxInsuranceRemarks.IsModified = false;
			textBoxInsuranceRemarks.Location = new System.Drawing.Point(140, 250);
			textBoxInsuranceRemarks.MaxLength = 255;
			textBoxInsuranceRemarks.MinimumSize = new System.Drawing.Size(0, 80);
			textBoxInsuranceRemarks.Multiline = true;
			textBoxInsuranceRemarks.Name = "textBoxInsuranceRemarks";
			textBoxInsuranceRemarks.Size = new System.Drawing.Size(652, 182);
			textBoxInsuranceRemarks.TabIndex = 15;
			dateTimePickerReviewDate.Checked = false;
			dateTimePickerReviewDate.CustomFormat = " ";
			dateTimePickerReviewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerReviewDate.Location = new System.Drawing.Point(526, 60);
			dateTimePickerReviewDate.Name = "dateTimePickerReviewDate";
			dateTimePickerReviewDate.Size = new System.Drawing.Size(278, 20);
			dateTimePickerReviewDate.TabIndex = 4;
			dateTimePickerReviewDate.Value = new System.DateTime(0L);
			layoutControl1.Controls.Add(textBoxInsuranceRemarks);
			layoutControl1.Controls.Add(comboBoxInsuranceProvider);
			layoutControl1.Controls.Add(textBoxInsuranceID);
			layoutControl1.Controls.Add(dateTimePickerValidTo);
			layoutControl1.Controls.Add(datetimePickerEffectiveDate);
			layoutControl1.Controls.Add(dateTimePickerReviewDate);
			layoutControl1.Controls.Add(comboBoxCustomer);
			layoutControl1.Controls.Add(textBoxVoucherNumber);
			layoutControl1.Controls.Add(comboBoxInsuranceRating);
			layoutControl1.Controls.Add(textBoxCustomerName);
			layoutControl1.Controls.Add(comboBoxInsuranceStatus);
			layoutControl1.Controls.Add(textBoxInsuranceApprovedAmount);
			layoutControl1.Controls.Add(textBoxProvider);
			layoutControl1.Controls.Add(dateTimePickerInsuranceDate);
			layoutControl1.Controls.Add(textBoxInsuranceReqAmount);
			layoutControl1.Controls.Add(textBoxInsuranceNumber);
			layoutControl1.Location = new System.Drawing.Point(-3, 36);
			layoutControl1.Name = "layoutControl1";
			layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(868, 246, 977, 755);
			layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
			layoutControl1.Root = Root;
			layoutControl1.Size = new System.Drawing.Size(816, 458);
			layoutControl1.TabIndex = 74;
			layoutControl1.Text = "layoutControl1";
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
			comboBoxInsuranceProvider.Location = new System.Drawing.Point(140, 130);
			comboBoxInsuranceProvider.MaxDropDownItems = 12;
			comboBoxInsuranceProvider.Name = "comboBoxInsuranceProvider";
			comboBoxInsuranceProvider.ShowInactiveItems = false;
			comboBoxInsuranceProvider.ShowQuickAdd = true;
			comboBoxInsuranceProvider.Size = new System.Drawing.Size(280, 20);
			comboBoxInsuranceProvider.TabIndex = 5;
			comboBoxInsuranceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxInsuranceProvider.SelectedIndexChanged += new System.EventHandler(comboBoxInsuranceProvider_SelectedIndexChanged);
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxCustomerName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxCustomer.Location = new System.Drawing.Point(128, 60);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(278, 20);
			comboBoxCustomer.TabIndex = 1;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxInsuranceStatus.AutoSize = false;
			comboBoxInsuranceStatus.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxInsuranceStatus.Location = new System.Drawing.Point(128, 36);
			comboBoxInsuranceStatus.Name = "comboBoxInsuranceStatus";
			comboBoxInsuranceStatus.SelectedID = 1;
			comboBoxInsuranceStatus.SelectedType = Micromind.Common.Data.CRMRelatedTypes.Lead;
			comboBoxInsuranceStatus.Size = new System.Drawing.Size(197, 20);
			comboBoxInsuranceStatus.TabIndex = 3;
			Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			Root.GroupBordersVisible = false;
			Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[8]
			{
				layoutControlItem4,
				layoutControlGroupInsuranceDetails,
				simpleSeparator2,
				layoutControlItem7,
				emptySpaceItem1,
				layoutControlItem1,
				layoutControlItem6,
				layoutControlItem8
			});
			Root.Name = "Root";
			Root.Size = new System.Drawing.Size(816, 458);
			Root.TextVisible = false;
			layoutControlItem7.Control = comboBoxCustomer;
			layoutControlItem7.Location = new System.Drawing.Point(0, 48);
			layoutControlItem7.Name = "layoutControlItem7";
			layoutControlItem7.Size = new System.Drawing.Size(398, 24);
			layoutControlItem7.Text = "Insurance Status: ";
			layoutControlItem7.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem4.AllowHide = false;
			layoutControlItem4.Control = comboBoxInsuranceStatus;
			layoutControlItem4.CustomizationFormText = "Customer";
			layoutControlItem4.Location = new System.Drawing.Point(0, 24);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new System.Drawing.Size(317, 24);
			layoutControlItem4.Text = "Customer:";
			layoutControlItem4.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem1.AllowHide = false;
			layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
			layoutControlItem1.Control = textBoxVoucherNumber;
			layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(796, 24);
			layoutControlItem1.Text = "Number:";
			layoutControlItem1.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem6.Control = textBoxCustomerName;
			layoutControlItem6.Location = new System.Drawing.Point(317, 24);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new System.Drawing.Size(479, 24);
			layoutControlItem6.Text = "ProviderName";
			layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem6.TextVisible = false;
			layoutControlItem8.Control = dateTimePickerReviewDate;
			layoutControlItem8.Location = new System.Drawing.Point(398, 48);
			layoutControlItem8.Name = "layoutControlItem8";
			layoutControlItem8.Size = new System.Drawing.Size(398, 24);
			layoutControlItem8.Text = "Review Date:";
			layoutControlItem8.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
			layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
			layoutControlItem3.Control = comboBoxInsuranceProvider;
			layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new System.Drawing.Size(400, 24);
			layoutControlItem3.Text = "Provider:";
			layoutControlItem3.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem5.Control = textBoxProvider;
			layoutControlItem5.CustomizationFormText = "ProviderName";
			layoutControlItem5.Location = new System.Drawing.Point(400, 0);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new System.Drawing.Size(372, 24);
			layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			layoutControlItem5.TextVisible = false;
			layoutControlItem10.Control = dateTimePickerInsuranceDate;
			layoutControlItem10.Location = new System.Drawing.Point(0, 24);
			layoutControlItem10.Name = "layoutControlItem10";
			layoutControlItem10.Size = new System.Drawing.Size(400, 24);
			layoutControlItem10.Text = "Application Date:";
			layoutControlItem10.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
			layoutControlItem11.Control = textBoxInsuranceNumber;
			layoutControlItem11.Location = new System.Drawing.Point(400, 24);
			layoutControlItem11.Name = "layoutControlItem11";
			layoutControlItem11.Size = new System.Drawing.Size(372, 24);
			layoutControlItem11.Text = "Application Number:";
			layoutControlItem11.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem12.AllowHtmlStringInCaption = true;
			layoutControlItem12.Control = textBoxInsuranceReqAmount;
			layoutControlItem12.Location = new System.Drawing.Point(0, 96);
			layoutControlItem12.Name = "layoutControlItem12";
			layoutControlItem12.Size = new System.Drawing.Size(400, 24);
			layoutControlItem12.Text = "Requested Amount:";
			layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Left;
			layoutControlItem12.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem12.Click += new System.EventHandler(layoutControlItem12_Click);
			layoutControlItem13.Control = textBoxInsuranceApprovedAmount;
			layoutControlItem13.Location = new System.Drawing.Point(400, 96);
			layoutControlItem13.Name = "layoutControlItem13";
			layoutControlItem13.Size = new System.Drawing.Size(372, 24);
			layoutControlItem13.Text = "Approved Amount:";
			layoutControlItem13.TextSize = new System.Drawing.Size(113, 13);
			simpleSeparator2.AllowHotTrack = false;
			simpleSeparator2.Location = new System.Drawing.Point(0, 436);
			simpleSeparator2.Name = "simpleSeparator2";
			simpleSeparator2.Size = new System.Drawing.Size(796, 2);
			layoutControlItem9.Control = comboBoxInsuranceRating;
			layoutControlItem9.Location = new System.Drawing.Point(0, 72);
			layoutControlItem9.Name = "layoutControlItem9";
			layoutControlItem9.Size = new System.Drawing.Size(400, 24);
			layoutControlItem9.Text = "Rating:";
			layoutControlItem9.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem14.Control = textBoxInsuranceID;
			layoutControlItem14.Location = new System.Drawing.Point(400, 72);
			layoutControlItem14.Name = "layoutControlItem14";
			layoutControlItem14.Size = new System.Drawing.Size(372, 24);
			layoutControlItem14.Text = "Insurance ID:";
			layoutControlItem14.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem15.Control = datetimePickerEffectiveDate;
			layoutControlItem15.Location = new System.Drawing.Point(0, 48);
			layoutControlItem15.Name = "layoutControlItem15";
			layoutControlItem15.Size = new System.Drawing.Size(400, 24);
			layoutControlItem15.Text = "Effective Date:";
			layoutControlItem15.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem16.Control = dateTimePickerValidTo;
			layoutControlItem16.Location = new System.Drawing.Point(400, 48);
			layoutControlItem16.Name = "layoutControlItem16";
			layoutControlItem16.Size = new System.Drawing.Size(372, 24);
			layoutControlItem16.Text = "Valid To:";
			layoutControlItem16.TextSize = new System.Drawing.Size(113, 13);
			layoutControlItem17.Control = textBoxInsuranceRemarks;
			layoutControlItem17.Location = new System.Drawing.Point(0, 120);
			layoutControlItem17.Name = "layoutControlItem17";
			layoutControlItem17.Size = new System.Drawing.Size(772, 186);
			layoutControlItem17.Text = "Remarks:";
			layoutControlItem17.TextSize = new System.Drawing.Size(113, 13);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 48;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			layoutControlGroupInsuranceDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[11]
			{
				layoutControlItem3,
				layoutControlItem9,
				layoutControlItem14,
				layoutControlItem11,
				layoutControlItem17,
				layoutControlItem12,
				layoutControlItem13,
				layoutControlItem10,
				layoutControlItem15,
				layoutControlItem16,
				layoutControlItem5
			});
			layoutControlGroupInsuranceDetails.Location = new System.Drawing.Point(0, 88);
			layoutControlGroupInsuranceDetails.Name = "layoutControlGroupInsuranceDetails";
			layoutControlGroupInsuranceDetails.Size = new System.Drawing.Size(796, 348);
			layoutControlGroupInsuranceDetails.Text = "Insurance Details";
			emptySpaceItem1.AllowHotTrack = false;
			emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
			emptySpaceItem1.Name = "emptySpaceItem1";
			emptySpaceItem1.Size = new System.Drawing.Size(796, 16);
			emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(827, 531);
			base.Controls.Add(layoutControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CustomerInsuranceDetailsForm";
			Text = "Credit Insurance Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceRating).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
			layoutControl1.ResumeLayout(false);
			layoutControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceStatus).EndInit();
			((System.ComponentModel.ISupportInitialize)Root).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
			((System.ComponentModel.ISupportInitialize)simpleSeparator2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroupInsuranceDetails).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
