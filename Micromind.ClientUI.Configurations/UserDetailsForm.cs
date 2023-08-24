using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries;
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

namespace Micromind.ClientUI.Configurations
{
	public class UserDetailsForm : Form, IForm
	{
		private UserData currentData;

		private const string TABLENAME_CONST = "Users";

		private const string IDFIELD_CONST = "UserID";

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

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private LocationComboBox comboBoxLocation;

		private MMTextBox textBoxPhone;

		private MMLabel mmLabel5;

		private MMTextBox textBoxEmail;

		private MMLabel mmLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private XPButton buttonGroups;

		private MMTextBox textBoxPassword;

		private MMTextBox textBoxConfirmPassword;

		private MMLabel mmLabel2;

		private MMLabel mmLabel7;

		private XPButton buttonChangePassword;

		private XPButton buttonAccessRight;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxDefaultSalesperson;

		private LocationComboBox comboBoxDefaultInvLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMTextBox textBoxDefaultInvLocation;

		private UltraGroupBox ultraGroupBox3;

		private LocationComboBox comboBoxDefaultTransLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private MMTextBox textBoxDefaultTransLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private MMTextBox textBoxLocationName;

		private CheckBox checkBoxIsAdmin;

		private SalespersonComboBox comboBoxDefaultSalesperson;

		private RegisterComboBox comboBoxDefaultTransRegister;

		private MMTextBox textBoxDefaultTransRegister;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private CheckBox checkBoxCLPass;

		private MMTextBox textBoxCLPass;

		private MMLabel labelCLPass;

		private XPButton buttonAddLocation;

		private XPButton buttonCreateHomeDashboard;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8003;

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
					buttonChangePassword.Visible = false;
					MMTextBox mMTextBox = textBoxPassword;
					bool readOnly = textBoxConfirmPassword.ReadOnly = false;
					mMTextBox.ReadOnly = readOnly;
					MMTextBox mMTextBox2 = textBoxPassword;
					readOnly = (textBoxConfirmPassword.TabStop = true);
					mMTextBox2.TabStop = readOnly;
					Color color2 = textBoxPassword.BackColor = (textBoxConfirmPassword.BackColor = Color.White);
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					buttonChangePassword.Visible = true;
					textBoxCode.ReadOnly = true;
					MMTextBox mMTextBox3 = textBoxPassword;
					bool readOnly = textBoxConfirmPassword.ReadOnly = true;
					mMTextBox3.ReadOnly = readOnly;
					MMTextBox mMTextBox4 = textBoxPassword;
					readOnly = (textBoxConfirmPassword.TabStop = false);
					mMTextBox4.TabStop = readOnly;
					Color color2 = textBoxPassword.BackColor = (textBoxConfirmPassword.BackColor = Color.WhiteSmoke);
				}
				buttonAccessRight.Enabled = !value;
				buttonGroups.Enabled = !value;
				buttonCreateHomeDashboard.Enabled = !value;
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

