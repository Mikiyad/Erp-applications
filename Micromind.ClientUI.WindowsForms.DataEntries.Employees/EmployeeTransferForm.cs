using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
	public class EmployeeTransferForm : Form, IForm
	{
		private EmployeeActivityData currentData;

		private const string TABLENAME_CONST = "Employee_Transfer";

		private const string IDFIELD_CONST = "ActivityID";

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

		private MMTextBox textBoxFromLocation;

		private MMTextBox textBoxFromDivision;

		private MMLabel mmLabel4;

		private MMTextBox textBoxFromDepartment;

		private FormManager formManager;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMLabel mmLabel7;

		private DepartmentComboBox comboBoxToDepartment;

		private DivisionComboBox comboBoxToDivision;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel1;

		private MMLabel mmLabel8;

		private MMTextBox textBoxRequestedBy;

		private MMLabel mmLabel9;

		private MMTextBox textBoxReference;

		private MMLabel mmLabel10;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxCode;

		private MMLabel label1TransactionID;

		private MMTextBox textBoxFromDepartmentID;

		private MMTextBox textBoxFromDivisionID;

		private MMTextBox textBoxFromLocationID;

		private MMLabel mmLabel12;

		private MMSDateTimePicker dateTimePickerDate;

		private PositionComboBox comboBoxToPosition;

		private MMTextBox textBoxFromPositionName;

		private MMLabel mmLabel13;

		private MMLabel mmLabel11;

		private MMTextBox textBoxFromPositionID;

		private UltraFormattedLinkLabel LabelEmployee;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private WorkLocationComboBox comboBoxToLocation;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5040;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
					textBoxCode.Clear();
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
				}
				comboBoxEmployee.Enabled = isNewRecord;
				MMLabel mMLabel = label1TransactionID;
				bool visible = textBoxCode.Visible = !isNewRecord;
				mMLabel.Visible = visible;
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

		public EmployeeTransferForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeTransferForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.Transfer);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["ActivityType"] = (byte)5;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["Reason"] = textBoxReason.Text;
				dataRow["Reference"] = textBoxReference.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeTransferTable.Rows[0] : currentData.EmployeeTransferTable.NewRow());
				if (textBoxFromLocationID.Text != "")
				{
					dataRow["TransferFromLocation"] = textBoxFromLocationID.Text;
				}
				else
				{
					dataRow["TransferFromLocation"] = DBNull.Value;
				}
				if (textBoxFromDivisionID.Text != "")
				{
					dataRow["TransferFromDivision"] = textBoxFromDivisionID.Text;
				}
				else
				{
					dataRow["TransferFromDivision"] = DBNull.Value;
				}
				if (textBoxFromDepartmentID.Text != "")
				{
					dataRow["TransferFromDep"] = textBoxFromDepartmentID.Text;
				}
				else
				{
					dataRow["TransferFromDep"] = DBNull.Value;
				}
				if (comboBoxToLocation.SelectedID != "")
				{
					dataRow["TransferToLocation"] = comboBoxToLocation.SelectedID;
				}
				else
				{
					dataRow["TransferToLocation"] = DBNull.Value;
				}
				if (comboBoxToDivision.SelectedID != "")
				{
					dataRow["TransferToDivision"] = comboBoxToDivision.SelectedID;
				}
				else
				{
					dataRow["TransferToDivision"] = DBNull.Value;
				}
				if (comboBoxToDepartment.SelectedID != "")
				{
					dataRow["TransferToDep"] = comboBoxToDepartment.SelectedID;
				}
				else
				{
					dataRow["TransferToDep"] = DBNull.Value;
				}
				if (textBoxFromPositionID.Text != "")
				{
					dataRow["FromPosition"] = textBoxFromPositionID.Text;
				}
				else
				{
					dataRow["FromPosition"] = DBNull.Value;
				}
				if (comboBoxToPosition.SelectedID != "")
				{
					dataRow["ToPosition"] = comboBoxToPosition.SelectedID;
				}
				else
				{
					dataRow["ToPosition"] = DBNull.Value;
				}
				dataRow["RequestedBy"] = textBoxRequestedBy.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeTransferTable.Rows.Add(dataRow);
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

		public void LoadData(int id)
		{
			try
			{
				if (CanClose())
				{
					currentData = Factory.EmployeeActivitySystem.GetEmployeeActivityByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id.ToString();
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
				DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
				textBoxCode.Text = dataRow["ActivityID"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxReason.Text = dataRow["Reason"].ToString();
				textBoxReference.Text = dataRow["Reference"].ToString();
				dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				dataRow = currentData.Tables["Employee_Transfer"].Rows[0];
				textBoxFromDivisionID.Text = dataRow["DivisionName"].ToString();
				textBoxFromDepartmentID.Text = dataRow["TransferFromDep"].ToString();
				textBoxFromDivisionID.Text = dataRow["TransferFromDivision"].ToString();
				textBoxFromLocationID.Text = dataRow["TransferFromLocation"].ToString();
				comboBoxToLocation.Text = dataRow["TransferToLocation"].ToString();
				comboBoxToDivision.Text = dataRow["TransferToDivision"].ToString();
				comboBoxToDepartment.Text = dataRow["TransferToDep"].ToString();
				textBoxFromPositionID.Text = dataRow["FromPosition"].ToString();
				textBoxFromPositionName.Text = dataRow["PositionName"].ToString();
				textBoxRequestedBy.Text = dataRow["RequestedBy"].ToString();
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
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Transfer, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Transfer, isUpdate: false);
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
			if (comboBoxEmployee.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
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
			comboBoxEmployee.Clear();
			textBoxEmployeeName.Clear();
			textBoxFromDepartment.Clear();
			textBoxFromDivision.Clear();
			textBoxFromLocation.Clear();
			textBoxNote.Clear();
			textBoxReason.Clear();
			dateTimePickerDate.Value = DateTime.Today;
			textBoxReference.Clear();
			textBoxRequestedBy.Clear();
			comboBoxToDepartment.Clear();
			comboBoxToDivision.Clear();
			comboBoxToLocation.Clear();
			textBoxFromPositionID.Clear();
			textBoxFromPositionName.Clear();
			formManager.ResetDirty();
			comboBoxEmployee.Focus();
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private bool Delete()
		{
			try
			{
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
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_Transfer", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_Transfer", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_Transfer", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_Transfer", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
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

		private void EmployeeTransferForm_Load(object sender, EventArgs e)
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

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				if (comboBoxEmployee.SelectedID == "")
				{
					textBoxFromLocation.Text = "";
					textBoxFromDivision.Text = "";
					textBoxFromDepartment.Text = "";
					textBoxFromPositionName.Clear();
				}
				else
				{
					DataSet employeeBriefInfo = Factory.EmployeeSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
					if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
						textBoxFromLocation.Text = dataRow["WorkLocationName"].ToString();
						textBoxFromDivision.Text = dataRow["DivisionName"].ToString();
						textBoxFromDepartment.Text = dataRow["DepartmentName"].ToString();
						textBoxFromLocationID.Text = dataRow["WorkLocationID"].ToString();
						textBoxFromDivisionID.Text = dataRow["DivisionID"].ToString();
						textBoxFromDepartmentID.Text = dataRow["DepartmentID"].ToString();
						textBoxFromPositionID.Text = dataRow["PositionID"].ToString();
						textBoxFromPositionName.Text = dataRow["PositionName"].ToString();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LabelEmployee_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditWorkLocation(comboBoxToLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDivision(comboBoxToDivision.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeTransferForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			comboBoxToDepartment = new Micromind.DataControls.DepartmentComboBox();
			comboBoxToDivision = new Micromind.DataControls.DivisionComboBox();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxRequestedBy = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxReference = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			label1TransactionID = new Micromind.UISupport.MMLabel();
			textBoxFromDepartmentID = new Micromind.UISupport.MMTextBox();
			textBoxFromDivisionID = new Micromind.UISupport.MMTextBox();
			textBoxFromLocationID = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxToPosition = new Micromind.DataControls.PositionComboBox();
			textBoxFromPositionName = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxFromPositionID = new Micromind.UISupport.MMTextBox();
			LabelEmployee = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxToLocation = new Micromind.DataControls.WorkLocationComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDepartment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(563, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 346);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(563, 40);
			panelButtons.TabIndex = 10;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(563, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 4;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(453, 8);
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
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
			textBoxFromLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocation.CustomReportFieldName = "";
			textBoxFromLocation.CustomReportKey = "";
			textBoxFromLocation.CustomReportValueType = 1;
			textBoxFromLocation.IsComboTextBox = false;
			textBoxFromLocation.Location = new System.Drawing.Point(112, 99);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(163, 20);
			textBoxFromLocation.TabIndex = 1;
			textBoxFromLocation.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.Location = new System.Drawing.Point(112, 121);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(163, 20);
			textBoxFromDivision.TabIndex = 4;
			textBoxFromDivision.TabStop = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(11, 102);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(77, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "From Location:";
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(112, 143);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(163, 20);
			textBoxFromDepartment.TabIndex = 5;
			textBoxFromDepartment.TabStop = false;
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
			comboBoxEmployee.Location = new System.Drawing.Point(112, 33);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(177, 20);
			comboBoxEmployee.TabIndex = 0;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(comboBoxEmployee_SelectedIndexChanged);
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(112, 55);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(358, 20);
			textBoxEmployeeName.TabIndex = 1;
			textBoxEmployeeName.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(11, 125);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(73, 13);
			mmLabel3.TabIndex = 9;
			mmLabel3.Text = "From Division:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(11, 146);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(91, 13);
			mmLabel5.TabIndex = 9;
			mmLabel5.Text = "From Department:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = true;
			mmLabel7.Location = new System.Drawing.Point(282, 146);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(95, 13);
			mmLabel7.TabIndex = 0;
			mmLabel7.Text = "To Department:";
			comboBoxToDepartment.Assigned = false;
			comboBoxToDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToDepartment.CustomReportFieldName = "";
			comboBoxToDepartment.CustomReportKey = "";
			comboBoxToDepartment.CustomReportValueType = 1;
			comboBoxToDepartment.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToDepartment.DisplayLayout.Appearance = appearance13;
			comboBoxToDepartment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToDepartment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDepartment.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDepartment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToDepartment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDepartment.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToDepartment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToDepartment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToDepartment.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToDepartment.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToDepartment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToDepartment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToDepartment.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToDepartment.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToDepartment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToDepartment.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDepartment.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToDepartment.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToDepartment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToDepartment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToDepartment.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToDepartment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToDepartment.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToDepartment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToDepartment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToDepartment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToDepartment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToDepartment.Editable = true;
			comboBoxToDepartment.FilterString = "";
			comboBoxToDepartment.HasAllAccount = false;
			comboBoxToDepartment.HasCustom = false;
			comboBoxToDepartment.IsDataLoaded = false;
			comboBoxToDepartment.Location = new System.Drawing.Point(383, 143);
			comboBoxToDepartment.MaxDropDownItems = 12;
			comboBoxToDepartment.Name = "comboBoxToDepartment";
			comboBoxToDepartment.ShowInactiveItems = false;
			comboBoxToDepartment.ShowQuickAdd = true;
			comboBoxToDepartment.Size = new System.Drawing.Size(163, 20);
			comboBoxToDepartment.TabIndex = 4;
			comboBoxToDepartment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToDivision.Assigned = false;
			comboBoxToDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToDivision.CustomReportFieldName = "";
			comboBoxToDivision.CustomReportKey = "";
			comboBoxToDivision.CustomReportValueType = 1;
			comboBoxToDivision.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToDivision.DisplayLayout.Appearance = appearance25;
			comboBoxToDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToDivision.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToDivision.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToDivision.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToDivision.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToDivision.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToDivision.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToDivision.Editable = true;
			comboBoxToDivision.FilterString = "";
			comboBoxToDivision.HasAllAccount = false;
			comboBoxToDivision.HasCustom = false;
			comboBoxToDivision.IsDataLoaded = false;
			comboBoxToDivision.Location = new System.Drawing.Point(383, 121);
			comboBoxToDivision.MaxDropDownItems = 12;
			comboBoxToDivision.Name = "comboBoxToDivision";
			comboBoxToDivision.ShowInactiveItems = false;
			comboBoxToDivision.ShowQuickAdd = true;
			comboBoxToDivision.Size = new System.Drawing.Size(163, 20);
			comboBoxToDivision.TabIndex = 3;
			comboBoxToDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.Location = new System.Drawing.Point(112, 187);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(434, 20);
			textBoxReason.TabIndex = 6;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(11, 190);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(47, 13);
			mmLabel1.TabIndex = 9;
			mmLabel1.Text = "Reason:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(11, 212);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(77, 13);
			mmLabel8.TabIndex = 9;
			mmLabel8.Text = "Requested By:";
			textBoxRequestedBy.BackColor = System.Drawing.Color.White;
			textBoxRequestedBy.CustomReportFieldName = "";
			textBoxRequestedBy.CustomReportKey = "";
			textBoxRequestedBy.CustomReportValueType = 1;
			textBoxRequestedBy.IsComboTextBox = false;
			textBoxRequestedBy.Location = new System.Drawing.Point(112, 209);
			textBoxRequestedBy.MaxLength = 30;
			textBoxRequestedBy.Name = "textBoxRequestedBy";
			textBoxRequestedBy.Size = new System.Drawing.Size(164, 20);
			textBoxRequestedBy.TabIndex = 7;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(282, 212);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(60, 13);
			mmLabel9.TabIndex = 9;
			mmLabel9.Text = "Reference:";
			textBoxReference.BackColor = System.Drawing.Color.White;
			textBoxReference.CustomReportFieldName = "";
			textBoxReference.CustomReportKey = "";
			textBoxReference.CustomReportValueType = 1;
			textBoxReference.IsComboTextBox = false;
			textBoxReference.Location = new System.Drawing.Point(384, 209);
			textBoxReference.MaxLength = 15;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(162, 20);
			textBoxReference.TabIndex = 8;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(12, 234);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(30, 13);
			mmLabel10.TabIndex = 9;
			mmLabel10.Text = "Note";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(112, 231);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(434, 55);
			textBoxNote.TabIndex = 9;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(383, 33);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(87, 20);
			textBoxCode.TabIndex = 1;
			textBoxCode.TabStop = false;
			textBoxCode.Visible = false;
			label1TransactionID.AutoSize = true;
			label1TransactionID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1TransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			label1TransactionID.IsFieldHeader = false;
			label1TransactionID.IsRequired = false;
			label1TransactionID.Location = new System.Drawing.Point(297, 35);
			label1TransactionID.Name = "label1TransactionID";
			label1TransactionID.PenWidth = 1f;
			label1TransactionID.ShowBorder = false;
			label1TransactionID.Size = new System.Drawing.Size(80, 13);
			label1TransactionID.TabIndex = 9;
			label1TransactionID.Text = "Transaction ID:";
			label1TransactionID.Visible = false;
			textBoxFromDepartmentID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartmentID.CustomReportFieldName = "";
			textBoxFromDepartmentID.CustomReportKey = "";
			textBoxFromDepartmentID.CustomReportValueType = 1;
			textBoxFromDepartmentID.IsComboTextBox = false;
			textBoxFromDepartmentID.Location = new System.Drawing.Point(64, 275);
			textBoxFromDepartmentID.MaxLength = 255;
			textBoxFromDepartmentID.Name = "textBoxFromDepartmentID";
			textBoxFromDepartmentID.ReadOnly = true;
			textBoxFromDepartmentID.Size = new System.Drawing.Size(38, 20);
			textBoxFromDepartmentID.TabIndex = 23;
			textBoxFromDepartmentID.Visible = false;
			textBoxFromDivisionID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivisionID.CustomReportFieldName = "";
			textBoxFromDivisionID.CustomReportKey = "";
			textBoxFromDivisionID.CustomReportValueType = 1;
			textBoxFromDivisionID.IsComboTextBox = false;
			textBoxFromDivisionID.Location = new System.Drawing.Point(64, 253);
			textBoxFromDivisionID.MaxLength = 64;
			textBoxFromDivisionID.Name = "textBoxFromDivisionID";
			textBoxFromDivisionID.ReadOnly = true;
			textBoxFromDivisionID.Size = new System.Drawing.Size(38, 20);
			textBoxFromDivisionID.TabIndex = 22;
			textBoxFromDivisionID.Visible = false;
			textBoxFromLocationID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocationID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocationID.CustomReportFieldName = "";
			textBoxFromLocationID.CustomReportKey = "";
			textBoxFromLocationID.CustomReportValueType = 1;
			textBoxFromLocationID.IsComboTextBox = false;
			textBoxFromLocationID.Location = new System.Drawing.Point(64, 231);
			textBoxFromLocationID.MaxLength = 15;
			textBoxFromLocationID.Name = "textBoxFromLocationID";
			textBoxFromLocationID.ReadOnly = true;
			textBoxFromLocationID.Size = new System.Drawing.Size(38, 20);
			textBoxFromLocationID.TabIndex = 21;
			textBoxFromLocationID.Visible = false;
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = true;
			mmLabel12.Location = new System.Drawing.Point(12, 81);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(38, 13);
			mmLabel12.TabIndex = 0;
			mmLabel12.Text = "Date:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(112, 77);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(111, 20);
			dateTimePickerDate.TabIndex = 1;
			dateTimePickerDate.Value = new System.DateTime(2016, 2, 17, 12, 12, 30, 816);
			comboBoxToPosition.Assigned = false;
			comboBoxToPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToPosition.CustomReportFieldName = "";
			comboBoxToPosition.CustomReportKey = "";
			comboBoxToPosition.CustomReportValueType = 1;
			comboBoxToPosition.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToPosition.DisplayLayout.Appearance = appearance37;
			comboBoxToPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPosition.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxToPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxToPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToPosition.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToPosition.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxToPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToPosition.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToPosition.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxToPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToPosition.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPosition.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxToPosition.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxToPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxToPosition.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxToPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxToPosition.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToPosition.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToPosition.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToPosition.Editable = true;
			comboBoxToPosition.FilterString = "";
			comboBoxToPosition.HasAllAccount = false;
			comboBoxToPosition.HasCustom = false;
			comboBoxToPosition.IsDataLoaded = false;
			comboBoxToPosition.Location = new System.Drawing.Point(383, 165);
			comboBoxToPosition.MaxDropDownItems = 12;
			comboBoxToPosition.Name = "comboBoxToPosition";
			comboBoxToPosition.ShowInactiveItems = false;
			comboBoxToPosition.ShowQuickAdd = true;
			comboBoxToPosition.Size = new System.Drawing.Size(163, 20);
			comboBoxToPosition.TabIndex = 5;
			comboBoxToPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxFromPositionName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromPositionName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromPositionName.CustomReportFieldName = "";
			textBoxFromPositionName.CustomReportKey = "";
			textBoxFromPositionName.CustomReportValueType = 1;
			textBoxFromPositionName.IsComboTextBox = false;
			textBoxFromPositionName.Location = new System.Drawing.Point(112, 165);
			textBoxFromPositionName.MaxLength = 15;
			textBoxFromPositionName.Name = "textBoxFromPositionName";
			textBoxFromPositionName.ReadOnly = true;
			textBoxFromPositionName.Size = new System.Drawing.Size(163, 20);
			textBoxFromPositionName.TabIndex = 25;
			textBoxFromPositionName.TabStop = false;
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(11, 168);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(73, 13);
			mmLabel13.TabIndex = 26;
			mmLabel13.Text = "From Position:";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = true;
			mmLabel11.Location = new System.Drawing.Point(282, 168);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(75, 13);
			mmLabel11.TabIndex = 24;
			mmLabel11.Text = "To Position:";
			textBoxFromPositionID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromPositionID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromPositionID.CustomReportFieldName = "";
			textBoxFromPositionID.CustomReportKey = "";
			textBoxFromPositionID.CustomReportValueType = 1;
			textBoxFromPositionID.IsComboTextBox = false;
			textBoxFromPositionID.Location = new System.Drawing.Point(64, 301);
			textBoxFromPositionID.MaxLength = 15;
			textBoxFromPositionID.Name = "textBoxFromPositionID";
			textBoxFromPositionID.ReadOnly = true;
			textBoxFromPositionID.Size = new System.Drawing.Size(38, 20);
			textBoxFromPositionID.TabIndex = 28;
			textBoxFromPositionID.Visible = false;
			appearance49.FontData.BoldAsString = "True";
			appearance49.FontData.Name = "Tahoma";
			LabelEmployee.Appearance = appearance49;
			LabelEmployee.AutoSize = true;
			LabelEmployee.Location = new System.Drawing.Point(12, 35);
			LabelEmployee.Name = "LabelEmployee";
			LabelEmployee.Size = new System.Drawing.Size(62, 15);
			LabelEmployee.TabIndex = 161;
			LabelEmployee.TabStop = true;
			LabelEmployee.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			LabelEmployee.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			LabelEmployee.Value = "Employee:";
			appearance50.ForeColor = System.Drawing.Color.Blue;
			LabelEmployee.VisitedLinkAppearance = appearance50;
			LabelEmployee.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(LabelEmployee_LinkClicked);
			appearance51.FontData.BoldAsString = "True";
			appearance51.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance51;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(285, 103);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(72, 15);
			ultraFormattedLinkLabel1.TabIndex = 162;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "To Location:";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance52;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance53.FontData.BoldAsString = "True";
			appearance53.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance53;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(285, 125);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(68, 15);
			ultraFormattedLinkLabel2.TabIndex = 163;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "To Division:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance54;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxToLocation.Assigned = false;
			comboBoxToLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLocation.CustomReportFieldName = "";
			comboBoxToLocation.CustomReportKey = "";
			comboBoxToLocation.CustomReportValueType = 1;
			comboBoxToLocation.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLocation.DisplayLayout.Appearance = appearance55;
			comboBoxToLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxToLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxToLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLocation.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLocation.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLocation.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxToLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLocation.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxToLocation.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxToLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLocation.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxToLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
			comboBoxToLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLocation.Editable = true;
			comboBoxToLocation.FilterString = "";
			comboBoxToLocation.HasAllAccount = false;
			comboBoxToLocation.HasCustom = false;
			comboBoxToLocation.IsDataLoaded = false;
			comboBoxToLocation.Location = new System.Drawing.Point(383, 99);
			comboBoxToLocation.MaxDropDownItems = 12;
			comboBoxToLocation.Name = "comboBoxToLocation";
			comboBoxToLocation.ShowAll = false;
			comboBoxToLocation.ShowConsignIn = false;
			comboBoxToLocation.ShowConsignOut = false;
			comboBoxToLocation.ShowInactiveItems = false;
			comboBoxToLocation.ShowNormalLocations = true;
			comboBoxToLocation.ShowPOSOnly = false;
			comboBoxToLocation.ShowQuickAdd = true;
			comboBoxToLocation.ShowWarehouseOnly = false;
			comboBoxToLocation.Size = new System.Drawing.Size(163, 20);
			comboBoxToLocation.TabIndex = 164;
			comboBoxToLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(563, 386);
			base.Controls.Add(comboBoxToLocation);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(LabelEmployee);
			base.Controls.Add(textBoxFromPositionID);
			base.Controls.Add(comboBoxToPosition);
			base.Controls.Add(textBoxFromPositionName);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(textBoxFromDepartmentID);
			base.Controls.Add(textBoxFromDivisionID);
			base.Controls.Add(textBoxFromLocationID);
			base.Controls.Add(comboBoxToDivision);
			base.Controls.Add(comboBoxToDepartment);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxReference);
			base.Controls.Add(textBoxRequestedBy);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxReason);
			base.Controls.Add(textBoxFromDepartment);
			base.Controls.Add(textBoxFromDivision);
			base.Controls.Add(label1TransactionID);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(textBoxFromLocation);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel12);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "EmployeeTransferForm";
			Text = "Employee Transfer";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDepartment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
