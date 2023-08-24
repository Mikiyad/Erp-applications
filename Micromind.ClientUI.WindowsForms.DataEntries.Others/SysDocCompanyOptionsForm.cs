using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SysDocCompanyOptionsForm : Form, IForm
	{
		private string sysDocTypeID = "";

		private string sysDocID = "";

		private const string TABLENAME_CONST = "System_Document";

		private const string IDFIELD_CONST = "SysDocID";

		private string docName = "";

		public int LocationY = 20;

		public int LocationX = 70;

		private bool isNewRecord = true;

		private SysDocEntityTypes entityType = SysDocEntityTypes.CustomerClass;

		private SysDocTypes doctype = SysDocTypes.None;

		public bool isEnabled = true;

		private CompanyInformationData currentData;

		private CompanyOptionData companyOptionData;

		private DataSet data;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator8;

		private MMLabel labelCode;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl tabPageDetails;

		private CheckBox checkBoxSalesPriceValidationInSQ;

		private UltraTabPageControl tabPageUserDefined;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraTabPageControl ultraTabPageControl5;

		private UltraTabPageControl ultraTabPageControl6;

		private UltraTabPageControl ultraTabPageControl7;

		private MMLabel mmLabel58;

		private OptionsAllowComboBox comboBoxoptionsAllocation;

		private MMLabel mmLabel59;

		private UltraTabPageControl tabPagePOS;

		private UltraTabPageControl ultraTabPageProject;

		private UltraTabPageControl ultraTabPageControl8;

		private UltraTabPageControl ultraTabPageControl9;

		private UltraTabPageControl ultraTabPageControl10;

		private SysDocComboBox comboBoxSysDoc;

		private SysDocTypeComboBox comboBoxType;

		private MMLabel mmLabel4;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private CheckBox checkBoxDNZeroQty;

		private UltraTabPageControl ultraTabPageControl1;

		private CheckBox checkBoxDefaultLocation;

		private UltraTabPageControl tabPageControlProperty;

		private Label label1;

		private SysDocComboBox sysDocComboBoxRecurringInvoice;

		private CheckBox checkBoxActivateSOEditing;

		private CheckBox checkBoxCreateProjectwithSO;

		private CheckBox checkBoxSOApproval;

		private System.Windows.Forms.ToolTip toolTipInfo;

		private ToolStrip toolStrip2;

		private CheckBox checkBoxActivatePOEditing;

		private CheckBox checkBoxSalesOrderChangePrice;

		private CheckBox checkBoxActivateGRNEditing;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6014;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string SysDocTypeID
		{
			set
			{
				sysDocTypeID = value;
				comboBoxType.SelectedID = value.ToString();
				formManager.ResetDirty();
			}
		}

		public string SysDocID
		{
			set
			{
				sysDocID = value;
				comboBoxSysDoc.SelectedID = value.ToString();
				formManager.ResetDirty();
			}
		}

		public bool IsEnabled
		{
			set
			{
				isEnabled = value;
				comboBoxSysDoc.Enabled = isEnabled;
				comboBoxType.ReadOnly = !isEnabled;
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
					comboBoxType.Enabled = true;
					docName = "";
				}
				else
				{
					comboBoxType.Enabled = false;
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
			}
		}

		public SysDocCompanyOptionsForm()
		{
			InitializeComponent();
			AddEvents();
			base.AcceptButton = buttonSave;
		}

		private void AddEvents()
		{
			base.Load += SysDocDetailsForm_Load;
			comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
			sysDocComboBoxRecurringInvoice.SelectedIndexChanged += sysDocComboBoxRecurringInvoice_SelectedIndexChanged;
		}

		private void checkBoxPrintAfterSave_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			doctype = (SysDocTypes)int.Parse(comboBoxType.SelectedID);
			comboBoxSysDoc.LoadData();
			comboBoxSysDoc.FilterByType(doctype);
		}

		private void sysDocComboBoxRecurringInvoice_SelectedIndexChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private bool GetData()
		{
			try
			{
				switch (byte.Parse(comboBoxType.SelectedID))
				{
				case 22:
					GetDataSales();
					break;
				case 24:
					GetDataSales();
					break;
				case 23:
				case 52:
					GetDataSales();
					break;
				case 7:
					GetDataAccounts();
					break;
				case 101:
				case 102:
					GetDataProperty();
					break;
				case 25:
				case 51:
					GetDataSales();
					break;
				case 31:
				case 32:
				case 38:
				case 50:
					GetDataPurchase();
					break;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetDataSales()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable dataTable = data.Tables["Company_Option"].Clone();
				switch (byte.Parse(comboBoxType.SelectedID))
				{
				case 22:
					dataTable.Rows.Add(10201, checkBoxSalesPriceValidationInSQ.Checked, 1, comboBoxSysDoc.SelectedID, (byte)22);
					break;
				case 24:
					dataTable.Rows.Add(10202, checkBoxDNZeroQty.Checked, 1, comboBoxSysDoc.SelectedID, (byte)24);
					break;
				case 23:
					dataTable.Rows.Add(10203, checkBoxCreateProjectwithSO.Checked, 1, comboBoxSysDoc.SelectedID, (byte)23);
					dataTable.Rows.Add(10204, checkBoxActivateSOEditing.Checked, 1, comboBoxSysDoc.SelectedID, (byte)23);
					dataTable.Rows.Add(10205, checkBoxSOApproval.Checked, 1, comboBoxSysDoc.SelectedID, (byte)23);
					break;
				case 52:
					dataTable.Rows.Add(10204, checkBoxActivateSOEditing.Checked, 1, comboBoxSysDoc.SelectedID, (byte)52);
					dataTable.Rows.Add(10203, checkBoxCreateProjectwithSO.Checked, 1, comboBoxSysDoc.SelectedID, (byte)52);
					break;
				case 25:
					dataTable.Rows.Add(10206, checkBoxSalesOrderChangePrice.Checked, 1, comboBoxSysDoc.SelectedID, (byte)25);
					break;
				case 51:
					dataTable.Rows.Add(10206, checkBoxSalesOrderChangePrice.Checked, 1, comboBoxSysDoc.SelectedID, (byte)51);
					break;
				}
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(dataTable);
					this.companyOptionData = companyOptionData;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetDataPurchase()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable dataTable = data.Tables["Company_Option"].Clone();
				switch (byte.Parse(comboBoxType.SelectedID))
				{
				case 31:
					dataTable.Rows.Add(10301, checkBoxActivatePOEditing.Checked, 1, comboBoxSysDoc.SelectedID, (byte)31);
					break;
				case 38:
					dataTable.Rows.Add(10301, checkBoxActivatePOEditing.Checked, 1, comboBoxSysDoc.SelectedID, (byte)38);
					break;
				case 32:
					dataTable.Rows.Add(10302, checkBoxActivateGRNEditing.Checked, 1, comboBoxSysDoc.SelectedID, (byte)32);
					break;
				case 50:
					dataTable.Rows.Add(10302, checkBoxActivateGRNEditing.Checked, 1, comboBoxSysDoc.SelectedID, (byte)50);
					break;
				}
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(dataTable);
					this.companyOptionData = companyOptionData;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetDataAccounts()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable dataTable = data.Tables["Company_Option"].Clone();
				byte b = byte.Parse(comboBoxType.SelectedID);
				if (b == 7)
				{
					dataTable.Rows.Add(10101, checkBoxDefaultLocation.Checked, 1, comboBoxSysDoc.SelectedID, (byte)7);
				}
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(dataTable);
					this.companyOptionData = companyOptionData;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetDataProperty()
		{
			try
			{
				currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
				data = Factory.CompanyOptionSystem.GetSysDocCompanyOptionList();
				DataTable dataTable = data.Tables["Company_Option"].Clone();
				switch (byte.Parse(comboBoxType.SelectedID))
				{
				case 101:
					dataTable.Rows.Add(10701, sysDocComboBoxRecurringInvoice.SelectedID, 1, comboBoxSysDoc.SelectedID, (byte)101);
					break;
				case 102:
					dataTable.Rows.Add(10701, sysDocComboBoxRecurringInvoice.SelectedID, 1, comboBoxSysDoc.SelectedID, (byte)102);
					break;
				}
				if (this.companyOptionData == null)
				{
					CompanyOptionData companyOptionData = new CompanyOptionData();
					companyOptionData.Tables.Remove("Company_Option");
					companyOptionData.Tables.Add(dataTable);
					this.companyOptionData = companyOptionData;
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
					currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
					companyOptionData = (Factory.CompanyOptionSystem.GetCompanyOptionList() as CompanyOptionData);
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
				_ = currentData.Tables[0].Rows[0];
				switch (int.Parse(comboBoxType.SelectedID))
				{
				case 22:
					checkBoxSalesPriceValidationInSQ.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PriceValidationInSQ, 22, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 24:
					checkBoxDNZeroQty.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadZeroQuantityinDN, 24, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 23:
					checkBoxCreateProjectwithSO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowtoCreateProjectwithSOSysDoc, 23, comboBoxSysDoc.SelectedID, defaultValue: false);
					checkBoxActivateSOEditing.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateSOEditing, 23, comboBoxSysDoc.SelectedID, defaultValue: false);
					checkBoxSOApproval.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PriceLessaThanCostValidation, 23, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 52:
					checkBoxActivateSOEditing.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateSOEditing, 52, comboBoxSysDoc.SelectedID, defaultValue: false);
					checkBoxCreateProjectwithSO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowtoCreateProjectwithSOSysDoc, 52, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 7:
					checkBoxDefaultLocation.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultLocationAccounts, 7, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 101:
					sysDocComboBoxRecurringInvoice.SelectedID = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultRecurringInvoiceSysDocID, 101, comboBoxSysDoc.SelectedID, "");
					break;
				case 102:
					sysDocComboBoxRecurringInvoice.SelectedID = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultRecurringInvoiceSysDocID, 102, comboBoxSysDoc.SelectedID, "");
					break;
				case 31:
					checkBoxActivatePOEditing.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivatePOEditing, 31, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 25:
					checkBoxSalesOrderChangePrice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowToChangeSalesInvoicePrice, 25, comboBoxSysDoc.SelectedID, defaultValue: true);
					break;
				case 51:
					checkBoxSalesOrderChangePrice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowToChangeSalesInvoicePrice, 51, comboBoxSysDoc.SelectedID, defaultValue: true);
					break;
				case 38:
					checkBoxActivatePOEditing.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivatePOEditing, 38, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 32:
					checkBoxActivateGRNEditing.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateGRNEditing, 32, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				case 50:
					checkBoxActivateGRNEditing.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateGRNEditing, 50, comboBoxSysDoc.SelectedID, defaultValue: false);
					break;
				}
				LoadControls();
			}
		}

		private void LoadControls()
		{
			if (comboBoxType.SelectedType == SysDocTypes.SalesQuote)
			{
				checkBoxSalesPriceValidationInSQ.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.DeliveryNote)
			{
				checkBoxDNZeroQty.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.SalesOrder)
			{
				checkBoxCreateProjectwithSO.Enabled = true;
				checkBoxActivateSOEditing.Enabled = true;
				checkBoxSOApproval.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ExportSalesOrder)
			{
				checkBoxActivateSOEditing.Enabled = true;
				checkBoxCreateProjectwithSO.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ChequeDeposit)
			{
				checkBoxDefaultLocation.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.PurchaseOrder)
			{
				checkBoxActivatePOEditing.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.SalesInvoice)
			{
				checkBoxSalesOrderChangePrice.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ExportSalesInvoice)
			{
				checkBoxSalesOrderChangePrice.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ImportPurchaseOrder)
			{
				checkBoxActivatePOEditing.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.GoodsReceivedNote)
			{
				checkBoxActivateGRNEditing.Enabled = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ImportGoodsReceivedNote)
			{
				checkBoxActivateGRNEditing.Enabled = true;
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
				bool flag = Factory.CompanyInformationSystem.UpdateCompanyOptions(currentData);
				if (!string.IsNullOrEmpty(comboBoxType.SelectedID))
				{
					int.Parse(comboBoxType.SelectedID);
				}
				flag &= Factory.CompanyOptionSystem.CreateSysDocCompanyOption(companyOptionData, 1);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.SysDoc, needRefresh: true);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyPreferences.LoadCompanyPreferences();
					CompanyOptions.LoadCompanyOptions();
					CompanyOptions.LoadSysDocCompanyOptions();
					ErrorHelper.InformationMessage("Some changes may need to restart the application in order to take effect.");
					ClearForm();
					Close();
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
				Close();
			}
		}

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
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
			return true;
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (!(num.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetLastID("System_Document", "SysDocID", "SysDocType", num.ToString()));
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (!(num.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetFirstID("System_Document", "SysDocID", "SysDocType", num.ToString()));
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
				else if (Factory.DatabaseSystem.ExistFieldValue("System_Document", "SysDocID", toolStripTextBoxFind.Text.Trim()))
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

		private void SysDocDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					sysDocComboBoxRecurringInvoice.FilterByType(SysDocTypes.PropertyServiceInvoice);
					IsNewRecord = true;
					ClearForm();
					LoadData("1");
					LoadTabs((SysDocTypes)int.Parse(comboBoxType.SelectedID));
					LoadControls();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void GetControls()
		{
			new DataSet();
			DataSet controls = Factory.SystemDocumentSystem.GetControls();
			LocationY = 20;
			foreach (DataRow row in controls.Tables[0].Rows)
			{
				SysDocControlTypes controlType = (SysDocControlTypes)int.Parse(row["ControlTypeId"].ToString());
				CreateControl(controlType, row["ControlValue"].ToString());
				checked
				{
					LocationY += 30;
				}
			}
		}

		private void LoadTabs(SysDocTypes docType)
		{
			if (comboBoxType.SelectedType == SysDocTypes.SalesQuote || comboBoxType.SelectedType == SysDocTypes.DeliveryNote || comboBoxType.SelectedType == SysDocTypes.SalesInvoice || comboBoxType.SelectedType == SysDocTypes.ExportSalesInvoice)
			{
				ultraTabControl1.Tabs[0].Visible = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.SalesOrder)
			{
				ultraTabControl1.Tabs[0].Visible = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ChequeDeposit)
			{
				ultraTabControl1.Tabs[11].Visible = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.PropertyRental || comboBoxType.SelectedType == SysDocTypes.PropertyRenew)
			{
				ultraTabControl1.Tabs["tabPageControlProperty"].Visible = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ExportSalesOrder)
			{
				ultraTabControl1.Tabs[0].Visible = true;
			}
			if (comboBoxType.SelectedType == SysDocTypes.PurchaseOrder || comboBoxType.SelectedType == SysDocTypes.ImportPurchaseOrder || comboBoxType.SelectedType == SysDocTypes.GoodsReceivedNote || comboBoxType.SelectedType == SysDocTypes.ImportGoodsReceivedNote)
			{
				ultraTabControl1.Tabs[2].Visible = true;
			}
		}

		private void CreateControl(SysDocControlTypes ControlType, string value)
		{
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
			new FormHelper().ShowList(DataComboType.SysDoc);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
		}

		private void buttonEntitySelection_Click(object sender, EventArgs e)
		{
		}

		private void buttonItemClasses_Click(object sender, EventArgs e)
		{
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
		}

		private void buttonAccountClasses_Click(object sender, EventArgs e)
		{
		}

		private void buttonUsers_Click(object sender, EventArgs e)
		{
		}

		private void checkBoxAllowmultiprint_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void buttonAttachements_Click(object sender, EventArgs e)
		{
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.FreightChargesFormObj);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.SysDoc);
		}

		private void checkBoxPrintAfterSave_CheckedChanged_1(object sender, EventArgs e)
		{
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraTabControl1_SelectedTabChanged_1(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraTabControl1_SelectedTabChanged_2(object sender, SelectedTabChangedEventArgs e)
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab13 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.SysDocCompanyOptionsForm));
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxSalesOrderChangePrice = new System.Windows.Forms.CheckBox();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			checkBoxSOApproval = new System.Windows.Forms.CheckBox();
			checkBoxActivateSOEditing = new System.Windows.Forms.CheckBox();
			checkBoxCreateProjectwithSO = new System.Windows.Forms.CheckBox();
			checkBoxDNZeroQty = new System.Windows.Forms.CheckBox();
			checkBoxSalesPriceValidationInSQ = new System.Windows.Forms.CheckBox();
			ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			comboBoxoptionsAllocation = new Micromind.DataControls.OptionsAllowComboBox();
			mmLabel59 = new Micromind.UISupport.MMLabel();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxActivateGRNEditing = new System.Windows.Forms.CheckBox();
			checkBoxActivatePOEditing = new System.Windows.Forms.CheckBox();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			tabPagePOS = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageProject = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl8 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl9 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl10 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxDefaultLocation = new System.Windows.Forms.CheckBox();
			tabPageControlProperty = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			sysDocComboBoxRecurringInvoice = new Micromind.DataControls.SysDocComboBox();
			label1 = new System.Windows.Forms.Label();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			toolTipInfo = new System.Windows.Forms.ToolTip();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxType = new Micromind.DataControls.SysDocTypeComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			tabPageDetails.SuspendLayout();
			ultraTabPageControl7.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			tabPageControlProperty.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)sysDocComboBoxRecurringInvoice).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxType).BeginInit();
			SuspendLayout();
			tabPageDetails.AutoScroll = true;
			tabPageDetails.Controls.Add(checkBoxSalesOrderChangePrice);
			tabPageDetails.Controls.Add(toolStrip2);
			tabPageDetails.Controls.Add(checkBoxSOApproval);
			tabPageDetails.Controls.Add(checkBoxActivateSOEditing);
			tabPageDetails.Controls.Add(checkBoxCreateProjectwithSO);
			tabPageDetails.Controls.Add(checkBoxDNZeroQty);
			tabPageDetails.Controls.Add(checkBoxSalesPriceValidationInSQ);
			tabPageDetails.Location = new System.Drawing.Point(1, 20);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(690, 406);
			checkBoxSalesOrderChangePrice.AutoSize = true;
			checkBoxSalesOrderChangePrice.Enabled = false;
			checkBoxSalesOrderChangePrice.Location = new System.Drawing.Point(23, 141);
			checkBoxSalesOrderChangePrice.Name = "checkBoxSalesOrderChangePrice";
			checkBoxSalesOrderChangePrice.Size = new System.Drawing.Size(277, 17);
			checkBoxSalesOrderChangePrice.TabIndex = 125;
			checkBoxSalesOrderChangePrice.Text = "Allow to change price when created from Sales Order";
			checkBoxSalesOrderChangePrice.UseVisualStyleBackColor = true;
			toolStrip2.Location = new System.Drawing.Point(0, 0);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(690, 25);
			toolStrip2.TabIndex = 13;
			toolStrip2.Text = "toolStrip2";
			checkBoxSOApproval.AutoSize = true;
			checkBoxSOApproval.Enabled = false;
			checkBoxSOApproval.Location = new System.Drawing.Point(23, 119);
			checkBoxSOApproval.Name = "checkBoxSOApproval";
			checkBoxSOApproval.Size = new System.Drawing.Size(208, 17);
			checkBoxSOApproval.TabIndex = 12;
			checkBoxSOApproval.Text = "Enable Price Less than Cost Validation";
			checkBoxSOApproval.UseVisualStyleBackColor = true;
			checkBoxActivateSOEditing.AutoSize = true;
			checkBoxActivateSOEditing.Enabled = false;
			checkBoxActivateSOEditing.Location = new System.Drawing.Point(23, 96);
			checkBoxActivateSOEditing.Name = "checkBoxActivateSOEditing";
			checkBoxActivateSOEditing.Size = new System.Drawing.Size(189, 17);
			checkBoxActivateSOEditing.TabIndex = 11;
			checkBoxActivateSOEditing.Text = "Allow to edit after Order Processed";
			toolTipInfo.SetToolTip(checkBoxActivateSOEditing, "Allow to edit after Order Processed");
			checkBoxActivateSOEditing.UseVisualStyleBackColor = true;
			checkBoxCreateProjectwithSO.AutoSize = true;
			checkBoxCreateProjectwithSO.Enabled = false;
			checkBoxCreateProjectwithSO.Location = new System.Drawing.Point(23, 75);
			checkBoxCreateProjectwithSO.Name = "checkBoxCreateProjectwithSO";
			checkBoxCreateProjectwithSO.Size = new System.Drawing.Size(133, 17);
			checkBoxCreateProjectwithSO.TabIndex = 10;
			checkBoxCreateProjectwithSO.Text = "Create Project with SO";
			toolTipInfo.SetToolTip(checkBoxCreateProjectwithSO, "Does automatically create Project with Order");
			checkBoxCreateProjectwithSO.UseVisualStyleBackColor = true;
			checkBoxDNZeroQty.AutoSize = true;
			checkBoxDNZeroQty.Enabled = false;
			checkBoxDNZeroQty.Location = new System.Drawing.Point(23, 54);
			checkBoxDNZeroQty.Name = "checkBoxDNZeroQty";
			checkBoxDNZeroQty.Size = new System.Drawing.Size(145, 17);
			checkBoxDNZeroQty.TabIndex = 9;
			checkBoxDNZeroQty.Text = "Load zero Quantity in DN";
			checkBoxDNZeroQty.UseVisualStyleBackColor = true;
			checkBoxSalesPriceValidationInSQ.Enabled = false;
			checkBoxSalesPriceValidationInSQ.Location = new System.Drawing.Point(23, 26);
			checkBoxSalesPriceValidationInSQ.Name = "checkBoxSalesPriceValidationInSQ";
			checkBoxSalesPriceValidationInSQ.Size = new System.Drawing.Size(237, 24);
			checkBoxSalesPriceValidationInSQ.TabIndex = 5;
			checkBoxSalesPriceValidationInSQ.Text = "Sales price less than cost validation in SQ";
			checkBoxSalesPriceValidationInSQ.UseVisualStyleBackColor = true;
			ultraTabPageControl7.Controls.Add(mmLabel58);
			ultraTabPageControl7.Controls.Add(comboBoxoptionsAllocation);
			ultraTabPageControl7.Controls.Add(mmLabel59);
			ultraTabPageControl7.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl7.Name = "ultraTabPageControl7";
			ultraTabPageControl7.Size = new System.Drawing.Size(690, 406);
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(24, 376);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(41, 13);
			mmLabel58.TabIndex = 23;
			mmLabel58.Text = "Action:";
			mmLabel58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			comboBoxoptionsAllocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxoptionsAllocation.FormattingEnabled = true;
			comboBoxoptionsAllocation.HasPasswordItem = false;
			comboBoxoptionsAllocation.Items.AddRange(new object[2]
			{
				"Allow",
				"Don't Allow "
			});
			comboBoxoptionsAllocation.Location = new System.Drawing.Point(70, 373);
			comboBoxoptionsAllocation.Name = "comboBoxoptionsAllocation";
			comboBoxoptionsAllocation.Size = new System.Drawing.Size(119, 21);
			comboBoxoptionsAllocation.TabIndex = 22;
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(14, 357);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(114, 13);
			mmLabel59.TabIndex = 24;
			mmLabel59.Text = "To Remove Allocation:";
			mmLabel59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			tabPageUserDefined.AutoScroll = true;
			tabPageUserDefined.Controls.Add(checkBoxActivateGRNEditing);
			tabPageUserDefined.Controls.Add(checkBoxActivatePOEditing);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(690, 406);
			checkBoxActivateGRNEditing.AutoSize = true;
			checkBoxActivateGRNEditing.Enabled = false;
			checkBoxActivateGRNEditing.Location = new System.Drawing.Point(9, 48);
			checkBoxActivateGRNEditing.Name = "checkBoxActivateGRNEditing";
			checkBoxActivateGRNEditing.Size = new System.Drawing.Size(187, 17);
			checkBoxActivateGRNEditing.TabIndex = 13;
			checkBoxActivateGRNEditing.Text = "Allow to edit after GRN Processed";
			toolTipInfo.SetToolTip(checkBoxActivateGRNEditing, "Allow to edit after Order Processed");
			checkBoxActivateGRNEditing.UseVisualStyleBackColor = true;
			checkBoxActivatePOEditing.AutoSize = true;
			checkBoxActivatePOEditing.Enabled = false;
			checkBoxActivatePOEditing.Location = new System.Drawing.Point(9, 27);
			checkBoxActivatePOEditing.Name = "checkBoxActivatePOEditing";
			checkBoxActivatePOEditing.Size = new System.Drawing.Size(189, 17);
			checkBoxActivatePOEditing.TabIndex = 12;
			checkBoxActivatePOEditing.Text = "Allow to edit after Order Processed";
			toolTipInfo.SetToolTip(checkBoxActivatePOEditing, "Allow to edit after Order Processed");
			checkBoxActivatePOEditing.UseVisualStyleBackColor = true;
			ultraTabPageControl3.AutoScroll = true;
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(690, 406);
			tabPagePOS.Location = new System.Drawing.Point(-10000, -10000);
			tabPagePOS.Name = "tabPagePOS";
			tabPagePOS.Size = new System.Drawing.Size(690, 406);
			ultraTabPageProject.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageProject.Name = "ultraTabPageProject";
			ultraTabPageProject.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl8.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl8.Name = "ultraTabPageControl8";
			ultraTabPageControl8.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl9.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl9.Name = "ultraTabPageControl9";
			ultraTabPageControl9.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl10.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl10.Name = "ultraTabPageControl10";
			ultraTabPageControl10.Size = new System.Drawing.Size(690, 406);
			ultraTabPageControl1.Controls.Add(checkBoxDefaultLocation);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(690, 406);
			checkBoxDefaultLocation.Enabled = false;
			checkBoxDefaultLocation.Location = new System.Drawing.Point(7, 16);
			checkBoxDefaultLocation.Name = "checkBoxDefaultLocation";
			checkBoxDefaultLocation.Size = new System.Drawing.Size(237, 26);
			checkBoxDefaultLocation.TabIndex = 6;
			checkBoxDefaultLocation.Text = "Default Location On Accounts";
			checkBoxDefaultLocation.UseVisualStyleBackColor = true;
			tabPageControlProperty.Controls.Add(sysDocComboBoxRecurringInvoice);
			tabPageControlProperty.Controls.Add(label1);
			tabPageControlProperty.Location = new System.Drawing.Point(-10000, -10000);
			tabPageControlProperty.Name = "tabPageControlProperty";
			tabPageControlProperty.Size = new System.Drawing.Size(690, 406);
			sysDocComboBoxRecurringInvoice.Assigned = false;
			sysDocComboBoxRecurringInvoice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			sysDocComboBoxRecurringInvoice.CustomReportFieldName = "";
			sysDocComboBoxRecurringInvoice.CustomReportKey = "";
			sysDocComboBoxRecurringInvoice.CustomReportValueType = 1;
			sysDocComboBoxRecurringInvoice.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Appearance = appearance;
			sysDocComboBoxRecurringInvoice.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			sysDocComboBoxRecurringInvoice.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			sysDocComboBoxRecurringInvoice.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			sysDocComboBoxRecurringInvoice.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			sysDocComboBoxRecurringInvoice.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			sysDocComboBoxRecurringInvoice.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			sysDocComboBoxRecurringInvoice.DisplayLayout.MaxColScrollRegions = 1;
			sysDocComboBoxRecurringInvoice.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.CellAppearance = appearance8;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.HeaderAppearance = appearance10;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.RowAppearance = appearance11;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			sysDocComboBoxRecurringInvoice.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			sysDocComboBoxRecurringInvoice.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			sysDocComboBoxRecurringInvoice.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			sysDocComboBoxRecurringInvoice.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			sysDocComboBoxRecurringInvoice.DivisionID = "";
			sysDocComboBoxRecurringInvoice.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			sysDocComboBoxRecurringInvoice.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			sysDocComboBoxRecurringInvoice.Editable = true;
			sysDocComboBoxRecurringInvoice.ExcludeFromSecurity = false;
			sysDocComboBoxRecurringInvoice.FilterString = "";
			sysDocComboBoxRecurringInvoice.HasAllAccount = false;
			sysDocComboBoxRecurringInvoice.HasCustom = false;
			sysDocComboBoxRecurringInvoice.IsDataLoaded = false;
			sysDocComboBoxRecurringInvoice.Location = new System.Drawing.Point(186, 21);
			sysDocComboBoxRecurringInvoice.MaxDropDownItems = 12;
			sysDocComboBoxRecurringInvoice.Name = "sysDocComboBoxRecurringInvoice";
			sysDocComboBoxRecurringInvoice.ShowAll = false;
			sysDocComboBoxRecurringInvoice.ShowInactiveItems = false;
			sysDocComboBoxRecurringInvoice.ShowQuickAdd = true;
			sysDocComboBoxRecurringInvoice.Size = new System.Drawing.Size(128, 20);
			sysDocComboBoxRecurringInvoice.TabIndex = 4;
			sysDocComboBoxRecurringInvoice.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(16, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(163, 13);
			label1.TabIndex = 0;
			label1.Text = "SysDocID for Recurring Invoice :";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButton1,
				toolStripSeparator8,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(700, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "Open List";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 527);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(700, 40);
			panelButtons.TabIndex = 22;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(596, 9);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 16;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(496, 9);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 15;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(700, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance13;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(14, 36);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(95, 15);
			ultraFormattedLinkLabel2.TabIndex = 126;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Document Type:";
			ultraFormattedLinkLabel2.Visible = false;
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl5);
			ultraTabControl1.Controls.Add(ultraTabPageControl6);
			ultraTabControl1.Controls.Add(ultraTabPageControl7);
			ultraTabControl1.Controls.Add(tabPagePOS);
			ultraTabControl1.Controls.Add(ultraTabPageProject);
			ultraTabControl1.Controls.Add(ultraTabPageControl8);
			ultraTabControl1.Controls.Add(ultraTabPageControl9);
			ultraTabControl1.Controls.Add(ultraTabPageControl10);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(tabPageControlProperty);
			ultraTabControl1.Location = new System.Drawing.Point(4, 94);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl1.Size = new System.Drawing.Size(692, 427);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 123;
			ultraTab.TabPage = tabPageDetails;
			ultraTab.Text = "C&ustomers && Sales";
			ultraTab.Visible = false;
			ultraTab2.TabPage = ultraTabPageControl7;
			ultraTab2.Text = "Receivables && &Collection";
			ultraTab2.Visible = false;
			ultraTab3.TabPage = tabPageUserDefined;
			ultraTab3.Text = "&Vendors && Purchasing";
			ultraTab3.Visible = false;
			ultraTab4.TabPage = ultraTabPageControl3;
			ultraTab4.Text = "&Inventory";
			ultraTab4.Visible = false;
			ultraTab5.TabPage = ultraTabPageControl6;
			ultraTab5.Text = "&HR && Admin";
			ultraTab5.Visible = false;
			ultraTab6.TabPage = ultraTabPageControl5;
			ultraTab6.Text = "&Reports";
			ultraTab6.Visible = false;
			ultraTab7.TabPage = tabPagePOS;
			ultraTab7.Text = "PO&S";
			ultraTab7.Visible = false;
			ultraTab8.TabPage = ultraTabPageProject;
			ultraTab8.Text = "&Project";
			ultraTab8.Visible = false;
			ultraTab9.TabPage = ultraTabPageControl8;
			ultraTab9.Text = "CR&M";
			ultraTab9.Visible = false;
			ultraTab10.TabPage = ultraTabPageControl9;
			ultraTab10.Text = "General";
			ultraTab10.Visible = false;
			ultraTab11.TabPage = ultraTabPageControl10;
			ultraTab11.Text = "Legal";
			ultraTab11.Visible = false;
			ultraTab12.TabPage = ultraTabPageControl1;
			ultraTab12.Text = "Accounts";
			ultraTab12.Visible = false;
			ultraTab13.Key = "tabPageControlProperty";
			ultraTab13.TabPage = tabPageControlProperty;
			ultraTab13.Text = "Property";
			ultraTab13.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[13]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8,
				ultraTab9,
				ultraTab10,
				ultraTab11,
				ultraTab12,
				ultraTab13
			});
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged_2);
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(690, 406);
			toolTipInfo.IsBalloon = true;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(11, 56);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(85, 13);
			labelCode.TabIndex = 124;
			labelCode.Text = "Document ID:";
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance15;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(125, 52);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(98, 20);
			comboBoxSysDoc.TabIndex = 122;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxType.Assigned = false;
			comboBoxType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxType.CustomReportFieldName = "";
			comboBoxType.CustomReportKey = "";
			comboBoxType.CustomReportValueType = 1;
			comboBoxType.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxType.DisplayLayout.Appearance = appearance27;
			comboBoxType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxType.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxType.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxType.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxType.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxType.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxType.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxType.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxType.Editable = true;
			comboBoxType.FilterString = "";
			comboBoxType.HasAllAccount = false;
			comboBoxType.HasCustom = false;
			comboBoxType.IsDataLoaded = false;
			comboBoxType.Location = new System.Drawing.Point(125, 26);
			comboBoxType.MaxDropDownItems = 12;
			comboBoxType.Name = "comboBoxType";
			comboBoxType.ShowInactiveItems = false;
			comboBoxType.ShowQuickAdd = true;
			comboBoxType.Size = new System.Drawing.Size(175, 20);
			comboBoxType.TabIndex = 120;
			comboBoxType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(11, 26);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(100, 13);
			mmLabel4.TabIndex = 121;
			mmLabel4.Text = "Document Type:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
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
			base.ClientSize = new System.Drawing.Size(700, 567);
			base.Controls.Add(labelCode);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(comboBoxType);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "SysDocCompanyOptionsForm";
			Text = "System Document Options";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(SysDocDetailsForm_Load);
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			ultraTabPageControl7.ResumeLayout(false);
			ultraTabPageControl7.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			tabPageUserDefined.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			tabPageControlProperty.ResumeLayout(false);
			tabPageControlProperty.PerformLayout();
			((System.ComponentModel.ISupportInitialize)sysDocComboBoxRecurringInvoice).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxType).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
