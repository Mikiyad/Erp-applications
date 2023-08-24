using DevExpress.XtraPivotGrid;
using DevExpress.XtraTab;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.DataControls.QueryBuilder;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Charts
{
	public class PivotDetailsForm : Form
	{
		private PivotData currentData;

		private const string TABLENAME_CONST = "Pivot_Report";

		private const string IDFIELD_CONST = "PivotID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private FormManager formManager;

		private XtraTabControl xtraTabControl1;

		private XtraTabPage xtraTabPage1;

		private XtraTabPage xtraTabPage2;

		private XtraTabPage xtraTabPage3;

		private Button buttonRetriveFields;

		private DataGridList dataGridPreview;

		private Button buttonValidateQuery;

		private Button buttonRefreshPreview;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel3;

		private PivotGroupComboBox comboBoxPivotGroup;

		private MMLabel mmLabel2;

		private DataEntryGrid dataGridFields;

		private QueryEditor textBoxQuery;

		private Button buttonValidate;

		private CheckBox checkBoxShowTotal;

		public ScreenAreas ScreenPivotGroup => ScreenAreas.General;

		public int ScreenID => 6001;

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
				textBoxCode.ReadOnly = !value;
			}
		}

		public PivotDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			SetupGrid();
			comboBoxPivotGroup.LoadData();
			if (comboBoxPivotGroup.DisplayLayout.Bands.Count > 0)
			{
				comboBoxPivotGroup.DisplayLayout.Bands[0].Columns["Code"].Hidden = true;
			}
			dataGridPreview.AllowUnfittedView = true;
			dataGridPreview.ApplyUIDesign();
			dataGridPreview.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
			ClearForm();
			textBoxQuery.GotFocus += TextBoxQuery_GotFocus;
			textBoxQuery.LostFocus += TextBoxQuery_LostFocus;
		}

		private void TextBoxQuery_LostFocus(object sender, EventArgs e)
		{
			base.AcceptButton = buttonSave;
		}

		private void TextBoxQuery_GotFocus(object sender, EventArgs e)
		{
			base.AcceptButton = null;
		}

		private void AddEvents()
		{
			base.Load += PivotDetailsForm_Load;
		}

		private void listViewFields_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SetupGrid()
		{
			dataGridFields.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("FieldName");
			dataTable.Columns.Add("Caption");
			dataTable.Columns.Add("Area", typeof(int));
			dataTable.Columns.Add("Interval", typeof(int));
			dataGridFields.DataSource = dataTable;
			dataGridFields.SetupUI();
			dataGridFields.DisplayLayout.Bands[0].Columns["FieldName"].CellActivation = Activation.NoEdit;
			dataGridFields.DisplayLayout.Bands[0].Columns["FieldName"].CellClickAction = CellClickAction.RowSelect;
			dataGridFields.DisplayLayout.Bands[0].Columns["FieldName"].CellAppearance.BackColor = Color.WhiteSmoke;
			ValueList valueList = new ValueList();
			PivotGroupInterval[] array = (PivotGroupInterval[])Enum.GetValues(typeof(PivotGroupInterval));
			for (int i = 0; i < array.Length; i++)
			{
				PivotGroupInterval pivotGroupInterval = array[i];
				valueList.ValueListItems.Add(pivotGroupInterval, pivotGroupInterval.ToString());
			}
			dataGridFields.DisplayLayout.Bands[0].Columns["Interval"].ValueList = valueList;
			valueList = new ValueList();
			PivotArea[] array2 = (PivotArea[])Enum.GetValues(typeof(PivotArea));
			for (int i = 0; i < array2.Length; i++)
			{
				PivotArea pivotArea = array2[i];
				valueList.ValueListItems.Add(pivotArea, pivotArea.ToString());
			}
			dataGridFields.DisplayLayout.Bands[0].Columns["Area"].ValueList = valueList;
			dataGridFields.AllowAddNew = false;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PivotData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PivotTable.Rows[0] : currentData.PivotTable.NewRow();
				dataRow.BeginEdit();
				dataRow["PivotID"] = textBoxCode.Text.Trim();
				dataRow["PivotName"] = textBoxName.Text.Trim();
				dataRow["DataQuery"] = textBoxQuery.Text;
				dataRow["GroupID"] = comboBoxPivotGroup.SelectedID;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PivotTable.Rows.Add(dataRow);
				}
				if (isNewRecord || currentData.Tables[1].Rows.Count == 0)
				{
					foreach (UltraGridRow row in dataGridFields.Rows)
					{
						DataRow dataRow2 = currentData.Tables[1].NewRow();
						dataRow2["PivotID"] = textBoxCode.Text;
						dataRow2["FieldName"] = "field" + row.Cells["FieldName"].Value.ToString();
						dataRow2["GroupInterval"] = (int)((!(row.Cells["Interval"].Value.ToString() == string.Empty)) ? ((PivotGroupInterval)row.Cells["Interval"].Value) : PivotGroupInterval.Default);
						dataRow2["Area"] = ((!(row.Cells["Area"].Value.ToString() == string.Empty)) ? int.Parse(row.Cells["Area"].Value.ToString()) : 0);
						currentData.Tables[1].Rows.Add(dataRow2);
					}
				}
				else
				{
					foreach (DataRow row2 in currentData.Tables[1].Rows)
					{
						string text = row2["FieldName"].ToString().Substring(5);
						foreach (UltraGridRow row3 in dataGridFields.Rows)
						{
							if (row3.Cells["FieldName"].Value.ToString().ToLower() == text.ToLower())
							{
								if (row3.Cells["Interval"].Value != null && row3.Cells["Interval"].Value.ToString() != "")
								{
									row2["GroupInterval"] = (int)(PivotGroupInterval)row3.Cells["Interval"].Value;
								}
								else
								{
									row2["GroupInterval"] = 0;
								}
								if (row3.Cells["Area"].Value != null && row3.Cells["Area"].Value.ToString() != "")
								{
									row2["Area"] = (int)(PivotArea)row3.Cells["Area"].Value;
								}
								else
								{
									row2["Area"] = 2;
								}
								if (row3.Cells["Caption"].Value != null)
								{
									row2["DisplayName"] = row3.Cells["Caption"].Value.ToString();
								}
								break;
							}
						}
						row2.AcceptChanges();
						row2.SetAdded();
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
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!(id.Trim() == "") && CanClose())
				{
					currentData = Factory.PivotSystem.GetPivotByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
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
				textBoxName.Text = dataRow["PivotName"].ToString();
				textBoxCode.Text = dataRow["PivotID"].ToString();
				textBoxQuery.Text = dataRow["DataQuery"].ToString();
				comboBoxPivotGroup.SelectedID = dataRow["GroupID"].ToString();
				bool @checked = false;
				checkBoxShowTotal.Checked = @checked;
				DataTable dataTable = dataGridFields.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables[1].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["FieldName"] = row["FieldName"].ToString().Substring(5);
					if (row["Area"] != DBNull.Value)
					{
						dataRow3["Area"] = (PivotArea)int.Parse(row["Area"].ToString());
					}
					else
					{
						dataRow3["Area"] = PivotArea.FilterArea;
					}
					if (row["GroupInterval"] != DBNull.Value)
					{
						dataRow3["Interval"] = (PivotGroupInterval)int.Parse(row["GroupInterval"].ToString());
					}
					else
					{
						dataRow3["Interval"] = PivotGroupInterval.Default;
					}
					dataTable.Rows.Add(dataRow3);
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
					flag = Factory.PivotSystem.InsertUpdatePivot(currentData, isUpdate: false);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.PivotGroup, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.PivotSystem.InsertUpdatePivot(currentData, isUpdate: true);
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
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || comboBoxPivotGroup.SelectedID == "")
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
			textBoxName.Clear();
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Pivot_Report", "PivotID");
			textBoxQuery.Clear();
			checkBoxShowTotal.Checked = false;
			dataGridPreview.DataSource = null;
			formManager.ResetDirty();
			(dataGridFields.DataSource as DataTable)?.Rows.Clear();
		}

		private void PivotGroupGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void PivotGroupGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
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

		private void PivotDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
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
			new FormHelper().ShowList(DataComboType.PivotGroup);
		}

		private void mmTextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void mmLabel2_Click(object sender, EventArgs e)
		{
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
		}

		private void mmLabel1_Click(object sender, EventArgs e)
		{
		}

		private void buttonRetriveFields_Click(object sender, EventArgs e)
		{
			LoadReport();
		}

		private void LoadReport()
		{
			try
			{
				DataSet reportByQuery = Factory.SmartListSystem.GetReportByQuery(textBoxQuery.Text, DateTime.MinValue, DateTime.MaxValue);
				DataTable dataTable = dataGridFields.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (reportByQuery != null && reportByQuery.Tables.Count > 0)
				{
					foreach (DataColumn column in reportByQuery.Tables[0].Columns)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["FieldName"] = column.ColumnName;
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void PivotDetailsForm_Load_1(object sender, EventArgs e)
		{
		}

		private void buttonRefreshPreview_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBoxQuery.Text.Trim() == "")
				{
					dataGridPreview.DataSource = null;
				}
				else
				{
					DataSet reportByQuery = Factory.SmartListSystem.GetReportByQuery(textBoxQuery.Text, DateTime.MinValue, DateTime.MaxValue);
					dataGridPreview.DataSource = reportByQuery;
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.WarningMessage("Error in Query:", ex.Message);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonValidate_Click(object sender, EventArgs e)
		{
			try
			{
				Factory.SmartListSystem.GetReportByQuery(textBoxQuery.Text, DateTime.Now, DateTime.Now);
				ErrorHelper.InformationMessage("Query executed successfully.");
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage("Wrong query.", ex.Message);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Charts.PivotDetailsForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonValidate = new System.Windows.Forms.Button();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			dataGridFields = new Micromind.DataControls.DataEntryGrid();
			buttonRetriveFields = new System.Windows.Forms.Button();
			xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			textBoxQuery = new Micromind.DataControls.QueryBuilder.QueryEditor();
			buttonValidateQuery = new System.Windows.Forms.Button();
			xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
			buttonRefreshPreview = new System.Windows.Forms.Button();
			dataGridPreview = new Micromind.UISupport.DataGridList(components);
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			comboBoxPivotGroup = new Micromind.DataControls.PivotGroupComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			checkBoxShowTotal = new System.Windows.Forms.CheckBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
			xtraTabControl1.SuspendLayout();
			xtraTabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridFields).BeginInit();
			xtraTabPage2.SuspendLayout();
			xtraTabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPreview).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPivotGroup).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonValidate);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 586);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(948, 40);
			panelButtons.TabIndex = 11;
			buttonValidate.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonValidate.Location = new System.Drawing.Point(29, 6);
			buttonValidate.Name = "buttonValidate";
			buttonValidate.Size = new System.Drawing.Size(104, 29);
			buttonValidate.TabIndex = 15;
			buttonValidate.Text = "&Validate Query";
			buttonValidate.UseVisualStyleBackColor = true;
			buttonValidate.Click += new System.EventHandler(buttonValidate_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(948, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(841, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(739, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 46);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Report Name:";
			mmLabel1.Click += new System.EventHandler(mmLabel1_Click);
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(100, 44);
			textBoxName.MaxLength = 32;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(391, 20);
			textBoxName.TabIndex = 2;
			textBoxName.TextChanged += new System.EventHandler(textBoxName_TextChanged);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			xtraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			xtraTabControl1.Location = new System.Drawing.Point(15, 123);
			xtraTabControl1.Name = "xtraTabControl1";
			xtraTabControl1.SelectedTabPage = xtraTabPage1;
			xtraTabControl1.Size = new System.Drawing.Size(922, 457);
			xtraTabControl1.TabIndex = 19;
			xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[3]
			{
				xtraTabPage1,
				xtraTabPage2,
				xtraTabPage3
			});
			xtraTabPage1.Controls.Add(dataGridFields);
			xtraTabPage1.Controls.Add(buttonRetriveFields);
			xtraTabPage1.Name = "xtraTabPage1";
			xtraTabPage1.Size = new System.Drawing.Size(916, 429);
			xtraTabPage1.Text = "General";
			dataGridFields.AllowAddNew = false;
			dataGridFields.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridFields.DisplayLayout.Appearance = appearance;
			dataGridFields.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridFields.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridFields.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridFields.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridFields.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridFields.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridFields.DisplayLayout.MaxColScrollRegions = 1;
			dataGridFields.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridFields.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridFields.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridFields.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridFields.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridFields.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridFields.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridFields.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridFields.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridFields.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridFields.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridFields.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridFields.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridFields.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridFields.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridFields.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridFields.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridFields.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridFields.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridFields.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridFields.IncludeLotItems = false;
			dataGridFields.LoadLayoutFailed = false;
			dataGridFields.Location = new System.Drawing.Point(15, 53);
			dataGridFields.Name = "dataGridFields";
			dataGridFields.ShowClearMenu = true;
			dataGridFields.ShowDeleteMenu = true;
			dataGridFields.ShowInsertMenu = true;
			dataGridFields.ShowMoveRowsMenu = true;
			dataGridFields.Size = new System.Drawing.Size(885, 363);
			dataGridFields.TabIndex = 25;
			dataGridFields.Text = "dataEntryGrid1";
			buttonRetriveFields.Location = new System.Drawing.Point(15, 16);
			buttonRetriveFields.Name = "buttonRetriveFields";
			buttonRetriveFields.Size = new System.Drawing.Size(126, 31);
			buttonRetriveFields.TabIndex = 0;
			buttonRetriveFields.Text = "Retrive Fields";
			buttonRetriveFields.UseVisualStyleBackColor = true;
			buttonRetriveFields.Click += new System.EventHandler(buttonRetriveFields_Click);
			xtraTabPage2.Controls.Add(textBoxQuery);
			xtraTabPage2.Controls.Add(buttonValidateQuery);
			xtraTabPage2.Name = "xtraTabPage2";
			xtraTabPage2.Size = new System.Drawing.Size(916, 429);
			xtraTabPage2.Text = "Query";
			textBoxQuery.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxQuery.BackColor = System.Drawing.Color.White;
			textBoxQuery.DetectUrls = false;
			textBoxQuery.FirstVisibleLine = 1;
			textBoxQuery.HideSelection = false;
			textBoxQuery.Location = new System.Drawing.Point(11, 11);
			textBoxQuery.Name = "textBoxQuery";
			textBoxQuery.Size = new System.Drawing.Size(893, 405);
			textBoxQuery.TabIndex = 4;
			textBoxQuery.Text = "";
			textBoxQuery.TextModified = false;
			buttonValidateQuery.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonValidateQuery.Location = new System.Drawing.Point(778, 437);
			buttonValidateQuery.Name = "buttonValidateQuery";
			buttonValidateQuery.Size = new System.Drawing.Size(126, 31);
			buttonValidateQuery.TabIndex = 3;
			buttonValidateQuery.Text = "Validate Query";
			buttonValidateQuery.UseVisualStyleBackColor = true;
			xtraTabPage3.Controls.Add(buttonRefreshPreview);
			xtraTabPage3.Controls.Add(dataGridPreview);
			xtraTabPage3.Name = "xtraTabPage3";
			xtraTabPage3.Size = new System.Drawing.Size(916, 429);
			xtraTabPage3.Text = "Data Preview";
			buttonRefreshPreview.Location = new System.Drawing.Point(15, 7);
			buttonRefreshPreview.Name = "buttonRefreshPreview";
			buttonRefreshPreview.Size = new System.Drawing.Size(126, 31);
			buttonRefreshPreview.TabIndex = 1;
			buttonRefreshPreview.Text = "Refresh Data";
			buttonRefreshPreview.UseVisualStyleBackColor = true;
			buttonRefreshPreview.Click += new System.EventHandler(buttonRefreshPreview_Click);
			dataGridPreview.AllowUnfittedView = false;
			dataGridPreview.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPreview.DisplayLayout.Appearance = appearance13;
			dataGridPreview.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPreview.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPreview.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPreview.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridPreview.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPreview.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridPreview.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPreview.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPreview.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPreview.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridPreview.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPreview.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridPreview.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPreview.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridPreview.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPreview.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPreview.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridPreview.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridPreview.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPreview.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridPreview.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridPreview.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPreview.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridPreview.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPreview.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPreview.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPreview.LoadLayoutFailed = false;
			dataGridPreview.Location = new System.Drawing.Point(13, 41);
			dataGridPreview.Name = "dataGridPreview";
			dataGridPreview.ShowDeleteMenu = false;
			dataGridPreview.ShowMinusInRed = true;
			dataGridPreview.ShowNewMenu = false;
			dataGridPreview.Size = new System.Drawing.Size(890, 378);
			dataGridPreview.TabIndex = 0;
			dataGridPreview.Text = "dataGridList1";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(100, 22);
			textBoxCode.MaxLength = 32;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(391, 20);
			textBoxCode.TabIndex = 1;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(12, 24);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(82, 13);
			mmLabel3.TabIndex = 20;
			mmLabel3.Text = "Report Code:";
			comboBoxPivotGroup.Assigned = false;
			comboBoxPivotGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPivotGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPivotGroup.CustomReportFieldName = "";
			comboBoxPivotGroup.CustomReportKey = "";
			comboBoxPivotGroup.CustomReportValueType = 1;
			comboBoxPivotGroup.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPivotGroup.DisplayLayout.Appearance = appearance25;
			comboBoxPivotGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPivotGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPivotGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxPivotGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPivotGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPivotGroup.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPivotGroup.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxPivotGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPivotGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPivotGroup.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxPivotGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPivotGroup.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPivotGroup.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxPivotGroup.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxPivotGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPivotGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxPivotGroup.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxPivotGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPivotGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxPivotGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPivotGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPivotGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPivotGroup.DisplayMember = "Name";
			comboBoxPivotGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPivotGroup.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxPivotGroup.Editable = true;
			comboBoxPivotGroup.FilterString = "";
			comboBoxPivotGroup.HasAllAccount = false;
			comboBoxPivotGroup.HasCustom = false;
			comboBoxPivotGroup.IsDataLoaded = false;
			comboBoxPivotGroup.Location = new System.Drawing.Point(100, 68);
			comboBoxPivotGroup.MaxDropDownItems = 12;
			comboBoxPivotGroup.Name = "comboBoxPivotGroup";
			comboBoxPivotGroup.ShowInactiveItems = false;
			comboBoxPivotGroup.ShowQuickAdd = true;
			comboBoxPivotGroup.Size = new System.Drawing.Size(177, 20);
			comboBoxPivotGroup.TabIndex = 3;
			comboBoxPivotGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(12, 70);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(45, 13);
			mmLabel2.TabIndex = 23;
			mmLabel2.Text = "Group:";
			checkBoxShowTotal.AutoSize = true;
			checkBoxShowTotal.Location = new System.Drawing.Point(409, 71);
			checkBoxShowTotal.Name = "checkBoxShowTotal";
			checkBoxShowTotal.Size = new System.Drawing.Size(75, 17);
			checkBoxShowTotal.TabIndex = 24;
			checkBoxShowTotal.Text = "Hide Total";
			checkBoxShowTotal.UseVisualStyleBackColor = true;
			checkBoxShowTotal.Visible = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(948, 626);
			base.Controls.Add(checkBoxShowTotal);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(comboBoxPivotGroup);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(xtraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PivotDetailsForm";
			Text = "Pivot Report";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(PivotDetailsForm_Load_1);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
			xtraTabControl1.ResumeLayout(false);
			xtraTabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridFields).EndInit();
			xtraTabPage2.ResumeLayout(false);
			xtraTabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPreview).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPivotGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
