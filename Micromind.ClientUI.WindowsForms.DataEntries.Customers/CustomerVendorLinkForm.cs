using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerVendorLinkForm : Form, IForm
	{
		private PartyData currentData;

		private const string TABLENAME_CONST = "Customer_Vendor_Link";

		private const string IDFIELD_CONST = "PartyID";

		private bool isNewRecord = true;

		private bool isLoading;

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

		private DataEntryGrid dataGridCustomerList;

		private customersFlatComboBox comboBoxGridCustomer;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxName;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel4;

		private UltraTabControl ultraTabControl;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageUserDefined;

		private UltraTabPageControl ultraTabCustomer;

		private DataEntryGrid dataGridVendorList;

		private vendorsFlatComboBox comboBoxGridVendor;

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

		public CustomerVendorLinkForm()
		{
			InitializeComponent();
			AddEvents();
			try
			{
				SetupCustomerGrid();
				dataGridCustomerList.SetupUI();
				SetupVendorGrid();
				dataGridVendorList.SetupUI();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e)
			{
				dataGridCustomerList.LoadLayoutFailed = true;
				dataGridVendorList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AddEvents()
		{
			base.Load += NationalAccountForm_Load;
			dataGridCustomerList.AfterCellUpdate += dataGridCustomerList_AfterCellUpdate;
			dataGridCustomerList.CellDataError += dataGridCustomerList_CellDataError;
			dataGridCustomerList.BeforeRowDeactivate += dataGridCustomerList_BeforeRowDeactivate;
			dataGridCustomerList.BeforeCellUpdate += dataGridCustomerList_BeforeCellUpdate;
			dataGridVendorList.AfterCellUpdate += dataGridVendorList_AfterCellUpdate;
			dataGridVendorList.CellDataError += dataGridVendorList_CellDataError;
			dataGridVendorList.BeforeRowDeactivate += dataGridVendorList_BeforeRowDeactivate;
			dataGridVendorList.BeforeCellUpdate += dataGridVendorList_BeforeCellUpdate;
		}

		private void dataGridCustomerList_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridCustomerList.ActiveRow != null && (dataGridCustomerList.ActiveRow.Cells["CustomerID"].Value == null || dataGridCustomerList.ActiveRow.Cells["CustomerID"].Value.ToString() == ""))
			{
				ErrorHelper.InformationMessage("Please select a customer");
				e.Cancel = true;
				dataGridCustomerList.EnterEditMode(dataGridCustomerList.ActiveRow.Cells["CustomerID"]);
			}
		}

		private void dataGridVendorList_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = (UltraGrid)sender;
			if (dataGridVendorList.ActiveRow != null && (dataGridVendorList.ActiveRow.Cells["VendorID"].Value == null || dataGridVendorList.ActiveRow.Cells["VendorID"].Value.ToString() == ""))
			{
				ErrorHelper.InformationMessage("Please select a child customer");
				e.Cancel = true;
				dataGridVendorList.EnterEditMode(dataGridVendorList.ActiveRow.Cells["VendorID"]);
			}
		}

		private void dataGridCustomerList_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridCustomerList.ActiveCell.Column.Key == "ChildCustomerID")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Customer does not exist. Please select a correct customer.");
			}
		}

		private void dataGridVendorList_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridVendorList.ActiveCell.Column.Key == "VendorID")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Vendor does not exist. Please select correct vendor.");
			}
		}

		private void dataGridCustomerList_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "CustomerID")
			{
				if (comboBoxGridCustomer.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Customer does not exist. Please select correct customer");
					e.Cancel = true;
					e.Cell.Activate();
				}
				bool flag = false;
				foreach (UltraGridRow row in dataGridCustomerList.Rows)
				{
					if (comboBoxGridCustomer.SelectedID == row.Cells["CustomerID"].Value.ToString())
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
			}
		}

		private void dataGridVendorList_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "VendorID")
			{
				if (comboBoxGridVendor.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Vendor does not exist. Please select a correct vendor");
					e.Cancel = true;
					e.Cell.Activate();
				}
				bool flag = false;
				foreach (UltraGridRow row in dataGridVendorList.Rows)
				{
					if (comboBoxGridVendor.SelectedID == row.Cells["VendorID"].Value.ToString())
					{
						flag = true;
					}
				}
				if (flag)
				{
					ErrorHelper.InformationMessage("Vendor already exist. Please select a new vendor");
					e.Cancel = true;
					e.Cell.Activate();
				}
			}
		}

		private void dataGridCustomerList_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "CustomerID")
			{
				if (comboBoxGridCustomer.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
				{
					comboBoxGridCustomer.SelectedID = e.Cell.Value.ToString();
				}
				else if (comboBoxGridCustomer.SelectedRow == null)
				{
					return;
				}
				if (dataGridCustomerList.ActiveRow != null)
				{
					dataGridCustomerList.ActiveRow.Cells["CustomerName"].Value = comboBoxGridCustomer.SelectedName;
				}
			}
		}

		private void dataGridVendorList_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "VendorID")
			{
				if (comboBoxGridVendor.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
				{
					comboBoxGridVendor.SelectedID = e.Cell.Value.ToString();
				}
				else if (comboBoxGridVendor.SelectedRow == null)
				{
					return;
				}
				if (dataGridVendorList.ActiveRow != null)
				{
					dataGridVendorList.ActiveRow.Cells["VendorName"].Value = comboBoxGridVendor.SelectedName;
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
				if (currentData == null || isNewRecord)
				{
					currentData = new PartyData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerVendorLinkTable.Rows[0] : currentData.CustomerVendorLinkTable.NewRow();
				dataRow.BeginEdit();
				if (isNewRecord)
				{
					dataRow["PartyID"] = textBoxCode.Text.Trim();
				}
				else
				{
					dataRow["PartyID"] = textBoxCode.Text;
				}
				dataRow["PartyName"] = textBoxName.Text.Trim();
				dataRow["Inactive"] = checkBoxIsInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerVendorLinkTable.Rows.Add(dataRow);
				}
				currentData.CustomerVendorLinkDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridCustomerList.Rows)
				{
					DataRow dataRow2 = currentData.CustomerVendorLinkDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["AccountID"] = row.Cells["CustomerID"].Value.ToString();
					dataRow2["EntityType"] = 1;
					currentData.CustomerVendorLinkDetailTable.Rows.Add(dataRow2);
					dataRow2.EndEdit();
				}
				foreach (UltraGridRow row2 in dataGridVendorList.Rows)
				{
					DataRow dataRow3 = currentData.CustomerVendorLinkDetailTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["AccountID"] = row2.Cells["VendorID"].Value.ToString();
					dataRow3["EntityType"] = 2;
					currentData.CustomerVendorLinkDetailTable.Rows.Add(dataRow3);
					dataRow3.EndEdit();
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
				if (!(id.Trim() == ""))
				{
					currentData = Factory.PartySystem.GetPartyByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
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
					textBoxCode.Text = dataRow["PartyID"].ToString();
					textBoxName.Text = dataRow["PartyName"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
					DataTable dataTable = dataGridCustomerList.DataSource as DataTable;
					DataTable dataTable2 = dataGridVendorList.DataSource as DataTable;
					dataTable?.Rows.Clear();
					dataTable2?.Rows.Clear();
					dataTable.BeginLoadData();
					dataTable2.BeginLoadData();
					foreach (DataRow row in currentData.CustomerVendorLinkDetailTable.Rows)
					{
						switch (int.Parse(row["EntityType"].ToString()))
						{
						case 1:
						{
							DataRow dataRow4 = dataTable.NewRow();
							dataRow4["CustomerID"] = row["AccountID"];
							dataRow4["CustomerName"] = row["AccountName"];
							dataTable.Rows.Add(dataRow4);
							break;
						}
						case 2:
						{
							DataRow dataRow3 = dataTable2.NewRow();
							dataRow3["VendorID"] = row["AccountID"];
							dataRow3["VendorName"] = row["AccountName"];
							dataTable2.Rows.Add(dataRow3);
							break;
						}
						}
					}
					dataTable.EndLoadData();
					dataTable2.EndLoadData();
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
				bool flag = Factory.PartySystem.CreateParty(currentData, !isNewRecord);
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
			if (dataGridCustomerList.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one child row of item.");
				return false;
			}
			bool flag = false;
			foreach (UltraGridRow row in dataGridCustomerList.Rows)
			{
				flag = true;
				if (row.Cells["CustomerID"].Value == null || row.Cells["CustomerID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item for all the rows.");
					dataGridCustomerList.Focus();
					row.Activate();
					return false;
				}
			}
			foreach (UltraGridRow row2 in dataGridVendorList.Rows)
			{
				flag = true;
				if (row2.Cells["VendorID"].Value == null || row2.Cells["VendorID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item for all the rows.");
					dataGridCustomerList.Focus();
					row2.Activate();
					return false;
				}
			}
			if (!flag)
			{
				ErrorHelper.InformationMessage("Please select at least one item in either detail rows");
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
			checkBoxIsInactive.Checked = false;
			dataGridCustomerList.Clear();
			dataGridVendorList.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
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
				return Factory.PartySystem.DeleteParty(textBoxCode.Text.Trim());
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
			LoadData(DatabaseHelper.GetNextID("Customer_Vendor_Link", "PartyID", textBoxCode.Text.Trim()));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Customer_Vendor_Link", "PartyID", textBoxCode.Text.Trim()));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Customer_Vendor_Link", "PartyID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Customer_Vendor_Link", "PartyID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer_Vendor_Link", "PartyID", toolStripTextBoxFind.Text.Trim()))
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
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void SetupCustomerGrid()
		{
			dataGridCustomerList.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("CustomerID");
			dataTable.Columns.Add("CustomerName", typeof(string));
			dataGridCustomerList.DataSource = dataTable;
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerID"].Header.Caption = "Customer Code";
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerID"].CharacterCasing = CharacterCasing.Upper;
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerID"].MaxLength = 15;
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerID"].ValueList = comboBoxGridCustomer;
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerName"].Header.Caption = "Customer Name";
			dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerName"].MaxLength = 255;
			checked
			{
				dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerName"].Width = 50 * Convert.ToInt32(unchecked(dataGridCustomerList.Width / 100));
				dataGridCustomerList.DisplayLayout.Bands[0].Columns["CustomerName"].CellActivation = Activation.NoEdit;
			}
		}

		private void SetupVendorGrid()
		{
			dataGridVendorList.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("VendorID");
			dataTable.Columns.Add("VendorName", typeof(string));
			dataGridVendorList.DataSource = dataTable;
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorID"].Header.Caption = "Vendor Code";
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorID"].CharacterCasing = CharacterCasing.Upper;
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorID"].MaxLength = 15;
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorID"].ValueList = comboBoxGridVendor;
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorName"].Header.Caption = "Vendor Name";
			dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorName"].MaxLength = 255;
			checked
			{
				dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorName"].Width = 50 * Convert.ToInt32(unchecked(dataGridVendorList.Width / 100));
				dataGridVendorList.DisplayLayout.Bands[0].Columns["VendorName"].CellActivation = Activation.NoEdit;
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
			new FormHelper().ShowList(DataComboType.CustomerVendorLink);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerVendorLinkForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			ultraTabCustomer = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxGridVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxGridCustomer = new Micromind.DataControls.customersFlatComboBox();
			dataGridCustomerList = new Micromind.DataControls.DataEntryGrid();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridVendorList = new Micromind.DataControls.DataEntryGrid();
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
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			ultraTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabCustomer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridCustomerList).BeginInit();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridVendorList).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl).BeginInit();
			ultraTabControl.SuspendLayout();
			SuspendLayout();
			ultraTabCustomer.Controls.Add(comboBoxGridVendor);
			ultraTabCustomer.Controls.Add(comboBoxGridCustomer);
			ultraTabCustomer.Controls.Add(dataGridCustomerList);
			ultraTabCustomer.Location = new System.Drawing.Point(2, 21);
			ultraTabCustomer.Name = "ultraTabCustomer";
			ultraTabCustomer.Size = new System.Drawing.Size(539, 248);
			comboBoxGridVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridVendor.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridVendor.DisplayLayout.Appearance = appearance;
			comboBoxGridVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridVendor.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridVendor.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridVendor.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridVendor.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxGridVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridVendor.Editable = true;
			comboBoxGridVendor.FilterString = "";
			comboBoxGridVendor.HasAll = false;
			comboBoxGridVendor.HasCustom = false;
			comboBoxGridVendor.Location = new System.Drawing.Point(424, 60);
			comboBoxGridVendor.MaxDropDownItems = 12;
			comboBoxGridVendor.Name = "comboBoxGridVendor";
			comboBoxGridVendor.ShowQuickAdd = true;
			comboBoxGridVendor.Size = new System.Drawing.Size(119, 20);
			comboBoxGridVendor.TabIndex = 132;
			comboBoxGridVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridVendor.Visible = false;
			comboBoxGridCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCustomer.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCustomer.DisplayLayout.Appearance = appearance13;
			comboBoxGridCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCustomer.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridCustomer.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCustomer.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCustomer.Editable = true;
			comboBoxGridCustomer.FilterString = "";
			comboBoxGridCustomer.HasAll = false;
			comboBoxGridCustomer.HasCustom = false;
			comboBoxGridCustomer.Location = new System.Drawing.Point(422, 86);
			comboBoxGridCustomer.MaxDropDownItems = 12;
			comboBoxGridCustomer.Name = "comboBoxGridCustomer";
			comboBoxGridCustomer.ShowQuickAdd = true;
			comboBoxGridCustomer.Size = new System.Drawing.Size(121, 20);
			comboBoxGridCustomer.TabIndex = 125;
			comboBoxGridCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCustomer.Visible = false;
			dataGridCustomerList.AllowAddNew = false;
			dataGridCustomerList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridCustomerList.DisplayLayout.Appearance = appearance25;
			dataGridCustomerList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridCustomerList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCustomerList.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCustomerList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridCustomerList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCustomerList.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridCustomerList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridCustomerList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridCustomerList.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridCustomerList.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridCustomerList.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridCustomerList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridCustomerList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridCustomerList.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridCustomerList.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridCustomerList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridCustomerList.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCustomerList.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridCustomerList.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridCustomerList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridCustomerList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridCustomerList.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridCustomerList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridCustomerList.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridCustomerList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridCustomerList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridCustomerList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridCustomerList.Location = new System.Drawing.Point(3, 3);
			dataGridCustomerList.Name = "dataGridCustomerList";
			dataGridCustomerList.ShowDeleteMenu = true;
			dataGridCustomerList.ShowInsertMenu = true;
			dataGridCustomerList.Size = new System.Drawing.Size(533, 239);
			dataGridCustomerList.TabIndex = 5;
			tabPageUserDefined.Controls.Add(dataGridVendorList);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(539, 248);
			dataGridVendorList.AllowAddNew = false;
			dataGridVendorList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridVendorList.DisplayLayout.Appearance = appearance37;
			dataGridVendorList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridVendorList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridVendorList.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridVendorList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridVendorList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridVendorList.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridVendorList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridVendorList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridVendorList.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridVendorList.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridVendorList.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridVendorList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridVendorList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridVendorList.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridVendorList.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridVendorList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridVendorList.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridVendorList.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridVendorList.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridVendorList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridVendorList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridVendorList.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridVendorList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridVendorList.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridVendorList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridVendorList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridVendorList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridVendorList.Location = new System.Drawing.Point(3, 3);
			dataGridVendorList.Name = "dataGridVendorList";
			dataGridVendorList.ShowDeleteMenu = true;
			dataGridVendorList.ShowInsertMenu = true;
			dataGridVendorList.Size = new System.Drawing.Size(533, 242);
			dataGridVendorList.TabIndex = 5;
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
			toolStrip1.Size = new System.Drawing.Size(551, 25);
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
			panelButtons.Location = new System.Drawing.Point(0, 387);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(551, 40);
			panelButtons.TabIndex = 6;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(551, 1);
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
			buttonDelete.TabIndex = 8;
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
			xpButton1.Location = new System.Drawing.Point(441, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 9;
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
			buttonNew.TabIndex = 7;
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
			buttonSave.TabIndex = 6;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(76, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Party Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 39);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(73, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Party Code:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			checkBoxIsInactive.AutoSize = true;
			checkBoxIsInactive.Location = new System.Drawing.Point(305, 37);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxIsInactive.TabIndex = 3;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = true;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(103, 79);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(384, 20);
			textBoxNote.TabIndex = 2;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(103, 57);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(384, 20);
			textBoxName.TabIndex = 1;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(103, 35);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(178, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(9, 82);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 130;
			mmLabel4.Text = "Note:";
			ultraTabControl.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl.Controls.Add(tabPageUserDefined);
			ultraTabControl.Controls.Add(ultraTabCustomer);
			ultraTabControl.Location = new System.Drawing.Point(5, 110);
			ultraTabControl.MinTabWidth = 80;
			ultraTabControl.Name = "ultraTabControl";
			ultraTabControl.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl.Size = new System.Drawing.Size(543, 271);
			ultraTabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl.TabIndex = 4;
			ultraTab.TabPage = ultraTabCustomer;
			ultraTab.Text = "&Customer List";
			ultraTab2.TabPage = tabPageUserDefined;
			ultraTab2.Text = "&Vendor List";
			ultraTabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(539, 248);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(551, 427);
			base.Controls.Add(ultraTabControl);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(checkBoxIsInactive);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CustomerVendorLinkForm";
			Text = "Customer Vendor Link Form";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			ultraTabCustomer.ResumeLayout(false);
			ultraTabCustomer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridCustomerList).EndInit();
			tabPageUserDefined.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridVendorList).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl).EndInit();
			ultraTabControl.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
