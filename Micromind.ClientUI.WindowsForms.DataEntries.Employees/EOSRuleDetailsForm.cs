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
	public class EOSRuleDetailsForm : Form, IForm
	{
		private EOSRuleData currentData;

		private const string TABLENAME_CONST = "Employee_EOSRule";

		private const string IDFIELD_CONST = "EOSRuleID";

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

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel2;

		private AllAccountsComboBox comboBoxAccount;

		private MMTextBox textBoxAccountName;

		private DataEntryGrid dataEntryGridResignedEOSRule;

		private MMLabel mmLabel4;

		private ComboBox comboBoxSystem;

		private MMLabel mmLabel5;

		private ToolStripButton toolStripButtonInformation;

		private DataEntryGrid dataEntryGridTerminatedEOSRule;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

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

		public EOSRuleDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxSystem.SelectedIndex = 1;
		}

		private void AddEvents()
		{
			base.Load += EOSRuleDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EOSRuleData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EOSRuleTable.Rows[0] : currentData.EOSRuleTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EOSRuleID"] = textBoxCode.Text.Trim();
				dataRow["EOSRuleName"] = textBoxName.Text.Trim();
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["AccountID"] = comboBoxAccount.SelectedID;
				dataRow["Note"] = textBoxNote.Text.Trim();
				dataRow["EOSSystem"] = comboBoxSystem.SelectedIndex;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EOSRuleTable.Rows.Add(dataRow);
				}
				currentData.EOSRuleDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataEntryGridResignedEOSRule.Rows)
				{
					dataRow = currentData.Tables["Employee_EOSRule_Detail"].NewRow();
					dataRow.BeginEdit();
					dataRow["EOSRuleID"] = textBoxCode.Text.Trim();
					dataRow["ResignedType"] = "1";
					dataRow["YearFrom"] = row.Cells["YearFrom"].Value.ToString();
					dataRow["YearTo"] = row.Cells["YearTo"].Value.ToString();
					dataRow["EOSDay"] = row.Cells["EOSDay"].Value.ToString();
					dataRow["RowIndex"] = row.Index;
					dataRow.EndEdit();
					currentData.Tables["Employee_EOSRule_Detail"].Rows.Add(dataRow);
				}
				foreach (UltraGridRow row2 in dataEntryGridTerminatedEOSRule.Rows)
				{
					dataRow = currentData.Tables["Employee_EOSRule_Detail"].NewRow();
					dataRow.BeginEdit();
					dataRow["EOSRuleID"] = textBoxCode.Text.Trim();
					dataRow["ResignedType"] = "3";
					dataRow["YearFrom"] = row2.Cells["YearFrom"].Value.ToString();
					dataRow["YearTo"] = row2.Cells["YearTo"].Value.ToString();
					dataRow["EOSDay"] = row2.Cells["EOSDay"].Value.ToString();
					dataRow["RowIndex"] = row2.Index;
					dataRow.EndEdit();
					currentData.Tables["Employee_EOSRule_Detail"].Rows.Add(dataRow);
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
					currentData = Factory.EOSRuleSystem.GetEOSRuleByID(id.Trim());
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
				textBoxCode.Text = dataRow["EOSRuleID"].ToString();
				textBoxName.Text = dataRow["EOSRuleName"].ToString();
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				comboBoxAccount.SelectedID = dataRow["AccountID"].ToString();
				textBoxAccountName.Text = comboBoxAccount.SelectedName;
				textBoxNote.Text = dataRow["Note"].ToString();
				comboBoxSystem.SelectedIndex = int.Parse(dataRow["EOSSystem"].ToString());
				DataTable dataTable = dataEntryGridResignedEOSRule.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables["Employee_EOSRule_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					foreach (DataColumn column in dataRow3.Table.Columns)
					{
						_ = column;
						dataRow3["YearFrom"] = row["YearFrom"];
						dataRow3["YearTo"] = row["YearTo"];
						dataRow3["EOSDay"] = row["EOSDay"];
					}
					dataRow3.EndEdit();
					if (!(row["ResignedType"].ToString() == "3"))
					{
						dataTable.Rows.Add(dataRow3);
					}
				}
				DataTable dataTable2 = dataEntryGridTerminatedEOSRule.DataSource as DataTable;
				dataTable2.Rows.Clear();
				foreach (DataRow row2 in currentData.Tables["Employee_EOSRule_Detail"].Rows)
				{
					DataRow dataRow5 = dataTable2.NewRow();
					foreach (DataColumn column2 in dataRow5.Table.Columns)
					{
						_ = column2;
						dataRow5["YearFrom"] = row2["YearFrom"];
						dataRow5["YearTo"] = row2["YearTo"];
						dataRow5["EOSDay"] = row2["EOSDay"];
					}
					dataRow5.EndEdit();
					if (!(row2["ResignedType"].ToString() == "1"))
					{
						dataTable2.Rows.Add(dataRow5);
					}
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataEntryGridResignedEOSRule.DisplayLayout.Bands[0].Columns.ClearUnbound();
				dataEntryGridTerminatedEOSRule.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("YearFrom", typeof(int));
				dataTable.Columns.Add("YearTo", typeof(int));
				dataTable.Columns.Add("EOSDay", typeof(int));
				DataTable dataTable2 = new DataTable();
				dataTable2.Columns.Add("YearFrom", typeof(int));
				dataTable2.Columns.Add("YearTo", typeof(int));
				dataTable2.Columns.Add("EOSDay", typeof(int));
				dataEntryGridResignedEOSRule.DataSource = dataTable;
				dataEntryGridTerminatedEOSRule.DataSource = dataTable2;
				dataEntryGridTerminatedEOSRule.DisplayLayout.Bands[0].MaxRows = 4;
				dataEntryGridResignedEOSRule.DisplayLayout.Bands[0].MaxRows = 4;
				dataEntryGridResignedEOSRule.DisplayLayout.Bands[0].Columns["EOSDay"].MaxValue = 31;
				dataEntryGridTerminatedEOSRule.DisplayLayout.Bands[0].Columns["EOSDay"].MaxValue = 31;
			}
			catch (Exception e)
			{
				dataEntryGridResignedEOSRule.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
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
					flag = Factory.EOSRuleSystem.CreateEOSRule(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.EOSRule, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.EOSRuleSystem.UpdateEOSRule(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Employee_EOSRule", "EOSRuleID", textBoxCode.Text.Trim()))
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
			checkBoxInactive.Checked = false;
			comboBoxAccount.Clear();
			textBoxAccountName.Clear();
			textBoxNote.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxSystem.SelectedIndex = 1;
			DataTable dataTable = dataEntryGridResignedEOSRule.DataSource as DataTable;
			DataTable obj = dataEntryGridTerminatedEOSRule.DataSource as DataTable;
			dataTable.Rows.Clear();
			obj.Rows.Clear();
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
				return Factory.EOSRuleSystem.DeleteEOSRule(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Employee_EOSRule", "EOSRuleID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Employee_EOSRule", "EOSRuleID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Employee_EOSRule", "EOSRuleID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Employee_EOSRule", "EOSRuleID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee_EOSRule", "EOSRuleID", toolStripTextBoxFind.Text.Trim()))
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

		private void EOSRuleDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					dataEntryGridResignedEOSRule.SetupUI();
					dataEntryGridTerminatedEOSRule.SetupUI();
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.EOSRule);
		}

		private void comboBoxSystem_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxSystem.SelectedIndex == 0)
			{
				ErrorHelper.InformationMessage("Custom EOS calculation is not supported in this version. Please contact Micromind or Partners for details.");
				comboBoxSystem.SelectedIndex = 1;
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EOSRuleDetailsForm));
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
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			dataEntryGridResignedEOSRule = new Micromind.DataControls.DataEntryGrid();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxSystem = new System.Windows.Forms.ComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			dataEntryGridTerminatedEOSRule = new Micromind.DataControls.DataEntryGrid();
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridResignedEOSRule).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridTerminatedEOSRule).BeginInit();
			groupBox2.SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(579, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 459);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(579, 40);
			panelButtons.TabIndex = 8;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(579, 1);
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
			xpButton1.Location = new System.Drawing.Point(469, 8);
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
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(69, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "EOS Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(88, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(203, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(43, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Name:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(88, 58);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(348, 20);
			textBoxName.TabIndex = 2;
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
			checkBoxInactive.Location = new System.Drawing.Point(297, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(88, 129);
			textBoxNote.MaxLength = 30;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(348, 20);
			textBoxNote.TabIndex = 5;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 130);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(33, 13);
			mmLabel2.TabIndex = 18;
			mmLabel2.Text = "Note:";
			comboBoxAccount.Assigned = false;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = textBoxAccountName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxAccount.Location = new System.Drawing.Point(88, 85);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(203, 20);
			comboBoxAccount.TabIndex = 3;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.CustomReportFieldName = "";
			textBoxAccountName.CustomReportKey = "";
			textBoxAccountName.CustomReportValueType = 1;
			textBoxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxAccountName.IsComboTextBox = false;
			textBoxAccountName.IsModified = false;
			textBoxAccountName.Location = new System.Drawing.Point(88, 107);
			textBoxAccountName.MaxLength = 255;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(347, 20);
			textBoxAccountName.TabIndex = 4;
			textBoxAccountName.TabStop = false;
			dataEntryGridResignedEOSRule.AllowAddNew = false;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridResignedEOSRule.DisplayLayout.Appearance = appearance13;
			dataEntryGridResignedEOSRule.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridResignedEOSRule.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridResignedEOSRule.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridResignedEOSRule.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataEntryGridResignedEOSRule.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridResignedEOSRule.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataEntryGridResignedEOSRule.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridResignedEOSRule.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.CellAppearance = appearance20;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataEntryGridResignedEOSRule.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.RowAppearance = appearance23;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridResignedEOSRule.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataEntryGridResignedEOSRule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridResignedEOSRule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridResignedEOSRule.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridResignedEOSRule.IncludeLotItems = false;
			dataEntryGridResignedEOSRule.LoadLayoutFailed = false;
			dataEntryGridResignedEOSRule.Location = new System.Drawing.Point(12, 217);
			dataEntryGridResignedEOSRule.Name = "dataEntryGridResignedEOSRule";
			dataEntryGridResignedEOSRule.ShowClearMenu = true;
			dataEntryGridResignedEOSRule.ShowDeleteMenu = true;
			dataEntryGridResignedEOSRule.ShowInsertMenu = true;
			dataEntryGridResignedEOSRule.ShowMoveRowsMenu = true;
			dataEntryGridResignedEOSRule.Size = new System.Drawing.Size(556, 109);
			dataEntryGridResignedEOSRule.TabIndex = 7;
			dataEntryGridResignedEOSRule.Text = "dataEntryGrid1";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(12, 186);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(165, 13);
			mmLabel4.TabIndex = 18;
			mmLabel4.Text = "End of Service Calculation Rules:";
			comboBoxSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSystem.FormattingEnabled = true;
			comboBoxSystem.Items.AddRange(new object[3]
			{
				"Custom",
				"UAE",
				"Others"
			});
			comboBoxSystem.Location = new System.Drawing.Point(88, 159);
			comboBoxSystem.Name = "comboBoxSystem";
			comboBoxSystem.Size = new System.Drawing.Size(98, 21);
			comboBoxSystem.TabIndex = 6;
			comboBoxSystem.SelectedIndexChanged += new System.EventHandler(comboBoxSystem_SelectedIndexChanged);
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(12, 162);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(69, 13);
			mmLabel5.TabIndex = 18;
			mmLabel5.Text = "EOS System:";
			dataEntryGridTerminatedEOSRule.AllowAddNew = false;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Appearance = appearance25;
			dataEntryGridTerminatedEOSRule.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridTerminatedEOSRule.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridTerminatedEOSRule.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridTerminatedEOSRule.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataEntryGridTerminatedEOSRule.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridTerminatedEOSRule.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataEntryGridTerminatedEOSRule.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridTerminatedEOSRule.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.CellAppearance = appearance32;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.RowAppearance = appearance35;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridTerminatedEOSRule.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataEntryGridTerminatedEOSRule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridTerminatedEOSRule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridTerminatedEOSRule.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridTerminatedEOSRule.IncludeLotItems = false;
			dataEntryGridTerminatedEOSRule.LoadLayoutFailed = false;
			dataEntryGridTerminatedEOSRule.Location = new System.Drawing.Point(6, 13);
			dataEntryGridTerminatedEOSRule.Name = "dataEntryGridTerminatedEOSRule";
			dataEntryGridTerminatedEOSRule.ShowClearMenu = true;
			dataEntryGridTerminatedEOSRule.ShowDeleteMenu = true;
			dataEntryGridTerminatedEOSRule.ShowInsertMenu = true;
			dataEntryGridTerminatedEOSRule.ShowMoveRowsMenu = true;
			dataEntryGridTerminatedEOSRule.Size = new System.Drawing.Size(554, 109);
			dataEntryGridTerminatedEOSRule.TabIndex = 20;
			dataEntryGridTerminatedEOSRule.Text = "dataEntryGrid1";
			groupBox1.Location = new System.Drawing.Point(6, 203);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(567, 128);
			groupBox1.TabIndex = 21;
			groupBox1.TabStop = false;
			groupBox1.Text = "If Resigned";
			groupBox2.Controls.Add(dataEntryGridTerminatedEOSRule);
			groupBox2.Location = new System.Drawing.Point(8, 331);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(565, 125);
			groupBox2.TabIndex = 22;
			groupBox2.TabStop = false;
			groupBox2.Text = "If Terminiated";
			appearance37.FontData.BoldAsString = "True";
			appearance37.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance37;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(15, 88);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel5.TabIndex = 23;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Account:";
			appearance38.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance38;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(579, 499);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(comboBoxSystem);
			base.Controls.Add(dataEntryGridResignedEOSRule);
			base.Controls.Add(comboBoxAccount);
			base.Controls.Add(textBoxAccountName);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(groupBox1);
			base.Controls.Add(groupBox2);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "EOSRuleDetailsForm";
			Text = "End of Service Benefits Rules";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridResignedEOSRule).EndInit();
			((System.ComponentModel.ISupportInitialize)dataEntryGridTerminatedEOSRule).EndInit();
			groupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
