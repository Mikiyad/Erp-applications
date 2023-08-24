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
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyDetailsForm : Form, IForm
	{
		private PropertyData currentData;

		private const string TABLENAME_CONST = "Property";

		private const string IDFIELD_CONST = "PropertyID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private FormManager formManager;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private UltraGroupBox ultraGroupBox6;

		private MMLabel mmLabel14;

		private MMTextBox textBoxRegisterNumber;

		private MMLabel mmLabel13;

		private MMLabel mmLabel12;

		private MMTextBox textBoxOwnerName;

		private MMTextBox textBoxBuiltBy;

		private MMLabel mmLabel11;

		private MMLabel mmLabel8;

		private MMLabel mmLabel10;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel6;

		private MMTextBox textBoxAddress1;

		private MMTextBox textBoxAddress2;

		private MMLabel mmLabel9;

		private ComboBox comboBoxOfferType;

		private MMLabel mmLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private UltraFormattedLinkLabel linkLabelArea;

		private UltraFormattedLinkLabel linkLabelCountry;

		private UltraFormattedLinkLabel linkLabelCustomerClass;

		private AreaComboBox comboBoxArea;

		private CountryComboBox comboBoxCountry;

		private MMLabel mmLabel5;

		private MMLabel labelCustomerNumber;

		private MMTextBox textBoxShortName;

		private MMTextBox textBoxCode;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxName;

		private MMLabel label1;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private UDFEntryControl udfEntryGrid;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private UltraTabPageControl ultraTabPageControl2;

		private Panel panel1;

		private MMLabel labelCustomerNameHeader;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMLabel mmLabel3;

		private DataEntryGrid dataGridPropertyUnits;

		private MMLabel mmLabel2;

		private DataEntryGrid dataGridFacility;

		private MMTextBox textBoxPrepaidRentalIncomeAccount;

		private MMTextBox textBoxRentalIncomeAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private PropertyClassComboBox ComboBoxPropertyClass;

		private AmountTextBox textBoxBuildArea;

		private AmountTextBox textBoxLandArea;

		private ComboBox comboBoxYearBuilt;

		private FixedAssetsComboBox comboBoxFixedAsset;

		private AllAccountsComboBox comboBoxRentalIncomeAccount;

		private CityComboBox comboBoxCity;

		private AllAccountsComboBox comboBoxPrepaidRentalIncomeAccount;

		private PropertyUnitComboBox comboBoxGridPropertyUnit;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonComments;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonInformation;

		private Panel panel2;

		private MMLabel mmLabel15;

		private Panel panel3;

		private MMLabel mmLabel16;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private LocationComboBox comboBoxLocation;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraGroupBox ultraGroupBox7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel4;

		private ComboBox comboBoxTaxOption;

		private MMTextBox textBoxTaxIDNumber;

		private MMLabel mmLabel58;

		private OpenFileDialog openFileDialog1;

		private PictureBox pictureBoxNoImage;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem importToolStripMenuItem;

		private ToolStripButton toolStripButtonShowPicture;

		private UltraTabPageControl ultraTabPageControl3;

		private MMLabel mmLabel18;

		private MMTextBox textBoxMunicipalityRegnNo;

		private MMLabel mmLabel17;

		private MMTextBox textBoxTelecomRegnNo;

		private MMLabel mmLabel7;

		private MMTextBox textBoxElectricityRegnNo;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private MMLabel mmLabel20;

		private MMTextBox textBoxElectricityContractNo;

		private MMLabel mmLabel19;

		private MMTextBox textBoxElectricityPremiseNo;

		private MMLabel mmLabel22;

		private MMLabel mmLabel23;

		private UltraTabControl ultraTabControl2;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private DataEntryGrid dataEntryGridOwner;

		private UltraTabPageControl ultraTabPageControl4;

		private GenericListComboBox ComboBoxPropertyOwner;

		private CheckBox checkBoxPeriodicInvoice;

		public ScreenAreas ScreenArea => ScreenAreas.PropertyRental;

		public int ScreenID => 6001;

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
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkRemovePicture;
					bool flag2 = linkAddPicture.Enabled = false;
					bool visible = ultraFormattedLinkLabel2.Enabled = flag2;
					ultraFormattedLinkLabel.Visible = visible;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					linkAddPicture.Enabled = true;
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

		public PropertyDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxYearBuilt.SelectedIndex = checked(DateTime.Today.Year - 2000);
			comboBoxYearBuilt.DropDownHeight = 100;
			comboBoxOfferType.SelectedIndex = 0;
			dataEntryGridOwner.AfterCellUpdate += dataEntryGridOwner_AfterCellUpdate;
		}

		private void AddEvents()
		{
			base.Load += PropertyDetailsForm_Load;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PropertyData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PropertyTable.Rows[0] : currentData.PropertyTable.NewRow();
				dataRow.BeginEdit();
				dataRow["PropertyID"] = textBoxCode.Text.Trim();
				dataRow["PropertyName"] = textBoxName.Text.Trim();
				dataRow["ShortName"] = textBoxShortName.Text.Trim();
				dataRow["OfferTypeID"] = checked(comboBoxOfferType.SelectedIndex + 1);
				if (comboBoxFixedAsset.SelectedID != "")
				{
					dataRow["FixedAssetID"] = comboBoxFixedAsset.SelectedID;
				}
				else
				{
					dataRow["FixedAssetID"] = DBNull.Value;
				}
				if (ComboBoxPropertyClass.SelectedID != "")
				{
					dataRow["PropertyClassID"] = ComboBoxPropertyClass.SelectedID;
				}
				else
				{
					dataRow["PropertyClassID"] = DBNull.Value;
				}
				if (comboBoxCountry.SelectedID != "")
				{
					dataRow["CountryID"] = comboBoxCountry.SelectedID;
				}
				else
				{
					dataRow["CountryID"] = DBNull.Value;
				}
				if (comboBoxCity.SelectedID != "")
				{
					dataRow["CityID"] = comboBoxCity.SelectedID;
				}
				else
				{
					dataRow["CityID"] = DBNull.Value;
				}
				if (comboBoxArea.SelectedID != "")
				{
					dataRow["AreaID"] = comboBoxArea.SelectedID;
				}
				else
				{
					dataRow["AreaID"] = DBNull.Value;
				}
				dataRow["ElectricityRegnNumber"] = textBoxElectricityRegnNo.Text.Trim();
				dataRow["MunicipalityRegnNumber"] = textBoxMunicipalityRegnNo.Text.Trim();
				dataRow["TelecomRegnNumber"] = textBoxTelecomRegnNo.Text.Trim();
				dataRow["ElectricityPremiseNumber"] = textBoxElectricityPremiseNo.Text.Trim();
				dataRow["ElectricityContractNumber"] = textBoxElectricityContractNo.Text.Trim();
				dataRow["Address1"] = textBoxAddress1.Text.Trim();
				dataRow["Address2"] = textBoxAddress2.Text.Trim();
				if (comboBoxYearBuilt.SelectedItem != null)
				{
					dataRow["YearBuilt"] = int.Parse(comboBoxYearBuilt.SelectedItem.ToString());
				}
				else
				{
					dataRow["YearBuilt"] = DBNull.Value;
				}
				dataRow["Builtby"] = textBoxBuiltBy.Text.Trim();
				dataRow["LandArea"] = textBoxLandArea.Text.Trim();
				dataRow["BuildArea"] = textBoxBuildArea.Text.Trim();
				dataRow["OwnerName"] = textBoxOwnerName.Text.Trim();
				dataRow["RegisterNumber"] = textBoxRegisterNumber.Text.Trim();
				dataRow["RentIncomeAccountID"] = comboBoxRentalIncomeAccount.SelectedID;
				dataRow["PrepaidRentIncomeAccountID"] = comboBoxPrepaidRentalIncomeAccount.SelectedID;
				dataRow["Status"] = checkBoxIsInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow["TaxIDNumber"] = textBoxTaxIDNumber.Text;
				dataRow["IsPeriodicInvoice"] = checkBoxPeriodicInvoice.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PropertyTable.Rows.Add(dataRow);
				}
				currentData.PropertyFacilityTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridFacility.Rows)
				{
					if (bool.Parse(row.Cells["R"].Value.ToString()))
					{
						bool flag = false;
						if (bool.Parse(row.Cells["R"].Value.ToString()))
						{
							flag = true;
						}
						if (flag)
						{
							DataRow dataRow2 = currentData.Tables["Property_Facility"].NewRow();
							dataRow2.BeginEdit();
							dataRow2["FacilityID"] = row.Cells["FacilityCode"].Value.ToString();
							dataRow2["PropertyID"] = textBoxCode.Text.Trim();
							dataRow2["Type"] = 1;
							dataRow2.EndEdit();
							currentData.Tables["Property_Facility"].Rows.Add(dataRow2);
						}
					}
				}
				currentData.PropertyOwnerDetailTable.Rows.Clear();
				foreach (UltraGridRow row2 in dataEntryGridOwner.Rows)
				{
					DataRow dataRow3 = currentData.PropertyOwnerDetailTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["PropertyID"] = textBoxCode.Text.Trim();
					dataRow3["PropertyOwnerID"] = row2.Cells["Owner"].Value.ToString();
					dataRow3["Description"] = row2.Cells["Description"].Value.ToString();
					dataRow3["OwnerShipPercent"] = row2.Cells["OwnerShipPercent"].Value.ToString();
					dataRow3["RowIndex"] = row2.Index;
					currentData.PropertyOwnerDetailTable.Rows.Add(dataRow3);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		private void dataEntryGridOwner_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataEntryGridOwner.ActiveRow != null && e.Cell.Column.Key == "Owner")
				{
					dataEntryGridOwner.ActiveRow.Cells["NAme"].Value = ComboBoxPropertyOwner.SelectedName;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.PropertySystem.GetPropertyByID(id.Trim());
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
						if (toolStripButtonShowPicture.Checked && linkLoadImage.Visible)
						{
							LoadPhoto();
						}
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
			textBoxCode.Text = dataRow["PropertyID"].ToString();
			textBoxName.Text = dataRow["PropertyName"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			textBoxShortName.Text = dataRow["ShortName"].ToString();
			if (dataRow["OfferTypeID"] != DBNull.Value)
			{
				comboBoxOfferType.SelectedIndex = checked(int.Parse(dataRow["OfferTypeID"].ToString()) - 1);
			}
			else
			{
				comboBoxOfferType.SelectedIndex = 0;
			}
			if (dataRow["FixedAssetID"] != DBNull.Value)
			{
				comboBoxFixedAsset.SelectedID = dataRow["FixedAssetID"].ToString();
			}
			if (dataRow["PropertyClassID"] != DBNull.Value)
			{
				ComboBoxPropertyClass.SelectedID = dataRow["PropertyClassID"].ToString();
			}
			if (dataRow["CountryID"] != DBNull.Value)
			{
				comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
			}
			if (dataRow["LocationID"] != DBNull.Value)
			{
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
			}
			if (dataRow["CityID"] != DBNull.Value)
			{
				comboBoxCity.SelectedID = dataRow["CityID"].ToString();
			}
			if (dataRow["AreaID"] != DBNull.Value)
			{
				comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
			}
			textBoxElectricityRegnNo.Text = dataRow["ElectricityRegnNumber"].ToString();
			textBoxMunicipalityRegnNo.Text = dataRow["MunicipalityRegnNumber"].ToString();
			textBoxTelecomRegnNo.Text = dataRow["TelecomRegnNumber"].ToString();
			textBoxElectricityContractNo.Text = dataRow["ElectricityPremiseNumber"].ToString();
			textBoxElectricityPremiseNo.Text = dataRow["ElectricityContractNumber"].ToString();
			textBoxAddress1.Text = dataRow["Address1"].ToString();
			textBoxAddress2.Text = dataRow["Address2"].ToString();
			textBoxBuiltBy.Text = dataRow["Builtby"].ToString();
			textBoxLandArea.Text = dataRow["LandArea"].ToString();
			textBoxBuildArea.Text = dataRow["BuildArea"].ToString();
			textBoxOwnerName.Text = dataRow["OwnerName"].ToString();
			textBoxRegisterNumber.Text = dataRow["RegisterNumber"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			checkBoxIsInactive.Checked = bool.Parse(dataRow["Status"].ToString());
			if (!string.IsNullOrEmpty(dataRow["IsPeriodicInvoice"].ToString()))
			{
				checkBoxPeriodicInvoice.Checked = bool.Parse(dataRow["IsPeriodicInvoice"].ToString());
			}
			else
			{
				checkBoxPeriodicInvoice.Checked = false;
			}
			if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
			{
				comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
			}
			else
			{
				comboBoxTaxOption.SelectedIndex = 0;
			}
			if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
			{
				comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
			}
			else
			{
				comboBoxTaxGroup.Clear();
			}
			textBoxTaxIDNumber.Text = dataRow["TaxIDNumber"].ToString();
			if (dataRow["RentIncomeAccountID"] != DBNull.Value)
			{
				comboBoxRentalIncomeAccount.SelectedID = dataRow["RentIncomeAccountID"].ToString();
			}
			if (dataRow["PrepaidRentIncomeAccountID"] != DBNull.Value)
			{
				comboBoxPrepaidRentalIncomeAccount.SelectedID = dataRow["PrepaidRentIncomeAccountID"].ToString();
			}
			if (dataRow["YearBuilt"] != DBNull.Value)
			{
				comboBoxYearBuilt.SelectedItem = dataRow["YearBuilt"].ToString();
			}
			else
			{
				comboBoxYearBuilt.SelectedItem = null;
			}
			if (dataRow["HasPhoto"] != DBNull.Value)
			{
				bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
				linkLoadImage.Visible = flag;
				linkRemovePicture.Enabled = flag;
				if (flag)
				{
					pictureBoxPhoto.Image = null;
				}
				else
				{
					pictureBoxPhoto.Image = pictureBoxNoImage.Image;
				}
			}
			else
			{
				linkLoadImage.Visible = false;
				pictureBoxPhoto.Image = pictureBoxNoImage.Image;
				linkRemovePicture.Enabled = false;
			}
			DataTable dataTable = dataGridPropertyUnits.DataSource as DataTable;
			dataTable?.Rows.Clear();
			dataTable.BeginLoadData();
			foreach (DataRow row in currentData.PropertyUnitTable.Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["UnitCode"] = row["PropertyUnitID"];
				dataRow3["UnitName"] = row["PropertyUnitName"];
				dataRow3["AvailableFrom"] = row["AvailableFrom"];
				dataRow3["AvailableTo"] = row["AvailableTo"];
				dataRow3["PropertyName"] = row["PropertyName"];
				dataTable.Rows.Add(dataRow3);
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			if (currentData.Tables["UDF"].Rows.Count > 0)
			{
				_ = currentData.Tables["UDF"].Rows[0];
				foreach (DataColumn column in currentData.Tables["UDF"].Columns)
				{
					_ = (column.ColumnName == "EntityID");
				}
			}
			else
			{
				udfEntryGrid.ClearData();
			}
			DataTable dataTable2 = dataGridFacility.DataSource as DataTable;
			dataTable2.Rows.Clear();
			dataGridFacility.BeginUpdate();
			foreach (DataRow row2 in currentData.Tables["Property_Facility"].Rows)
			{
				DataRow dataRow5 = dataTable2.NewRow();
				dataRow5["FacilityCode"] = row2["FacilityID"];
				dataRow5["FacilityName"] = row2["FacilityName"].ToString();
				dataRow5["R"] = ((row2["Type"].ToString() != "0") ? true : false);
				dataRow5.EndEdit();
				dataTable2.Rows.Add(dataRow5);
			}
			dataTable2.AcceptChanges();
			dataGridFacility.EndUpdate();
			DataTable dataTable3 = dataEntryGridOwner.DataSource as DataTable;
			dataTable3.Rows.Clear();
			dataEntryGridOwner.BeginUpdate();
			foreach (DataRow row3 in currentData.Tables["Property_Owner_Detail"].Rows)
			{
				DataRow dataRow7 = dataTable3.NewRow();
				dataRow7["Owner"] = row3["PropertyOwnerID"];
				dataRow7["Name"] = row3["OwnerName"].ToString();
				dataRow7["Description"] = row3["Description"].ToString();
				dataRow7["OwnerShipPercent"] = row3["OwnerShipPercent"].ToString();
				dataRow7.EndEdit();
				dataTable3.Rows.Add(dataRow7);
			}
			dataTable3.AcceptChanges();
			dataEntryGridOwner.EndUpdate();
		}

		private void FillGrid()
		{
			try
			{
				new DataSet();
				DataSet genericListList = Factory.GenericListSystem.GetGenericListList(GenericListTypes.PropertyFacility);
				DataTable dataTable = dataGridFacility.DataSource as DataTable;
				dataTable.Rows.Clear();
				dataGridFacility.BeginUpdate();
				foreach (DataRow row in genericListList.Tables["Generic_List"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["FacilityCode"] = row["Code"].ToString();
					dataRow2["FacilityName"] = row["Name"].ToString();
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
				dataGridFacility.EndUpdate();
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.PropertySystem.CreateProperty(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Property, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.PropertySystem.UpdateProperty(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Property", "PropertyID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxOfferType.SelectedIndex == -1)
			{
				ErrorHelper.InformationMessage("Please select  offertype.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Property", "PropertyID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Property", "PropertyID");
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxShortName.Clear();
			comboBoxFixedAsset.Clear();
			ComboBoxPropertyClass.Clear();
			comboBoxCountry.Clear();
			comboBoxCity.Clear();
			comboBoxArea.Clear();
			textBoxAddress1.Clear();
			textBoxAddress2.Clear();
			textBoxBuiltBy.Clear();
			textBoxLandArea.Clear();
			textBoxBuildArea.Clear();
			textBoxOwnerName.Clear();
			textBoxRegisterNumber.Clear();
			comboBoxRentalIncomeAccount.Clear();
			comboBoxPrepaidRentalIncomeAccount.Clear();
			checkBoxIsInactive.Checked = false;
			textBoxNote.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxOfferType.SelectedIndex = 0;
			textBoxElectricityRegnNo.Clear();
			textBoxMunicipalityRegnNo.Clear();
			textBoxTelecomRegnNo.Clear();
			textBoxElectricityContractNo.Clear();
			textBoxElectricityPremiseNo.Clear();
			comboBoxLocation.Clear();
			textBoxTaxIDNumber.Clear();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			if (CompanyPreferences.TaxEntityTypes.Contains("Pr"))
			{
				comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
				comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
			}
			(dataGridPropertyUnits.DataSource as DataTable).Rows.Clear();
			(dataGridFacility.DataSource as DataTable).Rows.Clear();
			(dataEntryGridOwner.DataSource as DataTable).Rows.Clear();
			udfEntryGrid.ClearData();
			checkBoxPeriodicInvoice.Checked = false;
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			FillGrid();
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

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 1)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				bool num = Factory.PropertySystem.DeleteProperty(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Property, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Property", "PropertyID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Property", "PropertyID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Property", "PropertyID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Property", "PropertyID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Property", "PropertyID", toolStripTextBoxFind.Text.Trim()))
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

		private void PropertyDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridPropertyUnits.SetupUI();
				SetupPropertyUnitGrid();
				dataGridFacility.SetupUI();
				SetupFacilityGrid();
				dataEntryGridOwner.SetupUI();
				SetupOwnerGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					if (CompanyPreferences.TaxEntityTypes.Contains("Pr"))
					{
						comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
						comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
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
			new FormHelper().ShowList(DataComboType.Property);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxRentalIncomeAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAsset(comboBoxFixedAsset.SelectedID);
		}

		private void linkLabelCustomerClass_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyClass(ComboBoxPropertyClass.SelectedID);
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.SelectedID);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCity(comboBoxCity.SelectedID);
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditArea(comboBoxArea.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxPrepaidRentalIncomeAccount.SelectedID);
		}

		private void SetupPropertyUnitGrid()
		{
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("UnitCode");
			dataTable.Columns.Add("UnitName");
			dataTable.Columns.Add("PropertyName");
			dataTable.Columns.Add("AvailableFrom", typeof(DateTime));
			dataTable.Columns.Add("AvailableTo", typeof(DateTime));
			dataGridPropertyUnits.DataSource = dataTable;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["UnitCode"].CharacterCasing = CharacterCasing.Upper;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["UnitCode"].MaxLength = 64;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["UnitCode"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["UnitName"].CharacterCasing = CharacterCasing.Upper;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["UnitName"].MaxLength = 64;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["UnitName"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["PropertyName"].CharacterCasing = CharacterCasing.Upper;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["PropertyName"].MaxLength = 64;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["PropertyName"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["AvailableFrom"].CharacterCasing = CharacterCasing.Upper;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["AvailableFrom"].MaxLength = 64;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["AvailableFrom"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["AvailableTo"].CharacterCasing = CharacterCasing.Upper;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["AvailableTo"].MaxLength = 64;
			dataGridPropertyUnits.DisplayLayout.Bands[0].Columns["AvailableTo"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPropertyUnits.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
		}

		private void SetupFacilityGrid()
		{
			dataGridFacility.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("FacilityCode");
			dataTable.Columns.Add("FacilityName");
			dataTable.Columns.Add("R", typeof(bool)).DefaultValue = false;
			dataGridFacility.DataSource = dataTable;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityCode"].CharacterCasing = CharacterCasing.Upper;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityCode"].MaxLength = 64;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityCode"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityName"].CharacterCasing = CharacterCasing.Upper;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityName"].MaxLength = 64;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityName"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			FillGrid();
		}

		private void SetupOwnerGrid()
		{
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Owner");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("OwnerShipPercent", typeof(decimal));
			dataEntryGridOwner.DataSource = dataTable;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].MaxLength = 30;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].Header.Appearance.Cursor = Cursors.Hand;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].Header.Caption = "OwnerID";
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Owner"].ValueList = ComboBoxPropertyOwner;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.NoEdit;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Name"].TabStop = false;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Name"].MaxLength = 60;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 100;
			dataEntryGridOwner.DisplayLayout.Bands[0].Columns["OwnerShipPercent"].MaxLength = 20;
			dataEntryGridOwner.SetupUI();
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
					docManagementForm.EntityType = EntityTypesEnum.Properties;
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
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = "IMG_" + textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Properties;
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
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel22_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetPropertyThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddPropertyPhoto(textBoxCode.Text, image, isMatrixParent: false))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.PropertySystem.RemovePropertyPhoto(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void documentsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyDocumentsFormObj);
			FormActivator.PropertyDocumentsFormObj.LoadData(textBoxCode.Text);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(textBoxCode.Text == ""))
				{
					DataSet propertyReport = Factory.PropertySystem.GetPropertyReport(DateTime.Now, textBoxCode.Text, textBoxCode.Text, "", "");
					if (propertyReport == null || propertyReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(propertyReport, "", "Property", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
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
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyDetailsForm));
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ComboBoxPropertyOwner = new Micromind.DataControls.GenericListComboBox();
			dataEntryGridOwner = new Micromind.DataControls.DataEntryGrid();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxPeriodicInvoice = new System.Windows.Forms.CheckBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCity = new Micromind.DataControls.CityComboBox();
			comboBoxFixedAsset = new Micromind.DataControls.FixedAssetsComboBox();
			ComboBoxPropertyClass = new Micromind.DataControls.PropertyClassComboBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox6 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			textBoxTaxIDNumber = new Micromind.UISupport.MMTextBox();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			comboBoxPrepaidRentalIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxPrepaidRentalIncomeAccount = new Micromind.UISupport.MMTextBox();
			comboBoxRentalIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxRentalIncomeAccount = new Micromind.UISupport.MMTextBox();
			comboBoxYearBuilt = new System.Windows.Forms.ComboBox();
			textBoxBuildArea = new Micromind.UISupport.AmountTextBox();
			textBoxLandArea = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxRegisterNumber = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxOwnerName = new Micromind.UISupport.MMTextBox();
			textBoxBuiltBy = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			comboBoxOfferType = new System.Windows.Forms.ComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelArea = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelCustomerClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxArea = new Micromind.DataControls.AreaComboBox();
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelCustomerNumber = new Micromind.UISupport.MMLabel();
			textBoxShortName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxElectricityContractNo = new Micromind.UISupport.MMTextBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxElectricityPremiseNo = new Micromind.UISupport.MMTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxMunicipalityRegnNo = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxTelecomRegnNo = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxElectricityRegnNo = new Micromind.UISupport.MMTextBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxGridPropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			dataGridPropertyUnits = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dataGridFacility = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			panel2 = new System.Windows.Forms.Panel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
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
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			documentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonComments = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			panel3 = new System.Windows.Forms.Panel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxPropertyOwner).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridOwner).BeginInit();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCity).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFixedAsset).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxPropertyClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox6).BeginInit();
			ultraGroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPrepaidRentalIncomeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRentalIncomeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).BeginInit();
			ultraTabControl2.SuspendLayout();
			ultraTabSharedControlsPage2.SuspendLayout();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridPropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridPropertyUnits).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridFacility).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel3.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl4.Controls.Add(ComboBoxPropertyOwner);
			ultraTabPageControl4.Controls.Add(dataEntryGridOwner);
			ultraTabPageControl4.Location = new System.Drawing.Point(1, 23);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(828, 194);
			ComboBoxPropertyOwner.Assigned = false;
			ComboBoxPropertyOwner.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxPropertyOwner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxPropertyOwner.CustomReportFieldName = "";
			ComboBoxPropertyOwner.CustomReportKey = "";
			ComboBoxPropertyOwner.CustomReportValueType = 1;
			ComboBoxPropertyOwner.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxPropertyOwner.DisplayLayout.Appearance = appearance;
			ComboBoxPropertyOwner.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxPropertyOwner.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxPropertyOwner.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxPropertyOwner.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ComboBoxPropertyOwner.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxPropertyOwner.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ComboBoxPropertyOwner.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxPropertyOwner.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxPropertyOwner.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxPropertyOwner.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ComboBoxPropertyOwner.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxPropertyOwner.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxPropertyOwner.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxPropertyOwner.DisplayLayout.Override.CellAppearance = appearance8;
			ComboBoxPropertyOwner.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxPropertyOwner.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxPropertyOwner.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ComboBoxPropertyOwner.DisplayLayout.Override.HeaderAppearance = appearance10;
			ComboBoxPropertyOwner.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxPropertyOwner.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ComboBoxPropertyOwner.DisplayLayout.Override.RowAppearance = appearance11;
			ComboBoxPropertyOwner.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxPropertyOwner.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ComboBoxPropertyOwner.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxPropertyOwner.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxPropertyOwner.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxPropertyOwner.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxPropertyOwner.Editable = true;
			ComboBoxPropertyOwner.FilterString = "";
			ComboBoxPropertyOwner.GenericListType = Micromind.Common.Data.GenericListTypes.PropertyOwner;
			ComboBoxPropertyOwner.HasAllAccount = false;
			ComboBoxPropertyOwner.HasCustom = false;
			ComboBoxPropertyOwner.IsDataLoaded = false;
			ComboBoxPropertyOwner.IsSingleColumn = false;
			ComboBoxPropertyOwner.Location = new System.Drawing.Point(71, 46);
			ComboBoxPropertyOwner.MaxDropDownItems = 12;
			ComboBoxPropertyOwner.Name = "ComboBoxPropertyOwner";
			ComboBoxPropertyOwner.ShowInactiveItems = false;
			ComboBoxPropertyOwner.ShowQuickAdd = true;
			ComboBoxPropertyOwner.Size = new System.Drawing.Size(161, 20);
			ComboBoxPropertyOwner.TabIndex = 28;
			ComboBoxPropertyOwner.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxPropertyOwner.Visible = false;
			dataEntryGridOwner.AllowAddNew = false;
			dataEntryGridOwner.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridOwner.DisplayLayout.Appearance = appearance13;
			dataEntryGridOwner.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridOwner.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridOwner.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridOwner.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataEntryGridOwner.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridOwner.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataEntryGridOwner.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridOwner.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridOwner.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridOwner.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataEntryGridOwner.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridOwner.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridOwner.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridOwner.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridOwner.DisplayLayout.Override.CellAppearance = appearance20;
			dataEntryGridOwner.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridOwner.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridOwner.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataEntryGridOwner.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataEntryGridOwner.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridOwner.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridOwner.DisplayLayout.Override.RowAppearance = appearance23;
			dataEntryGridOwner.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridOwner.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataEntryGridOwner.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridOwner.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridOwner.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridOwner.IncludeLotItems = false;
			dataEntryGridOwner.LoadLayoutFailed = false;
			dataEntryGridOwner.Location = new System.Drawing.Point(3, 2);
			dataEntryGridOwner.Name = "dataEntryGridOwner";
			dataEntryGridOwner.ShowClearMenu = true;
			dataEntryGridOwner.ShowDeleteMenu = true;
			dataEntryGridOwner.ShowInsertMenu = true;
			dataEntryGridOwner.ShowMoveRowsMenu = true;
			dataEntryGridOwner.Size = new System.Drawing.Size(822, 189);
			dataEntryGridOwner.TabIndex = 27;
			tabPageGeneral.Controls.Add(checkBoxPeriodicInvoice);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(comboBoxLocation);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(comboBoxCity);
			tabPageGeneral.Controls.Add(comboBoxFixedAsset);
			tabPageGeneral.Controls.Add(ComboBoxPropertyClass);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel10);
			tabPageGeneral.Controls.Add(ultraGroupBox6);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(comboBoxOfferType);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel9);
			tabPageGeneral.Controls.Add(linkLabelArea);
			tabPageGeneral.Controls.Add(linkLabelCountry);
			tabPageGeneral.Controls.Add(linkLabelCustomerClass);
			tabPageGeneral.Controls.Add(comboBoxArea);
			tabPageGeneral.Controls.Add(comboBoxCountry);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(labelCustomerNumber);
			tabPageGeneral.Controls.Add(textBoxShortName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(855, 493);
			checkBoxPeriodicInvoice.BackColor = System.Drawing.Color.Transparent;
			checkBoxPeriodicInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxPeriodicInvoice.Location = new System.Drawing.Point(13, 460);
			checkBoxPeriodicInvoice.Name = "checkBoxPeriodicInvoice";
			checkBoxPeriodicInvoice.Size = new System.Drawing.Size(162, 29);
			checkBoxPeriodicInvoice.TabIndex = 119;
			checkBoxPeriodicInvoice.Text = "Periodic invoice";
			checkBoxPeriodicInvoice.UseVisualStyleBackColor = false;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(739, 251);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 117;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance25.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance25;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(700, 251);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 116;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance26;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(367, 134);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(49, 14);
			ultraFormattedLinkLabel3.TabIndex = 115;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Location:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance27;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance28;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance29;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance30;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance31.BackColor2 = System.Drawing.SystemColors.Control;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance31;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance32;
			appearance33.BackColor = System.Drawing.SystemColors.Highlight;
			appearance33.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance33;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance34;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			appearance35.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance35;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance36.BackColor = System.Drawing.SystemColors.Control;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance36;
			appearance37.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance37;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance38;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance39;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(467, 131);
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
			comboBoxLocation.Size = new System.Drawing.Size(157, 20);
			comboBoxLocation.TabIndex = 10;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(701, 84);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 12;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance40.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance40;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			comboBoxCity.Assigned = false;
			comboBoxCity.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCity.CustomReportFieldName = "";
			comboBoxCity.CustomReportKey = "";
			comboBoxCity.CustomReportValueType = 1;
			comboBoxCity.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCity.DisplayLayout.Appearance = appearance41;
			comboBoxCity.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCity.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCity.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCity.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxCity.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCity.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxCity.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCity.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCity.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCity.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxCity.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCity.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCity.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCity.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxCity.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCity.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCity.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxCity.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxCity.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCity.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxCity.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxCity.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCity.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxCity.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCity.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCity.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCity.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCity.Editable = true;
			comboBoxCity.FilterString = "";
			comboBoxCity.HasAllAccount = false;
			comboBoxCity.HasCustom = false;
			comboBoxCity.IsDataLoaded = false;
			comboBoxCity.Location = new System.Drawing.Point(467, 81);
			comboBoxCity.MaxDropDownItems = 12;
			comboBoxCity.Name = "comboBoxCity";
			comboBoxCity.ShowInactiveItems = false;
			comboBoxCity.ShowQuickAdd = true;
			comboBoxCity.Size = new System.Drawing.Size(158, 20);
			comboBoxCity.TabIndex = 8;
			comboBoxCity.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFixedAsset.Assigned = false;
			comboBoxFixedAsset.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFixedAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFixedAsset.CustomReportFieldName = "";
			comboBoxFixedAsset.CustomReportKey = "";
			comboBoxFixedAsset.CustomReportValueType = 1;
			comboBoxFixedAsset.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFixedAsset.DisplayLayout.Appearance = appearance53;
			comboBoxFixedAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFixedAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxFixedAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFixedAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFixedAsset.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFixedAsset.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxFixedAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFixedAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAsset.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFixedAsset.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxFixedAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFixedAsset.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAsset.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxFixedAsset.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxFixedAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFixedAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxFixedAsset.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxFixedAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFixedAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxFixedAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFixedAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFixedAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFixedAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFixedAsset.Editable = true;
			comboBoxFixedAsset.FilterString = "";
			comboBoxFixedAsset.HasAllAccount = false;
			comboBoxFixedAsset.HasCustom = false;
			comboBoxFixedAsset.IsDataLoaded = false;
			comboBoxFixedAsset.Location = new System.Drawing.Point(132, 106);
			comboBoxFixedAsset.MaxDropDownItems = 12;
			comboBoxFixedAsset.Name = "comboBoxFixedAsset";
			comboBoxFixedAsset.ShowInactiveItems = false;
			comboBoxFixedAsset.ShowQuickAdd = true;
			comboBoxFixedAsset.Size = new System.Drawing.Size(136, 20);
			comboBoxFixedAsset.TabIndex = 5;
			comboBoxFixedAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxPropertyClass.Assigned = false;
			ComboBoxPropertyClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxPropertyClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxPropertyClass.CustomReportFieldName = "";
			ComboBoxPropertyClass.CustomReportKey = "";
			ComboBoxPropertyClass.CustomReportValueType = 1;
			ComboBoxPropertyClass.DescriptionTextBox = null;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxPropertyClass.DisplayLayout.Appearance = appearance65;
			ComboBoxPropertyClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxPropertyClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxPropertyClass.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxPropertyClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			ComboBoxPropertyClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxPropertyClass.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			ComboBoxPropertyClass.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxPropertyClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxPropertyClass.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxPropertyClass.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			ComboBoxPropertyClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxPropertyClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxPropertyClass.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxPropertyClass.DisplayLayout.Override.CellAppearance = appearance72;
			ComboBoxPropertyClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxPropertyClass.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxPropertyClass.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			ComboBoxPropertyClass.DisplayLayout.Override.HeaderAppearance = appearance74;
			ComboBoxPropertyClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxPropertyClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			ComboBoxPropertyClass.DisplayLayout.Override.RowAppearance = appearance75;
			ComboBoxPropertyClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxPropertyClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			ComboBoxPropertyClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxPropertyClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxPropertyClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxPropertyClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxPropertyClass.Editable = true;
			ComboBoxPropertyClass.FilterString = "";
			ComboBoxPropertyClass.HasAllAccount = false;
			ComboBoxPropertyClass.HasCustom = false;
			ComboBoxPropertyClass.IsDataLoaded = false;
			ComboBoxPropertyClass.Location = new System.Drawing.Point(467, 33);
			ComboBoxPropertyClass.MaxDropDownItems = 12;
			ComboBoxPropertyClass.Name = "ComboBoxPropertyClass";
			ComboBoxPropertyClass.ShowInactiveItems = false;
			ComboBoxPropertyClass.ShowQuickAdd = true;
			ComboBoxPropertyClass.Size = new System.Drawing.Size(158, 20);
			ComboBoxPropertyClass.TabIndex = 6;
			ComboBoxPropertyClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel10.AutoSize = true;
			appearance77.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.LinkAppearance = appearance77;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(9, 109);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabel10.TabIndex = 26;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Fixed Asset ID:";
			appearance78.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance78;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			ultraGroupBox6.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox6.Controls.Add(mmLabel23);
			ultraGroupBox6.Controls.Add(mmLabel22);
			ultraGroupBox6.Controls.Add(ultraGroupBox7);
			ultraGroupBox6.Controls.Add(comboBoxPrepaidRentalIncomeAccount);
			ultraGroupBox6.Controls.Add(comboBoxRentalIncomeAccount);
			ultraGroupBox6.Controls.Add(comboBoxYearBuilt);
			ultraGroupBox6.Controls.Add(textBoxBuildArea);
			ultraGroupBox6.Controls.Add(textBoxPrepaidRentalIncomeAccount);
			ultraGroupBox6.Controls.Add(textBoxLandArea);
			ultraGroupBox6.Controls.Add(textBoxRentalIncomeAccount);
			ultraGroupBox6.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox6.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox6.Controls.Add(mmLabel14);
			ultraGroupBox6.Controls.Add(textBoxRegisterNumber);
			ultraGroupBox6.Controls.Add(mmLabel13);
			ultraGroupBox6.Controls.Add(mmLabel12);
			ultraGroupBox6.Controls.Add(textBoxOwnerName);
			ultraGroupBox6.Controls.Add(textBoxBuiltBy);
			ultraGroupBox6.Controls.Add(mmLabel11);
			ultraGroupBox6.Controls.Add(mmLabel8);
			ultraGroupBox6.Controls.Add(mmLabel10);
			ultraGroupBox6.Location = new System.Drawing.Point(7, 266);
			ultraGroupBox6.Name = "ultraGroupBox6";
			ultraGroupBox6.Size = new System.Drawing.Size(760, 188);
			ultraGroupBox6.TabIndex = 14;
			ultraGroupBox6.Text = "Other Details";
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 9.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(378, 54);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(33, 16);
			mmLabel23.TabIndex = 37;
			mmLabel23.Text = "/ M²";
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 9.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(174, 54);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(33, 16);
			mmLabel22.TabIndex = 35;
			mmLabel22.Text = "/ M²";
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel4);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(mmLabel4);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Controls.Add(textBoxTaxIDNumber);
			ultraGroupBox7.Controls.Add(mmLabel58);
			ultraGroupBox7.Location = new System.Drawing.Point(497, 24);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(299, 87);
			ultraGroupBox7.TabIndex = 8;
			ultraGroupBox7.Text = "Tax Details";
			ultraFormattedLinkLabel4.AutoSize = true;
			appearance79.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.LinkAppearance = appearance79;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(4, 46);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel4.TabIndex = 72;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Tax Group:";
			appearance80.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance80;
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance81;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(117, 43);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(141, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(4, 22);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(64, 13);
			mmLabel4.TabIndex = 70;
			mmLabel4.Text = "Tax Option:";
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Based on Class",
				"Taxable",
				"NonTaxable"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(117, 18);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(141, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			textBoxTaxIDNumber.BackColor = System.Drawing.Color.White;
			textBoxTaxIDNumber.CustomReportFieldName = "";
			textBoxTaxIDNumber.CustomReportKey = "";
			textBoxTaxIDNumber.CustomReportValueType = 1;
			textBoxTaxIDNumber.IsComboTextBox = false;
			textBoxTaxIDNumber.IsModified = false;
			textBoxTaxIDNumber.Location = new System.Drawing.Point(117, 67);
			textBoxTaxIDNumber.MaxLength = 75;
			textBoxTaxIDNumber.Name = "textBoxTaxIDNumber";
			textBoxTaxIDNumber.Size = new System.Drawing.Size(141, 20);
			textBoxTaxIDNumber.TabIndex = 2;
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(4, 71);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(83, 13);
			mmLabel58.TabIndex = 67;
			mmLabel58.Text = "Tax ID Number:";
			comboBoxPrepaidRentalIncomeAccount.Assigned = false;
			comboBoxPrepaidRentalIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPrepaidRentalIncomeAccount.CustomReportFieldName = "";
			comboBoxPrepaidRentalIncomeAccount.CustomReportKey = "";
			comboBoxPrepaidRentalIncomeAccount.CustomReportValueType = 1;
			comboBoxPrepaidRentalIncomeAccount.DescriptionTextBox = textBoxPrepaidRentalIncomeAccount;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Appearance = appearance93;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPrepaidRentalIncomeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPrepaidRentalIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPrepaidRentalIncomeAccount.Editable = true;
			comboBoxPrepaidRentalIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxPrepaidRentalIncomeAccount.FilterString = "";
			comboBoxPrepaidRentalIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxPrepaidRentalIncomeAccount.FilterSysDocID = "";
			comboBoxPrepaidRentalIncomeAccount.HasAllAccount = false;
			comboBoxPrepaidRentalIncomeAccount.HasCustom = false;
			comboBoxPrepaidRentalIncomeAccount.IsDataLoaded = false;
			comboBoxPrepaidRentalIncomeAccount.Location = new System.Drawing.Point(150, 162);
			comboBoxPrepaidRentalIncomeAccount.MaxDropDownItems = 12;
			comboBoxPrepaidRentalIncomeAccount.MaxLength = 64;
			comboBoxPrepaidRentalIncomeAccount.Name = "comboBoxPrepaidRentalIncomeAccount";
			comboBoxPrepaidRentalIncomeAccount.ShowInactiveItems = false;
			comboBoxPrepaidRentalIncomeAccount.ShowQuickAdd = true;
			comboBoxPrepaidRentalIncomeAccount.Size = new System.Drawing.Size(195, 20);
			comboBoxPrepaidRentalIncomeAccount.TabIndex = 7;
			comboBoxPrepaidRentalIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPrepaidRentalIncomeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPrepaidRentalIncomeAccount.CustomReportFieldName = "";
			textBoxPrepaidRentalIncomeAccount.CustomReportKey = "";
			textBoxPrepaidRentalIncomeAccount.CustomReportValueType = 1;
			textBoxPrepaidRentalIncomeAccount.IsComboTextBox = false;
			textBoxPrepaidRentalIncomeAccount.IsModified = false;
			textBoxPrepaidRentalIncomeAccount.Location = new System.Drawing.Point(345, 162);
			textBoxPrepaidRentalIncomeAccount.MaxLength = 30;
			textBoxPrepaidRentalIncomeAccount.Name = "textBoxPrepaidRentalIncomeAccount";
			textBoxPrepaidRentalIncomeAccount.ReadOnly = true;
			textBoxPrepaidRentalIncomeAccount.Size = new System.Drawing.Size(320, 20);
			textBoxPrepaidRentalIncomeAccount.TabIndex = 33;
			textBoxPrepaidRentalIncomeAccount.TabStop = false;
			comboBoxRentalIncomeAccount.Assigned = false;
			comboBoxRentalIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRentalIncomeAccount.CustomReportFieldName = "";
			comboBoxRentalIncomeAccount.CustomReportKey = "";
			comboBoxRentalIncomeAccount.CustomReportValueType = 1;
			comboBoxRentalIncomeAccount.DescriptionTextBox = textBoxRentalIncomeAccount;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRentalIncomeAccount.DisplayLayout.Appearance = appearance105;
			comboBoxRentalIncomeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRentalIncomeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRentalIncomeAccount.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRentalIncomeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			comboBoxRentalIncomeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRentalIncomeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			comboBoxRentalIncomeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRentalIncomeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.CellAppearance = appearance112;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			comboBoxRentalIncomeAccount.DisplayLayout.Override.HeaderAppearance = appearance114;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.RowAppearance = appearance115;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRentalIncomeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
			comboBoxRentalIncomeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRentalIncomeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRentalIncomeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRentalIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRentalIncomeAccount.Editable = true;
			comboBoxRentalIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxRentalIncomeAccount.FilterString = "";
			comboBoxRentalIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxRentalIncomeAccount.FilterSysDocID = "";
			comboBoxRentalIncomeAccount.HasAllAccount = false;
			comboBoxRentalIncomeAccount.HasCustom = false;
			comboBoxRentalIncomeAccount.IsDataLoaded = false;
			comboBoxRentalIncomeAccount.Location = new System.Drawing.Point(150, 136);
			comboBoxRentalIncomeAccount.MaxDropDownItems = 12;
			comboBoxRentalIncomeAccount.MaxLength = 64;
			comboBoxRentalIncomeAccount.Name = "comboBoxRentalIncomeAccount";
			comboBoxRentalIncomeAccount.ShowInactiveItems = false;
			comboBoxRentalIncomeAccount.ShowQuickAdd = true;
			comboBoxRentalIncomeAccount.Size = new System.Drawing.Size(195, 20);
			comboBoxRentalIncomeAccount.TabIndex = 6;
			comboBoxRentalIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRentalIncomeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRentalIncomeAccount.CustomReportFieldName = "";
			textBoxRentalIncomeAccount.CustomReportKey = "";
			textBoxRentalIncomeAccount.CustomReportValueType = 1;
			textBoxRentalIncomeAccount.IsComboTextBox = false;
			textBoxRentalIncomeAccount.IsModified = false;
			textBoxRentalIncomeAccount.Location = new System.Drawing.Point(345, 136);
			textBoxRentalIncomeAccount.MaxLength = 30;
			textBoxRentalIncomeAccount.Name = "textBoxRentalIncomeAccount";
			textBoxRentalIncomeAccount.ReadOnly = true;
			textBoxRentalIncomeAccount.Size = new System.Drawing.Size(320, 20);
			textBoxRentalIncomeAccount.TabIndex = 33;
			textBoxRentalIncomeAccount.TabStop = false;
			comboBoxYearBuilt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxYearBuilt.FormattingEnabled = true;
			comboBoxYearBuilt.Items.AddRange(new object[101]
			{
				"1950",
				"1951",
				"1952",
				"1953",
				"1954",
				"1955",
				"1956",
				"1957",
				"1958",
				"1959",
				"1960",
				"1961",
				"1962",
				"1963",
				"1964",
				"1965",
				"1966",
				"1967",
				"1968",
				"1969",
				"1970",
				"1971",
				"1972",
				"1973",
				"1974",
				"1975",
				"1976",
				"1977",
				"1978",
				"1979",
				"1980",
				"1981",
				"1982",
				"1983",
				"1984",
				"1985",
				"1986",
				"1987",
				"1988",
				"1989",
				"1990",
				"1991",
				"1992",
				"1993",
				"1994",
				"1995",
				"1996",
				"1997",
				"1998",
				"1999",
				"2000",
				"2001",
				"2002",
				"2003",
				"2004",
				"2005",
				"2006",
				"2007",
				"2008",
				"2009",
				"2010",
				"2011",
				"2012",
				"2013",
				"2014",
				"2015",
				"2016",
				"2017",
				"2018",
				"2019",
				"2020",
				"2021",
				"2022",
				"2023",
				"2024",
				"2025",
				"2026",
				"2027",
				"2028",
				"2029",
				"2030",
				"2031",
				"2032",
				"2033",
				"2034",
				"2035",
				"2036",
				"2037",
				"2038",
				"2039",
				"2040",
				"2041",
				"2042",
				"2043",
				"2044",
				"2045",
				"2046",
				"2047",
				"2048",
				"2049",
				"2050"
			});
			comboBoxYearBuilt.Location = new System.Drawing.Point(90, 24);
			comboBoxYearBuilt.Name = "comboBoxYearBuilt";
			comboBoxYearBuilt.Size = new System.Drawing.Size(78, 21);
			comboBoxYearBuilt.TabIndex = 0;
			textBoxBuildArea.AllowDecimal = true;
			textBoxBuildArea.BackColor = System.Drawing.Color.White;
			textBoxBuildArea.CustomReportFieldName = "";
			textBoxBuildArea.CustomReportKey = "";
			textBoxBuildArea.CustomReportValueType = 1;
			textBoxBuildArea.IsComboTextBox = false;
			textBoxBuildArea.IsModified = false;
			textBoxBuildArea.Location = new System.Drawing.Point(282, 52);
			textBoxBuildArea.MaxLength = 15;
			textBoxBuildArea.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBuildArea.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBuildArea.Name = "textBoxBuildArea";
			textBoxBuildArea.NullText = "0";
			textBoxBuildArea.Size = new System.Drawing.Size(92, 20);
			textBoxBuildArea.TabIndex = 3;
			textBoxBuildArea.Text = "0.00";
			textBoxBuildArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBuildArea.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxLandArea.AllowDecimal = true;
			textBoxLandArea.BackColor = System.Drawing.Color.White;
			textBoxLandArea.CustomReportFieldName = "";
			textBoxLandArea.CustomReportKey = "";
			textBoxLandArea.CustomReportValueType = 1;
			textBoxLandArea.IsComboTextBox = false;
			textBoxLandArea.IsModified = false;
			textBoxLandArea.Location = new System.Drawing.Point(90, 51);
			textBoxLandArea.MaxLength = 15;
			textBoxLandArea.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLandArea.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLandArea.Name = "textBoxLandArea";
			textBoxLandArea.NullText = "0";
			textBoxLandArea.Size = new System.Drawing.Size(78, 20);
			textBoxLandArea.TabIndex = 2;
			textBoxLandArea.Text = "0.00";
			textBoxLandArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLandArea.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			ultraFormattedLinkLabel2.AutoSize = true;
			appearance117.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.LinkAppearance = appearance117;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(3, 164);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(131, 14);
			ultraFormattedLinkLabel2.TabIndex = 32;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Prepaid Rent Income A/C:";
			appearance118.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance118;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraFormattedLinkLabel1.AutoSize = true;
			appearance119.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.LinkAppearance = appearance119;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(3, 139);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(99, 14);
			ultraFormattedLinkLabel1.TabIndex = 32;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Rental Income A/C:";
			appearance120.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance120;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(3, 81);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(81, 13);
			mmLabel14.TabIndex = 31;
			mmLabel14.Text = "Owner Reg No:";
			textBoxRegisterNumber.BackColor = System.Drawing.Color.White;
			textBoxRegisterNumber.CustomReportFieldName = "";
			textBoxRegisterNumber.CustomReportKey = "";
			textBoxRegisterNumber.CustomReportValueType = 1;
			textBoxRegisterNumber.IsComboTextBox = false;
			textBoxRegisterNumber.IsModified = false;
			textBoxRegisterNumber.Location = new System.Drawing.Point(90, 78);
			textBoxRegisterNumber.MaxLength = 64;
			textBoxRegisterNumber.Name = "textBoxRegisterNumber";
			textBoxRegisterNumber.Size = new System.Drawing.Size(229, 20);
			textBoxRegisterNumber.TabIndex = 4;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(3, 107);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(73, 13);
			mmLabel13.TabIndex = 29;
			mmLabel13.Text = "Owner Name:";
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(223, 30);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(46, 13);
			mmLabel12.TabIndex = 8;
			mmLabel12.Text = "Built by:";
			textBoxOwnerName.BackColor = System.Drawing.Color.White;
			textBoxOwnerName.CustomReportFieldName = "";
			textBoxOwnerName.CustomReportKey = "";
			textBoxOwnerName.CustomReportValueType = 1;
			textBoxOwnerName.IsComboTextBox = false;
			textBoxOwnerName.IsModified = false;
			textBoxOwnerName.Location = new System.Drawing.Point(90, 104);
			textBoxOwnerName.MaxLength = 64;
			textBoxOwnerName.Name = "textBoxOwnerName";
			textBoxOwnerName.Size = new System.Drawing.Size(273, 20);
			textBoxOwnerName.TabIndex = 5;
			textBoxBuiltBy.BackColor = System.Drawing.Color.White;
			textBoxBuiltBy.CustomReportFieldName = "";
			textBoxBuiltBy.CustomReportKey = "";
			textBoxBuiltBy.CustomReportValueType = 1;
			textBoxBuiltBy.IsComboTextBox = false;
			textBoxBuiltBy.IsModified = false;
			textBoxBuiltBy.Location = new System.Drawing.Point(282, 26);
			textBoxBuiltBy.MaxLength = 30;
			textBoxBuiltBy.Name = "textBoxBuiltBy";
			textBoxBuiltBy.Size = new System.Drawing.Size(191, 20);
			textBoxBuiltBy.TabIndex = 1;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(223, 56);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(55, 13);
			mmLabel11.TabIndex = 6;
			mmLabel11.Text = "Build Area";
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(3, 30);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(56, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "Year Built:";
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(3, 56);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(56, 13);
			mmLabel10.TabIndex = 4;
			mmLabel10.Text = "Land Area";
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel6);
			ultraGroupBox1.Controls.Add(textBoxAddress1);
			ultraGroupBox1.Controls.Add(textBoxAddress2);
			ultraGroupBox1.Controls.Add(mmLabel9);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 180);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(613, 71);
			ultraGroupBox1.TabIndex = 13;
			ultraGroupBox1.Text = "Location";
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(-2, 25);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(59, 13);
			mmLabel6.TabIndex = 0;
			mmLabel6.Text = "Address 1:";
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.IsModified = false;
			textBoxAddress1.Location = new System.Drawing.Point(120, 22);
			textBoxAddress1.MaxLength = 30;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(470, 20);
			textBoxAddress1.TabIndex = 10;
			textBoxAddress2.BackColor = System.Drawing.Color.White;
			textBoxAddress2.CustomReportFieldName = "";
			textBoxAddress2.CustomReportKey = "";
			textBoxAddress2.CustomReportValueType = 1;
			textBoxAddress2.IsComboTextBox = false;
			textBoxAddress2.IsModified = false;
			textBoxAddress2.Location = new System.Drawing.Point(120, 44);
			textBoxAddress2.MaxLength = 30;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(470, 20);
			textBoxAddress2.TabIndex = 11;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(-2, 44);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(59, 13);
			mmLabel9.TabIndex = 4;
			mmLabel9.Text = "Address 2:";
			comboBoxOfferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxOfferType.FormattingEnabled = true;
			comboBoxOfferType.Items.AddRange(new object[3]
			{
				"Rent",
				"Sale",
				"Lease"
			});
			comboBoxOfferType.Location = new System.Drawing.Point(132, 81);
			comboBoxOfferType.Name = "comboBoxOfferType";
			comboBoxOfferType.Size = new System.Drawing.Size(229, 21);
			comboBoxOfferType.TabIndex = 4;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(9, 85);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(64, 13);
			mmLabel1.TabIndex = 23;
			mmLabel1.Text = "Offer Type:";
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(367, 84);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(27, 14);
			ultraFormattedLinkLabel9.TabIndex = 20;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "City:";
			appearance121.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance121;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			linkLabelArea.AutoSize = true;
			linkLabelArea.Location = new System.Drawing.Point(367, 109);
			linkLabelArea.Name = "linkLabelArea";
			linkLabelArea.Size = new System.Drawing.Size(30, 14);
			linkLabelArea.TabIndex = 20;
			linkLabelArea.TabStop = true;
			linkLabelArea.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelArea.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelArea.Value = "Area:";
			appearance122.ForeColor = System.Drawing.Color.Blue;
			linkLabelArea.VisitedLinkAppearance = appearance122;
			linkLabelArea.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelArea_LinkClicked);
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(367, 60);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(46, 14);
			linkLabelCountry.TabIndex = 18;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Country:";
			appearance123.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance123;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			linkLabelCustomerClass.AutoSize = true;
			appearance124.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.LinkAppearance = appearance124;
			linkLabelCustomerClass.Location = new System.Drawing.Point(367, 36);
			linkLabelCustomerClass.Name = "linkLabelCustomerClass";
			linkLabelCustomerClass.Size = new System.Drawing.Size(80, 14);
			linkLabelCustomerClass.TabIndex = 16;
			linkLabelCustomerClass.TabStop = true;
			linkLabelCustomerClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCustomerClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCustomerClass.Value = "Property Class:";
			appearance125.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.VisitedLinkAppearance = appearance125;
			linkLabelCustomerClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCustomerClass_LinkClicked);
			comboBoxArea.Assigned = false;
			comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArea.CustomReportFieldName = "";
			comboBoxArea.CustomReportKey = "";
			comboBoxArea.CustomReportValueType = 1;
			comboBoxArea.DescriptionTextBox = null;
			appearance126.BackColor = System.Drawing.SystemColors.Window;
			appearance126.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArea.DisplayLayout.Appearance = appearance126;
			comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance127.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance127.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance127;
			appearance128.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance128;
			comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance129.BackColor2 = System.Drawing.SystemColors.Control;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance129;
			comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance130;
			appearance131.BackColor = System.Drawing.SystemColors.Highlight;
			appearance131.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance131;
			comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance132.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance132;
			appearance133.BorderColor = System.Drawing.Color.Silver;
			appearance133.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArea.DisplayLayout.Override.CellAppearance = appearance133;
			comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArea.DisplayLayout.Override.CellPadding = 0;
			appearance134.BackColor = System.Drawing.SystemColors.Control;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance134;
			appearance135.TextHAlignAsString = "Left";
			comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance135;
			comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			comboBoxArea.DisplayLayout.Override.RowAppearance = appearance136;
			comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance137.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance137;
			comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArea.Editable = true;
			comboBoxArea.FilterString = "";
			comboBoxArea.HasAllAccount = false;
			comboBoxArea.HasCustom = false;
			comboBoxArea.IsDataLoaded = false;
			comboBoxArea.Location = new System.Drawing.Point(467, 106);
			comboBoxArea.MaxDropDownItems = 12;
			comboBoxArea.MaxLength = 15;
			comboBoxArea.Name = "comboBoxArea";
			comboBoxArea.ShowInactiveItems = false;
			comboBoxArea.ShowQuickAdd = true;
			comboBoxArea.Size = new System.Drawing.Size(158, 20);
			comboBoxArea.TabIndex = 9;
			comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCountry.Assigned = false;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = null;
			appearance138.BackColor = System.Drawing.SystemColors.Window;
			appearance138.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance138;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance139.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance139.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance139.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance139;
			appearance140.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance140;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance141.BackColor2 = System.Drawing.SystemColors.Control;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance141;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			appearance142.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance142;
			appearance143.BackColor = System.Drawing.SystemColors.Highlight;
			appearance143.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance143;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance144.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance144;
			appearance145.BorderColor = System.Drawing.Color.Silver;
			appearance145.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance145;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance146.BackColor = System.Drawing.SystemColors.Control;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance146;
			appearance147.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance147;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance148;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance149.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance149;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(467, 57);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.MaxLength = 15;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(158, 20);
			comboBoxCountry.TabIndex = 7;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 58);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(67, 13);
			mmLabel5.TabIndex = 8;
			mmLabel5.Text = "Short Name:";
			labelCustomerNumber.AutoSize = true;
			labelCustomerNumber.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNumber.IsFieldHeader = false;
			labelCustomerNumber.IsRequired = true;
			labelCustomerNumber.Location = new System.Drawing.Point(9, 10);
			labelCustomerNumber.Name = "labelCustomerNumber";
			labelCustomerNumber.PenWidth = 1f;
			labelCustomerNumber.ShowBorder = false;
			labelCustomerNumber.Size = new System.Drawing.Size(91, 13);
			labelCustomerNumber.TabIndex = 0;
			labelCustomerNumber.Text = "Property Code:";
			labelCustomerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxShortName.BackColor = System.Drawing.Color.White;
			textBoxShortName.CustomReportFieldName = "";
			textBoxShortName.CustomReportKey = "";
			textBoxShortName.CustomReportValueType = 1;
			textBoxShortName.IsComboTextBox = false;
			textBoxShortName.IsModified = false;
			textBoxShortName.Location = new System.Drawing.Point(132, 57);
			textBoxShortName.MaxLength = 64;
			textBoxShortName.Name = "textBoxShortName";
			textBoxShortName.Size = new System.Drawing.Size(229, 20);
			textBoxShortName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(132, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(229, 20);
			textBoxCode.TabIndex = 0;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(374, 10);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(65, 16);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(132, 33);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(229, 20);
			textBoxName.TabIndex = 2;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = true;
			label1.Location = new System.Drawing.Point(9, 31);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(95, 13);
			label1.TabIndex = 2;
			label1.Text = "Property Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(631, 10);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(211, 234);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 64;
			pictureBoxPhoto.TabStop = false;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(793, 196);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 118;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			ultraTabPageControl3.Controls.Add(ultraTabControl2);
			ultraTabPageControl3.Controls.Add(mmLabel20);
			ultraTabPageControl3.Controls.Add(textBoxElectricityContractNo);
			ultraTabPageControl3.Controls.Add(mmLabel19);
			ultraTabPageControl3.Controls.Add(textBoxElectricityPremiseNo);
			ultraTabPageControl3.Controls.Add(mmLabel18);
			ultraTabPageControl3.Controls.Add(textBoxMunicipalityRegnNo);
			ultraTabPageControl3.Controls.Add(mmLabel17);
			ultraTabPageControl3.Controls.Add(textBoxTelecomRegnNo);
			ultraTabPageControl3.Controls.Add(mmLabel7);
			ultraTabPageControl3.Controls.Add(textBoxElectricityRegnNo);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(855, 493);
			ultraTabControl2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl2.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl2.Controls.Add(ultraTabPageControl4);
			ultraTabControl2.Location = new System.Drawing.Point(20, 106);
			ultraTabControl2.Name = "ultraTabControl2";
			ultraTabControl2.SharedControls.AddRange(new System.Windows.Forms.Control[1]
			{
				dataEntryGridOwner
			});
			ultraTabControl2.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl2.Size = new System.Drawing.Size(832, 220);
			ultraTabControl2.TabIndex = 19;
			ultraTab.TabPage = ultraTabPageControl4;
			ultraTab.Text = "Owner Details";
			ultraTabControl2.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[1]
			{
				ultraTab
			});
			ultraTabSharedControlsPage2.Controls.Add(dataEntryGridOwner);
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(828, 194);
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(356, 44);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(118, 13);
			mmLabel20.TabIndex = 18;
			mmLabel20.Text = "Electricity Contract No:";
			textBoxElectricityContractNo.BackColor = System.Drawing.Color.White;
			textBoxElectricityContractNo.CustomReportFieldName = "";
			textBoxElectricityContractNo.CustomReportKey = "";
			textBoxElectricityContractNo.CustomReportValueType = 1;
			textBoxElectricityContractNo.IsComboTextBox = false;
			textBoxElectricityContractNo.IsModified = false;
			textBoxElectricityContractNo.Location = new System.Drawing.Point(498, 40);
			textBoxElectricityContractNo.MaxLength = 30;
			textBoxElectricityContractNo.Name = "textBoxElectricityContractNo";
			textBoxElectricityContractNo.Size = new System.Drawing.Size(191, 20);
			textBoxElectricityContractNo.TabIndex = 6;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(356, 18);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(113, 13);
			mmLabel19.TabIndex = 16;
			mmLabel19.Text = "Electricity Premise No:";
			textBoxElectricityPremiseNo.BackColor = System.Drawing.Color.White;
			textBoxElectricityPremiseNo.CustomReportFieldName = "";
			textBoxElectricityPremiseNo.CustomReportKey = "";
			textBoxElectricityPremiseNo.CustomReportValueType = 1;
			textBoxElectricityPremiseNo.IsComboTextBox = false;
			textBoxElectricityPremiseNo.IsModified = false;
			textBoxElectricityPremiseNo.Location = new System.Drawing.Point(498, 14);
			textBoxElectricityPremiseNo.MaxLength = 30;
			textBoxElectricityPremiseNo.Name = "textBoxElectricityPremiseNo";
			textBoxElectricityPremiseNo.Size = new System.Drawing.Size(191, 20);
			textBoxElectricityPremiseNo.TabIndex = 5;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(17, 73);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(114, 13);
			mmLabel18.TabIndex = 14;
			mmLabel18.Text = "Municipality Regn. No:";
			textBoxMunicipalityRegnNo.BackColor = System.Drawing.Color.White;
			textBoxMunicipalityRegnNo.CustomReportFieldName = "";
			textBoxMunicipalityRegnNo.CustomReportKey = "";
			textBoxMunicipalityRegnNo.CustomReportValueType = 1;
			textBoxMunicipalityRegnNo.IsComboTextBox = false;
			textBoxMunicipalityRegnNo.IsModified = false;
			textBoxMunicipalityRegnNo.Location = new System.Drawing.Point(159, 69);
			textBoxMunicipalityRegnNo.MaxLength = 30;
			textBoxMunicipalityRegnNo.Name = "textBoxMunicipalityRegnNo";
			textBoxMunicipalityRegnNo.Size = new System.Drawing.Size(191, 20);
			textBoxMunicipalityRegnNo.TabIndex = 4;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(17, 47);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(95, 13);
			mmLabel17.TabIndex = 12;
			mmLabel17.Text = "Telecom Regn.No:";
			textBoxTelecomRegnNo.BackColor = System.Drawing.Color.White;
			textBoxTelecomRegnNo.CustomReportFieldName = "";
			textBoxTelecomRegnNo.CustomReportKey = "";
			textBoxTelecomRegnNo.CustomReportValueType = 1;
			textBoxTelecomRegnNo.IsComboTextBox = false;
			textBoxTelecomRegnNo.IsModified = false;
			textBoxTelecomRegnNo.Location = new System.Drawing.Point(159, 43);
			textBoxTelecomRegnNo.MaxLength = 30;
			textBoxTelecomRegnNo.Name = "textBoxTelecomRegnNo";
			textBoxTelecomRegnNo.Size = new System.Drawing.Size(191, 20);
			textBoxTelecomRegnNo.TabIndex = 3;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(17, 21);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(102, 13);
			mmLabel7.TabIndex = 10;
			mmLabel7.Text = "Electricity Regn.No:";
			textBoxElectricityRegnNo.BackColor = System.Drawing.Color.White;
			textBoxElectricityRegnNo.CustomReportFieldName = "";
			textBoxElectricityRegnNo.CustomReportKey = "";
			textBoxElectricityRegnNo.CustomReportValueType = 1;
			textBoxElectricityRegnNo.IsComboTextBox = false;
			textBoxElectricityRegnNo.IsModified = false;
			textBoxElectricityRegnNo.Location = new System.Drawing.Point(159, 17);
			textBoxElectricityRegnNo.MaxLength = 30;
			textBoxElectricityRegnNo.Name = "textBoxElectricityRegnNo";
			textBoxElectricityRegnNo.Size = new System.Drawing.Size(191, 20);
			textBoxElectricityRegnNo.TabIndex = 2;
			tabPageDetails.Controls.Add(comboBoxGridPropertyUnit);
			tabPageDetails.Controls.Add(mmLabel3);
			tabPageDetails.Controls.Add(dataGridPropertyUnits);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(855, 493);
			comboBoxGridPropertyUnit.Assigned = false;
			comboBoxGridPropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridPropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridPropertyUnit.CustomReportFieldName = "";
			comboBoxGridPropertyUnit.CustomReportKey = "";
			comboBoxGridPropertyUnit.CustomReportValueType = 1;
			comboBoxGridPropertyUnit.DescriptionTextBox = null;
			appearance150.BackColor = System.Drawing.SystemColors.Window;
			appearance150.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridPropertyUnit.DisplayLayout.Appearance = appearance150;
			comboBoxGridPropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridPropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance151.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance151.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance151.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridPropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance151;
			appearance152.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridPropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance152;
			comboBoxGridPropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance153.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance153.BackColor2 = System.Drawing.SystemColors.Control;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridPropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance153;
			comboBoxGridPropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridPropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance154.BackColor = System.Drawing.SystemColors.Window;
			appearance154.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridPropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance154;
			appearance155.BackColor = System.Drawing.SystemColors.Highlight;
			appearance155.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridPropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance155;
			comboBoxGridPropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridPropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance156.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridPropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance156;
			appearance157.BorderColor = System.Drawing.Color.Silver;
			appearance157.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridPropertyUnit.DisplayLayout.Override.CellAppearance = appearance157;
			comboBoxGridPropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridPropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance158.BackColor = System.Drawing.SystemColors.Control;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridPropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance158;
			appearance159.TextHAlignAsString = "Left";
			comboBoxGridPropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance159;
			comboBoxGridPropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridPropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance160.BackColor = System.Drawing.SystemColors.Window;
			appearance160.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridPropertyUnit.DisplayLayout.Override.RowAppearance = appearance160;
			comboBoxGridPropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance161.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridPropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance161;
			comboBoxGridPropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridPropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridPropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridPropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridPropertyUnit.Editable = true;
			comboBoxGridPropertyUnit.FilterString = "";
			comboBoxGridPropertyUnit.HasAllAccount = false;
			comboBoxGridPropertyUnit.HasCustom = false;
			comboBoxGridPropertyUnit.IsDataLoaded = false;
			comboBoxGridPropertyUnit.Location = new System.Drawing.Point(491, 103);
			comboBoxGridPropertyUnit.MaxDropDownItems = 12;
			comboBoxGridPropertyUnit.Name = "comboBoxGridPropertyUnit";
			comboBoxGridPropertyUnit.ShowActiveOnly = false;
			comboBoxGridPropertyUnit.ShowInactiveItems = false;
			comboBoxGridPropertyUnit.ShowQuickAdd = true;
			comboBoxGridPropertyUnit.Size = new System.Drawing.Size(100, 20);
			comboBoxGridPropertyUnit.TabIndex = 357;
			comboBoxGridPropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridPropertyUnit.Visible = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(11, 16);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(156, 13);
			mmLabel3.TabIndex = 356;
			mmLabel3.Text = "Units available in this property:";
			dataGridPropertyUnits.AllowAddNew = false;
			dataGridPropertyUnits.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance162.BackColor = System.Drawing.SystemColors.Window;
			appearance162.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPropertyUnits.DisplayLayout.Appearance = appearance162;
			dataGridPropertyUnits.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPropertyUnits.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance163.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance163.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance163.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance163.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPropertyUnits.DisplayLayout.GroupByBox.Appearance = appearance163;
			appearance164.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPropertyUnits.DisplayLayout.GroupByBox.BandLabelAppearance = appearance164;
			dataGridPropertyUnits.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance165.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance165.BackColor2 = System.Drawing.SystemColors.Control;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPropertyUnits.DisplayLayout.GroupByBox.PromptAppearance = appearance165;
			dataGridPropertyUnits.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPropertyUnits.DisplayLayout.MaxRowScrollRegions = 1;
			appearance166.BackColor = System.Drawing.SystemColors.Window;
			appearance166.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPropertyUnits.DisplayLayout.Override.ActiveCellAppearance = appearance166;
			appearance167.BackColor = System.Drawing.SystemColors.Highlight;
			appearance167.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPropertyUnits.DisplayLayout.Override.ActiveRowAppearance = appearance167;
			dataGridPropertyUnits.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPropertyUnits.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPropertyUnits.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance168.BackColor = System.Drawing.SystemColors.Window;
			dataGridPropertyUnits.DisplayLayout.Override.CardAreaAppearance = appearance168;
			appearance169.BorderColor = System.Drawing.Color.Silver;
			appearance169.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPropertyUnits.DisplayLayout.Override.CellAppearance = appearance169;
			dataGridPropertyUnits.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPropertyUnits.DisplayLayout.Override.CellPadding = 0;
			appearance170.BackColor = System.Drawing.SystemColors.Control;
			appearance170.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance170.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance170.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPropertyUnits.DisplayLayout.Override.GroupByRowAppearance = appearance170;
			appearance171.TextHAlignAsString = "Left";
			dataGridPropertyUnits.DisplayLayout.Override.HeaderAppearance = appearance171;
			dataGridPropertyUnits.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPropertyUnits.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance172.BackColor = System.Drawing.SystemColors.Window;
			appearance172.BorderColor = System.Drawing.Color.Silver;
			dataGridPropertyUnits.DisplayLayout.Override.RowAppearance = appearance172;
			dataGridPropertyUnits.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance173.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPropertyUnits.DisplayLayout.Override.TemplateAddRowAppearance = appearance173;
			dataGridPropertyUnits.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPropertyUnits.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPropertyUnits.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPropertyUnits.IncludeLotItems = false;
			dataGridPropertyUnits.LoadLayoutFailed = false;
			dataGridPropertyUnits.Location = new System.Drawing.Point(11, 32);
			dataGridPropertyUnits.Name = "dataGridPropertyUnits";
			dataGridPropertyUnits.ShowClearMenu = true;
			dataGridPropertyUnits.ShowDeleteMenu = true;
			dataGridPropertyUnits.ShowInsertMenu = true;
			dataGridPropertyUnits.ShowMoveRowsMenu = true;
			dataGridPropertyUnits.Size = new System.Drawing.Size(843, 525);
			dataGridPropertyUnits.TabIndex = 355;
			dataGridPropertyUnits.Text = "dataEntryGrid1";
			ultraTabPageControl2.Controls.Add(mmLabel2);
			ultraTabPageControl2.Controls.Add(dataGridFacility);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(855, 493);
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(11, 16);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(172, 13);
			mmLabel2.TabIndex = 358;
			mmLabel2.Text = "Facilities available in this property:";
			dataGridFacility.AllowAddNew = false;
			dataGridFacility.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			appearance174.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridFacility.DisplayLayout.Appearance = appearance174;
			dataGridFacility.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridFacility.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance175.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance175.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance175.BorderColor = System.Drawing.SystemColors.Window;
			dataGridFacility.DisplayLayout.GroupByBox.Appearance = appearance175;
			appearance176.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridFacility.DisplayLayout.GroupByBox.BandLabelAppearance = appearance176;
			dataGridFacility.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance177.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance177.BackColor2 = System.Drawing.SystemColors.Control;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridFacility.DisplayLayout.GroupByBox.PromptAppearance = appearance177;
			dataGridFacility.DisplayLayout.MaxColScrollRegions = 1;
			dataGridFacility.DisplayLayout.MaxRowScrollRegions = 1;
			appearance178.BackColor = System.Drawing.SystemColors.Window;
			appearance178.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridFacility.DisplayLayout.Override.ActiveCellAppearance = appearance178;
			appearance179.BackColor = System.Drawing.SystemColors.Highlight;
			appearance179.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridFacility.DisplayLayout.Override.ActiveRowAppearance = appearance179;
			dataGridFacility.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridFacility.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridFacility.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance180.BackColor = System.Drawing.SystemColors.Window;
			dataGridFacility.DisplayLayout.Override.CardAreaAppearance = appearance180;
			appearance181.BorderColor = System.Drawing.Color.Silver;
			appearance181.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridFacility.DisplayLayout.Override.CellAppearance = appearance181;
			dataGridFacility.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridFacility.DisplayLayout.Override.CellPadding = 0;
			appearance182.BackColor = System.Drawing.SystemColors.Control;
			appearance182.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance182.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance182.BorderColor = System.Drawing.SystemColors.Window;
			dataGridFacility.DisplayLayout.Override.GroupByRowAppearance = appearance182;
			appearance183.TextHAlignAsString = "Left";
			dataGridFacility.DisplayLayout.Override.HeaderAppearance = appearance183;
			dataGridFacility.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridFacility.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance184.BackColor = System.Drawing.SystemColors.Window;
			appearance184.BorderColor = System.Drawing.Color.Silver;
			dataGridFacility.DisplayLayout.Override.RowAppearance = appearance184;
			dataGridFacility.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance185.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridFacility.DisplayLayout.Override.TemplateAddRowAppearance = appearance185;
			dataGridFacility.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridFacility.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridFacility.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridFacility.IncludeLotItems = false;
			dataGridFacility.LoadLayoutFailed = false;
			dataGridFacility.Location = new System.Drawing.Point(11, 32);
			dataGridFacility.Name = "dataGridFacility";
			dataGridFacility.ShowClearMenu = true;
			dataGridFacility.ShowDeleteMenu = true;
			dataGridFacility.ShowInsertMenu = true;
			dataGridFacility.ShowMoveRowsMenu = true;
			dataGridFacility.Size = new System.Drawing.Size(843, 525);
			dataGridFacility.TabIndex = 357;
			dataGridFacility.Text = "dataEntryGrid2";
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(855, 493);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(8, 13);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(861, 546);
			textBoxNote.TabIndex = 43;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(855, 493);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(3, 3);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(728, 500);
			udfEntryGrid.TabIndex = 0;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 562);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(879, 40);
			panelButtons.TabIndex = 11;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(879, 1);
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
			xpButton1.Location = new System.Drawing.Point(769, 8);
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
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			ultraTabControl1.Location = new System.Drawing.Point(20, 29);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(859, 516);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 315;
			appearance186.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab2.Appearance = appearance186;
			ultraTab2.TabPage = tabPageGeneral;
			ultraTab2.Text = "&General";
			ultraTab3.TabPage = ultraTabPageControl3;
			ultraTab3.Text = "Details";
			ultraTab4.TabPage = tabPageDetails;
			ultraTab4.Text = "&Units";
			ultraTab5.TabPage = ultraTabPageControl2;
			ultraTab5.Text = "Facility";
			ultraTab6.TabPage = ultraTabPageControl1;
			ultraTab6.Text = "&Note";
			ultraTab7.TabPage = tabPageUserDefined;
			ultraTab7.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[6]
			{
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(855, 493);
			panel1.Controls.Add(panel2);
			panel1.Controls.Add(toolStrip1);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(20, 0);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(859, 29);
			panel1.TabIndex = 316;
			panel2.Controls.Add(mmLabel15);
			panel2.Dock = System.Windows.Forms.DockStyle.Top;
			panel2.Location = new System.Drawing.Point(0, 31);
			panel2.MinimumSize = new System.Drawing.Size(0, 8);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(859, 30);
			panel2.TabIndex = 315;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = true;
			mmLabel15.Location = new System.Drawing.Point(24, 7);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(0, 13);
			mmLabel15.TabIndex = 1;
			mmLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[20]
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
				toolStripSeparator4,
				toolStripButton1,
				toolStripButtonAttach,
				toolStripButtonComments,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonInformation,
				toolStripDropDownButton1,
				toolStripButtonShowPicture
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(859, 31);
			toolStrip1.TabIndex = 307;
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				documentsToolStripMenuItem1
			});
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.salesperson;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(81, 28);
			toolStripButton1.Text = "More...";
			documentsToolStripMenuItem1.Name = "documentsToolStripMenuItem1";
			documentsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
			documentsToolStripMenuItem1.Text = "Documents";
			documentsToolStripMenuItem1.Click += new System.EventHandler(documentsToolStripMenuItem1_Click);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonComments.Image = Micromind.ClientUI.Properties.Resources.comment;
			toolStripButtonComments.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonComments.Name = "toolStripButtonComments";
			toolStripButtonComments.Size = new System.Drawing.Size(28, 28);
			toolStripButtonComments.Text = "Comments...";
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				importToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			importToolStripMenuItem.Name = "importToolStripMenuItem";
			importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			importToolStripMenuItem.Text = "Import";
			toolStripButtonShowPicture.Checked = true;
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.CheckState = System.Windows.Forms.CheckState.Checked;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(104, 28);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(24, 7);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 1;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panel3.Controls.Add(mmLabel16);
			panel3.Dock = System.Windows.Forms.DockStyle.Top;
			panel3.Location = new System.Drawing.Point(20, 545);
			panel3.MinimumSize = new System.Drawing.Size(0, 8);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(859, 10);
			panel3.TabIndex = 317;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = true;
			mmLabel16.Location = new System.Drawing.Point(24, 7);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(0, 13);
			mmLabel16.TabIndex = 1;
			mmLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			openFileDialog1.FileName = "openFileDialog1";
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
			base.ClientSize = new System.Drawing.Size(879, 602);
			base.Controls.Add(panel3);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PropertyDetailsForm";
			Text = "Property";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxPropertyOwner).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridOwner).EndInit();
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCity).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFixedAsset).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxPropertyClass).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox6).EndInit();
			ultraGroupBox6.ResumeLayout(false);
			ultraGroupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPrepaidRentalIncomeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRentalIncomeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).EndInit();
			ultraTabControl2.ResumeLayout(false);
			ultraTabSharedControlsPage2.ResumeLayout(false);
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridPropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridPropertyUnits).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridFacility).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			ResumeLayout(false);
		}
	}
}
