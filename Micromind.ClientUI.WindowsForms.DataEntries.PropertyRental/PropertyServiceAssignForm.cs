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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyServiceAssignForm : Form, IForm
	{
		private PropertyServiceData currentData;

		private const string TABLENAME_CONST = "Property_Service_Assign";

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

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonPhoto;

		private PropertyComboBox propertyComboBox1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private Panel panel1;

		private Label label4;

		private Label label3;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerPlannedDate;

		private TextBox textBoxRemarks;

		private ComboBox comboBoxStatus;

		private ToolStripButton toolStripButtonInformation;

		private DateTimePicker dateTimePickerStatusDate;

		private MMLabel mmLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelDoc;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelVoucherNumber;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonPreview;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl tabPageGeneral;

		private Panel panelDetails;

		private Panel panel2;

		private UltraTabPageControl tabPageDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelServiceProvider;

		private MMTextBox textBoxServiceProvider;

		private ServiceProviderComboBox comboBoxServiceProvider;

		private FormManager formManager;

		private TextBox textBoxServiceRequestVoucherID;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelServiceRequest;

		private XPButton buttonSelectDocument;

		private Label label1;

		private AmountTextBox textBoxAmount;

		private SelectionStatusComboBox ComboBoxPriority;

		private customersFlatComboBox comboBoxTenant;

		private TextBox textBoxTenant;

		private MMLabel mmLabel5;

		private MMLabel mmLabel4;

		private DateTimePicker dateTimeRequiredDate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private GroupBox groupBox4;

		private ListBox checkedListBoxFacilityType;

		private GroupBox groupBox3;

		private ListBox checkedListBoxServiceType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelTenant;

		private TextBox textBoxUnit;

		private TextBox textBoxPropertyName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelProperty;

		private PropertyComboBox comboBoxProperty;

		private PropertyUnitComboBox ComboBoxpropertyUnit;

		private DateTimePicker dateTimeConvenientDate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelUnit;

		private TextBox textBoxVoucherNumber;

		private TextBox textBoxServiceRequestSysDocID;

		private Label label2;

		private TextBox textBoxRequestDetails;

		private Label label5;

		private MMLabel mmLabel1;

		private DateTimePicker dateTimeReportingDate;

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
					textBoxVoucherNumber.Enabled = true;
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

		public PropertyServiceAssignForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += PropertyServiceDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PropertyServiceData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PropertyServiceAssignDetailTable.Rows[0] : currentData.PropertyServiceAssignDetailTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text.Trim();
				dataRow["PlannedDate"] = dateTimePickerPlannedDate.Value;
				dataRow["StatusDate"] = dateTimePickerStatusDate.Value;
				if (comboBoxStatus.SelectedIndex != -1)
				{
					dataRow["Status"] = comboBoxStatus.SelectedIndex;
				}
				else
				{
					dataRow["Status"] = DBNull.Value;
				}
				if (comboBoxServiceProvider.SelectedID != "")
				{
					dataRow["ServiceProviderID"] = comboBoxServiceProvider.SelectedID;
				}
				else
				{
					dataRow["ServiceProviderID"] = DBNull.Value;
				}
				dataRow["Remarks"] = textBoxRemarks.Text;
				dataRow["Amount"] = textBoxAmount.Text;
				dataRow["SourceSysDocID"] = textBoxServiceRequestSysDocID.Text;
				dataRow["SourceVoucherID"] = textBoxServiceRequestVoucherID.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PropertyServiceAssignDetailTable.Rows.Add(dataRow);
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
					currentData = Factory.PropertyServiceSystem.GetPropertyServiceAssignByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[3].Rows.Count == 0)
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[3].Rows.Count == 0)
			{
				return;
			}
			ClearForm();
			DataRow dataRow = currentData.Tables["Property_Service_Assign"].Rows[0];
			comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
			textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
			if (dataRow["ServiceProviderID"] != DBNull.Value)
			{
				comboBoxServiceProvider.SelectedID = dataRow["ServiceProviderID"].ToString();
			}
			dateTimePickerStatusDate.Value = DateTime.Parse(dataRow["StatusDate"].ToString());
			dateTimePickerPlannedDate.Value = DateTime.Parse(dataRow["PlannedDate"].ToString());
			textBoxRemarks.Text = dataRow["Remarks"].ToString();
			textBoxAmount.Text = dataRow["Amount"].ToString();
			if (dataRow["Status"] != DBNull.Value)
			{
				comboBoxStatus.SelectedIndex = int.Parse(dataRow["Status"].ToString());
			}
			else
			{
				comboBoxStatus.SelectedIndex = -1;
			}
			textBoxServiceRequestSysDocID.Text = dataRow["SourceSysDocID"].ToString();
			textBoxServiceRequestVoucherID.Text = dataRow["SourceVoucherID"].ToString();
			DataSet propertyServiceByID = Factory.PropertyServiceSystem.GetPropertyServiceByID(textBoxServiceRequestVoucherID.Text);
			if (propertyServiceByID != null && propertyServiceByID.Tables.Count != 0 && propertyServiceByID.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow2 = propertyServiceByID.Tables["Property_Service_Request"].Rows[0];
				if (dataRow2["PropertyID"] != DBNull.Value)
				{
					comboBoxProperty.SelectedID = dataRow2["PropertyID"].ToString();
				}
				if (dataRow2["UnitID"] != DBNull.Value)
				{
					ComboBoxpropertyUnit.SelectedID = dataRow2["UnitID"].ToString();
				}
				if (dataRow2["TenantID"] != DBNull.Value)
				{
					comboBoxTenant.SelectedID = dataRow2["TenantID"].ToString();
				}
				if (dataRow2["PriorityStatus"] != DBNull.Value)
				{
					ComboBoxPriority.SelectedIndex = int.Parse(dataRow2["PriorityStatus"].ToString());
				}
				else
				{
					ComboBoxPriority.SelectedIndex = -1;
				}
				dateTimeRequiredDate.Value = DateTime.Parse(dataRow2["RequiredDatetime"].ToString());
				dateTimeConvenientDate.Value = DateTime.Parse(dataRow2["ConvenientDatetime"].ToString());
				checkedListBoxFacilityType.Items.Clear();
				if (propertyServiceByID.Tables.Contains("Property_ServiceFacility_Detail"))
				{
					foreach (DataRow row in propertyServiceByID.Tables["Property_ServiceFacility_Detail"].Rows)
					{
						NameValue item = new NameValue(row["Doc ID"].ToString(), row["VoucherID"].ToString());
						checkedListBoxFacilityType.Items.Add(item);
					}
				}
				checkedListBoxServiceType.Items.Clear();
				if (propertyServiceByID.Tables.Contains("Property_ServiceType_Detail"))
				{
					foreach (DataRow row2 in propertyServiceByID.Tables["Property_ServiceType_Detail"].Rows)
					{
						NameValue item2 = new NameValue(row2["Doc ID"].ToString(), row2["VoucherID"].ToString());
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
				bool flag = (!isNewRecord) ? Factory.PropertyServiceSystem.UpdatePropertyServiceAssign(currentData) : Factory.PropertyServiceSystem.CreatePropertyServiceAssign(currentData);
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
			if (comboBoxServiceProvider.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Property_Service_Assign", "VoucherID", textBoxVoucherNumber.Text.Trim()))
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
			dateTimePickerStatusDate.Value = DateTime.Now;
			dateTimePickerPlannedDate.Value = DateTime.Now;
			comboBoxServiceProvider.Clear();
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			textBoxAmount.Clear();
			comboBoxProperty.Clear();
			textBoxPropertyName.Clear();
			comboBoxStatus.SelectedIndex = -1;
			textBoxRemarks.Clear();
			checkedListBoxFacilityType.Items.Clear();
			checkedListBoxServiceType.Items.Clear();
			ComboBoxPriority.SelectedIndex = -1;
			textBoxUnit.Clear();
			comboBoxTenant.Clear();
			ComboBoxpropertyUnit.Clear();
			textBoxServiceRequestSysDocID.Clear();
			textBoxServiceRequestVoucherID.Clear();
			IsNewRecord = true;
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
				return Factory.PropertyServiceSystem.DeletePropertyServiceAssign(textBoxVoucherNumber.Text);
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
			LoadData(DatabaseHelper.GetNextID("Property_Service_Assign", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Property_Service_Assign", "VoucherID", textBoxVoucherNumber.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Property_Service_Assign", "VoucherID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Property_Service_Assign", "VoucherID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Property_Service_Assign", "VoucherID", toolStripTextBoxFind.Text.Trim()))
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
				comboBoxSysDoc.FilterByType(SysDocTypes.PropertyServiceAssign);
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
			FormActivator.BringFormToFront(FormActivator.PropertyServiceAssignListFormObj);
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
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
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
		}

		private void buttonFacilityType_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabelDoc_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PropertyServiceAssign);
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
					DataSet propertyServiceAssignToPrint = Factory.PropertyServiceSystem.GetPropertyServiceAssignToPrint(selectedID, text);
					if (propertyServiceAssignToPrint == null || propertyServiceAssignToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(propertyServiceAssignToPrint, selectedID, printTemplateName, SysDocTypes.PropertyServiceAssign, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(propertyServiceAssignToPrint, selectedID, "Property Service Assign", SysDocTypes.PropertyServiceAssign, isPrint, showPrintDialog);
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

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			DataSet serviceRequestList = Factory.PropertyServiceSystem.GetServiceRequestList("");
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = serviceRequestList;
			selectDocumentDialog.Text = "Select Property Service Request";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				serviceRequestList = null;
				textBoxServiceRequestVoucherID.Text = "";
				textBoxServiceRequestSysDocID.Text = "";
				string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				serviceRequestList = Factory.PropertyServiceSystem.GetPropertyServiceByID(text2);
				DataRow dataRow = serviceRequestList.Tables["Property_Service_Request"].Rows[0];
				textBoxServiceRequestVoucherID.Text = text2;
				textBoxServiceRequestSysDocID.Text = text;
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
				if (dataRow["PriorityStatus"] != DBNull.Value)
				{
					ComboBoxPriority.SelectedIndex = int.Parse(dataRow["PriorityStatus"].ToString());
				}
				else
				{
					ComboBoxPriority.SelectedIndex = -1;
				}
				dateTimeReportingDate.Value = DateTime.Parse(dataRow["ReportingDate"].ToString());
				dateTimeRequiredDate.Value = DateTime.Parse(dataRow["RequiredDatetime"].ToString());
				dateTimeConvenientDate.Value = DateTime.Parse(dataRow["ConvenientDatetime"].ToString());
				textBoxRequestDetails.Text = dataRow["RequestNotes"].ToString();
				checkedListBoxFacilityType.Items.Clear();
				if (serviceRequestList.Tables.Contains("Property_ServiceFacility_Detail"))
				{
					foreach (DataRow row in serviceRequestList.Tables["Property_ServiceFacility_Detail"].Rows)
					{
						NameValue item = new NameValue(row["Number"].ToString(), row["Doc ID"].ToString());
						checkedListBoxFacilityType.Items.Add(item);
					}
				}
				checkedListBoxServiceType.Items.Clear();
				if (serviceRequestList.Tables.Contains("Property_ServiceType_Detail"))
				{
					foreach (DataRow row2 in serviceRequestList.Tables["Property_ServiceType_Detail"].Rows)
					{
						NameValue item2 = new NameValue(row2["Number"].ToString(), row2["Doc ID"].ToString());
						checkedListBoxServiceType.Items.Add(item2);
					}
				}
			}
		}

		private void ultraFormattedLinkLabelServiceProvider_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditServiceProvider(comboBoxServiceProvider.SelectedID);
		}

		private void ultraFormattedLinkLabelServiceRequest_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditServiceRequest(textBoxServiceRequestVoucherID.Text);
		}

		private void ultraFormattedLinkLabelProperty_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProperty(comboBoxProperty.SelectedID);
		}

		private void ultraFormattedLinkLabelUnit_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyUnit(ComboBoxpropertyUnit.SelectedID);
		}

		private void ultraFormattedLinkLabelTenant_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyServiceAssignForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelDetails = new System.Windows.Forms.Panel();
			panel2 = new System.Windows.Forms.Panel();
			textBoxServiceRequestSysDocID = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabelServiceRequest = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			textBoxServiceRequestVoucherID = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabelServiceProvider = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxServiceProvider = new Micromind.UISupport.MMTextBox();
			comboBoxServiceProvider = new Micromind.DataControls.ServiceProviderComboBox();
			textBoxRemarks = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			dateTimePickerStatusDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabelDoc = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerPlannedDate = new System.Windows.Forms.DateTimePicker();
			label3 = new System.Windows.Forms.Label();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimeReportingDate = new System.Windows.Forms.DateTimePicker();
			textBoxRequestDetails = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabelUnit = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimeConvenientDate = new System.Windows.Forms.DateTimePicker();
			ComboBoxPriority = new Micromind.DataControls.SelectionStatusComboBox();
			comboBoxTenant = new Micromind.DataControls.customersFlatComboBox();
			textBoxTenant = new System.Windows.Forms.TextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimeRequiredDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			groupBox4 = new System.Windows.Forms.GroupBox();
			checkedListBoxFacilityType = new System.Windows.Forms.ListBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			checkedListBoxServiceType = new System.Windows.Forms.ListBox();
			ultraFormattedLinkLabelTenant = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxUnit = new System.Windows.Forms.TextBox();
			textBoxPropertyName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabelProperty = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			ComboBoxpropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
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
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			propertyComboBox1 = new Micromind.DataControls.PropertyComboBox();
			tabPageGeneral.SuspendLayout();
			panelDetails.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTenant).BeginInit();
			groupBox4.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxpropertyUnit).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)propertyComboBox1).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(panelDetails);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(741, 456);
			panelDetails.Controls.Add(panel2);
			panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			panelDetails.Location = new System.Drawing.Point(0, 0);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(741, 456);
			panelDetails.TabIndex = 0;
			panel2.Controls.Add(textBoxServiceRequestSysDocID);
			panel2.Controls.Add(textBoxVoucherNumber);
			panel2.Controls.Add(label1);
			panel2.Controls.Add(textBoxAmount);
			panel2.Controls.Add(ultraFormattedLinkLabelServiceRequest);
			panel2.Controls.Add(buttonSelectDocument);
			panel2.Controls.Add(textBoxServiceRequestVoucherID);
			panel2.Controls.Add(ultraFormattedLinkLabelServiceProvider);
			panel2.Controls.Add(textBoxServiceProvider);
			panel2.Controls.Add(comboBoxServiceProvider);
			panel2.Controls.Add(textBoxRemarks);
			panel2.Controls.Add(label4);
			panel2.Controls.Add(dateTimePickerStatusDate);
			panel2.Controls.Add(ultraFormattedLinkLabelVoucherNumber);
			panel2.Controls.Add(mmLabel3);
			panel2.Controls.Add(ultraFormattedLinkLabelDoc);
			panel2.Controls.Add(comboBoxStatus);
			panel2.Controls.Add(mmLabel2);
			panel2.Controls.Add(dateTimePickerPlannedDate);
			panel2.Controls.Add(label3);
			panel2.Controls.Add(comboBoxSysDoc);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(0, 0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(741, 456);
			panel2.TabIndex = 0;
			textBoxServiceRequestSysDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServiceRequestSysDocID.Location = new System.Drawing.Point(119, 30);
			textBoxServiceRequestSysDocID.MaxLength = 64;
			textBoxServiceRequestSysDocID.Name = "textBoxServiceRequestSysDocID";
			textBoxServiceRequestSysDocID.ReadOnly = true;
			textBoxServiceRequestSysDocID.Size = new System.Drawing.Size(138, 20);
			textBoxServiceRequestSysDocID.TabIndex = 2;
			textBoxServiceRequestSysDocID.TabStop = false;
			textBoxVoucherNumber.Location = new System.Drawing.Point(369, 8);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(128, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(17, 82);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(46, 13);
			label1.TabIndex = 180;
			label1.Text = "Amount:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.White;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(118, 74);
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
			textBoxAmount.Size = new System.Drawing.Size(114, 20);
			textBoxAmount.TabIndex = 7;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelServiceRequest.Appearance = appearance;
			ultraFormattedLinkLabelServiceRequest.AutoSize = true;
			ultraFormattedLinkLabelServiceRequest.Location = new System.Drawing.Point(17, 34);
			ultraFormattedLinkLabelServiceRequest.Name = "ultraFormattedLinkLabelServiceRequest";
			ultraFormattedLinkLabelServiceRequest.Size = new System.Drawing.Size(97, 15);
			ultraFormattedLinkLabelServiceRequest.TabIndex = 178;
			ultraFormattedLinkLabelServiceRequest.TabStop = true;
			ultraFormattedLinkLabelServiceRequest.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelServiceRequest.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelServiceRequest.Value = "Service Request:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelServiceRequest.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabelServiceRequest.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelServiceRequest_LinkClicked);
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(612, 29);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 4;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			textBoxServiceRequestVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServiceRequestVoucherID.Location = new System.Drawing.Point(260, 30);
			textBoxServiceRequestVoucherID.MaxLength = 64;
			textBoxServiceRequestVoucherID.Name = "textBoxServiceRequestVoucherID";
			textBoxServiceRequestVoucherID.ReadOnly = true;
			textBoxServiceRequestVoucherID.Size = new System.Drawing.Size(351, 20);
			textBoxServiceRequestVoucherID.TabIndex = 3;
			textBoxServiceRequestVoucherID.TabStop = false;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelServiceProvider.Appearance = appearance3;
			ultraFormattedLinkLabelServiceProvider.AutoSize = true;
			ultraFormattedLinkLabelServiceProvider.Location = new System.Drawing.Point(17, 58);
			ultraFormattedLinkLabelServiceProvider.Name = "ultraFormattedLinkLabelServiceProvider";
			ultraFormattedLinkLabelServiceProvider.Size = new System.Drawing.Size(99, 15);
			ultraFormattedLinkLabelServiceProvider.TabIndex = 174;
			ultraFormattedLinkLabelServiceProvider.TabStop = true;
			ultraFormattedLinkLabelServiceProvider.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelServiceProvider.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelServiceProvider.Value = "Service Provider:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelServiceProvider.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabelServiceProvider.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelServiceProvider_LinkClicked);
			textBoxServiceProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServiceProvider.CustomReportFieldName = "";
			textBoxServiceProvider.CustomReportKey = "";
			textBoxServiceProvider.CustomReportValueType = 1;
			textBoxServiceProvider.IsComboTextBox = false;
			textBoxServiceProvider.IsModified = false;
			textBoxServiceProvider.Location = new System.Drawing.Point(296, 52);
			textBoxServiceProvider.MaxLength = 64;
			textBoxServiceProvider.Name = "textBoxServiceProvider";
			textBoxServiceProvider.ReadOnly = true;
			textBoxServiceProvider.Size = new System.Drawing.Size(252, 20);
			textBoxServiceProvider.TabIndex = 6;
			textBoxServiceProvider.TabStop = false;
			comboBoxServiceProvider.Assigned = false;
			comboBoxServiceProvider.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxServiceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxServiceProvider.CustomReportFieldName = "";
			comboBoxServiceProvider.CustomReportKey = "";
			comboBoxServiceProvider.CustomReportValueType = 1;
			comboBoxServiceProvider.DescriptionTextBox = textBoxServiceProvider;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxServiceProvider.DisplayLayout.Appearance = appearance5;
			comboBoxServiceProvider.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxServiceProvider.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxServiceProvider.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxServiceProvider.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxServiceProvider.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxServiceProvider.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxServiceProvider.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxServiceProvider.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxServiceProvider.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxServiceProvider.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxServiceProvider.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxServiceProvider.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxServiceProvider.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxServiceProvider.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxServiceProvider.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxServiceProvider.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxServiceProvider.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxServiceProvider.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxServiceProvider.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxServiceProvider.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
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
			comboBoxServiceProvider.Location = new System.Drawing.Point(118, 52);
			comboBoxServiceProvider.MaxDropDownItems = 12;
			comboBoxServiceProvider.Name = "comboBoxServiceProvider";
			comboBoxServiceProvider.ShowConsignmentOnly = false;
			comboBoxServiceProvider.ShowQuickAdd = true;
			comboBoxServiceProvider.Size = new System.Drawing.Size(176, 20);
			comboBoxServiceProvider.TabIndex = 5;
			comboBoxServiceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRemarks.Location = new System.Drawing.Point(118, 120);
			textBoxRemarks.MaxLength = 1000;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(537, 100);
			textBoxRemarks.TabIndex = 11;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(17, 126);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(55, 13);
			label4.TabIndex = 168;
			label4.Text = "Remarks :";
			dateTimePickerStatusDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStatusDate.Location = new System.Drawing.Point(533, 74);
			dateTimePickerStatusDate.Name = "dateTimePickerStatusDate";
			dateTimePickerStatusDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerStatusDate.TabIndex = 9;
			appearance17.FontData.BoldAsString = "True";
			appearance17.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelVoucherNumber.Appearance = appearance17;
			ultraFormattedLinkLabelVoucherNumber.AutoSize = true;
			ultraFormattedLinkLabelVoucherNumber.Location = new System.Drawing.Point(264, 10);
			ultraFormattedLinkLabelVoucherNumber.Name = "ultraFormattedLinkLabelVoucherNumber";
			ultraFormattedLinkLabelVoucherNumber.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabelVoucherNumber.TabIndex = 169;
			ultraFormattedLinkLabelVoucherNumber.TabStop = true;
			ultraFormattedLinkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelVoucherNumber.Value = "Voucher Number:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelVoucherNumber.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelVoucherNumber_LinkClicked);
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(451, 77);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(78, 13);
			mmLabel3.TabIndex = 170;
			mmLabel3.Text = "Status Date:";
			appearance19.FontData.BoldAsString = "True";
			appearance19.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelDoc.Appearance = appearance19;
			ultraFormattedLinkLabelDoc.AutoSize = true;
			ultraFormattedLinkLabelDoc.Location = new System.Drawing.Point(17, 10);
			ultraFormattedLinkLabelDoc.Name = "ultraFormattedLinkLabelDoc";
			ultraFormattedLinkLabelDoc.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabelDoc.TabIndex = 167;
			ultraFormattedLinkLabelDoc.TabStop = true;
			ultraFormattedLinkLabelDoc.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelDoc.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelDoc.Value = "Doc ID:";
			appearance20.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelDoc.VisitedLinkAppearance = appearance20;
			ultraFormattedLinkLabelDoc.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelDoc_LinkClicked);
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[6]
			{
				"Open",
				"Assigned",
				"Inprogress",
				"Hold",
				"Closed",
				"waiting for approval"
			});
			comboBoxStatus.Location = new System.Drawing.Point(118, 96);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(109, 21);
			comboBoxStatus.TabIndex = 10;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(238, 77);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(88, 13);
			mmLabel2.TabIndex = 168;
			mmLabel2.Text = "Planned Date:";
			dateTimePickerPlannedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerPlannedDate.Location = new System.Drawing.Point(331, 74);
			dateTimePickerPlannedDate.Name = "dateTimePickerPlannedDate";
			dateTimePickerPlannedDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerPlannedDate.TabIndex = 8;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(17, 104);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(43, 13);
			label3.TabIndex = 167;
			label3.Text = "Status :";
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance21;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance22;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance24.BackColor2 = System.Drawing.SystemColors.Control;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance25;
			appearance26.BackColor = System.Drawing.SystemColors.Highlight;
			appearance26.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance26;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance27;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			appearance28.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance28;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance29.BackColor = System.Drawing.SystemColors.Control;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance29;
			appearance30.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance30;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance31;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance32;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(119, 8);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(139, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			tabPageDetails.Controls.Add(mmLabel1);
			tabPageDetails.Controls.Add(dateTimeReportingDate);
			tabPageDetails.Controls.Add(textBoxRequestDetails);
			tabPageDetails.Controls.Add(label5);
			tabPageDetails.Controls.Add(label2);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabelUnit);
			tabPageDetails.Controls.Add(dateTimeConvenientDate);
			tabPageDetails.Controls.Add(ComboBoxPriority);
			tabPageDetails.Controls.Add(comboBoxTenant);
			tabPageDetails.Controls.Add(mmLabel5);
			tabPageDetails.Controls.Add(mmLabel4);
			tabPageDetails.Controls.Add(dateTimeRequiredDate);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel7);
			tabPageDetails.Controls.Add(groupBox4);
			tabPageDetails.Controls.Add(groupBox3);
			tabPageDetails.Controls.Add(textBoxTenant);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabelTenant);
			tabPageDetails.Controls.Add(textBoxUnit);
			tabPageDetails.Controls.Add(textBoxPropertyName);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabelProperty);
			tabPageDetails.Controls.Add(comboBoxProperty);
			tabPageDetails.Controls.Add(ComboBoxpropertyUnit);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(741, 456);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(477, 14);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(97, 13);
			mmLabel1.TabIndex = 211;
			mmLabel1.Text = "Reporting Date:";
			dateTimeReportingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeReportingDate.Location = new System.Drawing.Point(583, 10);
			dateTimeReportingDate.Name = "dateTimeReportingDate";
			dateTimeReportingDate.Size = new System.Drawing.Size(155, 20);
			dateTimeReportingDate.TabIndex = 210;
			textBoxRequestDetails.Anchor = System.Windows.Forms.AnchorStyles.Left;
			textBoxRequestDetails.Location = new System.Drawing.Point(83, 275);
			textBoxRequestDetails.MaxLength = 1000;
			textBoxRequestDetails.Multiline = true;
			textBoxRequestDetails.Name = "textBoxRequestDetails";
			textBoxRequestDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRequestDetails.Size = new System.Drawing.Size(646, 128);
			textBoxRequestDetails.TabIndex = 208;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(1, 278);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(81, 13);
			label5.TabIndex = 209;
			label5.Text = "Request Notes:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(8, 77);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(41, 13);
			label2.TabIndex = 207;
			label2.Text = "Priority:";
			appearance33.FontData.BoldAsString = "True";
			appearance33.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelUnit.Appearance = appearance33;
			ultraFormattedLinkLabelUnit.AutoSize = true;
			ultraFormattedLinkLabelUnit.Location = new System.Drawing.Point(8, 35);
			ultraFormattedLinkLabelUnit.Name = "ultraFormattedLinkLabelUnit";
			ultraFormattedLinkLabelUnit.Size = new System.Drawing.Size(31, 15);
			ultraFormattedLinkLabelUnit.TabIndex = 206;
			ultraFormattedLinkLabelUnit.TabStop = true;
			ultraFormattedLinkLabelUnit.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelUnit.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelUnit.Value = "Unit:";
			appearance34.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelUnit.VisitedLinkAppearance = appearance34;
			ultraFormattedLinkLabelUnit.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelUnit_LinkClicked);
			dateTimeConvenientDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeConvenientDate.Location = new System.Drawing.Point(583, 34);
			dateTimeConvenientDate.Name = "dateTimeConvenientDate";
			dateTimeConvenientDate.Size = new System.Drawing.Size(155, 20);
			dateTimeConvenientDate.TabIndex = 8;
			ComboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBoxPriority.FormattingEnabled = true;
			ComboBoxPriority.Items.AddRange(new object[3]
			{
				"Low",
				"High",
				"Medium"
			});
			ComboBoxPriority.Location = new System.Drawing.Point(82, 76);
			ComboBoxPriority.Name = "ComboBoxPriority";
			ComboBoxPriority.SelectedID = 0;
			ComboBoxPriority.Size = new System.Drawing.Size(139, 21);
			ComboBoxPriority.TabIndex = 6;
			comboBoxTenant.AlwaysInEditMode = true;
			comboBoxTenant.Assigned = false;
			comboBoxTenant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTenant.CustomReportFieldName = "";
			comboBoxTenant.CustomReportKey = "";
			comboBoxTenant.CustomReportValueType = 1;
			comboBoxTenant.DescriptionTextBox = textBoxTenant;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTenant.DisplayLayout.Appearance = appearance35;
			comboBoxTenant.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTenant.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTenant.DisplayLayout.GroupByBox.Appearance = appearance36;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTenant.DisplayLayout.GroupByBox.BandLabelAppearance = appearance37;
			comboBoxTenant.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance38.BackColor2 = System.Drawing.SystemColors.Control;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTenant.DisplayLayout.GroupByBox.PromptAppearance = appearance38;
			comboBoxTenant.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTenant.DisplayLayout.MaxRowScrollRegions = 1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTenant.DisplayLayout.Override.ActiveCellAppearance = appearance39;
			appearance40.BackColor = System.Drawing.SystemColors.Highlight;
			appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTenant.DisplayLayout.Override.ActiveRowAppearance = appearance40;
			comboBoxTenant.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTenant.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTenant.DisplayLayout.Override.CardAreaAppearance = appearance41;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			appearance42.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTenant.DisplayLayout.Override.CellAppearance = appearance42;
			comboBoxTenant.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTenant.DisplayLayout.Override.CellPadding = 0;
			appearance43.BackColor = System.Drawing.SystemColors.Control;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTenant.DisplayLayout.Override.GroupByRowAppearance = appearance43;
			appearance44.TextHAlignAsString = "Left";
			comboBoxTenant.DisplayLayout.Override.HeaderAppearance = appearance44;
			comboBoxTenant.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTenant.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			comboBoxTenant.DisplayLayout.Override.RowAppearance = appearance45;
			comboBoxTenant.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTenant.DisplayLayout.Override.TemplateAddRowAppearance = appearance46;
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
			comboBoxTenant.Location = new System.Drawing.Point(82, 54);
			comboBoxTenant.MaxDropDownItems = 12;
			comboBoxTenant.Name = "comboBoxTenant";
			comboBoxTenant.ShowConsignmentOnly = false;
			comboBoxTenant.ShowInactive = false;
			comboBoxTenant.ShowLPOCustomersOnly = false;
			comboBoxTenant.ShowPROCustomersOnly = true;
			comboBoxTenant.ShowQuickAdd = true;
			comboBoxTenant.Size = new System.Drawing.Size(138, 20);
			comboBoxTenant.TabIndex = 4;
			comboBoxTenant.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxTenant.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTenant.Location = new System.Drawing.Point(226, 54);
			textBoxTenant.MaxLength = 64;
			textBoxTenant.Name = "textBoxTenant";
			textBoxTenant.ReadOnly = true;
			textBoxTenant.Size = new System.Drawing.Size(251, 20);
			textBoxTenant.TabIndex = 5;
			textBoxTenant.TabStop = false;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = true;
			mmLabel5.Location = new System.Drawing.Point(478, 37);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(106, 13);
			mmLabel5.TabIndex = 204;
			mmLabel5.Text = "Convenient Date:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(477, 60);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(93, 13);
			mmLabel4.TabIndex = 203;
			mmLabel4.Text = "Required Date:";
			dateTimeRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRequiredDate.Location = new System.Drawing.Point(583, 58);
			dateTimeRequiredDate.Name = "dateTimeRequiredDate";
			dateTimeRequiredDate.Size = new System.Drawing.Size(155, 20);
			dateTimeRequiredDate.TabIndex = 7;
			appearance47.FontData.BoldAsString = "True";
			appearance47.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance47;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(535, 168);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(49, 15);
			ultraFormattedLinkLabel7.TabIndex = 202;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Priority:";
			ultraFormattedLinkLabel7.Visible = false;
			appearance48.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance48;
			groupBox4.Controls.Add(checkedListBoxFacilityType);
			groupBox4.Location = new System.Drawing.Point(277, 102);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new System.Drawing.Size(189, 166);
			groupBox4.TabIndex = 201;
			groupBox4.TabStop = false;
			groupBox4.Text = "Facility Type";
			checkedListBoxFacilityType.FormattingEnabled = true;
			checkedListBoxFacilityType.Location = new System.Drawing.Point(4, 14);
			checkedListBoxFacilityType.Name = "checkedListBoxFacilityType";
			checkedListBoxFacilityType.Size = new System.Drawing.Size(180, 147);
			checkedListBoxFacilityType.TabIndex = 0;
			groupBox3.Controls.Add(checkedListBoxServiceType);
			groupBox3.Location = new System.Drawing.Point(82, 102);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(189, 166);
			groupBox3.TabIndex = 200;
			groupBox3.TabStop = false;
			groupBox3.Text = "Service Type";
			checkedListBoxServiceType.FormattingEnabled = true;
			checkedListBoxServiceType.Location = new System.Drawing.Point(4, 14);
			checkedListBoxServiceType.Name = "checkedListBoxServiceType";
			checkedListBoxServiceType.Size = new System.Drawing.Size(180, 147);
			checkedListBoxServiceType.TabIndex = 0;
			appearance49.FontData.BoldAsString = "True";
			appearance49.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelTenant.Appearance = appearance49;
			ultraFormattedLinkLabelTenant.AutoSize = true;
			ultraFormattedLinkLabelTenant.Location = new System.Drawing.Point(8, 56);
			ultraFormattedLinkLabelTenant.Name = "ultraFormattedLinkLabelTenant";
			ultraFormattedLinkLabelTenant.Size = new System.Drawing.Size(47, 15);
			ultraFormattedLinkLabelTenant.TabIndex = 199;
			ultraFormattedLinkLabelTenant.TabStop = true;
			ultraFormattedLinkLabelTenant.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelTenant.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelTenant.Value = "Tenant:";
			appearance50.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelTenant.VisitedLinkAppearance = appearance50;
			ultraFormattedLinkLabelTenant.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelTenant_LinkClicked);
			textBoxUnit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnit.Location = new System.Drawing.Point(226, 32);
			textBoxUnit.MaxLength = 64;
			textBoxUnit.Name = "textBoxUnit";
			textBoxUnit.ReadOnly = true;
			textBoxUnit.Size = new System.Drawing.Size(251, 20);
			textBoxUnit.TabIndex = 3;
			textBoxUnit.TabStop = false;
			textBoxPropertyName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPropertyName.Location = new System.Drawing.Point(226, 10);
			textBoxPropertyName.MaxLength = 64;
			textBoxPropertyName.Name = "textBoxPropertyName";
			textBoxPropertyName.ReadOnly = true;
			textBoxPropertyName.Size = new System.Drawing.Size(251, 20);
			textBoxPropertyName.TabIndex = 1;
			textBoxPropertyName.TabStop = false;
			appearance51.FontData.BoldAsString = "True";
			appearance51.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelProperty.Appearance = appearance51;
			ultraFormattedLinkLabelProperty.AutoSize = true;
			ultraFormattedLinkLabelProperty.Location = new System.Drawing.Point(8, 14);
			ultraFormattedLinkLabelProperty.Name = "ultraFormattedLinkLabelProperty";
			ultraFormattedLinkLabelProperty.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabelProperty.TabIndex = 198;
			ultraFormattedLinkLabelProperty.TabStop = true;
			ultraFormattedLinkLabelProperty.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelProperty.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelProperty.Value = "Property:";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelProperty.VisitedLinkAppearance = appearance52;
			ultraFormattedLinkLabelProperty.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelProperty_LinkClicked);
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = textBoxPropertyName;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance53;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(82, 10);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(139, 20);
			comboBoxProperty.TabIndex = 0;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxpropertyUnit.Assigned = false;
			ComboBoxpropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxpropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxpropertyUnit.CustomReportFieldName = "";
			ComboBoxpropertyUnit.CustomReportKey = "";
			ComboBoxpropertyUnit.CustomReportValueType = 1;
			ComboBoxpropertyUnit.DescriptionTextBox = textBoxUnit;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxpropertyUnit.DisplayLayout.Appearance = appearance65;
			ComboBoxpropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxpropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			ComboBoxpropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxpropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxpropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxpropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			ComboBoxpropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxpropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellAppearance = appearance72;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance74;
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			ComboBoxpropertyUnit.DisplayLayout.Override.RowAppearance = appearance75;
			ComboBoxpropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxpropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			ComboBoxpropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxpropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxpropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxpropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxpropertyUnit.Editable = true;
			ComboBoxpropertyUnit.FilterString = "";
			ComboBoxpropertyUnit.HasAllAccount = false;
			ComboBoxpropertyUnit.HasCustom = false;
			ComboBoxpropertyUnit.IsDataLoaded = false;
			ComboBoxpropertyUnit.Location = new System.Drawing.Point(82, 32);
			ComboBoxpropertyUnit.MaxDropDownItems = 12;
			ComboBoxpropertyUnit.Name = "ComboBoxpropertyUnit";
			ComboBoxpropertyUnit.ShowActiveOnly = false;
			ComboBoxpropertyUnit.ShowInactiveItems = false;
			ComboBoxpropertyUnit.ShowQuickAdd = true;
			ComboBoxpropertyUnit.Size = new System.Drawing.Size(139, 20);
			ComboBoxpropertyUnit.TabIndex = 2;
			ComboBoxpropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			toolStrip1.Size = new System.Drawing.Size(765, 31);
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
			panelButtons.Size = new System.Drawing.Size(765, 40);
			panelButtons.TabIndex = 11;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(765, 1);
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
			buttonDelete.TabIndex = 1;
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
			xpButton1.Location = new System.Drawing.Point(655, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 2;
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
			buttonNew.TabIndex = 0;
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
			buttonSave.TabIndex = 4;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(2, 21);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(754, 441);
			panel1.Controls.Add(ultraTabControl1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(20, 31);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(745, 479);
			panel1.TabIndex = 17;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 0);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl1.Size = new System.Drawing.Size(745, 479);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 159;
			appearance77.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance77;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Service Assignment";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Service Request";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(741, 456);
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
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			propertyComboBox1.DisplayLayout.Appearance = appearance78;
			propertyComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			propertyComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance79.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			propertyComboBox1.DisplayLayout.GroupByBox.Appearance = appearance79;
			appearance80.ForeColor = System.Drawing.SystemColors.GrayText;
			propertyComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance80;
			propertyComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance81.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance81.BackColor2 = System.Drawing.SystemColors.Control;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			propertyComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance81;
			propertyComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			propertyComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.ForeColor = System.Drawing.SystemColors.ControlText;
			propertyComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance82;
			appearance83.BackColor = System.Drawing.SystemColors.Highlight;
			appearance83.ForeColor = System.Drawing.SystemColors.HighlightText;
			propertyComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance83;
			propertyComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			propertyComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			propertyComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance84;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			appearance85.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			propertyComboBox1.DisplayLayout.Override.CellAppearance = appearance85;
			propertyComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			propertyComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance86.BackColor = System.Drawing.SystemColors.Control;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			propertyComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance86;
			appearance87.TextHAlignAsString = "Left";
			propertyComboBox1.DisplayLayout.Override.HeaderAppearance = appearance87;
			propertyComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			propertyComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			propertyComboBox1.DisplayLayout.Override.RowAppearance = appearance88;
			propertyComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance89.BackColor = System.Drawing.SystemColors.ControlLight;
			propertyComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance89;
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
			base.ClientSize = new System.Drawing.Size(765, 550);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PropertyServiceAssignForm";
			Text = "Service Assign Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxServiceProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTenant).EndInit();
			groupBox4.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxpropertyUnit).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)propertyComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
