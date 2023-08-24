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
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class BankFacilityGroupDetailsForm : Form, IForm
	{
		private BankFacilityGroupData currentData;

		private const string TABLENAME_CONST = "Bank_Facility_Group";

		private const string IDFIELD_CONST = "GroupID";

		private bool isNewRecord = true;

		private DataSet facilityData;

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

		private MMLabel mmLabel4;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMTextBox textBoxNote;

		private BankComboBox comboBoxBank;

		private UltraFormattedLinkLabel linkLabelBank;

		private AmountTextBox textBoxTotalAmount;

		private MMLabel mmLabel2;

		private MMSDateTimePicker dateTimeStartDate;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMSDateTimePicker dateTimeRenewDate;

		private MMLabel mmLabel6;

		private MMSDateTimePicker dateTimeExpiryDate;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageContacts;

		private MMLabel mmLabel37;

		private DataEntryGrid dataGridContacts;

		private ContactsComboBox gridComboBoxContact;

		private Panel panel1;

		private MMLabel labelCustomerNameHeader;

		private MMTextBox textBoxContactNumber;

		private MMTextBox textBoxContactName;

		private MMLabel mmLabel9;

		private MMLabel mmLabel8;

		private MMLabel mmLabel10;

		private ToolStripButton toolStripButtonAttach;

		private MMLabel mmLabel11;

		private MMTextBox textBoxBankName;

		private BankFacilityStatusComboBox comboBoxStatus;

		private DataGridList dataGridFacility;

		private Panel panleEndDate;

		private MMSDateTimePicker dateTimeEndDate;

		private MMLabel mmLabel12;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private ToolStripMenuItem newContactToolStripMenuItem;

		private ToolStripMenuItem deleteContactToolStripMenuItem;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel13;

		private MMTextBox textBoxAlias;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6011;

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

		public BankFacilityGroupDetailsForm()
		{
			InitializeComponent();
			dataGridContacts.DropDownMenu.Items.Add(new ToolStripSeparator());
			checked
			{
				int num;
				for (num = 0; num < contextMenuStripContact.Items.Count; num++)
				{
					dataGridContacts.DropDownMenu.Items.Add(contextMenuStripContact.Items[num]);
					num--;
				}
				openContactToolStripMenuItem.Click += openContactToolStripMenuItem_Click;
				newContactToolStripMenuItem.Click += newContactToolStripMenuItem_Click;
				deleteContactToolStripMenuItem.Click += deleteContactToolStripMenuItem_Click;
				AddEvents();
			}
		}

		private void AddEvents()
		{
			base.Load += BankFacilityGroupDetailsForm_Load;
			comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
			dataGridContacts.AfterCellUpdate += dataGridContacts_AfterCellUpdate;
			dataGridContacts.BeforeCellUpdate += dataGridContacts_BeforeCellUpdate;
			dataGridContacts.CellDataError += dataGridContacts_CellDataError;
			dataGridFacility.OpenMenuClicked += dataGridFacility_OpenMenuClicked;
			dataGridFacility.DoubleClick += dataGridFacility_DoubleClick;
		}

		private void dataGridContacts_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridContacts.ActiveCell.Column.Key.ToString() == "ContactID")
			{
				e.RaiseErrorEvent = false;
				gridComboBoxContact.Text = dataGridContacts.ActiveCell.Text;
				gridComboBoxContact.QuickAddItem();
			}
		}

		private void dataGridContacts_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "ContactID")
			{
				DataRow dataRow = Factory.ContactSystem.GetContactByID(e.Cell.Value.ToString()).Tables[0].Rows[0];
				dataGridContacts.ActiveRow.Cells["FirstName"].Value = dataRow["FirstName"].ToString();
				dataGridContacts.ActiveRow.Cells["LastName"].Value = dataRow["LastName"].ToString();
				dataGridContacts.ActiveRow.Cells["JobTitle"].Value = dataRow["JobTitle"].ToString();
				dataGridContacts.ActiveRow.Cells["Note"].Value = dataRow["Note"].ToString();
			}
		}

		private void dataGridContacts_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "ContactID" && dataGridContacts.ExistCellValue("ContactID", e.NewValue.ToString()) >= 0)
			{
				ErrorHelper.InformationMessage("This contact is already added to list.", "Please select another contact.");
				e.Cancel = true;
			}
		}

		private void dataGridFacility_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridFacility_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void OpenForEdit()
		{
			string text = dataGridFacility.ActiveRow.Cells["FacilityID"].Value.ToString();
			FormActivator.BringFormToFront(FormActivator.BankFacilityFormObj);
			if (text != "")
			{
				FormActivator.BankFacilityFormObj.LoadData(text);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new BankFacilityGroupData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BankFacilityGroupTable.Rows[0] : currentData.BankFacilityGroupTable.NewRow();
				dataRow.BeginEdit();
				dataRow["GroupID"] = textBoxCode.Text.Trim();
				dataRow["GroupName"] = textBoxName.Text.Trim();
				dataRow["TotalLimit"] = ((textBoxTotalAmount.Text.Trim() != string.Empty) ? decimal.Parse(textBoxTotalAmount.Text.Trim()) : 0m);
				if (dateTimeStartDate.Checked)
				{
					dataRow["StartDate"] = dateTimeStartDate.Value;
				}
				else
				{
					dataRow["StartDate"] = DBNull.Value;
				}
				if (dateTimeEndDate.Checked && comboBoxStatus.SelectedID == 2)
				{
					dataRow["EndDate"] = dateTimeEndDate.Value;
				}
				else
				{
					dataRow["EndDate"] = DBNull.Value;
				}
				if (dateTimeRenewDate.Checked)
				{
					dataRow["RenewDate"] = dateTimeRenewDate.Value;
				}
				else
				{
					dataRow["RenewDate"] = DBNull.Value;
				}
				if (dateTimeExpiryDate.Checked)
				{
					dataRow["ExpiryDate"] = dateTimeExpiryDate.Value;
				}
				else
				{
					dataRow["ExpiryDate"] = DBNull.Value;
				}
				if (comboBoxBank.SelectedID != "")
				{
					dataRow["BankID"] = comboBoxBank.SelectedID;
				}
				else
				{
					dataRow["BankID"] = DBNull.Value;
				}
				if (comboBoxStatus.SelectedID != -1)
				{
					dataRow["Status"] = comboBoxStatus.SelectedID;
				}
				else
				{
					dataRow["Status"] = DBNull.Value;
				}
				dataRow["Alias"] = textBoxAlias.Text.Trim();
				dataRow["ContactName"] = textBoxContactName.Text.Trim();
				dataRow["ContactNumber"] = textBoxContactNumber.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.BankFacilityGroupTable.Rows.Add(dataRow);
				}
				currentData.BankFacilityGroupContactTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridContacts.Rows)
				{
					dataRow = currentData.BankFacilityGroupContactTable.NewRow();
					dataRow.BeginEdit();
					if (!(row.Cells["ContactID"].Value.ToString() == ""))
					{
						dataRow["GroupID"] = textBoxCode.Text;
						dataRow["ContactID"] = row.Cells["ContactID"].Value.ToString();
						dataRow["Note"] = row.Cells["Note"].Value.ToString();
						dataRow["RowIndex"] = row.Index;
						dataRow["JobTitle"] = row.Cells["JobTitle"].Value.ToString();
						dataRow.EndEdit();
						currentData.BankFacilityGroupContactTable.Rows.Add(dataRow);
					}
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
					currentData = Factory.BankFacilityGroupSystem.GetBankFacilityGroupByID(id.Trim());
					facilityData = Factory.BankFacilitySystem.GetBankFacilityListByID(id);
					dataGridFacility.DataSource = facilityData;
					if (dataGridFacility.Rows.Count > 0)
					{
						AdjustGridColumnSettings();
					}
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
						if (comboBoxStatus.SelectedID == 2)
						{
							panleEndDate.Visible = true;
						}
						else
						{
							panleEndDate.Visible = false;
						}
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
				textBoxCode.Text = dataRow["GroupID"].ToString();
				textBoxName.Text = dataRow["GroupName"].ToString();
				textBoxTotalAmount.Text = dataRow["TotalLimit"].ToString();
				textBoxAlias.Text = dataRow["Alias"].ToString();
				textBoxContactName.Text = dataRow["ContactName"].ToString();
				textBoxContactNumber.Text = dataRow["ContactNumber"].ToString();
				if (dataRow["StartDate"] != DBNull.Value)
				{
					dateTimeStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
					dateTimeStartDate.Checked = true;
				}
				else
				{
					dateTimeStartDate.IsNull = true;
					dateTimeStartDate.Checked = false;
				}
				if (dataRow["EndDate"] != DBNull.Value)
				{
					dateTimeEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
					dateTimeEndDate.Checked = true;
				}
				else
				{
					dateTimeEndDate.IsNull = true;
					dateTimeEndDate.Checked = false;
				}
				if (dataRow["RenewDate"] != DBNull.Value)
				{
					dateTimeRenewDate.Value = DateTime.Parse(dataRow["RenewDate"].ToString());
					dateTimeRenewDate.Checked = true;
				}
				else
				{
					dateTimeRenewDate.IsNull = true;
					dateTimeRenewDate.Checked = false;
				}
				if (dataRow["ExpiryDate"] != DBNull.Value)
				{
					dateTimeExpiryDate.Value = DateTime.Parse(dataRow["ExpiryDate"].ToString());
					dateTimeExpiryDate.Checked = true;
				}
				else
				{
					dateTimeExpiryDate.IsNull = true;
					dateTimeExpiryDate.Checked = false;
				}
				if (dataRow["BankID"] != DBNull.Value)
				{
					comboBoxBank.SelectedID = dataRow["BankID"].ToString();
				}
				else
				{
					comboBoxBank.Clear();
				}
				if (dataRow["Status"] != DBNull.Value)
				{
					comboBoxStatus.SelectedID = int.Parse(dataRow["Status"].ToString());
				}
				else
				{
					comboBoxStatus.Clear();
				}
				textBoxNote.Text = dataRow["Note"].ToString();
				if (currentData.Tables.Contains("Bank_Facility_Group_Contacts") || currentData.BankFacilityGroupContactTable.Rows.Count != 0)
				{
					DataTable dataTable = dataGridContacts.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.Tables["Bank_Facility_Group_Contacts"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						foreach (DataColumn column in dataTable.Columns)
						{
							if (dataRow3.Table.Columns.Contains(column.ColumnName))
							{
								dataRow3[column.ColumnName] = row[column.ColumnName];
							}
							else
							{
								ErrorHelper.ErrorMessage(column.ColumnName + " Does not exist.");
							}
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.BankFacilityGroupSystem.CreateBankFacilityGroup(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.BankFacilityGroup, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.BankFacilityGroupSystem.UpdateBankFacilityGroup(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Bank_Facility_Group", "GroupID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Bank_Facility_Group", "GroupID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Bank_Facility_Group", "GroupID");
			textBoxName.Clear();
			textBoxContactName.Clear();
			textBoxContactNumber.Clear();
			textBoxBankName.Clear();
			textBoxTotalAmount.Clear();
			dateTimeStartDate.Clear();
			dateTimeEndDate.Clear();
			dateTimeRenewDate.Clear();
			dateTimeExpiryDate.Clear();
			comboBoxStatus.LoadData();
			comboBoxStatus.SelectedID = 0;
			comboBoxBank.Clear();
			textBoxAlias.Clear();
			textBoxNote.Clear();
			(dataGridContacts.DataSource as DataTable).Rows.Clear();
			dataGridFacility.DataSource = null;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void BankFacilityGroupGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void BankFacilityGroupGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.BankFacilityGroupSystem.DeleteBankFacilityGroup(textBoxCode.Text);
			}
			catch (SqlException ex)
			{
				if (ex.Number == 547)
				{
					ErrorHelper.ErrorMessage("Record in use and could not be deleted!");
				}
				return false;
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
			LoadData(DatabaseHelper.GetNextID("Bank_Facility_Group", "GroupID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Bank_Facility_Group", "GroupID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Bank_Facility_Group", "GroupID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Bank_Facility_Group", "GroupID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Bank_Facility_Group", "GroupID", toolStripTextBoxFind.Text.Trim()))
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

		private void BankFacilityGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					dataGridContacts.SetupUI();
					SetupContactsGrid();
					dataGridFacility.LoadLayout();
					dataGridFacility.ApplyUIDesign();
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridContacts.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void AdjustGridColumnSettings()
		{
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityID"].Header.Caption = "Facility ID";
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityName"].Header.Caption = "Facility Name";
			dataGridFacility.DisplayLayout.Bands[0].Columns["StartDate"].Header.Caption = "Start Date";
			dataGridFacility.DisplayLayout.Bands[0].Columns["EndDate"].Header.Caption = "End Date";
			dataGridFacility.DisplayLayout.Bands[0].Columns["LimitAmount"].Header.Caption = "Limit";
			dataGridFacility.DisplayLayout.Bands[0].Columns["LimitAmount"].Format = Format.TotalAmountFormat;
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add(0, "LC");
			valueList.ValueListItems.Add(1, "TR");
			valueList.ValueListItems.Add(2, "Cheque Discount");
			valueList.ValueListItems.Add(3, "Invoice Discount");
			valueList.ValueListItems.Add(4, "Over Draft");
			valueList.ValueListItems.Add(5, "Term Loan");
			valueList.ValueListItems.Add(6, "Fixed Loan");
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityType"].ValueList = valueList;
			dataGridFacility.DisplayLayout.Bands[0].Columns["FacilityType"].Header.Caption = "Type";
			ValueList valueList2 = new ValueList();
			valueList2.ValueListItems.Add(0, "Active");
			valueList2.ValueListItems.Add(1, "Hold");
			valueList2.ValueListItems.Add(2, "Closed");
			dataGridFacility.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList2;
			UltraGridColumn ultraGridColumn = dataGridFacility.DisplayLayout.Bands[0].Columns["PayableAccountID"];
			bool hidden = dataGridFacility.DisplayLayout.Bands[0].Columns["CurrentAccountID"].Hidden = true;
			ultraGridColumn.Hidden = hidden;
			if (dataGridFacility.DisplayLayout.Bands[0].Columns.Exists("LimitAmount"))
			{
				dataGridFacility.DisplayLayout.Bands[0].Columns["LimitAmount"].CellAppearance.TextHAlign = HAlign.Right;
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

		private void SetupContactsGrid()
		{
			dataGridContacts.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ContactID").Unique = true;
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("JobTitle");
			dataTable.Columns.Add("Note");
			dataGridContacts.DataSource = dataTable;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].MaxLength = 30;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].MaxLength = 64;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].MaxLength = 255;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].Header.Caption = Translator.Translators.GetText("Job Title");
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Header.Caption = Translator.Translators.GetText("Contact Code");
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].Header.Caption = Translator.Translators.GetText("First Name");
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].Header.Caption = Translator.Translators.GetText("Last Name");
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].ValueList = gridComboBoxContact;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].CellActivation = Activation.AllowEdit;
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.BankFacilityGroup);
		}

		private void amountTextBox1_TextChanged(object sender, EventArgs e)
		{
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
					docManagementForm.EntityTypeLabel = "Group";
					docManagementForm.EntityType = EntityTypesEnum.BankFacilityGroups;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void linkLabelBank_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBank(comboBoxBank.SelectedID);
		}

		private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxStatus.SelectedID == 2)
			{
				panleEndDate.Visible = true;
			}
			else
			{
				panleEndDate.Visible = false;
			}
		}

		private void openContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string id = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				new FormHelper().EditContact(id);
			}
		}

		private void newContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			new FormHelper().EditContact(empty);
		}

		private void deleteContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string iD = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				Factory.ContactSystem.DeleteContact(iD);
			}
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.BankFacilityGroupDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxAlias = new Micromind.UISupport.MMTextBox();
			panleEndDate = new System.Windows.Forms.Panel();
			dateTimeEndDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel12 = new Micromind.UISupport.MMLabel();
			comboBoxStatus = new Micromind.DataControls.BankFacilityStatusComboBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxContactNumber = new Micromind.UISupport.MMTextBox();
			textBoxContactName = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimeExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxCode = new Micromind.UISupport.MMTextBox();
			dateTimeRenewDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			dateTimeStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			linkLabelBank = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxTotalAmount = new Micromind.UISupport.AmountTextBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridFacility = new Micromind.UISupport.DataGridList(components);
			mmLabel11 = new Micromind.UISupport.MMLabel();
			tabPageContacts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			dataGridContacts = new Micromind.DataControls.DataEntryGrid();
			gridComboBoxContact = new Micromind.DataControls.ContactsComboBox();
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			contextMenuStripContact = new System.Windows.Forms.ContextMenuStrip(components);
			openContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			tabPageGeneral.SuspendLayout();
			panleEndDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridFacility).BeginInit();
			tabPageContacts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStripContact.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(mmLabel13);
			tabPageGeneral.Controls.Add(textBoxAlias);
			tabPageGeneral.Controls.Add(panleEndDate);
			tabPageGeneral.Controls.Add(comboBoxStatus);
			tabPageGeneral.Controls.Add(mmLabel10);
			tabPageGeneral.Controls.Add(textBoxContactNumber);
			tabPageGeneral.Controls.Add(textBoxContactName);
			tabPageGeneral.Controls.Add(mmLabel9);
			tabPageGeneral.Controls.Add(mmLabel8);
			tabPageGeneral.Controls.Add(labelCode);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(mmLabel4);
			tabPageGeneral.Controls.Add(dateTimeExpiryDate);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(dateTimeRenewDate);
			tabPageGeneral.Controls.Add(textBoxBankName);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(textBoxNote);
			tabPageGeneral.Controls.Add(dateTimeStartDate);
			tabPageGeneral.Controls.Add(comboBoxBank);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(linkLabelBank);
			tabPageGeneral.Controls.Add(mmLabel2);
			tabPageGeneral.Controls.Add(textBoxTotalAmount);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(726, 359);
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(6, 59);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(32, 13);
			mmLabel13.TabIndex = 90;
			mmLabel13.Text = "Alias:";
			textBoxAlias.BackColor = System.Drawing.Color.White;
			textBoxAlias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAlias.CustomReportFieldName = "";
			textBoxAlias.CustomReportKey = "";
			textBoxAlias.CustomReportValueType = 1;
			textBoxAlias.IsComboTextBox = false;
			textBoxAlias.IsModified = false;
			textBoxAlias.Location = new System.Drawing.Point(134, 56);
			textBoxAlias.MaxLength = 64;
			textBoxAlias.Name = "textBoxAlias";
			textBoxAlias.Size = new System.Drawing.Size(511, 21);
			textBoxAlias.TabIndex = 4;
			panleEndDate.Controls.Add(dateTimeEndDate);
			panleEndDate.Controls.Add(mmLabel12);
			panleEndDate.Location = new System.Drawing.Point(460, 3);
			panleEndDate.Name = "panleEndDate";
			panleEndDate.Size = new System.Drawing.Size(185, 29);
			panleEndDate.TabIndex = 2;
			panleEndDate.Visible = false;
			dateTimeEndDate.Checked = false;
			dateTimeEndDate.CustomFormat = " ";
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeEndDate.Location = new System.Drawing.Point(57, 7);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.ShowCheckBox = true;
			dateTimeEndDate.Size = new System.Drawing.Size(127, 21);
			dateTimeEndDate.TabIndex = 2;
			dateTimeEndDate.Value = new System.DateTime(0L);
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(3, 10);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(55, 13);
			mmLabel12.TabIndex = 60;
			mmLabel12.Text = "End Date:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(359, 9);
			comboBoxStatus.MaxLength = 2;
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(95, 21);
			comboBoxStatus.TabIndex = 1;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(319, 13);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(42, 13);
			mmLabel10.TabIndex = 55;
			mmLabel10.Text = "Status:";
			textBoxContactNumber.BackColor = System.Drawing.Color.White;
			textBoxContactNumber.CustomReportFieldName = "";
			textBoxContactNumber.CustomReportKey = "";
			textBoxContactNumber.CustomReportValueType = 1;
			textBoxContactNumber.IsComboTextBox = false;
			textBoxContactNumber.IsModified = false;
			textBoxContactNumber.Location = new System.Drawing.Point(384, 152);
			textBoxContactNumber.MaxLength = 30;
			textBoxContactNumber.Name = "textBoxContactNumber";
			textBoxContactNumber.Size = new System.Drawing.Size(140, 21);
			textBoxContactNumber.TabIndex = 11;
			textBoxContactName.BackColor = System.Drawing.Color.White;
			textBoxContactName.CustomReportFieldName = "";
			textBoxContactName.CustomReportKey = "";
			textBoxContactName.CustomReportValueType = 1;
			textBoxContactName.IsComboTextBox = false;
			textBoxContactName.IsModified = false;
			textBoxContactName.Location = new System.Drawing.Point(134, 152);
			textBoxContactName.MaxLength = 64;
			textBoxContactName.Name = "textBoxContactName";
			textBoxContactName.Size = new System.Drawing.Size(143, 21);
			textBoxContactName.TabIndex = 10;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(283, 155);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(89, 13);
			mmLabel9.TabIndex = 53;
			mmLabel9.Text = "Contact Number:";
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(5, 155);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(79, 13);
			mmLabel8.TabIndex = 53;
			mmLabel8.Text = "Contact Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(6, 11);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(122, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Facility Group Code:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(6, 33);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(125, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Facility Group Name:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(5, 175);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			dateTimeExpiryDate.Checked = false;
			dateTimeExpiryDate.CustomFormat = " ";
			dateTimeExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeExpiryDate.Location = new System.Drawing.Point(574, 104);
			dateTimeExpiryDate.Name = "dateTimeExpiryDate";
			dateTimeExpiryDate.ShowCheckBox = true;
			dateTimeExpiryDate.Size = new System.Drawing.Size(140, 21);
			dateTimeExpiryDate.TabIndex = 8;
			dateTimeExpiryDate.Value = new System.DateTime(0L);
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(134, 10);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 21);
			textBoxCode.TabIndex = 0;
			dateTimeRenewDate.Checked = false;
			dateTimeRenewDate.CustomFormat = " ";
			dateTimeRenewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRenewDate.Location = new System.Drawing.Point(359, 104);
			dateTimeRenewDate.Name = "dateTimeRenewDate";
			dateTimeRenewDate.ShowCheckBox = true;
			dateTimeRenewDate.Size = new System.Drawing.Size(136, 21);
			dateTimeRenewDate.TabIndex = 7;
			dateTimeRenewDate.Value = new System.DateTime(0L);
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.Location = new System.Drawing.Point(279, 80);
			textBoxBankName.MaxLength = 64;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.ReadOnly = true;
			textBoxBankName.Size = new System.Drawing.Size(366, 21);
			textBoxBankName.TabIndex = 4;
			textBoxBankName.TabStop = false;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(134, 32);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(512, 21);
			textBoxName.TabIndex = 3;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(501, 107);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(67, 13);
			mmLabel6.TabIndex = 50;
			mmLabel6.Text = "Expiry Date:";
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(283, 107);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(70, 13);
			mmLabel5.TabIndex = 50;
			mmLabel5.Text = "Renew Date:";
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(134, 175);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(580, 167);
			textBoxNote.TabIndex = 12;
			dateTimeStartDate.Checked = false;
			dateTimeStartDate.CustomFormat = " ";
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeStartDate.Location = new System.Drawing.Point(134, 104);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.ShowCheckBox = true;
			dateTimeStartDate.Size = new System.Drawing.Size(144, 21);
			dateTimeStartDate.TabIndex = 6;
			dateTimeStartDate.Value = new System.DateTime(0L);
			comboBoxBank.Assigned = false;
			comboBoxBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = textBoxBankName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(134, 80);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.MaxLength = 15;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(143, 21);
			comboBoxBank.TabIndex = 5;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(6, 107);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(61, 13);
			mmLabel3.TabIndex = 50;
			mmLabel3.Text = "Start Date:";
			appearance13.FontData.BoldAsString = "True";
			linkLabelBank.Appearance = appearance13;
			linkLabelBank.AutoSize = true;
			linkLabelBank.Location = new System.Drawing.Point(8, 82);
			linkLabelBank.Name = "linkLabelBank";
			linkLabelBank.Size = new System.Drawing.Size(36, 15);
			linkLabelBank.TabIndex = 46;
			linkLabelBank.TabStop = true;
			linkLabelBank.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelBank.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelBank.Value = "Bank:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			linkLabelBank.VisitedLinkAppearance = appearance14;
			linkLabelBank.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelBank_LinkClicked);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(3, 131);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(108, 13);
			mmLabel2.TabIndex = 48;
			mmLabel2.Text = "Total Facility Amount:";
			textBoxTotalAmount.AllowDecimal = true;
			textBoxTotalAmount.CustomReportFieldName = "";
			textBoxTotalAmount.CustomReportKey = "";
			textBoxTotalAmount.CustomReportValueType = 1;
			textBoxTotalAmount.IsComboTextBox = false;
			textBoxTotalAmount.IsModified = false;
			textBoxTotalAmount.Location = new System.Drawing.Point(134, 128);
			textBoxTotalAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalAmount.Name = "textBoxTotalAmount";
			textBoxTotalAmount.NullText = "0";
			textBoxTotalAmount.Size = new System.Drawing.Size(144, 21);
			textBoxTotalAmount.TabIndex = 9;
			textBoxTotalAmount.Text = "0.00";
			textBoxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTotalAmount.TextChanged += new System.EventHandler(amountTextBox1_TextChanged);
			tabPageDetails.Controls.Add(dataGridFacility);
			tabPageDetails.Controls.Add(mmLabel11);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(726, 359);
			dataGridFacility.AllowUnfittedView = false;
			dataGridFacility.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridFacility.DisplayLayout.Appearance = appearance15;
			dataGridFacility.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridFacility.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			dataGridFacility.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridFacility.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			dataGridFacility.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridFacility.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			dataGridFacility.DisplayLayout.MaxColScrollRegions = 1;
			dataGridFacility.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridFacility.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridFacility.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			dataGridFacility.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridFacility.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			dataGridFacility.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridFacility.DisplayLayout.Override.CellAppearance = appearance22;
			dataGridFacility.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridFacility.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			dataGridFacility.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			dataGridFacility.DisplayLayout.Override.HeaderAppearance = appearance24;
			dataGridFacility.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridFacility.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			dataGridFacility.DisplayLayout.Override.RowAppearance = appearance25;
			dataGridFacility.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridFacility.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			dataGridFacility.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridFacility.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridFacility.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridFacility.LoadLayoutFailed = false;
			dataGridFacility.Location = new System.Drawing.Point(10, 29);
			dataGridFacility.Name = "dataGridFacility";
			dataGridFacility.ShowDeleteMenu = false;
			dataGridFacility.ShowMinusInRed = true;
			dataGridFacility.ShowNewMenu = false;
			dataGridFacility.Size = new System.Drawing.Size(704, 332);
			dataGridFacility.TabIndex = 357;
			dataGridFacility.Text = "dataGridList1";
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(10, 13);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(79, 13);
			mmLabel11.TabIndex = 51;
			mmLabel11.Text = "Facility Details:";
			tabPageContacts.Controls.Add(mmLabel37);
			tabPageContacts.Controls.Add(dataGridContacts);
			tabPageContacts.Controls.Add(gridComboBoxContact);
			tabPageContacts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageContacts.Name = "tabPageContacts";
			tabPageContacts.Size = new System.Drawing.Size(726, 359);
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(10, 14);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(158, 13);
			mmLabel37.TabIndex = 354;
			mmLabel37.Text = "Contacts related to this facility:";
			dataGridContacts.AllowAddNew = false;
			dataGridContacts.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridContacts.DisplayLayout.Appearance = appearance27;
			dataGridContacts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridContacts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			dataGridContacts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridContacts.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			dataGridContacts.DisplayLayout.MaxColScrollRegions = 1;
			dataGridContacts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridContacts.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridContacts.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			dataGridContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridContacts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridContacts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridContacts.DisplayLayout.Override.CellAppearance = appearance34;
			dataGridContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridContacts.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			dataGridContacts.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			dataGridContacts.DisplayLayout.Override.HeaderAppearance = appearance36;
			dataGridContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridContacts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			dataGridContacts.DisplayLayout.Override.RowAppearance = appearance37;
			dataGridContacts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridContacts.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			dataGridContacts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridContacts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridContacts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridContacts.IncludeLotItems = false;
			dataGridContacts.LoadLayoutFailed = false;
			dataGridContacts.Location = new System.Drawing.Point(10, 31);
			dataGridContacts.Name = "dataGridContacts";
			dataGridContacts.ShowClearMenu = true;
			dataGridContacts.ShowDeleteMenu = true;
			dataGridContacts.ShowInsertMenu = true;
			dataGridContacts.ShowMoveRowsMenu = true;
			dataGridContacts.Size = new System.Drawing.Size(704, 330);
			dataGridContacts.TabIndex = 0;
			dataGridContacts.Text = "dataEntryGrid1";
			gridComboBoxContact.Assigned = false;
			gridComboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			gridComboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			gridComboBoxContact.CustomReportFieldName = "";
			gridComboBoxContact.CustomReportKey = "";
			gridComboBoxContact.CustomReportValueType = 1;
			gridComboBoxContact.DescriptionTextBox = null;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			gridComboBoxContact.DisplayLayout.Appearance = appearance39;
			gridComboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			gridComboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			gridComboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			gridComboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			gridComboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			gridComboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			gridComboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			gridComboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			gridComboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			gridComboBoxContact.DisplayLayout.Override.CellAppearance = appearance46;
			gridComboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			gridComboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			gridComboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			gridComboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance48;
			gridComboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			gridComboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			gridComboBoxContact.DisplayLayout.Override.RowAppearance = appearance49;
			gridComboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			gridComboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			gridComboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			gridComboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			gridComboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			gridComboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			gridComboBoxContact.Editable = true;
			gridComboBoxContact.FilterString = "";
			gridComboBoxContact.HasAllAccount = false;
			gridComboBoxContact.HasCustom = false;
			gridComboBoxContact.IsDataLoaded = false;
			gridComboBoxContact.Location = new System.Drawing.Point(287, 221);
			gridComboBoxContact.MaxDropDownItems = 12;
			gridComboBoxContact.Name = "gridComboBoxContact";
			gridComboBoxContact.ShowInactiveItems = false;
			gridComboBoxContact.ShowQuickAdd = true;
			gridComboBoxContact.Size = new System.Drawing.Size(127, 21);
			gridComboBoxContact.TabIndex = 356;
			gridComboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			gridComboBoxContact.Visible = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
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
				toolStripButtonAttach,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(730, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 428);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(730, 40);
			panelButtons.TabIndex = 999;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(730, 1);
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
			buttonDelete.TabIndex = 14;
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
			xpButton1.Location = new System.Drawing.Point(620, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 15;
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
			buttonNew.TabIndex = 13;
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
			buttonSave.TabIndex = 12;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageContacts);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 46);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(730, 382);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance51.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance51;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = tabPageContacts;
			ultraTab3.Text = "Con&tacts";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(726, 359);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(730, 15);
			panel1.TabIndex = 315;
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
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 46);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			contextMenuStripContact.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				openContactToolStripMenuItem,
				newContactToolStripMenuItem,
				deleteContactToolStripMenuItem
			});
			contextMenuStripContact.Name = "contextMenuStripContact";
			contextMenuStripContact.Size = new System.Drawing.Size(153, 70);
			openContactToolStripMenuItem.Name = "openContactToolStripMenuItem";
			openContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			openContactToolStripMenuItem.Text = "Open Contact";
			newContactToolStripMenuItem.Name = "newContactToolStripMenuItem";
			newContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			newContactToolStripMenuItem.Text = "New Contact";
			deleteContactToolStripMenuItem.Name = "deleteContactToolStripMenuItem";
			deleteContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			deleteContactToolStripMenuItem.Text = "Delete Contact";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(730, 468);
			base.Controls.Add(formManager);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "BankFacilityGroupDetailsForm";
			Text = "Bank Facility Group";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			panleEndDate.ResumeLayout(false);
			panleEndDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridFacility).EndInit();
			tabPageContacts.ResumeLayout(false);
			tabPageContacts.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridContacts).EndInit();
			((System.ComponentModel.ISupportInitialize)gridComboBoxContact).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStripContact.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
