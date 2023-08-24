using Infragistics.Win;
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
	public class MatrixTemplateDetailsForm : Form, IForm
	{
		private MatrixTemplateData currentData;

		private const string TABLENAME_CONST = "Matrix_Template";

		private const string IDFIELD_CONST = "TemplateID";

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

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private Panel panelDimension1;

		private DataEntryGrid dataGridDimension1;

		private Label label1;

		private DimensionComboBox comboBoxDimension1;

		private Panel panelDimension2;

		private DataEntryGrid dataGridDimension2;

		private Label label2;

		private DimensionComboBox comboBoxDimension2;

		private Panel panelDimension3;

		private DataEntryGrid dataGridDimension3;

		private Label label3;

		private DimensionComboBox comboBoxDimension3;

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

		public MatrixTemplateDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridDimension1.SetupUI();
			dataGridDimension2.SetupUI();
			dataGridDimension3.SetupUI();
			dataGridDimension1.DisplayLayout.Bands[0].ColHeadersVisible = false;
			dataGridDimension2.DisplayLayout.Bands[0].ColHeadersVisible = false;
			dataGridDimension3.DisplayLayout.Bands[0].ColHeadersVisible = false;
			dataGridDimension1.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect;
			dataGridDimension2.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect;
			dataGridDimension3.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect;
		}

		private void AddEvents()
		{
			base.Load += MatrixTemplateDetailsForm_Load;
			comboBoxDimension1.SelectedIndexChanged += comboBoxDimension1_SelectedIndexChanged;
			comboBoxDimension2.SelectedIndexChanged += comboBoxDimension2_SelectedIndexChanged;
			comboBoxDimension3.SelectedIndexChanged += comboBoxDimension3_SelectedIndexChanged;
		}

		private void comboBoxDimension3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDimension3.SelectedID == "")
			{
				dataGridDimension3.DataSource = null;
				return;
			}
			DataSet dimensionAttributes = Factory.DimensionSystem.GetDimensionAttributes(comboBoxDimension3.SelectedID);
			dataGridDimension3.DataSource = dimensionAttributes;
		}

		private void comboBoxDimension2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDimension2.SelectedID == "")
			{
				dataGridDimension2.DataSource = null;
			}
			else
			{
				DataSet dimensionAttributes = Factory.DimensionSystem.GetDimensionAttributes(comboBoxDimension2.SelectedID);
				dataGridDimension2.DataSource = dimensionAttributes;
			}
			panelDimension3.Enabled = (comboBoxDimension2.SelectedID != "");
		}

		private void comboBoxDimension1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDimension1.SelectedID == "")
			{
				dataGridDimension1.DataSource = null;
			}
			else
			{
				DataSet dimensionAttributes = Factory.DimensionSystem.GetDimensionAttributes(comboBoxDimension1.SelectedID);
				dataGridDimension1.DataSource = dimensionAttributes;
				dataGridDimension3.DisplayLayout.Bands[0].HeaderVisible = false;
			}
			panelDimension2.Enabled = (comboBoxDimension1.SelectedID != "");
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new MatrixTemplateData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.MatrixTemplateTable.Rows[0] : currentData.MatrixTemplateTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TemplateID"] = textBoxCode.Text.Trim();
				dataRow["TemplateName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.MatrixTemplateTable.Rows.Add(dataRow);
				}
				if (!(comboBoxDimension3.SelectedID != ""))
				{
					_ = (comboBoxDimension2.SelectedID != "");
				}
				DataTable matrixTemplateDetailTable = currentData.MatrixTemplateDetailTable;
				matrixTemplateDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridDimension1.Rows)
				{
					dataRow = matrixTemplateDetailTable.NewRow();
					dataRow["TemplateID"] = textBoxCode.Text;
					dataRow["Dimension"] = 1;
					dataRow["DimensionID"] = comboBoxDimension1.SelectedID;
					dataRow["AttributeID"] = row.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row.Cells["AttributeName"].Value.ToString();
					dataRow["RowIndex"] = row.Index;
					dataRow.EndEdit();
					matrixTemplateDetailTable.Rows.Add(dataRow);
				}
				foreach (UltraGridRow row2 in dataGridDimension2.Rows)
				{
					dataRow = matrixTemplateDetailTable.NewRow();
					dataRow["TemplateID"] = textBoxCode.Text;
					dataRow["Dimension"] = 2;
					dataRow["DimensionID"] = comboBoxDimension2.SelectedID;
					dataRow["AttributeID"] = row2.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row2.Cells["AttributeName"].Value.ToString();
					dataRow["RowIndex"] = row2.Index;
					dataRow.EndEdit();
					matrixTemplateDetailTable.Rows.Add(dataRow);
				}
				foreach (UltraGridRow row3 in dataGridDimension3.Rows)
				{
					dataRow = matrixTemplateDetailTable.NewRow();
					dataRow["TemplateID"] = textBoxCode.Text;
					dataRow["Dimension"] = 3;
					dataRow["DimensionID"] = comboBoxDimension3.SelectedID;
					dataRow["AttributeID"] = row3.Cells["AttributeID"].Value.ToString();
					dataRow["AttributeName"] = row3.Cells["AttributeName"].Value.ToString();
					dataRow["RowIndex"] = row3.Index;
					dataRow.EndEdit();
					matrixTemplateDetailTable.Rows.Add(dataRow);
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
					currentData = Factory.MatrixTemplateSystem.GetMatrixTemplateByID(id.Trim());
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
				ClearForm();
			}
		}

		private void FillData()
		{
			checked
			{
				try
				{
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
					{
						DataRow dataRow = currentData.Tables[0].Rows[0];
						textBoxCode.Text = dataRow["TemplateID"].ToString();
						textBoxName.Text = dataRow["TemplateName"].ToString();
						textBoxNote.Text = dataRow["Note"].ToString();
						checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
						if (currentData.Tables.Contains("Matrix_Template_Detail") && currentData.Tables["Matrix_Template_Detail"].Rows.Count > 0)
						{
							DataTable dataTable = currentData.Tables["Matrix_Template_Detail"];
							DataRow[] array = dataTable.Select("Dimension=1");
							if (array.Length != 0)
							{
								comboBoxDimension1.SelectedID = array[0]["DimensionID"].ToString();
								DataTable dataTable2 = new DataTable();
								dataTable2.Columns.Add("AttributeID");
								dataTable2.Columns.Add("AttributeName");
								for (int i = 0; i < array.Length; i++)
								{
									dataTable2.Rows.Add(array[i]["AttributeID"].ToString(), array[i]["AttributeName"].ToString());
								}
							}
							array = dataTable.Select("Dimension=2");
							if (array.Length != 0)
							{
								comboBoxDimension2.SelectedID = array[0]["DimensionID"].ToString();
								DataTable dataTable3 = new DataTable();
								dataTable3.Columns.Add("AttributeID");
								dataTable3.Columns.Add("AttributeName");
								for (int j = 0; j < array.Length; j++)
								{
									dataTable3.Rows.Add(array[j]["AttributeID"].ToString(), array[j]["AttributeName"].ToString());
								}
							}
							array = dataTable.Select("Dimension=3");
							if (array.Length != 0)
							{
								comboBoxDimension3.SelectedID = array[0]["DimensionID"].ToString();
								DataTable dataTable4 = new DataTable();
								dataTable4.Columns.Add("AttributeID");
								dataTable4.Columns.Add("AttributeName");
								for (int k = 0; k < array.Length; k++)
								{
									dataTable4.Rows.Add(array[k]["AttributeID"].ToString(), array[k]["AttributeName"].ToString());
								}
							}
						}
					}
				}
				catch
				{
					throw;
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
					flag = Factory.MatrixTemplateSystem.CreateMatrixTemplate(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.MatrixTemplate, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.MatrixTemplateSystem.UpdateMatrixTemplate(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Matrix_Template", "TemplateID", textBoxCode.Text.Trim()))
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
			textBoxNote.Clear();
			comboBoxDimension1.Clear();
			comboBoxDimension2.Clear();
			comboBoxDimension3.Clear();
			dataGridDimension1.DataSource = null;
			dataGridDimension2.DataSource = null;
			dataGridDimension3.DataSource = null;
			Panel panel = panelDimension3;
			bool enabled = panelDimension2.Enabled = false;
			panel.Enabled = enabled;
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void MatrixTemplateGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void MatrixTemplateGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.MatrixTemplateSystem.DeleteMatrixTemplate(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.MatrixTemplate, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Matrix_Template", "TemplateID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Matrix_Template", "TemplateID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Matrix_Template", "TemplateID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Matrix_Template", "TemplateID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Matrix_Template", "TemplateID", toolStripTextBoxFind.Text.Trim()))
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

		private void MatrixTemplateDetailsForm_Load(object sender, EventArgs e)
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.MatrixTemplate);
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
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.MatrixTemplateDetailsForm));
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
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			panelDimension1 = new System.Windows.Forms.Panel();
			dataGridDimension1 = new Micromind.DataControls.DataEntryGrid();
			label1 = new System.Windows.Forms.Label();
			comboBoxDimension1 = new Micromind.DataControls.DimensionComboBox();
			panelDimension2 = new System.Windows.Forms.Panel();
			dataGridDimension2 = new Micromind.DataControls.DataEntryGrid();
			label2 = new System.Windows.Forms.Label();
			comboBoxDimension2 = new Micromind.DataControls.DimensionComboBox();
			panelDimension3 = new System.Windows.Forms.Panel();
			dataGridDimension3 = new Micromind.DataControls.DataEntryGrid();
			label3 = new System.Windows.Forms.Label();
			comboBoxDimension3 = new Micromind.DataControls.DimensionComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDimension1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension1).BeginInit();
			panelDimension2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension2).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension2).BeginInit();
			panelDimension3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension3).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension3).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
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
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(758, 31);
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 371);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(758, 40);
			panelButtons.TabIndex = 7;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(758, 1);
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
			xpButton1.Location = new System.Drawing.Point(648, 8);
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
			labelCode.Location = new System.Drawing.Point(8, 38);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(96, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Template Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(106, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(219, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(99, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Template Name:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(106, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(364, 20);
			textBoxName.TabIndex = 2;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 81);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(106, 80);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(364, 20);
			textBoxNote.TabIndex = 3;
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
			checkBoxInactive.Location = new System.Drawing.Point(331, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			panelDimension1.Controls.Add(dataGridDimension1);
			panelDimension1.Controls.Add(label1);
			panelDimension1.Controls.Add(comboBoxDimension1);
			panelDimension1.Location = new System.Drawing.Point(10, 110);
			panelDimension1.Name = "panelDimension1";
			panelDimension1.Size = new System.Drawing.Size(248, 222);
			panelDimension1.TabIndex = 4;
			dataGridDimension1.AllowAddNew = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDimension1.DisplayLayout.Appearance = appearance;
			dataGridDimension1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDimension1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridDimension1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridDimension1.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDimension1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDimension1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDimension1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridDimension1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDimension1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDimension1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridDimension1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDimension1.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridDimension1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDimension1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridDimension1.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridDimension1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDimension1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridDimension1.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridDimension1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDimension1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridDimension1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDimension1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDimension1.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridDimension1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDimension1.ExitEditModeOnLeave = false;
			dataGridDimension1.Location = new System.Drawing.Point(11, 53);
			dataGridDimension1.Name = "dataGridDimension1";
			dataGridDimension1.ShowClearMenu = true;
			dataGridDimension1.ShowDeleteMenu = true;
			dataGridDimension1.ShowInsertMenu = true;
			dataGridDimension1.ShowMoveRowsMenu = true;
			dataGridDimension1.Size = new System.Drawing.Size(230, 161);
			dataGridDimension1.TabIndex = 22;
			dataGridDimension1.Text = "dataEntryGrid1";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(10, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(68, 13);
			label1.TabIndex = 21;
			label1.Text = "Dimension 1:";
			comboBoxDimension1.Assigned = false;
			comboBoxDimension1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDimension1.CustomReportFieldName = "";
			comboBoxDimension1.CustomReportKey = "";
			comboBoxDimension1.CustomReportValueType = 1;
			comboBoxDimension1.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDimension1.DisplayLayout.Appearance = appearance13;
			comboBoxDimension1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDimension1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension1.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxDimension1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension1.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxDimension1.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDimension1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDimension1.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDimension1.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxDimension1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDimension1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDimension1.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDimension1.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxDimension1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDimension1.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension1.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxDimension1.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxDimension1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDimension1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxDimension1.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxDimension1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDimension1.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxDimension1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDimension1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDimension1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDimension1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDimension1.Editable = true;
			comboBoxDimension1.FilterString = "";
			comboBoxDimension1.HasAllAccount = false;
			comboBoxDimension1.HasCustom = false;
			comboBoxDimension1.IsDataLoaded = false;
			comboBoxDimension1.Location = new System.Drawing.Point(13, 27);
			comboBoxDimension1.MaxDropDownItems = 12;
			comboBoxDimension1.Name = "comboBoxDimension1";
			comboBoxDimension1.ShowInactiveItems = false;
			comboBoxDimension1.ShowQuickAdd = true;
			comboBoxDimension1.Size = new System.Drawing.Size(158, 20);
			comboBoxDimension1.TabIndex = 20;
			comboBoxDimension1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelDimension2.Controls.Add(dataGridDimension2);
			panelDimension2.Controls.Add(label2);
			panelDimension2.Controls.Add(comboBoxDimension2);
			panelDimension2.Enabled = false;
			panelDimension2.Location = new System.Drawing.Point(258, 110);
			panelDimension2.Name = "panelDimension2";
			panelDimension2.Size = new System.Drawing.Size(251, 222);
			panelDimension2.TabIndex = 5;
			dataGridDimension2.AllowAddNew = false;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDimension2.DisplayLayout.Appearance = appearance25;
			dataGridDimension2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDimension2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension2.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridDimension2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension2.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridDimension2.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDimension2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDimension2.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDimension2.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridDimension2.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDimension2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDimension2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridDimension2.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDimension2.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridDimension2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDimension2.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension2.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridDimension2.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridDimension2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDimension2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridDimension2.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridDimension2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDimension2.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridDimension2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDimension2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDimension2.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridDimension2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDimension2.ExitEditModeOnLeave = false;
			dataGridDimension2.Location = new System.Drawing.Point(9, 52);
			dataGridDimension2.Name = "dataGridDimension2";
			dataGridDimension2.ShowClearMenu = true;
			dataGridDimension2.ShowDeleteMenu = true;
			dataGridDimension2.ShowInsertMenu = true;
			dataGridDimension2.ShowMoveRowsMenu = true;
			dataGridDimension2.Size = new System.Drawing.Size(230, 161);
			dataGridDimension2.TabIndex = 22;
			dataGridDimension2.Text = "dataEntryGrid1";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(6, 7);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(68, 13);
			label2.TabIndex = 21;
			label2.Text = "Dimension 2:";
			comboBoxDimension2.Assigned = false;
			comboBoxDimension2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDimension2.CustomReportFieldName = "";
			comboBoxDimension2.CustomReportKey = "";
			comboBoxDimension2.CustomReportValueType = 1;
			comboBoxDimension2.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDimension2.DisplayLayout.Appearance = appearance37;
			comboBoxDimension2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDimension2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension2.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxDimension2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension2.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxDimension2.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDimension2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDimension2.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDimension2.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxDimension2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDimension2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDimension2.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDimension2.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxDimension2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDimension2.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension2.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxDimension2.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxDimension2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDimension2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxDimension2.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxDimension2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDimension2.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxDimension2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDimension2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDimension2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDimension2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDimension2.Editable = true;
			comboBoxDimension2.FilterString = "";
			comboBoxDimension2.HasAllAccount = false;
			comboBoxDimension2.HasCustom = false;
			comboBoxDimension2.IsDataLoaded = false;
			comboBoxDimension2.Location = new System.Drawing.Point(9, 26);
			comboBoxDimension2.MaxDropDownItems = 12;
			comboBoxDimension2.Name = "comboBoxDimension2";
			comboBoxDimension2.ShowInactiveItems = false;
			comboBoxDimension2.ShowQuickAdd = true;
			comboBoxDimension2.Size = new System.Drawing.Size(160, 20);
			comboBoxDimension2.TabIndex = 20;
			comboBoxDimension2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelDimension3.Controls.Add(dataGridDimension3);
			panelDimension3.Controls.Add(label3);
			panelDimension3.Controls.Add(comboBoxDimension3);
			panelDimension3.Enabled = false;
			panelDimension3.Location = new System.Drawing.Point(510, 110);
			panelDimension3.Name = "panelDimension3";
			panelDimension3.Size = new System.Drawing.Size(245, 222);
			panelDimension3.TabIndex = 6;
			dataGridDimension3.AllowAddNew = false;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDimension3.DisplayLayout.Appearance = appearance49;
			dataGridDimension3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDimension3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension3.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridDimension3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDimension3.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridDimension3.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDimension3.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDimension3.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDimension3.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridDimension3.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDimension3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDimension3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridDimension3.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDimension3.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridDimension3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDimension3.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDimension3.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridDimension3.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridDimension3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDimension3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridDimension3.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridDimension3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDimension3.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridDimension3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDimension3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDimension3.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridDimension3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDimension3.ExitEditModeOnLeave = false;
			dataGridDimension3.Location = new System.Drawing.Point(3, 53);
			dataGridDimension3.Name = "dataGridDimension3";
			dataGridDimension3.ShowClearMenu = true;
			dataGridDimension3.ShowDeleteMenu = true;
			dataGridDimension3.ShowInsertMenu = true;
			dataGridDimension3.ShowMoveRowsMenu = true;
			dataGridDimension3.Size = new System.Drawing.Size(230, 161);
			dataGridDimension3.TabIndex = 22;
			dataGridDimension3.Text = "dataEntryGrid1";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(0, 8);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(68, 13);
			label3.TabIndex = 21;
			label3.Text = "Dimension 3:";
			comboBoxDimension3.Assigned = false;
			comboBoxDimension3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDimension3.CustomReportFieldName = "";
			comboBoxDimension3.CustomReportKey = "";
			comboBoxDimension3.CustomReportValueType = 1;
			comboBoxDimension3.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDimension3.DisplayLayout.Appearance = appearance61;
			comboBoxDimension3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDimension3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension3.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxDimension3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDimension3.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxDimension3.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDimension3.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDimension3.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDimension3.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxDimension3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDimension3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDimension3.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDimension3.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxDimension3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDimension3.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDimension3.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxDimension3.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxDimension3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDimension3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxDimension3.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxDimension3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDimension3.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxDimension3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDimension3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDimension3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDimension3.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDimension3.Editable = true;
			comboBoxDimension3.FilterString = "";
			comboBoxDimension3.HasAllAccount = false;
			comboBoxDimension3.HasCustom = false;
			comboBoxDimension3.IsDataLoaded = false;
			comboBoxDimension3.Location = new System.Drawing.Point(3, 27);
			comboBoxDimension3.MaxDropDownItems = 12;
			comboBoxDimension3.Name = "comboBoxDimension3";
			comboBoxDimension3.ShowInactiveItems = false;
			comboBoxDimension3.ShowQuickAdd = true;
			comboBoxDimension3.Size = new System.Drawing.Size(158, 20);
			comboBoxDimension3.TabIndex = 20;
			comboBoxDimension3.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(758, 411);
			base.Controls.Add(panelDimension1);
			base.Controls.Add(panelDimension2);
			base.Controls.Add(panelDimension3);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "MatrixTemplateDetailsForm";
			Text = "Matrix Template";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDimension1.ResumeLayout(false);
			panelDimension1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension1).EndInit();
			panelDimension2.ResumeLayout(false);
			panelDimension2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension2).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension2).EndInit();
			panelDimension3.ResumeLayout(false);
			panelDimension3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridDimension3).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDimension3).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