		public UserDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += UserDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new UserData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.UserTable.Rows[0] : currentData.UserTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UserID"] = textBoxCode.Text.Trim();
				dataRow["UserName"] = textBoxName.Text.Trim();
				dataRow["Password"] = textBoxPassword.Text.Trim();
				dataRow["IsCLUser"] = checkBoxCLPass.Checked;
				if (checkBoxCLPass.Checked && !textBoxCLPass.Text.IsNullOrEmpty())
				{
					dataRow["CLUserPass"] = Factory.Encrypt(textBoxCLPass.Text.Trim());
				}
				else
				{
					dataRow["CLUserPass"] = DBNull.Value;
				}
				if (comboBoxEmployee.SelectedID != "")
				{
					dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				}
				else
				{
					dataRow["EmployeeID"] = DBNull.Value;
				}
				if (comboBoxLocation.SelectedID != "")
				{
					dataRow["LocationID"] = comboBoxLocation.SelectedID;
				}
				else
				{
					dataRow["LocationID"] = DBNull.Value;
				}
				if (comboBoxDefaultSalesperson.SelectedID != "")
				{
					dataRow["DefaultSalesPersonID"] = comboBoxDefaultSalesperson.SelectedID;
				}
				else
				{
					dataRow["DefaultSalesPersonID"] = DBNull.Value;
				}
				if (comboBoxDefaultInvLocation.SelectedID != "")
				{
					dataRow["DefaultInventoryLocationID"] = comboBoxDefaultInvLocation.SelectedID;
				}
				else
				{
					dataRow["DefaultInventoryLocationID"] = DBNull.Value;
				}
				if (comboBoxDefaultTransLocation.SelectedID != "")
				{
					dataRow["DefaultTransactionLocationID"] = comboBoxDefaultTransLocation.SelectedID;
				}
				else
				{
					dataRow["DefaultTransactionLocationID"] = DBNull.Value;
				}
				if (comboBoxDefaultTransRegister.SelectedID != "")
				{
					dataRow["DefaultTransactionRegisterID"] = comboBoxDefaultTransRegister.SelectedID;
				}
				else
				{
					dataRow["DefaultTransactionRegisterID"] = DBNull.Value;
				}
				dataRow["Phone"] = textBoxPhone.Text;
				dataRow["Email"] = textBoxEmail.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["IsAdmin"] = checkBoxIsAdmin.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.UserTable.Rows.Add(dataRow);
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
			if (SaveData())
			{
				textBoxCode.Focus();
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.UserSystem.GetUserByID(id.Trim());
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
				textBoxCode.Text = dataRow["UserID"].ToString();
				textBoxName.Text = dataRow["UserName"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
				comboBoxDefaultSalesperson.SelectedID = dataRow["DefaultSalesPersonID"].ToString();
				comboBoxDefaultInvLocation.SelectedID = dataRow["DefaultInventoryLocationID"].ToString();
				comboBoxDefaultTransLocation.SelectedID = dataRow["DefaultTransactionLocationID"].ToString();
				comboBoxDefaultTransRegister.SelectedID = dataRow["DefaultTransactionRegisterID"].ToString();
				if (!dataRow["IsCLUser"].IsDBNullOrEmpty())
				{
					checkBoxCLPass.Checked = bool.Parse(dataRow["IsCLUser"].ToString());
				}
				else
				{
					checkBoxCLPass.Checked = false;
				}
				if (!dataRow["CLUserPass"].IsNullOrEmpty())
				{
					textBoxCLPass.Text = Factory.Decrypt(dataRow["CLUserPass"].ToString());
				}
				textBoxPhone.Text = dataRow["Phone"].ToString();
				textBoxEmail.Text = dataRow["Email"].ToString();
				textBoxPassword.Text = "1234567890";
				textBoxConfirmPassword.Text = "1234567890";
				textBoxNote.Text = dataRow["Note"].ToString();
				if (dataRow["Inactive"] != DBNull.Value)
				{
					checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				}
				else
				{
					checkBoxInactive.Checked = false;
				}
				if (dataRow["IsAdmin"] != DBNull.Value)
				{
					checkBoxIsAdmin.Checked = bool.Parse(dataRow["IsAdmin"].ToString());
				}
				else
				{
					checkBoxIsAdmin.Checked = false;
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
				bool flag = (!isNewRecord) ? Factory.UserSystem.UpdateUser(currentData) : Factory.UserSystem.CreateUser(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.User, needRefresh: true);
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
			if (textBoxPassword.Text.Trim() == "" && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Creating a user with a blank password is a security threat.", "Are you sure you want to continue?") == DialogResult.No)
			{
				return false;
			}
			if (textBoxPassword.Text != textBoxConfirmPassword.Text)
			{
				ErrorHelper.ErrorMessage("Password does not match with the confirm password.", "Please try again.");
				textBoxConfirmPassword.Clear();
				textBoxPassword.Clear();
				textBoxPassword.Focus();
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Users", "UserID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("User ID already exist. Please enter another user ID.");
				textBoxCode.Focus();
				return false;
			}
			if (isNewRecord && Factory.UserSystem.ExistSQLLogin(textBoxCode.Text) && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "A SQL login ID with this name already exists.", "Click 'Yes' to use this login ID or click 'No' and enter another user ID.") == DialogResult.No)
			{
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
			textBoxNote.Clear();
			textBoxPassword.Clear();
			textBoxConfirmPassword.Clear();
			comboBoxEmployee.Clear();
			comboBoxLocation.Clear();
			textBoxEmployeeName.Clear();
			textBoxEmail.Clear();
			checkBoxCLPass.Checked = false;
			textBoxCLPass.Clear();
			comboBoxDefaultSalesperson.Clear();
			textBoxPhone.Clear();
			comboBoxDefaultTransLocation.Clear();
			comboBoxDefaultInvLocation.Clear();
			comboBoxDefaultTransLocation.Clear();
			comboBoxDefaultTransRegister.Clear();
			textBoxLocationName.Clear();
			textBoxDefaultTransLocation.Clear();
			textBoxDefaultSalesperson.Clear();
			textBoxDefaultInvLocation.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void UserGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void UserGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.UserSystem.DeleteUser(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Users", "UserID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Users", "UserID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Users", "UserID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Users", "UserID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Users", "UserID", toolStripTextBoxFind.Text.Trim()))
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

		private void UserDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDefaultTransRegister.ShowDefaultRegisterOnly = false;
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
			new FormHelper().ShowList(DataComboType.User);
		}

		private void textBoxCode_Validated(object sender, EventArgs e)
		{
			if (textBoxCode.Text.Trim() == "")
			{
				return;
			}
			for (int i = 0; i < textBoxCode.Text.Length; i = checked(i + 1))
			{
				char c = textBoxCode.Text[i];
				if (!char.IsLetterOrDigit(c))
				{
					textBoxCode.Text = textBoxCode.Text.Replace(c, '_');
				}
			}
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
			if (!(textBoxCode.Text.Trim() == "") && !char.IsLetter(textBoxCode.Text[0]))
			{
				ErrorHelper.InformationMessage("User ID must start with letter.");
				e.Cancel = true;
			}
		}

		private void buttonGroups_Click(object sender, EventArgs e)
		{
			UserGroupAssignForm userGroupAssignForm = new UserGroupAssignForm();
			userGroupAssignForm.UserID = textBoxCode.Text;
			userGroupAssignForm.ShowDialog();
		}

		private void buttonAccessRight_Click(object sender, EventArgs e)
		{
			AccessLevelAssignForm accessLevelAssignForm = new AccessLevelAssignForm();
			accessLevelAssignForm.UserID = textBoxCode.Text;
			accessLevelAssignForm.ShowDialog();
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxDefaultSalesperson.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxDefaultInvLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxDefaultTransLocation.SelectedID);
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
		}

		private void comboBoxDefaultSalesperson_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDefaultSalesperson.Text = comboBoxDefaultSalesperson.SelectedName;
		}

		private void comboBoxDefaultInvLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDefaultInvLocation.Text = comboBoxDefaultInvLocation.SelectedName;
		}

		private void comboBoxDefaultTransLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDefaultTransLocation.Text = comboBoxDefaultTransLocation.SelectedName;
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxLocationName.Text = comboBoxLocation.SelectedName;
		}

