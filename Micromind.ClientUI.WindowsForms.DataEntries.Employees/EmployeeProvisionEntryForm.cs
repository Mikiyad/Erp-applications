using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
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
	public class EmployeeProvisionEntryForm : Form, IForm
	{
		private EmployeeProvisionData currentData;

		private const string TABLENAME_CONST = "Employee_Provision";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isLoadingNewEmployee;

		private bool isVoid;

		private IContainer components;

		private ToolStrip toolStrip1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

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

		private DataEntryGrid dataGridItems;

		private ToolStripButton toolStripButtonInformation;

		private Panel panelDetails;

		private EmployeeProvisionTypeComboBox comboBoxProvisionType;

		private TextBox textBoxProvisionName;

		private EmployeeComboBox employeeComboBox;

		private TextBox textBoxReference;

		private Label label1;

		private XPButton buttonLoad;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private Label label3;

		private TextBox textBoxNote;

		private TextBox textBoxVoucherNumber;

		private DateTimePicker dateTimePickerDate;

		private MMLabel LabelPeriod;

		private XPButton buttonNew;

		private XPButton buttonDelete;

		private XPButton buttonVoid;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonPrint;

		private Label labelVoided;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonDistribution;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5013;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
				}
				toolStripButtonPrint.Enabled = !value;
				toolStripButtonPreview.Enabled = !value;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonInformation.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				SysDocComboBox sysDocComboBox = comboBoxSysDoc;
				enabled = (textBoxVoucherNumber.Enabled = isNewRecord);
				sysDocComboBox.Enabled = enabled;
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

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid != value)
				{
					isVoid = value;
					panelDetails.Enabled = !value;
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public EmployeeProvisionEntryForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeProvisionEntryFormForm_Load;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			base.FormClosing += EmployeeProvisionEntryForm_FormClosing;
			comboBoxProvisionType.SelectedIndexChanged += ComboBoxProvisionType_SelectedIndexChanged;
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
			}
		}

		private void ComboBoxProvisionType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProvisionType.SelectedID != "")
			{
				ProvisionForEnum provisionFor = comboBoxProvisionType.ProvisionFor;
				if (provisionFor == ProvisionForEnum.Ticket || provisionFor == ProvisionForEnum.Leave)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Due Amount"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Posted"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Service Period"].Hidden = true;
				}
			}
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null)
				{
					if (comboBoxProvisionType.SelectedID == "")
					{
						ErrorHelper.WarningMessage("Please select provision type.");
					}
					else if (e.Cell.Column.Key == "EmployeeID" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "EmployeeID")
					{
						dataGridItems.ActiveRow.Cells["EmployeeName"].Value = employeeComboBox.SelectedName;
						string text = dataGridItems.ActiveRow.Cells["EmployeeID"].Value.ToString();
						if (text != "")
						{
							DataSet dataSet = new DataSet();
							switch (comboBoxProvisionType.ProvisionFor)
							{
							case ProvisionForEnum.EOS:
								dataSet = Factory.EmployeeProvisionSystem.GetEmployeeList(text, text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", showInactive: false, "", dateTimePickerDate.Value, textBoxVoucherNumber.Text);
								if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables["Employee"].Rows.Count != 0)
								{
									DataRow dataRow3 = dataSet.Tables[0].Rows[0];
									if (dataRow3["JoiningDate"] != DBNull.Value)
									{
										dataGridItems.ActiveRow.Cells["DOJ"].Value = dataRow3["JoiningDate"];
									}
									if (dataRow3["Service Period"] != DBNull.Value)
									{
										dataGridItems.ActiveRow.Cells["Service Period"].Value = dataRow3["Service Period"];
									}
									if (dataRow3["Graduity"] != DBNull.Value)
									{
										dataGridItems.ActiveRow.Cells["Due Amount"].Value = dataRow3["Graduity"];
									}
									if (dataRow3["TotalPostedAmount"] != DBNull.Value)
									{
										dataGridItems.ActiveRow.Cells["Posted"].Value = dataRow3["TotalPostedAmount"];
									}
								}
								break;
							case ProvisionForEnum.Ticket:
								dataSet = Factory.EmployeeProvisionSystem.GetEmployeeTicketDetails(text, text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", showInactive: false, "", dateTimePickerDate.Value, textBoxVoucherNumber.Text);
								if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables["Employee"].Rows.Count != 0)
								{
									DataRow dataRow2 = dataSet.Tables[0].Rows[0];
									if (dataRow2["JoiningDate"] != DBNull.Value)
									{
										dataGridItems.ActiveRow.Cells["DOJ"].Value = dataRow2["JoiningDate"];
									}
									dataGridItems.ActiveRow.Cells["Service Period"].Value = 0;
									dataGridItems.ActiveRow.Cells["Due Amount"].Value = 0;
									dataGridItems.ActiveRow.Cells["Posted"].Value = 0;
									decimal result2 = default(decimal);
									decimal result3 = default(decimal);
									int result4 = 0;
									decimal.TryParse(dataRow2["TicketAmount"].ToString(), out result2);
									decimal.TryParse(dataRow2["TicketPeriod"].ToString(), out result3);
									int.TryParse(dataRow2["NumberOfTickets"].ToString(), out result4);
									decimal num = Math.Truncate(result3 / 365m);
									decimal d2 = result2 / 365m;
									int value2 = DateTime.DaysInMonth(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month);
									if (num != 0m)
									{
										dataGridItems.ActiveRow.Cells["Current Amount"].Value = (d2 * (decimal)value2 / num).ToString(Format.GridAmountFormat);
									}
									else
									{
										dataGridItems.ActiveRow.Cells["Current Amount"].Value = 0;
									}
								}
								break;
							case ProvisionForEnum.Leave:
								dataSet = Factory.EmployeeProvisionSystem.GetEmployeeList(text, text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", showInactive: false, "", dateTimePickerDate.Value, textBoxVoucherNumber.Text);
								if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables["Employee"].Rows.Count != 0)
								{
									DataRow dataRow = dataSet.Tables[0].Rows[0];
									if (dataRow["JoiningDate"] != DBNull.Value)
									{
										dataGridItems.ActiveRow.Cells["DOJ"].Value = dataRow["JoiningDate"];
									}
									dataGridItems.ActiveRow.Cells["Service Period"].Value = 0;
									dataGridItems.ActiveRow.Cells["Due Amount"].Value = 0;
									dataGridItems.ActiveRow.Cells["Posted"].Value = 0;
									decimal result = default(decimal);
									decimal.TryParse(dataRow["GrossSalary"].ToString(), out result);
									decimal d = result / 365m;
									int value = DateTime.DaysInMonth(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month);
									dataGridItems.ActiveRow.Cells["Current Amount"].Value = (d * (decimal)value).ToString(Format.GridAmountFormat);
								}
								break;
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItems.Focus();
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				dataGridItems.DisplayLayout.Bands[0].Summaries.Clear();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("EmployeeID");
				dataTable.Columns.Add("EmployeeName");
				dataTable.Columns.Add("DOJ");
				dataTable.Columns.Add("Service Period");
				dataTable.Columns.Add("Due Amount", typeof(decimal));
				dataTable.Columns.Add("Posted", typeof(decimal));
				dataTable.Columns.Add("Current Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataTable = new DataTable("EmployeeProvisionDetail");
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["DOJ"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Service Period"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Amount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Posted"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].ValueList = employeeComboBox;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].Header.Caption = "Employee ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].Header.Caption = "Employee Name";
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Posted"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Service Period"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Total", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Current Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].DisplayFormat = Format.GridAmountFormat;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Employee_Provision", "VoucherID", textBoxVoucherNumber.Text);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee_Provision", "VoucherID", textBoxVoucherNumber.Text);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee_Provision", "VoucherID");
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee_Provision", "VoucherID");
			if (!(firstID == ""))
			{
				LoadData(firstID);
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
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Employee_Provision", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
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

		private void EmployeeProvisionEntryForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void EmployeeProvisionEntryFormForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.EmployeeProvision);
				dateTimePickerDate.Value = DateTime.Now;
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void ClearForm()
		{
			try
			{
				textBoxNote.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxReference.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxProvisionType.Clear();
				textBoxProvisionName.Clear();
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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

		private bool SaveData()
		{
			return SaveData(clearAfter: true);
		}

		private bool ValidateData()
		{
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxProvisionType.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one item row.");
				return false;
			}
			return true;
		}

		private bool SaveData(bool clearAfter)
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
				bool flag = Factory.EmployeeProvisionSystem.CreateEmployeeProvision(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else if (clearAfter)
				{
					ClearForm();
					IsNewRecord = true;
				}
				else
				{
					formManager.ResetDirty();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.EmployeeProvisionSystem.GetEmployeeProvisionByID(SystemDocID, voucherID);
					if (currentData != null)
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
				ClearForm();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeProvisionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeProvisionTable.Rows[0] : currentData.EmployeeProvisionTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["Note"] = textBoxNote.Text.Trim();
				dataRow["Reference"] = textBoxReference.Text.Trim();
				dataRow["ProvisionTypeID"] = comboBoxProvisionType.SelectedID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.EmployeeProvisionTable.Rows.Add(dataRow);
				}
				currentData.EmployeeProvisionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.EmployeeProvisionDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["EmployeeID"] = row.Cells["EmployeeID"].Value.ToString();
					dataRow2["ServicePeriod"] = row.Cells["Service Period"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					if (row.Cells["Posted"].Value != null && row.Cells["Posted"].Value.ToString() != "")
					{
						dataRow2["Posted"] = row.Cells["Posted"].Value.ToString();
					}
					else
					{
						dataRow2["Posted"] = "0.00";
					}
					if (row.Cells["Current Amount"].Value != null && row.Cells["Current Amount"].Value.ToString() != "")
					{
						dataRow2["CurrentAmount"] = row.Cells["Current Amount"].Value.ToString();
					}
					else
					{
						dataRow2["CurrentAmount"] = "0.00";
					}
					if (row.Cells["Due Amount"].Value != null && row.Cells["Due Amount"].Value.ToString() != "")
					{
						dataRow2["DueAmount"] = row.Cells["Due Amount"].Value.ToString();
					}
					else
					{
						dataRow2["DueAmount"] = "0.00";
					}
					dataRow2.EndEdit();
					currentData.EmployeeProvisionDetailTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables["Employee_Provision"].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Employee_Provision"].Rows[0];
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					dateTimePickerDate.Text = dataRow["TransactionDate"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxReference.Text = dataRow["Reference"].ToString();
					comboBoxProvisionType.SelectedID = dataRow["ProvisionTypeID"].ToString();
					if (currentData.Tables.Contains("Employee_Provision_Detail") && currentData.EmployeeProvisionDetailTable.Rows.Count != 0)
					{
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						dataTable.Rows.Clear();
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Employee_Provision_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["EmployeeID"] = row["EmployeeID"].ToString();
							dataRow3["EmployeeName"] = row["EmployeeName"].ToString();
							dataRow3["DOJ"] = row["JoiningDate"].ToString();
							dataRow3["Service Period"] = row["ServicePeriod"].ToString();
							dataRow3["Due Amount"] = row["DueAmount"].ToString();
							dataRow3["Posted"] = row["Posted"].ToString();
							dataRow3["Current Amount"] = row["CurrentAmount"].ToString();
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.EndUpdate();
					}
				}
			}
			catch
			{
				throw;
			}
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
				return Factory.EmployeeProvisionSystem.DeleteEmployeeProvision(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProvisionType(comboBoxProvisionType.SelectedID);
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private bool SaveDraft()
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.EmployeeProvision);
					}
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 76.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.JobTimesheet);
					FillData();
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					_ = textBoxVoucherNumber.Text;
					DataSet employeeAppraisalToPrint = Factory.EmployeeAppraisalSystem.GetEmployeeAppraisalToPrint(SystemDocID, textBoxVoucherNumber.Text);
					if (employeeAppraisalToPrint == null || employeeAppraisalToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeAppraisalToPrint, SystemDocID, "Employee Appraisal", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private string GetDocumentTitle()
		{
			return "Employee Provision";
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeProvisionListFormObj);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EmployeeProvision);
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Employee_Provision", "VoucherID");
		}

		private void FillDataEmployee(DataSet data)
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (data.Tables[0].Rows.Count != 0)
			{
				ProvisionForEnum provisionFor = comboBoxProvisionType.ProvisionFor;
				dataGridItems.BeginUpdate();
				foreach (DataRow row in data.Tables["Employee"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					if (row["EmployeeID"] != DBNull.Value)
					{
						dataRow2["EmployeeID"] = row["EmployeeID"];
					}
					if (row["EmployeeName"] != DBNull.Value)
					{
						dataRow2["EmployeeName"] = row["EmployeeName"];
					}
					if (row["JoiningDate"] != DBNull.Value)
					{
						dataRow2["DOJ"] = row["JoiningDate"];
					}
					switch (provisionFor)
					{
					case ProvisionForEnum.EOS:
						if (row["Service Period"] != DBNull.Value)
						{
							dataRow2["Service Period"] = row["Service Period"];
						}
						if (row["Graduity"] != DBNull.Value)
						{
							dataRow2["Due Amount"] = row["Graduity"];
						}
						if (row["TotalPostedAmount"] != DBNull.Value)
						{
							dataRow2["Posted"] = row["TotalPostedAmount"];
						}
						break;
					case ProvisionForEnum.Ticket:
					{
						dataRow2["Service Period"] = 0;
						dataRow2["Due Amount"] = 0;
						dataRow2["Posted"] = 0;
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						int result4 = 0;
						decimal.TryParse(row["TicketAmount"].ToString(), out result2);
						decimal.TryParse(row["TicketPeriod"].ToString(), out result3);
						int.TryParse(row["NumberOfTickets"].ToString(), out result4);
						decimal num = Math.Truncate(result3 / 365m);
						decimal d2 = result2 / 365m;
						int value2 = DateTime.DaysInMonth(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month);
						if (num != 0m)
						{
							dataRow2["Current Amount"] = (d2 * (decimal)value2 / num).ToString(Format.GridAmountFormat);
						}
						else
						{
							dataRow2["Current Amount"] = 0;
						}
						break;
					}
					case ProvisionForEnum.Leave:
					{
						dataRow2["Service Period"] = 0;
						dataRow2["Due Amount"] = 0;
						dataRow2["Posted"] = 0;
						decimal result = default(decimal);
						decimal.TryParse(row["GrossSalary"].ToString(), out result);
						decimal d = result / 365m;
						int value = DateTime.DaysInMonth(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month);
						dataRow2["Current Amount"] = (d * (decimal)value).ToString(Format.GridAmountFormat);
						break;
					}
					}
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
				dataGridItems.EndUpdate();
			}
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			if (comboBoxProvisionType.SelectedID == "")
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Please select provisin type.", "Warning.");
				return;
			}
			CalculateSalaryForm calculateSalaryForm = new CalculateSalaryForm();
			if (calculateSalaryForm.ShowDialog() == DialogResult.OK)
			{
				DataSet data = new DataSet();
				switch (comboBoxProvisionType.ProvisionFor)
				{
				case ProvisionForEnum.EOS:
				case ProvisionForEnum.Leave:
					data = Factory.EmployeeProvisionSystem.GetEmployeeList(calculateSalaryForm.FromEmployee, calculateSalaryForm.ToEmployee, calculateSalaryForm.FromDepartment, calculateSalaryForm.ToDepartment, calculateSalaryForm.FromLocation, calculateSalaryForm.ToLocation, calculateSalaryForm.FromType, calculateSalaryForm.ToType, calculateSalaryForm.FromDivision, calculateSalaryForm.ToDivision, calculateSalaryForm.FromSponsor, calculateSalaryForm.ToSponsor, calculateSalaryForm.FromGroup, calculateSalaryForm.ToGroup, calculateSalaryForm.FromGrade, calculateSalaryForm.ToGrade, calculateSalaryForm.FromPosition, calculateSalaryForm.ToPosition, calculateSalaryForm.FromBank, calculateSalaryForm.ToBank, calculateSalaryForm.FromAccount, calculateSalaryForm.ToAccount, showInactive: false, calculateSalaryForm.MultipleEmployees, dateTimePickerDate.Value, textBoxVoucherNumber.Text);
					break;
				case ProvisionForEnum.Ticket:
					data = Factory.EmployeeProvisionSystem.GetEmployeeTicketDetails(calculateSalaryForm.FromEmployee, calculateSalaryForm.ToEmployee, calculateSalaryForm.FromDepartment, calculateSalaryForm.ToDepartment, calculateSalaryForm.FromLocation, calculateSalaryForm.ToLocation, calculateSalaryForm.FromType, calculateSalaryForm.ToType, calculateSalaryForm.FromDivision, calculateSalaryForm.ToDivision, calculateSalaryForm.FromSponsor, calculateSalaryForm.ToSponsor, calculateSalaryForm.FromGroup, calculateSalaryForm.ToGroup, calculateSalaryForm.FromGrade, calculateSalaryForm.ToGrade, calculateSalaryForm.FromPosition, calculateSalaryForm.ToPosition, calculateSalaryForm.FromBank, calculateSalaryForm.ToBank, calculateSalaryForm.FromAccount, calculateSalaryForm.ToAccount, showInactive: false, calculateSalaryForm.MultipleEmployees, dateTimePickerDate.Value, textBoxVoucherNumber.Text);
					break;
				}
				FillDataEmployee(data);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeProvisionEntryForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxProvisionType = new Micromind.DataControls.EmployeeProvisionTypeComboBox();
			textBoxProvisionName = new System.Windows.Forms.TextBox();
			employeeComboBox = new Micromind.DataControls.EmployeeComboBox();
			textBoxReference = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonLoad = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label3 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			LabelPeriod = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProvisionType).BeginInit();
			((System.ComponentModel.ISupportInitialize)employeeComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(886, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
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
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 477);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(886, 40);
			panelButtons.TabIndex = 0;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(338, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 3;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(226, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(118, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(886, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(776, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 141);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(862, 330);
			dataGridItems.TabIndex = 17;
			dataGridItems.Text = "dataEntryGrid1";
			panelDetails.Controls.Add(comboBoxProvisionType);
			panelDetails.Controls.Add(employeeComboBox);
			panelDetails.Controls.Add(textBoxReference);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(textBoxProvisionName);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(LabelPeriod);
			panelDetails.Location = new System.Drawing.Point(4, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(768, 101);
			panelDetails.TabIndex = 18;
			comboBoxProvisionType.Assigned = false;
			comboBoxProvisionType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProvisionType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProvisionType.CustomReportFieldName = "";
			comboBoxProvisionType.CustomReportKey = "";
			comboBoxProvisionType.CustomReportValueType = 1;
			comboBoxProvisionType.DescriptionTextBox = textBoxProvisionName;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProvisionType.DisplayLayout.Appearance = appearance13;
			comboBoxProvisionType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProvisionType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProvisionType.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProvisionType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxProvisionType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProvisionType.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxProvisionType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProvisionType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProvisionType.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProvisionType.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxProvisionType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProvisionType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProvisionType.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProvisionType.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxProvisionType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProvisionType.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProvisionType.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxProvisionType.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxProvisionType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProvisionType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxProvisionType.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxProvisionType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProvisionType.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxProvisionType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProvisionType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProvisionType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProvisionType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProvisionType.Editable = true;
			comboBoxProvisionType.FilterString = "";
			comboBoxProvisionType.HasAllAccount = false;
			comboBoxProvisionType.HasCustom = false;
			comboBoxProvisionType.IsDataLoaded = false;
			comboBoxProvisionType.Location = new System.Drawing.Point(98, 25);
			comboBoxProvisionType.MaxDropDownItems = 12;
			comboBoxProvisionType.Name = "comboBoxProvisionType";
			comboBoxProvisionType.ShowInactiveItems = false;
			comboBoxProvisionType.ShowQuickAdd = true;
			comboBoxProvisionType.Size = new System.Drawing.Size(125, 20);
			comboBoxProvisionType.TabIndex = 3;
			comboBoxProvisionType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProvisionName.Location = new System.Drawing.Point(232, 25);
			textBoxProvisionName.MaxLength = 15;
			textBoxProvisionName.Name = "textBoxProvisionName";
			textBoxProvisionName.ReadOnly = true;
			textBoxProvisionName.Size = new System.Drawing.Size(240, 20);
			textBoxProvisionName.TabIndex = 4;
			employeeComboBox.Assigned = false;
			employeeComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			employeeComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			employeeComboBox.CustomReportFieldName = "";
			employeeComboBox.CustomReportKey = "";
			employeeComboBox.CustomReportValueType = 1;
			employeeComboBox.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			employeeComboBox.DisplayLayout.Appearance = appearance25;
			employeeComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			employeeComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			employeeComboBox.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			employeeComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			employeeComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			employeeComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			employeeComboBox.DisplayLayout.MaxColScrollRegions = 1;
			employeeComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			employeeComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			employeeComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			employeeComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			employeeComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			employeeComboBox.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			employeeComboBox.DisplayLayout.Override.CellAppearance = appearance32;
			employeeComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			employeeComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			employeeComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			employeeComboBox.DisplayLayout.Override.HeaderAppearance = appearance34;
			employeeComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			employeeComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			employeeComboBox.DisplayLayout.Override.RowAppearance = appearance35;
			employeeComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			employeeComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			employeeComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			employeeComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			employeeComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			employeeComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			employeeComboBox.Editable = true;
			employeeComboBox.FilterString = "";
			employeeComboBox.HasAllAccount = false;
			employeeComboBox.HasCustom = false;
			employeeComboBox.IsDataLoaded = false;
			employeeComboBox.Location = new System.Drawing.Point(748, 44);
			employeeComboBox.MaxDropDownItems = 12;
			employeeComboBox.Name = "employeeComboBox";
			employeeComboBox.ShowInactiveItems = false;
			employeeComboBox.ShowQuickAdd = true;
			employeeComboBox.ShowTerminatedEmployees = true;
			employeeComboBox.Size = new System.Drawing.Size(100, 20);
			employeeComboBox.TabIndex = 169;
			employeeComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			employeeComboBox.Visible = false;
			textBoxReference.Location = new System.Drawing.Point(98, 46);
			textBoxReference.MaxLength = 15;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(125, 20);
			textBoxReference.TabIndex = 5;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 167;
			label1.Text = "Reference:";
			buttonLoad.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonLoad.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonLoad.BackColor = System.Drawing.Color.Silver;
			buttonLoad.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonLoad.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonLoad.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonLoad.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonLoad.Location = new System.Drawing.Point(778, 105);
			buttonLoad.Name = "buttonLoad";
			buttonLoad.Size = new System.Drawing.Size(96, 24);
			buttonLoad.TabIndex = 7;
			buttonLoad.Text = "Load";
			buttonLoad.UseVisualStyleBackColor = false;
			buttonLoad.Click += new System.EventHandler(buttonLoad_Click);
			ultraFormattedLinkLabel1.AutoSize = true;
			appearance37.FontData.BoldAsString = "True";
			ultraFormattedLinkLabel1.LinkAppearance = appearance37;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 5);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(42, 14);
			ultraFormattedLinkLabel1.TabIndex = 166;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Doc ID:";
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance38;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance39;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance40;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance41.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance41.BackColor2 = System.Drawing.SystemColors.Control;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance41;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance42;
			appearance43.BackColor = System.Drawing.SystemColors.Highlight;
			appearance43.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance43;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance44;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			appearance45.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance45;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance46.BackColor = System.Drawing.SystemColors.Control;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance46;
			appearance47.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance47;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance48;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance49.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance49;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(98, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(125, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel5.AutoSize = true;
			appearance50.FontData.BoldAsString = "True";
			ultraFormattedLinkLabel5.LinkAppearance = appearance50;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 27);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(83, 14);
			ultraFormattedLinkLabel5.TabIndex = 164;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Provision Type:";
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			linkLabelVoucherNumber.AutoSize = true;
			appearance51.FontData.BoldAsString = "True";
			linkLabelVoucherNumber.LinkAppearance = appearance51;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(234, 6);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(71, 14);
			linkLabelVoucherNumber.TabIndex = 135;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 71);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			textBoxNote.Location = new System.Drawing.Point(98, 67);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(581, 28);
			textBoxNote.TabIndex = 6;
			textBoxVoucherNumber.Location = new System.Drawing.Point(313, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(159, 20);
			textBoxVoucherNumber.TabIndex = 1;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(540, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDate.TabIndex = 2;
			LabelPeriod.AutoSize = true;
			LabelPeriod.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			LabelPeriod.IsFieldHeader = false;
			LabelPeriod.IsRequired = true;
			LabelPeriod.Location = new System.Drawing.Point(494, 6);
			LabelPeriod.Name = "LabelPeriod";
			LabelPeriod.PenWidth = 1f;
			LabelPeriod.ShowBorder = false;
			LabelPeriod.Size = new System.Drawing.Size(42, 13);
			LabelPeriod.TabIndex = 2;
			LabelPeriod.Text = "Date :";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(20, 353);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(835, 83);
			labelVoided.TabIndex = 118;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(886, 517);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(formManager);
			base.Controls.Add(buttonLoad);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "EmployeeProvisionEntryForm";
			Text = "Employee Provision";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProvisionType).EndInit();
			((System.ComponentModel.ISupportInitialize)employeeComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
