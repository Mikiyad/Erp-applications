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
	public class EmployeeDocumentsForm : Form, IForm
	{
		private EmployeeDocumentData currentData;

		private const string TABLENAME_CONST = "Employee";

		private const string IDFIELD_CONST = "EmployeeID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool isLoadingNewEmployee;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

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

		private EmployeeComboBox comboBoxEmployees;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel2;

		private NonDirtyPanel nonDirtyPanel1;

		private DataEntryGrid dataGrid;

		private EmployeeDocTypeComboBox comboBoxDocumentType;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel LabelEmployee;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5013;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return true;
			}
			set
			{
				isNewRecord = true;
				if (!screenRight.New || !screenRight.Edit)
				{
					buttonSave.Enabled = false;
				}
			}
		}

		public event EventHandler EmployeeDocumentChanged;

		public EmployeeDocumentsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeDocumentsForm_Load;
			dataGrid.CellDataError += dataGrid_CellDataError;
			dataGrid.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGrid.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGrid.ActiveRow;
			if (activeRow.Cells["Document Type"].Value.ToString() == "" && activeRow.Cells["Document Number"].Value.ToString() != "")
			{
				ErrorHelper.InformationMessage("Please select a document type.");
				e.Cancel = true;
				activeRow.Cells["Document Type"].Activate();
			}
			else if (activeRow.Cells["Document Number"].Value.ToString() == "" && activeRow.Cells["Document Type"].Value.ToString() != "")
			{
				ErrorHelper.InformationMessage("Please select a document number.");
				e.Cancel = true;
				activeRow.Cells["Document Number"].Activate();
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Document Number")
			{
				foreach (UltraGridRow row in dataGrid.Rows)
				{
					if (row.Cells[e.Cell.Column.Index].Value.ToString() == e.NewValue.ToString())
					{
						ErrorHelper.InformationMessage("A document with the same number is already added.");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGrid.ActiveCell.Column.Key.ToString() == "Document Type")
			{
				e.RaiseErrorEvent = false;
				comboBoxDocumentType.Text = dataGrid.ActiveCell.Text;
				comboBoxDocumentType.QuickAddItem();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeDocumentData();
				}
				foreach (UltraGridRow row in dataGrid.Rows)
				{
					DataRow dataRow = currentData.EmployeeDocumentTable.NewRow();
					dataRow.BeginEdit();
					if (!(row.Cells["Document Number"].Value.ToString() == ""))
					{
						string text = (string)(dataRow["EmployeeID"] = ((!isLoadingNewEmployee) ? comboBoxEmployees.SelectedID : comboBoxEmployees.OldValue));
						dataRow["DocumentTypeID"] = row.Cells["Document Type"].Value.ToString();
						dataRow["DocumentNumber"] = row.Cells["Document Number"].Value.ToString();
						if (row.Cells["Expiry Date"].Value.ToString() == "")
						{
							dataRow["ExpiryDate"] = DBNull.Value;
						}
						else
						{
							dataRow["ExpiryDate"] = DateTime.Parse(row.Cells["Expiry Date"].Value.ToString());
						}
						if (row.Cells["Issue Date"].Value.ToString() == "")
						{
							dataRow["IssueDate"] = DBNull.Value;
						}
						else
						{
							dataRow["IssueDate"] = DateTime.Parse(row.Cells["Issue Date"].Value.ToString());
						}
						dataRow["IssuePlace"] = row.Cells["Issue Place"].Value.ToString();
						dataRow["Remarks"] = row.Cells["Remarks"].Value.ToString();
						dataRow["RowIndex"] = row.Index;
						dataRow.EndEdit();
						currentData.EmployeeDocumentTable.Rows.Add(dataRow);
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

		private void SetupGrid()
		{
			try
			{
				dataGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Document Type");
				dataTable.Columns.Add("Document Number").Unique = true;
				dataTable.Columns.Add("Issue Date");
				dataTable.Columns.Add("Issue Place");
				dataTable.Columns.Add("Expiry Date");
				dataTable.Columns.Add("Remarks");
				dataGrid.DataSource = dataTable;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Number"].MaxLength = 30;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Number"].CharacterCasing = CharacterCasing.Upper;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].CharacterCasing = CharacterCasing.Upper;
				dataGrid.DisplayLayout.Bands[0].Columns["Issue Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGrid.DisplayLayout.Bands[0].Columns["Expiry Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGrid.DisplayLayout.Bands[0].Columns["Issue Place"].MaxLength = 15;
				dataGrid.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 255;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].ValueList = comboBoxDocumentType;
			}
			catch (Exception e)
			{
				dataGrid.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGrid.Focus();
		}

		public void LoadData(string employeeID)
		{
			try
			{
				if (!(employeeID.Trim() == ""))
				{
					if (comboBoxEmployees.Text != employeeID)
					{
						comboBoxEmployees.SelectedID = employeeID;
					}
					if (CanClose())
					{
						currentData = Factory.EmployeeDocumentSystem.GetEmployeeDocumentsByEmployeeID(employeeID);
						if (currentData != null)
						{
							FillData();
							IsNewRecord = false;
							formManager.ResetDirty();
						}
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
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0 && currentData.Tables.Contains("Employee_Document") && currentData.EmployeeDocumentTable.Rows.Count != 0)
			{
				DataTable dataTable = dataGrid.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables["Employee_Document"].Rows)
				{
					dataTable.Rows.Add(row["DocumentTypeID"].ToString(), row["DocumentNumber"].ToString(), row["IssueDate"].ToString(), row["IssuePlace"].ToString(), row["ExpiryDate"].ToString(), row["Remarks"].ToString());
				}
				dataTable.AcceptChanges();
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
					flag = Factory.EmployeeDocumentSystem.CreateEmployeeDocument(currentData);
				}
				else
				{
					flag = Factory.EmployeeDocumentSystem.UpdateEmployeeDocument(currentData);
					if (this.EmployeeDocumentChanged != null)
					{
						this.EmployeeDocumentChanged(currentData, null);
					}
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
			if (comboBoxEmployees.SelectedID == "")
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
			(dataGrid.DataSource as DataTable).Rows.Clear();
			if (!isLoadingNewEmployee)
			{
				comboBoxEmployees.Clear();
				textBoxEmployeeName.Clear();
			}
			formManager.ResetDirty();
		}

		private void EmployeeDocumentGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void EmployeeDocumentGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.EmployeeDocumentSystem.DeleteEmployeeDocument(comboBoxEmployees.SelectedID);
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
			string nextID = DatabaseHelper.GetNextID("Employee", "EmployeeID", comboBoxEmployees.SelectedID);
			if (!(nextID == ""))
			{
				comboBoxEmployees.SelectedID = nextID;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee", "EmployeeID", comboBoxEmployees.SelectedID);
			if (!(previousID == ""))
			{
				comboBoxEmployees.SelectedID = previousID;
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee", "EmployeeID");
			if (!(lastID == ""))
			{
				comboBoxEmployees.SelectedID = lastID;
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee", "EmployeeID");
			if (!(firstID == ""))
			{
				comboBoxEmployees.SelectedID = firstID;
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
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee", "EmployeeID", toolStripTextBoxFind.Text.Trim()))
				{
					string selectedID = toolStripTextBoxFind.Text.Trim();
					comboBoxEmployees.SelectedID = selectedID;
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

		private void EmployeeDocumentsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDocumentType.LoadData();
				dataGrid.SetupUI();
				SetupGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					ClearForm();
					dataGrid.Enabled = false;
				}
			}
			catch (Exception e2)
			{
				dataGrid.LoadLayoutFailed = true;
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

		private void comboBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxEmployees.SelectedRow != null && comboBoxEmployees.SelectedID != "")
			{
				dataGrid.Enabled = true;
				textBoxEmployeeName.Text = comboBoxEmployees.SelectedRow.Cells["Name"].Text.ToString();
				if (comboBoxEmployees.Text == "")
				{
					isLoadingNewEmployee = false;
					return;
				}
				if (isLoadingNewEmployee)
				{
					return;
				}
				isLoadingNewEmployee = true;
				if (CanClose())
				{
					ClearForm();
					LoadData(comboBoxEmployees.SelectedID);
				}
				else
				{
					comboBoxEmployees.SelectedID = comboBoxEmployees.OldValue;
				}
			}
			else
			{
				dataGrid.Enabled = false;
				textBoxEmployeeName.Clear();
				ClearForm();
			}
			isLoadingNewEmployee = false;
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void LabelEmployee_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployees.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeDocumentsForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			comboBoxEmployees = new Micromind.DataControls.EmployeeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			nonDirtyPanel1 = new Micromind.UISupport.NonDirtyPanel(components);
			LabelEmployee = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dataGrid = new Micromind.DataControls.DataEntryGrid();
			comboBoxDocumentType = new Micromind.DataControls.EmployeeDocTypeComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployees).BeginInit();
			nonDirtyPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDocumentType).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[10]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(683, 31);
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
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 438);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(683, 40);
			panelButtons.TabIndex = 11;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(683, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(573, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
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
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 31);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(87, 13);
			mmLabel2.TabIndex = 2;
			mmLabel2.Text = "Employee Name:";
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.Enabled = false;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(135, 29);
			textBoxEmployeeName.MaxLength = 255;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(229, 20);
			textBoxEmployeeName.TabIndex = 3;
			comboBoxEmployees.Assigned = false;
			comboBoxEmployees.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployees.CustomReportFieldName = "";
			comboBoxEmployees.CustomReportKey = "";
			comboBoxEmployees.CustomReportValueType = 1;
			comboBoxEmployees.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployees.DisplayLayout.Appearance = appearance;
			comboBoxEmployees.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployees.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployees.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxEmployees.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployees.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxEmployees.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployees.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployees.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployees.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxEmployees.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployees.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployees.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxEmployees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployees.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxEmployees.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxEmployees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployees.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployees.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxEmployees.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployees.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxEmployees.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployees.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployees.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployees.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployees.Editable = true;
			comboBoxEmployees.FilterString = "";
			comboBoxEmployees.HasAllAccount = false;
			comboBoxEmployees.HasCustom = false;
			comboBoxEmployees.IsDataLoaded = false;
			comboBoxEmployees.Location = new System.Drawing.Point(135, 7);
			comboBoxEmployees.MaxDropDownItems = 12;
			comboBoxEmployees.Name = "comboBoxEmployees";
			comboBoxEmployees.ShowInactiveItems = false;
			comboBoxEmployees.ShowQuickAdd = true;
			comboBoxEmployees.ShowTerminatedEmployees = true;
			comboBoxEmployees.Size = new System.Drawing.Size(229, 20);
			comboBoxEmployees.TabIndex = 1;
			comboBoxEmployees.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployees.SelectedIndexChanged += new System.EventHandler(comboBoxEmployees_SelectedIndexChanged);
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
			nonDirtyPanel1.Controls.Add(LabelEmployee);
			nonDirtyPanel1.Controls.Add(textBoxEmployeeName);
			nonDirtyPanel1.Controls.Add(mmLabel2);
			nonDirtyPanel1.Controls.Add(comboBoxEmployees);
			nonDirtyPanel1.Location = new System.Drawing.Point(0, 33);
			nonDirtyPanel1.Name = "nonDirtyPanel1";
			nonDirtyPanel1.Size = new System.Drawing.Size(536, 67);
			nonDirtyPanel1.TabIndex = 0;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			LabelEmployee.Appearance = appearance13;
			LabelEmployee.AutoSize = true;
			LabelEmployee.Location = new System.Drawing.Point(12, 9);
			LabelEmployee.Name = "LabelEmployee";
			LabelEmployee.Size = new System.Drawing.Size(93, 15);
			LabelEmployee.TabIndex = 163;
			LabelEmployee.TabStop = true;
			LabelEmployee.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			LabelEmployee.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			LabelEmployee.Value = "Employee Code:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			LabelEmployee.VisitedLinkAppearance = appearance14;
			LabelEmployee.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(LabelEmployee_LinkClicked);
			dataGrid.AllowAddNew = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance15;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			dataGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance22;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance24;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance25;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.IncludeLotItems = false;
			dataGrid.LoadLayoutFailed = false;
			dataGrid.Location = new System.Drawing.Point(12, 106);
			dataGrid.Name = "dataGrid";
			dataGrid.ShowClearMenu = true;
			dataGrid.ShowDeleteMenu = true;
			dataGrid.ShowInsertMenu = true;
			dataGrid.ShowMoveRowsMenu = true;
			dataGrid.Size = new System.Drawing.Size(659, 326);
			dataGrid.TabIndex = 17;
			dataGrid.Text = "dataEntryGrid1";
			comboBoxDocumentType.Assigned = false;
			comboBoxDocumentType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDocumentType.CustomReportFieldName = "";
			comboBoxDocumentType.CustomReportKey = "";
			comboBoxDocumentType.CustomReportValueType = 1;
			comboBoxDocumentType.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDocumentType.DisplayLayout.Appearance = appearance27;
			comboBoxDocumentType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDocumentType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocumentType.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocumentType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxDocumentType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocumentType.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxDocumentType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDocumentType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDocumentType.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDocumentType.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxDocumentType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDocumentType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDocumentType.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDocumentType.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxDocumentType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDocumentType.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocumentType.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxDocumentType.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxDocumentType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDocumentType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxDocumentType.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxDocumentType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDocumentType.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxDocumentType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDocumentType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDocumentType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDocumentType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDocumentType.Editable = true;
			comboBoxDocumentType.FilterString = "";
			comboBoxDocumentType.HasAllAccount = false;
			comboBoxDocumentType.HasCustom = false;
			comboBoxDocumentType.IsDataLoaded = false;
			comboBoxDocumentType.Location = new System.Drawing.Point(616, 65);
			comboBoxDocumentType.MaxDropDownItems = 12;
			comboBoxDocumentType.Name = "comboBoxDocumentType";
			comboBoxDocumentType.ShowInactiveItems = false;
			comboBoxDocumentType.ShowQuickAdd = true;
			comboBoxDocumentType.Size = new System.Drawing.Size(125, 20);
			comboBoxDocumentType.TabIndex = 18;
			comboBoxDocumentType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDocumentType.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(683, 478);
			base.Controls.Add(comboBoxDocumentType);
			base.Controls.Add(dataGrid);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(nonDirtyPanel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "EmployeeDocumentsForm";
			Text = "Employee Documents";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployees).EndInit();
			nonDirtyPanel1.ResumeLayout(false);
			nonDirtyPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDocumentType).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
