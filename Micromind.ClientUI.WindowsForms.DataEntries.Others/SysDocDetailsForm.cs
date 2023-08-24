using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.Reports.CustomDashboards;
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
	public class SysDocDetailsForm : Form, IForm
	{
		private SysDocTypes sysDocTypeID = SysDocTypes.None;

		private SystemDocumentData currentData;

		private const string TABLENAME_CONST = "System_Document";

		private const string IDFIELD_CONST = "SysDocID";

		private string docName = "";

		public int LocationY = 20;

		public int LocationX = 70;

		private bool isNewRecord = true;

		private SysDocEntityTypes entityType = SysDocEntityTypes.CustomerClass;

		private ScreenAccessRight screenRight;

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

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private UltraNumericEditor textBoxNextNumbern;

		private MMTextBox textBoxPrefix;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private LocationComboBox comboBoxLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private CheckBox checkBoxPrintAfterSave;

		private SysDocTypeComboBox comboBoxType;

		private MMLabel mmLabel4;

		private UltraGroupBox groupBoxConsignOut;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private LocationComboBox comboBoxConsignOutLocation;

		private MMTextBox textBoxTemplateName;

		private MMLabel mmLabel7;

		private XPButton buttonSelectTemplatePath;

		private MMLabel mmLabel5;

		private CheckBox checkBoxAllowFOC;

		private RadioButton radioButtonPrint;

		private RadioButton radioButtonPreview;

		private Panel panelPrintOption;

		private XPButton buttonEntitySelection;

		private XPButton buttonItemClasses;

		private XPButton buttonAccounts;

		private XPButton buttonAccountClasses;

		private XPButton buttonUsers;

		private ToolStripButton toolStripButtonInformation;

		private CheckBox checkBoxBOLmandatory;

		private CheckBox checkBoxAllowmultiprint;

		private XPButton buttonAttachements;

		private XPButton buttonOpenList;

		private UltraFormattedLinkLabel LabelDocTypeLink;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator8;

		private CheckBox checkBoxPriceIncludeTax;

		private CheckBox checkBoxSupplierInvoiceNoMandatory;

		private CheckBox checkBoxActivateItemFilter;

		private UltraTabControl sysDocTabControl;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private Panel controlPanel;

		private XPButton buttonDocIDCompanyOption;

		private XPButton buttonSysDocCompanyOption;

		private MMLabel mmLabel6;

		private NumberTextBox textBoxNextNumber;

		private CompanyDivisionComboBox comboBoxCompanyDivision;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6014;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public SysDocTypes SysDocTypeID
		{
			set
			{
				sysDocTypeID = value;
				comboBoxType.SelectedID = value.ToString();
				formManager.ResetDirty();
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
					textBoxCode.ReadOnly = false;
					comboBoxType.Enabled = true;
					docName = "";
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
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
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
				XPButton xPButton = buttonEntitySelection;
				XPButton xPButton2 = buttonItemClasses;
				bool flag2 = buttonOpenList.Enabled = !IsNewRecord;
				bool enabled = xPButton2.Enabled = flag2;
				xPButton.Enabled = enabled;
			}
		}

		public SysDocDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			base.AcceptButton = buttonSave;
			buttonAccounts.Visible = true;
		}

		private void AddEvents()
		{
			base.Load += SysDocDetailsForm_Load;
			comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
			checkBoxPrintAfterSave.CheckedChanged += checkBoxPrintAfterSave_CheckedChanged;
		}

		private void checkBoxPrintAfterSave_CheckedChanged(object sender, EventArgs e)
		{
			panelPrintOption.Enabled = checkBoxPrintAfterSave.Checked;
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(comboBoxType.SelectedID) && docName == "")
			{
				textBoxName.Text = comboBoxType.SelectedName;
			}
			buttonAccountClasses.Visible = false;
			buttonUsers.Visible = false;
			if (comboBoxType.SelectedType == SysDocTypes.ConsignOut || comboBoxType.SelectedType == SysDocTypes.ConsignOutReturn || comboBoxType.SelectedType == SysDocTypes.GarmentRental)
			{
				groupBoxConsignOut.Visible = true;
			}
			else
			{
				groupBoxConsignOut.Visible = false;
			}
			if (comboBoxType.SelectedType == SysDocTypes.SalesInvoice)
			{
				checkBoxAllowFOC.Visible = true;
			}
			else
			{
				checkBoxAllowFOC.Visible = false;
			}
			if (comboBoxType.SelectedType == SysDocTypes.PurchaseOrderNI)
			{
				checkBoxBOLmandatory.Visible = true;
			}
			else
			{
				checkBoxBOLmandatory.Visible = false;
			}
			if (comboBoxType.SelectedType == SysDocTypes.PurchaseInvoiceNI || comboBoxType.SelectedType == SysDocTypes.PurchaseInvoice || comboBoxType.SelectedType == SysDocTypes.ImportPurchaseInvoice)
			{
				checkBoxSupplierInvoiceNoMandatory.Visible = true;
			}
			else
			{
				checkBoxSupplierInvoiceNoMandatory.Visible = false;
			}
			if (comboBoxType.SelectedType == SysDocTypes.DeliveryNote)
			{
				checkBoxActivateItemFilter.Visible = true;
			}
			else
			{
				checkBoxActivateItemFilter.Visible = false;
			}
			if (comboBoxType.SelectedType == SysDocTypes.FreightCharges)
			{
				LabelDocTypeLink.Visible = true;
			}
			else
			{
				LabelDocTypeLink.Visible = false;
			}
			switch (comboBoxType.SelectedType)
			{
			case SysDocTypes.SalesQuote:
			case SysDocTypes.SalesOrder:
			case SysDocTypes.DeliveryNote:
			case SysDocTypes.SalesInvoice:
			case SysDocTypes.SalesReceipt:
			case SysDocTypes.CreditSalesReturn:
			case SysDocTypes.CashSalesReturn:
			case SysDocTypes.DeliveryReturn:
			case SysDocTypes.ConsignOut:
			case SysDocTypes.ConsignOutSettlement:
			case SysDocTypes.ExportSalesInvoice:
			case SysDocTypes.ExportSalesOrder:
			case SysDocTypes.ExportDeliveryNote:
			case SysDocTypes.ConsignOutReturn:
			case SysDocTypes.ExportPackingList:
			case SysDocTypes.W3PLGRN:
			case SysDocTypes.W3PLDelivery:
			case SysDocTypes.W3PLInvoice:
			case SysDocTypes.SalesEnquiry:
			case SysDocTypes.SalesProforma:
			case SysDocTypes.ExportPickList:
			case SysDocTypes.CreditLimitReview:
			case SysDocTypes.ExportSalesProfoma:
				buttonEntitySelection.Visible = true;
				buttonEntitySelection.Text = "Customer Classes...";
				entityType = SysDocEntityTypes.CustomerClass;
				buttonItemClasses.Visible = true;
				break;
			case SysDocTypes.PurchaseQuote:
			case SysDocTypes.PurchaseOrder:
			case SysDocTypes.GoodsReceivedNote:
			case SysDocTypes.PurchaseInvoice:
			case SysDocTypes.CashPurchase:
			case SysDocTypes.CashPurchaseReturn:
			case SysDocTypes.PackingList:
			case SysDocTypes.CreditPurchaseReturn:
			case SysDocTypes.ImportPurchaseOrder:
			case SysDocTypes.ImportPurchaseInvoice:
			case SysDocTypes.ImportGoodsReceivedNote:
			case SysDocTypes.ConsignIn:
			case SysDocTypes.ConsignInSettlement:
			case SysDocTypes.ConsignInReturn:
			case SysDocTypes.ProformaInvoice:
			case SysDocTypes.ConsignInClosing:
			case SysDocTypes.PurchaseOrderNI:
			case SysDocTypes.PurchaseInvoiceNI:
			case SysDocTypes.PurchaseCostEntry:
			case SysDocTypes.BOLList:
			case SysDocTypes.FreightCharges:
				buttonEntitySelection.Visible = true;
				buttonEntitySelection.Text = "Vendor Classes...";
				entityType = SysDocEntityTypes.SupplierClass;
				buttonItemClasses.Visible = true;
				break;
			case SysDocTypes.InventoryAdjustment:
			case SysDocTypes.TransitTransferOut:
			case SysDocTypes.TransitTransferIn:
			case SysDocTypes.ReturnTransitTransfer:
			case SysDocTypes.DirectInventoryTransfer:
			case SysDocTypes.JobMaterialRequisition:
			case SysDocTypes.InventoryNoneSale:
			case SysDocTypes.InventoryRepacking:
			case SysDocTypes.InventoryDismantle:
			case SysDocTypes.SalesForecasting:
				buttonItemClasses.Visible = true;
				buttonItemClasses.Location = new Point(256, 270);
				break;
			case SysDocTypes.GRNReturn:
				buttonEntitySelection.Visible = true;
				buttonEntitySelection.Text = "Vendor Classes...";
				entityType = SysDocEntityTypes.SupplierClass;
				buttonItemClasses.Enabled = false;
				break;
			default:
				buttonEntitySelection.Enabled = false;
				buttonItemClasses.Enabled = false;
				break;
			}
			switch (comboBoxType.SelectedType)
			{
			case SysDocTypes.SalesInvoice:
			case SysDocTypes.SalesReceipt:
			case SysDocTypes.CreditSalesReturn:
			case SysDocTypes.CashSalesReturn:
			case SysDocTypes.ExportSalesInvoice:
			case SysDocTypes.W3PLInvoice:
				buttonAccounts.Enabled = true;
				break;
			default:
				buttonAccounts.Enabled = false;
				break;
			}
			SysDocTypes selectedType = comboBoxType.SelectedType;
			if (selectedType == SysDocTypes.GJournal || selectedType == SysDocTypes.DebitNote || selectedType == SysDocTypes.CreditNote)
			{
				buttonAccountClasses.Visible = true;
			}
			else
			{
				buttonAccountClasses.Visible = false;
			}
			switch (comboBoxType.SelectedType)
			{
			case SysDocTypes.GJournal:
			case SysDocTypes.InventoryAdjustment:
			case SysDocTypes.TransitTransferOut:
			case SysDocTypes.TransitTransferIn:
			case SysDocTypes.ReturnTransitTransfer:
			case SysDocTypes.SalesQuote:
			case SysDocTypes.SalesOrder:
			case SysDocTypes.DeliveryNote:
			case SysDocTypes.SalesInvoice:
			case SysDocTypes.SalesReceipt:
			case SysDocTypes.CreditSalesReturn:
			case SysDocTypes.CashSalesReturn:
			case SysDocTypes.DeliveryReturn:
			case SysDocTypes.PurchaseQuote:
			case SysDocTypes.PurchaseOrder:
			case SysDocTypes.GoodsReceivedNote:
			case SysDocTypes.PurchaseInvoice:
			case SysDocTypes.CashPurchase:
			case SysDocTypes.CashPurchaseReturn:
			case SysDocTypes.PackingList:
			case SysDocTypes.CreditPurchaseReturn:
			case SysDocTypes.ImportPurchaseOrder:
			case SysDocTypes.ImportPurchaseInvoice:
			case SysDocTypes.DirectInventoryTransfer:
			case SysDocTypes.ImportGoodsReceivedNote:
			case SysDocTypes.ExportSalesInvoice:
			case SysDocTypes.ExportSalesOrder:
			case SysDocTypes.ExportDeliveryNote:
			case SysDocTypes.ProformaInvoice:
			case SysDocTypes.InventoryNoneSale:
			case SysDocTypes.InventoryRepacking:
			case SysDocTypes.ExportPackingList:
			case SysDocTypes.GRNReturn:
			case SysDocTypes.PurchaseClaim:
			case SysDocTypes.CRMActivity:
			case SysDocTypes.PurchaseOrderNI:
			case SysDocTypes.PurchaseInvoiceNI:
			case SysDocTypes.SalesEnquiry:
			case SysDocTypes.SalesProforma:
			case SysDocTypes.ExportPickList:
			case SysDocTypes.PurchaseCostEntry:
			case SysDocTypes.CreditLimitReview:
			case SysDocTypes.BOLList:
			case SysDocTypes.ExportSalesProfoma:
				buttonUsers.Visible = true;
				break;
			default:
				buttonUsers.Visible = false;
				buttonUsers.Location = new Point(126, 293);
				break;
			}
			switch (comboBoxType.SelectedType)
			{
			case SysDocTypes.SalesQuote:
			case SysDocTypes.SalesOrder:
			case SysDocTypes.DeliveryNote:
			case SysDocTypes.SalesInvoice:
			case SysDocTypes.SalesReceipt:
			case SysDocTypes.CreditSalesReturn:
			case SysDocTypes.CashSalesReturn:
			case SysDocTypes.DeliveryReturn:
			case SysDocTypes.PurchaseOrder:
			case SysDocTypes.ImportPurchaseOrder:
			case SysDocTypes.ConsignOut:
			case SysDocTypes.ConsignOutSettlement:
			case SysDocTypes.ExportSalesInvoice:
			case SysDocTypes.ExportSalesOrder:
			case SysDocTypes.ExportDeliveryNote:
			case SysDocTypes.ConsignOutReturn:
			case SysDocTypes.JobInvoice:
			case SysDocTypes.ExportPackingList:
			case SysDocTypes.PropertyRental:
			case SysDocTypes.PropertyRenew:
			case SysDocTypes.PropertyCancel:
			case SysDocTypes.SalesEnquiry:
			case SysDocTypes.SalesProforma:
			case SysDocTypes.ExportPickList:
			case SysDocTypes.TRApplication:
			case SysDocTypes.PropertyServiceInvoice:
			case SysDocTypes.ExportSalesProfoma:
				checkBoxAllowmultiprint.Visible = true;
				break;
			default:
				checkBoxAllowmultiprint.Visible = false;
				break;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					currentData = new SystemDocumentData();
				}
				DataRow dataRow = (currentData.SystemDocumentTable.Rows.Count != 0) ? currentData.SystemDocumentTable.Rows[0] : currentData.SystemDocumentTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = textBoxCode.Text.Trim();
				dataRow["DocName"] = textBoxName.Text.Trim();
				dataRow["NumberPrefix"] = textBoxPrefix.Text.Trim();
				dataRow["NextNumber"] = textBoxNextNumber.Text.Trim();
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["DivisionID"] = comboBoxCompanyDivision.SelectedID;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["PrintTemplateName"] = textBoxTemplateName.Text;
				dataRow["OpenListQuery"] = DBNull.Value;
				if (comboBoxConsignOutLocation.SelectedID != "" && (comboBoxType.SelectedType == SysDocTypes.ConsignOut || comboBoxType.SelectedType == SysDocTypes.ConsignOutReturn || comboBoxType.SelectedType == SysDocTypes.GarmentRental))
				{
					dataRow["ConsignOutLocationID"] = comboBoxConsignOutLocation.SelectedID;
				}
				else
				{
					dataRow["ConsignOutLocationID"] = DBNull.Value;
				}
				if (isNewRecord)
				{
					SysDocTypes sysDocTypes = (SysDocTypes)Enum.Parse(typeof(SysDocTypes), comboBoxType.SelectedID);
					dataRow["SysDocType"] = (int)sysDocTypes;
				}
				else
				{
					dataRow["SysDocType"] = (int)sysDocTypeID;
				}
				dataRow["DoPrint"] = checkBoxPrintAfterSave.Checked;
				if (checkBoxPrintAfterSave.Checked)
				{
					if (radioButtonPreview.Checked)
					{
						dataRow["PrintAfterSave"] = false;
					}
					else if (radioButtonPrint.Checked)
					{
						dataRow["PrintAfterSave"] = true;
					}
				}
				dataRow["AllowFOC"] = checkBoxAllowFOC.Checked;
				dataRow["IsBOLMandatory"] = checkBoxBOLmandatory.Checked;
				dataRow["AllowMultiTemplate"] = checkBoxAllowmultiprint.Checked;
				dataRow["PriceIncludeTax"] = checkBoxPriceIncludeTax.Checked;
				dataRow["IsSupplierInvoiceNoMandatory"] = checkBoxSupplierInvoiceNoMandatory.Checked;
				dataRow["ItemFilterBasedonCustomer"] = checkBoxActivateItemFilter.Checked;
				dataRow.EndEdit();
				if (currentData.SystemDocumentTable.Rows.Count == 0)
				{
					currentData.SystemDocumentTable.Rows.Add(dataRow);
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
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.SystemDocumentSystem.GetSystemDocumentByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			textBoxCode.Text = dataRow["SysDocID"].ToString();
			textBoxName.Text = dataRow["DocName"].ToString();
			docName = textBoxName.Text;
			textBoxPrefix.Text = dataRow["NumberPrefix"].ToString();
			comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
			comboBoxCompanyDivision.SelectedID = dataRow["DivisionID"].ToString();
			textBoxNextNumber.Text = dataRow["NextNumber"].ToString();
			textBoxTemplateName.Text = dataRow["PrintTemplateName"].ToString();
			if (comboBoxType.SelectedType == SysDocTypes.ConsignOut || comboBoxType.SelectedType == SysDocTypes.ConsignOutReturn || comboBoxType.SelectedType == SysDocTypes.GarmentRental)
			{
				comboBoxConsignOutLocation.SelectedID = dataRow["ConsignOutLocationID"].ToString();
			}
			if (dataRow["Inactive"] != DBNull.Value)
			{
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
			}
			else
			{
				checkBoxInactive.Checked = false;
			}
			bool @checked = false;
			if (dataRow["DoPrint"] != DBNull.Value)
			{
				@checked = bool.Parse(dataRow["DoPrint"].ToString());
				checkBoxPrintAfterSave.Checked = @checked;
			}
			else
			{
				checkBoxPrintAfterSave.Checked = @checked;
			}
			if (dataRow["PrintAfterSave"] != DBNull.Value)
			{
				if (!bool.Parse(dataRow["PrintAfterSave"].ToString()))
				{
					radioButtonPreview.Checked = true;
				}
				else if (bool.Parse(dataRow["PrintAfterSave"].ToString()))
				{
					radioButtonPrint.Checked = true;
				}
			}
			if (dataRow["AllowFOC"] != DBNull.Value)
			{
				checkBoxAllowFOC.Checked = bool.Parse(dataRow["AllowFOC"].ToString());
			}
			else
			{
				checkBoxAllowFOC.Checked = false;
			}
			if (dataRow["IsBOLMandatory"] != DBNull.Value)
			{
				checkBoxBOLmandatory.Checked = bool.Parse(dataRow["IsBOLMandatory"].ToString());
			}
			else
			{
				checkBoxBOLmandatory.Checked = false;
			}
			if (dataRow["AllowMultiTemplate"] != DBNull.Value)
			{
				checkBoxAllowmultiprint.Checked = bool.Parse(dataRow["AllowMultiTemplate"].ToString());
			}
			else
			{
				checkBoxAllowmultiprint.Checked = false;
			}
			if (dataRow["PriceIncludeTax"] != DBNull.Value)
			{
				checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
			}
			else
			{
				checkBoxPriceIncludeTax.Checked = false;
			}
			sysDocTypeID = (SysDocTypes)checked((int)Convert.ToInt64(dataRow["SysDocType"].ToString()));
			SysDocTypeComboBox sysDocTypeComboBox = comboBoxType;
			int num = (int)sysDocTypeID;
			sysDocTypeComboBox.SelectedID = num.ToString();
			if (dataRow["IsSupplierInvoiceNoMandatory"] != DBNull.Value)
			{
				checkBoxSupplierInvoiceNoMandatory.Checked = bool.Parse(dataRow["IsSupplierInvoiceNoMandatory"].ToString());
			}
			else
			{
				checkBoxSupplierInvoiceNoMandatory.Checked = false;
			}
			if (dataRow["ItemFilterBasedonCustomer"] != DBNull.Value)
			{
				checkBoxActivateItemFilter.Checked = bool.Parse(dataRow["ItemFilterBasedonCustomer"].ToString());
			}
			else
			{
				checkBoxActivateItemFilter.Checked = false;
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
				bool flag = (!isNewRecord) ? Factory.SystemDocumentSystem.UpdateSystemDocument(currentData) : Factory.SystemDocumentSystem.CreateSystemDocument(currentData);
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
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || comboBoxLocation.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxType.SelectedType == SysDocTypes.ConsignOut && comboBoxConsignOutLocation.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a consignment out store.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("System_Document", "SysDocID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist. Please enter another code.");
				textBoxCode.Focus();
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
			textBoxCode.Clear();
			textBoxName.Clear();
			textBoxNextNumber.Text = 1.ToString();
			textBoxPrefix.Clear();
			comboBoxConsignOutLocation.Clear();
			textBoxTemplateName.Clear();
			checkBoxAllowFOC.Checked = false;
			checkBoxBOLmandatory.Checked = false;
			comboBoxLocation.Clear();
			currentData = new SystemDocumentData();
			checkBoxPrintAfterSave.Checked = false;
			radioButtonPrint.Checked = false;
			radioButtonPreview.Checked = true;
			checkBoxAllowmultiprint.Checked = false;
			checkBoxSupplierInvoiceNoMandatory.Checked = false;
			checkBoxActivateItemFilter.Checked = false;
			comboBoxCompanyDivision.Clear();
			IsNewRecord = true;
			checkBoxPriceIncludeTax.Checked = false;
			sysDocTypeID = SysDocTypes.None;
			comboBoxType.SelectedID = SysDocTypes.None.ToString();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
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
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				return Factory.SystemDocumentSystem.DeleteSystemDocument(textBoxCode.Text);
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
			int result = 0;
			if (sysDocTypeID == SysDocTypes.None)
			{
				int.TryParse(comboBoxType.SelectedID, out result);
			}
			else
			{
				result = (int)sysDocTypeID;
			}
			if (!(result.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetNextID("System_Document", "SysDocID", textBoxCode.Text, "SysDocType", result.ToString()));
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			int result = 0;
			if (sysDocTypeID == SysDocTypes.None)
			{
				int.TryParse(comboBoxType.SelectedID, out result);
			}
			else
			{
				result = (int)sysDocTypeID;
			}
			if (!(result.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetPreviousID("System_Document", "SysDocID", textBoxCode.Text, "SysDocType", result.ToString()));
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = 0;
			if (sysDocTypeID == SysDocTypes.None)
			{
				int.TryParse(comboBoxType.SelectedID, out result);
			}
			else
			{
				result = (int)sysDocTypeID;
			}
			if (!(result.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetLastID("System_Document", "SysDocID", "SysDocType", result.ToString()));
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = 0;
			if (sysDocTypeID == SysDocTypes.None)
			{
				int.TryParse(comboBoxType.SelectedID, out result);
			}
			else
			{
				result = (int)sysDocTypeID;
			}
			if (!(result.ToString() == "0"))
			{
				LoadData(DatabaseHelper.GetFirstID("System_Document", "SysDocID", "SysDocType", result.ToString()));
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
					IsNewRecord = true;
					ClearForm();
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
			controlPanel.Controls.Clear();
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

		private void CreateControl(SysDocControlTypes ControlType, string value)
		{
			switch (ControlType)
			{
			case SysDocControlTypes.TextBox:
			{
				TextBox textBox = new TextBox();
				textBox.Text = value;
				textBox.Location = new Point(LocationX, LocationY);
				controlPanel.Controls.Add(textBox);
				break;
			}
			case SysDocControlTypes.CheckBox:
			{
				CheckBox checkBox = new CheckBox();
				checkBox.Checked = Convert.ToBoolean(value);
				checkBox.Location = new Point(LocationX, LocationY);
				controlPanel.Controls.Add(checkBox);
				break;
			}
			case SysDocControlTypes.Numeric:
			{
				NumericUpDown numericUpDown = new NumericUpDown();
				numericUpDown.Text = value;
				numericUpDown.Location = new Point(LocationX, LocationY);
				controlPanel.Controls.Add(numericUpDown);
				break;
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
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.SysDoc);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string printTemplatePath = PrintHelper.PrintTemplatePath;
			openFileDialog.InitialDirectory = printTemplatePath + "\\Documents";
			openFileDialog.DefaultExt = "*.repx";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxTemplateName.Text = openFileDialog.SafeFileName.Replace(".repx", "");
			}
		}

		private void buttonEntitySelection_Click(object sender, EventArgs e)
		{
			SysDocEntityLinkDialog sysDocEntityLinkDialog = new SysDocEntityLinkDialog();
			sysDocEntityLinkDialog.SysDocID = textBoxCode.Text;
			sysDocEntityLinkDialog.EntityType = entityType;
			sysDocEntityLinkDialog.ShowDialog(this);
		}

		private void buttonItemClasses_Click(object sender, EventArgs e)
		{
			SysDocEntityLinkDialog sysDocEntityLinkDialog = new SysDocEntityLinkDialog();
			sysDocEntityLinkDialog.SysDocID = textBoxCode.Text;
			sysDocEntityLinkDialog.EntityType = SysDocEntityTypes.ProductClass;
			sysDocEntityLinkDialog.ShowDialog(this);
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
			SysDocAccountsForm obj = new SysDocAccountsForm
			{
				ItemCode = textBoxCode.Text,
				ItemName = textBoxName.Text
			};
			if (currentData.SystemDocumentTable.Rows.Count == 0)
			{
				GetData();
			}
			obj.LoadData(currentData);
			obj.ShowDialog();
			if (obj.IsDirty)
			{
				formManager.IsForcedDirty = true;
			}
		}

		private void buttonAccountClasses_Click(object sender, EventArgs e)
		{
			SysDocEntityLinkDialog sysDocEntityLinkDialog = new SysDocEntityLinkDialog();
			sysDocEntityLinkDialog.SysDocID = textBoxCode.Text;
			sysDocEntityLinkDialog.EntityType = SysDocEntityTypes.AccountGroup;
			sysDocEntityLinkDialog.ShowDialog(this);
		}

		private void buttonUsers_Click(object sender, EventArgs e)
		{
			SysDocEntityLinkDialog sysDocEntityLinkDialog = new SysDocEntityLinkDialog();
			sysDocEntityLinkDialog.SysDocID = textBoxCode.Text;
			sysDocEntityLinkDialog.EntityType = SysDocEntityTypes.User;
			sysDocEntityLinkDialog.ShowDialog(this);
		}

		private void checkBoxAllowmultiprint_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAllowmultiprint.Checked)
			{
				buttonAttachements.Visible = true;
			}
			else
			{
				buttonAttachements.Visible = false;
			}
		}

		private void buttonAttachements_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = textBoxCode.Text;
			attachementDetailsForm.SysDocType = comboBoxType.SelectedType;
			attachementDetailsForm.FormType = "sysDoc";
			attachementDetailsForm.Show();
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			OpenListQueryDialog openListQueryDialog = new OpenListQueryDialog();
			openListQueryDialog.DocID = textBoxCode.Text;
			openListQueryDialog.DocName = textBoxName.Text;
			openListQueryDialog.DocType = comboBoxType.Text;
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				if (dataRow["OpenListQuery"] != DBNull.Value)
				{
					openListQueryDialog.Query = dataRow["OpenListQuery"].ToString();
				}
				if (openListQueryDialog.ShowDialog() == DialogResult.OK)
				{
					currentData.Tables[0].Rows[0]["OpenListQuery"] = openListQueryDialog.Query;
					Factory.SystemDocumentSystem.InsertOpenListQuery(currentData);
				}
			}
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

		private void buttonSysDocCompanyOption_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				SysDocCompanyOptionsForm sysDocCompanyOptionsForm = new SysDocCompanyOptionsForm();
				if (!IsNewRecord && !string.IsNullOrEmpty(comboBoxType.SelectedID))
				{
					sysDocCompanyOptionsForm.SysDocTypeID = comboBoxType.SelectedID;
					sysDocCompanyOptionsForm.IsEnabled = false;
				}
				sysDocCompanyOptionsForm.Show();
			}
		}

		private void buttonDocIDCompanyOption_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				SysDocCompanyOptionsForm sysDocCompanyOptionsForm = new SysDocCompanyOptionsForm();
				if (!IsNewRecord && !string.IsNullOrEmpty(comboBoxType.SelectedID))
				{
					sysDocCompanyOptionsForm.SysDocTypeID = comboBoxType.SelectedID;
					sysDocCompanyOptionsForm.SysDocID = textBoxCode.Text;
					sysDocCompanyOptionsForm.IsEnabled = false;
				}
				sysDocCompanyOptionsForm.Show();
			}
		}

		private void textBoxNextNumber_Validating(object sender, CancelEventArgs e)
		{
		}

		private void textBoxNextNumber_Validated(object sender, EventArgs e)
		{
			textBoxNextNumber.Text = Format.RemoveChar(textBoxNextNumber.Text, ',');
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCompanyDivision(comboBoxCompanyDivision.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditConsignLocation(comboBoxConsignOutLocation.SelectedID);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.SysDocDetailsForm));
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCompanyDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			textBoxNextNumber = new Micromind.UISupport.NumberTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			buttonDocIDCompanyOption = new Micromind.UISupport.XPButton();
			buttonSysDocCompanyOption = new Micromind.UISupport.XPButton();
			LabelDocTypeLink = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxActivateItemFilter = new System.Windows.Forms.CheckBox();
			groupBoxConsignOut = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxConsignOutLocation = new Micromind.DataControls.LocationComboBox();
			textBoxNextNumbern = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			checkBoxSupplierInvoiceNoMandatory = new System.Windows.Forms.CheckBox();
			labelCode = new Micromind.UISupport.MMLabel();
			checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			buttonOpenList = new Micromind.UISupport.XPButton();
			textBoxName = new Micromind.UISupport.MMTextBox();
			buttonAttachements = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			checkBoxAllowmultiprint = new System.Windows.Forms.CheckBox();
			checkBoxBOLmandatory = new System.Windows.Forms.CheckBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			buttonUsers = new Micromind.UISupport.XPButton();
			textBoxPrefix = new Micromind.UISupport.MMTextBox();
			buttonAccountClasses = new Micromind.UISupport.XPButton();
			buttonAccounts = new Micromind.UISupport.XPButton();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			buttonItemClasses = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonEntitySelection = new Micromind.UISupport.XPButton();
			checkBoxPrintAfterSave = new System.Windows.Forms.CheckBox();
			panelPrintOption = new System.Windows.Forms.Panel();
			radioButtonPrint = new System.Windows.Forms.RadioButton();
			radioButtonPreview = new System.Windows.Forms.RadioButton();
			comboBoxType = new Micromind.DataControls.SysDocTypeComboBox();
			checkBoxAllowFOC = new System.Windows.Forms.CheckBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxTemplateName = new Micromind.UISupport.MMTextBox();
			buttonSelectTemplatePath = new Micromind.UISupport.XPButton();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			controlPanel = new System.Windows.Forms.Panel();
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
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			sysDocTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupBoxConsignOut).BeginInit();
			groupBoxConsignOut.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignOutLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxNextNumbern).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			panelPrintOption.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxType).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)sysDocTabControl).BeginInit();
			sysDocTabControl.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel4);
			ultraTabPageControl1.Controls.Add(comboBoxCompanyDivision);
			ultraTabPageControl1.Controls.Add(textBoxNextNumber);
			ultraTabPageControl1.Controls.Add(mmLabel6);
			ultraTabPageControl1.Controls.Add(buttonDocIDCompanyOption);
			ultraTabPageControl1.Controls.Add(buttonSysDocCompanyOption);
			ultraTabPageControl1.Controls.Add(LabelDocTypeLink);
			ultraTabPageControl1.Controls.Add(checkBoxActivateItemFilter);
			ultraTabPageControl1.Controls.Add(groupBoxConsignOut);
			ultraTabPageControl1.Controls.Add(checkBoxSupplierInvoiceNoMandatory);
			ultraTabPageControl1.Controls.Add(labelCode);
			ultraTabPageControl1.Controls.Add(checkBoxPriceIncludeTax);
			ultraTabPageControl1.Controls.Add(mmLabel1);
			ultraTabPageControl1.Controls.Add(textBoxCode);
			ultraTabPageControl1.Controls.Add(buttonOpenList);
			ultraTabPageControl1.Controls.Add(textBoxName);
			ultraTabPageControl1.Controls.Add(buttonAttachements);
			ultraTabPageControl1.Controls.Add(checkBoxInactive);
			ultraTabPageControl1.Controls.Add(checkBoxAllowmultiprint);
			ultraTabPageControl1.Controls.Add(checkBoxBOLmandatory);
			ultraTabPageControl1.Controls.Add(mmLabel2);
			ultraTabPageControl1.Controls.Add(buttonUsers);
			ultraTabPageControl1.Controls.Add(textBoxPrefix);
			ultraTabPageControl1.Controls.Add(buttonAccountClasses);
			ultraTabPageControl1.Controls.Add(buttonAccounts);
			ultraTabPageControl1.Controls.Add(comboBoxLocation);
			ultraTabPageControl1.Controls.Add(buttonItemClasses);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel3);
			ultraTabPageControl1.Controls.Add(buttonEntitySelection);
			ultraTabPageControl1.Controls.Add(checkBoxPrintAfterSave);
			ultraTabPageControl1.Controls.Add(panelPrintOption);
			ultraTabPageControl1.Controls.Add(comboBoxType);
			ultraTabPageControl1.Controls.Add(checkBoxAllowFOC);
			ultraTabPageControl1.Controls.Add(mmLabel4);
			ultraTabPageControl1.Controls.Add(textBoxTemplateName);
			ultraTabPageControl1.Controls.Add(buttonSelectTemplatePath);
			ultraTabPageControl1.Controls.Add(mmLabel5);
			ultraTabPageControl1.Controls.Add(mmLabel7);
			ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(696, 399);
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(302, 83);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(51, 15);
			ultraFormattedLinkLabel4.TabIndex = 127;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Division:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCompanyDivision.Assigned = false;
			comboBoxCompanyDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCompanyDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCompanyDivision.CustomReportFieldName = "";
			comboBoxCompanyDivision.CustomReportKey = "";
			comboBoxCompanyDivision.CustomReportValueType = 1;
			comboBoxCompanyDivision.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCompanyDivision.DisplayLayout.Appearance = appearance3;
			comboBoxCompanyDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCompanyDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxCompanyDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCompanyDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCompanyDivision.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxCompanyDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCompanyDivision.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxCompanyDivision.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxCompanyDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCompanyDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxCompanyDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCompanyDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCompanyDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCompanyDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCompanyDivision.Editable = true;
			comboBoxCompanyDivision.FilterString = "";
			comboBoxCompanyDivision.HasAllAccount = false;
			comboBoxCompanyDivision.HasCustom = false;
			comboBoxCompanyDivision.IsDataLoaded = false;
			comboBoxCompanyDivision.Location = new System.Drawing.Point(381, 80);
			comboBoxCompanyDivision.MaxDropDownItems = 12;
			comboBoxCompanyDivision.Name = "comboBoxCompanyDivision";
			comboBoxCompanyDivision.ShowInactiveItems = false;
			comboBoxCompanyDivision.ShowQuickAdd = true;
			comboBoxCompanyDivision.Size = new System.Drawing.Size(198, 20);
			comboBoxCompanyDivision.TabIndex = 6;
			comboBoxCompanyDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxNextNumber.AllowDecimal = false;
			textBoxNextNumber.CustomReportFieldName = "";
			textBoxNextNumber.CustomReportKey = "";
			textBoxNextNumber.CustomReportValueType = 1;
			textBoxNextNumber.IsComboTextBox = false;
			textBoxNextNumber.IsModified = false;
			textBoxNextNumber.Location = new System.Drawing.Point(381, 105);
			textBoxNextNumber.MaxLength = 15;
			textBoxNextNumber.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNextNumber.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNextNumber.Name = "textBoxNextNumber";
			textBoxNextNumber.NullText = "1";
			textBoxNextNumber.Size = new System.Drawing.Size(198, 20);
			textBoxNextNumber.TabIndex = 8;
			textBoxNextNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNextNumber.Validating += new System.ComponentModel.CancelEventHandler(textBoxNextNumber_Validating);
			textBoxNextNumber.Validated += new System.EventHandler(textBoxNextNumber_Validated);
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(303, 109);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(72, 13);
			mmLabel6.TabIndex = 125;
			mmLabel6.Text = "Next Number:";
			buttonDocIDCompanyOption.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDocIDCompanyOption.BackColor = System.Drawing.Color.DarkGray;
			buttonDocIDCompanyOption.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDocIDCompanyOption.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDocIDCompanyOption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDocIDCompanyOption.Location = new System.Drawing.Point(299, 35);
			buttonDocIDCompanyOption.Name = "buttonDocIDCompanyOption";
			buttonDocIDCompanyOption.Size = new System.Drawing.Size(29, 21);
			buttonDocIDCompanyOption.TabIndex = 123;
			buttonDocIDCompanyOption.Text = "...";
			buttonDocIDCompanyOption.UseVisualStyleBackColor = false;
			buttonDocIDCompanyOption.Click += new System.EventHandler(buttonDocIDCompanyOption_Click);
			buttonSysDocCompanyOption.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSysDocCompanyOption.BackColor = System.Drawing.Color.DarkGray;
			buttonSysDocCompanyOption.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSysDocCompanyOption.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSysDocCompanyOption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSysDocCompanyOption.Location = new System.Drawing.Point(299, 12);
			buttonSysDocCompanyOption.Name = "buttonSysDocCompanyOption";
			buttonSysDocCompanyOption.Size = new System.Drawing.Size(29, 21);
			buttonSysDocCompanyOption.TabIndex = 122;
			buttonSysDocCompanyOption.Text = "...";
			buttonSysDocCompanyOption.UseVisualStyleBackColor = false;
			buttonSysDocCompanyOption.Click += new System.EventHandler(buttonSysDocCompanyOption_Click);
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			LabelDocTypeLink.Appearance = appearance15;
			LabelDocTypeLink.AutoSize = true;
			LabelDocTypeLink.Location = new System.Drawing.Point(11, 15);
			LabelDocTypeLink.Name = "LabelDocTypeLink";
			LabelDocTypeLink.Size = new System.Drawing.Size(95, 15);
			LabelDocTypeLink.TabIndex = 0;
			LabelDocTypeLink.TabStop = true;
			LabelDocTypeLink.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			LabelDocTypeLink.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			LabelDocTypeLink.Value = "Document Type:";
			LabelDocTypeLink.Visible = false;
			appearance16.ForeColor = System.Drawing.Color.Blue;
			LabelDocTypeLink.VisitedLinkAppearance = appearance16;
			LabelDocTypeLink.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			checkBoxActivateItemFilter.AutoSize = true;
			checkBoxActivateItemFilter.Location = new System.Drawing.Point(124, 243);
			checkBoxActivateItemFilter.Name = "checkBoxActivateItemFilter";
			checkBoxActivateItemFilter.Size = new System.Drawing.Size(133, 17);
			checkBoxActivateItemFilter.TabIndex = 20;
			checkBoxActivateItemFilter.Text = "Item Filter on Customer";
			checkBoxActivateItemFilter.UseVisualStyleBackColor = true;
			groupBoxConsignOut.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			groupBoxConsignOut.Controls.Add(ultraFormattedLinkLabel1);
			groupBoxConsignOut.Controls.Add(comboBoxConsignOutLocation);
			groupBoxConsignOut.Controls.Add(textBoxNextNumbern);
			groupBoxConsignOut.Controls.Add(mmLabel3);
			groupBoxConsignOut.Location = new System.Drawing.Point(-2, 340);
			groupBoxConsignOut.Name = "groupBoxConsignOut";
			groupBoxConsignOut.Size = new System.Drawing.Size(659, 39);
			groupBoxConsignOut.TabIndex = 26;
			groupBoxConsignOut.Visible = false;
			appearance17.FontData.BoldAsString = "True";
			appearance17.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance17;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(15, 12);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(114, 15);
			ultraFormattedLinkLabel1.TabIndex = 114;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Consignment Store:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxConsignOutLocation.Assigned = false;
			comboBoxConsignOutLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignOutLocation.CustomReportFieldName = "";
			comboBoxConsignOutLocation.CustomReportKey = "";
			comboBoxConsignOutLocation.CustomReportValueType = 1;
			comboBoxConsignOutLocation.DescriptionTextBox = null;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignOutLocation.DisplayLayout.Appearance = appearance19;
			comboBoxConsignOutLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignOutLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance20.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutLocation.DisplayLayout.GroupByBox.Appearance = appearance20;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignOutLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
			comboBoxConsignOutLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance22.BackColor2 = System.Drawing.SystemColors.Control;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignOutLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
			comboBoxConsignOutLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignOutLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignOutLocation.DisplayLayout.Override.ActiveCellAppearance = appearance23;
			appearance24.BackColor = System.Drawing.SystemColors.Highlight;
			appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignOutLocation.DisplayLayout.Override.ActiveRowAppearance = appearance24;
			comboBoxConsignOutLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignOutLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutLocation.DisplayLayout.Override.CardAreaAppearance = appearance25;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignOutLocation.DisplayLayout.Override.CellAppearance = appearance26;
			comboBoxConsignOutLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignOutLocation.DisplayLayout.Override.CellPadding = 0;
			appearance27.BackColor = System.Drawing.SystemColors.Control;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutLocation.DisplayLayout.Override.GroupByRowAppearance = appearance27;
			appearance28.TextHAlignAsString = "Left";
			comboBoxConsignOutLocation.DisplayLayout.Override.HeaderAppearance = appearance28;
			comboBoxConsignOutLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignOutLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignOutLocation.DisplayLayout.Override.RowAppearance = appearance29;
			comboBoxConsignOutLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignOutLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
			comboBoxConsignOutLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignOutLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignOutLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignOutLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignOutLocation.Editable = true;
			comboBoxConsignOutLocation.FilterString = "";
			comboBoxConsignOutLocation.HasAllAccount = false;
			comboBoxConsignOutLocation.HasCustom = false;
			comboBoxConsignOutLocation.IsDataLoaded = false;
			comboBoxConsignOutLocation.Location = new System.Drawing.Point(135, 9);
			comboBoxConsignOutLocation.MaxDropDownItems = 12;
			comboBoxConsignOutLocation.Name = "comboBoxConsignOutLocation";
			comboBoxConsignOutLocation.ShowAll = false;
			comboBoxConsignOutLocation.ShowConsignIn = false;
			comboBoxConsignOutLocation.ShowConsignOut = true;
			comboBoxConsignOutLocation.ShowDefaultLocationOnly = false;
			comboBoxConsignOutLocation.ShowInactiveItems = false;
			comboBoxConsignOutLocation.ShowNormalLocations = false;
			comboBoxConsignOutLocation.ShowPOSOnly = false;
			comboBoxConsignOutLocation.ShowQuickAdd = true;
			comboBoxConsignOutLocation.ShowWarehouseOnly = false;
			comboBoxConsignOutLocation.Size = new System.Drawing.Size(157, 20);
			comboBoxConsignOutLocation.TabIndex = 4;
			comboBoxConsignOutLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxNextNumbern.Location = new System.Drawing.Point(390, 9);
			textBoxNextNumbern.MaxValue = 99999999;
			textBoxNextNumbern.MinValue = 1;
			textBoxNextNumbern.Name = "textBoxNextNumbern";
			textBoxNextNumbern.PromptChar = ' ';
			textBoxNextNumbern.Size = new System.Drawing.Size(139, 21);
			textBoxNextNumbern.TabIndex = 7;
			textBoxNextNumbern.TabNavigation = Infragistics.Win.UltraWinMaskedEdit.MaskedEditTabNavigation.NextControl;
			textBoxNextNumbern.Visible = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(308, 13);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(72, 13);
			mmLabel3.TabIndex = 20;
			mmLabel3.Text = "Next Number:";
			mmLabel3.Visible = false;
			checkBoxSupplierInvoiceNoMandatory.AutoSize = true;
			checkBoxSupplierInvoiceNoMandatory.Location = new System.Drawing.Point(245, 220);
			checkBoxSupplierInvoiceNoMandatory.Name = "checkBoxSupplierInvoiceNoMandatory";
			checkBoxSupplierInvoiceNoMandatory.Size = new System.Drawing.Size(160, 17);
			checkBoxSupplierInvoiceNoMandatory.TabIndex = 19;
			checkBoxSupplierInvoiceNoMandatory.Text = "Supplier Doc No Mandatory ";
			checkBoxSupplierInvoiceNoMandatory.UseVisualStyleBackColor = true;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 39);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(85, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Document ID:";
			checkBoxPriceIncludeTax.AutoSize = true;
			checkBoxPriceIncludeTax.Location = new System.Drawing.Point(124, 220);
			checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
			checkBoxPriceIncludeTax.Size = new System.Drawing.Size(119, 17);
			checkBoxPriceIncludeTax.TabIndex = 18;
			checkBoxPriceIncludeTax.Text = "Price including VAT";
			checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 62);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(104, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Document Name:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(124, 35);
			textBoxCode.MaxLength = 7;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(175, 20);
			textBoxCode.TabIndex = 2;
			buttonOpenList.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenList.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenList.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenList.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenList.Location = new System.Drawing.Point(520, 280);
			buttonOpenList.Name = "buttonOpenList";
			buttonOpenList.Size = new System.Drawing.Size(126, 24);
			buttonOpenList.TabIndex = 24;
			buttonOpenList.Text = "Listing...";
			buttonOpenList.UseVisualStyleBackColor = false;
			buttonOpenList.Click += new System.EventHandler(xpButton2_Click);
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(124, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(548, 20);
			textBoxName.TabIndex = 4;
			buttonAttachements.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAttachements.BackColor = System.Drawing.Color.DarkGray;
			buttonAttachements.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAttachements.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAttachements.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAttachements.Location = new System.Drawing.Point(502, 162);
			buttonAttachements.Name = "buttonAttachements";
			buttonAttachements.Size = new System.Drawing.Size(126, 24);
			buttonAttachements.TabIndex = 15;
			buttonAttachements.Text = "Additional Templates...";
			buttonAttachements.UseVisualStyleBackColor = false;
			buttonAttachements.Visible = false;
			buttonAttachements.Click += new System.EventHandler(buttonAttachements_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(373, 37);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 3;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			checkBoxAllowmultiprint.AutoSize = true;
			checkBoxAllowmultiprint.Location = new System.Drawing.Point(347, 164);
			checkBoxAllowmultiprint.Name = "checkBoxAllowmultiprint";
			checkBoxAllowmultiprint.Size = new System.Drawing.Size(149, 17);
			checkBoxAllowmultiprint.TabIndex = 14;
			checkBoxAllowmultiprint.Text = "Allow Multiple Print Format";
			checkBoxAllowmultiprint.UseVisualStyleBackColor = true;
			checkBoxAllowmultiprint.CheckedChanged += new System.EventHandler(checkBoxAllowmultiprint_CheckedChanged);
			checkBoxBOLmandatory.AutoSize = true;
			checkBoxBOLmandatory.Location = new System.Drawing.Point(245, 192);
			checkBoxBOLmandatory.Name = "checkBoxBOLmandatory";
			checkBoxBOLmandatory.Size = new System.Drawing.Size(133, 17);
			checkBoxBOLmandatory.TabIndex = 17;
			checkBoxBOLmandatory.Text = "BOL Mandatory on PO";
			checkBoxBOLmandatory.UseVisualStyleBackColor = true;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(9, 108);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(76, 13);
			mmLabel2.TabIndex = 19;
			mmLabel2.Text = "Number Prefix:";
			buttonUsers.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonUsers.BackColor = System.Drawing.Color.DarkGray;
			buttonUsers.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonUsers.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonUsers.Location = new System.Drawing.Point(124, 306);
			buttonUsers.Name = "buttonUsers";
			buttonUsers.Size = new System.Drawing.Size(126, 24);
			buttonUsers.TabIndex = 25;
			buttonUsers.Text = "Users...";
			buttonUsers.UseVisualStyleBackColor = false;
			buttonUsers.Visible = false;
			buttonUsers.Click += new System.EventHandler(buttonUsers_Click);
			textBoxPrefix.BackColor = System.Drawing.Color.White;
			textBoxPrefix.CustomReportFieldName = "";
			textBoxPrefix.CustomReportKey = "";
			textBoxPrefix.CustomReportValueType = 1;
			textBoxPrefix.IsComboTextBox = false;
			textBoxPrefix.IsModified = false;
			textBoxPrefix.Location = new System.Drawing.Point(124, 104);
			textBoxPrefix.MaxLength = 10;
			textBoxPrefix.Name = "textBoxPrefix";
			textBoxPrefix.Size = new System.Drawing.Size(133, 20);
			textBoxPrefix.TabIndex = 7;
			buttonAccountClasses.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAccountClasses.BackColor = System.Drawing.Color.DarkGray;
			buttonAccountClasses.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAccountClasses.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAccountClasses.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAccountClasses.Location = new System.Drawing.Point(256, 280);
			buttonAccountClasses.Name = "buttonAccountClasses";
			buttonAccountClasses.Size = new System.Drawing.Size(126, 24);
			buttonAccountClasses.TabIndex = 22;
			buttonAccountClasses.Text = "Account Classes...";
			buttonAccountClasses.UseVisualStyleBackColor = false;
			buttonAccountClasses.Visible = false;
			buttonAccountClasses.Click += new System.EventHandler(buttonAccountClasses_Click);
			buttonAccounts.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAccounts.BackColor = System.Drawing.Color.DarkGray;
			buttonAccounts.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAccounts.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAccounts.Enabled = false;
			buttonAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAccounts.Location = new System.Drawing.Point(388, 280);
			buttonAccounts.Name = "buttonAccounts";
			buttonAccounts.Size = new System.Drawing.Size(126, 24);
			buttonAccounts.TabIndex = 23;
			buttonAccounts.Text = "Accounts...";
			buttonAccounts.UseVisualStyleBackColor = false;
			buttonAccounts.Visible = false;
			buttonAccounts.Click += new System.EventHandler(buttonAccounts_Click);
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance31;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(124, 80);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(175, 20);
			comboBoxLocation.TabIndex = 5;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonItemClasses.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonItemClasses.BackColor = System.Drawing.Color.DarkGray;
			buttonItemClasses.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonItemClasses.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonItemClasses.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonItemClasses.Location = new System.Drawing.Point(124, 281);
			buttonItemClasses.Name = "buttonItemClasses";
			buttonItemClasses.Size = new System.Drawing.Size(126, 24);
			buttonItemClasses.TabIndex = 21;
			buttonItemClasses.Text = "Item Classes...";
			buttonItemClasses.UseVisualStyleBackColor = false;
			buttonItemClasses.Visible = false;
			buttonItemClasses.Click += new System.EventHandler(buttonItemClasses_Click);
			appearance43.FontData.BoldAsString = "True";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance43;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 83);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabel3.TabIndex = 113;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Location:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			buttonEntitySelection.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonEntitySelection.BackColor = System.Drawing.Color.DarkGray;
			buttonEntitySelection.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonEntitySelection.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonEntitySelection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonEntitySelection.Location = new System.Drawing.Point(256, 281);
			buttonEntitySelection.Name = "buttonEntitySelection";
			buttonEntitySelection.Size = new System.Drawing.Size(126, 24);
			buttonEntitySelection.TabIndex = 13;
			buttonEntitySelection.Text = "Customer Classes...";
			buttonEntitySelection.UseVisualStyleBackColor = false;
			buttonEntitySelection.Visible = false;
			buttonEntitySelection.Click += new System.EventHandler(buttonEntitySelection_Click);
			checkBoxPrintAfterSave.AutoSize = true;
			checkBoxPrintAfterSave.Location = new System.Drawing.Point(124, 164);
			checkBoxPrintAfterSave.Name = "checkBoxPrintAfterSave";
			checkBoxPrintAfterSave.Size = new System.Drawing.Size(82, 17);
			checkBoxPrintAfterSave.TabIndex = 12;
			checkBoxPrintAfterSave.Text = "After saving";
			checkBoxPrintAfterSave.UseVisualStyleBackColor = true;
			checkBoxPrintAfterSave.CheckedChanged += new System.EventHandler(checkBoxPrintAfterSave_CheckedChanged_1);
			panelPrintOption.Controls.Add(radioButtonPrint);
			panelPrintOption.Controls.Add(radioButtonPreview);
			panelPrintOption.Enabled = false;
			panelPrintOption.Location = new System.Drawing.Point(207, 159);
			panelPrintOption.Name = "panelPrintOption";
			panelPrintOption.Size = new System.Drawing.Size(134, 28);
			panelPrintOption.TabIndex = 13;
			radioButtonPrint.AutoSize = true;
			radioButtonPrint.Location = new System.Drawing.Point(70, 4);
			radioButtonPrint.Name = "radioButtonPrint";
			radioButtonPrint.Size = new System.Drawing.Size(46, 17);
			radioButtonPrint.TabIndex = 1;
			radioButtonPrint.Text = "Print";
			radioButtonPrint.UseVisualStyleBackColor = true;
			radioButtonPreview.AutoSize = true;
			radioButtonPreview.Checked = true;
			radioButtonPreview.Location = new System.Drawing.Point(7, 4);
			radioButtonPreview.Name = "radioButtonPreview";
			radioButtonPreview.Size = new System.Drawing.Size(63, 17);
			radioButtonPreview.TabIndex = 1;
			radioButtonPreview.TabStop = true;
			radioButtonPreview.Text = "Preview";
			radioButtonPreview.UseVisualStyleBackColor = true;
			comboBoxType.Assigned = false;
			comboBoxType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxType.CustomReportFieldName = "";
			comboBoxType.CustomReportKey = "";
			comboBoxType.CustomReportValueType = 1;
			comboBoxType.DescriptionTextBox = null;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxType.DisplayLayout.Appearance = appearance45;
			comboBoxType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxType.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxType.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxType.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxType.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxType.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxType.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxType.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
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
			comboBoxType.Location = new System.Drawing.Point(124, 12);
			comboBoxType.MaxDropDownItems = 12;
			comboBoxType.Name = "comboBoxType";
			comboBoxType.ShowInactiveItems = false;
			comboBoxType.ShowQuickAdd = true;
			comboBoxType.Size = new System.Drawing.Size(175, 20);
			comboBoxType.TabIndex = 1;
			comboBoxType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			checkBoxAllowFOC.AutoSize = true;
			checkBoxAllowFOC.Location = new System.Drawing.Point(124, 192);
			checkBoxAllowFOC.Name = "checkBoxAllowFOC";
			checkBoxAllowFOC.Size = new System.Drawing.Size(115, 17);
			checkBoxAllowFOC.TabIndex = 16;
			checkBoxAllowFOC.Text = "Allow FOC quantity";
			checkBoxAllowFOC.UseVisualStyleBackColor = true;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(10, 12);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(100, 13);
			mmLabel4.TabIndex = 115;
			mmLabel4.Text = "Document Type:";
			textBoxTemplateName.BackColor = System.Drawing.Color.White;
			textBoxTemplateName.CustomReportFieldName = "";
			textBoxTemplateName.CustomReportKey = "";
			textBoxTemplateName.CustomReportValueType = 1;
			textBoxTemplateName.IsComboTextBox = false;
			textBoxTemplateName.IsModified = false;
			textBoxTemplateName.Location = new System.Drawing.Point(124, 127);
			textBoxTemplateName.MaxLength = 64;
			textBoxTemplateName.Name = "textBoxTemplateName";
			textBoxTemplateName.Size = new System.Drawing.Size(346, 20);
			textBoxTemplateName.TabIndex = 9;
			textBoxTemplateName.TabStop = false;
			buttonSelectTemplatePath.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectTemplatePath.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectTemplatePath.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectTemplatePath.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectTemplatePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectTemplatePath.Location = new System.Drawing.Point(508, 127);
			buttonSelectTemplatePath.Name = "buttonSelectTemplatePath";
			buttonSelectTemplatePath.Size = new System.Drawing.Size(25, 21);
			buttonSelectTemplatePath.TabIndex = 11;
			buttonSelectTemplatePath.Text = "...";
			buttonSelectTemplatePath.UseVisualStyleBackColor = false;
			buttonSelectTemplatePath.Click += new System.EventHandler(buttonSelectTemplatePath_Click);
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(476, 131);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(30, 13);
			mmLabel5.TabIndex = 10;
			mmLabel5.Text = ".repx";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(9, 131);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(109, 13);
			mmLabel7.TabIndex = 119;
			mmLabel7.Text = "Print Template Name:";
			ultraTabPageControl2.Controls.Add(controlPanel);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(696, 399);
			controlPanel.AutoScroll = true;
			controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			controlPanel.Location = new System.Drawing.Point(0, 0);
			controlPanel.Name = "controlPanel";
			controlPanel.Size = new System.Drawing.Size(696, 399);
			controlPanel.TabIndex = 0;
			controlPanel.Visible = false;
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 452);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(700, 40);
			panelButtons.TabIndex = 22;
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
			xpButton1.Location = new System.Drawing.Point(590, 8);
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
			appearance57.FontData.BoldAsString = "True";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance57;
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
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance58;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			sysDocTabControl.Controls.Add(ultraTabSharedControlsPage1);
			sysDocTabControl.Controls.Add(ultraTabPageControl1);
			sysDocTabControl.Controls.Add(ultraTabPageControl2);
			sysDocTabControl.Location = new System.Drawing.Point(0, 31);
			sysDocTabControl.Name = "sysDocTabControl";
			sysDocTabControl.SharedControlsPage = ultraTabSharedControlsPage1;
			sysDocTabControl.Size = new System.Drawing.Size(700, 422);
			sysDocTabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			sysDocTabControl.TabIndex = 0;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Details";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Settings";
			ultraTab2.Visible = false;
			sysDocTabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(696, 399);
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
			base.ClientSize = new System.Drawing.Size(700, 492);
			base.Controls.Add(sysDocTabControl);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "SysDocDetailsForm";
			Text = "System Document Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(SysDocDetailsForm_Load);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)groupBoxConsignOut).EndInit();
			groupBoxConsignOut.ResumeLayout(false);
			groupBoxConsignOut.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignOutLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxNextNumbern).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			panelPrintOption.ResumeLayout(false);
			panelPrintOption.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxType).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)sysDocTabControl).EndInit();
			sysDocTabControl.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