		private void buttonChangePassword_Click(object sender, EventArgs e)
		{
			ChangePasswordDialog changePasswordDialog = new ChangePasswordDialog();
			changePasswordDialog.UserID = textBoxCode.Text;
			changePasswordDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRegister(comboBoxDefaultTransRegister.SelectedID);
		}

		private void checkBoxCLPass_CheckedChanged(object sender, EventArgs e)
		{
			MMLabel mMLabel = labelCLPass;
			bool visible = textBoxCLPass.Visible = checkBoxCLPass.Checked;
			mMLabel.Visible = visible;
		}

		private void buttonAddLocation_Click(object sender, EventArgs e)
		{
			LocationAssignForm locationAssignForm = new LocationAssignForm();
			locationAssignForm.UserID = textBoxCode.Text;
			locationAssignForm.ShowDialog();
		}

		private void buttonCreateHomeDashboard_Click(object sender, EventArgs e)
		{
			SelectDashboardTemplateDialog selectDashboardTemplateDialog = new SelectDashboardTemplateDialog();
			selectDashboardTemplateDialog.UserID = textBoxCode.Text;
			selectDashboardTemplateDialog.ShowDialog();
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.UserDetailsForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			textBoxPhone = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxEmail = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonGroups = new Micromind.UISupport.XPButton();
			textBoxPassword = new Micromind.UISupport.MMTextBox();
			textBoxConfirmPassword = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			buttonChangePassword = new Micromind.UISupport.XPButton();
			buttonAccessRight = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDefaultSalesperson = new Micromind.UISupport.MMTextBox();
			comboBoxDefaultInvLocation = new Micromind.DataControls.LocationComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDefaultInvLocation = new Micromind.UISupport.MMTextBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			buttonAddLocation = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDefaultTransRegister = new Micromind.UISupport.MMTextBox();
			comboBoxDefaultTransRegister = new Micromind.DataControls.RegisterComboBox();
			comboBoxDefaultSalesperson = new Micromind.DataControls.SalespersonComboBox();
			comboBoxDefaultTransLocation = new Micromind.DataControls.LocationComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDefaultTransLocation = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxLocationName = new Micromind.UISupport.MMTextBox();
			checkBoxIsAdmin = new System.Windows.Forms.CheckBox();
			checkBoxCLPass = new System.Windows.Forms.CheckBox();
			textBoxCLPass = new Micromind.UISupport.MMTextBox();
			labelCLPass = new Micromind.UISupport.MMLabel();
			buttonCreateHomeDashboard = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultInvLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultTransRegister).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultTransLocation).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
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
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(598, 31);
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 435);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(598, 40);
			panelButtons.TabIndex = 16;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(598, 1);
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
			xpButton1.Location = new System.Drawing.Point(488, 8);
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
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 38);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(54, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "User ID:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(126, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(164, 20);
			textBoxCode.TabIndex = 0;
			textBoxCode.Validating += new System.ComponentModel.CancelEventHandler(textBoxCode_Validating);
			textBoxCode.Validated += new System.EventHandler(textBoxCode_Validated);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(73, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "User Name:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(126, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(461, 20);
			textBoxName.TabIndex = 2;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 211);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(126, 208);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(458, 20);
			textBoxNote.TabIndex = 11;
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
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(293, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(126, 142);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(164, 20);
			comboBoxEmployee.TabIndex = 6;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(comboBoxEmployee_SelectedIndexChanged);
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.ForeColor = System.Drawing.Color.Black;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(293, 142);
			textBoxEmployeeName.MaxLength = 64;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(291, 20);
			textBoxEmployeeName.TabIndex = 7;
			textBoxEmployeeName.TabStop = false;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance13;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(126, 164);
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
			comboBoxLocation.Size = new System.Drawing.Size(164, 20);
			comboBoxLocation.TabIndex = 8;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.SelectedIndexChanged += new System.EventHandler(comboBoxLocation_SelectedIndexChanged);
			textBoxPhone.BackColor = System.Drawing.Color.White;
			textBoxPhone.CustomReportFieldName = "";
			textBoxPhone.CustomReportKey = "";
			textBoxPhone.CustomReportValueType = 1;
			textBoxPhone.IsComboTextBox = false;
			textBoxPhone.IsModified = false;
			textBoxPhone.Location = new System.Drawing.Point(126, 186);
			textBoxPhone.MaxLength = 255;
			textBoxPhone.Name = "textBoxPhone";
			textBoxPhone.Size = new System.Drawing.Size(164, 20);
			textBoxPhone.TabIndex = 9;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(8, 190);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(41, 13);
			mmLabel5.TabIndex = 21;
			mmLabel5.Text = "Phone:";
			textBoxEmail.BackColor = System.Drawing.Color.White;
			textBoxEmail.CustomReportFieldName = "";
			textBoxEmail.CustomReportKey = "";
			textBoxEmail.CustomReportValueType = 1;
			textBoxEmail.IsComboTextBox = false;
			textBoxEmail.IsModified = false;
			textBoxEmail.Location = new System.Drawing.Point(340, 186);
			textBoxEmail.MaxLength = 255;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(244, 20);
			textBoxEmail.TabIndex = 10;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(299, 189);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(35, 13);
			mmLabel6.TabIndex = 23;
			mmLabel6.Text = "Email:";
			appearance25.FontData.BoldAsString = "False";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance25;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 143);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(54, 15);
			ultraFormattedLinkLabel3.TabIndex = 138;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Employee:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			buttonGroups.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonGroups.BackColor = System.Drawing.Color.DarkGray;
			buttonGroups.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonGroups.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonGroups.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonGroups.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonGroups.Location = new System.Drawing.Point(126, 405);
			buttonGroups.Name = "buttonGroups";
			buttonGroups.Size = new System.Drawing.Size(109, 24);
			buttonGroups.TabIndex = 14;
			buttonGroups.Text = "User Groups...";
			buttonGroups.UseVisualStyleBackColor = false;
			buttonGroups.Click += new System.EventHandler(buttonGroups_Click);
			textBoxPassword.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPassword.CustomReportFieldName = "";
			textBoxPassword.CustomReportKey = "";
			textBoxPassword.CustomReportValueType = 1;
			textBoxPassword.ForeColor = System.Drawing.Color.Black;
			textBoxPassword.IsComboTextBox = false;
			textBoxPassword.IsModified = false;
			textBoxPassword.Location = new System.Drawing.Point(126, 80);
			textBoxPassword.MaxLength = 64;
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(164, 20);
			textBoxPassword.TabIndex = 3;
			textBoxPassword.UseSystemPasswordChar = true;
			textBoxConfirmPassword.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConfirmPassword.CustomReportFieldName = "";
			textBoxConfirmPassword.CustomReportKey = "";
			textBoxConfirmPassword.CustomReportValueType = 1;
			textBoxConfirmPassword.ForeColor = System.Drawing.Color.Black;
			textBoxConfirmPassword.IsComboTextBox = false;
			textBoxConfirmPassword.IsModified = false;
			textBoxConfirmPassword.Location = new System.Drawing.Point(126, 102);
			textBoxConfirmPassword.MaxLength = 64;
			textBoxConfirmPassword.Name = "textBoxConfirmPassword";
			textBoxConfirmPassword.Size = new System.Drawing.Size(164, 20);
			textBoxConfirmPassword.TabIndex = 4;
			textBoxConfirmPassword.UseSystemPasswordChar = true;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(8, 83);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(56, 13);
			mmLabel2.TabIndex = 18;
			mmLabel2.Text = "Password:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(8, 105);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(94, 13);
			mmLabel7.TabIndex = 18;
			mmLabel7.Text = "Confirm Password:";
			buttonChangePassword.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonChangePassword.BackColor = System.Drawing.Color.DarkGray;
			buttonChangePassword.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonChangePassword.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonChangePassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonChangePassword.Location = new System.Drawing.Point(293, 79);
			buttonChangePassword.Name = "buttonChangePassword";
			buttonChangePassword.Size = new System.Drawing.Size(114, 23);
			buttonChangePassword.TabIndex = 5;
			buttonChangePassword.Text = "Change Password...";
			buttonChangePassword.UseVisualStyleBackColor = false;
			buttonChangePassword.Click += new System.EventHandler(buttonChangePassword_Click);
			buttonAccessRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAccessRight.BackColor = System.Drawing.Color.DarkGray;
			buttonAccessRight.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAccessRight.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAccessRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAccessRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAccessRight.Location = new System.Drawing.Point(241, 405);
			buttonAccessRight.Name = "buttonAccessRight";
			buttonAccessRight.Size = new System.Drawing.Size(109, 24);
			buttonAccessRight.TabIndex = 15;
			buttonAccessRight.Text = "Access Rights...";
			buttonAccessRight.UseVisualStyleBackColor = false;
			buttonAccessRight.Click += new System.EventHandler(buttonAccessRight_Click);
			appearance27.FontData.BoldAsString = "False";
			appearance27.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance27;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(4, 19);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel1.TabIndex = 141;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Salesperson:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			textBoxDefaultSalesperson.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDefaultSalesperson.CustomReportFieldName = "";
			textBoxDefaultSalesperson.CustomReportKey = "";
			textBoxDefaultSalesperson.CustomReportValueType = 1;
			textBoxDefaultSalesperson.ForeColor = System.Drawing.Color.Black;
			textBoxDefaultSalesperson.IsComboTextBox = false;
			textBoxDefaultSalesperson.IsModified = false;
			textBoxDefaultSalesperson.Location = new System.Drawing.Point(286, 16);
			textBoxDefaultSalesperson.MaxLength = 64;
			textBoxDefaultSalesperson.Name = "textBoxDefaultSalesperson";
			textBoxDefaultSalesperson.ReadOnly = true;
			textBoxDefaultSalesperson.Size = new System.Drawing.Size(291, 20);
			textBoxDefaultSalesperson.TabIndex = 1;
			textBoxDefaultSalesperson.TabStop = false;
			comboBoxDefaultInvLocation.Assigned = false;
			comboBoxDefaultInvLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefaultInvLocation.CustomReportFieldName = "";
			comboBoxDefaultInvLocation.CustomReportKey = "";
			comboBoxDefaultInvLocation.CustomReportValueType = 1;
			comboBoxDefaultInvLocation.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefaultInvLocation.DisplayLayout.Appearance = appearance29;
			comboBoxDefaultInvLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefaultInvLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxDefaultInvLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefaultInvLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefaultInvLocation.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefaultInvLocation.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxDefaultInvLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefaultInvLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultInvLocation.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefaultInvLocation.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxDefaultInvLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefaultInvLocation.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultInvLocation.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxDefaultInvLocation.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxDefaultInvLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefaultInvLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefaultInvLocation.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxDefaultInvLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefaultInvLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxDefaultInvLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefaultInvLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefaultInvLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefaultInvLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefaultInvLocation.Editable = true;
			comboBoxDefaultInvLocation.FilterString = "";
			comboBoxDefaultInvLocation.HasAllAccount = false;
			comboBoxDefaultInvLocation.HasCustom = false;
			comboBoxDefaultInvLocation.IsDataLoaded = false;
			comboBoxDefaultInvLocation.Location = new System.Drawing.Point(120, 38);
			comboBoxDefaultInvLocation.MaxDropDownItems = 12;
			comboBoxDefaultInvLocation.Name = "comboBoxDefaultInvLocation";
			comboBoxDefaultInvLocation.ShowAll = false;
			comboBoxDefaultInvLocation.ShowConsignIn = false;
			comboBoxDefaultInvLocation.ShowConsignOut = false;
			comboBoxDefaultInvLocation.ShowDefaultLocationOnly = false;
			comboBoxDefaultInvLocation.ShowInactiveItems = false;
			comboBoxDefaultInvLocation.ShowNormalLocations = true;
			comboBoxDefaultInvLocation.ShowPOSOnly = false;
			comboBoxDefaultInvLocation.ShowQuickAdd = true;
			comboBoxDefaultInvLocation.ShowWarehouseOnly = false;
			comboBoxDefaultInvLocation.Size = new System.Drawing.Size(163, 20);
			comboBoxDefaultInvLocation.TabIndex = 2;
			comboBoxDefaultInvLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDefaultInvLocation.SelectedIndexChanged += new System.EventHandler(comboBoxDefaultInvLocation_SelectedIndexChanged);
			appearance41.FontData.BoldAsString = "False";
			appearance41.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance41;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(4, 39);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(99, 15);
			ultraFormattedLinkLabel2.TabIndex = 141;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Inventory Location:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance42;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxDefaultInvLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDefaultInvLocation.CustomReportFieldName = "";
			textBoxDefaultInvLocation.CustomReportKey = "";
			textBoxDefaultInvLocation.CustomReportValueType = 1;
			textBoxDefaultInvLocation.ForeColor = System.Drawing.Color.Black;
			textBoxDefaultInvLocation.IsComboTextBox = false;
			textBoxDefaultInvLocation.IsModified = false;
			textBoxDefaultInvLocation.Location = new System.Drawing.Point(286, 38);
			textBoxDefaultInvLocation.MaxLength = 64;
			textBoxDefaultInvLocation.Name = "textBoxDefaultInvLocation";
			textBoxDefaultInvLocation.ReadOnly = true;
			textBoxDefaultInvLocation.Size = new System.Drawing.Size(200, 20);
			textBoxDefaultInvLocation.TabIndex = 3;
			textBoxDefaultInvLocation.TabStop = false;
			ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox3.Controls.Add(buttonAddLocation);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel6);
			ultraGroupBox3.Controls.Add(textBoxDefaultTransRegister);
			ultraGroupBox3.Controls.Add(comboBoxDefaultTransRegister);
			ultraGroupBox3.Controls.Add(comboBoxDefaultSalesperson);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox3.Controls.Add(comboBoxDefaultTransLocation);
			ultraGroupBox3.Controls.Add(comboBoxDefaultInvLocation);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel4);
			ultraGroupBox3.Controls.Add(textBoxDefaultSalesperson);
			ultraGroupBox3.Controls.Add(textBoxDefaultTransLocation);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox3.Controls.Add(textBoxDefaultInvLocation);
			ultraGroupBox3.Location = new System.Drawing.Point(6, 268);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(577, 106);
			ultraGroupBox3.TabIndex = 12;
			ultraGroupBox3.Text = "Defaults";
			buttonAddLocation.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAddLocation.BackColor = System.Drawing.Color.DarkGray;
			buttonAddLocation.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAddLocation.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAddLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAddLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAddLocation.Location = new System.Drawing.Point(487, 38);
			buttonAddLocation.Name = "buttonAddLocation";
			buttonAddLocation.Size = new System.Drawing.Size(90, 20);
			buttonAddLocation.TabIndex = 148;
			buttonAddLocation.Text = "Add Location";
			buttonAddLocation.UseVisualStyleBackColor = false;
			buttonAddLocation.Click += new System.EventHandler(buttonAddLocation_Click);
			appearance43.FontData.BoldAsString = "False";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance43;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(4, 84);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(107, 15);
			ultraFormattedLinkLabel6.TabIndex = 145;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Transaction Register:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			textBoxDefaultTransRegister.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDefaultTransRegister.CustomReportFieldName = "";
			textBoxDefaultTransRegister.CustomReportKey = "";
			textBoxDefaultTransRegister.CustomReportValueType = 1;
			textBoxDefaultTransRegister.ForeColor = System.Drawing.Color.Black;
			textBoxDefaultTransRegister.IsComboTextBox = false;
			textBoxDefaultTransRegister.IsModified = false;
			textBoxDefaultTransRegister.Location = new System.Drawing.Point(286, 82);
			textBoxDefaultTransRegister.MaxLength = 64;
			textBoxDefaultTransRegister.Name = "textBoxDefaultTransRegister";
			textBoxDefaultTransRegister.ReadOnly = true;
			textBoxDefaultTransRegister.Size = new System.Drawing.Size(291, 20);
			textBoxDefaultTransRegister.TabIndex = 7;
			textBoxDefaultTransRegister.TabStop = false;
			comboBoxDefaultTransRegister.Assigned = false;
			comboBoxDefaultTransRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefaultTransRegister.CustomReportFieldName = "";
			comboBoxDefaultTransRegister.CustomReportKey = "";
			comboBoxDefaultTransRegister.CustomReportValueType = 1;
			comboBoxDefaultTransRegister.DescriptionTextBox = textBoxDefaultTransRegister;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefaultTransRegister.DisplayLayout.Appearance = appearance45;
			comboBoxDefaultTransRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefaultTransRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxDefaultTransRegister.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefaultTransRegister.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefaultTransRegister.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefaultTransRegister.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxDefaultTransRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefaultTransRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTransRegister.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefaultTransRegister.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxDefaultTransRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefaultTransRegister.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTransRegister.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxDefaultTransRegister.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxDefaultTransRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefaultTransRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefaultTransRegister.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxDefaultTransRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefaultTransRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxDefaultTransRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefaultTransRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefaultTransRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefaultTransRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefaultTransRegister.Editable = true;
			comboBoxDefaultTransRegister.FilterString = "";
			comboBoxDefaultTransRegister.HasAllAccount = false;
			comboBoxDefaultTransRegister.HasCustom = false;
			comboBoxDefaultTransRegister.IsDataLoaded = false;
			comboBoxDefaultTransRegister.Location = new System.Drawing.Point(120, 82);
			comboBoxDefaultTransRegister.MaxDropDownItems = 12;
			comboBoxDefaultTransRegister.Name = "comboBoxDefaultTransRegister";
			comboBoxDefaultTransRegister.ShowDefaultRegisterOnly = false;
			comboBoxDefaultTransRegister.ShowInactiveItems = false;
			comboBoxDefaultTransRegister.ShowQuickAdd = true;
			comboBoxDefaultTransRegister.Size = new System.Drawing.Size(163, 20);
			comboBoxDefaultTransRegister.TabIndex = 6;
			comboBoxDefaultTransRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDefaultSalesperson.Assigned = false;
			comboBoxDefaultSalesperson.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDefaultSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefaultSalesperson.CustomReportFieldName = "";
			comboBoxDefaultSalesperson.CustomReportKey = "";
			comboBoxDefaultSalesperson.CustomReportValueType = 1;
			comboBoxDefaultSalesperson.DescriptionTextBox = null;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefaultSalesperson.DisplayLayout.Appearance = appearance57;
			comboBoxDefaultSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefaultSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.Appearance = appearance58;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
			comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance60.BackColor2 = System.Drawing.SystemColors.Control;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
			comboBoxDefaultSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefaultSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefaultSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance61;
			appearance62.BackColor = System.Drawing.SystemColors.Highlight;
			appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefaultSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance62;
			comboBoxDefaultSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefaultSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance63;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefaultSalesperson.DisplayLayout.Override.CellAppearance = appearance64;
			comboBoxDefaultSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefaultSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance65.BackColor = System.Drawing.SystemColors.Control;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance65;
			appearance66.TextHAlignAsString = "Left";
			comboBoxDefaultSalesperson.DisplayLayout.Override.HeaderAppearance = appearance66;
			comboBoxDefaultSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefaultSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefaultSalesperson.DisplayLayout.Override.RowAppearance = appearance67;
			comboBoxDefaultSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefaultSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
			comboBoxDefaultSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefaultSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefaultSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefaultSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefaultSalesperson.Editable = true;
			comboBoxDefaultSalesperson.FilterString = "";
			comboBoxDefaultSalesperson.HasAllAccount = false;
			comboBoxDefaultSalesperson.HasCustom = false;
			comboBoxDefaultSalesperson.IsDataLoaded = false;
			comboBoxDefaultSalesperson.Location = new System.Drawing.Point(120, 16);
			comboBoxDefaultSalesperson.MaxDropDownItems = 12;
			comboBoxDefaultSalesperson.Name = "comboBoxDefaultSalesperson";
			comboBoxDefaultSalesperson.ShowInactiveItems = false;
			comboBoxDefaultSalesperson.ShowQuickAdd = true;
			comboBoxDefaultSalesperson.Size = new System.Drawing.Size(163, 20);
			comboBoxDefaultSalesperson.TabIndex = 0;
			comboBoxDefaultSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDefaultSalesperson.SelectedIndexChanged += new System.EventHandler(comboBoxDefaultSalesperson_SelectedIndexChanged);
			comboBoxDefaultTransLocation.Assigned = false;
			comboBoxDefaultTransLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefaultTransLocation.CustomReportFieldName = "";
			comboBoxDefaultTransLocation.CustomReportKey = "";
			comboBoxDefaultTransLocation.CustomReportValueType = 1;
			comboBoxDefaultTransLocation.DescriptionTextBox = null;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefaultTransLocation.DisplayLayout.Appearance = appearance69;
			comboBoxDefaultTransLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefaultTransLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxDefaultTransLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefaultTransLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefaultTransLocation.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefaultTransLocation.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxDefaultTransLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefaultTransLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTransLocation.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefaultTransLocation.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxDefaultTransLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefaultTransLocation.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTransLocation.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxDefaultTransLocation.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxDefaultTransLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefaultTransLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefaultTransLocation.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxDefaultTransLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefaultTransLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxDefaultTransLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefaultTransLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefaultTransLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefaultTransLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefaultTransLocation.Editable = true;
			comboBoxDefaultTransLocation.FilterString = "";
			comboBoxDefaultTransLocation.HasAllAccount = false;
			comboBoxDefaultTransLocation.HasCustom = false;
			comboBoxDefaultTransLocation.IsDataLoaded = false;
			comboBoxDefaultTransLocation.Location = new System.Drawing.Point(120, 60);
			comboBoxDefaultTransLocation.MaxDropDownItems = 12;
			comboBoxDefaultTransLocation.Name = "comboBoxDefaultTransLocation";
			comboBoxDefaultTransLocation.ShowAll = false;
			comboBoxDefaultTransLocation.ShowConsignIn = false;
			comboBoxDefaultTransLocation.ShowConsignOut = false;
			comboBoxDefaultTransLocation.ShowDefaultLocationOnly = false;
			comboBoxDefaultTransLocation.ShowInactiveItems = false;
			comboBoxDefaultTransLocation.ShowNormalLocations = true;
			comboBoxDefaultTransLocation.ShowPOSOnly = false;
			comboBoxDefaultTransLocation.ShowQuickAdd = true;
			comboBoxDefaultTransLocation.ShowWarehouseOnly = false;
			comboBoxDefaultTransLocation.Size = new System.Drawing.Size(163, 20);
			comboBoxDefaultTransLocation.TabIndex = 4;
			comboBoxDefaultTransLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDefaultTransLocation.SelectedIndexChanged += new System.EventHandler(comboBoxDefaultTransLocation_SelectedIndexChanged);
			appearance81.FontData.BoldAsString = "False";
			appearance81.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance81;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(4, 61);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(107, 15);
			ultraFormattedLinkLabel4.TabIndex = 141;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Transaction Location:";
			appearance82.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance82;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			textBoxDefaultTransLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDefaultTransLocation.CustomReportFieldName = "";
			textBoxDefaultTransLocation.CustomReportKey = "";
			textBoxDefaultTransLocation.CustomReportValueType = 1;
			textBoxDefaultTransLocation.ForeColor = System.Drawing.Color.Black;
			textBoxDefaultTransLocation.IsComboTextBox = false;
			textBoxDefaultTransLocation.IsModified = false;
			textBoxDefaultTransLocation.Location = new System.Drawing.Point(286, 60);
			textBoxDefaultTransLocation.MaxLength = 64;
			textBoxDefaultTransLocation.Name = "textBoxDefaultTransLocation";
			textBoxDefaultTransLocation.ReadOnly = true;
			textBoxDefaultTransLocation.Size = new System.Drawing.Size(291, 20);
			textBoxDefaultTransLocation.TabIndex = 5;
			textBoxDefaultTransLocation.TabStop = false;
			appearance83.FontData.BoldAsString = "False";
			appearance83.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance83;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 164);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel5.TabIndex = 144;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Location:";
			appearance84.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance84;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxLocationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLocationName.CustomReportFieldName = "";
			textBoxLocationName.CustomReportKey = "";
			textBoxLocationName.CustomReportValueType = 1;
			textBoxLocationName.ForeColor = System.Drawing.Color.Black;
			textBoxLocationName.IsComboTextBox = false;
			textBoxLocationName.IsModified = false;
			textBoxLocationName.Location = new System.Drawing.Point(293, 164);
			textBoxLocationName.MaxLength = 64;
			textBoxLocationName.Name = "textBoxLocationName";
			textBoxLocationName.ReadOnly = true;
			textBoxLocationName.Size = new System.Drawing.Size(290, 20);
			textBoxLocationName.TabIndex = 7;
			textBoxLocationName.TabStop = false;
			checkBoxIsAdmin.AutoSize = true;
			checkBoxIsAdmin.Location = new System.Drawing.Point(126, 382);
			checkBoxIsAdmin.Name = "checkBoxIsAdmin";
			checkBoxIsAdmin.Size = new System.Drawing.Size(153, 17);
			checkBoxIsAdmin.TabIndex = 13;
			checkBoxIsAdmin.Text = "User is Admin (Full Access)";
			checkBoxIsAdmin.UseVisualStyleBackColor = true;
			checkBoxCLPass.AutoSize = true;
			checkBoxCLPass.Location = new System.Drawing.Point(126, 234);
			checkBoxCLPass.Name = "checkBoxCLPass";
			checkBoxCLPass.Size = new System.Drawing.Size(247, 17);
			checkBoxCLPass.TabIndex = 145;
			checkBoxCLPass.Text = "Can approve over credit limit sales transactions";
			checkBoxCLPass.UseVisualStyleBackColor = true;
			checkBoxCLPass.CheckedChanged += new System.EventHandler(checkBoxCLPass_CheckedChanged);
			textBoxCLPass.BackColor = System.Drawing.Color.White;
			textBoxCLPass.CustomReportFieldName = "";
			textBoxCLPass.CustomReportKey = "";
			textBoxCLPass.CustomReportValueType = 1;
			textBoxCLPass.ForeColor = System.Drawing.Color.Black;
			textBoxCLPass.IsComboTextBox = false;
			textBoxCLPass.IsModified = false;
			textBoxCLPass.Location = new System.Drawing.Point(461, 232);
			textBoxCLPass.MaxLength = 10;
			textBoxCLPass.Name = "textBoxCLPass";
			textBoxCLPass.Size = new System.Drawing.Size(122, 20);
			textBoxCLPass.TabIndex = 146;
			textBoxCLPass.UseSystemPasswordChar = true;
			textBoxCLPass.Visible = false;
			labelCLPass.AutoSize = true;
			labelCLPass.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCLPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCLPass.IsFieldHeader = false;
			labelCLPass.IsRequired = false;
			labelCLPass.Location = new System.Drawing.Point(399, 234);
			labelCLPass.Name = "labelCLPass";
			labelCLPass.PenWidth = 1f;
			labelCLPass.ShowBorder = false;
			labelCLPass.Size = new System.Drawing.Size(56, 13);
			labelCLPass.TabIndex = 147;
			labelCLPass.Text = "Password:";
			labelCLPass.Visible = false;
			buttonCreateHomeDashboard.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCreateHomeDashboard.BackColor = System.Drawing.Color.DarkGray;
			buttonCreateHomeDashboard.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCreateHomeDashboard.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCreateHomeDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCreateHomeDashboard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCreateHomeDashboard.Location = new System.Drawing.Point(356, 405);
			buttonCreateHomeDashboard.Name = "buttonCreateHomeDashboard";
			buttonCreateHomeDashboard.Size = new System.Drawing.Size(136, 24);
			buttonCreateHomeDashboard.TabIndex = 148;
			buttonCreateHomeDashboard.Text = "Create Home Dashboard";
			buttonCreateHomeDashboard.UseVisualStyleBackColor = false;
			buttonCreateHomeDashboard.Click += new System.EventHandler(buttonCreateHomeDashboard_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(598, 475);
			base.Controls.Add(buttonCreateHomeDashboard);
			base.Controls.Add(labelCLPass);
			base.Controls.Add(textBoxCLPass);
			base.Controls.Add(checkBoxCLPass);
			base.Controls.Add(checkBoxIsAdmin);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(buttonAccessRight);
			base.Controls.Add(buttonChangePassword);
			base.Controls.Add(buttonGroups);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(textBoxEmail);
			base.Controls.Add(textBoxLocationName);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(textBoxPhone);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(comboBoxLocation);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxConfirmPassword);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "UserDetailsForm";
			Text = "User Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultInvLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultTransRegister).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultTransLocation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
