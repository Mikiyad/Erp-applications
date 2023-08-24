using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyServiceRequestForm : Form, IForm
	{
		private PropertyServiceData currentData;

		private const string TABLENAME_CONST = "Property_Service_Request";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string voucherID;

		private ScreenAccessRight screenRight;

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

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonPhoto;

		private PropertyComboBox propertyComboBox1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private Panel panel1;

		private Panel panel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private PropertyUnitComboBox ComboBoxpropertyUnit;

		private PropertyComboBox comboBoxProperty;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private DateTimePicker dateTimeReportingDate;

		private TextBox textBoxUnit;

		private TextBox textBoxPropertyName;

		private Label label1;

		private GenericListComboBox ComboBoxServiceType;

		private TextBox textBoxRequestDetails;

		private Label label4;

		private Label label3;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerPlannedDate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private GenericListComboBox ComboBoxAssignedTo;

		private TextBox textBoxRemarks;

		private ComboBox comboBoxStatus;

		private ToolStripButton toolStripButtonInformation;

		private DateTimePicker dateTimePickerStatusDate;

		private MMLabel mmLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelDoc;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelVoucherNumber;

		private TextBox textBoxTenant;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private GenericListComboBox genericListComboBox1;

		private GroupBox groupBox4;

		private GroupBox groupBox3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private MMLabel mmLabel5;

		private DateTimePicker dateTimeConvenientDate;

		private MMLabel mmLabel4;

		private DateTimePicker dateTimeRequiredDate;

		private ToolStripButton toolStripButtonPrint;

		private customersFlatComboBox comboBoxTenant;

		private ListBox checkedListBoxFacilityType;

		private ListBox checkedListBoxServiceType;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonPreview;

		private SelectionStatusComboBox ComboBoxPriority;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelFacilityType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelServiceType;

		private Label label2;

		private Button buttonFacilityType;

		private Button buttonServiceType;

		public ScreenAreas ScreenArea => ScreenAreas.PropertyRental;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		public string VoucherID
		{
			get
			{
				return voucherID;
			}
			set
			{
				string text2 = voucherID = (textBoxVoucherNumber.Text = value);
			}
		}

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

		public PropertyServiceRequestForm()
		{
			InitializeComponent();
			AddEvents();
			dateTimeConvenientDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
			dateTimeRequiredDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
		}

		private void AddEvents()
		{
			base.Load += PropertyServiceDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxProperty.SelectedIndexChanged += comboBoxProperty_SelectedIndexChanged;
			ComboBoxpropertyUnit.SelectedIndexChanged += comboBoxPropertyUnit_SelectedIndexChanged;
		}

		private void comboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProperty.SelectedID != "")
			{
				ComboBoxpropertyUnit.FilterByPropertyId(comboBoxProperty.SelectedID);
			}
			else
			{
				ComboBoxpropertyUnit.FilterByPropertyId("");
			}
		}

		private void comboBoxPropertyUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ComboBoxpropertyUnit.SelectedID != "")
			{
				string selectedID = ComboBoxpropertyUnit.SelectedID;
				DateTime value = dateTimeReportingDate.Value;
				DataSet dataSet = new DataSet();
				dataSet = Factory.PropertyServiceSystem.GetTenantByUnit(selectedID, value);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					comboBoxTenant.SelectedID = dataSet.Tables[0].Rows[0]["CustomerID"].ToString();
				}
			}
			else
			{
				comboBoxTenant.SelectedID = "";
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PropertyServiceData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PropertyServiceDetailTable.Rows[0] : currentData.PropertyServiceDetailTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text.Trim();
				dataRow["ReportingDate"] = dateTimeReportingDate.Value;
				dataRow["ConvenientDatetime"] = dateTimeConvenientDate.Value;
				dataRow["RequiredDatetime"] = dateTimeRequiredDate.Value;
				if (comboBoxProperty.SelectedID != "")
				{
					dataRow["PropertyID"] = comboBoxProperty.SelectedID;
				}
				else
				{
					dataRow["PropertyID"] = DBNull.Value;
				}
				if (ComboBoxpropertyUnit.SelectedID != "")
				{
					dataRow["UnitID"] = ComboBoxpropertyUnit.SelectedID;
				}
				else
				{
					dataRow["UnitID"] = DBNull.Value;
				}
				if (comboBoxTenant.SelectedID != "")
				{
					dataRow["TenantID"] = comboBoxTenant.SelectedID;
				}
				else
				{
					dataRow["TenantID"] = DBNull.Value;
				}
				dataRow["RequestNotes"] = textBoxRequestDetails.Text;
				if (ComboBoxPriority.SelectedIndex != -1)
				{
					dataRow["PriorityStatus"] = ComboBoxPriority.SelectedIndex;
				}
				else
				{
					dataRow["PriorityStatus"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PropertyServiceDetailTable.Rows.Add(dataRow);
				}
				currentData.PropertyServiceFacilityDetailTable.Rows.Clear();
				foreach (object item in checkedListBoxFacilityType.Items)
				{
					NameValue nameValue = item as NameValue;
					DataRow dataRow2 = currentData.PropertyServiceFacilityDetailTable.NewRow();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text.Trim();
					dataRow2["FacilityID"] = nameValue.ID;
					dataRow2.EndEdit();
					currentData.PropertyServiceFacilityDetailTable.Rows.Add(dataRow2);
				}
				currentData.PropertyServiceTypeDetailTable.Rows.Clear();
				foreach (object item2 in checkedListBoxServiceType.Items)
				{
					NameValue nameValue2 = item2 as NameValue;
					DataRow dataRow3 = currentData.PropertyServiceTypeDetailTable.NewRow();
					dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow3["VoucherID"] = textBoxVoucherNumber.Text.Trim();
					dataRow3["ServiceTypeID"] = nameValue2.ID;
					dataRow3.EndEdit();
					currentData.PropertyServiceTypeDetailTable.Rows.Add(dataRow3);
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
					currentData = Factory.PropertyServiceSystem.GetPropertyServiceByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
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
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["Property_Service_Request"].Rows[0];
				comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				dateTimeReportingDate.Value = DateTime.Parse(dataRow["ReportingDate"].ToString());
				if (dataRow["PropertyID"] != DBNull.Value)
				{
					comboBoxProperty.SelectedID = dataRow["PropertyID"].ToString();
				}
				if (dataRow["UnitID"] != DBNull.Value)
				{
					ComboBoxpropertyUnit.SelectedID = dataRow["UnitID"].ToString();
				}
				if (dataRow["TenantID"] != DBNull.Value)
				{
					comboBoxTenant.SelectedID = dataRow["TenantID"].ToString();
				}
				dateTimeRequiredDate.Value = DateTime.Parse(dataRow["RequiredDatetime"].ToString());
				dateTimeConvenientDate.Value = DateTime.Parse(dataRow["ConvenientDatetime"].ToString());
				textBoxRequestDetails.Text = dataRow["RequestNotes"].ToString();
				if (dataRow["PriorityStatus"] != DBNull.Value)
				{
					ComboBoxPriority.SelectedIndex = int.Parse(dataRow["PriorityStatus"].ToString());
				}
				else
				{
					ComboBoxPriority.SelectedIndex = -1;
				}
				checkedListBoxFacilityType.Items.Clear();
				if (currentData.Tables.Contains("Property_ServiceFacility_Detail"))
				{
					foreach (DataRow row in currentData.Tables["Property_ServiceFacility_Detail"].Rows)
					{
						NameValue item = new NameValue(row["Number"].ToString(), row["Doc ID"].ToString());
						checkedListBoxFacilityType.Items.Add(item);
					}
				}
				checkedListBoxServiceType.Items.Clear();
				if (currentData.Tables.Contains("Property_ServiceType_Detail"))
				{
					foreach (DataRow row2 in currentData.Tables["Property_ServiceType_Detail"].Rows)
					{
						NameValue item2 = new NameValue(row2["Number"].ToString(), row2["Doc ID"].ToString());
						checkedListBoxServiceType.Items.Add(item2);
					}
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
				bool flag = (!isNewRecord) ? Factory.PropertyServiceSystem.UpdatePropertyService(currentData) : Factory.PropertyServiceSystem.CreatePropertyService(currentData);
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
			if (comboBoxProperty.Text == "")
			{
				ErrorHelper.InformationMessage("Please select  Property.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim().Length == 0 || textBoxVoucherNumber.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxStatus.SelectedIndex == -1)
			{
				ErrorHelper.InformationMessage("Please select  status.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Property_Service_Request", "VoucherID", textBoxVoucherNumber.Text.Trim()))
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
			textBoxVoucherNumber.Clear();
			dateTimeReportingDate.Value = DateTime.Now;
			comboBoxProperty.Clear();
			ComboBoxServiceType.Clear();
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			textBoxRequestDetails.Clear();
			ComboBoxAssignedTo.Clear();
			dateTimeConvenientDate.Value = DateTime.Now;
			dateTimeRequiredDate.Value = DateTime.Now;
			comboBoxStatus.SelectedIndex = 0;
			textBoxRequestDetails.Clear();
			checkedListBoxFacilityType.Items.Clear();
			checkedListBoxServiceType.Items.Clear();
			textBoxUnit.Clear();
			comboBoxTenant.Clear();
			ComboBoxpropertyUnit.Clear();
			ComboBoxpropertyUnit.FilterByPropertyId("");
			IsNewRecord = true;
			ComboBoxPriority.SelectedIndex = 0;
			formManager.ResetDirty();
			textBoxVoucherNumber.Focus();
		}

		private void AreaGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void AreaGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.PropertyServiceSystem.DeletePropertyService(textBoxVoucherNumber.Text);
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
			LoadData(DatabaseHelper.GetNextID("Property_Service_Request", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Property_Service_Request", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Property_Service_Request", "VoucherID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Property_Service_Request", "VoucherID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Property_Service_Request", "VoucherID", toolStripTextBoxFind.Text.Trim()))
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

		private void PropertyServiceDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxSysDoc.FilterByType(SysDocTypes.PropertyServiceRequest);
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.PropertyServiceRequestListFormObj);
		}

		private void linkLabelCustomerClass_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
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

		private void toolStripButtonPhoto_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProperty(comboBoxProperty.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyUnit(ComboBoxpropertyUnit.SelectedID);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void buttonServiceType_Click(object sender, EventArgs e)
		{
			GenericListTypes listType = GenericListTypes.PropertyServiceType;
			DataSet dataSet = new DataSet();
			List<string> list = new List<string>();
			dataSet = Factory.GenericListSystem.GetGenericListList(listType, islistType: true);
			foreach (NameValue item3 in checkedListBoxServiceType.Items)
			{
				if (item3.ID != null && item3.ID != "")
				{
					string item = item3.ID + item3.Name;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedDocuments = list;
			selectDocumentDialog.Text = "Select PropertyServiceType";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				checkedListBoxServiceType.Items.Clear();
				list = selectDocumentDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Doc ID"].Value.ToString();
					string text2 = selectedRow.Cells["Number"].Value.ToString();
					bool flag = false;
					foreach (NameValue item4 in checkedListBoxServiceType.Items)
					{
						if (item4.ID + item4.Name == text + text2)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						NameValue item2 = new NameValue(text2, text);
						checkedListBoxServiceType.Items.Add(item2);
					}
				}
			}
		}

		private void buttonFacilityType_Click(object sender, EventArgs e)
		{
			GenericListTypes listType = GenericListTypes.PropertyFacility;
			DataSet dataSet = new DataSet();
			List<string> list = new List<string>();
			dataSet = Factory.GenericListSystem.GetGenericListList(listType, islistType: true);
			foreach (NameValue item3 in checkedListBoxFacilityType.Items)
			{
				if (item3.ID != null && item3.ID != "")
				{
					string item = item3.ID + item3.Name;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedDocuments = list;
			selectDocumentDialog.Text = "Select PropertyFacilityType";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				checkedListBoxFacilityType.Items.Clear();
				list = selectDocumentDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Doc ID"].Value.ToString();
					string text2 = selectedRow.Cells["Number"].Value.ToString();
					bool flag = false;
					foreach (NameValue item4 in checkedListBoxFacilityType.Items)
					{
						if (item4.ID + item4.Name == text + text2)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						NameValue item2 = new NameValue(text2, text);
						checkedListBoxFacilityType.Items.Add(item2);
					}
				}
			}
		}

		private void ultraFormattedLinkLabelDoc_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PropertyServiceRequest);
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
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

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet propertyServiceRequestToPrint = Factory.PropertyServiceSystem.GetPropertyServiceRequestToPrint(selectedID, text);
					if (propertyServiceRequestToPrint == null || propertyServiceRequestToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(propertyServiceRequestToPrint, selectedID, printTemplateName, SysDocTypes.PropertyServiceRequest, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(propertyServiceRequestToPrint, selectedID, "Property Service Request", SysDocTypes.PropertyServiceRequest, isPrint, showPrintDialog);
						}
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

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void ultraFormattedLinkLabelFacilityType_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyFacilityType(comboBoxProperty.SelectedID);
		}

		private void ultraFormattedLinkLabelServiceType_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyServiceType(comboBoxProperty.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTenant(comboBoxTenant.SelectedID);
		}

		private void ultraFormattedLinkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyServiceRequestForm));
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
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPhoto = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			buttonServiceType = new System.Windows.Forms.Button();
			buttonFacilityType = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabelFacilityType = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabelServiceType = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ComboBoxPriority = new Micromind.DataControls.SelectionStatusComboBox();
			comboBoxTenant = new Micromind.DataControls.customersFlatComboBox();
			textBoxTenant = new System.Windows.Forms.TextBox();
			panel2 = new System.Windows.Forms.Panel();
			groupBox2 = new System.Windows.Forms.GroupBox();
			dateTimePickerStatusDate = new System.Windows.Forms.DateTimePicker();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerPlannedDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ComboBoxAssignedTo = new Micromind.DataControls.GenericListComboBox();
			textBoxRemarks = new System.Windows.Forms.TextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			dateTimeConvenientDate = new System.Windows.Forms.DateTimePicker();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimeRequiredDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			groupBox4 = new System.Windows.Forms.GroupBox();
			checkedListBoxFacilityType = new System.Windows.Forms.ListBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			checkedListBoxServiceType = new System.Windows.Forms.ListBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genericListComboBox1 = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabelDoc = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxRequestDetails = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			ComboBoxServiceType = new Micromind.DataControls.GenericListComboBox();
			textBoxUnit = new System.Windows.Forms.TextBox();
			textBoxPropertyName = new System.Windows.Forms.TextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimeReportingDate = new System.Windows.Forms.DateTimePicker();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ComboBoxpropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			formManager = new Micromind.DataControls.FormManager();
			propertyComboBox1 = new Micromind.DataControls.PropertyComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panel1.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTenant).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxAssignedTo).BeginInit();
			groupBox4.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)genericListComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxServiceType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxpropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)propertyComboBox1).BeginInit();
			SuspendLayout();
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
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator5,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonInformation,
				toolStripButtonPhoto
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(797, 31);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
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
			toolStripButtonPhoto.Image = Micromind.ClientUI.Properties.Resources.camera_icon;
			toolStripButtonPhoto.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPhoto.Name = "toolStripButtonPhoto";
			toolStripButtonPhoto.Size = new System.Drawing.Size(72, 28);
			toolStripButtonPhoto.Text = "Photos";
			toolStripButtonPhoto.Visible = false;
			toolStripButtonPhoto.Click += new System.EventHandler(toolStripButtonPhoto_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 510);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(797, 40);
			panelButtons.TabIndex = 11;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(797, 1);
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
			xpButton1.Location = new System.Drawing.Point(687, 8);
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
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(2, 21);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(754, 441);
			panel1.Controls.Add(groupBox1);
			panel1.Location = new System.Drawing.Point(9, 31);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(789, 472);
			panel1.TabIndex = 17;
			groupBox1.Controls.Add(buttonServiceType);
			groupBox1.Controls.Add(buttonFacilityType);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(ultraFormattedLinkLabelFacilityType);
			groupBox1.Controls.Add(ultraFormattedLinkLabelServiceType);
			groupBox1.Controls.Add(ComboBoxPriority);
			groupBox1.Controls.Add(comboBoxTenant);
			groupBox1.Controls.Add(panel2);
			groupBox1.Controls.Add(groupBox2);
			groupBox1.Controls.Add(mmLabel5);
			groupBox1.Controls.Add(dateTimeConvenientDate);
			groupBox1.Controls.Add(mmLabel4);
			groupBox1.Controls.Add(dateTimeRequiredDate);
			groupBox1.Controls.Add(ultraFormattedLinkLabel7);
			groupBox1.Controls.Add(groupBox4);
			groupBox1.Controls.Add(groupBox3);
			groupBox1.Controls.Add(textBoxTenant);
			groupBox1.Controls.Add(ultraFormattedLinkLabel6);
			groupBox1.Controls.Add(genericListComboBox1);
			groupBox1.Controls.Add(ultraFormattedLinkLabelDoc);
			groupBox1.Controls.Add(comboBoxSysDoc);
			groupBox1.Controls.Add(ultraFormattedLinkLabelVoucherNumber);
			groupBox1.Controls.Add(textBoxRequestDetails);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(ComboBoxServiceType);
			groupBox1.Controls.Add(textBoxUnit);
			groupBox1.Controls.Add(textBoxPropertyName);
			groupBox1.Controls.Add(mmLabel1);
			groupBox1.Controls.Add(dateTimeReportingDate);
			groupBox1.Controls.Add(linkLabelVoucherNumber);
			groupBox1.Controls.Add(textBoxVoucherNumber);
			groupBox1.Controls.Add(ultraFormattedLinkLabel1);
			groupBox1.Controls.Add(comboBoxProperty);
			groupBox1.Controls.Add(ultraFormattedLinkLabel4);
			groupBox1.Controls.Add(ComboBoxpropertyUnit);
			groupBox1.Location = new System.Drawing.Point(4, 2);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(782, 466);
			groupBox1.TabIndex = 158;
			groupBox1.TabStop = false;
			groupBox1.Text = "Service Request";
			buttonServiceType.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonServiceType.Location = new System.Drawing.Point(179, 131);
			buttonServiceType.Name = "buttonServiceType";
			buttonServiceType.Size = new System.Drawing.Size(23, 22);
			buttonServiceType.TabIndex = 371;
			buttonServiceType.UseVisualStyleBackColor = true;
			buttonServiceType.Click += new System.EventHandler(buttonServiceType_Click);
			buttonFacilityType.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonFacilityType.Location = new System.Drawing.Point(374, 131);
			buttonFacilityType.Name = "buttonFacilityType";
			buttonFacilityType.Size = new System.Drawing.Size(23, 22);
			buttonFacilityType.TabIndex = 370;
			buttonFacilityType.UseVisualStyleBackColor = true;
			buttonFacilityType.Click += new System.EventHandler(buttonFacilityType_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(6, 106);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(41, 13);
			label2.TabIndex = 188;
			label2.Text = "Priority:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelFacilityType.Appearance = appearance;
			ultraFormattedLinkLabelFacilityType.AutoSize = true;
			ultraFormattedLinkLabelFacilityType.Location = new System.Drawing.Point(284, 135);
			ultraFormattedLinkLabelFacilityType.Name = "ultraFormattedLinkLabelFacilityType";
			ultraFormattedLinkLabelFacilityType.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabelFacilityType.TabIndex = 187;
			ultraFormattedLinkLabelFacilityType.TabStop = true;
			ultraFormattedLinkLabelFacilityType.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelFacilityType.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelFacilityType.Value = "Facility Type:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelFacilityType.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabelFacilityType.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelFacilityType_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelServiceType.Appearance = appearance3;
			ultraFormattedLinkLabelServiceType.AutoSize = true;
			ultraFormattedLinkLabelServiceType.Location = new System.Drawing.Point(87, 135);
			ultraFormattedLinkLabelServiceType.Name = "ultraFormattedLinkLabelServiceType";
			ultraFormattedLinkLabelServiceType.Size = new System.Drawing.Size(78, 15);
			ultraFormattedLinkLabelServiceType.TabIndex = 186;
			ultraFormattedLinkLabelServiceType.TabStop = true;
			ultraFormattedLinkLabelServiceType.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelServiceType.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelServiceType.Value = "Service Type:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelServiceType.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabelServiceType.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelServiceType_LinkClicked);
			ComboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBoxPriority.FormattingEnabled = true;
			ComboBoxPriority.Items.AddRange(new object[3]
			{
				"Low",
				"High",
				"Medium"
			});
			ComboBoxPriority.Location = new System.Drawing.Point(87, 101);
			ComboBoxPriority.Name = "ComboBoxPriority";
			ComboBoxPriority.SelectedID = 0;
			ComboBoxPriority.Size = new System.Drawing.Size(139, 21);
			ComboBoxPriority.TabIndex = 9;
			comboBoxTenant.AlwaysInEditMode = true;
			comboBoxTenant.Assigned = false;
			comboBoxTenant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTenant.CustomReportFieldName = "";
			comboBoxTenant.CustomReportKey = "";
			comboBoxTenant.CustomReportValueType = 1;
			comboBoxTenant.DescriptionTextBox = textBoxTenant;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTenant.DisplayLayout.Appearance = appearance5;
			comboBoxTenant.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTenant.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTenant.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTenant.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxTenant.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTenant.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxTenant.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTenant.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTenant.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTenant.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxTenant.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTenant.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTenant.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTenant.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxTenant.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTenant.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTenant.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxTenant.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxTenant.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTenant.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxTenant.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxTenant.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTenant.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxTenant.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTenant.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTenant.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTenant.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTenant.Editable = true;
			comboBoxTenant.FilterString = "";
			comboBoxTenant.FilterSysDocID = "";
			comboBoxTenant.HasAll = false;
			comboBoxTenant.HasCustom = false;
			comboBoxTenant.IsDataLoaded = false;
			comboBoxTenant.Location = new System.Drawing.Point(87, 79);
			comboBoxTenant.MaxDropDownItems = 12;
			comboBoxTenant.Name = "comboBoxTenant";
			comboBoxTenant.ShowConsignmentOnly = false;
			comboBoxTenant.ShowInactive = false;
			comboBoxTenant.ShowLPOCustomersOnly = false;
			comboBoxTenant.ShowPROCustomersOnly = true;
			comboBoxTenant.ShowQuickAdd = true;
			comboBoxTenant.Size = new System.Drawing.Size(139, 20);
			comboBoxTenant.TabIndex = 7;
			comboBoxTenant.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxTenant.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTenant.Location = new System.Drawing.Point(231, 80);
			textBoxTenant.MaxLength = 64;
			textBoxTenant.Name = "textBoxTenant";
			textBoxTenant.ReadOnly = true;
			textBoxTenant.Size = new System.Drawing.Size(262, 20);
			textBoxTenant.TabIndex = 8;
			textBoxTenant.TabStop = false;
			panel2.Location = new System.Drawing.Point(555, 224);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(57, 29);
			panel2.TabIndex = 18;
			panel2.Visible = false;
			groupBox2.Controls.Add(dateTimePickerStatusDate);
			groupBox2.Controls.Add(mmLabel3);
			groupBox2.Controls.Add(comboBoxStatus);
			groupBox2.Controls.Add(label4);
			groupBox2.Controls.Add(label3);
			groupBox2.Controls.Add(mmLabel2);
			groupBox2.Controls.Add(dateTimePickerPlannedDate);
			groupBox2.Controls.Add(ultraFormattedLinkLabel2);
			groupBox2.Controls.Add(ComboBoxAssignedTo);
			groupBox2.Controls.Add(textBoxRemarks);
			groupBox2.Location = new System.Drawing.Point(513, 160);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(125, 58);
			groupBox2.TabIndex = 0;
			groupBox2.TabStop = false;
			groupBox2.Text = "Service Assignment";
			groupBox2.Visible = false;
			dateTimePickerStatusDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStatusDate.Location = new System.Drawing.Point(399, 43);
			dateTimePickerStatusDate.Name = "dateTimePickerStatusDate";
			dateTimePickerStatusDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerStatusDate.TabIndex = 171;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(317, 44);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(78, 13);
			mmLabel3.TabIndex = 170;
			mmLabel3.Text = "Status Date:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[5]
			{
				"Open",
				"Assigned",
				"Inprogress",
				"Hold",
				"Closed"
			});
			comboBoxStatus.Location = new System.Drawing.Point(84, 42);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(109, 21);
			comboBoxStatus.TabIndex = 169;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(27, 108);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(55, 13);
			label4.TabIndex = 168;
			label4.Text = "Remarks :";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(39, 48);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(43, 13);
			label3.TabIndex = 167;
			label3.Text = "Status :";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(307, 21);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(88, 13);
			mmLabel2.TabIndex = 168;
			mmLabel2.Text = "Planned Date:";
			dateTimePickerPlannedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerPlannedDate.Location = new System.Drawing.Point(400, 18);
			dateTimePickerPlannedDate.Name = "dateTimePickerPlannedDate";
			dateTimePickerPlannedDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerPlannedDate.TabIndex = 167;
			appearance17.FontData.BoldAsString = "True";
			appearance17.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance17;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(3, 23);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(78, 15);
			ultraFormattedLinkLabel2.TabIndex = 167;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Assigned To :";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance18;
			ComboBoxAssignedTo.Assigned = false;
			ComboBoxAssignedTo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxAssignedTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxAssignedTo.CustomReportFieldName = "";
			ComboBoxAssignedTo.CustomReportKey = "";
			ComboBoxAssignedTo.CustomReportValueType = 1;
			ComboBoxAssignedTo.DescriptionTextBox = null;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxAssignedTo.DisplayLayout.Appearance = appearance19;
			ComboBoxAssignedTo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxAssignedTo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance20.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxAssignedTo.DisplayLayout.GroupByBox.Appearance = appearance20;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxAssignedTo.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
			ComboBoxAssignedTo.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance22.BackColor2 = System.Drawing.SystemColors.Control;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxAssignedTo.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
			ComboBoxAssignedTo.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxAssignedTo.DisplayLayout.MaxRowScrollRegions = 1;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxAssignedTo.DisplayLayout.Override.ActiveCellAppearance = appearance23;
			appearance24.BackColor = System.Drawing.SystemColors.Highlight;
			appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxAssignedTo.DisplayLayout.Override.ActiveRowAppearance = appearance24;
			ComboBoxAssignedTo.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxAssignedTo.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxAssignedTo.DisplayLayout.Override.CardAreaAppearance = appearance25;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxAssignedTo.DisplayLayout.Override.CellAppearance = appearance26;
			ComboBoxAssignedTo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxAssignedTo.DisplayLayout.Override.CellPadding = 0;
			appearance27.BackColor = System.Drawing.SystemColors.Control;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxAssignedTo.DisplayLayout.Override.GroupByRowAppearance = appearance27;
			appearance28.TextHAlignAsString = "Left";
			ComboBoxAssignedTo.DisplayLayout.Override.HeaderAppearance = appearance28;
			ComboBoxAssignedTo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxAssignedTo.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			ComboBoxAssignedTo.DisplayLayout.Override.RowAppearance = appearance29;
			ComboBoxAssignedTo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxAssignedTo.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
			ComboBoxAssignedTo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxAssignedTo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxAssignedTo.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxAssignedTo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxAssignedTo.Editable = true;
			ComboBoxAssignedTo.FilterString = "";
			ComboBoxAssignedTo.GenericListType = Micromind.Common.Data.GenericListTypes.PropertyUnitType;
			ComboBoxAssignedTo.HasAllAccount = false;
			ComboBoxAssignedTo.HasCustom = false;
			ComboBoxAssignedTo.IsDataLoaded = false;
			ComboBoxAssignedTo.IsSingleColumn = false;
			ComboBoxAssignedTo.Location = new System.Drawing.Point(83, 19);
			ComboBoxAssignedTo.MaxDropDownItems = 12;
			ComboBoxAssignedTo.Name = "ComboBoxAssignedTo";
			ComboBoxAssignedTo.ShowInactiveItems = false;
			ComboBoxAssignedTo.ShowQuickAdd = true;
			ComboBoxAssignedTo.Size = new System.Drawing.Size(177, 20);
			ComboBoxAssignedTo.TabIndex = 167;
			ComboBoxAssignedTo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRemarks.Anchor = System.Windows.Forms.AnchorStyles.Left;
			textBoxRemarks.Location = new System.Drawing.Point(84, 10);
			textBoxRemarks.MaxLength = 1000;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(533, 100);
			textBoxRemarks.TabIndex = 167;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = true;
			mmLabel5.Location = new System.Drawing.Point(498, 106);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(106, 13);
			mmLabel5.TabIndex = 185;
			mmLabel5.Text = "Convenient Date:";
			dateTimeConvenientDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeConvenientDate.Location = new System.Drawing.Point(605, 103);
			dateTimeConvenientDate.Name = "dateTimeConvenientDate";
			dateTimeConvenientDate.Size = new System.Drawing.Size(128, 20);
			dateTimeConvenientDate.TabIndex = 11;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(228, 107);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(93, 13);
			mmLabel4.TabIndex = 183;
			mmLabel4.Text = "Required Date:";
			dateTimeRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRequiredDate.Location = new System.Drawing.Point(322, 103);
			dateTimeRequiredDate.Name = "dateTimeRequiredDate";
			dateTimeRequiredDate.Size = new System.Drawing.Size(171, 20);
			dateTimeRequiredDate.TabIndex = 10;
			appearance31.FontData.BoldAsString = "True";
			appearance31.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance31;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(610, 266);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(49, 15);
			ultraFormattedLinkLabel7.TabIndex = 181;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Priority:";
			ultraFormattedLinkLabel7.Visible = false;
			appearance32.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance32;
			groupBox4.Controls.Add(checkedListBoxFacilityType);
			groupBox4.Location = new System.Drawing.Point(284, 160);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new System.Drawing.Size(189, 166);
			groupBox4.TabIndex = 15;
			groupBox4.TabStop = false;
			groupBox4.Text = "Facility Type";
			checkedListBoxFacilityType.FormattingEnabled = true;
			checkedListBoxFacilityType.Location = new System.Drawing.Point(4, 14);
			checkedListBoxFacilityType.Name = "checkedListBoxFacilityType";
			checkedListBoxFacilityType.Size = new System.Drawing.Size(180, 147);
			checkedListBoxFacilityType.TabIndex = 0;
			groupBox3.Controls.Add(checkedListBoxServiceType);
			groupBox3.Location = new System.Drawing.Point(87, 160);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(189, 166);
			groupBox3.TabIndex = 14;
			groupBox3.TabStop = false;
			groupBox3.Text = "Service Type";
			checkedListBoxServiceType.FormattingEnabled = true;
			checkedListBoxServiceType.Location = new System.Drawing.Point(4, 14);
			checkedListBoxServiceType.Name = "checkedListBoxServiceType";
			checkedListBoxServiceType.Size = new System.Drawing.Size(180, 147);
			checkedListBoxServiceType.TabIndex = 0;
			appearance33.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance33;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(6, 84);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(42, 15);
			ultraFormattedLinkLabel6.TabIndex = 174;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Tenant:";
			appearance34.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance34;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			genericListComboBox1.Assigned = false;
			genericListComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBox1.CustomReportFieldName = "";
			genericListComboBox1.CustomReportKey = "";
			genericListComboBox1.CustomReportValueType = 1;
			genericListComboBox1.DescriptionTextBox = null;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBox1.DisplayLayout.Appearance = appearance35;
			genericListComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.GroupByBox.Appearance = appearance36;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance37;
			genericListComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance38.BackColor2 = System.Drawing.SystemColors.Control;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance38;
			genericListComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance39;
			appearance40.BackColor = System.Drawing.SystemColors.Highlight;
			appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance40;
			genericListComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance41;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			appearance42.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBox1.DisplayLayout.Override.CellAppearance = appearance42;
			genericListComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance43.BackColor = System.Drawing.SystemColors.Control;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance43;
			appearance44.TextHAlignAsString = "Left";
			genericListComboBox1.DisplayLayout.Override.HeaderAppearance = appearance44;
			genericListComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			genericListComboBox1.DisplayLayout.Override.RowAppearance = appearance45;
			genericListComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance46;
			genericListComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBox1.Editable = true;
			genericListComboBox1.FilterString = "";
			genericListComboBox1.GenericListType = Micromind.Common.Data.GenericListTypes.PropertyUnitType;
			genericListComboBox1.HasAllAccount = false;
			genericListComboBox1.HasCustom = false;
			genericListComboBox1.IsDataLoaded = false;
			genericListComboBox1.IsSingleColumn = false;
			genericListComboBox1.Location = new System.Drawing.Point(498, 81);
			genericListComboBox1.MaxDropDownItems = 12;
			genericListComboBox1.Name = "genericListComboBox1";
			genericListComboBox1.ShowInactiveItems = false;
			genericListComboBox1.ShowQuickAdd = true;
			genericListComboBox1.Size = new System.Drawing.Size(139, 20);
			genericListComboBox1.TabIndex = 172;
			genericListComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			genericListComboBox1.Visible = false;
			appearance47.FontData.BoldAsString = "True";
			appearance47.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelDoc.Appearance = appearance47;
			ultraFormattedLinkLabelDoc.AutoSize = true;
			ultraFormattedLinkLabelDoc.Location = new System.Drawing.Point(6, 18);
			ultraFormattedLinkLabelDoc.Name = "ultraFormattedLinkLabelDoc";
			ultraFormattedLinkLabelDoc.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabelDoc.TabIndex = 167;
			ultraFormattedLinkLabelDoc.TabStop = true;
			ultraFormattedLinkLabelDoc.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelDoc.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelDoc.Value = "Doc ID:";
			appearance48.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelDoc.VisitedLinkAppearance = appearance48;
			ultraFormattedLinkLabelDoc.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelDoc_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance49;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(87, 13);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(139, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance61.FontData.BoldAsString = "True";
			appearance61.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelVoucherNumber.Appearance = appearance61;
			ultraFormattedLinkLabelVoucherNumber.AutoSize = true;
			ultraFormattedLinkLabelVoucherNumber.Location = new System.Drawing.Point(231, 15);
			ultraFormattedLinkLabelVoucherNumber.Name = "ultraFormattedLinkLabelVoucherNumber";
			ultraFormattedLinkLabelVoucherNumber.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabelVoucherNumber.TabIndex = 169;
			ultraFormattedLinkLabelVoucherNumber.TabStop = true;
			ultraFormattedLinkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelVoucherNumber.Value = "Voucher Number:";
			appearance62.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelVoucherNumber.VisitedLinkAppearance = appearance62;
			ultraFormattedLinkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelVoucherNumber_LinkClicked);
			textBoxRequestDetails.Anchor = System.Windows.Forms.AnchorStyles.Left;
			textBoxRequestDetails.Location = new System.Drawing.Point(87, 332);
			textBoxRequestDetails.MaxLength = 255;
			textBoxRequestDetails.Multiline = true;
			textBoxRequestDetails.Name = "textBoxRequestDetails";
			textBoxRequestDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRequestDetails.Size = new System.Drawing.Size(646, 128);
			textBoxRequestDetails.TabIndex = 16;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 335);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(81, 13);
			label1.TabIndex = 165;
			label1.Text = "Request Notes:";
			ComboBoxServiceType.Assigned = false;
			ComboBoxServiceType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxServiceType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxServiceType.CustomReportFieldName = "";
			ComboBoxServiceType.CustomReportKey = "";
			ComboBoxServiceType.CustomReportValueType = 1;
			ComboBoxServiceType.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxServiceType.DisplayLayout.Appearance = appearance63;
			ComboBoxServiceType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxServiceType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxServiceType.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxServiceType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			ComboBoxServiceType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxServiceType.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			ComboBoxServiceType.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxServiceType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxServiceType.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxServiceType.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			ComboBoxServiceType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxServiceType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxServiceType.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxServiceType.DisplayLayout.Override.CellAppearance = appearance70;
			ComboBoxServiceType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxServiceType.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxServiceType.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			ComboBoxServiceType.DisplayLayout.Override.HeaderAppearance = appearance72;
			ComboBoxServiceType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxServiceType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			ComboBoxServiceType.DisplayLayout.Override.RowAppearance = appearance73;
			ComboBoxServiceType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxServiceType.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			ComboBoxServiceType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxServiceType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxServiceType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxServiceType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxServiceType.Editable = true;
			ComboBoxServiceType.FilterString = "";
			ComboBoxServiceType.GenericListType = Micromind.Common.Data.GenericListTypes.PropertyUnitType;
			ComboBoxServiceType.HasAllAccount = false;
			ComboBoxServiceType.HasCustom = false;
			ComboBoxServiceType.IsDataLoaded = false;
			ComboBoxServiceType.IsSingleColumn = false;
			ComboBoxServiceType.Location = new System.Drawing.Point(498, 60);
			ComboBoxServiceType.MaxDropDownItems = 12;
			ComboBoxServiceType.Name = "ComboBoxServiceType";
			ComboBoxServiceType.ShowInactiveItems = false;
			ComboBoxServiceType.ShowQuickAdd = true;
			ComboBoxServiceType.Size = new System.Drawing.Size(139, 20);
			ComboBoxServiceType.TabIndex = 164;
			ComboBoxServiceType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxServiceType.Visible = false;
			textBoxUnit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnit.Location = new System.Drawing.Point(231, 58);
			textBoxUnit.MaxLength = 64;
			textBoxUnit.Name = "textBoxUnit";
			textBoxUnit.ReadOnly = true;
			textBoxUnit.Size = new System.Drawing.Size(262, 20);
			textBoxUnit.TabIndex = 6;
			textBoxUnit.TabStop = false;
			textBoxPropertyName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPropertyName.Location = new System.Drawing.Point(231, 35);
			textBoxPropertyName.MaxLength = 64;
			textBoxPropertyName.Name = "textBoxPropertyName";
			textBoxPropertyName.ReadOnly = true;
			textBoxPropertyName.Size = new System.Drawing.Size(262, 20);
			textBoxPropertyName.TabIndex = 4;
			textBoxPropertyName.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(498, 16);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(97, 13);
			mmLabel1.TabIndex = 161;
			mmLabel1.Text = "Reporting Date:";
			dateTimeReportingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeReportingDate.Location = new System.Drawing.Point(605, 13);
			dateTimeReportingDate.Name = "dateTimeReportingDate";
			dateTimeReportingDate.Size = new System.Drawing.Size(128, 20);
			dateTimeReportingDate.TabIndex = 2;
			appearance75.FontData.BoldAsString = "True";
			appearance75.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance75;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(498, 43);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(77, 15);
			linkLabelVoucherNumber.TabIndex = 159;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Req Number:";
			linkLabelVoucherNumber.Visible = false;
			appearance76.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance76;
			textBoxVoucherNumber.Location = new System.Drawing.Point(336, 12);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(157, 20);
			textBoxVoucherNumber.TabIndex = 1;
			appearance77.FontData.BoldAsString = "True";
			appearance77.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance77;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(6, 41);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabel1.TabIndex = 156;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Property:";
			appearance78.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance78;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = textBoxPropertyName;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance79;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(87, 35);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(139, 20);
			comboBoxProperty.TabIndex = 3;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance91.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance91;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(6, 63);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(28, 15);
			ultraFormattedLinkLabel4.TabIndex = 157;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Unit:";
			appearance92.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance92;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			ComboBoxpropertyUnit.Assigned = false;
			ComboBoxpropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxpropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxpropertyUnit.CustomReportFieldName = "";
			ComboBoxpropertyUnit.CustomReportKey = "";
			ComboBoxpropertyUnit.CustomReportValueType = 1;
			ComboBoxpropertyUnit.DescriptionTextBox = textBoxUnit;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxpropertyUnit.DisplayLayout.Appearance = appearance93;
			ComboBoxpropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxpropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			ComboBoxpropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxpropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxpropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxpropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			ComboBoxpropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxpropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellAppearance = appearance100;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance102;
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			ComboBoxpropertyUnit.DisplayLayout.Override.RowAppearance = appearance103;
			ComboBoxpropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxpropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			ComboBoxpropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxpropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxpropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxpropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxpropertyUnit.Editable = true;
			ComboBoxpropertyUnit.FilterString = "";
			ComboBoxpropertyUnit.HasAllAccount = false;
			ComboBoxpropertyUnit.HasCustom = false;
			ComboBoxpropertyUnit.IsDataLoaded = false;
			ComboBoxpropertyUnit.Location = new System.Drawing.Point(87, 57);
			ComboBoxpropertyUnit.MaxDropDownItems = 12;
			ComboBoxpropertyUnit.Name = "ComboBoxpropertyUnit";
			ComboBoxpropertyUnit.ShowActiveOnly = false;
			ComboBoxpropertyUnit.ShowInactiveItems = false;
			ComboBoxpropertyUnit.ShowQuickAdd = true;
			ComboBoxpropertyUnit.Size = new System.Drawing.Size(139, 20);
			ComboBoxpropertyUnit.TabIndex = 5;
			ComboBoxpropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			propertyComboBox1.Assigned = false;
			propertyComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			propertyComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			propertyComboBox1.CustomReportFieldName = "";
			propertyComboBox1.CustomReportKey = "";
			propertyComboBox1.CustomReportValueType = 1;
			propertyComboBox1.DescriptionTextBox = null;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			propertyComboBox1.DisplayLayout.Appearance = appearance105;
			propertyComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			propertyComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			propertyComboBox1.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			propertyComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			propertyComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			propertyComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			propertyComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			propertyComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			propertyComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			propertyComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			propertyComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			propertyComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			propertyComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			propertyComboBox1.DisplayLayout.Override.CellAppearance = appearance112;
			propertyComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			propertyComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			propertyComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			propertyComboBox1.DisplayLayout.Override.HeaderAppearance = appearance114;
			propertyComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			propertyComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			propertyComboBox1.DisplayLayout.Override.RowAppearance = appearance115;
			propertyComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			propertyComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
			propertyComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			propertyComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			propertyComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			propertyComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			propertyComboBox1.Editable = true;
			propertyComboBox1.FilterString = "";
			propertyComboBox1.HasAllAccount = false;
			propertyComboBox1.HasCustom = false;
			propertyComboBox1.IsDataLoaded = false;
			propertyComboBox1.Location = new System.Drawing.Point(78, 19);
			propertyComboBox1.MaxDropDownItems = 12;
			propertyComboBox1.Name = "propertyComboBox1";
			propertyComboBox1.ShowInactiveItems = false;
			propertyComboBox1.ShowQuickAdd = true;
			propertyComboBox1.Size = new System.Drawing.Size(204, 20);
			propertyComboBox1.TabIndex = 4;
			propertyComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(797, 550);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PropertyServiceRequestForm";
			Text = "Service Request Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panel1.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTenant).EndInit();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxAssignedTo).EndInit();
			groupBox4.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)genericListComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxServiceType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxpropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)propertyComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
