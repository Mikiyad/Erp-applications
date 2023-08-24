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
	public class PropertyUnitDetailsForm : Form, IForm
	{
		private PropertyUnitData currentData;

		private const string TABLENAME_CONST = "Property_Unit";

		private const string IDFIELD_CONST = "PropertyUnitID";

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

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel6;

		private MMLabel mmLabel9;

		private ComboBox comboBoxStatus;

		private MMLabel mmLabel1;

		private UltraFormattedLinkLabel linkLabelCustomerClass;

		private MMLabel mmLabel5;

		private MMLabel labelCustomerNumber;

		private MMTextBox textBoxShortName;

		private MMTextBox textBoxCode;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxName;

		private MMLabel label1;

		private UltraTabPageControl tabPageUserDefined;

		private UDFEntryControl udfEntryGrid;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private Panel panel1;

		private MMLabel labelCustomerNameHeader;

		private XPButton buttonCategories;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMLabel mmLabel7;

		private flatDatePicker dateTimePickerToDate;

		private flatDatePicker dateTimePickerFromDate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private MMLabel mmLabel17;

		private MMLabel mmLabel16;

		private MMLabel mmLabel15;

		private MMLabel mmLabel4;

		private PropertyComboBox ComboBoxProperty;

		private GenericListComboBox ComboBoxUnitType;

		private GenericListComboBox ComboBoxViewType;

		private AmountTextBox textBoxArea;

		private NumberTextBox textBoxNoofBedrooms;

		private NumberTextBox textBoxNoofBathRooms;

		private NumberTextBox textBoxTotalrooms;

		private NumberTextBox textBoxNoofparking;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private MMLabel mmLabel2;

		private RadioButton radioButtonResidential;

		private RadioButton radioButtonCommercial;

		private UltraGroupBox ultraGroupBox7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel3;

		private ComboBox comboBoxTaxOption;

		private PictureBox pictureBoxNoImage;

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

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonShowPicture;

		private OpenFileDialog openFileDialog1;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private AmountTextBox amountTextBox;

		private MMLabel mmLabel8;

		private MMLabel mmLabel23;

		private UltraTabPageControl ultraTabPageControl2;

		private MMLabel mmLabel13;

		private MMTextBox textBoxElectricityPermitNumber;

		private MMLabel mmLabel12;

		private MMTextBox textBoxElectricityFileNo;

		private MMLabel mmLabel11;

		private MMTextBox textBoxMunicipalityPermitNo;

		private MMLabel mmLabel10;

		private MMTextBox textBoxMunicipalityFileNo;

		private MMLabel mmLabel19;

		private MMTextBox textBoxElectricityPremiseNo;

		private PropertyUnitComboBox comboBoxPropertyParentUnit;

		private MMLabel mmLabel18;

		private MMLabel mmLabel14;

		private ContextMenuStrip contextMenuStripCurrentDetails;

		private ToolStripMenuItem currentDetailsToolStripMenuItem;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelKitchenType;

		private GenericListComboBox comboBoxKitchenType;

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
				currentDetailsToolStripMenuItem.Enabled = !value;
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

		public PropertyUnitDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxStatus.SelectedIndex = 0;
		}

		private void AddEvents()
		{
			base.Load += PropertyUnitDetailsForm_Load;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PropertyUnitData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PropertyUnitTable.Rows[0] : currentData.PropertyUnitTable.NewRow();
				dataRow.BeginEdit();
				dataRow["PropertyUnitID"] = textBoxCode.Text.Trim();
				dataRow["PropertyUnitName"] = textBoxName.Text.Trim();
				dataRow["ShortName"] = textBoxShortName.Text.Trim();
				dataRow["UnitStatus"] = checked(comboBoxStatus.SelectedIndex + 1);
				dataRow["AvailableFrom"] = dateTimePickerFromDate.Value;
				dataRow["AvailableTo"] = dateTimePickerToDate.Value;
				if (ComboBoxProperty.SelectedID != "")
				{
					dataRow["PropertyID"] = ComboBoxProperty.SelectedID;
				}
				else
				{
					dataRow["PropertyID"] = DBNull.Value;
				}
				if (ComboBoxProperty.SelectedID != "")
				{
					dataRow["PropertyID"] = ComboBoxProperty.SelectedID;
				}
				else
				{
					dataRow["PropertyID"] = DBNull.Value;
				}
				if (ComboBoxUnitType.SelectedID != "")
				{
					dataRow["UnitTypeID"] = ComboBoxUnitType.SelectedID;
				}
				else
				{
					dataRow["UnitTypeID"] = DBNull.Value;
				}
				if (comboBoxPropertyParentUnit.SelectedID != "")
				{
					dataRow["ParentUnitID"] = comboBoxPropertyParentUnit.SelectedID;
				}
				else
				{
					dataRow["ParentUnitID"] = DBNull.Value;
				}
				if (textBoxNoofBedrooms.Text != "")
				{
					dataRow["NoBedRooms"] = textBoxNoofBedrooms.Text.Trim();
				}
				else
				{
					dataRow["NoBedRooms"] = 0;
				}
				if (textBoxNoofBathRooms.Text != "")
				{
					dataRow["NoBathRooms"] = textBoxNoofBathRooms.Text.Trim();
				}
				else
				{
					dataRow["NoBathRooms"] = 0;
				}
				if (textBoxTotalrooms.Text != "")
				{
					dataRow["TotalRooms"] = textBoxTotalrooms.Text.Trim();
				}
				else
				{
					dataRow["TotalRooms"] = 0;
				}
				if (textBoxNoofparking.Text != "")
				{
					dataRow["NoofParking"] = textBoxNoofparking.Text.Trim();
				}
				else
				{
					dataRow["NoofParking"] = 0;
				}
				dataRow["Area"] = textBoxArea.Text.Trim();
				if (ComboBoxViewType.SelectedID != "")
				{
					dataRow["ViewTypeID"] = ComboBoxViewType.SelectedID;
				}
				else
				{
					dataRow["ViewTypeID"] = DBNull.Value;
				}
				dataRow["Status"] = checkBoxIsInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				if (radioButtonCommercial.Checked)
				{
					dataRow["PropertyType"] = PropertyType.Commercial;
				}
				else
				{
					dataRow["PropertyType"] = PropertyType.Residential;
				}
				dataRow["ElectricityFileNumber"] = textBoxElectricityFileNo.Text;
				dataRow["ElectricityPermitNumber"] = textBoxElectricityPermitNumber.Text;
				dataRow["ElectricityPremiseNumber"] = textBoxElectricityPremiseNo.Text;
				dataRow["MunicipalityFileNumber"] = textBoxMunicipalityFileNo.Text;
				dataRow["MunicipalityPermitNumber"] = textBoxMunicipalityPermitNo.Text;
				if (!string.IsNullOrEmpty(comboBoxKitchenType.SelectedID))
				{
					dataRow["KitchenTypeID"] = comboBoxKitchenType.SelectedID;
				}
				else
				{
					dataRow["KitchenTypeID"] = DBNull.Value;
				}
				if (amountTextBox.Text != "")
				{
					dataRow["RentalIncome"] = amountTextBox.Text;
				}
				else
				{
					dataRow["RentalIncome"] = 0;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PropertyUnitTable.Rows.Add(dataRow);
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
					currentData = Factory.PropertyUnitSystem.GetPropertyUnitByID(id.Trim());
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
			textBoxCode.Text = dataRow["PropertyUnitID"].ToString();
			textBoxName.Text = dataRow["PropertyUnitName"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (dataRow["UnitStatus"] != DBNull.Value)
			{
				comboBoxStatus.SelectedIndex = checked(int.Parse(dataRow["UnitStatus"].ToString()) - 1);
			}
			else
			{
				comboBoxStatus.SelectedIndex = 0;
			}
			textBoxShortName.Text = dataRow["ShortName"].ToString();
			dateTimePickerFromDate.Value = DateTime.Parse(dataRow["AvailableFrom"].ToString());
			dateTimePickerToDate.Value = DateTime.Parse(dataRow["AvailableTo"].ToString());
			if (dataRow["PropertyID"] != DBNull.Value)
			{
				ComboBoxProperty.SelectedID = dataRow["PropertyID"].ToString();
			}
			if (dataRow["UnitTypeID"] != DBNull.Value)
			{
				ComboBoxUnitType.SelectedID = dataRow["UnitTypeID"].ToString();
			}
			if (dataRow["ParentUnitID"] != DBNull.Value)
			{
				comboBoxPropertyParentUnit.SelectedID = dataRow["ParentUnitID"].ToString();
			}
			textBoxNoofBedrooms.Text = dataRow["NoBedRooms"].ToString();
			textBoxNoofBathRooms.Text = dataRow["NoBathRooms"].ToString();
			textBoxTotalrooms.Text = dataRow["TotalRooms"].ToString();
			textBoxArea.Text = dataRow["Area"].ToString();
			textBoxNoofparking.Text = dataRow["NoofParking"].ToString();
			if (!string.IsNullOrEmpty(dataRow["PropertyType"].ToString()))
			{
				if (int.Parse(dataRow["PropertyType"].ToString()) == 1)
				{
					radioButtonCommercial.Checked = true;
				}
				else if (int.Parse(dataRow["PropertyType"].ToString()) == 2)
				{
					radioButtonResidential.Checked = true;
				}
			}
			if (dataRow["ViewTypeID"] != DBNull.Value)
			{
				ComboBoxViewType.SelectedID = dataRow["ViewTypeID"].ToString();
			}
			if (dataRow["KitchenTypeID"] != DBNull.Value)
			{
				comboBoxKitchenType.SelectedID = dataRow["KitchenTypeID"].ToString();
			}
			else
			{
				comboBoxKitchenType.Clear();
			}
			checkBoxIsInactive.Checked = bool.Parse(dataRow["Status"].ToString());
			textBoxNote.Text = dataRow["Note"].ToString();
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
			textBoxElectricityPremiseNo.Text = dataRow["ElectricityPremiseNumber"].ToString();
			textBoxElectricityFileNo.Text = dataRow["ElectricityFileNumber"].ToString();
			textBoxElectricityPermitNumber.Text = dataRow["ElectricityPermitNumber"].ToString();
			textBoxMunicipalityFileNo.Text = dataRow["MunicipalityFileNumber"].ToString();
			textBoxMunicipalityPermitNo.Text = dataRow["MunicipalityPermitNumber"].ToString();
			if (dataRow["RentalIncome"] != DBNull.Value)
			{
				amountTextBox.Text = dataRow["RentalIncome"].ToString();
			}
			else
			{
				amountTextBox.Text = "0.00";
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
					flag = Factory.PropertyUnitSystem.CreatePropertyUnit(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Area, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.PropertyUnitSystem.UpdatePropertyUnit(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Property_Unit", "PropertyUnitID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (ComboBoxProperty.Text == "")
			{
				ErrorHelper.InformationMessage("Please select  Property.");
				return false;
			}
			if (comboBoxStatus.SelectedIndex == -1)
			{
				ErrorHelper.InformationMessage("Please select  status.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Property_Unit", "PropertyUnitID", textBoxCode.Text.Trim()))
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

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Property_Unit", "PropertyUnitID");
			dateTimePickerFromDate.Value = DateTime.Now;
			dateTimePickerToDate.Value = DateTime.Now;
			textBoxName.Clear();
			textBoxNote.Clear();
			amountTextBox.Clear();
			comboBoxStatus.SelectedIndex = 0;
			textBoxShortName.Clear();
			dateTimePickerFromDate.Value = DateTime.Now;
			dateTimePickerToDate.Value = DateTime.Now;
			ComboBoxProperty.Clear();
			comboBoxPropertyParentUnit.Clear();
			ComboBoxUnitType.Clear();
			textBoxNoofBedrooms.Clear();
			textBoxNoofBathRooms.Clear();
			textBoxTotalrooms.Clear();
			textBoxArea.Clear();
			textBoxNoofparking.Clear();
			ComboBoxViewType.Clear();
			checkBoxIsInactive.Checked = false;
			textBoxNote.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			udfEntryGrid.ClearData();
			radioButtonCommercial.Checked = false;
			radioButtonResidential.Checked = false;
			comboBoxKitchenType.Clear();
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			if (CompanyPreferences.TaxEntityTypes.Contains("U"))
			{
				comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
				comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
			}
			textBoxElectricityFileNo.Clear();
			textBoxElectricityPermitNumber.Clear();
			textBoxElectricityPremiseNo.Clear();
			textBoxMunicipalityFileNo.Clear();
			textBoxMunicipalityPermitNo.Clear();
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				bool num = Factory.PropertyUnitSystem.DeletePropertyUnit(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Area, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Property_Unit", "PropertyUnitID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Property_Unit", "PropertyUnitID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Property_Unit", "PropertyUnitID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Property_Unit", "PropertyUnitID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Property_Unit", "PropertyUnitID", toolStripTextBoxFind.Text.Trim()))
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

		private void PropertyUnitDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					if (CompanyPreferences.TaxEntityTypes.Contains("U"))
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
			new FormHelper().ShowList(DataComboType.PropertyUnit);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkLabelCustomerClass_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProperty(ComboBoxProperty.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.PropertyUnitType, ComboBoxUnitType.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.PropertyView, ComboBoxViewType.SelectedID);
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
			PropertyCategoryAssignDialog propertyCategoryAssignDialog = new PropertyCategoryAssignDialog();
			propertyCategoryAssignDialog.PropertyID = textBoxCode.Text;
			propertyCategoryAssignDialog.PropertyName = textBoxName.Text;
			propertyCategoryAssignDialog.EntityType = EntityTypesEnum.Properties;
			if (!screenRight.Edit)
			{
				propertyCategoryAssignDialog.AllowEdit = false;
			}
			propertyCategoryAssignDialog.ShowDialog(this);
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
					docManagementForm.EntityType = EntityTypesEnum.PropertyUnits;
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
					docManagementForm.EntityType = EntityTypesEnum.PropertyUnits;
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

		private void ComboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(ComboBoxProperty.SelectedID))
			{
				return;
			}
			DataSet propertyByID = Factory.PropertySystem.GetPropertyByID(ComboBoxProperty.SelectedID);
			if (propertyByID != null && propertyByID.Tables.Count != 0 && propertyByID.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = propertyByID.Tables[0].Rows[0];
				if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
				{
					comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
				}
				if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
				{
					comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
				}
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

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetPropertyUnitThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddPropertyUnitPhoto(textBoxCode.Text, image, isMatrixParent: false))
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
					if (Factory.PropertyUnitSystem.RemovePropertyUnitPhoto(textBoxCode.Text))
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
			if (!string.IsNullOrEmpty(ComboBoxProperty.SelectedID))
			{
				FormActivator.BringFormToFront(FormActivator.PropertyDocumentsFormObj);
				FormActivator.PropertyDocumentsFormObj.LoadData(ComboBoxProperty.SelectedID);
			}
		}

		private void currentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void currentDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			PropertyUnitCurrentDetailsForm propertyUnitCurrentDetailsForm = new PropertyUnitCurrentDetailsForm();
			propertyUnitCurrentDetailsForm.PropertyUnitID = textBoxCode.Text;
			propertyUnitCurrentDetailsForm.ShowDialog();
		}

		private void ultraFormattedLinkLabelKitchenType_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.KitchenType, comboBoxKitchenType.SelectedID);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyUnitDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxPropertyParentUnit = new Micromind.DataControls.PropertyUnitComboBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			radioButtonResidential = new System.Windows.Forms.RadioButton();
			radioButtonCommercial = new System.Windows.Forms.RadioButton();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			ComboBoxUnitType = new Micromind.DataControls.GenericListComboBox();
			ComboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			buttonCategories = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			dateTimePickerToDate = new Micromind.UISupport.flatDatePicker();
			dateTimePickerFromDate = new Micromind.UISupport.flatDatePicker();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxKitchenType = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabelKitchenType = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			amountTextBox = new Micromind.UISupport.AmountTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxNoofparking = new Micromind.UISupport.NumberTextBox();
			textBoxTotalrooms = new Micromind.UISupport.NumberTextBox();
			textBoxNoofBathRooms = new Micromind.UISupport.NumberTextBox();
			textBoxNoofBedrooms = new Micromind.UISupport.NumberTextBox();
			textBoxArea = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ComboBoxViewType = new Micromind.DataControls.GenericListComboBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			linkLabelCustomerClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelCustomerNumber = new Micromind.UISupport.MMLabel();
			textBoxShortName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxElectricityPermitNumber = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxElectricityFileNo = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxMunicipalityPermitNo = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxMunicipalityFileNo = new Micromind.UISupport.MMTextBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxElectricityPremiseNo = new Micromind.UISupport.MMTextBox();
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			contextMenuStripCurrentDetails = new System.Windows.Forms.ContextMenuStrip(components);
			currentDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyParentUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxUnitType).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxKitchenType).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxViewType).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			toolStrip1.SuspendLayout();
			contextMenuStripCurrentDetails.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(comboBoxPropertyParentUnit);
			tabPageGeneral.Controls.Add(mmLabel18);
			tabPageGeneral.Controls.Add(mmLabel14);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Controls.Add(mmLabel2);
			tabPageGeneral.Controls.Add(radioButtonResidential);
			tabPageGeneral.Controls.Add(radioButtonCommercial);
			tabPageGeneral.Controls.Add(ultraGroupBox7);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(ComboBoxUnitType);
			tabPageGeneral.Controls.Add(ComboBoxProperty);
			tabPageGeneral.Controls.Add(buttonCategories);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(mmLabel7);
			tabPageGeneral.Controls.Add(dateTimePickerToDate);
			tabPageGeneral.Controls.Add(dateTimePickerFromDate);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(comboBoxStatus);
			tabPageGeneral.Controls.Add(mmLabel4);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(linkLabelCustomerClass);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(labelCustomerNumber);
			tabPageGeneral.Controls.Add(textBoxShortName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(754, 444);
			comboBoxPropertyParentUnit.Assigned = false;
			comboBoxPropertyParentUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyParentUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyParentUnit.CustomReportFieldName = "";
			comboBoxPropertyParentUnit.CustomReportKey = "";
			comboBoxPropertyParentUnit.CustomReportValueType = 1;
			comboBoxPropertyParentUnit.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyParentUnit.DisplayLayout.Appearance = appearance;
			comboBoxPropertyParentUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyParentUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyParentUnit.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyParentUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxPropertyParentUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyParentUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxPropertyParentUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyParentUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyParentUnit.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyParentUnit.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxPropertyParentUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyParentUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyParentUnit.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyParentUnit.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxPropertyParentUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyParentUnit.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyParentUnit.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxPropertyParentUnit.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxPropertyParentUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyParentUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyParentUnit.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxPropertyParentUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyParentUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxPropertyParentUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyParentUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyParentUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyParentUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyParentUnit.Editable = true;
			comboBoxPropertyParentUnit.FilterString = "";
			comboBoxPropertyParentUnit.HasAllAccount = false;
			comboBoxPropertyParentUnit.HasCustom = false;
			comboBoxPropertyParentUnit.IsDataLoaded = false;
			comboBoxPropertyParentUnit.Location = new System.Drawing.Point(132, 148);
			comboBoxPropertyParentUnit.MaxDropDownItems = 12;
			comboBoxPropertyParentUnit.Name = "comboBoxPropertyParentUnit";
			comboBoxPropertyParentUnit.ShowActiveOnly = false;
			comboBoxPropertyParentUnit.ShowInactiveItems = false;
			comboBoxPropertyParentUnit.ShowQuickAdd = true;
			comboBoxPropertyParentUnit.Size = new System.Drawing.Size(204, 20);
			comboBoxPropertyParentUnit.TabIndex = 7;
			comboBoxPropertyParentUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(10, 152);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(65, 13);
			mmLabel18.TabIndex = 121;
			mmLabel18.Text = "Parent Unit:";
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(10, 177);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(35, 13);
			mmLabel14.TabIndex = 120;
			mmLabel14.Text = "From:";
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(693, 141);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 119;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(10, 203);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(80, 13);
			mmLabel2.TabIndex = 78;
			mmLabel2.Text = "Property Type:";
			radioButtonResidential.AutoSize = true;
			radioButtonResidential.Location = new System.Drawing.Point(251, 203);
			radioButtonResidential.Name = "radioButtonResidential";
			radioButtonResidential.Size = new System.Drawing.Size(77, 17);
			radioButtonResidential.TabIndex = 11;
			radioButtonResidential.TabStop = true;
			radioButtonResidential.Text = "Residential";
			radioButtonResidential.UseVisualStyleBackColor = true;
			radioButtonCommercial.AutoSize = true;
			radioButtonCommercial.Location = new System.Drawing.Point(135, 203);
			radioButtonCommercial.Name = "radioButtonCommercial";
			radioButtonCommercial.Size = new System.Drawing.Size(79, 17);
			radioButtonCommercial.TabIndex = 10;
			radioButtonCommercial.TabStop = true;
			radioButtonCommercial.Text = "Commercial";
			radioButtonCommercial.UseVisualStyleBackColor = true;
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel4);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(mmLabel3);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Location = new System.Drawing.Point(8, 342);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(696, 75);
			ultraGroupBox7.TabIndex = 13;
			ultraGroupBox7.Text = "Tax Details";
			ultraFormattedLinkLabel4.AutoSize = true;
			appearance13.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.LinkAppearance = appearance13;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(4, 46);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel4.TabIndex = 72;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Tax Group:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance14;
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance15;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
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
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(4, 22);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(64, 13);
			mmLabel3.TabIndex = 70;
			mmLabel3.Text = "Tax Option:";
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
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(614, 206);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 71;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance27;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(575, 206);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 70;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance28;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(614, 102);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 68;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance29.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance29;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(557, 13);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(185, 187);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 69;
			pictureBoxPhoto.TabStop = false;
			ComboBoxUnitType.Assigned = false;
			ComboBoxUnitType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxUnitType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxUnitType.CustomReportFieldName = "";
			ComboBoxUnitType.CustomReportKey = "";
			ComboBoxUnitType.CustomReportValueType = 1;
			ComboBoxUnitType.DescriptionTextBox = null;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxUnitType.DisplayLayout.Appearance = appearance30;
			ComboBoxUnitType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxUnitType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxUnitType.DisplayLayout.GroupByBox.Appearance = appearance31;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxUnitType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance32;
			ComboBoxUnitType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance33.BackColor2 = System.Drawing.SystemColors.Control;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxUnitType.DisplayLayout.GroupByBox.PromptAppearance = appearance33;
			ComboBoxUnitType.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxUnitType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxUnitType.DisplayLayout.Override.ActiveCellAppearance = appearance34;
			appearance35.BackColor = System.Drawing.SystemColors.Highlight;
			appearance35.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxUnitType.DisplayLayout.Override.ActiveRowAppearance = appearance35;
			ComboBoxUnitType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxUnitType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxUnitType.DisplayLayout.Override.CardAreaAppearance = appearance36;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			appearance37.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxUnitType.DisplayLayout.Override.CellAppearance = appearance37;
			ComboBoxUnitType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxUnitType.DisplayLayout.Override.CellPadding = 0;
			appearance38.BackColor = System.Drawing.SystemColors.Control;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxUnitType.DisplayLayout.Override.GroupByRowAppearance = appearance38;
			appearance39.TextHAlignAsString = "Left";
			ComboBoxUnitType.DisplayLayout.Override.HeaderAppearance = appearance39;
			ComboBoxUnitType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxUnitType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			ComboBoxUnitType.DisplayLayout.Override.RowAppearance = appearance40;
			ComboBoxUnitType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxUnitType.DisplayLayout.Override.TemplateAddRowAppearance = appearance41;
			ComboBoxUnitType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxUnitType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxUnitType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxUnitType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxUnitType.Editable = true;
			ComboBoxUnitType.FilterString = "";
			ComboBoxUnitType.GenericListType = Micromind.Common.Data.GenericListTypes.PropertyUnitType;
			ComboBoxUnitType.HasAllAccount = false;
			ComboBoxUnitType.HasCustom = false;
			ComboBoxUnitType.IsDataLoaded = false;
			ComboBoxUnitType.IsSingleColumn = false;
			ComboBoxUnitType.Location = new System.Drawing.Point(132, 125);
			ComboBoxUnitType.MaxDropDownItems = 12;
			ComboBoxUnitType.Name = "ComboBoxUnitType";
			ComboBoxUnitType.ShowInactiveItems = false;
			ComboBoxUnitType.ShowQuickAdd = true;
			ComboBoxUnitType.Size = new System.Drawing.Size(204, 20);
			ComboBoxUnitType.TabIndex = 5;
			ComboBoxUnitType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxProperty.Assigned = false;
			ComboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxProperty.CustomReportFieldName = "";
			ComboBoxProperty.CustomReportKey = "";
			ComboBoxProperty.CustomReportValueType = 1;
			ComboBoxProperty.DescriptionTextBox = null;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxProperty.DisplayLayout.Appearance = appearance42;
			ComboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance43;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance44;
			ComboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance45.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance45.BackColor2 = System.Drawing.SystemColors.Control;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance45;
			ComboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance46;
			appearance47.BackColor = System.Drawing.SystemColors.Highlight;
			appearance47.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance47;
			ComboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance48;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			appearance49.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxProperty.DisplayLayout.Override.CellAppearance = appearance49;
			ComboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance50.BackColor = System.Drawing.SystemColors.Control;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance50;
			appearance51.TextHAlignAsString = "Left";
			ComboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance51;
			ComboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			ComboBoxProperty.DisplayLayout.Override.RowAppearance = appearance52;
			ComboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance53.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance53;
			ComboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxProperty.Editable = true;
			ComboBoxProperty.FilterString = "";
			ComboBoxProperty.HasAllAccount = false;
			ComboBoxProperty.HasCustom = false;
			ComboBoxProperty.IsDataLoaded = false;
			ComboBoxProperty.Location = new System.Drawing.Point(132, 102);
			ComboBoxProperty.MaxDropDownItems = 12;
			ComboBoxProperty.Name = "ComboBoxProperty";
			ComboBoxProperty.ShowInactiveItems = false;
			ComboBoxProperty.ShowQuickAdd = true;
			ComboBoxProperty.Size = new System.Drawing.Size(204, 20);
			ComboBoxProperty.TabIndex = 4;
			ComboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxProperty.SelectedIndexChanged += new System.EventHandler(ComboBoxProperty_SelectedIndexChanged);
			buttonCategories.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCategories.BackColor = System.Drawing.Color.DarkGray;
			buttonCategories.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCategories.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCategories.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCategories.Location = new System.Drawing.Point(338, 124);
			buttonCategories.Name = "buttonCategories";
			buttonCategories.Size = new System.Drawing.Size(146, 24);
			buttonCategories.TabIndex = 6;
			buttonCategories.Text = "Categories...";
			buttonCategories.UseVisualStyleBackColor = false;
			buttonCategories.Click += new System.EventHandler(buttonCategories_Click);
			ultraFormattedLinkLabel3.AutoSize = true;
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.LinkAppearance = appearance54;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 128);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(55, 14);
			ultraFormattedLinkLabel3.TabIndex = 28;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Unit Type:";
			appearance55.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance55;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(258, 177);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(23, 13);
			mmLabel7.TabIndex = 27;
			mmLabel7.Text = "To:";
			dateTimePickerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerToDate.Location = new System.Drawing.Point(300, 173);
			dateTimePickerToDate.Name = "dateTimePickerToDate";
			dateTimePickerToDate.Size = new System.Drawing.Size(120, 20);
			dateTimePickerToDate.TabIndex = 9;
			dateTimePickerToDate.Value = new System.DateTime(2015, 11, 29, 15, 19, 41, 785);
			dateTimePickerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFromDate.Location = new System.Drawing.Point(132, 173);
			dateTimePickerFromDate.Name = "dateTimePickerFromDate";
			dateTimePickerFromDate.Size = new System.Drawing.Size(120, 20);
			dateTimePickerFromDate.TabIndex = 8;
			dateTimePickerFromDate.Value = new System.DateTime(2015, 11, 29, 15, 19, 41, 785);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(comboBoxKitchenType);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabelKitchenType);
			ultraGroupBox1.Controls.Add(mmLabel23);
			ultraGroupBox1.Controls.Add(amountTextBox);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(textBoxNoofparking);
			ultraGroupBox1.Controls.Add(textBoxTotalrooms);
			ultraGroupBox1.Controls.Add(textBoxNoofBathRooms);
			ultraGroupBox1.Controls.Add(textBoxNoofBedrooms);
			ultraGroupBox1.Controls.Add(textBoxArea);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel5);
			ultraGroupBox1.Controls.Add(ComboBoxViewType);
			ultraGroupBox1.Controls.Add(mmLabel17);
			ultraGroupBox1.Controls.Add(mmLabel6);
			ultraGroupBox1.Controls.Add(mmLabel16);
			ultraGroupBox1.Controls.Add(mmLabel15);
			ultraGroupBox1.Controls.Add(mmLabel9);
			ultraGroupBox1.Location = new System.Drawing.Point(3, 231);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(696, 97);
			ultraGroupBox1.TabIndex = 12;
			ultraGroupBox1.Text = "Details:";
			comboBoxKitchenType.Assigned = false;
			comboBoxKitchenType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxKitchenType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxKitchenType.CustomReportFieldName = "";
			comboBoxKitchenType.CustomReportKey = "";
			comboBoxKitchenType.CustomReportValueType = 1;
			comboBoxKitchenType.DescriptionTextBox = null;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxKitchenType.DisplayLayout.Appearance = appearance56;
			comboBoxKitchenType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxKitchenType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance57.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxKitchenType.DisplayLayout.GroupByBox.Appearance = appearance57;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxKitchenType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance58;
			comboBoxKitchenType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance59.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance59.BackColor2 = System.Drawing.SystemColors.Control;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxKitchenType.DisplayLayout.GroupByBox.PromptAppearance = appearance59;
			comboBoxKitchenType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxKitchenType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			appearance60.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxKitchenType.DisplayLayout.Override.ActiveCellAppearance = appearance60;
			appearance61.BackColor = System.Drawing.SystemColors.Highlight;
			appearance61.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxKitchenType.DisplayLayout.Override.ActiveRowAppearance = appearance61;
			comboBoxKitchenType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxKitchenType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			comboBoxKitchenType.DisplayLayout.Override.CardAreaAppearance = appearance62;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			appearance63.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxKitchenType.DisplayLayout.Override.CellAppearance = appearance63;
			comboBoxKitchenType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxKitchenType.DisplayLayout.Override.CellPadding = 0;
			appearance64.BackColor = System.Drawing.SystemColors.Control;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxKitchenType.DisplayLayout.Override.GroupByRowAppearance = appearance64;
			appearance65.TextHAlignAsString = "Left";
			comboBoxKitchenType.DisplayLayout.Override.HeaderAppearance = appearance65;
			comboBoxKitchenType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxKitchenType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			comboBoxKitchenType.DisplayLayout.Override.RowAppearance = appearance66;
			comboBoxKitchenType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance67.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxKitchenType.DisplayLayout.Override.TemplateAddRowAppearance = appearance67;
			comboBoxKitchenType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxKitchenType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxKitchenType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxKitchenType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxKitchenType.Editable = true;
			comboBoxKitchenType.FilterString = "";
			comboBoxKitchenType.GenericListType = Micromind.Common.Data.GenericListTypes.KitchenType;
			comboBoxKitchenType.HasAllAccount = false;
			comboBoxKitchenType.HasCustom = false;
			comboBoxKitchenType.IsDataLoaded = false;
			comboBoxKitchenType.IsSingleColumn = false;
			comboBoxKitchenType.Location = new System.Drawing.Point(417, 74);
			comboBoxKitchenType.MaxDropDownItems = 12;
			comboBoxKitchenType.Name = "comboBoxKitchenType";
			comboBoxKitchenType.ShowInactiveItems = false;
			comboBoxKitchenType.ShowQuickAdd = true;
			comboBoxKitchenType.Size = new System.Drawing.Size(166, 20);
			comboBoxKitchenType.TabIndex = 40;
			comboBoxKitchenType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabelKitchenType.AutoSize = true;
			appearance68.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelKitchenType.LinkAppearance = appearance68;
			ultraFormattedLinkLabelKitchenType.Location = new System.Drawing.Point(339, 76);
			ultraFormattedLinkLabelKitchenType.Name = "ultraFormattedLinkLabelKitchenType";
			ultraFormattedLinkLabelKitchenType.Size = new System.Drawing.Size(72, 14);
			ultraFormattedLinkLabelKitchenType.TabIndex = 39;
			ultraFormattedLinkLabelKitchenType.TabStop = true;
			ultraFormattedLinkLabelKitchenType.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelKitchenType.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelKitchenType.Value = "Kitchen Type:";
			appearance69.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelKitchenType.VisitedLinkAppearance = appearance69;
			ultraFormattedLinkLabelKitchenType.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelKitchenType_LinkClicked);
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(448, 21);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(33, 16);
			mmLabel23.TabIndex = 38;
			mmLabel23.Text = "/ M²";
			amountTextBox.AllowDecimal = true;
			amountTextBox.BackColor = System.Drawing.Color.White;
			amountTextBox.CustomReportFieldName = "";
			amountTextBox.CustomReportKey = "";
			amountTextBox.CustomReportValueType = 1;
			amountTextBox.IsComboTextBox = false;
			amountTextBox.IsModified = false;
			amountTextBox.Location = new System.Drawing.Point(563, 19);
			amountTextBox.MaxLength = 15;
			amountTextBox.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			amountTextBox.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			amountTextBox.Name = "amountTextBox";
			amountTextBox.NullText = "0";
			amountTextBox.Size = new System.Drawing.Size(74, 20);
			amountTextBox.TabIndex = 4;
			amountTextBox.Text = "0.00";
			amountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			amountTextBox.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(481, 22);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(80, 13);
			mmLabel8.TabIndex = 6;
			mmLabel8.Text = "Rental Income:";
			textBoxNoofparking.AllowDecimal = true;
			textBoxNoofparking.CustomReportFieldName = "";
			textBoxNoofparking.CustomReportKey = "";
			textBoxNoofparking.CustomReportValueType = 1;
			textBoxNoofparking.IsComboTextBox = false;
			textBoxNoofparking.IsModified = false;
			textBoxNoofparking.Location = new System.Drawing.Point(274, 41);
			textBoxNoofparking.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNoofparking.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNoofparking.Name = "textBoxNoofparking";
			textBoxNoofparking.NullText = "0";
			textBoxNoofparking.Size = new System.Drawing.Size(49, 20);
			textBoxNoofparking.TabIndex = 6;
			textBoxNoofparking.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalrooms.AllowDecimal = true;
			textBoxTotalrooms.CustomReportFieldName = "";
			textBoxTotalrooms.CustomReportKey = "";
			textBoxTotalrooms.CustomReportValueType = 1;
			textBoxTotalrooms.IsComboTextBox = false;
			textBoxTotalrooms.IsModified = false;
			textBoxTotalrooms.Location = new System.Drawing.Point(274, 18);
			textBoxTotalrooms.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalrooms.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalrooms.Name = "textBoxTotalrooms";
			textBoxTotalrooms.NullText = "0";
			textBoxTotalrooms.Size = new System.Drawing.Size(49, 20);
			textBoxTotalrooms.TabIndex = 2;
			textBoxTotalrooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNoofBathRooms.AllowDecimal = true;
			textBoxNoofBathRooms.CustomReportFieldName = "";
			textBoxNoofBathRooms.CustomReportKey = "";
			textBoxNoofBathRooms.CustomReportValueType = 1;
			textBoxNoofBathRooms.IsComboTextBox = false;
			textBoxNoofBathRooms.IsModified = false;
			textBoxNoofBathRooms.Location = new System.Drawing.Point(120, 42);
			textBoxNoofBathRooms.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNoofBathRooms.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNoofBathRooms.Name = "textBoxNoofBathRooms";
			textBoxNoofBathRooms.NullText = "0";
			textBoxNoofBathRooms.Size = new System.Drawing.Size(55, 20);
			textBoxNoofBathRooms.TabIndex = 5;
			textBoxNoofBathRooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNoofBedrooms.AllowDecimal = true;
			textBoxNoofBedrooms.CustomReportFieldName = "";
			textBoxNoofBedrooms.CustomReportKey = "";
			textBoxNoofBedrooms.CustomReportValueType = 1;
			textBoxNoofBedrooms.IsComboTextBox = false;
			textBoxNoofBedrooms.IsModified = false;
			textBoxNoofBedrooms.Location = new System.Drawing.Point(120, 16);
			textBoxNoofBedrooms.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNoofBedrooms.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNoofBedrooms.Name = "textBoxNoofBedrooms";
			textBoxNoofBedrooms.NullText = "0";
			textBoxNoofBedrooms.Size = new System.Drawing.Size(55, 20);
			textBoxNoofBedrooms.TabIndex = 1;
			textBoxNoofBedrooms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxArea.AllowDecimal = true;
			textBoxArea.BackColor = System.Drawing.Color.White;
			textBoxArea.CustomReportFieldName = "";
			textBoxArea.CustomReportKey = "";
			textBoxArea.CustomReportValueType = 1;
			textBoxArea.IsComboTextBox = false;
			textBoxArea.IsModified = false;
			textBoxArea.Location = new System.Drawing.Point(373, 19);
			textBoxArea.MaxLength = 15;
			textBoxArea.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxArea.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxArea.Name = "textBoxArea";
			textBoxArea.NullText = "0";
			textBoxArea.Size = new System.Drawing.Size(74, 20);
			textBoxArea.TabIndex = 3;
			textBoxArea.Text = "0.00";
			textBoxArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxArea.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			ultraFormattedLinkLabel5.AutoSize = true;
			appearance70.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.LinkAppearance = appearance70;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(7, 75);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel5.TabIndex = 30;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "View Type:";
			appearance71.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance71;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			ComboBoxViewType.Assigned = false;
			ComboBoxViewType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxViewType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxViewType.CustomReportFieldName = "";
			ComboBoxViewType.CustomReportKey = "";
			ComboBoxViewType.CustomReportValueType = 1;
			ComboBoxViewType.DescriptionTextBox = null;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			appearance72.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxViewType.DisplayLayout.Appearance = appearance72;
			ComboBoxViewType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxViewType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance73.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxViewType.DisplayLayout.GroupByBox.Appearance = appearance73;
			appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxViewType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance74;
			ComboBoxViewType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance75.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance75.BackColor2 = System.Drawing.SystemColors.Control;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxViewType.DisplayLayout.GroupByBox.PromptAppearance = appearance75;
			ComboBoxViewType.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxViewType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxViewType.DisplayLayout.Override.ActiveCellAppearance = appearance76;
			appearance77.BackColor = System.Drawing.SystemColors.Highlight;
			appearance77.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxViewType.DisplayLayout.Override.ActiveRowAppearance = appearance77;
			ComboBoxViewType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxViewType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxViewType.DisplayLayout.Override.CardAreaAppearance = appearance78;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			appearance79.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxViewType.DisplayLayout.Override.CellAppearance = appearance79;
			ComboBoxViewType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxViewType.DisplayLayout.Override.CellPadding = 0;
			appearance80.BackColor = System.Drawing.SystemColors.Control;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxViewType.DisplayLayout.Override.GroupByRowAppearance = appearance80;
			appearance81.TextHAlignAsString = "Left";
			ComboBoxViewType.DisplayLayout.Override.HeaderAppearance = appearance81;
			ComboBoxViewType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxViewType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			ComboBoxViewType.DisplayLayout.Override.RowAppearance = appearance82;
			ComboBoxViewType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance83.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxViewType.DisplayLayout.Override.TemplateAddRowAppearance = appearance83;
			ComboBoxViewType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxViewType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxViewType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxViewType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxViewType.Editable = true;
			ComboBoxViewType.FilterString = "";
			ComboBoxViewType.GenericListType = Micromind.Common.Data.GenericListTypes.PropertyView;
			ComboBoxViewType.HasAllAccount = false;
			ComboBoxViewType.HasCustom = false;
			ComboBoxViewType.IsDataLoaded = false;
			ComboBoxViewType.IsSingleColumn = false;
			ComboBoxViewType.Location = new System.Drawing.Point(119, 73);
			ComboBoxViewType.MaxDropDownItems = 12;
			ComboBoxViewType.Name = "ComboBoxViewType";
			ComboBoxViewType.ShowInactiveItems = false;
			ComboBoxViewType.ShowQuickAdd = true;
			ComboBoxViewType.Size = new System.Drawing.Size(204, 20);
			ComboBoxViewType.TabIndex = 7;
			ComboBoxViewType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(186, 45);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(75, 13);
			mmLabel17.TabIndex = 8;
			mmLabel17.Text = "No of Parking:";
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 20);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(87, 13);
			mmLabel6.TabIndex = 0;
			mmLabel6.Text = "No of Bedrooms:";
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(333, 22);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(34, 13);
			mmLabel16.TabIndex = 6;
			mmLabel16.Text = "Area:";
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(186, 19);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(67, 13);
			mmLabel15.TabIndex = 4;
			mmLabel15.Text = "Total rooms:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(9, 46);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(86, 13);
			mmLabel9.TabIndex = 4;
			mmLabel9.Text = "No of Bathroom:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[3]
			{
				"Available",
				"Rented",
				"Maintenance"
			});
			comboBoxStatus.Location = new System.Drawing.Point(132, 78);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(138, 21);
			comboBoxStatus.TabIndex = 3;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(339, 404);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(79, 13);
			mmLabel4.TabIndex = 23;
			mmLabel4.Text = "Available from:";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(9, 82);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(42, 13);
			mmLabel1.TabIndex = 23;
			mmLabel1.Text = "Status:";
			appearance84.FontData.BoldAsString = "True";
			appearance84.FontData.ItalicAsString = "False";
			appearance84.FontData.Name = "Tahoma";
			appearance84.FontData.SizeInPoints = 8.25f;
			appearance84.FontData.StrikeoutAsString = "False";
			appearance84.FontData.UnderlineAsString = "False";
			linkLabelCustomerClass.Appearance = appearance84;
			linkLabelCustomerClass.AutoSize = true;
			appearance85.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.LinkAppearance = appearance85;
			linkLabelCustomerClass.Location = new System.Drawing.Point(9, 105);
			linkLabelCustomerClass.Name = "linkLabelCustomerClass";
			linkLabelCustomerClass.Size = new System.Drawing.Size(56, 15);
			linkLabelCustomerClass.TabIndex = 16;
			linkLabelCustomerClass.TabStop = true;
			linkLabelCustomerClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCustomerClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCustomerClass.Value = "Property:";
			appearance86.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.VisitedLinkAppearance = appearance86;
			linkLabelCustomerClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCustomerClass_LinkClicked);
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 59);
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
			labelCustomerNumber.Location = new System.Drawing.Point(9, 13);
			labelCustomerNumber.Name = "labelCustomerNumber";
			labelCustomerNumber.PenWidth = 1f;
			labelCustomerNumber.ShowBorder = false;
			labelCustomerNumber.Size = new System.Drawing.Size(64, 13);
			labelCustomerNumber.TabIndex = 0;
			labelCustomerNumber.Text = "Unit Code:";
			labelCustomerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxShortName.BackColor = System.Drawing.Color.White;
			textBoxShortName.CustomReportFieldName = "";
			textBoxShortName.CustomReportKey = "";
			textBoxShortName.CustomReportValueType = 1;
			textBoxShortName.IsComboTextBox = false;
			textBoxShortName.IsModified = false;
			textBoxShortName.Location = new System.Drawing.Point(132, 55);
			textBoxShortName.MaxLength = 64;
			textBoxShortName.Name = "textBoxShortName";
			textBoxShortName.Size = new System.Drawing.Size(294, 20);
			textBoxShortName.TabIndex = 2;
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
			textBoxCode.Size = new System.Drawing.Size(294, 20);
			textBoxCode.TabIndex = 0;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(443, 12);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(65, 16);
			checkBoxIsInactive.TabIndex = 6;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(132, 32);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(294, 20);
			textBoxName.TabIndex = 1;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = true;
			label1.Location = new System.Drawing.Point(9, 36);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(68, 13);
			label1.TabIndex = 2;
			label1.Text = "Unit Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl2.Controls.Add(mmLabel13);
			ultraTabPageControl2.Controls.Add(textBoxElectricityPermitNumber);
			ultraTabPageControl2.Controls.Add(mmLabel12);
			ultraTabPageControl2.Controls.Add(textBoxElectricityFileNo);
			ultraTabPageControl2.Controls.Add(mmLabel11);
			ultraTabPageControl2.Controls.Add(textBoxMunicipalityPermitNo);
			ultraTabPageControl2.Controls.Add(mmLabel10);
			ultraTabPageControl2.Controls.Add(textBoxMunicipalityFileNo);
			ultraTabPageControl2.Controls.Add(mmLabel19);
			ultraTabPageControl2.Controls.Add(textBoxElectricityPremiseNo);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(754, 444);
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(353, 69);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(106, 13);
			mmLabel13.TabIndex = 50;
			mmLabel13.Text = "Electricity Permit No:";
			textBoxElectricityPermitNumber.BackColor = System.Drawing.Color.White;
			textBoxElectricityPermitNumber.CustomReportFieldName = "";
			textBoxElectricityPermitNumber.CustomReportKey = "";
			textBoxElectricityPermitNumber.CustomReportValueType = 1;
			textBoxElectricityPermitNumber.IsComboTextBox = false;
			textBoxElectricityPermitNumber.IsModified = false;
			textBoxElectricityPermitNumber.Location = new System.Drawing.Point(495, 65);
			textBoxElectricityPermitNumber.MaxLength = 30;
			textBoxElectricityPermitNumber.Name = "textBoxElectricityPermitNumber";
			textBoxElectricityPermitNumber.Size = new System.Drawing.Size(191, 20);
			textBoxElectricityPermitNumber.TabIndex = 4;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(11, 70);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(92, 13);
			mmLabel12.TabIndex = 48;
			mmLabel12.Text = "Electricity File No:";
			textBoxElectricityFileNo.BackColor = System.Drawing.Color.White;
			textBoxElectricityFileNo.CustomReportFieldName = "";
			textBoxElectricityFileNo.CustomReportKey = "";
			textBoxElectricityFileNo.CustomReportValueType = 1;
			textBoxElectricityFileNo.IsComboTextBox = false;
			textBoxElectricityFileNo.IsModified = false;
			textBoxElectricityFileNo.Location = new System.Drawing.Point(153, 66);
			textBoxElectricityFileNo.MaxLength = 30;
			textBoxElectricityFileNo.Name = "textBoxElectricityFileNo";
			textBoxElectricityFileNo.Size = new System.Drawing.Size(191, 20);
			textBoxElectricityFileNo.TabIndex = 3;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(353, 45);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(115, 13);
			mmLabel11.TabIndex = 46;
			mmLabel11.Text = "Municipality Permit No:";
			textBoxMunicipalityPermitNo.BackColor = System.Drawing.Color.White;
			textBoxMunicipalityPermitNo.CustomReportFieldName = "";
			textBoxMunicipalityPermitNo.CustomReportKey = "";
			textBoxMunicipalityPermitNo.CustomReportValueType = 1;
			textBoxMunicipalityPermitNo.IsComboTextBox = false;
			textBoxMunicipalityPermitNo.IsModified = false;
			textBoxMunicipalityPermitNo.Location = new System.Drawing.Point(495, 41);
			textBoxMunicipalityPermitNo.MaxLength = 30;
			textBoxMunicipalityPermitNo.Name = "textBoxMunicipalityPermitNo";
			textBoxMunicipalityPermitNo.Size = new System.Drawing.Size(191, 20);
			textBoxMunicipalityPermitNo.TabIndex = 2;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(11, 46);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(101, 13);
			mmLabel10.TabIndex = 44;
			mmLabel10.Text = "Municipality File No:";
			textBoxMunicipalityFileNo.BackColor = System.Drawing.Color.White;
			textBoxMunicipalityFileNo.CustomReportFieldName = "";
			textBoxMunicipalityFileNo.CustomReportKey = "";
			textBoxMunicipalityFileNo.CustomReportValueType = 1;
			textBoxMunicipalityFileNo.IsComboTextBox = false;
			textBoxMunicipalityFileNo.IsModified = false;
			textBoxMunicipalityFileNo.Location = new System.Drawing.Point(153, 42);
			textBoxMunicipalityFileNo.MaxLength = 30;
			textBoxMunicipalityFileNo.Name = "textBoxMunicipalityFileNo";
			textBoxMunicipalityFileNo.Size = new System.Drawing.Size(191, 20);
			textBoxMunicipalityFileNo.TabIndex = 1;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(11, 22);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(113, 13);
			mmLabel19.TabIndex = 42;
			mmLabel19.Text = "Electricity Premise No:";
			textBoxElectricityPremiseNo.BackColor = System.Drawing.Color.White;
			textBoxElectricityPremiseNo.CustomReportFieldName = "";
			textBoxElectricityPremiseNo.CustomReportKey = "";
			textBoxElectricityPremiseNo.CustomReportValueType = 1;
			textBoxElectricityPremiseNo.IsComboTextBox = false;
			textBoxElectricityPremiseNo.IsModified = false;
			textBoxElectricityPremiseNo.Location = new System.Drawing.Point(153, 18);
			textBoxElectricityPremiseNo.MaxLength = 30;
			textBoxElectricityPremiseNo.Name = "textBoxElectricityPremiseNo";
			textBoxElectricityPremiseNo.Size = new System.Drawing.Size(191, 20);
			textBoxElectricityPremiseNo.TabIndex = 0;
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(754, 444);
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
			textBoxNote.Size = new System.Drawing.Size(739, 501);
			textBoxNote.TabIndex = 43;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(754, 444);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(3, 3);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(748, 521);
			udfEntryGrid.TabIndex = 0;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 496);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(778, 40);
			panelButtons.TabIndex = 11;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(778, 1);
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
			xpButton1.Location = new System.Drawing.Point(668, 8);
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
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(20, 29);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(758, 467);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance87.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance87;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Details";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "&Note";
			ultraTab4.TabPage = tabPageUserDefined;
			ultraTab4.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(754, 444);
			panel1.Controls.Add(toolStrip1);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(20, 0);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(758, 29);
			panel1.TabIndex = 316;
			panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
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
				toolStripSeparator4,
				toolStripButton1,
				toolStripButtonAttach,
				toolStripButtonComments,
				toolStripSeparator2,
				toolStripButtonInformation,
				toolStripButtonShowPicture
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(758, 31);
			toolStrip1.TabIndex = 308;
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
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
			contextMenuStripCurrentDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				currentDetailsToolStripMenuItem
			});
			contextMenuStripCurrentDetails.Name = "contextMenuStripCurrentDetails";
			contextMenuStripCurrentDetails.Size = new System.Drawing.Size(153, 26);
			currentDetailsToolStripMenuItem.Name = "currentDetailsToolStripMenuItem";
			currentDetailsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			currentDetailsToolStripMenuItem.Text = "Current Details";
			currentDetailsToolStripMenuItem.Click += new System.EventHandler(currentDetailsToolStripMenuItem_Click_1);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(778, 536);
			ContextMenuStrip = contextMenuStripCurrentDetails;
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PropertyUnitDetailsForm";
			Text = "Property Unit";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyParentUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxUnitType).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxKitchenType).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxViewType).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			contextMenuStripCurrentDetails.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
