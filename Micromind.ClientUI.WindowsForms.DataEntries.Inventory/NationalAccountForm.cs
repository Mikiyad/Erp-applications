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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class NationalAccountForm : Form, IForm
	{
		private bool canAccessCost = true;

		private CustomerData currentData;

		private const string TABLENAME_CONST = "Customer";

		private const string IDFIELD_CONST = "CustomerID";

		private const string PARENTIDFIELD_CONST = "ParentCustomerID";

		private bool isNewRecord = true;

		private bool isLoading;

		private string seekID;

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

		private MMLabel mmLabel1;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private OpenFileDialog openFileDialog1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private MMLabel mmLabel13;

		private DataEntryGrid dataEntryGrid;

		private customersFlatComboBox comboBoxCustomer;

		private TextBox textBoxName;

		private customersFlatComboBox comboBoxGridCustomer;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4010;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
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

		private string SeekID
		{
			get
			{
				return seekID;
			}
			set
			{
				seekID = value;
			}
		}

		public NationalAccountForm()
		{
			InitializeComponent();
			AddEvents();
			try
			{
				SetupGrid();
				dataEntryGrid.SetupUI();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void AddEvents()
		{
			base.Load += NationalAccountForm_Load;
			dataEntryGrid.AfterCellUpdate += dataEntryGridBOM_AfterCellUpdate;
			dataEntryGrid.CellDataError += dataEntryGridBOM_CellDataError;
			dataEntryGrid.BeforeRowDeactivate += dataEntryGridBOM_BeforeRowDeactivate;
			dataEntryGrid.BeforeCellUpdate += dataEntryGrid_BeforeCellUpdate;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadData(comboBoxCustomer.SelectedID);
		}

		private void dataEntryGridBOM_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (dataEntryGrid.ActiveRow != null && (dataEntryGrid.ActiveRow.Cells["ChildCustomerID"].Value == null || dataEntryGrid.ActiveRow.Cells["ChildCustomerID"].Value.ToString() == ""))
			{
				ErrorHelper.InformationMessage("Please select a child customer");
				e.Cancel = true;
				dataEntryGrid.EnterEditMode(dataEntryGrid.ActiveRow.Cells["ChildCustomerID"]);
			}
		}

		private void dataEntryGridBOM_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataEntryGrid.ActiveCell.Column.Key == "ChildCustomerID")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Customer does not exist. Please select a correct customer.");
			}
		}

		private void dataEntryGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "ChildCustomerID")
			{
				bool flag = false;
				foreach (UltraGridRow row in dataEntryGrid.Rows)
				{
					if (comboBoxGridCustomer.SelectedID == row.Cells["ChildCustomerID"].Value.ToString())
					{
						flag = true;
					}
				}
				if (flag)
				{
					ErrorHelper.InformationMessage("Customer already exist. Please select a new customer");
					e.Cancel = true;
					e.Cell.Activate();
				}
				else if (comboBoxGridCustomer.SelectedID == comboBoxCustomer.SelectedID)
				{
					ErrorHelper.InformationMessage("Customer can not be a child & parent at the same time! Please select another customer.");
					e.Cancel = true;
					e.Cell.Activate();
				}
				else if (comboBoxGridCustomer.HasParent)
				{
					ErrorHelper.InformationMessage("Selected Customer belongs to another parent.");
					e.Cancel = true;
					e.Cell.Activate();
				}
				else if (comboBoxGridCustomer.HasChildren)
				{
					ErrorHelper.InformationMessage("Selected Customer has child records.");
					e.Cancel = true;
					e.Cell.Activate();
				}
			}
		}

		private void dataEntryGridBOM_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (!(e.Cell.Column.Key == "ChildCustomerID"))
			{
				return;
			}
			if (comboBoxGridCustomer.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
			{
				comboBoxGridCustomer.SelectedID = e.Cell.Value.ToString();
			}
			else if (comboBoxGridCustomer.SelectedRow == null)
			{
				return;
			}
			if (dataEntryGrid.ActiveRow != null)
			{
				dataEntryGrid.ActiveRow.Cells["Name"].Value = comboBoxGridCustomer.SelectedName;
				if (comboBoxGridCustomer.Balance != null && comboBoxGridCustomer.Balance.ToString() != "")
				{
					dataEntryGrid.ActiveRow.Cells["CurrentBalance"].Value = decimal.Parse(comboBoxGridCustomer.Balance);
				}
				else
				{
					dataEntryGrid.ActiveRow.Cells["CurrentBalance"].Value = 0;
				}
			}
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				currentData = new CustomerData();
				foreach (UltraGridRow row in dataEntryGrid.Rows)
				{
					DataRow dataRow = currentData.CustomerTable.NewRow();
					dataRow.BeginEdit();
					dataRow["ParentCustomerID"] = comboBoxCustomer.SelectedID;
					dataRow["CustomerID"] = row.Cells["ChildCustomerID"].Value.ToString();
					dataRow["CustomerName"] = row.Cells["Name"].Value.ToString();
					currentData.CustomerTable.Rows.Add(dataRow);
					dataRow.EndEdit();
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
				comboBoxCustomer.Focus();
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == ""))
				{
					currentData = Factory.CustomerSystem.GetCustomerChildren(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						dataEntryGrid.Clear();
						IsNewRecord = true;
						comboBoxCustomer.Focus();
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				isLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					comboBoxCustomer.SelectedID = dataRow["ParentCustomerID"].ToString();
					DataTable dataTable = dataEntryGrid.DataSource as DataTable;
					dataTable.Rows.Clear();
					dataTable.BeginLoadData();
					foreach (DataRow row in currentData.CustomerTable.Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["ChildCustomerID"] = row["CustomerID"];
						dataRow3["Name"] = row["CustomerName"];
						dataRow3["CurrentBalance"] = row["Balance"];
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.EndLoadData();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isLoading = false;
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
				bool flag = Factory.CustomerSystem.UpdateCustomerParentChildRelation(currentData);
				if (flag)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Customer, needRefresh: true);
					comboBoxGridCustomer.LoadData();
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
			if (comboBoxCustomer.SelectedID == "" || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (dataEntryGrid.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one child row of item.");
				return false;
			}
			foreach (UltraGridRow row in dataEntryGrid.Rows)
			{
				if (row.Cells["ChildCustomerID"].Value == null || row.Cells["ChildCustomerID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item for all the rows.");
					dataEntryGrid.Focus();
					row.Activate();
					return false;
				}
				if (row.Cells["ChildCustomerID"].Value.ToString() == comboBoxCustomer.SelectedID)
				{
					ErrorHelper.InformationMessage("One of the selected customer can not be a child and parent at the same time!");
					dataEntryGrid.Focus();
					row.Activate();
					return false;
				}
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
			comboBoxCustomer.Clear();
			textBoxName.Clear();
			dataEntryGrid.Clear();
			formManager.ResetDirty();
			comboBoxCustomer.Focus();
		}

		private void ProductGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.CustomerSystem.DeleteCustomerChildren(comboBoxCustomer.SelectedID);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Customer, needRefresh: true);
					comboBoxGridCustomer.LoadData();
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
			SeekID = DatabaseHelper.GetNextID("Customer", "ParentCustomerID", comboBoxCustomer.SelectedID);
			if (SeekID != "")
			{
				comboBoxCustomer.SelectedID = SeekID;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			SeekID = DatabaseHelper.GetPreviousID("Customer", "ParentCustomerID", comboBoxCustomer.SelectedID);
			if (SeekID != "")
			{
				comboBoxCustomer.SelectedID = SeekID;
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			SeekID = DatabaseHelper.GetLastID("Customer", "ParentCustomerID");
			if (SeekID != "")
			{
				comboBoxCustomer.SelectedID = SeekID;
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			SeekID = DatabaseHelper.GetFirstID("Customer", "ParentCustomerID");
			if (SeekID != "")
			{
				comboBoxCustomer.SelectedID = SeekID;
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
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer", "CustomerID", toolStripTextBoxFind.Text.Trim()))
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

		private void NationalAccountForm_Load(object sender, EventArgs e)
		{
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void SetupGrid()
		{
			try
			{
				dataEntryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ChildCustomerID");
				dataTable.Columns.Add("Name", typeof(string));
				dataTable.Columns.Add("CurrentBalance", typeof(decimal));
				dataEntryGrid.DataSource = dataTable;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["ChildCustomerID"].Header.Caption = "Child Customer Code";
				dataEntryGrid.DisplayLayout.Bands[0].Columns["ChildCustomerID"].CharacterCasing = CharacterCasing.Upper;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["ChildCustomerID"].MaxLength = 15;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["ChildCustomerID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["ChildCustomerID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["ChildCustomerID"].ValueList = comboBoxGridCustomer;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["Name"].MaxLength = 255;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["Name"].Width = Convert.ToInt32((double)dataEntryGrid.Width / 2.5);
				dataEntryGrid.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.NoEdit;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["CurrentBalance"].Header.Caption = "Current Balance";
				dataEntryGrid.DisplayLayout.Bands[0].Columns["CurrentBalance"].CellAppearance.TextHAlign = HAlign.Right;
				dataEntryGrid.DisplayLayout.Bands[0].Columns["CurrentBalance"].Format = "#,##0.00";
				dataEntryGrid.DisplayLayout.Bands[0].Columns["CurrentBalance"].CellActivation = Activation.NoEdit;
				dataEntryGrid.DisplayLayout.Bands[0].Summaries.Add("Total", SummaryType.Sum, dataEntryGrid.DisplayLayout.Bands[0].Columns["CurrentBalance"], SummaryPosition.UseSummaryPositionColumn);
				dataEntryGrid.DisplayLayout.Bands[0].Summaries["Total"].Appearance.BackColor = Color.White;
				dataEntryGrid.DisplayLayout.Bands[0].Summaries["Total"].Appearance.TextHAlign = HAlign.Right;
				dataEntryGrid.DisplayLayout.Bands[0].Summaries["Total"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataEntryGrid.DisplayLayout.Bands[0].Summaries["Total"].DisplayFormat = "{0:n}";
			}
			catch (Exception e)
			{
				dataEntryGrid.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
		}

		private void buttonRemoveImage_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomerRelation);
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.NationalAccountForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			textBoxName = new System.Windows.Forms.TextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxGridCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			dataEntryGrid = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(619, 25);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(23, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(23, 22);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(23, 22);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(50, 22);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 404);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(619, 40);
			panelButtons.TabIndex = 3;
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.Location = new System.Drawing.Point(152, 59);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(293, 20);
			textBoxName.TabIndex = 1;
			textBoxName.TabStop = false;
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(12, 93);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(113, 13);
			mmLabel13.TabIndex = 58;
			mmLabel13.Text = "Enter Child Customers:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(619, 1);
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
			buttonDelete.TabIndex = 5;
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
			xpButton1.Location = new System.Drawing.Point(509, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 6;
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
			buttonNew.TabIndex = 4;
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
			buttonSave.TabIndex = 3;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(140, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Parent Customer Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 39);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(137, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Parent Customer Code:";
			comboBoxGridCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCustomer.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCustomer.DisplayLayout.Appearance = appearance;
			comboBoxGridCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCustomer.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridCustomer.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCustomer.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxGridCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCustomer.Editable = true;
			comboBoxGridCustomer.FilterString = "";
			comboBoxGridCustomer.HasAll = false;
			comboBoxGridCustomer.HasCustom = false;
			comboBoxGridCustomer.Location = new System.Drawing.Point(494, 135);
			comboBoxGridCustomer.MaxDropDownItems = 12;
			comboBoxGridCustomer.Name = "comboBoxGridCustomer";
			comboBoxGridCustomer.ShowQuickAdd = true;
			comboBoxGridCustomer.Size = new System.Drawing.Size(121, 20);
			comboBoxGridCustomer.TabIndex = 125;
			comboBoxGridCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCustomer.Visible = false;
			comboBoxCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.DescriptionTextBox = textBoxName;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance13;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.Location = new System.Drawing.Point(152, 37);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(172, 20);
			comboBoxCustomer.TabIndex = 0;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dataEntryGrid.AllowAddNew = false;
			dataEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGrid.DisplayLayout.Appearance = appearance25;
			dataEntryGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataEntryGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataEntryGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGrid.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGrid.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataEntryGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGrid.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGrid.DisplayLayout.Override.CellAppearance = appearance32;
			dataEntryGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGrid.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataEntryGrid.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataEntryGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataEntryGrid.DisplayLayout.Override.RowAppearance = appearance35;
			dataEntryGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataEntryGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGrid.Location = new System.Drawing.Point(10, 111);
			dataEntryGrid.Name = "dataEntryGrid";
			dataEntryGrid.ShowDeleteMenu = true;
			dataEntryGrid.ShowInsertMenu = true;
			dataEntryGrid.Size = new System.Drawing.Size(595, 287);
			dataEntryGrid.TabIndex = 2;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Location = new System.Drawing.Point(0, 25);
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
			base.ClientSize = new System.Drawing.Size(619, 444);
			base.Controls.Add(textBoxName);
			base.Controls.Add(comboBoxCustomer);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(dataEntryGrid);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(comboBoxGridCustomer);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "NationalAccountForm";
			Text = "National Account Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
