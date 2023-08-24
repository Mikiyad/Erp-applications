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

namespace Micromind.ClientUI.WindowsForms.DataSync
{
	public class DataSyncSetupDetailsForm : Form, IForm
	{
		private bool isNewRecord = true;

		private DataSyncData currentData;

		private const string TABLENAME_CONST = "Data_Sync";

		private const string IDFIELD_CONST = "Code";

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel2;

		private ComboBox comboBoxType;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMLabel mmLabel6;

		private MMLabel mmLabel7;

		private MMTextBox textBoxDatabaseName;

		private MMTextBox textBoxServer;

		private MMTextBox textBoxUserID;

		private MMTextBox textBoxPassword;

		private DataEntryGrid dataGridItems;

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

		public DataSyncSetupDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += DataSyncSetupDetailsForm_Load;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key.ToString() == "SyncType")
			{
				ValueList valueList = null;
				if (e.Cell.Value.ToString() == "M")
				{
					valueList = new ValueList();
					valueList.ValueListItems.Add("Product");
					valueList.ValueListItems.Add("Customer");
					valueList.ValueListItems.Add("NewCustomer");
				}
				else if (e.Cell.Value.ToString() == "T")
				{
					valueList = new ValueList();
					valueList.ValueListItems.Add("SalesReceipt");
					valueList.ValueListItems.Add("CashSalesReturn");
					valueList.ValueListItems.Add("CashReceipt");
					valueList.ValueListItems.Add("ChequeReceipt");
					valueList.ValueListItems.Add("TransitTransferIn");
					valueList.ValueListItems.Add("TransitTransferOut");
				}
				e.Cell.Row.Cells["Name"].ValueList = valueList;
			}
			if (!(e.Cell.Column.Key.ToString() == "Name"))
			{
				return;
			}
			if (dataGridItems.ActiveRow.Cells["SyncType"].Value.ToString() == "M")
			{
				string text = dataGridItems.ActiveCell.Value.ToString();
				if (text == "NewCustomer")
				{
					dataGridItems.ActiveRow.Cells["RecordType"].Value = 101;
				}
				else
				{
					DataComboType dataComboType = DataComboType.None;
					dataComboType = (DataComboType)Enum.Parse(typeof(DataComboType), text, ignoreCase: true);
					dataGridItems.ActiveRow.Cells["RecordType"].Value = (int)dataComboType;
				}
			}
			else if (dataGridItems.ActiveRow.Cells["SyncType"].Value.ToString() == "T")
			{
				string value = dataGridItems.ActiveCell.Value.ToString();
				SysDocTypes sysDocTypes = SysDocTypes.None;
				sysDocTypes = (SysDocTypes)Enum.Parse(typeof(SysDocTypes), value, ignoreCase: true);
				dataGridItems.ActiveRow.Cells["RecordType"].Value = (int)sysDocTypes;
			}
			dataGridItems.ActiveRow.Cells["Description"].Value = e.Cell.Value.ToString();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void comboBoxFeeType_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
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
				bool flag = Factory.DataSyncSystem.CreateDataSync(currentData, !IsNewRecord);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Data_Sync", "Code", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || comboBoxType.SelectedIndex < 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Data_Sync", "Code", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
			}
			return true;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new DataSyncData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.DataSyncTable.Rows[0] : currentData.DataSyncTable.NewRow();
				dataRow.BeginEdit();
				dataRow["Code"] = textBoxCode.Text.Trim();
				dataRow["Name"] = textBoxName.Text.Trim();
				dataRow["Type"] = 1;
				dataRow["DatabaseName"] = textBoxDatabaseName.Text.Trim();
				dataRow["ServerID"] = textBoxServer.Text.Trim();
				dataRow["UserID"] = textBoxUserID.Text.Trim();
				dataRow["Password"] = textBoxPassword.Text.Trim();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.DataSyncTable.Rows.Add(dataRow);
				}
				currentData.DataSyncTableDetail.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.DataSyncTableDetail.NewRow();
					dataRow2.BeginEdit();
					dataRow2["Code"] = textBoxCode.Text.Trim();
					if (row.Cells["SyncType"].Value.ToString() == "M")
					{
						dataRow2["SyncType"] = 1;
					}
					else if (row.Cells["SyncType"].Value.ToString() == "T")
					{
						dataRow2["SyncType"] = 2;
					}
					dataRow2["RecordType"] = int.Parse(row.Cells["RecordType"].Value.ToString());
					dataRow2["Name"] = row.Cells["Name"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					if (row.Cells["DefaultSysDocID"].Value.ToString() != "")
					{
						dataRow2["DefaultSysDocID"] = row.Cells["DefaultSysDocID"].Value.ToString();
					}
					else
					{
						dataRow2["DefaultSysDocID"] = DBNull.Value;
					}
					if (row.Cells["DefaultRegisterID"].Value.ToString() != "")
					{
						dataRow2["DefaultRegisterID"] = row.Cells["DefaultRegisterID"].Value.ToString();
					}
					else
					{
						dataRow2["DefaultRegisterID"] = DBNull.Value;
					}
					if (row.Cells["LastSyncTime"].Value.ToString() != "")
					{
						dataRow2["LastSyncTime"] = DateTime.Parse(row.Cells["LastSyncTime"].Value.ToString());
					}
					else
					{
						dataRow2["LastSyncTime"] = DBNull.Value;
					}
					if (row.Cells["SyncInterval"].Value.ToString() != "")
					{
						dataRow2["SyncInterval"] = int.Parse(row.Cells["SyncInterval"].Value.ToString());
					}
					else
					{
						dataRow2["SyncInterval"] = DBNull.Value;
					}
					dataRow2["Status"] = false;
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.DataSyncTableDetail.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			ClearForm();
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Data_Sync", "Code");
			textBoxName.Clear();
			comboBoxType.SelectedIndex = -1;
			textBoxDatabaseName.Clear();
			textBoxServer.Clear();
			textBoxUserID.Clear();
			textBoxPassword.Clear();
			textBoxCode.Focus();
			formManager.ResetDirty();
			(dataGridItems.DataSource as DataTable).Rows.Clear();
			isNewRecord = true;
		}

		private void JobFeeGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JobFeeGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Data_Sync", "Code", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Data_Sync", "Code", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Data_Sync", "Code"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Data_Sync", "Code"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Data_Sync", "Code", toolStripTextBoxFind.Text.Trim()))
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
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave);
					if (dialogResult != DialogResult.No && dialogResult != DialogResult.Yes)
					{
						return false;
					}
				}
			}
			return true;
		}

		private void DataSyncSetupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					SetupGrid();
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.DataSync);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.SetupUI();
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SyncType");
				dataTable.Columns.Add("RecordType");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("DefaultSysDocID");
				dataTable.Columns.Add("DefaultRegisterID");
				dataTable.Columns.Add("LastSyncTime", typeof(DateTime));
				dataTable.Columns.Add("SyncInterval");
				dataTable.Columns.Add("Status");
				dataGridItems.DataSource = dataTable;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["SyncType"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["SyncType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["SyncType"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["SyncType"].Header.Appearance.Cursor = Cursors.Hand;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add("M");
				valueList.ValueListItems.Add("T");
				dataGridItems.DisplayLayout.Bands[0].Columns["SyncType"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[0].Columns["Name"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Name"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Name"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Name"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Status"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["RecordType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SyncInterval"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["LastSyncTime"].Hidden = true;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.DataSyncSystem.GetDataSyncByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables["Data_Sync"].Rows.Count == 0)
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
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables["Data_Sync"].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["Data_Sync"].Rows[0];
				textBoxCode.Text = dataRow["Code"].ToString();
				textBoxName.Text = dataRow["Name"].ToString();
				comboBoxType.SelectedIndex = 0;
				textBoxDatabaseName.Text = dataRow["DatabaseName"].ToString();
				textBoxServer.Text = dataRow["ServerID"].ToString();
				textBoxUserID.Text = dataRow["UserID"].ToString();
				textBoxPassword.Text = dataRow["Password"].ToString();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData.Tables.Contains("Data_Sync_Detail") && currentData.DataSyncTableDetail.Rows.Count != 0)
				{
					dataGridItems.BeginUpdate();
					foreach (DataRow row in currentData.Tables["Data_Sync_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						if (row["SyncType"].ToString() == "1")
						{
							dataRow3["SyncType"] = "M";
						}
						else if (row["SyncType"].ToString() == "2")
						{
							dataRow3["SyncType"] = "T";
						}
						RecordTypeLoadData();
						dataRow3["RecordType"] = row["RecordType"];
						dataRow3["Name"] = row["Name"];
						dataRow3["Description"] = row["Description"];
						if (!string.IsNullOrEmpty(row["LastSyncTime"].ToString()))
						{
							dataRow3["LastSyncTime"] = row["LastSyncTime"];
						}
						dataRow3["DefaultSysDocID"] = row["DefaultSysDocID"];
						dataRow3["DefaultRegisterID"] = row["DefaultRegisterID"];
						dataRow3["SyncInterval"] = row["SyncInterval"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
					dataGridItems.EndUpdate();
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						ValueList valueList = null;
						if (row2.Cells["SyncType"].Value.ToString() == "M")
						{
							valueList = new ValueList();
							valueList.ValueListItems.Add("Product");
							valueList.ValueListItems.Add("Customer");
							valueList.ValueListItems.Add("NewCustomer");
						}
						else if (row2.Cells["SyncType"].Value.ToString() == "T")
						{
							valueList = new ValueList();
							valueList.ValueListItems.Add("SalesReceipt");
							valueList.ValueListItems.Add("CashSalesReturn");
							valueList.ValueListItems.Add("CashReceipt");
							valueList.ValueListItems.Add("ChequeReceipt");
							valueList.ValueListItems.Add("TransitTransferIn");
							valueList.ValueListItems.Add("TransitTransferOut");
						}
						row2.Cells["Name"].ValueList = valueList;
					}
				}
			}
		}

		private void RecordTypeLoadData()
		{
			if (dataGridItems.ActiveRow != null)
			{
				ValueList valueList = null;
				if (dataGridItems.ActiveRow.Cells["SyncType"].Value.ToString() == "M")
				{
					valueList = new ValueList();
					valueList.ValueListItems.Add("Product");
					valueList.ValueListItems.Add("Customer");
					valueList.ValueListItems.Add("NewCustomer");
				}
				else if (dataGridItems.ActiveRow.Cells["SyncType"].Value.ToString() == "T")
				{
					valueList = new ValueList();
					valueList.ValueListItems.Add("SalesReceipt");
					valueList.ValueListItems.Add("CashSalesReturn");
					valueList.ValueListItems.Add("CashReceipt");
					valueList.ValueListItems.Add("ChequeReceipt");
					valueList.ValueListItems.Add("TransitTransferIn");
					valueList.ValueListItems.Add("TransitTransferOut");
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Name"].ValueList = valueList;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataSync.DataSyncSetupDetailsForm));
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
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxType = new System.Windows.Forms.ComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxDatabaseName = new Micromind.UISupport.MMTextBox();
			textBoxServer = new Micromind.UISupport.MMTextBox();
			textBoxUserID = new Micromind.UISupport.MMTextBox();
			textBoxPassword = new Micromind.UISupport.MMTextBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(718, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 374);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(718, 40);
			panelButtons.TabIndex = 8;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(718, 1);
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
			buttonDelete.TabIndex = 10;
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
			xpButton1.Location = new System.Drawing.Point(608, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 11;
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
			buttonNew.TabIndex = 9;
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
			buttonSave.TabIndex = 8;
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
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(92, 92);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(182, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(92, 69);
			textBoxCode.MaxLength = 30;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(182, 20);
			textBoxCode.TabIndex = 1;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 94);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(43, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(13, 72);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(40, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Code:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(12, 48);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(39, 13);
			mmLabel2.TabIndex = 84;
			mmLabel2.Text = "Type:";
			comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxType.FormattingEnabled = true;
			comboBoxType.Items.AddRange(new object[1]
			{
				"Incube"
			});
			comboBoxType.Location = new System.Drawing.Point(92, 45);
			comboBoxType.Name = "comboBoxType";
			comboBoxType.Size = new System.Drawing.Size(182, 21);
			comboBoxType.TabIndex = 0;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(313, 45);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(56, 13);
			mmLabel3.TabIndex = 86;
			mmLabel3.Text = "Database:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(313, 117);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(56, 13);
			mmLabel5.TabIndex = 87;
			mmLabel5.Text = "Password:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(313, 93);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(43, 13);
			mmLabel6.TabIndex = 88;
			mmLabel6.Text = "UserID:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(313, 69);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(41, 13);
			mmLabel7.TabIndex = 89;
			mmLabel7.Text = "Server:";
			textBoxDatabaseName.BackColor = System.Drawing.Color.White;
			textBoxDatabaseName.CustomReportFieldName = "";
			textBoxDatabaseName.CustomReportKey = "";
			textBoxDatabaseName.CustomReportValueType = 1;
			textBoxDatabaseName.IsComboTextBox = false;
			textBoxDatabaseName.IsModified = false;
			textBoxDatabaseName.Location = new System.Drawing.Point(393, 42);
			textBoxDatabaseName.MaxLength = 64;
			textBoxDatabaseName.Name = "textBoxDatabaseName";
			textBoxDatabaseName.Size = new System.Drawing.Size(212, 20);
			textBoxDatabaseName.TabIndex = 3;
			textBoxServer.BackColor = System.Drawing.Color.White;
			textBoxServer.CustomReportFieldName = "";
			textBoxServer.CustomReportKey = "";
			textBoxServer.CustomReportValueType = 1;
			textBoxServer.IsComboTextBox = false;
			textBoxServer.IsModified = false;
			textBoxServer.Location = new System.Drawing.Point(393, 65);
			textBoxServer.MaxLength = 64;
			textBoxServer.Name = "textBoxServer";
			textBoxServer.Size = new System.Drawing.Size(212, 20);
			textBoxServer.TabIndex = 4;
			textBoxUserID.BackColor = System.Drawing.Color.White;
			textBoxUserID.CustomReportFieldName = "";
			textBoxUserID.CustomReportKey = "";
			textBoxUserID.CustomReportValueType = 1;
			textBoxUserID.IsComboTextBox = false;
			textBoxUserID.IsModified = false;
			textBoxUserID.Location = new System.Drawing.Point(393, 88);
			textBoxUserID.MaxLength = 64;
			textBoxUserID.Name = "textBoxUserID";
			textBoxUserID.Size = new System.Drawing.Size(212, 20);
			textBoxUserID.TabIndex = 5;
			textBoxPassword.BackColor = System.Drawing.Color.White;
			textBoxPassword.CustomReportFieldName = "";
			textBoxPassword.CustomReportKey = "";
			textBoxPassword.CustomReportValueType = 1;
			textBoxPassword.IsComboTextBox = false;
			textBoxPassword.IsModified = false;
			textBoxPassword.Location = new System.Drawing.Point(393, 111);
			textBoxPassword.MaxLength = 64;
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(212, 20);
			textBoxPassword.TabIndex = 6;
			textBoxPassword.UseSystemPasswordChar = true;
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
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 137);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(694, 231);
			dataGridItems.TabIndex = 7;
			dataGridItems.Text = "dataEntryGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(718, 414);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(textBoxUserID);
			base.Controls.Add(textBoxServer);
			base.Controls.Add(textBoxDatabaseName);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(comboBoxType);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "DataSyncSetupDetailsForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Data Sync Setup";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
