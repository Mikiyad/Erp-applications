using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class VisaDetailsForm : Form, IForm
	{
		private VisaData currentData;

		private const string TABLENAME_CONST = "Visa";

		private const string IDFIELD_CONST = "VisaID";

		private bool isNewRecord = true;

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

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private MMLabel mmLabel7;

		private MMLabel mmLabel5;

		private MMLabel mmLabel1;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel4;

		private MMTextBox textBoxApplicantName;

		private MMTextBox textBoxName;

		private MMTextBox textBoxCode;

		private MMLabel labelCode;

		private SponsorComboBox comboBoxSponsor;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraGroupBox ultraGroupBox1;

		private MMSDateTimePicker dateTimePickerPassportExpiryDate;

		private MMLabel mmLabel6;

		private MMLabel mmLabel3;

		private MMTextBox textBoxPassportIssuePlace;

		private UltraGroupBox ultraGroupBox2;

		private MMSDateTimePicker dateTimePickerValidityDate;

		private MMLabel mmLabel8;

		private MMLabel mmLabel10;

		private MMTextBox textBoxIssuePlace;

		private MMLabel mmLabel12;

		private MMTextBox textBoxPassportNumber;

		private MMSDateTimePicker dateTimePickerArrivalDate;

		private MMLabel mmLabel14;

		private MMSDateTimePicker dateTimePickerIssueDate;

		private MMLabel mmLabel13;

		private MMSDateTimePicker dateTimePickerDepartureDate;

		private MMLabel mmLabel9;

		private MMSDateTimePicker dateTimePickerExpiryDate;

		private MMLabel mmLabel2;

		private VisaTypeComboBox comboBoxType;

		private NumberTextBox textBoxDays;

		private MMLabel mmLabel15;

		private MMLabel mmLabel11;

		private VisaStatusComboBox comboBoxStatus;

		private UltraGroupBox ultraGroupBox3;

		private MMSDateTimePicker dateTimePickerDateofBirth;

		private MMLabel mmLabel17;

		private MMLabel mmLabel16;

		private GenderComboBox comboBoxGender;

		private NationalityComboBox comboBoxNationality;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private ContactsComboBox comboBoxContact;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5047;

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

		public VisaDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += VisaDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new VisaData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.VisaTable.Rows[0] : currentData.VisaTable.NewRow();
				dataRow.BeginEdit();
				dataRow["VisaID"] = textBoxCode.Text.Trim();
				dataRow["Description"] = textBoxName.Text;
				dataRow["Status"] = comboBoxStatus.SelectedID;
				if (comboBoxSponsor.SelectedID != "")
				{
					dataRow["SponsorID"] = comboBoxSponsor.SelectedID;
				}
				else
				{
					dataRow["SponsorID"] = DBNull.Value;
				}
				dataRow["VisaType"] = comboBoxType.SelectedID;
				dataRow["Days"] = textBoxDays.Text;
				dataRow["IssuePlace"] = textBoxIssuePlace.Text;
				if (dateTimePickerIssueDate.Checked)
				{
					dataRow["IssueDate"] = dateTimePickerIssueDate.Value;
				}
				else
				{
					dataRow["IssueDate"] = DBNull.Value;
				}
				if (dateTimePickerValidityDate.Checked)
				{
					dataRow["ValidityDate"] = dateTimePickerValidityDate.Value;
				}
				else
				{
					dataRow["ValidityDate"] = DBNull.Value;
				}
				if (dateTimePickerArrivalDate.Checked)
				{
					dataRow["ArrivalDate"] = dateTimePickerArrivalDate.Value;
				}
				else
				{
					dataRow["ArrivalDate"] = DBNull.Value;
				}
				if (dateTimePickerExpiryDate.Checked)
				{
					dataRow["ExpiryDate"] = dateTimePickerExpiryDate.Value;
				}
				else
				{
					dataRow["ExpiryDate"] = DBNull.Value;
				}
				if (dateTimePickerDepartureDate.Checked)
				{
					dataRow["DepartureDate"] = dateTimePickerDepartureDate.Value;
				}
				else
				{
					dataRow["DepartureDate"] = DBNull.Value;
				}
				dataRow["ApplicantName"] = textBoxApplicantName.Text;
				if (comboBoxContact.SelectedID != "")
				{
					dataRow["ContactID"] = comboBoxContact.SelectedID;
				}
				else
				{
					dataRow["ContactID"] = DBNull.Value;
				}
				if (comboBoxNationality.SelectedID != "")
				{
					dataRow["Nationality"] = comboBoxNationality.SelectedID;
				}
				else
				{
					dataRow["Nationality"] = DBNull.Value;
				}
				dataRow["Gender"] = comboBoxGender.SelectedGender;
				if (dateTimePickerDateofBirth.Checked)
				{
					dataRow["BirthDate"] = dateTimePickerDateofBirth.Value;
				}
				else
				{
					dataRow["BirthDate"] = DBNull.Value;
				}
				dataRow["PassportNumber"] = textBoxPassportNumber.Text;
				dataRow["PassportIssuePlace"] = textBoxPassportIssuePlace.Text;
				if (dateTimePickerPassportExpiryDate.Checked)
				{
					dataRow["PassportExpiryDate"] = dateTimePickerPassportExpiryDate.Value;
				}
				else
				{
					dataRow["PassportExpiryDate"] = DBNull.Value;
				}
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.VisaTable.Rows.Add(dataRow);
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
					currentData = Factory.VisaSystem.GetVisaByID(id.Trim());
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
				textBoxCode.Text = dataRow["VisaID"].ToString();
				textBoxName.Text = dataRow["Description"].ToString();
				comboBoxStatus.SelectedID = byte.Parse(dataRow["Status"].ToString());
				comboBoxSponsor.Text = dataRow["SponsorID"].ToString();
				comboBoxType.SelectedID = byte.Parse(dataRow["VisaType"].ToString());
				textBoxDays.Text = dataRow["Days"].ToString();
				textBoxIssuePlace.Text = dataRow["IssuePlace"].ToString();
				if (dataRow["IssueDate"] != DBNull.Value)
				{
					dateTimePickerIssueDate.Value = DateTime.Parse(dataRow["IssueDate"].ToString());
					dateTimePickerIssueDate.Checked = true;
				}
				else
				{
					dateTimePickerIssueDate.Checked = false;
				}
				if (dataRow["ValidityDate"] != DBNull.Value)
				{
					dateTimePickerValidityDate.Value = DateTime.Parse(dataRow["ValidityDate"].ToString());
					dateTimePickerValidityDate.Checked = true;
				}
				else
				{
					dateTimePickerValidityDate.Checked = false;
				}
				if (dataRow["ArrivalDate"] != DBNull.Value)
				{
					dateTimePickerArrivalDate.Value = DateTime.Parse(dataRow["ArrivalDate"].ToString());
					dateTimePickerArrivalDate.Checked = true;
				}
				else
				{
					dateTimePickerArrivalDate.Checked = false;
				}
				if (dataRow["ExpiryDate"] != DBNull.Value)
				{
					dateTimePickerExpiryDate.Value = DateTime.Parse(dataRow["ExpiryDate"].ToString());
					dateTimePickerExpiryDate.Checked = true;
				}
				else
				{
					dateTimePickerExpiryDate.Checked = false;
				}
				if (dataRow["DepartureDate"] != DBNull.Value)
				{
					dateTimePickerDepartureDate.Value = DateTime.Parse(dataRow["DepartureDate"].ToString());
					dateTimePickerDepartureDate.Checked = true;
				}
				else
				{
					dateTimePickerDepartureDate.Checked = false;
				}
				textBoxApplicantName.Text = dataRow["ApplicantName"].ToString();
				comboBoxContact.SelectedID = dataRow["ContactID"].ToString();
				comboBoxNationality.SelectedID = dataRow["Nationality"].ToString();
				comboBoxGender.SelectedGender = char.Parse(dataRow["Gender"].ToString());
				if (dataRow["BirthDate"] != DBNull.Value)
				{
					dateTimePickerDateofBirth.Value = DateTime.Parse(dataRow["BirthDate"].ToString());
					dateTimePickerDateofBirth.Checked = true;
				}
				else
				{
					dateTimePickerDateofBirth.Checked = false;
				}
				textBoxPassportNumber.Text = dataRow["PassportNumber"].ToString();
				textBoxPassportIssuePlace.Text = dataRow["PassportIssuePlace"].ToString();
				if (dataRow["PassportExpiryDate"] != DBNull.Value)
				{
					dateTimePickerPassportExpiryDate.Value = DateTime.Parse(dataRow["PassportExpiryDate"].ToString());
					dateTimePickerPassportExpiryDate.Checked = true;
				}
				else
				{
					dateTimePickerPassportExpiryDate.Checked = false;
				}
				textBoxNote.Text = dataRow["Note"].ToString();
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
				bool flag = (!isNewRecord) ? Factory.VisaSystem.UpdateVisa(currentData) : Factory.VisaSystem.CreateVisa(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Visa", "VisaID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Visa", "VisaID");
			textBoxName.Clear();
			comboBoxStatus.SelectedID = 1;
			comboBoxSponsor.Clear();
			comboBoxType.SelectedID = 1;
			textBoxDays.Clear();
			textBoxIssuePlace.Clear();
			dateTimePickerIssueDate.Checked = false;
			dateTimePickerValidityDate.Checked = false;
			dateTimePickerArrivalDate.Checked = false;
			dateTimePickerExpiryDate.Checked = false;
			dateTimePickerDepartureDate.Checked = false;
			textBoxApplicantName.Clear();
			comboBoxContact.Clear();
			comboBoxNationality.Clear();
			comboBoxGender.Clear();
			dateTimePickerDateofBirth.Checked = false;
			textBoxPassportNumber.Clear();
			textBoxPassportIssuePlace.Clear();
			dateTimePickerPassportExpiryDate.Checked = false;
			textBoxNote.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void VisaGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void VisaGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.VisaSystem.DeleteVisa(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Visa", "VisaID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Visa", "VisaID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Visa", "VisaID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Visa", "VisaID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Visa", "VisaID", toolStripTextBoxFind.Text.Trim()))
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

		private void VisaDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxGender.LoadData();
				comboBoxStatus.LoadData();
				comboBoxType.LoadData();
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

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSponsor(comboBoxSponsor.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditContact(comboBoxContact.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditNationality(comboBoxNationality.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Visa);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.VisaDetailsForm));
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxApplicantName = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxPassportNumber = new Micromind.UISupport.MMTextBox();
			dateTimePickerPassportExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxPassportIssuePlace = new Micromind.UISupport.MMTextBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			comboBoxStatus = new Micromind.DataControls.VisaStatusComboBox();
			comboBoxType = new Micromind.DataControls.VisaTypeComboBox();
			dateTimePickerDepartureDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel9 = new Micromind.UISupport.MMLabel();
			dateTimePickerExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerArrivalDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel14 = new Micromind.UISupport.MMLabel();
			dateTimePickerIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel13 = new Micromind.UISupport.MMLabel();
			dateTimePickerValidityDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxIssuePlace = new Micromind.UISupport.MMTextBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxContact = new Micromind.DataControls.ContactsComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimePickerDateofBirth = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			comboBoxGender = new Micromind.DataControls.GenderComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxContact).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
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
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(603, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
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
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 500);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(603, 40);
			panelButtons.TabIndex = 4;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(603, 1);
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
			xpButton1.Location = new System.Drawing.Point(493, 8);
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
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(424, 28);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(40, 13);
			mmLabel7.TabIndex = 73;
			mmLabel7.Text = "Status:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(10, 22);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(85, 13);
			mmLabel5.TabIndex = 95;
			mmLabel5.Text = "Applicant Name:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 49);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(63, 13);
			mmLabel1.TabIndex = 94;
			mmLabel1.Text = "Description:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(129, 390);
			textBoxNote.MaxLength = 3000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(460, 98);
			textBoxNote.TabIndex = 3;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(14, 390);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 89;
			mmLabel4.Text = "Note:";
			textBoxApplicantName.BackColor = System.Drawing.Color.White;
			textBoxApplicantName.CustomReportFieldName = "";
			textBoxApplicantName.CustomReportKey = "";
			textBoxApplicantName.CustomReportValueType = 1;
			textBoxApplicantName.IsComboTextBox = false;
			textBoxApplicantName.Location = new System.Drawing.Point(122, 19);
			textBoxApplicantName.MaxLength = 64;
			textBoxApplicantName.Name = "textBoxApplicantName";
			textBoxApplicantName.Size = new System.Drawing.Size(243, 20);
			textBoxApplicantName.TabIndex = 0;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(122, 47);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(449, 20);
			textBoxName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(122, 25);
			textBoxCode.MaxLength = 20;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(296, 20);
			textBoxCode.TabIndex = 1;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(10, 27);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(82, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Visa Number:";
			comboBoxSponsor.Assigned = false;
			comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSponsor.CustomReportFieldName = "";
			comboBoxSponsor.CustomReportKey = "";
			comboBoxSponsor.CustomReportValueType = 1;
			comboBoxSponsor.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSponsor.DisplayLayout.Appearance = appearance;
			comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSponsor.Editable = true;
			comboBoxSponsor.FilterString = "";
			comboBoxSponsor.HasAllAccount = false;
			comboBoxSponsor.HasCustom = false;
			comboBoxSponsor.IsDataLoaded = false;
			comboBoxSponsor.Location = new System.Drawing.Point(122, 69);
			comboBoxSponsor.MaxDropDownItems = 12;
			comboBoxSponsor.Name = "comboBoxSponsor";
			comboBoxSponsor.ShowInactiveItems = false;
			comboBoxSponsor.ShowQuickAdd = true;
			comboBoxSponsor.Size = new System.Drawing.Size(127, 20);
			comboBoxSponsor.TabIndex = 4;
			comboBoxSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(10, 71);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(46, 14);
			ultraFormattedLinkLabel2.TabIndex = 98;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Sponsor:";
			appearance13.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance13;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.Controls.Add(mmLabel12);
			ultraGroupBox1.Controls.Add(textBoxPassportNumber);
			ultraGroupBox1.Controls.Add(dateTimePickerPassportExpiryDate);
			ultraGroupBox1.Controls.Add(mmLabel6);
			ultraGroupBox1.Controls.Add(mmLabel3);
			ultraGroupBox1.Controls.Add(textBoxPassportIssuePlace);
			ultraGroupBox1.Location = new System.Drawing.Point(7, 308);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(582, 76);
			ultraGroupBox1.TabIndex = 2;
			ultraGroupBox1.Text = "Applicant Passport Info";
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(7, 26);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(91, 13);
			mmLabel12.TabIndex = 97;
			mmLabel12.Text = "Passport Number:";
			textBoxPassportNumber.BackColor = System.Drawing.Color.White;
			textBoxPassportNumber.CustomReportFieldName = "";
			textBoxPassportNumber.CustomReportKey = "";
			textBoxPassportNumber.CustomReportValueType = 1;
			textBoxPassportNumber.IsComboTextBox = false;
			textBoxPassportNumber.Location = new System.Drawing.Point(122, 23);
			textBoxPassportNumber.MaxLength = 30;
			textBoxPassportNumber.Name = "textBoxPassportNumber";
			textBoxPassportNumber.Size = new System.Drawing.Size(154, 20);
			textBoxPassportNumber.TabIndex = 0;
			dateTimePickerPassportExpiryDate.Checked = false;
			dateTimePickerPassportExpiryDate.CustomFormat = " ";
			dateTimePickerPassportExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerPassportExpiryDate.Location = new System.Drawing.Point(355, 45);
			dateTimePickerPassportExpiryDate.Name = "dateTimePickerPassportExpiryDate";
			dateTimePickerPassportExpiryDate.ShowCheckBox = true;
			dateTimePickerPassportExpiryDate.Size = new System.Drawing.Size(154, 20);
			dateTimePickerPassportExpiryDate.TabIndex = 2;
			dateTimePickerPassportExpiryDate.Value = new System.DateTime(0L);
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(282, 47);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(67, 13);
			mmLabel6.TabIndex = 95;
			mmLabel6.Text = "Expiry Date:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(7, 47);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(65, 13);
			mmLabel3.TabIndex = 93;
			mmLabel3.Text = "Issue Place:";
			textBoxPassportIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxPassportIssuePlace.CustomReportFieldName = "";
			textBoxPassportIssuePlace.CustomReportKey = "";
			textBoxPassportIssuePlace.CustomReportValueType = 1;
			textBoxPassportIssuePlace.IsComboTextBox = false;
			textBoxPassportIssuePlace.Location = new System.Drawing.Point(122, 45);
			textBoxPassportIssuePlace.MaxLength = 30;
			textBoxPassportIssuePlace.Name = "textBoxPassportIssuePlace";
			textBoxPassportIssuePlace.Size = new System.Drawing.Size(154, 20);
			textBoxPassportIssuePlace.TabIndex = 1;
			ultraGroupBox2.Controls.Add(textBoxDays);
			ultraGroupBox2.Controls.Add(mmLabel15);
			ultraGroupBox2.Controls.Add(mmLabel11);
			ultraGroupBox2.Controls.Add(comboBoxStatus);
			ultraGroupBox2.Controls.Add(comboBoxType);
			ultraGroupBox2.Controls.Add(dateTimePickerDepartureDate);
			ultraGroupBox2.Controls.Add(mmLabel9);
			ultraGroupBox2.Controls.Add(mmLabel7);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox2.Controls.Add(dateTimePickerExpiryDate);
			ultraGroupBox2.Controls.Add(comboBoxSponsor);
			ultraGroupBox2.Controls.Add(mmLabel2);
			ultraGroupBox2.Controls.Add(dateTimePickerArrivalDate);
			ultraGroupBox2.Controls.Add(mmLabel14);
			ultraGroupBox2.Controls.Add(dateTimePickerIssueDate);
			ultraGroupBox2.Controls.Add(mmLabel1);
			ultraGroupBox2.Controls.Add(mmLabel13);
			ultraGroupBox2.Controls.Add(dateTimePickerValidityDate);
			ultraGroupBox2.Controls.Add(mmLabel8);
			ultraGroupBox2.Controls.Add(mmLabel10);
			ultraGroupBox2.Controls.Add(textBoxName);
			ultraGroupBox2.Controls.Add(textBoxIssuePlace);
			ultraGroupBox2.Controls.Add(textBoxCode);
			ultraGroupBox2.Controls.Add(labelCode);
			ultraGroupBox2.Location = new System.Drawing.Point(7, 34);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(582, 189);
			ultraGroupBox2.TabIndex = 0;
			ultraGroupBox2.Text = "Visa Details";
			textBoxDays.AllowDecimal = false;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.IsComboTextBox = false;
			textBoxDays.Location = new System.Drawing.Point(479, 69);
			textBoxDays.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxDays.MinValue = new decimal(new int[4]);
			textBoxDays.Name = "textBoxDays";
			textBoxDays.NullText = "0";
			textBoxDays.Size = new System.Drawing.Size(92, 20);
			textBoxDays.TabIndex = 6;
			textBoxDays.Text = "0";
			textBoxDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(424, 72);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(34, 13);
			mmLabel15.TabIndex = 107;
			mmLabel15.Text = "Days:";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(254, 72);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(34, 13);
			mmLabel11.TabIndex = 106;
			mmLabel11.Text = "Type:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(480, 24);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.SelectedID = 0;
			comboBoxStatus.Size = new System.Drawing.Size(91, 21);
			comboBoxStatus.TabIndex = 2;
			comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxType.FormattingEnabled = true;
			comboBoxType.Location = new System.Drawing.Point(305, 69);
			comboBoxType.Name = "comboBoxType";
			comboBoxType.SelectedID = 0;
			comboBoxType.Size = new System.Drawing.Size(113, 21);
			comboBoxType.TabIndex = 5;
			dateTimePickerDepartureDate.Checked = false;
			dateTimePickerDepartureDate.CustomFormat = " ";
			dateTimePickerDepartureDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerDepartureDate.Location = new System.Drawing.Point(122, 158);
			dateTimePickerDepartureDate.Name = "dateTimePickerDepartureDate";
			dateTimePickerDepartureDate.ShowCheckBox = true;
			dateTimePickerDepartureDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerDepartureDate.TabIndex = 12;
			dateTimePickerDepartureDate.Value = new System.DateTime(0L);
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(10, 160);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(86, 13);
			mmLabel9.TabIndex = 103;
			mmLabel9.Text = "Departure Date:";
			dateTimePickerExpiryDate.Checked = false;
			dateTimePickerExpiryDate.CustomFormat = " ";
			dateTimePickerExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerExpiryDate.Location = new System.Drawing.Point(331, 136);
			dateTimePickerExpiryDate.Name = "dateTimePickerExpiryDate";
			dateTimePickerExpiryDate.ShowCheckBox = true;
			dateTimePickerExpiryDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerExpiryDate.TabIndex = 11;
			dateTimePickerExpiryDate.Value = new System.DateTime(0L);
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(255, 140);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(67, 13);
			mmLabel2.TabIndex = 101;
			mmLabel2.Text = "Expiry Date:";
			dateTimePickerArrivalDate.Checked = false;
			dateTimePickerArrivalDate.CustomFormat = " ";
			dateTimePickerArrivalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerArrivalDate.Location = new System.Drawing.Point(122, 136);
			dateTimePickerArrivalDate.Name = "dateTimePickerArrivalDate";
			dateTimePickerArrivalDate.ShowCheckBox = true;
			dateTimePickerArrivalDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerArrivalDate.TabIndex = 10;
			dateTimePickerArrivalDate.Value = new System.DateTime(0L);
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(10, 138);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(68, 13);
			mmLabel14.TabIndex = 99;
			mmLabel14.Text = "Arrival Date:";
			dateTimePickerIssueDate.Checked = false;
			dateTimePickerIssueDate.CustomFormat = " ";
			dateTimePickerIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerIssueDate.Location = new System.Drawing.Point(122, 114);
			dateTimePickerIssueDate.Name = "dateTimePickerIssueDate";
			dateTimePickerIssueDate.ShowCheckBox = true;
			dateTimePickerIssueDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerIssueDate.TabIndex = 8;
			dateTimePickerIssueDate.Value = new System.DateTime(0L);
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(10, 116);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(63, 13);
			mmLabel13.TabIndex = 97;
			mmLabel13.Text = "Issue Date:";
			dateTimePickerValidityDate.Checked = false;
			dateTimePickerValidityDate.CustomFormat = " ";
			dateTimePickerValidityDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerValidityDate.Location = new System.Drawing.Point(331, 114);
			dateTimePickerValidityDate.Name = "dateTimePickerValidityDate";
			dateTimePickerValidityDate.ShowCheckBox = true;
			dateTimePickerValidityDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerValidityDate.TabIndex = 9;
			dateTimePickerValidityDate.Value = new System.DateTime(0L);
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(254, 118);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(71, 13);
			mmLabel8.TabIndex = 95;
			mmLabel8.Text = "Validity Date:";
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(10, 94);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(65, 13);
			mmLabel10.TabIndex = 93;
			mmLabel10.Text = "Issue Place:";
			textBoxIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxIssuePlace.CustomReportFieldName = "";
			textBoxIssuePlace.CustomReportKey = "";
			textBoxIssuePlace.CustomReportValueType = 1;
			textBoxIssuePlace.IsComboTextBox = false;
			textBoxIssuePlace.Location = new System.Drawing.Point(122, 92);
			textBoxIssuePlace.MaxLength = 30;
			textBoxIssuePlace.Name = "textBoxIssuePlace";
			textBoxIssuePlace.Size = new System.Drawing.Size(215, 20);
			textBoxIssuePlace.TabIndex = 7;
			ultraGroupBox3.Controls.Add(comboBoxContact);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel3);
			ultraGroupBox3.Controls.Add(comboBoxNationality);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox3.Controls.Add(dateTimePickerDateofBirth);
			ultraGroupBox3.Controls.Add(mmLabel17);
			ultraGroupBox3.Controls.Add(mmLabel16);
			ultraGroupBox3.Controls.Add(comboBoxGender);
			ultraGroupBox3.Controls.Add(textBoxApplicantName);
			ultraGroupBox3.Controls.Add(mmLabel5);
			ultraGroupBox3.Location = new System.Drawing.Point(7, 229);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(582, 73);
			ultraGroupBox3.TabIndex = 1;
			ultraGroupBox3.Text = "Applicant Info";
			ultraGroupBox3.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
			comboBoxContact.Assigned = false;
			comboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxContact.CustomReportFieldName = "";
			comboBoxContact.CustomReportKey = "";
			comboBoxContact.CustomReportValueType = 1;
			comboBoxContact.DescriptionTextBox = null;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxContact.DisplayLayout.Appearance = appearance14;
			comboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance15;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance16;
			comboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance17.BackColor2 = System.Drawing.SystemColors.Control;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance17;
			comboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance18;
			appearance19.BackColor = System.Drawing.SystemColors.Highlight;
			appearance19.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance19;
			comboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance20;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			appearance21.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxContact.DisplayLayout.Override.CellAppearance = appearance21;
			comboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance22.BackColor = System.Drawing.SystemColors.Control;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance22;
			appearance23.TextHAlignAsString = "Left";
			comboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance23;
			comboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			comboBoxContact.DisplayLayout.Override.RowAppearance = appearance24;
			comboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance25;
			comboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxContact.Editable = true;
			comboBoxContact.FilterString = "";
			comboBoxContact.HasAllAccount = false;
			comboBoxContact.HasCustom = false;
			comboBoxContact.IsDataLoaded = false;
			comboBoxContact.Location = new System.Drawing.Point(444, 19);
			comboBoxContact.MaxDropDownItems = 12;
			comboBoxContact.Name = "comboBoxContact";
			comboBoxContact.ShowInactiveItems = false;
			comboBoxContact.ShowQuickAdd = true;
			comboBoxContact.Size = new System.Drawing.Size(127, 20);
			comboBoxContact.TabIndex = 1;
			comboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(371, 22);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(44, 14);
			ultraFormattedLinkLabel3.TabIndex = 107;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Contact:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxNationality.Assigned = false;
			comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNationality.CustomReportFieldName = "";
			comboBoxNationality.CustomReportKey = "";
			comboBoxNationality.CustomReportValueType = 1;
			comboBoxNationality.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNationality.DisplayLayout.Appearance = appearance27;
			comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNationality.Editable = true;
			comboBoxNationality.FilterString = "";
			comboBoxNationality.HasAllAccount = false;
			comboBoxNationality.HasCustom = false;
			comboBoxNationality.IsDataLoaded = false;
			comboBoxNationality.Location = new System.Drawing.Point(122, 41);
			comboBoxNationality.MaxDropDownItems = 12;
			comboBoxNationality.Name = "comboBoxNationality";
			comboBoxNationality.ShowInactiveItems = false;
			comboBoxNationality.ShowQuickAdd = true;
			comboBoxNationality.Size = new System.Drawing.Size(115, 20);
			comboBoxNationality.TabIndex = 2;
			comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(10, 43);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(56, 14);
			ultraFormattedLinkLabel1.TabIndex = 106;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Nationality:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			dateTimePickerDateofBirth.Checked = false;
			dateTimePickerDateofBirth.CustomFormat = " ";
			dateTimePickerDateofBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerDateofBirth.Location = new System.Drawing.Point(444, 41);
			dateTimePickerDateofBirth.Name = "dateTimePickerDateofBirth";
			dateTimePickerDateofBirth.ShowCheckBox = true;
			dateTimePickerDateofBirth.Size = new System.Drawing.Size(127, 20);
			dateTimePickerDateofBirth.TabIndex = 4;
			dateTimePickerDateofBirth.Value = new System.DateTime(0L);
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(370, 43);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(72, 13);
			mmLabel17.TabIndex = 105;
			mmLabel17.Text = "Date of Birth:";
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(243, 43);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(45, 13);
			mmLabel16.TabIndex = 97;
			mmLabel16.Text = "Gender:";
			comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGender.FormattingEnabled = true;
			comboBoxGender.Location = new System.Drawing.Point(289, 41);
			comboBoxGender.Name = "comboBoxGender";
			comboBoxGender.SelectedID = 0;
			comboBoxGender.Size = new System.Drawing.Size(76, 21);
			comboBoxGender.TabIndex = 3;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(603, 540);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "VisaDetailsForm";
			Text = "Visa Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxContact).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
