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
	public class ArrivalReportTemplateForm : Form, IForm
	{
		private ArrivalReportTemplateData currentData;

		private const string TABLENAME_CONST = "Arrival_Report_Template";

		private const string IDFIELD_CONST = "TemplateID";

		private bool isNewRecord = true;

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

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private DataEntryGrid dataEntryGrid1;

		private MMTextBox textBoxTemplateName;

		private MMLabel mmLabel5;

		private MMLabel mmLabel7;

		private XPButton buttonSelectTemplatePath;

		private ToolStripButton toolStripButtonInformation;

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

		public ArrivalReportTemplateForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ArrivalReportTemplateForm_Load;
			dataEntryGrid1.AfterCellUpdate += dataEntryGrid1_AfterCellUpdate;
		}

		private void dataEntryGrid1_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Caption")
			{
				if (e.Cell.Value.ToString().Trim() == "")
				{
					e.Cell.Row.Cells["C"].Value = "False";
				}
				else
				{
					e.Cell.Row.Cells["C"].Value = "True";
				}
			}
			else
			{
				_ = (e.Cell.Column.Key == "C");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ArrivalReportTemplateData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ArrivalReportTemplateTable.Rows[0] : currentData.ArrivalReportTemplateTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TemplateID"] = textBoxCode.Text.Trim();
				dataRow["TemplateName"] = textBoxName.Text.Trim();
				dataRow["PrintTemplateName"] = textBoxTemplateName.Text.Trim();
				UltraGridRow ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "Issue1");
				dataRow["Issue1Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				dataRow["Issue1LossPercent"] = (ultraGridRow.Cells["LossPercent"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["LossPercent"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "Issue2");
				dataRow["Issue2Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				dataRow["Issue2LossPercent"] = (ultraGridRow.Cells["LossPercent"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["LossPercent"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "Issue3");
				dataRow["Issue3Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				dataRow["Issue3LossPercent"] = (ultraGridRow.Cells["LossPercent"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["LossPercent"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "Issue4");
				dataRow["Issue4Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				dataRow["Issue4LossPercent"] = (ultraGridRow.Cells["LossPercent"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["LossPercent"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrNum1");
				dataRow["AtrNum1Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrNum2");
				dataRow["AtrNum2Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrNum3");
				dataRow["AtrNum3Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrNum4");
				dataRow["AtrNum4Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrText1");
				dataRow["AtrText1Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrText2");
				dataRow["AtrText2Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrText3");
				dataRow["AtrText3Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "AtrText4");
				dataRow["AtrText4Name"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)ultraGridRow.Cells["Caption"].Value.ToString()));
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "IsBrix");
				dataRow["IsBrix"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((object)false) : ultraGridRow.Cells["C"].Value.ToString());
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "IsPressure");
				dataRow["IsPressure"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((object)false) : ultraGridRow.Cells["C"].Value.ToString());
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "IsGrower");
				dataRow["IsGrower"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((object)false) : ultraGridRow.Cells["C"].Value.ToString());
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "IsDateCode");
				dataRow["IsDateCode"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((object)false) : ultraGridRow.Cells["C"].Value.ToString());
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "IsPalletID");
				dataRow["IsPalletID"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((object)false) : ultraGridRow.Cells["C"].Value.ToString());
				ultraGridRow = dataEntryGrid1.SearchRowByValue("Key", "IsTemperature");
				dataRow["IsTemperature"] = (ultraGridRow.Cells["C"].Value.IsNullOrEmpty() ? ((object)false) : ultraGridRow.Cells["C"].Value.ToString());
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ArrivalReportTemplateTable.Rows.Add(dataRow);
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
					currentData = Factory.ArrivalReportTemplateSystem.GetArrivalReportTemplateByID(id.Trim());
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			textBoxCode.Text = dataRow["TemplateID"].ToString();
			textBoxName.Text = dataRow["TemplateName"].ToString();
			textBoxTemplateName.Text = dataRow["PrintTemplateName"].ToString();
			if (dataRow["Issue1Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue1").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "Issue1").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue1").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "Issue1").Cells["Caption"].Value = dataRow["Issue1Name"].ToString();
				if (!dataRow["Issue1LossPercent"].IsDBNullOrEmpty())
				{
					dataEntryGrid1.SearchRowByValue("Key", "Issue1").Cells["LossPercent"].Value = dataRow["Issue1LossPercent"].ToString();
				}
			}
			if (dataRow["Issue2Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue2").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "Issue2").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue2").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "Issue2").Cells["Caption"].Value = dataRow["Issue2Name"].ToString();
				if (!dataRow["Issue2LossPercent"].IsDBNullOrEmpty())
				{
					dataEntryGrid1.SearchRowByValue("Key", "Issue2").Cells["LossPercent"].Value = dataRow["Issue2LossPercent"].ToString();
				}
			}
			if (dataRow["Issue3Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue3").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "Issue3").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue3").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "Issue3").Cells["Caption"].Value = dataRow["Issue3Name"].ToString();
				if (!dataRow["Issue3LossPercent"].IsDBNullOrEmpty())
				{
					dataEntryGrid1.SearchRowByValue("Key", "Issue3").Cells["LossPercent"].Value = dataRow["Issue3LossPercent"].ToString();
				}
			}
			if (dataRow["Issue4Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue4").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "Issue4").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "Issue4").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "Issue4").Cells["Caption"].Value = dataRow["Issue4Name"].ToString();
				if (!dataRow["Issue4LossPercent"].IsDBNullOrEmpty())
				{
					dataEntryGrid1.SearchRowByValue("Key", "Issue4").Cells["LossPercent"].Value = dataRow["Issue4LossPercent"].ToString();
				}
			}
			if (dataRow["AtrNum1Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum1").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum1").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum1").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum1").Cells["Caption"].Value = dataRow["AtrNum1Name"].ToString();
			}
			if (dataRow["AtrNum2Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum2").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum2").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum2").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum2").Cells["Caption"].Value = dataRow["AtrNum2Name"].ToString();
			}
			if (dataRow["AtrNum3Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum3").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum3").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum3").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum3").Cells["Caption"].Value = dataRow["AtrNum3Name"].ToString();
			}
			if (dataRow["AtrNum4Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum4").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum4").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum4").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrNum4").Cells["Caption"].Value = dataRow["AtrNum4Name"].ToString();
			}
			if (dataRow["AtrText1Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText1").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText1").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText1").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText1").Cells["Caption"].Value = dataRow["AtrText1Name"].ToString();
			}
			if (dataRow["AtrText2Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText2").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText2").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText2").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText2").Cells["Caption"].Value = dataRow["AtrText2Name"].ToString();
			}
			if (dataRow["AtrText3Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText3").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText3").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText3").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText3").Cells["Caption"].Value = dataRow["AtrText3Name"].ToString();
			}
			if (dataRow["AtrText4Name"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText4").Cells["C"].Value = false;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText4").Cells["Caption"].Value = "";
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "AtrText4").Cells["C"].Value = true;
				dataEntryGrid1.SearchRowByValue("Key", "AtrText4").Cells["Caption"].Value = dataRow["AtrText4Name"].ToString();
			}
			if (dataRow["IsBrix"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsBrix").Cells["C"].Value = false;
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsBrix").Cells["C"].Value = true;
			}
			if (dataRow["IsPressure"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsPressure").Cells["C"].Value = false;
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsPressure").Cells["C"].Value = true;
			}
			if (dataRow["IsGrower"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsGrower").Cells["C"].Value = false;
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsGrower").Cells["C"].Value = true;
			}
			if (dataRow["IsDateCode"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsDateCode").Cells["C"].Value = false;
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsDateCode").Cells["C"].Value = true;
			}
			if (dataRow["IsPalletID"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsPalletID").Cells["C"].Value = false;
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsPalletID").Cells["C"].Value = true;
			}
			if (dataRow["IsTemperature"].IsDBNullOrEmpty())
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsTemperature").Cells["C"].Value = false;
			}
			else
			{
				dataEntryGrid1.SearchRowByValue("Key", "IsTemperature").Cells["C"].Value = true;
			}
			if (dataRow["IsInactive"] != DBNull.Value)
			{
				checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
			}
			else
			{
				checkBoxInactive.Checked = false;
			}
			(dataEntryGrid1.DataSource as DataTable).AcceptChanges();
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataSet().Tables.Add("Template");
				dataTable.Columns.Add("C", typeof(bool));
				dataTable.Columns.Add("Key", typeof(string));
				dataTable.Columns.Add("Name", typeof(string));
				dataTable.Columns.Add("Caption", typeof(string));
				dataTable.Columns.Add("LossPercent", typeof(decimal));
				dataEntryGrid1.DataSource = dataTable;
				dataTable.Rows.Add(false, "Issue1", "Issue 1", "");
				dataTable.Rows.Add(false, "Issue2", "Issue 2", "");
				dataTable.Rows.Add(false, "Issue3", "Issue 3", "");
				dataTable.Rows.Add(false, "Issue4", "Issue 4", "");
				dataTable.Rows.Add(false, "AtrNum1", "Numeric Attribute 1", "");
				dataTable.Rows.Add(false, "AtrNum2", "Numeric Attribute 2", "");
				dataTable.Rows.Add(false, "AtrNum3", "Numeric Attribute 3", "");
				dataTable.Rows.Add(false, "AtrNum4", "Numeric Attribute 4", "");
				dataTable.Rows.Add(false, "AtrText1", "Text Attribute 1", "");
				dataTable.Rows.Add(false, "AtrText2", "Text Attribute 2", "");
				dataTable.Rows.Add(false, "AtrText3", "Text Attribute 3", "");
				dataTable.Rows.Add(false, "AtrText4", "Text Attribute 4", "");
				dataTable.Rows.Add(false, "IsBrix", "Brix", "");
				dataTable.Rows.Add(false, "IsPressure", "Pressure", "");
				dataTable.Rows.Add(false, "IsGrower", "Grower", "");
				dataTable.Rows.Add(false, "IsDateCode", "DateCode", "");
				dataTable.Rows.Add(false, "IsPalletID", "PalletID", "");
				dataTable.Rows.Add(false, "IsTemperature", "Temperature", "");
				dataEntryGrid1.SetupUI();
				dataEntryGrid1.DisplayLayout.Bands[0].Columns["LossPercent"].Header.Caption = "Loss%";
				dataEntryGrid1.DisplayLayout.Bands[0].Columns["Key"].Hidden = true;
				dataEntryGrid1.DisplayLayout.Bands[0].Columns["Name"].CellActivation = Activation.NoEdit;
				dataEntryGrid1.DisplayLayout.Bands[0].Columns["Name"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataEntryGrid1.DisplayLayout.Bands[0].Columns["Name"].TabStop = false;
				dataEntryGrid1.AllowAddNew = false;
				dataTable.AcceptChanges();
				foreach (UltraGridRow row in dataEntryGrid1.Rows)
				{
					if (row.Cells["Key"].Value.ToString().StartsWith("Is") && !row.Cells["Key"].Value.ToString().StartsWith("Issue"))
					{
						row.Cells["Caption"].Activation = Activation.NoEdit;
						row.Cells["Caption"].Appearance.BackColor = Color.WhiteSmoke;
					}
					if (!row.Cells["Key"].Value.ToString().StartsWith("Issue"))
					{
						row.Cells["LossPercent"].Activation = Activation.NoEdit;
						row.Cells["LossPercent"].Appearance.BackColor = Color.WhiteSmoke;
					}
				}
			}
			catch (Exception e)
			{
				dataEntryGrid1.LoadLayoutFailed = true;
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
					flag = Factory.ArrivalReportTemplateSystem.CreateArrivalReportTemplate(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.ArrivalReportTemplate, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ArrivalReportTemplateSystem.UpdateArrivalReportTemplate(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Arrival_Report_Template", "TemplateID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Arrival_Report_Template", "TemplateID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Arrival_Report_Template", "TemplateID");
			textBoxName.Clear();
			textBoxTemplateName.Clear();
			foreach (UltraGridRow row in dataEntryGrid1.Rows)
			{
				row.Cells["C"].Value = false;
				row.Cells["Caption"].Value = "";
				row.Cells["LossPercent"].Value = DBNull.Value;
			}
			(dataEntryGrid1.DataSource as DataTable).AcceptChanges();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void ArrivalReportTemplateFormGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ArrivalReportTemplateFormGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.ArrivalReportTemplateSystem.DeleteArrivalReportTemplate(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Arrival_Report_Template", "TemplateID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Arrival_Report_Template", "TemplateID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Arrival_Report_Template", "TemplateID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Arrival_Report_Template", "TemplateID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Arrival_Report_Template", "TemplateID", toolStripTextBoxFind.Text.Trim()))
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

		private void ArrivalReportTemplateForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ArrivalReportTemplate);
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string printTemplatePath = PrintHelper.PrintTemplatePath;
			openFileDialog.InitialDirectory = printTemplatePath + "\\Documents";
			openFileDialog.DefaultExt = "*.repx";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxTemplateName.Text = openFileDialog.SafeFileName.Replace(".repx", "");
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ArrivalReportTemplateForm));
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
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			dataEntryGrid1 = new Micromind.DataControls.DataEntryGrid();
			textBoxTemplateName = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			buttonSelectTemplatePath = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(723, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 428);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(723, 40);
			panelButtons.TabIndex = 6;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(723, 1);
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
			buttonDelete.TabIndex = 6;
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
			xpButton1.Location = new System.Drawing.Point(613, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 7;
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
			buttonNew.TabIndex = 5;
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
			buttonSave.TabIndex = 4;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(297, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
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
			textBoxName.Location = new System.Drawing.Point(129, 58);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(308, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(129, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(162, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(99, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Template Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(96, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Template Code:";
			dataEntryGrid1.AllowAddNew = false;
			dataEntryGrid1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGrid1.DisplayLayout.Appearance = appearance;
			dataEntryGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataEntryGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataEntryGrid1.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGrid1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataEntryGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGrid1.DisplayLayout.Override.CellAppearance = appearance8;
			dataEntryGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGrid1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataEntryGrid1.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataEntryGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataEntryGrid1.DisplayLayout.Override.RowAppearance = appearance11;
			dataEntryGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataEntryGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGrid1.IncludeLotItems = false;
			dataEntryGrid1.LoadLayoutFailed = false;
			dataEntryGrid1.Location = new System.Drawing.Point(12, 125);
			dataEntryGrid1.Name = "dataEntryGrid1";
			dataEntryGrid1.ShowClearMenu = true;
			dataEntryGrid1.ShowDeleteMenu = true;
			dataEntryGrid1.ShowInsertMenu = true;
			dataEntryGrid1.ShowMoveRowsMenu = true;
			dataEntryGrid1.Size = new System.Drawing.Size(697, 297);
			dataEntryGrid1.TabIndex = 17;
			dataEntryGrid1.Text = "dataEntryGrid1";
			textBoxTemplateName.BackColor = System.Drawing.Color.White;
			textBoxTemplateName.CustomReportFieldName = "";
			textBoxTemplateName.CustomReportKey = "";
			textBoxTemplateName.CustomReportValueType = 1;
			textBoxTemplateName.IsComboTextBox = false;
			textBoxTemplateName.IsModified = false;
			textBoxTemplateName.Location = new System.Drawing.Point(129, 84);
			textBoxTemplateName.MaxLength = 64;
			textBoxTemplateName.Name = "textBoxTemplateName";
			textBoxTemplateName.Size = new System.Drawing.Size(217, 20);
			textBoxTemplateName.TabIndex = 120;
			textBoxTemplateName.TabStop = false;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(348, 87);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(30, 13);
			mmLabel5.TabIndex = 121;
			mmLabel5.Text = ".repx";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(14, 87);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(109, 13);
			mmLabel7.TabIndex = 123;
			mmLabel7.Text = "Print Template Name:";
			buttonSelectTemplatePath.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectTemplatePath.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectTemplatePath.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectTemplatePath.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectTemplatePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectTemplatePath.Location = new System.Drawing.Point(380, 83);
			buttonSelectTemplatePath.Name = "buttonSelectTemplatePath";
			buttonSelectTemplatePath.Size = new System.Drawing.Size(25, 21);
			buttonSelectTemplatePath.TabIndex = 122;
			buttonSelectTemplatePath.Text = "...";
			buttonSelectTemplatePath.UseVisualStyleBackColor = false;
			buttonSelectTemplatePath.Click += new System.EventHandler(buttonSelectTemplatePath_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(723, 468);
			base.Controls.Add(textBoxTemplateName);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(buttonSelectTemplatePath);
			base.Controls.Add(dataEntryGrid1);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ArrivalReportTemplateForm";
			Text = "Arrival Report Template";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
