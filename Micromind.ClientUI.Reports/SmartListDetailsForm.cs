using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.Reports.CustomReports;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class SmartListDetailsForm : Form, IForm
	{
		public string CategoryID = "";

		private SmartListData currentData;

		private CustomReport currentReport;

		private ExternalReportData currentExtData;

		private const string TABLENAME_CONST = "SmartList";

		private const string IDFIELD_CONST = "SmartListID";

		private bool isNewRecord = true;

		private string setreport = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private MMTextBox textBoxName;

		private FormManager formManager;

		private SmartListCategoryComboBox comboBoxCategory;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private Button buttonDeleteParameter;

		private Button buttonEditParameter;

		private Button buttonAddParameter;

		private Label label3;

		private ListBox listBoxParameters;

		private Button buttonDeleteRelation;

		private Button buttonEditRelation;

		private Button buttonAddRelation;

		private Button buttonDeleteTable;

		private Button buttonEditTable;

		private Button buttonAddTable;

		private Label label2;

		private Label label1;

		private ListBox listBoxRelations;

		private ListBox listBoxTables;

		private UltraTabPageControl ultraTabPageControl1;

		private Label label4;

		private ComboBox comboBoxDrillDown;

		private Panel panelActionTransaction;

		private TextBox textBoxActionTRVoucher;

		private Label label6;

		private TextBox textBoxActionTRSysDocID;

		private Label label5;

		private Panel panelActionCard;

		private Label label7;

		private TextBox textBoxDrilCardID;

		private Label label8;

		private CardTypesComboBox comboBoxCardTypes;

		private Panel panelActionSmartList;

		private Label label13;

		private TextBox textBoxParm4;

		private TextBox textBoxParm3;

		private Label label11;

		private Label label12;

		private TextBox textBoxParm2;

		private Label label9;

		private TextBox textBoxParm1;

		private Label label10;

		private CheckBox checkBoxIsSubReport;

		private SubSmartListComboBox comboBoxSubReport;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private CheckBox checkBoxTransactionPreview;

		private GroupBox groupBoxExternalRpt;

		private ExternalReportCategoryComboBox comboBoxExCategory;

		private MMTextBox textBoxExName;

		private GroupBox groupBoxSmartList;

		private XPButton buttonAttach;

		private Label label16;

		private Label label15;

		private Label label14;

		private Label label18;

		private Label label17;

		private XPButton buttonUsers;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonImport;

		private SaveFileDialog saveFileDialog1;

		private OpenFileDialog openFileDialog1;

		private TextBox textBoxDisplayNote;

		private Label label20;

		private TextBox textBoxNote;

		private Label label19;

		private CheckBox checkBoxHideFilter;

		private Panel panelNote;

		private CheckBox checkBoxSetDateEqualTo;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		public string SetReport
		{
			get
			{
				return setreport;
			}
			set
			{
				setreport = value;
			}
		}

		public bool SetGroup
		{
			set
			{
				if (setreport == "")
				{
					groupBoxExternalRpt.Visible = false;
					groupBoxSmartList.Visible = true;
					checkBoxIsSubReport.Visible = true;
					textBoxName.Visible = !comboBoxCategory.Visible;
					textBoxExName.Visible = comboBoxExCategory.Visible;
					panelNote.Visible = true;
					panelNote.Location = new Point(11, 107);
					return;
				}
				UltraTabsCollection tabs = ultraTabControl1.Tabs;
				groupBoxExternalRpt.Visible = true;
				groupBoxSmartList.Visible = false;
				tabs[0].Visible = true;
				UltraTab ultraTab = tabs[1];
				bool visible = tabs[2].Visible = false;
				ultraTab.Visible = visible;
				checkBoxIsSubReport.Visible = false;
				MMTextBox mMTextBox = textBoxName;
				visible = (comboBoxCategory.Visible = checkBoxIsSubReport.Visible);
				mMTextBox.Visible = visible;
				textBoxExName.Visible = !comboBoxExCategory.Visible;
				groupBoxExternalRpt.Location = new Point(8, -1);
				panelNote.Visible = false;
				panelNote.Location = new Point(11, 107);
			}
		}

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
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
			}
		}

		public SmartListDetailsForm()
		{
			InitializeComponent();
			comboBoxCardTypes.LoadData();
			comboBoxCategory.LoadData(isReferesh: true);
			comboBoxSubReport.LoadData();
			comboBoxExCategory.LoadData(isReferesh: true);
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += SmartListDetailsForm_Load;
			comboBoxDrillDown.SelectedIndexChanged += comboBoxDrillDown_SelectedIndexChanged;
		}

		private void comboBoxDrillDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			Panel panel = panelActionCard;
			Panel panel2 = panelActionTransaction;
			bool flag2 = panelActionSmartList.Visible = false;
			bool visible = panel2.Visible = flag2;
			panel.Visible = visible;
			if (comboBoxDrillDown.SelectedIndex == 1)
			{
				panelActionCard.Visible = true;
			}
			else if (comboBoxDrillDown.SelectedIndex == 2)
			{
				panelActionTransaction.Visible = true;
			}
			else if (comboBoxDrillDown.SelectedIndex == 3)
			{
				panelActionSmartList.Visible = true;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new SmartListData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SmartListTable.Rows[0] : currentData.SmartListTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SmartListName"] = textBoxName.Text.Trim();
				dataRow["Query"] = "";
				dataRow["CategoryID"] = comboBoxCategory.SelectedID;
				dataRow["IsSubReport"] = checkBoxIsSubReport.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["DisplayNote"] = textBoxDisplayNote.Text;
				dataRow["IsHideDateFilter"] = bool.Parse(checkBoxHideFilter.Checked.ToString());
				dataRow["IsSetDateEqualTo"] = bool.Parse(checkBoxSetDateEqualTo.Checked.ToString());
				int selectedIndex = comboBoxDrillDown.SelectedIndex;
				dataRow["DrillAction"] = selectedIndex;
				switch (selectedIndex)
				{
				case 1:
					dataRow["DrillCardIDField"] = textBoxDrilCardID.Text;
					dataRow["DrillCardTypeID"] = comboBoxCardTypes.SelectedID;
					break;
				case 2:
					dataRow["DrillTransactionSysDocIDField"] = textBoxActionTRSysDocID.Text;
					dataRow["DrillTransactionVoucherIDField"] = textBoxActionTRVoucher.Text;
					dataRow["IsPreview"] = checkBoxTransactionPreview.Checked;
					break;
				case 3:
					dataRow["DrillSubReportID"] = comboBoxSubReport.SelectedID;
					dataRow["DrillParm1"] = (textBoxParm1.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm1.Text));
					dataRow["DrillParm2"] = (textBoxParm2.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm2.Text));
					dataRow["DrillParm3"] = (textBoxParm3.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm3.Text));
					dataRow["DrillParm4"] = (textBoxParm4.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm4.Text));
					break;
				}
				CustomReport customReport = new CustomReport();
				foreach (object item4 in listBoxTables.Items)
				{
					CustomReportTable item = item4 as CustomReportTable;
					customReport.Tables.Add(item);
				}
				foreach (object item5 in listBoxRelations.Items)
				{
					ReportRelation item2 = item5 as ReportRelation;
					customReport.Relations.Add(item2);
				}
				foreach (object item6 in listBoxParameters.Items)
				{
					ReportParameter item3 = item6 as ReportParameter;
					customReport.Parameters.Add(item3);
				}
				MemoryStream memoryStream = Global.SerializeToStream(customReport);
				dataRow["ReportData"] = memoryStream.ToArray();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.SmartListTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetExternalData()
		{
			try
			{
				if (currentExtData == null || isNewRecord)
				{
					currentExtData = new ExternalReportData();
				}
				DataRow dataRow = (!isNewRecord) ? currentExtData.ExternalReportTable.Rows[0] : currentExtData.ExternalReportTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ExternalReportName"] = textBoxExName.Text.Trim();
				dataRow["Query"] = "";
				dataRow["CategoryID"] = comboBoxExCategory.SelectedID;
				dataRow["IsSubReport"] = checkBoxIsSubReport.Checked;
				int selectedIndex = comboBoxDrillDown.SelectedIndex;
				dataRow["DrillAction"] = selectedIndex;
				switch (selectedIndex)
				{
				case 1:
					dataRow["DrillCardIDField"] = textBoxDrilCardID.Text;
					dataRow["DrillCardTypeID"] = comboBoxCardTypes.SelectedID;
					break;
				case 2:
					dataRow["DrillTransactionSysDocIDField"] = textBoxActionTRSysDocID.Text;
					dataRow["DrillTransactionVoucherIDField"] = textBoxActionTRVoucher.Text;
					dataRow["IsPreview"] = checkBoxTransactionPreview.Checked;
					break;
				case 3:
					dataRow["DrillSubReportID"] = comboBoxSubReport.SelectedID;
					dataRow["DrillParm1"] = (textBoxParm1.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm1.Text));
					dataRow["DrillParm2"] = (textBoxParm2.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm2.Text));
					dataRow["DrillParm3"] = (textBoxParm3.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm3.Text));
					dataRow["DrillParm4"] = (textBoxParm4.Text.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)textBoxParm4.Text));
					break;
				}
				CustomReport customReport = new CustomReport();
				foreach (object item4 in listBoxTables.Items)
				{
					CustomReportTable item = item4 as CustomReportTable;
					customReport.Tables.Add(item);
				}
				foreach (object item5 in listBoxRelations.Items)
				{
					ReportRelation item2 = item5 as ReportRelation;
					customReport.Relations.Add(item2);
				}
				foreach (object item6 in listBoxParameters.Items)
				{
					ReportParameter item3 = item6 as ReportParameter;
					customReport.Parameters.Add(item3);
				}
				MemoryStream memoryStream = Global.SerializeToStream(customReport);
				dataRow["ReportData"] = memoryStream.ToArray();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentExtData.ExternalReportTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void FillExternalData()
		{
			if (currentExtData != null && currentExtData.Tables.Count != 0 && currentExtData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentExtData.Tables[0].Rows[0];
				textBoxExName.Text = dataRow["ExternalReportName"].ToString();
				textBoxExName.ReadOnly = true;
				comboBoxExCategory.SelectedID = dataRow["CategoryID"].ToString();
				buttonAttach.Enabled = true;
				buttonUsers.Enabled = true;
				if (!dataRow["DrillAction"].IsDBNullOrEmpty())
				{
					comboBoxDrillDown.SelectedIndex = int.Parse(dataRow["DrillAction"].ToString());
				}
				else
				{
					comboBoxDrillDown.SelectedIndex = 0;
				}
				textBoxDrilCardID.Text = dataRow["DrillCardIDField"].ToString();
				if (!dataRow["DrillCardTypeID"].IsDBNullOrEmpty())
				{
					comboBoxCardTypes.SelectedID = int.Parse(dataRow["DrillCardTypeID"].ToString());
				}
				else
				{
					comboBoxCardTypes.SelectedIndex = 0;
				}
				textBoxActionTRSysDocID.Text = dataRow["DrillTransactionSysDocIDField"].ToString();
				textBoxActionTRVoucher.Text = dataRow["DrillTransactionVoucherIDField"].ToString();
				if (!dataRow["IsPreview"].IsDBNullOrEmpty())
				{
					checkBoxTransactionPreview.Checked = bool.Parse(dataRow["IsPreview"].ToString());
				}
				else
				{
					checkBoxTransactionPreview.Checked = false;
				}
				if (!dataRow["DrillSubReportID"].IsDBNullOrEmpty())
				{
					comboBoxSubReport.SelectedID = int.Parse(dataRow["DrillSubReportID"].ToString());
				}
				else
				{
					comboBoxSubReport.Clear();
				}
				textBoxParm1.Text = dataRow["DrillParm1"].ToString();
				textBoxParm2.Text = dataRow["DrillParm2"].ToString();
				textBoxParm3.Text = dataRow["DrillParm3"].ToString();
				textBoxParm4.Text = dataRow["DrillParm4"].ToString();
				byte[] array = null;
				if (dataRow["ReportData"] != DBNull.Value)
				{
					array = (byte[])dataRow["ReportData"];
					currentReport = (CustomReport)Global.DeserializeFromStream(array);
				}
				listBoxTables.Items.Clear();
				if (currentReport != null && currentReport.Tables.Count > 0)
				{
					foreach (CustomReportTable table in currentReport.Tables)
					{
						listBoxTables.Items.Add(table);
					}
				}
				else if (dataRow["Query"] != DBNull.Value && dataRow["Query"].ToString() != "")
				{
					CustomReportTable customReportTable = new CustomReportTable();
					customReportTable.query = dataRow["Query"].ToString();
					customReportTable.TableName = "Report";
					listBoxTables.Items.Add(customReportTable);
				}
				listBoxRelations.Items.Clear();
				if (currentReport != null && currentReport.Relations != null && currentReport.Relations.Count > 0)
				{
					foreach (ReportRelation relation in currentReport.Relations)
					{
						listBoxRelations.Items.Add(relation);
					}
				}
				listBoxParameters.Items.Clear();
				if (currentReport != null && currentReport.Parameters.Count > 0)
				{
					foreach (ReportParameter parameter in currentReport.Parameters)
					{
						listBoxParameters.Items.Add(parameter);
					}
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					if (SetReport == "")
					{
						currentData = Factory.SmartListSystem.GetSmartListByID(id.Trim());
					}
					else
					{
						currentExtData = Factory.ExternalReportSystem.GetExternalReportByID(id.Trim());
					}
					if (SetReport == "")
					{
						if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
						{
							FillData();
							goto IL_011d;
						}
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						if (currentExtData != null && currentExtData.Tables.Count != 0 && currentExtData.Tables[0].Rows.Count != 0)
						{
							FillExternalData();
							goto IL_011d;
						}
						ClearForm();
						IsNewRecord = true;
					}
				}
				goto end_IL_0000;
				IL_011d:
				IsNewRecord = false;
				formManager.ResetDirty();
				end_IL_0000:;
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
				textBoxName.Text = dataRow["SmartListName"].ToString();
				comboBoxCategory.SelectedID = dataRow["CategoryID"].ToString();
				if (!dataRow["DrillAction"].IsDBNullOrEmpty())
				{
					comboBoxDrillDown.SelectedIndex = int.Parse(dataRow["DrillAction"].ToString());
				}
				else
				{
					comboBoxDrillDown.SelectedIndex = 0;
				}
				if (!dataRow["IsSubReport"].IsDBNullOrEmpty())
				{
					checkBoxIsSubReport.Checked = bool.Parse(dataRow["IsSubReport"].ToString());
				}
				else
				{
					checkBoxIsSubReport.Checked = false;
				}
				textBoxDrilCardID.Text = dataRow["DrillCardIDField"].ToString();
				if (!dataRow["DrillCardTypeID"].IsDBNullOrEmpty())
				{
					comboBoxCardTypes.SelectedID = int.Parse(dataRow["DrillCardTypeID"].ToString());
				}
				else
				{
					comboBoxCardTypes.SelectedIndex = 0;
				}
				textBoxActionTRSysDocID.Text = dataRow["DrillTransactionSysDocIDField"].ToString();
				textBoxActionTRVoucher.Text = dataRow["DrillTransactionVoucherIDField"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxDisplayNote.Text = dataRow["DisplayNote"].ToString();
				if (!dataRow["IsHideDateFilter"].IsDBNullOrEmpty())
				{
					checkBoxHideFilter.Checked = bool.Parse(dataRow["IsHideDateFilter"].ToString());
				}
				else
				{
					checkBoxHideFilter.Checked = false;
				}
				if (!dataRow["IsSetDateEqualTo"].IsDBNullOrEmpty())
				{
					checkBoxSetDateEqualTo.Checked = bool.Parse(dataRow["IsSetDateEqualTo"].ToString());
				}
				else
				{
					checkBoxSetDateEqualTo.Checked = false;
				}
				if (!dataRow["IsPreview"].IsDBNullOrEmpty())
				{
					checkBoxTransactionPreview.Checked = bool.Parse(dataRow["IsPreview"].ToString());
				}
				else
				{
					checkBoxTransactionPreview.Checked = false;
				}
				if (!dataRow["DrillSubReportID"].IsDBNullOrEmpty())
				{
					comboBoxSubReport.SelectedID = int.Parse(dataRow["DrillSubReportID"].ToString());
				}
				else
				{
					comboBoxSubReport.Clear();
				}
				textBoxParm1.Text = dataRow["DrillParm1"].ToString();
				textBoxParm2.Text = dataRow["DrillParm2"].ToString();
				textBoxParm3.Text = dataRow["DrillParm3"].ToString();
				textBoxParm4.Text = dataRow["DrillParm4"].ToString();
				byte[] array = null;
				if (dataRow["ReportData"] != DBNull.Value)
				{
					array = (byte[])dataRow["ReportData"];
					currentReport = (CustomReport)Global.DeserializeFromStream(array);
				}
				listBoxTables.Items.Clear();
				if (currentReport != null && currentReport.Tables.Count > 0)
				{
					foreach (CustomReportTable table in currentReport.Tables)
					{
						listBoxTables.Items.Add(table);
					}
				}
				else if (dataRow["Query"] != DBNull.Value && dataRow["Query"].ToString() != "")
				{
					CustomReportTable customReportTable = new CustomReportTable();
					customReportTable.query = dataRow["Query"].ToString();
					customReportTable.TableName = "Report";
					listBoxTables.Items.Add(customReportTable);
				}
				listBoxRelations.Items.Clear();
				if (currentReport != null && currentReport.Relations != null && currentReport.Relations.Count > 0)
				{
					foreach (ReportRelation relation in currentReport.Relations)
					{
						listBoxRelations.Items.Add(relation);
					}
				}
				listBoxParameters.Items.Clear();
				if (currentReport != null && currentReport.Parameters.Count > 0)
				{
					foreach (ReportParameter parameter in currentReport.Parameters)
					{
						listBoxParameters.Items.Add(parameter);
					}
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
			if (SetReport == "")
			{
				if (!GetData())
				{
					return false;
				}
			}
			else if (!GetExternalData())
			{
				return false;
			}
			try
			{
				bool flag = (setreport == "") ? ((!isNewRecord) ? Factory.SmartListSystem.UpdateSmartList(currentData) : Factory.SmartListSystem.CreateSmartList(currentData)) : ((!isNewRecord) ? Factory.ExternalReportSystem.UpdateExternalReport(currentExtData) : Factory.ExternalReportSystem.CreateExternalReport(currentExtData));
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
			if (SetReport == "")
			{
				if (textBoxName.Text.Trim().Length == 0 || comboBoxCategory.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please enter required fields.");
					return false;
				}
			}
			else if (textBoxExName.Text.Trim().Length == 0 || comboBoxExCategory.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (listBoxTables.Items.Count > 0)
			{
				listBoxTables.Items[0].ToString();
				string value = "fox";
				"The quick brown fox jumps over the lazy dog".Contains(value);
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
			textBoxName.Focus();
			listBoxTables.Items.Clear();
			listBoxRelations.Items.Clear();
			listBoxParameters.Items.Clear();
			comboBoxCardTypes.SelectedIndex = 0;
			textBoxActionTRVoucher.Clear();
			textBoxActionTRSysDocID.Clear();
			textBoxDrilCardID.Clear();
			comboBoxDrillDown.SelectedIndex = 0;
			textBoxExName.Clear();
			textBoxNote.Clear();
			textBoxDisplayNote.Clear();
			checkBoxHideFilter.Checked = false;
			checkBoxSetDateEqualTo.Checked = false;
			formManager.ResetDirty();
		}

		private void SmartListGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SmartListGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("SmartList", "SmartListID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("SmartList", "SmartListID"));
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

		private void SmartListDetailsForm_Load(object sender, EventArgs e)
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

		private void mmLabel4_Click(object sender, EventArgs e)
		{
		}

		private void SmartListDetailsForm_Load_1(object sender, EventArgs e)
		{
		}

		private void buttonAddTable_Click(object sender, EventArgs e)
		{
			AddTableDialog addTableDialog = new AddTableDialog();
			if (addTableDialog.ShowDialog() == DialogResult.OK)
			{
				CustomReportTable customReportTable = new CustomReportTable();
				customReportTable.tableName = addTableDialog.TableName;
				customReportTable.query = addTableDialog.Query;
				listBoxTables.Items.Add(customReportTable);
				SetDateFilter(addTableDialog.Query);
			}
		}

		private void buttonEditTable_Click(object sender, EventArgs e)
		{
			if (listBoxTables.SelectedItem != null)
			{
				AddTableDialog addTableDialog = new AddTableDialog();
				addTableDialog.EditTable((CustomReportTable)listBoxTables.SelectedItem);
				if (addTableDialog.ShowDialog() == DialogResult.OK)
				{
					int index = listBoxTables.Items.IndexOf(listBoxTables.SelectedItem);
					listBoxTables.Items.Remove(listBoxTables.SelectedItem);
					listBoxTables.Items.Insert(index, addTableDialog.ReportTable);
					SetDateFilter(addTableDialog.Query);
				}
			}
		}

		private void buttonDeleteTable_Click(object sender, EventArgs e)
		{
			if (listBoxTables.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the table?") == DialogResult.Yes)
			{
				listBoxTables.Items.Remove(listBoxTables.SelectedItem);
			}
		}

		private void buttonAddRelation_Click(object sender, EventArgs e)
		{
			CustomReport customReport = new CustomReport();
			foreach (object item2 in listBoxTables.Items)
			{
				CustomReportTable item = item2 as CustomReportTable;
				customReport.Tables.Add(item);
			}
			AddRelationDialog addRelationDialog = new AddRelationDialog();
			addRelationDialog.LoadReportData(customReport);
			if (addRelationDialog.ShowDialog() == DialogResult.OK)
			{
				listBoxRelations.Items.Add(addRelationDialog.Relationship);
			}
		}

		private void buttonEditRelation_Click(object sender, EventArgs e)
		{
			if (listBoxRelations.SelectedItem != null)
			{
				CustomReport customReport = new CustomReport();
				foreach (object item2 in listBoxTables.Items)
				{
					CustomReportTable item = item2 as CustomReportTable;
					customReport.Tables.Add(item);
				}
				AddRelationDialog addRelationDialog = new AddRelationDialog();
				addRelationDialog.LoadReportData(customReport);
				addRelationDialog.EditRelation((ReportRelation)listBoxRelations.SelectedItem);
				if (addRelationDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxRelations.Items.Remove(listBoxRelations.SelectedItem);
					listBoxRelations.Items.Add(addRelationDialog.Relationship);
				}
			}
		}

		private void buttonDeleteRelation_Click(object sender, EventArgs e)
		{
			if (listBoxRelations.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the relationship?") == DialogResult.Yes)
			{
				listBoxRelations.Items.Remove(listBoxRelations.SelectedItem);
			}
		}

		private void buttonAddParameter_Click(object sender, EventArgs e)
		{
			AddParameterDialog addParameterDialog = new AddParameterDialog();
			if (addParameterDialog.ShowDialog() == DialogResult.OK)
			{
				ReportParameter reportParameter = new ReportParameter();
				reportParameter.ParameterName = addParameterDialog.ParameterName;
				reportParameter.ParameterType = addParameterDialog.DataType;
				listBoxParameters.Items.Add(reportParameter);
			}
		}

		private void buttonEditParameter_Click(object sender, EventArgs e)
		{
			if (listBoxParameters.SelectedItem != null)
			{
				AddParameterDialog addParameterDialog = new AddParameterDialog();
				addParameterDialog.EditParameter((ReportParameter)listBoxParameters.SelectedItem);
				if (addParameterDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxParameters.Items.Remove(listBoxParameters.SelectedItem);
					listBoxParameters.Items.Add(addParameterDialog.ReportParameter);
				}
			}
		}

		private void buttonDeleteParameter_Click(object sender, EventArgs e)
		{
			if (listBoxParameters.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the parameter?") == DialogResult.Yes)
			{
				listBoxParameters.Items.Remove(listBoxParameters.SelectedItem);
			}
		}

		private void comboBoxDrillDown_SelectedIndexChanged_1(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			SmartListDetailsForm smartListDetailsForm = new SmartListDetailsForm();
			smartListDetailsForm.Show();
			smartListDetailsForm.LoadData(comboBoxSubReport.SelectedID.ToString());
		}

		private void buttonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				DocManagementForm docManagementForm = new DocManagementForm();
				docManagementForm.EntityID = textBoxExName.Text.Trim();
				docManagementForm.EntitySysDocID = "EXR";
				docManagementForm.EntityName = "EXR";
				docManagementForm.EntityType = EntityTypesEnum.ExternalReports;
				docManagementForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxExName_TextChanged(object sender, EventArgs e)
		{
			string text = textBoxExName.Text;
			if (text.Trim() != "" && text.Trim() != string.Empty)
			{
				buttonAttach.Enabled = true;
				buttonUsers.Enabled = true;
			}
			else
			{
				buttonAttach.Enabled = false;
				buttonUsers.Enabled = false;
			}
		}

		private void buttonUsers_Click(object sender, EventArgs e)
		{
			SysDocEntityLinkDialog sysDocEntityLinkDialog = new SysDocEntityLinkDialog();
			sysDocEntityLinkDialog.ReportID = textBoxExName.Text;
			sysDocEntityLinkDialog.EntityType = SysDocEntityTypes.User;
			sysDocEntityLinkDialog.ShowDialog(this);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0)
				{
					saveFileDialog1.AddExtension = true;
					saveFileDialog1.Filter = "Axolon Smartlist|*.axs";
					saveFileDialog1.FileName = currentData.SmartListTable.Rows[0]["SmartListID"].ToString();
					if (saveFileDialog1.ShowDialog() == DialogResult.OK && currentData != null)
					{
						currentData.WriteXml(saveFileDialog1.FileName, XmlWriteMode.WriteSchema);
						ErrorHelper.InformationMessage("Custom gadget exported successfully.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonImport_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog1.Filter = "Axolon Smartlist|*.axs";
				openFileDialog1.FileName = "";
				openFileDialog1.Multiselect = true;
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					currentData = new SmartListData();
					string[] fileNames = openFileDialog1.FileNames;
					foreach (string fileName in fileNames)
					{
						currentData.ReadXml(fileName, XmlReadMode.ReadSchema);
						FillData();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetDateFilter(string qry)
		{
			if (qry.Contains("@FromDate") || qry.Contains("@EndDate"))
			{
				checkBoxHideFilter.Checked = false;
			}
			else
			{
				checkBoxHideFilter.Checked = true;
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.SmartListDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelNote = new System.Windows.Forms.Panel();
			label19 = new System.Windows.Forms.Label();
			textBoxDisplayNote = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label20 = new System.Windows.Forms.Label();
			groupBoxExternalRpt = new System.Windows.Forms.GroupBox();
			buttonUsers = new Micromind.UISupport.XPButton();
			label18 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			buttonAttach = new Micromind.UISupport.XPButton();
			comboBoxExCategory = new Micromind.DataControls.ExternalReportCategoryComboBox();
			textBoxExName = new Micromind.UISupport.MMTextBox();
			checkBoxIsSubReport = new System.Windows.Forms.CheckBox();
			comboBoxCategory = new Micromind.DataControls.SmartListCategoryComboBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			groupBoxSmartList = new System.Windows.Forms.GroupBox();
			checkBoxSetDateEqualTo = new System.Windows.Forms.CheckBox();
			checkBoxHideFilter = new System.Windows.Forms.CheckBox();
			label16 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonDeleteParameter = new System.Windows.Forms.Button();
			buttonEditParameter = new System.Windows.Forms.Button();
			buttonAddParameter = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			listBoxParameters = new System.Windows.Forms.ListBox();
			buttonDeleteRelation = new System.Windows.Forms.Button();
			buttonEditRelation = new System.Windows.Forms.Button();
			buttonAddRelation = new System.Windows.Forms.Button();
			buttonDeleteTable = new System.Windows.Forms.Button();
			buttonEditTable = new System.Windows.Forms.Button();
			buttonAddTable = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			listBoxRelations = new System.Windows.Forms.ListBox();
			listBoxTables = new System.Windows.Forms.ListBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label4 = new System.Windows.Forms.Label();
			comboBoxDrillDown = new System.Windows.Forms.ComboBox();
			panelActionTransaction = new System.Windows.Forms.Panel();
			checkBoxTransactionPreview = new System.Windows.Forms.CheckBox();
			textBoxActionTRVoucher = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxActionTRSysDocID = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			panelActionCard = new System.Windows.Forms.Panel();
			comboBoxCardTypes = new Micromind.DataControls.CardTypesComboBox();
			label7 = new System.Windows.Forms.Label();
			textBoxDrilCardID = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			panelActionSmartList = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSubReport = new Micromind.DataControls.SubSmartListComboBox();
			label13 = new System.Windows.Forms.Label();
			textBoxParm4 = new System.Windows.Forms.TextBox();
			textBoxParm3 = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			textBoxParm2 = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			textBoxParm1 = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
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
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			panelNote.SuspendLayout();
			groupBoxExternalRpt.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxExCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			groupBoxSmartList.SuspendLayout();
			tabPageDetails.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			panelActionTransaction.SuspendLayout();
			panelActionCard.SuspendLayout();
			panelActionSmartList.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			toolStrip1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(panelNote);
			tabPageGeneral.Controls.Add(groupBoxExternalRpt);
			tabPageGeneral.Controls.Add(checkBoxIsSubReport);
			tabPageGeneral.Controls.Add(comboBoxCategory);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(groupBoxSmartList);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(767, 432);
			panelNote.Controls.Add(label19);
			panelNote.Controls.Add(textBoxDisplayNote);
			panelNote.Controls.Add(textBoxNote);
			panelNote.Controls.Add(label20);
			panelNote.Location = new System.Drawing.Point(9, 205);
			panelNote.Name = "panelNote";
			panelNote.Size = new System.Drawing.Size(526, 172);
			panelNote.TabIndex = 25;
			label19.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(3, 8);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(33, 13);
			label19.TabIndex = 22;
			label19.Text = "Note:";
			textBoxDisplayNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxDisplayNote.Location = new System.Drawing.Point(103, 88);
			textBoxDisplayNote.MaxLength = 4000;
			textBoxDisplayNote.Multiline = true;
			textBoxDisplayNote.Name = "textBoxDisplayNote";
			textBoxDisplayNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxDisplayNote.Size = new System.Drawing.Size(420, 79);
			textBoxDisplayNote.TabIndex = 1;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(103, 5);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(420, 79);
			textBoxNote.TabIndex = 0;
			label20.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(3, 91);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(70, 13);
			label20.TabIndex = 24;
			label20.Text = "Display Note:";
			groupBoxExternalRpt.Controls.Add(buttonUsers);
			groupBoxExternalRpt.Controls.Add(label18);
			groupBoxExternalRpt.Controls.Add(label17);
			groupBoxExternalRpt.Controls.Add(buttonAttach);
			groupBoxExternalRpt.Controls.Add(comboBoxExCategory);
			groupBoxExternalRpt.Controls.Add(textBoxExName);
			groupBoxExternalRpt.Location = new System.Drawing.Point(11, 107);
			groupBoxExternalRpt.Name = "groupBoxExternalRpt";
			groupBoxExternalRpt.Size = new System.Drawing.Size(522, 95);
			groupBoxExternalRpt.TabIndex = 5;
			groupBoxExternalRpt.TabStop = false;
			groupBoxExternalRpt.Text = "External Report";
			buttonUsers.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonUsers.BackColor = System.Drawing.Color.DarkGray;
			buttonUsers.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonUsers.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonUsers.Enabled = false;
			buttonUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonUsers.Location = new System.Drawing.Point(204, 59);
			buttonUsers.Name = "buttonUsers";
			buttonUsers.Size = new System.Drawing.Size(96, 24);
			buttonUsers.TabIndex = 3;
			buttonUsers.Text = "Users...";
			buttonUsers.UseVisualStyleBackColor = false;
			buttonUsers.Click += new System.EventHandler(buttonUsers_Click);
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label18.Location = new System.Drawing.Point(1, 39);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(101, 13);
			label18.TabIndex = 9;
			label18.Text = "Category Name :";
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label17.Location = new System.Drawing.Point(2, 17);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(89, 13);
			label17.TabIndex = 9;
			label17.Text = "Report Name :";
			buttonAttach.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAttach.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonAttach.BackColor = System.Drawing.Color.DarkGray;
			buttonAttach.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAttach.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAttach.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAttach.Enabled = false;
			buttonAttach.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAttach.Location = new System.Drawing.Point(102, 59);
			buttonAttach.Name = "buttonAttach";
			buttonAttach.Size = new System.Drawing.Size(96, 24);
			buttonAttach.TabIndex = 2;
			buttonAttach.Text = "&Attach";
			buttonAttach.UseVisualStyleBackColor = false;
			buttonAttach.Click += new System.EventHandler(buttonAttach_Click);
			comboBoxExCategory.Assigned = false;
			comboBoxExCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExCategory.CustomReportFieldName = "";
			comboBoxExCategory.CustomReportKey = "";
			comboBoxExCategory.CustomReportValueType = 1;
			comboBoxExCategory.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExCategory.DisplayLayout.Appearance = appearance;
			comboBoxExCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExCategory.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxExCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxExCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExCategory.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExCategory.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxExCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExCategory.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExCategory.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxExCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExCategory.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExCategory.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxExCategory.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxExCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxExCategory.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxExCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxExCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExCategory.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxExCategory.Editable = true;
			comboBoxExCategory.FilterString = "";
			comboBoxExCategory.HasAllAccount = false;
			comboBoxExCategory.HasCustom = false;
			comboBoxExCategory.IsDataLoaded = false;
			comboBoxExCategory.Location = new System.Drawing.Point(102, 36);
			comboBoxExCategory.MaxDropDownItems = 12;
			comboBoxExCategory.Name = "comboBoxExCategory";
			comboBoxExCategory.ShowInactiveItems = false;
			comboBoxExCategory.ShowQuickAdd = true;
			comboBoxExCategory.Size = new System.Drawing.Size(376, 20);
			comboBoxExCategory.TabIndex = 1;
			comboBoxExCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxExName.BackColor = System.Drawing.Color.White;
			textBoxExName.CustomReportFieldName = "";
			textBoxExName.CustomReportKey = "";
			textBoxExName.CustomReportValueType = 1;
			textBoxExName.IsComboTextBox = false;
			textBoxExName.IsModified = false;
			textBoxExName.Location = new System.Drawing.Point(102, 12);
			textBoxExName.MaxLength = 64;
			textBoxExName.Name = "textBoxExName";
			textBoxExName.Size = new System.Drawing.Size(376, 20);
			textBoxExName.TabIndex = 0;
			textBoxExName.TextChanged += new System.EventHandler(textBoxExName_TextChanged);
			checkBoxIsSubReport.AutoSize = true;
			checkBoxIsSubReport.Location = new System.Drawing.Point(114, 60);
			checkBoxIsSubReport.Name = "checkBoxIsSubReport";
			checkBoxIsSubReport.Size = new System.Drawing.Size(139, 17);
			checkBoxIsSubReport.TabIndex = 2;
			checkBoxIsSubReport.Text = "Use as drill down details";
			checkBoxIsSubReport.UseVisualStyleBackColor = true;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance13;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(114, 34);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.ShowQuickAdd = true;
			comboBoxCategory.Size = new System.Drawing.Size(376, 20);
			comboBoxCategory.TabIndex = 1;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(114, 11);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(376, 20);
			textBoxName.TabIndex = 0;
			groupBoxSmartList.Controls.Add(checkBoxSetDateEqualTo);
			groupBoxSmartList.Controls.Add(checkBoxHideFilter);
			groupBoxSmartList.Controls.Add(label16);
			groupBoxSmartList.Controls.Add(label15);
			groupBoxSmartList.Controls.Add(label14);
			groupBoxSmartList.Location = new System.Drawing.Point(9, -1);
			groupBoxSmartList.Name = "groupBoxSmartList";
			groupBoxSmartList.Size = new System.Drawing.Size(526, 98);
			groupBoxSmartList.TabIndex = 3;
			groupBoxSmartList.TabStop = false;
			groupBoxSmartList.Text = "Smart List";
			checkBoxSetDateEqualTo.AutoSize = true;
			checkBoxSetDateEqualTo.Location = new System.Drawing.Point(363, 61);
			checkBoxSetDateEqualTo.Name = "checkBoxSetDateEqualTo";
			checkBoxSetDateEqualTo.Size = new System.Drawing.Size(107, 17);
			checkBoxSetDateEqualTo.TabIndex = 1;
			checkBoxSetDateEqualTo.Text = "Set date equal to";
			checkBoxSetDateEqualTo.UseVisualStyleBackColor = true;
			checkBoxHideFilter.AutoSize = true;
			checkBoxHideFilter.Location = new System.Drawing.Point(250, 61);
			checkBoxHideFilter.Name = "checkBoxHideFilter";
			checkBoxHideFilter.Size = new System.Drawing.Size(107, 17);
			checkBoxHideFilter.TabIndex = 0;
			checkBoxHideFilter.Text = "Disable date filter";
			checkBoxHideFilter.UseVisualStyleBackColor = true;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label16.Location = new System.Drawing.Point(3, 39);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(101, 13);
			label16.TabIndex = 2;
			label16.Text = "Category Name :";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(243, 43);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(41, 13);
			label15.TabIndex = 1;
			label15.Text = "label15";
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label14.Location = new System.Drawing.Point(3, 19);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(89, 13);
			label14.TabIndex = 0;
			label14.Text = "Report Name :";
			tabPageDetails.Controls.Add(buttonDeleteParameter);
			tabPageDetails.Controls.Add(buttonEditParameter);
			tabPageDetails.Controls.Add(buttonAddParameter);
			tabPageDetails.Controls.Add(label3);
			tabPageDetails.Controls.Add(listBoxParameters);
			tabPageDetails.Controls.Add(buttonDeleteRelation);
			tabPageDetails.Controls.Add(buttonEditRelation);
			tabPageDetails.Controls.Add(buttonAddRelation);
			tabPageDetails.Controls.Add(buttonDeleteTable);
			tabPageDetails.Controls.Add(buttonEditTable);
			tabPageDetails.Controls.Add(buttonAddTable);
			tabPageDetails.Controls.Add(label2);
			tabPageDetails.Controls.Add(label1);
			tabPageDetails.Controls.Add(listBoxRelations);
			tabPageDetails.Controls.Add(listBoxTables);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(767, 432);
			buttonDeleteParameter.Location = new System.Drawing.Point(614, 85);
			buttonDeleteParameter.Name = "buttonDeleteParameter";
			buttonDeleteParameter.Size = new System.Drawing.Size(75, 23);
			buttonDeleteParameter.TabIndex = 12;
			buttonDeleteParameter.Text = "Delete";
			buttonDeleteParameter.UseVisualStyleBackColor = true;
			buttonDeleteParameter.Visible = false;
			buttonDeleteParameter.Click += new System.EventHandler(buttonDeleteParameter_Click);
			buttonEditParameter.Location = new System.Drawing.Point(614, 56);
			buttonEditParameter.Name = "buttonEditParameter";
			buttonEditParameter.Size = new System.Drawing.Size(75, 23);
			buttonEditParameter.TabIndex = 11;
			buttonEditParameter.Text = "Edit";
			buttonEditParameter.UseVisualStyleBackColor = true;
			buttonEditParameter.Visible = false;
			buttonEditParameter.Click += new System.EventHandler(buttonEditParameter_Click);
			buttonAddParameter.Location = new System.Drawing.Point(614, 27);
			buttonAddParameter.Name = "buttonAddParameter";
			buttonAddParameter.Size = new System.Drawing.Size(75, 23);
			buttonAddParameter.TabIndex = 10;
			buttonAddParameter.Text = "Add";
			buttonAddParameter.UseVisualStyleBackColor = true;
			buttonAddParameter.Visible = false;
			buttonAddParameter.Click += new System.EventHandler(buttonAddParameter_Click);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(387, 11);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(63, 13);
			label3.TabIndex = 9;
			label3.Text = "Parameters:";
			label3.Visible = false;
			listBoxParameters.FormattingEnabled = true;
			listBoxParameters.Location = new System.Drawing.Point(390, 27);
			listBoxParameters.Name = "listBoxParameters";
			listBoxParameters.Size = new System.Drawing.Size(218, 160);
			listBoxParameters.TabIndex = 8;
			listBoxParameters.Visible = false;
			buttonDeleteRelation.Location = new System.Drawing.Point(239, 270);
			buttonDeleteRelation.Name = "buttonDeleteRelation";
			buttonDeleteRelation.Size = new System.Drawing.Size(75, 23);
			buttonDeleteRelation.TabIndex = 7;
			buttonDeleteRelation.Text = "Delete";
			buttonDeleteRelation.UseVisualStyleBackColor = true;
			buttonDeleteRelation.Click += new System.EventHandler(buttonDeleteRelation_Click);
			buttonEditRelation.Location = new System.Drawing.Point(239, 241);
			buttonEditRelation.Name = "buttonEditRelation";
			buttonEditRelation.Size = new System.Drawing.Size(75, 23);
			buttonEditRelation.TabIndex = 6;
			buttonEditRelation.Text = "Edit";
			buttonEditRelation.UseVisualStyleBackColor = true;
			buttonEditRelation.Click += new System.EventHandler(buttonEditRelation_Click);
			buttonAddRelation.Location = new System.Drawing.Point(239, 212);
			buttonAddRelation.Name = "buttonAddRelation";
			buttonAddRelation.Size = new System.Drawing.Size(75, 23);
			buttonAddRelation.TabIndex = 5;
			buttonAddRelation.Text = "Add";
			buttonAddRelation.UseVisualStyleBackColor = true;
			buttonAddRelation.Click += new System.EventHandler(buttonAddRelation_Click);
			buttonDeleteTable.Location = new System.Drawing.Point(239, 85);
			buttonDeleteTable.Name = "buttonDeleteTable";
			buttonDeleteTable.Size = new System.Drawing.Size(75, 23);
			buttonDeleteTable.TabIndex = 4;
			buttonDeleteTable.Text = "Delete";
			buttonDeleteTable.UseVisualStyleBackColor = true;
			buttonDeleteTable.Click += new System.EventHandler(buttonDeleteTable_Click);
			buttonEditTable.Location = new System.Drawing.Point(239, 56);
			buttonEditTable.Name = "buttonEditTable";
			buttonEditTable.Size = new System.Drawing.Size(75, 23);
			buttonEditTable.TabIndex = 3;
			buttonEditTable.Text = "Edit";
			buttonEditTable.UseVisualStyleBackColor = true;
			buttonEditTable.Click += new System.EventHandler(buttonEditTable_Click);
			buttonAddTable.Location = new System.Drawing.Point(239, 27);
			buttonAddTable.Name = "buttonAddTable";
			buttonAddTable.Size = new System.Drawing.Size(75, 23);
			buttonAddTable.TabIndex = 2;
			buttonAddTable.Text = "Add";
			buttonAddTable.UseVisualStyleBackColor = true;
			buttonAddTable.Click += new System.EventHandler(buttonAddTable_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 196);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(80, 13);
			label2.TabIndex = 1;
			label2.Text = "Data Relations:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(42, 13);
			label1.TabIndex = 1;
			label1.Text = "Tables:";
			listBoxRelations.FormattingEnabled = true;
			listBoxRelations.Location = new System.Drawing.Point(15, 212);
			listBoxRelations.Name = "listBoxRelations";
			listBoxRelations.Size = new System.Drawing.Size(218, 134);
			listBoxRelations.TabIndex = 0;
			listBoxTables.FormattingEnabled = true;
			listBoxTables.Location = new System.Drawing.Point(15, 27);
			listBoxTables.Name = "listBoxTables";
			listBoxTables.Size = new System.Drawing.Size(218, 160);
			listBoxTables.TabIndex = 0;
			ultraTabPageControl1.Controls.Add(label4);
			ultraTabPageControl1.Controls.Add(comboBoxDrillDown);
			ultraTabPageControl1.Controls.Add(panelActionTransaction);
			ultraTabPageControl1.Controls.Add(panelActionCard);
			ultraTabPageControl1.Controls.Add(panelActionSmartList);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(767, 432);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(10, 19);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(91, 13);
			label4.TabIndex = 3;
			label4.Text = "Drill Down Action:";
			comboBoxDrillDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDrillDown.FormattingEnabled = true;
			comboBoxDrillDown.Items.AddRange(new object[4]
			{
				"None",
				"Open Card",
				"Open Transaction",
				"Open Smart List"
			});
			comboBoxDrillDown.Location = new System.Drawing.Point(115, 16);
			comboBoxDrillDown.Name = "comboBoxDrillDown";
			comboBoxDrillDown.Size = new System.Drawing.Size(137, 21);
			comboBoxDrillDown.TabIndex = 2;
			comboBoxDrillDown.SelectedIndexChanged += new System.EventHandler(comboBoxDrillDown_SelectedIndexChanged_1);
			panelActionTransaction.Controls.Add(checkBoxTransactionPreview);
			panelActionTransaction.Controls.Add(textBoxActionTRVoucher);
			panelActionTransaction.Controls.Add(label6);
			panelActionTransaction.Controls.Add(textBoxActionTRSysDocID);
			panelActionTransaction.Controls.Add(label5);
			panelActionTransaction.Location = new System.Drawing.Point(4, 43);
			panelActionTransaction.Name = "panelActionTransaction";
			panelActionTransaction.Size = new System.Drawing.Size(356, 106);
			panelActionTransaction.TabIndex = 4;
			panelActionTransaction.Visible = false;
			checkBoxTransactionPreview.AutoSize = true;
			checkBoxTransactionPreview.Location = new System.Drawing.Point(111, 69);
			checkBoxTransactionPreview.Name = "checkBoxTransactionPreview";
			checkBoxTransactionPreview.Size = new System.Drawing.Size(129, 17);
			checkBoxTransactionPreview.TabIndex = 8;
			checkBoxTransactionPreview.Text = "Open as print preview";
			checkBoxTransactionPreview.UseVisualStyleBackColor = true;
			textBoxActionTRVoucher.Location = new System.Drawing.Point(111, 34);
			textBoxActionTRVoucher.Name = "textBoxActionTRVoucher";
			textBoxActionTRVoucher.Size = new System.Drawing.Size(137, 20);
			textBoxActionTRVoucher.TabIndex = 7;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(6, 37);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(92, 13);
			label6.TabIndex = 6;
			label6.Text = "Voucher No Field:";
			textBoxActionTRSysDocID.Location = new System.Drawing.Point(111, 11);
			textBoxActionTRSysDocID.Name = "textBoxActionTRSysDocID";
			textBoxActionTRSysDocID.Size = new System.Drawing.Size(137, 20);
			textBoxActionTRSysDocID.TabIndex = 5;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(6, 14);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(75, 13);
			label5.TabIndex = 4;
			label5.Text = "Sys Doc Field:";
			panelActionCard.Controls.Add(comboBoxCardTypes);
			panelActionCard.Controls.Add(label7);
			panelActionCard.Controls.Add(textBoxDrilCardID);
			panelActionCard.Controls.Add(label8);
			panelActionCard.Location = new System.Drawing.Point(4, 43);
			panelActionCard.Name = "panelActionCard";
			panelActionCard.Size = new System.Drawing.Size(356, 106);
			panelActionCard.TabIndex = 5;
			panelActionCard.Visible = false;
			comboBoxCardTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCardTypes.FormattingEnabled = true;
			comboBoxCardTypes.Location = new System.Drawing.Point(111, 35);
			comboBoxCardTypes.Name = "comboBoxCardTypes";
			comboBoxCardTypes.Size = new System.Drawing.Size(137, 21);
			comboBoxCardTypes.TabIndex = 7;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(6, 37);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(59, 13);
			label7.TabIndex = 6;
			label7.Text = "Card Type:";
			textBoxDrilCardID.Location = new System.Drawing.Point(111, 11);
			textBoxDrilCardID.Name = "textBoxDrilCardID";
			textBoxDrilCardID.Size = new System.Drawing.Size(137, 20);
			textBoxDrilCardID.TabIndex = 5;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(6, 14);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(85, 13);
			label8.TabIndex = 4;
			label8.Text = "Card Code Field:";
			panelActionSmartList.Controls.Add(ultraFormattedLinkLabel1);
			panelActionSmartList.Controls.Add(comboBoxSubReport);
			panelActionSmartList.Controls.Add(label13);
			panelActionSmartList.Controls.Add(textBoxParm4);
			panelActionSmartList.Controls.Add(textBoxParm3);
			panelActionSmartList.Controls.Add(label11);
			panelActionSmartList.Controls.Add(label12);
			panelActionSmartList.Controls.Add(textBoxParm2);
			panelActionSmartList.Controls.Add(label9);
			panelActionSmartList.Controls.Add(textBoxParm1);
			panelActionSmartList.Controls.Add(label10);
			panelActionSmartList.Location = new System.Drawing.Point(4, 44);
			panelActionSmartList.Name = "panelActionSmartList";
			panelActionSmartList.Size = new System.Drawing.Size(356, 211);
			panelActionSmartList.TabIndex = 6;
			panelActionSmartList.Visible = false;
			appearance25.FontData.BoldAsString = "False";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance25;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 5);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel1.TabIndex = 135;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Detail Smart List:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxSubReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSubReport.FormattingEnabled = true;
			comboBoxSubReport.Location = new System.Drawing.Point(111, 2);
			comboBoxSubReport.Name = "comboBoxSubReport";
			comboBoxSubReport.Size = new System.Drawing.Size(137, 21);
			comboBoxSubReport.TabIndex = 0;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(6, 42);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(168, 13);
			label13.TabIndex = 12;
			label13.Text = "Assign columns to the parameters:";
			textBoxParm4.Location = new System.Drawing.Point(111, 134);
			textBoxParm4.Name = "textBoxParm4";
			textBoxParm4.Size = new System.Drawing.Size(137, 20);
			textBoxParm4.TabIndex = 4;
			textBoxParm3.Location = new System.Drawing.Point(111, 111);
			textBoxParm3.Name = "textBoxParm3";
			textBoxParm3.Size = new System.Drawing.Size(137, 20);
			textBoxParm3.TabIndex = 3;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(6, 139);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(48, 13);
			label11.TabIndex = 9;
			label11.Text = "@Parm4";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(6, 116);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(51, 13);
			label12.TabIndex = 8;
			label12.Text = "@Parm3:";
			textBoxParm2.Location = new System.Drawing.Point(111, 89);
			textBoxParm2.Name = "textBoxParm2";
			textBoxParm2.Size = new System.Drawing.Size(137, 20);
			textBoxParm2.TabIndex = 2;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(6, 92);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(48, 13);
			label9.TabIndex = 6;
			label9.Text = "@Parm2";
			textBoxParm1.Location = new System.Drawing.Point(111, 66);
			textBoxParm1.Name = "textBoxParm1";
			textBoxParm1.Size = new System.Drawing.Size(137, 20);
			textBoxParm1.TabIndex = 1;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(6, 69);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(51, 13);
			label10.TabIndex = 4;
			label10.Text = "@Parm1:";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 498);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(795, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(795, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(685, 8);
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
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Location = new System.Drawing.Point(12, 37);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(771, 455);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 17;
			appearance27.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance27;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Data";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "Drill Down";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(767, 432);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
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
				toolStripButton1,
				toolStripButtonImport
			});
			toolStrip1.Location = new System.Drawing.Point(20, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(775, 31);
			toolStrip1.TabIndex = 18;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(36, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Visible = false;
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Visible = false;
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Visible = false;
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Visible = false;
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator1.Visible = false;
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Visible = false;
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator3.Visible = false;
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.Visible = false;
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Visible = false;
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator2.Visible = false;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.ExportToXMLFile48;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(68, 28);
			toolStripButton1.Text = "Export";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonImport.Image = Micromind.ClientUI.Properties.Resources.download_icon;
			toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonImport.Name = "toolStripButtonImport";
			toolStripButtonImport.Size = new System.Drawing.Size(71, 28);
			toolStripButtonImport.Text = "Import";
			toolStripButtonImport.Click += new System.EventHandler(toolStripButtonImport_Click);
			openFileDialog1.FileName = "openFileDialog1";
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(795, 538);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "SmartListDetailsForm";
			Text = "Smart Report";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(SmartListDetailsForm_Load_1);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			panelNote.ResumeLayout(false);
			panelNote.PerformLayout();
			groupBoxExternalRpt.ResumeLayout(false);
			groupBoxExternalRpt.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxExCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			groupBoxSmartList.ResumeLayout(false);
			groupBoxSmartList.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			panelActionTransaction.ResumeLayout(false);
			panelActionTransaction.PerformLayout();
			panelActionCard.ResumeLayout(false);
			panelActionCard.PerformLayout();
			panelActionSmartList.ResumeLayout(false);
			panelActionSmartList.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
