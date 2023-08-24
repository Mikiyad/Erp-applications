using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class LeaveTypeDetailsForm : Form, IForm
	{
		private LeaveTypeData currentData;

		private const string TABLENAME_CONST = "Leave_Type";

		private const string IDFIELD_CONST = "LeaveTypeID";

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private NumberTextBox textBoxDays;

		private Label label1;

		private CheckBox checkBoxIsPaid;

		private CheckBox checkBoxIsCumulative;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private CheckBox checkBoxIsAnnual;

		private Label label2;

		private ComboBox comboBoxProportion;

		private UltraFormattedLinkLabel labelAccount;

		private MMTextBox textBoxAccountName;

		private AllAccountsComboBox comboBoxAccount;

		private ToolStripButton toolStripButtonInformation;

		private Label label3;

		private Label label4;

		private Label label5;

		private GroupBox groupBoxLeaveSettings;

		private CheckBox checkBoxallowtopaythroughsettlement;

		private CheckBox checkBoxallowtoEncash;

		private NumberTextBox textBoxDays1;

		private NumberTextBox textBoxDays2;

		private NumberTextBox textBoxDays3;

		private NumberTextBox textBoxLSMonth1;

		private NumberTextBox textBoxLSMonth2;

		private NumberTextBox textBoxLSMonth3;

		private Label label11;

		private Label label10;

		private Label label9;

		private Label label8;

		private Label label7;

		private Label label6;

		private NumberTextBox textBoxGRMonth3;

		private NumberTextBox textBoxGRMonth2;

		private NumberTextBox textBoxGRMonth1;

		private CheckBox checkBoxApplyHC;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

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

		public LeaveTypeDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += LeaveTypeDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new LeaveTypeData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.LeaveTypeTable.Rows[0] : currentData.LeaveTypeTable.NewRow();
				dataRow.BeginEdit();
				dataRow["LeaveTypeID"] = textBoxCode.Text.Trim();
				dataRow["LeaveTypeName"] = textBoxName.Text.Trim();
				dataRow["Days"] = decimal.Parse(textBoxDays.Text);
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["IsPayable"] = checkBoxIsPaid.Checked;
				dataRow["IsCumulative"] = checkBoxIsCumulative.Checked;
				dataRow["IsAnnual"] = checkBoxIsAnnual.Checked;
				dataRow["ActivateHC"] = checkBoxApplyHC.Checked;
				dataRow["DeductionProportion"] = decimal.Parse(comboBoxProportion.SelectedItem.ToString());
				dataRow["AccountID"] = comboBoxAccount.SelectedID;
				if (textBoxGRMonth1.Text != null && textBoxGRMonth1.Text != "")
				{
					dataRow["MonthGreater1"] = int.Parse(textBoxGRMonth1.Text);
				}
				else
				{
					dataRow["MonthGreater1"] = DBNull.Value;
				}
				if (textBoxLSMonth1.Text != null && textBoxLSMonth1.Text != "")
				{
					dataRow["MonthLesser1"] = int.Parse(textBoxLSMonth1.Text);
				}
				else
				{
					dataRow["MonthLesser1"] = DBNull.Value;
				}
				if (textBoxDays1.Text != null && textBoxDays1.Text != "")
				{
					dataRow["AllowedDays1"] = decimal.Parse(textBoxDays1.Text);
				}
				else
				{
					dataRow["AllowedDays1"] = DBNull.Value;
				}
				if (textBoxGRMonth2.Text != null && textBoxGRMonth2.Text != "")
				{
					dataRow["MonthGreater2"] = textBoxGRMonth2.Text;
				}
				else
				{
					dataRow["MonthGreater2"] = DBNull.Value;
				}
				if (textBoxLSMonth2.Text != null && textBoxLSMonth2.Text != "")
				{
					dataRow["MonthLesser2"] = textBoxLSMonth2.Text;
				}
				else
				{
					dataRow["MonthLesser2"] = DBNull.Value;
				}
				if (textBoxDays2.Text != null && textBoxDays2.Text != "")
				{
					dataRow["AllowedDays2"] = decimal.Parse(textBoxDays2.Text);
				}
				else
				{
					dataRow["AllowedDays2"] = DBNull.Value;
				}
				if (textBoxGRMonth3.Text != null && textBoxGRMonth3.Text != "")
				{
					dataRow["MonthGreater3"] = textBoxGRMonth3.Text;
				}
				else
				{
					dataRow["MonthGreater3"] = DBNull.Value;
				}
				if (textBoxLSMonth3.Text != null && textBoxLSMonth3.Text != "")
				{
					dataRow["MonthLesser3"] = textBoxLSMonth3.Text;
				}
				else
				{
					dataRow["MonthLesser3"] = DBNull.Value;
				}
				if (textBoxLSMonth3.Text != null && textBoxLSMonth3.Text != "")
				{
					dataRow["AllowedDays3"] = decimal.Parse(textBoxDays3.Text);
				}
				else
				{
					dataRow["AllowedDays3"] = DBNull.Value;
				}
				dataRow["IsEncash"] = checkBoxallowtoEncash.Checked;
				dataRow["IsLeaveSettle"] = checkBoxallowtopaythroughsettlement.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.LeaveTypeTable.Rows.Add(dataRow);
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
					currentData = Factory.LeaveTypeSystem.GetLeaveTypeByID(id.Trim());
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
				textBoxCode.Text = dataRow["LeaveTypeID"].ToString();
				textBoxName.Text = dataRow["LeaveTypeName"].ToString();
				comboBoxAccount.SelectedID = dataRow["AccountID"].ToString();
				if (dataRow["IsPayable"] != DBNull.Value)
				{
					checkBoxIsPaid.Checked = bool.Parse(dataRow["IsPayable"].ToString());
				}
				else
				{
					checkBoxIsPaid.Checked = false;
				}
				if (dataRow["IsCumulative"] != DBNull.Value)
				{
					checkBoxIsCumulative.Checked = bool.Parse(dataRow["IsCumulative"].ToString());
				}
				else
				{
					checkBoxIsCumulative.Checked = false;
				}
				if (dataRow["Days"] != DBNull.Value)
				{
					textBoxDays.Text = int.Parse(dataRow["Days"].ToString()).ToString();
				}
				else
				{
					textBoxDays.Text = "0";
				}
				if (dataRow["Inactive"] != DBNull.Value)
				{
					checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				}
				else
				{
					checkBoxInactive.Checked = false;
				}
				if (dataRow["IsAnnual"] != DBNull.Value)
				{
					checkBoxIsAnnual.Checked = bool.Parse(dataRow["IsAnnual"].ToString());
				}
				else
				{
					checkBoxIsAnnual.Checked = false;
				}
				if (dataRow["ActivateHC"] != DBNull.Value)
				{
					checkBoxApplyHC.Checked = bool.Parse(dataRow["ActivateHC"].ToString());
				}
				else
				{
					checkBoxApplyHC.Checked = false;
				}
				if (dataRow["DeductionProportion"] != DBNull.Value)
				{
					comboBoxProportion.SelectedItem = dataRow["DeductionProportion"].ToString();
				}
				else
				{
					comboBoxProportion.SelectedItem = "0";
				}
				if (dataRow["MonthGreater1"] != DBNull.Value)
				{
					textBoxGRMonth1.Text = int.Parse(dataRow["MonthGreater1"].ToString()).ToString();
				}
				else
				{
					textBoxGRMonth1.Text = "0";
				}
				if (dataRow["MonthLesser1"] != DBNull.Value)
				{
					textBoxLSMonth1.Text = int.Parse(dataRow["MonthLesser1"].ToString()).ToString();
				}
				else
				{
					textBoxLSMonth1.Text = "0";
				}
				if (dataRow["AllowedDays1"] != DBNull.Value)
				{
					textBoxDays1.Text = decimal.Parse(dataRow["AllowedDays1"].ToString()).ToString();
				}
				else
				{
					textBoxDays1.Text = "0";
				}
				if (dataRow["MonthGreater2"] != DBNull.Value)
				{
					textBoxGRMonth2.Text = int.Parse(dataRow["MonthGreater2"].ToString()).ToString();
				}
				else
				{
					textBoxGRMonth2.Text = "0";
				}
				if (dataRow["MonthLesser2"] != DBNull.Value)
				{
					textBoxLSMonth2.Text = int.Parse(dataRow["MonthLesser2"].ToString()).ToString();
				}
				else
				{
					textBoxLSMonth2.Text = "0";
				}
				if (dataRow["AllowedDays2"] != DBNull.Value)
				{
					textBoxDays2.Text = decimal.Parse(dataRow["AllowedDays2"].ToString()).ToString();
				}
				else
				{
					textBoxDays2.Text = "0";
				}
				if (dataRow["MonthGreater3"] != DBNull.Value)
				{
					textBoxGRMonth3.Text = int.Parse(dataRow["MonthGreater3"].ToString()).ToString();
				}
				else
				{
					textBoxGRMonth3.Text = "0";
				}
				if (dataRow["MonthLesser3"] != DBNull.Value)
				{
					textBoxLSMonth3.Text = int.Parse(dataRow["MonthLesser3"].ToString()).ToString();
				}
				else
				{
					textBoxLSMonth3.Text = "0";
				}
				if (dataRow["AllowedDays3"] != DBNull.Value)
				{
					textBoxDays3.Text = decimal.Parse(dataRow["AllowedDays3"].ToString()).ToString();
				}
				else
				{
					textBoxDays3.Text = "0";
				}
				if (dataRow["IsEncash"] != DBNull.Value)
				{
					checkBoxallowtoEncash.Checked = bool.Parse(dataRow["IsEncash"].ToString());
				}
				else
				{
					checkBoxallowtoEncash.Checked = false;
				}
				if (dataRow["IsLeaveSettle"] != DBNull.Value)
				{
					checkBoxallowtopaythroughsettlement.Checked = bool.Parse(dataRow["IsLeaveSettle"].ToString());
				}
				else
				{
					checkBoxallowtopaythroughsettlement.Checked = false;
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.LeaveTypeSystem.CreateLeaveType(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.LeaveType, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.LeaveTypeSystem.UpdateLeaveType(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Leave_Type", "LeaveTypeID", textBoxCode.Text.Trim()))
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
			textBoxCode.Clear();
			textBoxName.Clear();
			textBoxDays.Text = "0";
			checkBoxInactive.Checked = false;
			checkBoxIsCumulative.Checked = false;
			checkBoxIsPaid.Checked = false;
			checkBoxIsAnnual.Checked = false;
			checkBoxApplyHC.Checked = false;
			comboBoxProportion.SelectedIndex = 0;
			comboBoxAccount.SelectedIndex = 0;
			textBoxGRMonth1.Text = "0";
			textBoxLSMonth1.Text = "0";
			textBoxDays1.Text = "0";
			textBoxGRMonth2.Text = "0";
			textBoxLSMonth2.Text = "0";
			textBoxDays2.Text = "0";
			textBoxGRMonth3.Text = "0";
			textBoxLSMonth3.Text = "0";
			textBoxDays3.Text = "0";
			checkBoxallowtoEncash.Checked = false;
			checkBoxallowtopaythroughsettlement.Checked = false;
			EnableLeaveSettings(status: true);
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void LeaveTypeGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void LeaveTypeGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.LeaveTypeSystem.DeleteLeaveType(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Leave_Type", "LeaveTypeID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Leave_Type", "LeaveTypeID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Leave_Type", "LeaveTypeID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Leave_Type", "LeaveTypeID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Leave_Type", "LeaveTypeID", toolStripTextBoxFind.Text.Trim()))
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

		private void LeaveTypeDetailsForm_Load(object sender, EventArgs e)
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.LeaveType);
		}

		private void checkBoxIsAnnual_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxIsAnnual.Checked)
			{
				AllAccountsComboBox allAccountsComboBox = comboBoxAccount;
				bool readOnly = textBoxAccountName.ReadOnly = false;
				allAccountsComboBox.ReadOnly = readOnly;
				textBoxDays.ReadOnly = true;
				textBoxDays.Text = "0";
				EnableLeaveSettings(status: false);
			}
			else
			{
				AllAccountsComboBox allAccountsComboBox2 = comboBoxAccount;
				bool readOnly = textBoxAccountName.ReadOnly = true;
				allAccountsComboBox2.ReadOnly = readOnly;
				textBoxDays.ReadOnly = false;
				textBoxDays.Clear();
				EnableLeaveSettings(status: true);
				comboBoxAccount.Clear();
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void EnableLeaveSettings(bool status)
		{
			NumberTextBox numberTextBox = textBoxGRMonth1;
			NumberTextBox numberTextBox2 = textBoxGRMonth2;
			NumberTextBox numberTextBox3 = textBoxGRMonth3;
			NumberTextBox numberTextBox4 = textBoxLSMonth1;
			NumberTextBox numberTextBox5 = textBoxLSMonth2;
			bool flag2 = textBoxLSMonth3.ReadOnly = status;
			bool flag4 = numberTextBox5.ReadOnly = flag2;
			bool flag6 = numberTextBox4.ReadOnly = flag4;
			bool flag8 = numberTextBox3.ReadOnly = flag6;
			bool readOnly = numberTextBox2.ReadOnly = flag8;
			numberTextBox.ReadOnly = readOnly;
			NumberTextBox numberTextBox6 = textBoxDays1;
			NumberTextBox numberTextBox7 = textBoxDays2;
			flag8 = (textBoxDays3.ReadOnly = status);
			readOnly = (numberTextBox7.ReadOnly = flag8);
			numberTextBox6.ReadOnly = readOnly;
			CheckBox checkBox = checkBoxallowtoEncash;
			readOnly = (checkBoxallowtopaythroughsettlement.Enabled = !status);
			checkBox.Enabled = readOnly;
			if (!status)
			{
				textBoxGRMonth1.Text = "0";
				textBoxGRMonth2.Text = "6";
				textBoxGRMonth3.Text = "12";
				textBoxLSMonth1.Text = "6";
				textBoxLSMonth2.Text = "12";
				textBoxLSMonth3.Text = "600";
				textBoxDays1.Text = "0";
				textBoxDays2.Text = "2";
				textBoxDays3.Text = "2.5";
			}
			else
			{
				textBoxGRMonth1.Clear();
				textBoxGRMonth2.Clear();
				textBoxGRMonth3.Clear();
				textBoxLSMonth1.Clear();
				textBoxLSMonth2.Clear();
				textBoxLSMonth3.Clear();
				textBoxDays1.Clear();
				textBoxDays2.Clear();
				textBoxDays3.Clear();
			}
		}

		private void labelAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAccount.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.LeaveTypeDetailsForm));
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
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			checkBoxIsPaid = new System.Windows.Forms.CheckBox();
			checkBoxIsCumulative = new System.Windows.Forms.CheckBox();
			checkBoxIsAnnual = new System.Windows.Forms.CheckBox();
			label2 = new System.Windows.Forms.Label();
			comboBoxProportion = new System.Windows.Forms.ComboBox();
			labelAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			groupBoxLeaveSettings = new System.Windows.Forms.GroupBox();
			checkBoxallowtopaythroughsettlement = new System.Windows.Forms.CheckBox();
			checkBoxallowtoEncash = new System.Windows.Forms.CheckBox();
			textBoxDays1 = new Micromind.UISupport.NumberTextBox();
			textBoxDays2 = new Micromind.UISupport.NumberTextBox();
			textBoxDays3 = new Micromind.UISupport.NumberTextBox();
			textBoxLSMonth1 = new Micromind.UISupport.NumberTextBox();
			textBoxLSMonth2 = new Micromind.UISupport.NumberTextBox();
			textBoxLSMonth3 = new Micromind.UISupport.NumberTextBox();
			label11 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			textBoxGRMonth3 = new Micromind.UISupport.NumberTextBox();
			textBoxGRMonth2 = new Micromind.UISupport.NumberTextBox();
			textBoxGRMonth1 = new Micromind.UISupport.NumberTextBox();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			checkBoxApplyHC = new System.Windows.Forms.CheckBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			groupBoxLeaveSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(470, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 346);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(470, 40);
			panelButtons.TabIndex = 12;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(470, 1);
			linePanelDown.TabIndex = 3;
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
			xpButton1.Location = new System.Drawing.Point(360, 8);
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
			buttonSave.TabIndex = 4;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(297, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 102);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(149, 13);
			label1.TabIndex = 4;
			label1.Text = "Maximum leave days per year:";
			checkBoxIsPaid.AutoSize = true;
			checkBoxIsPaid.Location = new System.Drawing.Point(15, 132);
			checkBoxIsPaid.Name = "checkBoxIsPaid";
			checkBoxIsPaid.Size = new System.Drawing.Size(86, 17);
			checkBoxIsPaid.TabIndex = 7;
			checkBoxIsPaid.Text = "Is paid leave";
			checkBoxIsPaid.UseVisualStyleBackColor = true;
			checkBoxIsCumulative.AutoSize = true;
			checkBoxIsCumulative.Location = new System.Drawing.Point(15, 155);
			checkBoxIsCumulative.Name = "checkBoxIsCumulative";
			checkBoxIsCumulative.Size = new System.Drawing.Size(250, 17);
			checkBoxIsCumulative.TabIndex = 8;
			checkBoxIsCumulative.Text = "Leave is cumulative and can be carried forward";
			checkBoxIsCumulative.UseVisualStyleBackColor = true;
			checkBoxIsAnnual.AutoSize = true;
			checkBoxIsAnnual.Location = new System.Drawing.Point(163, 132);
			checkBoxIsAnnual.Name = "checkBoxIsAnnual";
			checkBoxIsAnnual.Size = new System.Drawing.Size(70, 17);
			checkBoxIsAnnual.TabIndex = 9;
			checkBoxIsAnnual.Text = "Is Annual";
			checkBoxIsAnnual.UseVisualStyleBackColor = true;
			checkBoxIsAnnual.CheckStateChanged += new System.EventHandler(checkBoxIsAnnual_CheckStateChanged);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(252, 102);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(110, 13);
			label2.TabIndex = 18;
			label2.Text = "Deduction Proportion:";
			comboBoxProportion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxProportion.FormattingEnabled = true;
			comboBoxProportion.Items.AddRange(new object[11]
			{
				"0",
				"0.5",
				"1",
				"1.5",
				"2",
				"2.5",
				"3",
				"3.5",
				"4",
				"4.5",
				"5"
			});
			comboBoxProportion.Location = new System.Drawing.Point(360, 99);
			comboBoxProportion.Name = "comboBoxProportion";
			comboBoxProportion.Size = new System.Drawing.Size(60, 21);
			comboBoxProportion.TabIndex = 6;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			labelAccount.Appearance = appearance;
			labelAccount.AutoSize = true;
			labelAccount.Location = new System.Drawing.Point(16, 177);
			labelAccount.Name = "labelAccount";
			labelAccount.Size = new System.Drawing.Size(53, 15);
			labelAccount.TabIndex = 9;
			labelAccount.TabStop = true;
			labelAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelAccount.Value = "Account:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			labelAccount.VisitedLinkAppearance = appearance2;
			labelAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelAccount_LinkClicked);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 238);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(106, 13);
			label3.TabIndex = 68;
			label3.Text = "Service greater than ";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(11, 261);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(106, 13);
			label4.TabIndex = 69;
			label4.Text = "Service greater than ";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(12, 287);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(106, 13);
			label5.TabIndex = 70;
			label5.Text = "Service greater than ";
			groupBoxLeaveSettings.Controls.Add(checkBoxallowtopaythroughsettlement);
			groupBoxLeaveSettings.Controls.Add(checkBoxallowtoEncash);
			groupBoxLeaveSettings.Controls.Add(textBoxDays1);
			groupBoxLeaveSettings.Controls.Add(textBoxDays2);
			groupBoxLeaveSettings.Controls.Add(textBoxDays3);
			groupBoxLeaveSettings.Controls.Add(textBoxLSMonth1);
			groupBoxLeaveSettings.Controls.Add(textBoxLSMonth2);
			groupBoxLeaveSettings.Controls.Add(textBoxLSMonth3);
			groupBoxLeaveSettings.Controls.Add(label11);
			groupBoxLeaveSettings.Controls.Add(label10);
			groupBoxLeaveSettings.Controls.Add(label9);
			groupBoxLeaveSettings.Controls.Add(label8);
			groupBoxLeaveSettings.Controls.Add(label7);
			groupBoxLeaveSettings.Controls.Add(label6);
			groupBoxLeaveSettings.Controls.Add(textBoxGRMonth3);
			groupBoxLeaveSettings.Controls.Add(textBoxGRMonth2);
			groupBoxLeaveSettings.Controls.Add(textBoxGRMonth1);
			groupBoxLeaveSettings.Location = new System.Drawing.Point(9, 211);
			groupBoxLeaveSettings.Name = "groupBoxLeaveSettings";
			groupBoxLeaveSettings.Size = new System.Drawing.Size(432, 129);
			groupBoxLeaveSettings.TabIndex = 11;
			groupBoxLeaveSettings.TabStop = false;
			checkBoxallowtopaythroughsettlement.AutoSize = true;
			checkBoxallowtopaythroughsettlement.Location = new System.Drawing.Point(198, 104);
			checkBoxallowtopaythroughsettlement.Name = "checkBoxallowtopaythroughsettlement";
			checkBoxallowtopaythroughsettlement.Size = new System.Drawing.Size(202, 17);
			checkBoxallowtopaythroughsettlement.TabIndex = 72;
			checkBoxallowtopaythroughsettlement.Text = "Allow to pay through leave settlement";
			checkBoxallowtopaythroughsettlement.UseVisualStyleBackColor = true;
			checkBoxallowtoEncash.AutoSize = true;
			checkBoxallowtoEncash.Location = new System.Drawing.Point(8, 104);
			checkBoxallowtoEncash.Name = "checkBoxallowtoEncash";
			checkBoxallowtoEncash.Size = new System.Drawing.Size(135, 17);
			checkBoxallowtoEncash.TabIndex = 72;
			checkBoxallowtoEncash.Text = "Allow to Encash Leave";
			checkBoxallowtoEncash.UseVisualStyleBackColor = true;
			textBoxDays1.AllowDecimal = true;
			textBoxDays1.CustomReportFieldName = "";
			textBoxDays1.CustomReportKey = "";
			textBoxDays1.CustomReportValueType = 1;
			textBoxDays1.IsComboTextBox = false;
			textBoxDays1.Location = new System.Drawing.Point(351, 24);
			textBoxDays1.MaxLength = 4;
			textBoxDays1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDays1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDays1.Name = "textBoxDays1";
			textBoxDays1.NullText = "0";
			textBoxDays1.Size = new System.Drawing.Size(40, 20);
			textBoxDays1.TabIndex = 86;
			textBoxDays1.Text = "0";
			textBoxDays1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDays2.AllowDecimal = true;
			textBoxDays2.CustomReportFieldName = "";
			textBoxDays2.CustomReportKey = "";
			textBoxDays2.CustomReportValueType = 1;
			textBoxDays2.IsComboTextBox = false;
			textBoxDays2.Location = new System.Drawing.Point(351, 48);
			textBoxDays2.MaxLength = 4;
			textBoxDays2.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDays2.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDays2.Name = "textBoxDays2";
			textBoxDays2.NullText = "0";
			textBoxDays2.Size = new System.Drawing.Size(40, 20);
			textBoxDays2.TabIndex = 85;
			textBoxDays2.Text = "0";
			textBoxDays2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDays3.AllowDecimal = true;
			textBoxDays3.CustomReportFieldName = "";
			textBoxDays3.CustomReportKey = "";
			textBoxDays3.CustomReportValueType = 1;
			textBoxDays3.IsComboTextBox = false;
			textBoxDays3.Location = new System.Drawing.Point(351, 72);
			textBoxDays3.MaxLength = 4;
			textBoxDays3.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDays3.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDays3.Name = "textBoxDays3";
			textBoxDays3.NullText = "0";
			textBoxDays3.Size = new System.Drawing.Size(40, 20);
			textBoxDays3.TabIndex = 84;
			textBoxDays3.Text = "0";
			textBoxDays3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLSMonth1.AllowDecimal = false;
			textBoxLSMonth1.CustomReportFieldName = "";
			textBoxLSMonth1.CustomReportKey = "";
			textBoxLSMonth1.CustomReportValueType = 1;
			textBoxLSMonth1.IsComboTextBox = false;
			textBoxLSMonth1.Location = new System.Drawing.Point(249, 27);
			textBoxLSMonth1.MaxLength = 5;
			textBoxLSMonth1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLSMonth1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLSMonth1.Name = "textBoxLSMonth1";
			textBoxLSMonth1.NullText = "0";
			textBoxLSMonth1.Size = new System.Drawing.Size(40, 20);
			textBoxLSMonth1.TabIndex = 83;
			textBoxLSMonth1.Text = "0";
			textBoxLSMonth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLSMonth2.AllowDecimal = false;
			textBoxLSMonth2.CustomReportFieldName = "";
			textBoxLSMonth2.CustomReportKey = "";
			textBoxLSMonth2.CustomReportValueType = 1;
			textBoxLSMonth2.IsComboTextBox = false;
			textBoxLSMonth2.Location = new System.Drawing.Point(249, 49);
			textBoxLSMonth2.MaxLength = 5;
			textBoxLSMonth2.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLSMonth2.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLSMonth2.Name = "textBoxLSMonth2";
			textBoxLSMonth2.NullText = "0";
			textBoxLSMonth2.Size = new System.Drawing.Size(40, 20);
			textBoxLSMonth2.TabIndex = 82;
			textBoxLSMonth2.Text = "0";
			textBoxLSMonth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLSMonth3.AllowDecimal = false;
			textBoxLSMonth3.CustomReportFieldName = "";
			textBoxLSMonth3.CustomReportKey = "";
			textBoxLSMonth3.CustomReportValueType = 1;
			textBoxLSMonth3.IsComboTextBox = false;
			textBoxLSMonth3.Location = new System.Drawing.Point(249, 71);
			textBoxLSMonth3.MaxLength = 5;
			textBoxLSMonth3.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLSMonth3.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLSMonth3.Name = "textBoxLSMonth3";
			textBoxLSMonth3.NullText = "0";
			textBoxLSMonth3.Size = new System.Drawing.Size(40, 20);
			textBoxLSMonth3.TabIndex = 81;
			textBoxLSMonth3.Text = "0";
			textBoxLSMonth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(329, 8);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(71, 13);
			label11.TabIndex = 80;
			label11.Text = "Allowed Days";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(247, 8);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(42, 13);
			label10.TabIndex = 79;
			label10.Text = "Months";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(105, 8);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(42, 13);
			label9.TabIndex = 78;
			label9.Text = "Months";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(173, 77);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(73, 13);
			label8.TabIndex = 77;
			label8.Text = "and less than ";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(172, 52);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(73, 13);
			label7.TabIndex = 76;
			label7.Tag = "0";
			label7.Text = "and less than ";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(172, 30);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(73, 13);
			label6.TabIndex = 75;
			label6.Text = "and less than ";
			textBoxGRMonth3.AllowDecimal = false;
			textBoxGRMonth3.CustomReportFieldName = "";
			textBoxGRMonth3.CustomReportKey = "";
			textBoxGRMonth3.CustomReportValueType = 1;
			textBoxGRMonth3.IsComboTextBox = false;
			textBoxGRMonth3.Location = new System.Drawing.Point(109, 70);
			textBoxGRMonth3.MaxLength = 5;
			textBoxGRMonth3.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxGRMonth3.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxGRMonth3.Name = "textBoxGRMonth3";
			textBoxGRMonth3.NullText = "0";
			textBoxGRMonth3.Size = new System.Drawing.Size(41, 20);
			textBoxGRMonth3.TabIndex = 74;
			textBoxGRMonth3.Text = "0";
			textBoxGRMonth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxGRMonth2.AllowDecimal = false;
			textBoxGRMonth2.CustomReportFieldName = "";
			textBoxGRMonth2.CustomReportKey = "";
			textBoxGRMonth2.CustomReportValueType = 1;
			textBoxGRMonth2.IsComboTextBox = false;
			textBoxGRMonth2.Location = new System.Drawing.Point(109, 47);
			textBoxGRMonth2.MaxLength = 5;
			textBoxGRMonth2.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxGRMonth2.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxGRMonth2.Name = "textBoxGRMonth2";
			textBoxGRMonth2.NullText = "0";
			textBoxGRMonth2.Size = new System.Drawing.Size(40, 20);
			textBoxGRMonth2.TabIndex = 73;
			textBoxGRMonth2.Text = "0";
			textBoxGRMonth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxGRMonth1.AllowDecimal = false;
			textBoxGRMonth1.CustomReportFieldName = "";
			textBoxGRMonth1.CustomReportKey = "";
			textBoxGRMonth1.CustomReportValueType = 1;
			textBoxGRMonth1.IsComboTextBox = false;
			textBoxGRMonth1.Location = new System.Drawing.Point(109, 24);
			textBoxGRMonth1.MaxLength = 5;
			textBoxGRMonth1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxGRMonth1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxGRMonth1.Name = "textBoxGRMonth1";
			textBoxGRMonth1.NullText = "0";
			textBoxGRMonth1.Size = new System.Drawing.Size(40, 20);
			textBoxGRMonth1.TabIndex = 72;
			textBoxGRMonth1.Text = "0";
			textBoxGRMonth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.CustomReportFieldName = "";
			textBoxAccountName.CustomReportKey = "";
			textBoxAccountName.CustomReportValueType = 1;
			textBoxAccountName.Enabled = false;
			textBoxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxAccountName.IsComboTextBox = false;
			textBoxAccountName.Location = new System.Drawing.Point(129, 194);
			textBoxAccountName.MaxLength = 15;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(312, 20);
			textBoxAccountName.TabIndex = 66;
			textBoxAccountName.TabStop = false;
			comboBoxAccount.Assigned = false;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = textBoxAccountName;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance3;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccount.Editable = true;
			comboBoxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccount.FilterString = "";
			comboBoxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccount.FilterSysDocID = "";
			comboBoxAccount.HasAllAccount = false;
			comboBoxAccount.HasCustom = false;
			comboBoxAccount.IsDataLoaded = false;
			comboBoxAccount.Location = new System.Drawing.Point(129, 172);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ReadOnly = true;
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(169, 20);
			comboBoxAccount.TabIndex = 10;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDays.AllowDecimal = false;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.IsComboTextBox = false;
			textBoxDays.Location = new System.Drawing.Point(163, 99);
			textBoxDays.MaxLength = 3;
			textBoxDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDays.Name = "textBoxDays";
			textBoxDays.NullText = "0";
			textBoxDays.Size = new System.Drawing.Size(74, 20);
			textBoxDays.TabIndex = 5;
			textBoxDays.Text = "0";
			textBoxDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(129, 58);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(307, 20);
			textBoxName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(129, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(162, 20);
			textBoxCode.TabIndex = 1;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Description:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(111, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Leave Type Code:";
			checkBoxApplyHC.AutoSize = true;
			checkBoxApplyHC.Location = new System.Drawing.Point(277, 132);
			checkBoxApplyHC.Name = "checkBoxApplyHC";
			checkBoxApplyHC.Size = new System.Drawing.Size(148, 17);
			checkBoxApplyHC.TabIndex = 71;
			checkBoxApplyHC.Text = "Activate Holiday Calendar";
			checkBoxApplyHC.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(470, 386);
			base.Controls.Add(checkBoxApplyHC);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(labelAccount);
			base.Controls.Add(textBoxAccountName);
			base.Controls.Add(comboBoxAccount);
			base.Controls.Add(comboBoxProportion);
			base.Controls.Add(label2);
			base.Controls.Add(checkBoxIsAnnual);
			base.Controls.Add(checkBoxIsCumulative);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxDays);
			base.Controls.Add(checkBoxIsPaid);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(groupBoxLeaveSettings);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "LeaveTypeDetailsForm";
			Text = "Leave Type";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			groupBoxLeaveSettings.ResumeLayout(false);
			groupBoxLeaveSettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
