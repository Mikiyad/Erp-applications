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
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset
{
	public class FixedAssetDetailsForm : Form, IForm
	{
		private FixedAssetData currentData;

		private const string TABLENAME_CONST = "FixedAsset";

		private const string IDFIELD_CONST = "AssetID";

		private bool isNewRecord = true;

		private bool allowEdit = true;

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

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMSDateTimePicker dateTimePickerPurchaseDate;

		private MMLabel mmLabel3;

		private FixedAssetGroupComboBox comboBoxGroup;

		private FixedAssetLocationComboBox comboBoxLocation;

		private MMLabel mmLabel7;

		private DivisionComboBox comboBoxDivision;

		private DepartmentComboBox comboBoxDepartment;

		private AmountTextBox textBoxOriginalValue;

		private AmountTextBox textBoxSalvage;

		private MMLabel mmLabel10;

		private MMLabel mmLabel11;

		private MMTextBox textBoxInvoiceNumber;

		private MMLabel mmLabel12;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxSupplierName;

		private MMLabel mmLabel13;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraTabPageControl ultraTabPageControl4;

		private AmountTextBox textBoxAccumDep;

		private MMLabel mmLabel16;

		private MMLabel mmLabel19;

		private MMLabel mmLabel18;

		private MMLabel mmLabel17;

		private MMLabel mmLabel20;

		private FixedAssetClassComboBox comboBoxClass;

		private MMTextBox textBoxAssetClassName;

		private MMTextBox textBoxAssetGroupName;

		private MMTextBox textBoxAssetLocationName;

		private MMTextBox textBoxDivisionName;

		private MMTextBox textBoxDepName;

		private DepMethodComboBox comboBoxDepMethod;

		private MMLabel mmLabel4;

		private AmountTextBox textBoxAquesitionCost;

		private NumberTextBox textBoxLife;

		private MMLabel mmLabel22;

		private MMLabel mmLabel23;

		private AmountTextBox textBoxBookValue;

		private PercentTextBox textBoxDepreciationPerc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private NumberTextBox numberTextBox1;

		private MMLabel mmLabel5;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimeDepStartDate;

		private MMLabel mmLabel8;

		private MMLabel mmLabel6;

		private MMTextBox textBoxModel;

		private MMTextBox textBoxBarcode;

		private MMTextBox textBoxSerial;

		private AmountTextBox textBoxOpeningDepAmount;

		private MMLabel mmLabel9;

		private ToolStripButton toolStripButtonInformation;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonAttach;

		private CompanyDivisionComboBox comboBoxCompanyDivision;

		private MMTextBox textBoxCompanyDivision;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private System.Windows.Forms.ToolTip toolTip1;

		public ScreenAreas ScreenArea => ScreenAreas.FixedAsset;

		public int ScreenID => 4007;

		public ScreenTypes ScreenType => ScreenTypes.Card;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
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

		public FixedAssetDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += FixedAssetDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new FixedAssetData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.AssetTable.Rows[0] : currentData.AssetTable.NewRow();
				dataRow.BeginEdit();
				dataRow["AssetID"] = textBoxCode.Text.Trim();
				dataRow["AssetName"] = textBoxName.Text.Trim();
				dataRow["AssetGroupID"] = comboBoxGroup.SelectedID;
				dataRow["AssetLocationID"] = comboBoxLocation.SelectedID;
				dataRow["DepartmentID"] = comboBoxDepartment.SelectedID;
				dataRow["DivisionID"] = comboBoxDivision.SelectedID;
				dataRow["CompanyDivisionID"] = comboBoxCompanyDivision.SelectedID;
				dataRow["AssetClassID"] = comboBoxClass.SelectedID;
				dataRow["BarcodeNumber"] = textBoxBarcode.Text;
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["AquesitionCost"] = textBoxAquesitionCost.Text;
				dataRow["OpeningDepAmount"] = textBoxOpeningDepAmount.Text;
				dataRow["DepMethod"] = comboBoxDepMethod.SelectedID;
				if (textBoxDepreciationPerc.Text != "")
				{
					dataRow["DepPercent"] = textBoxDepreciationPerc.Text;
				}
				else
				{
					dataRow["DepPercent"] = 0;
				}
				dataRow["AccumDep"] = textBoxAccumDep.Text;
				dataRow["BookValue"] = textBoxBookValue.Text;
				dataRow["InvoiceNumber"] = textBoxInvoiceNumber.Text;
				if (textBoxLife.Text != "")
				{
					dataRow["Life"] = textBoxLife.Text;
				}
				else
				{
					dataRow["Life"] = DBNull.Value;
				}
				dataRow["ModelNumber"] = textBoxModel.Text;
				if (textBoxOriginalValue.Text != "")
				{
					dataRow["OriginalValue"] = textBoxOriginalValue.Text;
				}
				if (dateTimePickerPurchaseDate.Checked)
				{
					dataRow["PurchaseDate"] = dateTimePickerPurchaseDate.Value;
				}
				else
				{
					dataRow["PurchaseDate"] = DBNull.Value;
				}
				if (textBoxSalvage.Text != "")
				{
					dataRow["SalvageValue"] = textBoxSalvage.Text;
				}
				else
				{
					dataRow["SalvageValue"] = DBNull.Value;
				}
				dataRow["SerialNumber"] = textBoxSerial.Text;
				dataRow["SupplierName"] = textBoxSupplierName.Text;
				dataRow["DepStartDate"] = dateTimeDepStartDate.Value;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.AssetTable.Rows.Add(dataRow);
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
					currentData = Factory.FixedAssetSystem.GetAssetByID(id.Trim());
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
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxCode.Text = dataRow["AssetID"].ToString();
				textBoxName.Text = dataRow["AssetName"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				if (dataRow["Inactive"] != DBNull.Value)
				{
					checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				}
				else
				{
					checkBoxInactive.Checked = false;
				}
				comboBoxGroup.SelectedID = dataRow["AssetGroupID"].ToString();
				comboBoxLocation.SelectedID = dataRow["AssetLocationID"].ToString();
				comboBoxDepartment.SelectedID = dataRow["DepartmentID"].ToString();
				comboBoxDivision.SelectedID = dataRow["DivisionID"].ToString();
				comboBoxClass.SelectedID = dataRow["AssetClassID"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				textBoxBarcode.Text = dataRow["BarcodeNumber"].ToString();
				comboBoxCompanyDivision.SelectedID = dataRow["CompanyDivisionID"].ToString();
				comboBoxDepMethod.SelectedValue = dataRow["DepMethod"].ToString();
				textBoxInvoiceNumber.Text = dataRow["InvoiceNumber"].ToString();
				textBoxLife.Text = dataRow["Life"].ToString();
				textBoxModel.Text = dataRow["ModelNumber"].ToString();
				textBoxOriginalValue.Text = dataRow["OriginalValue"].ToString();
				if (dataRow["DepMethod"] != DBNull.Value)
				{
					comboBoxDepMethod.SelectedID = int.Parse(dataRow["DepMethod"].ToString());
				}
				else
				{
					comboBoxDepMethod.SelectedID = 0;
				}
				if (dataRow["DepPercent"] != DBNull.Value)
				{
					textBoxDepreciationPerc.Text = decimal.Parse(dataRow["DepPercent"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxDepreciationPerc.Text = 0.ToString(Format.PercentageFormat);
				}
				if (dataRow["OpeningDepAmount"] != DBNull.Value)
				{
					textBoxOpeningDepAmount.Text = decimal.Parse(dataRow["OpeningDepAmount"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxOpeningDepAmount.Text = 0.ToString(Format.TotalAmountFormat);
				}
				if (dataRow["AccumulatedDep"] != DBNull.Value)
				{
					textBoxAccumDep.Text = decimal.Parse(dataRow["AccumulatedDep"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxAccumDep.Text = 0.ToString(Format.TotalAmountFormat);
				}
				if (dataRow["CurrentValue"] != DBNull.Value)
				{
					textBoxBookValue.Text = decimal.Parse(dataRow["CurrentValue"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxBookValue.SetZero();
				}
				if (dataRow["AquesitionCost"] != DBNull.Value)
				{
					textBoxAquesitionCost.Text = decimal.Parse(dataRow["AquesitionCost"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxAquesitionCost.Text = 0.ToString(Format.TotalAmountFormat);
				}
				if (dataRow["PurchaseDate"] != DBNull.Value)
				{
					dateTimePickerPurchaseDate.Value = DateTime.Parse(dataRow["PurchaseDate"].ToString());
					dateTimePickerPurchaseDate.Checked = true;
				}
				else
				{
					dateTimePickerPurchaseDate.Checked = false;
				}
				textBoxSalvage.Text = dataRow["SalvageValue"].ToString();
				textBoxSerial.Text = dataRow["SerialNumber"].ToString();
				textBoxSupplierName.Text = dataRow["SupplierName"].ToString();
				if (dataRow["DepStartDate"] != DBNull.Value)
				{
					DateTime value = DateTime.Parse(dataRow["DepStartDate"].ToString());
					dateTimeDepStartDate.Value = value;
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.FixedAssetSystem.CreateAsset(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.FixedAsset, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.FixedAssetSystem.UpdateAsset(currentData);
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
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxClass.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select an asset class.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("FixedAsset", "AssetID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
			}
			if (comboBoxClass.SelectedID == "" || comboBoxGroup.SelectedID == "" || comboBoxDepartment.SelectedID == "" || comboBoxLocation.SelectedID == "" || comboBoxDivision.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
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
			allowEdit = true;
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("FixedAsset", "AssetID");
			textBoxName.Clear();
			textBoxNote.Clear();
			comboBoxClass.Clear();
			comboBoxGroup.Clear();
			comboBoxLocation.Clear();
			textBoxAquesitionCost.SetZero();
			comboBoxDepartment.Clear();
			comboBoxDivision.Clear();
			comboBoxClass.Clear();
			comboBoxEmployee.Clear();
			textBoxBookValue.SetZero();
			textBoxBarcode.Clear();
			comboBoxDepMethod.SelectedIndex = 0;
			textBoxInvoiceNumber.Clear();
			textBoxLife.Clear();
			textBoxModel.Clear();
			textBoxOriginalValue.Clear();
			textBoxDepreciationPerc.Clear();
			textBoxAccumDep.Text = 0.ToString(Format.TotalAmountFormat);
			dateTimePickerPurchaseDate.Checked = false;
			comboBoxCompanyDivision.Clear();
			textBoxOpeningDepAmount.SetZero();
			textBoxSalvage.Clear();
			textBoxSerial.Clear();
			textBoxSupplierName.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void AssetGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void AssetGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.FixedAssetSystem.DeleteAsset(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("FixedAsset", "AssetID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("FixedAsset", "AssetID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("FixedAsset", "AssetID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("FixedAsset", "AssetID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("FixedAsset", "AssetID", toolStripTextBoxFind.Text.Trim()))
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

		private void FixedAssetDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDepMethod.LoadData();
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
			new FormHelper().ShowList(DataComboType.FixedAsset);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAssetClass(comboBoxClass.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAssetLocation(comboBoxLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDivision(comboBoxDivision.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDepartment(comboBoxDepartment.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAssetGroup(comboBoxLocation.SelectedID);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void comboBoxClass_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void textBoxAssetClassName_TextChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void textBoxAssetLocationName_TextChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false))))
				{
					DataSet assetListReport = Factory.FixedAssetSystem.GetAssetListReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", showInactive: true);
					if (assetListReport == null || assetListReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(assetListReport, "", "Fixed Asset", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
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

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCompanyDivision(comboBoxCompanyDivision.SelectedID);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.FixedAsset.FixedAssetDetailsForm));
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dateTimeDepStartDate = new System.Windows.Forms.DateTimePicker();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			textBoxCompanyDivision = new Micromind.UISupport.MMTextBox();
			comboBoxCompanyDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			textBoxOpeningDepAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxAccumDep = new Micromind.UISupport.AmountTextBox();
			formManager = new Micromind.DataControls.FormManager();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxSupplierName = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxInvoiceNumber = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			dateTimePickerPurchaseDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxOriginalValue = new Micromind.UISupport.AmountTextBox();
			numberTextBox1 = new Micromind.UISupport.NumberTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxDepreciationPerc = new Micromind.UISupport.PercentTextBox();
			comboBoxDepMethod = new Micromind.DataControls.DepMethodComboBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxLife = new Micromind.UISupport.NumberTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxSalvage = new Micromind.UISupport.AmountTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxModel = new Micromind.UISupport.MMTextBox();
			textBoxBarcode = new Micromind.UISupport.MMTextBox();
			textBoxSerial = new Micromind.UISupport.MMTextBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxBookValue = new Micromind.UISupport.AmountTextBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxAquesitionCost = new Micromind.UISupport.AmountTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxDepName = new Micromind.UISupport.MMTextBox();
			textBoxDivisionName = new Micromind.UISupport.MMTextBox();
			textBoxAssetLocationName = new Micromind.UISupport.MMTextBox();
			textBoxAssetGroupName = new Micromind.UISupport.MMTextBox();
			textBoxAssetClassName = new Micromind.UISupport.MMTextBox();
			comboBoxClass = new Micromind.DataControls.FixedAssetClassComboBox();
			comboBoxDepartment = new Micromind.DataControls.DepartmentComboBox();
			comboBoxDivision = new Micromind.DataControls.DivisionComboBox();
			comboBoxLocation = new Micromind.DataControls.FixedAssetLocationComboBox();
			comboBoxGroup = new Micromind.DataControls.FixedAssetGroupComboBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			ultraTabPageControl1.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			ultraTabPageControl4.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).BeginInit();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(textBoxSupplierName);
			ultraTabPageControl1.Controls.Add(mmLabel13);
			ultraTabPageControl1.Controls.Add(textBoxInvoiceNumber);
			ultraTabPageControl1.Controls.Add(mmLabel12);
			ultraTabPageControl1.Controls.Add(mmLabel3);
			ultraTabPageControl1.Controls.Add(dateTimePickerPurchaseDate);
			ultraTabPageControl1.Controls.Add(mmLabel10);
			ultraTabPageControl1.Controls.Add(textBoxOriginalValue);
			ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(609, 178);
			ultraTabPageControl2.Controls.Add(numberTextBox1);
			ultraTabPageControl2.Controls.Add(mmLabel5);
			ultraTabPageControl2.Controls.Add(mmLabel2);
			ultraTabPageControl2.Controls.Add(dateTimeDepStartDate);
			ultraTabPageControl2.Controls.Add(textBoxDepreciationPerc);
			ultraTabPageControl2.Controls.Add(comboBoxDepMethod);
			ultraTabPageControl2.Controls.Add(mmLabel20);
			ultraTabPageControl2.Controls.Add(textBoxLife);
			ultraTabPageControl2.Controls.Add(mmLabel8);
			ultraTabPageControl2.Controls.Add(mmLabel6);
			ultraTabPageControl2.Controls.Add(mmLabel22);
			ultraTabPageControl2.Controls.Add(textBoxSalvage);
			ultraTabPageControl2.Controls.Add(mmLabel7);
			ultraTabPageControl2.Controls.Add(mmLabel11);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(609, 178);
			dateTimeDepStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeDepStartDate.Location = new System.Drawing.Point(144, 37);
			dateTimeDepStartDate.Name = "dateTimeDepStartDate";
			dateTimeDepStartDate.Size = new System.Drawing.Size(139, 20);
			dateTimeDepStartDate.TabIndex = 1;
			ultraTabPageControl3.Controls.Add(textBoxModel);
			ultraTabPageControl3.Controls.Add(textBoxBarcode);
			ultraTabPageControl3.Controls.Add(textBoxSerial);
			ultraTabPageControl3.Controls.Add(mmLabel19);
			ultraTabPageControl3.Controls.Add(mmLabel18);
			ultraTabPageControl3.Controls.Add(mmLabel17);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(609, 178);
			ultraTabPageControl4.Controls.Add(textBoxNote);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(609, 178);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
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
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(635, 31);
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 479);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(635, 40);
			panelButtons.TabIndex = 14;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(306, 37);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Location = new System.Drawing.Point(12, 269);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(613, 204);
			ultraTabControl1.TabIndex = 13;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Purchase Info";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Depreciation Info";
			ultraTab3.TabPage = ultraTabPageControl3;
			ultraTab3.Text = "Additional Info";
			ultraTab4.TabPage = ultraTabPageControl4;
			ultraTab4.Text = "Note";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(609, 178);
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(8, 100);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(70, 15);
			ultraFormattedLinkLabel2.TabIndex = 66;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Asset Class:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance3;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(8, 120);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel1.TabIndex = 67;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Asset Group:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance5;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(8, 142);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(89, 15);
			ultraFormattedLinkLabel3.TabIndex = 68;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Asset Location:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance7;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(8, 164);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(51, 15);
			ultraFormattedLinkLabel4.TabIndex = 69;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Division:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance9.FontData.BoldAsString = "True";
			appearance9.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance9;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(8, 187);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel5.TabIndex = 70;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Department:";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance11.FontData.BoldAsString = "True";
			appearance11.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance11;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(8, 210);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(62, 15);
			ultraFormattedLinkLabel6.TabIndex = 76;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Employee:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance13;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(8, 231);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(86, 15);
			ultraFormattedLinkLabel7.TabIndex = 133;
			ultraFormattedLinkLabel7.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabel7, "Company Division");
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Comp.Division:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			textBoxCompanyDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCompanyDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCompanyDivision.CustomReportFieldName = "";
			textBoxCompanyDivision.CustomReportKey = "";
			textBoxCompanyDivision.CustomReportValueType = 1;
			textBoxCompanyDivision.IsComboTextBox = false;
			textBoxCompanyDivision.IsModified = false;
			textBoxCompanyDivision.Location = new System.Drawing.Point(213, 229);
			textBoxCompanyDivision.MaxLength = 15;
			textBoxCompanyDivision.Name = "textBoxCompanyDivision";
			textBoxCompanyDivision.ReadOnly = true;
			textBoxCompanyDivision.Size = new System.Drawing.Size(248, 20);
			textBoxCompanyDivision.TabIndex = 132;
			textBoxCompanyDivision.TabStop = false;
			comboBoxCompanyDivision.Assigned = false;
			comboBoxCompanyDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCompanyDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCompanyDivision.CustomReportFieldName = "";
			comboBoxCompanyDivision.CustomReportKey = "";
			comboBoxCompanyDivision.CustomReportValueType = 1;
			comboBoxCompanyDivision.DescriptionTextBox = textBoxCompanyDivision;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCompanyDivision.DisplayLayout.Appearance = appearance15;
			comboBoxCompanyDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCompanyDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxCompanyDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCompanyDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCompanyDivision.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxCompanyDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCompanyDivision.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxCompanyDivision.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxCompanyDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCompanyDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxCompanyDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCompanyDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCompanyDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCompanyDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCompanyDivision.Editable = true;
			comboBoxCompanyDivision.FilterString = "";
			comboBoxCompanyDivision.HasAllAccount = false;
			comboBoxCompanyDivision.HasCustom = false;
			comboBoxCompanyDivision.IsDataLoaded = false;
			comboBoxCompanyDivision.Location = new System.Drawing.Point(102, 229);
			comboBoxCompanyDivision.MaxDropDownItems = 12;
			comboBoxCompanyDivision.Name = "comboBoxCompanyDivision";
			comboBoxCompanyDivision.ShowInactiveItems = false;
			comboBoxCompanyDivision.ShowQuickAdd = true;
			comboBoxCompanyDivision.Size = new System.Drawing.Size(111, 20);
			comboBoxCompanyDivision.TabIndex = 9;
			comboBoxCompanyDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance27;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(103, 207);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = false;
			comboBoxEmployee.Size = new System.Drawing.Size(110, 20);
			comboBoxEmployee.TabIndex = 8;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(213, 207);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(248, 20);
			textBoxEmployeeName.TabIndex = 74;
			textBoxEmployeeName.TabStop = false;
			textBoxOpeningDepAmount.AllowDecimal = true;
			textBoxOpeningDepAmount.BackColor = System.Drawing.Color.White;
			textBoxOpeningDepAmount.CustomReportFieldName = "";
			textBoxOpeningDepAmount.CustomReportKey = "";
			textBoxOpeningDepAmount.CustomReportValueType = 1;
			textBoxOpeningDepAmount.IsComboTextBox = false;
			textBoxOpeningDepAmount.IsModified = false;
			textBoxOpeningDepAmount.Location = new System.Drawing.Point(493, 119);
			textBoxOpeningDepAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxOpeningDepAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxOpeningDepAmount.Name = "textBoxOpeningDepAmount";
			textBoxOpeningDepAmount.NullText = "0";
			textBoxOpeningDepAmount.Size = new System.Drawing.Size(132, 20);
			textBoxOpeningDepAmount.TabIndex = 11;
			textBoxOpeningDepAmount.Text = "0.00";
			textBoxOpeningDepAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOpeningDepAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(381, 121);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(107, 13);
			mmLabel9.TabIndex = 72;
			mmLabel9.Text = "Opening Accum Dep:";
			textBoxAccumDep.AllowDecimal = true;
			textBoxAccumDep.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccumDep.CustomReportFieldName = "";
			textBoxAccumDep.CustomReportKey = "";
			textBoxAccumDep.CustomReportValueType = 1;
			textBoxAccumDep.IsComboTextBox = false;
			textBoxAccumDep.IsModified = false;
			textBoxAccumDep.Location = new System.Drawing.Point(493, 141);
			textBoxAccumDep.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAccumDep.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAccumDep.Name = "textBoxAccumDep";
			textBoxAccumDep.NullText = "0";
			textBoxAccumDep.ReadOnly = true;
			textBoxAccumDep.Size = new System.Drawing.Size(132, 20);
			textBoxAccumDep.TabIndex = 12;
			textBoxAccumDep.Text = "0.00";
			textBoxAccumDep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAccumDep.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
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
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(635, 1);
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
			xpButton1.Location = new System.Drawing.Point(525, 8);
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
			textBoxSupplierName.BackColor = System.Drawing.Color.White;
			textBoxSupplierName.CustomReportFieldName = "";
			textBoxSupplierName.CustomReportKey = "";
			textBoxSupplierName.CustomReportValueType = 1;
			textBoxSupplierName.IsComboTextBox = false;
			textBoxSupplierName.IsModified = false;
			textBoxSupplierName.Location = new System.Drawing.Point(100, 54);
			textBoxSupplierName.MaxLength = 64;
			textBoxSupplierName.Name = "textBoxSupplierName";
			textBoxSupplierName.Size = new System.Drawing.Size(367, 20);
			textBoxSupplierName.TabIndex = 3;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 57);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(79, 13);
			mmLabel13.TabIndex = 62;
			mmLabel13.Text = "Supplier Name:";
			textBoxInvoiceNumber.BackColor = System.Drawing.Color.White;
			textBoxInvoiceNumber.CustomReportFieldName = "";
			textBoxInvoiceNumber.CustomReportKey = "";
			textBoxInvoiceNumber.CustomReportValueType = 1;
			textBoxInvoiceNumber.IsComboTextBox = false;
			textBoxInvoiceNumber.IsModified = false;
			textBoxInvoiceNumber.Location = new System.Drawing.Point(100, 31);
			textBoxInvoiceNumber.MaxLength = 30;
			textBoxInvoiceNumber.Name = "textBoxInvoiceNumber";
			textBoxInvoiceNumber.Size = new System.Drawing.Size(131, 20);
			textBoxInvoiceNumber.TabIndex = 1;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(9, 34);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(86, 13);
			mmLabel12.TabIndex = 60;
			mmLabel12.Text = "Invoice Number:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(9, 13);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(81, 13);
			mmLabel3.TabIndex = 48;
			mmLabel3.Text = "Purchase Date:";
			dateTimePickerPurchaseDate.Checked = false;
			dateTimePickerPurchaseDate.CustomFormat = " ";
			dateTimePickerPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerPurchaseDate.Location = new System.Drawing.Point(100, 8);
			dateTimePickerPurchaseDate.Name = "dateTimePickerPurchaseDate";
			dateTimePickerPurchaseDate.ShowCheckBox = true;
			dateTimePickerPurchaseDate.Size = new System.Drawing.Size(131, 20);
			dateTimePickerPurchaseDate.TabIndex = 0;
			dateTimePickerPurchaseDate.Value = new System.DateTime(0L);
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(237, 34);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(81, 13);
			mmLabel10.TabIndex = 48;
			mmLabel10.Text = "Purchase Price:";
			textBoxOriginalValue.AllowDecimal = true;
			textBoxOriginalValue.CustomReportFieldName = "";
			textBoxOriginalValue.CustomReportKey = "";
			textBoxOriginalValue.CustomReportValueType = 1;
			textBoxOriginalValue.IsComboTextBox = false;
			textBoxOriginalValue.IsModified = false;
			textBoxOriginalValue.Location = new System.Drawing.Point(325, 31);
			textBoxOriginalValue.MaxLength = 20;
			textBoxOriginalValue.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxOriginalValue.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxOriginalValue.Name = "textBoxOriginalValue";
			textBoxOriginalValue.NullText = "0";
			textBoxOriginalValue.Size = new System.Drawing.Size(142, 20);
			textBoxOriginalValue.TabIndex = 2;
			textBoxOriginalValue.Text = "0.00";
			textBoxOriginalValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOriginalValue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			numberTextBox1.AllowDecimal = true;
			numberTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			numberTextBox1.CustomReportFieldName = "";
			numberTextBox1.CustomReportKey = "";
			numberTextBox1.CustomReportValueType = 1;
			numberTextBox1.IsComboTextBox = false;
			numberTextBox1.IsModified = false;
			numberTextBox1.Location = new System.Drawing.Point(499, 13);
			numberTextBox1.MaxValue = new decimal(new int[4]
			{
				1000,
				0,
				0,
				0
			});
			numberTextBox1.MinValue = new decimal(new int[4]);
			numberTextBox1.Name = "numberTextBox1";
			numberTextBox1.NullText = "0";
			numberTextBox1.ReadOnly = true;
			numberTextBox1.Size = new System.Drawing.Size(70, 20);
			numberTextBox1.TabIndex = 5;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(575, 16);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(34, 13);
			mmLabel5.TabIndex = 69;
			mmLabel5.Text = "Years";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(441, 17);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(42, 13);
			mmLabel2.TabIndex = 69;
			mmLabel2.Text = "Months";
			textBoxDepreciationPerc.CustomReportFieldName = "";
			textBoxDepreciationPerc.CustomReportKey = "";
			textBoxDepreciationPerc.CustomReportValueType = 1;
			textBoxDepreciationPerc.IsComboTextBox = false;
			textBoxDepreciationPerc.IsModified = false;
			textBoxDepreciationPerc.Location = new System.Drawing.Point(144, 60);
			textBoxDepreciationPerc.Name = "textBoxDepreciationPerc";
			textBoxDepreciationPerc.Size = new System.Drawing.Size(67, 20);
			textBoxDepreciationPerc.TabIndex = 2;
			textBoxDepreciationPerc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxDepMethod.FormattingEnabled = true;
			comboBoxDepMethod.Location = new System.Drawing.Point(144, 12);
			comboBoxDepMethod.Name = "comboBoxDepMethod";
			comboBoxDepMethod.Size = new System.Drawing.Size(139, 21);
			comboBoxDepMethod.TabIndex = 0;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(13, 14);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(110, 13);
			mmLabel20.TabIndex = 61;
			mmLabel20.Text = "Depreciation Method:";
			textBoxLife.AllowDecimal = true;
			textBoxLife.CustomReportFieldName = "";
			textBoxLife.CustomReportKey = "";
			textBoxLife.CustomReportValueType = 1;
			textBoxLife.IsComboTextBox = false;
			textBoxLife.IsModified = false;
			textBoxLife.Location = new System.Drawing.Point(367, 14);
			textBoxLife.MaxValue = new decimal(new int[4]
			{
				5000,
				0,
				0,
				0
			});
			textBoxLife.MinValue = new decimal(new int[4]);
			textBoxLife.Name = "textBoxLife";
			textBoxLife.NullText = "0";
			textBoxLife.Size = new System.Drawing.Size(70, 20);
			textBoxLife.TabIndex = 4;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(213, 63);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(18, 13);
			mmLabel8.TabIndex = 54;
			mmLabel8.Text = "%";
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(13, 63);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(111, 13);
			mmLabel6.TabIndex = 54;
			mmLabel6.Text = "Depreciation Percent:";
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(13, 39);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(98, 13);
			mmLabel22.TabIndex = 54;
			mmLabel22.Text = "Depreciation Start:";
			textBoxSalvage.AllowDecimal = true;
			textBoxSalvage.CustomReportFieldName = "";
			textBoxSalvage.CustomReportKey = "";
			textBoxSalvage.CustomReportValueType = 1;
			textBoxSalvage.IsComboTextBox = false;
			textBoxSalvage.IsModified = false;
			textBoxSalvage.Location = new System.Drawing.Point(144, 83);
			textBoxSalvage.MaxLength = 20;
			textBoxSalvage.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSalvage.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSalvage.Name = "textBoxSalvage";
			textBoxSalvage.NullText = "0";
			textBoxSalvage.Size = new System.Drawing.Size(139, 20);
			textBoxSalvage.TabIndex = 3;
			textBoxSalvage.Text = "0.00";
			textBoxSalvage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSalvage.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(303, 17);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(58, 13);
			mmLabel7.TabIndex = 48;
			mmLabel7.Text = "Asset Life:";
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(13, 86);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(78, 13);
			mmLabel11.TabIndex = 48;
			mmLabel11.Text = "Salvage Value:";
			textBoxModel.BackColor = System.Drawing.Color.White;
			textBoxModel.CustomReportFieldName = "";
			textBoxModel.CustomReportKey = "";
			textBoxModel.CustomReportValueType = 1;
			textBoxModel.IsComboTextBox = false;
			textBoxModel.IsModified = false;
			textBoxModel.Location = new System.Drawing.Point(111, 59);
			textBoxModel.MaxLength = 30;
			textBoxModel.Name = "textBoxModel";
			textBoxModel.Size = new System.Drawing.Size(305, 20);
			textBoxModel.TabIndex = 2;
			textBoxBarcode.BackColor = System.Drawing.Color.White;
			textBoxBarcode.CustomReportFieldName = "";
			textBoxBarcode.CustomReportKey = "";
			textBoxBarcode.CustomReportValueType = 1;
			textBoxBarcode.IsComboTextBox = false;
			textBoxBarcode.IsModified = false;
			textBoxBarcode.Location = new System.Drawing.Point(111, 37);
			textBoxBarcode.MaxLength = 30;
			textBoxBarcode.Name = "textBoxBarcode";
			textBoxBarcode.Size = new System.Drawing.Size(305, 20);
			textBoxBarcode.TabIndex = 1;
			textBoxSerial.BackColor = System.Drawing.Color.White;
			textBoxSerial.CustomReportFieldName = "";
			textBoxSerial.CustomReportKey = "";
			textBoxSerial.CustomReportValueType = 1;
			textBoxSerial.IsComboTextBox = false;
			textBoxSerial.IsModified = false;
			textBoxSerial.Location = new System.Drawing.Point(111, 14);
			textBoxSerial.MaxLength = 30;
			textBoxSerial.Name = "textBoxSerial";
			textBoxSerial.Size = new System.Drawing.Size(305, 20);
			textBoxSerial.TabIndex = 0;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(16, 63);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(79, 13);
			mmLabel19.TabIndex = 63;
			mmLabel19.Text = "Model Number:";
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(15, 40);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(90, 13);
			mmLabel18.TabIndex = 61;
			mmLabel18.Text = "Barcode Number:";
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(15, 17);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(77, 13);
			mmLabel17.TabIndex = 59;
			mmLabel17.Text = "Serial Number:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(13, 15);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(579, 135);
			textBoxNote.TabIndex = 0;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(381, 145);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(94, 13);
			mmLabel16.TabIndex = 52;
			mmLabel16.Text = "Accumulated Dep:";
			textBoxBookValue.AllowDecimal = true;
			textBoxBookValue.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBookValue.CustomReportFieldName = "";
			textBoxBookValue.CustomReportKey = "";
			textBoxBookValue.CustomReportValueType = 1;
			textBoxBookValue.IsComboTextBox = false;
			textBoxBookValue.IsModified = false;
			textBoxBookValue.Location = new System.Drawing.Point(493, 165);
			textBoxBookValue.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBookValue.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBookValue.Name = "textBoxBookValue";
			textBoxBookValue.NullText = "0";
			textBoxBookValue.ReadOnly = true;
			textBoxBookValue.Size = new System.Drawing.Size(132, 20);
			textBoxBookValue.TabIndex = 13;
			textBoxBookValue.Text = "0.00";
			textBoxBookValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBookValue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(383, 166);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(63, 13);
			mmLabel23.TabIndex = 65;
			mmLabel23.Text = "Book Value:";
			textBoxAquesitionCost.AllowDecimal = true;
			textBoxAquesitionCost.CustomReportFieldName = "";
			textBoxAquesitionCost.CustomReportKey = "";
			textBoxAquesitionCost.CustomReportValueType = 1;
			textBoxAquesitionCost.IsComboTextBox = false;
			textBoxAquesitionCost.IsModified = false;
			textBoxAquesitionCost.Location = new System.Drawing.Point(493, 97);
			textBoxAquesitionCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAquesitionCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAquesitionCost.Name = "textBoxAquesitionCost";
			textBoxAquesitionCost.NullText = "0";
			textBoxAquesitionCost.Size = new System.Drawing.Size(132, 20);
			textBoxAquesitionCost.TabIndex = 10;
			textBoxAquesitionCost.Text = "0.00";
			textBoxAquesitionCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAquesitionCost.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(384, 100);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(86, 13);
			mmLabel4.TabIndex = 65;
			mmLabel4.Text = "Aquesition Cost:";
			textBoxDepName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDepName.CustomReportFieldName = "";
			textBoxDepName.CustomReportKey = "";
			textBoxDepName.CustomReportValueType = 1;
			textBoxDepName.IsComboTextBox = true;
			textBoxDepName.IsModified = false;
			textBoxDepName.Location = new System.Drawing.Point(213, 185);
			textBoxDepName.MaxLength = 64;
			textBoxDepName.Name = "textBoxDepName";
			textBoxDepName.ReadOnly = true;
			textBoxDepName.Size = new System.Drawing.Size(167, 20);
			textBoxDepName.TabIndex = 63;
			textBoxDepName.TabStop = false;
			textBoxDivisionName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDivisionName.CustomReportFieldName = "";
			textBoxDivisionName.CustomReportKey = "";
			textBoxDivisionName.CustomReportValueType = 1;
			textBoxDivisionName.IsComboTextBox = true;
			textBoxDivisionName.IsModified = false;
			textBoxDivisionName.Location = new System.Drawing.Point(213, 163);
			textBoxDivisionName.MaxLength = 64;
			textBoxDivisionName.Name = "textBoxDivisionName";
			textBoxDivisionName.ReadOnly = true;
			textBoxDivisionName.Size = new System.Drawing.Size(167, 20);
			textBoxDivisionName.TabIndex = 63;
			textBoxDivisionName.TabStop = false;
			textBoxAssetLocationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetLocationName.CustomReportFieldName = "";
			textBoxAssetLocationName.CustomReportKey = "";
			textBoxAssetLocationName.CustomReportValueType = 1;
			textBoxAssetLocationName.IsComboTextBox = true;
			textBoxAssetLocationName.IsModified = false;
			textBoxAssetLocationName.Location = new System.Drawing.Point(213, 141);
			textBoxAssetLocationName.MaxLength = 64;
			textBoxAssetLocationName.Name = "textBoxAssetLocationName";
			textBoxAssetLocationName.ReadOnly = true;
			textBoxAssetLocationName.Size = new System.Drawing.Size(167, 20);
			textBoxAssetLocationName.TabIndex = 63;
			textBoxAssetLocationName.TabStop = false;
			textBoxAssetGroupName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetGroupName.CustomReportFieldName = "";
			textBoxAssetGroupName.CustomReportKey = "";
			textBoxAssetGroupName.CustomReportValueType = 1;
			textBoxAssetGroupName.IsComboTextBox = true;
			textBoxAssetGroupName.IsModified = false;
			textBoxAssetGroupName.Location = new System.Drawing.Point(213, 119);
			textBoxAssetGroupName.MaxLength = 64;
			textBoxAssetGroupName.Name = "textBoxAssetGroupName";
			textBoxAssetGroupName.ReadOnly = true;
			textBoxAssetGroupName.Size = new System.Drawing.Size(167, 20);
			textBoxAssetGroupName.TabIndex = 63;
			textBoxAssetGroupName.TabStop = false;
			textBoxAssetClassName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetClassName.CustomReportFieldName = "";
			textBoxAssetClassName.CustomReportKey = "";
			textBoxAssetClassName.CustomReportValueType = 1;
			textBoxAssetClassName.IsComboTextBox = true;
			textBoxAssetClassName.IsModified = false;
			textBoxAssetClassName.Location = new System.Drawing.Point(213, 97);
			textBoxAssetClassName.MaxLength = 64;
			textBoxAssetClassName.Name = "textBoxAssetClassName";
			textBoxAssetClassName.ReadOnly = true;
			textBoxAssetClassName.Size = new System.Drawing.Size(167, 20);
			textBoxAssetClassName.TabIndex = 63;
			textBoxAssetClassName.TabStop = false;
			textBoxAssetClassName.TextChanged += new System.EventHandler(textBoxAssetClassName_TextChanged);
			comboBoxClass.Assigned = false;
			comboBoxClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxClass.CustomReportFieldName = "";
			comboBoxClass.CustomReportKey = "";
			comboBoxClass.CustomReportValueType = 1;
			comboBoxClass.DescriptionTextBox = textBoxAssetClassName;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxClass.DisplayLayout.Appearance = appearance39;
			comboBoxClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxClass.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxClass.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxClass.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxClass.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxClass.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxClass.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxClass.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxClass.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxClass.Editable = true;
			comboBoxClass.FilterString = "";
			comboBoxClass.HasAllAccount = false;
			comboBoxClass.HasCustom = false;
			comboBoxClass.IsDataLoaded = false;
			comboBoxClass.Location = new System.Drawing.Point(103, 97);
			comboBoxClass.MaxDropDownItems = 12;
			comboBoxClass.Name = "comboBoxClass";
			comboBoxClass.ShowInactiveItems = true;
			comboBoxClass.ShowQuickAdd = true;
			comboBoxClass.Size = new System.Drawing.Size(110, 20);
			comboBoxClass.TabIndex = 3;
			comboBoxClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxClass.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxClass_InitializeLayout);
			comboBoxDepartment.Assigned = false;
			comboBoxDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDepartment.CustomReportFieldName = "";
			comboBoxDepartment.CustomReportKey = "";
			comboBoxDepartment.CustomReportValueType = 1;
			comboBoxDepartment.DescriptionTextBox = textBoxDepName;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDepartment.DisplayLayout.Appearance = appearance51;
			comboBoxDepartment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDepartment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxDepartment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartment.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxDepartment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDepartment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDepartment.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDepartment.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxDepartment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDepartment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDepartment.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxDepartment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDepartment.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxDepartment.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxDepartment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDepartment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxDepartment.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxDepartment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDepartment.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			comboBoxDepartment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDepartment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDepartment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDepartment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDepartment.Editable = true;
			comboBoxDepartment.FilterString = "";
			comboBoxDepartment.HasAllAccount = false;
			comboBoxDepartment.HasCustom = false;
			comboBoxDepartment.IsDataLoaded = false;
			comboBoxDepartment.Location = new System.Drawing.Point(103, 185);
			comboBoxDepartment.MaxDropDownItems = 12;
			comboBoxDepartment.Name = "comboBoxDepartment";
			comboBoxDepartment.ShowInactiveItems = false;
			comboBoxDepartment.ShowQuickAdd = true;
			comboBoxDepartment.Size = new System.Drawing.Size(110, 20);
			comboBoxDepartment.TabIndex = 7;
			comboBoxDepartment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDivision.Assigned = false;
			comboBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDivision.CustomReportFieldName = "";
			comboBoxDivision.CustomReportKey = "";
			comboBoxDivision.CustomReportValueType = 1;
			comboBoxDivision.DescriptionTextBox = textBoxDivisionName;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDivision.DisplayLayout.Appearance = appearance63;
			comboBoxDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDivision.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDivision.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDivision.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDivision.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxDivision.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxDivision.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDivision.Editable = true;
			comboBoxDivision.FilterString = "";
			comboBoxDivision.HasAllAccount = false;
			comboBoxDivision.HasCustom = false;
			comboBoxDivision.IsDataLoaded = false;
			comboBoxDivision.Location = new System.Drawing.Point(103, 163);
			comboBoxDivision.MaxDropDownItems = 12;
			comboBoxDivision.Name = "comboBoxDivision";
			comboBoxDivision.ShowInactiveItems = false;
			comboBoxDivision.ShowQuickAdd = true;
			comboBoxDivision.Size = new System.Drawing.Size(110, 20);
			comboBoxDivision.TabIndex = 6;
			comboBoxDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = textBoxAssetLocationName;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance75;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(103, 141);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowInactiveItems = true;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.Size = new System.Drawing.Size(110, 20);
			comboBoxLocation.TabIndex = 5;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGroup.Assigned = false;
			comboBoxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGroup.CustomReportFieldName = "";
			comboBoxGroup.CustomReportKey = "";
			comboBoxGroup.CustomReportValueType = 1;
			comboBoxGroup.DescriptionTextBox = textBoxAssetGroupName;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGroup.DisplayLayout.Appearance = appearance87;
			comboBoxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.GroupByBox.Appearance = appearance88;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance89;
			comboBoxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance90.BackColor2 = System.Drawing.SystemColors.Control;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance90;
			comboBoxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance91;
			appearance92.BackColor = System.Drawing.SystemColors.Highlight;
			appearance92.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance92;
			comboBoxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.CardAreaAppearance = appearance93;
			appearance94.BorderColor = System.Drawing.Color.Silver;
			appearance94.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGroup.DisplayLayout.Override.CellAppearance = appearance94;
			comboBoxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance95.BackColor = System.Drawing.SystemColors.Control;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance95;
			appearance96.TextHAlignAsString = "Left";
			comboBoxGroup.DisplayLayout.Override.HeaderAppearance = appearance96;
			comboBoxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			comboBoxGroup.DisplayLayout.Override.RowAppearance = appearance97;
			comboBoxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance98;
			comboBoxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGroup.Editable = true;
			comboBoxGroup.FilterString = "";
			comboBoxGroup.HasAllAccount = false;
			comboBoxGroup.HasCustom = false;
			comboBoxGroup.IsDataLoaded = false;
			comboBoxGroup.Location = new System.Drawing.Point(103, 119);
			comboBoxGroup.MaxDropDownItems = 12;
			comboBoxGroup.Name = "comboBoxGroup";
			comboBoxGroup.ShowInactiveItems = true;
			comboBoxGroup.ShowQuickAdd = true;
			comboBoxGroup.Size = new System.Drawing.Size(110, 20);
			comboBoxGroup.TabIndex = 4;
			comboBoxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(102, 59);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(503, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(102, 37);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(197, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(78, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Asset Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 38);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(75, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Asset Code:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(635, 519);
			base.Controls.Add(ultraFormattedLinkLabel7);
			base.Controls.Add(textBoxCompanyDivision);
			base.Controls.Add(comboBoxCompanyDivision);
			base.Controls.Add(ultraFormattedLinkLabel6);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(textBoxOpeningDepAmount);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(textBoxAccumDep);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(mmLabel16);
			base.Controls.Add(textBoxBookValue);
			base.Controls.Add(mmLabel23);
			base.Controls.Add(textBoxAquesitionCost);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(textBoxDepName);
			base.Controls.Add(textBoxDivisionName);
			base.Controls.Add(textBoxAssetLocationName);
			base.Controls.Add(textBoxAssetGroupName);
			base.Controls.Add(textBoxAssetClassName);
			base.Controls.Add(comboBoxClass);
			base.Controls.Add(comboBoxDepartment);
			base.Controls.Add(comboBoxDivision);
			base.Controls.Add(comboBoxLocation);
			base.Controls.Add(comboBoxGroup);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "FixedAssetDetailsForm";
			Text = "Fixed Asset Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
