using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class VehicleDetailsForm : Form, IForm
	{
		private VehicleData currentData;

		private const string TABLENAME_CONST = "Vehicle";

		private const string IDFIELD_CONST = "VehicleID";

		private bool isNewRecord = true;

		private bool EnableVehicleAnalysis = CompanyPreferences.EnableVehicleAnalysisCode;

		private DataSet companyInformation;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

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

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private TextBox textBoxCityName;

		private MMLabel mmLabel2;

		private MMTextBox textBoxRegistrationNo;

		private CountryComboBox comboBoxCountry;

		private TextBox textBoxCountryName;

		private MMLabel mmLabel6;

		private MMLabel mmLabel8;

		private MMLabel mmLabel9;

		private MMLabel mmLabel10;

		private MMLabel mmLabel11;

		private FixedAssetsComboBox comboBoxFixedAsset;

		private vendorsFlatComboBox comboBoxInsuranceVendor;

		private MMLabel mmLabel14;

		private MMLabel mmLabel15;

		private MMLabel mmLabel16;

		private MMLabel mmLabel17;

		private LocationComboBox comboBoxLocation;

		private DivisionComboBox comboBoxDivision;

		private MMLabel mmLabel20;

		private MMLabel mmLabel21;

		private AreaComboBox comboBoxArea;

		private TextBox textBoxAreaName;

		private MMTextBox textBoxChasisNo;

		private MMTextBox textBoxColor;

		private ComboBox comboBoxYear;

		private MMTextBox textBoxFuel;

		private DateTimePicker dateTimeRegExpiry;

		private MMTextBox textBoxModel;

		private TextBox textBoxFixedAssetName;

		private TextBox textBoxInsuranceVendorName;

		private MMTextBox textBoxInsurancePolicyNo;

		private DateTimePicker dateTimeInsExpiry;

		private MMTextBox textBoxTrackingNo;

		private TextBox textBoxLocationName;

		private TextBox textBoxDivisionName;

		private GenericListComboBox comboBoxVehicleType;

		private CityComboBox comboBoxCity;

		private AmountTextBox textBoxWeightCapacity;

		private AmountTextBox textBoxVehicleWeight;

		private ToolStripButton toolStripButtonAttach;

		private UltraGroupBox ultraGroupBox1;

		private UltraGroupBox ultraGroupBox2;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private UltraFormattedLinkLabel linkLabelCountry;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private CheckBox checkBoxIsInactive;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private AnalysisComboBox comboBoxAnalysis;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxPhoto;

		private OpenFileDialog openFileDialog1;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxNoImage;

		private ToolStripButton toolStripButtonShowPicture;

		private MMTextBox mmTextBoxTrafficNo;

		private MMLabel mmLabel3;

		private MMTextBox mmTextBoxPlateNo;

		private MMLabel mmLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private CountryComboBox countryComboBoxOrigin;

		private EmployeeComboBox comboBoxEmployee;

		private TextBox textBoxNote;

		private Label label3;

		public ScreenAreas ScreenArea => ScreenAreas.Vehicle;

		public int ScreenID => 5021;

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

		public VehicleDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxVehicleType.GenericListType = GenericListTypes.VehicleType;
			companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
			comboBoxAnalysis.FilterByAccount(companyInformation.Tables[0].Rows[0]["VehicleAnalysisGroup"].ToString());
		}

		private void AddEvents()
		{
			base.Load += VehicleDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new VehicleData();
				}
				string text = "";
				string text2 = "";
				string text3 = "";
				AnalysisData analysisData = new AnalysisData();
				if (isNewRecord && EnableVehicleAnalysis)
				{
					companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
					text = companyInformation.Tables[0].Rows[0]["VehicleAnalysisGroup"].ToString();
					text2 = companyInformation.Tables[0].Rows[0]["VehicleAnalysisPrefix"].ToString();
					if (text2 == string.Empty || text2 == null)
					{
						ErrorHelper.WarningMessage("Set AnalysisPrefix on Company settings");
						return false;
					}
					text3 = text2 + textBoxCode.Text;
					DataRow dataRow = analysisData.AnalysisTable.NewRow();
					dataRow["AnalysisID"] = text3;
					dataRow["AnalysisName"] = text3;
					dataRow["GroupID"] = text;
					dataRow.EndEdit();
					analysisData.AnalysisTable.Rows.Add(dataRow);
				}
				DataRow dataRow2 = (!isNewRecord) ? currentData.VehicleTable.Rows[0] : currentData.VehicleTable.NewRow();
				dataRow2.BeginEdit();
				dataRow2["VehicleID"] = textBoxCode.Text.Trim();
				dataRow2["VehicleName"] = textBoxName.Text.Trim();
				dataRow2["RegistrationNumber"] = textBoxRegistrationNo.Text.Trim();
				dataRow2["ChasisNumber"] = textBoxChasisNo.Text.Trim();
				dataRow2["Color"] = textBoxColor.Text.Trim();
				dataRow2["Fuel"] = textBoxFuel.Text.Trim();
				dataRow2["Model"] = textBoxModel.Text.Trim();
				dataRow2["InsurancePolicyNumber"] = textBoxInsurancePolicyNo.Text.Trim();
				dataRow2["TrackingNumber"] = textBoxTrackingNo.Text.Trim();
				dataRow2["WeightCapacity"] = textBoxWeightCapacity.Text.Trim();
				dataRow2["VehicleWeight"] = textBoxVehicleWeight.Text.Trim();
				dataRow2["TrafficFileNo"] = mmTextBoxTrafficNo.Text.Trim();
				dataRow2["PlateNo"] = mmTextBoxPlateNo.Text.Trim();
				if (comboBoxCity.SelectedID != null)
				{
					dataRow2["RegistrationCityID"] = comboBoxCity.SelectedID;
				}
				else
				{
					dataRow2["RegistrationCityID"] = DBNull.Value;
				}
				if (comboBoxCity.SelectedID != null)
				{
					dataRow2["CountryID"] = comboBoxCity.SelectedID;
				}
				else
				{
					dataRow2["CountryID"] = DBNull.Value;
				}
				if (comboBoxArea.SelectedID != null)
				{
					dataRow2["WorkingAreaID"] = comboBoxArea.SelectedID;
				}
				else
				{
					dataRow2["WorkingAreaID"] = DBNull.Value;
				}
				if (comboBoxVehicleType.SelectedID != null)
				{
					dataRow2["VehicleTypeID"] = comboBoxVehicleType.SelectedID;
				}
				else
				{
					dataRow2["VehicleTypeID"] = DBNull.Value;
				}
				if (comboBoxYear.SelectedItem != null)
				{
					dataRow2["Year"] = int.Parse(comboBoxYear.SelectedItem.ToString());
				}
				else
				{
					dataRow2["Year"] = DBNull.Value;
				}
				if (comboBoxFixedAsset.SelectedID != null)
				{
					dataRow2["FixedAssetID"] = comboBoxFixedAsset.SelectedID;
				}
				else
				{
					dataRow2["FixedAssetID"] = DBNull.Value;
				}
				if (comboBoxInsuranceVendor.SelectedID != null)
				{
					dataRow2["InsuranceVendorID"] = comboBoxInsuranceVendor.SelectedID;
				}
				else
				{
					dataRow2["InsuranceVendorID"] = DBNull.Value;
				}
				if (comboBoxLocation.SelectedID != null)
				{
					dataRow2["LocationID"] = comboBoxLocation.SelectedID;
				}
				else
				{
					dataRow2["LocationID"] = DBNull.Value;
				}
				if (comboBoxDivision.SelectedID != null)
				{
					dataRow2["DivisionID"] = comboBoxDivision.SelectedID;
				}
				else
				{
					dataRow2["DivisionID"] = DBNull.Value;
				}
				dataRow2["InsuranceExpiryDate"] = dateTimeInsExpiry.Value;
				dataRow2["RegistrationExpiryDate"] = dateTimeRegExpiry.Value;
				if (comboBoxEmployee.SelectedID != null)
				{
					dataRow2["OwnedBy"] = comboBoxEmployee.SelectedID;
				}
				else
				{
					dataRow2["OwnedBy"] = DBNull.Value;
				}
				dataRow2["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow2["Note"] = textBoxNote.Text;
				if (comboBoxAnalysis.SelectedID != "")
				{
					dataRow2["AnalysisID"] = comboBoxAnalysis.SelectedID;
				}
				else if (text3 != "")
				{
					dataRow2["AnalysisID"] = text3;
				}
				else
				{
					dataRow2["AnalysisID"] = DBNull.Value;
				}
				if (countryComboBoxOrigin.SelectedID != null)
				{
					dataRow2["Origin"] = countryComboBoxOrigin.SelectedID;
				}
				else
				{
					dataRow2["Origin"] = DBNull.Value;
				}
				dataRow2.EndEdit();
				if (isNewRecord)
				{
					currentData.VehicleTable.Rows.Add(dataRow2);
				}
				if (analysisData != null)
				{
					currentData.Merge(analysisData);
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
					currentData = Factory.VehicleSystem.GetVehicleByID(id.Trim());
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
			textBoxCode.Text = dataRow["VehicleID"].ToString();
			textBoxName.Text = dataRow["VehicleName"].ToString();
			textBoxRegistrationNo.Text = dataRow["RegistrationNumber"].ToString();
			textBoxChasisNo.Text = dataRow["ChasisNumber"].ToString();
			textBoxColor.Text = dataRow["Color"].ToString();
			textBoxFuel.Text = dataRow["Fuel"].ToString();
			textBoxModel.Text = dataRow["Model"].ToString();
			textBoxInsurancePolicyNo.Text = dataRow["InsurancePolicyNumber"].ToString();
			textBoxTrackingNo.Text = dataRow["TrackingNumber"].ToString();
			mmTextBoxTrafficNo.Text = dataRow["TrafficFileNo"].ToString();
			mmTextBoxPlateNo.Text = dataRow["PlateNo"].ToString();
			if (dataRow["WeightCapacity"] != DBNull.Value)
			{
				textBoxWeightCapacity.Text = dataRow["WeightCapacity"].ToString();
			}
			else
			{
				textBoxWeightCapacity.Clear();
			}
			if (dataRow["VehicleWeight"] != DBNull.Value)
			{
				textBoxVehicleWeight.Text = dataRow["VehicleWeight"].ToString();
			}
			else
			{
				textBoxVehicleWeight.Clear();
			}
			if (dataRow["RegistrationCityID"] != DBNull.Value)
			{
				comboBoxCity.SelectedID = dataRow["RegistrationCityID"].ToString();
			}
			else
			{
				comboBoxCity.Clear();
			}
			if (dataRow["CountryID"] != DBNull.Value)
			{
				comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
			}
			else
			{
				comboBoxCountry.Clear();
			}
			if (dataRow["WorkingAreaID"] != DBNull.Value)
			{
				comboBoxArea.SelectedID = dataRow["WorkingAreaID"].ToString();
			}
			else
			{
				comboBoxArea.Clear();
			}
			if (dataRow["VehicleTypeID"] != DBNull.Value)
			{
				comboBoxVehicleType.SelectedID = dataRow["VehicleTypeID"].ToString();
			}
			else
			{
				comboBoxVehicleType.Clear();
			}
			if (dataRow["Year"] != DBNull.Value)
			{
				comboBoxYear.SelectedItem = dataRow["Year"].ToString();
			}
			else
			{
				comboBoxYear.SelectedItem = null;
			}
			if (dataRow["FixedAssetID"] != DBNull.Value)
			{
				comboBoxFixedAsset.SelectedID = dataRow["FixedAssetID"].ToString();
			}
			else
			{
				comboBoxFixedAsset.Clear();
			}
			if (dataRow["InsuranceVendorID"] != DBNull.Value)
			{
				comboBoxInsuranceVendor.SelectedID = dataRow["InsuranceVendorID"].ToString();
			}
			else
			{
				comboBoxInsuranceVendor.Clear();
			}
			if (dataRow["LocationID"] != DBNull.Value)
			{
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
			}
			else
			{
				comboBoxLocation.Clear();
			}
			if (dataRow["DivisionID"] != DBNull.Value)
			{
				comboBoxDivision.SelectedID = dataRow["DivisionID"].ToString();
			}
			else
			{
				comboBoxDivision.Clear();
			}
			if (!string.IsNullOrEmpty(dataRow["InsuranceExpiryDate"].ToString()))
			{
				dateTimeInsExpiry.Value = DateTime.Parse(dataRow["InsuranceExpiryDate"].ToString());
			}
			else
			{
				dateTimeInsExpiry.Value = DateTime.Now;
			}
			if (!string.IsNullOrEmpty(dataRow["RegistrationExpiryDate"].ToString()))
			{
				dateTimeRegExpiry.Value = DateTime.Parse(dataRow["RegistrationExpiryDate"].ToString());
			}
			else
			{
				dateTimeRegExpiry.Value = DateTime.Now;
			}
			if (dataRow["OwnedBy"] != DBNull.Value)
			{
				comboBoxEmployee.SelectedID = dataRow["OwnedBy"].ToString();
			}
			else
			{
				comboBoxEmployee.Clear();
			}
			if (dataRow["IsInactive"] != DBNull.Value)
			{
				checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
			}
			if (dataRow["AnalysisID"] != DBNull.Value)
			{
				comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
			}
			else
			{
				comboBoxAnalysis.Clear();
			}
			if (dataRow["Origin"] != DBNull.Value)
			{
				countryComboBoxOrigin.SelectedID = dataRow["Origin"].ToString();
			}
			else
			{
				countryComboBoxOrigin.Clear();
			}
			textBoxNote.Text = dataRow["Note"].ToString();
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
					flag = Factory.VehicleSystem.CreateVehicle(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Vehicle, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.VehicleSystem.UpdateVehicle(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Vehicle", "VehicleID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Vehicle", "VehicleID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Vehicle", "VehicleID");
			textBoxName.Clear();
			textBoxRegistrationNo.Clear();
			textBoxChasisNo.Clear();
			textBoxColor.Clear();
			textBoxFuel.Clear();
			textBoxTrackingNo.Clear();
			textBoxInsurancePolicyNo.Clear();
			textBoxWeightCapacity.Clear();
			textBoxVehicleWeight.Clear();
			textBoxCityName.Clear();
			textBoxCountryName.Clear();
			textBoxAreaName.Clear();
			textBoxFixedAssetName.Clear();
			textBoxInsuranceVendorName.Clear();
			textBoxLocationName.Clear();
			textBoxDivisionName.Clear();
			textBoxModel.Clear();
			mmTextBoxTrafficNo.Clear();
			mmTextBoxPlateNo.Clear();
			checkBoxIsInactive.Checked = false;
			dateTimeRegExpiry.Value = DateTime.Now;
			dateTimeInsExpiry.Value = DateTime.Now;
			comboBoxCity.Clear();
			comboBoxCountry.Clear();
			comboBoxVehicleType.Clear();
			comboBoxArea.Clear();
			comboBoxYear.SelectedItem = null;
			comboBoxFixedAsset.Clear();
			comboBoxInsuranceVendor.Clear();
			comboBoxLocation.Clear();
			comboBoxDivision.Clear();
			comboBoxEmployee.Clear();
			countryComboBoxOrigin.Clear();
			comboBoxAnalysis.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			textBoxNote.Clear();
		}

		private void VehicleGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void VehicleGroupDetailsForm_Validated(object sender, EventArgs e)
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
				Factory.AnalysisSystem.DeleteAnalysis(comboBoxAnalysis.SelectedID);
				return Factory.VehicleSystem.DeleteVehicle(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Vehicle", "VehicleID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Vehicle", "VehicleID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Vehicle", "VehicleID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Vehicle", "VehicleID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Vehicle", "VehicleID", toolStripTextBoxFind.Text.Trim()))
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

		private void VehicleDetailsForm_Load(object sender, EventArgs e)
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
			new FormHelper().ShowList(DataComboType.Vehicle);
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
					docManagementForm.EntityType = EntityTypesEnum.Vehicles;
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

		private void documentsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.VehicleDocumentsFormObj);
			FormActivator.VehicleDocumentsFormObj.LoadData(textBoxCode.Text);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVehicleType(comboBoxVehicleType.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxInsuranceVendor.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDivision(comboBoxDivision.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditFixedAsset(comboBoxFixedAsset.SelectedID);
		}

		private void mmLabel20_Click(object sender, EventArgs e)
		{
		}

		private void mmLabel11_Click(object sender, EventArgs e)
		{
		}

		private void mmLabel9_Click(object sender, EventArgs e)
		{
		}

		private void mmLabel6_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel16_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.SelectedID);
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void mmLabel2_Click(object sender, EventArgs e)
		{
		}

		private void mmLabel1_Click(object sender, EventArgs e)
		{
		}

		private void labelCode_Click(object sender, EventArgs e)
		{
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddVehiclePhoto(textBoxCode.Text, image))
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
					if (Factory.VehicleSystem.RemoveVehiclePhoto(textBoxCode.Text))
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

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetVehicleThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(countryComboBoxOrigin.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.VehicleDetailsForm));
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
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			documentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxCityName = new System.Windows.Forms.TextBox();
			textBoxCountryName = new System.Windows.Forms.TextBox();
			textBoxAreaName = new System.Windows.Forms.TextBox();
			comboBoxYear = new System.Windows.Forms.ComboBox();
			dateTimeRegExpiry = new System.Windows.Forms.DateTimePicker();
			textBoxFixedAssetName = new System.Windows.Forms.TextBox();
			textBoxInsuranceVendorName = new System.Windows.Forms.TextBox();
			dateTimeInsExpiry = new System.Windows.Forms.DateTimePicker();
			textBoxLocationName = new System.Windows.Forms.TextBox();
			textBoxDivisionName = new System.Windows.Forms.TextBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxInsurancePolicyNo = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			comboBoxInsuranceVendor = new Micromind.DataControls.vendorsFlatComboBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxDivision = new Micromind.DataControls.DivisionComboBox();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxFixedAsset = new Micromind.DataControls.FixedAssetsComboBox();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			countryComboBoxOrigin = new Micromind.DataControls.CountryComboBox();
			mmTextBoxPlateNo = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmTextBoxTrafficNo = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			textBoxVehicleWeight = new Micromind.UISupport.AmountTextBox();
			textBoxWeightCapacity = new Micromind.UISupport.AmountTextBox();
			comboBoxCity = new Micromind.DataControls.CityComboBox();
			comboBoxVehicleType = new Micromind.DataControls.GenericListComboBox();
			textBoxTrackingNo = new Micromind.UISupport.MMTextBox();
			textBoxModel = new Micromind.UISupport.MMTextBox();
			textBoxFuel = new Micromind.UISupport.MMTextBox();
			textBoxColor = new Micromind.UISupport.MMTextBox();
			textBoxChasisNo = new Micromind.UISupport.MMTextBox();
			comboBoxArea = new Micromind.DataControls.AreaComboBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			textBoxRegistrationNo = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFixedAsset).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)countryComboBoxOrigin).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCity).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
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
				toolStripButton1,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripButtonInformation,
				toolStripButtonShowPicture
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(655, 31);
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
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(104, 28);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 583);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(655, 40);
			panelButtons.TabIndex = 27;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(655, 1);
			linePanelDown.TabIndex = 2;
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
			xpButton1.Location = new System.Drawing.Point(545, 8);
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
			textBoxCityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCityName.Location = new System.Drawing.Point(294, 126);
			textBoxCityName.MaxLength = 64;
			textBoxCityName.Name = "textBoxCityName";
			textBoxCityName.ReadOnly = true;
			textBoxCityName.Size = new System.Drawing.Size(201, 20);
			textBoxCityName.TabIndex = 7;
			textBoxCityName.TabStop = false;
			textBoxCountryName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCountryName.Location = new System.Drawing.Point(294, 148);
			textBoxCountryName.MaxLength = 64;
			textBoxCountryName.Name = "textBoxCountryName";
			textBoxCountryName.ReadOnly = true;
			textBoxCountryName.Size = new System.Drawing.Size(201, 20);
			textBoxCountryName.TabIndex = 9;
			textBoxCountryName.TabStop = false;
			textBoxAreaName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAreaName.Location = new System.Drawing.Point(294, 171);
			textBoxAreaName.MaxLength = 64;
			textBoxAreaName.Name = "textBoxAreaName";
			textBoxAreaName.ReadOnly = true;
			textBoxAreaName.Size = new System.Drawing.Size(201, 20);
			textBoxAreaName.TabIndex = 11;
			textBoxAreaName.TabStop = false;
			comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxYear.FormattingEnabled = true;
			comboBoxYear.Items.AddRange(new object[101]
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
			comboBoxYear.Location = new System.Drawing.Point(131, 216);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(89, 21);
			comboBoxYear.TabIndex = 13;
			dateTimeRegExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeRegExpiry.Location = new System.Drawing.Point(393, 104);
			dateTimeRegExpiry.Name = "dateTimeRegExpiry";
			dateTimeRegExpiry.Size = new System.Drawing.Size(102, 20);
			dateTimeRegExpiry.TabIndex = 5;
			textBoxFixedAssetName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFixedAssetName.Location = new System.Drawing.Point(294, 63);
			textBoxFixedAssetName.MaxLength = 64;
			textBoxFixedAssetName.Name = "textBoxFixedAssetName";
			textBoxFixedAssetName.ReadOnly = true;
			textBoxFixedAssetName.Size = new System.Drawing.Size(265, 20);
			textBoxFixedAssetName.TabIndex = 5;
			textBoxFixedAssetName.TabStop = false;
			textBoxInsuranceVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInsuranceVendorName.Location = new System.Drawing.Point(294, 19);
			textBoxInsuranceVendorName.MaxLength = 64;
			textBoxInsuranceVendorName.Name = "textBoxInsuranceVendorName";
			textBoxInsuranceVendorName.ReadOnly = true;
			textBoxInsuranceVendorName.Size = new System.Drawing.Size(265, 20);
			textBoxInsuranceVendorName.TabIndex = 1;
			textBoxInsuranceVendorName.TabStop = false;
			dateTimeInsExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeInsExpiry.Location = new System.Drawing.Point(433, 42);
			dateTimeInsExpiry.Name = "dateTimeInsExpiry";
			dateTimeInsExpiry.Size = new System.Drawing.Size(126, 20);
			dateTimeInsExpiry.TabIndex = 3;
			textBoxLocationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLocationName.Location = new System.Drawing.Point(294, 18);
			textBoxLocationName.MaxLength = 64;
			textBoxLocationName.Name = "textBoxLocationName";
			textBoxLocationName.ReadOnly = true;
			textBoxLocationName.Size = new System.Drawing.Size(265, 20);
			textBoxLocationName.TabIndex = 1;
			textBoxLocationName.TabStop = false;
			textBoxDivisionName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDivisionName.Location = new System.Drawing.Point(294, 40);
			textBoxDivisionName.MaxLength = 64;
			textBoxDivisionName.Name = "textBoxDivisionName";
			textBoxDivisionName.ReadOnly = true;
			textBoxDivisionName.Size = new System.Drawing.Size(265, 20);
			textBoxDivisionName.TabIndex = 3;
			textBoxDivisionName.TabStop = false;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel8);
			ultraGroupBox1.Controls.Add(dateTimeInsExpiry);
			ultraGroupBox1.Controls.Add(textBoxInsurancePolicyNo);
			ultraGroupBox1.Controls.Add(textBoxInsuranceVendorName);
			ultraGroupBox1.Controls.Add(mmLabel15);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(comboBoxInsuranceVendor);
			ultraGroupBox1.Location = new System.Drawing.Point(0, 375);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(613, 68);
			ultraGroupBox1.TabIndex = 25;
			ultraGroupBox1.Text = "Insurance Information";
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(10, 24);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(94, 14);
			ultraFormattedLinkLabel8.TabIndex = 57;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Insurance Vendor:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			textBoxInsurancePolicyNo.BackColor = System.Drawing.Color.White;
			textBoxInsurancePolicyNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxInsurancePolicyNo.CustomReportFieldName = "";
			textBoxInsurancePolicyNo.CustomReportKey = "";
			textBoxInsurancePolicyNo.CustomReportValueType = 1;
			textBoxInsurancePolicyNo.IsComboTextBox = false;
			textBoxInsurancePolicyNo.IsModified = false;
			textBoxInsurancePolicyNo.Location = new System.Drawing.Point(129, 41);
			textBoxInsurancePolicyNo.MaxLength = 20;
			textBoxInsurancePolicyNo.Name = "textBoxInsurancePolicyNo";
			textBoxInsurancePolicyNo.Size = new System.Drawing.Size(162, 20);
			textBoxInsurancePolicyNo.TabIndex = 2;
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(298, 44);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(114, 13);
			mmLabel15.TabIndex = 39;
			mmLabel15.Text = "Insurance Expiry Date:";
			mmLabel14.AutoSize = true;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(10, 44);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(105, 13);
			mmLabel14.TabIndex = 38;
			mmLabel14.Text = "Insurance Policy No:";
			comboBoxInsuranceVendor.Assigned = false;
			comboBoxInsuranceVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxInsuranceVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInsuranceVendor.CustomReportFieldName = "";
			comboBoxInsuranceVendor.CustomReportKey = "";
			comboBoxInsuranceVendor.CustomReportValueType = 1;
			comboBoxInsuranceVendor.DescriptionTextBox = textBoxInsuranceVendorName;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInsuranceVendor.DisplayLayout.Appearance = appearance2;
			comboBoxInsuranceVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInsuranceVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInsuranceVendor.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInsuranceVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxInsuranceVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInsuranceVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxInsuranceVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInsuranceVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInsuranceVendor.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInsuranceVendor.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxInsuranceVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInsuranceVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInsuranceVendor.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInsuranceVendor.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxInsuranceVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInsuranceVendor.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInsuranceVendor.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxInsuranceVendor.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxInsuranceVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInsuranceVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxInsuranceVendor.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxInsuranceVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInsuranceVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxInsuranceVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInsuranceVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInsuranceVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInsuranceVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInsuranceVendor.Editable = true;
			comboBoxInsuranceVendor.FilterString = "";
			comboBoxInsuranceVendor.FilterSysDocID = "";
			comboBoxInsuranceVendor.HasAll = false;
			comboBoxInsuranceVendor.HasCustom = false;
			comboBoxInsuranceVendor.IsDataLoaded = false;
			comboBoxInsuranceVendor.Location = new System.Drawing.Point(129, 19);
			comboBoxInsuranceVendor.MaxDropDownItems = 12;
			comboBoxInsuranceVendor.Name = "comboBoxInsuranceVendor";
			comboBoxInsuranceVendor.ShowConsignmentOnly = false;
			comboBoxInsuranceVendor.ShowQuickAdd = true;
			comboBoxInsuranceVendor.Size = new System.Drawing.Size(162, 20);
			comboBoxInsuranceVendor.TabIndex = 0;
			comboBoxInsuranceVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel7);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel6);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel5);
			ultraGroupBox2.Controls.Add(textBoxDivisionName);
			ultraGroupBox2.Controls.Add(textBoxLocationName);
			ultraGroupBox2.Controls.Add(textBoxFixedAssetName);
			ultraGroupBox2.Controls.Add(comboBoxDivision);
			ultraGroupBox2.Controls.Add(comboBoxLocation);
			ultraGroupBox2.Controls.Add(comboBoxFixedAsset);
			ultraGroupBox2.Location = new System.Drawing.Point(0, 441);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(613, 93);
			ultraGroupBox2.TabIndex = 26;
			ultraGroupBox2.Text = "Fixed Asset Information";
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(12, 65);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabel7.TabIndex = 57;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Fixed Asset ID:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(12, 44);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(60, 14);
			ultraFormattedLinkLabel6.TabIndex = 56;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Division ID:";
			appearance15.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance15;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(63, 14);
			ultraFormattedLinkLabel5.TabIndex = 55;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Location ID:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance16;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxDivision.Assigned = false;
			comboBoxDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDivision.CustomReportFieldName = "";
			comboBoxDivision.CustomReportKey = "";
			comboBoxDivision.CustomReportValueType = 1;
			comboBoxDivision.DescriptionTextBox = textBoxDivisionName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDivision.DisplayLayout.Appearance = appearance17;
			comboBoxDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDivision.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDivision.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDivision.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDivision.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxDivision.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxDivision.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDivision.Editable = true;
			comboBoxDivision.FilterString = "";
			comboBoxDivision.HasAllAccount = false;
			comboBoxDivision.HasCustom = false;
			comboBoxDivision.IsDataLoaded = false;
			comboBoxDivision.Location = new System.Drawing.Point(129, 40);
			comboBoxDivision.MaxDropDownItems = 12;
			comboBoxDivision.Name = "comboBoxDivision";
			comboBoxDivision.ShowInactiveItems = false;
			comboBoxDivision.ShowQuickAdd = true;
			comboBoxDivision.Size = new System.Drawing.Size(162, 20);
			comboBoxDivision.TabIndex = 2;
			comboBoxDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = textBoxLocationName;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance29;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(129, 18);
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
			comboBoxLocation.Size = new System.Drawing.Size(162, 20);
			comboBoxLocation.TabIndex = 0;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFixedAsset.Assigned = false;
			comboBoxFixedAsset.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFixedAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFixedAsset.CustomReportFieldName = "";
			comboBoxFixedAsset.CustomReportKey = "";
			comboBoxFixedAsset.CustomReportValueType = 1;
			comboBoxFixedAsset.DescriptionTextBox = textBoxFixedAssetName;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFixedAsset.DisplayLayout.Appearance = appearance41;
			comboBoxFixedAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFixedAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFixedAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxFixedAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFixedAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFixedAsset.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFixedAsset.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxFixedAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFixedAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAsset.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFixedAsset.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxFixedAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFixedAsset.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFixedAsset.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxFixedAsset.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxFixedAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFixedAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxFixedAsset.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxFixedAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFixedAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxFixedAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFixedAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFixedAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFixedAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFixedAsset.Editable = true;
			comboBoxFixedAsset.FilterString = "";
			comboBoxFixedAsset.HasAllAccount = false;
			comboBoxFixedAsset.HasCustom = false;
			comboBoxFixedAsset.IsDataLoaded = false;
			comboBoxFixedAsset.Location = new System.Drawing.Point(129, 63);
			comboBoxFixedAsset.MaxDropDownItems = 12;
			comboBoxFixedAsset.Name = "comboBoxFixedAsset";
			comboBoxFixedAsset.ShowInactiveItems = false;
			comboBoxFixedAsset.ShowQuickAdd = true;
			comboBoxFixedAsset.Size = new System.Drawing.Size(162, 20);
			comboBoxFixedAsset.TabIndex = 4;
			comboBoxFixedAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(13, 128);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(27, 14);
			linkLabelCountry.TabIndex = 50;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "City:";
			appearance53.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance53;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(13, 150);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(46, 14);
			ultraFormattedLinkLabel1.TabIndex = 51;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Country:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance54;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(13, 173);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(74, 14);
			ultraFormattedLinkLabel2.TabIndex = 52;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Working Area:";
			appearance55.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance55;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(13, 105);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(71, 14);
			ultraFormattedLinkLabel3.TabIndex = 53;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Vehicle Type:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(13, 285);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(58, 14);
			ultraFormattedLinkLabel4.TabIndex = 54;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Owned By:";
			appearance57.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance57;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(311, 36);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(65, 16);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(13, 306);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(78, 14);
			ultraFormattedLinkLabel16.TabIndex = 81;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.Never;
			ultraFormattedLinkLabel16.Value = "Analysis Code:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance58;
			ultraFormattedLinkLabel16.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel16_LinkClicked);
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(554, 171);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 84;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance59.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance59;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(515, 171);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 83;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance60.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance60;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(515, 39);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 82;
			pictureBoxPhoto.TabStop = false;
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(554, 86);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 86;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance61.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance61;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(592, 99);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 87;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(15, 353);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(37, 14);
			ultraFormattedLinkLabel9.TabIndex = 92;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Origin:";
			appearance62.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance62;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance63;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(131, 283);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(162, 20);
			comboBoxEmployee.TabIndex = 18;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			countryComboBoxOrigin.Assigned = false;
			countryComboBoxOrigin.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			countryComboBoxOrigin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			countryComboBoxOrigin.CustomReportFieldName = "";
			countryComboBoxOrigin.CustomReportKey = "";
			countryComboBoxOrigin.CustomReportValueType = 1;
			countryComboBoxOrigin.DescriptionTextBox = null;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			countryComboBoxOrigin.DisplayLayout.Appearance = appearance75;
			countryComboBoxOrigin.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			countryComboBoxOrigin.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			countryComboBoxOrigin.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			countryComboBoxOrigin.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			countryComboBoxOrigin.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			countryComboBoxOrigin.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			countryComboBoxOrigin.DisplayLayout.MaxColScrollRegions = 1;
			countryComboBoxOrigin.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			countryComboBoxOrigin.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			countryComboBoxOrigin.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			countryComboBoxOrigin.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			countryComboBoxOrigin.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			countryComboBoxOrigin.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			countryComboBoxOrigin.DisplayLayout.Override.CellAppearance = appearance82;
			countryComboBoxOrigin.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			countryComboBoxOrigin.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			countryComboBoxOrigin.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			countryComboBoxOrigin.DisplayLayout.Override.HeaderAppearance = appearance84;
			countryComboBoxOrigin.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			countryComboBoxOrigin.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			countryComboBoxOrigin.DisplayLayout.Override.RowAppearance = appearance85;
			countryComboBoxOrigin.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			countryComboBoxOrigin.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			countryComboBoxOrigin.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			countryComboBoxOrigin.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			countryComboBoxOrigin.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			countryComboBoxOrigin.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			countryComboBoxOrigin.Editable = true;
			countryComboBoxOrigin.FilterString = "";
			countryComboBoxOrigin.HasAllAccount = false;
			countryComboBoxOrigin.HasCustom = false;
			countryComboBoxOrigin.IsDataLoaded = false;
			countryComboBoxOrigin.Location = new System.Drawing.Point(131, 349);
			countryComboBoxOrigin.MaxDropDownItems = 12;
			countryComboBoxOrigin.Name = "countryComboBoxOrigin";
			countryComboBoxOrigin.ShowInactiveItems = false;
			countryComboBoxOrigin.ShowQuickAdd = true;
			countryComboBoxOrigin.Size = new System.Drawing.Size(162, 20);
			countryComboBoxOrigin.TabIndex = 24;
			countryComboBoxOrigin.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmTextBoxPlateNo.BackColor = System.Drawing.Color.White;
			mmTextBoxPlateNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			mmTextBoxPlateNo.CustomReportFieldName = "";
			mmTextBoxPlateNo.CustomReportKey = "";
			mmTextBoxPlateNo.CustomReportValueType = 1;
			mmTextBoxPlateNo.IsComboTextBox = false;
			mmTextBoxPlateNo.IsModified = false;
			mmTextBoxPlateNo.Location = new System.Drawing.Point(393, 327);
			mmTextBoxPlateNo.MaxLength = 64;
			mmTextBoxPlateNo.Name = "mmTextBoxPlateNo";
			mmTextBoxPlateNo.Size = new System.Drawing.Size(145, 20);
			mmTextBoxPlateNo.TabIndex = 23;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(302, 332);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(51, 13);
			mmLabel4.TabIndex = 91;
			mmLabel4.Text = "Plate No:";
			mmTextBoxTrafficNo.BackColor = System.Drawing.Color.White;
			mmTextBoxTrafficNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			mmTextBoxTrafficNo.CustomReportFieldName = "";
			mmTextBoxTrafficNo.CustomReportKey = "";
			mmTextBoxTrafficNo.CustomReportValueType = 1;
			mmTextBoxTrafficNo.IsComboTextBox = false;
			mmTextBoxTrafficNo.IsModified = false;
			mmTextBoxTrafficNo.Location = new System.Drawing.Point(131, 327);
			mmTextBoxTrafficNo.MaxLength = 64;
			mmTextBoxTrafficNo.Name = "mmTextBoxTrafficNo";
			mmTextBoxTrafficNo.Size = new System.Drawing.Size(162, 20);
			mmTextBoxTrafficNo.TabIndex = 22;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(13, 331);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(76, 13);
			mmLabel3.TabIndex = 89;
			mmLabel3.Text = "Traffic File No:";
			comboBoxAnalysis.Assigned = false;
			comboBoxAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysis.CustomReportFieldName = "";
			comboBoxAnalysis.CustomReportKey = "";
			comboBoxAnalysis.CustomReportValueType = 1;
			comboBoxAnalysis.DescriptionTextBox = null;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance87;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance88;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance89;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance90.BackColor2 = System.Drawing.SystemColors.Control;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance90;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance91;
			appearance92.BackColor = System.Drawing.SystemColors.Highlight;
			appearance92.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance92;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance93;
			appearance94.BorderColor = System.Drawing.Color.Silver;
			appearance94.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance94;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance95.BackColor = System.Drawing.SystemColors.Control;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance95;
			appearance96.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance96;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance97;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance98;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(131, 305);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ReadOnly = true;
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(162, 20);
			comboBoxAnalysis.TabIndex = 20;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVehicleWeight.AllowDecimal = true;
			textBoxVehicleWeight.CustomReportFieldName = "";
			textBoxVehicleWeight.CustomReportKey = "";
			textBoxVehicleWeight.CustomReportValueType = 1;
			textBoxVehicleWeight.IsComboTextBox = false;
			textBoxVehicleWeight.IsModified = false;
			textBoxVehicleWeight.Location = new System.Drawing.Point(393, 305);
			textBoxVehicleWeight.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxVehicleWeight.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxVehicleWeight.Name = "textBoxVehicleWeight";
			textBoxVehicleWeight.NullText = "0";
			textBoxVehicleWeight.Size = new System.Drawing.Size(145, 20);
			textBoxVehicleWeight.TabIndex = 21;
			textBoxVehicleWeight.Text = "0.00";
			textBoxVehicleWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxVehicleWeight.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxWeightCapacity.AllowDecimal = true;
			textBoxWeightCapacity.CustomReportFieldName = "";
			textBoxWeightCapacity.CustomReportKey = "";
			textBoxWeightCapacity.CustomReportValueType = 1;
			textBoxWeightCapacity.IsComboTextBox = false;
			textBoxWeightCapacity.IsModified = false;
			textBoxWeightCapacity.Location = new System.Drawing.Point(131, 261);
			textBoxWeightCapacity.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxWeightCapacity.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxWeightCapacity.Name = "textBoxWeightCapacity";
			textBoxWeightCapacity.NullText = "0";
			textBoxWeightCapacity.Size = new System.Drawing.Size(162, 20);
			textBoxWeightCapacity.TabIndex = 16;
			textBoxWeightCapacity.Text = "0.00";
			textBoxWeightCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxWeightCapacity.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxCity.Assigned = false;
			comboBoxCity.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCity.CustomReportFieldName = "";
			comboBoxCity.CustomReportKey = "";
			comboBoxCity.CustomReportValueType = 1;
			comboBoxCity.DescriptionTextBox = textBoxCityName;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCity.DisplayLayout.Appearance = appearance99;
			comboBoxCity.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCity.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance100.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance100.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCity.DisplayLayout.GroupByBox.Appearance = appearance100;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCity.DisplayLayout.GroupByBox.BandLabelAppearance = appearance101;
			comboBoxCity.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance102.BackColor2 = System.Drawing.SystemColors.Control;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCity.DisplayLayout.GroupByBox.PromptAppearance = appearance102;
			comboBoxCity.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCity.DisplayLayout.MaxRowScrollRegions = 1;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCity.DisplayLayout.Override.ActiveCellAppearance = appearance103;
			appearance104.BackColor = System.Drawing.SystemColors.Highlight;
			appearance104.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCity.DisplayLayout.Override.ActiveRowAppearance = appearance104;
			comboBoxCity.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCity.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCity.DisplayLayout.Override.CardAreaAppearance = appearance105;
			appearance106.BorderColor = System.Drawing.Color.Silver;
			appearance106.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCity.DisplayLayout.Override.CellAppearance = appearance106;
			comboBoxCity.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCity.DisplayLayout.Override.CellPadding = 0;
			appearance107.BackColor = System.Drawing.SystemColors.Control;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCity.DisplayLayout.Override.GroupByRowAppearance = appearance107;
			appearance108.TextHAlignAsString = "Left";
			comboBoxCity.DisplayLayout.Override.HeaderAppearance = appearance108;
			comboBoxCity.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCity.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.Color.Silver;
			comboBoxCity.DisplayLayout.Override.RowAppearance = appearance109;
			comboBoxCity.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCity.DisplayLayout.Override.TemplateAddRowAppearance = appearance110;
			comboBoxCity.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCity.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCity.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCity.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCity.Editable = true;
			comboBoxCity.FilterString = "";
			comboBoxCity.HasAllAccount = false;
			comboBoxCity.HasCustom = false;
			comboBoxCity.IsDataLoaded = false;
			comboBoxCity.Location = new System.Drawing.Point(131, 126);
			comboBoxCity.MaxDropDownItems = 12;
			comboBoxCity.Name = "comboBoxCity";
			comboBoxCity.ShowInactiveItems = false;
			comboBoxCity.ShowQuickAdd = true;
			comboBoxCity.Size = new System.Drawing.Size(162, 20);
			comboBoxCity.TabIndex = 6;
			comboBoxCity.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicleType.Assigned = false;
			comboBoxVehicleType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicleType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicleType.CustomReportFieldName = "";
			comboBoxVehicleType.CustomReportKey = "";
			comboBoxVehicleType.CustomReportValueType = 1;
			comboBoxVehicleType.DescriptionTextBox = null;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicleType.DisplayLayout.Appearance = appearance111;
			comboBoxVehicleType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicleType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance112.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance112.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleType.DisplayLayout.GroupByBox.Appearance = appearance112;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance113;
			comboBoxVehicleType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance114.BackColor2 = System.Drawing.SystemColors.Control;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleType.DisplayLayout.GroupByBox.PromptAppearance = appearance114;
			comboBoxVehicleType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicleType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicleType.DisplayLayout.Override.ActiveCellAppearance = appearance115;
			appearance116.BackColor = System.Drawing.SystemColors.Highlight;
			appearance116.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicleType.DisplayLayout.Override.ActiveRowAppearance = appearance116;
			comboBoxVehicleType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicleType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleType.DisplayLayout.Override.CardAreaAppearance = appearance117;
			appearance118.BorderColor = System.Drawing.Color.Silver;
			appearance118.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicleType.DisplayLayout.Override.CellAppearance = appearance118;
			comboBoxVehicleType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicleType.DisplayLayout.Override.CellPadding = 0;
			appearance119.BackColor = System.Drawing.SystemColors.Control;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleType.DisplayLayout.Override.GroupByRowAppearance = appearance119;
			appearance120.TextHAlignAsString = "Left";
			comboBoxVehicleType.DisplayLayout.Override.HeaderAppearance = appearance120;
			comboBoxVehicleType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicleType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicleType.DisplayLayout.Override.RowAppearance = appearance121;
			comboBoxVehicleType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicleType.DisplayLayout.Override.TemplateAddRowAppearance = appearance122;
			comboBoxVehicleType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicleType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicleType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicleType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicleType.Editable = true;
			comboBoxVehicleType.FilterString = "";
			comboBoxVehicleType.GenericListType = Micromind.Common.Data.GenericListTypes.All;
			comboBoxVehicleType.HasAllAccount = false;
			comboBoxVehicleType.HasCustom = false;
			comboBoxVehicleType.IsDataLoaded = false;
			comboBoxVehicleType.IsSingleColumn = false;
			comboBoxVehicleType.Location = new System.Drawing.Point(131, 103);
			comboBoxVehicleType.MaxDropDownItems = 12;
			comboBoxVehicleType.Name = "comboBoxVehicleType";
			comboBoxVehicleType.ShowInactiveItems = false;
			comboBoxVehicleType.ShowQuickAdd = true;
			comboBoxVehicleType.Size = new System.Drawing.Size(126, 20);
			comboBoxVehicleType.TabIndex = 4;
			comboBoxVehicleType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxTrackingNo.BackColor = System.Drawing.Color.White;
			textBoxTrackingNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTrackingNo.CustomReportFieldName = "";
			textBoxTrackingNo.CustomReportKey = "";
			textBoxTrackingNo.CustomReportValueType = 1;
			textBoxTrackingNo.IsComboTextBox = false;
			textBoxTrackingNo.IsModified = false;
			textBoxTrackingNo.Location = new System.Drawing.Point(393, 283);
			textBoxTrackingNo.MaxLength = 30;
			textBoxTrackingNo.Name = "textBoxTrackingNo";
			textBoxTrackingNo.Size = new System.Drawing.Size(145, 20);
			textBoxTrackingNo.TabIndex = 19;
			textBoxModel.BackColor = System.Drawing.Color.White;
			textBoxModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxModel.CustomReportFieldName = "";
			textBoxModel.CustomReportKey = "";
			textBoxModel.CustomReportValueType = 1;
			textBoxModel.IsComboTextBox = false;
			textBoxModel.IsModified = false;
			textBoxModel.Location = new System.Drawing.Point(131, 240);
			textBoxModel.MaxLength = 15;
			textBoxModel.Name = "textBoxModel";
			textBoxModel.Size = new System.Drawing.Size(162, 20);
			textBoxModel.TabIndex = 14;
			textBoxFuel.BackColor = System.Drawing.Color.White;
			textBoxFuel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFuel.CustomReportFieldName = "";
			textBoxFuel.CustomReportKey = "";
			textBoxFuel.CustomReportValueType = 1;
			textBoxFuel.IsComboTextBox = false;
			textBoxFuel.IsModified = false;
			textBoxFuel.Location = new System.Drawing.Point(393, 261);
			textBoxFuel.MaxLength = 15;
			textBoxFuel.Name = "textBoxFuel";
			textBoxFuel.Size = new System.Drawing.Size(145, 20);
			textBoxFuel.TabIndex = 17;
			textBoxColor.BackColor = System.Drawing.Color.White;
			textBoxColor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxColor.CustomReportFieldName = "";
			textBoxColor.CustomReportKey = "";
			textBoxColor.CustomReportValueType = 1;
			textBoxColor.IsComboTextBox = false;
			textBoxColor.IsModified = false;
			textBoxColor.Location = new System.Drawing.Point(393, 239);
			textBoxColor.MaxLength = 15;
			textBoxColor.Name = "textBoxColor";
			textBoxColor.Size = new System.Drawing.Size(145, 20);
			textBoxColor.TabIndex = 15;
			textBoxChasisNo.BackColor = System.Drawing.Color.White;
			textBoxChasisNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxChasisNo.CustomReportFieldName = "";
			textBoxChasisNo.CustomReportKey = "";
			textBoxChasisNo.CustomReportValueType = 1;
			textBoxChasisNo.IsComboTextBox = false;
			textBoxChasisNo.IsModified = false;
			textBoxChasisNo.Location = new System.Drawing.Point(131, 193);
			textBoxChasisNo.MaxLength = 20;
			textBoxChasisNo.Name = "textBoxChasisNo";
			textBoxChasisNo.Size = new System.Drawing.Size(162, 20);
			textBoxChasisNo.TabIndex = 12;
			comboBoxArea.Assigned = false;
			comboBoxArea.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArea.CustomReportFieldName = "";
			comboBoxArea.CustomReportKey = "";
			comboBoxArea.CustomReportValueType = 1;
			comboBoxArea.DescriptionTextBox = textBoxAreaName;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArea.DisplayLayout.Appearance = appearance123;
			comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance124.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance124.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance124;
			appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance125;
			comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance126.BackColor2 = System.Drawing.SystemColors.Control;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance126;
			comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance127;
			appearance128.BackColor = System.Drawing.SystemColors.Highlight;
			appearance128.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance128;
			comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance129;
			appearance130.BorderColor = System.Drawing.Color.Silver;
			appearance130.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArea.DisplayLayout.Override.CellAppearance = appearance130;
			comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArea.DisplayLayout.Override.CellPadding = 0;
			appearance131.BackColor = System.Drawing.SystemColors.Control;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance131;
			appearance132.TextHAlignAsString = "Left";
			comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance132;
			comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.Color.Silver;
			comboBoxArea.DisplayLayout.Override.RowAppearance = appearance133;
			comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance134;
			comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArea.Editable = true;
			comboBoxArea.FilterString = "";
			comboBoxArea.HasAllAccount = false;
			comboBoxArea.HasCustom = false;
			comboBoxArea.IsDataLoaded = false;
			comboBoxArea.Location = new System.Drawing.Point(131, 171);
			comboBoxArea.MaxDropDownItems = 12;
			comboBoxArea.Name = "comboBoxArea";
			comboBoxArea.ShowInactiveItems = false;
			comboBoxArea.ShowQuickAdd = true;
			comboBoxArea.Size = new System.Drawing.Size(162, 20);
			comboBoxArea.TabIndex = 10;
			comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel21.AutoSize = true;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(302, 309);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(82, 13);
			mmLabel21.TabIndex = 47;
			mmLabel21.Text = "Vehicle Weight:";
			mmLabel20.AutoSize = true;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(13, 264);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(88, 13);
			mmLabel20.TabIndex = 46;
			mmLabel20.Text = "Weight Capacity:";
			mmLabel20.Click += new System.EventHandler(mmLabel20_Click);
			mmLabel17.AutoSize = true;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(302, 286);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(69, 13);
			mmLabel17.TabIndex = 41;
			mmLabel17.Text = "Tracking No:";
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(260, 107);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(123, 13);
			mmLabel16.TabIndex = 40;
			mmLabel16.Text = "Registration Expiry Date:";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(13, 244);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(39, 13);
			mmLabel11.TabIndex = 33;
			mmLabel11.Text = "Model:";
			mmLabel11.Click += new System.EventHandler(mmLabel11_Click);
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(302, 264);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(30, 13);
			mmLabel10.TabIndex = 32;
			mmLabel10.Text = "Fuel:";
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(13, 220);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(32, 13);
			mmLabel9.TabIndex = 31;
			mmLabel9.Text = "Year:";
			mmLabel9.Click += new System.EventHandler(mmLabel9_Click);
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(302, 242);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(34, 13);
			mmLabel8.TabIndex = 30;
			mmLabel8.Text = "Color:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(13, 197);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(58, 13);
			mmLabel6.TabIndex = 28;
			mmLabel6.Text = "Chasis No:";
			mmLabel6.Click += new System.EventHandler(mmLabel6_Click);
			comboBoxCountry.Assigned = false;
			comboBoxCountry.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = textBoxCountryName;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance135;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance136.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance136.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance136.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance136;
			appearance137.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance137;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance138.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance138.BackColor2 = System.Drawing.SystemColors.Control;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance138;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance139;
			appearance140.BackColor = System.Drawing.SystemColors.Highlight;
			appearance140.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance140;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance141;
			appearance142.BorderColor = System.Drawing.Color.Silver;
			appearance142.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance142;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance143.BackColor = System.Drawing.SystemColors.Control;
			appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance143.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance143.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance143;
			appearance144.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance144;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance145;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance146;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(131, 148);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(162, 20);
			comboBoxCountry.TabIndex = 8;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRegistrationNo.BackColor = System.Drawing.Color.White;
			textBoxRegistrationNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRegistrationNo.CustomReportFieldName = "";
			textBoxRegistrationNo.CustomReportKey = "";
			textBoxRegistrationNo.CustomReportValueType = 1;
			textBoxRegistrationNo.IsComboTextBox = false;
			textBoxRegistrationNo.IsModified = false;
			textBoxRegistrationNo.Location = new System.Drawing.Point(131, 80);
			textBoxRegistrationNo.MaxLength = 15;
			textBoxRegistrationNo.Name = "textBoxRegistrationNo";
			textBoxRegistrationNo.Size = new System.Drawing.Size(162, 20);
			textBoxRegistrationNo.TabIndex = 3;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(13, 84);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(83, 13);
			mmLabel2.TabIndex = 21;
			mmLabel2.Text = "Registration No:";
			mmLabel2.Click += new System.EventHandler(mmLabel2_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 24;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(131, 58);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(205, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(131, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(162, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(13, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(89, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Vehicle Name:";
			mmLabel1.Click += new System.EventHandler(mmLabel1_Click);
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(13, 37);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(86, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Vehicle Code:";
			labelCode.Click += new System.EventHandler(labelCode_Click);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(129, 530);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(430, 47);
			textBoxNote.TabIndex = 93;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 530);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 94;
			label3.Text = "Note:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(655, 623);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(countryComboBoxOrigin);
			base.Controls.Add(ultraFormattedLinkLabel9);
			base.Controls.Add(mmTextBoxPlateNo);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmTextBoxTrafficNo);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(linkLoadImage);
			base.Controls.Add(linkRemovePicture);
			base.Controls.Add(linkAddPicture);
			base.Controls.Add(ultraFormattedLinkLabel16);
			base.Controls.Add(comboBoxAnalysis);
			base.Controls.Add(checkBoxIsInactive);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(linkLabelCountry);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBoxVehicleWeight);
			base.Controls.Add(textBoxWeightCapacity);
			base.Controls.Add(comboBoxCity);
			base.Controls.Add(comboBoxVehicleType);
			base.Controls.Add(textBoxTrackingNo);
			base.Controls.Add(textBoxModel);
			base.Controls.Add(dateTimeRegExpiry);
			base.Controls.Add(textBoxFuel);
			base.Controls.Add(comboBoxYear);
			base.Controls.Add(textBoxColor);
			base.Controls.Add(textBoxChasisNo);
			base.Controls.Add(textBoxAreaName);
			base.Controls.Add(comboBoxArea);
			base.Controls.Add(mmLabel21);
			base.Controls.Add(mmLabel20);
			base.Controls.Add(mmLabel17);
			base.Controls.Add(mmLabel16);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(textBoxCountryName);
			base.Controls.Add(comboBoxCountry);
			base.Controls.Add(textBoxRegistrationNo);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxCityName);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(pictureBoxPhoto);
			base.Controls.Add(pictureBoxNoImage);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "VehicleDetailsForm";
			Text = "Vehicle";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(VehicleDetailsForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFixedAsset).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)countryComboBoxOrigin).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCity).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
