using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class AccountAnalysisDetailsForm : Form, IForm
	{
		private DataSet currentData;

		private const string TABLENAME_CONST = "Account_Analysis_Detail";

		private const string IDFIELD_CONST = "AccountID";

		private bool isNewRecord = true;

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

		private MMLabel labelCode;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private AllAccountsComboBox comboBoxAccounts;

		private TextBox textBoxAccountName;

		private DataEntryGrid dataGrid;

		private AnalysisGroupComboBox comboBoxAnalysisGroup;

		private NonDirtyPanel nonDirtyPanel1;

		private MMLabel mmLabel1;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1001;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

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
			}
		}

		public AccountAnalysisDetailsForm()
		{
			InitializeComponent();
			dataGrid.AfterRowInsert += dataGrid_AfterRowInsert;
			dataGrid.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (dataGrid.ActiveCell.Column.Index == 0)
			{
				foreach (UltraGridRow row in dataGrid.Rows)
				{
					if (row != dataGrid.ActiveRow && row.Cells[0].Text == e.NewValue.ToString())
					{
						ErrorHelper.WarningMessage("This analysis group is already exist for this account.");
						e.Cancel = true;
						break;
					}
				}
			}
		}

		private void dataGrid_AfterRowInsert(object sender, RowEventArgs e)
		{
			if (e.Row.Cells[1].Value == null)
			{
				e.Row.Cells[1].Value = 1;
			}
		}

		private bool GetData()
		{
			try
			{
				currentData = new AccountAnalysisData();
				foreach (UltraGridRow row in dataGrid.Rows)
				{
					if (row.Cells[0].Value != null && !(row.Cells[0].Value.ToString() == ""))
					{
						DataRow dataRow = currentData.Tables[0].NewRow();
						dataRow.BeginEdit();
						dataRow["AccountID"] = comboBoxAccounts.SelectedID;
						dataRow["AnalysisGroupID"] = row.Cells["Analysis Group"].Value;
						dataRow["TYPE"] = row.Cells["Type"].Value;
						dataRow.EndEdit();
						currentData.Tables[0].Rows.Add(dataRow);
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
			comboBoxAccounts.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.AccountAnalysisDetailSystem.GetAccountAnalysisGroupsByAccountID(id.Trim());
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
					{
						IsNewRecord = false;
						FillData();
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
				DataTable dataTable = dataGrid.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					foreach (DataRow row in currentData.Tables[0].Rows)
					{
						string text = row[2].ToString();
						if (text == "")
						{
							text = "1";
						}
						dataTable.Rows.Add(row[1].ToString(), text);
					}
					dataTable.AcceptChanges();
					comboBoxAnalysisGroup.SelectedID = "";
				}
			}
			catch
			{
				throw;
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
				bool flag = (dataGrid.Rows.Count <= 0) ? Factory.AccountAnalysisDetailSystem.DeleteAccountAnalysis(comboBoxAccounts.SelectedID) : Factory.AccountAnalysisDetailSystem.CreateAccountAnalysisDetail(currentData);
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
			if (comboBoxAccounts.Text.Trim().Length == 0 || comboBoxAccounts.Text.Trim().Length == 0)
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
			comboBoxAccounts.Clear();
			textBoxAccountName.Clear();
			if (dataGrid.DataSource != null)
			{
				((DataTable)dataGrid.DataSource).Rows.Clear();
			}
			formManager.ResetDirty();
			comboBoxAccounts.Focus();
		}

		private void AnalysisGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void AnalysisGroupDetailsForm_Validated(object sender, EventArgs e)
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
			string nextID = DatabaseHelper.GetNextID("Account", "AccountID", comboBoxAccounts.Text);
			comboBoxAccounts.SelectedID = nextID;
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Account", "AccountID", comboBoxAccounts.Text);
			comboBoxAccounts.SelectedID = previousID;
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Account", "AccountID");
			comboBoxAccounts.SelectedID = lastID;
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Account", "AccountID");
			comboBoxAccounts.SelectedID = firstID;
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Account", "AccountID", toolStripTextBoxFind.Text.Trim()))
				{
					comboBoxAccounts.SelectedID = toolStripTextBoxFind.Text.Trim();
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

		private void AnalysisGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				IsNewRecord = true;
				ClearForm();
				SetupGrid();
				comboBoxAnalysisGroup.LoadData();
			}
			catch (Exception e2)
			{
				dataGrid.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		public void BindData()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Analysis Group", typeof(string)).Unique = true;
			dataTable.Columns.Add("Type", typeof(byte));
			dataGrid.DataSource = dataTable;
			dataGrid.SetupUI();
		}

		public void SetupGrid()
		{
			dataGrid.SetupUI();
			BindData();
			dataGrid.DisplayLayout.Bands[0].Columns["Analysis Group"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGrid.DisplayLayout.Bands[0].Columns["Analysis Group"].ValueList = comboBoxAnalysisGroup;
			dataGrid.DisplayLayout.Bands[0].Columns["Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add(1, "Optional");
			valueList.ValueListItems.Add(2, "Required");
			valueList.ValueListItems.Add(3, "Inactive");
			dataGrid.DisplayLayout.Bands[0].Columns["Type"].ValueList = valueList;
			dataGrid.DisplayLayout.Bands[0].Columns["Type"].DefaultCellValue = 1;
		}

		private void comboBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxAccounts.SelectedRow != null && comboBoxAccounts.SelectedID != "")
			{
				dataGrid.Enabled = true;
				textBoxAccountName.Text = comboBoxAccounts.SelectedName;
				if (!(comboBoxAccounts.Text == ""))
				{
					LoadData(comboBoxAccounts.Text);
				}
				return;
			}
			dataGrid.Enabled = false;
			if (dataGrid.DataSource != null)
			{
				(dataGrid.DataSource as DataTable).Rows.Clear();
			}
			textBoxAccountName.Clear();
			ClearForm();
		}

		private void comboBoxAccounts_ValueChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.AccountAnalysisDetailsForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			nonDirtyPanel1 = new Micromind.UISupport.NonDirtyPanel(components);
			textBoxAccountName = new System.Windows.Forms.TextBox();
			comboBoxAccounts = new Micromind.DataControls.AllAccountsComboBox();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxAnalysisGroup = new Micromind.DataControls.AnalysisGroupComboBox();
			dataGrid = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			nonDirtyPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccounts).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysisGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(551, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 375);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(551, 40);
			panelButtons.TabIndex = 2;
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
			xpButton1.TabIndex = 1;
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
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(12, 109);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Analysis Groups:";
			nonDirtyPanel1.Controls.Add(textBoxAccountName);
			nonDirtyPanel1.Controls.Add(comboBoxAccounts);
			nonDirtyPanel1.Controls.Add(labelCode);
			nonDirtyPanel1.Location = new System.Drawing.Point(0, 34);
			nonDirtyPanel1.Name = "nonDirtyPanel1";
			nonDirtyPanel1.Size = new System.Drawing.Size(445, 60);
			nonDirtyPanel1.TabIndex = 0;
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.Location = new System.Drawing.Point(80, 31);
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(353, 20);
			textBoxAccountName.TabIndex = 18;
			comboBoxAccounts.Assigned = false;
			comboBoxAccounts.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccounts.CustomReportFieldName = "";
			comboBoxAccounts.CustomReportKey = "";
			comboBoxAccounts.CustomReportValueType = 1;
			comboBoxAccounts.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccounts.DisplayLayout.Appearance = appearance;
			comboBoxAccounts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccounts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccounts.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccounts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAccounts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccounts.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAccounts.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccounts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccounts.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccounts.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAccounts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccounts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccounts.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccounts.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAccounts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccounts.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccounts.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAccounts.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAccounts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccounts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccounts.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAccounts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccounts.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxAccounts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccounts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccounts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccounts.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccounts.Editable = true;
			comboBoxAccounts.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccounts.FilterString = "";
			comboBoxAccounts.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccounts.FilterSysDocID = "";
			comboBoxAccounts.HasAllAccount = false;
			comboBoxAccounts.HasCustom = false;
			comboBoxAccounts.IsDataLoaded = false;
			comboBoxAccounts.Location = new System.Drawing.Point(80, 7);
			comboBoxAccounts.MaxDropDownItems = 12;
			comboBoxAccounts.Name = "comboBoxAccounts";
			comboBoxAccounts.ShowInactiveItems = false;
			comboBoxAccounts.ShowQuickAdd = true;
			comboBoxAccounts.Size = new System.Drawing.Size(353, 20);
			comboBoxAccounts.TabIndex = 1;
			comboBoxAccounts.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAccounts.SelectedIndexChanged += new System.EventHandler(comboBoxAccounts_SelectedIndexChanged);
			comboBoxAccounts.ValueChanged += new System.EventHandler(comboBoxAccounts_ValueChanged);
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 11);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(58, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Account:";
			comboBoxAnalysisGroup.Assigned = false;
			comboBoxAnalysisGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAnalysisGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysisGroup.CustomReportFieldName = "";
			comboBoxAnalysisGroup.CustomReportKey = "";
			comboBoxAnalysisGroup.CustomReportValueType = 1;
			comboBoxAnalysisGroup.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysisGroup.DisplayLayout.Appearance = appearance13;
			comboBoxAnalysisGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysisGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysisGroup.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysisGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxAnalysisGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysisGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxAnalysisGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysisGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysisGroup.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysisGroup.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxAnalysisGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysisGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysisGroup.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysisGroup.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxAnalysisGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysisGroup.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysisGroup.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxAnalysisGroup.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxAnalysisGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysisGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysisGroup.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxAnalysisGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysisGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxAnalysisGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysisGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysisGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysisGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysisGroup.Editable = true;
			comboBoxAnalysisGroup.FilterString = "";
			comboBoxAnalysisGroup.HasAllAccount = false;
			comboBoxAnalysisGroup.HasCustom = false;
			comboBoxAnalysisGroup.IsDataLoaded = false;
			comboBoxAnalysisGroup.Location = new System.Drawing.Point(455, 41);
			comboBoxAnalysisGroup.MaxDropDownItems = 12;
			comboBoxAnalysisGroup.Name = "comboBoxAnalysisGroup";
			comboBoxAnalysisGroup.ShowInactiveItems = false;
			comboBoxAnalysisGroup.ShowQuickAdd = true;
			comboBoxAnalysisGroup.Size = new System.Drawing.Size(80, 20);
			comboBoxAnalysisGroup.TabIndex = 20;
			comboBoxAnalysisGroup.Text = "analysisGroupComboBox1";
			comboBoxAnalysisGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAnalysisGroup.Visible = false;
			dataGrid.AllowAddNew = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance25;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance32;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance35;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.Enabled = false;
			dataGrid.IncludeLotItems = false;
			dataGrid.LoadLayoutFailed = false;
			dataGrid.Location = new System.Drawing.Point(12, 125);
			dataGrid.Name = "dataGrid";
			dataGrid.ShowClearMenu = true;
			dataGrid.ShowDeleteMenu = true;
			dataGrid.ShowInsertMenu = true;
			dataGrid.ShowMoveRowsMenu = true;
			dataGrid.Size = new System.Drawing.Size(525, 244);
			dataGrid.TabIndex = 1;
			dataGrid.Text = "dataEntryGrid1";
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(551, 415);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(nonDirtyPanel1);
			base.Controls.Add(comboBoxAnalysisGroup);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGrid);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "AccountAnalysisDetailsForm";
			Text = "Accounts Analysis Setup";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AnalysisGroupDetailsForm_Load);
			base.Validating += new System.ComponentModel.CancelEventHandler(AnalysisGroupDetailsForm_Validating);
			base.Validated += new System.EventHandler(AnalysisGroupDetailsForm_Validated);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			nonDirtyPanel1.ResumeLayout(false);
			nonDirtyPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccounts).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysisGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
